using Entities;

namespace DAOInterfaces;

public interface IGuardService
{
    Task<Guard> CreateGuardAsync(Guard guard);
    Task<Guard> GetGuardByIdAsync(long id);
    Task RemoveGuardAsync(long id);
    Task<Guard> UpdateGuardAsync(Guard? guard);
    Task<ICollection<Guard>> GetGuardsAsync();
    Task<Sector> GetGuardSector(long id);
    Task<ICollection<Guard>> GetGuardsPerSectTodayAsync(long sectorId);
    Task<List<int>> GetNumGuardsPerSectAsync();
    Task<List<int>> GetNumGuardsPerSectTodayAsync();
    Task<bool> IsAssignedAsync(long id);
    Task<bool> IsWorkingAsync(long id);
}