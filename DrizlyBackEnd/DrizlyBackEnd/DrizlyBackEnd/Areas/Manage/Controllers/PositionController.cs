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
    //[Authorize(Roles = "SuperAdmin,Admin")]
    public class PositionController : Controller
    {
        private readonly DrizlyContext _context;

        public PositionController(DrizlyContext context)
        {
            _context = context;
        }
        //INDEX ACTION
        [Authorize(Roles = "SuperAdmin,Creator,Editor,Reader")]
        public IActionResult Index(int page = 1)
        {
            string pageSizeStr = _context.Settings.FirstOrDefault(x => x.Key == "PageSize").Value;
            int pageSize = string.IsNullOrWhiteSpace(pageSizeStr) ? 3 : int.Parse(pageSizeStr);
            return View(PagenatedList<Position>.Create(_context.Positions.Include(x=>x.Employees).AsQueryable(), page, pageSize));
        }
        //CREATE ACTION
        [Authorize(Roles = "SuperAdmin,Creator")]
        public IActionResult Create()
        {
            return View();
        }
        [Authorize(Roles = "SuperAdmin,Creator")]
        [HttpPost]
        public IActionResult Create(Position position)
        {

            if (!ModelState.IsValid)
                return View();

            if (position.Name.Length>75)
            {
                ModelState.AddModelError("Name", "name cannot be longer than 75 characters!");
                return View(position);
            }


            _context.Positions.Add(position);
            _context.SaveChanges();
            TempData["Success"] = "Position created successfully!";
            return RedirectToAction("index");
        }

        //EDIT ACTION
        [Authorize(Roles = "SuperAdmin,Editor")]
        public IActionResult Edit(int id)
        {
            Position position = _context.Positions.FirstOrDefault(x => x.Id == id);

            if (position == null) return NotFound();

            return View(position);
        }

        [Authorize(Roles = "SuperAdmin,Editor")]
        [HttpPost]
        public IActionResult Edit(Position position)
        {
            if (!ModelState.IsValid)
                return View();

            Position existPosition = _context.Positions.FirstOrDefault(x => x.Id == position.Id);
            if (existPosition == null) return NotFound();

            existPosition.Name = position.Name;

            _context.SaveChanges();
            TempData["Success"] = "Position updated successfully!";
            return RedirectToAction("index");
        }

        //DELETE ACTION
        [Authorize(Roles = "SuperAdmin,Editor")]
        public IActionResult Delete(int id)
        {
            Position existPosition = _context.Positions.FirstOrDefault(x => x.Id == id);

            if (existPosition == null)
                return NotFound();

            _context.Positions.Remove(existPosition);

            _context.SaveChanges();
            TempData["Success"] = "Position deleted successfully!";
            return RedirectToAction("index");
        }
    }
}
