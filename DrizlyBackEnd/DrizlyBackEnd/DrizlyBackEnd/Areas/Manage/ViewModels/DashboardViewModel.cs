using DrizlyBackEnd.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DrizlyBackEnd.Areas.Manage.ViewModels
{
    public class DashboardViewModel
    {
        public List<AppUser> AppUsers { get; set; }
        public List<Order> TotalOrders { get; set; }
        public List<Order> ThisWeekOrders { get; set; }
        public List<Order> TillLastWreekOrders { get; set; }
        public List<WebSiteVisitor> TotalVisitors { get; set; }
        public List<WebSiteVisitor> WebSiteVisitorsLastWeek { get; set; }
        public List<WebSiteVisitor> WebSiteVisitorsthisweek { get; set; }
        public List<ContactUs> TotalMessages { get; set; }
        public List<ContactUs> ThisWeekMessages { get; set; }
        public List<ContactUs> LastWeekMessages { get; set; }
        public List<Product> Products { get; set; } 
        public List<OrderItem> CategoryTotalSale { get; set; }
        public List<Order> RecentOrders { get; set; }
        //MONTHLY VISITORS
        public List<WebSiteVisitor> JanuaryVisitors { get; set; }
        public List<WebSiteVisitor> FebruaryVisitors { get; set; }
        public List<WebSiteVisitor> MarchVisitors { get; set; }
        public List<WebSiteVisitor> AprilVisitors { get; set; }
        public List<WebSiteVisitor> MayVisitors { get; set; }
        public List<WebSiteVisitor> JuneVisitors { get; set; }
        public List<WebSiteVisitor> JulyVisitors { get; set; }
        public List<WebSiteVisitor> AugustVisitors { get; set; }
        public List<WebSiteVisitor> SeptemberVisitors { get; set; }
        public List<WebSiteVisitor> OctoberVisitors { get; set; }
        public List<WebSiteVisitor> NovemberVisitors { get; set; }
        public List<WebSiteVisitor> DecemberVisitors { get; set; }
        //END MONTLY VISITORS REPORT
        public List<Country> Countries { get; set; }
        //PRODUCT OF ALL TIME
        public Product MostPopularProduct { get; set; }
    }
}
