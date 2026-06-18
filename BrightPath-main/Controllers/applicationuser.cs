using Microsoft.AspNetCore.Identity;

namespace BrightPath.Models
{
    public class applicationuser : IdentityUser
    {
        public string name { get; set; }
        public string? imageUrl { get; set; }
        public int? GradeId { get; set; }
        public Grade? grade { get; set; }

    }
}