using SolarWatch.Data.Entities;
using SolarWatch.Models;

namespace SolarWatch.Services.Repositories
{
    public interface ICityRepository
    {
        Task<List<City>> GetAll();
        Task<City>? Get(string name);
        int Add(City city);

    }
}
