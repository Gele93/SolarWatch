namespace SolarWatch.Services.ApiServices
{
    public interface ICityDataProvider
    {
        Task<string> GetCityData(string city);
    }
}
