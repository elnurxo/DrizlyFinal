using DrizlyBackEnd.Models;
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
            //VISITOR TASK
            //string visitorCookie = HttpContext.Request.Cookies["visitor"];

            //if (sessionStorage.getItem("visitor") === null)
            //{
            //    //...
            //}

            //if (!string.IsNullOrWhiteSpace(visitorCookie))
            //{

            //if (HttpContext.Request.Cookies["visitor"] == null)
            //{
            //    HttpContext.Response.SetCookie(new HttpCookie("visitor"));

            //    //do your first time stuff
            //}
          

            //}
            //END VISITOR TASK   


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
                            CreatedAt = DateTime.UtcNow.AddHours(4),
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
                RecentComments = _context.ProductComments.Include(x => x.AppUser).Include(x => x.Product).OrderByDescending(x=>x.CreatedAt).Take(4).ToList(),
                RecentlyBought = _context.Orders.Include(x=>x.OrderItems).ThenInclude(x=>x.Product).ThenInclude(x=>x.ProductComments).OrderByDescending(x=>x.CreatedAt).Take(4).ToList()
            };
            return View(homeVM);
        }

        [HttpPost]
        public IActionResult IncrementVisitorCount()
        {
            WebSiteVisitor visitor = new WebSiteVisitor();
            visitor.VisitDate = DateTime.UtcNow.AddHours(4);
            _context.WebSiteVisitors.Add(visitor);
            _context.SaveChanges();

            return Ok();
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

        //SEARCH ACTION
        public IActionResult SearchProductHome(string searchStringhome)
        {
            var products = _context.Products.AsQueryable();
            if (!String.IsNullOrEmpty(searchStringhome))
            {
                products = products.Where(x => x.Name.ToLower().Trim().Contains(searchStringhome.ToLower().Trim()));
            }
            else
            {
                products = null;
            }
            return PartialView("_SearchHeroSectionPartialView", products.ToList());
        }

        public IActionResult Error()
        {
            return PartialView("_ErrorPagePartialView");
        }
    }
}
