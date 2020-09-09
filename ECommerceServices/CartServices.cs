using ECommerceData;
using ECommerceData.Migrations;
using ECommerceIServices;
using ECommerceModels.Authentication;
using ECommerceModels.Models;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceServices
{
    public class CartServices : ICartServices
    {
        private readonly ECommerceContext appDb;
        public CartServices(ECommerceContext appDb)
        {
            this.appDb = appDb;
        }

        public async Task<HttpResponseMessage> AddToCart(ApplicationUser user, int productId)
        {
            var cart = await appDb.Carts.Where(c => c.UserId == user.Id).FirstOrDefaultAsync();

            if (cart == null)
            {
                cart = new ShoppingCart();
                await appDb.AddAsync(cart);
                await appDb.SaveChangesAsync();
            }

            var cartProduct = await appDb.CartProducts.FindAsync(cart.CartId, productId);

            if (cartProduct == null)
            {
                cartProduct = new CartProduct()
                {
                    CartId = cart.CartId,
                    ProductId = productId,
                    Quantity = 1
                };

                await appDb.CartProducts.AddAsync(cartProduct);                
            } else
            {
                cartProduct.Quantity++;
            }
          
            await appDb.SaveChangesAsync();

            cart.TotalPrice = GetCartPrice(cart.CartId);
            await appDb.SaveChangesAsync();

            return new HttpResponseMessage(System.Net.HttpStatusCode.OK);
        }

        public float GetCartPrice(int cartId)
        {
            return appDb.CartProducts
                        .Where(x => x.CartId == cartId)
                        .Select(x => x.Product.ProductPrice * x.Quantity)
                        .Sum();
        }

        public List<Product> GetCartProducts(int cartId)
        {
            return appDb.CartProducts.Where(c => c.CartId == cartId)
                .Select(p => p.Product)
                .ToList();
        }

        public async Task<HttpResponseMessage> RemoveFromCart(ApplicationUser user, int productId)
        {
            var cart = await appDb.Carts.Where(c => c.UserId == user.Id).FirstOrDefaultAsync();

            if(cart == null)
            {
                // error
                return new HttpResponseMessage(System.Net.HttpStatusCode.MethodNotAllowed);
            }

            var cartProduct = await appDb.CartProducts.FindAsync(cart.CartId, productId);
            
            if(cartProduct == null)
            {
                // error
                return new HttpResponseMessage(System.Net.HttpStatusCode.MethodNotAllowed);
            }

            if(cartProduct.Quantity <= 1)
            {
                appDb.CartProducts.Remove(cartProduct);            
            } else
            {
                cartProduct.Quantity--;             
            }

            await appDb.SaveChangesAsync();

            cart.TotalPrice = GetCartPrice(cart.CartId);
            await appDb.SaveChangesAsync();

            return new HttpResponseMessage(System.Net.HttpStatusCode.OK);
        }
    }
}
