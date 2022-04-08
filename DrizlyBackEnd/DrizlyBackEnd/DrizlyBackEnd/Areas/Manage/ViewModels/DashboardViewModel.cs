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

    }
}
