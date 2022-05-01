using DAOInterfaces;
using Entities;
using Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DataAcessAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class PrisonerController : ControllerBase
{
    private IPrisonerService _prisonerService;

    public PrisonerController(IPrisonerService prisonerService)
    {
        _prisonerService = prisonerService;
    }
    
    
    [HttpPost]
    public async Task<ActionResult<Prisoner>> CreatePrisoner([FromBody] Prisoner prisoner)
    {
        try
        {
            Prisoner toAdd = await _prisonerService.CreatePrisonerAsync(prisoner);
            return Created($"/Prisoner/{toAdd.Id}", toAdd);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }
    
    [HttpDelete]
    [Route("{id:long}")]
    public async Task<ActionResult<Prisoner>> RemovePrisoner([FromRoute] long id)
    {
        try
        {
            await _prisonerService.RemovePrisonerAsync(id);
            return Ok("Prisoner " + id + " deleted");
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }
    
    [HttpPatch]
    public async Task<ActionResult<Prisoner>> UpdatePrisoner([FromBody] Prisoner? prisoner)
    {
        try
        {
            await _prisonerService.UpdatePrisonerAsync(prisoner);
            return Ok("Prisoner "+prisoner.Id+" updated");

        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }
    
    
    [HttpGet]
    public async Task<ActionResult<Prisoner>> GetPrisoners()
    {
        try
        {
            ICollection<Prisoner> prisoners = await _prisonerService.GetPrisoners();
            PrisonersList prisonersList = new PrisonersList(prisoners);
            return Ok(prisonersList);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }
    
    [HttpGet]
    [Route("{id:long}")]
    public async Task<ActionResult<Prisoner>> GetPrisonerById(long id)
    {
        try
        {
            Prisoner toGet = await _prisonerService.GetPrisonerByIdAsync(id);
            return Ok(toGet);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }
}