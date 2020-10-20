using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ECommerceData;
using ECommerceIServices;
using ECommerceModels.Authentication;
using ECommerceModels.DTOs;
using ECommerceModels.Models;
using EllipticCurve;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;

namespace ECommerceWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly ILogger<OrderController> logger;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ECommerceContext appDb;
        private readonly IOrderServices orderServices;

        public OrderController(ILogger<OrderController> logger,UserManager<ApplicationUser> userManager, ECommerceContext appDb, IOrderServices orderServices)
        {
            this.userManager = userManager;
            this.appDb = appDb;
            this.orderServices = orderServices;
            this.logger = logger;
        }
                  
        [HttpPost]
        [Route("createOrder")]
        public async Task<IActionResult> CreateOrder([FromBody]OrderDTO orderModel)
        {
            logger.LogInformation($"Starting method {nameof(CreateOrder)}.");
            try
            {
                var user = await userManager.GetUserAsync(HttpContext.User);

                if (user != null)
                    await orderServices.createOrderUser(orderModel, user);
                else
                    await orderServices.createOrderGuest(orderModel);
                
                logger.LogInformation($"Finished method {nameof(CreateOrder)}");
                return Ok();
            }
            catch (System.Web.Http.HttpResponseException ex)
            {
                logger.LogError($"{ex.Message}");
                throw;
            }
        }

        [HttpPost]
        [Route("cancelOrder")]
        public async Task<IActionResult> CancelOrder(int orderId)
        {
            logger.LogInformation($"Starting method {nameof(CancelOrder)}.");
            try
            {
                await orderServices.cancelOrder(orderId);

                logger.LogInformation($"Finished method {nameof(CancelOrder)}");
                return Ok();
            }
            catch (System.Web.Http.HttpResponseException ex)
            {
                logger.LogError($"{ex.Message}");
                throw;
            }
        }


    }
}