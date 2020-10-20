using ECommerceModels.Authentication;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ECommerceModels.Models
{
    public class Address
    {
        [Key]
        public int Id { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string HouseNumber { get; set; }
        public string PostCode {get;set; }
        
        public IList<ApplicationUser> ApplicationUsers { get; set; }
        public IList<Order> Orders { get; set; }
    }
}
