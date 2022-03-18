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
    public class NewsController : Controller
    {
        private readonly DrizlyContext _context;
        private readonly IWebHostEnvironment _env;

        public NewsController(DrizlyContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        //INDEX ACTION
        public IActionResult Index(int page = 1)
        {
            string pageSizeStr = _context.Settings.FirstOrDefault(x => x.Key == "PageSize").Value;
            int pageSize = string.IsNullOrWhiteSpace(pageSizeStr) ? 3 : int.Parse(pageSizeStr);
            return View(PagenatedList<News>.Create(_context.News.AsQueryable(), page, pageSize));
        }
        //CREATE ACTION
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(News news)
        {
            if (news.ImageFile == null)
                ModelState.AddModelError("ImageFile", "Image file is required!");

            if (!ModelState.IsValid)
                return View();

            if (news.ImageFile.ContentType != "image/jpeg" && news.ImageFile.ContentType != "image/png")
            {
                ModelState.AddModelError("ImageFile", "File type must be jpeg or png");
                return View(news);
            }

            if (news.ImageFile.Length > 2097152)
            {
                ModelState.AddModelError("ImageFile", "file size must be less than 2mb");
                return View(news);
            }

            string fileName = news.ImageFile.FileName;

            if (fileName.Length > 64)
            {
                fileName = fileName.Substring(news.ImageFile.FileName.Length - 64, 64);
            }

            news.Image = (Guid.NewGuid().ToString() + (fileName));

            string path = Path.Combine(_env.WebRootPath, "uploads/news", news.Image);

            using (FileStream stream = new FileStream(path, FileMode.Create))
            {
                news.ImageFile.CopyTo(stream);
            }

            news.CreatedAt = DateTime.UtcNow.AddHours(4);

            _context.News.Add(news);
            _context.SaveChanges();

            return RedirectToAction("index");
        }

        //EDIT ACTION
        public IActionResult Edit(int id)
        {
            News news = _context.News.FirstOrDefault(x => x.Id == id);

            if (news == null) return NotFound();

            return View(news);
        }

        [HttpPost]
        public IActionResult Edit(News news)
        {
            if (!ModelState.IsValid)
                return View();

            News existNews = _context.News.FirstOrDefault(x => x.Id == news.Id);
            if (existNews == null) return NotFound();
            if (news.ImageFile != null)
            {
                if (news.ImageFile.ContentType != "image/jpeg" && news.ImageFile.ContentType != "image/png")
                {
                    ModelState.AddModelError("ImageFile", "file type must be jpeg or png");
                    return View(news);
                }

                if (news.ImageFile.Length > 2097152)
                {
                    ModelState.AddModelError("ImageFile", "file size must be less than 2mb");
                    return View(news);
                }

                string fileName = news.ImageFile.FileName;

                if (fileName.Length > 64)
                {
                    fileName = fileName.Substring(news.ImageFile.FileName.Length - 64, 64);
                }

                news.Image = (Guid.NewGuid().ToString() + (fileName));

                string path = Path.Combine(_env.WebRootPath, "uploads/news", news.Image);

                using (FileStream stream = new FileStream(path, FileMode.Create))
                {
                    news.ImageFile.CopyTo(stream);
                }

                if (existNews.Image != null)
                {
                    string existPath = Path.Combine(_env.WebRootPath, "uploads/news", existNews.Image);
                    if (System.IO.File.Exists(existPath))
                        System.IO.File.Delete(existPath);
                }

                existNews.Image = news.Image;
            }
            else
            {
                if (news.Image == null && existNews.Image != null)
                {
                    string existPath = Path.Combine(_env.WebRootPath, "uploads/news", existNews.Image);
                    if (System.IO.File.Exists(existPath))
                        System.IO.File.Delete(existPath);

                    existNews.Image = null;
                }
            }

            if (existNews == null) return NotFound();
            existNews.Image = news.Image;

            _context.SaveChanges();
            return RedirectToAction("index");
        }

        //DELETE ACTION
        public IActionResult Delete(int id)
        {
            News existNews = _context.News.FirstOrDefault(x => x.Id == id);

            if (existNews == null)
                return NotFound();

            _context.News.Remove(existNews);

            string existPath = Path.Combine(_env.WebRootPath, "uploads/news", existNews.Image);
            if (System.IO.File.Exists(existPath))
                System.IO.File.Delete(existPath);

            _context.SaveChanges();

            return RedirectToAction("index");
        }
    }
}
