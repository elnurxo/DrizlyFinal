using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DrizlyBackEnd.Models
{
    public class ProductSize
    {
        public int Id { get; set; }
        [Required]
        [StringLength(maximumLength: 85)]
        public string Size { get; set; }
        public List<Product> Products { get; set; }
    }
}
