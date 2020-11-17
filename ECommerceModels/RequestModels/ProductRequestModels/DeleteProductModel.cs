using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerceModels.RequestModels.ProductRequestModels
{
    public class DeleteProductModel
    {
        public int ProductId { get; set; }
        public int ProductVariationId { get; set; }
    }
}
