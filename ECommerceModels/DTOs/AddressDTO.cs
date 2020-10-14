using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerceModels.DTOs
{
   public class AddressDTO
    {
        public int AdresId { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string HouseNumber { get; set; }
        public string PostCode { get; set; }
    }
}
