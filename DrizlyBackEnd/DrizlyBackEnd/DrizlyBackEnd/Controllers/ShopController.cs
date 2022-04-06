using DrizlyBackEnd.Models;
using DrizlyBackEnd.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
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
            if (id == 0 || id>4)
            {
                return PartialView("_ErrorPagePartialView");
            }

            var products = _context.Products
            .Where(x=>x.CategoryId==id)
            .Include(x => x.Brand)
            .Include(x=>x.Country)
            .Include(x => x.ProductComments).ThenInclude(c => c.AppUser)
            .Include(x => x.TypeProduct)
            .ThenInclude(x=>x.Category)
            .Where(x => !x.IsDeleted);

            ViewBag.CategoryId = id;
            ViewBag.BrandId = brandId;
            ViewBag.TypeId = typeId;
            ViewBag.CountryId = countryId;

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
                productVM.MinAbv = products.Min(x => x.Abv);
                productVM.MaxAbv = products.Max(x => x.Abv);
            }

            ViewBag.FilterMinPrice = minPrice ?? productVM.MinPrice;
            ViewBag.FilterMaxPrice = maxPrice ?? productVM.MaxPrice;
            ViewBag.FilterMinAbv = minAbv ?? productVM.MinAbv;
            ViewBag.FilterMaxAbv = maxAbv ?? productVM.MaxAbv;

            if (minPrice != null && maxPrice != null)
                products = products.Where(x => x.SalePrice >= minPrice && x.SalePrice <= maxPrice);
            if (minAbv != null && maxAbv != null)
                products = products.Where(x => x.Abv >= minAbv && x.Abv <= maxAbv);


            ViewBag.TotalProducts = products.Count();
            productVM.Settings = _context.Settings.ToList();
            //PRODUCTS
            productVM.Products = _context.Products.Where(x=>!x.IsDeleted && x.CategoryId==id).Include(x=>x.Brand).Include(x=>x.Country).Include(x=>x.ProductComments).ToList();
            //BRANDS
            productVM.Brands = _context.Brands.Where(x => !x.IsDeleted).Include(x => x.Products).ToList();
            //COUNTRIES
            productVM.Countries = _context.Countries.Include(x => x.Products).ToList();
            productVM.TypeProducts = _context.TypeProducts.Where(x => !x.IsDeleted).Where(x=>x.CategoryId==id).Include(x => x.Products).Include(x=>x.Category).ToList();
            productVM.Category = _context.Categories.FirstOrDefault(x => x.Id == id);
            productVM.Comment = _context.ProductComments.ToList();


            return View(productVM);
        }

        //SHOP ACTION
        public IActionResult Shop(int id, string sort,int? brandId, int? typeId, int? countryId, decimal? minPrice, decimal? maxPrice, decimal? minAbv, decimal? maxAbv,int page=1)
        {
            if (id == 0 || id > 4)
            {
                return PartialView("_ErrorPagePartialView");
            }

            var products = _context.Products
            .Where(x => x.CategoryId == id)
            .Include(x => x.Brand)
            .Include(x => x.Country)
            .Include(x => x.ProductComments).ThenInclude(c => c.AppUser)
            .Include(x => x.TypeProduct)
            .ThenInclude(x => x.Category)
            .Where(x => !x.IsDeleted);

            ViewBag.CategoryId = id;
            ViewBag.BrandId = brandId;
            ViewBag.TypeId = typeId;
            ViewBag.CountryId = countryId;
            ViewBag.PageIndex = page;

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
                products = products.Where(x => x.CountryId == countryId);
            }
            if (typeId != null)
            {
                products = products.Where(x => x.TypeProductId == typeId);
            }
            if (products.Any())
            {
                productVM.MinPrice = products.Min(x => x.SalePrice);
                productVM.MaxPrice = products.Max(x => x.SalePrice);
                productVM.MinAbv = products.Min(x => x.Abv);
                productVM.MaxAbv = products.Max(x => x.Abv);
            }

            ViewBag.FilterMinPrice = minPrice ?? productVM.MinPrice;
            ViewBag.FilterMaxPrice = maxPrice ?? productVM.MaxPrice;
            ViewBag.FilterMinAbv = minAbv ?? productVM.MinAbv;
            ViewBag.FilterMaxAbv = maxAbv ?? productVM.MaxAbv;

            if (minPrice != null && maxPrice != null)
                products = products.Where(x => x.SalePrice >= minPrice && x.SalePrice <= maxPrice);
            if (minAbv != null && maxAbv != null)
                products = products.Where(x => x.Abv >= minAbv && x.Abv <= maxAbv);

            ViewBag.TotalPages = (int)Math.Ceiling(products.Count() / 6d);
            ViewBag.TotalProducts = products.Count();
            productVM.Settings = _context.Settings.ToList();
    
            //BRANDS
            productVM.Brands = _context.Brands.Where(x => !x.IsDeleted).Include(x => x.Products).ToList();
            //COUNTRIES
            productVM.Countries = _context.Countries.Include(x => x.Products).ToList();
            productVM.TypeProducts = _context.TypeProducts.Where(x => !x.IsDeleted).Where(x => x.CategoryId == id).Include(x => x.Products).Include(x => x.Category).ToList();
            productVM.Category = _context.Categories.FirstOrDefault(x => x.Id == id);
            productVM.Comment = _context.ProductComments.ToList();

            ViewBag.Sort = sort;

            switch (sort)
            {
                case "price_high":
                   
                    products = products.OrderBy(x=>x.SalePrice);
                    break;
                case "price_low":
                    products = products.OrderByDescending(x => x.SalePrice);
                    break;
                case "name_asc":
                    products = products.OrderBy(x => x.Name);
                    break;
                case "name_desc":
                    products = products.OrderByDescending(x => x.Name);
                    break;
                case "rate_desc":
                    products = products.OrderByDescending(x => x.Rate);
                    break;
                case "rate_asc":
                    products = products.OrderBy(x => x.Rate);
                    break;
                default:
                    break;
            }

            //PRODUCTS
            var resultproducts =  products.Skip((page - 1) * 6).Take(6).AsQueryable();

            productVM.Products = resultproducts.ToList();

            return View(productVM);
        }

        //DETAIL ACTION
        public IActionResult Detail(int id, int page = 1)
        {
            Product product = _context.Products
                .Include(x => x.Brand).Include(x => x.Country).Include(x => x.ProductCount).Include(x => x.ProductSize)
                .Include(x => x.sweetDryScale).Include(x => x.ProductFoodPairings).ThenInclude(x=>x.WineFoodPairing).Include(x => x.LiquorColor).Include(x => x.LiquorFlavor)
                .Include(x => x.TypeProduct).ThenInclude(x => x.Category)
                .Include(x => x.ProductComments).ThenInclude(c => c.AppUser)
                .FirstOrDefault(x => x.Id == id && !x.IsDeleted);   
            if (product == null) return PartialView("_ErrorPagePartialView");

            string pageSizeStr = _context.Settings.FirstOrDefault(x => x.Key == "PageSize").Value;
            int pageSize = string.IsNullOrWhiteSpace(pageSizeStr) ? 3 : int.Parse(pageSizeStr);

            var productcomments = _context.ProductComments.Where(x=>x.ProductId==id).AsQueryable();

            ProductDetailViewModel productDetailVM = new ProductDetailViewModel
            {
                Product = product,
                Comment = new ProductComment { ProductId = id },
                Comments = PagenatedList<ProductComment>.Create(productcomments, page, pageSize)
            };

            return View(productDetailVM);
        }

        //COMMENT ACTION
        [HttpPost]
        [Authorize(Roles = "Member")]
        public IActionResult Comment(ProductComment comment)
        {

            AppUser member = null;
            if (User.Identity.IsAuthenticated)
            {
                member = _userManager.Users.FirstOrDefault(x => x.UserName == User.Identity.Name && !x.IsAdmin);
            }
            if (member == null)
                return RedirectToAction("login", "account");

            Product product = _context.Products
               .Include(x => x.Brand).Include(x => x.Country).Include(x => x.ProductCount).Include(x => x.ProductSize)
               .Include(x => x.sweetDryScale).Include(x => x.ProductFoodPairings).ThenInclude(x => x.WineFoodPairing).Include(x => x.LiquorColor).Include(x => x.LiquorFlavor)
                .Include(x => x.TypeProduct).ThenInclude(x => x.Category)
               .Include(x => x.ProductComments).ThenInclude(c => c.AppUser)
               .FirstOrDefault(x => x.Id == comment.ProductId && !x.IsDeleted);

            if (product == null) return PartialView("_ErrorPagePartialView"); ;

            if (!ModelState.IsValid)
            {

                ProductDetailViewModel productDetailVM = new ProductDetailViewModel
                {
                    Product = product,
                    Comment = comment,
                };

                return View("Detail", productDetailVM);
            }

            comment.AppUserId = member.Id;

            comment.CreatedAt = DateTime.UtcNow.AddHours(4);
            product.ProductComments.Add(comment);
            product.Rate = (int)Math.Ceiling(product.ProductComments.Sum(x => x.Rate) / (double)product.ProductComments.Count);


            List<ProductComment> existComments = _context.ProductComments.Include(x=>x.AppUser).Include(x=>x.Product).ToList();
            foreach (var existcomment in existComments)
            {
                if (existcomment.ProductId == comment.ProductId && existcomment.AppUserId == comment.AppUserId)
                {
                    ModelState.AddModelError("", "One comment for one product");
                    TempData["Warning"] = "You can only share comments once for a product!";
                    return RedirectToAction("detail", new { id = product.Id });
                }
            }

            _context.SaveChanges();
            TempData["Success"] = "Comment posted successfully!";
            return RedirectToAction("detail", new { id = comment.ProductId });
        }

        // ADD BASKET
        public IActionResult AddBasket(int id, int page = 1)
        {
            if (!_context.Products.Any(x => x.Id == id && !x.IsDeleted))
                return PartialView("_ErrorPagePartialView");


            Product product = _context.Products
                .Include(x => x.Brand).Include(x => x.Country).Include(x => x.ProductCount).Include(x => x.ProductSize)
                .Include(x => x.sweetDryScale).Include(x => x.ProductFoodPairings).ThenInclude(x => x.WineFoodPairing).Include(x => x.LiquorColor).Include(x => x.LiquorFlavor)
                .Include(x => x.TypeProduct).ThenInclude(x => x.Category)
                .Include(x => x.ProductComments).ThenInclude(c => c.AppUser)
                .FirstOrDefault(x => x.Id == id && !x.IsDeleted);
            if (product == null) return PartialView("_ErrorPagePartialView");

            if (product.IsAvailable==false)
            {
                TempData["Warning"] = "This product not available right now";           
                return RedirectToAction("detail","shop", new { id = product.Id });
            }

            AppUser member = null;
            if (User.Identity.IsAuthenticated)
            {
                member = _userManager.Users.FirstOrDefault(x => x.UserName == User.Identity.Name && !x.IsAdmin);
            }

            if (member == null)
            {
                string basketItemsStr = HttpContext.Request.Cookies["basket"];
                List<BasketItemViewModel> items = new List<BasketItemViewModel>();

                if (!string.IsNullOrWhiteSpace(basketItemsStr))
                    items = JsonConvert.DeserializeObject<List<BasketItemViewModel>>(basketItemsStr);

                BasketItemViewModel item = items.FirstOrDefault(x => x.ProductId == id);

                if (item == null)
                {
                    item = new BasketItemViewModel { ProductId = id, Count = 1 };
                    items.Add(item);
                }
                else
                    item.Count++;

                basketItemsStr = JsonConvert.SerializeObject(items);
                TempData["Success"] = "Added to basket successfully!";
                HttpContext.Response.Cookies.Append("basket", basketItemsStr);
                return PartialView("_BasketPartialView", _getBasket(items));
            }
            else
            {
                BasketItem item = _context.BasketItems.FirstOrDefault(x => x.AppUserId == member.Id && x.ProductId == id);

                if (item == null)
                {
                    item = new BasketItem
                    {
                        AppUserId = member.Id,
                        ProductId = id,
                        CreatedAt = DateTime.UtcNow.AddHours(4),
                        Count = 1
                    };
                    _context.BasketItems.Add(item);
                }
                else
                {
                    item.Count++;
                }

                TempData["Success"] = "Added to basket successfully!";
                _context.SaveChanges();

                var items = _context.BasketItems.Where(x => x.AppUserId == member.Id).ToList();
                return PartialView("_BasketPartialView", _getBasket(items));
            }
        }

        //REMOVE BASKET
        public IActionResult RemoveBasket(int id)
        {
            if (!_context.Products.Any(x => x.Id == id && !x.IsDeleted))
                return PartialView("_ErrorPagePartialView");

            AppUser member = null;
            if (User.Identity.IsAuthenticated)
            {
                member = _userManager.Users.FirstOrDefault(x => x.UserName == User.Identity.Name && !x.IsAdmin);
            }

            if (member == null)
            {
                string basketItemsStr = HttpContext.Request.Cookies["basket"];
                List<BasketItemViewModel> items = new List<BasketItemViewModel>();

                if (!string.IsNullOrWhiteSpace(basketItemsStr))
                    items = JsonConvert.DeserializeObject<List<BasketItemViewModel>>(basketItemsStr);

                BasketItemViewModel item = items.FirstOrDefault(x => x.ProductId == id);

                if (item.Count > 0)
                {
                    item.Count--;
                    if (item.Count == 0)
                    {
                        items.Remove(item);
                    }
                }
                else
                    items.Remove(item);

                basketItemsStr = JsonConvert.SerializeObject(items);

                HttpContext.Response.Cookies.Append("basket", basketItemsStr);
                TempData["Warning"] = "Removed from basket successfully!";
                return PartialView("_BasketPartialView", _getBasket(items));
            }
            else
            {
                BasketItem item = _context.BasketItems.FirstOrDefault(x => x.AppUserId == member.Id && x.ProductId == id);

                if (item.Count > 0)
                {
                    item.Count--;
                    if (item.Count == 0)
                    {
                        _context.BasketItems.Remove(item);
                    }
                }
                else
                    _context.BasketItems.Remove(item);

                _context.SaveChanges();

                var items = _context.BasketItems.Where(x => x.AppUserId == member.Id).ToList();
                TempData["Warning"] = "Removed from basket successfully!";
                return PartialView("_BasketPartialView", _getBasket(items));
            }
        }

        //REMOVE ALL ONE KINDA PRODUCT
        public IActionResult RemoveAllBasket(int id)
        {
            if (!_context.Products.Any(x => x.Id == id && !x.IsDeleted))
                return PartialView("_ErrorPagePartialView");

            AppUser member = null;
            if (User.Identity.IsAuthenticated)
            {
                member = _userManager.Users.FirstOrDefault(x => x.UserName == User.Identity.Name && !x.IsAdmin);
            }

            if (member == null)
            {
                string basketItemsStr = HttpContext.Request.Cookies["basket"];
                List<BasketItemViewModel> items = new List<BasketItemViewModel>();

                if (!string.IsNullOrWhiteSpace(basketItemsStr))
                    items = JsonConvert.DeserializeObject<List<BasketItemViewModel>>(basketItemsStr);

                BasketItemViewModel item = items.FirstOrDefault(x => x.ProductId == id);

                items.Remove(item);

                basketItemsStr = JsonConvert.SerializeObject(items);

                HttpContext.Response.Cookies.Append("basket", basketItemsStr);
                TempData["Warning"] = "Removed from basket successfully!";
                return PartialView("_BasketViewPartialView", _getBasket(items));
            }
            else
            {
                BasketItem item = _context.BasketItems.FirstOrDefault(x => x.AppUserId == member.Id && x.ProductId == id);


                _context.BasketItems.Remove(item);

                _context.SaveChanges();

                var items = _context.BasketItems.Where(x => x.AppUserId == member.Id).ToList();
                TempData["Warning"] = "Removed from basket successfully!";
                return PartialView("_BasketViewPartialView", _getBasket(items));
            }
        }

        // GET BASKET PRIVATE 
        private BasketViewModel _getBasket(List<BasketItemViewModel> basketItems)
        {
            BasketViewModel basketVM = new BasketViewModel
            {
                BasketItems = new List<ProductBasketItemViewModel>(),
                TotalPrice = 0
            };

            foreach (var item in basketItems)
            {
                    Product product = _context.Products.Where(x=>x.IsAvailable==true && !x.IsDeleted).FirstOrDefault(x => x.Id == item.ProductId);
                    ProductBasketItemViewModel productBasketItem = new ProductBasketItemViewModel
                    {
                        Product = product,
                        Count = item.Count
                    };

                if (product!=null)
                {
                    basketVM.BasketItems.Add(productBasketItem);
                    decimal totalPrice = product.DiscountPercent > 0 ? (product.SalePrice * (1 - product.DiscountPercent / 100)) : product.SalePrice;
                    basketVM.TotalPrice += totalPrice * item.Count;
                }
            }

            return basketVM;
        }

        private BasketViewModel _getBasket(List<BasketItem> basketItems)
        {
            BasketViewModel basketVM = new BasketViewModel
            {
                BasketItems = new List<ProductBasketItemViewModel>(),
                TotalPrice = 0
            };

            foreach (var item in basketItems)
            {
                Product product = _context.Products.Where(x => x.IsAvailable == true && !x.IsDeleted).FirstOrDefault(x => x.Id == item.ProductId);
                ProductBasketItemViewModel productBasketItem = new ProductBasketItemViewModel
                {
                    Product = product,
                    Count = item.Count
                };

                if (product!=null)
                {
                    basketVM.BasketItems.Add(productBasketItem);
                    decimal totalPrice = product.DiscountPercent > 0 ? (product.SalePrice * (1 - product.DiscountPercent / 100)) : product.SalePrice;
                    basketVM.TotalPrice += totalPrice * item.Count;
                }
            }

            return basketVM;
        }
    }
}
