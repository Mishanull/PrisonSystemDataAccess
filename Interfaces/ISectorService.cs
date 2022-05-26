using Entities;

namespace Interfaces;

public interface ISectorService
{
    Task<ICollection<Sector>> GetSectorsAsync();
    Task<Sector> GetSectorByIdAsync(long id);
}