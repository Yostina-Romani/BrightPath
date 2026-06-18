using BrightPath.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace BrightPath.Controllers
{
    public class AdminController : Controller
    { private readonly dbContext _dbcontext;
        public AdminController(dbContext dbContext) {
            _dbcontext = dbContext;

        }
        [Authorize(Roles ="Admin")]
        public IActionResult Dashboard()
        {
            int courseCount = _dbcontext.courses.Count();
            int lessonsCount = _dbcontext.lessons.Count();
            ViewBag.course = courseCount;
            ViewBag.lesson = lessonsCount;
            return View();
        }
        [HttpGet]
        [Authorize(Roles ="Admin")]
        public IActionResult ManageCourses()
        {
            var courses = _dbcontext.courses.Include(c => c.lessons).Where(c => !c.IsDeleted).ToList();
            return View(courses);
        }
    }
}
