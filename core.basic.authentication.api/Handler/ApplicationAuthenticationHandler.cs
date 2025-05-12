using core.basic.authentication.api.Database;
using core.basic.authentication.api.Infrastructures;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using Microsoft.Identity.Client;
using Microsoft.Net.Http.Headers;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;

namespace core.basic.authentication.api.Handler
{
    public class ApplicationAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private readonly ApplicationDBContext _context;
        public ApplicationAuthenticationHandler(IOptionsMonitor<AuthenticationSchemeOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock, ApplicationDBContext context)
            : base(options, logger, encoder, clock)
        {
            _context = context;
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (!Request.Headers.ContainsKey("Authorization"))
            {
                return AuthenticateResult.Fail("Missing Authorization Header");
            }

            try
            {
                var headerNames = Request.Headers[HeaderNames.Authorization];
                var headerValues = AuthenticationHeaderValue.Parse(headerNames);
                var parameter = Convert.FromBase64String(headerValues.Parameter ?? "");
                var values = Encoding.UTF8.GetString(parameter).Split(':');
                var username = values[0];
                var password = values[1];

                var user = _context.Users.Where(e => e.Username == username).FirstOrDefault();
                if (user == null)
                {
                    return AuthenticateResult.Fail("User not found");
                }

                if (!PasswordHash.Verify(password, user.PasswordHash))
                {
                    return AuthenticateResult.Fail("Invalid Password");
                }
                var claims = new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, username),
                    new Claim(ClaimTypes.Name, username),
                };
                var identity = new ClaimsIdentity(claims, Scheme.Name);
                var principal = new ClaimsPrincipal(identity);
                var ticket = new AuthenticationTicket(principal, Scheme.Name);
                return AuthenticateResult.Success(ticket);
            }
            catch (Exception ex)
            {
                return AuthenticateResult.Fail(ex);
            }
        }
    }
}
