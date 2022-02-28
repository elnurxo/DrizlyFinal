using System;
using System.Collections.Generic;
using System.Text;

namespace DrizlyFinal.Core.Models
{
    public class BaseEntity
    {
        public int Id { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt{ get; set; }
    }
}
