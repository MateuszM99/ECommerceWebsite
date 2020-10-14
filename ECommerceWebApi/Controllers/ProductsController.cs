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
using ECommerceModels.SpecificationPattern;
using ECommerceWebApi.ModlBinder;
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

        [HttpPost]
        [Route("addProduct")]
        public async Task<IActionResult> CreateProduct([ModelBinder(BinderType = typeof(JsonModelBinder))] ProductDTO productModel,IFormFile productImage)
        {
            logger.LogInformation($"Starting method {nameof(CreateProduct)}.");  
            try
            {
                await productServices.CreateProductAsync(productModel,productImage);

                logger.LogInformation($"Finished method {nameof(CreateProduct)}");
                return Ok();
            } 
            catch(System.Web.Http.HttpResponseException ex)
            {
                logger.LogError($"{ex.Message}");
                throw;
            }
        }

        [HttpPost]
        [Route("deleteProduct")]
        public async Task<IActionResult> DeleteProduct(int productId)
        {
            logger.LogInformation($"Starting method {nameof(DeleteProduct)}.");
            try
            {
                await productServices.DeleteProductAsync(productId);

                logger.LogInformation($"Finished method {nameof(DeleteProduct)}");
                return Ok();
            }
            catch(System.Web.Http.HttpResponseException ex)
            {
                logger.LogError($"{ex.Message}");
                throw;
            }
        }

        [HttpPost]
        [Route("editProduct")]
        public async Task<IActionResult> EditProduct([ModelBinder(BinderType = typeof(JsonModelBinder))] ProductDTO productModel, IFormFile productImage)
        {
            logger.LogInformation($"Starting method {nameof(EditProduct)}.");
            try
            {
                await productServices.EditProductAsync(productModel, productImage);

                logger.LogInformation($"Finished method {nameof(EditProduct)}");
                return Ok();
            }
            catch(System.Web.Http.HttpResponseException ex)
            {
                logger.LogError($"{ex.Message}");
                throw;
            }
        }



        [EnableCors("Policy")]
        [HttpGet]
        [Route("getProducts")]
        public async Task<IActionResult> GetProducts()
        {           
            logger.LogInformation($"Starting method {nameof(GetProducts)}.");
            
            try
            {
               var products = await productServices.GetAllProductsAsync();

               var productsDTO = mapper.Map<ProductDTO>(products);

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
                var product = await productServices.GetProductAsync(productId);

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
        [Route("getCategories")]
        public List<Category> GetCategories()
        {
            return appDb.Categories.ToList();
        }

        [EnableCors("Policy")]
        [HttpGet]
        [Route("products")]
        public async Task<IActionResult> FilterProducts(string productName,string categoryName,string sortType,string orderType,Size? size,Color? color,float? priceFrom=0,float? priceTo=99999)
        {
            logger.LogInformation($"Starting method {nameof(FilterProducts)}.");

            try
            {
                List<Product> products = await productServices.FilterProductsAsync(productName, categoryName, sortType, orderType, size, color, priceFrom, priceTo);

                var productsDTO = mapper.Map<ProductDTO>(products);

                logger.LogInformation($"Finished method {nameof(FilterProducts)}.");

                return Ok(productsDTO);
            }
            catch (System.Web.Http.HttpResponseException ex)
            {
                logger.LogError($"{ex.Message}");
                throw;
            }
            
        }

       

        [HttpPost]
        [Route("addOptionGroup")]
        public async Task<IActionResult> AddOptionGroup([FromBody]OptionGroupDTO optionGroupModel)
        {
            logger.LogInformation($"Starting method {nameof(AddOptionGroup)}.");

            try
            {
                await productServices.AddOptionGroupAsync(optionGroupModel);

                logger.LogInformation($"Finished method {nameof(AddOptionGroup)}.");

                return Ok();
            }
            catch (System.Web.Http.HttpResponseException ex)
            {
                logger.LogError($"{ex.Message}");
                throw;
            }
        }

        [HttpPost]
        [Route("addOptionToProduct")]
        public async Task<IActionResult> AddOptionToProduct(int productId,int optionId)
        {
            await productServices.AddOptionToProduct(productId, optionId);

            return Ok();
        }

        [HttpPost]
        [Route("addOption")]
        public async Task<IActionResult> AddOption([FromBody]OptionDTO optionModel)
        {
            logger.LogInformation($"Starting method {nameof(AddOption)}.");

            try
            {
                await productServices.AddOptionAsync(optionModel);

                logger.LogInformation($"Finished method {nameof(AddOption)}.");

                return Ok();
            }
            catch (System.Web.Http.HttpResponseException ex)
            {
                logger.LogError($"{ex.Message}");
                throw;
            }
        }

        [HttpPost]
        [Route("addCategory")]
        public async Task<IActionResult> AddCategory([FromBody]Category categoryModel)
        {
            if (categoryModel == null)
                return StatusCode(StatusCodes.Status204NoContent);

            await productServices.AddCategory(categoryModel);

            return Ok();
        }

        [HttpPost]
        [Route("addCategoryTo")]
        public async Task<IActionResult> AddCategoryTo(int productId,int categoryId)
        {
            await productServices.AddCategoryToProduct(productId, categoryId);

            return Ok();
        }

        [HttpPost]
        [Route("addImage")]
        public async Task<IActionResult> AddImage(IFormFile imageFile,int productId)
        {
            

            var file = HttpContext.Request.Form.Files[0];

            var product = await appDb.Products.FindAsync(productId);

            await uploadServices.UploadProductPhotoAsync(file, product);

            return Ok();
        }


    }
}