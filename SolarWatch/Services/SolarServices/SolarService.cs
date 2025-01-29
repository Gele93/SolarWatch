using SolarWatch.Data.Entities;
using SolarWatch.Models;
using SolarWatch.Services.Repositories;
using System.Reflection.Metadata.Ecma335;

namespace SolarWatch.Services.SolarServices
{
    public class SolarService : ISolar
    {
        private readonly ICityRepository _cityRepo;
        private readonly ISunMovementRepository _sunRepo;
        public SolarService(ICityRepository cityRepository, ISunMovementRepository sunMovementRepository)
        {
            _cityRepo = cityRepository;
            _sunRepo = sunMovementRepository;
        }

        public async Task<bool> ExistsInDb(SunApiDto sunData)
        {
            int? cityId = _cityRepo.Get(sunData.City).Id;

            if (cityId == null)
            {
                return false;
            }
            else
            {
                int validCityId = cityId.Value;
                int? sunMoveId = _sunRepo.GetByCityDate(validCityId, sunData.Date).Id;

                return sunMoveId != null;
            }
        }

        public async Task<SunMovement> GetSunMovement(SunApiDto sunData)
        {
            throw new NotImplementedException();
        }


        public async void SaveIntoDb(CityApiDto cityData, SunMovementDto sunMoveData)
        {
            throw new NotImplementedException();
        }

    }
}
