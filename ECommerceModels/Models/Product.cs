using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ECommerceModels.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public float Price { get; set; }
        public string Description { get; set; }
        public string SKU { get; set; }
        public string ImageUrl { get; set; }
        [ForeignKey("Standard")]
        public int? CategoryId { get; set; }       
        public Category Category { get; set; }
        public DateTime AddedAt { get; set; }
        public IList<CartProduct> CartProducts { get; set; }
        public IList<ProductOption> ProductOptions { get; set; }
        public IList<OrderItem> OrderItems { get; set; }
    }
}
