using DrizlyBackEnd.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DrizlyBackEnd.Services
{
    public class LayoutService
    {

        private readonly DrizlyContext _context;

        public LayoutService(DrizlyContext context)
        {
            _context = context;
        }
        public Dictionary<string, string> GetSettings()
        {
            return _context.Settings.ToDictionary(x => x.Key, x => x.Value);
        }
    }
}
