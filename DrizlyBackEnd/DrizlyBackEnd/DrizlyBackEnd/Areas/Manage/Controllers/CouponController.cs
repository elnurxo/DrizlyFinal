using DrizlyBackEnd.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DrizlyBackEnd.Areas.Manage.Controllers
{
    [Area("manage")]
    //[Authorize(Roles = "SuperAdmin")]
    public class CouponController : Controller
    {
        private readonly DrizlyContext _context;

        public CouponController(DrizlyContext context)
        {
            _context = context;
        }

        //INDEX ACTION
        [Authorize(Roles = "SuperAdmin,Creator,Editor,Reader")]
        public IActionResult Index(int page = 1)
        {
            var coupons = _context.CouponCategories.AsQueryable();

            string pageSizeStr = _context.Settings.FirstOrDefault(x => x.Key == "PageSize").Value;
            int pageSize = string.IsNullOrWhiteSpace(pageSizeStr) ? 3 : int.Parse(pageSizeStr);
            return View(PagenatedList<CouponCategory>.Create(coupons, page, pageSize));
        }

        //CREATE ACTION
        [Authorize(Roles = "SuperAdmin,Creator")]
        public IActionResult Create()
        {
            return View();
        }
        [Authorize(Roles = "SuperAdmin,Creator")]
        [HttpPost]
        public IActionResult Create(CouponCategory coupon)
        {
            if (!ModelState.IsValid)
                return View();

            if (coupon.Name == null)
                ModelState.AddModelError("Name", "Name is required!");
            if (coupon.DiscountPercent == 0)
                ModelState.AddModelError("DiscountPercent", "Discount Percent is required!");
            if (coupon.DiscountPercent<0 || coupon.DiscountPercent>100)
            {
                ModelState.AddModelError("DiscountPercent", "coupon discount percent not valid!");
            }
            if (coupon.SaleValue == 0)
                ModelState.AddModelError("SaleValue", "SaleValue is required!");



            _context.CouponCategories.Add(coupon);
            _context.SaveChanges();

            return RedirectToAction("index");
        }
        //DELETE ACTION
        [Authorize(Roles = "SuperAdmin,Editor")]
        public IActionResult Delete(int id)
        {
            CouponCategory existCoupon = _context.CouponCategories.FirstOrDefault(x => x.Id == id && !x.IsDeleted);

            if (existCoupon == null)
                return NotFound();

            existCoupon.IsDeleted = true;
            _context.SaveChanges();

            return RedirectToAction("index");
        }

        //RESTORE ACTION
        [Authorize(Roles = "SuperAdmin,Editor")]
        public IActionResult Restore(int id)
        {
            CouponCategory existCoupon = _context.CouponCategories.FirstOrDefault(x => x.Id == id && x.IsDeleted);

            if (existCoupon == null)
                return NotFound();

            existCoupon.IsDeleted = false;
            _context.SaveChanges();

            return RedirectToAction("index");
        }

        //EDIT ACTION
        [Authorize(Roles = "SuperAdmin,Editor")]
        public IActionResult Edit(int id)
        {
            CouponCategory coupon = _context.CouponCategories.FirstOrDefault(x => x.Id == id);

            if (coupon == null) return NotFound();

            return View(coupon);
        }
        [Authorize(Roles = "SuperAdmin,Editor")]
        [HttpPost]
        public IActionResult Edit(CouponCategory coupon)
        {
            CouponCategory existCoupon = _context.CouponCategories.FirstOrDefault(x => x.Id == coupon.Id);
            if (existCoupon == null) return NotFound();

            if (coupon.Name != null)
            {
                if (coupon.Name.Length > 50)
                {
                    ModelState.AddModelError("Name", "coupon name cannot be longer than 50 characters!");
                    return View(existCoupon);
                }
            }
            else
            {
                ModelState.AddModelError("Name", "coupon name cannot be null!");
                return View(existCoupon);
            }
            if (coupon.DiscountPercent<0 || coupon.DiscountPercent>100)
            {
                ModelState.AddModelError("DiscountPercent", "coupon discount percent not valid!");
                return View(existCoupon);
            }
            if (coupon.DiscountPercent==0)
            {
                ModelState.AddModelError("DiscountPercent", "coupon discount percent cannot be 0!");
                return View(existCoupon);
            }
            if (coupon.SaleValue == 0)
            {
                ModelState.AddModelError("SaleValue", "coupon sale value cannot be 0!");
                return View(existCoupon);
            }

            existCoupon.Name = coupon.Name;
            existCoupon.DiscountPercent = coupon.DiscountPercent;
            existCoupon.SaleValue = coupon.SaleValue;
            _context.SaveChanges();
            return RedirectToAction("index");
        }
    }
}
