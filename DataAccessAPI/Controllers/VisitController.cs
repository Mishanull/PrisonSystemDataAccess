using Entities;
using Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DataAccessAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class VisitController : ControllerBase
{
    private IVisitService _visitService;

    public VisitController(IVisitService visitService)
    {
        _visitService = visitService;
    }
    
    [HttpGet]
    public async Task<ActionResult<ICollection<Visit>>> GetVisits()
    {
        try
        {
            ICollection<Visit> visits = await _visitService.GetVisitsAsync();
            return Ok(visits);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }
    
    [HttpGet]
    [Route("{accessCode}")]
    public async Task<ActionResult<ICollection<Visit>>> GetVisitByAccessCode([FromRoute] string accessCode)
    {
        try
        {
            Visit visit = await _visitService.GetVisitByAccessCodeAsync(accessCode);
            return Ok(visit);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }
    
    [HttpPost]
    public async Task<ActionResult<Visit>> CreateVisit([FromBody] Visit visit)
    {
        try
        {
            Visit created = await _visitService.CreateVisitAsync(visit);
            return Ok(created);
            // return Created($"/Visit/{created.Id}", created);
        }
        catch (Exception e)
        {
            Console.WriteLine("no such prisoner exists");
            return StatusCode(500, e.Message);
        }
    }
    
    [HttpPatch]
    public async Task<ActionResult<Visit>> UpdateVisitStatus([FromBody] Tuple<long,Visit.Status> idStatusPair)
    {
        try
        {
            Visit updated = await _visitService.UpdateVisitStatusAsync(idStatusPair.Item1, idStatusPair.Item2);
            return Ok(updated);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }
}