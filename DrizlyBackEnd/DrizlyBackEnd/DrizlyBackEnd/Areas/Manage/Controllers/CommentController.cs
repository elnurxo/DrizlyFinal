using DrizlyBackEnd.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DrizlyBackEnd.Areas.Manage.Controllers
{
    [Area("manage")]
    public class CommentController : Controller
    {
        private readonly DrizlyContext _context;
        private readonly IWebHostEnvironment _env;

        public CommentController(DrizlyContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        //INDEX ACTION (ALL PRODUCT REVIEWS)
        [Authorize(Roles = "SuperAdmin,Creator,Editor,Reader")]
        public IActionResult Index(int page = 1)
        {
            var reviews = _context.ProductComments.Include(x => x.AppUser).Include(x => x.Product).AsQueryable();
            string pageSizeStr = _context.Settings.FirstOrDefault(x => x.Key == "PageSize").Value;
            int pageSize = string.IsNullOrWhiteSpace(pageSizeStr) ? 3 : int.Parse(pageSizeStr);
            return View(PagenatedList<ProductComment>.Create(reviews, page, pageSize));
        }
        //INDEX ACTION (FOR SPECIFIC PRODUCT)
        [Authorize(Roles = "SuperAdmin,Creator,Editor,Reader")]
        public IActionResult ProductReview(int id, int page = 1)
        {
            var reviews = _context.ProductComments.Include(x => x.AppUser).Include(x => x.Product).Where(x => x.ProductId == id).AsQueryable();
            string pageSizeStr = _context.Settings.FirstOrDefault(x => x.Key == "PageSize").Value;
            int pageSize = string.IsNullOrWhiteSpace(pageSizeStr) ? 3 : int.Parse(pageSizeStr);
            return View(PagenatedList<ProductComment>.Create(reviews, page, pageSize));
        }
        [Authorize(Roles = "SuperAdmin,Editor")]
        //DELETE ACTION
        public IActionResult Delete(int id)
        {
            ProductComment existReview = _context.ProductComments.FirstOrDefault(x => x.Id == id);

            if (existReview == null)
                return NotFound();

            _context.ProductComments.Remove(existReview);
            _context.SaveChanges();

            return RedirectToAction("index");
        }
    }
}
