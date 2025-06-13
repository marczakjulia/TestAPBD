using System.ComponentModel.DataAnnotations;

namespace Test2.Models;

public class Language
{
    public required int Id { get; set; }
    [Required]
    [StringLength(100)]
    public required string Name { get; set; }
    public ICollection<Record> Records { get; set; }
}