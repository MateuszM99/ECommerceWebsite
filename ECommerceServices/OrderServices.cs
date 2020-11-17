using AutoMapper;
using ECommerceData;
using ECommerceIServices;
using ECommerceModels.Authentication;
using ECommerceModels.DTOs;
using ECommerceModels.Enums;
using ECommerceModels.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace ECommerceServices
{
    public class OrderServices : IOrderServices
    {
        private readonly ILogger<OrderServices> logger;
        private readonly ECommerceContext appDb;
        private readonly IMapper mapper;
        public OrderServices(ECommerceContext appDb,ILogger<OrderServices> logger,IMapper mapper)
        {
            this.logger = logger;
            this.appDb = appDb;
            this.mapper = mapper;
        }
       
        public async Task createOrderUser(OrderDTO orderModel,ApplicationUser user)
        {
            logger.LogInformation($"Starting method {nameof(createOrderGuest)}.");

            if (user == null)
            {
                var message = new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest)
                {
                    Content = new StringContent($"The user is not found, check if you are logged in")
                };
                throw new HttpResponseException(message);
            }

            if (user.Address == null)
            {
                if (orderModel.Address == null)
                {
                    var message = new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest)
                    {
                        Content = new StringContent($"The order is missing address details, please check if you filled all the details")
                    };
                    throw new HttpResponseException(message);
                }
            }

            if (orderModel.DeliveryMethodId == null)
            {
                var message = new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest)
                {
                    Content = new StringContent($"The delivery method is not chosen, please check if the delivery method is selected")
                };
                throw new HttpResponseException(message);
            }

            if (orderModel.PaymentMethodId == null)
            {
                var message = new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest)
                {
                    Content = new StringContent($"The payment method is not chosen, please check if the payment method is selected")
                };
                throw new HttpResponseException(message);
            }

            var cart = await appDb.Carts.FindAsync(orderModel.CartId);

            if (cart == null)
            {
                var message = new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest)
                {
                    Content = new StringContent($"Could not find a cart with the given {nameof(orderModel.CartId)}")
                };
                throw new HttpResponseException(message);
            }

            var order = mapper.Map<Order>(orderModel);

            order.AddedAt = DateTime.Now;
            order.ModifiedAt = DateTime.Now;
            order.isConfirmed = false;
            order.UserId = user.Id;


            await appDb.Orders.AddAsync(order);
            await appDb.SaveChangesAsync();

            var cartItems = await appDb.CartProducts
                .Where(c => c.CartId == orderModel.CartId)
                .Include(o => o.Option)
                .ToListAsync();

            if (cartItems == null)
            {
                var message = new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest)
                {
                    Content = new StringContent($"There are no items inside cart with given id {nameof(orderModel.CartId)},{nameof(cartItems)} is null")
                };
                throw new HttpResponseException(message);
            }

            List<OrderItem> orderItems = new List<OrderItem>();

            foreach (var cartItem in cartItems)
            {
                OrderItem orderItem = new OrderItem
                {
                    OrderId = order.Id,
                    ProductId = cartItem.ProductId,
                    Quantity = cartItem.Quantity,
                    Option = cartItem.Option
                };

                // remove the amount of ordered products from product stock
                var productOption = await appDb.ProductOption.FindAsync(cartItem.ProductId, cartItem.OptionId);
                productOption.ProductStock -= cartItem.Quantity;
               
                orderItems.Add(orderItem);
            }

            await appDb.OrderItems.AddRangeAsync(orderItems);
            appDb.Carts.Remove(cart);
            await appDb.SaveChangesAsync();          
        }

        public async Task createOrderGuest(OrderDTO orderModel)
        {
            logger.LogInformation($"Starting method {nameof(createOrderGuest)}.");

            if (orderModel.ClientEmail == null || orderModel.ClientName == null || orderModel.ClientPhone == null || orderModel.ClientSurname == null)
            {
                var message = new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest)
                {
                    Content = new StringContent($"The order is missing client details, please check if you filled all the details")
                };
                throw new HttpResponseException(message);
            }

            if(orderModel.Address == null)
            {
                var message = new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest)
                {
                    Content = new StringContent($"The order is missing address details, please check if you filled all the details")
                };
                throw new HttpResponseException(message);
            }

            if(orderModel.DeliveryMethodId == null)
            {
                var message = new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest)
                {
                    Content = new StringContent($"The delivery method is not chosen, please check if the delivery method is selected")
                };
                throw new HttpResponseException(message);
            }

            if(orderModel.PaymentMethodId == null)
            {
                var message = new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest)
                {
                    Content = new StringContent($"The payment method is not chosen, please check if the payment method is selected")
                };
                throw new HttpResponseException(message);
            }

            var cart = await appDb.Carts.FindAsync(orderModel.CartId);

            if(cart == null)
            {
                var message = new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest)
                {
                    Content = new StringContent($"Could not find a cart with the given {nameof(orderModel.CartId)}")
                };
                throw new HttpResponseException(message);
            }

            var order = mapper.Map<Order>(orderModel);

            order.AddedAt = DateTime.Now;
            order.ModifiedAt = DateTime.Now;
            order.isConfirmed = false;
                  
            var cartItems = await appDb.CartProducts
                .Where(c => c.CartId == orderModel.CartId)
                .Include(o => o.Option)
                .ToListAsync();

            if (cartItems == null)
            {
                var message = new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest)
                {
                    Content = new StringContent($"There are no items inside cart with given id {nameof(orderModel.CartId)},{nameof(cartItems)} is null")
                };
                throw new HttpResponseException(message);
            }

            await appDb.Orders.AddAsync(order);
            await appDb.SaveChangesAsync();

            List<OrderItem> orderItems = new List<OrderItem>();

            foreach (var cartItem in cartItems)
            {
                OrderItem orderItem = new OrderItem
                {
                    OrderId = order.Id,
                    ProductId = cartItem.ProductId,
                    Quantity = cartItem.Quantity,
                    Option = cartItem.Option
                };

                // remove the amount of ordered products from product stock
                var productOption = await appDb.ProductOption.FindAsync(cartItem.OptionId,cartItem.ProductId);
                productOption.ProductStock -= 1;

                orderItems.Add(orderItem);               
            }          

            await appDb.OrderItems.AddRangeAsync(orderItems);
            appDb.CartProducts.RemoveRange(cartItems);
            appDb.Carts.Remove(cart);
            await appDb.SaveChangesAsync();           
        }

        public async Task cancelOrder(int orderId)
        {
            var order = await appDb.Orders.FindAsync(orderId);

            if (order == null)
            {
                var message = new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest)
                {
                    Content = new StringContent($"There are no found with given id {nameof(orderId)}")
                };
                throw new HttpResponseException(message);
            }

            foreach (var orderItem in order.Items)
            {
                // add the amount of ordered products to product stock
                var product = await appDb.ProductOption.FindAsync(orderItem.ProductId, orderItem.OptionId);
                product.ProductStock += orderItem.Quantity;
            }

            order.Status = OrderStatus.Canceled;
            order.ModifiedAt = DateTime.Now;
            await appDb.SaveChangesAsync();            
        }

        public Task EditOrder()
        {
            throw new NotImplementedException();
        }

        public async Task<List<Order>> getAllUsersOrdersAsync(ApplicationUser user)
        {
            logger.LogInformation($"Starting method {nameof(getAllUsersOrdersAsync)}.");

            if (user == null)
            {
                var message = new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest)
                {
                    Content = new StringContent($"The user is not found, check if you are logged in")
                };
                throw new HttpResponseException(message);
            }

            var orders = await appDb.Orders.Where(o => o.UserId == user.Id)
                .Include(o => o.Items)
                .ToListAsync();

            logger.LogInformation($"Finished method {nameof(getAllUsersOrdersAsync)}.");

            return orders;
        }
    }
}
