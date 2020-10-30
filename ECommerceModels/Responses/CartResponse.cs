using ECommerceModels.Responses;


namespace ECommerceModels.Models
{
    public class CartResponse : Response
    {
        public int? CartId { get; set; }
        public int? CartCount { get; set; }
        public double CartPrice { get; set; }
      
    }
}
