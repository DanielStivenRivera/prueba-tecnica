using app_server.Adapters;
using app_server.Application.DTOs;
using app_server.Application.DTOs.Auth;
using Microsoft.AspNetCore.Mvc;

namespace app_server.Controllers;

[ApiController]
[Route("[Controller]")]
public class Auth : ControllerBase
{
    private readonly UserAdapter _userAdapter;

    public Auth(UserAdapter userAdapter)
    {
        _userAdapter = userAdapter;
    }
    
    [HttpPost("register")]
    public IActionResult Register([FromBody] CreateUserRequest createUserRequest)
    {
        string token = _userAdapter.RegisterUser(createUserRequest);
        return Ok(new TokenResponse { token = token });
    }
    
    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginUserRequest loginUserRequest)
    {
        string token =  _userAdapter.LoginUser(loginUserRequest);
        return Ok(new TokenResponse {token = token});
    }
}