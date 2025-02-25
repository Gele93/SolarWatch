using SolarWatch.Data.Entities;
using SolarWatch.Models;
using SolarWatch.Services.Repositories;

namespace SolarWatch.Services.SunMovementServices
{
    public class SunMovementService : ISunMovementService
    {
        private readonly ISunMovementRepository _sunMovementRepo;
        private readonly ICityRepository _cityRepository;

        public SunMovementService(ISunMovementRepository sunMovementRepo, ICityRepository cityRepository)
        {
            _sunMovementRepo = sunMovementRepo;
            _cityRepository = cityRepository;
        }

        public async Task<int> CreateSunMovement(SunMovementToCityDto sunData)
        {
            var sunMove = new SunMovement
            {
                CityId = sunData.Cityid,
                Date = sunData.Date,
                SunRise = sunData.SunRise.ToString(),
                Sunset = sunData.SunSet.ToString()
            };

            var sunMoveId = await _sunMovementRepo.Add(sunMove);

            return sunMoveId;

        }
        public async Task<SunMovement> EditSunMovement(SunMovementDto sunData, int cityId, DateTime date)
        {
            var sunMove = await _sunMovementRepo.GetByCityDate(cityId, date);

            if (sunMove is null)
            {
                throw new NullReferenceException($"Sunmovement for city#{cityId} at {date} not found");
            }

            sunMove.CityId = cityId;
            sunMove.Date = date;
            sunMove.SunRise = sunData.SunRise.ToString();
            sunMove.Sunset = sunData.SunSet.ToString();

            var editedSunMove = await _sunMovementRepo.Edit(sunMove);

            return editedSunMove;

        }
        public async Task<bool> RemoveSunMovement(int sunId)
        {
            var sunMoveToDelete = await _sunMovementRepo.GetById(sunId);

            if (sunMoveToDelete is null)
            {
                throw new NullReferenceException($"Sunmovement#{sunId} not found");
            }

            var result = await _sunMovementRepo.Delete(sunMoveToDelete);
            return result;
        }

        public async Task<List<SunMovement>> GetAllByCity(string cityName)
        {
            var city = await _cityRepository.Get(cityName);

            if (city is null) throw new Exception($"{cityName} was not found");

            var cityId = city.Id;

            var sunMovements = await _sunMovementRepo.GetAllByCity(cityId);

            return sunMovements;
        }

    }
}
