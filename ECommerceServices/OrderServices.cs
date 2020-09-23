using ECommerceData;
using ECommerceIServices;
using ECommerceModels.Authentication;
using ECommerceModels.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceServices
{
    public class OrderServices : IOrderServices
    {
        private readonly ECommerceContext appDb;
        public OrderServices(ECommerceContext appDb)
        {
            this.appDb = appDb;
        }
        public async Task<OrderResponse> createOrder(ApplicationUser user, int cartId, GuestUser guestUser, Address address,int deliveryId,int paymentId)
        {
            
            // If there is no address and no user, return error
            if (address == null && user == null)
            {
                return new OrderResponse { ErrorMessage = "You must specify delivery address"};
            }

            // If there is no address but user is not null use users Address
            if (address == null && user != null)
            {
                if(user.Address == null)
                    return new OrderResponse { ErrorMessage = "No delivery address specified" };
                
                address = user.Address;
            }

            var cart = await appDb.Carts.FindAsync(cartId);

            if(cart == null)
                return new OrderResponse { ErrorMessage = "Could not find a cart" };

            var deliveryMethod = await appDb.DeliveryMethods.FindAsync(deliveryId);

            if(deliveryMethod == null)
                return new OrderResponse { ErrorMessage = "Could not find this delivery method" };

            var paymentMethod = await appDb.PaymentMethods.FindAsync(paymentId);

            if(paymentMethod == null)
                return new OrderResponse { ErrorMessage = "Could not find this payment method" };

            Order order = new Order
            {
                OrderStatus = "New order",
                OrderDate = DateTime.Now,
                ModifiedAt = DateTime.Now,
                User = user,
                Address = address,
                OrderPrice = cart.TotalPrice,
                isConfirmed = false,
                DeliveryMethod = deliveryMethod,
                PaymentMethod = paymentMethod
            };

            // if someone orders as a guest require additional info and put that into order
            if(user == null)
            {
                order.ClientEmail = guestUser.Email;
                order.ClientName = guestUser.Name;
                order.ClientSurname = guestUser.Surname;
                order.ClientPhone = guestUser.Phone;
            }

            await appDb.Orders.AddAsync(order);
            await appDb.SaveChangesAsync();

            var cartItems = await appDb.CartProducts.Where(c => c.CartId == cartId).ToListAsync();          

            if(cartItems == null)
                return new OrderResponse { ErrorMessage = "No items in a cart" };

            foreach (var cartItem in cartItems)
            {
                OrderItem orderItem = new OrderItem
                {
                    OrderId = order.OrderId,
                    ProductId = cartItem.ProductId,
                    Quantity = cartItem.Quantity,
                    Option = cartItem.Option
                };

                // remove the amount of ordered products from product stock
                var product = await appDb.ProductOption.FindAsync(cartItem.ProductId, cartItem.OptionId);
                product.ProductStock -= cartItem.Quantity;

                await appDb.OrderItems.AddAsync(orderItem);
            }

            // after order is created remove the cart
            appDb.Carts.Remove(cart);

            await appDb.SaveChangesAsync();
            return new OrderResponse { SuccessMessage = "Successfully created order" };
        }

        public async Task<OrderResponse> cancelOrder(int orderId)
        {
            var order = await appDb.Orders.FindAsync(orderId);

            foreach(var orderItem in order.OrderItems)
            {
                // add the amount of ordered products to product stock
                var product = await appDb.ProductOption.FindAsync(orderItem.ProductId, orderItem.OptionId);
                product.ProductStock += orderItem.Quantity;
            }

            appDb.Orders.Remove(order);
            await appDb.SaveChangesAsync();
            return new OrderResponse { SuccessMessage = "Successfully canceled order" };
        }

        public Task EditOrder()
        {
            throw new NotImplementedException();
        }
    }
}
