using SolarWatch.Data.Entities;
using SolarWatch.Models;

namespace SolarWatch.Services.Repositories
{
    public interface ISunMovementRepository
    {
        Task<List<SunMovement>> GetAllByCity(int cityId);
        Task<SunMovement>? GetByCityDate(int cityId, DateTime date);
        void Add(SunMovement sunMovementData);

    }
}
