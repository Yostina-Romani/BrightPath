

using System.ComponentModel.DataAnnotations;

namespace BrightPath.Models
{
    public class Student
    {
        [Key]
        public int studentId { get; set; }

        public string name { get; set; }


        public string email { get; set; }

        public string password { get; set; }

        public DateTime enrollmentAt { get; set; }


    }
}

