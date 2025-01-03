using app_server.Domain.Entities;

namespace app_server.Application.interfaces;

public interface ISpaceRepository
{
    IEnumerable<Space> Search();

    Space? GetById(int id);
}