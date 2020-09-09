using ECommerceData;
using ECommerceIServices;
using ECommerceModels.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceServices
{
    public class ProductSevices : IProductServices
    {
        private readonly ECommerceContext appDb;
        public ProductSevices(ECommerceContext appDb)
        {
            this.appDb = appDb;
        }
        public Task CreateProduct(Product productModel)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteProduct(int productId)
        {
            var product = await appDb.Products.FindAsync(productId);
            appDb.Products.Remove(product);
            await appDb.SaveChangesAsync();         
        }

        public Task EditProduct(Product productModel)
        {
            throw new NotImplementedException();
        }

        public List<Product> GetAllProducts()
        {
            return appDb.Products.ToList();
        }

        public List<Product> GetCategoryProducts(int categoryId)
        {
            return appDb.Products
                    .Where(p => p.CategoryId == categoryId)
                    .ToList();
        }

        public Product GetProduct(int productId)
        {
            return appDb.Products.Find(productId);
        }

        public List<Product> SearchProductsByName(string productName)
        {
            return appDb.Products
                        .Where(p => p.ProductName.ToLower().Contains(productName.ToLower()))
                        .ToList();
        }
    }
}
