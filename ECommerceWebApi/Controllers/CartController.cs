using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using ECommerceData;
using ECommerceIServices;
using ECommerceModels.Authentication;
using ECommerceModels.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ECommerceWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ECommerceContext appDb;
        private readonly ICartServices cartServices;
        public CartController(UserManager<ApplicationUser> userManager,ECommerceContext appDb, ICartServices cartServices)
        {
            this.userManager = userManager;
            this.appDb = appDb;
            this.cartServices = cartServices;
        }

        [EnableCors("Policy")]
        [HttpPost]
        [Route("addCart")]
        public async Task<IActionResult> AddToCart(int? cartId,int productId,int? quantity,string optionName)
        {                               
            var cartResponse = await cartServices.AddToCart(cartId, productId,quantity,optionName);

            // If function has any errors
            if (cartResponse.ErrorMessage != null)
                return StatusCode(StatusCodes.Status400BadRequest,cartResponse.ErrorMessage);

            return Ok(new {
                cartResponse.CartId,cartResponse.SuccessMessage
            });            
        }

        [EnableCors("Policy")]
        [HttpPost]
        [Route("removeCart")]
        public async Task<IActionResult> RemoveFromCart(int cartId,int productId)
        {
             
             var cartResponse = await cartServices.RemoveFromCart(cartId, productId);

            if (cartResponse.ErrorMessage != null)
                return StatusCode(StatusCodes.Status400BadRequest, cartResponse.ErrorMessage);

            return Ok(new
            {            
                cartResponse.SuccessMessage
            });
        }

        [EnableCors("Policy")]
        [HttpGet]
        [Route("getCart")]
        public List<ProductOptionQuantity> getCartProducts(int cartId)
        {
            return cartServices.GetCartProducts(cartId);
        }

        [EnableCors("Policy")]
        [HttpGet]
        [Route("getCartCount")]
        public int getCartProductsCount(int cartId)
        {
            List<ProductOptionQuantity> productQuantities = cartServices.GetCartProducts(cartId);

            int count = 0;

            foreach(var product in productQuantities)
            {
                count += 1 * product.quantity;
            }

            return count;
        }


   
    }
}