using DrizlyBackEnd.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DrizlyBackEnd.Areas.Manage.Controllers
{
    [Area("manage")]
    public class BanWordController : Controller
    {
        private readonly DrizlyContext _context;

        public BanWordController(DrizlyContext context)
        {
            _context = context;
        }
        //INDEX ACTION
        [Authorize(Roles = "SuperAdmin,Creator,Editor,Reader")]
        public IActionResult Index(int page = 1)
        {
            var badwords = _context.BanWords.AsQueryable();

            string pageSizeStr = _context.Settings.FirstOrDefault(x => x.Key == "PageSize").Value;
            int pageSize = string.IsNullOrWhiteSpace(pageSizeStr) ? 3 : int.Parse(pageSizeStr);
            return View(PagenatedList<BanWord>.Create(badwords, page, pageSize));
        }
        //CREATE ACTION
        [Authorize(Roles = "SuperAdmin,Creator")]
        public IActionResult Create()
        {
            return View();
        }
        [Authorize(Roles = "SuperAdmin,Creator")]
        [HttpPost]
        public IActionResult Create(BanWord banWord)
        {

            if (!ModelState.IsValid)
                return View();

            if (banWord.Text.Length > 100)
            {
                ModelState.AddModelError("Text", "text cannot be longer than 100 characters!");
                return View(banWord);
            }


            _context.BanWords.Add(banWord);
            _context.SaveChanges();
            TempData["Success"] = "Ban Word created successfully!";
            return RedirectToAction("index");
        }
        //DELETE ACTION
        [Authorize(Roles = "SuperAdmin,Editor")]
        public IActionResult Delete(int id)
        {
            BanWord existBanWord = _context.BanWords.FirstOrDefault(x => x.Id == id);

            if (existBanWord == null)
                return NotFound();

            _context.BanWords.Remove(existBanWord);

            _context.SaveChanges();
            TempData["Success"] = "Banned word deleted successfully!";
            return RedirectToAction("index");
        }

        //EDIT ACTION
        [Authorize(Roles = "SuperAdmin,Editor")]
        public IActionResult Edit(int id)
        {
            BanWord banWord = _context.BanWords.FirstOrDefault(x => x.Id == id);

            if (banWord == null) return NotFound();

            return View(banWord);
        }

        [Authorize(Roles = "SuperAdmin,Editor")]
        [HttpPost]
        public IActionResult Edit(BanWord banWord)
        {
            if (!ModelState.IsValid)
                return View();

            BanWord existBanWord = _context.BanWords.FirstOrDefault(x => x.Id == banWord.Id);
            if (existBanWord == null) return NotFound();

            existBanWord.Text = banWord.Text;

            _context.SaveChanges();
            TempData["Success"] = "Banned word updated successfully!";
            return RedirectToAction("index");
        }
    }
}
