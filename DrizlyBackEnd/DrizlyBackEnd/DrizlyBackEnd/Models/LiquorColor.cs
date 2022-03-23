using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DrizlyBackEnd.Models
{
    public class LiquorColor : BaseEntity
    {
        [StringLength(maximumLength: 85)]
        public string Color { get; set; }
    }
}
