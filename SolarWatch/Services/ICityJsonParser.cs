namespace SolarWatch.Services
{
    public interface ICityJsonParser
    {
        float GetLongitude(string cityData);
        float GetLatitude(string cityData);
    }
}
