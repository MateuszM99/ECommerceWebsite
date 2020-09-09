using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ECommerceData;
using ECommerceIServices;
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
        private readonly ICartServices cartServices;
        public CartController(UserManager<ApplicationUser> userManager,ECommerceContext appDb, ICartServices cartServices)
        {
            this.userManager = userManager;
            this.appDb = appDb;
            this.cartServices = cartServices;
        }
        
        [HttpPost]
        [Route("addCart")]
        public async Task<IActionResult> AddToCart(int productId)
        {         
            var user = await userManager.GetUserAsync(User);

            var result = await cartServices.AddToCart(user, productId);

            
            return Ok();            
        }
        
        public async Task<IActionResult> RemoveFromCart(int productId)
        {
            var user = await userManager.GetUserAsync(User);

            var result = await cartServices.RemoveFromCart(user, productId);

            return Ok();
        }
    

    
    
    
    
    }
}