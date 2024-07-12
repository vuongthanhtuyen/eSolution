using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eShopSolution.Data.Migrations
{
    /// <inheritdoc />
    public partial class changeFileLengthType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "FileSize",
                table: "ProductImages",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("7d24e79a-210b-4788-b7b6-d2a1aa192a02"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "e95228d9-9a6f-4cc5-bed4-532ffe2007e2", "AQAAAAIAAYagAAAAEDnpKQ9NiuJt6uKFPAHEkIPdHyJkvablr/ypX4Fp8buvwUPx4cIM71KtyRv+T3zypQ==" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2023, 11, 29, 20, 23, 22, 165, DateTimeKind.Local).AddTicks(8379));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "FileSize",
                table: "ProductImages",
                type: "int",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

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
        }
    }
}
