using SolarWatch.Data.Context;
using SolarWatch.Data.Entities;

namespace SolarWatch.Services.Repositories
{
    public class CityNameRepository : ICityNameRepository
    {
        private SolarWatchContext _dbContext;

        public CityNameRepository(SolarWatchContext context)
        {
            _dbContext = context;
        }

        public async Task<List<CityName>> GetAll()=> _dbContext.CityNames.ToList();

    }
}
