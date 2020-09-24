using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ECommerceModels.Authentication
{
    public class EditUserModel
    {     
        public string FirstName { get; set; }
     
        public string LastName { get; set; }        

        [EmailAddress]     
        public string Email { get; set; }

        [Phone]       
        public string Phone { get; set; }
    }
}
