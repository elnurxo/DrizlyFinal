using DrizlyBackEnd.Models;
using DrizlyBackEnd.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace DrizlyBackEnd.Controllers
{
    public class OrderController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly DrizlyContext _context;
        private readonly IWebHostEnvironment _env;

        public OrderController(UserManager<AppUser> userManager, DrizlyContext context,IWebHostEnvironment env)
        {
            _userManager = userManager;
            _context = context;
            _env = env;
        }

        //BASKET VIEW  CART ACTION
        public IActionResult Basket()
        {
            return View();
        }

        //CHECKOUT ACTION
        public IActionResult Checkout()
        {
            AppUser member = null;
            if (User.Identity.IsAuthenticated)
            {
                member = _userManager.Users.FirstOrDefault(x => x.UserName == User.Identity.Name && !x.IsAdmin);
            }


            CheckoutViewModel checkoutVM = new CheckoutViewModel
            {
                Basket = _getBasket(member),
                Order = new Order
                {
                    Email = member?.Email,
                    PhoneNumber = member?.PhoneNumber,
                    FullName = member?.FullName,
                    Address = member?.Address,
                    City = member?.City,
                    Country = member?.Country
                }
            };
            return View(checkoutVM);
        }

        //ORDER ACTION
        //[Authorize(Roles = "Member")]
        [HttpPost]
        public IActionResult Create(Order order)
        {

            AppUser member = null;
            if (User.Identity.IsAuthenticated)
            {
                member = _userManager.Users.FirstOrDefault(x => x.UserName == User.Identity.Name && !x.IsAdmin);
            }
            if (member == null)
            {
                TempData["Warning"] = "You have to register to order!";
                return RedirectToAction("register", "account");
            }
            CheckoutViewModel checkoutVM = new CheckoutViewModel
            {
                Basket = _getBasket(member),
                Order = order
            };

            if (!ModelState.IsValid)
                return View("Checkout", checkoutVM);

            if (checkoutVM.Basket.BasketItems.Count == 0)
            {
                ModelState.AddModelError("", "Your basket is empty, you can't order emptiness!");
                return View("Checkout", checkoutVM);
            }


            order.AppUserId = member?.Id;
            order.CreatedAt = DateTime.UtcNow.AddHours(4);
            order.LastUpdateDate = DateTime.UtcNow.AddHours(4);
            order.Status = Enums.OrderStatus.Pending;
           


            order.OrderItems = new List<OrderItem>();

            foreach (var item in checkoutVM.Basket.BasketItems)
            {
                OrderItem orderItem = new OrderItem
                {
                    ProductId = item.Product.Id,
                    SalePrice = item.Product.SalePrice,
                    CostPrice = item.Product.CostPrice,
                    DiscountedPrice = item.Product.DiscountPercent > 0 ? (item.Product.SalePrice * (1 - item.Product.DiscountPercent / 100)) : item.Product.SalePrice,
                    Count = item.Count
                };
                order.OrderItems.Add(orderItem);
                order.TotalPrice += orderItem.DiscountedPrice * orderItem.Count;         
            }

            _context.Orders.Add(order);
            _context.BasketItems.RemoveRange(_context.BasketItems.Where(x => x.AppUserId == member.Id));
            _context.SaveChanges();
            //email
            string body = String.Empty;
            var path = _env.WebRootPath + Path.DirectorySeparatorChar.ToString() + "Template" + Path.DirectorySeparatorChar.ToString() + "EmailTemplates" + Path.DirectorySeparatorChar.ToString() + "Order.html";
            using (StreamReader streamReader = System.IO.File.OpenText(path))
            {
                body = streamReader.ReadToEnd();
            }

            body = body.Replace("{fullname}", order.FullName);
            body = body.Replace("{date}", order.CreatedAt.ToString("MMMM dd, yyyy"));
            body = body.Replace("{status}", order.Status.ToString());
            body = body.Replace("{total}", order.TotalPrice.ToString("0.00"));
            body = body.Replace("{orderId}", order.Id.ToString());
            body = body.Replace("{orderitemCount}", order.OrderItems.Count().ToString());

            MailMessage mailMessage = new MailMessage();
            mailMessage.To.Add(order.Email);
            mailMessage.From = new MailAddress("drizlycode@gmail.com");
            mailMessage.Subject = "Welcome to the Drizly Side " + order.FullName+"!";
            mailMessage.Body = body;
            mailMessage.IsBodyHtml = true;

            SmtpClient smtp = new SmtpClient();

            smtp.Credentials = new NetworkCredential("drizlycode@gmail.com", "Drizly21");
            smtp.Port = 587;
            smtp.Host = "smtp.gmail.com";
            smtp.EnableSsl = true;
            smtp.Send(mailMessage);

            TempData["Success"] = "Product(s) Ordered Successfully";
            return RedirectToAction("profile", "account");
        }


        //Get basket private 
        private BasketViewModel _getBasket(AppUser member)
        {
            BasketViewModel basketVM = new BasketViewModel
            {
                BasketItems = new List<ProductBasketItemViewModel>()
            };

            List<BasketItemViewModel> items = new List<BasketItemViewModel>();

            if (member == null)
            {
                string basketItemsStr = HttpContext.Request.Cookies["basket"];

                if (!string.IsNullOrWhiteSpace(basketItemsStr))
                    items = JsonConvert.DeserializeObject<List<BasketItemViewModel>>(basketItemsStr);
            }
            else
            {
                items = _context.BasketItems.Where(x => x.AppUserId == member.Id).Select(b => new BasketItemViewModel { ProductId = b.ProductId, Count = b.Count }).ToList();
            }

            foreach (var item in items)
            {
                Product product = _context.Products.Where(x => x.IsAvailable == true && !x.IsDeleted).FirstOrDefault(x => x.Id == item.ProductId);
                ProductBasketItemViewModel basketItem = new ProductBasketItemViewModel
                {
                    Product = product,
                    Count = item.Count
                };

                if (product!=null)
                {
                    decimal totalPrice = product.DiscountPercent > 0 ? (product.SalePrice * (1 - product.DiscountPercent / 100)) : product.SalePrice;
                    basketVM.TotalPrice += totalPrice * item.Count;
                    basketVM.BasketItems.Add(basketItem);
                }
             
            }

            return basketVM;
        }
    }
}
