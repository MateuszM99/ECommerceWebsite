using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ECommerceData;
using ECommerceModels.Authentication;
using ECommerceModels.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ECommerceContext appDb;
        public ProductsController(UserManager<ApplicationUser> userManager, ECommerceContext appDb)
        {
            this.userManager = userManager;
            this.appDb = appDb;
        }

        [HttpPost]
        [Route("addProduct")]
        public async Task<IActionResult> AddProduct([FromBody]Product productModel)
        {
            if (productModel == null)
                return StatusCode(StatusCodes.Status204NoContent);

            var product = await appDb.Products.FindAsync(productModel.ProductId);

            if (product != null)
                return StatusCode(StatusCodes.Status403Forbidden);

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
    }
}