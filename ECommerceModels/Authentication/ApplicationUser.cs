using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using ECommerceModels.Models;
using Microsoft.AspNetCore.Identity;

namespace ECommerceModels.Authentication
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Address Address { get; set; }
    }
}
