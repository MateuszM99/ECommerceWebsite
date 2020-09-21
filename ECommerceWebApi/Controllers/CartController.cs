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
        public async Task<IActionResult> AddToCart(int? cartId,int productId,int? quantity)
        {         
           
            var id = await cartServices.AddToCart(cartId, productId,quantity);

            
            return Ok(new {
                id
            });            
        }

        [EnableCors("Policy")]
        [HttpPost]
        [Route("removeCart")]
        public async Task<HttpResponseMessage> RemoveFromCart(int cartId,int productId)
        {
             
             var result = await cartServices.RemoveFromCart(cartId, productId);

             return result;
         }

        [EnableCors("Policy")]
        [HttpGet]
        [Route("getCart")]
        public List<ProductQuantity> getCartProducts(int cartId)
        {
            return cartServices.GetCartProducts(cartId);
        }

        [EnableCors("Policy")]
        [HttpGet]
        [Route("getCartCount")]
        public int getCartProductsCount(int cartId)
        {
            List<ProductQuantity> productQuantities = cartServices.GetCartProducts(cartId);

            int count = 0;

            foreach(var product in productQuantities)
            {
                count += 1 * product.quantity;
            }

            return count;
        }


   
    }
}