namespace Test2.DTOs;

public class RecordFilterDTO
{
    public DateTime? FromDate { get; set; }
    public DateTime? ToDate { get; set; }
    public int? LanguageId { get; set; }
    public int? TaskId { get; set; }
}