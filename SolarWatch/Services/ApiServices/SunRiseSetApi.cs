using System.Net;

namespace SolarWatch.Services.ApiServices
{
    public class SunRiseSetApi : ISunMoveProvider
    {
        private readonly ILogger<SunRiseSetApi> _logger;
        public SunRiseSetApi(ILogger<SunRiseSetApi> logger)
        {
            _logger = logger;
        }
        public async Task<string> GetSunMoveData(DateOnly date, float lng, float lat)
        {
            string url = $"https://api.sunrise-sunset.org/json?lat={lat}&lng={lng}&date={date.ToString("yyyy-MM-dd")}";

            using var client = new HttpClient();

            _logger.LogInformation($"Calling sun Api: {url}");

            if (date == DateOnly.FromDateTime(DateTime.Parse("0001-01-01"))) throw new Exception("Invalid date");

            try
            {
                var response = await client.GetAsync(url);
                return await response.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Could not download Sun move data: {ex.Message}");
            }

        }
    }
}
