using CourseCreator.Data;
using CourseCreator.Domain;
using Microsoft.EntityFrameworkCore;

namespace CourseCreator.Repository.Courses;

public class EfCourseRepository(AppDbContext appDbContext) : ICourseRepository
{
    public async Task<IEnumerable<Course>> GetCoursesAsync()
    {
        return await appDbContext.Courses.ToListAsync();
    }

    public async Task<Course?> GetCourseAsync(string id)
    {
        if (Guid.TryParse(id, out Guid guid))
        {
            return await appDbContext.Courses.FirstOrDefaultAsync(c => c.Id == guid);
        }
        
        return null;
    }

    public async Task<Course> CreateCourseAsync(Course course)
    {
        var newCourse = await appDbContext.Courses.AddAsync(course);
        await appDbContext.SaveChangesAsync();
        return newCourse.Entity;
    }

    public Task UpdateCourseAsync(Course course)
    {
        throw new NotImplementedException();
    }

    public async Task DeleteCourseAsync(string id)
    {
        if (Guid.TryParse(id, out Guid guid))
        {
            var course = await appDbContext.Courses.FirstOrDefaultAsync(c => c.Id == guid);

            if (course != null)
            {
                appDbContext.Courses.Remove(course);
                await appDbContext.SaveChangesAsync();
            }
        }
    }
}