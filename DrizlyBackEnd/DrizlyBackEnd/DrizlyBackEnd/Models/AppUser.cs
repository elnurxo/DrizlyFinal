using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DrizlyBackEnd.Models
{
    public class AppUser : IdentityUser
    {
        [StringLength(maximumLength: 150)]
        public string FullName { get; set; }
        [Required]
        public int  Age { get; set; }
        public bool IsAdmin { get; set; }
        [NotMapped]
        public IFormFile ImageFile { get; set; }
        public string Image { get; set;  }

    }
}
