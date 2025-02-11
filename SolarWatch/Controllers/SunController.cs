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
    public class SunController : ControllerBase
    {
        private readonly ILogger<SunController> _logger;
        private ICityDataProvider _cityDataProvider;
        private ICityJsonParser _cityJsonParser;
        private ISunMoveProvider _moveProvider;
        private ISunJsonParser _sunJsonParser;
        private ISolar _solarService;


        public SunController(
            ILogger<SunController> logger,
            ICityDataProvider cityDataProvider,
            ICityJsonParser cityJsonParser,
            ISunMoveProvider moveProvider,
            ISunJsonParser sunJsonParser,
            ISolar solarService
            )
        {
            _logger = logger;
            _cityDataProvider = cityDataProvider;
            _cityJsonParser = cityJsonParser;
            _moveProvider = moveProvider;
            _sunJsonParser = sunJsonParser;
            _solarService = solarService;
        }

        [HttpGet(), Authorize(Policy = "RequireUserOrAdmin")]
        public async Task<IActionResult> GetSunMovement(SunApiDto sunData)
        {

            if (sunData.City == null) return BadRequest("invalid city name");

            DateOnly date = DateOnly.FromDateTime(sunData.Date);


            if (await _solarService.ExistsInDb(sunData))
            {
                var sunMovement = await _solarService.GetSunMovement(sunData);

                _logger.LogInformation("Read from local DB");
                return Ok(new SunMovementDto(sunMovement.Sunset, sunMovement.SunRise));
            }

            try
            {
                var cityJson = await _cityDataProvider.GetCityData(sunData.City);
                var cityData = _cityJsonParser.GetCityData(cityJson);

                var sunMoveJson = await _moveProvider.GetSunMoveData(date, cityData.Longitude, cityData.Latitude);
                SunMovementDto sunMoveData = new(_sunJsonParser.GetSunRise(sunMoveJson), _sunJsonParser.GetSunSet(sunMoveJson));

                await _solarService.SaveIntoDb(cityData, sunMoveData, sunData.Date);

                _logger.LogInformation("Read from Sun-Api, saved in local DB");
                return Ok(sunMoveData);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

    }
}
