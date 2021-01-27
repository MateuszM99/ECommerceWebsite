using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerceModels.Models
{
    public class PasswordChangeModel
    {
       public string oldPassword { get; set; }
       public string newPassword { get; set; }
       public string newPasswordConfirm { get; set; }
    }
}
