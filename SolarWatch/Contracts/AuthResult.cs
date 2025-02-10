namespace SolarWatch.Contracts
{
    public record AuthResult(
        bool Success,
        string Email,
        string Username,
        string Token)
    {
        //Error code - error message
        public readonly Dictionary<string, string> ErrorMessages = new();
    }

}
