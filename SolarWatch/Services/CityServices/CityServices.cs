using SolarWatch.Data.Entities;
using SolarWatch.Models;
using SolarWatch.Services.Repositories;
using System.Diagnostics.Metrics;

namespace SolarWatch.Services.CityServices
{
    public class CityServices : ICityService
    {
        private readonly ICityRepository _cityRepo;
        public CityServices(ICityRepository cityRepo)
        {
            _cityRepo = cityRepo;
        }
        public async Task<int> CreateCity(CityApiDto cityData)
        {
            var city = new City
            {
                Name = cityData.Name,
                Longitude = cityData.Longitude,
                Latitude = cityData.Latitude,
                Country = cityData.Country,
                State = cityData.State
            };

            var cityId = await _cityRepo.Add(city);
            return cityId;
        }
        public async Task<City> EditCity(CityApiDto cityData, int cityId)
        {
            var cityToUpdate = await _cityRepo.Get(cityId);

            if (cityToUpdate == null)
            {
                throw new NullReferenceException($"{cityData.Name} was not found in the database");
            }

            cityToUpdate.Name = cityData.Name;
            cityToUpdate.Longitude = cityData.Longitude;
            cityToUpdate.Latitude = cityData.Latitude;
            cityToUpdate.Country = cityData.Country;
            cityToUpdate.State = cityData.State;

            var city = await _cityRepo.Edit(cityToUpdate);
            return city;
        }
        public async Task<bool> RemoveCity(int cityId)
        {
            var cityToDelete = await _cityRepo.Get(cityId);

            if (cityToDelete == null)
            {
                throw new NullReferenceException($"cit#{cityId} was not found in the database");
            }

            var result = await _cityRepo.Delete(cityToDelete);

            return result;
        }

    }
}
