namespace Entities;

public class WorkShiftList
{
    public ICollection<WorkShift> WorkShifts { get; set; }

    public WorkShiftList()
    {
        WorkShifts = new List<WorkShift>();
    }

    public WorkShiftList(ICollection<WorkShift> workShifts)
    {
        WorkShifts = workShifts;
    }
    
}