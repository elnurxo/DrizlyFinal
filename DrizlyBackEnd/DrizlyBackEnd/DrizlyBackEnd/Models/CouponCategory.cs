using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DrizlyBackEnd.Models
{
    public class CouponCategory
    {
        public int Id { get; set; }
        [Required]
        [StringLength(maximumLength: 50)]
        public string Name { get; set; }
        [Required]
        public int DiscountPercent { get; set; }
        [Required]
        public int SaleValue { get; set; }
        public bool IsDeleted { get; set; }
    }
}
