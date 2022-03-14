using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DrizlyBackEnd.Models
{
    public class Settings : BaseEntity
    {
        [Required]
        [StringLength(maximumLength: 300)]
        public string Key { get; set; }
        [Required]
        [StringLength(maximumLength: 300)]
        public string Value { get; set; }
        [NotMapped]
        public IFormFile ImageFile { get; set; }
    }
}
