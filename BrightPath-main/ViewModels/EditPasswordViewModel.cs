using System.ComponentModel.DataAnnotations;

namespace BrightPath.ViewModels
{
    public class EditPasswordViewModel
    {
        [Required]
        public string currentpassword { get; set; }
        [Required]
        public string NewPassword { get; set; }
       
    }
}
