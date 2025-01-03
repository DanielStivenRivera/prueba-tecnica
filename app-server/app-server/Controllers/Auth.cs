using app_server.Adapters;
using app_server.Application.DTOs;
using app_server.Application.DTOs.Auth;
using Microsoft.AspNetCore.Mvc;

namespace app_server.Controllers;

[ApiController]
[Route("auth")]
public class Auth : ControllerBase
{
    private readonly AuthAdapter _authAdapter;

    public Auth(AuthAdapter authAdapter)
    {
        _authAdapter = authAdapter;
    }
    
    [HttpPost("register")]
    public IActionResult Register([FromBody] CreateUserRequest createUserRequest)
    {
        string token = _authAdapter.RegisterUser(createUserRequest);
        return Ok(new TokenResponse { token = token });
    }
    
    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginUserRequest loginUserRequest)
    {
        string token =  _authAdapter.LoginUser(loginUserRequest);
        return Ok(new TokenResponse {token = token});
    }
}