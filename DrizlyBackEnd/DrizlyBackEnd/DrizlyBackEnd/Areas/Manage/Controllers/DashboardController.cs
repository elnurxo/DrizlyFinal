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
                TotalOrders = _context.Orders.ToList(),
                TillLastWreekOrders = _context.Orders.Where(x=>x.CreatedAt < DateTime.UtcNow.AddHours(4).AddDays(-7)).ToList(),
                ThisWeekOrders = _context.Orders.Where(x=>x.CreatedAt > DateTime.UtcNow.AddHours(4).AddDays(-7)).ToList(),
                TotalVisitors = _context.WebSiteVisitors.ToList(),
                WebSiteVisitorsLastWeek = _context.WebSiteVisitors.Where(x=>x.VisitDate < DateTime.UtcNow.AddHours(4).AddDays(-7)).ToList(),
                WebSiteVisitorsthisweek = _context.WebSiteVisitors.Where(x => x.VisitDate > DateTime.UtcNow.AddHours(4).AddDays(-7)).ToList(),
                TotalMessages = _context.ContactUs.ToList(),
                LastWeekMessages = _context.ContactUs.Where(x => x.CreatedAt < DateTime.UtcNow.AddHours(4).AddDays(-7)).ToList(),
                ThisWeekMessages = _context.ContactUs.Where(x => x.CreatedAt > DateTime.UtcNow.AddHours(4).AddDays(-7)).ToList(),
            };
            return View(dashboardVM);
        }
    }
}
