using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DrizlyBackEnd.Areas.Manage.ViewModels
{
    public class AdminCreateViewModel
    {
        [Required]
        [StringLength(maximumLength: 30)]
        public string UserName { get; set; }
        [Required]
        [StringLength(maximumLength: 60)]
        public string FullName { get; set; }
        [Required]
        [StringLength(maximumLength: 100)]
        public string Email { get; set; }
        [Required]
        public int Age { get; set; }
        [Required]
        [StringLength(maximumLength: 25)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [StringLength(maximumLength: 25)]
        [DataType(DataType.Password)]
        [Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }
        [StringLength(maximumLength: 100)]
        [Required]
        public string Role { get; set; }
    }
}
