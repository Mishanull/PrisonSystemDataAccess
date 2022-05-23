using Entities;
using Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DataAccessAPI.Controllers;

[ApiController]
[Route("[controller]")]

public class NoteController : ControllerBase
{
    private INoteService _noteService;

    public NoteController(INoteService noteService)
    {
        _noteService = noteService;
    }
    
    [HttpPost]
    [Route("{prisonerId:long}")]
    public async Task<ActionResult<Note>> AddNote([FromRoute]long prisonerId,[FromBody] string text)
    {
        try
        {
            Console.WriteLine(prisonerId + text);
            await _noteService.AddNoteAsync(prisonerId,text);
            return Ok();

        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }
    
    [HttpDelete]
    [Route("{id:long}")]
    public async Task<ActionResult<Note>> RemoveNote([FromRoute] long id)
    {
        try
        {
            await _noteService.RemoveNoteAsync(id);
            return Ok("Note " + id + " deleted");
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }

    [HttpPatch]
    public async Task<ActionResult<Note>> UpdateNote([FromBody] Note note)
    {
        try
        {
            await _noteService.UpdateNoteAsync(note);
            return Ok();
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }
}