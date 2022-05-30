using Entities;

namespace Interfaces;

public interface IPrisonerService
{
    Task<Prisoner> CreatePrisonerAsync(Prisoner prisoner);
    Task RemovePrisonerAsync(long id);
    Task<Prisoner> UpdatePrisonerAsync(Prisoner? prisoner);
    Task<Prisoner> GetPrisonerByIdAsync(long id);
    Task<ICollection<Prisoner>> GetPrisonersAsync();
    Task<ICollection<Prisoner>> GetPrisonersAsync(int pageNumber, int pageSize);
    Task<Prisoner> GetPrisonerBySsnAsync(string ssn);
    int GetPrisonerCountAsync();
    Task<ICollection<Prisoner>> GetPrisonersBySectorAsync(int pageNumber, int pageSize, long sectorId);
    Task<List<int>> GetNumPrisPerSectAsync();
    Task AddPointsToPrisonerAsync(long id, int points);
    Task<ICollection<Prisoner>> GetPrisonersWithLowBehaviourAsync();
}