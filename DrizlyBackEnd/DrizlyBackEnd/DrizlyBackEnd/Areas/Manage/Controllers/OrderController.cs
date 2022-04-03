using DrizlyBackEnd.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace DrizlyBackEnd.Areas.Manage.Controllers
{
    //[Authorize(Roles = "Admin,SuperAdmin")]
    [Area("manage")]
    public class OrderController : Controller
    {
        private readonly DrizlyContext _context;
        private readonly IWebHostEnvironment _env;

        public OrderController(DrizlyContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }


        //Index ACTION ORDER
        public IActionResult Index(int? filter, int page = 1)
        {
            ViewBag.Filter = filter;
            var orders = _context.Orders.Include(x => x.OrderItems).ThenInclude(x => x.Product).AsQueryable();

            if (filter!=0 && filter!=null)
            {
                orders = orders.Where(x => (int)x.Status == filter);
            }

            string pageSizeStr = _context.Settings.FirstOrDefault(x => x.Key == "PageSize").Value;
            int pageSize = pageSizeStr == null ? 3 : int.Parse(pageSizeStr);
            return View(PagenatedList<Order>.Create(orders, page, pageSize));
        }

        //DETAIL ORDER ACTION
        public IActionResult Detail(int id)
        {
            Order order = _context.Orders.Include(x => x.OrderItems).ThenInclude(x => x.Product).FirstOrDefault(x => x.Id == id);

            if (order == null) return NotFound();

            return View(order);
        }

        //EDIT ACTION ORDER
        public IActionResult Edit(int id)
        {
            Order order = _context.Orders.Include(x => x.OrderItems).ThenInclude(x => x.Product).FirstOrDefault(x => x.Id == id);

            if (order == null) return NotFound();

            return View(order);
        }

        [HttpPost]
        public IActionResult Edit(Order order)
        {
            if (order.Id == 0)
            {
                ModelState.AddModelError("", "Error has Occured!");
                return View();
            }

            Order existOrder = _context.Orders.FirstOrDefault(x => x.Id == order.Id);
            if (order == null) return NotFound();

            existOrder.Status = order.Status;
            _context.SaveChanges();

            //email send on order status change
            string body = String.Empty;
            var path = _env.WebRootPath + Path.DirectorySeparatorChar.ToString() + "Template" + Path.DirectorySeparatorChar.ToString() + "EmailTemplates" + Path.DirectorySeparatorChar.ToString() + "OrderStatus.html";
            using (StreamReader streamReader = System.IO.File.OpenText(path))
            {
                body = streamReader.ReadToEnd();
            }

            body = body.Replace("{fullname}", existOrder.FullName);
            body = body.Replace("{date}", existOrder.CreatedAt.ToString("MMMM dd, yyyy"));
            body = body.Replace("{status}", existOrder.Status.ToString());
            body = body.Replace("{total}", existOrder.TotalPrice.ToString("0.00"));
            body = body.Replace("{orderId}", existOrder.Id.ToString());

            MailMessage mailMessage = new MailMessage();
            mailMessage.To.Add(existOrder.Email);
            mailMessage.From = new MailAddress("drizlycode@gmail.com");
            mailMessage.Subject = "New Order Status for order No" + existOrder.Id + "!";
            mailMessage.Body = body;
            mailMessage.IsBodyHtml = true;

            SmtpClient smtp = new SmtpClient();

            smtp.Credentials = new NetworkCredential("drizlycode@gmail.com", "Drizly21");
            smtp.Port = 587;
            smtp.Host = "smtp.gmail.com";
            smtp.EnableSsl = true;
            smtp.Send(mailMessage);

            return RedirectToAction("index");
        }
    }
}
