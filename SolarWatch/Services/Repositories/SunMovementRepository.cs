using SolarWatch.Data.Entities;
using SolarWatch.Models;
using SolarWatch.Data.Context;

namespace SolarWatch.Services.Repositories
{
    public class SunMovementRepository : ISunMovementRepository
    {
        private SolarWatchContext _dbContext;

        public SunMovementRepository(SolarWatchContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<SunMovement>> GetAllByCity(int cityId) => _dbContext.SunMovements
                .Where(sm => sm.CityId == cityId)
                .ToList();

        public async Task<SunMovement>? GetByCityDate(int cityId, DateTime date) => _dbContext.SunMovements
            .Where(sm => sm.CityId == cityId)
            .FirstOrDefault(sm => sm.Date == date);
        public async Task<int> Add(SunMovement sunMovementData)
        {
            sunMovementData = UniversalizeDate(sunMovementData);
            await _dbContext.SunMovements.AddAsync(sunMovementData);
            await _dbContext.SaveChangesAsync();
            return sunMovementData.Id;
        }
        public async Task<SunMovement> GetById(int sunId) => _dbContext.SunMovements
            .FirstOrDefault(sm => sm.Id == sunId);

        public async Task<SunMovement> Edit(SunMovement sunMove)
        {
            _dbContext.SunMovements.Update(sunMove);
            await _dbContext.SaveChangesAsync();
            return sunMove;
        }
        public async Task<bool> Delete(SunMovement sunMove)
        {
            _dbContext.SunMovements.Remove(sunMove);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        private SunMovement UniversalizeDate(SunMovement sunMovement)
        {
            var universalizedSunMovement = sunMovement;
            universalizedSunMovement.Date = sunMovement.Date.ToUniversalTime();
            return universalizedSunMovement;
        }
    }
}
