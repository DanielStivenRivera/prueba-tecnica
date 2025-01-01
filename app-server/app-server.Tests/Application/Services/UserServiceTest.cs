using app_server.Application.services;
using app_server.Domain.Entities;
using app_server.Domain.Exceptions;
using app_server.Infrastructure.Persistence.Repositories;
using app_server.Infrastructure.Security;
using Moq;

namespace app_server.Tests.Application.Services
{
    public class UserServiceTests
    {
        private readonly Mock<UserRepository> _mockUserRepository;
        private readonly Mock<JwtTokenGenerator> _mockJwtTokenGenerator;
        private readonly UserService _userService;

        public UserServiceTests()
        {
            _mockUserRepository = new Mock<UserRepository>();
            _mockJwtTokenGenerator = new Mock<JwtTokenGenerator>();
            _userService = new UserService(_mockUserRepository.Object, _mockJwtTokenGenerator.Object);
        }

        [Fact]
        public void CreateUser_ShouldThrowException_WhenEmailAlreadyRegistered()
        {
            // Arrange
            var user = new User { email = "testuser@example.com" };

            _mockUserRepository.Setup(repo => repo.GetByEmail(user.email)).Returns(new User());

            // Act & Assert
            var exception = Assert.Throws<UserAlreadyRegisteredException>(() => _userService.CreateUser(user));
            Assert.Equal("Email already registered", exception.Message);
        }

        [Fact]
        public void CreateUser_ShouldReturnToken_WhenUserIsCreatedSuccessfully()
        {
            // Arrange
            var user = new User { email = "newuser@example.com", password = "password123" };
            _mockUserRepository.Setup(repo => repo.GetByEmail(user.email)).Returns((User)null);
            _mockUserRepository.Setup(repo => repo.Save(It.IsAny<User>())).Returns(user);
            _mockJwtTokenGenerator.Setup(generator => generator.GenerateToken(user.id, user.email)).Returns("some-jwt-token");

            // Act
            var token = _userService.CreateUser(user);

            // Assert
            Assert.Equal("some-jwt-token", token);
            _mockUserRepository.Verify(repo => repo.Save(It.IsAny<User>()), Times.Once);
            _mockJwtTokenGenerator.Verify(generator => generator.GenerateToken(user.id, user.email), Times.Once);
        }

        [Fact]
        public void LoginUser_ShouldThrowException_WhenUserNotFound()
        {
            // Arrange
            var email = "nonexistent@example.com";
            var password = "password123";
            _mockUserRepository.Setup(repo => repo.GetByEmail(email)).Returns((User)null);

            // Act & Assert
            var exception = Assert.Throws<UnauthorizedAccessException>(() => _userService.LoginUser(email, password));
            Assert.Equal("email/password incorrect", exception.Message);
        }

        [Fact]
        public void LoginUser_ShouldThrowException_WhenPasswordIncorrect()
        {
            // Arrange
            var user = new User { email = "testuser@example.com", password = BCrypt.Net.BCrypt.HashPassword("correctPassword") };
            var email = user.email;
            var incorrectPassword = "wrongPassword";
            _mockUserRepository.Setup(repo => repo.GetByEmail(email)).Returns(user);

            // Act & Assert
            var exception = Assert.Throws<UnauthorizedAccessException>(() => _userService.LoginUser(email, incorrectPassword));
            Assert.Equal("email/password incorrect", exception.Message);
        }

        [Fact]
        public void LoginUser_ShouldReturnToken_WhenLoginIsSuccessful()
        {
            // Arrange
            var user = new User { email = "testuser@example.com", password = BCrypt.Net.BCrypt.HashPassword("correctPassword") };
            var email = user.email;
            var password = "correctPassword";
            _mockUserRepository.Setup(repo => repo.GetByEmail(email)).Returns(user);
            _mockJwtTokenGenerator.Setup(generator => generator.GenerateToken(user.id, user.email)).Returns("some-jwt-token");

            // Act
            var token = _userService.LoginUser(email, password);

            // Assert
            Assert.Equal("some-jwt-token", token);
            _mockUserRepository.Verify(repo => repo.GetByEmail(email), Times.Once);
            _mockJwtTokenGenerator.Verify(generator => generator.GenerateToken(user.id, user.email), Times.Once);
        }
    }
}
