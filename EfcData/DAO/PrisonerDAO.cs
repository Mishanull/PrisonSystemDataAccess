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
        long largestId = -1;
        if (_prisonSystemContext.Prisoners.Any())
        {
            largestId = _prisonSystemContext.Prisoners.Max(p => p.Id);
        }

        prisoner.Id = ++largestId;

        //TODO ...incrementing does not work ???
        prisoner.Sector.OccupiedCells++;
        _prisonSystemContext.Sectors.Update(prisoner.Sector);
        if (prisoner.Sector != null) _prisonSystemContext.Sectors.Attach(prisoner.Sector);
        _prisonSystemContext.Prisoners.Add(prisoner);

        await _prisonSystemContext.SaveChangesAsync();
        return prisoner;
    }

    public async Task RemovePrisonerAsync(long id)
    {
        Prisoner p = await GetPrisonerByIdAsync(id);
        p.Sector.OccupiedCells--;
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
            .Include(p => p.Sector)
            .Include(p => p.Notes)
            .First(p => id.Equals(p.Id));
        return foundedPrisoner;
    }

    public async Task<ICollection<Prisoner>> GetPrisonersAsync()
    {
        ICollection<Prisoner> p = _prisonSystemContext.Prisoners
            .Include(p => p.Sector)
            .Include(p => p.Notes)
            .ToList();

        return p;
    }

    public async Task<ICollection<Prisoner>> GetPrisonersAsync(int pageNumber, int pageSize)
    {
        return _prisonSystemContext.Prisoners
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .Include(p => p.Sector)
            .ToList();
    }

    public async Task<Prisoner> GetPrisonerBySsnAsync(string ssn)
    {
        Prisoner prisoner = _prisonSystemContext.Prisoners
            .Include(p => p.Sector)
            .First(p => ssn.Equals(p.Ssn.ToString()));
        return prisoner;
    }

    public int? GetPrisonerCount()
    {
        return _prisonSystemContext.Prisoners.Count();
    }

    public async Task<ICollection<Prisoner>> GetPrisonersBySectorAsync(int pageNumber, int pageSize, int sectorId)
    {
        return _prisonSystemContext.Prisoners.Where(p=>p.Sector!.Id==sectorId)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .Include(p => p.Sector)
            .ToList();
    }
}