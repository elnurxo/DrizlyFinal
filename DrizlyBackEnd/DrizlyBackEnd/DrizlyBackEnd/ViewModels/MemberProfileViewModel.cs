using DrizlyBackEnd.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DrizlyBackEnd.ViewModels
{
    public class MemberProfileViewModel
    {
        public MemberUpdateViewModel Member { get; set; }
        public List<Order> Orders { get; set; }
        public List<AppUserCoupon> AppUserCoupons { get; set; }
        public List<CouponCategory> CouponCategories { get; set; }
    }
}
