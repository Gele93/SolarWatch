namespace SolarWatch.Services
{
    public interface ISunMoveProvider
    {
        Task<string> GetSunMoveData(DateOnly date, float lng, float lat);
    }
}
