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
        Task<CartResponse> addToCartAsync(int? cartId, int productId,int? quantity, string optionName);
        Task<CartResponse> removeFromCartAsync(int? cartId,int productId);
        Task<List<ProductOptionQuantity>> getCartProductsAsync(int cartId);
        Task<double> getCartPriceAsync(int cartId);
        Task<int> getCartProductsCountAsync(int cartId);
    }
}
