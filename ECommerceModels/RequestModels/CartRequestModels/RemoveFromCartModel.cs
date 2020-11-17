using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerceModels.RequestModels.CartRequestModels
{
    public class RemoveFromCartModel
    {
        public int CartId { get; set; }
        public int ProductId { get; set; }
        public int ProductVariationId { get; set; }
    }
}
