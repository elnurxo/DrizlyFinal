using DrizlyBackEnd.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DrizlyBackEnd.Areas.Manage.Controllers
{
    [Area("manage")]
    public class LiquorFlavorController : Controller
    {
        private readonly DrizlyContext _context;

        public LiquorFlavorController(DrizlyContext context)
        {
            _context = context;
        }

        //INDEX ACTION
        [Authorize(Roles = "SuperAdmin,Creator,Editor,Reader")]
        public IActionResult Index(string filter,int page = 1)
        {
            ViewBag.Products = _context.Products.Where(x => !x.IsDeleted).ToList();

            ViewBag.Filter = filter;

            var flavor = _context.LiquorFlavors.AsQueryable();

            if (filter != null)
                flavor = flavor.Where(x => x.IsDeleted == (filter == "true" ? true : false));

            string pageSizeStr = _context.Settings.FirstOrDefault(x => x.Key == "PageSize").Value;
            int pageSize = string.IsNullOrWhiteSpace(pageSizeStr) ? 3 : int.Parse(pageSizeStr);
            return View(PagenatedList<LiquorFlavor>.Create(flavor, page, pageSize));
        }

        //CREATE ACTION
        [Authorize(Roles = "SuperAdmin,Creator")]
        public IActionResult Create()
        {
            return View();
        }
        [Authorize(Roles = "SuperAdmin,Creator")]
        [HttpPost]
        public IActionResult Create(LiquorFlavor liquorFlavor)
        {

            if (!ModelState.IsValid)
                return View();

            if (liquorFlavor.Name != null)
            {
                if (liquorFlavor.Name.Length > 85)
                {
                    ModelState.AddModelError("Name", "flavor name cannot be longer than 85 characters!");
                    return View(liquorFlavor);
                }
            }
            else
            {
                ModelState.AddModelError("Name", "flavor name cannot be null!");
                return View(liquorFlavor);
            }

            liquorFlavor.CreatedAt = DateTime.UtcNow.AddHours(4);
            liquorFlavor.LastUpdateDate = DateTime.UtcNow.AddHours(4);
            _context.LiquorFlavors.Add(liquorFlavor);
            _context.SaveChanges();
            TempData["Success"] = "Liquor Flavor created successfully!";
            return RedirectToAction("index");
        }
        //EDIT ACTION
        [Authorize(Roles = "SuperAdmin,Editor")]
        public IActionResult Edit(int id)
        {
            LiquorFlavor liquorFlavor = _context.LiquorFlavors.FirstOrDefault(x => x.Id == id);

            if (liquorFlavor == null) return NotFound();

            return View(liquorFlavor);
        }
        [Authorize(Roles = "SuperAdmin,Editor")]
        [HttpPost]
        public IActionResult Edit(LiquorFlavor liquorFlavor)
        {
            if (!ModelState.IsValid)
                return View();

            LiquorFlavor existLiquorFlavor = _context.LiquorFlavors.FirstOrDefault(x => x.Id == liquorFlavor.Id);
            if (existLiquorFlavor == null) return NotFound();

            if (liquorFlavor.Name != null)
            {
                if (liquorFlavor.Name.Length > 85)
                {
                    ModelState.AddModelError("Name", "flavor name cannot be longer than 85 characters!");
                    return View(liquorFlavor);
                }
            }
            else
            {
                ModelState.AddModelError("Name", "flavor name cannot be null!");
                return View(liquorFlavor);
            }


            existLiquorFlavor.Name = liquorFlavor.Name;
            existLiquorFlavor.LastUpdateDate = DateTime.UtcNow.AddHours(4);
            _context.SaveChanges();
            TempData["Success"] = "Liquor Flavor updated successfully!";
            return RedirectToAction("index");
        }

        //DELETE ACTION
        [Authorize(Roles = "SuperAdmin,Editor")]
        public IActionResult Delete(int id)
        {
            LiquorFlavor existLiquorFlavor = _context.LiquorFlavors.FirstOrDefault(x => x.Id == id && !x.IsDeleted);

            if (existLiquorFlavor == null)
                return NotFound();

            ViewBag.Products = _context.Products.Where(x=>!x.IsDeleted).Where(x => x.LiquorFlavorId == id).ToList();

            foreach (var product in ViewBag.Products)
            {
                product.IsDeleted = true;
            }

            existLiquorFlavor.IsDeleted = true;
            _context.SaveChanges();
            TempData["Success"] = "Liquor Flavor deleted successfully!";
            return RedirectToAction("index");
        }

        //RESTORE ACTION
        [Authorize(Roles = "SuperAdmin,Editor")]
        public IActionResult Restore(int id)
        {
            LiquorFlavor existLiquorFlavor = _context.LiquorFlavors.FirstOrDefault(x => x.Id == id && x.IsDeleted);

            if (existLiquorFlavor == null)
                return NotFound();

            ViewBag.Products = _context.Products.Where(x => x.IsDeleted).Where(x => x.LiquorFlavorId == id).ToList();

            foreach (var product in ViewBag.Products)
            {
                product.IsDeleted = false;
            }

            existLiquorFlavor.IsDeleted = false;
            _context.SaveChanges();
            TempData["Success"] = "Liquor Flavor restored successfully!";
            return RedirectToAction("index");
        }
    }
}
