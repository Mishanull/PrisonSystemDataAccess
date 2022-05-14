using Entities;

namespace Interfaces;

public interface IWorkShiftService
{
    public Task<WorkShift> CreateWorkShiftAsync(WorkShift shift);
    public Task<ICollection<WorkShift>> GetWorkShiftsAsync();
    public Task<WorkShift> AddGuardToWorkShiftAsync(long guardId, long shiftId);
    public Task SetGuardsInWorkShiftAsync(ICollection<Guard> guards, long shiftId);
    public Task RemoveWorkShiftAsync(long id);
    public Task<WorkShift> UpdateWorkShiftAsync(WorkShift shift);
    public Task RemoveGuardFromWorkShiftAsync(long guardId, long shiftId);  
}