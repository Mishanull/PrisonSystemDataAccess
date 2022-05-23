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
    public async Task<ActionResult<Note>> AddNote([FromBody] String[] request)
    {
        try
        {
            Console.WriteLine(request);
            await _noteService.AddNoteAsync(long.Parse(request[0]), request[1]);
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