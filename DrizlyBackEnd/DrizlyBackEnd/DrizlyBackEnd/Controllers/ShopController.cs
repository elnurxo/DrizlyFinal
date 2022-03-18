using DrizlyBackEnd.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DrizlyBackEnd.Controllers
{
    public class ShopController : Controller
    {
        private readonly DrizlyContext _context;
        public ShopController(DrizlyContext context)
        {
            _context = context;
        }

        //INDEX ACTION
        public IActionResult Index()
        { 
            return View();
        }
    }
}
