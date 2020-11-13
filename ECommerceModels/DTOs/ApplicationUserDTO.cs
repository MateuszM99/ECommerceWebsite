using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerceModels.DTOs
{
    public class ApplicationUserDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public AddressDTO Address { get; set; }

    }
}
