using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities;

public class WorkShift
{
    public long Id { get; set; }
    public DateTime? Start { get; set; }
    public DateTime? End { get; set; }
    public Sector? Sector { get; set; }
    public string? DaysOfWeek { get; set; }
    public ICollection<Guard>? Guards { get; set; }

    public override string ToString()
    {
        return $"Id: {Id}, Start: {Start}, End: {End}, Sector: {Sector}";
    }
}