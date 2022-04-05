using DrizlyBackEnd.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DrizlyBackEnd.Areas.Manage.Controllers
{
    [Area("manage")]
    //[Authorize(Roles = "SuperAdmin")]
    public class MessageController : Controller
    {
        private readonly DrizlyContext _context;
        private readonly IWebHostEnvironment _env;

        public MessageController(DrizlyContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }


        //INDEX ACTION
        [Authorize(Roles = "SuperAdmin,Creator,Editor,Reader")]
        public IActionResult Index(string filter,int page = 1)
        {
            ViewBag.Filter = filter;

            var messages = _context.ContactUs.AsQueryable();

            if (filter != null)
                messages = messages.Where(x => x.IsRead == (filter == "true" ? true : false));


            string pageSizeStr = _context.Settings.FirstOrDefault(x => x.Key == "PageSize").Value;
            int pageSize = string.IsNullOrWhiteSpace(pageSizeStr) ? 3 : int.Parse(pageSizeStr);
            return View(PagenatedList<ContactUs>.Create(messages, page, pageSize));
        }

        //DELETE ACTION
        [Authorize(Roles = "SuperAdmin,Editor")]
        public IActionResult Delete(int id)
        {
            ContactUs existMessage = _context.ContactUs.FirstOrDefault(x => x.Id == id);

            if (existMessage == null)
                return NotFound();

            _context.ContactUs.Remove(existMessage);
            _context.SaveChanges();
            TempData["Success"] = "Message deleted successfully!";
            return RedirectToAction("index");
        }

        //MARK-AS-READ ACTION
        [Authorize(Roles = "SuperAdmin,Editor")]
        public IActionResult MarkAsRead(int id)
        {
            ContactUs existMessage = _context.ContactUs.FirstOrDefault(x => x.Id == id);

            if (existMessage == null)
                return NotFound();

            existMessage.IsRead = true;
            _context.SaveChanges();
            TempData["Success"] = "Message marked as Read successfully!";
            return RedirectToAction("index");
        }
    }
}
