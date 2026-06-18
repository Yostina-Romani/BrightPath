namespace BrightPath.Models
{
    public class studentCourse
    {
        public int id {  get; set; }
        public int courseId { get; set; }
        public Course ?course { get; set; }
        public string studentId { get; set; }
        public Student ?student { get; set; }
    }
}
