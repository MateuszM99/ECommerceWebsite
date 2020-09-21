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
        public async Task createOrder(ApplicationUser user, int cartId, GuestUser guestUser, Address address)
        {
            
            // If there is no address and no user, return error
            if (address == null && user == null)
            {
                // error 
            }

            // If there is no address but user is not null use users Address
            if (address == null && user != null)
            {
                address = user.Address;
            }

            var cart = await appDb.Carts.FindAsync(cartId);

            Order order = new Order
            {
                OrderStatus = "New order",
                OrderDate = DateTime.Now,
                ModifiedAt = DateTime.Now,
                User = user,
                Address = address,
                OrderPrice = cart.TotalPrice,
                isConfirmed = false
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

            foreach (var cartItem in cartItems)
            {
                OrderItem orderItem = new OrderItem
                {
                    OrderId = order.OrderId,
                    ProductId = cartItem.ProductId,
                    Quantity = cartItem.Quantity
                };

                await appDb.OrderItems.AddAsync(orderItem);
            }

            await appDb.SaveChangesAsync();
        }

        public async Task cancelOrder(int orderId)
        {
            var order = await appDb.Orders.FindAsync(orderId);
            appDb.Orders.Remove(order);
            await appDb.SaveChangesAsync();
        }

        public Task EditOrder()
        {
            throw new NotImplementedException();
        }
    }
}
