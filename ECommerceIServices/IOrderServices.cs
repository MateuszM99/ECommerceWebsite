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
        Task cancelOrder(int orderId);
        Task EditOrder();
    }
}
