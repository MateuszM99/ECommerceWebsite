using ECommerceModels.Authentication;
using ECommerceModels.Models;
using ECommerceModels.RequestModels.CartRequestModels;
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
        Task<CartResponse> addToCartAsync(AddToCartModel addToCartModel);
        Task<CartResponse> removeFromCartAsync(RemoveFromCartModel removeFromCartModel);
        Task<List<ProductOptionQuantity>> getCartProductsAsync(int cartId);
        Task<double> getCartPriceAsync(int cartId);
        Task<int> getCartProductsCountAsync(int cartId);
    }
}
