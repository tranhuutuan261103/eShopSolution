using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eShopSolution.Data.Entities;
using Microsoft.EntityFrameworkCore;
using eShopSolution.Data.Enums;
using Microsoft.AspNetCore.Identity;

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
                new Language() { Id = "en-US", Name = "English", IsDefault = false }
            );

            modelBuilder.Entity<Category>().HasData(
                new Category()
                {
                    Id = 1,
                    IsShowOnHome = true,
                    ParentId = null,
                    SortOrder = 1,
                    Status = Status.Active
                },
                new Category()
                {
                    Id = 2,
                    IsShowOnHome = true,
                    ParentId = null,
                    SortOrder = 2,
                    Status = Status.Active
                }
            );

            modelBuilder.Entity<CategoryTranslation>().HasData(
                new CategoryTranslation()
                {
                    Id = 1,
                    CategoryId = 1,
                    Name = "Áo nam",
                    LanguageId = "vi-VN",
                    SeoAlias = "ao-nam",
                    SeoDescription = "Sản phẩm áo thời trang nam",
                    SeoTitle = "Sản phẩm áo thời trang nam"
                },
                new CategoryTranslation()
                {
                    Id = 2,
                    CategoryId = 1,
                    Name = "Men shirt",
                    LanguageId = "en-US",
                    SeoAlias = "men-shirt",
                    SeoDescription = "The shirt product for men",
                    SeoTitle = "The shirt product for men"
                },
                new CategoryTranslation()
                {
                    Id = 3,
                    CategoryId = 2,
                    Name = "Áo nữ",
                    LanguageId = "vi-VN",
                    SeoAlias = "ao-nu",
                    SeoDescription = "Sản phẩm áo thời trang nữ",
                    SeoTitle = "Sản phẩm áo thời trang nữ"
                },
                new CategoryTranslation()
                {
                     Id = 4,
                     CategoryId = 2,
                     Name = "Women shirt",
                     LanguageId = "en-US",
                     SeoAlias = "women-shirt",
                     SeoDescription = "The shirt product for women",
                     SeoTitle = "The shirt product for women"
                }
            );

            modelBuilder.Entity<Product>().HasData(
                new Product()
                {
                    Id = 1,
                    DateCreated = DateTime.Now,
                    OriginalPrice = 100000,
                    Price = 200000,
                    Stock = 0,
                    ViewCount = 0
                }
            );

            modelBuilder.Entity<ProductTranslation>().HasData(
                new ProductTranslation() {
                     Id = 1,
                     ProductId = 1,
                     Name = "Áo sơ mi nam trắng Việt Tiến",
                     LanguageId = "vi-VN",
                     SeoAlias = "ao-nam",
                     SeoDescription = "Áo sơ mi nam trắng Việt Tiến",
                     SeoTitle = "Sản phẩm áo thời trang nam",
                     Details = "Áo sơ mi nam trắng Việt Tiến",
                     Description = "Áo sơ mi nam trắng Việt Tiến"
                },
                new ProductTranslation() {
                     Id = 2,
                     ProductId = 1,
                     Name = "Viet Tien Men T-shirt",
                     LanguageId = "en-US",
                     SeoAlias = "viet-tien-men-t-shirt",
                     SeoDescription = "Viet Tien Men T-shirt",
                     SeoTitle = "Viet Tien Men T-shirt",
                     Details = "Viet Tien Men T-shirt",
                     Description = "Viet Tien Men T-shirt"
                }
            );

            modelBuilder.Entity<ProductInCategory>().HasData(
                new ProductInCategory() { ProductId = 1, CategoryId = 1 }
            );

            // any guid
            var ADMIN_ID = new Guid("D2F0A1F9-7D0C-4F0B-B7E3-2B0B265C1F6E");
            var ROLE_ID = new Guid("E2F0A1F9-7D0C-4F0B-B7E3-2B0B265C1F6E");

            modelBuilder.Entity<AppRole>().HasData(new AppRole
            {
                Id = ROLE_ID,
                Name = "admin",
                NormalizedName = "admin",
                Description = "Administrator role",
            });

            var hasher = new PasswordHasher<AppUser>();
            modelBuilder.Entity<AppUser>().HasData(new AppUser
            {
                Id = ADMIN_ID,
                UserName = "admin",
                NormalizedUserName = "admin",
                Email = "some-admin@gmail.com",
                NormalizedEmail = "some-admin@gmail.com",
                EmailConfirmed = true,
                PasswordHash = hasher.HashPassword(null, "123456"),
                SecurityStamp = string.Empty,
                FirstName = "Tuân",
                LastName = "Trần",
                Dob = new DateTime(2003, 11, 26)
            });

            modelBuilder.Entity<IdentityUserRole<Guid>>().HasData(new IdentityUserRole<Guid>
            {
                RoleId = ADMIN_ID,
                UserId = ADMIN_ID
            });

			modelBuilder.Entity<Slide>().HasData(
				new Slide()
                {
                    Id = 1,
					Name = "Second Thumbnail label",
					Description = "Cras justo odio, dapibus ac facilisis in, egestas eget quam. Donec id elit non mi porta gravida at eget metus. Nullam id dolor id nibh ultricies vehicula ut id elit.",
					Url = "#",
					Image = "themes/images/carousel/1.png",
					SortOrder = 1,
					Status = Status.Active
				},

				new Slide()
				{
					Id = 2,
					Name = "Second Thumbnail label",
					Description = "Cras justo odio, dapibus ac facilisis in, egestas eget quam. Donec id elit non mi porta gravida at eget metus. Nullam id dolor id nibh ultricies vehicula ut id elit.",
					Url = "#",
					Image = "themes/images/carousel/2.png",
					SortOrder = 2,
					Status = Status.Active
				},

				new Slide()
				{
					Id = 3,
					Name = "Second Thumbnail label",
					Description = "Cras justo odio, dapibus ac facilisis in, egestas eget quam. Donec id elit non mi porta gravida at eget metus. Nullam id dolor id nibh ultricies vehicula ut id elit.",
					Url = "#",
					Image = "themes/images/carousel/3.png",
					SortOrder = 3,
					Status = Status.Active
				},

				new Slide()
				{
					Id = 4,
					Name = "Second Thumbnail label",
					Description = "Cras justo odio, dapibus ac facilisis in, egestas eget quam. Donec id elit non mi porta gravida at eget metus. Nullam id dolor id nibh ultricies vehicula ut id elit.",
					Url = "#",
					Image = "themes/images/carousel/4.png",
					SortOrder = 4,
					Status = Status.Active
				},

				new Slide()
				{
					Id = 5,
					Name = "Second Thumbnail label",
					Description = "Cras justo odio, dapibus ac facilisis in, egestas eget quam. Donec id elit non mi porta gravida at eget metus. Nullam id dolor id nibh ultricies vehicula ut id elit.",
					Url = "#",
					Image = "themes/images/carousel/5.png",
					SortOrder = 5,
					Status = Status.Active
				},

				new Slide()
				{
					Id = 6,
					Name = "Second Thumbnail label",
					Description = "Cras justo odio, dapibus ac facilisis in, egestas eget quam. Donec id elit non mi porta gravida at eget metus. Nullam id dolor id nibh ultricies vehicula ut id elit.",
					Url = "#",
					Image = "themes/images/carousel/6.png",
					SortOrder = 6,
					Status = Status.Active
				}
			);
		}
    }
}
