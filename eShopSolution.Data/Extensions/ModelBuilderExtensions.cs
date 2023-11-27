using eShopSolution.Data.Entities;
using eShopSolution.Data.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace eShopSolution.Data.Extensions
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AppConfig>().HasData(
               new AppConfig() { Key = "HomeTitle", Value = "This is home page of eShopSolution" },
               new AppConfig() { Key = "HomeKeyword", Value = "This is keyword of eShopSolution" },
               new AppConfig() { Key = "HomeDescription", Value = "This is description of eShopSolution" }
               );
            modelBuilder.Entity<Language>().HasData(
                new Language() { Id = "vi-VN", Name = "Tiếng Việt", IsDefault = true },
                new Language() { Id = "en-US", Name = "English", IsDefault = false });

            modelBuilder.Entity<Category>().HasData(
                new Category()
                {
                    Id = 1,
                    IsShowOnHome = true,
                    ParentId = null,
                    SortOrder = 1,
                    Status = Status.Active,
                },
                 new Category()
                 {
                     Id = 2,
                     IsShowOnHome = true,
                     ParentId = null,
                     SortOrder = 2,
                     Status = Status.Active
                 });

            modelBuilder.Entity<CategoryTranslation>().HasData(
                  new CategoryTranslation() { Id = 1, CategoryId = 1, Name = "Áo nam", LanguageId = "vi-VN", SeoAlias = "ao-nam", SeoDescription = "Sản phẩm áo thời trang nam", SeoTitle = "Sản phẩm áo thời trang nam" },
                  new CategoryTranslation() { Id = 2, CategoryId = 1, Name = "Men Shirt", LanguageId = "en-US", SeoAlias = "men-shirt", SeoDescription = "The shirt products for men", SeoTitle = "The shirt products for men" },
                  new CategoryTranslation() { Id = 3, CategoryId = 2, Name = "Áo nữ", LanguageId = "vi-VN", SeoAlias = "ao-nu", SeoDescription = "Sản phẩm áo thời trang nữ", SeoTitle = "Sản phẩm áo thời trang women" },
                  new CategoryTranslation() { Id = 4, CategoryId = 2, Name = "Women Shirt", LanguageId = "en-US", SeoAlias = "women-shirt", SeoDescription = "The shirt products for women", SeoTitle = "The shirt products for women" }
                    );

            modelBuilder.Entity<Product>().HasData(
           new Product()
           {
               Id = 2,
               DateCreated = DateTime.Now,
               OriginalPrice = 150000,
               Price = 300000,
               Stock = 0,
               ViewCount = 0,
           });
            modelBuilder.Entity<ProductTranslation>().HasData(
                 new ProductTranslation()
                 {
                     Id = 3,    
                     ProductId = 2,
                     Name = "Áo sơ mi nữ Việt Tiến",
                     LanguageId = "vi-VN",
                     SeoAlias = "ao-so-mi-nữ-trang-viet-tien",
                     SeoDescription = "Áo sơ mi nữ trắng Việt Tiến",
                     SeoTitle = "Áo sơ mi nữ trắng Việt Tiến",
                     Details = "Áo sơ mi nữ trắng Việt Tiến",
                     Description = "Áo sơ mi nữ trắng Việt Tiến"
                 },
                    new ProductTranslation()
                    {
                        Id = 4,
                        ProductId = 2,
                        Name = "Viet Tien woman T-Shirt",
                        LanguageId = "en-US",
                        SeoAlias = "viet-tien-woman-t-shirt",
                        SeoDescription = "Viet Tien woman T-Shirt",
                        SeoTitle = "Viet Tien woman T-Shirt",
                        Details = "Viet Tien woman T-Shirt",
                        Description = "Viet Tien woman T-Shirt"
                    });
            modelBuilder.Entity<ProductInCategory>().HasData(
                new ProductInCategory() { ProductId = 2, CategoryId = 2 }
                );
        }


    }
}