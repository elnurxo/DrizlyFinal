using DrizlyBackEnd.Models;
using DrizlyBackEnd.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
        public HomeController(DrizlyContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            HomeViewModel homeVM = new HomeViewModel
            {
                Settings = _context.Settings.ToList(),
                Partnerships = _context.Partnerships.ToList(),
            };
            return View(homeVM);
        }

    }
}
