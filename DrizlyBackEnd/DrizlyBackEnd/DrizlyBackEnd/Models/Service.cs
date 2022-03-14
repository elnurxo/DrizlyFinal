using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DrizlyBackEnd.Models
{
    public class Service
    {
        public int Id { get; set; }
        [Required]
        [StringLength(maximumLength: 75)]
        public string Title { get; set; }
        [StringLength(maximumLength: 250)]
        public string Desc { get; set; }
        [StringLength(maximumLength: 100)]
        public string Image { get; set; }
        [NotMapped]
        public IFormFile ImageFile { get; set; }
    }
}
