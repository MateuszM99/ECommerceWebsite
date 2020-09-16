using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ECommerceData;
using ECommerceModels.Authentication;
using ECommerceModels.Models;
using EllipticCurve;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ECommerceContext appDb;

        public OrderController()
        {
            
        }

        public async Task<IActionResult> createOrder(Address address,int cartId)
        {
            return null;
        }


    }
}