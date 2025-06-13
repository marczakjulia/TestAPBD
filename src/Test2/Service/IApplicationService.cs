using Test2.DTOs;
using Test2.Models;

namespace Test2.Service;

public interface IApplicationService
{
    Task<IEnumerable<RecordDTO>> GetRecordsAsync(RecordFilterDTO filter);
    Task<RecordDTO> CreateRecordAsync(CreateRecordDTO record);
}