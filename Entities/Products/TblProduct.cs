﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class TblProduct
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }  
        public int Stock { get; set; }
    }
}
