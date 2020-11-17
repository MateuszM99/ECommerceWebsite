using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using ECommerceData;
using ECommerceIServices;
using ECommerceModels.Authentication;
using ECommerceModels.DTOs;
using ECommerceModels.Models;
using EllipticCurve;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        private readonly IMapper mapper;

        public OrderController(ILogger<OrderController> logger,UserManager<ApplicationUser> userManager, ECommerceContext appDb, IOrderServices orderServices, IMapper mapper)
        {
            this.userManager = userManager;
            this.appDb = appDb;
            this.orderServices = orderServices;
            this.logger = logger;
            this.mapper = mapper;
        }
                  
        [EnableCors("Policy")]
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

        public async Task<IActionResult> GetAllOrders()
        {
            logger.LogInformation($"Starting method {nameof(GetAllOrders)}.");
            try
            {             
                var orders = await orderServices.getAllOrdersAsync();

                var ordersDTO = mapper.Map<List<OrderDTO>>(orders);

                logger.LogInformation($"Finished method {nameof(GetAllOrders)}");
                return Ok(ordersDTO);
            }
            catch (System.Web.Http.HttpResponseException ex)
            {
                logger.LogError($"{ex.Message}");
                throw;
            }
        }

        public async Task<IActionResult> GetAllUsersOrders()
        {
            logger.LogInformation($"Starting method {nameof(GetAllUsersOrders)}.");
            try
            {
                var user = await userManager.GetUserAsync(HttpContext.User);

                var orders = await orderServices.getAllUsersOrdersAsync(user);

                var ordersDTO = mapper.Map<List<OrderDTO>>(orders);

                logger.LogInformation($"Finished method {nameof(GetAllUsersOrders)}");
                return Ok(ordersDTO);
            }
            catch (System.Web.Http.HttpResponseException ex)
            {
                logger.LogError($"{ex.Message}");
                throw;
            }
        }

        [EnableCors("Policy")]
        [HttpGet]
        [Route("getDeliveries")]
        public async Task<IActionResult> GetAvailableDeliveryMethods()
        {
            var deliveryMethods = await appDb.DeliveryMethods.ToListAsync();

            return Ok(new {deliveryMethods});
        }

        [EnableCors("Policy")]
        [HttpGet]
        [Route("getPayments")]
        public async Task<IActionResult> GetAvailablePaymentMethods()
        {
            var paymentMethods = await appDb.PaymentMethods.ToListAsync();

            return Ok(new { paymentMethods });
        }


    }
}