using DrizlyBackEnd.Models;
using DrizlyBackEnd.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DrizlyBackEnd.Services
{
    public class LayoutService
    {

        private readonly DrizlyContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public LayoutService(DrizlyContext context, UserManager<AppUser> userManager, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }
        public Dictionary<string, string> GetSettings()
        {
            return _context.Settings.ToDictionary(x => x.Key, x => x.Value);
        }
        public BasketViewModel GetBasket()
        {
            BasketViewModel basketVM = new BasketViewModel
            {
                BasketItems = new List<ProductBasketItemViewModel>(),
                TotalPrice = 0
            };

            List<BasketItemViewModel> basketItems = new List<BasketItemViewModel>();    
            AppUser member = null;
            if (_httpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
            {
                member = _userManager.Users.FirstOrDefault(x => x.UserName == _httpContextAccessor.HttpContext.User.Identity.Name && !x.IsAdmin);
            }

            if (member == null)
            {
                var baksetStr = _httpContextAccessor.HttpContext.Request.Cookies["basket"];

                if (!string.IsNullOrWhiteSpace(baksetStr))
                    basketItems = JsonConvert.DeserializeObject<List<BasketItemViewModel>>(baksetStr);

            }
            else
            {
                basketItems = _context.BasketItems.Where(x => x.AppUserId == member.Id).Select(x => new BasketItemViewModel { ProductId = x.ProductId, Count = x.Count }).ToList();
            }


            foreach (var item in basketItems)
            {
                Product product = _context.Products.Where(x => x.IsAvailable == true && !x.IsDeleted).FirstOrDefault(x => x.Id == item.ProductId);
                if (product!=null)
                {
                    ProductBasketItemViewModel bookBasketItem = new ProductBasketItemViewModel
                    {
                        Product = product,
                        Count = item.Count
                    };

                    basketVM.BasketItems.Add(bookBasketItem);
                    if (item.Count > 0)
                    {
                        decimal totalPrice = product.DiscountPercent > 0 ? (product.SalePrice * (1 - product.DiscountPercent / 100)) : product.SalePrice;
                        basketVM.TotalPrice += totalPrice * item.Count;
                    }
                }
         
            }

            return basketVM;
        }

        public int GetUnreadMessages()
        {
            int messages = _context.ContactUs.Where(x => x.IsRead == false).Count();

            return (messages);
        }
    }
}
