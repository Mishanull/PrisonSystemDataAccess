using EfcData.Context;
using Entities;
using Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EfcData.DAO;

public class NoteDAO : INoteService
{
    private PrisonSystemContext _prisonSystemContext;

    public NoteDAO(PrisonSystemContext prisonSystemContext)
    {
        _prisonSystemContext = prisonSystemContext;
    }

    public async Task<Note> AddNoteAsync(long prisonerId, string text)
    {
        Prisoner? prisoner = _prisonSystemContext.Prisoners
            .Include(p=>p.Notes)
            .First(p => prisonerId.Equals(p.Id));
        Note note = new Note
        {
            Text = text
        };

        if (prisoner.Sector != null) _prisonSystemContext.Sectors.Attach(prisoner.Sector);
        _prisonSystemContext.Notes.Attach(note);
        prisoner.Notes?.Add(note);
            
        await _prisonSystemContext.SaveChangesAsync();
        return note;
    }

    public async Task RemoveNoteAsync(long noteId)
    {
        Note? note = await _prisonSystemContext.Notes.FindAsync(noteId);
        _prisonSystemContext.Notes.Remove(note!);
        await _prisonSystemContext.SaveChangesAsync();
    }

    public async Task<Note> UpdateNoteAsync(Note note)
    {
        _prisonSystemContext.Notes.Update(note);
            await _prisonSystemContext.SaveChangesAsync();
            return note!;
    }
}