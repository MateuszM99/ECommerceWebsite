using ECommerceModels.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerceModels.DTOs
{
    public class ProductDTO
    {
        public int? Id { get; set; }
        public int? VariationId { get; set; }
        public string Name { get; set; }
        public float? Price { get; set; }
        public string Description { get; set; }
        public string SKU { get; set; }
        public string ImageUrl { get; set; }
        public int? CategoryId { get; set; }
        public string CategoryName { get; set; }
        public List<OptionDTO> Options { get; set; }
    }
}
