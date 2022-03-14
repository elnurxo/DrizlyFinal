using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DrizlyBackEnd.Models
{
    public class BaseEntity
    {
        public int Id { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime LastUpdateDate { get; set; }
    }
}
