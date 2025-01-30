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
        public async Task Add(SunMovement sunMovementData)
        {
            _dbContext.SunMovements.Add(sunMovementData);
            await _dbContext.SaveChangesAsync();
        }
    }
}
