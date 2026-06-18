using BrightPath.Models;
using BrightPath.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
namespace BrightPath.Controllers
{
    public class StudentController : Controller
    {
        private readonly dbContext _dbContext;
        private readonly UserManager<applicationuser> _userManager;
        private readonly SignInManager<applicationuser> _signInManager;
        public  StudentController(dbContext dbContext, UserManager<applicationuser> userManager, SignInManager<applicationuser> signInManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
            _signInManager = signInManager;
        }


        [HttpPost]
        public IActionResult Search(string search)
        {
            var courses = _dbContext.courses.ToList();

            if (search == null)
            {
                return View("~/Views/Home/Index.cshtml",courses);
            }
            List<Course> courselist = new List<Course>();
            foreach(var course in courses)
            {
                if ((!course.IsDeleted)&&course.courseName.Contains(search) || (!course.IsDeleted)&&course.courseDescription.Contains(search))
                {
                    courselist.Add(course);
                }
            }
            return View ("~/Views/Home/Index.cshtml",courselist);
        }

        [HttpGet]
        public async Task< IActionResult> CourseOfGrade()
        {
            var user = await _userManager.GetUserAsync(User);
            var courses = _dbContext.courses.Where(c => c.gradeId == user.GradeId&&!c.IsDeleted).ToList();
            return View(courses);
        }
        
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
       
        [HttpPost]
        public async Task< IActionResult>Login(LoginViewModels model)
        {
          
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result= await _signInManager.PasswordSignInAsync(model.email, model.password, false, false);
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Invalid email or password");
                return View(model);
            }
            
                var user = await _userManager.FindByEmailAsync(model.email);
                if (await _userManager.IsInRoleAsync(user,"Admin"))
                {
                    return RedirectToAction("Dashboard", "Admin");
                }
                return RedirectToAction("CourseOfGrade", "Student");
               
            
            ModelState.AddModelError("", "invalid login attempt");
            return View(model);
        }
        [HttpGet]
        public IActionResult Register()
        {
            ViewBag.grades = _dbContext.grade.Select(g => new SelectListItem
            {
                Value = g.GradeId.ToString(),
                Text = g.GradeName
            });
            return View();
        }
        [HttpPost]
        public async Task <IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);

            }
            var user = new applicationuser
            {
                UserName = model.email,
                Email = model.email,
                name = model.name,
                GradeId=model.gradeid

            };
            var result = await _userManager.CreateAsync(user, model.password);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "Student");
                await _signInManager.SignInAsync(user, false);

                return RedirectToAction("Login", "Student");
           }
            foreach(var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);

            }
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task <IActionResult> Logout(){
          await  _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");

        }
        [HttpPost]
        [Authorize(Roles ="Student")]
        public async Task<IActionResult> Enroll(int courseId)
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                return Content("User is null");
            }

            var userId = _userManager.GetUserId(User);
            
            var exist = _dbContext.studentCourses.FirstOrDefault(sc => sc.studentId == userId && (sc.courseId == courseId));
            if (exist != null)
            {
                TempData["errorenroll"]= "You are already enrolled in this course";
                return RedirectToAction("CourseDetails", "Course", new { id = courseId });
            }

            var enrollment = new studentCourse
            {
                studentId = userId,
                courseId = courseId
            };
           await _dbContext.studentCourses.AddAsync(enrollment);
            await _dbContext.SaveChangesAsync();
            return RedirectToAction("Mycourses", "Student");
        }
        [HttpGet]
        [Authorize(Roles ="Student")]
        public IActionResult Mycourses()
        {
            var userid = _userManager.GetUserId(User);
            var courses = _dbContext.studentCourses
                    .Where(sc => sc.studentId.ToString() == userid)
                    .Include(sc => sc.course)
                    .Select(sc => sc.course)
                    .ToList();
            if (courses == null)
            {
                TempData["Cerror"] = "you donnot enroll in any course";
                return View();
            }
            return View(courses);

        }
        [HttpGet]
        [Authorize(Roles = "Student")]
        public async Task<IActionResult> StudentDashboard()
        {
            var user = await _userManager.GetUserAsync(User);
            var userId = user.Id;
            var enrollmentCourses = _dbContext.studentCourses.Where(s => s.studentId == userId).Include(s => s.course).ThenInclude(s => s.lessons).ToList();
            var lessonCount = enrollmentCourses.SelectMany(c => c.course.lessons).Count(l => !l.isDeleted);


            var model = new StudentDashboardViewModel()
            {
                StudentName = user.name,
                CourseCount = enrollmentCourses.Count(),
                LessonCount = lessonCount,
                MyCourses = enrollmentCourses.Select(c =>
                {
                    int totalLessons = c.course.lessons.Count(l => !l.isDeleted);

                    int completedLessons = _dbContext.lessonProgresses.Count(lp =>
                        lp.studentId == userId &&
                        lp.courseId == c.course.courseId &&
                        lp.isCopmleted);

                    return new CourseProgressViewModel
                    {
                        CourseId = c.course.courseId,
                        CourseName = c.course.courseName,
                        TeacherName = c.course.teacherName,
                        Progress = totalLessons == 0
                            ? 0
                            : (completedLessons * 100) / totalLessons
                    };
                }).ToList()

            };
            var courseIds = enrollmentCourses.Select(c => c.course.courseId).ToList();
            var lessonCompleted = _dbContext.lessonProgresses.Count(lp => lp.studentId == user.Id && courseIds.Contains(lp.lesson.courseId) && lp.isCopmleted);

            int progress = lessonCount == 0
                ? 0
                : (lessonCompleted * 100) / lessonCount;

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> CompleteLesson(int lessonId)
        {
            var userId = _userManager.GetUserId(User);

            var lesson = _dbContext.lessons
                .FirstOrDefault(l => l.lessonId == lessonId);

            if (lesson == null)
            {
                return NotFound();
            }

            var progress = _dbContext.lessonProgresses
                .FirstOrDefault(lp => lp.studentId == userId &&
                                      lp.lessonId == lessonId);

            if (progress == null)
            {
                progress = new LessonProgress
                {
                    studentId = userId,
                    lessonId = lessonId,
                    courseId = lesson.courseId, // مهم جدًا
                    isCopmleted = true
                };

                _dbContext.lessonProgresses.Add(progress);
            }
            else
            {
                progress.isCopmleted = true;
            }

            await _dbContext.SaveChangesAsync();

            return RedirectToAction(
                "CourseDetails",
                "Course",
                new { id = lesson.courseId });
        }
    }
}
