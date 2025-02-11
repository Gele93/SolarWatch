using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using SolarWatch.Models;
using SolarWatch.Services.ApiServices;
using SolarWatch.Services.CityServices;
using SolarWatch.Services.ParseServices;
using SolarWatch.Services.SolarServices;

namespace SolarWatch.Controllers
{
    [Controller]
    [Route("[controller]")]
    public class CityController : ControllerBase
    {
        private readonly ILogger<CityController> _logger;
        private ICityService _cityService;

        public CityController(ILogger<CityController> logger, ICityService cityService)
        {
            _logger = logger;
            _cityService = cityService;
        }

        [HttpPost("cities"), Authorize(Policy = "RequireAdmin")]
        public async Task<IActionResult> CreateCity(CityApiDto cityData)
        {
            try
            {
                var cityId = await _cityService.CreateCity(cityData);
                return Ok(cityId);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return BadRequest(e.Message);
            }
        }

        [HttpPut("cities/{cityId}"), Authorize(Policy = "RequireAdmin")]
        public async Task<IActionResult> EditCity(CityApiDto cityData, int cityId)
        {
            try
            {
                var city = await _cityService.EditCity(cityData, cityId);
                return Ok(city);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("cities/{cityId}"), Authorize(Policy = "RequireAdmin")]
        public async Task<IActionResult> RemoveCity(int cityId)
        {
            try
            {
                var isDeleted = await _cityService.RemoveCity(cityId);
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
