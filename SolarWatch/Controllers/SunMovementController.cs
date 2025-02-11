using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SolarWatch.Models;
using SolarWatch.Services.CityServices;
using SolarWatch.Services.SunMovementServices;

namespace SolarWatch.Controllers
{
    [Controller]
    [Route("[controller]")]
    public class SunMovementController : ControllerBase
    {
        private readonly ILogger<SunMovementController> _logger;
        private ISunMovementService _sunMovementService;

        public SunMovementController(ILogger<SunMovementController> logger, ISunMovementService sunMovementService)
        {
            _logger = logger;
            _sunMovementService = sunMovementService;
        }

        [HttpPost("sunmove"), Authorize(Policy = "RequireAdmin")]
        public async Task<IActionResult> CreateSunMove(SunMovementToCityDto sunData)
        {
            try
            {
                var sunMoveId = await _sunMovementService.CreateSunMovement(sunData);
                return Ok(sunMoveId);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return BadRequest(e.Message);
            }
        }

        [HttpPut("sunmove/{cityId}/{date}"), Authorize(Policy = "RequireAdmin")]
        public async Task<IActionResult> EditCity(SunMovementDto sunData, int cityId, DateTime date)
        {
            try
            {
                var sunMovement = await _sunMovementService.EditSunMovement(sunData, cityId, date);
                return Ok(sunMovement);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("sunmove/{sunId}"), Authorize(Policy = "RequireAdmin")]
        public async Task<IActionResult> RemoveCity(int sunId)
        {
            try
            {
                var isDeleted = await _sunMovementService.RemoveSunMovement(sunId);
                return Ok();
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return BadRequest(e.Message);
            }
        }
    }
}
