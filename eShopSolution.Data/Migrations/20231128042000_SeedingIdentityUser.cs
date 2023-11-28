using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace eShopSolution.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedingIdentityUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ProductInCategories",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { 2, 2 });

            migrationBuilder.DeleteData(
                table: "ProductTranslations",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "ProductTranslations",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.InsertData(
                table: "AppRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Description", "Name", "NormalizedName" },
                values: new object[] { new Guid("3917110d-c1c0-4659-9dde-05dc69855208"), null, "Administrator role", "admin", "admin" });

            migrationBuilder.InsertData(
                table: "AppUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { new Guid("3917110d-c1c0-4659-9dde-05dc69855208"), new Guid("7d24e79a-210b-4788-b7b6-d2a1aa192a02") });

            migrationBuilder.InsertData(
                table: "AppUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Dob", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { new Guid("7d24e79a-210b-4788-b7b6-d2a1aa192a02"), 0, "0a3dc9ef-1f3d-452f-8944-94123262f5aa", new DateTime(2023, 1, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), "vuongthanhtuyen13579@gmail.com", true, "Tuyen", "Vuong", false, null, "tedu.international@gmail.com", "admin", "AQAAAAIAAYagAAAAEOZJqQkKb6GrqgcaPQKMD1GimrT8jRZ6wXgdWGijMI+6jDnzGvRHE9i7PM5GkzgFQg==", null, false, "", false, "admin" });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "DateCreated", "IsFeatured", "OriginalPrice", "Price" },
                values: new object[] { 1, new DateTime(2023, 11, 28, 11, 19, 59, 404, DateTimeKind.Local).AddTicks(3267), null, 100000m, 200000m });

            migrationBuilder.InsertData(
                table: "ProductInCategories",
                columns: new[] { "CategoryId", "ProductId" },
                values: new object[] { 1, 1 });

            migrationBuilder.InsertData(
                table: "ProductTranslations",
                columns: new[] { "Id", "Description", "Details", "LanguageId", "Name", "ProductId", "SeoAlias", "SeoDescription", "SeoTitle" },
                values: new object[,]
                {
                    { 1, "Áo sơ mi nam trắng Việt Tiến", "Áo sơ mi nam trắng Việt Tiến", "vi-VN", "Áo sơ mi nam trắng Việt Tiến", 1, "ao-so-mi-nam-trang-viet-tien", "Áo sơ mi nam trắng Việt Tiến", "Áo sơ mi nam trắng Việt Tiến" },
                    { 2, "Viet Tien Men T-Shirt", "Viet Tien Men T-Shirt", "en-US", "Viet Tien Men T-Shirt", 1, "viet-tien-men-t-shirt", "Viet Tien Men T-Shirt", "Viet Tien Men T-Shirt" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("3917110d-c1c0-4659-9dde-05dc69855208"));

            migrationBuilder.DeleteData(
                table: "AppUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("3917110d-c1c0-4659-9dde-05dc69855208"), new Guid("7d24e79a-210b-4788-b7b6-d2a1aa192a02") });

            migrationBuilder.DeleteData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("7d24e79a-210b-4788-b7b6-d2a1aa192a02"));

            migrationBuilder.DeleteData(
                table: "ProductInCategories",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "ProductTranslations",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ProductTranslations",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "DateCreated", "IsFeatured", "OriginalPrice", "Price" },
                values: new object[] { 2, new DateTime(2023, 11, 28, 10, 26, 36, 951, DateTimeKind.Local).AddTicks(963), null, 150000m, 300000m });

            migrationBuilder.InsertData(
                table: "ProductInCategories",
                columns: new[] { "CategoryId", "ProductId" },
                values: new object[] { 2, 2 });

            migrationBuilder.InsertData(
                table: "ProductTranslations",
                columns: new[] { "Id", "Description", "Details", "LanguageId", "Name", "ProductId", "SeoAlias", "SeoDescription", "SeoTitle" },
                values: new object[,]
                {
                    { 3, "Áo sơ mi nữ trắng Việt Tiến", "Áo sơ mi nữ trắng Việt Tiến", "vi-VN", "Áo sơ mi nữ Việt Tiến", 2, "ao-so-mi-nữ-trang-viet-tien", "Áo sơ mi nữ trắng Việt Tiến", "Áo sơ mi nữ trắng Việt Tiến" },
                    { 4, "Viet Tien woman T-Shirt", "Viet Tien woman T-Shirt", "en-US", "Viet Tien woman T-Shirt", 2, "viet-tien-woman-t-shirt", "Viet Tien woman T-Shirt", "Viet Tien woman T-Shirt" }
                });
        }
    }
}
