using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DrizlyBackEnd.Models
{
    public class ContactUs
    {
        public int Id { get; set; }
        [Required]
        [StringLength(maximumLength: 50)]
        public string Name { get; set; }
        [Required]
        [StringLength(maximumLength: 60)]
        public string Phone { get; set; }
        [Required]
        [StringLength(maximumLength: 70)]
        public string Email { get; set; }
        [Required]
        [StringLength(maximumLength: 80)]
        public string Subject { get; set; }
        [StringLength(maximumLength: 400)]
        public string YourMessage { get; set; }
        public bool IsRead { get; set; }
    }
}
