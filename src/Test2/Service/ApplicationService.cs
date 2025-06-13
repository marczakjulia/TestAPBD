using Microsoft.EntityFrameworkCore;
using Test;
using Test2.DTOs;
using Test2.Models;
using Task = Test2.Models.Task;

namespace Test2.Service;

public class ApplicationService : IApplicationService
{
    private readonly ApplicationDbContext _context;

    public ApplicationService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<RecordDTO>> GetRecordsAsync(RecordFilterDTO filter)
    {
        var query = _context.Record
            .Include(r => r.Language)
            .Include(r => r.Task)
            .Include(r => r.Student)
            .AsQueryable();

        if (filter.FromDate.HasValue)
        {
            query = query.Where(r => r.CreatedAt >= filter.FromDate.Value);
        }

        if (filter.ToDate.HasValue)
        {
            query = query.Where(r => r.CreatedAt <= filter.ToDate.Value);
        }

        if (filter.LanguageId.HasValue)
        {
            query = query.Where(r => r.LanguageId == filter.LanguageId.Value);
        }

        if (filter.TaskId.HasValue)
        {
            query = query.Where(r => r.TaskId == filter.TaskId.Value);
        }
        //the ordering as in the task
        var records = await query
            .OrderByDescending(r => r.CreatedAt)
            .ThenBy(r => r.Student.LastName)
            .Select(r => new RecordDTO
            {
                Id = r.Id,
                Language = new LanguageDTO
                {
                    Id = r.Language.Id,
                    Name = r.Language.Name
                },
                Task = new TaskDTO
                {
                    Id = r.Task.Id,
                    Name = r.Task.Name,
                    Description = r.Task.Description
                },
                Student = new StudentDTO
                {
                    Id = r.Student.Id,
                    FirstName = r.Student.FirstName,
                    LastName = r.Student.LastName,
                    Email = r.Student.Email
                },
                ExecutionTime = r.ExecutionTime,
                Created = r.CreatedAt
            })
            .ToListAsync();

        return records;
    }

    public async Task<RecordDTO> CreateRecordAsync(CreateRecordDTO recordDto)
    {
        var language = await _context.Language.FindAsync(recordDto.LanguageId);
        if (language == null)
            throw new KeyNotFoundException($"langage with ID {recordDto.LanguageId} not found");

        var student = await _context.Student.FindAsync(recordDto.StudentId);
        if (student == null)
            throw new KeyNotFoundException($"student with ID {recordDto.StudentId} not found");

        Task task = null;
        if (recordDto.Task != null && recordDto.Task.Id.HasValue)
        {
            task = await _context.Task.FindAsync(recordDto.Task.Id.Value);
            if (task == null)
            {
                if (!string.IsNullOrEmpty(recordDto.Task.Name) && !string.IsNullOrEmpty(recordDto.Task.Description))
                {
                    task = new Task
                    {
                        Name = recordDto.Task.Name,
                        Description = recordDto.Task.Description
                    };
                    _context.Task.Add(task);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    throw new ArgumentException($"task with ID {recordDto.Task.Id.Value} cannot be found or inserted");
                }
            }
        }
        else if (recordDto.Task != null && !string.IsNullOrEmpty(recordDto.Task.Name) && !string.IsNullOrEmpty(recordDto.Task.Description))
        {
            task = new Task
            {
                Name = recordDto.Task.Name,
                Description = recordDto.Task.Description
            };
            _context.Task.Add(task);
            await _context.SaveChangesAsync();
        }
        else
        {
            throw new ArgumentException("the task parameter is required.");
        }

        var record = new Record
        {
            LanguageId = recordDto.LanguageId,
            TaskId = task.Id,
            StudentId = recordDto.StudentId,
            ExecutionTime = (long)recordDto.ExecutionTime,
            CreatedAt = recordDto.Created ?? DateTime.UtcNow //internet told me to do it works so im leaving it 
        };

        _context.Record.Add(record);
        await _context.SaveChangesAsync();

        return new RecordDTO
        {
            Id = record.Id,
            Language = new LanguageDTO
            {
                Id = language.Id,
                Name = language.Name
            },
            Task = new TaskDTO
            {
                Id = task.Id,
                Name = task.Name,
                Description = task.Description
            },
            Student = new StudentDTO
            {
                Id = student.Id,
                FirstName = student.FirstName,
                LastName = student.LastName,
                Email = student.Email
            },
            ExecutionTime = record.ExecutionTime,
            Created = record.CreatedAt
        };
    }
}