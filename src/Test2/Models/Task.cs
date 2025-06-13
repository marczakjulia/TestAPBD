using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Test2.Models;

public class Task
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public int Id { get; set; }
    [Required]
    [StringLength(100)]
    public required string Name { get; set; }
    [Required]
    [StringLength(20000)]
    public required string Description { get; set; }
    public ICollection<Record> Records { get; set; }
}