using SolarWatch.Data.Entities;
using SolarWatch.Models;

namespace SolarWatch.Services.SolarServices
{
    public interface ISolar
    {
        Task<bool> ExistsInDb(SunApiDto sunData);
        Task<SunMovement> GetSunMovement(SunApiDto sunData);
        void SaveIntoDb(CityApiDto cityData, SunMovementDto sunMoveData);
    }
}
