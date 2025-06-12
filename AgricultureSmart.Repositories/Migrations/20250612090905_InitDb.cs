using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AgricultureSmart.Repositories.Migrations
{
    /// <inheritdoc />
    public partial class InitDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BlogCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Slug = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlogCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NewsCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Slug = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NewsCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Slug = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "News",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Excerpt = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Author = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    PublishedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    Featured = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    Urgent = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    Tags = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Source = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    ViewCount = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_News", x => x.Id);
                    table.ForeignKey(
                        name: "FK_News_NewsCategories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "NewsCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    Stock = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    ImageUrl = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    SKU = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DiscountPrice = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_ProductCategories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "ProductCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Blogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AuthorId = table.Column<int>(type: "int", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FeaturedImage = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Slug = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, defaultValue: "draft"),
                    ViewCount = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    PublishedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Blogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Blogs_BlogCategories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "BlogCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Blogs_Users_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Carts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    TotalAmount = table.Column<decimal>(type: "decimal(10,2)", nullable: false, defaultValue: 0m),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Carts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Carts_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Engineers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Specialization = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    ExperienceYears = table.Column<int>(type: "int", nullable: false),
                    Certification = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Bio = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Engineers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Engineers_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Farmers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    FarmLocation = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    FarmSize = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    CropTypes = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FarmingExperienceYears = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Farmers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Farmers_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    OrderNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    TotalAmount = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, defaultValue: "pending"),
                    ShippingAddress = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    PaymentMethod = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PaymentStatus = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, defaultValue: "pending"),
                    PaidAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserRoles_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRoles_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reviews",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    ReviewValue = table.Column<int>(type: "int", nullable: false),
                    ReviewMessage = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reviews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reviews_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reviews_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CartItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CartId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    TotalPrice = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CartItems_Carts_CartId",
                        column: x => x.CartId,
                        principalTable: "Carts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CartItems_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EngineerFarmerAssignments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EngineerId = table.Column<int>(type: "int", nullable: false),
                    FarmerId = table.Column<int>(type: "int", nullable: false),
                    AssignedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EngineerFarmerAssignments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EngineerFarmerAssignments_Engineers_EngineerId",
                        column: x => x.EngineerId,
                        principalTable: "Engineers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EngineerFarmerAssignments_Farmers_FarmerId",
                        column: x => x.FarmerId,
                        principalTable: "Farmers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Tickets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FarmerId = table.Column<int>(type: "int", nullable: false),
                    AssignedEngineerId = table.Column<int>(type: "int", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Category = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CropType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Location = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Priority = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, defaultValue: "medium"),
                    ContactMethod = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, defaultValue: "open"),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ResolvedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tickets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tickets_Engineers_AssignedEngineerId",
                        column: x => x.AssignedEngineerId,
                        principalTable: "Engineers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Tickets_Farmers_FarmerId",
                        column: x => x.FarmerId,
                        principalTable: "Farmers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OrderItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    TotalPrice = table.Column<decimal>(type: "decimal(10,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderItems_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderItems_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TicketComments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TicketId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsInternal = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketComments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TicketComments_Tickets_TicketId",
                        column: x => x.TicketId,
                        principalTable: "Tickets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TicketComments_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "BlogCategories",
                columns: new[] { "Id", "CreatedAt", "Description", "IsActive", "Name", "Slug" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 6, 12, 9, 9, 4, 411, DateTimeKind.Utc).AddTicks(5059), "Các bài viết về bệnh hại trên cây trồng và cách phòng trị", true, "Bệnh cây trồng", "benh-cay-trong" },
                    { 2, new DateTime(2025, 6, 12, 9, 9, 4, 411, DateTimeKind.Utc).AddTicks(5059), "Hướng dẫn kỹ thuật trồng trọt và chăm sóc cây", true, "Kỹ thuật canh tác", "ky-thuat-canh-tac" },
                    { 3, new DateTime(2025, 6, 12, 9, 9, 4, 411, DateTimeKind.Utc).AddTicks(5059), "Thông tin về các loại phân bón và cách sử dụng", true, "Phân bón", "phan-bon" },
                    { 4, new DateTime(2025, 6, 12, 9, 9, 4, 411, DateTimeKind.Utc).AddTicks(5059), "Hướng dẫn sử dụng thuốc BVTV an toàn", true, "Thuốc bảo vệ thực vật", "thuoc-bao-ve-thuc-vat" },
                    { 5, new DateTime(2025, 6, 12, 9, 9, 4, 411, DateTimeKind.Utc).AddTicks(5059), "Lịch thời vụ và mùa vụ canh tác", true, "Thời vụ", "thoi-vu" }
                });

            migrationBuilder.InsertData(
                table: "NewsCategories",
                columns: new[] { "Id", "CreatedAt", "Description", "IsActive", "Name", "Slug" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 6, 12, 9, 9, 4, 411, DateTimeKind.Utc).AddTicks(5059), "Tin tức về chính sách nông nghiệp", true, "Chính sách", "chinh-sach" },
                    { 2, new DateTime(2025, 6, 12, 9, 9, 4, 411, DateTimeKind.Utc).AddTicks(5059), "Thông tin thị trường nông sản", true, "Thị trường", "thi-truong" },
                    { 3, new DateTime(2025, 6, 12, 9, 9, 4, 411, DateTimeKind.Utc).AddTicks(5059), "Công nghệ mới trong nông nghiệp", true, "Công nghệ", "cong-nghe" },
                    { 4, new DateTime(2025, 6, 12, 9, 9, 4, 411, DateTimeKind.Utc).AddTicks(5059), "Các sự kiện nông nghiệp", true, "Sự kiện", "su-kien" },
                    { 5, new DateTime(2025, 6, 12, 9, 9, 4, 411, DateTimeKind.Utc).AddTicks(5059), "Dự báo thời tiết phục vụ sản xuất", true, "Thời tiết", "thoi-tiet" }
                });

            migrationBuilder.InsertData(
                table: "ProductCategories",
                columns: new[] { "Id", "CreatedAt", "Description", "IsActive", "Name", "Slug" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 6, 12, 9, 9, 4, 411, DateTimeKind.Utc).AddTicks(5059), "Các loại hạt giống cây trồng", true, "Hạt giống", "hat-giong" },
                    { 2, new DateTime(2025, 6, 12, 9, 9, 4, 411, DateTimeKind.Utc).AddTicks(5059), "Các loại phân bón hữu cơ và vô cơ", true, "Phân bón", "phan-bon" },
                    { 3, new DateTime(2025, 6, 12, 9, 9, 4, 411, DateTimeKind.Utc).AddTicks(5059), "Thuốc bảo vệ thực vật", true, "Thuốc BVTV", "thuoc-bvtv" },
                    { 4, new DateTime(2025, 6, 12, 9, 9, 4, 411, DateTimeKind.Utc).AddTicks(5059), "Các dụng cụ và thiết bị nông nghiệp", true, "Dụng cụ nông nghiệp", "dung-cu-nong-nghiep" },
                    { 5, new DateTime(2025, 6, 12, 9, 9, 4, 411, DateTimeKind.Utc).AddTicks(5059), "Máy móc thiết bị nông nghiệp", true, "Máy móc", "may-moc" }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "CreatedAt", "Description", "Name" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 6, 12, 9, 9, 4, 411, DateTimeKind.Utc).AddTicks(5059), "System Administrator", "Admin" },
                    { 2, new DateTime(2025, 6, 12, 9, 9, 4, 411, DateTimeKind.Utc).AddTicks(5059), "Agricultural Engineer", "Engineer" },
                    { 3, new DateTime(2025, 6, 12, 9, 9, 4, 411, DateTimeKind.Utc).AddTicks(5059), "Farmer User", "Farmer" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Address", "CreatedAt", "Email", "IsActive", "Password", "PhoneNumber", "UpdatedAt", "UserName" },
                values: new object[,]
                {
                    { 1, "123 Đường Cách Mạng Tháng 8, Quận 1, TP.HCM", new DateTime(2025, 6, 12, 9, 9, 4, 411, DateTimeKind.Utc).AddTicks(5059), "admin@agricultural.com", true, "admin123", "0901234567", new DateTime(2025, 6, 12, 9, 9, 4, 411, DateTimeKind.Utc).AddTicks(5059), "admin" },
                    { 2, "456 Đường Lê Lợi, Quận 3, TP.HCM", new DateTime(2025, 6, 12, 9, 9, 4, 411, DateTimeKind.Utc).AddTicks(5059), "nguyenvana@agricultural.com", true, "engineer123", "0902345678", new DateTime(2025, 6, 12, 9, 9, 4, 411, DateTimeKind.Utc).AddTicks(5059), "engineer1" },
                    { 3, "789 Đường Nguyễn Huệ, Quận 1, TP.HCM", new DateTime(2025, 6, 12, 9, 9, 4, 411, DateTimeKind.Utc).AddTicks(5059), "tranthib@agricultural.com", true, "engineer123", "0903456789", new DateTime(2025, 6, 12, 9, 9, 4, 411, DateTimeKind.Utc).AddTicks(5059), "engineer2" },
                    { 4, "321 Đường Pasteur, Quận 3, TP.HCM", new DateTime(2025, 6, 12, 9, 9, 4, 411, DateTimeKind.Utc).AddTicks(5059), "levanc@agricultural.com", true, "engineer123", "0904567890", new DateTime(2025, 6, 12, 9, 9, 4, 411, DateTimeKind.Utc).AddTicks(5059), "engineer3" },
                    { 5, "Ấp 1, Xã Tân Phú, Huyện Châu Thành, Tỉnh An Giang", new DateTime(2025, 6, 12, 9, 9, 4, 411, DateTimeKind.Utc).AddTicks(5059), "nguyenthid@gmail.com", true, "farmer123", "0905678901", new DateTime(2025, 6, 12, 9, 9, 4, 411, DateTimeKind.Utc).AddTicks(5059), "farmer1" },
                    { 6, "Ấp 2, Xã Long Phú, Huyện Phú Tân, Tỉnh An Giang", new DateTime(2025, 6, 12, 9, 9, 4, 411, DateTimeKind.Utc).AddTicks(5059), "tranvane@gmail.com", true, "farmer123", "0906789012", new DateTime(2025, 6, 12, 9, 9, 4, 411, DateTimeKind.Utc).AddTicks(5059), "farmer2" },
                    { 7, "Ấp 3, Xã Vĩnh Hậu, Huyện Tân Hưng, Tỉnh Long An", new DateTime(2025, 6, 12, 9, 9, 4, 411, DateTimeKind.Utc).AddTicks(5059), "lethif@gmail.com", true, "farmer123", "0907890123", new DateTime(2025, 6, 12, 9, 9, 4, 411, DateTimeKind.Utc).AddTicks(5059), "farmer3" },
                    { 8, "Ấp 4, Xã Đức Hòa, Huyện Đức Hòa, Tỉnh Long An", new DateTime(2025, 6, 12, 9, 9, 4, 411, DateTimeKind.Utc).AddTicks(5059), "phamvang@gmail.com", true, "farmer123", "0908901234", new DateTime(2025, 6, 12, 9, 9, 4, 411, DateTimeKind.Utc).AddTicks(5059), "farmer4" },
                    { 9, "Ấp 5, Xã Tân Trụ, Huyện Tân Trụ, Tỉnh Long An", new DateTime(2025, 6, 12, 9, 9, 4, 411, DateTimeKind.Utc).AddTicks(5059), "hoangthih@gmail.com", true, "farmer123", "0909012345", new DateTime(2025, 6, 12, 9, 9, 4, 411, DateTimeKind.Utc).AddTicks(5059), "farmer5" }
                });

            migrationBuilder.InsertData(
                table: "Blogs",
                columns: new[] { "Id", "AuthorId", "CategoryId", "Content", "CreatedAt", "FeaturedImage", "PublishedAt", "Slug", "Status", "Title", "UpdatedAt", "ViewCount" },
                values: new object[,]
                {
                    { 1, 2, 1, "Bệnh đạo ôn lúa là một trong những bệnh phổ biến và nguy hiểm nhất đối với cây lúa. Bài viết này sẽ hướng dẫn cách nhận biết triệu chứng và các biện pháp phòng trị hiệu quả...", new DateTime(2025, 5, 28, 9, 9, 4, 411, DateTimeKind.Utc).AddTicks(5059), "/images/blog/dao-on-lua.jpg", new DateTime(2025, 6, 2, 9, 9, 4, 411, DateTimeKind.Utc).AddTicks(5059), "cach-nhan-biet-va-phong-tri-benh-dao-on-lua", "published", "Cách nhận biết và phòng trị bệnh đạo ôn lúa", new DateTime(2025, 6, 2, 9, 9, 4, 411, DateTimeKind.Utc).AddTicks(5059), 1250 },
                    { 2, 3, 3, "Việc bón phân đúng cách và đúng thời điểm là yếu tố quyết định năng suất lúa. Bài viết này sẽ hướng dẫn chi tiết cách bón phân NPK cho cây lúa...", new DateTime(2025, 5, 31, 9, 9, 4, 411, DateTimeKind.Utc).AddTicks(5059), "/images/blog/bon-phan-lua.jpg", new DateTime(2025, 6, 5, 9, 9, 4, 411, DateTimeKind.Utc).AddTicks(5059), "huong-dan-bon-phan-npk-cho-cay-lua", "published", "Hướng dẫn bón phân NPK cho cây lúa theo từng giai đoạn", new DateTime(2025, 6, 5, 9, 9, 4, 411, DateTimeKind.Utc).AddTicks(5059), 980 },
                    { 3, 4, 2, "Trồng rau màu trong nhà kính giúp kiểm soát được điều kiện môi trường, tăng năng suất và chất lượng sản phẩm. Bài viết này sẽ chia sẻ những kỹ thuật cần thiết...", new DateTime(2025, 6, 4, 9, 9, 4, 411, DateTimeKind.Utc).AddTicks(5059), "/images/blog/rau-nha-kinh.jpg", new DateTime(2025, 6, 7, 9, 9, 4, 411, DateTimeKind.Utc).AddTicks(5059), "ky-thuat-trong-rau-mau-trong-nha-kinh", "published", "Kỹ thuật trồng rau màu trong nhà kính", new DateTime(2025, 6, 7, 9, 9, 4, 411, DateTimeKind.Utc).AddTicks(5059), 756 },
                    { 4, 2, 4, "Việc sử dụng thuốc BVTV cần tuân thủ nghiêm ngặt các quy định về an toàn để bảo vệ sức khỏe người sử dụng và môi trường...", new DateTime(2025, 6, 6, 9, 9, 4, 411, DateTimeKind.Utc).AddTicks(5059), "/images/blog/an-toan-thuoc-bvtv.jpg", new DateTime(2025, 6, 9, 9, 9, 4, 411, DateTimeKind.Utc).AddTicks(5059), "an-toan-khi-su-dung-thuoc-bao-ve-thuc-vat", "published", "An toàn khi sử dụng thuốc bảo vệ thực vật", new DateTime(2025, 6, 9, 9, 9, 4, 411, DateTimeKind.Utc).AddTicks(5059), 1100 },
                    { 5, 3, 5, "Lịch thời vụ trồng lúa miền Nam được xây dựng dựa trên điều kiện khí hậu, thủy văn và kinh nghiệm sản xuất của nông dân...", new DateTime(2025, 6, 8, 9, 9, 4, 411, DateTimeKind.Utc).AddTicks(5059), "/images/blog/lich-thoi-vu-lua.jpg", new DateTime(2025, 6, 11, 9, 9, 4, 411, DateTimeKind.Utc).AddTicks(5059), "lich-thoi-vu-trong-lua-mien-nam-2024", "published", "Lịch thời vụ trồng lúa miền Nam năm 2024", new DateTime(2025, 6, 11, 9, 9, 4, 411, DateTimeKind.Utc).AddTicks(5059), 2100 }
                });

            migrationBuilder.InsertData(
                table: "Carts",
                columns: new[] { "Id", "CreatedAt", "TotalAmount", "UpdatedAt", "UserId" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 6, 10, 9, 9, 4, 411, DateTimeKind.Utc).AddTicks(5059), 127000m, new DateTime(2025, 6, 12, 3, 9, 4, 411, DateTimeKind.Utc).AddTicks(5059), 5 },
                    { 2, new DateTime(2025, 6, 11, 9, 9, 4, 411, DateTimeKind.Utc).AddTicks(5059), 540000m, new DateTime(2025, 6, 12, 6, 9, 4, 411, DateTimeKind.Utc).AddTicks(5059), 6 },
                    { 3, new DateTime(2025, 6, 11, 21, 9, 4, 411, DateTimeKind.Utc).AddTicks(5059), 85000m, new DateTime(2025, 6, 12, 7, 9, 4, 411, DateTimeKind.Utc).AddTicks(5059), 7 }
                });

            migrationBuilder.InsertData(
                table: "Engineers",
                columns: new[] { "Id", "Bio", "Certification", "CreatedAt", "ExperienceYears", "Specialization", "UpdatedAt", "UserId" },
                values: new object[,]
                {
                    { 1, "Chuyên gia về bệnh hại cây trồng với 8 năm kinh nghiệm trong lĩnh vực chẩn đoán và điều trị bệnh lúa, rau màu.", "[\"Chứng chỉ Kỹ sư Nông nghiệp\", \"Chứng chỉ Chuyên gia Bệnh học thực vật\"]", new DateTime(2025, 6, 12, 9, 9, 4, 411, DateTimeKind.Utc).AddTicks(5059), 8, "Bệnh học thực vật", new DateTime(2025, 6, 12, 9, 9, 4, 411, DateTimeKind.Utc).AddTicks(5059), 2 },
                    { 2, "Chuyên gia về dinh dưỡng và phân bón cây trồng, có kinh nghiệm tư vấn cho nhiều hợp tác xã nông nghiệp.", "[\"Chứng chỉ Kỹ sư Nông nghiệp\", \"Chứng chỉ Chuyên gia Dinh dưỡng thực vật\"]", new DateTime(2025, 6, 12, 9, 9, 4, 411, DateTimeKind.Utc).AddTicks(5059), 6, "Dinh dưỡng cây trồng", new DateTime(2025, 6, 12, 9, 9, 4, 411, DateTimeKind.Utc).AddTicks(5059), 3 },
                    { 3, "Chuyên gia kỹ thuật canh tác với 10 năm kinh nghiệm, chuyên về nông nghiệp hữu cơ và canh tác bền vững.", "[\"Chứng chỉ Kỹ sư Nông nghiệp\", \"Chứng chỉ Chuyên gia Kỹ thuật canh tác\", \"Chứng chỉ Nông nghiệp hữu cơ\"]", new DateTime(2025, 6, 12, 9, 9, 4, 411, DateTimeKind.Utc).AddTicks(5059), 10, "Kỹ thuật canh tác", new DateTime(2025, 6, 12, 9, 9, 4, 411, DateTimeKind.Utc).AddTicks(5059), 4 }
                });

            migrationBuilder.InsertData(
                table: "Farmers",
                columns: new[] { "Id", "CreatedAt", "CropTypes", "FarmLocation", "FarmSize", "FarmingExperienceYears", "UpdatedAt", "UserId" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 6, 12, 9, 9, 4, 411, DateTimeKind.Utc).AddTicks(5059), "[\"Lúa\", \"Rau màu\", \"Cây ăn trái\"]", "Ấp 1, Xã Tân Phú, Huyện Châu Thành, Tỉnh An Giang", 2.5m, 15, new DateTime(2025, 6, 12, 9, 9, 4, 411, DateTimeKind.Utc).AddTicks(5059), 5 },
                    { 2, new DateTime(2025, 6, 12, 9, 9, 4, 411, DateTimeKind.Utc).AddTicks(5059), "[\"Lúa\", \"Ngô\", \"Đậu tương\"]", "Ấp 2, Xã Long Phú, Huyện Phú Tân, Tỉnh An Giang", 3.2m, 12, new DateTime(2025, 6, 12, 9, 9, 4, 411, DateTimeKind.Utc).AddTicks(5059), 6 },
                    { 3, new DateTime(2025, 6, 12, 9, 9, 4, 411, DateTimeKind.Utc).AddTicks(5059), "[\"Rau màu\", \"Cây ăn trái\", \"Hoa màu\"]", "Ấp 3, Xã Vĩnh Hậu, Huyện Tân Hưng, Tỉnh Long An", 1.8m, 8, new DateTime(2025, 6, 12, 9, 9, 4, 411, DateTimeKind.Utc).AddTicks(5059), 7 },
                    { 4, new DateTime(2025, 6, 12, 9, 9, 4, 411, DateTimeKind.Utc).AddTicks(5059), "[\"Lúa\", \"Mía\", \"Cây ăn trái\"]", "Ấp 4, Xã Đức Hòa, Huyện Đức Hòa, Tỉnh Long An", 4.1m, 20, new DateTime(2025, 6, 12, 9, 9, 4, 411, DateTimeKind.Utc).AddTicks(5059), 8 },
                    { 5, new DateTime(2025, 6, 12, 9, 9, 4, 411, DateTimeKind.Utc).AddTicks(5059), "[\"Lúa\", \"Rau màu\", \"Đậu các loại\"]", "Ấp 5, Xã Tân Trụ, Huyện Tân Trụ, Tỉnh Long An", 2.9m, 10, new DateTime(2025, 6, 12, 9, 9, 4, 411, DateTimeKind.Utc).AddTicks(5059), 9 }
                });

            migrationBuilder.InsertData(
                table: "News",
                columns: new[] { "Id", "Author", "CategoryId", "Content", "CreatedAt", "Excerpt", "Featured", "ImageUrl", "PublishedAt", "Source", "Tags", "Title", "UpdatedAt", "ViewCount" },
                values: new object[] { 1, "Bộ Nông nghiệp và Phát triển Nông thôn", 1, "Chính phủ vừa phê duyệt chương trình hỗ trợ nông dân chuyển đổi số trong sản xuất nông nghiệp với tổng kinh phí 500 tỷ đồng...", new DateTime(2025, 6, 10, 9, 9, 4, 411, DateTimeKind.Utc).AddTicks(5059), "Chương trình hỗ trợ nông dân ứng dụng công nghệ số trong sản xuất nông nghiệp với tổng kinh phí 500 tỷ đồng.", true, "/images/news/ho-tro-chuyen-doi-so.jpg", new DateTime(2025, 6, 10, 9, 9, 4, 411, DateTimeKind.Utc).AddTicks(5059), "Bộ Nông nghiệp và Phát triển Nông thôn", "[\"Chính sách\", \"Chuyển đổi số\", \"Hỗ trợ nông dân\"]", "Chính phủ hỗ trợ 500 tỷ đồng cho nông dân chuyển đổi số", new DateTime(2025, 6, 10, 9, 9, 4, 411, DateTimeKind.Utc).AddTicks(5059), 3500 });

            migrationBuilder.InsertData(
                table: "News",
                columns: new[] { "Id", "Author", "CategoryId", "Content", "CreatedAt", "Excerpt", "ImageUrl", "PublishedAt", "Source", "Tags", "Title", "UpdatedAt", "Urgent", "ViewCount" },
                values: new object[] { 2, "Hiệp hội Lương thực Việt Nam", 2, "Theo báo cáo từ Sở Nông nghiệp các tỉnh ĐBSCL, giá lúa đã tăng mạnh trong tuần qua...", new DateTime(2025, 6, 11, 9, 9, 4, 411, DateTimeKind.Utc).AddTicks(5059), "Giá lúa tại các tỉnh ĐBSCL tăng 200-300 đồng/kg so với tuần trước nhờ nhu cầu xuất khẩu tăng cao.", "/images/news/gia-lua-tang.jpg", new DateTime(2025, 6, 11, 9, 9, 4, 411, DateTimeKind.Utc).AddTicks(5059), "Hiệp hội Lương thực Việt Nam", "[\"Giá lúa\", \"Xuất khẩu\", \"Thị trường\"]", "Giá lúa tăng mạnh do xuất khẩu khởi sắc", new DateTime(2025, 6, 11, 9, 9, 4, 411, DateTimeKind.Utc).AddTicks(5059), true, 2800 });

            migrationBuilder.InsertData(
                table: "News",
                columns: new[] { "Id", "Author", "CategoryId", "Content", "CreatedAt", "Excerpt", "Featured", "ImageUrl", "PublishedAt", "Source", "Tags", "Title", "UpdatedAt", "ViewCount" },
                values: new object[] { 3, "Viện Bảo vệ thực vật", 3, "Các ứng dụng AI hiện đại đang giúp nông dân chẩn đoán bệnh cây trồng nhanh chóng và chính xác...", new DateTime(2025, 6, 11, 21, 9, 4, 411, DateTimeKind.Utc).AddTicks(5059), "Công nghệ trí tuệ nhân tạo đang được ứng dụng rộng rãi trong việc chẩn đoán bệnh hại cây trồng với độ chính xác cao.", true, "/images/news/ai-chan-doan-benh.jpg", new DateTime(2025, 6, 11, 21, 9, 4, 411, DateTimeKind.Utc).AddTicks(5059), "Viện Bảo vệ thực vật", "[\"AI\", \"Công nghệ\", \"Chẩn đoán bệnh\"]", "Ứng dụng AI trong chẩn đoán bệnh cây trồng", new DateTime(2025, 6, 11, 21, 9, 4, 411, DateTimeKind.Utc).AddTicks(5059), 1900 });

            migrationBuilder.InsertData(
                table: "News",
                columns: new[] { "Id", "Author", "CategoryId", "Content", "CreatedAt", "Excerpt", "ImageUrl", "PublishedAt", "Source", "Tags", "Title", "UpdatedAt", "ViewCount" },
                values: new object[] { 4, "Ban Tổ chức", 4, "Hội nghị sẽ quy tụ các chuyên gia hàng đầu thế giới về nông nghiệp bền vững...", new DateTime(2025, 6, 12, 3, 9, 4, 411, DateTimeKind.Utc).AddTicks(5059), "Hội nghị quốc tế về nông nghiệp bền vững và an ninh lương thực sẽ được tổ chức tại Hà Nội từ ngày 15-17/2/2024.", "/images/news/hoi-nghi-quoc-te.jpg", new DateTime(2025, 6, 12, 3, 9, 4, 411, DateTimeKind.Utc).AddTicks(5059), "Bộ Nông nghiệp và Phát triển Nông thôn", "[\"Hội nghị\", \"Quốc tế\", \"Bền vững\", \"An ninh lương thực\"]", "Hội nghị quốc tế về nông nghiệp bền vững sẽ diễn ra tại Hà Nội", new DateTime(2025, 6, 12, 3, 9, 4, 411, DateTimeKind.Utc).AddTicks(5059), 1200 });

            migrationBuilder.InsertData(
                table: "News",
                columns: new[] { "Id", "Author", "CategoryId", "Content", "CreatedAt", "Excerpt", "ImageUrl", "PublishedAt", "Source", "Tags", "Title", "UpdatedAt", "Urgent", "ViewCount" },
                values: new object[] { 5, "Trung tâm Dự báo khí tượng thủy văn", 5, "Theo dự báo, đợt không khí lạnh mạnh sẽ ảnh hưởng đến các tỉnh miền Bắc và Bắc Trung Bộ...", new DateTime(2025, 6, 12, 6, 9, 4, 411, DateTimeKind.Utc).AddTicks(5059), "Trung tâm Dự báo khí tượng thủy văn cảnh báo đợt rét đậm có thể ảnh hưởng đến vụ lúa Đông Xuân.", "/images/news/canh-bao-thoi-tiet.jpg", new DateTime(2025, 6, 12, 6, 9, 4, 411, DateTimeKind.Utc).AddTicks(5059), "Trung tâm Dự báo khí tượng thủy văn", "[\"Thời tiết\", \"Cảnh báo\", \"Lúa Đông Xuân\"]", "Cảnh báo thời tiết bất lợi cho vụ lúa Đông Xuân", new DateTime(2025, 6, 12, 6, 9, 4, 411, DateTimeKind.Utc).AddTicks(5059), true, 4200 });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "Id", "CreatedAt", "OrderNumber", "PaidAt", "PaymentMethod", "PaymentStatus", "ShippingAddress", "Status", "TotalAmount", "UpdatedAt", "UserId" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 5, 23, 9, 9, 4, 411, DateTimeKind.Utc).AddTicks(5059), "ORD-2024-001", new DateTime(2025, 5, 28, 9, 9, 4, 411, DateTimeKind.Utc).AddTicks(5059), "cod", "paid", "Ấp 1, Xã Tân Phú, Huyện Châu Thành, Tỉnh An Giang", "delivered", 189000m, new DateTime(2025, 5, 28, 9, 9, 4, 411, DateTimeKind.Utc).AddTicks(5059), 5 },
                    { 2, new DateTime(2025, 6, 2, 9, 9, 4, 411, DateTimeKind.Utc).AddTicks(5059), "ORD-2024-002", new DateTime(2025, 6, 4, 9, 9, 4, 411, DateTimeKind.Utc).AddTicks(5059), "bank_transfer", "paid", "Ấp 2, Xã Long Phú, Huyện Phú Tân, Tỉnh An Giang", "shipped", 3450000m, new DateTime(2025, 6, 10, 9, 9, 4, 411, DateTimeKind.Utc).AddTicks(5059), 6 },
                    { 3, new DateTime(2025, 6, 9, 9, 9, 4, 411, DateTimeKind.Utc).AddTicks(5059), "ORD-2024-003", null, "cod", "pending", "Ấp 3, Xã Vĩnh Hậu, Huyện Tân Hưng, Tỉnh Long An", "processing", 160000m, new DateTime(2025, 6, 11, 9, 9, 4, 411, DateTimeKind.Utc).AddTicks(5059), 7 },
                    { 4, new DateTime(2025, 6, 11, 9, 9, 4, 411, DateTimeKind.Utc).AddTicks(5059), "ORD-2024-004", null, "cod", "pending", "Ấp 4, Xã Đức Hòa, Huyện Đức Hòa, Tỉnh Long An", "pending", 95000m, new DateTime(2025, 6, 11, 9, 9, 4, 411, DateTimeKind.Utc).AddTicks(5059), 8 }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "CreatedAt", "Description", "DiscountPrice", "ImageUrl", "IsActive", "Name", "Price", "SKU", "Stock", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2025, 6, 12, 9, 9, 4, 411, DateTimeKind.Utc).AddTicks(5059), "Giống lúa ST25 chất lượng cao, năng suất ổn định, kháng bệnh tốt. Thời gian sinh trưởng 95-100 ngày.", 42000m, "/images/products/hat-giong-lua-st25.jpg", true, "Hạt giống lúa ST25", 45000m, "HG-ST25-001", 500, new DateTime(2025, 6, 12, 9, 9, 4, 411, DateTimeKind.Utc).AddTicks(5059) },
                    { 2, 1, new DateTime(2025, 6, 12, 9, 9, 4, 411, DateTimeKind.Utc).AddTicks(5059), "Hạt giống rau cải xanh F1, tỷ lệ nảy mầm cao, sinh trưởng nhanh, chống chịu tốt.", null, "/images/products/hat-giong-cai-xanh.jpg", true, "Hạt giống rau cải xanh", 25000m, "HG-CX-002", 200, new DateTime(2025, 6, 12, 9, 9, 4, 411, DateTimeKind.Utc).AddTicks(5059) },
                    { 3, 1, new DateTime(2025, 6, 12, 9, 9, 4, 411, DateTimeKind.Utc).AddTicks(5059), "Giống cà chua F1 năng suất cao, quả to, màu đỏ đẹp, thích hợp trồng quanh năm.", 32000m, "/images/products/hat-giong-ca-chua.jpg", true, "Hạt giống cà chua F1", 35000m, "HG-CC-003", 150, new DateTime(2025, 6, 12, 9, 9, 4, 411, DateTimeKind.Utc).AddTicks(5059) },
                    { 4, 2, new DateTime(2025, 6, 12, 9, 9, 4, 411, DateTimeKind.Utc).AddTicks(5059), "Phân NPK 16-16-8 chuyên dụng cho cây lúa, cung cấp đầy đủ dinh dưỡng cho cây trồng.", null, "/images/products/phan-npk-16-16-8.jpg", true, "Phân NPK 16-16-8", 18000m, "PB-NPK-004", 1000, new DateTime(2025, 6, 12, 9, 9, 4, 411, DateTimeKind.Utc).AddTicks(5059) },
                    { 5, 2, new DateTime(2025, 6, 12, 9, 9, 4, 411, DateTimeKind.Utc).AddTicks(5059), "Phân hữu cơ vi sinh giúp cải tạo đất, tăng cường sức đề kháng cho cây trồng.", 20000m, "/images/products/phan-huu-co-vi-sinh.jpg", true, "Phân hữu cơ vi sinh", 22000m, "PB-HCVS-005", 800, new DateTime(2025, 6, 12, 9, 9, 4, 411, DateTimeKind.Utc).AddTicks(5059) },
                    { 6, 3, new DateTime(2025, 6, 12, 9, 9, 4, 411, DateTimeKind.Utc).AddTicks(5059), "Thuốc trừ sâu Regent 50SC hiệu quả cao, an toàn cho người và môi trường.", null, "/images/products/thuoc-tru-sau-regent.jpg", true, "Thuốc trừ sâu Regent 50SC", 85000m, "BVTV-REG-006", 300, new DateTime(2025, 6, 12, 9, 9, 4, 411, DateTimeKind.Utc).AddTicks(5059) },
                    { 7, 3, new DateTime(2025, 6, 12, 9, 9, 4, 411, DateTimeKind.Utc).AddTicks(5059), "Thuốc diệt cỏ Gramoxone tác dụng nhanh, hiệu quả cao với nhiều loại cỏ dại.", null, "/images/products/thuoc-diet-co-gramoxone.jpg", true, "Thuốc diệt cỏ Gramoxone", 95000m, "BVTV-GRA-007", 250, new DateTime(2025, 6, 12, 9, 9, 4, 411, DateTimeKind.Utc).AddTicks(5059) },
                    { 8, 4, new DateTime(2025, 6, 12, 9, 9, 4, 411, DateTimeKind.Utc).AddTicks(5059), "Máy phun thuốc bình xịt dung tích 16L, áp suất cao, phun đều, tiết kiệm thuốc.", 420000m, "/images/products/may-phun-thuoc-16l.jpg", true, "Máy phun thuốc bình xịt 16L", 450000m, "DC-MPT-008", 50, new DateTime(2025, 6, 12, 9, 9, 4, 411, DateTimeKind.Utc).AddTicks(5059) },
                    { 9, 4, new DateTime(2025, 6, 12, 9, 9, 4, 411, DateTimeKind.Utc).AddTicks(5059), "Cuốc xới đất cán gỗ chất lượng cao, bền bỉ, phù hợp cho mọi loại đất.", null, "/images/products/cuoc-xoi-dat.jpg", true, "Cuốc xới đất cán gỗ", 75000m, "DC-CXD-009", 100, new DateTime(2025, 6, 12, 9, 9, 4, 411, DateTimeKind.Utc).AddTicks(5059) },
                    { 10, 5, new DateTime(2025, 6, 12, 9, 9, 4, 411, DateTimeKind.Utc).AddTicks(5059), "Máy cắt cỏ Honda GX35 công suất mạnh, tiết kiệm nhiên liệu, độ bền cao.", 3300000m, "/images/products/may-cat-co-honda.jpg", true, "Máy cắt cỏ Honda GX35", 3500000m, "MM-MCC-010", 20, new DateTime(2025, 6, 12, 9, 9, 4, 411, DateTimeKind.Utc).AddTicks(5059) }
                });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "Id", "CreatedAt", "RoleId", "UserId" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 6, 12, 9, 9, 4, 411, DateTimeKind.Utc).AddTicks(5059), 1, 1 },
                    { 2, new DateTime(2025, 6, 12, 9, 9, 4, 411, DateTimeKind.Utc).AddTicks(5059), 2, 2 },
                    { 3, new DateTime(2025, 6, 12, 9, 9, 4, 411, DateTimeKind.Utc).AddTicks(5059), 2, 3 },
                    { 4, new DateTime(2025, 6, 12, 9, 9, 4, 411, DateTimeKind.Utc).AddTicks(5059), 2, 4 },
                    { 5, new DateTime(2025, 6, 12, 9, 9, 4, 411, DateTimeKind.Utc).AddTicks(5059), 3, 5 },
                    { 6, new DateTime(2025, 6, 12, 9, 9, 4, 411, DateTimeKind.Utc).AddTicks(5059), 3, 6 },
                    { 7, new DateTime(2025, 6, 12, 9, 9, 4, 411, DateTimeKind.Utc).AddTicks(5059), 3, 7 },
                    { 8, new DateTime(2025, 6, 12, 9, 9, 4, 411, DateTimeKind.Utc).AddTicks(5059), 3, 8 },
                    { 9, new DateTime(2025, 6, 12, 9, 9, 4, 411, DateTimeKind.Utc).AddTicks(5059), 3, 9 }
                });

            migrationBuilder.InsertData(
                table: "CartItems",
                columns: new[] { "Id", "CartId", "CreatedAt", "ProductId", "Quantity", "TotalPrice", "UnitPrice", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2025, 6, 10, 9, 9, 4, 411, DateTimeKind.Utc).AddTicks(5059), 1, 2, 84000m, 42000m, new DateTime(2025, 6, 10, 9, 9, 4, 411, DateTimeKind.Utc).AddTicks(5059) },
                    { 2, 1, new DateTime(2025, 6, 12, 3, 9, 4, 411, DateTimeKind.Utc).AddTicks(5059), 2, 1, 25000m, 25000m, new DateTime(2025, 6, 12, 3, 9, 4, 411, DateTimeKind.Utc).AddTicks(5059) },
                    { 3, 1, new DateTime(2025, 6, 12, 3, 9, 4, 411, DateTimeKind.Utc).AddTicks(5059), 4, 1, 18000m, 18000m, new DateTime(2025, 6, 12, 3, 9, 4, 411, DateTimeKind.Utc).AddTicks(5059) },
                    { 4, 2, new DateTime(2025, 6, 11, 9, 9, 4, 411, DateTimeKind.Utc).AddTicks(5059), 8, 1, 420000m, 420000m, new DateTime(2025, 6, 11, 9, 9, 4, 411, DateTimeKind.Utc).AddTicks(5059) },
                    { 5, 2, new DateTime(2025, 6, 12, 6, 9, 4, 411, DateTimeKind.Utc).AddTicks(5059), 4, 5, 90000m, 18000m, new DateTime(2025, 6, 12, 6, 9, 4, 411, DateTimeKind.Utc).AddTicks(5059) },
                    { 6, 2, new DateTime(2025, 6, 12, 6, 9, 4, 411, DateTimeKind.Utc).AddTicks(5059), 9, 2, 150000m, 75000m, new DateTime(2025, 6, 12, 6, 9, 4, 411, DateTimeKind.Utc).AddTicks(5059) },
                    { 7, 3, new DateTime(2025, 6, 11, 21, 9, 4, 411, DateTimeKind.Utc).AddTicks(5059), 6, 1, 85000m, 85000m, new DateTime(2025, 6, 11, 21, 9, 4, 411, DateTimeKind.Utc).AddTicks(5059) }
                });

            migrationBuilder.InsertData(
                table: "EngineerFarmerAssignments",
                columns: new[] { "Id", "AssignedAt", "EngineerId", "FarmerId", "IsActive", "Notes" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 6, 12, 9, 9, 4, 411, DateTimeKind.Utc).AddTicks(5059), 1, 1, true, "Hỗ trợ chẩn đoán bệnh lúa" },
                    { 2, new DateTime(2025, 6, 12, 9, 9, 4, 411, DateTimeKind.Utc).AddTicks(5059), 1, 2, true, "Tư vấn phòng trị bệnh hại" },
                    { 3, new DateTime(2025, 6, 12, 9, 9, 4, 411, DateTimeKind.Utc).AddTicks(5059), 2, 3, true, "Tư vấn dinh dưỡng cây trồng" },
                    { 4, new DateTime(2025, 6, 12, 9, 9, 4, 411, DateTimeKind.Utc).AddTicks(5059), 2, 4, true, "Hướng dẫn bón phân" },
                    { 5, new DateTime(2025, 6, 12, 9, 9, 4, 411, DateTimeKind.Utc).AddTicks(5059), 3, 5, true, "Tư vấn kỹ thuật canh tác hữu cơ" }
                });

            migrationBuilder.InsertData(
                table: "OrderItems",
                columns: new[] { "Id", "OrderId", "ProductId", "Quantity", "TotalPrice", "UnitPrice" },
                values: new object[,]
                {
                    { 1, 1, 1, 3, 126000m, 42000m },
                    { 2, 1, 2, 1, 25000m, 25000m },
                    { 3, 1, 4, 2, 36000m, 18000m },
                    { 4, 1, 9, 1, 75000m, 75000m },
                    { 5, 2, 10, 1, 3300000m, 3300000m },
                    { 6, 2, 4, 5, 90000m, 18000m },
                    { 7, 2, 5, 3, 60000m, 20000m },
                    { 8, 3, 3, 5, 160000m, 32000m },
                    { 9, 4, 7, 1, 95000m, 95000m }
                });

            migrationBuilder.InsertData(
                table: "Reviews",
                columns: new[] { "Id", "CreatedAt", "ProductId", "ReviewMessage", "ReviewValue", "UpdatedAt", "UserId", "UserName" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 5, 23, 9, 9, 4, 411, DateTimeKind.Utc).AddTicks(5059), 1, "Hạt giống chất lượng tuyệt vời! Tỷ lệ nảy mầm cao, cây lúa sinh trưởng khỏe mạnh. Năng suất đạt như quảng cáo. Sẽ tiếp tục mua ở lần sau.", 5, new DateTime(2025, 5, 23, 9, 9, 4, 411, DateTimeKind.Utc).AddTicks(5059), 5, "Nguyễn Thị D" },
                    { 2, new DateTime(2025, 5, 25, 9, 9, 4, 411, DateTimeKind.Utc).AddTicks(5059), 1, "Giống lúa tốt, năng suất ổn định. Chỉ có điều giá hơi cao so với các giống khác. Nhưng chất lượng xứng đáng với giá tiền.", 4, new DateTime(2025, 5, 25, 9, 9, 4, 411, DateTimeKind.Utc).AddTicks(5059), 6, "Trần Văn E" },
                    { 3, new DateTime(2025, 5, 28, 9, 9, 4, 411, DateTimeKind.Utc).AddTicks(5059), 4, "Phân bón hiệu quả tốt, cây trồng xanh tốt sau khi bón. Giá cả hợp lý, giao hàng nhanh chóng.", 5, new DateTime(2025, 5, 28, 9, 9, 4, 411, DateTimeKind.Utc).AddTicks(5059), 7, "Lê Thị F" },
                    { 4, new DateTime(2025, 5, 31, 9, 9, 4, 411, DateTimeKind.Utc).AddTicks(5059), 8, "Máy phun hoạt động tốt, áp suất ổn định. Dung tích 16L vừa phải cho diện tích nhỏ. Chỉ có điều hơi nặng khi mang lâu.", 4, new DateTime(2025, 5, 31, 9, 9, 4, 411, DateTimeKind.Utc).AddTicks(5059), 8, "Phạm Văn G" },
                    { 5, new DateTime(2025, 6, 2, 9, 9, 4, 411, DateTimeKind.Utc).AddTicks(5059), 5, "Phân hữu cơ rất tốt! Đất trở nên tơi xốp hơn, cây trồng khỏe mạnh. Đặc biệt hiệu quả với rau màu. Giá có khuyến mãi nữa, rất hài lòng!", 5, new DateTime(2025, 6, 2, 9, 9, 4, 411, DateTimeKind.Utc).AddTicks(5059), 9, "Hoàng Thị H" },
                    { 6, new DateTime(2025, 6, 4, 9, 9, 4, 411, DateTimeKind.Utc).AddTicks(5059), 6, "Thuốc trừ sâu hiệu quả, sâu chết nhanh sau khi phun. Tuy nhiên cần chú ý an toàn khi sử dụng.", 4, new DateTime(2025, 6, 4, 9, 9, 4, 411, DateTimeKind.Utc).AddTicks(5059), 5, "Nguyễn Thị D" },
                    { 7, new DateTime(2025, 6, 7, 9, 9, 4, 411, DateTimeKind.Utc).AddTicks(5059), 10, "Máy cắt cỏ Honda chất lượng xuất sắc! Máy chạy êm, cắt sạch, tiết kiệm xăng. Đáng đồng tiền bát gạo. Khuyên mọi người nên mua.", 5, new DateTime(2025, 6, 7, 9, 9, 4, 411, DateTimeKind.Utc).AddTicks(5059), 6, "Trần Văn E" }
                });

            migrationBuilder.InsertData(
                table: "Tickets",
                columns: new[] { "Id", "AssignedEngineerId", "Category", "ContactMethod", "CreatedAt", "CropType", "Description", "FarmerId", "ImageUrl", "Location", "PhoneNumber", "Priority", "ResolvedAt", "Status", "Title", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, 1, "Bệnh cây trồng", "Điện thoại", new DateTime(2025, 6, 7, 9, 9, 4, 411, DateTimeKind.Utc).AddTicks(5059), "Lúa", "Lúa của tôi đang trong giai đoạn đẻ nhánh nhưng bị vàng lá từ dưới lên, một số cây đã chết khô. Tôi đã tưới nước đầy đủ nhưng tình trạng không cải thiện.", 1, "default.jpg", "Ruộng A1, Ấp 1, Xã Tân Phú", "0905678901", "high", null, "in_progress", "Lúa bị vàng lá và chết khô", new DateTime(2025, 6, 10, 9, 9, 4, 411, DateTimeKind.Utc).AddTicks(5059) },
                    { 2, 2, "Dinh dưỡng cây trồng", "Email", new DateTime(2025, 6, 9, 9, 9, 4, 411, DateTimeKind.Utc).AddTicks(5059), "Lúa", "Tôi chuẩn bị gieo sạ vụ lúa mới, muốn được tư vấn về lượng phân bón cần thiết cho 3.2 hecta đất.", 2, "default.jpg", "Ruộng B2, Ấp 2, Xã Long Phú", "0906789012", "medium", null, "assigned", "Tư vấn lượng phân bón cho vụ lúa mới", new DateTime(2025, 6, 9, 9, 9, 4, 411, DateTimeKind.Utc).AddTicks(5059) },
                    { 3, null, "Sâu bệnh", "Điện thoại", new DateTime(2025, 6, 11, 9, 9, 4, 411, DateTimeKind.Utc).AddTicks(5059), "Rau màu", "Rau cải của tôi bị sâu ăn lá nghiêm trọng, lá bị thủng lỗ chỗ. Cần tư vấn thuốc trừ sâu phù hợp.", 3, "default.jpg", "Vườn C1, Ấp 3, Xã Vĩnh Hậu", "0907890123", "urgent", null, "open", "Rau cải bị sâu ăn lá", new DateTime(2025, 6, 11, 9, 9, 4, 411, DateTimeKind.Utc).AddTicks(5059) },
                    { 4, 3, "Kỹ thuật canh tác", "Email", new DateTime(2025, 6, 2, 9, 9, 4, 411, DateTimeKind.Utc).AddTicks(5059), "Cây ăn trái", "Tôi muốn chuyển đổi sang mô hình trồng trọt hữu cơ cho vườn cây ăn trái. Cần được hướng dẫn quy trình và kỹ thuật.", 4, "default.jpg", "Vườn D1, Ấp 4, Xã Đức Hòa", "0908901234", "low", new DateTime(2025, 6, 11, 9, 9, 4, 411, DateTimeKind.Utc).AddTicks(5059), "resolved", "Hướng dẫn kỹ thuật trồng hữu cơ", new DateTime(2025, 6, 11, 9, 9, 4, 411, DateTimeKind.Utc).AddTicks(5059) },
                    { 5, null, "Đất đai", "Điện thoại", new DateTime(2025, 6, 11, 21, 9, 4, 411, DateTimeKind.Utc).AddTicks(5059), "Rau màu", "Đất trồng rau của tôi có vẻ bị chua, cây trồng sinh trưởng chậm, lá vàng. Cần tư vấn cách cải tạo đất.", 5, "default.jpg", "Ruộng E1, Ấp 5, Xã Tân Trụ", "0909012345", "medium", null, "open", "Đất bị chua, cây trồng sinh trưởng kém", new DateTime(2025, 6, 11, 21, 9, 4, 411, DateTimeKind.Utc).AddTicks(5059) }
                });

            migrationBuilder.InsertData(
                table: "TicketComments",
                columns: new[] { "Id", "Comment", "CreatedAt", "IsInternal", "TicketId", "UserId" },
                values: new object[,]
                {
                    { 1, "Dựa vào mô tả và hình ảnh, có thể cây lúa của anh bị bệnh khô vằn. Tôi sẽ đến khảo sát thực địa vào chiều mai.", new DateTime(2025, 6, 8, 9, 9, 4, 411, DateTimeKind.Utc).AddTicks(5059), false, 1, 2 },
                    { 2, "Cảm ơn kỹ sư. Tôi sẽ chờ anh đến khảo sát. Hiện tại tình trạng vẫn đang lan rộng.", new DateTime(2025, 6, 8, 9, 9, 4, 411, DateTimeKind.Utc).AddTicks(5059), false, 1, 5 },
                    { 3, "Đã khảo sát thực địa. Xác định là bệnh khô vằn do nấm. Đã hướng dẫn anh sử dụng thuốc Validamycin 3% với liều lượng 1.5L/ha.", new DateTime(2025, 6, 10, 9, 9, 4, 411, DateTimeKind.Utc).AddTicks(5059), false, 1, 2 },
                    { 4, "Với diện tích 3.2ha lúa, anh nên sử dụng: Phân lót 200kg NPK 16-16-8, phân thúc lần 1: 100kg Urea, phân thúc lần 2: 80kg NPK 20-20-15.", new DateTime(2025, 6, 10, 9, 9, 4, 411, DateTimeKind.Utc).AddTicks(5059), false, 2, 3 },
                    { 5, "Đã hoàn thành hướng dẫn chuyển đổi hữu cơ cho anh. Gửi kèm tài liệu quy trình chi tiết qua email.", new DateTime(2025, 6, 11, 9, 9, 4, 411, DateTimeKind.Utc).AddTicks(5059), false, 4, 4 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_BlogCategories_Name",
                table: "BlogCategories",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BlogCategories_Slug",
                table: "BlogCategories",
                column: "Slug",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Blogs_AuthorId",
                table: "Blogs",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_Blogs_CategoryId",
                table: "Blogs",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Blogs_Slug",
                table: "Blogs",
                column: "Slug",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_CartId",
                table: "CartItems",
                column: "CartId");

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_ProductId",
                table: "CartItems",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Carts_UserId",
                table: "Carts",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EngineerFarmerAssignments_FarmerId",
                table: "EngineerFarmerAssignments",
                column: "FarmerId");

            migrationBuilder.CreateIndex(
                name: "IX_EngineerFarmerAssignments_Unique_Active",
                table: "EngineerFarmerAssignments",
                columns: new[] { "EngineerId", "FarmerId", "IsActive" },
                unique: true,
                filter: "[IsActive] = 1");

            migrationBuilder.CreateIndex(
                name: "IX_Engineers_UserId",
                table: "Engineers",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Farmers_UserId",
                table: "Farmers",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_News_CategoryId",
                table: "News",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_NewsCategories_Name",
                table: "NewsCategories",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_NewsCategories_Slug",
                table: "NewsCategories",
                column: "Slug",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_OrderId",
                table: "OrderItems",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_ProductId",
                table: "OrderItems",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_OrderNumber",
                table: "Orders",
                column: "OrderNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Orders_UserId",
                table: "Orders",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductCategories_Name",
                table: "ProductCategories",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductCategories_Slug",
                table: "ProductCategories",
                column: "Slug",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_SKU",
                table: "Products",
                column: "SKU",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_ProductId",
                table: "Reviews",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_UserId",
                table: "Reviews",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Roles_Name",
                table: "Roles",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TicketComments_TicketId",
                table: "TicketComments",
                column: "TicketId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketComments_UserId",
                table: "TicketComments",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_AssignedEngineerId",
                table: "Tickets",
                column: "AssignedEngineerId");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_FarmerId",
                table: "Tickets",
                column: "FarmerId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_RoleId",
                table: "UserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_UserId",
                table: "UserRoles",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_PhoneNumber",
                table: "Users",
                column: "PhoneNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_UserName",
                table: "Users",
                column: "UserName",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Blogs");

            migrationBuilder.DropTable(
                name: "CartItems");

            migrationBuilder.DropTable(
                name: "EngineerFarmerAssignments");

            migrationBuilder.DropTable(
                name: "News");

            migrationBuilder.DropTable(
                name: "OrderItems");

            migrationBuilder.DropTable(
                name: "Reviews");

            migrationBuilder.DropTable(
                name: "TicketComments");

            migrationBuilder.DropTable(
                name: "UserRoles");

            migrationBuilder.DropTable(
                name: "BlogCategories");

            migrationBuilder.DropTable(
                name: "Carts");

            migrationBuilder.DropTable(
                name: "NewsCategories");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Tickets");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "ProductCategories");

            migrationBuilder.DropTable(
                name: "Engineers");

            migrationBuilder.DropTable(
                name: "Farmers");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
