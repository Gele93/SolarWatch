using Azure;
using Microsoft.AspNetCore.Identity;
using SolarWatch.Contracts;

namespace SolarWatch.Services.AuthServices
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ITokenService _tokenService;
        public AuthService(UserManager<IdentityUser> userManager, ITokenService tokenService)
        {
            _userManager = userManager;
            _tokenService = tokenService;
        }

        public async Task<AuthResult> RegisterAsync(string username, string email, string password, string role)
        {
            var user = new IdentityUser { UserName = username, Email = email };

            var result = await _userManager.CreateAsync(user, password);

            if (!result.Succeeded)
            {
                return FailedRegistration(user, result);
            }

            await _userManager.AddToRoleAsync(user, role);

            return new(true, email, username, "");
        }

        public async Task<AuthResult> LoginAsync(string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user is null)
            {
                return WrongEmail(email);
            }

            if (!await _userManager.CheckPasswordAsync(user, password))
            {
                return WrongPassword(user);
            }

            var roles = await _userManager.GetRolesAsync(user);

            var token = _tokenService.CreateToken(user, roles[0]);

            var validResultUser = new AuthResult(true, user.Email, user.UserName, token);
            return validResultUser;
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

        private AuthResult WrongEmail(string email)
        {
            var authResult = new AuthResult(false, email, "", "");
            authResult.ErrorMessages.Add("Unknown email", $"{email} was not found");
            return authResult;
        }
        private AuthResult WrongPassword(IdentityUser user)
        {
            var authResult = new AuthResult(false, user.Email, user.UserName, "");
            authResult.ErrorMessages.Add("Invalid Password", $"Email address or password is invalid");
            return authResult;
        }
    }
}
