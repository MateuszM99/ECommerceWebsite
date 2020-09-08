using ECommerceModels.Authentication;
using ECommerceModels.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceIServices
{
    public interface ICartServices
    {
        Task AddToCart(ApplicationUser user,int productId);
        Task RemoveFromCart(ApplicationUser user,int productId);
        List<Product> GetCartProducts(int cartId);
        float GetCartPrice(int cartId);
    }
}
