using DAOInterfaces;
using Entities;
using Microsoft.AspNetCore.Mvc;

namespace DataAcessAPI.Controllers;

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
            Guard toAdd = await _guardService.CreateGuard(guard);
            return Created($"/Guard/{toAdd.Id}", toAdd);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }

    [HttpGet]
    [Route("{id:long}")]
    public async Task<ActionResult<Guard>> GetGuardById(long id)
    {
        try
        {
            Guard toGet = await _guardService.GetGuardById(id);
            return Ok(toGet);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }

}