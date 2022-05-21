using System.Collections;
using System.Collections.ObjectModel;
using System.Text.Json;
using Entities;
using Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DataAccessAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class AlertController : ControllerBase
{

    private IAlertService _alertService;

    public AlertController(IAlertService alertService)
    {
        _alertService = alertService;
    }

    
    [HttpPost]
    public async Task ArchiveAlert([FromBody] Alert alert)
    {
        try
        {
            await _alertService.AddAlertAsync(alert);
            Console.WriteLine(alert);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }
    [HttpGet]
    public async Task<ActionResult<String>> GetAlerts()
    {
        try
        {
            ICollection<Alert> alerts = await _alertService.getAlertsAsync();
            String response=JsonSerializer.Serialize(alerts);
            return Ok(response);
        }
        catch(Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }
}