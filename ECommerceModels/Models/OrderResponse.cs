using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerceModels.Models
{
    public class OrderResponse
    {
        public int OrderId { get; set; }
        public string ErrorMessage { get; set; }
        public string SuccessMessage { get; set; }
    }
}
