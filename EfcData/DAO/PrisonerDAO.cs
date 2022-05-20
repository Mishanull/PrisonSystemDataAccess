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
        
        _prisonSystemContext.Prisoners?.Remove(_prisonSystemContext.Prisoners.First((p => p.Id == id)));

        await _prisonSystemContext.SaveChangesAsync();
    }

    public async Task<Prisoner> UpdatePrisonerAsync(Prisoner prisoner)
    {
        if (prisoner.Sector != null) _prisonSystemContext.Sectors.Attach(prisoner.Sector);
        _prisonSystemContext.Prisoners.Update(prisoner);
        await _prisonSystemContext.SaveChangesAsync();
        return prisoner;
    }

    public async Task<Prisoner> GetPrisonerByIdAsync(long id)
    {
        Prisoner foundedPrisoner = _prisonSystemContext.Prisoners
            .Include(p=>p.Sector)
            .First(p => id.Equals(p.Id));
        return foundedPrisoner;
    }

    public async Task<ICollection<Prisoner>> GetPrisoners()
    {
        return _prisonSystemContext.Prisoners
            .Include(p=>p.Sector)
            .ToList();
    }
}