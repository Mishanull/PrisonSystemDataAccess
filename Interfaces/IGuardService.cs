using Entities;

namespace DAOInterfaces;

public interface IGuardService
{
    Task<Guard> CreateGuardAsync(Guard guard);
    Task<Guard> GetGuardByIdAsync(long id);
    Task RemoveGuardAsync(long id);
    Task<Guard> UpdateGuardAsync(Guard? guard);
    Task<ICollection<Guard>> GetGuards();
    Task<Sector> GetGuardBySector(long id);
    Task<ICollection<Guard>> GetGuardsPerSectTodayAsync(long sectorId);
    Task<List<int>> GetNumGuardsPerSectAsync();
    Task<List<int>> GetNumGuardsPerSectTodayAsync();

    Task<bool> IsAssigned(long id);
    Task<bool> IsWorking(long id);
}