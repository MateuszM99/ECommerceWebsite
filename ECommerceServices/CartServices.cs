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

        public async Task<CartResponse> AddToCart(int? cartId, int productId,int? quantity,string optionName)
        {
            var cart = await appDb.Carts.FindAsync(cartId);
            var cartProduct = await appDb.CartProducts.FindAsync(cart.CartId, productId);
            var productOption = await appDb.Options.Where(o => o.OptionName == optionName).FirstOrDefaultAsync();

            // If null return error indicator
            if (productOption == null)
                return new CartResponse { ErrorMessage = "Option not found" };

            var productStock = appDb.ProductOption.Where(p => p.ProductId == productId && p.OptionId == productOption.OptionId).Select(p => p.ProductStock).FirstOrDefault();

            if (productStock < quantity)
                return new CartResponse { ErrorMessage = "Not enough products in stock" };

            if (cart == null)
            {
                cart = new ShoppingCart();
                await appDb.AddAsync(cart);
                await appDb.SaveChangesAsync();
            }
                      
            if(quantity == null)
            {
                quantity = 1;
            }
         

            if (cartProduct == null)
            {
                cartProduct = new CartProduct()
                {
                    CartId = cart.CartId,
                    ProductId = productId,
                    Quantity = (int)quantity,
                    OptionId = productOption.OptionId,
                    Option = productOption
                };
                await appDb.CartProducts.AddAsync(cartProduct);
            }
            else
            {
                cartProduct.Quantity += (int)quantity;
            }

            await appDb.SaveChangesAsync();

            cart.TotalPrice = GetCartPrice(cart.CartId);
            await appDb.SaveChangesAsync();

            return new CartResponse { CartId = cart.CartId, SuccessMessage = "Succesfully added product to cart"};
        }

        public async Task<CartResponse> RemoveFromCart(int? cartId, int productId)
        {
            var cart = await appDb.Carts.FindAsync(cartId);

            if (cart == null)
            {
                return new CartResponse {ErrorMessage = "You must specify cart id" };
            }

            var cartProduct = await appDb.CartProducts.FindAsync(cart.CartId, productId);

            if (cartProduct == null)
            {
                return new CartResponse { ErrorMessage = "Not found product with given id" };
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

            return new CartResponse { SuccessMessage = "Succesfully removed item from cart" };
        }

        public float GetCartPrice(int cartId)
        {
            return appDb.CartProducts
                        .Where(x => x.CartId == cartId)
                        .Select(x => x.Product.ProductPrice * x.Quantity)
                        .Sum();
        }

        public List<ProductOptionQuantity> GetCartProducts(int cartId)
        {
            var products = appDb.CartProducts.Include(cp => cp.Option).Where(c => c.CartId == cartId)
                .Select(p => new ProductOptionQuantity(p.Product, p.Quantity,p.Option)).ToList();

            return products;
        }          
    }
}
