using DrizlyBackEnd.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DrizlyBackEnd.Areas.Manage.Controllers
{
    [Area("manage")]
    //[Authorize(Roles = "SuperAdmin,Admin")]
    public class SettingController : Controller
    {
        private readonly DrizlyContext _context;
        private readonly IWebHostEnvironment _env;

        public SettingController(DrizlyContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        //INDEX ACTION
        public IActionResult Index(int page = 1)
        {
            string pageSizeStr = _context.Settings.FirstOrDefault(x => x.Key == "PageSize").Value;
            int pageSize = string.IsNullOrWhiteSpace(pageSizeStr) ? 3 : int.Parse(pageSizeStr);
            return View(PagenatedList<Settings>.Create(_context.Settings.AsQueryable(), page, pageSize));
        }

        //EDIT ACTION
        public IActionResult Edit(int id)
        {
            Settings settings = _context.Settings.FirstOrDefault(x => x.Id == id);

            if (settings == null) return NotFound();

            return View(settings);
        }
        [HttpPost]
        public IActionResult Edit(Settings settings)
        {
            if (!ModelState.IsValid)
                return View(settings);

            Settings existSetting = _context.Settings.FirstOrDefault(x => x.Id == settings.Id);
            if (existSetting == null) return NotFound();
            if (settings.ImageFile != null)
            {
                if (settings.ImageFile.ContentType != "image/jpeg" && settings.ImageFile.ContentType != "image/png")
                {
                    ModelState.AddModelError("ImageFile", "file type must be jpeg or png");
                    return View(settings);
                }

                if (settings.ImageFile.Length > 4194304)
                {
                    ModelState.AddModelError("ImageFile", "file size must be less than 4mb");
                    return View(settings);
                }

                string fileName = settings.ImageFile.FileName;

                if (fileName.Length > 64)
                {
                    fileName = fileName.Substring(settings.ImageFile.FileName.Length - 64, 64);
                }

                settings.Value = (Guid.NewGuid().ToString() + (fileName));

                string path = Path.Combine(_env.WebRootPath, "uploads/settings", settings.Value);

                using (FileStream stream = new FileStream(path, FileMode.Create))
                {
                    settings.ImageFile.CopyTo(stream);
                }

                if (existSetting.Value != null)
                {
                    string existPath = Path.Combine(_env.WebRootPath, "uploads/settings", existSetting.Value);
                    if (System.IO.File.Exists(existPath))
                        System.IO.File.Delete(existPath);
                }

                existSetting.Value = settings.Value;
            }
            else
            {
                if (settings.Value == null && existSetting.Value != null)
                {
                    string existPath = Path.Combine(_env.WebRootPath, "uploads/settings", existSetting.Value);
                    if (System.IO.File.Exists(existPath))
                        System.IO.File.Delete(existPath);

                    existSetting.Value = null;
                }
            }

            existSetting.Value = settings.Value;
            existSetting.LastUpdateDate = DateTime.UtcNow.AddHours(4);

            _context.SaveChanges();
            return RedirectToAction("index");
        }
    }
}
