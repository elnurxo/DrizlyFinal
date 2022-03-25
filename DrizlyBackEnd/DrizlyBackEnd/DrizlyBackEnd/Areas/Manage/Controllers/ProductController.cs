using DrizlyBackEnd.Helpers;
using DrizlyBackEnd.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DrizlyBackEnd.Areas.Manage.Controllers
{
    [Area("manage")]
    //[Authorize(Roles = "SuperAdmin")]
    public class ProductController : Controller
    {
        private readonly DrizlyContext _context;
        private readonly IWebHostEnvironment _env;

        public ProductController(DrizlyContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        //INDEX ACTION
        public IActionResult Index(string filter, int page = 1)
        {
            ViewBag.Filter = filter;

            var products = _context.Products.Include(x => x.Brand).Include(x=>x.Country).Include(x=>x.ProductCount).Include(x => x.ProductSize).Include(x=>x.TypeProduct).ThenInclude(x=>x.Category).AsQueryable();

            if (filter != null)
                products = products.Where(x => x.IsDeleted == (filter == "true" ? true : false));

            string pageSizeStr = _context.Settings.FirstOrDefault(x => x.Key == "PageSize").Value;
            int pageSize = pageSizeStr == null ? 3 : int.Parse(pageSizeStr);
            return View(PagenatedList<Product>.Create(products, page, pageSize));
        }

        //CREATE ACTION
        public IActionResult Create()
        {
            ViewBag.Brands = _context.Brands.Where(x => !x.IsDeleted).ToList();
            ViewBag.Countries = _context.Countries.ToList();
            ViewBag.ProductSize = _context.ProductSize.ToList();
            ViewBag.ProductCountPack = _context.ProductCount.ToList();
            ViewBag.Type = _context.TypeProducts.Where(x=> !x.IsDeleted).ToList();
            ViewBag.Categories = _context.Categories.ToList();
            ViewBag.Sweetdry = _context.sweetDryScales.ToList();
            ViewBag.LiquorFlavor = _context.LiquorFlavors.Where(x => !x.IsDeleted).ToList();
            ViewBag.LiquorColor = _context.LiquorColors.Where(x => !x.IsDeleted).ToList();
            ViewBag.WineFoodPairing = _context.WineFoodPairings.Where(x => !x.IsDeleted).ToList();

            return View();
        }
        // CREATE POST  ACTION------------------     
        [HttpPost]
        public IActionResult Create(Product product)
        {
            ViewBag.Brands = _context.Brands.Where(x => !x.IsDeleted).ToList();
            ViewBag.Countries = _context.Countries.ToList();
            ViewBag.ProductSize = _context.ProductSize.ToList();
            ViewBag.ProductCountPack = _context.ProductCount.ToList();
            ViewBag.Type = _context.TypeProducts.Where(x => !x.IsDeleted).Include(x=>x.Category).ToList();
            ViewBag.Categories = _context.Categories.ToList();
            ViewBag.Sweetdry = _context.sweetDryScales.ToList();
            ViewBag.LiquorFlavor = _context.LiquorFlavors.Where(x=>!x.IsDeleted).ToList();
            ViewBag.LiquorColor = _context.LiquorColors.Where(x => !x.IsDeleted).ToList();
            ViewBag.WineFoodPairing = _context.WineFoodPairings.Where(x => !x.IsDeleted).ToList();

            

            if (product.CostPrice < 0)
            {
                ModelState.AddModelError("CostPrice", "Cost Price cannot be Negative");
                return View();
            }
            if (product.CostPrice == 0)
            {
                ModelState.AddModelError("CostPrice", "CostPrice value cannot be null");
                return View();
            }
            if (product.SalePrice == 0)
            {
                ModelState.AddModelError("SalePrice", "SalePrice value cannot be null");
                return View();
            }
            if (product.SalePrice < 0)
            {
                ModelState.AddModelError("SalePrice", "Sale Price cannot be Negative");
                return View();
            }
            if (product.DiscountPercent < 0)
            {
                ModelState.AddModelError("DiscountPercent", "Discount Percent cannot be Negative");
                return View();
            }
            if (product.CostPrice > product.SalePrice)
            {
                ModelState.AddModelError("SalePrice", "Cost Price cannot be less than SalePrice");
                return View();
            }
            if (product.Desc!=null)
            {
                if (product.Desc.Length > 800)
                {
                    ModelState.AddModelError("Desc", "Description cannot be greater than 800 characters");
                    return View(product);
                }
            }
            else
            {
                ModelState.AddModelError("Desc", "Description cannot be null");
                return View(product);
            }
            if (product.Name!=null)
            {
                if (product.Name.Length > 85)
                {
                    ModelState.AddModelError("Name", "Name cannot be greater than 85 characters");
                    return View(product);
                }
            }
            else
            {
                ModelState.AddModelError("Name", "Name cannot be null");
                return View(product);
            }
            if (product.TypeProductId == 0)
            {
                ModelState.AddModelError("TypeProductId", "Type cannot be null");
                return View(product);
            }
            //IMAGE 
            if (product.ImageFile == null)
            {
                ModelState.AddModelError("ImageFile", "Image is required");
                return View();
            }
            else
            {
                if (product.ImageFile.ContentType != "image/jpeg" && product.ImageFile.ContentType != "image/png")
                {
                    ModelState.AddModelError("ImageFile", "File type must be jpeg or png");
                    return View(product);
                }

                if (product.ImageFile.Length > 4194304)
                {
                    ModelState.AddModelError("ImageFile", "file size must be less than 4mb");
                    return View(product);
                }
                string fileName = product.ImageFile.FileName;

                if (fileName.Length > 64)
                {
                    fileName = fileName.Substring(product.ImageFile.FileName.Length - 64, 64);
                }

                product.Image = (Guid.NewGuid().ToString() + (fileName));

                string path = Path.Combine(_env.WebRootPath, "uploads/products", product.Image);

                using (FileStream stream = new FileStream(path, FileMode.Create))
                {
                    product.ImageFile.CopyTo(stream);
                }
            }


            if (!_context.Brands.Any(x => x.Id == product.BrandId && !x.IsDeleted))
            {
                ModelState.AddModelError("BrandId", "Brand not found");
                return View();
            }

            if (!_context.Countries.Any(x => x.Id == product.CountryId))
            {
                ModelState.AddModelError("CountryId", "Country not found");
                return View();
            }

            if (!_context.TypeProducts.Any(x => x.Id == product.TypeProductId))
            {
                ModelState.AddModelError("TypeProductId", "Type not found");
                return View();
            }
            if (product.ProductCountId!=null)
            {
                if (!_context.ProductCount.Any(x => x.Id == product.ProductCountId))
                {
                    ModelState.AddModelError("ProductCountId", "This kinda packet not found");
                    return View();
                }
            }
            if (product.ProductSizeId != null)
            {
                if (!_context.ProductSize.Any(x => x.Id == product.ProductSizeId))
                {
                    ModelState.AddModelError("ProductSizeId", "This kinda size not found");
                    return View();
                }
            }
            if (product.Abv < 0)
            {
                ModelState.AddModelError("Abv", "Alcohol percentage cannot be less than zero");
                return View();
            }
            else if (product.Abv > 100)
            {
                ModelState.AddModelError("Abv", "Alcohol percentage cannot be greater than 100");
                return View();
            }

            string CodePrefix = "";
                foreach (var type in ViewBag.Type)
                {
                    if (product.TypeProductId==type.Id)
                    {
                    foreach (var category in ViewBag.Categories)
                    {
                           if (type.CategoryId == category.Id)
                        {
                            product.CategoryId = category.Id;
                            CodePrefix = category.Name.Substring(0, 2).ToUpper();
                        }
                    }
                     
                    }
                }
            if (product.CategoryId != 4)
            {
                if (product.Abv == null)
                {
                    ModelState.AddModelError("Abv", "ABV value cannot be null");
                    return View();
                }
            }
            if (_context.Products.OrderByDescending(x => x.CodeNum).FirstOrDefault() == null)
            {
                int CodeNum = 1001;
                product.CodeNum = CodeNum;
                product.CodePref = CodePrefix;
            }
            else
            {
                var lastProduct = _context.Products.OrderByDescending(x => x.CodeNum).FirstOrDefault();
                int CodeNum = lastProduct == null ? 1001 : lastProduct.CodeNum + 1;
                product.CodeNum = CodeNum;
                product.CodePref = CodePrefix;
            }

            product.CreatedAt = DateTime.UtcNow.AddHours(4);
            product.LastUpdateDate = DateTime.UtcNow.AddHours(4);
            _context.Products.Add(product);
            _context.SaveChanges();

            return RedirectToAction("index");
        }

        //DELETE ACTION
        public IActionResult Delete(int id)
        {
            Product existProduct= _context.Products.FirstOrDefault(x => x.Id == id && !x.IsDeleted);

            if (existProduct == null)
                return NotFound();


            existProduct.IsDeleted = true;
            _context.SaveChanges();

            return RedirectToAction("index");
        }

        //RESTORE ACTION
        public IActionResult Restore(int id)
        {
            Product existProduct = _context.Products.FirstOrDefault(x => x.Id == id && x.IsDeleted);

            if (existProduct == null)
                return NotFound();

            existProduct.IsDeleted = false;
            _context.SaveChanges();

            return RedirectToAction("index");
        }

        //EDIT ACTION
        public IActionResult Edit(int id)
        {
            Product product = _context.Products.Where(x=>!x.IsDeleted).Include(x => x.Brand).Include(x => x.Country).Include(x => x.ProductSize).Include(x => x.ProductCount).Include(x => x.LiquorColor).Include(x => x.LiquorFlavor).Include(x => x.sweetDryScale).Include(x => x.WineFoodPairing).Include(x => x.TypeProduct).ThenInclude(x => x.Category).FirstOrDefault(x => x.Id == id);
            if (product == null) return NotFound();

            ViewBag.Brands = _context.Brands.Where(x => !x.IsDeleted).ToList();
            ViewBag.Products = _context.Products.Where(x => !x.IsDeleted).Where(x => x.Id == product.Id).Include(x => x.ProductCount).Include(x => x.ProductSize).Include(x => x.TypeProduct).ThenInclude(x => x.Category).ToList();
            ViewBag.Countries = _context.Countries.ToList();
            ViewBag.ProductSize = _context.ProductSize.ToList();
            ViewBag.ProductCountPack = _context.ProductCount.ToList();
            ViewBag.Type = _context.TypeProducts.Where(x => !x.IsDeleted).ToList();
            ViewBag.Categories = _context.Categories.ToList();
            ViewBag.Sweetdry = _context.sweetDryScales.ToList();
            ViewBag.LiquorFlavor = _context.LiquorFlavors.Where(x => !x.IsDeleted).ToList();
            ViewBag.LiquorColor = _context.LiquorColors.Where(x => !x.IsDeleted).ToList();
            ViewBag.WineFoodPairing = _context.WineFoodPairings.Where(x => !x.IsDeleted).ToList();

            return View(product);
        }

        [HttpPost]
        public IActionResult Edit(Product product)
        {
            ViewBag.Brands = _context.Brands.Where(x => !x.IsDeleted).ToList();
            ViewBag.Products = _context.Products.Where(x => !x.IsDeleted).Where(x=>x.Id==product.Id).Include(x=>x.ProductCount).Include(x=>x.ProductSize).Include(x=>x.TypeProduct).ThenInclude(x=>x.Category).ToList();
            ViewBag.Countries = _context.Countries.ToList();
            ViewBag.ProductSize = _context.ProductSize.ToList();
            ViewBag.ProductCountPack = _context.ProductCount.ToList();
            ViewBag.Type = _context.TypeProducts.Where(x => !x.IsDeleted).ToList();
            ViewBag.Categories = _context.Categories.ToList();
            ViewBag.Sweetdry = _context.sweetDryScales.ToList();
            ViewBag.LiquorFlavor = _context.LiquorFlavors.Where(x => !x.IsDeleted).ToList();
            ViewBag.LiquorColor = _context.LiquorColors.Where(x => !x.IsDeleted).ToList();
            ViewBag.WineFoodPairing = _context.WineFoodPairings.Where(x => !x.IsDeleted).ToList();

            Product existProduct = _context.Products.Where(x => !x.IsDeleted).Include(x => x.Brand).Include(x => x.Country).Include(x => x.ProductSize).Include(x => x.ProductCount).Include(x => x.LiquorColor).Include(x => x.LiquorFlavor).Include(x => x.sweetDryScale).Include(x => x.WineFoodPairing).Include(x => x.TypeProduct).ThenInclude(x => x.Category).FirstOrDefault(x => x.Id == product.Id);
            if (existProduct == null) return NotFound();

            if (!_context.Brands.Any(x => x.Id == product.BrandId && !x.IsDeleted))
            {
                ModelState.AddModelError("BrandId", "Brand not found");
                return View();
            }

            if (!_context.Countries.Any(x => x.Id == product.CountryId))
            {
                ModelState.AddModelError("CountryId", "Country not found");
                return View();
            }
            if (product.CostPrice == 0)
            {
                ModelState.AddModelError("CostPrice", "CostPrice value cannot be null");
                return View();
            }
            if (product.SalePrice == 0)
            {
                ModelState.AddModelError("SalePrice", "SalePrice value cannot be null");
                return View();
            }
            if (product.CostPrice < 0)
            {
                ModelState.AddModelError("CostPrice", "Cost Price cannot be Negative");
                return View();
            }
            if (product.SalePrice < 0)
            {
                ModelState.AddModelError("SalePrice", "Sale Price cannot be Negative");
                return View();
            }
            if (product.DiscountPercent < 0)
            {
                ModelState.AddModelError("DiscountPercent", "Discount Percent cannot be Negative");
                return View();
            }
            if (product.CostPrice > product.SalePrice)
            {
                ModelState.AddModelError("SalePrice", "Cost Price cannot be less than SalePrice");
                return View();
            }
            if (product.Desc != null)
            {
                if (product.Desc.Length > 800)
                {
                    ModelState.AddModelError("Desc", "Description cannot be greater than 800 characters");
                    return View(product);
                }
            }
            else
            {
                ModelState.AddModelError("Desc", "Description cannot be null");
                return View(product);
            }
            if (existProduct.CategoryId != 4)
            {
                if (product.Abv < 0)
                {
                    ModelState.AddModelError("Abv", "Alcohol percentage cannot be less than zero");
                    return View(existProduct);
                }
                else if (product.Abv > 100)
                {
                    ModelState.AddModelError("Abv", "Alcohol percentage cannot be greater than 100");
                    return View(existProduct);
                }
                existProduct.Abv = product.Abv;
            }
            if (product.Name != null)
            {
                if (product.Name.Length > 85)
                {
                    ModelState.AddModelError("Name", "Name cannot be greater than 85 characters");
                    return View(product);
                }
            }
            else
            {
                ModelState.AddModelError("Name", "Name cannot be null");
                return View(product);
            }

            if (existProduct.CategoryId != 4)
            {
                if (product.Abv == null)
                {
                    ModelState.AddModelError("Abv", "ABV value cannot be null");
                    return View();
                }
            }
            //IMAGE
            if (product.ImageFile != null)
            {
                if (product.ImageFile.ContentType != "image/jpeg" && product.ImageFile.ContentType != "image/png")
                {
                    ModelState.AddModelError("ImageFile", "file type must be jpeg or png");
                    return View(existProduct);
                }

                if (product.ImageFile.Length > 4194304)
                {
                    ModelState.AddModelError("ImageFile", "file size must be less than 4mb");
                    return View(existProduct);
                }

                string fileName = product.ImageFile.FileName;

                if (fileName.Length > 64)
                {
                    fileName = fileName.Substring(product.ImageFile.FileName.Length - 64, 64);
                }

                product.Image = (Guid.NewGuid().ToString() + (fileName));

                string path = Path.Combine(_env.WebRootPath, "uploads/products", product.Image);

                using (FileStream stream = new FileStream(path, FileMode.Create))
                {
                    product.ImageFile.CopyTo(stream);
                }

                if (existProduct.Image != null)
                {
                    string existPath = Path.Combine(_env.WebRootPath, "uploads/products", existProduct.Image);
                    if (System.IO.File.Exists(existPath))
                        System.IO.File.Delete(existPath);
                }

                existProduct.Image = product.Image;
            }

            if (existProduct == null) return NotFound();


            existProduct.Name = product.Name;
            existProduct.Desc = product.Desc;
            existProduct.SalePrice = product.SalePrice;
            existProduct.CostPrice = product.CostPrice;
            existProduct.DiscountPercent = product.DiscountPercent;
            existProduct.IsAvailable = product.IsAvailable;
            existProduct.BrandId = product.BrandId;
            existProduct.CountryId = product.CountryId;
            if (existProduct.ProductCountId!=null)
            {
                existProduct.ProductCountId = product.ProductCountId;
            }
            else if (existProduct.ProductSizeId!=null)
            {
                existProduct.ProductSizeId = product.ProductSizeId;
            }
            if (existProduct.CategoryId==2)
            {
                existProduct.SweetDryScaleId = product.SweetDryScaleId;
                existProduct.WineFoodPairingId = product.WineFoodPairingId;
            }
            if (existProduct.CategoryId==3)
            {
                existProduct.LiquorColorId = product.LiquorColorId;
                existProduct.LiquorFlavorId = product.LiquorFlavorId;
            }

            existProduct.LastUpdateDate = DateTime.UtcNow.AddHours(4);

            _context.SaveChanges();
            return RedirectToAction("index");
        }
    }
}
