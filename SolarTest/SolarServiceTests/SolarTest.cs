using Microsoft.Extensions.Logging;
using SolarWatch.Data.Context;
using SolarWatch.Services.Repositories;
using SolarWatch.Services.SolarServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using SolarWatch.Models;
using SolarWatch.Data.Entities;

namespace SolarTest.SolarServiceTests
{
    [TestFixture]
    public class SolarTest
    {
        private Mock<ILogger<SolarService>> _logger;
        private Mock<ICityRepository> _cityRepoMock;
        private Mock<ISunMovementRepository> _sunRepoMock;
        public ISolar _service;

        public void Setup()
        {
            _logger = new();
            _cityRepoMock = new();
            _sunRepoMock = new();
            _service = new SolarService(_cityRepoMock.Object, _sunRepoMock.Object, _logger.Object);
        }

        public SolarTest()
        {
            Setup();
        }

        [Test]
        public async Task IfExistsInDb_ReturnsTrue()
        {
            SunApiDto sunData = new("City", DateTime.Now);
            City city = new City("City", 1.0f, 1.0f, "Country", "State");
            city.Id = 1;

            _cityRepoMock.Setup(x => x.Get(sunData.City)).ReturnsAsync(city);

            _sunRepoMock.Setup(x => x.GetByCityDate(city.Id, sunData.Date))
                .ReturnsAsync(new SunMovement(1, DateTime.Now, "Sunrise", "Sunset"));

            var result = await _service.ExistsInDb(sunData);

            Assert.That(result, Is.True);
        }

        [Test]
        public async Task IfNotExistsInDb_ReturnsFalse()
        {
            SunApiDto sunData = new("City", DateTime.Now);

            _cityRepoMock.Setup(x => x.Get(sunData.City)).ReturnsAsync((City?)null);

            var result = await _service.ExistsInDb(sunData);

            Assert.That(result, Is.False);
        }

        [Test]
        public async Task ValidData_SavesIntoDb_CityExisted()
        {
            CityApiDto cityData = new("ExistingCity", 1.0f, 1.0f, "Country", "State");
            SunMovementDto sunData = new("Sunrise", "Sunset");
            DateTime date = DateTime.Now;
            City city = new City(cityData.Name, cityData.Longitude, cityData.Latitude, cityData.Country, cityData.State);
            city.Id = 1;

            _cityRepoMock.Setup(x => x.Get(cityData.Name))
                .ReturnsAsync(city);

            _cityRepoMock.Setup(x => x.Add(It.IsAny<City>()))
                .ReturnsAsync(1);

            _sunRepoMock.Setup(x => x.Add(It.IsAny<SunMovement>()))
                .ReturnsAsync(1);

            await _service.SaveIntoDb(cityData, sunData, date);

            _cityRepoMock.Verify(x => x.Get(cityData.Name), Times.Once);
            _cityRepoMock.Verify(x => x.Add(It.Is<City>(c => c.Name == cityData.Name)), Times.Never);
            _sunRepoMock.Verify(x => x.Add(It.Is<SunMovement>(s => s.CityId == 1 && s.Date == date)), Times.Once);
        }


        [Test]
        public async Task ValidData_SavesIntoDb_CityNotExisted()
        {
            CityApiDto cityData = new("NonExistingCity", 1.0f, 1.0f, "Country", "State");
            SunMovementDto sunData = new("Sunrise", "Sunset");
            DateTime date = DateTime.Now;
            City city = new City(cityData.Name, cityData.Longitude, cityData.Latitude, cityData.Country, cityData.State);
            city.Id = 1;

            _cityRepoMock.Setup(x => x.Get(cityData.Name))
                .ReturnsAsync((City?) null);

            _cityRepoMock.Setup(x => x.Add(It.IsAny<City>()))
                .ReturnsAsync(1);

            _sunRepoMock.Setup(x => x.Add(It.IsAny<SunMovement>()))
                .ReturnsAsync(1);

            await _service.SaveIntoDb(cityData, sunData, date);

            _cityRepoMock.Verify(x => x.Get(cityData.Name), Times.Once);
            _cityRepoMock.Verify(x => x.Add(It.Is<City>(c => c.Name == cityData.Name)), Times.Once);
            _sunRepoMock.Verify(x => x.Add(It.Is<SunMovement>(s => s.CityId == 1 && s.Date == date)), Times.Once);
        }
    }
}
