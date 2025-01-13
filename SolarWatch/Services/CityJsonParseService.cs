using System.Text.Json;

namespace SolarWatch.Services
{
    public class CityJsonParseService :ICityJsonParser
    {
        public float GetLongitude(string cityData)
        {
            JsonDocument json = JsonDocument.Parse(cityData);
            var lng = json.RootElement[0].GetProperty("lon").GetSingle();
            return lng;
        }
        public float GetLatitude(string cityData)
        {
            JsonDocument json = JsonDocument.Parse(cityData);
            var lat = json.RootElement[0].GetProperty("lat").GetSingle();
            return lat;
        }
    }
}
