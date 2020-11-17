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
        Task deleteProductAsync(DeleteProductModel deleteProductModel);
        Task<Product> editProductAsync(ProductDTO productModel,IFormFile productImage);
        Task<List<Product>> getAllProductsAsync();        
        Task<Product> getProductAsync(int productId);
        Task<List<Product>> filterProductsAsync(string productName,string categoryName, string sortType, string orderType, Size? size, float? priceFrom, float? priceTo);
        Task addCategoryAsync(CategoryDTO categoryModel);
        Task deleteCategoryAsync(CategoryDTO categoryModel);
        Task addCategoryToProduct(int productId, int categoryId);
        Task addOptionAsync(OptionDTO optionModel);
        Task deleteOptionAsync(OptionDTO optionModel);
        Task addOptionToProduct(int productId, int optionId);          
        Task addStockToProductOption(AddStockModel addStockModel);
    }
}
