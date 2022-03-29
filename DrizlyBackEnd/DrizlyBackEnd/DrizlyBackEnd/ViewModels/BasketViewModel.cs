using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DrizlyBackEnd.ViewModels
{
    public class BasketViewModel
    {
        public List<ProductBasketItemViewModel> BasketItems { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
