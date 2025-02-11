using SolarWatch.Data.Entities;
using SolarWatch.Models;

namespace SolarWatch.Services.Repositories
{
    public interface ICityRepository
    {
        Task<List<City>> GetAll();
        Task<City>? Get(string name);
        Task<City>? Get(int cityId);
        Task<int> Add(City city);
        Task<City> Edit(City city);
        Task<bool> Delete(City city);
    }
}
