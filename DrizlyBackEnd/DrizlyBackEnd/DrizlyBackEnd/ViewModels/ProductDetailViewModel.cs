using DrizlyBackEnd.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DrizlyBackEnd.ViewModels
{
    public class ProductDetailViewModel
    {
        public Product Product { get; set; }
        public ProductComment Comment { get; set; }
    }
}
