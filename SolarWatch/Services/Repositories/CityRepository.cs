using SolarWatch.Data.Entities;
using SolarWatch.Models;
using SolarWatch.Data.Context;

namespace SolarWatch.Services.Repositories
{
    public class CityRepository : ICityRepository
    {

        private SolarWatchContext _dbContext;

        public CityRepository(SolarWatchContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<List<City>> GetAll() => _dbContext.Cities
            .ToList();
        public async Task<City>? Get(string name) => _dbContext.Cities
            .FirstOrDefault(c => c.Name == name);
        public int Add(City city)
        {
            _dbContext.Cities.Add(city);
            _dbContext.SaveChanges();
            return city.Id;
        }
    }
}


