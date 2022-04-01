using DrizlyBackEnd.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DrizlyBackEnd.Models
{
    public class Order : BaseEntity
    {
        public string AppUserId { get; set; }
        [Required]
        [StringLength(maximumLength: 50)]
        public string FullName { get; set; }
        [Required]
        [StringLength(maximumLength: 20)]
        public string PhoneNumber { get; set; }
        [StringLength(maximumLength: 100)]
        public string Email { get; set; }
        [Required]
        [StringLength(maximumLength: 50)]
        public string Country { get; set; }
        [Required]
        [StringLength(maximumLength: 50)]
        public string City { get; set; }
        [Required]
        [StringLength(maximumLength: 350)]
        public string Address { get; set; }
        [Required]
        [StringLength(maximumLength: 30)]
        public string PostZip { get; set; }
        public OrderStatus Status { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalPrice { get; set; }
        [StringLength(maximumLength: 400)]
        public string Note { get; set; }

        public List<OrderItem> OrderItems { get; set; }
        public AppUser AppUser { get; set; }
    }
}
