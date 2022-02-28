using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DrizlyFinal.Core.Models
{
    public class AppUser : BaseEntity
    {
        public string FullName { get; set; }
        public string ProfileImage { get; set; }
        public bool IsAdmin { get; set; }
    }
}
