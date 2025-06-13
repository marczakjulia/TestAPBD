using System.ComponentModel.DataAnnotations;
using Test2.Models;
using Task = Test2.Models.Task;

namespace Test2.DTOs;

public class RecordDTO
{
    public int Id { get; set; }
    public LanguageDTO Language { get; set; }
    public TaskDTO Task { get; set; }
    public StudentDTO Student { get; set; }
    public double ExecutionTime { get; set; }
    public DateTime Created { get; set; }
}







