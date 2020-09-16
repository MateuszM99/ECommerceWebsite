using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerceModels.Models
{
    public class ProductQuantity
    {
        public ProductQuantity(Product product,int quantity)
        {
            this.product = product;
            this.quantity = quantity;
        }

        public Product product { get; set; }
        public int quantity { get; set; }
    }
}
