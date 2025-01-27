using System.Text.Json;

namespace SolarWatch.Services
{
    public class SunJsonParseService : ISunJsonParser
    {
        public DateTime GetSunRise(string sunData)
        {
            JsonDocument json = JsonDocument.Parse(sunData);
            var sunRise = json.RootElement.GetProperty("results").GetProperty("sunrise").GetString();
            DateTime sunRiseTime = DateTime.Parse(sunRise);
            return sunRiseTime;
        }
        
        public DateTime GetSunSet(string sunData)
        {
            JsonDocument json = JsonDocument.Parse(sunData);
            var sunRise = json.RootElement.GetProperty("results").GetProperty("sunset").GetString();
            DateTime sunRiseTime = DateTime.Parse(sunRise);
            return sunRiseTime;
        }
    }
}
