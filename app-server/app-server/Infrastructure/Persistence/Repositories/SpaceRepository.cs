using app_server.Application.interfaces;
using app_server.Domain.Entities;

namespace app_server.Infrastructure.Persistence.Repositories;

public class SpaceRepository : ISpaceRepository
{
    private readonly ApplicationDbContext _context;
    
    public SpaceRepository() { }
    
    public SpaceRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public virtual IEnumerable<Space> Search()
    {
        return _context.Set<Space>().ToList();
    }

    public Space? GetById(int id)
    {
        return _context.Find<Space>(id);
    }
}