using System.ComponentModel.DataAnnotations;

namespace BrightPath.Models
{
    public class Lessons
    {
        [Key]
      public  int lessonId { get; set; }
        [Required]
        public string lessonName { get; set; }
        [Required]
        public string lessonVideoUrl { get; set; }
        public int courseId { get; set; }
        public Course ?course { get; set; }
        [Required]
        public bool isDeleted { get; set; } = false;
    }
}
