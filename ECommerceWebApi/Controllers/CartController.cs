using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using ECommerceData;
using ECommerceIServices;
using ECommerceModels.Authentication;
using ECommerceModels.Models;
using ECommerceModels.RequestModels.CartRequestModels;
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
        public async Task<IActionResult> AddToCart([FromBody]AddToCartModel addToCartModel)
        {                               
            var cartResponse = await cartServices.addToCartAsync(addToCartModel);

            // If function has any errors
            if (cartResponse.Status == "Error")
                return StatusCode(StatusCodes.Status400BadRequest,cartResponse.Message);

            return Ok(new {
                cartResponse.CartId,cartResponse.CartPrice,cartResponse.CartCount,cartResponse.CartProducts,cartResponse.Message
            });            
        }

        [EnableCors("Policy")]
        [HttpPost]
        [Route("removeFromCart")]
        public async Task<IActionResult> RemoveFromCart([FromBody]RemoveFromCartModel removeFromCartModel)
        {
             
             var cartResponse = await cartServices.removeFromCartAsync(removeFromCartModel);

            if (cartResponse.Status == "Error")
                return StatusCode(StatusCodes.Status400BadRequest, cartResponse.Message);

            return Ok(new {
                cartResponse.CartId,cartResponse.CartPrice,cartResponse.CartCount,cartResponse.CartProducts,cartResponse.Message
            });
        }
        
        [EnableCors("Policy")]
        [HttpGet]
        [Route("getCart")]
        public async Task<IActionResult> GetCart(int cartId)
        {
            var cartProducts = await cartServices.getCartProductsAsync(cartId);
            var cartPrice = await cartServices.getCartPriceAsync(cartId);
            var cartCount = await cartServices.getCartProductsCountAsync(cartId);


            return Ok(new {
                cartProducts, cartPrice, cartCount
            });
        }

       
   
    }
}