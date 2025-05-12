using core.basic.authentication.api.Models;

namespace core.basic.authentication.api.Services
{
    public interface IAuthService
    {
        Task<AuthResponse> LoginAsync(AuthRequest request);
        Task<AuthResponse> RegisterAsync(AuthRequest request);
    }
}
