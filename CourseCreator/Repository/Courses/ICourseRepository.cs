using CourseCreator.Domain;

namespace CourseCreator.Repository.Courses;

public interface ICourseRepository
{
    public Task<IEnumerable<Course>> GetCoursesAsync();
    
    public Task<Course?> GetCourseAsync(string id);
    
    public Task<Course> CreateCourseAsync(Course course);
    
    public Task UpdateCourseAsync(Course course);
    
    public Task DeleteCourseAsync(string id);
}