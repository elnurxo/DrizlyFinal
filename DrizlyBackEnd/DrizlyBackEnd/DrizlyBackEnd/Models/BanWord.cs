using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DrizlyBackEnd.Models
{
    public class BanWord
    {
        public int Id { get; set; }
        [Required]
        [StringLength(maximumLength: 100)]
        public string Text { get; set; }
    }
}
