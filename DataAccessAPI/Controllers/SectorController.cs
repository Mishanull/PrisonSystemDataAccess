using Entities;
using Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DataAccessAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class SectorController :ControllerBase
{
    private ISectorService _sectorService;

    public SectorController(ISectorService sectorService)
    {
        _sectorService = sectorService;
    }
    
    [HttpGet]
    public async Task<ActionResult<SectorList>> GetSectors()
    {
        try
        {
            ICollection<Sector> sectors = await _sectorService.GetSectorsAsync();
            SectorList sectorList = new(sectors);
            return Ok(sectorList);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }
}