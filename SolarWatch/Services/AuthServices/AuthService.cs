using Microsoft.AspNetCore.Identity;
using SolarWatch.Contracts;

namespace SolarWatch.Services.AuthServices
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<IdentityUser> _userManager;

        public AuthService(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<AuthResult> RegisterAsync(string username, string email, string password)
        {
            var user = new IdentityUser { UserName = username, Email = email };

            var result = await _userManager.CreateAsync(user, password);

            if (!result.Succeeded)
            {
                return FailedRegistration(user, result);
            }

            return new(true, user.Email, user.UserName, "");

        }

        private AuthResult FailedRegistration(IdentityUser user, IdentityResult result)
        {
            var authResult = new AuthResult(false, user.Email, user.UserName, "");

            foreach (var error in result.Errors)
            {
                authResult.ErrorMessages.Add(error.Code, error.Description);
            }

            return authResult;
        }
    }
}
