using Microsoft.AspNetCore.Mvc;
using SolarWatch.Contracts;
using SolarWatch.Services.AuthServices;

namespace SolarWatch.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<AuthResponse>> RegisterUser([FromBody] RegistrationRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _authService.RegisterAsync(request.Username, request.Email, request.Password);

            if (!result.Success)
            {
                AddErrors(result);
                return BadRequest(ModelState);
            }

            return CreatedAtAction(nameof(RegisterUser), new RegistrationResponse(result.Username, result.Email));
        }

        private void AddErrors(AuthResult result)
        {
            foreach (var error in result.ErrorMessages)
            {
                ModelState.AddModelError(error.Key, error.Value);
            }
        }
    }
}
