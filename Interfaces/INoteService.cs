using Entities;

namespace Interfaces;

public interface INoteService
{
    Task<Note> AddNoteAsync(long prisonerId, string text);
    Task RemoveNoteAsync(long noteId);
    Task<Note> UpdateNoteAsync(Note note);
}