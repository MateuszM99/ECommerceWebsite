using ECommerceModels.DTOs;
using ECommerceModels.Enums;
using ECommerceModels.Models;
using ECommerceModels.RequestModels.ProductRequestModels;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceIServices
{
    public interface IProductServices
    {
        Task createProductAsync(CreateProductModel createProductModel);
        Task deleteProductAsync(int productId);
        Task<Product> editProductAsync(ProductDTO productModel,IFormFile productImage);
        Task<List<Product>> getAllProductsAsync();        
        Task<Product> getProductAsync(int productId);
        Task<List<Product>> filterProductsAsync(string productName,string categoryName, string sortType, string orderType, Size? size, Color? color, float? priceFrom, float? priceTo);
        Task addCategoryAsync(CategoryDTO categoryModel);
        Task addCategoryToProduct(int productId, int categoryId);
        Task addOptionAsync(OptionDTO optionModel);
        Task addOptionToProduct(int productId, int optionId);
        Task addOptionGroupAsync(OptionGroupDTO optionGroupModel);      
        Task addStockToProductOption(AddStockModel addStockModel);
    }
}
