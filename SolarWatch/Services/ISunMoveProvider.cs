namespace SolarWatch.Services
{
    public interface ISunMoveProvider
    {
        string GetSunMoveData(DateOnly date, float lng, float lat);
    }
}
