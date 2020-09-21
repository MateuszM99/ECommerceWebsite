using ECommerceData;
using ECommerceIServices;
using ECommerceModels.Enums;
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
    public class ProductServices : IProductServices
    {
        private readonly ECommerceContext appDb;
        public ProductServices(ECommerceContext appDb)
        {
            this.appDb = appDb;
        }

        public async Task AddCategory(Category categoryModel)
        {
            Category category = new Category()
            {
                CategoryName = categoryModel.CategoryName
            };

            await appDb.Categories.AddAsync(category);
            await appDb.SaveChangesAsync();
        }

        public async Task AddCategoryToProduct(int productId, int categoryId)
        {
            var product = await appDb.Products.FindAsync(productId);
            product.CategoryId = categoryId;
            await appDb.SaveChangesAsync();
        }

        public async Task AddOption(Option optionModel)
        {
            Option option = new Option()
            {
                OptionName = optionModel.OptionName,
                OptionGroupId = optionModel.OptionGroupId
            };

            await appDb.Options.AddAsync(option);
            await appDb.SaveChangesAsync();
        }

        public async Task AddOptionGroup(OptionGroup optionGroupModel)
        {
            OptionGroup optionGroup = new OptionGroup()
            {
                OptionGroupName = optionGroupModel.OptionGroupName
            };

            await appDb.OptionGroups.AddAsync(optionGroup);
            await appDb.SaveChangesAsync();
        }

        public async Task AddOptionToProduct(int productId, int optionId)
        {
            ProductOption productOption = new ProductOption()
            {
                ProductId = productId,
                OptionId = optionId
            };

            await appDb.ProductOption.AddAsync(productOption);
            await appDb.SaveChangesAsync();
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

        public List<Product> FilterProducts(string productName,string categoryName, string sortType, string orderType, Size? size, Color? color, float? priceFrom, float? priceTo)
        {
            IQueryable<Product> query = appDb.Products
                                           .Include(p => p.Category)
                                           .Include(p => p.ProductOptions)
                                                .ThenInclude(po => po.Option);

            if(productName != null)
            {
                query = query.Where(p => p.ProductName.ToLower().Contains(productName.ToLower()));                        
            }

            if (categoryName != null)
            {
                query = query.Where(p => 
                        p.Category.CategoryName == categoryName);
            }

            if (size != null)
            {
                query = query.Where(p => 
                        p.ProductOptions.Intersect(p.ProductOptions.Where(o => o.Option.OptionName == size.ToString()))
                        .Any());
            }

            if (color != null)
            {
                query = query.Where(p => 
                        p.ProductOptions.Intersect(p.ProductOptions.Where(o => o.Option.OptionName == color.ToString()))
                        .Any());
            }

            query = query.Where(p => 
                    p.ProductPrice >= priceFrom && p.ProductPrice <= priceTo);

            switch (sortType)
            {
                case "name":
                    if (orderType == null)
                        query = query.OrderBy(p => p.ProductName);
                    else if (orderType == "desc")
                        query = query.OrderByDescending(p => p.ProductName);
                    break;
                case "price":
                    if (orderType == null)
                        query = query.OrderBy(p => p.ProductPrice);
                    else if (orderType == "desc")
                        query = query.OrderByDescending(p => p.ProductPrice);
                    break;
                default:
                    break;
            }

            List<Product> products = query.ToList();

            return products;
        }

        public List<Product> GetAllProducts()
        {
            return appDb.Products
                        .Include(p => p.Category)
                        .Include(p => p.ProductOptions)
                            .ThenInclude(po => po.Option)
                        .ToList();
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
    }
}
