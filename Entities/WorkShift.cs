using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities;

public class WorkShift
{
    public long Id { get; set; }
    [Key]
    public string Start { get; set; }
    [Key]
    public string End { get; set; }
    public Sector? Sector { get; set; }
    public ICollection<Guard>? Guards { get; set; }
    
}