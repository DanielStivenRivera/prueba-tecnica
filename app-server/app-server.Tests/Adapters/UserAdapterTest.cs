using app_server.Adapters;
using app_server.Application.DTOs.Auth;
using app_server.Application.services;
using app_server.Domain.Entities;
using Moq;

namespace app_server.Tests.Adapters
{
    public class UserAdapterTests
    {
        private readonly Mock<UserService> _mockUserService;
        private readonly UserAdapter _userAdapter;

        public UserAdapterTests()
        {
            _mockUserService = new Mock<UserService>();
            _userAdapter = new UserAdapter(_mockUserService.Object);
        }

        [Fact]
        public void RegisterUser_ShouldCallCreateUser_WhenValidRequest()
        {
            // Arrange
            var createUserRequest = new CreateUserRequest
            {
                username = "testUser",
                password = "testPassword",
                email = "testuser@example.com"
            };

            // Setup: Simula el comportamiento de CreateUser
            _mockUserService.Setup(service => service.CreateUser(It.IsAny<User>())).Returns("UserCreated");

            // Act
            var result = _userAdapter.RegisterUser(createUserRequest);

            // Assert
            Assert.Equal("UserCreated", result);
            _mockUserService.Verify(service => service.CreateUser(It.Is<User>(user =>
                user.username == createUserRequest.username &&
                user.email == createUserRequest.email &&
                BCrypt.Net.BCrypt.Verify(createUserRequest.password, user.password) // Verificamos que la contraseña esté hasheada
            )), Times.Once);
        }

        [Fact]
        public void RegisterUser_ShouldThrowException_WhenInvalidRequest()
        {
            // Arrange
            var createUserRequest = new CreateUserRequest
            {
                username = "testUser",
                password = "",
                email = "testuser@example.com"
            };

        }

        [Fact]
        public void LoginUser_ShouldThrowException_WhenEmailIsEmpty()
        {
            // Arrange
            var loginUserRequest = new LoginUserRequest
            {
                email = "",
                password = "testPassword"
            };

            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() => _userAdapter.LoginUser(loginUserRequest));
            Assert.Equal("email is required", exception.Message);
        }

        [Fact]
        public void LoginUser_ShouldThrowException_WhenPasswordIsEmpty()
        {
            // Arrange
            var loginUserRequest = new LoginUserRequest
            {
                email = "testuser@example.com",
                password = ""
            };

            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() => _userAdapter.LoginUser(loginUserRequest));
            Assert.Equal("password is required", exception.Message);
        }

        [Fact]
        public void LoginUser_ShouldCallLoginUser_WhenValidRequest()
        {
            // Arrange
            var loginUserRequest = new LoginUserRequest
            {
                email = "testuser@example.com",
                password = "testPassword"
            };

            // Setup: Simula el comportamiento de LoginUser
            _mockUserService.Setup(service => service.LoginUser(loginUserRequest.email, loginUserRequest.password)).Returns("LoginSuccess");

            // Act
            var result = _userAdapter.LoginUser(loginUserRequest);

            // Assert
            Assert.Equal("LoginSuccess", result);
            _mockUserService.Verify(service => service.LoginUser(loginUserRequest.email, loginUserRequest.password), Times.Once);
        }
    }
}
