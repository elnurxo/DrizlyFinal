﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DrizlyBackEnd.Models
{
    public class LiquorFlavor : BaseEntity
    {
        [StringLength(maximumLength: 85)]
        public string Name { get; set; }
    }
}
