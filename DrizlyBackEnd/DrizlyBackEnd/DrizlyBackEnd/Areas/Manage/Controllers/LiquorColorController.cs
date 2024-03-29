﻿using DrizlyBackEnd.Models;
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
    //[Authorize(Roles = "SuperAdmin,Admin")]
    public class LiquorColorController : Controller
    {
        private readonly DrizlyContext _context;

        public LiquorColorController(DrizlyContext context)
        {
            _context = context;
        }
        //INDEX ACTION
        [Authorize(Roles = "SuperAdmin,Creator,Editor,Reader")]
        public IActionResult Index(string filter, int page = 1)
        {
            ViewBag.Products = _context.Products.Where(x => !x.IsDeleted).ToList();

            ViewBag.Filter = filter;

            var color = _context.LiquorColors.AsQueryable();

            if (filter != null)
                color = color.Where(x => x.IsDeleted == (filter == "true" ? true : false));

            string pageSizeStr = _context.Settings.FirstOrDefault(x => x.Key == "PageSize").Value;
            int pageSize = string.IsNullOrWhiteSpace(pageSizeStr) ? 3 : int.Parse(pageSizeStr);
            return View(PagenatedList<LiquorColor>.Create(color, page, pageSize));
        }

        //CREATE ACTION
        [Authorize(Roles = "SuperAdmin,Creator")]
        public IActionResult Create()
        {
            return View();
        }
        [Authorize(Roles = "SuperAdmin,Creator")]
        [HttpPost]
        public IActionResult Create(LiquorColor liquorColor)
        {

            if (!ModelState.IsValid)
                return View();

            if (liquorColor.Color!=null)
            {
                if (liquorColor.Color.Length > 85)
                {
                    ModelState.AddModelError("Color", "color name cannot be longer than 85 characters!");
                    return View(liquorColor);
                }
            }
            else
            {
                ModelState.AddModelError("Color", "color name cannot be null!");
                return View(liquorColor);
            }


            liquorColor.CreatedAt = DateTime.UtcNow.AddHours(4);
            liquorColor.LastUpdateDate = DateTime.UtcNow.AddHours(4);
            _context.LiquorColors.Add(liquorColor);
            _context.SaveChanges();
            TempData["Success"] = "Liquor Color created successfully!";
            return RedirectToAction("index");
        }

        //EDIT ACTION
        [Authorize(Roles = "SuperAdmin,Editor")]
        public IActionResult Edit(int id)
        {
            LiquorColor liquorColor = _context.LiquorColors.FirstOrDefault(x => x.Id == id);

            if (liquorColor == null) return NotFound();

            return View(liquorColor);
        }
        [Authorize(Roles = "SuperAdmin,Editor")]
        [HttpPost]
        public IActionResult Edit(LiquorColor liquorColor)
        {
            if (!ModelState.IsValid)
                return View();

            LiquorColor existliquorColor = _context.LiquorColors.FirstOrDefault(x => x.Id == liquorColor.Id);
            if (existliquorColor == null) return NotFound();

            if (liquorColor.Color != null)
            {
                if (liquorColor.Color.Length > 85)
                {
                    ModelState.AddModelError("Color", "color name cannot be longer than 85 characters!");
                    return View(liquorColor);
                }
            }
            else
            {
                ModelState.AddModelError("Color", "color name cannot be null!");
                return View(liquorColor);
            }


            existliquorColor.Color = liquorColor.Color;
            existliquorColor.LastUpdateDate = DateTime.UtcNow.AddHours(4);
            _context.SaveChanges();
            TempData["Success"] = "Liquor Color updated successfully!";
            return RedirectToAction("index");
        }
        //DELETE ACTION
        [Authorize(Roles = "SuperAdmin,Editor")]
        public IActionResult Delete(int id)
        {
            LiquorColor existLiquorColor = _context.LiquorColors.FirstOrDefault(x => x.Id == id && !x.IsDeleted);

            if (existLiquorColor == null)
                return NotFound();


            ViewBag.Products = _context.Products.Where(x => !x.IsDeleted).Where(x => x.LiquorColorId == id).ToList();

            foreach (var product in ViewBag.Products)
            {
                product.IsDeleted = true;
            }

            existLiquorColor.IsDeleted = true;
            _context.SaveChanges();
            TempData["Success"] = "Liquor Color deleted successfully!";
            return RedirectToAction("index");
        }

        //RESTORE ACTION
        [Authorize(Roles = "SuperAdmin,Editor")]
        public IActionResult Restore(int id)
        {
            LiquorColor existLiquorColor = _context.LiquorColors.FirstOrDefault(x => x.Id == id && x.IsDeleted);

            if (existLiquorColor == null)
                return NotFound();

            ViewBag.Products = _context.Products.Where(x => x.IsDeleted).Where(x => x.LiquorColorId == id).ToList();

            foreach (var product in ViewBag.Products)
            {
                product.IsDeleted = false;
            }

            existLiquorColor.IsDeleted = false;
            _context.SaveChanges();
            TempData["Success"] = "Liquor Color restored successfully!";
            return RedirectToAction("index");
        }
    }
}
