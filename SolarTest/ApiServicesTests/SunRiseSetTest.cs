using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;

namespace SolarTest.ApiServicesTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Text.Json;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Logging;
    using Moq;
    using SolarWatch.Services.ApiServices;

    namespace SolarTest.ApiServicesTests
    {
        [TestFixture]
        public class SunRiseSetTest
        {
            private Mock<ILogger<SunRiseSetApi>> _loggerMock;
            private ISunMoveProvider _service;

            public void Setup()
            {
                _loggerMock = new Mock<ILogger<SunRiseSetApi>>();
                _service = new SunRiseSetApi(_loggerMock.Object);
            }

            private static object[] validSunData = new object[]
            {
            new object[] { DateOnly.FromDateTime(DateTime.Parse("2025-01-01")), 10, 10 },
            new object[] { DateOnly.FromDateTime(DateTime.Parse("2035-01-01")), 15, -15 },
            new object[] { DateOnly.FromDateTime(DateTime.Parse("2015-01-01")), -15, 15 },

            };

            private static object[] invalidSunData = new object[]
{
            new object[] { DateOnly.FromDateTime(DateTime.Parse("0001-01-01")), 10, 10 },
};

            [Test, TestCaseSource(nameof(validSunData))]
            public async Task CorrectSunReturnsCorrectSunMoves(DateOnly date, float lng, float ltd)
            {
                Setup();

                string sunRise = "";
                string sunSet = "";
                var sunData = await _service.GetSunMoveData(date, lng, ltd);
                (sunRise, sunSet) = ParseSun(sunData);

                DateTime riseTime, setTime;

                bool isValidRise = DateTime.TryParseExact(sunRise, "h:mm:ss tt",
                    System.Globalization.CultureInfo.InvariantCulture,
                    System.Globalization.DateTimeStyles.None,
                    out riseTime);

                bool isValidSet = DateTime.TryParseExact(sunSet, "h:mm:ss tt",
                    System.Globalization.CultureInfo.InvariantCulture,
                    System.Globalization.DateTimeStyles.None,
                    out setTime);


                Assert.That(isValidRise, Is.True);
                Assert.That(isValidSet, Is.True);
            }


            [Test, TestCaseSource(nameof(invalidSunData))]
            public async Task IncorrectDateThrowsExcpetion(DateOnly date, float lng, float ltd)
            {
                Setup();

                Assert.ThrowsAsync<Exception>(async () => await _service.GetSunMoveData(date, lng, ltd));
            }

            private (string, string) ParseSun(string sunData)
            {
                JsonDocument json = JsonDocument.Parse(sunData);
                var sunRise = json.RootElement.GetProperty("results").GetProperty("sunrise").GetString();
                var sunSet = json.RootElement.GetProperty("results").GetProperty("sunset").GetString();
                return (sunRise, sunSet);
            }
        }
    }

}
