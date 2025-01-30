using System.Text.Json;

namespace SolarWatch.Services.ParseServices
{
    public class SunJsonParseService : ISunJsonParser
    {
        public string GetSunRise(string sunData)
        {
            JsonDocument json = JsonDocument.Parse(sunData);
            var sunRise = json.RootElement.GetProperty("results").GetProperty("sunrise").GetString();
       //     DateTime sunRiseTime = DateTime.Parse(sunRise);
            return sunRise;
        }

        public string GetSunSet(string sunData)
        {
            JsonDocument json = JsonDocument.Parse(sunData);
            var sunRise = json.RootElement.GetProperty("results").GetProperty("sunset").GetString();
            //DateTime sunRiseTime = DateTime.Parse(sunRise);
            return sunRise;
        }
    }
}
