using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DrizlyBackEnd.Models
{
    public class ProductFoodPairing
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int WineFoodPairingId { get; set; }
        public Product Product { get; set; }
        public WineFoodPairing WineFoodPairing { get; set; }
    }
}
