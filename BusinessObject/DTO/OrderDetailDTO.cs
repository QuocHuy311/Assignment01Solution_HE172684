﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.DTO
{
    public class OrderDetailDTO
    {
        public int ProductId { get; set; }
        public int? Quantity { get; set; }
        public double? Discount { get; set; }
    }

}
