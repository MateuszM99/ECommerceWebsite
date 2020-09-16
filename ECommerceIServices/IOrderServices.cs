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
        Task createOrder(ApplicationUser user,int cartId,Address address);
        Task cancelOrder(int orderId);
        Task EditOrder();
    }
}
