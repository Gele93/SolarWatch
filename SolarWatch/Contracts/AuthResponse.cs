namespace SolarWatch.Contracts
{
    public record AuthResponse(
        string Username,
        string Email,
        string Token
        );
}
