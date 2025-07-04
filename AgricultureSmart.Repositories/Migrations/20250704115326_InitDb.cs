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
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    Rating = table.Column<double>(type: "float", nullable: false)
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
                    { 1, new DateTime(2025, 7, 4, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840), "Các bài viết về bệnh hại trên cây trồng và cách phòng trị", true, "Bệnh cây trồng", "benh-cay-trong" },
                    { 2, new DateTime(2025, 7, 4, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840), "Hướng dẫn kỹ thuật trồng trọt và chăm sóc cây", true, "Kỹ thuật canh tác", "ky-thuat-canh-tac" },
                    { 3, new DateTime(2025, 7, 4, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840), "Thông tin về các loại phân bón và cách sử dụng", true, "Phân bón", "phan-bon" },
                    { 4, new DateTime(2025, 7, 4, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840), "Hướng dẫn sử dụng thuốc BVTV an toàn", true, "Thuốc bảo vệ thực vật", "thuoc-bao-ve-thuc-vat" },
                    { 5, new DateTime(2025, 7, 4, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840), "Lịch thời vụ và mùa vụ canh tác", true, "Thời vụ", "thoi-vu" }
                });

            migrationBuilder.InsertData(
                table: "NewsCategories",
                columns: new[] { "Id", "CreatedAt", "Description", "IsActive", "Name", "Slug" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 7, 4, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840), "Tin tức về chính sách nông nghiệp", true, "Chính sách", "chinh-sach" },
                    { 2, new DateTime(2025, 7, 4, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840), "Thông tin thị trường nông sản", true, "Thị trường", "thi-truong" },
                    { 3, new DateTime(2025, 7, 4, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840), "Công nghệ mới trong nông nghiệp", true, "Công nghệ", "cong-nghe" },
                    { 4, new DateTime(2025, 7, 4, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840), "Các sự kiện nông nghiệp", true, "Sự kiện", "su-kien" },
                    { 5, new DateTime(2025, 7, 4, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840), "Dự báo thời tiết phục vụ sản xuất", true, "Thời tiết", "thoi-tiet" }
                });

            migrationBuilder.InsertData(
                table: "ProductCategories",
                columns: new[] { "Id", "CreatedAt", "Description", "IsActive", "Name", "Slug" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 7, 4, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840), "Các loại hạt giống cây trồng", true, "Hạt giống", "hat-giong" },
                    { 2, new DateTime(2025, 7, 4, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840), "Các loại phân bón hữu cơ và vô cơ", true, "Phân bón", "phan-bon" },
                    { 3, new DateTime(2025, 7, 4, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840), "Thuốc bảo vệ thực vật", true, "Thuốc BVTV", "thuoc-bvtv" },
                    { 4, new DateTime(2025, 7, 4, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840), "Các dụng cụ và thiết bị nông nghiệp", true, "Dụng cụ nông nghiệp", "dung-cu-nong-nghiep" },
                    { 5, new DateTime(2025, 7, 4, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840), "Máy móc thiết bị nông nghiệp", true, "Máy móc", "may-moc" }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "CreatedAt", "Description", "Name" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 7, 4, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840), "System Administrator", "Admin" },
                    { 2, new DateTime(2025, 7, 4, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840), "Agricultural Engineer", "Engineer" },
                    { 3, new DateTime(2025, 7, 4, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840), "Farmer User", "Farmer" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Address", "CreatedAt", "Email", "IsActive", "Password", "PhoneNumber", "UpdatedAt", "UserName" },
                values: new object[,]
                {
                    { 1, "123 Đường Cách Mạng Tháng 8, Quận 1, TP.HCM", new DateTime(2025, 7, 4, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840), "admin@agricultural.com", true, "admin123", "0901234567", new DateTime(2025, 7, 4, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840), "admin" },
                    { 2, "456 Đường Lê Lợi, Quận 3, TP.HCM", new DateTime(2025, 7, 4, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840), "nguyenvana@agricultural.com", true, "engineer123", "0902345678", new DateTime(2025, 7, 4, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840), "engineer1" },
                    { 3, "789 Đường Nguyễn Huệ, Quận 1, TP.HCM", new DateTime(2025, 7, 4, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840), "tranthib@agricultural.com", true, "engineer123", "0903456789", new DateTime(2025, 7, 4, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840), "engineer2" },
                    { 4, "321 Đường Pasteur, Quận 3, TP.HCM", new DateTime(2025, 7, 4, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840), "levanc@agricultural.com", true, "engineer123", "0904567890", new DateTime(2025, 7, 4, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840), "engineer3" },
                    { 5, "Ấp 1, Xã Tân Phú, Huyện Châu Thành, Tỉnh An Giang", new DateTime(2025, 7, 4, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840), "nguyenthid@gmail.com", true, "farmer123", "0905678901", new DateTime(2025, 7, 4, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840), "farmer1" },
                    { 6, "Ấp 2, Xã Long Phú, Huyện Phú Tân, Tỉnh An Giang", new DateTime(2025, 7, 4, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840), "tranvane@gmail.com", true, "farmer123", "0906789012", new DateTime(2025, 7, 4, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840), "farmer2" },
                    { 7, "Ấp 3, Xã Vĩnh Hậu, Huyện Tân Hưng, Tỉnh Long An", new DateTime(2025, 7, 4, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840), "lethif@gmail.com", true, "farmer123", "0907890123", new DateTime(2025, 7, 4, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840), "farmer3" },
                    { 8, "Ấp 4, Xã Đức Hòa, Huyện Đức Hòa, Tỉnh Long An", new DateTime(2025, 7, 4, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840), "phamvang@gmail.com", true, "farmer123", "0908901234", new DateTime(2025, 7, 4, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840), "farmer4" },
                    { 9, "Ấp 5, Xã Tân Trụ, Huyện Tân Trụ, Tỉnh Long An", new DateTime(2025, 7, 4, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840), "hoangthih@gmail.com", true, "farmer123", "0909012345", new DateTime(2025, 7, 4, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840), "farmer5" }
                });

            migrationBuilder.InsertData(
                table: "Blogs",
                columns: new[] { "Id", "AuthorId", "CategoryId", "Content", "CreatedAt", "FeaturedImage", "PublishedAt", "Slug", "Status", "Title", "UpdatedAt", "ViewCount" },
                values: new object[,]
                {
                    { 1, 2, 1, "<h2>Cách nhận biết và phòng trị bệnh đạo ôn lúa</h2> <p>Bệnh đạo ôn là một trong những bệnh hại nguy hiểm nhất trên cây lúa, có thể gây thất thu nghiêm trọng nếu không được phát hiện và xử lý kịp thời. Bệnh thường phát triển mạnh trong điều kiện thời tiết ẩm ướt, mưa nhiều, đặc biệt trong giai đoạn mạ và đẻ nhánh.</p> <h3>Dấu hiệu nhận biết bệnh đạo ôn</h3> <p>Bà con cần lưu ý các dấu hiệu sau để kịp thời phát hiện bệnh:</p> <ul> <li>Vết bệnh hình thoi, có màu xám ở giữa, viền nâu hoặc tím</li> <li>Lá bị cháy khô từng đốm hoặc toàn bộ, dễ gãy</li> <li>Bệnh lan nhanh khi trời âm u, độ ẩm cao</li> <li>Có thể xuất hiện trên cổ bông, hạt lép lửng nhiều</li> </ul> <h3>Biện pháp phòng trị hiệu quả</h3> <p>Để phòng và trị bệnh đạo ôn, bà con nên thực hiện các biện pháp sau:</p> <ul> <li>Sử dụng giống lúa kháng bệnh, gieo sạ với mật độ hợp lý</li> <li>Bón phân cân đối, tránh bón thừa đạm</li> <li>Thăm đồng thường xuyên để phát hiện sớm</li> <li>Phun thuốc đặc trị đạo ôn ngay khi phát hiện triệu chứng đầu tiên</li> </ul> <h3>Kết luận</h3> <p>Bệnh đạo ôn có thể được kiểm soát nếu được phát hiện sớm và xử lý kịp thời. Bà con cần chủ động áp dụng các biện pháp phòng bệnh và thường xuyên theo dõi diễn biến đồng ruộng để đảm bảo năng suất và chất lượng vụ mùa.</p>", new DateTime(2025, 6, 19, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840), "https://w7zbytrd10.ufs.sh/f/fnkloM7shIMicD3CbMsrbCMWJ2s37qBzo5ImL9DTFgHUYEQh", new DateTime(2025, 6, 24, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840), "cach-nhan-biet-va-phong-tri-benh-dao-on-lua", "published", "Cách nhận biết và phòng trị bệnh đạo ôn lúa", new DateTime(2025, 6, 24, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840), 1250 },
                    { 2, 3, 3, "<h2>Hướng dẫn bón phân NPK cho cây lúa theo từng giai đoạn</h2> <p>Bón phân hợp lý theo từng giai đoạn sinh trưởng là yếu tố quan trọng giúp cây lúa phát triển khỏe mạnh, tăng năng suất và chất lượng hạt. Phân NPK (Đạm – Lân – Kali) đóng vai trò thiết yếu trong việc cung cấp dinh dưỡng cho cây lúa suốt vụ mùa.</p> <h3>Nhu cầu phân bón theo từng giai đoạn</h3> <p>Tùy theo giai đoạn phát triển, cây lúa cần lượng phân khác nhau:</p> <ul> <li><strong>Giai đoạn mạ và cấy:</strong> Tăng cường lân giúp bộ rễ phát triển, bón lót NPK tỷ lệ cao lân (ví dụ: 10-20-10)</li> <li><strong>Giai đoạn đẻ nhánh:</strong> Cần nhiều đạm để thúc đẩy sinh trưởng, sử dụng NPK cân đối (16-16-8 hoặc 20-10-10)</li> <li><strong>Giai đoạn làm đòng – trổ:</strong> Tăng kali giúp cứng cây, bông to, hạt chắc; dùng NPK có hàm lượng kali cao (13-13-21 hoặc 15-5-20)</li> <li><strong>Giai đoạn sau trổ:</strong> Hạn chế bón thêm đạm, ưu tiên kali để chống đổ ngã và cải thiện chất lượng hạt</li> </ul> <h3>Khuyến cáo khi bón phân</h3> <p>Để bón phân NPK hiệu quả, bà con cần chú ý:</p> <ul> <li>Bón đúng lúc, đúng lượng, đúng cách để tránh lãng phí và gây ô nhiễm</li> <li>Không bón dồn, chia làm 2–3 lần theo từng giai đoạn</li> <li>Kết hợp bón thúc và bón qua lá nếu cần thiết</li> <li>Tham khảo hướng dẫn kỹ thuật và điều chỉnh theo loại đất, giống lúa và thời tiết</li> </ul> <h3>Kết luận</h3> <p>Việc bón phân NPK đúng kỹ thuật giúp cây lúa phát triển ổn định, tăng sức chống chịu và mang lại hiệu quả kinh tế cao. Bà con cần nắm rõ nhu cầu dinh dưỡng của cây theo từng giai đoạn để có cách bón hợp lý nhất.</p>", new DateTime(2025, 6, 22, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840), "https://w7zbytrd10.ufs.sh/f/fnkloM7shIMiSBML9wFO98YZg2Hhe6lTzWnFvqcoitkbuxsU", new DateTime(2025, 6, 27, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840), "huong-dan-bon-phan-npk-cho-cay-lua", "published", "Hướng dẫn bón phân NPK cho cây lúa theo từng giai đoạn", new DateTime(2025, 6, 27, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840), 980 },
                    { 3, 4, 2, "<h2>Kỹ thuật trồng rau màu trong nhà kính</h2> <p>Trồng rau màu trong nhà kính là giải pháp nông nghiệp hiện đại giúp kiểm soát môi trường trồng trọt, hạn chế sâu bệnh và nâng cao năng suất, chất lượng sản phẩm. Phương pháp này đặc biệt phù hợp trong điều kiện khí hậu khắc nghiệt hoặc khu vực canh tác chuyên canh.</p> <h3>Điều kiện cần thiết khi trồng trong nhà kính</h3> <p>Để rau màu phát triển tốt trong môi trường nhà kính, cần đảm bảo:</p> <ul> <li>Hệ thống thông gió và che nắng linh hoạt</li> <li>Nhiệt độ duy trì từ 20–28°C, độ ẩm 60–80%</li> <li>Đất hoặc giá thể sạch bệnh, tơi xốp, giàu dinh dưỡng</li> <li>Có hệ thống tưới nhỏ giọt hoặc phun sương tự động</li> </ul> <h3>Kỹ thuật trồng và chăm sóc</h3> <p>Quy trình trồng rau màu trong nhà kính nên tuân thủ các bước sau:</p> <ul> <li>Làm đất kỹ, bón lót phân hữu cơ hoặc NPK cân đối</li> <li>Chọn giống rau phù hợp: xà lách, cải ngọt, cà chua, dưa leo…</li> <li>Gieo hạt hoặc trồng cây con theo mật độ hợp lý</li> <li>Thường xuyên theo dõi sâu bệnh, điều chỉnh ánh sáng và nước tưới</li> </ul> <h3>Kết luận</h3> <p>Trồng rau màu trong nhà kính là mô hình hiệu quả, bền vững và phù hợp với xu hướng nông nghiệp công nghệ cao. Bà con cần đầu tư đúng kỹ thuật, theo dõi sát điều kiện môi trường để tối ưu hóa hiệu quả sản xuất và chất lượng rau thương phẩm.</p>", new DateTime(2025, 6, 26, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840), "https://w7zbytrd10.ufs.sh/f/fnkloM7shIMisIb1G3pZK8uXSfxmLQDTeF5A42vawjIYbitn", new DateTime(2025, 6, 29, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840), "ky-thuat-trong-rau-mau-trong-nha-kinh", "published", "Kỹ thuật trồng rau màu trong nhà kính", new DateTime(2025, 6, 29, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840), 756 },
                    { 4, 2, 4, "<h2>An toàn khi sử dụng thuốc bảo vệ thực vật</h2> <p>Thuốc bảo vệ thực vật (BVTV) là công cụ quan trọng giúp phòng trừ sâu bệnh và bảo vệ năng suất cây trồng. Tuy nhiên, nếu sử dụng không đúng cách có thể gây hại cho sức khỏe con người, vật nuôi và môi trường. Vì vậy, việc sử dụng thuốc BVTV cần tuân thủ đúng kỹ thuật và nguyên tắc an toàn.</p> <h3>Nguy cơ khi sử dụng sai cách</h3> <p>Sử dụng thuốc BVTV không đúng quy định có thể gây ra nhiều hậu quả nghiêm trọng:</p> <ul> <li>Ngộ độc cho người phun hoặc người tiêu dùng</li> <li>Làm ô nhiễm đất, nước và không khí</li> <li>Gây hiện tượng kháng thuốc ở sâu bệnh</li> <li>Ảnh hưởng đến hệ sinh thái và đa dạng sinh học</li> </ul> <h3>Khuyến cáo sử dụng an toàn</h3> <p>Để đảm bảo an toàn khi sử dụng thuốc BVTV, bà con nên:</p> <ul> <li>Đọc kỹ nhãn mác, hướng dẫn sử dụng và tuân thủ liều lượng</li> <li>Mặc đồ bảo hộ khi pha và phun thuốc (khẩu trang, găng tay, áo dài tay)</li> <li>Không ăn uống, hút thuốc trong khi phun thuốc</li> <li>Bảo quản thuốc nơi cao ráo, xa trẻ em và thực phẩm</li> <li>Thu gom, xử lý bao bì thuốc đúng quy định, không vứt bừa bãi</li> </ul> <h3>Kết luận</h3> <p>Việc sử dụng thuốc bảo vệ thực vật đúng cách không chỉ giúp bảo vệ cây trồng mà còn góp phần bảo vệ sức khỏe cộng đồng và môi trường sống. Bà con cần nâng cao nhận thức và thực hiện nghiêm túc các nguyên tắc an toàn trong suốt quá trình sử dụng thuốc BVTV.</p>", new DateTime(2025, 6, 28, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840), "https://w7zbytrd10.ufs.sh/f/fnkloM7shIMieGNO3RkRhUuAPvxwgZQ8qnB6MjCy4GDiFcm2", new DateTime(2025, 7, 1, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840), "an-toan-khi-su-dung-thuoc-bao-ve-thuc-vat", "published", "An toàn khi sử dụng thuốc bảo vệ thực vật", new DateTime(2025, 7, 1, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840), 1100 },
                    { 5, 3, 5, "<h2>Lịch thời vụ trồng lúa miền Nam năm 2024</h2> <p>Lịch thời vụ là yếu tố then chốt quyết định hiệu quả sản xuất lúa. Ở miền Nam, với điều kiện khí hậu nhiệt đới và hệ thống thủy lợi đa dạng, việc bố trí thời vụ hợp lý giúp né tránh thiên tai, sâu bệnh và tối ưu hóa năng suất. Dưới đây là lịch thời vụ trồng lúa năm 2024 được khuyến cáo cho các tỉnh miền Nam.</p> <h3>Các vụ lúa chính trong năm</h3> <p>Tại miền Nam, nông dân thường canh tác 2–3 vụ lúa/năm tùy điều kiện địa phương:</p> <ul> <li><strong>Vụ Đông Xuân (chính vụ):</strong> Gieo sạ từ tháng 11 đến giữa tháng 12/2023, thu hoạch vào tháng 2–3/2024</li> <li><strong>Vụ Hè Thu:</strong> Gieo sạ từ tháng 4 đến giữa tháng 5/2024, thu hoạch tháng 7–8</li> <li><strong>Vụ Thu Đông:</strong> Gieo sạ từ cuối tháng 7 đến giữa tháng 8/2024, thu hoạch tháng 10–11</li> </ul> <h3>Khuyến cáo thời vụ theo vùng</h3> <p>Tùy theo điều kiện thủy lợi và xâm nhập mặn, các địa phương cần điều chỉnh thời vụ hợp lý:</p> <ul> <li>Vùng ven biển cần gieo sạ sớm để né mặn (trong tháng 11 cho vụ Đông Xuân)</li> <li>Vùng có nguy cơ hạn, thiếu nước nên ưu tiên lúa ngắn ngày, gieo sạ tập trung</li> <li>Chọn giống phù hợp với từng vụ và điều kiện thổ nhưỡng, ưu tiên giống kháng sâu bệnh</li> <li>Tuân thủ lịch khuyến cáo của Sở Nông nghiệp và PTNT địa phương</li> </ul> <h3>Kết luận</h3> <p>Việc nắm vững và thực hiện đúng lịch thời vụ là giải pháp bền vững để nâng cao hiệu quả canh tác lúa. Bà con nông dân cần theo dõi sát tình hình thời tiết, nguồn nước và hướng dẫn của ngành chuyên môn để điều chỉnh lịch gieo sạ phù hợp trong năm 2024.</p>", new DateTime(2025, 6, 30, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840), "https://w7zbytrd10.ufs.sh/f/fnkloM7shIMi0mslrgCuPjHfi79dGltDa8U2WEFp6mTq1Ix3", new DateTime(2025, 7, 3, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840), "lich-thoi-vu-trong-lua-mien-nam-2024", "published", "Lịch thời vụ trồng lúa miền Nam năm 2024", new DateTime(2025, 7, 3, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840), 2100 }
                });

            migrationBuilder.InsertData(
                table: "Carts",
                columns: new[] { "Id", "CreatedAt", "TotalAmount", "UpdatedAt", "UserId" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 7, 2, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840), 127000m, new DateTime(2025, 7, 4, 5, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840), 5 },
                    { 2, new DateTime(2025, 7, 3, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840), 540000m, new DateTime(2025, 7, 4, 8, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840), 6 },
                    { 3, new DateTime(2025, 7, 3, 23, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840), 85000m, new DateTime(2025, 7, 4, 9, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840), 7 }
                });

            migrationBuilder.InsertData(
                table: "Engineers",
                columns: new[] { "Id", "Bio", "Certification", "CreatedAt", "ExperienceYears", "Specialization", "UpdatedAt", "UserId" },
                values: new object[,]
                {
                    { 1, "Chuyên gia về bệnh hại cây trồng với 8 năm kinh nghiệm trong lĩnh vực chẩn đoán và điều trị bệnh lúa, rau màu.", "[\"Chứng chỉ Kỹ sư Nông nghiệp\", \"Chứng chỉ Chuyên gia Bệnh học thực vật\"]", new DateTime(2025, 7, 4, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840), 8, "Bệnh học thực vật", new DateTime(2025, 7, 4, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840), 2 },
                    { 2, "Chuyên gia về dinh dưỡng và phân bón cây trồng, có kinh nghiệm tư vấn cho nhiều hợp tác xã nông nghiệp.", "[\"Chứng chỉ Kỹ sư Nông nghiệp\", \"Chứng chỉ Chuyên gia Dinh dưỡng thực vật\"]", new DateTime(2025, 7, 4, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840), 6, "Dinh dưỡng cây trồng", new DateTime(2025, 7, 4, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840), 3 },
                    { 3, "Chuyên gia kỹ thuật canh tác với 10 năm kinh nghiệm, chuyên về nông nghiệp hữu cơ và canh tác bền vững.", "[\"Chứng chỉ Kỹ sư Nông nghiệp\", \"Chứng chỉ Chuyên gia Kỹ thuật canh tác\", \"Chứng chỉ Nông nghiệp hữu cơ\"]", new DateTime(2025, 7, 4, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840), 10, "Kỹ thuật canh tác", new DateTime(2025, 7, 4, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840), 4 }
                });

            migrationBuilder.InsertData(
                table: "Farmers",
                columns: new[] { "Id", "CreatedAt", "CropTypes", "FarmLocation", "FarmSize", "FarmingExperienceYears", "UpdatedAt", "UserId" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 7, 4, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840), "[\"Lúa\", \"Rau màu\", \"Cây ăn trái\"]", "Ấp 1, Xã Tân Phú, Huyện Châu Thành, Tỉnh An Giang", 2.5m, 15, new DateTime(2025, 7, 4, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840), 5 },
                    { 2, new DateTime(2025, 7, 4, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840), "[\"Lúa\", \"Ngô\", \"Đậu tương\"]", "Ấp 2, Xã Long Phú, Huyện Phú Tân, Tỉnh An Giang", 3.2m, 12, new DateTime(2025, 7, 4, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840), 6 },
                    { 3, new DateTime(2025, 7, 4, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840), "[\"Rau màu\", \"Cây ăn trái\", \"Hoa màu\"]", "Ấp 3, Xã Vĩnh Hậu, Huyện Tân Hưng, Tỉnh Long An", 1.8m, 8, new DateTime(2025, 7, 4, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840), 7 },
                    { 4, new DateTime(2025, 7, 4, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840), "[\"Lúa\", \"Mía\", \"Cây ăn trái\"]", "Ấp 4, Xã Đức Hòa, Huyện Đức Hòa, Tỉnh Long An", 4.1m, 20, new DateTime(2025, 7, 4, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840), 8 },
                    { 5, new DateTime(2025, 7, 4, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840), "[\"Lúa\", \"Rau màu\", \"Đậu các loại\"]", "Ấp 5, Xã Tân Trụ, Huyện Tân Trụ, Tỉnh Long An", 2.9m, 10, new DateTime(2025, 7, 4, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840), 9 }
                });

            migrationBuilder.InsertData(
                table: "News",
                columns: new[] { "Id", "Author", "CategoryId", "Content", "CreatedAt", "Excerpt", "Featured", "ImageUrl", "PublishedAt", "Source", "Tags", "Title", "UpdatedAt", "ViewCount" },
                values: new object[] { 1, "Bộ Nông nghiệp và Phát triển Nông thôn", 1, "<h2>Chính phủ hỗ trợ 500 tỷ đồng cho nông dân chuyển đổi số</h2> <p>Nhằm thúc đẩy phát triển nông nghiệp hiện đại và bền vững, Chính phủ vừa phê duyệt gói hỗ trợ 500 tỷ đồng cho chương trình chuyển đổi số trong lĩnh vực nông nghiệp. Gói hỗ trợ này kỳ vọng sẽ giúp nông dân tiếp cận công nghệ, nâng cao năng suất và hiệu quả sản xuất.</p> <h3>Mục tiêu của chương trình</h3> <p>Chương trình chuyển đổi số hướng đến các mục tiêu chính:</p> <ul> <li>Ứng dụng công nghệ số vào sản xuất, giám sát và tiêu thụ nông sản</li> <li>Xây dựng cơ sở dữ liệu nông nghiệp hiện đại</li> <li>Hỗ trợ nông dân tiếp cận nền tảng thương mại điện tử</li> <li>Tăng cường đào tạo kỹ năng số cho lực lượng lao động nông thôn</li> </ul> <h3>Hình thức hỗ trợ cụ thể</h3> <p>Gói hỗ trợ 500 tỷ đồng sẽ được phân bổ thông qua các hình thức sau:</p> <ul> <li>Trang bị thiết bị thông minh, cảm biến và phần mềm quản lý canh tác</li> <li>Miễn/giảm chi phí đào tạo kỹ thuật số cho nông dân</li> <li>Hỗ trợ hợp tác xã và doanh nghiệp nông nghiệp xây dựng hệ thống truy xuất nguồn gốc</li> <li>Kết nối nông dân với các sàn giao dịch nông sản trực tuyến</li> </ul> <h3>Kết luận</h3> <p>Việc Chính phủ đầu tư 500 tỷ đồng cho chuyển đổi số trong nông nghiệp là một bước tiến quan trọng, tạo nền tảng cho nông dân hòa nhập vào nền kinh tế số. Bà con cần chủ động tham gia các chương trình đào tạo, ứng dụng công nghệ để nâng cao giá trị sản phẩm và cải thiện đời sống.</p>", new DateTime(2025, 7, 2, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840), "Chương trình hỗ trợ nông dân ứng dụng công nghệ số trong sản xuất nông nghiệp với tổng kinh phí 500 tỷ đồng.", true, "https://w7zbytrd10.ufs.sh/f/fnkloM7shIMiMuBS1lcg8GwqCcP0yiLAJIUza2njdx6s74f3", new DateTime(2025, 7, 2, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840), "Bộ Nông nghiệp và Phát triển Nông thôn", "[\"Chính sách\", \"Chuyển đổi số\", \"Hỗ trợ nông dân\"]", "Chính phủ hỗ trợ 500 tỷ đồng cho nông dân chuyển đổi số", new DateTime(2025, 7, 2, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840), 3500 });

            migrationBuilder.InsertData(
                table: "News",
                columns: new[] { "Id", "Author", "CategoryId", "Content", "CreatedAt", "Excerpt", "ImageUrl", "PublishedAt", "Source", "Tags", "Title", "UpdatedAt", "Urgent", "ViewCount" },
                values: new object[] { 2, "Hiệp hội Lương thực Việt Nam", 2, "<h2>Giá lúa tăng mạnh do xuất khẩu khởi sắc</h2> <p>Trong những tuần gần đây, thị trường lúa gạo ghi nhận đà tăng giá mạnh nhờ nhu cầu xuất khẩu từ nhiều thị trường lớn tăng cao. Đây là tín hiệu tích cực giúp nông dân phấn khởi và kỳ vọng vào một vụ mùa bội thu cả về năng suất lẫn giá trị.</p> <h3>Nguyên nhân giá lúa tăng</h3> <p>Nhiều yếu tố góp phần đẩy giá lúa tăng cao trên thị trường:</p> <ul> <li>Nhu cầu nhập khẩu gạo tăng mạnh từ Philippines, Indonesia và châu Phi</li> <li>Giá gạo thế giới duy trì ở mức cao do nguồn cung bị thắt chặt</li> <li>Chất lượng gạo Việt Nam được cải thiện, đáp ứng tiêu chuẩn quốc tế</li> <li>Chính sách mở rộng thị trường và xúc tiến thương mại hiệu quả</li> </ul> <h3>Tác động đối với nông dân</h3> <p>Giá lúa tăng mang lại nhiều lợi ích thiết thực cho người trồng lúa:</p> <ul> <li>Lợi nhuận vụ mùa tăng, giúp cải thiện thu nhập</li> <li>Thúc đẩy đầu tư vào sản xuất chất lượng cao, đạt chuẩn xuất khẩu</li> <li>Tăng động lực chuyển đổi canh tác theo hướng hữu cơ, bền vững</li> <li>Khuyến khích tham gia vào chuỗi liên kết sản xuất – tiêu thụ</li> </ul> <h3>Kết luận</h3> <p>Giá lúa tăng mạnh nhờ xuất khẩu khởi sắc là tín hiệu đáng mừng cho ngành lúa gạo Việt Nam. Để tận dụng cơ hội này, bà con cần tiếp tục nâng cao chất lượng canh tác, tham gia vào chuỗi giá trị và cập nhật thông tin thị trường để tối ưu hóa hiệu quả sản xuất.</p>", new DateTime(2025, 7, 3, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840), "Giá lúa tại các tỉnh ĐBSCL tăng 200-300 đồng/kg so với tuần trước nhờ nhu cầu xuất khẩu tăng cao.", "https://w7zbytrd10.ufs.sh/f/fnkloM7shIMipcsfHs4Lo9fDNC7QxjEYURZ8n2SbTa5isgWd", new DateTime(2025, 7, 3, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840), "Hiệp hội Lương thực Việt Nam", "[\"Giá lúa\", \"Xuất khẩu\", \"Thị trường\"]", "Giá lúa tăng mạnh do xuất khẩu khởi sắc", new DateTime(2025, 7, 3, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840), true, 2800 });

            migrationBuilder.InsertData(
                table: "News",
                columns: new[] { "Id", "Author", "CategoryId", "Content", "CreatedAt", "Excerpt", "Featured", "ImageUrl", "PublishedAt", "Source", "Tags", "Title", "UpdatedAt", "ViewCount" },
                values: new object[] { 3, "Viện Bảo vệ thực vật", 3, "<h2>Ứng dụng AI trong chẩn đoán bệnh cây trồng</h2> <p>Trí tuệ nhân tạo (AI) đang trở thành công cụ hữu hiệu trong lĩnh vực nông nghiệp, đặc biệt là trong việc chẩn đoán và phòng trị bệnh trên cây trồng. Việc áp dụng AI không chỉ giúp phát hiện bệnh nhanh chóng mà còn góp phần nâng cao hiệu quả sản xuất và giảm chi phí cho nông dân.</p> <h3>Lợi ích khi ứng dụng AI</h3> <p>AI mang lại nhiều lợi ích thiết thực trong việc quản lý sức khỏe cây trồng:</p> <ul> <li>Phát hiện sớm các dấu hiệu bệnh qua hình ảnh lá, thân, quả</li> <li>Phân tích dữ liệu nhanh và chính xác, giảm phụ thuộc vào kinh nghiệm cá nhân</li> <li>Đề xuất giải pháp xử lý phù hợp với từng loại bệnh và điều kiện thực tế</li> <li>Tiết kiệm chi phí nhân công, hạn chế sử dụng thuốc BVTV không cần thiết</li> </ul> <h3>Cách ứng dụng trong thực tiễn</h3> <p>Nông dân có thể áp dụng AI trong chẩn đoán bệnh cây trồng theo các cách sau:</p> <ul> <li>Sử dụng ứng dụng di động AI để chụp ảnh lá và nhận diện bệnh qua camera</li> <li>Kết hợp AI với thiết bị IoT (cảm biến, camera) để giám sát liên tục tình trạng cây</li> <li>Truy cập nền tảng chẩn đoán trực tuyến tích hợp AI và cơ sở dữ liệu lớn</li> <li>Tham gia các mô hình canh tác thông minh có hỗ trợ AI phân tích toàn diện</li> </ul> <h3>Kết luận</h3> <p>Ứng dụng AI trong chẩn đoán bệnh cây trồng là bước tiến quan trọng giúp nông nghiệp chuyển mình theo hướng hiện đại và bền vững. Bà con cần mạnh dạn tiếp cận công nghệ mới, kết hợp cùng kiến thức thực tiễn để tối ưu hóa năng suất và bảo vệ cây trồng hiệu quả hơn.</p>", new DateTime(2025, 7, 3, 23, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840), "Công nghệ trí tuệ nhân tạo đang được ứng dụng rộng rãi trong việc chẩn đoán bệnh hại cây trồng với độ chính xác cao.", true, "https://w7zbytrd10.ufs.sh/f/fnkloM7shIMiw5ctErqi875G0OqPFCXHSnsbpNYd9g6WhuzA", new DateTime(2025, 7, 3, 23, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840), "Viện Bảo vệ thực vật", "[\"AI\", \"Công nghệ\", \"Chẩn đoán bệnh\"]", "Ứng dụng AI trong chẩn đoán bệnh cây trồng", new DateTime(2025, 7, 3, 23, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840), 1900 });

            migrationBuilder.InsertData(
                table: "News",
                columns: new[] { "Id", "Author", "CategoryId", "Content", "CreatedAt", "Excerpt", "ImageUrl", "PublishedAt", "Source", "Tags", "Title", "UpdatedAt", "ViewCount" },
                values: new object[] { 4, "Ban Tổ chức", 4, "<h2>Hội nghị quốc tế về nông nghiệp bền vững sẽ diễn ra tại Hà Nội</h2> <p>Một sự kiện quan trọng trong lĩnh vực nông nghiệp – Hội nghị quốc tế về nông nghiệp bền vững – sẽ được tổ chức tại Hà Nội vào tháng 10 năm 2024. Sự kiện dự kiến quy tụ hàng trăm chuyên gia, nhà khoa học và doanh nghiệp trong và ngoài nước cùng thảo luận về giải pháp phát triển nông nghiệp xanh, thân thiện với môi trường.</p> <h3>Mục tiêu của hội nghị</h3> <p>Hội nghị hướng đến các mục tiêu then chốt nhằm thúc đẩy phát triển nông nghiệp bền vững:</p> <ul> <li>Chia sẻ kinh nghiệm, công nghệ và mô hình sản xuất nông nghiệp hiệu quả</li> <li>Thúc đẩy hợp tác quốc tế về đổi mới sáng tạo trong nông nghiệp</li> <li>Kết nối nhà nông, nhà khoa học và doanh nghiệp trong chuỗi giá trị nông sản</li> <li>Đề xuất chính sách hỗ trợ phát triển nông nghiệp thích ứng với biến đổi khí hậu</li> </ul> <h3>Nội dung và hoạt động nổi bật</h3> <p>Trong khuôn khổ hội nghị sẽ diễn ra nhiều hoạt động thiết thực:</p> <ul> <li>Hội thảo chuyên đề về nông nghiệp công nghệ cao, hữu cơ và tuần hoàn</li> <li>Triển lãm sản phẩm, thiết bị và công nghệ nông nghiệp hiện đại</li> <li>Ký kết biên bản ghi nhớ hợp tác giữa các tổ chức trong và ngoài nước</li> <li>Tham quan các mô hình sản xuất bền vững tại vùng ngoại thành Hà Nội</li> </ul> <h3>Kết luận</h3> <p>Hội nghị quốc tế về nông nghiệp bền vững là cơ hội để Việt Nam học hỏi kinh nghiệm quốc tế và thể hiện vai trò trong quá trình chuyển đổi nông nghiệp xanh. Bà con, hợp tác xã và doanh nghiệp nông nghiệp nên quan tâm theo dõi và tham gia để tiếp cận các giải pháp tiến bộ phục vụ sản xuất hiệu quả và lâu dài.</p>", new DateTime(2025, 7, 4, 5, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840), "Hội nghị quốc tế về nông nghiệp bền vững và an ninh lương thực sẽ được tổ chức tại Hà Nội từ ngày 15-17/2/2024.", "https://w7zbytrd10.ufs.sh/f/fnkloM7shIMiAu5Tm4BJvhs5EtjkY9RlbFo4QLXHyT6gUfm0", new DateTime(2025, 7, 4, 5, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840), "Bộ Nông nghiệp và Phát triển Nông thôn", "[\"Hội nghị\", \"Quốc tế\", \"Bền vững\", \"An ninh lương thực\"]", "Hội nghị quốc tế về nông nghiệp bền vững sẽ diễn ra tại Hà Nội", new DateTime(2025, 7, 4, 5, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840), 1200 });

            migrationBuilder.InsertData(
                table: "News",
                columns: new[] { "Id", "Author", "CategoryId", "Content", "CreatedAt", "Excerpt", "ImageUrl", "PublishedAt", "Source", "Tags", "Title", "UpdatedAt", "Urgent", "ViewCount" },
                values: new object[] { 5, "Trung tâm Dự báo khí tượng thủy văn", 5, "<h2>Cảnh báo thời tiết bất lợi cho vụ lúa Đông Xuân</h2> <p>Trung tâm Dự báo Khí tượng Thủy văn quốc gia vừa đưa ra cảnh báo về một đợt không khí lạnh mạnh tràn về trong những ngày tới. Đợt rét đậm này được dự báo sẽ ảnh hưởng trực tiếp đến các tỉnh miền Bắc và Bắc Trung Bộ, đặc biệt trong giai đoạn gieo cấy và chăm sóc lúa vụ Đông Xuân.</p> <h3>Ảnh hưởng đến sản xuất nông nghiệp</h3> <p>Thời tiết giá rét kéo dài có thể gây ảnh hưởng nghiêm trọng đến cây lúa:</p> <ul> <li>Làm chậm quá trình sinh trưởng của mạ và lúa non</li> <li>Hạn chế khả năng đẻ nhánh và phát triển đồng đều</li> <li>Gia tăng nguy cơ cây lúa bị chết rét nếu không được bảo vệ</li> <li>Gây khó khăn trong công tác làm đất và gieo cấy</li> </ul> <h3>Khuyến cáo cho bà con nông dân</h3> <p>Để giảm thiểu thiệt hại do thời tiết gây ra, bà con nên chủ động:</p> <ul> <li>Che phủ nilon cho mạ để giữ ấm và tránh sương muối</li> <li>Giữ mực nước ổn định trên ruộng để tránh rét cho rễ lúa</li> <li>Điều chỉnh lịch gieo cấy phù hợp theo khuyến cáo của địa phương</li> <li>Theo dõi sát các bản tin thời tiết để kịp thời ứng phó</li> </ul> <h3>Kết luận</h3> <p>Việc chủ động phòng tránh và ứng phó với thời tiết bất lợi là yếu tố then chốt để bảo vệ vụ mùa Đông Xuân. Trung tâm Dự báo Khí tượng Thủy văn sẽ tiếp tục cập nhật thông tin để hỗ trợ kịp thời cho bà con trong sản xuất nông nghiệp.</p>", new DateTime(2025, 7, 4, 8, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840), "Trung tâm Dự báo khí tượng thủy văn cảnh báo đợt rét đậm có thể ảnh hưởng đến vụ lúa Đông Xuân.", "https://w7zbytrd10.ufs.sh/f/fnkloM7shIMif1eAaj7shIMig9TJ5BHNmxLRtAlZ6YUEnOjo", new DateTime(2025, 7, 4, 8, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840), "Trung tâm Dự báo khí tượng thủy văn", "[\"Thời tiết\", \"Cảnh báo\", \"Lúa Đông Xuân\"]", "Cảnh báo thời tiết bất lợi cho vụ lúa Đông Xuân", new DateTime(2025, 7, 4, 8, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840), true, 4200 });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "Id", "CreatedAt", "OrderNumber", "PaidAt", "PaymentMethod", "PaymentStatus", "ShippingAddress", "Status", "TotalAmount", "UpdatedAt", "UserId" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 6, 14, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840), "ORD-2024-001", new DateTime(2025, 6, 19, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840), "cod", "paid", "Ấp 1, Xã Tân Phú, Huyện Châu Thành, Tỉnh An Giang", "delivered", 189000m, new DateTime(2025, 6, 19, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840), 5 },
                    { 2, new DateTime(2025, 6, 24, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840), "ORD-2024-002", new DateTime(2025, 6, 26, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840), "bank_transfer", "paid", "Ấp 2, Xã Long Phú, Huyện Phú Tân, Tỉnh An Giang", "shipped", 3450000m, new DateTime(2025, 7, 2, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840), 6 },
                    { 3, new DateTime(2025, 7, 1, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840), "ORD-2024-003", null, "cod", "pending", "Ấp 3, Xã Vĩnh Hậu, Huyện Tân Hưng, Tỉnh Long An", "processing", 160000m, new DateTime(2025, 7, 3, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840), 7 },
                    { 4, new DateTime(2025, 7, 3, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840), "ORD-2024-004", null, "cod", "pending", "Ấp 4, Xã Đức Hòa, Huyện Đức Hòa, Tỉnh Long An", "pending", 95000m, new DateTime(2025, 7, 3, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840), 8 }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "CreatedAt", "Description", "DiscountPrice", "ImageUrl", "IsActive", "Name", "Price", "Rating", "SKU", "Stock", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2025, 7, 4, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840), "Giống lúa ST25 chất lượng cao, năng suất ổn định, kháng bệnh tốt. Thời gian sinh trưởng 95-100 ngày.", 42000m, "https://w7zbytrd10.ufs.sh/f/fnkloM7shIMikAYBBqdMhyIJur9wTGngj3U7esvR5SDPNaBf", true, "Hạt giống lúa ST25", 45000m, 0.0, "HG-ST25-001", 500, new DateTime(2025, 7, 4, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840) },
                    { 2, 1, new DateTime(2025, 7, 4, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840), "Hạt giống rau cải xanh F1, tỷ lệ nảy mầm cao, sinh trưởng nhanh, chống chịu tốt.", null, "https://w7zbytrd10.ufs.sh/f/fnkloM7shIMi7FiwoUmKzDYxH2eZFfa8LUyRitIc60bGAChn", true, "Hạt giống rau cải xanh", 25000m, 0.0, "HG-CX-002", 200, new DateTime(2025, 7, 4, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840) },
                    { 3, 1, new DateTime(2025, 7, 4, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840), "Giống cà chua F1 năng suất cao, quả to, màu đỏ đẹp, thích hợp trồng quanh năm.", 32000m, "https://w7zbytrd10.ufs.sh/f/fnkloM7shIMig5SEVijNoTLOf1Hx8v7bwqdVm2u0zcaKMXAj", true, "Hạt giống cà chua F1", 35000m, 0.0, "HG-CC-003", 150, new DateTime(2025, 7, 4, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840) },
                    { 4, 2, new DateTime(2025, 7, 4, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840), "Phân NPK 16-16-8 chuyên dụng cho cây lúa, cung cấp đầy đủ dinh dưỡng cho cây trồng.", null, "https://w7zbytrd10.ufs.sh/f/fnkloM7shIMiY9g3ootpCmfro6zTANiU9p8hjKOLsk7wDntP", true, "Phân NPK 16-16-8", 18000m, 0.0, "PB-NPK-004", 1000, new DateTime(2025, 7, 4, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840) },
                    { 5, 2, new DateTime(2025, 7, 4, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840), "Phân hữu cơ vi sinh giúp cải tạo đất, tăng cường sức đề kháng cho cây trồng.", 20000m, "https://w7zbytrd10.ufs.sh/f/fnkloM7shIMiOkToSwbxkV0rWJbLqG68eNRt4co2zhjKsm9S", true, "Phân hữu cơ vi sinh", 22000m, 0.0, "PB-HCVS-005", 800, new DateTime(2025, 7, 4, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840) },
                    { 6, 3, new DateTime(2025, 7, 4, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840), "Thuốc trừ sâu Regent 50SC hiệu quả cao, an toàn cho người và môi trường.", null, "https://w7zbytrd10.ufs.sh/f/fnkloM7shIMidS0AFEO3AqDOcx1KzTwFvNSjELyC04fnd8Z6", true, "Thuốc trừ sâu Regent 50SC", 85000m, 0.0, "BVTV-REG-006", 300, new DateTime(2025, 7, 4, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840) },
                    { 7, 3, new DateTime(2025, 7, 4, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840), "Thuốc diệt cỏ Gramoxone tác dụng nhanh, hiệu quả cao với nhiều loại cỏ dại.", null, "https://w7zbytrd10.ufs.sh/f/fnkloM7shIMieJbxxBkRhUuAPvxwgZQ8qnB6MjCy4GDiFcm2", true, "Thuốc diệt cỏ Gramoxone", 95000m, 0.0, "BVTV-GRA-007", 250, new DateTime(2025, 7, 4, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840) },
                    { 8, 4, new DateTime(2025, 7, 4, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840), "Máy phun thuốc bình xịt dung tích 16L, áp suất cao, phun đều, tiết kiệm thuốc.", 420000m, "https://w7zbytrd10.ufs.sh/f/fnkloM7shIMiwIHKwKyqi875G0OqPFCXHSnsbpNYd9g6Whuz", true, "Máy phun thuốc bình xịt 16L", 450000m, 0.0, "DC-MPT-008", 50, new DateTime(2025, 7, 4, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840) },
                    { 9, 4, new DateTime(2025, 7, 4, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840), "Cuốc xới đất cán gỗ chất lượng cao, bền bỉ, phù hợp cho mọi loại đất.", null, "https://w7zbytrd10.ufs.sh/f/fnkloM7shIMi32ePH4OidIJn67yYQzmPNuqjLaUeT9KvWgG0", true, "Cuốc xới đất cán gỗ", 75000m, 0.0, "DC-CXD-009", 100, new DateTime(2025, 7, 4, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840) },
                    { 10, 5, new DateTime(2025, 7, 4, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840), "Máy cắt cỏ Honda GX35 công suất mạnh, tiết kiệm nhiên liệu, độ bền cao.", 3300000m, "https://w7zbytrd10.ufs.sh/f/fnkloM7shIMiAlClfeJvhs5EtjkY9RlbFo4QLXHyT6gUfm0p", true, "Máy cắt cỏ Honda GX35", 3500000m, 0.0, "MM-MCC-010", 20, new DateTime(2025, 7, 4, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840) }
                });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "Id", "CreatedAt", "RoleId", "UserId" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 7, 4, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840), 1, 1 },
                    { 2, new DateTime(2025, 7, 4, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840), 2, 2 },
                    { 3, new DateTime(2025, 7, 4, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840), 2, 3 },
                    { 4, new DateTime(2025, 7, 4, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840), 2, 4 },
                    { 5, new DateTime(2025, 7, 4, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840), 3, 5 },
                    { 6, new DateTime(2025, 7, 4, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840), 3, 6 },
                    { 7, new DateTime(2025, 7, 4, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840), 3, 7 },
                    { 8, new DateTime(2025, 7, 4, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840), 3, 8 },
                    { 9, new DateTime(2025, 7, 4, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840), 3, 9 }
                });

            migrationBuilder.InsertData(
                table: "CartItems",
                columns: new[] { "Id", "CartId", "CreatedAt", "ProductId", "Quantity", "TotalPrice", "UnitPrice", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2025, 7, 2, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840), 1, 2, 84000m, 42000m, new DateTime(2025, 7, 2, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840) },
                    { 2, 1, new DateTime(2025, 7, 4, 5, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840), 2, 1, 25000m, 25000m, new DateTime(2025, 7, 4, 5, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840) },
                    { 3, 1, new DateTime(2025, 7, 4, 5, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840), 4, 1, 18000m, 18000m, new DateTime(2025, 7, 4, 5, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840) },
                    { 4, 2, new DateTime(2025, 7, 3, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840), 8, 1, 420000m, 420000m, new DateTime(2025, 7, 3, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840) },
                    { 5, 2, new DateTime(2025, 7, 4, 8, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840), 4, 5, 90000m, 18000m, new DateTime(2025, 7, 4, 8, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840) },
                    { 6, 2, new DateTime(2025, 7, 4, 8, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840), 9, 2, 150000m, 75000m, new DateTime(2025, 7, 4, 8, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840) },
                    { 7, 3, new DateTime(2025, 7, 3, 23, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840), 6, 1, 85000m, 85000m, new DateTime(2025, 7, 3, 23, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840) }
                });

            migrationBuilder.InsertData(
                table: "EngineerFarmerAssignments",
                columns: new[] { "Id", "AssignedAt", "EngineerId", "FarmerId", "IsActive", "Notes" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 7, 4, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840), 1, 1, true, "Hỗ trợ chẩn đoán bệnh lúa" },
                    { 2, new DateTime(2025, 7, 4, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840), 1, 2, true, "Tư vấn phòng trị bệnh hại" },
                    { 3, new DateTime(2025, 7, 4, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840), 2, 3, true, "Tư vấn dinh dưỡng cây trồng" },
                    { 4, new DateTime(2025, 7, 4, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840), 2, 4, true, "Hướng dẫn bón phân" },
                    { 5, new DateTime(2025, 7, 4, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840), 3, 5, true, "Tư vấn kỹ thuật canh tác hữu cơ" }
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
                    { 1, new DateTime(2025, 6, 14, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840), 1, "Hạt giống chất lượng tuyệt vời! Tỷ lệ nảy mầm cao, cây lúa sinh trưởng khỏe mạnh. Năng suất đạt như quảng cáo. Sẽ tiếp tục mua ở lần sau.", 5, new DateTime(2025, 6, 14, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840), 5, "Nguyễn Thị D" },
                    { 2, new DateTime(2025, 6, 16, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840), 1, "Giống lúa tốt, năng suất ổn định. Chỉ có điều giá hơi cao so với các giống khác. Nhưng chất lượng xứng đáng với giá tiền.", 4, new DateTime(2025, 6, 16, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840), 6, "Trần Văn E" },
                    { 3, new DateTime(2025, 6, 19, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840), 4, "Phân bón hiệu quả tốt, cây trồng xanh tốt sau khi bón. Giá cả hợp lý, giao hàng nhanh chóng.", 5, new DateTime(2025, 6, 19, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840), 7, "Lê Thị F" },
                    { 4, new DateTime(2025, 6, 22, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840), 8, "Máy phun hoạt động tốt, áp suất ổn định. Dung tích 16L vừa phải cho diện tích nhỏ. Chỉ có điều hơi nặng khi mang lâu.", 4, new DateTime(2025, 6, 22, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840), 8, "Phạm Văn G" },
                    { 5, new DateTime(2025, 6, 24, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840), 5, "Phân hữu cơ rất tốt! Đất trở nên tơi xốp hơn, cây trồng khỏe mạnh. Đặc biệt hiệu quả với rau màu. Giá có khuyến mãi nữa, rất hài lòng!", 5, new DateTime(2025, 6, 24, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840), 9, "Hoàng Thị H" },
                    { 6, new DateTime(2025, 6, 26, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840), 6, "Thuốc trừ sâu hiệu quả, sâu chết nhanh sau khi phun. Tuy nhiên cần chú ý an toàn khi sử dụng.", 4, new DateTime(2025, 6, 26, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840), 5, "Nguyễn Thị D" },
                    { 7, new DateTime(2025, 6, 29, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840), 10, "Máy cắt cỏ Honda chất lượng xuất sắc! Máy chạy êm, cắt sạch, tiết kiệm xăng. Đáng đồng tiền bát gạo. Khuyên mọi người nên mua.", 5, new DateTime(2025, 6, 29, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840), 6, "Trần Văn E" }
                });

            migrationBuilder.InsertData(
                table: "Tickets",
                columns: new[] { "Id", "AssignedEngineerId", "Category", "ContactMethod", "CreatedAt", "CropType", "Description", "FarmerId", "ImageUrl", "Location", "PhoneNumber", "Priority", "ResolvedAt", "Status", "Title", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, 1, "Bệnh cây trồng", "Điện thoại", new DateTime(2025, 6, 29, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840), "Lúa", "Lúa của tôi đang trong giai đoạn đẻ nhánh nhưng bị vàng lá từ dưới lên, một số cây đã chết khô. Tôi đã tưới nước đầy đủ nhưng tình trạng không cải thiện.", 1, "default.jpg", "Ruộng A1, Ấp 1, Xã Tân Phú", "0905678901", "high", null, "in_progress", "Lúa bị vàng lá và chết khô", new DateTime(2025, 7, 2, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840) },
                    { 2, 2, "Dinh dưỡng cây trồng", "Email", new DateTime(2025, 7, 1, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840), "Lúa", "Tôi chuẩn bị gieo sạ vụ lúa mới, muốn được tư vấn về lượng phân bón cần thiết cho 3.2 hecta đất.", 2, "default.jpg", "Ruộng B2, Ấp 2, Xã Long Phú", "0906789012", "medium", null, "assigned", "Tư vấn lượng phân bón cho vụ lúa mới", new DateTime(2025, 7, 1, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840) },
                    { 3, null, "Sâu bệnh", "Điện thoại", new DateTime(2025, 7, 3, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840), "Rau màu", "Rau cải của tôi bị sâu ăn lá nghiêm trọng, lá bị thủng lỗ chỗ. Cần tư vấn thuốc trừ sâu phù hợp.", 3, "default.jpg", "Vườn C1, Ấp 3, Xã Vĩnh Hậu", "0907890123", "urgent", null, "open", "Rau cải bị sâu ăn lá", new DateTime(2025, 7, 3, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840) },
                    { 4, 3, "Kỹ thuật canh tác", "Email", new DateTime(2025, 6, 24, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840), "Cây ăn trái", "Tôi muốn chuyển đổi sang mô hình trồng trọt hữu cơ cho vườn cây ăn trái. Cần được hướng dẫn quy trình và kỹ thuật.", 4, "default.jpg", "Vườn D1, Ấp 4, Xã Đức Hòa", "0908901234", "low", new DateTime(2025, 7, 3, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840), "resolved", "Hướng dẫn kỹ thuật trồng hữu cơ", new DateTime(2025, 7, 3, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840) },
                    { 5, null, "Đất đai", "Điện thoại", new DateTime(2025, 7, 3, 23, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840), "Rau màu", "Đất trồng rau của tôi có vẻ bị chua, cây trồng sinh trưởng chậm, lá vàng. Cần tư vấn cách cải tạo đất.", 5, "default.jpg", "Ruộng E1, Ấp 5, Xã Tân Trụ", "0909012345", "medium", null, "open", "Đất bị chua, cây trồng sinh trưởng kém", new DateTime(2025, 7, 3, 23, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840) }
                });

            migrationBuilder.InsertData(
                table: "TicketComments",
                columns: new[] { "Id", "Comment", "CreatedAt", "IsInternal", "TicketId", "UserId" },
                values: new object[,]
                {
                    { 1, "Dựa vào mô tả và hình ảnh, có thể cây lúa của anh bị bệnh khô vằn. Tôi sẽ đến khảo sát thực địa vào chiều mai.", new DateTime(2025, 6, 30, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840), false, 1, 2 },
                    { 2, "Cảm ơn kỹ sư. Tôi sẽ chờ anh đến khảo sát. Hiện tại tình trạng vẫn đang lan rộng.", new DateTime(2025, 6, 30, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840), false, 1, 5 },
                    { 3, "Đã khảo sát thực địa. Xác định là bệnh khô vằn do nấm. Đã hướng dẫn anh sử dụng thuốc Validamycin 3% với liều lượng 1.5L/ha.", new DateTime(2025, 7, 2, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840), false, 1, 2 },
                    { 4, "Với diện tích 3.2ha lúa, anh nên sử dụng: Phân lót 200kg NPK 16-16-8, phân thúc lần 1: 100kg Urea, phân thúc lần 2: 80kg NPK 20-20-15.", new DateTime(2025, 7, 2, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840), false, 2, 3 },
                    { 5, "Đã hoàn thành hướng dẫn chuyển đổi hữu cơ cho anh. Gửi kèm tài liệu quy trình chi tiết qua email.", new DateTime(2025, 7, 3, 11, 53, 26, 472, DateTimeKind.Utc).AddTicks(1840), false, 4, 4 }
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
