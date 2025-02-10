using SolarWatch.Data.Entities;
using SolarWatch.Models;
using SolarWatch.Data.Context;
using Microsoft.EntityFrameworkCore;

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
        public async Task<int> Add(City city)
        {
            await _dbContext.Cities.AddAsync(city);
            await _dbContext.SaveChangesAsync();
            return city.Id;
        }
    }
}


