using app_server.Domain.Entities;
using app_server.Infrastructure.Persistence.Repositories;

namespace app_server.Application.services;

public class SpaceService
{
    private readonly SpaceRepository _spaceRepository;
    
    public SpaceService() { }
    
    public SpaceService(SpaceRepository spaceRepository)
    {
        _spaceRepository = spaceRepository;
    }
    
    public virtual IEnumerable<Space> Search()
    {
        return _spaceRepository.Search();
    }
    
    public virtual Space? GetById(int id)
    {
        return this._spaceRepository.GetById(id);
    }
}