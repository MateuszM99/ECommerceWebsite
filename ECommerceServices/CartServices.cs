using ECommerceData;
using ECommerceIServices;
using ECommerceModels.Authentication;
using ECommerceModels.Models;
using ECommerceModels.RequestModels.CartRequestModels;
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

        public async Task<CartResponse> addToCartAsync(AddToCartModel addToCartModel)
        {
            if (addToCartModel == null)
            {
                return new CartResponse { Status = "Error", Message = "Bad request" };
            }

            var cart = await appDb.Carts.SingleOrDefaultAsync(x => x.Id == addToCartModel.CartId);

            if (cart == null)
            {
                cart = new ShoppingCart();
                await appDb.AddAsync(cart);
                await appDb.SaveChangesAsync();
            }

            var cartProduct = await appDb.CartProducts.FindAsync(cart.Id, addToCartModel.ProductId, addToCartModel.ProductVariationId);
            var productOption = await appDb.Options.Where(o => o.Name == addToCartModel.OptionName).FirstOrDefaultAsync();

            // If null return error indicator
            if (productOption == null)
            {
                return new CartResponse { Status = "Error", Message = "Option not found" };
            }

            var productStock = appDb.ProductOptions
                                .Where(p => p.ProductId == addToCartModel.ProductId && p.ProductVariationId == addToCartModel.ProductVariationId && p.OptionId == productOption.Id)
                                .Select(p => p.ProductStock)
                                .FirstOrDefault();

            if (productStock < addToCartModel.Quantity)
            {
                return new CartResponse { Status = "Error", Message = "Not enough products in stock" };
            }
                               
            if(addToCartModel.Quantity == null)
            {
                addToCartModel.Quantity = 1;
            }
         
            if (cartProduct == null)
            {
                cartProduct = new CartProduct()
                {
                    CartId = cart.Id,
                    ProductId = addToCartModel.ProductId,
                    ProductVariationId = addToCartModel.ProductVariationId,
                    Quantity = (int)addToCartModel.Quantity,
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

                cartProduct.Quantity += (int)addToCartModel.Quantity;
            }

            await appDb.SaveChangesAsync();

            cart.TotalPrice = await getCartPriceAsync(cart.Id);
            await appDb.SaveChangesAsync();
            var cartCount = await getCartProductsCountAsync(cart.Id);
            var cartProducts = await getCartProductsAsync(cart.Id);


            return new CartResponse { CartId = cart.Id,CartPrice = cart.TotalPrice,CartCount = cartCount, CartProducts = cartProducts, Status="Success", Message = "Succesfully added product to cart"};
        }

        public async Task<CartResponse> removeFromCartAsync(RemoveFromCartModel removeFromCartModel)
        {
            var cart = await appDb.Carts.FindAsync(removeFromCartModel.CartId);

            if (cart == null)
            {
                return new CartResponse { Status = "Error", Message = "You must specify cart id" };
            }

            var cartProduct = await appDb.CartProducts.FindAsync(cart.Id, removeFromCartModel.ProductId, removeFromCartModel.ProductVariationId);

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

            if(cartProducts == null)
            {
                appDb.Carts.Remove(cart);
                await appDb.SaveChangesAsync();
            }

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
            var products = await appDb.CartProducts
                                .Include(cp => cp.Option)
                                .Where(c => c.CartId == cartId)
                                .Select(p => new ProductOptionQuantity(p.Product, p.Quantity,p.Option))
                                .ToListAsync();

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
