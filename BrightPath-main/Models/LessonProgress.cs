namespace BrightPath.Models
{
    public class LessonProgress
    {
        public int id { get; set; }
        public string studentId { get; set; }
        public applicationuser student {  get; set; }
        public bool isCopmleted { get; set; }
        public int courseId {  get; set; }
        public Course? course { get; set; }
        public int lessonId { get; set; }
        public Lessons ?lesson { get; set; }
    }
}
