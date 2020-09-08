using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerceModels.Models
{
   public class ProductOption
    {
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int OptionId { get; set; }
        public Option Option { get; set; }
    }
}
