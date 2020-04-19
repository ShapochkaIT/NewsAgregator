﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsAgregator.Models
{
    public class Tag
    {
        public int Id { get; set; }
        public string Category { get; set; }
        public News News { get; set; }
    }
}
