using DrizlyBackEnd.Helpers;
using DrizlyBackEnd.Models;
using DrizlyBackEnd.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace DrizlyBackEnd.Controllers
{
    public class AccountController : Controller
    {
        private readonly DrizlyContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IWebHostEnvironment _env;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountController(DrizlyContext context, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IWebHostEnvironment env, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _env = env;
            _roleManager = roleManager;
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
            if (registerVM.UserName.Length<3)
            {
                ModelState.AddModelError("UserName", "username must be at least 3 symbols!");
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
            //VERIFY EMAIL
            string token = await _userManager.GenerateEmailConfirmationTokenAsync(member);
            string link = Url.Action(nameof(VerfiyEmail), "Account", new { email = member.Email, token }, Request.Scheme, Request.Host.ToString());
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress("drizlycode@gmail.com", "Drizly");
            mail.To.Add(new MailAddress(member.Email));
            mail.Subject = "VerifyEmail";

            string body = string.Empty;
            using (StreamReader stream = new StreamReader("wwwroot/Template/Verify.html"))
            {
                body = stream.ReadToEnd();
            }

            string Info = $"welcome {member.FullName}";
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

            return RedirectToAction("index", "home");
        }
        //VERIFY EMAIL
        public async Task<IActionResult> VerfiyEmail(string email, string token)
        {
            AppUser user = await _userManager.FindByEmailAsync(email);
            if (user == null) return BadRequest();
            await _userManager.ConfirmEmailAsync(user, token);
            TempData["Success"] = "Your Email Successfully Verified";
            await _signInManager.SignInAsync(user, true);
            return RedirectToAction("Index", "Home");
        }

        //TEST
    //    public async Task<IActionResult> test()
    //    {
    //        AppUser appUser = new AppUser
    //        {
    //            FullName = "Polad Mammadli",
    //            UserName = "PoladAdmin",
    //            Email = "polad123@mail.ru"
    //        };

    //    var result = await _userManager.CreateAsync(appUser, "Polad123");

    //    AppUser admin = await _userManager.FindByNameAsync("PoladAdmin");

    //    var result1 = await _userManager.AddToRoleAsync(admin, "Reader");

    //        return Ok(result.Errors);
    //}
    //public async Task<IActionResult> Test()
    //{
    //    var result1 = await _roleManager.CreateAsync(new IdentityRole("Member"));
    //    var result2 = await _roleManager.CreateAsync(new IdentityRole("SuperAdmin"));
    //    var result3 = await _roleManager.CreateAsync(new IdentityRole("Creator"));
    //    var result4 = await _roleManager.CreateAsync(new IdentityRole("Editor"));
    //    var result5 = await _roleManager.CreateAsync(new IdentityRole("Reader"));

    //    AppUser admin = await _userManager.FindByNameAsync("Elnuradmin");

    //    var result = await _userManager.AddToRoleAsync(admin, "SuperAdmin");

    //    return Ok();
    //}

    //LOGIN ACTION
    public IActionResult Login()
        {
            return View();
        }
        //LOGIN POST ACTION
        [HttpPost]
        public async Task<IActionResult> Login(MemberLoginViewModel loginVM)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            AppUser member = await _userManager.Users.FirstOrDefaultAsync(x => x.UserName == loginVM.UserName && !x.IsAdmin);
            if (member == null)
            {
                ModelState.AddModelError("", "username or password is incorrect!");
                return View();
            }

            var result = await _signInManager.PasswordSignInAsync(member, loginVM.Password, loginVM.IsPersistent, true);
            if (result.IsLockedOut)
            {
                ModelState.AddModelError("", "too many false attempts, try again after 2 minutes ~.~");
                return View();
            }
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "username or password is incorrect!");
                return View();
            }


            return RedirectToAction("index", "home");
        }


        //PROFILE ACTION
        [Authorize(Roles = "Member")]
        public async Task<IActionResult> Profile()
        {
            AppUser member = await _userManager.Users.Where(x => x.UserName == User.Identity.Name && x.IsAdmin==false).FirstOrDefaultAsync();

            if (member == null)
            {
                return NotFound();
            }

            MemberProfileViewModel profileVM = new MemberProfileViewModel
            {
                Member = new MemberUpdateViewModel
                {
                    UserName = member.UserName,
                    FullName = member.FullName,
                    Email = member.Email,
                    Phone = member.PhoneNumber,
                    Address = member.Address,
                    City = member.City,
                    Country = member.Country,
                    Age = member.Age,
                    Image = member.Image
                },
                Orders = _context.Orders.Include(x => x.OrderItems).ThenInclude(x => x.Product).Where(x => x.AppUserId == member.Id).ToList(),
                AppUserCoupons = _context.AppUserCoupons.Include(x=>x.AppUser).Include(x=>x.CouponCategory).Where(x=>x.AppUserId==member.Id).ToList(),
                CouponCategories = _context.CouponCategories.Where(x=>x.IsDeleted==false).ToList()
            };
            return View(profileVM);
        }

        //EDIT ACTION
        [Authorize(Roles = "Member")]
        [HttpPost]
        public async Task<IActionResult> Edit(MemberUpdateViewModel memberVM)
        {
            if (memberVM == null)
            {
                return NotFound();
            }

            AppUser member = await _userManager.FindByNameAsync(User.Identity.Name);
            MemberProfileViewModel profileVM = new MemberProfileViewModel
            {
                Member = memberVM,
                Orders = _context.Orders.Include(x => x.OrderItems).ThenInclude(x => x.Product).Where(x => x.AppUserId == member.Id).ToList(),
                AppUserCoupons = _context.AppUserCoupons.Include(x => x.AppUser).Include(x => x.CouponCategory).Where(x => x.AppUserId == member.Id).ToList(),
                CouponCategories = _context.CouponCategories.Where(x => x.IsDeleted == false).ToList()
            };

            TempData["ProfileTab"] = "Account";

            if (!ModelState.IsValid)
                return View("Profile", profileVM);


            if (member.UserName != memberVM.UserName && _userManager.Users.Any(x => x.NormalizedUserName == memberVM.UserName.ToUpper()))
            {
                ModelState.AddModelError("UserName", "username is already exists ");
                return View("Profile", profileVM);
            }

            if (member.Email != memberVM.Email && _userManager.Users.Any(x => x.NormalizedEmail == memberVM.Email.ToUpper()))
            {
                ModelState.AddModelError("Email", "email is already exists");
                return View("Profile", profileVM);
            }

            member.UserName = memberVM.UserName;
            member.FullName = memberVM.FullName;
            member.Email = memberVM.Email;
            member.Country = memberVM.Country;
            member.City = memberVM.City;
            member.Address = memberVM.Address;
            member.PhoneNumber = memberVM.Phone;

            //IMAGE
            if (memberVM.ImageFile != null)
            {
                if (memberVM.ImageFile.ContentType != "image/jpeg" && memberVM.ImageFile.ContentType != "image/png")
                {
                    ModelState.AddModelError("ImageFile", "file type must be image/jpeg or image/png");
                    return View("Profile", profileVM);
                }

                if (memberVM.ImageFile.Length > 4194304)
                {
                    ModelState.AddModelError("ImageFile", "file size must be less than 4mb");
                    return View("Profile", profileVM);
                }

                 member.Image = FileManager.Save(_env.WebRootPath, "uploads/users", memberVM.ImageFile);
            }
            else
            {
                if (memberVM.Image == null)
                {
                    if (member.Image != null)
                    {
                        FileManager.Delete(_env.WebRootPath, "uploads/users", member.Image);
                    }
                    member.Image = null;
                }
            }

            var result = await _userManager.UpdateAsync(member);

            if (!result.Succeeded)
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
                return View("Profile", profileVM);
            }

            if (memberVM.Password != null)
            {
                if (string.IsNullOrWhiteSpace(memberVM.CurrentPassword))
                {
                    ModelState.AddModelError("CurrentPassword", "Current Password is required!");
                    return View("Profile", profileVM);
                }
                if (string.IsNullOrWhiteSpace(memberVM.Password))
                {
                    ModelState.AddModelError("Password", "New Password is required!");
                    return View("Profile", profileVM);
                }

                if (!await _userManager.CheckPasswordAsync(member, memberVM.CurrentPassword))
                {
                    ModelState.AddModelError("CurrentPassword", "Current Password is incorrect!");
                    return View("Profile", profileVM);
                }

                if (memberVM.CurrentPassword == memberVM.Password)
                {
                    ModelState.AddModelError("Password", "New Password is same as the current password");
                    return View("Profile", profileVM);
                }

                var passwordResult = _userManager.ChangePasswordAsync(member, memberVM.CurrentPassword, memberVM.Password);

                if (!passwordResult.Result.Succeeded)
                {
                    foreach (var item in passwordResult.Result.Errors)
                    {
                        ModelState.AddModelError("Password", item.Description);
                    }
                    return View("Profile", profileVM);
                }

            }

            await _signInManager.SignInAsync(member, false);

            TempData["Success"] = "Profile updated successfully";
            return RedirectToAction("profile");
        }

        //LOGOUT ACTION
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction("index", "home");
        }

        #region ForgotPassword
        public IActionResult ForgotPassword()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassword(string email)    
        {
            if (string.IsNullOrEmpty(email))
            {
                ModelState.AddModelError("Email", "Email is empty!");
                return View();
            }

            AppUser dbUser = await _userManager.FindByEmailAsync(email);
            if (dbUser is null)
            {
                ModelState.AddModelError("Email", "Email is not found!");
                return View();
            }
            //send email
            string forgotpasswordtoken = await _userManager.GeneratePasswordResetTokenAsync(dbUser);

            string link = Url.Action("ResetPassword", "Account", new { dbUser.Id,forgotpasswordtoken },Request.Scheme);

            //send EMAIL to reset password
            string resetbody = String.Empty;
            var pathreset = _env.WebRootPath + Path.DirectorySeparatorChar.ToString() + "Template" + Path.DirectorySeparatorChar.ToString() + "EmailTemplates" + Path.DirectorySeparatorChar.ToString() + "ResetPassword.html";
            using (StreamReader streamReader = System.IO.File.OpenText(pathreset))
            {
                resetbody = streamReader.ReadToEnd();
            }

            resetbody = resetbody.Replace("{fullname}", dbUser.FullName);
            resetbody = resetbody.Replace("{link}", link);

            MailMessage mailMessagecoupon = new MailMessage();
            mailMessagecoupon.To.Add(dbUser.Email);
            mailMessagecoupon.From = new MailAddress("drizlycode@gmail.com");
            mailMessagecoupon.Subject = "Hi " + dbUser.FullName + ",looks like you forgot your password!";
            mailMessagecoupon.Body = resetbody;
            mailMessagecoupon.IsBodyHtml = true;

            SmtpClient smtpcoupon = new SmtpClient();

            smtpcoupon.Credentials = new NetworkCredential("drizlycode@gmail.com", "Drizly21");
            smtpcoupon.Port = 587;
            smtpcoupon.Host = "smtp.gmail.com";
            smtpcoupon.EnableSsl = true;
            smtpcoupon.Send(mailMessagecoupon);


            TempData["Success"] = "Email sent to reset password!";
            return RedirectToAction("Login");
        }
        #endregion

        #region Reset Password
        public async  Task<IActionResult> ResetPassword(string id,string forgotpasswordtoken)
        {
            if (string.IsNullOrEmpty(id) || string.IsNullOrEmpty(forgotpasswordtoken))
            {
                return BadRequest();
            }
            var dbUser = await _userManager.FindByIdAsync(id);
            if (dbUser is null)
            {
                return NotFound();
            }

            ResetPasswordViewModel resetPasswordVM = new ResetPasswordViewModel
            {
                Id = id,
                Token = forgotpasswordtoken
            };

            return View(resetPasswordVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel resetpasswordVM)
        {
            if (string.IsNullOrEmpty(resetpasswordVM.Id) || string.IsNullOrEmpty(resetpasswordVM.Token))
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return View();
            }

            var dbUser = await _userManager.FindByIdAsync(resetpasswordVM.Id);
            if (dbUser is null)
            {
                return NotFound();
            }

            var result = await _userManager.ResetPasswordAsync(dbUser, resetpasswordVM.Token, resetpasswordVM.NewPassword);
            if (result.Errors.Count() !=  0)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View();
            }




            TempData["Success"] = "Password reset successfully";
            return RedirectToAction("Login");
        }
        #endregion
    }
}
