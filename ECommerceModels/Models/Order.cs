using ECommerceModels.Authentication;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ECommerceModels.Models
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }
        public string OrderStatus { get; set; }
        public double OrderPrice { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime ModifiedAt { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public IList<OrderItem> OrderItems { get; set; }
        public int AddressId { get; set; }
        public Address Address { get; set; }
    }
}
