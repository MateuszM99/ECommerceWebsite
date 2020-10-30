using ECommerceModels.Authentication;
using ECommerceModels.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceIServices
{
    public interface ICartServices
    {
        Task<CartResponse> AddToCart(int? cartId, int productId,int? quantity, string optionName);
        Task<CartResponse> RemoveFromCart(int? cartId,int productId);
        Task<List<ProductOptionQuantity>> GetCartProductsAsync(int cartId);
        Task<double> GetCartPrice(int cartId);
        Task<int> getCartProductsCount(int cartId);
    }
}
