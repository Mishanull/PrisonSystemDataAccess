using Entities;

namespace Interfaces;

public interface IWorkShiftService
{
    Task<WorkShift> CreateWorkShiftAsync(WorkShift shift);
    Task<ICollection<WorkShift>> GetWorkShiftsAsync();
    Task<WorkShift> GetWorkShiftByIdAsync(long id);
    Task<WorkShift> AddGuardToWorkShiftAsync(long guardId, long shiftId);
    Task RemoveWorkShiftAsync(long id);
    Task<WorkShift> UpdateWorkShiftAsync(WorkShift shift);
    Task RemoveGuardFromWorkShiftAsync(long guardId, long shiftId);  
}