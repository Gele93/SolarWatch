using SolarWatch.Data.Entities;
using SolarWatch.Models;

namespace SolarWatch.Services.CityServices
{
    public interface ICityService
    {
        Task<int> CreateCity(CityApiDto cityData);
        Task<City> EditCity(CityApiDto cityData, int cityId);
        Task<bool> RemoveCity(int cityId);
    }
}
