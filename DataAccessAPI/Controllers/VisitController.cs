using System.Text.Json;
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
    public async Task<ActionResult<ICollection<Visit>>> GetVisits([FromQuery]int pageNumber, [FromQuery]int pageSize)
    {
        try
        {
            ICollection<Visit> visits = await _visitService.GetVisitsAsync(pageNumber, pageSize);
            return Ok(visits);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }
    
    [HttpGet]
    [Route("{accessCode}")]
    public async Task<ActionResult<Visit>> GetVisitByAccessCode([FromRoute] string accessCode)
    {
        try
        {
            Visit visit = await _visitService.GetVisitByAccessCodeAsync(accessCode);
            String response=JsonSerializer.Serialize(visit);
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
    public async Task<ActionResult<String>> UpdateVisitStatus([FromBody] String[] request)
    {
        try
        {
            string? accessCode = null;
            if (request.Length == 3)
            {
                 accessCode = request[2];
            }
            Enum.TryParse(request[1], out Status status);
            Visit updated = await _visitService.UpdateVisitStatusAsync(long.Parse(request[0]),status,accessCode!);
            return Ok("success");
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }
    
    [HttpGet]
    [Route("visitsToday")]
    public async Task<ActionResult<List<int>>> GetVisitsToday()
    {
        try
        {
            var numVisitsToday = await _visitService.GetNumVisitsTodayAsync();
            return Ok(numVisitsToday);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }
}