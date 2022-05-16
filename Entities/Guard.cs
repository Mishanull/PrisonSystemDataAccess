using System.ComponentModel.DataAnnotations;

namespace Entities;

public class Guard : User
{
    [Required]
    public string  Email { get; set; }
    
    [Required]
    public string  PhoneNumber { get; set; }
    
}