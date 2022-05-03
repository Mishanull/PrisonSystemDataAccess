using System.ComponentModel.DataAnnotations;

namespace Entities;

public class Guard
{
    [Key]
    public long Id { get; set; }
    
    [Required]
    public string Username { get; set; }
    
    [Required]
    public string Password { get; set; }
    
    [Required]
    public string  FirstName { get; set; }
    
    [Required]
    public string  LastName { get; set; }
    // Enum sector? 
    // Enum schedule?
    public string  Email { get; set; }
    public string  PhoneNumber { get; set; }
    public string Role { get; set; }
    
}