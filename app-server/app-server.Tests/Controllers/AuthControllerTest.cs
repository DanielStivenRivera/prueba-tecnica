using app_server.Adapters;
using app_server.Application.DTOs.Auth;
using app_server.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;


namespace app_server.Tests.Controllers;

public class AuthControllerTest
{
    private readonly Mock<AuthAdapter> _mockUserAdapter;

    private readonly Auth _authController;

    public AuthControllerTest()
    {
        _mockUserAdapter = new Mock<AuthAdapter>();
        _authController = new Auth(_mockUserAdapter.Object);
    }

    [Fact]
    public void Register_ShouldReturnOkResult_WithTokenResponse()
    {
        var createUserRequest = new CreateUserRequest
        {
            username = "test",
            password = "test",
            email = "email@email.com"
        };

        string token = "mockedtoken";

        _mockUserAdapter.Setup(adapter => adapter.RegisterUser(createUserRequest)).Returns(token);
        
        //Act
        
        var result = _authController.Register(createUserRequest) as OkObjectResult;
        
        //Assert
        Assert.NotNull(result);
        Assert.Equal(200, result.StatusCode);
        var response = Assert.IsType<TokenResponse>(result.Value);
        Assert.Equal(token, response.token);

    }

    [Fact]
    public void Login_ShouldReturnOkResult_WithTokenResponse()
    {
        LoginUserRequest loginUserRequest = new LoginUserRequest
        {
            email = "email@email.com",
            password = "test"
        };
        string token = "mockedtoken";
        
        // Act
        var result = _authController.Login(loginUserRequest) as OkObjectResult;
        
        // Assert
        Assert.NotNull(result);
        Assert.Equal(200, result.StatusCode);
        var response = Assert.IsType<TokenResponse>(result.Value);
    }

}