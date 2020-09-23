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
        List<ProductOptionQuantity> GetCartProducts(int cartId);
        float GetCartPrice(int cartId);
    }
}
