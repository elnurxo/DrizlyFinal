using DrizlyBackEnd.Areas.Manage.ViewModels;
using DrizlyBackEnd.Enums;
using DrizlyBackEnd.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace DrizlyBackEnd.Areas.Manage.Controllers
{
    [Area("manage")]
    public class AdminController : Controller
    {
        private readonly DrizlyContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IWebHostEnvironment _env;

        public AdminController(DrizlyContext context, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IWebHostEnvironment env, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _env = env;
        }
        //VIEW ALL THE OTHER ADMINS ACTION (except superadmin and users)
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Index()
        {
            List<AppUser> Admins = _userManager.Users.Where(x => x.IsAdmin == true && x.UserName != User.Identity.Name).ToList();

            return View(Admins);
        }
        [Authorize(Roles = "SuperAdmin")]
        //CREATE ADMIN ACTION
        public IActionResult Create()
        {
            return View();
        }
        //CREATE ADMIN POST ACTION
        [Authorize(Roles = "SuperAdmin")]
        [HttpPost]
        public async Task<IActionResult> Create(AdminCreateViewModel admincreateVM)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            AppUser admin = await _userManager.FindByNameAsync(admincreateVM.UserName);
            if (admin != null)
            {
                ModelState.AddModelError("UserName", "username's already taken!");
                return View();
            }

            if (admincreateVM.Age < 21)
            {
                ModelState.AddModelError("Age", "Age must be at least 21");
                return View();
            }
            if (admincreateVM.UserName.Length < 3)
            {
                ModelState.AddModelError("UserName", "username must be at least 3 symbols!");
                return View();
            }

            admin = new AppUser
            {
                FullName = admincreateVM.FullName,
                UserName = admincreateVM.UserName,
                Email = admincreateVM.Email,
                Age = admincreateVM.Age,
                IsAdmin = true,
                RoleName = admincreateVM.Role
            };

            var result = await _userManager.CreateAsync(admin, admincreateVM.Password);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View();
            }

            var roleResult = await _userManager.AddToRoleAsync(admin, admincreateVM.Role);

            if (!roleResult.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View();
            }
            //VERIFY EMAIL
            string token = await _userManager.GenerateEmailConfirmationTokenAsync(admin);
            string link = Url.Action(nameof(VerfiyEmail), "Admin", new { email = admin.Email, token }, Request.Scheme, Request.Host.ToString());
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress("drizlycode@gmail.com", "Drizly");
            mail.To.Add(new MailAddress(admin.Email));
            mail.Subject = "VerifyEmail";

            string body = string.Empty;
            using (StreamReader stream = new StreamReader("wwwroot/Template/Verify.html"))
            {
                body = stream.ReadToEnd();
            }

            string Info = $"welcome {admin.FullName}";
            body = body.Replace("{{link}}", link);
            mail.Body = body.Replace("{{info}}", Info);
            mail.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;
            smtp.EnableSsl = true;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential("drizlycode@gmail.com", "Drizly21");
            smtp.Send(mail);



            TempData["Warning"] = "Verify Your Email";

            //END VERIFY EMAIL

            return RedirectToAction("index", "dashboard");
        }
        //VERIFY EMAIL
        public async Task<IActionResult> VerfiyEmail(string email, string token)
        {
            AppUser user = await _userManager.FindByEmailAsync(email);
            if (user == null) return BadRequest();
            await _userManager.ConfirmEmailAsync(user, token);
            TempData["Success"] = "Your Email Successfully Verified";
            await _signInManager.SignInAsync(user, true);
            return RedirectToAction("Index", "Dashboard");
        }

        //DELETE ACTION
        [Authorize(Roles = "SuperAdmin")]
        public IActionResult Delete(string id)
        {
            AppUser existAdmin = _context.AppUsers.FirstOrDefault(x => x.Id == id);

            if (existAdmin == null)
                return NotFound();

            _context.AppUsers.Remove(existAdmin);

            if (existAdmin.Image!=null)
            {
                string existPath = Path.Combine(_env.WebRootPath, "uploads/admins", existAdmin.Image);
                if (System.IO.File.Exists(existPath))
                    System.IO.File.Delete(existPath);
            }
            if (existAdmin.BackgroundImage!=null)
            {
                string existPath = Path.Combine(_env.WebRootPath, "uploads/adminbackgrounds", existAdmin.Image);
                if (System.IO.File.Exists(existPath))
                    System.IO.File.Delete(existPath);
            }


            _context.SaveChanges();
            TempData["Success"] = "Admin Deleted Successfully!";
            return RedirectToAction("index");
        }

        //EDIT ADMIN ACTION GET
        [Authorize(Roles = "SuperAdmin")]
        public IActionResult Edit(string id)
        {
            var existAdmin = _context.AppUsers.FirstOrDefault(x => x.Id == id);

            if (existAdmin == null) return NotFound();

            return View(existAdmin);
        }
        //EDIT ADMIN ACTION POST
        [Authorize(Roles = "SuperAdmin")]
        [HttpPost]
        public async Task<IActionResult> Edit(AppUser admin)
        {
            var existAdmin = _context.AppUsers.FirstOrDefault(x => x.Id == admin.Id);

            await _userManager.RemoveFromRoleAsync(existAdmin, existAdmin.RoleName);

            await _userManager.AddToRoleAsync(existAdmin, admin.RoleName);


            existAdmin.RoleName = admin.RoleName;
            TempData["Success"] = "Admin role edited successfully!";
            await _context.SaveChangesAsync();
            TempData["Success"] = "Admin role updated successfully!";
            return RedirectToAction("Index");
        }
        //DETAIL ADMIN ACTION
        [Authorize(Roles = "SuperAdmin")]
        public IActionResult Detail(string id)
        {
            var existAdmin = _context.AppUsers.FirstOrDefault(x => x.Id == id);

            if (existAdmin == null) return NotFound();

            return View(existAdmin);
        }
    }
}
