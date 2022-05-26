using DAOInterfaces;
using Entities;
using Interfaces;

namespace FileContext.Prisoners;

public class PrisonerFileDAO : IPrisonerService
{
    private PrisonerFileContext _prisonerFileContext;

    public PrisonerFileDAO(PrisonerFileContext prisonerFileContext)
    {
        _prisonerFileContext = prisonerFileContext;
    }
    public async Task<Prisoner> CreatePrisonerAsync(Prisoner prisoner)
    {
        long largestId = -1;
        if (_prisonerFileContext.Prisoners.Any())
        {
            largestId = _prisonerFileContext.Prisoners.Max(p => p.Id);
        }

        prisoner.Id = ++largestId;
        _prisonerFileContext.Prisoners.Add(prisoner);
        await _prisonerFileContext.SaveChangesAsync();
        return prisoner;
    }

    public async Task RemovePrisonerAsync(long id)
    {
        _prisonerFileContext.Prisoners?.Remove(_prisonerFileContext.Prisoners.First((p => p.Id == id)));
        await _prisonerFileContext.SaveChangesAsync();
    }

    public async Task<Prisoner> UpdatePrisonerAsync(Prisoner prisoner)
    {
        Prisoner? prisonerToUpdate = GetPrisonerByIdAsync(prisoner.Id).Result;
        prisonerToUpdate.FirstName = prisoner.FirstName;
        prisonerToUpdate.LastName = prisoner.LastName;
        prisonerToUpdate.Ssn = prisoner.Ssn;
        prisonerToUpdate.Points = prisoner.Points;
        await _prisonerFileContext.SaveChangesAsync();
        return prisonerToUpdate;
    }

    public async Task<Prisoner> GetPrisonerByIdAsync(long id)
    {
        Prisoner foundedPrisoner = _prisonerFileContext.Prisoners.First(p => id.Equals(p.Id));
        return foundedPrisoner;
    }

    public async Task<ICollection<Prisoner>> GetPrisonersAsync()
    {
        return _prisonerFileContext.Prisoners.ToList();
    }

    public Task<ICollection<Prisoner>> GetPrisonersAsync(int pageNumber, int pageSize)
    {
        throw new NotImplementedException();
    }

    public Task<Prisoner> GetPrisonerBySsnAsync(string ssn)
    {
        throw new NotImplementedException();
    }

    public int? GetPrisonerCount()
    {
        throw new NotImplementedException();
    }

    public Task<ICollection<Prisoner>> GetPrisonersBySectorAsync(int pageNumber, int pageSize, int sectorId)
    {
        throw new NotImplementedException();
    }

    public Task<List<int>> GetNumPrisPerSectAsync()
    {
        throw new NotImplementedException();
    }
}