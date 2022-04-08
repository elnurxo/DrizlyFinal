using DrizlyBackEnd.Areas.Manage.ViewModels;
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
                Products = _context.Products.Where(x => x.IsDeleted == false).ToList(),
                CategoryTotalSale = _context.OrderItems.Include(x=>x.Product).ToList(),
                RecentOrders = _context.Orders.OrderByDescending(x=>x.CreatedAt).Include(x=>x.OrderItems).Take(7).ToList(),
                //WEBSITE VISITORS BY MONTHS
                JanuaryVisitors = _context.WebSiteVisitors.Where(x=>x.VisitDate.Month==1).ToList(),
                FebruaryVisitors = _context.WebSiteVisitors.Where(x=>x.VisitDate.Month==2).ToList(),
                MarchVisitors = _context.WebSiteVisitors.Where(x=>x.VisitDate.Month==3).ToList(),
                AprilVisitors = _context.WebSiteVisitors.Where(x => x.VisitDate.Month == 4).ToList(),
                MayVisitors = _context.WebSiteVisitors.Where(x=>x.VisitDate.Month==5).ToList(),
                JuneVisitors = _context.WebSiteVisitors.Where(x=>x.VisitDate.Month==6).ToList(),
                JulyVisitors = _context.WebSiteVisitors.Where(x=>x.VisitDate.Month==7).ToList(),
                AugustVisitors = _context.WebSiteVisitors.Where(x=>x.VisitDate.Month==8).ToList(),
                SeptemberVisitors = _context.WebSiteVisitors.Where(x=>x.VisitDate.Month==9).ToList(),
                OctoberVisitors = _context.WebSiteVisitors.Where(x=>x.VisitDate.Month==10).ToList(),
                NovemberVisitors = _context.WebSiteVisitors.Where(x=>x.VisitDate.Month==11).ToList(),
                DecemberVisitors = _context.WebSiteVisitors.Where(x=>x.VisitDate.Month==12).ToList(),      

            };
            return View(dashboardVM);
        }
    }
}
