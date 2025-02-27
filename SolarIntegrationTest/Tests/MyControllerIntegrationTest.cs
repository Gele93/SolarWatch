using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Validations;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SolarIntegrationTest.Factories;
using SolarIntegrationTest.Utils;
using SolarWatch.Contracts;
using SolarWatch.Data.Entities;
using SolarWatch.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Xunit.Abstractions;

namespace SolarIntegrationTest.Tests
{
    [Collection("IntegrationTests")]
    public class MyControllerIntegrationTest
    {
        private readonly SolarWebApplicationFactory _app;
        private readonly HttpClient _client;
        private readonly ITestOutputHelper _output;

        public MyControllerIntegrationTest(ITestOutputHelper output)
        {
            _app = new();
            _client = _app.CreateClient();
            _output = output;
        }

        [Fact]
        public async Task Test_GetSunDataForCity()
        {
            var city = "London";
            var date = DateTime.Now;

            var token = await RegisterAndLoginUser();
            var sunMovement = await TestGetSunMovement(city, date, token);

            _output.WriteLine(sunMovement.SunSet);
            _output.WriteLine(sunMovement.SunRise);
        }

        [Fact]
        public async Task Test_NewCityWritesIntoDb()
        {
            var city = "London";
            var date = DateTime.Now;
            List<City> cities = new();

            var token = await LoginAdmin();
            cities = await TestGetAllCities(token);

            Assert.True(cities.Count == 0);

            await TestGetSunMovement(city, date, token);
            cities = await TestGetAllCities(token);

            Assert.True(cities.Count == 1);

            _output.WriteLine(cities[0].Name);
        }

        [Fact]
        public async Task Test_KnownCityDoesNotWriteIntoDb()
        {
            var city = "London";
            var date = DateTime.Now;
            List<City> cities = new();

            var token = await LoginAdmin();
            await TestGetSunMovement(city, date, token);
            cities = await TestGetAllCities(token);

            Assert.True(cities.Count() == 1);

            await TestGetSunMovement(city, date, token);

            Assert.True(cities.Count() == 1);

            _output.WriteLine(cities[0].Name);
        }


        private async Task<string> LoginAdmin()
        {
            var username = "admin";
            var email = "admin@admin.com";
            var password = "admin123";

            var loginResponse = await TestLogin(username, email, password);
            return loginResponse.Token;
        }
        private async Task<string> RegisterAndLoginUser()
        {
            var username = "user";
            var email = "user@user.com";
            var password = "user123";

            await TestRegister(username, email, password);
            var loginResponse = await TestLogin(username, email, password);
            return loginResponse.Token;
        }
        private async Task<RegistrationResponse> TestRegister(string username, string email, string password)
        {
            var url = "/register";

            var regData = new RegistrationRequest(username, email, password);
            var content = ConvertExtensions.ToHttpContent(regData);
            var expectedRegResult = new RegistrationResponse(username, email);

            var response = await _client.PostAsync(url, content);

            response.EnsureSuccessStatusCode();

            var regResult = await response.Content.ReadFromJsonAsync<RegistrationResponse>();

            try
            {
                Assert.Equal(expectedRegResult, regResult);
                Console.WriteLine("Registration Succeeded!");
                return regResult;
            }
            catch (Xunit.Sdk.XunitException ex)
            {
                throw new Exception($"Registration failed: ${ex.Message}");
            }
        }
        private async Task<AuthResponse> TestLogin(string username, string email, string password)
        {
            var url = "/login";

            var loginData = new AuthRequest(email, password);
            var content = ConvertExtensions.ToHttpContent(loginData);
            var expectedLoginResult = new AuthResponse(username, email, "token");

            var response = await _client.PostAsync(url, content);

            response.EnsureSuccessStatusCode();

            var loginResult = await response.Content.ReadFromJsonAsync<AuthResponse>();

            try
            {
                Assert.Equal(expectedLoginResult.Username, loginResult.Username);
                Assert.Equal(expectedLoginResult.Email, loginResult.Email);
                Assert.False(string.IsNullOrEmpty(loginResult.Token));
                Console.WriteLine("Login Succeeded!");
                return loginResult;
            }
            catch (Xunit.Sdk.XunitException ex)
            {
                throw new Exception($"Login failed: ${ex.Message}");
            }
        }
        private async Task<SunMovementDto> TestGetSunMovement(string city, DateTime date, string token)
        {
            var url = $"/sun?city={city}&date={date}";

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _client.GetAsync(url);

            response.EnsureSuccessStatusCode();

            var sunResult = await response.Content.ReadFromJsonAsync<SunMovementDto>();

            try
            {
                Assert.False(string.IsNullOrEmpty(sunResult.SunSet));
                Assert.False(string.IsNullOrEmpty(sunResult.SunRise));
                Console.WriteLine("Getting SunMovement Succeeded!");
                return sunResult;
            }
            catch (Xunit.Sdk.XunitException ex)
            {
                throw new Exception($"Getting SunMovement failed: ${ex.Message}");
            }
        }
        private async Task<List<City>> TestGetAllCities(string token)
        {
            var url = "/city";

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _client.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var cityResult = await response.Content.ReadFromJsonAsync<List<City>>();

            try
            {
                _output.WriteLine($"Cities read successfully. Cities count: {cityResult.Count()}");
                return cityResult;
            }
            catch (Xunit.Sdk.XunitException ex)
            {
                throw new Exception($"Getting all cities failed: ${ex.Message}");
            }

        }
        private async Task<int> TestCreateCity(CityApiDto city, string token)
        {
            var url = "/city";
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var content = ConvertExtensions.ToHttpContent(city);
            var response = await _client.PostAsync(url, content);

            response.EnsureSuccessStatusCode();

            var cityId = await response.Content.ReadFromJsonAsync<int>();

            try
            {
                Console.WriteLine("Creating City Succeeded!");
                return cityId;
            }
            catch (Xunit.Sdk.XunitException ex)
            {
                throw new Exception($"Creating City failed: ${ex.Message}");
            }
        }
    }
}
