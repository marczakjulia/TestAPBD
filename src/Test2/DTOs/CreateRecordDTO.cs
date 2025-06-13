using System.ComponentModel.DataAnnotations;

namespace Test2.DTOs;

public class CreateRecordDTO
{
    [Required]
    public required int LanguageId { get; set; }
    [Required]
    public int StudentId { get; set; }
    [Required]
    public  required TaskInputDTO Task { get; set; }
    [Required]
    public required double ExecutionTime { get; set; }
    public DateTime? Created { get; set; }
}
