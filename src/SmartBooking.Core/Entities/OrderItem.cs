﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartBooking.Core.Entities
{
    public class OrderItem : BaseEntity<int>
    {

        public int OrderId { get; set; }
        public Order Order { get; set; }

        public int MenuItemId { get; set; }
        public MenuItem MenuItem { get; set; }

        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
