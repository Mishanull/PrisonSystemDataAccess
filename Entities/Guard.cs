using System.ComponentModel.DataAnnotations;

namespace Entities;

public class Guard : User
{
    public string  Email { get; set; }
    public string  PhoneNumber { get; set; }
    
}