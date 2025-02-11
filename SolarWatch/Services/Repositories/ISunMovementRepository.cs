using SolarWatch.Data.Entities;
using SolarWatch.Models;

namespace SolarWatch.Services.Repositories
{
    public interface ISunMovementRepository
    {
        Task<List<SunMovement>> GetAllByCity(int cityId);
        Task<SunMovement>? GetByCityDate(int cityId, DateTime date);
        Task<SunMovement> GetById(int sunId);
        Task<int> Add(SunMovement sunMovementData);
        Task<SunMovement> Edit(SunMovement sunMove);
        Task<bool> Delete(SunMovement sunMove);

    }
}
