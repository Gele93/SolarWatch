using SolarWatch.Data.Entities;
using SolarWatch.Models;

namespace SolarWatch.Services.SunMovementServices
{
    public interface ISunMovementService
    {
        Task<List<SunMovement>> GetAllByCity(string city);
        Task<int> CreateSunMovement(SunMovementToCityDto sunData);
        Task<SunMovement> EditSunMovement(SunMovementDto sunData, int cityId, DateTime date);
        Task<bool> RemoveSunMovement(int sunId);
    }
}
