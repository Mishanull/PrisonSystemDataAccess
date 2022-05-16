
using System.ComponentModel.DataAnnotations;

namespace Entities;

public class Sector
{
    [Key]
    public long Id { get; set; }
    
    [Required]
    public int Capacity { get; set; }
}