using SolarWatch.Data.Entities;
using SolarWatch.Services.Repositories;

namespace SolarWatch.Services.CityNameServices
{
    public interface ICityNameService
    {
        Task<List<CityName>> GetFilteredCityNames(string filterName);
    }
}
