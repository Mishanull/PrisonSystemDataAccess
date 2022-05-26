using EfcData.Context;
using Entities;
using Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EfcData.DAO;

public class PrisonerDAO : IPrisonerService
{
    private PrisonSystemContext _prisonSystemContext;

    public PrisonerDAO(PrisonSystemContext prisonSystemContext)
    {
        _prisonSystemContext = prisonSystemContext;
    }
    
    public async Task<Prisoner> CreatePrisonerAsync(Prisoner prisoner)
    {
        if (prisoner.Sector!.FreeCells == 0) throw new Exception($"Error: Sector {prisoner.Sector.Id} is full");
        long largestId = -1;
        if (_prisonSystemContext.Prisoners.Any())
        {
            largestId = _prisonSystemContext.Prisoners.Max(p => p.Id);
        }

        prisoner.Id = ++largestId;
        
        if (prisoner.Sector != null) _prisonSystemContext.Sectors.Attach(prisoner.Sector);
        _prisonSystemContext.Prisoners.Add(prisoner);
        prisoner.Sector!.OccupiedCells++;
        _prisonSystemContext.Sectors.Update(prisoner.Sector);
        await _prisonSystemContext.SaveChangesAsync();
        return prisoner;
    }

    public async Task RemovePrisonerAsync(long id)
    {
        Prisoner p = await GetPrisonerByIdAsync(id);
        p.Sector!.OccupiedCells--;
        _prisonSystemContext.Sectors.Update(p.Sector);
        
        if (p.Notes != null)
        {
            ICollection<Note> notes = p.Notes;
            foreach (var note in notes)
            {
                _prisonSystemContext.Notes.Remove(note);
            }
        }
        
        _prisonSystemContext.Prisoners?.Remove(_prisonSystemContext.Prisoners
            .First(prisoner => prisoner.Id == id));

        await _prisonSystemContext.SaveChangesAsync();
    }

    public async Task<Prisoner> UpdatePrisonerAsync(Prisoner prisoner)
    {
        if (prisoner.Sector != null) _prisonSystemContext.Sectors.Attach(prisoner.Sector);
        if (prisoner.Notes != null) _prisonSystemContext.Notes.AttachRange(prisoner.Notes);
        _prisonSystemContext.Prisoners.Update(prisoner);
        await _prisonSystemContext.SaveChangesAsync();
        return prisoner;
    }

    public async Task<Prisoner> GetPrisonerByIdAsync(long id)
    {
        Prisoner foundedPrisoner = _prisonSystemContext.Prisoners
            .Include(p=>p.Sector)
            .Include(p=>p.Notes)
            .First(p => id.Equals(p.Id));
        return foundedPrisoner;
    }

    public async Task<ICollection<Prisoner>> GetPrisonersAsync()
    {
        ICollection<Prisoner> p= _prisonSystemContext.Prisoners
            .Include(p=>p.Sector)
            .Include(p=>p.Notes)
            .ToList();
        
        return p;
    }

    public async Task<ICollection<Prisoner>> GetPrisonersAsync(int pageNumber, int pageSize)
    {
        return _prisonSystemContext.Prisoners
            .Skip((pageNumber-1) * pageSize)
            .Take(pageSize)
            .Include(p=>p.Sector)
            .ToList();
    }

    public async Task<Prisoner> GetPrisonerBySsnAsync(string ssn)
    {
        Prisoner prisoner = _prisonSystemContext.Prisoners
            .Include(p => p.Sector)
            .First(p => ssn.Equals(p.Ssn.ToString()));
        return prisoner;
    }
    
    // List of 3 int, with the amount of prisoners per sector. [sect1, sect2, sect3]
    public async Task<List<int>> GetNumPrisPerSectAsync()
    {
        ICollection<Prisoner> prisoners = await GetPrisonersAsync();
        
        int sect1 = 0, sect2 = 0, sect3 = 0;
            foreach (var prisoner in prisoners)
            {
                switch (prisoner.Sector!.Id)
                {
                    case 1:
                        sect1++;
                        break;
                    case 2:
                        sect2++;
                        break;
                    case 3:
                        sect3++;
                        break;
                }
            }
        var numPrisPerSect = new List<int>
        {
            sect1,
            sect2,
            sect3
        };
        return numPrisPerSect;
    }
}