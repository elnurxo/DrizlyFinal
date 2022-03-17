using DrizlyBackEnd.Models;
using DrizlyBackEnd.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DrizlyBackEnd.Controllers
{
    public class AboutUsController : Controller
    {
        private readonly DrizlyContext _context;
        public AboutUsController(DrizlyContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            AboutUsViewModel aboutusVM = new AboutUsViewModel
            {
                Settings = _context.Settings.ToList()
            };
            return View(aboutusVM);
        }
    }
}
