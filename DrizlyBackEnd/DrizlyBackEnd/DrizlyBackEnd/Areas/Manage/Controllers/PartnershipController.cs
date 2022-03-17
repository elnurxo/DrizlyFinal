using DrizlyBackEnd.Models;
using Microsoft.AspNetCore.Authorization;
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
    public class PartnershipController : Controller
    {
        private readonly DrizlyContext _context;
        private readonly IWebHostEnvironment _env;

        public PartnershipController(DrizlyContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        //INDEX ACTION
        public IActionResult Index(int page = 1)
        {
            string pageSizeStr = _context.Settings.FirstOrDefault(x => x.Key == "PageSize").Value;
            int pageSize = string.IsNullOrWhiteSpace(pageSizeStr) ? 3 : int.Parse(pageSizeStr);
            return View(PagenatedList<Partnership>.Create(_context.Partnerships.AsQueryable(), page, pageSize));
        }
        //CREATE ACTION
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Partnership partnership)
        {
            if (partnership.ImageFile == null)
                ModelState.AddModelError("ImageFile", "Image file is required!");

            if (!ModelState.IsValid)
                return View();

            if (partnership.ImageFile.ContentType != "image/jpeg" && partnership.ImageFile.ContentType != "image/png")
            {
                ModelState.AddModelError("ImageFile", "File type must be jpeg or png");
                return View(partnership);
            }

            if (partnership.ImageFile.Length > 2097152)
            {
                ModelState.AddModelError("ImageFile", "file size must be less than 2mb");
                return View(partnership);
            }

            string fileName = partnership.ImageFile.FileName;

            if (fileName.Length > 64)
            {
                fileName = fileName.Substring(partnership.ImageFile.FileName.Length - 64, 64);
            }

            partnership.Image = (Guid.NewGuid().ToString() + (fileName));

            string path = Path.Combine(_env.WebRootPath, "uploads/partnerships", partnership.Image);

            using (FileStream stream = new FileStream(path, FileMode.Create))
            {
                partnership.ImageFile.CopyTo(stream);
            }

            partnership.CreatedAt = DateTime.UtcNow.AddHours(4);

            _context.Partnerships.Add(partnership);
            _context.SaveChanges();

            return RedirectToAction("index");
        }

        //EDIT ACTION
        public IActionResult Edit(int id)
        {
            Partnership partner = _context.Partnerships.FirstOrDefault(x => x.Id == id);

            if (partner == null) return NotFound();

            return View(partner);
        }

        [HttpPost]
        public IActionResult Edit(Partnership partner)
        {
            if (!ModelState.IsValid)
                return View();

            Partnership existPartner = _context.Partnerships.FirstOrDefault(x => x.Id == partner.Id);
            if (existPartner == null) return NotFound();
            if (partner.ImageFile != null)
            {
                if (partner.ImageFile.ContentType != "image/jpeg" && partner.ImageFile.ContentType != "image/png")
                {
                    ModelState.AddModelError("ImageFile", "file type must be jpeg or png");
                    return View(partner);
                }

                if (partner.ImageFile.Length > 2097152)
                {
                    ModelState.AddModelError("ImageFile", "file size must be less than 2mb");
                    return View(partner);
                }

                string fileName = partner.ImageFile.FileName;

                if (fileName.Length > 64)
                {
                    fileName = fileName.Substring(partner.ImageFile.FileName.Length - 64, 64);
                }

                partner.Image = (Guid.NewGuid().ToString() + (fileName));

                string path = Path.Combine(_env.WebRootPath, "uploads/partnerships", partner.Image);

                using (FileStream stream = new FileStream(path, FileMode.Create))
                {
                    partner.ImageFile.CopyTo(stream);
                }

                if (existPartner.Image != null)
                {
                    string existPath = Path.Combine(_env.WebRootPath, "uploads/partnerships", existPartner.Image);
                    if (System.IO.File.Exists(existPath))
                        System.IO.File.Delete(existPath);
                }

                existPartner.Image = partner.Image;
            }
            else
            {
                if (partner.Image == null && existPartner.Image != null)
                {
                    string existPath = Path.Combine(_env.WebRootPath, "uploads/partnerships", existPartner.Image);
                    if (System.IO.File.Exists(existPath))
                        System.IO.File.Delete(existPath);

                    existPartner.Image = null;
                }
            }

            if (existPartner == null) return NotFound();
            existPartner.Image = partner.Image;

            _context.SaveChanges();
            return RedirectToAction("index");
        }

        //DELETE ACTION
        public IActionResult Delete(int id)
        {
            Partnership existPartner = _context.Partnerships.FirstOrDefault(x => x.Id == id);

            if (existPartner == null)
                return NotFound();

            _context.Partnerships.Remove(existPartner);

            string existPath = Path.Combine(_env.WebRootPath, "uploads/partnerships", existPartner.Image);
            if (System.IO.File.Exists(existPath))
                System.IO.File.Delete(existPath);

            _context.SaveChanges();

            return RedirectToAction("index");
        }
    }
}
