﻿using DrizlyBackEnd.Models;
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
    public class TypeProductController : Controller
    {
        private readonly DrizlyContext _context;

        public TypeProductController(DrizlyContext context)
        {
            _context = context;
        }

        public IActionResult Index(string filter, int page = 1)
        {
            ViewBag.Products = _context.Products.Where(x => !x.IsDeleted).ToList();

            ViewBag.Filter = filter;

            var type = _context.TypeProducts.Include(x=>x.Category).AsQueryable();

            if (filter != null)
                type = type.Where(x => x.IsDeleted == (filter == "true" ? true : false));

            string pageSizeStr = _context.Settings.FirstOrDefault(x => x.Key == "PageSize").Value;
            int pageSize = string.IsNullOrWhiteSpace(pageSizeStr) ? 3 : int.Parse(pageSizeStr);
            return View(PagenatedList<TypeProduct>.Create(type, page, pageSize));
        }
        //CREATE ACTION
        public IActionResult Create()
        {
            ViewBag.Categories = _context.Categories.ToList();

            return View();
        }
        // CREATE POST  ACTION------------------     
        [HttpPost]
        public IActionResult Create(TypeProduct typeProduct)
            {
                 ViewBag.Categories = _context.Categories.ToList();

            if (typeProduct.Name != null)
                {
                    if (typeProduct.Name.Length > 85)
                    {
                        ModelState.AddModelError("Name", "name cannot be longer than 85 characters!");
                        return View(typeProduct);
                    }
                }
                else
                {
                    ModelState.AddModelError("Name", "name cannot be null!");
                    return View(typeProduct);
                }

                typeProduct.CreatedAt = DateTime.UtcNow.AddHours(4);
                typeProduct.LastUpdateDate = DateTime.UtcNow.AddHours(4);
                _context.TypeProducts.Add(typeProduct);
                _context.SaveChanges();

                return RedirectToAction("index");
             }
        //EDIT ACTION
        public IActionResult Edit(int id)
        {
            TypeProduct typeProduct = _context.TypeProducts.Include(x=>x.Category).FirstOrDefault(x => x.Id == id);

            //ViewBag.Categories = _context.Categories.Where(x=>x.Id==id).ToList();

            if (typeProduct == null) return NotFound();

            return View(typeProduct);
        }

        [HttpPost]
        public IActionResult Edit(TypeProduct typeProduct)
        {
            TypeProduct existTypeProduct = _context.TypeProducts.Include(x=>x.Category).FirstOrDefault(x => x.Id == typeProduct.Id);
            if (existTypeProduct == null) return NotFound();

            if (typeProduct.Name != null)
            {
                if (typeProduct.Name.Length > 85)
                {
                    ModelState.AddModelError("Name", "type name cannot be longer than 85 characters!");
                    return View(existTypeProduct);
                }
            }
            else
            {
                ModelState.AddModelError("Name", "type name cannot be null!");
                return View(existTypeProduct);
            }


            existTypeProduct.Name = typeProduct.Name;
            existTypeProduct.LastUpdateDate = DateTime.UtcNow.AddHours(4);
            _context.SaveChanges();
            return RedirectToAction("index");
        }
        //DELETE ACTION
        public IActionResult Delete(int id)
        {
            TypeProduct existTypeProduct = _context.TypeProducts.FirstOrDefault(x => x.Id == id && !x.IsDeleted);

            if (existTypeProduct == null)
                return NotFound();

            ViewBag.Products = _context.Products.Where(x => x.IsDeleted).Where(x => x.TypeProductId == id).ToList();

            foreach (var product in ViewBag.Products)
            {
                product.IsDeleted = true;
            }

            existTypeProduct.IsDeleted = true;
            _context.SaveChanges();

            return RedirectToAction("index");
        }

        //RESTORE ACTION
        public IActionResult Restore(int id)
        {
            TypeProduct existTypeProduct = _context.TypeProducts.FirstOrDefault(x => x.Id == id && x.IsDeleted);

            if (existTypeProduct == null)
                return NotFound();

            ViewBag.Products = _context.Products.Where(x => x.IsDeleted).Where(x => x.TypeProductId == id).ToList();

            foreach (var product in ViewBag.Products)
            {
                product.IsDeleted = false;
            }

            existTypeProduct.IsDeleted = false;
            _context.SaveChanges();

            return RedirectToAction("index");
        }
    }
        
}