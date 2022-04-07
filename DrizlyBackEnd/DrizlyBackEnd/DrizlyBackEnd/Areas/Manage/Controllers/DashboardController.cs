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
    [Authorize(Roles = "SuperAdmin,Reader,Creator,Editor")]
    public class DashboardController : Controller
    {
        private readonly DrizlyContext _context;
        public DashboardController(DrizlyContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {

            //var yesterdayOrders = _context.Orders.Where(x => x.CreatedAt >= DateTime.UtcNow.AddHours(4).AddDays(-1) );
            //var todayOrders = _context.Orders.Where(x => x.CreatedAt < DateTime.UtcNow.AddHours(4)); 



            DashboardViewModel dashboardVM = new DashboardViewModel
            {
                AppUsers = _context.AppUsers.ToList(),
                YesterdayOrders = _context.Orders.Where(x=>x.CreatedAt >= DateTime.UtcNow.AddHours(4).AddDays(-2) && x.CreatedAt <= DateTime.UtcNow.AddHours(4).AddDays(-1)).ToList(),
            };
            return View(dashboardVM);
        }
    }
}
