using ECommerceModels.Authentication;
using ECommerceModels.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ECommerceModels.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }
        public OrderStatus Status { get; set; }
        public double Price { get; set; }
        public DateTime AddedAt { get; set; }
        public DateTime ModifiedAt { get; set; }        
        public string ClientEmail { get; set; }
        public string ClientName { get; set; }
        public string ClientSurname { get; set; }
        public string ClientPhone { get; set; }      
        public bool isConfirmed { get; set; }
        [NotMapped]
        public string token { get; set; }

        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public IList<OrderProduct> Products { get; set; }
        public int AddressId { get; set; }
        public Address Address { get; set; }
        public int DeliveryMethodId { get; set; }
        public DeliveryMethod DeliveryMethod { get; set; }
        public int PaymentMethodId { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
    }
}
