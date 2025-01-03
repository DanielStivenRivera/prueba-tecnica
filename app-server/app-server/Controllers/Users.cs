

using app_server.Adapters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace app_server.Controllers;

[ApiController]
[Route("users")]
public class Users : ControllerBase
{
    private readonly UserAdapter _userAdapter;

    public Users(UserAdapter userAdapter)
    {
        _userAdapter = userAdapter;
    }

    [Authorize]
    [HttpGet("{id}")]
    public IActionResult GetUser(int id)
    {
        Console.Write(id);
        var user = _userAdapter.getUserById(id);
        return Ok(user);
    }
}