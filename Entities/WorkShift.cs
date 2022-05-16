using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities;

public class WorkShift
{
    [Key]
    public long Id { get; set; }
    
    [Required]
    public string Start { get; set; }
    
    [Required]
    public string End { get; set; }
    
    [Required]
    public Sector? Sector { get; set; }
    
    [Required]
    public string? Days { get; set; }
    public ICollection<Guard>? Guards { get; set; }
    
}