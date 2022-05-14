using EfcData.Context;
using Entities;
using Interfaces;
using Microsoft.EntityFrameworkCore;

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
        long largestId = -1;
        if (_prisonSystemContext.WorkShifts.Any())
        {
            largestId = _prisonSystemContext.WorkShifts.Max(p => p.Id);
        }
        shift.Id = ++largestId;
        
        _prisonSystemContext.WorkShifts.Add(shift);
        await _prisonSystemContext.SaveChangesAsync();
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
        
        _prisonSystemContext.Guards.AttachRange(guardToAdd);
        shiftToUpdate.Guards?.Add(guardToAdd);
        
        await _prisonSystemContext.SaveChangesAsync();
        return shiftToUpdate;
    }

    public async Task SetGuardsInWorkShiftAsync(ICollection<Guard> guards, long shiftId)
    {
        WorkShift shiftToUpdate = GetWorkShiftByIdAsync(shiftId).Result;
        
        _prisonSystemContext.Guards.AttachRange(guards);
        shiftToUpdate.Guards = guards;
        await _prisonSystemContext.SaveChangesAsync();
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