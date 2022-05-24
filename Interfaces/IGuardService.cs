using Entities;

namespace DAOInterfaces;

public interface IGuardService
{
    public Task<Guard> CreateGuardAsync(Guard guard);
    public Task<Guard> GetGuardByIdAsync(long id);
    public Task RemoveGuardAsync(long id);
    public Task<Guard> UpdateGuardAsync(Guard? guard);
    Task<ICollection<Guard>> GetGuards();
    Task<Sector> GetGuardBySector(long id);
}