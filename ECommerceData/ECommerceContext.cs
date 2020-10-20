using ECommerceModels.Authentication;
using ECommerceModels.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

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
        public DbSet<OptionGroup> OptionGroups { get; set; }
        public DbSet<ProductOption> ProductOption { get; set; }
        public DbSet<ShoppingCart> Carts { get; set; }
        public DbSet<CartProduct> CartProducts { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<DeliveryMethod> DeliveryMethods { get; set; }
        public DbSet<PaymentMethod> PaymentMethods { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.SeedData();
            
            builder.Entity<ProductOption>().HasKey(key => new { key.OptionId, key.ProductId });

            builder.Entity<CartProduct>().HasKey(key => new { key.CartId, key.ProductId });

            builder.Entity<OrderItem>().HasKey(key => new { key.OrderId, key.ProductId });

            builder.Entity<CartProduct>()
                .HasOne<Product>(p => p.Product)
                .WithMany(c => c.CartProducts)
                .HasForeignKey(p => p.ProductId);

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
                .HasForeignKey(p => p.ProductId);

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

            builder.Entity<OptionGroup>()
               .HasMany(o => o.Options)
               .WithOne(og => og.OptionGroup);

            builder.Entity<ShoppingCart>()
                .HasOne(u => u.AppUser)
                .WithOne(c => c.Cart)
                .HasForeignKey<ShoppingCart>(i => i.UserId);

            builder.Entity<OrderItem>()
               .HasOne<Product>(p => p.Product)
               .WithMany(o => o.OrderItems)
               .HasForeignKey(p => p.ProductId);

            builder.Entity<OrderItem>()
                .HasOne<Order>(o => o.Order)
                .WithMany(o => o.Items)
                .HasForeignKey(o => o.OrderId);

            builder.Entity<OrderItem>()
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
