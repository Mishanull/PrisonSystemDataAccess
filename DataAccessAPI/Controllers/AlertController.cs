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
}