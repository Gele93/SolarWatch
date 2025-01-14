using Microsoft.AspNetCore.DataProtection.KeyManagement;
using System.Net;

namespace SolarWatch.Services
{
    public class OpenWeatherMapApi : ICityDataProvider
    {
        private const string _apikey = "600345c91e1c34c8389209c358d1c32a";
        private readonly ILogger<OpenWeatherMapApi> _logger;

        public OpenWeatherMapApi(ILogger<OpenWeatherMapApi> logger)
        {
            _logger = logger;
        }

        public string GetCityData(string city)
        {
            string url = $"http://api.openweathermap.org/geo/1.0/direct?q={city}&appid={_apikey}";

            using var client = new WebClient();

            _logger.LogInformation($"Calling {url}");

            var cityData = client.DownloadString(url);

            if (cityData == "[]") throw new Exception($"Could not download city data: {url}");

            return cityData;

        }
    }
}
