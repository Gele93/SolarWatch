using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using SolarWatch.Models;
using SolarWatch.Services.CityServices;
using SolarWatch.Services.Repositories;
using SolarWatch.Data.Entities;

namespace SolarTest.CityServiceTests
{
    [TestFixture]
    public class CityTest
    {
        public Mock<ICityRepository> _cityRepoMock;
        public CityServices _service;

        public void Setup()
        {
            _cityRepoMock = new Mock<ICityRepository>();
            _service = new(_cityRepoMock.Object);
        }

        public CityTest()
        {
            Setup();
        }

        [Test]
        public async Task CreateCity_WithValidCityData_ReturnsCorrectId()
        {
            CityApiDto cityData = new("name", 10, 10, "country", "state");

            _cityRepoMock.Setup(x => x.Add(It.IsAny<City>()))
                .ReturnsAsync(1);

            var result = await _service.CreateCity(cityData);

            Assert.That(result, Is.EqualTo(1));
        }

        [Test]
        public async Task EditCity_WithValidCityData_ReturnsUpdatedCity()
        {
            CityApiDto newCityData = new("name", 10, 10, "country", "state");
            var cityToUpdate = new City()
            {
                Name = "oldName",
                Longitude = 20,
                Latitude = 20,
                Country = "oldCountry",
                State = "oldState"
            };
            int cityId = 1;

            _cityRepoMock.Setup(x => x.Get(It.IsAny<int>()))
                .ReturnsAsync(cityToUpdate);

            _cityRepoMock.Setup(x => x.Edit(It.IsAny<City>()))
                .ReturnsAsync(cityToUpdate);

            var result = await _service.EditCity(newCityData, cityId);

            Assert.That(result.Name, Is.EqualTo(newCityData.Name));
            Assert.That(result.Latitude, Is.EqualTo(newCityData.Latitude));
            Assert.That(result.Longitude, Is.EqualTo(newCityData.Longitude));
            Assert.That(result.Country, Is.EqualTo(newCityData.Country));
            Assert.That(result.State, Is.EqualTo(newCityData.State));
        }

        [Test]
        public async Task EditCity_WithInvalidCityData_ThrowsException()
        {
            CityApiDto newCityData = new("name", 10, 10, "country", "state");
            var cityToUpdate = new City()
            {
                Name = "oldName",
                Longitude = 20,
                Latitude = 20,
                Country = "oldCountry",
                State = "oldState"
            };
            int cityId = 1;

            _cityRepoMock.Setup(x => x.Get(It.IsAny<int>()))
                .ReturnsAsync((City?)null);

            Assert.ThrowsAsync<NullReferenceException>(async () => await _service.EditCity(newCityData, cityId));
        }

        [Test]
        public async Task DeleteCity_WithValidCityId_ReturnsTrue()
        {
            int cityId = 1;
            var cityToDelete = new City()
            {
                Name = "oldName",
                Longitude = 20,
                Latitude = 20,
                Country = "oldCountry",
                State = "oldState"
            };

            _cityRepoMock.Setup(x => x.Get(It.IsAny<int>()))
                .ReturnsAsync(cityToDelete);

            _cityRepoMock.Setup(x => x.Delete(It.IsAny<City>()))
                .ReturnsAsync(true);

            var result = await _service.RemoveCity(cityId);

            Assert.That(result, Is.True);
        }

        [Test]
        public async Task DeleteCity_WithInvalidCityId_ThrowsException()
        {
            int cityId = 1;

            _cityRepoMock.Setup(x => x.Get(It.IsAny<int>()))
                .ReturnsAsync((City?)null);

            Assert.ThrowsAsync<NullReferenceException>(async () => await _service.RemoveCity(cityId));
        }
    }
}
