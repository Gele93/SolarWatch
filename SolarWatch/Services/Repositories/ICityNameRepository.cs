using SolarWatch.Data.Entities;

namespace SolarWatch.Services.Repositories
{
    public interface ICityNameRepository
    {
        Task<List<CityName>> GetAll();
    }
}
