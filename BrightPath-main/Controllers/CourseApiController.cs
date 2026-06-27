using BrightPath.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BrightPath.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CourseApiController : Controller
    {
        private readonly dbContext _dbcontext;
        public CourseApiController(dbContext dbContext)
        {
            _dbcontext = dbContext;
        }

        [HttpGet]
        public IActionResult GetCourse()
        {
            var courses = _dbcontext.courses.Where(c => !c.IsDeleted).ToList();
            return Ok(courses);
        }
        [HttpGet("{id}")]
        public IActionResult getcoursebyId(int id)
        {
            var courses = _dbcontext.courses.Where(c => c.courseId == id).ToList();
            if (courses == null)
            {
                return NotFound();
            }
            return Ok(courses);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult AddCourse([FromBody] Course course)
        {
            _dbcontext.courses.Add(course);
            _dbcontext.SaveChanges();
            return CreatedAtAction(nameof(getcoursebyId), new { id = course.courseId }, course);
        }
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public IActionResult update(int id,Course course)
        {
            var cours = _dbcontext.courses.FirstOrDefault(c => c.courseId == id);
            if (cours == null)
            {
                return NotFound();
            }
            cours.courseDescription = course.courseDescription;
            cours.courseName = course.courseName;
            cours.coursePrice = course.coursePrice;
            cours.teacherName = course.teacherName;
            _dbcontext.SaveChanges();

            return Ok(cours);

        }
        [Authorize(Roles ="Admin")]
        [HttpDelete("{id}")]
        public IActionResult delete(int id)
        {
            var course = _dbcontext.courses.FirstOrDefault(c => c.courseId == id);
            if (course == null)
            {
                return NotFound();
            }
            course.IsDeleted = true;
            _dbcontext.SaveChanges();
            return Ok("course deleted successfully");
        }
    }
}
