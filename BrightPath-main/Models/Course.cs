using System.ComponentModel.DataAnnotations;

namespace BrightPath.Models
{
    public class Course
    {
        [Key]
        public int courseId { get; set; }
        [Required]
        public string courseName { get; set; }
        [Required]
        public decimal coursePrice { get; set; }
        [Required]
        public string courseDescription { get; set; }

        public List<Lessons>? lessons { get; set; }

        public bool IsDeleted { get; set; } = false;
        public int gradeId { get; set; }
        public Grade ?grade { get; set; }
        public string teacherName { get; set; }
        public string ?courseImage { get; set; }
        public int Progress { get; set; }

        public List<LessonProgress> ?lessonProgresses { get; set; }
    }
}
