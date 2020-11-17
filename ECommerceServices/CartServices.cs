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

        public async Task<CartResponse> addToCartAsync(int? cartId, int productId,int? quantity,string optionName)
        {
            var cart = await appDb.Carts.FindAsync(cartId);

            if (cart == null)
            {
                cart = new ShoppingCart();
                await appDb.AddAsync(cart);
                await appDb.SaveChangesAsync();
            }

            var cartProduct = await appDb.CartProducts.FindAsync(cart.Id, productId);
            var productOption = await appDb.Options.Where(o => o.Name == optionName).FirstOrDefaultAsync();

            // If null return error indicator
            if (productOption == null)
                return new CartResponse { Status="Error", Message = "Option not found" };

            var productStock = appDb.ProductOption.Where(p => p.ProductId == productId && p.OptionId == productOption.Id).Select(p => p.ProductStock).FirstOrDefault();

            if (productStock < quantity)
                return new CartResponse { Status = "Error", Message = "Not enough products in stock" };
                               
            if(quantity == null)
            {
                quantity = 1;
            }
         
            if (cartProduct == null)
            {
                cartProduct = new CartProduct()
                {
                    CartId = cart.Id,
                    ProductId = productId,
                    Quantity = (int)quantity,
                    OptionId = productOption.Id,
                    Option = productOption
                };
                await appDb.CartProducts.AddAsync(cartProduct);
            }
            else
            {          

                if(productStock <= cartProduct.Quantity)
                {
                    return new CartResponse { Status = "Error", Message = "Not enough products in stock" };
                }

                cartProduct.Quantity += (int)quantity;
            }

            await appDb.SaveChangesAsync();

            cart.TotalPrice = await getCartPriceAsync(cart.Id);
            await appDb.SaveChangesAsync();
            var cartCount = await getCartProductsCountAsync(cart.Id);
            var cartProducts = await getCartProductsAsync(cart.Id);


            return new CartResponse { CartId = cart.Id,CartPrice = cart.TotalPrice,CartCount = cartCount, CartProducts = cartProducts, Status="Success", Message = "Succesfully added product to cart"};
        }

        public async Task<CartResponse> removeFromCartAsync(int? cartId, int productId)
        {
            var cart = await appDb.Carts.FindAsync(cartId);

            if (cart == null)
            {
                return new CartResponse { Status = "Error", Message = "You must specify cart id" };
            }

            var cartProduct = await appDb.CartProducts.FindAsync(cart.Id, productId);

            if (cartProduct == null)
            {
                return new CartResponse { Status = "Error", Message = "Not found product with given id" };
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
            cart.TotalPrice = await getCartPriceAsync(cart.Id);
            await appDb.SaveChangesAsync();
            var cartCount = await getCartProductsCountAsync(cart.Id);
            var cartProducts = await getCartProductsAsync(cart.Id);

            return new CartResponse { CartId = cart.Id,CartPrice = cart.TotalPrice, CartCount = cartCount, CartProducts = cartProducts, Status = "Success", Message = "Succesfully removed item from cart" };
        }

        public async Task<double> getCartPriceAsync(int cartId)
        {
            return await appDb.CartProducts
                        .Where(x => x.CartId == cartId)
                        .Select(x => x.Product.Price * x.Quantity)
                        .SumAsync();
        }

        public async Task<List<ProductOptionQuantity>> getCartProductsAsync(int cartId)
        {
            var products = await appDb.CartProducts.Include(cp => cp.Option).Where(c => c.CartId == cartId)
                .Select(p => new ProductOptionQuantity(p.Product, p.Quantity,p.Option)).ToListAsync();

            return products;
        }

        public async Task<int> getCartProductsCountAsync(int cartId)
        {
            List<ProductOptionQuantity> productQuantities = await getCartProductsAsync(cartId);

            int count = 0;

            foreach (var product in productQuantities)
            {
                count += 1 * product.quantity;
            }

            return count;
        }
    }
}
