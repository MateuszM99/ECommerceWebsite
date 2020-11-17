using ECommerceModels.Authentication;
using ECommerceModels.Enums;
using ECommerceModels.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerceData
{
    public static class ModelBuilderExtension
    {        
        public static void SeedData(this ModelBuilder modelBuilder)
        {
         
            modelBuilder.Entity<Option>()
                .HasData(
                    new Option
                    {
                        Id = 1,
                        Name = Size.XS.ToString(),                       
                    },
                    new Option
                    {
                        Id = 2,
                        Name = Size.S.ToString(),                      
                    },
                    new Option
                    {
                        Id = 3,
                        Name = Size.M.ToString(),                        
                    },
                    new Option
                    {
                        Id = 4,
                        Name = Size.L.ToString(),                        
                    },
                     new Option
                     {
                         Id = 5,
                         Name = Size.XL.ToString(),                      
                     },
                    new Option
                    {
                        Id = 6,
                        Name = Size.XXL.ToString(),                        
                    }                 
                );

            modelBuilder.Entity<Category>()
                .HasData(
                    new Category 
                    {
                        Id = 1,
                        Name = "T-shirt"
                    },
                    new Category
                    {
                        Id = 2,
                        Name = "Jumper"
                    },
                    new Category
                    {
                        Id = 3,
                        Name = "Longsleeve"
                    }
                );

            modelBuilder.Entity<Product>()
                .HasData(
                    new Product
                    {
                        Id = 1,
                        VariationId = 1,
                        Name = "Black t-shirt",
                        SKU = "BL-T-1",
                        Price = 24.99,
                        Description = "Plain black silk t-shirt",
                        AddedAt = DateTime.Now,
                        CategoryId = 1
                    },
                    new Product
                    {
                        Id = 2,
                        VariationId = 1,
                        Name = "White t-shirt",
                        SKU = "WT-T-2",
                        Price = 24.99,
                        Description = "Plain white silk t-shirt",
                        AddedAt = DateTime.Now,
                        CategoryId = 1
                    },
                    new Product
                    {
                        Id = 3,
                        VariationId = 1,
                        Name = "Bogo Jumper",
                        SKU = "BG-JMP-3",
                        Price = 69.99,
                        Description = "Jumper with logo",
                        AddedAt = DateTime.Now,
                        CategoryId = 2
                    }, 
                    new Product
                    {
                        Id = 4,
                        VariationId = 1,
                        Name = "Oversize hoodie",
                        SKU = "OS-H-4",
                        Price = 79.99,
                        Description = "Comfortable oversize hoodie",
                        AddedAt = DateTime.Now,
                        CategoryId = 2
                    },
                     new Product
                     {
                         Id = 5,
                         VariationId = 1,
                         Name = "Grey Stripped Longsleeve",
                         SKU = "GRST-LS-2",
                         Price = 49.99,
                         Description = "Longsleeve with white stripes",
                         AddedAt = DateTime.Now,
                         CategoryId = 3
                     }
                );

            modelBuilder.Entity<ProductOption>()
                .HasData(
                new ProductOption
                {
                    ProductId = 1,
                    ProductVariationId = 1,
                    OptionId = 1,
                    ProductStock = 5
                }, 
                new ProductOption
                {
                    ProductId = 1,
                    ProductVariationId = 1,
                    OptionId = 2,
                    ProductStock = 5
                }, 
                new ProductOption
                {
                    ProductId = 1,
                    ProductVariationId = 1,
                    OptionId = 3,
                    ProductStock = 5
                }, 
                new ProductOption
                {
                    ProductId = 1,
                    ProductVariationId = 1,
                    OptionId = 4,
                    ProductStock = 5
                }, 
                new ProductOption
                {
                    ProductId = 1,
                    ProductVariationId = 1,
                    OptionId = 5,
                    ProductStock = 5
                },
                new ProductOption
                {
                    ProductId = 2,
                    ProductVariationId = 1,
                    OptionId = 1,
                    ProductStock = 5
                },
                new ProductOption
                {
                    ProductId = 2,
                    ProductVariationId = 1,
                    OptionId = 2,
                    ProductStock = 5
                },
                new ProductOption
                {
                    ProductId = 2,
                    ProductVariationId = 1,
                    OptionId = 3,
                    ProductStock = 5
                },
                new ProductOption
                {
                    ProductId = 2,
                    ProductVariationId = 1,
                    OptionId = 4,
                    ProductStock = 5
                },
                new ProductOption
                {
                    ProductId = 2,
                    ProductVariationId = 1,
                    OptionId = 5,
                    ProductStock = 5
                },
                new ProductOption
                {
                    ProductId = 3,
                    ProductVariationId = 1,
                    OptionId = 1,
                    ProductStock = 5
                },
                new ProductOption
                {
                    ProductId = 3,
                    ProductVariationId = 1,
                    OptionId = 2,
                    ProductStock = 5
                },
                new ProductOption
                {
                    ProductId = 3,
                    ProductVariationId = 1,
                    OptionId = 3,
                    ProductStock = 5
                },
                new ProductOption
                {
                    ProductId = 3,
                    ProductVariationId = 1,
                    OptionId = 4,
                    ProductStock = 5
                },
                new ProductOption
                {
                    ProductId = 3,
                    ProductVariationId = 1,
                    OptionId = 5,
                    ProductStock = 5
                },
                 new ProductOption
                 {
                     ProductId = 4,
                     ProductVariationId = 1,
                     OptionId = 4,
                     ProductStock = 5
                 },
                new ProductOption
                {
                    ProductId = 4,
                    ProductVariationId = 1,
                    OptionId = 5,
                    ProductStock = 5
                },
                 new ProductOption
                 {
                     ProductId = 5,
                     ProductVariationId = 1,
                     OptionId = 4,
                     ProductStock = 5
                 },
                new ProductOption
                {
                    ProductId = 5,
                    ProductVariationId = 1,
                    OptionId = 5,
                    ProductStock = 5
                }
                );

            modelBuilder.Entity<DeliveryMethod>()
                .HasData(
                    new DeliveryMethod
                    {
                        Id = 1,
                        Name = "DPD Courier",
                        Price = 14.99
                    },
                    new DeliveryMethod
                    {
                        Id = 2,
                        Name = "DHL Courier",
                        Price = 12.99
                    }
                );

            modelBuilder.Entity<PaymentMethod>()
                .HasData(
                    new PaymentMethod
                    {
                        Id = 1,
                        Name = "Cash on delivery",                     
                    }
                );


        }
        
        public static void SeedUsersData(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
        {
            string adminUsername = "admin";

            var userExists = userManager.FindByNameAsync(adminUsername).Result;
            if (userExists == null)
            {
                ApplicationUser admin = new ApplicationUser()
                {
                    Email = "raidenplayforyou@gmail.com",
                    SecurityStamp = Guid.NewGuid().ToString(),
                    UserName = adminUsername
                };

                userManager.CreateAsync(admin,"Test%123").GetAwaiter();

                if (!roleManager.RoleExistsAsync(UserRoles.Admin).Result)
                    roleManager.CreateAsync(new IdentityRole(UserRoles.Admin)).GetAwaiter();

                if (roleManager.RoleExistsAsync(UserRoles.Admin).Result)
                {
                    userManager.AddToRoleAsync(admin, UserRoles.Admin).GetAwaiter();
                }
            }
        }

    }
}
