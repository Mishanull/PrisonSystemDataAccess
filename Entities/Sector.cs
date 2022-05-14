using System.ComponentModel.DataAnnotations;

namespace Entities;

public class Sector
{
    [Key]
    public long Id { get; set; }
    public int Capacity { get; set; }
}