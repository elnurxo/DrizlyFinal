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
    public class ServiceController : Controller
    {
        private readonly DrizlyContext _context;
        private readonly IWebHostEnvironment _env;

        public ServiceController(DrizlyContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        //INDEX ACTION
        [Authorize(Roles = "SuperAdmin,Creator,Editor,Reader")]
        public IActionResult Index(int page = 1)
        {
            string pageSizeStr = _context.Settings.FirstOrDefault(x => x.Key == "PageSize").Value;
            int pageSize = string.IsNullOrWhiteSpace(pageSizeStr) ? 3 : int.Parse(pageSizeStr);
            return View(PagenatedList<Service>.Create(_context.Services.AsQueryable(), page, pageSize));
        }
        //CREATE ACTION
        [Authorize(Roles = "SuperAdmin,Creator")]
        public IActionResult Create()
        {
            return View();
        }

        [Authorize(Roles = "SuperAdmin,Creator")]
        [HttpPost]
        public IActionResult Create(Service service)
        {
            if (service.ImageFile == null)
                ModelState.AddModelError("ImageFile", "Image file is required!");

            if (!ModelState.IsValid)
                return View();

            if (service.ImageFile.ContentType != "image/jpeg" && service.ImageFile.ContentType != "image/png")
            {
                ModelState.AddModelError("ImageFile", "File type must be jpeg or png");
                return View(service);
            }

            if (service.ImageFile.Length > 2097152)
            {
                ModelState.AddModelError("ImageFile", "file size must be less than 2mb");
                return View(service);
            }

            string fileName = service.ImageFile.FileName;

            if (fileName.Length > 64)
            {
                fileName = fileName.Substring(service.ImageFile.FileName.Length - 64, 64);
            }

            service.Image = (Guid.NewGuid().ToString() + (fileName));

            string path = Path.Combine(_env.WebRootPath, "uploads/services", service.Image);

            using (FileStream stream = new FileStream(path, FileMode.Create))
            {
                service.ImageFile.CopyTo(stream);
            }


            _context.Services.Add(service);
            _context.SaveChanges();

            return RedirectToAction("index");
        }

        //EDIT ACTION
        [Authorize(Roles = "SuperAdmin,Editor")]
        public IActionResult Edit(int id)
        {
            Service service = _context.Services.FirstOrDefault(x => x.Id == id);

            if (service == null) return NotFound();

            return View(service);
        }

        [Authorize(Roles = "SuperAdmin,Editor")]
        [HttpPost]
        public IActionResult Edit(Service service)
        {
            if (!ModelState.IsValid)
                return View();

            Service existService = _context.Services.FirstOrDefault(x => x.Id == service.Id);
            if (existService == null) return NotFound();
            if (service.ImageFile != null)
            {
                if (service.ImageFile.ContentType != "image/jpeg" && service.ImageFile.ContentType != "image/png")
                {
                    ModelState.AddModelError("ImageFile", "file type must be jpeg or png");
                    return View(service);
                }

                if (service.ImageFile.Length > 2097152)
                {
                    ModelState.AddModelError("ImageFile", "file size must be less than 2mb");
                    return View(service);
                }

                string fileName = service.ImageFile.FileName;

                if (fileName.Length > 64)
                {
                    fileName = fileName.Substring(service.ImageFile.FileName.Length - 64, 64);
                }

                service.Image = (Guid.NewGuid().ToString() + (fileName));

                string path = Path.Combine(_env.WebRootPath, "uploads/services", service.Image);

                using (FileStream stream = new FileStream(path, FileMode.Create))
                {
                    service.ImageFile.CopyTo(stream);
                }

                if (existService.Image != null)
                {
                    string existPath = Path.Combine(_env.WebRootPath, "uploads/services", existService.Image);
                    if (System.IO.File.Exists(existPath))
                        System.IO.File.Delete(existPath);
                }

                existService.Image = service.Image;
            }

            if (existService == null) return NotFound();
            existService.Title = service.Title;
            existService.Desc = service.Desc;

            _context.SaveChanges();
            return RedirectToAction("index");
        }

        //DELETE ACTION
        [Authorize(Roles = "SuperAdmin,Editor")]
        public IActionResult Delete(int id)
        {
            Service existService = _context.Services.FirstOrDefault(x => x.Id == id);

            if (existService == null)
                return NotFound();

            _context.Services.Remove(existService);

            string existPath = Path.Combine(_env.WebRootPath, "uploads/services", existService.Image);
            if (System.IO.File.Exists(existPath))
                System.IO.File.Delete(existPath);

            _context.SaveChanges();

            return RedirectToAction("index");
        }
    }
}
