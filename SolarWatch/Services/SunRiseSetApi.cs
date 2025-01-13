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
            string dateString = $"{date.Year}-{date.Month}-{date.Day}";
            string url = $"https://api.sunrise-sunset.org/json?lat={lat}&lng={lng}&date={dateString}";

            using var client = new WebClient();

            _logger.LogInformation($"Calling sun Api: {url}");

            return client.DownloadString(url);

        }
    }
}
