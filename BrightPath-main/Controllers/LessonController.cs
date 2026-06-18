using BrightPath.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BrightPath.Controllers
{
    public class LessonController : Controller
    {
        private readonly dbContext _dbcontext;
        public LessonController(dbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult AddLesson(int courseid)
        {

            return View(new Lessons
            {
                courseId = courseid
            });
        }
        [Authorize(Roles="Admin")]
        [HttpPost]
        public IActionResult AddLesson(Lessons lessons)
        {
            if (!ModelState.IsValid)
            {

                return View(lessons);
            }
            _dbcontext.lessons.Add(lessons);
            _dbcontext.SaveChanges();
            return RedirectToAction("CourseDetails", "Course", new {id=lessons.courseId});
        }
        [HttpGet]
        [Authorize(Roles ="Admin")]
        public IActionResult EditLesson(int lessonId,int courseId)
        {
            var lesson = _dbcontext.lessons.FirstOrDefault(l => l.lessonId == lessonId&&(courseId==l.courseId));
            if (lesson == null)
            {
                TempData["editerror"] = "Not found";
                return RedirectToAction("Dashboard", "Admin");
            }
            return View(lesson);
        }
        [HttpPost]
        [Authorize(Roles ="Admin")]
        [ValidateAntiForgeryToken]

        public async Task< IActionResult >EditLesson(Lessons lessons)
        {
            if (!ModelState.IsValid)
            {
                return View(lessons);
            }
            var lesson = _dbcontext.lessons.FirstOrDefault(l => l.lessonId == lessons.lessonId && (l.courseId == lessons.courseId));
            lesson.lessonName = lessons.lessonName;
            lesson.lessonVideoUrl = lessons.lessonVideoUrl;
           await  _dbcontext.SaveChangesAsync();

            return RedirectToAction("CourseDetails", "Course", new {id=lesson.courseId});
        }
        [Authorize(Roles ="Admin")]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task< IActionResult> DeleteLesson(int lId,int cId)
        {
            var lesson = _dbcontext.lessons.FirstOrDefault(l => l.lessonId == lId && (l.courseId == cId));
            if (lesson == null)
            {
                return Content("not found");
            }

            lesson.isDeleted = true;
            await _dbcontext.SaveChangesAsync();
            return RedirectToAction("CourseDetails", "Course",new {id=lesson.courseId});
        }

    }
}
