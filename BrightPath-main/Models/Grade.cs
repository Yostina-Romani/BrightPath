using System.ComponentModel.DataAnnotations;

namespace BrightPath.Models
{
    public class Grade
    {
        [Required]
        [Key]
        public int GradeId { get; set; }
        public string GradeName { get; set; }
        public List<Course> ?course { get; set; }
    }
}
