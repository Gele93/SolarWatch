using SolarWatch.Data.Entities;
using SolarWatch.Services.Repositories;

namespace SolarWatch.Services.CityNameServices
{
    public class CityNameService : ICityNameService
    {
        private readonly ICityNameRepository _cityNameRepository;
        public CityNameService(ICityNameRepository cityNameRepository)
        {
            _cityNameRepository = cityNameRepository;
        }
        public async Task<List<CityName>> GetFilteredCityNames(string filterName)
        {
            var cities = await _cityNameRepository.GetAll();
            return cities.Where(c => c.Name.ToLower().Contains(filterName.ToLower())).ToList();
        }
    }
}
