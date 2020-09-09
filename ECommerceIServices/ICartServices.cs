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
        Task<HttpResponseMessage> AddToCart(ApplicationUser user,int productId);
        Task<HttpResponseMessage> RemoveFromCart(ApplicationUser user,int productId);
        List<Product> GetCartProducts(int cartId);
        float GetCartPrice(int cartId);
    }
}
