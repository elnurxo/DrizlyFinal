using DrizlyBackEnd.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DrizlyBackEnd.ViewModels
{
    public class HomeViewModel
    {
        public List<Settings> Settings { get; set; }
        public List<Partnership> Partnerships { get; set; }
        public List<Service> Services { get; set; }
        public List<Category> Categories { get; set; }
        public List<Product> OnSaleProducts { get; set; }
        public List<ProductComment> RecentComments { get; set; }
    }
}
