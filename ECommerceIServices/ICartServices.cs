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
        Task<int> AddToCart(int? cartId, int productId);
        Task<HttpResponseMessage> RemoveFromCart(int? cartId,int productId);
        List<ProductQuantity> GetCartProducts(int cartId);
        float GetCartPrice(int cartId);
    }
}
