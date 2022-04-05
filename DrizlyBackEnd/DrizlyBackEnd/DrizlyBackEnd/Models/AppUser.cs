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
        [StringLength(maximumLength: 100)]
        public string Image { get; set;  }
        [NotMapped]
        public IFormFile BackGroundFile { get; set; }
        [StringLength(maximumLength: 100)]
        public string BackgroundImage { get; set; }
        [StringLength(maximumLength: 50)]
        public string Country { get; set; }
        [StringLength(maximumLength: 50)]
        public string City { get; set; }
        [StringLength(maximumLength: 350)]
        public string Address { get; set; }
        [StringLength(maximumLength: 120)]
        public string FacebookUrl { get; set; }
        [StringLength(maximumLength: 120)]
        public string TwitterUrl { get; set; }
        [StringLength(maximumLength: 120)]
        public string InstagramUrl { get; set; }
        public List<AppUserCoupon> appUserCoupons { get; set; }
        [StringLength(maximumLength: 50)]
        public string RoleName { get; set; }

    }
}
