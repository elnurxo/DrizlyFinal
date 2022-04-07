using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DrizlyBackEnd.Models
{
    public class BlackList
    {
        public int Id { get; set; }
        [StringLength(maximumLength: 256)]
        public DateTime BanStartDate { get; set; }
        public DateTime BanEndDate { get; set; }
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }
    }
}
