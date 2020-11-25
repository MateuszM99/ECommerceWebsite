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
        private readonly IEmailSender emailSender;
        private readonly IMapper mapper;
        public OrderServices(ECommerceContext appDb,ILogger<OrderServices> logger,IMapper mapper, IEmailSender emailSender)
        {
            this.logger = logger;
            this.appDb = appDb;
            this.mapper = mapper;
            this.emailSender = emailSender;
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

            var cartItems = await appDb.CartProducts
                .Where(c => c.CartId == orderModel.CartId)
                .Include(p => p.Product)
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

            var order = mapper.Map<Order>(orderModel);

            order.AddedAt = DateTime.Now;
            order.ModifiedAt = DateTime.Now;
            order.isConfirmed = false;
            order.UserId = user.Id;


            await appDb.Orders.AddAsync(order);
            await appDb.SaveChangesAsync();

            
            List<OrderProduct> orderItems = new List<OrderProduct>();

            foreach (var cartItem in cartItems)
            {
                OrderProduct orderItem = new OrderProduct
                {
                    OrderId = order.Id,
                    ProductId = cartItem.ProductId,
                    ProductVariationId = cartItem.ProductVariationId,
                    Quantity = cartItem.Quantity,
                    Option = cartItem.Option
                };

                // remove the amount of ordered products from product stock
                var productOption = await appDb.ProductOptions.FindAsync(cartItem.OptionId, cartItem.ProductId, cartItem.ProductVariationId);
                productOption.ProductStock -= cartItem.Quantity;
                order.Price += cartItem.Product.Price * cartItem.Quantity;
               
                orderItems.Add(orderItem);
            }

            await appDb.OrderProducts.AddRangeAsync(orderItems);
            appDb.Carts.Remove(cart);
            await appDb.SaveChangesAsync();
            
            await sendOrderConfirmationEmail(order.Id, order.User.Email);
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

            List<OrderProduct> orderItems = new List<OrderProduct>();

            foreach (var cartItem in cartItems)
            {
                OrderProduct orderItem = new OrderProduct
                {
                    OrderId = order.Id,
                    ProductId = cartItem.ProductId,
                    ProductVariationId = cartItem.ProductVariationId,
                    Quantity = cartItem.Quantity,
                    Option = cartItem.Option
                };

                // remove the amount of ordered products from product stock
                var productOption = await appDb.ProductOptions.FindAsync(cartItem.OptionId,cartItem.ProductId,cartItem.ProductVariationId);
                productOption.ProductStock -= cartItem.Quantity;
                order.Price += cartItem.Product.Price * cartItem.Quantity;

                orderItems.Add(orderItem);               
            }          

            await appDb.OrderProducts.AddRangeAsync(orderItems);
            appDb.CartProducts.RemoveRange(cartItems);
            appDb.Carts.Remove(cart);
            await appDb.SaveChangesAsync();

            await sendOrderConfirmationEmail(order.Id, order.ClientEmail);
        }

        public async Task editOrder(OrderDTO orderModel)
        {
            if(orderModel == null)
            {
                var message = new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest)
                {
                    Content = new StringContent($"The {nameof(orderModel)} can't be null")
                };
                throw new HttpResponseException(message);
            }

            if(orderModel.Id == null)
            {
                var message = new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest)
                {
                    Content = new StringContent($"The order id {nameof(orderModel.Id)} can't be null")
                };
                throw new HttpResponseException(message);
            }

            var order = await appDb.Orders.FindAsync(orderModel.Id);

            order = mapper.Map<Order>(orderModel);

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

            foreach (var orderItem in order.Products)
            {
                // add the amount of ordered products to product stock
                var product = await appDb.ProductOptions.FindAsync(orderItem.OptionId, orderItem.ProductId,orderItem.ProductVariationId);
                product.ProductStock += orderItem.Quantity;
            }

            order.Status = OrderStatus.Canceled;
            order.ModifiedAt = DateTime.Now;
            await appDb.SaveChangesAsync();            
        }
       
        public async Task<List<Order>> getAllOrdersAsync()
        {
            logger.LogInformation($"Starting method {nameof(getAllOrdersAsync)}.");
           
            var orders = await appDb.Orders
                                .Include(o => o.Products)
                                .ToListAsync();

            logger.LogInformation($"Finished method {nameof(getAllOrdersAsync)}.");

            return orders;
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
                .Include(o => o.Products)
                .ToListAsync();

            logger.LogInformation($"Finished method {nameof(getAllUsersOrdersAsync)}.");

            return orders;
        }

        public async Task<string> generateOrderConfirmationTokenAsync(int id)
        {
            var order = await appDb.Orders.FindAsync(id);

            if(order != null)
            {
                order.token = randomString(20);
                await appDb.SaveChangesAsync();
                return order.token;
            }

            return null;
        }

        public async Task confirmOrderAsync(int id,string token)
        {
            var order = await appDb.Orders.FindAsync(id);

            if (order == null)
            {
                var message = new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest)
                {
                    Content = new StringContent($"The order is not found")
                };
                throw new HttpResponseException(message);
            }

            if (order != null)
            {
                if(order.token == token)
                {
                    order.isConfirmed = true;
                    await appDb.SaveChangesAsync();                           
                } else
                {
                    var message = new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest)
                    {
                        Content = new StringContent($"The token is wrong")
                    };
                    throw new HttpResponseException(message);
                }            
            }
        }

        public async Task sendOrderConfirmationEmail(int id,string email)
        {
            var token = await generateOrderConfirmationTokenAsync(id);            
            var baseUrl = "http://localhost:3000/accountConfirm";
            var confirmationLink = baseUrl + String.Format("/?orderId={0}&token={1}", id, token);
            //var confirmationLink = Url.Action("ConfirmEmail", "Authenticate", new { userId = user.Id, token = token },Request.Scheme);
            string message = $"Click this link to confirm your order: " + confirmationLink;

            await emailSender.SendEmailAsync(email, "Confirm your account", message);         
        }

        private string randomString(int length)
        {
            Random random = new Random();

            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
