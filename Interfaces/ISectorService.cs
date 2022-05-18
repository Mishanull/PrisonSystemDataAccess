using Entities;

namespace Interfaces;

public interface ISectorService
{
    Task<ICollection<Sector>> GetSectorsAsync();
}