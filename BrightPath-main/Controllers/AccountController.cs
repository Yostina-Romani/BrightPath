using BrightPath.Models;
using BrightPath.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Configuration;

namespace BrightPath.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly UserManager<applicationuser> _usermanager;
        private readonly SignInManager<applicationuser> _signInManager;
        public AccountController(UserManager<applicationuser>userManager,SignInManager<applicationuser>signInManager)
        {
            _signInManager = signInManager;
           _usermanager=userManager;

        }
        [HttpGet]
        public async Task<IActionResult> ChangePassword()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task< IActionResult> ChangePassword(EditPasswordViewModel model)
        {

            if (!ModelState.IsValid)
            {
                TempData["error"] = string.Join(" | ",
                    ModelState.Values.SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage));
                return View(model);
            }

            var user =await _usermanager.GetUserAsync(User);
            if (user == null)
                return RedirectToAction("Login", "Student");
            var result = await _usermanager.ChangePasswordAsync(user,model.currentpassword, model.NewPassword);
            if (result.Succeeded) {
                await _signInManager.RefreshSignInAsync(user);
                TempData["success"] = "Password updated successfully";
                return RedirectToAction("MyProfile", "Profile");
            }
            else
            {
                TempData["error"] = string.Join(" | ",
                    result.Errors.Select(e => e.Description));
            }
            return View(model);
        }
    }
}
