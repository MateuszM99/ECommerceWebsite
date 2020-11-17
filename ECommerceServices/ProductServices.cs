using AutoMapper;
using ECommerceData;
using ECommerceIServices;
using ECommerceModels.DTOs;
using ECommerceModels.Enums;
using ECommerceModels.Models;
using ECommerceModels.RequestModels.ProductRequestModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace ECommerceServices
{
    public class ProductServices : IProductServices
    {
        private readonly ILogger<ProductServices> logger;
        private readonly ECommerceContext appDb;
        private readonly IMapper mapper;
        private readonly IUploadServices uploadServices;
        public ProductServices(ILogger<ProductServices> logger, ECommerceContext appDb, IMapper mapper, IUploadServices uploadServices)
        {
            this.logger = logger;
            this.appDb = appDb;
            this.mapper = mapper;
            this.uploadServices = uploadServices;
        }

        public async Task createProductAsync(CreateProductModel createProductModel)
        {
            logger.LogInformation($"Starting method {nameof(createProductAsync)}.");

            if (createProductModel == null)
            {
                var message = new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest)
                {
                    Content = new StringContent($"{nameof(createProductModel)} cannot be null")
                };
                throw new HttpResponseException(message);
            }

            if (createProductModel.ProductImage == null)
            {
                var message = new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest)
                {
                    Content = new StringContent($"{nameof(createProductModel.ProductImage)} cannot be null, check if you correctltly attached the image")
                };
                throw new HttpResponseException(message);
            }

            if (createProductModel.Name == null || createProductModel.Price == null || createProductModel.SKU == null || createProductModel.CategoryId == null)
            {
                var message = new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest)
                {
                    Content = new StringContent($"{nameof(createProductModel)} is missing some required values")
                };
                throw new HttpResponseException(message);
            }

            var productExists = await appDb.Products.AnyAsync(x => x.Name == createProductModel.Name && x.SKU == createProductModel.SKU);

            if (productExists)
            {
                var message = new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest)
                {
                    Content = new StringContent($"There already is an element inside the database as the one to be added {nameof(createProductModel.Name)} and {nameof(createProductModel.SKU)} are not unique")
                };
                throw new HttpResponseException(message);
            }



            var product = mapper.Map<Product>(createProductModel);
            product.AddedAt = DateTime.Now;

            await appDb.Products.AddAsync(product);
            await appDb.SaveChangesAsync();

            await uploadServices.UploadProductPhotoAsync(createProductModel.ProductImage, product);

            logger.LogInformation($"Finished method {nameof(createProductAsync)}.");
        }

        public async Task deleteProductAsync(int productId)
        {
            logger.LogInformation($"Starting method {nameof(deleteProductAsync)}.");

            var product = await appDb.Products.FindAsync(productId);

            if (product == null)
            {
                var message = new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest)
                {
                    Content = new StringContent($"The product with given id {nameof(productId)} cannot be removed beacause it doesn't exist in database, please check if the given id is correct")
                };
                throw new HttpResponseException(message);
            }

            appDb.Products.Remove(product);
            await appDb.SaveChangesAsync();

            logger.LogInformation($"Finished method {nameof(deleteProductAsync)}.");
        }

        public async Task<Product> editProductAsync(ProductDTO productModel, IFormFile productImage)
        {
            logger.LogInformation($"Starting method {nameof(editProductAsync)}.");

            if (productModel == null)
            {
                var message = new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest)
                {
                    Content = new StringContent($"{nameof(productModel)} cannot be null")
                };
                throw new HttpResponseException(message);
            }

            Product product = await appDb.Products.FindAsync(productModel.Id);

            if (product == null)
            {
                var message = new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest)
                {
                    Content = new StringContent($"The product with given id {nameof(productModel.Id)} cannot be edited beacause it doesn't exist in database, please check if the given id is correct")
                };
                throw new HttpResponseException(message);
            }

            mapper.Map(productModel, product);

            await appDb.SaveChangesAsync();

            if (productImage != null)
            {
                await uploadServices.UploadProductPhotoAsync(productImage, product);
            }

            logger.LogInformation($"Finished method {nameof(editProductAsync)}.");

            return product;
        }

        public async Task<List<Product>> getAllProductsAsync()
        {
            logger.LogInformation($"Starting method {nameof(getAllProductsAsync)}.");

            var products = await appDb.Products
                        .Include(p => p.Category)
                        .Include(p => p.ProductOptions)
                            .ThenInclude(po => po.Option)
                        .ToListAsync();

            if (products == null)
            {
                var message = new HttpResponseMessage(System.Net.HttpStatusCode.NotFound)
                {
                    Content = new StringContent($"There are no products in the database")
                };
                throw new HttpResponseException(message);
            }

            logger.LogInformation($"Finished method {nameof(getAllProductsAsync)}.");

            return products;
        }

        public async Task<Product> getProductAsync(int productId)
        {
            logger.LogInformation($"Starting method {nameof(getProductAsync)}.");

            var product = await appDb.Products
                                .Include(p => p.Category)
                                .Include(p => p.ProductOptions)
                                    .ThenInclude(po => po.Option)
                                .SingleOrDefaultAsync(p => p.Id == productId);

            if (product == null)
            {
                var message = new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest)
                {
                    Content = new StringContent($"The product with given id {nameof(productId)} was not found in database, please check if the given id is correct")
                };
                throw new HttpResponseException(message);
            }

            logger.LogInformation($"Finished method {nameof(getProductAsync)}.");
            return product;
        }

        public async Task<List<Product>> filterProductsAsync(string productName, string categoryName, string sortType, string orderType, Size? size, Color? color, float? priceFrom, float? priceTo)
        {
            IQueryable<Product> query = appDb.Products
                                           .Include(p => p.Category)
                                           .Include(p => p.ProductOptions)
                                                .ThenInclude(po => po.Option);

            if (productName != null)
            {
                query = query.Where(p => p.Name.ToLower().Contains(productName.ToLower()));
            }

            if (categoryName != null)
            {
                query = query.Where(p =>
                        p.Category.Name == categoryName);
            }

            if (size != null)
            {
                query = query.Where(p =>
                        p.ProductOptions.Intersect(p.ProductOptions.Where(o => o.Option.Name == size.ToString()))
                        .Any());
            }

            if (color != null)
            {
                query = query.Where(p =>
                        p.ProductOptions.Intersect(p.ProductOptions.Where(o => o.Option.Name == color.ToString()))
                        .Any());
            }

            query = query.Where(p =>
                    p.Price >= priceFrom && p.Price <= priceTo);

            switch (sortType)
            {
                case "name":
                    if (orderType == null)
                        query = query.OrderBy(p => p.Name);
                    else if (orderType == "desc")
                        query = query.OrderByDescending(p => p.Name);
                    break;
                case "price":
                    if (orderType == null)
                        query = query.OrderBy(p => p.Price);
                    else if (orderType == "desc")
                        query = query.OrderByDescending(p => p.Price);
                    break;
                default:
                    break;
            }

            List<Product> products = await query.ToListAsync();

            if (products == null)
            {
                var message = new HttpResponseMessage(System.Net.HttpStatusCode.NotFound)
                {
                    Content = new StringContent($"No products with given parameters were found in the database")
                };
                throw new HttpResponseException(message);
            }

            return products;
        }

        public async Task addCategoryAsync(CategoryDTO categoryModel)
        {
            logger.LogInformation($"Starting method {nameof(addCategoryAsync)}.");

            if (categoryModel == null)
            {
                var message = new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest)
                {
                    Content = new StringContent($"{nameof(categoryModel)} cannot be null")
                };
                throw new HttpResponseException(message);
            }

            if (categoryModel.Name == null)
            {
                var message = new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest)
                {
                    Content = new StringContent($"{nameof(categoryModel)} is missing some required values")
                };
                throw new HttpResponseException(message);
            }

            var categoryExists = await appDb.Categories.Where(x => x.Name == categoryModel.Name).AnyAsync();

            if (categoryExists)
            {
                var message = new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest)
                {
                    Content = new StringContent($"There already is an element inside the database as the one to be added {nameof(categoryModel.Name)} is not unique")
                };
                throw new HttpResponseException(message);
            }

            var category = mapper.Map<Category>(categoryModel);

            await appDb.Categories.AddAsync(category);
            await appDb.SaveChangesAsync();

            logger.LogInformation($"Finished method {nameof(addCategoryAsync)}.");
        }

        public async Task addCategoryToProduct(int productId, int categoryId)
        {
            var product = await appDb.Products.FindAsync(productId);
            product.CategoryId = categoryId;
            await appDb.SaveChangesAsync();
        }

        public async Task addOptionAsync(OptionDTO optionModel)
        {
            logger.LogInformation($"Starting method {nameof(addOptionAsync)}.");

            if (optionModel == null)
            {
                var message = new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest)
                {
                    Content = new StringContent($"{nameof(optionModel)} cannot be null")
                };
                throw new HttpResponseException(message);
            }

            if (optionModel.Name == null)
            {
                var message = new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest)
                {
                    Content = new StringContent($"{nameof(optionModel)} is missing some required values")
                };
                throw new HttpResponseException(message);
            }

            var optionExists = await appDb.Options.Where(x => x.Name == optionModel.Name).AnyAsync();

            if (optionExists)
            {
                var message = new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest)
                {
                    Content = new StringContent($"There already is an element inside the database as the one to be added {nameof(optionModel.Name)} is not unique")
                };
                throw new HttpResponseException(message);
            }

            var option = mapper.Map<Option>(optionModel);

            await appDb.Options.AddAsync(option);
            await appDb.SaveChangesAsync();

            logger.LogInformation($"Finished method {nameof(addOptionAsync)}.");
        }
      
        public async Task addOptionToProduct(int productId, int optionId)
        {
            ProductOption productOption = new ProductOption()
            {
                ProductId = productId,
                OptionId = optionId
            };

            await appDb.ProductOption.AddAsync(productOption);
            await appDb.SaveChangesAsync();
        }

        public async Task addStockToProductOption(AddStockModel addStockModel)
        {
            var productOption = await appDb.ProductOption.FindAsync(addStockModel.OptionId, addStockModel.ProductId);

            productOption.ProductStock = addStockModel.Stock;

            await appDb.SaveChangesAsync();
        }

        public async Task addOptionGroupAsync(OptionGroupDTO optionGroupModel)
        {
            logger.LogInformation($"Starting method {nameof(addOptionGroupAsync)}.");

            if (optionGroupModel == null)
            {
                var message = new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest)
                {
                    Content = new StringContent($"{nameof(optionGroupModel)} cannot be null")
                };
                throw new HttpResponseException(message);
            }

            if (optionGroupModel.Name == null)
            {
                var message = new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest)
                {
                    Content = new StringContent($"{nameof(optionGroupModel)} is missing some required values")
                };
                throw new HttpResponseException(message);
            }

            var optionGroupExists = await appDb.OptionGroups.Where(x => x.Name == optionGroupModel.Name).AnyAsync();

            if (optionGroupExists)
            {
                var message = new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest)
                {
                    Content = new StringContent($"There already is an element inside the database as the one to be added {nameof(optionGroupModel.Name)} is not unique")
                };
                throw new HttpResponseException(message);
            }

            var optionGroup = mapper.Map<OptionGroup>(optionGroupModel);

            await appDb.OptionGroups.AddAsync(optionGroup);
            await appDb.SaveChangesAsync();

            logger.LogInformation($"Finished method {nameof(addOptionGroupAsync)}.");
        }


    }
}
