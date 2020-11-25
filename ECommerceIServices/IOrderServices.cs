using ECommerceModels.Authentication;
using ECommerceModels.DTOs;
using ECommerceModels.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceIServices
{
    public interface IOrderServices
    {
        Task createOrderGuest(OrderDTO orderModel);
        Task createOrderUser(OrderDTO orderModel,ApplicationUser user);
        Task editOrder(OrderDTO orderModel);
        Task cancelOrder(int orderId);
        Task<List<Order>> getAllOrdersAsync();
        Task<List<Order>> getAllUsersOrdersAsync(ApplicationUser user);
        Task<string> generateOrderConfirmationTokenAsync(int id);
        Task confirmOrderAsync(int id, string token);
        Task sendOrderConfirmationEmail(int id, string email);
    }
}
