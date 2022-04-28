using Entities;

namespace DAOInterfaces;

public interface IPrisonerService
{
    public Task<Prisoner> CreatePrisonerAsync(Prisoner prisoner);
    public Task RemovePrisonerAsync(long id);
    public Task<Prisoner> UpdatePrisonerAsync(Prisoner? prisoner);
    public Task<Prisoner> GetPrisonerByIdAsync(long id);
}