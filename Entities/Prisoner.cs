using System.ComponentModel.DataAnnotations;

namespace Entities;

public class Prisoner
{
    [Key]
    public long Id { get; set; }
    
    [Required]
    public string  FirstName { get; set; }
    
    [Required]
    public string  LastName { get; set; } 
    
    [Required]
    public int Ssn { get; set; }
    
    [Required]
    public string CrimeCommitted { get; set; }
    
    
    public int Points { get; set; }
    
    
    public string Note { get; set; }
}