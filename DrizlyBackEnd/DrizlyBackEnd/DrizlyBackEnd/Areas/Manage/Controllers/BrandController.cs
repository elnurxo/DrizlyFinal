﻿using DrizlyBackEnd.Models;
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
    public class BrandController : Controller
    {
        private readonly DrizlyContext _context;

        public BrandController(DrizlyContext context)
        {
            _context = context;
        }
        //INDEX ACTION
        [Authorize(Roles = "SuperAdmin,Creator,Editor,Reader")]
        public IActionResult Index(string filter, int page = 1)
        {
            ViewBag.Filter = filter;

            var brands = _context.Brands.Include(x => x.Products).AsQueryable();

            if (filter != null)
                brands = brands.Where(x => x.IsDeleted == (filter == "true" ? true : false));

            string pageSizeStr = _context.Settings.FirstOrDefault(x => x.Key == "PageSize").Value;
            int pageSize = string.IsNullOrWhiteSpace(pageSizeStr) ? 3 : int.Parse(pageSizeStr);
            return View(PagenatedList<Brand>.Create(brands, page, pageSize));
        }
        //CREATE ACTION
        [Authorize(Roles = "SuperAdmin,Creator")]
        public IActionResult Create()
        {
            return View();
        }
        [Authorize(Roles = "SuperAdmin,Creator")]
        [HttpPost]
        public IActionResult Create(Brand brand)
        {

            if (!ModelState.IsValid)
                return View();

            if (brand.Name.Length > 85)
            {
                ModelState.AddModelError("Name", "name cannot be longer than 85 characters!");
                return View(brand);
            }

            brand.CreatedAt = DateTime.UtcNow.AddHours(4);
            brand.LastUpdateDate = DateTime.UtcNow.AddHours(4);
            _context.Brands.Add(brand);
            _context.SaveChanges();
            TempData["Success"] = "Brand created successfully!";
            return RedirectToAction("index");
        }

        //EDIT ACTION
        [Authorize(Roles = "SuperAdmin,Editor")]
        public IActionResult Edit(int id)
        {
            Brand brand = _context.Brands.FirstOrDefault(x => x.Id == id);

            if (brand == null) return NotFound();

            return View(brand);
        }
        [Authorize(Roles = "SuperAdmin,Editor")]
        [HttpPost]
        public IActionResult Edit(Brand brand)
        {
            if (!ModelState.IsValid)
                return View();

            Brand existBrand = _context.Brands.FirstOrDefault(x => x.Id == brand.Id);
            if (existBrand == null) return NotFound();

            existBrand.Name = brand.Name;

            existBrand.LastUpdateDate = DateTime.UtcNow.AddHours(4);
            _context.SaveChanges();
            TempData["Success"] = "Brand updated successfully!";
            return RedirectToAction("index");
        }

        //DELETE ACTION
        [Authorize(Roles = "SuperAdmin,Editor")]
        public IActionResult Delete(int id)
        {
            Brand existBrand = _context.Brands.FirstOrDefault(x => x.Id == id && !x.IsDeleted);

            if (existBrand == null)
                return NotFound();

            ViewBag.Products = _context.Products.Where(x => !x.IsDeleted).Where(x => x.BrandId == id).ToList();

            foreach (var product in ViewBag.Products)
            {
                product.IsDeleted = true;
            }

            existBrand.IsDeleted = true;
            _context.SaveChanges();
            TempData["Success"] = "Brand deleted successfully!";
            return RedirectToAction("index");
        }

        //RESTORE ACTION
        [Authorize(Roles = "SuperAdmin,Editor")]
        public IActionResult Restore(int id)
        {
            Brand existBrand = _context.Brands.FirstOrDefault(x => x.Id == id && x.IsDeleted);

            if (existBrand == null)
                return NotFound();

            ViewBag.Products = _context.Products.Where(x => x.IsDeleted).Where(x => x.BrandId == id).ToList();

            foreach (var product in ViewBag.Products)
            {
                product.IsDeleted = false;
            }

            existBrand.IsDeleted = false;
            _context.SaveChanges();
            TempData["Success"] = "Brand restored successfully!";
            return RedirectToAction("index");
        }
    }
}
