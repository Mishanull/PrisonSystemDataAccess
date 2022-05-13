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
    public async Task<ActionResult<GuardsList>> GetGuards()
    {
        try
        {
            ICollection<Guard> guards = await _guardService.GetGuards();
            GuardsList guardsList = new(guards);
            return Ok(guardsList);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }

}