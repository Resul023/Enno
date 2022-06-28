using Enno.Areas.Manage.ViewModel;
using Enno.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Enno.Areas.Manage.Controllers
{
    [Area("manage")]
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountController(UserManager<AppUser>userManager,SignInManager<AppUser> signInManager,RoleManager<IdentityRole> roleManager)
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
            this._roleManager = roleManager;
        }
        public async Task<IActionResult> Index()
        {
            AppUser admin = new AppUser
            {
                FullName = "Hikmet Abbasov",
                UserName = "Hikmet123",
                Email = "Hikmet@gmail.com",
                IsAdmin = true,
            };
            var result = await _userManager.CreateAsync(admin, "Alfa123");
            if (!result.Succeeded)
            {
                return Ok(result.Errors);
            }
            await _userManager.AddToRoleAsync(admin,"Admin");
            return View();
        }
        public async Task<IActionResult> CreateRole()
        {
            await _roleManager.CreateAsync(new IdentityRole ("Admin"));
            return Ok();
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(AdminLoginViewModel admin)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            AppUser user = await _userManager.FindByNameAsync(admin.UserName);
            if (user == null)
            {
                ModelState.AddModelError("","Username or password is not true");
                return View();
            }
            var result = await _signInManager.PasswordSignInAsync(user,admin.Password,false,false);
            if (!result.Succeeded)
            {
                ModelState.AddModelError("","Username or password is not true");
                return View();
            }
            
            return RedirectToAction("index","home");
        }
        public async Task<IActionResult> SignOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("index", "home");
        }
    }
}
