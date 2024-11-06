using CourseCreator.Domain;
using CourseCreator.Domain.DTO;
using CourseCreator.Repository.Courses;
using Microsoft.AspNetCore.Mvc;

namespace CourseCreator.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {

        private readonly ICourseRepository _courseRepository;
        private readonly IConfiguration _configuration;
        public CourseController(ICourseRepository courseRepository, IConfiguration configuration)
        {
            _courseRepository = courseRepository;
            _configuration = configuration;
        }

        [HttpGet]
        [Route("connString")]
        public IActionResult GetConnectionString()
        {
            return Ok(_configuration.GetConnectionString("PostgresConnection"));
        }
        
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Course>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCourses()
        {
            return Ok(await _courseRepository.GetCoursesAsync());
        }
        
        [HttpGet]
        [Route("{courseId}")]
        [ProducesResponseType(typeof(Course), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCourse(string courseId)
        {
            return Ok(await _courseRepository.GetCourseAsync(courseId));
        }

        [HttpPost]
        [ProducesResponseType(typeof(Course), StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateCourse([FromBody] CreateCourseDto dto)
        {
            var newCourse = await _courseRepository.CreateCourseAsync(new Course
            {
                Name = dto.Name,
                Description = dto.Description,
            });
            
            var location = Url.Action(nameof(GetCourse), new { courseId = newCourse.Id.ToString() });
            return Created(location, newCourse);
        }

        [HttpDelete]
        [Route("{courseId}")]
        public async Task<IActionResult> DeleteCourse(string courseId)
        {
            await _courseRepository.DeleteCourseAsync(courseId);
            return Ok();
        }
    }
}
