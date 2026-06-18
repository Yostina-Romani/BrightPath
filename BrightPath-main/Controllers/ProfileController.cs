using BrightPath.Models;
using BrightPath.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BrightPath.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        private readonly UserManager<applicationuser> _userManager;
        private readonly SignInManager<applicationuser> _signInManager;
        private readonly dbContext _dbcontext;
        public ProfileController(dbContext dbContext, SignInManager<applicationuser> signInManager,UserManager<applicationuser>userManager)
        {
            _dbcontext = dbContext;
            _signInManager = signInManager;
            _userManager = userManager;
        }
        [HttpGet]
        public async Task< IActionResult> MyProfile()
        {

            var user =  await _userManager.GetUserAsync(User);
            var role = await _userManager.GetRolesAsync(user);
            var model = new ProfileViewModel
            {
                Name = user.name,
                Email = user.Email,
                Roles = role,
                imageurl=user.imageUrl
            };
            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> EditProfile()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToAction("Login", "Student");
            }
            var model = new ViewModelsGroup
            {
                profile=new EditProfileViewModel
                {
                    name = user.name,
                    

                },
                password=new EditPasswordViewModel()



            };

            return View(model.profile);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult>EditProfile(EditProfileViewModel model,IFormFile image)
        {
            
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToAction("Login", "Student");

            }
            if (image != null)
            {
                var filename = Guid.NewGuid() + Path.GetExtension(image.FileName);
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images", filename);
                using (var stream=new FileStream(path,FileMode.Create))
                {
                    await image.CopyToAsync(stream);
                };
                user.imageUrl = "/Images/" + filename;
            }
           
            user.name = model.name;

          
            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                return RedirectToAction("MyProfile", "Profile");
            }
            foreach(var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
            return View(model);
        }
       
    }
}
