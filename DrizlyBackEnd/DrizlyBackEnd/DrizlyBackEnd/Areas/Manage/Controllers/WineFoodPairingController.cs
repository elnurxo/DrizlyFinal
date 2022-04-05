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
    //[Authorize(Roles = "SuperAdmin,Admin")]
    public class WineFoodPairingController : Controller
    {
        private readonly DrizlyContext _context;

        public WineFoodPairingController(DrizlyContext context)
        {
            _context = context;
        }
        //INDEX ACTION
        [Authorize(Roles = "SuperAdmin,Creator,Editor,Reader")]
        public IActionResult Index(string filter,int page = 1)
        {
            ViewBag.Products = _context.ProductFoodPairings.ToList();

            ViewBag.Filter = filter;

            var winefood = _context.WineFoodPairings.AsQueryable();

            if (filter != null)
                winefood = winefood.Where(x => x.IsDeleted == (filter == "true" ? true : false));

            string pageSizeStr = _context.Settings.FirstOrDefault(x => x.Key == "PageSize").Value;
            int pageSize = string.IsNullOrWhiteSpace(pageSizeStr) ? 3 : int.Parse(pageSizeStr);
            return View(PagenatedList<WineFoodPairing>.Create(winefood, page, pageSize));
        }

        //CREATE ACTION
        [Authorize(Roles = "SuperAdmin,Creator")]
        public IActionResult Create()
        {
            return View();
        }

        [Authorize(Roles = "SuperAdmin,Creator")]
        [HttpPost]
        public IActionResult Create(WineFoodPairing wineFoodPairing)
        {

            if (!ModelState.IsValid)
                return View();

            if (wineFoodPairing.Name != null)
            {
                if (wineFoodPairing.Name.Length > 85)
                {
                    ModelState.AddModelError("Name", "name cannot be longer than 85 characters!");
                    return View(wineFoodPairing);
                }
            }
            else
            {
                ModelState.AddModelError("Name", "name cannot be null!");
                return View(wineFoodPairing);
            }

            wineFoodPairing.CreatedAt = DateTime.UtcNow.AddHours(4);
            wineFoodPairing.LastUpdateDate = DateTime.UtcNow.AddHours(4);
            _context.WineFoodPairings.Add(wineFoodPairing);
            _context.SaveChanges();

            return RedirectToAction("index");
        }

        //EDIT ACTION
        [Authorize(Roles = "SuperAdmin,Editor")]
        public IActionResult Edit(int id)
        {
            WineFoodPairing wineFoodPairing = _context.WineFoodPairings.FirstOrDefault(x => x.Id == id);

            if (wineFoodPairing == null) return NotFound();

            return View(wineFoodPairing);
        }

        [Authorize(Roles = "SuperAdmin,Editor")]
        [HttpPost]
        public IActionResult Edit(WineFoodPairing wineFoodPairing)
        {
            if (!ModelState.IsValid)
                return View();

            WineFoodPairing existWineFoodPairing = _context.WineFoodPairings.FirstOrDefault(x => x.Id == wineFoodPairing.Id);
            if (existWineFoodPairing == null) return NotFound();

            if (wineFoodPairing.Name != null)
            {
                if (wineFoodPairing.Name.Length > 85)
                {
                    ModelState.AddModelError("Name", "name cannot be longer than 85 characters!");
                    return View(wineFoodPairing);
                }
            }
            else
            {
                ModelState.AddModelError("Name", "name cannot be null!");
                return View(wineFoodPairing);
            }


            existWineFoodPairing.Name = wineFoodPairing.Name;
            existWineFoodPairing.LastUpdateDate = DateTime.UtcNow.AddHours(4);
            _context.SaveChanges();
            return RedirectToAction("index");
        }

        //DELETE ACTION
        [Authorize(Roles = "SuperAdmin,Editor")]
        public IActionResult Delete(int id)
        {
            WineFoodPairing existWineFoodPairing = _context.WineFoodPairings.FirstOrDefault(x => x.Id == id && !x.IsDeleted);

            if (existWineFoodPairing == null)
                return NotFound();

            ViewBag.FoodPairing = _context.ProductFoodPairings.Where(x => x.WineFoodPairingId == id).Include(x=>x.Product).ToList();

            foreach (var product in ViewBag.FoodPairing)
            {
                _context.ProductFoodPairings.Remove(product);
            }
           

            existWineFoodPairing.IsDeleted = true;
            _context.SaveChanges();

            return RedirectToAction("index");
        }

        //RESTORE ACTION
        [Authorize(Roles = "SuperAdmin,Editor")]
        public IActionResult Restore(int id)
        {
            WineFoodPairing existWineFoodPairing = _context.WineFoodPairings.FirstOrDefault(x => x.Id == id && x.IsDeleted);

            if (existWineFoodPairing == null)
                return NotFound();

            ViewBag.FoodPairing = _context.ProductFoodPairings.Where(x => x.WineFoodPairingId == id).Include(x => x.Product).ToList();

            foreach (var product in ViewBag.FoodPairing)
            {
                _context.ProductFoodPairings.Add(product);
            }

            existWineFoodPairing.IsDeleted = false;
            _context.SaveChanges();

            return RedirectToAction("index");
        }
    }
}
