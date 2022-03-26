using DrizlyBackEnd.Models;
using DrizlyBackEnd.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DrizlyBackEnd.Controllers
{
    public class ShopController : Controller
    {
        private readonly DrizlyContext _context;
        private readonly UserManager<AppUser> _userManager;
        public ShopController(DrizlyContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        //INDEX ACTION
        public IActionResult Index(int id,int? brandId,int? typeId,int? countryId, decimal? minPrice, decimal? maxPrice, decimal? minAbv, decimal? maxAbv)
        {
            var products = _context.Products
            .Where(x=>x.CategoryId==id)
            .Include(x => x.Brand)
            .Include(x=>x.Country)
            .Include(x => x.TypeProduct)
            .ThenInclude(x=>x.Category)
            .Where(x => !x.IsDeleted).Where(x => x.IsAvailable);

            ViewBag.CategoryId = id;
            ViewBag.BrandId = brandId;
            ViewBag.TypeId = typeId;
            ViewBag.CountryId = countryId;
            ViewBag.TotalProducts = products.Count();

            ShopViewModel productVM = new ShopViewModel();

            products = products.Where(x => x.CategoryId == id);

            if (id > 4 || id < 0)
            {
                return BadRequest();
            }

            if (brandId != null)
            {
                products = products.Where(x => x.BrandId == brandId);
            }
            if (countryId != null)
            {
                products = products.Where(x => x.CategoryId == id);
            }
            if (typeId != null)
            {
                products = products.Where(x => x.TypeProductId == typeId);
            }
            if (products.Any())
            {
                productVM.MinPrice = products.Min(x => x.SalePrice);
                productVM.MaxPrice = products.Max(x => x.SalePrice);
                productVM.MinPAbv = products.Min(x => x.Abv);
                productVM.MaxAbv = products.Max(x => x.Abv);
            }

            ViewBag.FilterMinPrice = minPrice ?? productVM.MinPrice;
            ViewBag.FilterMaxPrice = maxPrice ?? productVM.MaxPrice;
            ViewBag.FilterMinAbv = minAbv ?? productVM.MinPAbv;
            ViewBag.FilterMaxAbv = maxAbv ?? productVM.MaxAbv;

            if (minPrice != null && maxPrice != null)
                products = products.Where(x => x.SalePrice >= minPrice && x.SalePrice <= maxPrice);
            if (minAbv != null && maxAbv != null)
                products = products.Where(x => x.Abv >= minAbv && x.Abv <= maxAbv);

            productVM.Settings = _context.Settings.ToList();
            productVM.Products = _context.Products.Where(x=>!x.IsDeleted && x.CategoryId==id).Include(x=>x.Brand).Include(x=>x.Country).ToList();
            productVM.Brands = _context.Brands.Where(x => !x.IsDeleted).Include(x => x.Products).ToList();
            productVM.Countries = _context.Countries.Include(x => x.Products).ToList();
            productVM.TypeProducts = _context.TypeProducts.Where(x => !x.IsDeleted).Where(x=>x.CategoryId==id).Include(x => x.Products).Include(x=>x.Category).ToList();
            productVM.Category = _context.Categories.FirstOrDefault(x => x.Id == id);

            return View(productVM);
        }
        //DETAIL ACTION
        public IActionResult Detail(int id)
        {
            Product product = _context.Products
                .Include(x => x.Brand).Include(x => x.Country).Include(x => x.ProductCount).Include(x => x.ProductSize)
                .Include(x=>x.sweetDryScale).Include(x=>x.WineFoodPairing).Include(x=>x.LiquorColor).Include(x=>x.LiquorFlavor)
                .Include(x => x.TypeProduct).ThenInclude(x => x.Category)
                .FirstOrDefault(x => x.Id == id && !x.IsDeleted);
            if (product == null) return NotFound();

            ProductDetailViewModel productDetailVM = new ProductDetailViewModel
            {
                Product = product,
            };

            return View(productDetailVM);
        }
    }
}
