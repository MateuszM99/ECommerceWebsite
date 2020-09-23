using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerceModels.Models
{
    public class ProductOptionQuantity
    {
        public ProductOptionQuantity(Product product,int quantity,Option option)
        {
            this.product = product;
            this.quantity = quantity;
            this.option = option;
        }

        public Product product { get; set; }
        public Option option { get; set; }
        public int quantity { get; set; }
    }
}
