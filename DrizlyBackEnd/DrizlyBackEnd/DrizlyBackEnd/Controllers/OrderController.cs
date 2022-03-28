using DrizlyBackEnd.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DrizlyBackEnd.Controllers
{
    public class OrderController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly DrizlyContext _context;

        public OrderController(UserManager<AppUser> userManager, DrizlyContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        //BASKET VIEW  CART ACTION
        public IActionResult Basket()
        {
            return View();
        }
    }
}
