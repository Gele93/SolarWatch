using SolarWatch.Data.Entities;

namespace SolarWatch.Services.Repositories
{
    public class SunMovementRepository : ISunMovementRepository
    {
        public async Task<List<SunMovement>> GetAllByCity(int cityId)
        {
            throw new NotImplementedException();
        }
        public async Task<SunMovement> GetByCityDate(int cityId, DateTime date)
        {
            throw new NotImplementedException();
        }
        public void Add(SunMovement sunMovement)
        {
            throw new NotImplementedException();
        }
    }
}
