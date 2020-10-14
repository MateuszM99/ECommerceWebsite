using AutoMapper;
using ECommerceData;
using ECommerceIServices;
using ECommerceModels.DTOs;
using ECommerceModels.Enums;
using ECommerceModels.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
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

        public async Task AddCategory(Category categoryModel)
        {
            Category category = new Category()
            {
                Name = categoryModel.Name
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

        public async Task AddOptionAsync(OptionDTO optionModel)
        {
            logger.LogInformation($"Starting method {nameof(AddOptionAsync)}.");

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

            logger.LogInformation($"Finished method {nameof(AddOptionAsync)}.");
        }

        public async Task AddOptionGroupAsync(OptionGroupDTO optionGroupModel)
        {
            logger.LogInformation($"Starting method {nameof(AddOptionGroupAsync)}.");

            if (optionGroupModel == null)
            {
                var message = new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest)
                {
                    Content = new StringContent($"{nameof(optionGroupModel)} cannot be null")
                };
                throw new HttpResponseException(message);
            }

            if(optionGroupModel.Name == null)
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

            logger.LogInformation($"Finished method {nameof(AddOptionGroupAsync)}.");
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

        public async Task CreateProductAsync(ProductDTO productModel, IFormFile productImage)
        {
            logger.LogInformation($"Starting method {nameof(CreateProductAsync)}.");

            if (productModel == null)
            {
                var message = new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest)
                {
                    Content = new StringContent($"{nameof(productModel)} cannot be null")
                };
                throw new HttpResponseException(message);
            }

            if (productModel.Name == null || productModel.Description == null || productModel.SKU == null || productModel.Options == null)
            {
                var message = new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest)
                {
                    Content = new StringContent($"{nameof(productModel)} is missing some required values")
                };
                throw new HttpResponseException(message);
            }
           
            var productExists = await appDb.Products.AnyAsync(x => x.Name == productModel.Name && x.SKU == productModel.SKU);

            if (productExists)
            {
                var message = new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest)
                {
                    Content = new StringContent($"There already is an element inside the database as the one to be added {nameof(productModel.Name)} and {nameof(productModel.SKU)} are not unique")
                };
                throw new HttpResponseException(message);
            }
            
            var product = mapper.Map<Product>(productModel);

            await appDb.Products.AddAsync(product);
            await appDb.SaveChangesAsync();

            if(productImage == null)
            {
                var message = new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest)
                {
                    Content = new StringContent($"{nameof(productImage)} cannot be null, check if you correctltly attached the image")
                };
                throw new HttpResponseException(message);
            }

            await uploadServices.UploadProductPhotoAsync(productImage, product);

            logger.LogInformation($"Finished method {nameof(CreateProductAsync)}.");
        }

        public async Task DeleteProductAsync(int productId)
        {
            logger.LogInformation($"Starting method {nameof(DeleteProductAsync)}.");
          
            var product = await appDb.Products.FindAsync(productId);

            if(product == null)
            {
                var message = new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest)
                {
                    Content = new StringContent($"The product with given id {nameof(productId)} cannot be removed beacause it doesn't exist in database, please check if the given id is correct")
                };
                throw new HttpResponseException(message);
            }

            appDb.Products.Remove(product);
            await appDb.SaveChangesAsync();

            logger.LogInformation($"Finished method {nameof(DeleteProductAsync)}.");
        }

        public async Task EditProductAsync(ProductDTO productModel,IFormFile productImage)
        {
            logger.LogInformation($"Starting method {nameof(EditProductAsync)}.");

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

            logger.LogInformation($"Finished method {nameof(EditProductAsync)}.");
        }

        public async Task<List<Product>> FilterProductsAsync(string productName,string categoryName, string sortType, string orderType, Size? size, Color? color, float? priceFrom, float? priceTo)
        {
            IQueryable<Product> query = appDb.Products
                                           .Include(p => p.Category)
                                           .Include(p => p.ProductOptions)
                                                .ThenInclude(po => po.Option);

            if(productName != null)
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

        public async Task<List<Product>> GetAllProductsAsync()
        {
            logger.LogInformation($"Starting method {nameof(GetAllProductsAsync)}.");

            logger.LogInformation($"Finished method {nameof(GetAllProductsAsync)}.");

            return await appDb.Products
                        .Include(p => p.Category)
                        .Include(p => p.ProductOptions)
                            .ThenInclude(po => po.Option)
                        .ToListAsync();
        }
       
        public async Task<Product> GetProductAsync(int productId)
        {
            logger.LogInformation($"Starting method {nameof(GetProductAsync)}.");

            var product = await appDb.Products
                                .Include(p => p.Category)
                                .Include(p => p.ProductOptions)
                                    .ThenInclude(po => po.Option)
                                .SingleAsync(p => p.Id == productId);

            if (product == null)
            {
                var message = new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest)
                {
                    Content = new StringContent($"The product with given id {nameof(productId)} was not found in database, please check if the given id is correct")
                };
                throw new HttpResponseException(message);
            }

            logger.LogInformation($"Finished method {nameof(GetProductAsync)}.");
            return product;
        }
    }
}
