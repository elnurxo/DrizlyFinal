using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DrizlyBackEnd.Models
{
    public class AppUserCoupon
    {
        public int Id { get; set; }
        [Required]
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }
        [Required]
        [StringLength(maximumLength: 36)]
        public string Coupon { get; set; }
        public bool IsUsed { get; set; }
        public DateTime CreatedAt { get; set; }
        public int CouponCategoryId { get; set; }
        public CouponCategory CouponCategory { get; set; }
    }
}
