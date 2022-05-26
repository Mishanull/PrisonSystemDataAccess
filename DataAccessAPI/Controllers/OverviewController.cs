using DAOInterfaces;
using Entities;
using Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DataAccessAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class OverViewController : ControllerBase
{
    private IGuardService _guardService;
    private IPrisonerService _prisonerService;
    private IAlertService _alertService;
    private IVisitService _visitService;

    public OverViewController(IGuardService guardService, IPrisonerService prisonerService, IAlertService alertService, IVisitService visitService)
    {
        _guardService = guardService;
        _prisonerService = prisonerService;
        _alertService = alertService;
        _visitService = visitService;
    }
    
    [HttpGet]
    [Route("{sectorId:long}")]
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
    [Route("prisoners")]
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
    
    [HttpGet]
    [Route("guardsSector")]
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
    [Route("guardsSectorToday")]
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
    
    [HttpGet]
    [Route("alertsToday")]
    public async Task<ActionResult<List<int>>> GetAlertsToday()
    {
        try
        {
            var numAlertsToday = await _alertService.GetAlertsTodayAsync();
            return Ok(numAlertsToday);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }
}