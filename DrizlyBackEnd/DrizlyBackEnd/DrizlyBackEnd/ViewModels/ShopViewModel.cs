using DrizlyBackEnd.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DrizlyBackEnd.ViewModels
{
    public class ShopViewModel
    {
        public List<Settings> Settings { get; set; }
        public List<Product> Products { get; set; }
        public List<ProductComment> Comment { get; set; }
        public List<Country> Countries { get; set; }
        public List<TypeProduct> TypeProducts { get; set; }
        public Category Category { get; set; }
        public List<Brand> Brands { get; set; }
        public decimal MaxPrice { get; set; }
        public decimal MinPrice { get; set; }
        public decimal? MaxAbv { get; set; }
        public decimal? MinAbv { get; set; }
    }
}
