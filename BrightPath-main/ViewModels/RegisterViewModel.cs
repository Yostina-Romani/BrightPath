using BrightPath.Models;
using System.ComponentModel.DataAnnotations;


namespace BrightPath.ViewModels
{
    public class RegisterViewModel
    {
        [Required]

        public string name { get; set; }
        [Required]
        [EmailAddress]
        public string email { get; set; }
        [Required]
        public string password { get; set; }
        [Required, Compare("password")]

        public string confirmpass { get; set; }
        [Required]
        public string age { get; set; }
        public int gradeid { get; set; }
        public Grade? grade { get; set; }

    }
}
