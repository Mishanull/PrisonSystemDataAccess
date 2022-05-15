using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities;

public class WorkShift
{
    public long Id { get; set; }
    public string Start { get; set; }
    public string End { get; set; }
    public Sector? Sector { get; set; }
    public ICollection<DaysOfWeek>? DaysOfWeeks { get; set; } //Enum
    public ICollection<Guard>? Guards { get; set; }
    
}