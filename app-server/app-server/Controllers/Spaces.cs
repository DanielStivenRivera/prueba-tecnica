using app_server.Adapters;
using Microsoft.AspNetCore.Mvc;

namespace app_server.Controllers;

[ApiController]
[Route("spaces")]
public class Spaces: ControllerBase
{
    private readonly SpaceAdapter _spaceAdapter;
    
    public Spaces(SpaceAdapter spaceAdapter)
    {
        _spaceAdapter = spaceAdapter;
    }
    
    [HttpGet()]
    public IActionResult GetSpaces()
    {
        var spaces = _spaceAdapter.Search();
        return Ok(spaces);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        return Ok(_spaceAdapter.GetById(id));
    }
}