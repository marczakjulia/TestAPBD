using Microsoft.AspNetCore.Mvc;
using Test2.DTOs;
using Test2.Service;

namespace Test2.Controllers;

[ApiController]
[Route("api/records")]
public class RecordController : ControllerBase
{
    private readonly IApplicationService _service;

    public RecordController(IApplicationService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<RecordDTO>>> GetRecords([FromQuery] RecordFilterDTO filter)
    {
        try
        {
            var records = await _service.GetRecordsAsync(filter);
            return Ok(records);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { 
                message = "error when getting recrods", 
                error = ex.Message });
        }
    }

    [HttpPost]
    public async Task<ActionResult<RecordDTO>> CreateRecord([FromBody] CreateRecordDTO record)
    {
        try
        {
            var createdRecord = await _service.CreateRecordAsync(record);
            return Created();
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "errorwhile creating record", error = ex.Message });
        }
    }
} 