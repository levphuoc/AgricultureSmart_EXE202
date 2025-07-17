using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AgricultureSmart.Repositories.Migrations
{
    /// <inheritdoc />
    public partial class V3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "BlogCategories",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 7, 6, 12, 18, 19, 403, DateTimeKind.Utc).AddTicks(6117));

            migrationBuilder.UpdateData(
                table: "BlogCategories",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 7, 6, 12, 18, 19, 403, DateTimeKind.Utc).AddTicks(6117));

            migrationBuilder.UpdateData(
                table: "BlogCategories",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 7, 6, 12, 18, 19, 403, DateTimeKind.Utc).AddTicks(6117));

            migrationBuilder.UpdateData(
                table: "BlogCategories",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2025, 7, 6, 12, 18, 19, 403, DateTimeKind.Utc).AddTicks(6117));

            migrationBuilder.UpdateData(
                table: "BlogCategories",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2025, 7, 6, 12, 18, 19, 403, DateTimeKind.Utc).AddTicks(6117));

            migrationBuilder.UpdateData(
                table: "Blogs",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PublishedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 21, 12, 18, 19, 403, DateTimeKind.Utc).AddTicks(6117), new DateTime(2025, 6, 26, 12, 18, 19, 403, DateTimeKind.Utc).AddTicks(6117), new DateTime(2025, 6, 26, 12, 18, 19, 403, DateTimeKind.Utc).AddTicks(6117) });

            migrationBuilder.UpdateData(
                table: "Blogs",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "PublishedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 24, 12, 18, 19, 403, DateTimeKind.Utc).AddTicks(6117), new DateTime(2025, 6, 29, 12, 18, 19, 403, DateTimeKind.Utc).AddTicks(6117), new DateTime(2025, 6, 29, 12, 18, 19, 403, DateTimeKind.Utc).AddTicks(6117) });

            migrationBuilder.UpdateData(
                table: "Blogs",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "PublishedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 28, 12, 18, 19, 403, DateTimeKind.Utc).AddTicks(6117), new DateTime(2025, 7, 1, 12, 18, 19, 403, DateTimeKind.Utc).AddTicks(6117), new DateTime(2025, 7, 1, 12, 18, 19, 403, DateTimeKind.Utc).AddTicks(6117) });

            migrationBuilder.UpdateData(
                table: "Blogs",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "PublishedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 30, 12, 18, 19, 403, DateTimeKind.Utc).AddTicks(6117), new DateTime(2025, 7, 3, 12, 18, 19, 403, DateTimeKind.Utc).AddTicks(6117), new DateTime(2025, 7, 3, 12, 18, 19, 403, DateTimeKind.Utc).AddTicks(6117) });

            migrationBuilder.UpdateData(
                table: "Blogs",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "PublishedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 7, 2, 12, 18, 19, 403, DateTimeKind.Utc).AddTicks(6117), new DateTime(2025, 7, 5, 12, 18, 19, 403, DateTimeKind.Utc).AddTicks(6117), new DateTime(2025, 7, 5, 12, 18, 19, 403, DateTimeKind.Utc).AddTicks(6117) });

            migrationBuilder.UpdateData(
                table: "CartItems",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 7, 4, 12, 18, 19, 403, DateTimeKind.Utc).AddTicks(6117), new DateTime(2025, 7, 4, 12, 18, 19, 403, DateTimeKind.Utc).AddTicks(6117) });

            migrationBuilder.UpdateData(
                table: "CartItems",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 7, 6, 6, 18, 19, 403, DateTimeKind.Utc).AddTicks(6117), new DateTime(2025, 7, 6, 6, 18, 19, 403, DateTimeKind.Utc).AddTicks(6117) });

            migrationBuilder.UpdateData(
                table: "CartItems",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 7, 6, 6, 18, 19, 403, DateTimeKind.Utc).AddTicks(6117), new DateTime(2025, 7, 6, 6, 18, 19, 403, DateTimeKind.Utc).AddTicks(6117) });

            migrationBuilder.UpdateData(
                table: "CartItems",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 7, 5, 12, 18, 19, 403, DateTimeKind.Utc).AddTicks(6117), new DateTime(2025, 7, 5, 12, 18, 19, 403, DateTimeKind.Utc).AddTicks(6117) });

            migrationBuilder.UpdateData(
                table: "CartItems",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 7, 6, 9, 18, 19, 403, DateTimeKind.Utc).AddTicks(6117), new DateTime(2025, 7, 6, 9, 18, 19, 403, DateTimeKind.Utc).AddTicks(6117) });

            migrationBuilder.UpdateData(
                table: "CartItems",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 7, 6, 9, 18, 19, 403, DateTimeKind.Utc).AddTicks(6117), new DateTime(2025, 7, 6, 9, 18, 19, 403, DateTimeKind.Utc).AddTicks(6117) });

            migrationBuilder.UpdateData(
                table: "CartItems",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 7, 6, 0, 18, 19, 403, DateTimeKind.Utc).AddTicks(6117), new DateTime(2025, 7, 6, 0, 18, 19, 403, DateTimeKind.Utc).AddTicks(6117) });

            migrationBuilder.UpdateData(
                table: "Carts",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 7, 4, 12, 18, 19, 403, DateTimeKind.Utc).AddTicks(6117), new DateTime(2025, 7, 6, 6, 18, 19, 403, DateTimeKind.Utc).AddTicks(6117) });

            migrationBuilder.UpdateData(
                table: "Carts",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 7, 5, 12, 18, 19, 403, DateTimeKind.Utc).AddTicks(6117), new DateTime(2025, 7, 6, 9, 18, 19, 403, DateTimeKind.Utc).AddTicks(6117) });

            migrationBuilder.UpdateData(
                table: "Carts",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 7, 6, 0, 18, 19, 403, DateTimeKind.Utc).AddTicks(6117), new DateTime(2025, 7, 6, 10, 18, 19, 403, DateTimeKind.Utc).AddTicks(6117) });

            migrationBuilder.UpdateData(
                table: "EngineerFarmerAssignments",
                keyColumn: "Id",
                keyValue: 1,
                column: "AssignedAt",
                value: new DateTime(2025, 7, 6, 12, 18, 19, 403, DateTimeKind.Utc).AddTicks(6117));

            migrationBuilder.UpdateData(
                table: "EngineerFarmerAssignments",
                keyColumn: "Id",
                keyValue: 2,
                column: "AssignedAt",
                value: new DateTime(2025, 7, 6, 12, 18, 19, 403, DateTimeKind.Utc).AddTicks(6117));

            migrationBuilder.UpdateData(
                table: "EngineerFarmerAssignments",
                keyColumn: "Id",
                keyValue: 3,
                column: "AssignedAt",
                value: new DateTime(2025, 7, 6, 12, 18, 19, 403, DateTimeKind.Utc).AddTicks(6117));

            migrationBuilder.UpdateData(
                table: "EngineerFarmerAssignments",
                keyColumn: "Id",
                keyValue: 4,
                column: "AssignedAt",
                value: new DateTime(2025, 7, 6, 12, 18, 19, 403, DateTimeKind.Utc).AddTicks(6117));

            migrationBuilder.UpdateData(
                table: "EngineerFarmerAssignments",
                keyColumn: "Id",
                keyValue: 5,
                column: "AssignedAt",
                value: new DateTime(2025, 7, 6, 12, 18, 19, 403, DateTimeKind.Utc).AddTicks(6117));

            migrationBuilder.UpdateData(
                table: "Engineers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 7, 6, 12, 18, 19, 403, DateTimeKind.Utc).AddTicks(6117), new DateTime(2025, 7, 6, 12, 18, 19, 403, DateTimeKind.Utc).AddTicks(6117) });

            migrationBuilder.UpdateData(
                table: "Engineers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 7, 6, 12, 18, 19, 403, DateTimeKind.Utc).AddTicks(6117), new DateTime(2025, 7, 6, 12, 18, 19, 403, DateTimeKind.Utc).AddTicks(6117) });

            migrationBuilder.UpdateData(
                table: "Engineers",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 7, 6, 12, 18, 19, 403, DateTimeKind.Utc).AddTicks(6117), new DateTime(2025, 7, 6, 12, 18, 19, 403, DateTimeKind.Utc).AddTicks(6117) });

            migrationBuilder.UpdateData(
                table: "Farmers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 7, 6, 12, 18, 19, 403, DateTimeKind.Utc).AddTicks(6117), new DateTime(2025, 7, 6, 12, 18, 19, 403, DateTimeKind.Utc).AddTicks(6117) });

            migrationBuilder.UpdateData(
                table: "Farmers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 7, 6, 12, 18, 19, 403, DateTimeKind.Utc).AddTicks(6117), new DateTime(2025, 7, 6, 12, 18, 19, 403, DateTimeKind.Utc).AddTicks(6117) });

            migrationBuilder.UpdateData(
                table: "Farmers",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 7, 6, 12, 18, 19, 403, DateTimeKind.Utc).AddTicks(6117), new DateTime(2025, 7, 6, 12, 18, 19, 403, DateTimeKind.Utc).AddTicks(6117) });

            migrationBuilder.UpdateData(
                table: "Farmers",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 7, 6, 12, 18, 19, 403, DateTimeKind.Utc).AddTicks(6117), new DateTime(2025, 7, 6, 12, 18, 19, 403, DateTimeKind.Utc).AddTicks(6117) });

            migrationBuilder.UpdateData(
                table: "Farmers",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 7, 6, 12, 18, 19, 403, DateTimeKind.Utc).AddTicks(6117), new DateTime(2025, 7, 6, 12, 18, 19, 403, DateTimeKind.Utc).AddTicks(6117) });

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PublishedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 7, 4, 12, 18, 19, 403, DateTimeKind.Utc).AddTicks(6117), new DateTime(2025, 7, 4, 12, 18, 19, 403, DateTimeKind.Utc).AddTicks(6117), new DateTime(2025, 7, 4, 12, 18, 19, 403, DateTimeKind.Utc).AddTicks(6117) });

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "PublishedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 7, 5, 12, 18, 19, 403, DateTimeKind.Utc).AddTicks(6117), new DateTime(2025, 7, 5, 12, 18, 19, 403, DateTimeKind.Utc).AddTicks(6117), new DateTime(2025, 7, 5, 12, 18, 19, 403, DateTimeKind.Utc).AddTicks(6117) });

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "PublishedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 7, 6, 0, 18, 19, 403, DateTimeKind.Utc).AddTicks(6117), new DateTime(2025, 7, 6, 0, 18, 19, 403, DateTimeKind.Utc).AddTicks(6117), new DateTime(2025, 7, 6, 0, 18, 19, 403, DateTimeKind.Utc).AddTicks(6117) });

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "PublishedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 7, 6, 6, 18, 19, 403, DateTimeKind.Utc).AddTicks(6117), new DateTime(2025, 7, 6, 6, 18, 19, 403, DateTimeKind.Utc).AddTicks(6117), new DateTime(2025, 7, 6, 6, 18, 19, 403, DateTimeKind.Utc).AddTicks(6117) });

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "PublishedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 7, 6, 9, 18, 19, 403, DateTimeKind.Utc).AddTicks(6117), new DateTime(2025, 7, 6, 9, 18, 19, 403, DateTimeKind.Utc).AddTicks(6117), new DateTime(2025, 7, 6, 9, 18, 19, 403, DateTimeKind.Utc).AddTicks(6117) });

            migrationBuilder.UpdateData(
                table: "NewsCategories",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 7, 6, 12, 18, 19, 403, DateTimeKind.Utc).AddTicks(6117));

            migrationBuilder.UpdateData(
                table: "NewsCategories",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 7, 6, 12, 18, 19, 403, DateTimeKind.Utc).AddTicks(6117));

            migrationBuilder.UpdateData(
                table: "NewsCategories",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 7, 6, 12, 18, 19, 403, DateTimeKind.Utc).AddTicks(6117));

            migrationBuilder.UpdateData(
                table: "NewsCategories",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2025, 7, 6, 12, 18, 19, 403, DateTimeKind.Utc).AddTicks(6117));

            migrationBuilder.UpdateData(
                table: "NewsCategories",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2025, 7, 6, 12, 18, 19, 403, DateTimeKind.Utc).AddTicks(6117));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PaidAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 16, 12, 18, 19, 403, DateTimeKind.Utc).AddTicks(6117), new DateTime(2025, 6, 21, 12, 18, 19, 403, DateTimeKind.Utc).AddTicks(6117), new DateTime(2025, 6, 21, 12, 18, 19, 403, DateTimeKind.Utc).AddTicks(6117) });

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "PaidAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 26, 12, 18, 19, 403, DateTimeKind.Utc).AddTicks(6117), new DateTime(2025, 6, 28, 12, 18, 19, 403, DateTimeKind.Utc).AddTicks(6117), new DateTime(2025, 7, 4, 12, 18, 19, 403, DateTimeKind.Utc).AddTicks(6117) });

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 7, 3, 12, 18, 19, 403, DateTimeKind.Utc).AddTicks(6117), new DateTime(2025, 7, 5, 12, 18, 19, 403, DateTimeKind.Utc).AddTicks(6117) });

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 7, 5, 12, 18, 19, 403, DateTimeKind.Utc).AddTicks(6117), new DateTime(2025, 7, 5, 12, 18, 19, 403, DateTimeKind.Utc).AddTicks(6117) });

            migrationBuilder.UpdateData(
                table: "ProductCategories",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 7, 6, 12, 18, 19, 403, DateTimeKind.Utc).AddTicks(6117));

            migrationBuilder.UpdateData(
                table: "ProductCategories",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 7, 6, 12, 18, 19, 403, DateTimeKind.Utc).AddTicks(6117));

            migrationBuilder.UpdateData(
                table: "ProductCategories",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 7, 6, 12, 18, 19, 403, DateTimeKind.Utc).AddTicks(6117));

            migrationBuilder.UpdateData(
                table: "ProductCategories",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2025, 7, 6, 12, 18, 19, 403, DateTimeKind.Utc).AddTicks(6117));

            migrationBuilder.UpdateData(
                table: "ProductCategories",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2025, 7, 6, 12, 18, 19, 403, DateTimeKind.Utc).AddTicks(6117));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 7, 6, 12, 18, 19, 403, DateTimeKind.Utc).AddTicks(6117), new DateTime(2025, 7, 6, 12, 18, 19, 403, DateTimeKind.Utc).AddTicks(6117) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 7, 6, 12, 18, 19, 403, DateTimeKind.Utc).AddTicks(6117), new DateTime(2025, 7, 6, 12, 18, 19, 403, DateTimeKind.Utc).AddTicks(6117) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 7, 6, 12, 18, 19, 403, DateTimeKind.Utc).AddTicks(6117), new DateTime(2025, 7, 6, 12, 18, 19, 403, DateTimeKind.Utc).AddTicks(6117) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 7, 6, 12, 18, 19, 403, DateTimeKind.Utc).AddTicks(6117), new DateTime(2025, 7, 6, 12, 18, 19, 403, DateTimeKind.Utc).AddTicks(6117) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 7, 6, 12, 18, 19, 403, DateTimeKind.Utc).AddTicks(6117), new DateTime(2025, 7, 6, 12, 18, 19, 403, DateTimeKind.Utc).AddTicks(6117) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 7, 6, 12, 18, 19, 403, DateTimeKind.Utc).AddTicks(6117), new DateTime(2025, 7, 6, 12, 18, 19, 403, DateTimeKind.Utc).AddTicks(6117) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 7, 6, 12, 18, 19, 403, DateTimeKind.Utc).AddTicks(6117), new DateTime(2025, 7, 6, 12, 18, 19, 403, DateTimeKind.Utc).AddTicks(6117) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 7, 6, 12, 18, 19, 403, DateTimeKind.Utc).AddTicks(6117), new DateTime(2025, 7, 6, 12, 18, 19, 403, DateTimeKind.Utc).AddTicks(6117) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 7, 6, 12, 18, 19, 403, DateTimeKind.Utc).AddTicks(6117), new DateTime(2025, 7, 6, 12, 18, 19, 403, DateTimeKind.Utc).AddTicks(6117) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 7, 6, 12, 18, 19, 403, DateTimeKind.Utc).AddTicks(6117), new DateTime(2025, 7, 6, 12, 18, 19, 403, DateTimeKind.Utc).AddTicks(6117) });

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 16, 12, 18, 19, 403, DateTimeKind.Utc).AddTicks(6117), new DateTime(2025, 6, 16, 12, 18, 19, 403, DateTimeKind.Utc).AddTicks(6117) });

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 18, 12, 18, 19, 403, DateTimeKind.Utc).AddTicks(6117), new DateTime(2025, 6, 18, 12, 18, 19, 403, DateTimeKind.Utc).AddTicks(6117) });

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 21, 12, 18, 19, 403, DateTimeKind.Utc).AddTicks(6117), new DateTime(2025, 6, 21, 12, 18, 19, 403, DateTimeKind.Utc).AddTicks(6117) });

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 24, 12, 18, 19, 403, DateTimeKind.Utc).AddTicks(6117), new DateTime(2025, 6, 24, 12, 18, 19, 403, DateTimeKind.Utc).AddTicks(6117) });

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 26, 12, 18, 19, 403, DateTimeKind.Utc).AddTicks(6117), new DateTime(2025, 6, 26, 12, 18, 19, 403, DateTimeKind.Utc).AddTicks(6117) });

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 28, 12, 18, 19, 403, DateTimeKind.Utc).AddTicks(6117), new DateTime(2025, 6, 28, 12, 18, 19, 403, DateTimeKind.Utc).AddTicks(6117) });

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 18, 19, 403, DateTimeKind.Utc).AddTicks(6117), new DateTime(2025, 7, 1, 12, 18, 19, 403, DateTimeKind.Utc).AddTicks(6117) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 7, 6, 12, 18, 19, 403, DateTimeKind.Utc).AddTicks(6117));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 7, 6, 12, 18, 19, 403, DateTimeKind.Utc).AddTicks(6117));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 7, 6, 12, 18, 19, 403, DateTimeKind.Utc).AddTicks(6117));

            migrationBuilder.UpdateData(
                table: "TicketComments",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 7, 2, 12, 18, 19, 403, DateTimeKind.Utc).AddTicks(6117));

            migrationBuilder.UpdateData(
                table: "TicketComments",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 7, 2, 12, 18, 19, 403, DateTimeKind.Utc).AddTicks(6117));

            migrationBuilder.UpdateData(
                table: "TicketComments",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 7, 4, 12, 18, 19, 403, DateTimeKind.Utc).AddTicks(6117));

            migrationBuilder.UpdateData(
                table: "TicketComments",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2025, 7, 4, 12, 18, 19, 403, DateTimeKind.Utc).AddTicks(6117));

            migrationBuilder.UpdateData(
                table: "TicketComments",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2025, 7, 5, 12, 18, 19, 403, DateTimeKind.Utc).AddTicks(6117));

            migrationBuilder.UpdateData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 7, 1, 12, 18, 19, 403, DateTimeKind.Utc).AddTicks(6117), new DateTime(2025, 7, 4, 12, 18, 19, 403, DateTimeKind.Utc).AddTicks(6117) });

            migrationBuilder.UpdateData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 7, 3, 12, 18, 19, 403, DateTimeKind.Utc).AddTicks(6117), new DateTime(2025, 7, 3, 12, 18, 19, 403, DateTimeKind.Utc).AddTicks(6117) });

            migrationBuilder.UpdateData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 7, 5, 12, 18, 19, 403, DateTimeKind.Utc).AddTicks(6117), new DateTime(2025, 7, 5, 12, 18, 19, 403, DateTimeKind.Utc).AddTicks(6117) });

            migrationBuilder.UpdateData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "ResolvedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 26, 12, 18, 19, 403, DateTimeKind.Utc).AddTicks(6117), new DateTime(2025, 7, 5, 12, 18, 19, 403, DateTimeKind.Utc).AddTicks(6117), new DateTime(2025, 7, 5, 12, 18, 19, 403, DateTimeKind.Utc).AddTicks(6117) });

            migrationBuilder.UpdateData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 7, 6, 0, 18, 19, 403, DateTimeKind.Utc).AddTicks(6117), new DateTime(2025, 7, 6, 0, 18, 19, 403, DateTimeKind.Utc).AddTicks(6117) });

            migrationBuilder.UpdateData(
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 7, 6, 12, 18, 19, 403, DateTimeKind.Utc).AddTicks(6117));

            migrationBuilder.UpdateData(
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 7, 6, 12, 18, 19, 403, DateTimeKind.Utc).AddTicks(6117));

            migrationBuilder.UpdateData(
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 7, 6, 12, 18, 19, 403, DateTimeKind.Utc).AddTicks(6117));

            migrationBuilder.UpdateData(
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2025, 7, 6, 12, 18, 19, 403, DateTimeKind.Utc).AddTicks(6117));

            migrationBuilder.UpdateData(
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2025, 7, 6, 12, 18, 19, 403, DateTimeKind.Utc).AddTicks(6117));

            migrationBuilder.UpdateData(
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2025, 7, 6, 12, 18, 19, 403, DateTimeKind.Utc).AddTicks(6117));

            migrationBuilder.UpdateData(
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2025, 7, 6, 12, 18, 19, 403, DateTimeKind.Utc).AddTicks(6117));

            migrationBuilder.UpdateData(
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateTime(2025, 7, 6, 12, 18, 19, 403, DateTimeKind.Utc).AddTicks(6117));

            migrationBuilder.UpdateData(
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: 9,
                column: "CreatedAt",
                value: new DateTime(2025, 7, 6, 12, 18, 19, 403, DateTimeKind.Utc).AddTicks(6117));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 7, 6, 12, 18, 19, 403, DateTimeKind.Utc).AddTicks(6117), new DateTime(2025, 7, 6, 12, 18, 19, 403, DateTimeKind.Utc).AddTicks(6117) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 7, 6, 12, 18, 19, 403, DateTimeKind.Utc).AddTicks(6117), new DateTime(2025, 7, 6, 12, 18, 19, 403, DateTimeKind.Utc).AddTicks(6117) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 7, 6, 12, 18, 19, 403, DateTimeKind.Utc).AddTicks(6117), new DateTime(2025, 7, 6, 12, 18, 19, 403, DateTimeKind.Utc).AddTicks(6117) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 7, 6, 12, 18, 19, 403, DateTimeKind.Utc).AddTicks(6117), new DateTime(2025, 7, 6, 12, 18, 19, 403, DateTimeKind.Utc).AddTicks(6117) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 7, 6, 12, 18, 19, 403, DateTimeKind.Utc).AddTicks(6117), new DateTime(2025, 7, 6, 12, 18, 19, 403, DateTimeKind.Utc).AddTicks(6117) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 7, 6, 12, 18, 19, 403, DateTimeKind.Utc).AddTicks(6117), new DateTime(2025, 7, 6, 12, 18, 19, 403, DateTimeKind.Utc).AddTicks(6117) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 7, 6, 12, 18, 19, 403, DateTimeKind.Utc).AddTicks(6117), new DateTime(2025, 7, 6, 12, 18, 19, 403, DateTimeKind.Utc).AddTicks(6117) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 7, 6, 12, 18, 19, 403, DateTimeKind.Utc).AddTicks(6117), new DateTime(2025, 7, 6, 12, 18, 19, 403, DateTimeKind.Utc).AddTicks(6117) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 7, 6, 12, 18, 19, 403, DateTimeKind.Utc).AddTicks(6117), new DateTime(2025, 7, 6, 12, 18, 19, 403, DateTimeKind.Utc).AddTicks(6117) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "BlogCategories",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 7, 4, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840));

            migrationBuilder.UpdateData(
                table: "BlogCategories",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 7, 4, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840));

            migrationBuilder.UpdateData(
                table: "BlogCategories",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 7, 4, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840));

            migrationBuilder.UpdateData(
                table: "BlogCategories",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2025, 7, 4, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840));

            migrationBuilder.UpdateData(
                table: "BlogCategories",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2025, 7, 4, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840));

            migrationBuilder.UpdateData(
                table: "Blogs",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PublishedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 19, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840), new DateTime(2025, 6, 24, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840), new DateTime(2025, 6, 24, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840) });

            migrationBuilder.UpdateData(
                table: "Blogs",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "PublishedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 22, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840), new DateTime(2025, 6, 27, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840), new DateTime(2025, 6, 27, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840) });

            migrationBuilder.UpdateData(
                table: "Blogs",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "PublishedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 26, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840), new DateTime(2025, 6, 29, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840), new DateTime(2025, 6, 29, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840) });

            migrationBuilder.UpdateData(
                table: "Blogs",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "PublishedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 28, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840), new DateTime(2025, 7, 1, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840), new DateTime(2025, 7, 1, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840) });

            migrationBuilder.UpdateData(
                table: "Blogs",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "PublishedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 30, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840), new DateTime(2025, 7, 3, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840), new DateTime(2025, 7, 3, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840) });

            migrationBuilder.UpdateData(
                table: "CartItems",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 7, 2, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840), new DateTime(2025, 7, 2, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840) });

            migrationBuilder.UpdateData(
                table: "CartItems",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 7, 4, 5, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840), new DateTime(2025, 7, 4, 5, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840) });

            migrationBuilder.UpdateData(
                table: "CartItems",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 7, 4, 5, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840), new DateTime(2025, 7, 4, 5, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840) });

            migrationBuilder.UpdateData(
                table: "CartItems",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 7, 3, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840), new DateTime(2025, 7, 3, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840) });

            migrationBuilder.UpdateData(
                table: "CartItems",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 7, 4, 8, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840), new DateTime(2025, 7, 4, 8, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840) });

            migrationBuilder.UpdateData(
                table: "CartItems",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 7, 4, 8, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840), new DateTime(2025, 7, 4, 8, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840) });

            migrationBuilder.UpdateData(
                table: "CartItems",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 7, 3, 23, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840), new DateTime(2025, 7, 3, 23, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840) });

            migrationBuilder.UpdateData(
                table: "Carts",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 7, 2, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840), new DateTime(2025, 7, 4, 5, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840) });

            migrationBuilder.UpdateData(
                table: "Carts",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 7, 3, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840), new DateTime(2025, 7, 4, 8, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840) });

            migrationBuilder.UpdateData(
                table: "Carts",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 7, 3, 23, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840), new DateTime(2025, 7, 4, 9, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840) });

            migrationBuilder.UpdateData(
                table: "EngineerFarmerAssignments",
                keyColumn: "Id",
                keyValue: 1,
                column: "AssignedAt",
                value: new DateTime(2025, 7, 4, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840));

            migrationBuilder.UpdateData(
                table: "EngineerFarmerAssignments",
                keyColumn: "Id",
                keyValue: 2,
                column: "AssignedAt",
                value: new DateTime(2025, 7, 4, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840));

            migrationBuilder.UpdateData(
                table: "EngineerFarmerAssignments",
                keyColumn: "Id",
                keyValue: 3,
                column: "AssignedAt",
                value: new DateTime(2025, 7, 4, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840));

            migrationBuilder.UpdateData(
                table: "EngineerFarmerAssignments",
                keyColumn: "Id",
                keyValue: 4,
                column: "AssignedAt",
                value: new DateTime(2025, 7, 4, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840));

            migrationBuilder.UpdateData(
                table: "EngineerFarmerAssignments",
                keyColumn: "Id",
                keyValue: 5,
                column: "AssignedAt",
                value: new DateTime(2025, 7, 4, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840));

            migrationBuilder.UpdateData(
                table: "Engineers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 7, 4, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840), new DateTime(2025, 7, 4, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840) });

            migrationBuilder.UpdateData(
                table: "Engineers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 7, 4, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840), new DateTime(2025, 7, 4, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840) });

            migrationBuilder.UpdateData(
                table: "Engineers",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 7, 4, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840), new DateTime(2025, 7, 4, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840) });

            migrationBuilder.UpdateData(
                table: "Farmers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 7, 4, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840), new DateTime(2025, 7, 4, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840) });

            migrationBuilder.UpdateData(
                table: "Farmers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 7, 4, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840), new DateTime(2025, 7, 4, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840) });

            migrationBuilder.UpdateData(
                table: "Farmers",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 7, 4, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840), new DateTime(2025, 7, 4, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840) });

            migrationBuilder.UpdateData(
                table: "Farmers",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 7, 4, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840), new DateTime(2025, 7, 4, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840) });

            migrationBuilder.UpdateData(
                table: "Farmers",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 7, 4, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840), new DateTime(2025, 7, 4, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840) });

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PublishedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 7, 2, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840), new DateTime(2025, 7, 2, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840), new DateTime(2025, 7, 2, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840) });

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "PublishedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 7, 3, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840), new DateTime(2025, 7, 3, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840), new DateTime(2025, 7, 3, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840) });

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "PublishedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 7, 3, 23, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840), new DateTime(2025, 7, 3, 23, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840), new DateTime(2025, 7, 3, 23, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840) });

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "PublishedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 7, 4, 5, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840), new DateTime(2025, 7, 4, 5, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840), new DateTime(2025, 7, 4, 5, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840) });

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "PublishedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 7, 4, 8, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840), new DateTime(2025, 7, 4, 8, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840), new DateTime(2025, 7, 4, 8, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840) });

            migrationBuilder.UpdateData(
                table: "NewsCategories",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 7, 4, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840));

            migrationBuilder.UpdateData(
                table: "NewsCategories",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 7, 4, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840));

            migrationBuilder.UpdateData(
                table: "NewsCategories",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 7, 4, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840));

            migrationBuilder.UpdateData(
                table: "NewsCategories",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2025, 7, 4, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840));

            migrationBuilder.UpdateData(
                table: "NewsCategories",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2025, 7, 4, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PaidAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 14, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840), new DateTime(2025, 6, 19, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840), new DateTime(2025, 6, 19, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840) });

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "PaidAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 24, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840), new DateTime(2025, 6, 26, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840), new DateTime(2025, 7, 2, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840) });

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 7, 1, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840), new DateTime(2025, 7, 3, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840) });

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 7, 3, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840), new DateTime(2025, 7, 3, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840) });

            migrationBuilder.UpdateData(
                table: "ProductCategories",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 7, 4, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840));

            migrationBuilder.UpdateData(
                table: "ProductCategories",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 7, 4, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840));

            migrationBuilder.UpdateData(
                table: "ProductCategories",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 7, 4, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840));

            migrationBuilder.UpdateData(
                table: "ProductCategories",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2025, 7, 4, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840));

            migrationBuilder.UpdateData(
                table: "ProductCategories",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2025, 7, 4, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 7, 4, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840), new DateTime(2025, 7, 4, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 7, 4, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840), new DateTime(2025, 7, 4, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 7, 4, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840), new DateTime(2025, 7, 4, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 7, 4, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840), new DateTime(2025, 7, 4, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 7, 4, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840), new DateTime(2025, 7, 4, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 7, 4, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840), new DateTime(2025, 7, 4, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 7, 4, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840), new DateTime(2025, 7, 4, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 7, 4, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840), new DateTime(2025, 7, 4, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 7, 4, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840), new DateTime(2025, 7, 4, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 7, 4, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840), new DateTime(2025, 7, 4, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840) });

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 14, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840), new DateTime(2025, 6, 14, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840) });

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 16, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840), new DateTime(2025, 6, 16, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840) });

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 19, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840), new DateTime(2025, 6, 19, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840) });

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 22, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840), new DateTime(2025, 6, 22, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840) });

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 24, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840), new DateTime(2025, 6, 24, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840) });

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 26, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840), new DateTime(2025, 6, 26, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840) });

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 29, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840), new DateTime(2025, 6, 29, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 7, 4, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 7, 4, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 7, 4, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840));

            migrationBuilder.UpdateData(
                table: "TicketComments",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 6, 30, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840));

            migrationBuilder.UpdateData(
                table: "TicketComments",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 6, 30, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840));

            migrationBuilder.UpdateData(
                table: "TicketComments",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 7, 2, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840));

            migrationBuilder.UpdateData(
                table: "TicketComments",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2025, 7, 2, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840));

            migrationBuilder.UpdateData(
                table: "TicketComments",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2025, 7, 3, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840));

            migrationBuilder.UpdateData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 29, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840), new DateTime(2025, 7, 2, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840) });

            migrationBuilder.UpdateData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 7, 1, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840), new DateTime(2025, 7, 1, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840) });

            migrationBuilder.UpdateData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 7, 3, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840), new DateTime(2025, 7, 3, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840) });

            migrationBuilder.UpdateData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "ResolvedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 6, 24, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840), new DateTime(2025, 7, 3, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840), new DateTime(2025, 7, 3, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840) });

            migrationBuilder.UpdateData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 7, 3, 23, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840), new DateTime(2025, 7, 3, 23, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840) });

            migrationBuilder.UpdateData(
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 7, 4, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840));

            migrationBuilder.UpdateData(
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 7, 4, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840));

            migrationBuilder.UpdateData(
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 7, 4, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840));

            migrationBuilder.UpdateData(
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2025, 7, 4, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840));

            migrationBuilder.UpdateData(
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2025, 7, 4, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840));

            migrationBuilder.UpdateData(
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2025, 7, 4, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840));

            migrationBuilder.UpdateData(
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2025, 7, 4, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840));

            migrationBuilder.UpdateData(
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateTime(2025, 7, 4, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840));

            migrationBuilder.UpdateData(
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: 9,
                column: "CreatedAt",
                value: new DateTime(2025, 7, 4, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 7, 4, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840), new DateTime(2025, 7, 4, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 7, 4, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840), new DateTime(2025, 7, 4, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 7, 4, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840), new DateTime(2025, 7, 4, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 7, 4, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840), new DateTime(2025, 7, 4, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 7, 4, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840), new DateTime(2025, 7, 4, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 7, 4, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840), new DateTime(2025, 7, 4, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 7, 4, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840), new DateTime(2025, 7, 4, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 7, 4, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840), new DateTime(2025, 7, 4, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 7, 4, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840), new DateTime(2025, 7, 4, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840) });
        }
    }
}
