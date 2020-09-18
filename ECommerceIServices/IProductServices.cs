﻿using ECommerceModels.Enums;
using ECommerceModels.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceIServices
{
    public interface IProductServices
    {
        Task CreateProduct(Product productModel);
        Task DeleteProduct(int productId);
        Task EditProduct(Product productModel);
        List<Product> GetAllProducts();
        List<Product> GetCategoryProducts(int categoryId);
        Product GetProduct(int productId);
        List<Product> SearchProductsByName(string productName);
        List<Product> FilterProducts(string categoryName, string sortType, string orderType, Size? size, Color? color, float? priceFrom, float? priceTo);
        Task AddOptionGroup(OptionGroup optionGroupModel);
        Task AddOption(Option optionModel);
        Task AddOptionToProduct(int productId, int optionId);
        Task AddCategory(Category categoryModel);
        Task AddCategoryToProduct(int productId, int categoryId);
    }
}
