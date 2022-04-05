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
    [Area("manage")]
    public class EmployeeController : Controller
    {
        private readonly DrizlyContext _context;
        private readonly IWebHostEnvironment _env;

        public EmployeeController(DrizlyContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        //INDEX ACTION
        [Authorize(Roles = "SuperAdmin,Creator,Editor,Reader")]
        public IActionResult Index(string filter,int page = 1)
        {
            ViewBag.Filter = filter;

            var employees = _context.Employees.Include(x => x.Position).AsQueryable();

            if (filter != null)
                employees = employees.Where(x => x.IsDeleted == (filter == "true" ? true : false));

            string pageSizeStr = _context.Settings.FirstOrDefault(x => x.Key == "PageSize").Value;
            int pageSize = string.IsNullOrWhiteSpace(pageSizeStr) ? 3 : int.Parse(pageSizeStr);
            return View(PagenatedList<Employee>.Create( employees, page, pageSize));
        }

        //CREATE ACTION
        [Authorize(Roles = "SuperAdmin,Creator")]
        public IActionResult Create()
        {
            ViewBag.Positions = _context.Positions.ToList();

            return View();
        }
        [Authorize(Roles = "SuperAdmin,Creator")]
        [HttpPost]
        public IActionResult Create(Employee employee)
        {
            ViewBag.Positions = _context.Positions.ToList();

            if (!ModelState.IsValid)
                return View();

            if (employee.ImageFile == null)
            {
                ModelState.AddModelError("ImageFile", "Image file is required!");
                return View(employee);
            }

            if (employee.ImageFile.ContentType != "image/jpeg" && employee.ImageFile.ContentType != "image/png")
            {
                ModelState.AddModelError("ImageFile", "File type must be jpeg or png");
                return View(employee);
            }

            if (employee.ImageFile.Length > 4194304)
            {
                ModelState.AddModelError("ImageFile", "file size must be less than 4mb");
                return View(employee);
            }

            string fileName = employee.ImageFile.FileName;

            if (fileName.Length > 64)
            {
                fileName = fileName.Substring(employee.ImageFile.FileName.Length - 64, 64);
            }

            employee.Image = (Guid.NewGuid().ToString() + (fileName));

            string path = Path.Combine(_env.WebRootPath, "uploads/employees", employee.Image);

            using (FileStream stream = new FileStream(path, FileMode.Create))
            {
                employee.ImageFile.CopyTo(stream);
            }


            _context.Employees.Add(employee);
            _context.SaveChanges();
            TempData["Success"] = "Employee created successfully!";
            return RedirectToAction("index");
        }

        // EDIT ACTION
        [Authorize(Roles = "SuperAdmin,Editor")]
        public IActionResult Edit(int id)
        {
            Employee employee = _context.Employees.Include(x => x.Position).FirstOrDefault(x => x.Id == id);
            if (employee == null) return NotFound();

            ViewBag.Positions = _context.Positions.ToList();    

            return View(employee);
        }
        [Authorize(Roles = "SuperAdmin,Editor")]
        [HttpPost]
        public IActionResult Edit(Employee employee)
        {
            ViewBag.Positions = _context.Positions.ToList();

            Employee existEmployee = _context.Employees.Include(x => x.Position).FirstOrDefault(x => x.Id == employee.Id);

            if (existEmployee == null) return NotFound();

            if (!_context.Positions.Any(x => x.Id == employee.PositionId))
            {
                ModelState.AddModelError("PositionId", "Position not found");
                return View();
            }

            //IMAGE
            if (employee.ImageFile != null)
            {
                if (employee.ImageFile.ContentType != "image/jpeg" && employee.ImageFile.ContentType != "image/png")
                {
                    ModelState.AddModelError("ImageFile", "file type must be jpeg or png");
                    return View(existEmployee);
                }

                if (employee.ImageFile.Length > 4194304)
                {
                    ModelState.AddModelError("ImageFile", "file size must be less than 4mb");
                    return View(existEmployee);
                }

                string fileName = employee.ImageFile.FileName;

                if (fileName.Length > 64)
                {
                    fileName = fileName.Substring(employee.ImageFile.FileName.Length - 64, 64);
                }

                employee.Image = (Guid.NewGuid().ToString() + (fileName));

                string path = Path.Combine(_env.WebRootPath, "uploads/employees", employee.Image);

                using (FileStream stream = new FileStream(path, FileMode.Create))
                {
                    employee.ImageFile.CopyTo(stream);
                }

                if (existEmployee.Image != null)
                {
                    string existPath = Path.Combine(_env.WebRootPath, "uploads/employees", existEmployee.Image);
                    if (System.IO.File.Exists(existPath))
                        System.IO.File.Delete(existPath);
                }

                existEmployee.Image = employee.Image;
            }

            if (existEmployee == null) return NotFound();



            existEmployee.FullName = employee.FullName;
            existEmployee.PositionId = employee.PositionId;

            _context.SaveChanges();
            TempData["Success"] = "Employee updated successfully!";
            return RedirectToAction("index");
        }

        //DELETE ACTION
        [Authorize(Roles = "SuperAdmin,Editor")]
        public IActionResult Delete(int id)
        {
            Employee existEmployee = _context.Employees.FirstOrDefault(x => x.Id == id && !x.IsDeleted);

            if (existEmployee == null)
                return NotFound();


            existEmployee.IsDeleted = true;
            _context.SaveChanges();
            TempData["Success"] = "Employee deleted successfully!";
            return RedirectToAction("index");
        }

        //RESTORE ACTION
        [Authorize(Roles = "SuperAdmin,Editor")]
        public IActionResult Restore(int id)
        {
            Employee existEmployee = _context.Employees.FirstOrDefault(x => x.Id == id && x.IsDeleted);

            if (existEmployee == null)
                return NotFound();

            existEmployee.IsDeleted = false;
            _context.SaveChanges();
            TempData["Success"] = "Employee restored successfully!";
            return RedirectToAction("index");
        }

    }
}
