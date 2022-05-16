using EfcData.Context;
using Entities;
using Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;

namespace EfcData.DAO;

public class WorkShiftDAO : IWorkShiftService
{
    private PrisonSystemContext _prisonSystemContext;

    public WorkShiftDAO(PrisonSystemContext prisonSystemContext)
    {
        _prisonSystemContext = prisonSystemContext;
    }
    
    public async Task<WorkShift> CreateWorkShiftAsync(WorkShift shift)
    {
        try{
            long largestId = -1;
            if (_prisonSystemContext.WorkShifts.Any())
            {
                largestId = _prisonSystemContext.WorkShifts.Max(p => p.Id);
            }

            shift.Id = ++largestId;

            if (shift.Sector != null) _prisonSystemContext.Sectors.Attach(shift.Sector);
            if (shift.Guards != null) _prisonSystemContext.Guards.AttachRange(shift.Guards);
            _prisonSystemContext.WorkShifts.Add(shift);
            await _prisonSystemContext.SaveChangesAsync();
            return shift;
        }
        catch(Exception ex)
        {
            Console.WriteLine(ex.Message );
        }

        return shift;
    }

    public async Task<ICollection<WorkShift>> GetWorkShiftsAsync()
    {
        ICollection<WorkShift> shifts = await _prisonSystemContext.WorkShifts
            .Include(shift => shift.Guards)
            .ToListAsync();
        return shifts;
    }

    public async Task<WorkShift> AddGuardToWorkShiftAsync(long guardId, long shiftId)
    {
        WorkShift shiftToUpdate = GetWorkShiftByIdAsync(shiftId).Result;
        Guard? guardToAdd = await _prisonSystemContext.Guards.FindAsync(guardId);

        if (shiftToUpdate.Sector != null) _prisonSystemContext.Sectors.Attach(shiftToUpdate.Sector);
        if (guardToAdd != null)
        {
            _prisonSystemContext.Guards.AttachRange(guardToAdd);
            shiftToUpdate.Guards?.Add(guardToAdd);
        }

        await _prisonSystemContext.SaveChangesAsync();
        return shiftToUpdate;
    }

    public async Task RemoveWorkShiftAsync(long shiftId)
    {
        WorkShift shiftToRemove = GetWorkShiftByIdAsync(shiftId).Result;;
        
        _prisonSystemContext.Remove(shiftToRemove);
        await _prisonSystemContext.SaveChangesAsync();
    }

    public async Task<WorkShift> UpdateWorkShiftAsync(WorkShift shift)
    {
        _prisonSystemContext.WorkShifts.Update(shift);
        await _prisonSystemContext.SaveChangesAsync();
        return shift;
    }

    public async Task RemoveGuardFromWorkShiftAsync(long guardId, long shiftId)
    {
        WorkShift shiftToUpdate = GetWorkShiftByIdAsync(shiftId).Result;
        Guard? guardToRemove = await _prisonSystemContext.Guards.FindAsync(guardId);
        
        if (guardToRemove != null) shiftToUpdate.Guards?.Remove(guardToRemove);
        await _prisonSystemContext.SaveChangesAsync();
    }
    
    private Task<WorkShift> GetWorkShiftByIdAsync(long id)
    {
        WorkShift? shift = _prisonSystemContext.WorkShifts
            .Include(shift => shift.Guards)
            .First(g => g.Id == id);
        return Task.FromResult(shift);
    }
}