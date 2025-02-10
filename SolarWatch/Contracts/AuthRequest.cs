using System.ComponentModel.DataAnnotations;

namespace SolarWatch.Contracts
{
    public record AuthRequest(
        [Required]
        string Email,
        [Required]
        string Password);

}
