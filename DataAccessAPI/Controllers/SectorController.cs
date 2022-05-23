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
    public async Task<ActionResult<ICollection<Sector>>> GetSectors()
    {
        try
        {
            ICollection<Sector> sectors = await _sectorService.GetSectorsAsync();
            return Ok(sectors);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }
}