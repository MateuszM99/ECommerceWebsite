using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ECommerceModels.Models
{
    public class GuestUser
    {
        [EmailAddress]
        public string Email { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        [Phone]
        public string Phone { get; set; }
    }
}
