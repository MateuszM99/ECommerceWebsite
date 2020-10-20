using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerceModels.Models
{
    public class DeliveryMethod
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }

        public IList<Order> Orders { get; set; }
    }
}
