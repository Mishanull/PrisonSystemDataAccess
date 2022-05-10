using EfcData.Context;
using Entities;
using Interfaces;

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
        _prisonSystemContext.Prisoners.Add(prisoner);
        await _prisonSystemContext.SaveChangesAsync();
        return prisoner;
    }

    public async Task RemovePrisonerAsync(long id)
    {
        _prisonSystemContext.Prisoners?.Remove(_prisonSystemContext.Prisoners.First((p => p.Id == id)));
        await _prisonSystemContext.SaveChangesAsync();
    }

    public async Task<Prisoner> UpdatePrisonerAsync(Prisoner prisoner)
    {
        Prisoner? prisonerToUpdate = GetPrisonerByIdAsync(prisoner.Id).Result;
        prisonerToUpdate.FirstName = prisoner.FirstName;
        prisonerToUpdate.LastName = prisoner.LastName;
        prisonerToUpdate.Ssn = prisoner.Ssn;
        prisonerToUpdate.Points = prisoner.Points;
        await _prisonSystemContext.SaveChangesAsync();
        return prisonerToUpdate;
    }

    public async Task<Prisoner> GetPrisonerByIdAsync(long id)
    {
        Prisoner foundedPrisoner = _prisonSystemContext.Prisoners.First(p => id.Equals(p.Id));
        return foundedPrisoner;
    }

    public async Task<ICollection<Prisoner>> GetPrisoners()
    {
        return _prisonSystemContext.Prisoners.ToList();
    }
}