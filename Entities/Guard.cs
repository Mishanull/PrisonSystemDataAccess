using System.ComponentModel.DataAnnotations;

namespace Entities;

public class Guard : User
{
    // Enum sector? 
    // Enum schedule?
    public string  Email { get; set; }
    public string  PhoneNumber { get; set; }
    
}