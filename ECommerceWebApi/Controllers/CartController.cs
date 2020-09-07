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
using Microsoft.EntityFrameworkCore;

namespace ECommerceWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ECommerceContext appDb;
        public CartController(UserManager<ApplicationUser> userManager,ECommerceContext appDb)
        {
            this.userManager = userManager;
            this.appDb = appDb;
        }
        
        [HttpPost]
        [Route("addCart")]
        public async Task<IActionResult> AddToCart(int productId)
        {         
            var user = await userManager.GetUserAsync(User);
            var cart = await appDb.Carts.Where(c => c.UserId == user.Id).FirstOrDefaultAsync();
           
            if (cart == null)
            {
                cart = new ShoppingCart();
                await appDb.AddAsync(cart);
                await appDb.SaveChangesAsync();
            }

            var cartProduct = await appDb.CartProducts.FindAsync(cart.CartId, productId);

            if(cartProduct == null)
            {
                cartProduct = new CartProduct()
                {
                    CartId = cart.CartId,
                    ProductId = productId,
                    Quantity = 1
                };

                await appDb.CartProducts.AddAsync(cartProduct);
                await appDb.SaveChangesAsync();

                return Ok();
            }

            cartProduct.Quantity++;
            await appDb.SaveChangesAsync();

            cart.TotalPrice = appDb.CartProducts
                        .Where(x => x.CartId == cart.CartId)
                        .Select(x => x.Product.ProductPrice * x.Quantity)
                        .Sum();
            
            await appDb.SaveChangesAsync();

            return Ok();            
        }
        
        public async Task<IActionResult> RemoveFromCart()
        {
            return Ok();
        }
    

    
    
    
    
    }
}