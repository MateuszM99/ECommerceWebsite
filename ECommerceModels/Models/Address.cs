using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ECommerceModels.Models
{
    public class Address
    {
        [Key]
        public int AdresId { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Addres1 { get; set; }
        public string HouseNumber { get; set; }
        public string PostCode {get;set; }
    }
}
