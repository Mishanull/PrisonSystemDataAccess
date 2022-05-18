using Entities;
using Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DataAccessAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class WorkShiftController : ControllerBase
{
    private IWorkShiftService _workShiftService;

    public WorkShiftController(IWorkShiftService workShiftService)
    {
        _workShiftService = workShiftService;
    }
    
    [HttpPost]
    public async Task<ActionResult<String>> CreateWorkShift([FromBody] WorkShift shift)
    {
        try
        {
            WorkShift toCreate = await _workShiftService.CreateWorkShiftAsync(shift);
            return Created($"/WorkShift/{toCreate.Id}", toCreate);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }

    [HttpGet]
    public async Task<ActionResult<ICollection<WorkShift>>> GetWorkShifts()
    {
        try
        {
            ICollection<WorkShift> shifts = await _workShiftService.GetWorkShiftsAsync();
            WorkShiftList workShiftList = new(shifts);
            return Ok(workShiftList);

        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }
    
    [HttpGet]
    [Route("{id:long}")]
    public async Task<ActionResult<WorkShift>> GetWorkShiftById(long id)
    {
        try
        {
            WorkShift workShift = await _workShiftService.GetWorkShiftByIdAsync(id);
            return Ok(workShift);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }

    [HttpPatch]
    [Route("addGuard/{guardId:int}/{shiftId:int}")]
    public async Task<ActionResult<String>> AddGuardToWorkShift([FromRoute] long guardId, [FromRoute] long shiftId)
    {
        try
        {
            await _workShiftService.AddGuardToWorkShiftAsync(guardId, shiftId);
            return Ok("Guard " + guardId + " added to shift " + shiftId);

        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }

    [HttpDelete]
    [Route("{shiftId:long}")]
    public async Task<ActionResult<String>> RemoveWorkShift([FromRoute] long shiftId)
    {
        try
        {
            await _workShiftService.RemoveWorkShiftAsync(shiftId);
            return Ok("Shift " + shiftId + " deleted");
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }

    [HttpPatch]
    public async Task<ActionResult<WorkShift>> UpdateWorkShift([FromBody] WorkShift shift)
    {
        try
        {
            await _workShiftService.UpdateWorkShiftAsync(shift);
            return Ok();

        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }

    [HttpPatch]
    [Route("removeGuard/{guardId:int}/{shiftId:int}")]
    public async Task<ActionResult<WorkShift>> RemoveGuardFromWorkShift([FromRoute] long guardId, [FromRoute] long shiftId)
    {
        try
        {
            await _workShiftService.RemoveGuardFromWorkShiftAsync(guardId, shiftId);
            return Ok();

        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }
}