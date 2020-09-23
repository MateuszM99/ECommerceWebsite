using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerceModels.Models
{
    public class PaymentMethod
    {
        public int Id { get; set; }
        public string Name { get; set; }       
        public IList<Order> Orders { get; set; }
    }
}
