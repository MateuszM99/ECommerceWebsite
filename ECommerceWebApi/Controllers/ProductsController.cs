using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using ECommerceData;
using ECommerceIServices;
using ECommerceModels.Authentication;
using ECommerceModels.Enums;
using ECommerceModels.Models;
using ECommerceModels.SpecificationPattern;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ECommerceWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ECommerceContext appDb;
        private readonly IProductServices productServices;
        public ProductsController(UserManager<ApplicationUser> userManager, ECommerceContext appDb, IProductServices productServices)
        {
            this.userManager = userManager;
            this.appDb = appDb;
            this.productServices = productServices;
        }

        [HttpPost]
        [Route("addProduct")]
        public async Task<IActionResult> AddProduct([FromBody]Product productModel)
        {
            if (productModel == null)
                return StatusCode(StatusCodes.Status204NoContent);

            var product = await appDb.Products.FindAsync(productModel.ProductId);

            if (product != null)
                return StatusCode(StatusCodes.Status405MethodNotAllowed);

            product = new Product
            {               
                ProductName = productModel.ProductName,
                ProductDescription = productModel.ProductDescription,
                ProductPrice = productModel.ProductPrice,
                AddedAt = DateTime.Now
            };

            await appDb.Products.AddAsync(product);
            await appDb.SaveChangesAsync();

            return Ok();
        }

        [EnableCors("Policy")]
        [HttpGet]
        [Route("getProducts")]
        public List<Product> GetProducts()
        {
            return productServices.GetAllProducts();
        }

        [HttpGet]
        [Route("products")]
        public List<Product> FilterProducts(string categoryName,string sortType,string orderType,Size? size,Color? color,float? priceFrom=0,float? priceTo=99999)
        {
            List<Product> products = productServices.FilterProducts(categoryName, sortType, orderType, size, color, priceFrom, priceTo);

            return products;
        }

        public List<Product> SearchProductByName(string productName)
        {
            var products = productServices.SearchProductsByName(productName);

            return products;
        }

        [HttpPost]
        [Route("addOptionGroup")]
        public async Task<IActionResult> AddOptionGroup([FromBody]OptionGroup optionGroupModel)
        {
            if (optionGroupModel == null)
                return StatusCode(StatusCodes.Status204NoContent);

            await productServices.AddOptionGroup(optionGroupModel);

            return Ok();
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
        public async Task<IActionResult> AddOption([FromBody]Option optionModel)
        {
            if (optionModel == null)
                return StatusCode(StatusCodes.Status204NoContent);

            await productServices.AddOption(optionModel);

            return Ok();
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


    }
}