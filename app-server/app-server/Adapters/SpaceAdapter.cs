using app_server.Application.services;
using app_server.Domain.Entities;

namespace app_server.Adapters;

public class SpaceAdapter
{
    private readonly SpaceService _spaceService;
    
    public SpaceAdapter() { }
    
    public SpaceAdapter(SpaceService spaceService)
    {
        _spaceService = spaceService;
    }
    
    public virtual IEnumerable<Space> Search()
    {
        return _spaceService.Search();
    }

    public virtual Space? GetById(int id)
    {
        return _spaceService.GetById(id);
    }
    
}