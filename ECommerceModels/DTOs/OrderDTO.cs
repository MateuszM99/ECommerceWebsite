using ECommerceModels.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerceModels.DTOs
{
    public class OrderDTO
    {
        public int? Id { get; set; }
        public OrderStatus Status { get; set; }
        public double Price { get; set; }
        public DateTime AddedAt { get; set; }
        public DateTime ModifiedAt { get; set; }      
        public List<ProductDTO> Products { get; set; }       
        public AddressDTO Address { get; set; }
        public string ClientEmail { get; set; }
        public string ClientName { get; set; }
        public string ClientSurname { get; set; }
        public string ClientPhone { get; set; }
        public int? DeliveryMethodId { get; set; }       
        public int? PaymentMethodId { get; set; }     
        public int? CartId { get; set; }
        public bool isConfirmed { get; set; }
    }
}
