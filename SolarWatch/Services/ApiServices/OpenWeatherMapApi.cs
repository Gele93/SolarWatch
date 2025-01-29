using Microsoft.AspNetCore.DataProtection.KeyManagement;
using System.Net;

namespace SolarWatch.Services.ApiServices
{
    public class OpenWeatherMapApi : ICityDataProvider
    {
        private const string _apikey = "600345c91e1c34c8389209c358d1c32a";
        private readonly ILogger<OpenWeatherMapApi> _logger;

        public OpenWeatherMapApi(ILogger<OpenWeatherMapApi> logger)
        {
            _logger = logger;
        }

        public async Task<string> GetCityData(string city)
        {
            string url = $"http://api.openweathermap.org/geo/1.0/direct?q={city}&appid={_apikey}";

            using var client = new HttpClient();

            _logger.LogInformation($"Calling {url}");

            var response = await client.GetAsync(url);
            var cityData = await response.Content.ReadAsStringAsync();

            if (cityData == "[]") throw new Exception($"Could not download city data: {url}");

            return cityData;

        }
    }
}
