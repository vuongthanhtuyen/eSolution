using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eShopSolution.Data.Migrations
{
    /// <inheritdoc />
    public partial class addProductImageTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProductImages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    ImagePath = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Caption = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    IsDefault = table.Column<bool>(type: "bit", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SortOrder = table.Column<int>(type: "int", nullable: false),
                    FileSize = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductImages_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("7d24e79a-210b-4788-b7b6-d2a1aa192a02"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "84d78e51-bece-4379-9730-597e19d60f8b", "AQAAAAIAAYagAAAAEA9VgR9nGZihULy4adREf5vWQBUpMQc6HCYQb2gfbIzyIb4zxSLjq95X5dxtzu+h/A==" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2023, 11, 29, 14, 29, 37, 576, DateTimeKind.Local).AddTicks(8073));

            migrationBuilder.CreateIndex(
                name: "IX_ProductImages_ProductId",
                table: "ProductImages",
                column: "ProductId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductImages");

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("7d24e79a-210b-4788-b7b6-d2a1aa192a02"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "0a3dc9ef-1f3d-452f-8944-94123262f5aa", "AQAAAAIAAYagAAAAEOZJqQkKb6GrqgcaPQKMD1GimrT8jRZ6wXgdWGijMI+6jDnzGvRHE9i7PM5GkzgFQg==" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2023, 11, 28, 11, 19, 59, 404, DateTimeKind.Local).AddTicks(3267));
        }
    }
}
