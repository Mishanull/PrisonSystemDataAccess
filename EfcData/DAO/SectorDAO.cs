using EfcData.Context;
using Entities;
using Interfaces;

namespace EfcData.DAO;

public class SectorDAO :ISectorService
{
    private PrisonSystemContext _context;

    public SectorDAO(PrisonSystemContext context)
    {
        _context = context;
    }

    public async Task<ICollection<Sector>> GetSectorsAsync()
    {
        return _context.Sectors.ToList();
    }
}