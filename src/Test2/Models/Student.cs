using System.ComponentModel.DataAnnotations;

namespace Test2.Models;

public class Student
{
    public int Id { get; set; }
    [Required]
    [StringLength(100)]
    public required string FirstName { get; set; }
    [Required]
    [StringLength(100)]
    public required string LastName { get; set; }
    [Required]
    [StringLength(250)]
    public required string Email { get; set; }
    public ICollection<Record> Records { get; set; }
    
}