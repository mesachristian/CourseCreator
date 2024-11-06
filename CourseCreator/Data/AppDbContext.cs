using CourseCreator.Domain;
using Microsoft.EntityFrameworkCore;

namespace CourseCreator.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Course> Courses { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Course>().HasData(
            new Course
            {
                Id = Guid.NewGuid(),
                Name = "Course 1",
                Description = "Test Course 1"
            },
            new Course
            {
                Id = Guid.NewGuid(),
                Name = "Course 2",
                Description = "Test Course 2"
            },
            new Course
            {
                Id = Guid.NewGuid(),
                Name = "Course 3",
                Description = "Test Course 3"
            }
            );
    }
}