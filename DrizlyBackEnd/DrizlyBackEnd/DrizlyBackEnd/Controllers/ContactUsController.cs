using DrizlyBackEnd.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DrizlyBackEnd.Controllers
{
    public class ContactUsController : Controller
    {
        private readonly DrizlyContext _context;
        private readonly UserManager<AppUser> _userManager;

        public ContactUsController(DrizlyContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public IActionResult Index()
        {  
            return View();
        }
        //Contact Us Action
        [HttpPost]
        public IActionResult SendMessage(ContactUs message)
        {
            if (!ModelState.IsValid)
            {
                return View("index", message);
            }
            if (message.Name == null)
            {
                ModelState.AddModelError("Name", "Name is Required!");
                return View();
            }
            if (message.Email == null)
            {
                ModelState.AddModelError("Email", "Email is Required!");
                return View();
            }
            if (message.Subject == null)
            {
                ModelState.AddModelError("Subject", "Subject is Required!");
                return View();
            }
            if (message.Phone == null)
            {
                ModelState.AddModelError("Phone", "Phone is Required!");
                return View();
            }


            _context.ContactUs.Add(message);
            _context.SaveChanges();
            TempData["Success"] = "Message Sent!";
            return RedirectToAction("index","home");
        }
    }
}
