using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerceModels.Models
{
    public class CartProduct
    {       
        public int Quantity { get; set; }       

        public int CartId { get; set; }
        public ShoppingCart Cart { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int OptionId { get; set; }
        public Option Option { get; set; }
    }
}
