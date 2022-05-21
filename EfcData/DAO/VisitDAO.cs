using EfcData.Context;
using Entities;
using Interfaces;

namespace EfcData.DAO;

public class VisitDAO : IVisitService
{
    private PrisonSystemContext _context;

    public VisitDAO(PrisonSystemContext context)
    {
        _context = context;
    }

    public async Task<Visit> CreateVisitAsync(Visit visit)
    {
        //this throws an excpetion if such prisoner does not exist.
        Prisoner p = _context.Prisoners.First(p => p.Ssn == visit.PrisonerSsn);
        
        await _context.Visits.AddAsync(visit);
        await _context.SaveChangesAsync();

        return visit;
    }

    public async Task<ICollection<Visit>> GetVisitsAsync()
    {
        return _context.Visits.ToList();
    }

    public async Task<Visit> GetVisitByAccessCodeAsync(string accessCode)
    {
        return _context.Visits.First(v => v.AccessCode.Equals(accessCode));
    }

    public async Task<Visit> UpdateVisitStatusAsync(long id,Status status)
    {
        Visit? v = await _context.Visits.FindAsync(id);
        v.Status0 = status;
        await _context.SaveChangesAsync();
        return v;
    }
}