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

            string cityData = String.Empty;

            try
            {
                var response = await client.GetAsync(url);
                cityData = await response.Content.ReadAsStringAsync();
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                throw new Exception($"Could not download city data: {url}");
            }

            if (cityData == "[]" || String.IsNullOrEmpty(cityData)) throw new Exception($"Could not download city data: {url}");

            return cityData;
        }
    }
}
