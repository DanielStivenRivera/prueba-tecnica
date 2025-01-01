using app_server.Domain.Entities;

namespace app_server.Application.interfaces;

public interface IUserRepository
{
    User Save(User user);
    User? GetById(int id);
    IEnumerable<User> GetAll();
    User? GetByEmail(string email);
    
}