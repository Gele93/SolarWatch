using Microsoft.EntityFrameworkCore;
using SolarWatch.Controllers;
using SolarWatch.Data.Context;
using SolarWatch.Data.Entities;
using SolarWatch.Models;
using SolarWatch.Services.Repositories;
using System.Reflection.Metadata.Ecma335;

namespace SolarWatch.Services.SolarServices
{
    public class SolarService : ISolar
    {
     //   private SolarWatchContext _dbContext;

        private readonly ILogger<SolarService> _logger;
        private readonly ICityRepository _cityRepo;
        private readonly ISunMovementRepository _sunRepo;
        public SolarService(ICityRepository cityRepository, ISunMovementRepository sunMovementRepository, ILogger<SolarService> logger)
        {
       //     _dbContext = dbContext;
            _logger = logger;
            _cityRepo = cityRepository;
            _sunRepo = sunMovementRepository;
        }

        public async Task<bool> ExistsInDb(SunApiDto sunData)
        {
            City? city = await _cityRepo.Get(sunData.City);

            if (city == null)
            {
                return false;
            }
            else
            {
                int validCityId = city.Id;

                SunMovement? sunMove = await _sunRepo.GetByCityDate(validCityId, sunData.Date);

                return sunMove != null;
            }
        }

        public async Task<SunMovement> GetSunMovement(SunApiDto sunData)
        {
            var city = await _cityRepo.Get(sunData.City);
            int cityId = city.Id;

            var sunMove = await _sunRepo.GetByCityDate(cityId, sunData.Date);

            return sunMove;
        }


        public async Task SaveIntoDb(CityApiDto cityData, SunMovementDto sunMoveData, DateTime date)
        {
            try
            {
                City? city = await _cityRepo.Get(cityData.Name);

                int cityId = 0;

                if (city is null)
                {
                    cityId = await _cityRepo.Add(new City(cityData.Name, cityData.Longitude, cityData.Latitude, cityData.Country, cityData.State));
                }
                else
                {
                    cityId = city.Id;
                }

                SunMovement sunMoveDataToAdd = new(cityId, date, sunMoveData.SunRise, sunMoveData.SunSet);

                await _sunRepo.Add(sunMoveDataToAdd);

            } catch (Exception ex)
            {
                _logger.LogError($"Error while trying to write local DB: {ex.Message}");
                throw new Exception("Saving failed");
            }
        }

    }
}
