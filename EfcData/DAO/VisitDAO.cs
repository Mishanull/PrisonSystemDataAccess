﻿using EfcData.Context;
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

    public async Task<Visit> GetVisitByAccessCodeAsync(string accessCode)
    {
        try
        {
            Visit v = _context.Visits.First(v => v.AccessCode!.Equals(accessCode));
            return v;
        }
        catch (InvalidOperationException)
        {
            return new Visit
            {
                AccessCode = "invalid"
            };
        }
    }

    public async Task<Visit> UpdateVisitStatusAsync(long id,Status status,String code)
    {
        Visit? v = await _context.Visits.FindAsync(id);
        v.Status = status;
        v.AccessCode = code;
        await _context.SaveChangesAsync();
        return v;
    }

    public async Task<ICollection<Visit>> GetVisitsAsync(int pageNumber, int pageSize)
    {
        return _context.Visits.Skip((pageNumber-1)*pageSize).Take(pageSize).ToList();
    }
}