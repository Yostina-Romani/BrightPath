using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using BrightPath.Models;
using Microsoft.AspNetCore.Mvc;

namespace BrightPath.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly dbContext _dbcontext;

        public HomeController(ILogger<HomeController> logger, dbContext dbcontext)
        {
            _logger = logger;
            _dbcontext = dbcontext;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var courses = _dbcontext.courses.Include(c=>c.lessons).Where(c=>!c.IsDeleted).ToList();
            return View(courses);
        }

     

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
