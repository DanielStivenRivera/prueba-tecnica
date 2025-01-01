using app_server.Application.interfaces;
using app_server.Domain.Entities;
using app_server.Domain.Exceptions;
using app_server.Infrastructure.Persistence.Repositories;
using app_server.Infrastructure.Security;

namespace app_server.Application.services;

public class UserService
{
    private readonly UserRepository _userRepository;

    private JwtTokenGenerator _jwtTokenGenerator;
    
    public UserService() { }
    
    public UserService(UserRepository userRepository, JwtTokenGenerator jwtTokenGenerator)
    {
        _userRepository = userRepository;
        _jwtTokenGenerator = jwtTokenGenerator;
    }

    public virtual string CreateUser(User user)
    {
        if (_userRepository.GetByEmail(user.email) != null)
        {
            throw new UserAlreadyRegisteredException("Email already registered");
        }
        User newUser = _userRepository.Save(user);
        return _jwtTokenGenerator.GenerateToken(newUser.id, newUser.email);
    }
    
    public virtual string LoginUser(string email, string password)
    {
        var user = _userRepository.GetByEmail(email);
        if (user == null)
        {
            throw new InvalidCredentialsException("email/password incorrect");
        }
        if (!BCrypt.Net.BCrypt.Verify(password, user.password))
        {
            throw new InvalidCredentialsException("email/password incorrect");
        }

        return _jwtTokenGenerator.GenerateToken(user.id, user.email);
    }
    
}