using ECommerceModels.Authentication;
using ECommerceModels.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceIServices
{
    public interface IOrderServices
    {
        Task<OrderResponse> createOrder(ApplicationUser user,int cartId, GuestUser guestUser,Address address, int deliveryId, int paymentId);
        Task<OrderResponse> cancelOrder(int orderId);
        Task EditOrder();
    }
}
