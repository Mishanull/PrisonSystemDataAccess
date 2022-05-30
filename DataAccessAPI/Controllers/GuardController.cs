using DAOInterfaces;
using Entities;
using Microsoft.AspNetCore.Mvc;

namespace DataAccessAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class GuardController : ControllerBase
{
    private IGuardService _guardService;

    public GuardController(IGuardService guardService)
    {
        _guardService = guardService;
    }

    [HttpPost]
    public async Task<ActionResult<Guard>> CreateGuard([FromBody] Guard guard)
    {
        try
        {
            Console.WriteLine(CreateGuard);
            Guard toAdd = await _guardService.CreateGuardAsync(guard);
            return Created($"/Guard/{toAdd.Id}", toAdd);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }

    [HttpGet]
    [Route("{id:long}")]
    public async Task<ActionResult<Guard>> GetGuardByIdAsync(long id)
    {
        try
        {
            Guard toGet = await _guardService.GetGuardByIdAsync(id);
            return Ok(toGet);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }
    
    [HttpDelete]
    [Route("{id:long}")]
    public async Task<ActionResult<Guard>> RemoveGuard([FromRoute] long id)
    {
        try
        {
            await _guardService.RemoveGuardAsync(id);
            return Ok("Guard " + id + " deleted");
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }
    
    [HttpPatch]
    public async Task<ActionResult<Guard>> UpdateGuard([FromBody] Guard? guard)
    {
        try
        {
            Console.WriteLine(guard);
            await _guardService.UpdateGuardAsync(guard);
            return Ok();

        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }
    
    
    [HttpGet]
    public async Task<ActionResult<ICollection<Guard>>> GetGuards()
    {
        try
        {
            ICollection<Guard> guards = await _guardService.GetGuards();
            return Ok(guards);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }

    [HttpGet]
    [Route("{id:long}/Sector")]
    public async Task<ActionResult<String>> GetGuardBySector([FromRoute] long id)
    {
        try
        {
            Sector sector = await _guardService.GetGuardBySector(id);
            return Ok(sector);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }
    [HttpGet]
    [Route("guardsSectorToday/{sectorId:long}")]
    public async Task<ActionResult<ICollection<Guard>>> GetGuardsPerSectToday(long sectorId)
    {
        try
        { 
            var guardsSector = await _guardService.GetGuardsPerSectTodayAsync(sectorId);
            return Ok(guardsSector);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    
    [HttpGet]
    [Route("numPerSector")]
    public async Task<ActionResult<List<int>>> GetNumGuardsPerSect()
    {
        try
        {
            var numGuardPerSect = await _guardService.GetNumGuardsPerSectAsync();
            return Ok(numGuardPerSect);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }
    
    [HttpGet]
    [Route("numPerSectorToday")]
    public async Task<ActionResult<List<int>>> GetNumGuardsPerSectToday()
    {
        try
        {
            var numGuardPerSectToday = await _guardService.GetNumGuardsPerSectTodayAsync();
            return Ok(numGuardPerSectToday);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }
    [HttpGet]
    [Route("assigned/{id:long}")]
    public async Task<ActionResult<bool>> IsGuardAssigned([FromRoute] long id)
    {
        try
        {
            bool assigned = await _guardService.IsAssigned(id);
            return Ok(assigned);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }
    [HttpGet]
    [Route("working/{id:long}")]
    public async Task<ActionResult<bool>> IsGuardWorking([FromRoute] long id)
    {
        try
        {
            bool working = await _guardService.IsWorking(id);
            return Ok(working);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }
}