using DrizlyBackEnd.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DrizlyBackEnd.ViewModels
{
    public class HomeViewModel
    {
        public List<Settings> Settings { get; set; }
        public List<Partnership> Partnerships { get; set; }
    }
}
