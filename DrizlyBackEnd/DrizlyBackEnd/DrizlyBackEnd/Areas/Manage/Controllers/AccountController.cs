using DrizlyBackEnd.Areas.Manage.ViewModels;
using DrizlyBackEnd.Helpers;
using DrizlyBackEnd.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DrizlyBackEnd.Areas.Manage.Controllers
{
    [Area("manage")]
    public class AccountController : Controller
    {
        private readonly DrizlyContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IWebHostEnvironment _env;

        public AccountController(DrizlyContext context, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IWebHostEnvironment env, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _env = env;
        }
        //LOGIN ADMIN ACTION
        public IActionResult Login()
        {
            return View();
        }
        //LOGIN ADMIN POST ACTION
        [HttpPost]
        public async Task<IActionResult> Login(AdminLoginViewModel loginVM)
        {
            if (!ModelState.IsValid)
                return View();

            AppUser admin = await _userManager.Users.FirstOrDefaultAsync(x => x.UserName == loginVM.UserName && x.IsAdmin);

            if (admin == null)
            {
                ModelState.AddModelError("", "username or password is incorrect");
                return View();
            }

            var result = await _signInManager.PasswordSignInAsync(admin, loginVM.Password, loginVM.IsPersistent, false);

            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "username or password is incorrect");
                return View();
            }

            return RedirectToAction("index", "dashboard");
        }

        //LOGOUT ACTION
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction("login", "account");
        }

        //PROFILE ACTION
        [Authorize(Roles = "SuperAdmin,Creator,Editor,Reader")]
        public async Task<IActionResult> Profile()
        {
            AppUser admin = await _userManager.Users.Where(x => x.UserName == User.Identity.Name && x.IsAdmin == true).FirstOrDefaultAsync();

            var adminrole = await _roleManager.FindByIdAsync(admin.Id);

            if (admin == null)
            {
                return NotFound();
            }



            AdminProfileViewModel profileVM = new AdminProfileViewModel
            {   
                Admin = new AdminUpdateViewModel
                {
                    UserName = admin.UserName,
                    FullName = admin.FullName,
                    Email = admin.Email,
                    Phone = admin.PhoneNumber,
                    Address = admin.Address,
                    City = admin.City,
                    Country = admin.Country,
                    Age = admin.Age,
                    Image = admin.Image,
                    BackgroundImage = admin.BackgroundImage,
                    FacebookUrl = admin.FacebookUrl,
                    InstagramUrl = admin.InstagramUrl,
                    TwitterUrl = admin.TwitterUrl,
                    Role = (await _userManager.GetRolesAsync(admin))[0]
                }
            };
            return View(profileVM);
        }

        //EDIT ACTION ADMIN ACCOUNT ITSELF
        //EDIT ACTION
        [Authorize(Roles = "SuperAdmin,Creator,Editor,Reader")]
        [HttpPost]
        public async Task<IActionResult> Edit(AdminUpdateViewModel adminVM)
        {
            if (adminVM == null)
            {
                return NotFound();
            }

            AppUser admin = await _userManager.FindByNameAsync(User.Identity.Name);
            AdminProfileViewModel profileVM = new AdminProfileViewModel
            {
                Admin = adminVM
            };

            if (!ModelState.IsValid)
                return View("Profile", profileVM);


            if (admin.UserName != adminVM.UserName && _userManager.Users.Any(x => x.NormalizedUserName == adminVM.UserName.ToUpper()))
            {
                ModelState.AddModelError("UserName", "username is already exists ");
                return View("Profile", profileVM);
            }

            if (admin.Email != adminVM.Email && _userManager.Users.Any(x => x.NormalizedEmail == adminVM.Email.ToUpper()))
            {
                ModelState.AddModelError("Email", "email is already exists");
                return View("Profile", profileVM);
            }

            admin.UserName = adminVM.UserName;
            admin.FullName = adminVM.FullName;
            admin.Email = adminVM.Email;
            admin.Country = adminVM.Country;
            admin.City = adminVM.City;
            admin.Address = adminVM.Address;
            admin.PhoneNumber = adminVM.Phone;
            admin.FacebookUrl = adminVM.FacebookUrl;
            admin.TwitterUrl = adminVM.TwitterUrl;
            admin.InstagramUrl = adminVM.InstagramUrl;

            //IMAGE
            if (adminVM.ImageFile != null)
            {
                if (adminVM.ImageFile.ContentType != "image/jpeg" && adminVM.ImageFile.ContentType != "image/png")
                {
                    ModelState.AddModelError("ImageFile", "file type must be image/jpeg or image/png");
                    return View("Profile", profileVM);
                }

                if (adminVM.ImageFile.Length > 4194304)
                {
                    ModelState.AddModelError("ImageFile", "file size must be less than 4mb");
                    return View("Profile", profileVM);
                }

                admin.Image = FileManager.Save(_env.WebRootPath, "uploads/admins", adminVM.ImageFile);
            }
            else
            {
                if (adminVM.Image == null)
                {
                    if (admin.Image != null)
                    {
                        FileManager.Delete(_env.WebRootPath, "uploads/admins", admin.Image);
                    }
                    admin.Image = null;
                }
            }
            //End Image
            //Background Image
            if (adminVM.BackGroundFile != null)
            {
                if (adminVM.BackGroundFile.ContentType != "image/jpeg" && adminVM.BackGroundFile.ContentType != "image/png")
                {
                    ModelState.AddModelError("BackGroundFile", "file type must be image/jpeg or image/png");
                    return View("Profile", profileVM);
                }

                if (adminVM.BackGroundFile.Length > 4194304)
                {
                    ModelState.AddModelError("BackGroundFile", "file size must be less than 4mb");
                    return View("Profile", profileVM);
                }

                admin.BackgroundImage = FileManager.Save(_env.WebRootPath, "uploads/adminbackgrounds", adminVM.BackGroundFile);
            }
            else
            {
                if (adminVM.BackgroundImage == null)
                {
                    if (admin.BackgroundImage != null)
                    {
                        FileManager.Delete(_env.WebRootPath, "uploads/adminbackgrounds", admin.BackgroundImage);
                    }
                    admin.BackgroundImage = null;
                }
            }
            //End Image

            var result = await _userManager.UpdateAsync(admin);

            if (!result.Succeeded)
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
                return View("Profile", profileVM);
            }

            if (adminVM.Password != null)
            {
                if (string.IsNullOrWhiteSpace(adminVM.CurrentPassword))
                {
                    ModelState.AddModelError("CurrentPassword", "Current Password is required!");
                    return View("Profile", profileVM);
                }
                if (string.IsNullOrWhiteSpace(adminVM.Password))
                {
                    ModelState.AddModelError("Password", "New Password is required!");
                    return View("Profile", profileVM);
                }

                if (!await _userManager.CheckPasswordAsync(admin, adminVM.CurrentPassword))
                {
                    ModelState.AddModelError("CurrentPassword", "Current Password is incorrect!");
                    return View("Profile", profileVM);
                }

                if (adminVM.CurrentPassword == adminVM.Password)
                {
                    ModelState.AddModelError("Password", "New Password is same as the current password");
                    return View("Profile", profileVM);
                }

                var passwordResult = _userManager.ChangePasswordAsync(admin, adminVM.CurrentPassword, adminVM.Password);

                if (!passwordResult.Result.Succeeded)
                {
                    foreach (var item in passwordResult.Result.Errors)
                    {
                        ModelState.AddModelError("Password", item.Description);
                    }
                    return View("Profile", profileVM);
                }

            }

            await _signInManager.SignInAsync(admin, false);
            TempData["Success"] = "Profile updated successfully";
            return RedirectToAction("profile");
        }
    }
}
