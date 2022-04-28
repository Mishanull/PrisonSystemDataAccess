using Entities;

namespace DAOInterfaces;

public interface IGuardService
{
    public Task<Guard> CreateGuard(Guard guard);
    public Task<Guard> GetGuardById(long id);
}