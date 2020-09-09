using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerceModels.Models
{
   public class OrderItem
    {
        public int OrderId { get; set; }
        public Order Order { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
    }
}
