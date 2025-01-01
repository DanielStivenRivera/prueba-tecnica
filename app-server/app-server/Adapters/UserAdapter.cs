using app_server.Application.DTOs;
using app_server.Application.DTOs.Auth;
using app_server.Application.services;
using app_server.Domain.Entities;
using BCrypt.Net;

namespace app_server.Adapters;

public class UserAdapter
{
    private readonly UserService _userService;
    
    public UserAdapter() { }
    
    public UserAdapter(UserService userService)
    {
        _userService = userService;
    }
    
    public virtual string RegisterUser(CreateUserRequest createUserRequest)
    {
        var user = new User
        {
            username = createUserRequest.username,
            password = BCrypt.Net.BCrypt.HashPassword(createUserRequest.password),
            email = createUserRequest.email
        };
        return _userService.CreateUser(user);
    }

    public virtual string LoginUser(LoginUserRequest loginUserRequest)
    {
        if (string.IsNullOrEmpty(loginUserRequest.email))
        {
            throw new ArgumentException("email is required");
        }
        if (string.IsNullOrEmpty(loginUserRequest.password))
        {
            throw new ArgumentException("password is required");
        }
        return _userService.LoginUser(loginUserRequest.email, loginUserRequest.password);
    }

}