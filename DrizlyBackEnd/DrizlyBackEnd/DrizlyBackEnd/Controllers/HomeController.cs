﻿using DrizlyBackEnd.Models;
using DrizlyBackEnd.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace DrizlyBackEnd.Controllers
{
    public class HomeController : Controller
    {
        private readonly DrizlyContext _context;
        private readonly UserManager<AppUser> _usermanager;
        public HomeController(DrizlyContext context, UserManager<AppUser> usermanager)
        {
            _context = context;
            _usermanager = usermanager;
        }
        public async  Task<IActionResult> Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                List<BasketItemViewModel> basketProducts = new List<BasketItemViewModel>();
                AppUser user = User.Identity.IsAuthenticated ? await _usermanager.FindByNameAsync(User.Identity.Name) : null;

                if (HttpContext.Request.Cookies["basket"] != null)
                {
                    basketProducts = JsonConvert.DeserializeObject<List<BasketItemViewModel>>(HttpContext.Request.Cookies["basket"]);
                    BasketItem memberBasketItem = await _context.BasketItems.FirstOrDefaultAsync(x => x.AppUserId == user.Id);

                    foreach (var item in basketProducts)
                    {
                        memberBasketItem = new BasketItem
                        {
                            AppUserId = user.Id,
                            Count = item.Count,
                            ProductId = item.ProductId,
                            CreatedAt = DateTime.Now,
                        };
                        await _context.BasketItems.AddAsync(memberBasketItem);

                        await _context.SaveChangesAsync();

                    }
                    TempData["Success"] = "New items in your basket!";
                    Response.Cookies.Delete("basket");
                }
            }


            HomeViewModel homeVM = new HomeViewModel
            {
                Settings = _context.Settings.ToList(),
                Partnerships = _context.Partnerships.ToList(),
                Services = _context.Services.ToList(),
                Categories = _context.Categories.Include(x => x.TypeProducts).ToList(),
                OnSaleProducts = _context.Products.Where(x=>x.IsDeleted==false).Where(x => x.DiscountPercent > 0).OrderByDescending(x => x.DiscountPercent).Take(15).Include(x => x.ProductComments).ToList(),
                RecentComments = _context.ProductComments.Include(x => x.AppUser).Include(x => x.Product).OrderByDescending(x=>x.CreatedAt).Take(4).ToList()
            };
            return View(homeVM);
        }

        //SEARCH ACTION
         public IActionResult SearchProduct(string searchString)
        {
            var products = _context.Products.AsQueryable();
            if (!String.IsNullOrEmpty(searchString))
            {
                products = products.Where(x => x.Name.ToLower().Trim().Contains(searchString.ToLower().Trim()));
            }
            else
            {
                products = null;    
            }
            return PartialView("_SearchProductPartialView",products.ToList());
        }
    }
}
