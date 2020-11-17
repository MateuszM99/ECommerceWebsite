using ECommerceModels.DTOs;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerceModels.RequestModels.ProductRequestModels
{
    public class CreateProductModel
    { 
        public int? ProductId { get; set; }
        public String Name { get; set; }
        public string Price { get; set; }
        public string Description { get; set; }
        public string SKU { get; set; }
        public int? CategoryId { get; set; }
        public IFormFile ProductImage { get; set; }
    }
}
