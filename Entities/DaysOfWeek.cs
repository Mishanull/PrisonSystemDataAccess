using System.ComponentModel.DataAnnotations;

namespace Entities;

public class DaysOfWeek
{
    [Key]
    public long Id { get; set; }
    public DayOfWeeks Day { get; set; }

    public enum DayOfWeeks
    {
        Sunday = 0,
        Monday = 1,
        Tuesday = 2,
        Wednesday = 3,
        Thursday = 4,
        Friday = 5,
        Saturday = 6,
    }
    
    public ICollection<WorkShift>? WorkShifts { get; set; }
}