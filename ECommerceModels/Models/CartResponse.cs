using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerceModels.Models
{
    public class CartResponse
    {
        public int? CartId { get; set; }
        public string SuccessMessage { get; set; }
        public string ErrorMessage { get; set; }
    }
}
