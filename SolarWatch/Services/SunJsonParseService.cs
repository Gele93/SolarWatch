using System.Text.Json;

namespace SolarWatch.Services
{
    public class SunJsonParseService : ISunJsonParser
    {
        public TimeOnly GetSunRise(string sunData)
        {
            JsonDocument json = JsonDocument.Parse(sunData);
            var sunRise = json.RootElement.GetProperty("results").GetProperty("sunrise").GetString();
            TimeOnly sunRiseTime = TimeOnly.Parse(sunRise);
            return sunRiseTime;
        }
        
        public TimeOnly GetSunSet(string sunData)
        {
            JsonDocument json = JsonDocument.Parse(sunData);
            var sunRise = json.RootElement.GetProperty("results").GetProperty("sunset").GetString();
            TimeOnly sunRiseTime = TimeOnly.Parse(sunRise);
            return sunRiseTime;
        }
    }
}
