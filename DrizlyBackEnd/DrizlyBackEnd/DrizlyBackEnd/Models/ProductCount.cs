using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DrizlyBackEnd.Models
{
    public class ProductCount
    {
        public int Id { get; set; }
        [Required]
        [StringLength(maximumLength: 85)]
        public int SizePack { get; set; }
        public List<Product> Products { get; set; }
    }
}
