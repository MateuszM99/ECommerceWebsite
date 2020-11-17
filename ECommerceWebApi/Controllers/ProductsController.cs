using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using AutoMapper;
using ECommerceData;
using ECommerceIServices;
using ECommerceModels.Authentication;
using ECommerceModels.DTOs;
using ECommerceModels.Enums;
using ECommerceModels.Models;
using ECommerceModels.RequestModels.ProductRequestModels;
using ECommerceModels.Responses;
using EllipticCurve.Utils;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ECommerceWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ILogger<ProductsController> logger;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ECommerceContext appDb;
        private readonly IProductServices productServices;
        private readonly IUploadServices uploadServices;
        private readonly IMapper mapper;
        public ProductsController(ILogger<ProductsController> logger, UserManager<ApplicationUser> userManager, ECommerceContext appDb, IProductServices productServices, IUploadServices uploadServices,IMapper mapper)
        {
            this.logger = logger;
            this.userManager = userManager;
            this.appDb = appDb;
            this.productServices = productServices;
            this.uploadServices = uploadServices;
            this.mapper = mapper;
        }

        [EnableCors("Policy")]
        [HttpPost]
        [Route("addProduct")]
        public async Task<IActionResult> CreateProduct([FromForm]CreateProductModel createProductModel)
        {
            logger.LogInformation($"Starting method {nameof(CreateProduct)}.");  
            try
            {               
                await productServices.createProductAsync(createProductModel);

                logger.LogInformation($"Finished method {nameof(CreateProduct)}");
                return Ok(new ProductResponse
                {
                    Message = "Succesfully added product"
                });
            } 
            catch(System.Web.Http.HttpResponseException ex)
            {
                logger.LogError($"{ex.Message}");
                throw;
            }
        }

        [EnableCors("Policy")]
        [HttpPost]
        [Route("deleteProduct")]
        public async Task<IActionResult> DeleteProduct(DeleteProductModel deleteProductModel)
        {
            logger.LogInformation($"Starting method {nameof(DeleteProduct)}.");
            try
            {
                await productServices.deleteProductAsync(deleteProductModel);

                logger.LogInformation($"Finished method {nameof(DeleteProduct)}");
                return Ok(new ProductResponse
                {
                    Message = "Succesfully deleted product"
                });
            }
            catch(System.Web.Http.HttpResponseException ex)
            {
                logger.LogError($"{ex.Message}");
                throw;
            }
        }
        
        /*
        [HttpPost]
        [Route("editProduct")]
        public async Task<IActionResult> EditProduct([ModelBinder(BinderType = typeof(JsonModelBinder))] ProductDTO productModel, IFormFile productImage)
        {
            logger.LogInformation($"Starting method {nameof(EditProduct)}.");
            try
            {
               Product product = await productServices.editProductAsync(productModel, productImage);

               var productDTO = mapper.Map<ProductDTO>(product);

               logger.LogInformation($"Finished method {nameof(EditProduct)}");
               return Ok(productDTO);
            }
            catch(System.Web.Http.HttpResponseException ex)
            {
                logger.LogError($"{ex.Message}");
                throw;
            }
        }*/

        [EnableCors("Policy")]
        [HttpGet]
        [Route("getProducts")]
        public async Task<IActionResult> GetProducts()
        {           
            logger.LogInformation($"Starting method {nameof(GetProducts)}.");
            
            try
            {
               var products = await productServices.getAllProductsAsync();

               var productsDTO = mapper.Map<List<ProductDTO>>(products);

               logger.LogInformation($"Finished method {nameof(GetProducts)}");
               return Ok(productsDTO);
            }
            catch (System.Web.Http.HttpResponseException ex)
            {
               logger.LogError($"{ex.Message}");
               throw;
            }
        }
        
        [EnableCors("Policy")]
        [HttpGet]
        [Route("getProduct")]
        public async Task<IActionResult> GetProduct(int productId)
        {
            logger.LogInformation($"Starting method {nameof(GetProduct)}.");

            try
            {
                var product = await productServices.getProductAsync(productId);

                var productDTO = mapper.Map<ProductDTO>(product);

                logger.LogInformation($"Finished method {nameof(GetProduct)}");
                return Ok(productDTO);
            }
            catch (System.Web.Http.HttpResponseException ex)
            {
                logger.LogError($"{ex.Message}");
                throw;
            }
        }
      
        [EnableCors("Policy")]
        [HttpGet]
        [Route("products")]
        public async Task<IActionResult> FilterProducts(string productName,string categoryName,string sortType,string orderType,Size? size,float? priceFrom=0,float? priceTo=99999)
        {
            logger.LogInformation($"Starting method {nameof(FilterProducts)}.");

            try
            {
                List<Product> products = await productServices.filterProductsAsync(productName, categoryName, sortType, orderType, size, priceFrom, priceTo);

                var productsDTO = mapper.Map<List<ProductDTO>>(products);

                logger.LogInformation($"Finished method {nameof(FilterProducts)}.");

                return Ok(productsDTO);
            }
            catch (System.Web.Http.HttpResponseException ex)
            {
                logger.LogError($"{ex.Message}");
                throw;
            }
            
        }

        [EnableCors("Policy")]
        [HttpPost]
        [Route("addCategory")]
        public async Task<IActionResult> AddCategory([FromBody]CategoryDTO categoryModel)
        {
            logger.LogInformation($"Starting method {nameof(AddCategory)}.");

            try
            {
                await productServices.addCategoryAsync(categoryModel);

                logger.LogInformation($"Finished method {nameof(AddCategory)}.");

                return Ok(new ProductResponse
                {
                    Message = "Category added succesfully"
                });
            }
            catch (System.Web.Http.HttpResponseException ex)
            {
                logger.LogError($"{ex.Message}");
                throw;
            }
        }

        [EnableCors("Policy")]
        [HttpPost]
        [Route("deleteCategory")]
        public async Task<IActionResult> DeleteCategory([FromBody]CategoryDTO categoryModel)
        {
            logger.LogInformation($"Starting method {nameof(DeleteCategory)}.");

            try
            {
                await productServices.deleteCategoryAsync(categoryModel);

                logger.LogInformation($"Finished method {nameof(DeleteCategory)}.");

                return Ok(new ProductResponse
                {
                    Message = "Category deleted succesfully"
                });
            }
            catch (System.Web.Http.HttpResponseException ex)
            {
                logger.LogError($"{ex.Message}");
                throw;
            }
        }

        [HttpPost]
        [Route("addCategoryTo")]
        public async Task<IActionResult> AddCategoryTo(int productId, int categoryId)
        {
            await productServices.addCategoryToProduct(productId, categoryId);

            return Ok();
        }

        [EnableCors("Policy")]
        [HttpGet]
        [Route("getCategories")]
        public async Task<IActionResult> GetCategories()
        {
            var categories = await appDb.Categories.ToListAsync();
            var categoriesDTO = mapper.Map<List<CategoryDTO>>(categories);

            return Ok(categoriesDTO);
        }

        [EnableCors("Policy")]
        [HttpPost]
        [Route("addOption")]
        public async Task<IActionResult> AddOption([FromBody]OptionDTO optionModel)
        {
            logger.LogInformation($"Starting method {nameof(AddOption)}.");

            try
            {
                await productServices.addOptionAsync(optionModel);

                logger.LogInformation($"Finished method {nameof(AddOption)}.");

                return Ok(new ProductResponse
                {
                    Message = "Option created succesfully"
                });
            }
            catch (System.Web.Http.HttpResponseException ex)
            {
                logger.LogError($"{ex.Message}");
                throw;
            }
        }

        [EnableCors("Policy")]
        [HttpPost]
        [Route("deleteOption")]
        public async Task<IActionResult> DeleteOption([FromBody]OptionDTO optionModel)
        {
            logger.LogInformation($"Starting method {nameof(DeleteOption)}.");

            try
            {
                await productServices.deleteOptionAsync(optionModel);

                logger.LogInformation($"Finished method {nameof(DeleteOption)}.");

                return Ok(new ProductResponse
                {
                    Message = "Option deleted succesfully"
                });
            }
            catch (System.Web.Http.HttpResponseException ex)
            {
                logger.LogError($"{ex.Message}");
                throw;
            }
        }

        [EnableCors("Policy")]
        [HttpPost]
        [Route("addOptionToProduct")]
        public async Task<IActionResult> AddOptionToProduct(int productId, int optionId)
        {
            await productServices.addOptionToProduct(productId, optionId);

            return Ok();
        }

        [EnableCors("Policy")]
        [HttpGet]
        [Route("getOptions")]
        public async Task<IActionResult> GetAllOptions()
        {
            logger.LogInformation($"Starting method {nameof(GetAllOptions)}.");

            try
            {
                var options = await appDb.Options.ToListAsync();

                var optionsDTO = mapper.Map<List<OptionDTO>>(options);

                logger.LogInformation($"Finished method {nameof(GetAllOptions)}.");

                return Ok(optionsDTO);
            }
            catch (System.Web.Http.HttpResponseException ex)
            {
                logger.LogError($"{ex.Message}");
                throw;
            }
        }
      
        [EnableCors("Policy")]
        [HttpPost]
        [Route("addStockToProductOption")]
        public async Task<IActionResult> AddStockToProductOption([FromBody]AddStockModel addStockModel)
        {
            await productServices.addStockToProductOption(addStockModel);

            return Ok(new ProductResponse
            {
                Message = "Succesfully added stock"
            });
        }
                     
    }
}