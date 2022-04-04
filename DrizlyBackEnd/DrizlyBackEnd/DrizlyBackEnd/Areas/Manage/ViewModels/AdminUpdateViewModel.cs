using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DrizlyBackEnd.Areas.Manage.ViewModels
{
    public class AdminUpdateViewModel
    {
        [Required]
        [StringLength(maximumLength: 60)]
        public string FullName { get; set; }
        [Required]
        [StringLength(maximumLength: 30)]
        public string UserName { get; set; }
        [Required]
        [StringLength(maximumLength: 100)]
        public string Email { get; set; }
        [StringLength(maximumLength: 50)]
        public string Country { get; set; }
        [StringLength(maximumLength: 50)]
        public string City { get; set; }
        [StringLength(maximumLength: 350)]
        public string Address { get; set; }
        [StringLength(maximumLength: 20)]
        public string Phone { get; set; }
        public int Age { get; set; }

        [StringLength(maximumLength: 25)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [StringLength(maximumLength: 25)]
        [DataType(DataType.Password)]
        [Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }

        [DataType(DataType.Password)]
        [StringLength(maximumLength: 25)]
        public string CurrentPassword { get; set; }
        [StringLength(maximumLength: 100)]
        public string Image { get; set; }
        [NotMapped]
        public IFormFile ImageFile { get; set; }
        [StringLength(maximumLength: 100)]
        public string BackgroundImage { get; set; }
        [NotMapped]
        public IFormFile BackGroundFile { get; set; }
        [StringLength(maximumLength: 120)]
        public string FacebookUrl { get; set; }
        [StringLength(maximumLength: 120)]
        public string TwitterUrl { get; set; }
        [StringLength(maximumLength: 120)]
        public string InstagramUrl { get; set; }
        public string Role { get; set; }
    }
}
