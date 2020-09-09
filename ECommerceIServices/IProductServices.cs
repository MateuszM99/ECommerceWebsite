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
        Task DeleteProduct();
        Task EditProduct();
        List<Product> GetAllProducts();
        List<Product> GetCategoryProducts(int categoryId);
        Product GetProduct(int productId);
        List<Product> SearchProductsByName(string productName);

    }
}
