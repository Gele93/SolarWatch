using System.Net;

namespace SolarWatch.Services
{
    public class SunRiseSetApi : ISunMoveProvider
    {
        private readonly ILogger<SunRiseSetApi> _logger;
        public SunRiseSetApi(ILogger<SunRiseSetApi> logger)
        {
            _logger = logger;
        }
        public string GetSunMoveData(DateOnly date, float lng, float lat)
        {
            string url = $"https://api.sunrise-sunset.org/json?lat={lat}&lng={lng}&date={date.ToString("yyyy-MM-dd")}";

            using var client = new WebClient();

            _logger.LogInformation($"Calling sun Api: {url}");

            try
            {
                return client.DownloadString(url);
            }
            catch (Exception ex)
            {
                throw new Exception($"Could not download Sun move data: {ex.Message}");
            }

        }
    }
}
