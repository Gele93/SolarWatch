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
    public class OpenWeatherMapTest
    {
        private Mock<ILogger<OpenWeatherMapApi>> _loggerMock;
        private ICityDataProvider _service;

        public void Setup()
        {
            _loggerMock = new Mock<ILogger<OpenWeatherMapApi>>();
            _service = new OpenWeatherMapApi(_loggerMock.Object);
        }

        private static object[] validCityTestCases = new object[]
        {
            new object[] { "London" },
            new object[] { "Paris" },
            new object[] { "Berlin" }
        };
        private static object[] invalidCityTestCases = new object[]
{
            new object[] { "Lordion" },
            new object[] { "Pardizs" },
            new object[] { "Börlinnő" }
};


        [Test, TestCaseSource(nameof(validCityTestCases))]
        public async Task CorrectCityReturnsCorrectCityName(string city)
        {
            Setup();

            var cityData = await _service.GetCityData(city);
            var name = ParseCity(cityData);

            Assert.That(city, Is.EqualTo(name));
        }

        [Test, TestCaseSource(nameof(invalidCityTestCases))]
        public async Task IncorrectCityThrowsException(string city)
        {
            Setup();

            Assert.ThrowsAsync<Exception>(async () => await _service.GetCityData(city));
        }

        private string ParseCity(string cityData)
        {
            JsonDocument json = JsonDocument.Parse(cityData);
            return json.RootElement[0].GetProperty("name").GetString();
        }

    }
}
