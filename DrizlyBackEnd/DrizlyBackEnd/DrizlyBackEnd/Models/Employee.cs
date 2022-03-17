using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DrizlyBackEnd.Models
{
    public class Employee
    {
        public int Id { get; set; }
        [Required]
        [StringLength(maximumLength: 75)]
        public string FullName { get; set; }
        [Required]
        public int PositionId { get; set; }
        public Position Position { get; set; }
        [StringLength(maximumLength: 100)]
        public string Image { get; set; }
        [NotMapped]
        public IFormFile ImageFile { get; set; }
        public bool IsDeleted { get; set; }
    }
}
