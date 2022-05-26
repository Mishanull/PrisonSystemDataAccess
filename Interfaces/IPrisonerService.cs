using Entities;

namespace Interfaces;

public interface IPrisonerService
{
    public Task<Prisoner> CreatePrisonerAsync(Prisoner prisoner);
    public Task RemovePrisonerAsync(long id);
    public Task<Prisoner> UpdatePrisonerAsync(Prisoner? prisoner);
    public Task<Prisoner> GetPrisonerByIdAsync(long id);
    Task<ICollection<Prisoner>> GetPrisonersAsync();
    Task<ICollection<Prisoner>> GetPrisonersAsync(int pageNumber, int pageSize);
    Task<Prisoner> GetPrisonerBySsnAsync(string ssn);
    public int? GetPrisonerCount();
    Task<ICollection<Prisoner>> GetPrisonersBySectorAsync(int pageNumber, int pageSize, int sectorId);
}