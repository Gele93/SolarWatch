using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Moq;
using SolarWatch.Services.AuthServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolarTest.AuthServiceTests
{
    [TestFixture]
    public class TokenTest
    {
        Mock<IConfiguration> _mockConfig;
        ITokenService _service;

        public void Setup()
        {
            _mockConfig = new Mock<IConfiguration>();

            _mockConfig.Setup(x => x["JwtSettings:Issuer"]).Returns("Issuer");
            _mockConfig.Setup(x => x["JwtSettings:Audience"]).Returns("Audience");
            _mockConfig.Setup(x => x["JwtSettings:SecretKey"]).Returns("!SomethingSecret!!SomethingSecret!");

            _service = new TokenService(_mockConfig.Object);
        }

        public TokenTest()
        {
            Setup();
        }


        [Test]
        public void AllDetailReturnValidToken()
        {
            var user = new IdentityUser
            {
                Id = "1",
                UserName = "Test",
                Email = "test@test.com"
            };
            var role = "User";

            var result = _service.CreateToken(user, role);

            Assert.That(string.IsNullOrEmpty(result), Is.False);
        }


        [Test]
        public void WithoutIdReturnValidToken()
        {
            var user = new IdentityUser
            {
                UserName = "Test",
                Email = "test@test.com"
            };
            var role = "User";

            var result = _service.CreateToken(user, role);

            Assert.That(string.IsNullOrEmpty(result), Is.False);
        }

        [Test]
        public void WithoutRoleReturnValidToken()
        {
            var user = new IdentityUser
            {
                Id= "1",
                UserName = "Test",
                Email = "test@test.com"
            };

            string role = null;

            var result = _service.CreateToken(user, role);

            Assert.That(string.IsNullOrEmpty(result), Is.False);
        }

        [Test]
        public void WithoutUsernameThrowsException()
        {
            var user = new IdentityUser
            {
                Id = "1",
                Email = "test@test.com"
            };
            var role = "User";

            Assert.Throws<Exception>(() => _service.CreateToken(user, role));
        }

        [Test]
        public void WithoutEmailThrowsException()
        {
            var user = new IdentityUser
            {
                Id = "1",
                UserName = "Test"
            };
            var role = "User";

            Assert.Throws<Exception>(() => _service.CreateToken(user, role));
        }
    }
}
