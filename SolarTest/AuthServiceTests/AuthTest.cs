using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Moq;
using SolarWatch.Services.AuthServices;

namespace SolarTest.AuthServiceTests
{
    [TestFixture]
    public class AuthTest
    {
        private Mock<UserManager<IdentityUser>> _userManagerMock;
        private Mock<ITokenService> _tokenServiceMock;
        private AuthService _service;

        public void Setup()
        {
            _userManagerMock = new Mock<UserManager<IdentityUser>>(
                Mock.Of<IUserStore<IdentityUser>>(),
                null, null, null, null, null, null, null, null
            ); _tokenServiceMock = new Mock<ITokenService>();
            _service = new(_userManagerMock.Object, _tokenServiceMock.Object);
        }

        public AuthTest()
        {
            Setup();
        }


        [Test]
        public async Task RegisterAsync_WhenUserIsCreated_ReturnsAuthResult()
        {
            // Arrange
            var username = "test";
            var email = "test@test.hu";
            var password = "test123";
            var role = "User";

            _userManagerMock.Setup(x => x.CreateAsync(It.IsAny<IdentityUser>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Success);

            var result = await _service.RegisterAsync(username, email, password, role);

            Assert.That(result.Success, Is.True);
            Assert.That(result.Username, Is.EqualTo(username));
            Assert.That(result.Email, Is.EqualTo(email));
        }

        [Test]
        public async Task RegisterAsync_WhenUserIsNotCreated_ReturnsFaieldAuthResult()
        {
            // Arrange
            var username = "test";
            var email = "test@test.hu";
            var password = "test123";
            var role = "User";

            _userManagerMock.Setup(x => x.CreateAsync(It.IsAny<IdentityUser>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Failed());

            var result = await _service.RegisterAsync(username, email, password, role);

            Assert.That(result.Success, Is.False);
            Assert.That(result.Username, Is.EqualTo(username));
            Assert.That(result.Email, Is.EqualTo(email));
        }

        [Test]
        public async Task LoginAsync_WhenLoginSucces_ReturnsValidResult()
        {
            var username = "test";
            var email = "test@test.hu";
            var password = "test123";
            var token = "this_is_a_token";

            _userManagerMock.Setup(x => x.FindByEmailAsync(email))
                .ReturnsAsync(new IdentityUser { UserName = username, Email = email });

            _userManagerMock.Setup(x => x.CheckPasswordAsync(It.IsAny<IdentityUser>(), It.IsAny<string>()))
                .ReturnsAsync(true);

            _userManagerMock.Setup(x => x.GetRolesAsync(It.IsAny<IdentityUser>()))
                .ReturnsAsync(new List<string> { "User" });

            _tokenServiceMock.Setup(x => x.CreateToken(It.IsAny<IdentityUser>(), It.IsAny<string>()))
                .Returns(token);

            var result = await _service.LoginAsync(email, password);

            Assert.That(result.Success, Is.True);
            Assert.That(result.Email, Is.EqualTo(email));
            Assert.That(result.Username, Is.EqualTo(username));
            Assert.That(result.Token, Is.EqualTo(token));
        }

        [Test]
        public async Task LoginAsync_WhenWrongEmail_ReturnsFailResult()
        {
            var username = "test";
            var email = "";
            var password = "test123";

            _userManagerMock.Setup(x => x.FindByEmailAsync(email))
                .ReturnsAsync((IdentityUser?)null);

            var result = await _service.LoginAsync(email, password);

            Assert.That(result.Success, Is.False);
            Assert.That(result.Email, Is.EqualTo(email));
            Assert.That(result.Username, Is.EqualTo(string.Empty));
            Assert.That(result.Token, Is.EqualTo(string.Empty));
        }

        [Test]
        public async Task LoginAsync_WhenWrongPassword_ReturnsFailResult()
        {
            var username = "test";
            var email = "test@test.hu";
            var password = "";

            _userManagerMock.Setup(x => x.FindByEmailAsync(email))
                .ReturnsAsync(new IdentityUser { UserName = username, Email = email });

            _userManagerMock.Setup(x => x.CheckPasswordAsync(It.IsAny<IdentityUser>(), It.IsAny<string>()))
                .ReturnsAsync(false);

            var result = await _service.LoginAsync(email, password);

            Assert.That(result.Success, Is.False);
            Assert.That(result.Email, Is.EqualTo(email));
            Assert.That(result.Username, Is.EqualTo(username));
            Assert.That(result.Token, Is.EqualTo(string.Empty));
        }
    }
}
