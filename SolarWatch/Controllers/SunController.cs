using Microsoft.AspNetCore.Mvc;
using SolarWatch.Services;

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

        public SunController(ILogger<SunController> logger, ICityDataProvider cityDataProvider, ICityJsonParser cityJsonParser, ISunMoveProvider moveProvider, ISunJsonParser sunJsonParser)
        {
            _logger = logger;
            _cityDataProvider = cityDataProvider;
            _cityJsonParser = cityJsonParser;
            _moveProvider = moveProvider;
            _sunJsonParser = sunJsonParser;
        }

        [HttpGet("sunrise")]
        public IActionResult GetSunrise(int year, int month, int day, string city)
        {
            var date = new DateOnly(year, month, day);

            try
            {
                var cityData = _cityDataProvider.GetCityData(city);
                var lat = _cityJsonParser.GetLatitude(cityData);
                var lng = _cityJsonParser.GetLongitude(cityData);
                var sunData = _moveProvider.GetSunMoveData(date, lng, lat);
                var sunRise = _sunJsonParser.GetSunRise(sunData);

                return Ok(sunRise);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("sunset")]
        public IActionResult GetSunset(int year, int month, int day, string city)
        {
            var date = new DateOnly(year, month, day);

            try
            {
                var cityData = _cityDataProvider.GetCityData(city);
                var lat = _cityJsonParser.GetLatitude(cityData);
                var lng = _cityJsonParser.GetLongitude(cityData);
                var sunData = _moveProvider.GetSunMoveData(date, lng, lat);
                var sunSet = _sunJsonParser.GetSunSet(sunData);

                return Ok(sunSet);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
