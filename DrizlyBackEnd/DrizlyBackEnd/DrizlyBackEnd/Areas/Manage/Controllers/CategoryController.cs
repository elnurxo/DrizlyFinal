using DrizlyBackEnd.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DrizlyBackEnd.Areas.Manage.Controllers
{
    [Microsoft.AspNetCore.Mvc.Area("manage")]
    //[Authorize(Roles = "SuperAdmin,Admin")]
    public class CategoryController : Controller
    { 
        private readonly DrizlyContext _context;
        private readonly IWebHostEnvironment _env;

        public CategoryController(DrizlyContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        //INDEX ACTION
        public IActionResult Index(int page = 1)
        {
            ViewBag.Products = _context.Products.Where(x => !x.IsDeleted).ToList();

            string pageSizeStr = _context.Settings.FirstOrDefault(x => x.Key == "PageSize").Value;
            int pageSize = string.IsNullOrWhiteSpace(pageSizeStr) ? 3 : int.Parse(pageSizeStr);
            return View(PagenatedList<Category>.Create(_context.Categories.Include(x => x.TypeProducts).ThenInclude(x=>x.Products).AsQueryable(), page, pageSize));
        }

        // EDIT ACTION
        public IActionResult Edit(int id)
        {
            Category category = _context.Categories.FirstOrDefault(x => x.Id == id);
            if (category == null) return NotFound();

            return View(category);
        }
        [HttpPost]
        public IActionResult Edit(Category category)
        {

            Category existCategory = _context.Categories.FirstOrDefault(x => x.Id == category.Id);

            if (existCategory == null) return NotFound();


            //IMAGE
            if (category.ImageFile != null)
            {
                if (category.ImageFile.ContentType != "image/jpeg" && category.ImageFile.ContentType != "image/png")
                {
                    ModelState.AddModelError("ImageFile", "file type must be jpeg or png");
                    return View(existCategory);
                }

                if (category.ImageFile.Length > 4194304)
                {
                    ModelState.AddModelError("ImageFile", "file size must be less than 4mb");
                    return View(existCategory);
                }

                string fileName = category.ImageFile.FileName;

                if (fileName.Length > 64)
                {
                    fileName = fileName.Substring(category.ImageFile.FileName.Length - 64, 64);
                }

                category.Image = (Guid.NewGuid().ToString() + (fileName));

                string path = Path.Combine(_env.WebRootPath, "uploads/categories", category.Image);

                using (FileStream stream = new FileStream(path, FileMode.Create))
                {
                    category.ImageFile.CopyTo(stream);
                }

                if (existCategory.Image != null)
                {
                    string existPath = Path.Combine(_env.WebRootPath, "uploads/categories", existCategory.Image);
                    if (System.IO.File.Exists(existPath))
                        System.IO.File.Delete(existPath);
                }

                existCategory.Image = category.Image;
            }

            if (existCategory == null) return NotFound();



            existCategory.Desc = category.Desc;
            _context.SaveChanges();

            return RedirectToAction("index");
        }
    }
}
