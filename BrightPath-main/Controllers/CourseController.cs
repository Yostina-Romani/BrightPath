using BrightPath.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Specialized;
using System.Threading.Tasks;
namespace BrightPath.Controllers
{
    public class CourseController : Controller
    {
        private readonly dbContext _dbcontext;
        public CourseController(dbContext dbContext)
        {
            _dbcontext = dbContext;

        }


        [Authorize (Roles ="Admin")]
        
        [HttpGet]
        public IActionResult AddCourse()
        {
            ViewBag.grades = _dbcontext.grade.Select(g => new SelectListItem
            {
                Value = g.GradeId.ToString(),
                Text = g.GradeName

            }).ToList();
            return View();
        }
        [HttpPost]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> AddCourse(Course course,IFormFile courseImage)
        {

            if (!ModelState.IsValid)
            {
              
                ViewBag.grades = _dbcontext.grade.Select(g => new SelectListItem
                {
                    Value = g.GradeId.ToString(),
                    Text = g.GradeName

                }).ToList();
                Console.WriteLine("GradeId = " + course.gradeId);
                return View(course);
            }
            if (courseImage != null)
            {
                var filename = Guid.NewGuid() + Path.GetExtension(courseImage.FileName);
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images", filename);
                using(var stream=new FileStream(path, FileMode.Create))
                {
                    await courseImage.CopyToAsync(stream);
                };
                course.courseImage = "/Images/" + filename;
            }
            var gradeExists = _dbcontext.grade.Any(g => g.GradeId == course.gradeId);

            if (!gradeExists)
            {
                ModelState.AddModelError("gradeId", "Please select a valid grade");
                var errors = ModelState.Values
     .SelectMany(v => v.Errors)
     .Select(e => e.ErrorMessage);

                return Content(string.Join(" | ", errors));
            }
            _dbcontext.courses.Add(course);
            _dbcontext.SaveChanges();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult CourseDetails(int id)
        {
            var courses = _dbcontext.courses.Include(c=>c.lessons).Where(c=>!c.IsDeleted).FirstOrDefault(c => c.courseId == id);
            if (courses == null)
            {
                return Content($"Course {id} not found");
            }

            return View(courses);
        }

        [HttpPost]
        [Authorize(Roles ="Admin")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteCourse(int id)
        {
            var course = _dbcontext.courses.FirstOrDefault(c => c.courseId == id);
            course.IsDeleted = true;
            _dbcontext.SaveChanges();
            return RedirectToAction("Dashboard", "Admin");
        }

        [HttpGet]
        [Authorize(Roles ="Admin")]
        public IActionResult EditCourse(int id)
        {
            var course = _dbcontext.courses.FirstOrDefault(c => c.courseId == id);
            if (course == null)
            {
                TempData["editerror"] = "not found";
                return RedirectToAction("Dashboard", "Admin");
            }
            return View(course);
        }
        [HttpPost]
        public  async Task<IActionResult> EditCourse(Course course)
        {
            if (!ModelState.IsValid)
            {
                return View(course);
            }
            var cor = _dbcontext.courses.FirstOrDefault(c=>c.courseId==course.courseId);
            if (cor == null)
            {
                return NotFound(); 
            }

          
            cor.courseName = course.courseName;
            cor.coursePrice = course.coursePrice;
            cor.courseDescription = course.courseDescription;
            cor.teacherName = course.teacherName;


           await _dbcontext.SaveChangesAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
