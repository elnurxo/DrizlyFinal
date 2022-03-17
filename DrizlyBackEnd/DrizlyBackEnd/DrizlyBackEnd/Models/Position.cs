using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DrizlyBackEnd.Models
{
    public class Position
    {
        public int Id { get; set; }
        [Required]
        [StringLength(maximumLength: 75)]
        public string Name { get; set; }
        public List<Employee> Employees { get; set; }
    }
}
