using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SolarWatch.Services.CityNameServices;

namespace SolarWatch.Controllers
{
    [Controller]
    [Route("[controller]")]
    public class CityNameController : ControllerBase
    {
        private readonly ICityNameService _cityNameService;
        public CityNameController(ICityNameService cityNameService)
        {
            _cityNameService = cityNameService;
        }

        [HttpGet("{filterName}"), Authorize(Policy = "RequireUserOrAdmin")]
        public async Task<IActionResult> GetCityNames(string filterName)
        {
            var cities = await _cityNameService.GetFilteredCityNames(filterName);
            return Ok(cities);
        }


    }
}
