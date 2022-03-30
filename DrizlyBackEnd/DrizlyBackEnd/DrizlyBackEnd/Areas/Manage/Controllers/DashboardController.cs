using DrizlyBackEnd.Areas.Manage.ViewModels;
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
    //[Authorize(Roles = "SuperAdmin,Admin")]
    public class DashboardController : Controller
    {
        private readonly DrizlyContext _context;
        public DashboardController(DrizlyContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {

            DashboardViewModel dashboardVM = new DashboardViewModel
            {
                AppUsers = _context.AppUsers.ToList()
            };
            return View(dashboardVM);
        }
    }
}
