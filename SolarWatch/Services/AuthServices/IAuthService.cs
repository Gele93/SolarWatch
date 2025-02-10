using SolarWatch.Contracts;

namespace SolarWatch.Services.AuthServices
{
    public interface IAuthService
    {
        Task<AuthResult> RegisterAsync (string username, string email, string password);
    }
}
