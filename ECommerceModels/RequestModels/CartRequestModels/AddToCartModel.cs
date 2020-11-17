using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerceModels.RequestModels.CartRequestModels
{
    public class AddToCartModel
    {
        public int? CartId { get; set; }
        public int ProductId { get; set; }
        public int ProductVariationId { get; set; }
        public int? Quantity { get; set; }
        public string OptionName { get; set; }
    }
}
