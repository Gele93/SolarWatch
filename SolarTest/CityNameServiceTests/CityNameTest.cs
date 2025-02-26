using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using SolarWatch.Services.CityNameServices;
using SolarWatch.Services.Repositories;
using SolarWatch.Data.Entities;

namespace SolarTest.CityNameServiceTests
{
    [TestFixture]
    public class CityNameTest
    {
        public Mock<ICityNameRepository> _cityNameRepositoryMock;
        public CityNameService _service;

        public static List<CityName> cityNames = new()
        {
            new CityName() { Name = "New York" },
            new CityName() { Name = "London" },
            new CityName() { Name = "Budapest" },
            new CityName() { Name = "Berlin" },
            new CityName() { Name = "Wien" },
            new CityName() { Name = "Bucarest" },
            new CityName() { Name = "Moscow" },
        };

        public void Setup()
        {
            _cityNameRepositoryMock = new Mock<ICityNameRepository>();
            _service = new CityNameService(_cityNameRepositoryMock.Object);
        }

        public CityNameTest()
        {
            Setup();
        }
        public static object[] cityNameTestSources = new object[]
            {
              new object[] { cityNames, "new", 1 },
              new object[] { cityNames, "NEW", 1 },
              new object[] { cityNames, "new ", 1 },
              new object[] { cityNames, "a", 2 },
              new object[] { cityNames, "x", 0 },
              new object[] { cityNames, "", 7 },

            };


        [TestCaseSource(nameof(cityNameTestSources))]
        public async Task ValidFilterReturnsValidCities(List<CityName> cities, string filter, int expected)
        {
            _cityNameRepositoryMock.Setup(x => x.GetAll())
                .ReturnsAsync(cities.ToList());

            var result = await _service.GetFilteredCityNames(filter);
            Assert.That(result.Count, Is.EqualTo(expected));
        }

    }
}
