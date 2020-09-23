using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ECommerceData;
using ECommerceIServices;
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
        private readonly IOrderServices orderServices;

        public OrderController(UserManager<ApplicationUser> userManager, ECommerceContext appDb, IOrderServices orderServices)
        {
            this.userManager = userManager;
            this.appDb = appDb;
            this.orderServices = orderServices;
        }

        public async Task<IActionResult> createOrder(ApplicationUser user, int cartId, GuestUser guestUser, Address address, int deliveryId, int paymentId)
        {
            var orderResponse = await orderServices.createOrder(user, cartId, guestUser, address, deliveryId, paymentId);

            if (orderResponse.ErrorMessage != null)
                return StatusCode(StatusCodes.Status400BadRequest, orderResponse.ErrorMessage);

            return Ok(new { orderResponse.SuccessMessage });
        }


    }
}