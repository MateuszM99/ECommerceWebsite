using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerceModels.Models
{
    public class CartProduct
    {
        public int CartId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
