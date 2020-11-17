using ECommerceModels.Responses;
using System.Collections.Generic;

namespace ECommerceModels.Models
{
    public class CartResponse : Response
    {
        public int? CartId { get; set; }
        public int? CartCount { get; set; }
        public double CartPrice { get; set; }
        public List<ProductOptionQuantity> CartProducts { get; set; }
    }
}
