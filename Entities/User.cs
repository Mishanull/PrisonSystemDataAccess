using System.ComponentModel.DataAnnotations;

namespace Entities;


public class User
{
    [Key]
    public long Id { get; set; }
    
    [Required]
    public string Username { get; set; }
    
    [Required]
    public string FirstName { get; set; }
    
    [Required]
    public string LastName { get; set; }
    
    [Required]
    public string Password { get; set; }
    [Required]
    public string Role { get; set; }

}
