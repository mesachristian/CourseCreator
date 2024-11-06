using System.ComponentModel.DataAnnotations;

namespace CourseCreator.Domain;

public class Course
{
    [Key]
    public Guid Id { get; set; }
    
    public required string Name { get; set; }
    
    public string? Description { get; set; }
}