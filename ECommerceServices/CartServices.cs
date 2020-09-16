using ECommerceData;
using ECommerceIServices;
using ECommerceModels.Authentication;
using ECommerceModels.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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

        public async Task<int> AddToCart(int? cartId, int productId)
        {
            var cart = await appDb.Carts.FindAsync(cartId);

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
            }
            else
            {
                cartProduct.Quantity++;
            }

            await appDb.SaveChangesAsync();

            cart.TotalPrice = GetCartPrice(cart.CartId);
            await appDb.SaveChangesAsync();

            return cart.CartId;
        }

        public async Task<HttpResponseMessage> RemoveFromCart(int? cartId, int productId)
        {
            var cart = await appDb.Carts.FindAsync(cartId);

            if (cart == null)
            {
                return new HttpResponseMessage(System.Net.HttpStatusCode.MethodNotAllowed);
            }

            var cartProduct = await appDb.CartProducts.FindAsync(cart.CartId, productId);

            if (cartProduct == null)
            {
                return new HttpResponseMessage(System.Net.HttpStatusCode.MethodNotAllowed);
            }

            if (cartProduct.Quantity <= 1)
            {
                appDb.CartProducts.Remove(cartProduct);
            }
            else
            {
                cartProduct.Quantity--;
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

        public List<ProductQuantity> GetCartProducts(int cartId)
        {
            var products = appDb.CartProducts.Where(c => c.CartId == cartId)
                .Select(p => new ProductQuantity(p.Product, p.Quantity)).ToList();

            return products;
        }          
    }
}
