using System.Text.Json;
using SolarWatch.Models;

namespace SolarWatch.Services.ParseServices
{
    public class CityJsonParseService : ICityJsonParser
    {
        public CityApiDto GetCityData(string cityData)
        {
            JsonDocument json = JsonDocument.Parse(cityData);
            var lng = json.RootElement[0].GetProperty("lon").GetSingle();
            var lat = json.RootElement[0].GetProperty("lat").GetSingle();
            var name = json.RootElement[0].GetProperty("name").GetString();
            var country = json.RootElement[0].GetProperty("country").GetString();

            string? state = null;

            if (json.RootElement[0].TryGetProperty("state", out JsonElement stateElement))
            {
                state = stateElement.GetString();
            }

            return new CityApiDto(name, lng, lat, country, state);

        }

    }
}
