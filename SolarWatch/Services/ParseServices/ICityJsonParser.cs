using SolarWatch.Models;

namespace SolarWatch.Services.ParseServices
{
    public interface ICityJsonParser
    {
        CityApiDto GetCityData(string cityData);
    }
}
