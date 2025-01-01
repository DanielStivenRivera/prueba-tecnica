using app_server.Application.interfaces;
using app_server.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace app_server.Infrastructure.Persistence.Repositories;

public class UserRepository : IUserRepository
{
    
    private readonly ApplicationDbContext _context;
    
    public UserRepository() { }
    

    public UserRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public virtual User Save(User user)
    {
        var newUser = _context.Add<User>(user);
        _context.SaveChanges();
        return newUser.Entity;
    }

    public virtual User? GetById(int id)
    {
        return _context.Find<User>(id);
    }
    
    public IEnumerable<User> GetAll()
    {
        return _context.Set<User>().ToList();
    }
    
    public virtual User? GetByEmail(string email)
    {
        return _context.Set<User>().FirstOrDefault(u => u.email.ToLower() == email.ToLower());
    }
}