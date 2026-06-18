using BrightPath.Models;

namespace BrightPath.ViewModels
{
    public class StudentDashboardViewModel
    {

        public string StudentName { get; set; }

        public int CourseCount { get; set; }

        public int LessonCount { get; set; }
        public List<CourseProgressViewModel> MyCourses { get; set; }
    }
}
