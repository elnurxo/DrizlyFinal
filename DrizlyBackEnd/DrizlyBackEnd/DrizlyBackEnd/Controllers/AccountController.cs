using DrizlyBackEnd.Helpers;
using DrizlyBackEnd.Models;
using DrizlyBackEnd.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DrizlyBackEnd.Controllers
{
    public class AccountController : Controller
    {
        private readonly DrizlyContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IWebHostEnvironment _env;

        public AccountController(DrizlyContext context, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IWebHostEnvironment env)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _env = env;
        }
        //REGISTER ACTION
        public IActionResult Register()
        {
            return View();
        }


        //REGISTER POST ACTION
        [HttpPost]
        public async Task<IActionResult> Register(MemberRegisterViewModel registerVM)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            AppUser member = await _userManager.FindByNameAsync(registerVM.UserName);
            if (member != null)
            {
                ModelState.AddModelError("UserName", "username's already taken!");
                return View();
            }

            if (registerVM.Age < 21)
            {
                ModelState.AddModelError("Age", "Age must be at least 21");
                return View();
            }

            member = new AppUser
            {
                FullName = registerVM.FullName,
                UserName = registerVM.UserName,
                Email = registerVM.Email,
                Age = registerVM.Age,
                IsAdmin = false
            };

            //IMAGE
            if (registerVM.ImageFile!=null)
            {
                if (registerVM.ImageFile.ContentType != "image/jpeg" && registerVM.ImageFile.ContentType != "image/png")
                {
                    ModelState.AddModelError("ImageFile", "file type must be image/jpeg or image/png");
                    return View();
                }

                if (registerVM.ImageFile.Length > 4194304)
                {
                    ModelState.AddModelError("ImageFile", "file size must be less than 4mb");
                    return View();
                }

                member.Image = FileManager.Save(_env.WebRootPath, "uploads/users", registerVM.ImageFile);
            }

            var result = await _userManager.CreateAsync(member, registerVM.Password);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View();
            }

            var roleResult = await _userManager.AddToRoleAsync(member, "Member");

            if (!roleResult.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View();
            }

            await _signInManager.SignInAsync(member, true);


            return RedirectToAction("index", "home");
        }

        //LOGOUT ACTION
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction("index", "home");
        }
    }
}
