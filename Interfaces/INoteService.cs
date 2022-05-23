using Entities;

namespace Interfaces;

public interface INoteService
{
    public Task<Note> AddNoteAsync(long prisonerId, string text);
    public Task RemoveNoteAsync(long noteId);
    public Task<Note> UpdateNoteAsync(Note note);
}