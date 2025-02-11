using Microsoft.AspNetCore.Identity;

namespace SolarWatch.Services.AuthServices
{
    public interface ITokenService
    {
        public string CreateToken(IdentityUser user, string role);

    }
}
