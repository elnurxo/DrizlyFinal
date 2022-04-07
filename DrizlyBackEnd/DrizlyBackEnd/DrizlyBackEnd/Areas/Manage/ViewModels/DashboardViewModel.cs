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
        public List<Order> YesterdayOrders { get; set; }
    }
}
