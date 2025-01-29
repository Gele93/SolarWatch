using SolarWatch.Data.Entities;

namespace SolarWatch.Services.Repositories
{
    public interface ICityRepository
    {
        Task<List<City>> GetAll();
        Task<City>? Get(string name);
        Task<int> Add (City city);

    }
}
