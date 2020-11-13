using ECommerceModels.Authentication;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ECommerceModels.Models
{
    public class ShoppingCart
    {
        [Key]
        public int Id { get; set; }
        public double TotalPrice { get; set; }
        
        public string UserId { get; set; }
        public ApplicationUser AppUser { get; set; }      
        public IList<CartProduct> CartProducts { get; set; }
    }
}
