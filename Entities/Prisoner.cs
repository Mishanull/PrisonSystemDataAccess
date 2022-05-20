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
    
    public Sector? Sector { get; set; }
    
    public int Points { get; set; }
    
    //Length of sentence??
    //Note, one? many?
    public string Note { get; set; }

    [Required]
    // public DateTime EntryDate { get; set; }
    public string EntryDate { get; set; }

    [Required]
    public int DurationInMonths { get; set; }
}