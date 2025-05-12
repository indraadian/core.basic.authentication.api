using core.basic.authentication.api.Database;
using core.basic.authentication.api.Infrastructures;
using core.basic.authentication.api.Models;
using Microsoft.EntityFrameworkCore;

namespace core.basic.authentication.api.Services
{
    public class AuthService : IAuthService
    {
        private readonly ApplicationDBContext _context;
        public AuthService(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<AuthResponse> LoginAsync(AuthRequest request)
        {
            var result = new AuthResponse()
            {
                Success = true,
            };

            if (string.IsNullOrEmpty(request.Username))
            {
                result.Success = false;
                result.Message = "Username is required";
                return result;
            }

            if (string.IsNullOrEmpty(request.Password))
            {
                result.Success = false;
                result.Message = "Password is required";
                return result;
            }

            var user = await _context.Users.Where(e => e.Username == request.Username).FirstOrDefaultAsync();
            if (user == null)
            {
                result.Success = false;
                result.Message = "Username not found";
                return result;
            }

            if (!PasswordHash.Verify(request.Password, user.PasswordHash))
            {
                result.Success = false;
                result.Message = "Username not found";
                return result;
            }

            result.UserId = user.Id;
            return result;

        }

        public async Task<AuthResponse> RegisterAsync(AuthRequest request)
        {
            var result = new AuthResponse()
            {
                Success = true,
            };

            if (string.IsNullOrEmpty(request.Username))
            {
                result.Success = false;
                result.Message = "Username is required";
                return result;
            }

            if (string.IsNullOrEmpty(request.Password))
            {
                result.Success = false;
                result.Message = "Password is required";
                return result;
            }

            if (_context.Users.Any(e => e.Username == request.Username))
            {
                result.Success = false;
                result.Message = "Username already exist";
                return result;
            }

            var newUser = new User
            {
                Username = request.Username,
                PasswordHash = PasswordHash.Hash(request.Password),
            };

            _context.Users.Add(newUser);
            var saveResult = await _context.SaveChangesAsync();
            if (saveResult > 0)
            {
                result.UserId = newUser.Id;
            }
            return result;
        }
    }
}
