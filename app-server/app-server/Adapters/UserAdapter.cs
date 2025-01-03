using app_server.Application.services;
using app_server.Domain.Entities;

namespace app_server.Adapters;

public class UserAdapter
{
    private readonly UserService _userService;
    
    public UserAdapter() {}

    public UserAdapter(UserService userService)
    {
        _userService = userService;
    }

    public virtual User? getUserById(int id)
    {
        return _userService.getById(id);
    }
    
}