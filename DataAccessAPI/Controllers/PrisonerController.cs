using System.Text.Json;
using Entities;
using Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DataAccessAPI.Controllers;

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
    public async Task<ActionResult<String>> RemovePrisoner([FromRoute] long id)
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
            return Ok();

        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }
    
    
    [HttpGet]
    public async Task<ActionResult<ICollection<Prisoner>>> GetPrisoners([FromQuery]int pageNumber, [FromQuery]int pageSize)
    {
        try
        {
            ICollection<Prisoner> prisoners;
            if (pageNumber==0 && pageSize==0)
            {
                prisoners = await _prisonerService.GetPrisonersAsync();
            }
            else
            {
                prisoners = await _prisonerService.GetPrisonersAsync(pageNumber, pageSize);
            }
            return Ok(prisoners);
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
            Console.WriteLine(toGet.FirstName);
            return Ok(toGet);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }
    [HttpGet]
    [Route("ssn/{ssn}")]
    public async Task<ActionResult<String>> GetPrisonerBySSN([FromRoute]string ssn)
    {
        try
        {
            Prisoner toGet = await _prisonerService.GetPrisonerBySsnAsync(ssn);
            return Ok(toGet);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }

    [HttpGet]
    [Route("count")]
    public async Task<ActionResult<String>> GetPrisonersCount()
    {
        try
        {
            return Ok(_prisonerService.GetPrisonerCount());
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }

    [HttpGet]
    [Route("sector")]
    public async Task<ActionResult<ICollection<Prisoner>>> GetPrisonersBySector([FromQuery]int pageNumber,[FromQuery]int pageSize,[FromQuery]int sectorId)
    {
        try
        {
            ICollection<Prisoner> prisoners =
                await _prisonerService.GetPrisonersBySectorAsync(pageNumber, pageSize, sectorId);
            return Ok(prisoners);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }
    
    [HttpGet]
    [Route("numPerSect")]
    public async Task<ActionResult<List<int>>> GetNumPrisPerSect()
    {
        try
        {
            var numPrisPerSect = await _prisonerService.GetNumPrisPerSectAsync();
            return Ok(numPrisPerSect);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }
    
    [HttpPatch]
    [Route("addPoints")]
    public async Task<ActionResult<Prisoner>> UpdatePrisoner([FromBody] String[] request)
    {
        try
        {
            long.TryParse(request[0], out var id);
            int.TryParse(request[1], out var points);
            await _prisonerService.AddPointsToPrisoner(id,points);
            return Ok();
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }
    [HttpGet]
    [Route("lowBehaviour")]
    public async Task<ActionResult<ICollection<Prisoner>>> GetPrisoners()
    {
        try
        {
            
                ICollection<Prisoner> prisoners = await _prisonerService.GetPrisonersWithLowBehaviourAsync();
                return Ok(prisoners);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }
}