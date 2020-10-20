using ECommerceModels.DTOs;
using ECommerceModels.Enums;
using ECommerceModels.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceIServices
{
    public interface IProductServices
    {
        Task CreateProductAsync(ProductDTO productModel, IFormFile productImage);
        Task DeleteProductAsync(int productId);
        Task<Product> EditProductAsync(ProductDTO productModel,IFormFile productImage);
        Task<List<Product>> GetAllProductsAsync();        
        Task<Product> GetProductAsync(int productId);
        Task<List<Product>> FilterProductsAsync(string productName,string categoryName, string sortType, string orderType, Size? size, Color? color, float? priceFrom, float? priceTo);
        Task AddOptionGroupAsync(OptionGroupDTO optionGroupModel);
        Task AddOptionAsync(OptionDTO optionModel);
        Task AddOptionToProduct(int productId, int optionId);
        Task AddCategoryAsync(CategoryDTO categoryModel);
        Task AddCategoryToProduct(int productId, int categoryId);
    }
}
