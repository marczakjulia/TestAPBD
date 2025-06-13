using Microsoft.EntityFrameworkCore;
using Test2.Models;
using Task = Test2.Models.Task;


namespace Test;

public class ApplicationDbContext: DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
    public DbSet<Language> Language { get; set; }
    public DbSet<Record> Record { get; set; }
    public DbSet<Student> Student { get; set; }
    public DbSet<Task> Task { get; set; }

}