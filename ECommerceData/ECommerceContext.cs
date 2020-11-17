using ECommerceModels.Authentication;
using ECommerceModels.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerceData
{
    public class ECommerceContext : IdentityDbContext<ApplicationUser>
    {
        public ECommerceContext(DbContextOptions<ECommerceContext> options)
            : base(options)
        {
               
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Option> Options { get; set; }      
        public DbSet<ProductOption> ProductOptions { get; set; }
        public DbSet<ShoppingCart> Carts { get; set; }
        public DbSet<CartProduct> CartProducts { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderProduct> OrderProducts { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<DeliveryMethod> DeliveryMethods { get; set; }
        public DbSet<PaymentMethod> PaymentMethods { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.SeedData();

            builder.Entity<Product>()               
                .HasKey(key => new { key.Id, key.VariationId });
            
            

            builder.Entity<ProductOption>().HasKey(key => new { key.OptionId,key.ProductId,key.ProductVariationId});

            builder.Entity<CartProduct>().HasKey(key => new { key.CartId, key.ProductId, key.ProductVariationId});

            builder.Entity<OrderProduct>().HasKey(key => new { key.OrderId, key.ProductId, key.ProductVariationId});

            builder.Entity<CartProduct>()
                .HasOne<Product>(p => p.Product)
                .WithMany(c => c.CartProducts)
                .HasForeignKey(p => new { p.ProductId,p.ProductVariationId});

            builder.Entity<CartProduct>()
                .HasOne<ShoppingCart>(c => c.Cart)
                .WithMany(p => p.CartProducts)
                .HasForeignKey(c => c.CartId);

            builder.Entity<CartProduct>()
                .HasOne<Option>(o => o.Option)
                .WithMany(c => c.CartProducts)
                .HasForeignKey(o => o.OptionId);

            builder.Entity<ProductOption>()
                .HasOne<Product>(p => p.Product)
                .WithMany(po => po.ProductOptions)
                .HasForeignKey(p => new { p.ProductId, p.ProductVariationId });

            builder.Entity<ProductOption>()
               .HasOne<Option>(o => o.Option)
               .WithMany(po => po.ProductOptions)

               .HasForeignKey(o => o.OptionId);

            builder.Entity<ApplicationUser>()
                .HasOne(a => a.Address)
                .WithMany(a => a.ApplicationUsers)
                .HasForeignKey(a => a.AdresId);

            builder.Entity<Address>()
                .HasMany(u => u.ApplicationUsers)
                .WithOne(a => a.Address);

            builder.Entity<Category>()
               .HasMany(p => p.Products)
               .WithOne(c => c.Category);
           
            builder.Entity<ShoppingCart>()
                .HasOne(u => u.AppUser)
                .WithOne(c => c.Cart)
                .HasForeignKey<ShoppingCart>(i => i.UserId);

            builder.Entity<OrderProduct>()
               .HasOne<Product>(p => p.Product)
               .WithMany(o => o.OrderProducts)
               .HasForeignKey(p => new { p.ProductId, p.ProductVariationId });

            builder.Entity<OrderProduct>()
                .HasOne<Order>(o => o.Order)
                .WithMany(o => o.Products)
                .HasForeignKey(o => o.OrderId);

            builder.Entity<OrderProduct>()
               .HasOne<Option>(o => o.Option)
               .WithMany(oi => oi.OrderItems)
               .HasForeignKey(o => o.OptionId);

            builder.Entity<Order>()
                .HasOne<ApplicationUser>(u => u.User)
                .WithMany(o => o.Orders)
                .HasForeignKey(u => u.UserId);

            builder.Entity<Order>()
                .HasOne<Address>(a => a.Address)
                .WithMany(o => o.Orders)
                .HasForeignKey(a => a.AddressId);

            builder.Entity<Order>()
                .HasOne<DeliveryMethod>(d => d.DeliveryMethod)
                .WithMany(o => o.Orders)
                .HasForeignKey(d => d.DeliveryMethodId);

            builder.Entity<Order>()
                .HasOne<PaymentMethod>(p => p.PaymentMethod)
                .WithMany(o => o.Orders)
                .HasForeignKey(p => p.PaymentMethodId);           
        }
    }
}
