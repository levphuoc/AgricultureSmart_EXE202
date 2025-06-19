using AgricultureSmart.Repositories.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgricultureSmart.Repositories.DbAgriContext
{
    public class AgricultureSmartDbContext : DbContext
    {
        public AgricultureSmartDbContext(DbContextOptions<AgricultureSmartDbContext> options)
            : base(options)
        {
            /*Database.Migrate();*/
        }

        // User Management DbSets
        public DbSet<Users> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Engineer> Engineers { get; set; }
        public DbSet<Farmer> Farmers { get; set; }
        public DbSet<EngineerFarmerAssignment> EngineerFarmerAssignments { get; set; }

        // Blog System DbSets
        public DbSet<BlogCategory> BlogCategories { get; set; }
        public DbSet<Blog> Blogs { get; set; }

        // News System DbSets
        public DbSet<NewsCategory> NewsCategories { get; set; }
        public DbSet<News> News { get; set; }

        // Ticket System DbSets
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<TicketComment> TicketComments { get; set; }

        // E-commerce DbSets
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure table names
            modelBuilder.Entity<Users>().ToTable("Users");
            modelBuilder.Entity<Role>().ToTable("Roles");
            modelBuilder.Entity<UserRole>().ToTable("UserRoles");
            modelBuilder.Entity<Engineer>().ToTable("Engineers");
            modelBuilder.Entity<Farmer>().ToTable("Farmers");
            modelBuilder.Entity<EngineerFarmerAssignment>().ToTable("EngineerFarmerAssignments");
            modelBuilder.Entity<BlogCategory>().ToTable("BlogCategories");
            modelBuilder.Entity<Blog>().ToTable("Blogs");
            modelBuilder.Entity<NewsCategory>().ToTable("NewsCategories");
            modelBuilder.Entity<News>().ToTable("News");
            modelBuilder.Entity<Ticket>().ToTable("Tickets");
            modelBuilder.Entity<TicketComment>().ToTable("TicketComments");
            modelBuilder.Entity<ProductCategory>().ToTable("ProductCategories");
            modelBuilder.Entity<Product>().ToTable("Products");
            modelBuilder.Entity<Review>().ToTable("Reviews");
            modelBuilder.Entity<Cart>().ToTable("Carts");
            modelBuilder.Entity<CartItem>().ToTable("CartItems");
            modelBuilder.Entity<Order>().ToTable("Orders");
            modelBuilder.Entity<OrderItem>().ToTable("OrderItems");

            // Configure unique constraints
            modelBuilder.Entity<Users>()
                .HasIndex(u => u.Email)
                .IsUnique()
                .HasDatabaseName("IX_Users_Email");

            modelBuilder.Entity<Users>()
                .HasIndex(u => u.PhoneNumber)
                .IsUnique()
                .HasDatabaseName("IX_Users_PhoneNumber");

            modelBuilder.Entity<Users>()
                .HasIndex(u => u.UserName)
                .IsUnique()
                .HasDatabaseName("IX_Users_UserName");

            modelBuilder.Entity<Role>()
                .HasIndex(r => r.Name)
                .IsUnique()
                .HasDatabaseName("IX_Roles_Name");

            modelBuilder.Entity<BlogCategory>()
                .HasIndex(bc => bc.Name)
                .IsUnique()
                .HasDatabaseName("IX_BlogCategories_Name");

            modelBuilder.Entity<BlogCategory>()
                .HasIndex(bc => bc.Slug)
                .IsUnique()
                .HasDatabaseName("IX_BlogCategories_Slug");

            modelBuilder.Entity<Blog>()
                .HasIndex(b => b.Slug)
                .IsUnique()
                .HasDatabaseName("IX_Blogs_Slug");

            modelBuilder.Entity<NewsCategory>()
                .HasIndex(nc => nc.Name)
                .IsUnique()
                .HasDatabaseName("IX_NewsCategories_Name");

            modelBuilder.Entity<NewsCategory>()
                .HasIndex(nc => nc.Slug)
                .IsUnique()
                .HasDatabaseName("IX_NewsCategories_Slug");

            modelBuilder.Entity<ProductCategory>()
                .HasIndex(pc => pc.Name)
                .IsUnique()
                .HasDatabaseName("IX_ProductCategories_Name");

            modelBuilder.Entity<ProductCategory>()
                .HasIndex(pc => pc.Slug)
                .IsUnique()
                .HasDatabaseName("IX_ProductCategories_Slug");

            modelBuilder.Entity<Product>()
                .HasIndex(p => p.SKU)
                .IsUnique()
                .HasDatabaseName("IX_Products_SKU");

            modelBuilder.Entity<Order>()
                .HasIndex(o => o.OrderNumber)
                .IsUnique()
                .HasDatabaseName("IX_Orders_OrderNumber");

            // Configure composite unique constraint for active assignments
            modelBuilder.Entity<EngineerFarmerAssignment>()
                .HasIndex(efa => new { efa.EngineerId, efa.FarmerId, efa.IsActive })
                .IsUnique()
                .HasDatabaseName("IX_EngineerFarmerAssignments_Unique_Active")
                .HasFilter("[IsActive] = 1");

            // Configure relationships
            ConfigureUserRelationships(modelBuilder);
            ConfigureBlogRelationships(modelBuilder);
            ConfigureNewsRelationships(modelBuilder);
            ConfigureTicketRelationships(modelBuilder);
            ConfigureEcommerceRelationships(modelBuilder);

            // Configure default values
            ConfigureDefaultValues(modelBuilder);

            // Seed initial data
            SeedData(modelBuilder);
        }

        private void ConfigureUserRelationships(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserRole>()
                .HasOne(ur => ur.User)
                .WithMany(u => u.UserRoles)
                .HasForeignKey(ur => ur.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<UserRole>()
                .HasOne(ur => ur.Role)
                .WithMany(r => r.UserRoles)
                .HasForeignKey(ur => ur.RoleId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Engineer>()
                .HasOne(e => e.User)
                .WithOne(u => u.Engineer)
                .HasForeignKey<Engineer>(e => e.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Farmer>()
                .HasOne(f => f.User)
                .WithOne(u => u.Farmer)
                .HasForeignKey<Farmer>(f => f.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<EngineerFarmerAssignment>()
                .HasOne(efa => efa.Engineer)
                .WithMany(e => e.EngineerFarmerAssignments)
                .HasForeignKey(efa => efa.EngineerId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<EngineerFarmerAssignment>()
                .HasOne(efa => efa.Farmer)
                .WithMany(f => f.EngineerFarmerAssignments)
                .HasForeignKey(efa => efa.FarmerId)
                .OnDelete(DeleteBehavior.Restrict);
        }

        private void ConfigureBlogRelationships(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Blog>()
                .HasOne(b => b.Author)
                .WithMany(u => u.Blogs)
                .HasForeignKey(b => b.AuthorId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Blog>()
                .HasOne(b => b.Category)
                .WithMany(bc => bc.Blogs)
                .HasForeignKey(b => b.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);
        }

        private void ConfigureNewsRelationships(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<News>()
                .HasOne(n => n.Category)
                .WithMany(nc => nc.News)
                .HasForeignKey(n => n.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);
        }

        private void ConfigureTicketRelationships(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Ticket>()
                .HasOne(t => t.Farmer)
                .WithMany(f => f.Tickets)
                .HasForeignKey(t => t.FarmerId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Ticket>()
                .HasOne(t => t.AssignedEngineer)
                .WithMany(e => e.AssignedTickets)
                .HasForeignKey(t => t.AssignedEngineerId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<TicketComment>()
                .HasOne(tc => tc.Ticket)
                .WithMany(t => t.Comments)
                .HasForeignKey(tc => tc.TicketId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<TicketComment>()
                .HasOne(tc => tc.User)
                .WithMany(u => u.TicketComments)
                .HasForeignKey(tc => tc.UserId)
                .OnDelete(DeleteBehavior.Restrict);
        }

        private void ConfigureEcommerceRelationships(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                .HasOne(p => p.Category)
                .WithMany(pc => pc.Products)
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Review>()
                .HasOne(r => r.Product)
                .WithMany(p => p.Reviews)
                .HasForeignKey(r => r.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Review>()
                .HasOne(r => r.User)
                .WithMany(u => u.Reviews)
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Cart>()
                .HasOne(c => c.User)
                .WithOne(u => u.Cart)
                .HasForeignKey<Cart>(c => c.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<CartItem>()
                .HasOne(ci => ci.Cart)
                .WithMany(c => c.CartItems)
                .HasForeignKey(ci => ci.CartId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<CartItem>()
                .HasOne(ci => ci.Product)
                .WithMany(p => p.CartItems)
                .HasForeignKey(ci => ci.ProductId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Order>()
                .HasOne(o => o.User)
                .WithMany(u => u.Orders)
                .HasForeignKey(o => o.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.Order)
                .WithMany(o => o.OrderItems)
                .HasForeignKey(oi => oi.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.Product)
                .WithMany(p => p.OrderItems)
                .HasForeignKey(oi => oi.ProductId)
                .OnDelete(DeleteBehavior.Restrict);
        }

        private void ConfigureDefaultValues(ModelBuilder modelBuilder)
        {
            // Users defaults
            modelBuilder.Entity<Users>()
                .Property(u => u.IsActive)
                .HasDefaultValue(true);

            modelBuilder.Entity<Users>()
                .Property(u => u.CreatedAt)
                .HasDefaultValueSql("GETUTCDATE()");

            modelBuilder.Entity<Users>()
                .Property(u => u.UpdatedAt)
                .HasDefaultValueSql("GETUTCDATE()");

            // Blog defaults
            modelBuilder.Entity<BlogCategory>()
                .Property(bc => bc.IsActive)
                .HasDefaultValue(true);

            modelBuilder.Entity<Blog>()
                .Property(b => b.Status)
                .HasDefaultValue("draft");

            modelBuilder.Entity<Blog>()
                .Property(b => b.ViewCount)
                .HasDefaultValue(0);

            // News defaults
            modelBuilder.Entity<NewsCategory>()
                .Property(nc => nc.IsActive)
                .HasDefaultValue(true);

            modelBuilder.Entity<NewsCategory>()
                .Property(nc => nc.CreatedAt)
                .HasDefaultValueSql("GETUTCDATE()");

            modelBuilder.Entity<News>()
                .Property(n => n.Featured)
                .HasDefaultValue(false);

            modelBuilder.Entity<News>()
                .Property(n => n.Urgent)
                .HasDefaultValue(false);

            modelBuilder.Entity<News>()
                .Property(n => n.ViewCount)
                .HasDefaultValue(0);

            modelBuilder.Entity<News>()
                .Property(n => n.CreatedAt)
                .HasDefaultValueSql("GETUTCDATE()");

            modelBuilder.Entity<News>()
                .Property(n => n.UpdatedAt)
                .HasDefaultValueSql("GETUTCDATE()");

            // Product defaults
            modelBuilder.Entity<ProductCategory>()
                .Property(pc => pc.IsActive)
                .HasDefaultValue(true);

            modelBuilder.Entity<ProductCategory>()
                .Property(pc => pc.CreatedAt)
                .HasDefaultValueSql("GETUTCDATE()");

            modelBuilder.Entity<Product>()
                .Property(p => p.IsActive)
                .HasDefaultValue(true);

            modelBuilder.Entity<Product>()
                .Property(p => p.Stock)
                .HasDefaultValue(0);

            modelBuilder.Entity<Product>()
                .Property(p => p.CreatedAt)
                .HasDefaultValueSql("GETUTCDATE()");

            modelBuilder.Entity<Product>()
                .Property(p => p.UpdatedAt)
                .HasDefaultValueSql("GETUTCDATE()");

            // Review defaults
            modelBuilder.Entity<Review>()
                .Property(r => r.CreatedAt)
                .HasDefaultValueSql("GETUTCDATE()");

            modelBuilder.Entity<Review>()
                .Property(r => r.UpdatedAt)
                .HasDefaultValueSql("GETUTCDATE()");

            // Other defaults
            modelBuilder.Entity<EngineerFarmerAssignment>()
                .Property(efa => efa.IsActive)
                .HasDefaultValue(true);

            modelBuilder.Entity<Ticket>()
                .Property(t => t.Status)
                .HasDefaultValue("open");

            modelBuilder.Entity<Ticket>()
                .Property(t => t.Priority)
                .HasDefaultValue("medium");

            modelBuilder.Entity<Cart>()
                .Property(c => c.TotalAmount)
                .HasDefaultValue(0);

            modelBuilder.Entity<Cart>()
                .Property(c => c.CreatedAt)
                .HasDefaultValueSql("GETUTCDATE()");

            modelBuilder.Entity<Cart>()
                .Property(c => c.UpdatedAt)
                .HasDefaultValueSql("GETUTCDATE()");

            modelBuilder.Entity<CartItem>()
                .Property(ci => ci.CreatedAt)
                .HasDefaultValueSql("GETUTCDATE()");

            modelBuilder.Entity<CartItem>()
                .Property(ci => ci.UpdatedAt)
                .HasDefaultValueSql("GETUTCDATE()");

            modelBuilder.Entity<Order>()
                .Property(o => o.Status)
                .HasDefaultValue("pending");

            modelBuilder.Entity<Order>()
                .Property(o => o.PaymentStatus)
                .HasDefaultValue("pending");

            modelBuilder.Entity<Order>()
                .Property(o => o.CreatedAt)
                .HasDefaultValueSql("GETUTCDATE()");

            modelBuilder.Entity<Order>()
                .Property(o => o.UpdatedAt)
                .HasDefaultValueSql("GETUTCDATE()");
        }

        private void SeedData(ModelBuilder modelBuilder)
        {
            var now = DateTime.UtcNow;

            // Seed Roles
            modelBuilder.Entity<Role>().HasData(
                new Role { Id = 1, Name = "Admin", Description = "System Administrator", CreatedAt = now },
                new Role { Id = 2, Name = "Engineer", Description = "Agricultural Engineer", CreatedAt = now },
                new Role { Id = 3, Name = "Farmer", Description = "Farmer User", CreatedAt = now }
            );

            // Seed Users
            modelBuilder.Entity<Users>().HasData(
                // Admin user
                new Users
                {
                    Id = 1,
                    UserName = "admin",
                    Password = "admin123", // Should be hashed in production
                    Email = "admin@agricultural.com",
                    Address = "123 Đường Cách Mạng Tháng 8, Quận 1, TP.HCM",
                    PhoneNumber = "0901234567",
                    IsActive = true,
                    CreatedAt = now,
                    UpdatedAt = now
                },
                // Engineer users
                new Users
                {
                    Id = 2,
                    UserName = "engineer1",
                    Password = "engineer123",
                    Email = "nguyenvana@agricultural.com",
                    Address = "456 Đường Lê Lợi, Quận 3, TP.HCM",
                    PhoneNumber = "0902345678",
                    IsActive = true,
                    CreatedAt = now,
                    UpdatedAt = now
                },
                new Users
                {
                    Id = 3,
                    UserName = "engineer2",
                    Password = "engineer123",
                    Email = "tranthib@agricultural.com",
                    Address = "789 Đường Nguyễn Huệ, Quận 1, TP.HCM",
                    PhoneNumber = "0903456789",
                    IsActive = true,
                    CreatedAt = now,
                    UpdatedAt = now
                },
                new Users
                {
                    Id = 4,
                    UserName = "engineer3",
                    Password = "engineer123",
                    Email = "levanc@agricultural.com",
                    Address = "321 Đường Pasteur, Quận 3, TP.HCM",
                    PhoneNumber = "0904567890",
                    IsActive = true,
                    CreatedAt = now,
                    UpdatedAt = now
                },
                // Farmer users
                new Users
                {
                    Id = 5,
                    UserName = "farmer1",
                    Password = "farmer123",
                    Email = "nguyenthid@gmail.com",
                    Address = "Ấp 1, Xã Tân Phú, Huyện Châu Thành, Tỉnh An Giang",
                    PhoneNumber = "0905678901",
                    IsActive = true,
                    CreatedAt = now,
                    UpdatedAt = now
                },
                new Users
                {
                    Id = 6,
                    UserName = "farmer2",
                    Password = "farmer123",
                    Email = "tranvane@gmail.com",
                    Address = "Ấp 2, Xã Long Phú, Huyện Phú Tân, Tỉnh An Giang",
                    PhoneNumber = "0906789012",
                    IsActive = true,
                    CreatedAt = now,
                    UpdatedAt = now
                },
                new Users
                {
                    Id = 7,
                    UserName = "farmer3",
                    Password = "farmer123",
                    Email = "lethif@gmail.com",
                    Address = "Ấp 3, Xã Vĩnh Hậu, Huyện Tân Hưng, Tỉnh Long An",
                    PhoneNumber = "0907890123",
                    IsActive = true,
                    CreatedAt = now,
                    UpdatedAt = now
                },
                new Users
                {
                    Id = 8,
                    UserName = "farmer4",
                    Password = "farmer123",
                    Email = "phamvang@gmail.com",
                    Address = "Ấp 4, Xã Đức Hòa, Huyện Đức Hòa, Tỉnh Long An",
                    PhoneNumber = "0908901234",
                    IsActive = true,
                    CreatedAt = now,
                    UpdatedAt = now
                },
                new Users
                {
                    Id = 9,
                    UserName = "farmer5",
                    Password = "farmer123",
                    Email = "hoangthih@gmail.com",
                    Address = "Ấp 5, Xã Tân Trụ, Huyện Tân Trụ, Tỉnh Long An",
                    PhoneNumber = "0909012345",
                    IsActive = true,
                    CreatedAt = now,
                    UpdatedAt = now
                }
            );

            // Seed UserRoles
            modelBuilder.Entity<UserRole>().HasData(
                new UserRole { Id = 1, UserId = 1, RoleId = 1, CreatedAt = now }, // Admin
                new UserRole { Id = 2, UserId = 2, RoleId = 2, CreatedAt = now }, // Engineer 1
                new UserRole { Id = 3, UserId = 3, RoleId = 2, CreatedAt = now }, // Engineer 2
                new UserRole { Id = 4, UserId = 4, RoleId = 2, CreatedAt = now }, // Engineer 3
                new UserRole { Id = 5, UserId = 5, RoleId = 3, CreatedAt = now }, // Farmer 1
                new UserRole { Id = 6, UserId = 6, RoleId = 3, CreatedAt = now }, // Farmer 2
                new UserRole { Id = 7, UserId = 7, RoleId = 3, CreatedAt = now }, // Farmer 3
                new UserRole { Id = 8, UserId = 8, RoleId = 3, CreatedAt = now }, // Farmer 4
                new UserRole { Id = 9, UserId = 9, RoleId = 3, CreatedAt = now }  // Farmer 5
            );

            // Seed Engineers
            modelBuilder.Entity<Engineer>().HasData(
                new Engineer
                {
                    Id = 1,
                    UserId = 2,
                    Specialization = "Bệnh học thực vật",
                    ExperienceYears = 8,
                    Certification = "[\"Chứng chỉ Kỹ sư Nông nghiệp\", \"Chứng chỉ Chuyên gia Bệnh học thực vật\"]",
                    Bio = "Chuyên gia về bệnh hại cây trồng với 8 năm kinh nghiệm trong lĩnh vực chẩn đoán và điều trị bệnh lúa, rau màu.",
                    CreatedAt = now,
                    UpdatedAt = now
                },
                new Engineer
                {
                    Id = 2,
                    UserId = 3,
                    Specialization = "Dinh dưỡng cây trồng",
                    ExperienceYears = 6,
                    Certification = "[\"Chứng chỉ Kỹ sư Nông nghiệp\", \"Chứng chỉ Chuyên gia Dinh dưỡng thực vật\"]",
                    Bio = "Chuyên gia về dinh dưỡng và phân bón cây trồng, có kinh nghiệm tư vấn cho nhiều hợp tác xã nông nghiệp.",
                    CreatedAt = now,
                    UpdatedAt = now
                },
                new Engineer
                {
                    Id = 3,
                    UserId = 4,
                    Specialization = "Kỹ thuật canh tác",
                    ExperienceYears = 10,
                    Certification = "[\"Chứng chỉ Kỹ sư Nông nghiệp\", \"Chứng chỉ Chuyên gia Kỹ thuật canh tác\", \"Chứng chỉ Nông nghiệp hữu cơ\"]",
                    Bio = "Chuyên gia kỹ thuật canh tác với 10 năm kinh nghiệm, chuyên về nông nghiệp hữu cơ và canh tác bền vững.",
                    CreatedAt = now,
                    UpdatedAt = now
                }
            );

            // Seed Farmers
            modelBuilder.Entity<Farmer>().HasData(
                new Farmer
                {
                    Id = 1,
                    UserId = 5,
                    FarmLocation = "Ấp 1, Xã Tân Phú, Huyện Châu Thành, Tỉnh An Giang",
                    FarmSize = 2.5m,
                    CropTypes = "[\"Lúa\", \"Rau màu\", \"Cây ăn trái\"]",
                    FarmingExperienceYears = 15,
                    CreatedAt = now,
                    UpdatedAt = now
                },
                new Farmer
                {
                    Id = 2,
                    UserId = 6,
                    FarmLocation = "Ấp 2, Xã Long Phú, Huyện Phú Tân, Tỉnh An Giang",
                    FarmSize = 3.2m,
                    CropTypes = "[\"Lúa\", \"Ngô\", \"Đậu tương\"]",
                    FarmingExperienceYears = 12,
                    CreatedAt = now,
                    UpdatedAt = now
                },
                new Farmer
                {
                    Id = 3,
                    UserId = 7,
                    FarmLocation = "Ấp 3, Xã Vĩnh Hậu, Huyện Tân Hưng, Tỉnh Long An",
                    FarmSize = 1.8m,
                    CropTypes = "[\"Rau màu\", \"Cây ăn trái\", \"Hoa màu\"]",
                    FarmingExperienceYears = 8,
                    CreatedAt = now,
                    UpdatedAt = now
                },
                new Farmer
                {
                    Id = 4,
                    UserId = 8,
                    FarmLocation = "Ấp 4, Xã Đức Hòa, Huyện Đức Hòa, Tỉnh Long An",
                    FarmSize = 4.1m,
                    CropTypes = "[\"Lúa\", \"Mía\", \"Cây ăn trái\"]",
                    FarmingExperienceYears = 20,
                    CreatedAt = now,
                    UpdatedAt = now
                },
                new Farmer
                {
                    Id = 5,
                    UserId = 9,
                    FarmLocation = "Ấp 5, Xã Tân Trụ, Huyện Tân Trụ, Tỉnh Long An",
                    FarmSize = 2.9m,
                    CropTypes = "[\"Lúa\", \"Rau màu\", \"Đậu các loại\"]",
                    FarmingExperienceYears = 10,
                    CreatedAt = now,
                    UpdatedAt = now
                }
            );

            // Seed EngineerFarmerAssignments
            modelBuilder.Entity<EngineerFarmerAssignment>().HasData(
                new EngineerFarmerAssignment { Id = 1, EngineerId = 1, FarmerId = 1, AssignedAt = now, IsActive = true, Notes = "Hỗ trợ chẩn đoán bệnh lúa" },
                new EngineerFarmerAssignment { Id = 2, EngineerId = 1, FarmerId = 2, AssignedAt = now, IsActive = true, Notes = "Tư vấn phòng trị bệnh hại" },
                new EngineerFarmerAssignment { Id = 3, EngineerId = 2, FarmerId = 3, AssignedAt = now, IsActive = true, Notes = "Tư vấn dinh dưỡng cây trồng" },
                new EngineerFarmerAssignment { Id = 4, EngineerId = 2, FarmerId = 4, AssignedAt = now, IsActive = true, Notes = "Hướng dẫn bón phân" },
                new EngineerFarmerAssignment { Id = 5, EngineerId = 3, FarmerId = 5, AssignedAt = now, IsActive = true, Notes = "Tư vấn kỹ thuật canh tác hữu cơ" }
            );

            // Seed BlogCategories (already exists, keeping the same)
            modelBuilder.Entity<BlogCategory>().HasData(
                new BlogCategory { Id = 1, Name = "Bệnh cây trồng", Description = "Các bài viết về bệnh hại trên cây trồng và cách phòng trị", Slug = "benh-cay-trong", IsActive = true, CreatedAt = now },
                new BlogCategory { Id = 2, Name = "Kỹ thuật canh tác", Description = "Hướng dẫn kỹ thuật trồng trọt và chăm sóc cây", Slug = "ky-thuat-canh-tac", IsActive = true, CreatedAt = now },
                new BlogCategory { Id = 3, Name = "Phân bón", Description = "Thông tin về các loại phân bón và cách sử dụng", Slug = "phan-bon", IsActive = true, CreatedAt = now },
                new BlogCategory { Id = 4, Name = "Thuốc bảo vệ thực vật", Description = "Hướng dẫn sử dụng thuốc BVTV an toàn", Slug = "thuoc-bao-ve-thuc-vat", IsActive = true, CreatedAt = now },
                new BlogCategory { Id = 5, Name = "Thời vụ", Description = "Lịch thời vụ và mùa vụ canh tác", Slug = "thoi-vu", IsActive = true, CreatedAt = now }
            );

            // Seed Blogs
            modelBuilder.Entity<Blog>().HasData(
                new Blog
                {
                    Id = 1,
                    AuthorId = 2,
                    CategoryId = 1,
                    Title = "Cách nhận biết và phòng trị bệnh đạo ôn lúa",
                    Content = "Bệnh đạo ôn lúa là một trong những bệnh phổ biến và nguy hiểm nhất đối với cây lúa. Bài viết này sẽ hướng dẫn cách nhận biết triệu chứng và các biện pháp phòng trị hiệu quả...",
                    FeaturedImage = "/images/blog/dao-on-lua.jpg",
                    Slug = "cach-nhan-biet-va-phong-tri-benh-dao-on-lua",
                    Status = "published",
                    ViewCount = 1250,
                    PublishedAt = now.AddDays(-10),
                    CreatedAt = now.AddDays(-15),
                    UpdatedAt = now.AddDays(-10)
                },
                new Blog
                {
                    Id = 2,
                    AuthorId = 3,
                    CategoryId = 3,
                    Title = "Hướng dẫn bón phân NPK cho cây lúa theo từng giai đoạn",
                    Content = "Việc bón phân đúng cách và đúng thời điểm là yếu tố quyết định năng suất lúa. Bài viết này sẽ hướng dẫn chi tiết cách bón phân NPK cho cây lúa...",
                    FeaturedImage = "/images/blog/bon-phan-lua.jpg",
                    Slug = "huong-dan-bon-phan-npk-cho-cay-lua",
                    Status = "published",
                    ViewCount = 980,
                    PublishedAt = now.AddDays(-7),
                    CreatedAt = now.AddDays(-12),
                    UpdatedAt = now.AddDays(-7)
                },
                new Blog
                {
                    Id = 3,
                    AuthorId = 4,
                    CategoryId = 2,
                    Title = "Kỹ thuật trồng rau màu trong nhà kính",
                    Content = "Trồng rau màu trong nhà kính giúp kiểm soát được điều kiện môi trường, tăng năng suất và chất lượng sản phẩm. Bài viết này sẽ chia sẻ những kỹ thuật cần thiết...",
                    FeaturedImage = "/images/blog/rau-nha-kinh.jpg",
                    Slug = "ky-thuat-trong-rau-mau-trong-nha-kinh",
                    Status = "published",
                    ViewCount = 756,
                    PublishedAt = now.AddDays(-5),
                    CreatedAt = now.AddDays(-8),
                    UpdatedAt = now.AddDays(-5)
                },
                new Blog
                {
                    Id = 4,
                    AuthorId = 2,
                    CategoryId = 4,
                    Title = "An toàn khi sử dụng thuốc bảo vệ thực vật",
                    Content = "Việc sử dụng thuốc BVTV cần tuân thủ nghiêm ngặt các quy định về an toàn để bảo vệ sức khỏe người sử dụng và môi trường...",
                    FeaturedImage = "/images/blog/an-toan-thuoc-bvtv.jpg",
                    Slug = "an-toan-khi-su-dung-thuoc-bao-ve-thuc-vat",
                    Status = "published",
                    ViewCount = 1100,
                    PublishedAt = now.AddDays(-3),
                    CreatedAt = now.AddDays(-6),
                    UpdatedAt = now.AddDays(-3)
                },
                new Blog
                {
                    Id = 5,
                    AuthorId = 3,
                    CategoryId = 5,
                    Title = "Lịch thời vụ trồng lúa miền Nam năm 2024",
                    Content = "Lịch thời vụ trồng lúa miền Nam được xây dựng dựa trên điều kiện khí hậu, thủy văn và kinh nghiệm sản xuất của nông dân...",
                    FeaturedImage = "/images/blog/lich-thoi-vu-lua.jpg",
                    Slug = "lich-thoi-vu-trong-lua-mien-nam-2024",
                    Status = "published",
                    ViewCount = 2100,
                    PublishedAt = now.AddDays(-1),
                    CreatedAt = now.AddDays(-4),
                    UpdatedAt = now.AddDays(-1)
                }
            );

            // Seed NewsCategories
            modelBuilder.Entity<NewsCategory>().HasData(
                new NewsCategory { Id = 1, Name = "Chính sách", Description = "Tin tức về chính sách nông nghiệp", Slug = "chinh-sach", IsActive = true, CreatedAt = now },
                new NewsCategory { Id = 2, Name = "Thị trường", Description = "Thông tin thị trường nông sản", Slug = "thi-truong", IsActive = true, CreatedAt = now },
                new NewsCategory { Id = 3, Name = "Công nghệ", Description = "Công nghệ mới trong nông nghiệp", Slug = "cong-nghe", IsActive = true, CreatedAt = now },
                new NewsCategory { Id = 4, Name = "Sự kiện", Description = "Các sự kiện nông nghiệp", Slug = "su-kien", IsActive = true, CreatedAt = now },
                new NewsCategory { Id = 5, Name = "Thời tiết", Description = "Dự báo thời tiết phục vụ sản xuất", Slug = "thoi-tiet", IsActive = true, CreatedAt = now }
            );

            // Seed News
            modelBuilder.Entity<News>().HasData(
                new News
                {
                    Id = 1,
                    Title = "Chính phủ hỗ trợ 500 tỷ đồng cho nông dân chuyển đổi số",
                    Excerpt = "Chương trình hỗ trợ nông dân ứng dụng công nghệ số trong sản xuất nông nghiệp với tổng kinh phí 500 tỷ đồng.",
                    Content = "Chính phủ vừa phê duyệt chương trình hỗ trợ nông dân chuyển đổi số trong sản xuất nông nghiệp với tổng kinh phí 500 tỷ đồng...",
                    Author = "Bộ Nông nghiệp và Phát triển Nông thôn",
                    PublishedAt = now.AddDays(-2),
                    CategoryId = 1,
                    Featured = true,
                    Urgent = false,
                    Tags = "[\"Chính sách\", \"Chuyển đổi số\", \"Hỗ trợ nông dân\"]",
                    Source = "Bộ Nông nghiệp và Phát triển Nông thôn",
                    ImageUrl = "/images/news/ho-tro-chuyen-doi-so.jpg",
                    ViewCount = 3500,
                    CreatedAt = now.AddDays(-2),
                    UpdatedAt = now.AddDays(-2)
                },
                new News
                {
                    Id = 2,
                    Title = "Giá lúa tăng mạnh do xuất khẩu khởi sắc",
                    Excerpt = "Giá lúa tại các tỉnh ĐBSCL tăng 200-300 đồng/kg so với tuần trước nhờ nhu cầu xuất khẩu tăng cao.",
                    Content = "Theo báo cáo từ Sở Nông nghiệp các tỉnh ĐBSCL, giá lúa đã tăng mạnh trong tuần qua...",
                    Author = "Hiệp hội Lương thực Việt Nam",
                    PublishedAt = now.AddDays(-1),
                    CategoryId = 2,
                    Featured = false,
                    Urgent = true,
                    Tags = "[\"Giá lúa\", \"Xuất khẩu\", \"Thị trường\"]",
                    Source = "Hiệp hội Lương thực Việt Nam",
                    ImageUrl = "/images/news/gia-lua-tang.jpg",
                    ViewCount = 2800,
                    CreatedAt = now.AddDays(-1),
                    UpdatedAt = now.AddDays(-1)
                },
                new News
                {
                    Id = 3,
                    Title = "Ứng dụng AI trong chẩn đoán bệnh cây trồng",
                    Excerpt = "Công nghệ trí tuệ nhân tạo đang được ứng dụng rộng rãi trong việc chẩn đoán bệnh hại cây trồng với độ chính xác cao.",
                    Content = "Các ứng dụng AI hiện đại đang giúp nông dân chẩn đoán bệnh cây trồng nhanh chóng và chính xác...",
                    Author = "Viện Bảo vệ thực vật",
                    PublishedAt = now.AddHours(-12),
                    CategoryId = 3,
                    Featured = true,
                    Urgent = false,
                    Tags = "[\"AI\", \"Công nghệ\", \"Chẩn đoán bệnh\"]",
                    Source = "Viện Bảo vệ thực vật",
                    ImageUrl = "/images/news/ai-chan-doan-benh.jpg",
                    ViewCount = 1900,
                    CreatedAt = now.AddHours(-12),
                    UpdatedAt = now.AddHours(-12)
                },
                new News
                {
                    Id = 4,
                    Title = "Hội nghị quốc tế về nông nghiệp bền vững sẽ diễn ra tại Hà Nội",
                    Excerpt = "Hội nghị quốc tế về nông nghiệp bền vững và an ninh lương thực sẽ được tổ chức tại Hà Nội từ ngày 15-17/2/2024.",
                    Content = "Hội nghị sẽ quy tụ các chuyên gia hàng đầu thế giới về nông nghiệp bền vững...",
                    Author = "Ban Tổ chức",
                    PublishedAt = now.AddHours(-6),
                    CategoryId = 4,
                    Featured = false,
                    Urgent = false,
                    Tags = "[\"Hội nghị\", \"Quốc tế\", \"Bền vững\", \"An ninh lương thực\"]",
                    Source = "Bộ Nông nghiệp và Phát triển Nông thôn",
                    ImageUrl = "/images/news/hoi-nghi-quoc-te.jpg",
                    ViewCount = 1200,
                    CreatedAt = now.AddHours(-6),
                    UpdatedAt = now.AddHours(-6)
                },
                new News
                {
                    Id = 5,
                    Title = "Cảnh báo thời tiết bất lợi cho vụ lúa Đông Xuân",
                    Excerpt = "Trung tâm Dự báo khí tượng thủy văn cảnh báo đợt rét đậm có thể ảnh hưởng đến vụ lúa Đông Xuân.",
                    Content = "Theo dự báo, đợt không khí lạnh mạnh sẽ ảnh hưởng đến các tỉnh miền Bắc và Bắc Trung Bộ...",
                    Author = "Trung tâm Dự báo khí tượng thủy văn",
                    PublishedAt = now.AddHours(-3),
                    CategoryId = 5,
                    Featured = false,
                    Urgent = true,
                    Tags = "[\"Thời tiết\", \"Cảnh báo\", \"Lúa Đông Xuân\"]",
                    Source = "Trung tâm Dự báo khí tượng thủy văn",
                    ImageUrl = "/images/news/canh-bao-thoi-tiet.jpg",
                    ViewCount = 4200,
                    CreatedAt = now.AddHours(-3),
                    UpdatedAt = now.AddHours(-3)
                }
            );

            // Seed ProductCategories
            modelBuilder.Entity<ProductCategory>().HasData(
                new ProductCategory { Id = 1, Name = "Hạt giống", Description = "Các loại hạt giống cây trồng", Slug = "hat-giong", IsActive = true, CreatedAt = now },
                new ProductCategory { Id = 2, Name = "Phân bón", Description = "Các loại phân bón hữu cơ và vô cơ", Slug = "phan-bon", IsActive = true, CreatedAt = now },
                new ProductCategory { Id = 3, Name = "Thuốc BVTV", Description = "Thuốc bảo vệ thực vật", Slug = "thuoc-bvtv", IsActive = true, CreatedAt = now },
                new ProductCategory { Id = 4, Name = "Dụng cụ nông nghiệp", Description = "Các dụng cụ và thiết bị nông nghiệp", Slug = "dung-cu-nong-nghiep", IsActive = true, CreatedAt = now },
                new ProductCategory { Id = 5, Name = "Máy móc", Description = "Máy móc thiết bị nông nghiệp", Slug = "may-moc", IsActive = true, CreatedAt = now }
            );

            // Seed Products
            modelBuilder.Entity<Product>().HasData(
                // Hạt giống
                new Product
                {
                    Id = 1,
                    Name = "Hạt giống lúa ST25",
                    Description = "Giống lúa ST25 chất lượng cao, năng suất ổn định, kháng bệnh tốt. Thời gian sinh trưởng 95-100 ngày.",
                    Price = 45000m,
                    CategoryId = 1,
                    Stock = 500,
                    ImageUrl = "/images/products/hat-giong-lua-st25.jpg",
                    IsActive = true,
                    SKU = "HG-ST25-001",
                    DiscountPrice = 42000m,
                    CreatedAt = now,
                    UpdatedAt = now
                },
                new Product
                {
                    Id = 2,
                    Name = "Hạt giống rau cải xanh",
                    Description = "Hạt giống rau cải xanh F1, tỷ lệ nảy mầm cao, sinh trưởng nhanh, chống chịu tốt.",
                    Price = 25000m,
                    CategoryId = 1,
                    Stock = 200,
                    ImageUrl = "/images/products/hat-giong-cai-xanh.jpg",
                    IsActive = true,
                    SKU = "HG-CX-002",
                    CreatedAt = now,
                    UpdatedAt = now
                },
                new Product
                {
                    Id = 3,
                    Name = "Hạt giống cà chua F1",
                    Description = "Giống cà chua F1 năng suất cao, quả to, màu đỏ đẹp, thích hợp trồng quanh năm.",
                    Price = 35000m,
                    CategoryId = 1,
                    Stock = 150,
                    ImageUrl = "/images/products/hat-giong-ca-chua.jpg",
                    IsActive = true,
                    SKU = "HG-CC-003",
                    DiscountPrice = 32000m,
                    CreatedAt = now,
                    UpdatedAt = now
                },
                // Phân bón
                new Product
                {
                    Id = 4,
                    Name = "Phân NPK 16-16-8",
                    Description = "Phân NPK 16-16-8 chuyên dụng cho cây lúa, cung cấp đầy đủ dinh dưỡng cho cây trồng.",
                    Price = 18000m,
                    CategoryId = 2,
                    Stock = 1000,
                    ImageUrl = "/images/products/phan-npk-16-16-8.jpg",
                    IsActive = true,
                    SKU = "PB-NPK-004",
                    CreatedAt = now,
                    UpdatedAt = now
                },
                new Product
                {
                    Id = 5,
                    Name = "Phân hữu cơ vi sinh",
                    Description = "Phân hữu cơ vi sinh giúp cải tạo đất, tăng cường sức đề kháng cho cây trồng.",
                    Price = 22000m,
                    CategoryId = 2,
                    Stock = 800,
                    ImageUrl = "/images/products/phan-huu-co-vi-sinh.jpg",
                    IsActive = true,
                    SKU = "PB-HCVS-005",
                    DiscountPrice = 20000m,
                    CreatedAt = now,
                    UpdatedAt = now
                },
                // Thuốc BVTV
                new Product
                {
                    Id = 6,
                    Name = "Thuốc trừ sâu Regent 50SC",
                    Description = "Thuốc trừ sâu Regent 50SC hiệu quả cao, an toàn cho người và môi trường.",
                    Price = 85000m,
                    CategoryId = 3,
                    Stock = 300,
                    ImageUrl = "/images/products/thuoc-tru-sau-regent.jpg",
                    IsActive = true,
                    SKU = "BVTV-REG-006",
                    CreatedAt = now,
                    UpdatedAt = now
                },
                new Product
                {
                    Id = 7,
                    Name = "Thuốc diệt cỏ Gramoxone",
                    Description = "Thuốc diệt cỏ Gramoxone tác dụng nhanh, hiệu quả cao với nhiều loại cỏ dại.",
                    Price = 95000m,
                    CategoryId = 3,
                    Stock = 250,
                    ImageUrl = "/images/products/thuoc-diet-co-gramoxone.jpg",
                    IsActive = true,
                    SKU = "BVTV-GRA-007",
                    CreatedAt = now,
                    UpdatedAt = now
                },
                // Dụng cụ
                new Product
                {
                    Id = 8,
                    Name = "Máy phun thuốc bình xịt 16L",
                    Description = "Máy phun thuốc bình xịt dung tích 16L, áp suất cao, phun đều, tiết kiệm thuốc.",
                    Price = 450000m,
                    CategoryId = 4,
                    Stock = 50,
                    ImageUrl = "/images/products/may-phun-thuoc-16l.jpg",
                    IsActive = true,
                    SKU = "DC-MPT-008",
                    DiscountPrice = 420000m,
                    CreatedAt = now,
                    UpdatedAt = now
                },
                new Product
                {
                    Id = 9,
                    Name = "Cuốc xới đất cán gỗ",
                    Description = "Cuốc xới đất cán gỗ chất lượng cao, bền bỉ, phù hợp cho mọi loại đất.",
                    Price = 75000m,
                    CategoryId = 4,
                    Stock = 100,
                    ImageUrl = "/images/products/cuoc-xoi-dat.jpg",
                    IsActive = true,
                    SKU = "DC-CXD-009",
                    CreatedAt = now,
                    UpdatedAt = now
                },
                // Máy móc
                new Product
                {
                    Id = 10,
                    Name = "Máy cắt cỏ Honda GX35",
                    Description = "Máy cắt cỏ Honda GX35 công suất mạnh, tiết kiệm nhiên liệu, độ bền cao.",
                    Price = 3500000m,
                    CategoryId = 5,
                    Stock = 20,
                    ImageUrl = "/images/products/may-cat-co-honda.jpg",
                    IsActive = true,
                    SKU = "MM-MCC-010",
                    DiscountPrice = 3300000m,
                    CreatedAt = now,
                    UpdatedAt = now
                }
            );

            // Seed Tickets
            modelBuilder.Entity<Ticket>().HasData(
                new Ticket
                {
                    Id = 1,
                    FarmerId = 1,
                    AssignedEngineerId = 1,
                    Title = "Lúa bị vàng lá và chết khô",
                    Category = "Bệnh cây trồng",
                    CropType = "Lúa",
                    Location = "Ruộng A1, Ấp 1, Xã Tân Phú",
                    Description = "Lúa của tôi đang trong giai đoạn đẻ nhánh nhưng bị vàng lá từ dưới lên, một số cây đã chết khô. Tôi đã tưới nước đầy đủ nhưng tình trạng không cải thiện.",
                    Priority = "high",
                    ContactMethod = "Điện thoại",
                    PhoneNumber = "0905678901",
                    ImageUrl = "default.jpg",
                    Status = "in_progress",
                    CreatedAt = now.AddDays(-5),
                    UpdatedAt = now.AddDays(-2)
                },
                new Ticket
                {
                    Id = 2,
                    FarmerId = 2,
                    AssignedEngineerId = 2,
                    Title = "Tư vấn lượng phân bón cho vụ lúa mới",
                    Category = "Dinh dưỡng cây trồng",
                    CropType = "Lúa",
                    Location = "Ruộng B2, Ấp 2, Xã Long Phú",
                    Description = "Tôi chuẩn bị gieo sạ vụ lúa mới, muốn được tư vấn về lượng phân bón cần thiết cho 3.2 hecta đất.",
                    Priority = "medium",
                    ContactMethod = "Email",
                    PhoneNumber = "0906789012",
                    ImageUrl = "default.jpg",
                    Status = "assigned",
                    CreatedAt = now.AddDays(-3),
                    UpdatedAt = now.AddDays(-3)
                },
                new Ticket
                {
                    Id = 3,
                    FarmerId = 3,
                    Title = "Rau cải bị sâu ăn lá",
                    Category = "Sâu bệnh",
                    CropType = "Rau màu",
                    Location = "Vườn C1, Ấp 3, Xã Vĩnh Hậu",
                    Description = "Rau cải của tôi bị sâu ăn lá nghiêm trọng, lá bị thủng lỗ chỗ. Cần tư vấn thuốc trừ sâu phù hợp.",
                    Priority = "urgent",
                    ContactMethod = "Điện thoại",
                    PhoneNumber = "0907890123",
                    ImageUrl = "default.jpg",
                    Status = "open",
                    CreatedAt = now.AddDays(-1),
                    UpdatedAt = now.AddDays(-1)
                },
                new Ticket
                {
                    Id = 4,
                    FarmerId = 4,
                    AssignedEngineerId = 3,
                    Title = "Hướng dẫn kỹ thuật trồng hữu cơ",
                    Category = "Kỹ thuật canh tác",
                    CropType = "Cây ăn trái",
                    Location = "Vườn D1, Ấp 4, Xã Đức Hòa",
                    Description = "Tôi muốn chuyển đổi sang mô hình trồng trọt hữu cơ cho vườn cây ăn trái. Cần được hướng dẫn quy trình và kỹ thuật.",
                    Priority = "low",
                    ContactMethod = "Email",
                    PhoneNumber = "0908901234",
                    ImageUrl = "default.jpg",
                    Status = "resolved",
                    CreatedAt = now.AddDays(-10),
                    UpdatedAt = now.AddDays(-1),
                    ResolvedAt = now.AddDays(-1)
                },
                new Ticket
                {
                    Id = 5,
                    FarmerId = 5,
                    Title = "Đất bị chua, cây trồng sinh trưởng kém",
                    Category = "Đất đai",
                    CropType = "Rau màu",
                    Location = "Ruộng E1, Ấp 5, Xã Tân Trụ",
                    Description = "Đất trồng rau của tôi có vẻ bị chua, cây trồng sinh trưởng chậm, lá vàng. Cần tư vấn cách cải tạo đất.",
                    Priority = "medium",
                    ContactMethod = "Điện thoại",
                    PhoneNumber = "0909012345",
                    ImageUrl = "default.jpg",
                    Status = "open",
                    CreatedAt = now.AddHours(-12),
                    UpdatedAt = now.AddHours(-12)
                }
            );

            // Seed TicketComments
            modelBuilder.Entity<TicketComment>().HasData(
                new TicketComment
                {
                    Id = 1,
                    TicketId = 1,
                    UserId = 2, // Engineer 1
                    Comment = "Dựa vào mô tả và hình ảnh, có thể cây lúa của anh bị bệnh khô vằn. Tôi sẽ đến khảo sát thực địa vào chiều mai.",
                    IsInternal = false,
                    CreatedAt = now.AddDays(-4)
                },
                new TicketComment
                {
                    Id = 2,
                    TicketId = 1,
                    UserId = 5, // Farmer 1
                    Comment = "Cảm ơn kỹ sư. Tôi sẽ chờ anh đến khảo sát. Hiện tại tình trạng vẫn đang lan rộng.",
                    IsInternal = false,
                    CreatedAt = now.AddDays(-4)
                },
                new TicketComment
                {
                    Id = 3,
                    TicketId = 1,
                    UserId = 2, // Engineer 1
                    Comment = "Đã khảo sát thực địa. Xác định là bệnh khô vằn do nấm. Đã hướng dẫn anh sử dụng thuốc Validamycin 3% với liều lượng 1.5L/ha.",
                    IsInternal = false,
                    CreatedAt = now.AddDays(-2)
                },
                new TicketComment
                {
                    Id = 4,
                    TicketId = 2,
                    UserId = 3, // Engineer 2
                    Comment = "Với diện tích 3.2ha lúa, anh nên sử dụng: Phân lót 200kg NPK 16-16-8, phân thúc lần 1: 100kg Urea, phân thúc lần 2: 80kg NPK 20-20-15.",
                    IsInternal = false,
                    CreatedAt = now.AddDays(-2)
                },
                new TicketComment
                {
                    Id = 5,
                    TicketId = 4,
                    UserId = 4, // Engineer 3
                    Comment = "Đã hoàn thành hướng dẫn chuyển đổi hữu cơ cho anh. Gửi kèm tài liệu quy trình chi tiết qua email.",
                    IsInternal = false,
                    CreatedAt = now.AddDays(-1)
                }
            );

            // Seed Reviews
            modelBuilder.Entity<Review>().HasData(
                new Review
                {
                    Id = 1,
                    ProductId = 1, // Hạt giống lúa ST25
                    UserId = 5, // Farmer 1
                    UserName = "Nguyễn Thị D",
                    ReviewValue = 5,
                    ReviewMessage = "Hạt giống chất lượng tuyệt vời! Tỷ lệ nảy mầm cao, cây lúa sinh trưởng khỏe mạnh. Năng suất đạt như quảng cáo. Sẽ tiếp tục mua ở lần sau.",
                    CreatedAt = now.AddDays(-20),
                    UpdatedAt = now.AddDays(-20)
                },
                new Review
                {
                    Id = 2,
                    ProductId = 1, // Hạt giống lúa ST25
                    UserId = 6, // Farmer 2
                    UserName = "Trần Văn E",
                    ReviewValue = 4,
                    ReviewMessage = "Giống lúa tốt, năng suất ổn định. Chỉ có điều giá hơi cao so với các giống khác. Nhưng chất lượng xứng đáng với giá tiền.",
                    CreatedAt = now.AddDays(-18),
                    UpdatedAt = now.AddDays(-18)
                },
                new Review
                {
                    Id = 3,
                    ProductId = 4, // Phân NPK 16-16-8
                    UserId = 7, // Farmer 3
                    UserName = "Lê Thị F",
                    ReviewMessage = "Phân bón hiệu quả tốt, cây trồng xanh tốt sau khi bón. Giá cả hợp lý, giao hàng nhanh chóng.",
                    ReviewValue = 5,
                    CreatedAt = now.AddDays(-15),
                    UpdatedAt = now.AddDays(-15)
                },
                new Review
                {
                    Id = 4,
                    ProductId = 8, // Máy phun thuốc 16L
                    UserId = 8, // Farmer 4
                    UserName = "Phạm Văn G",
                    ReviewValue = 4,
                    ReviewMessage = "Máy phun hoạt động tốt, áp suất ổn định. Dung tích 16L vừa phải cho diện tích nhỏ. Chỉ có điều hơi nặng khi mang lâu.",
                    CreatedAt = now.AddDays(-12),
                    UpdatedAt = now.AddDays(-12)
                },
                new Review
                {
                    Id = 5,
                    ProductId = 5, // Phân hữu cơ vi sinh
                    UserId = 9, // Farmer 5
                    UserName = "Hoàng Thị H",
                    ReviewValue = 5,
                    ReviewMessage = "Phân hữu cơ rất tốt! Đất trở nên tơi xốp hơn, cây trồng khỏe mạnh. Đặc biệt hiệu quả với rau màu. Giá có khuyến mãi nữa, rất hài lòng!",
                    CreatedAt = now.AddDays(-10),
                    UpdatedAt = now.AddDays(-10)
                },
                new Review
                {
                    Id = 6,
                    ProductId = 6, // Thuốc trừ sâu Regent
                    UserId = 5, // Farmer 1
                    UserName = "Nguyễn Thị D",
                    ReviewValue = 4,
                    ReviewMessage = "Thuốc trừ sâu hiệu quả, sâu chết nhanh sau khi phun. Tuy nhiên cần chú ý an toàn khi sử dụng.",
                    CreatedAt = now.AddDays(-8),
                    UpdatedAt = now.AddDays(-8)
                },
                new Review
                {
                    Id = 7,
                    ProductId = 10, // Máy cắt cỏ Honda
                    UserId = 6, // Farmer 2
                    UserName = "Trần Văn E",
                    ReviewValue = 5,
                    ReviewMessage = "Máy cắt cỏ Honda chất lượng xuất sắc! Máy chạy êm, cắt sạch, tiết kiệm xăng. Đáng đồng tiền bát gạo. Khuyên mọi người nên mua.",
                    CreatedAt = now.AddDays(-5),
                    UpdatedAt = now.AddDays(-5)
                }
            );

            // Seed Carts
            modelBuilder.Entity<Cart>().HasData(
                new Cart
                {
                    Id = 1,
                    UserId = 5, // Farmer 1
                    TotalAmount = 127000m,
                    CreatedAt = now.AddDays(-2),
                    UpdatedAt = now.AddHours(-6)
                },
                new Cart
                {
                    Id = 2,
                    UserId = 6, // Farmer 2
                    TotalAmount = 540000m,
                    CreatedAt = now.AddDays(-1),
                    UpdatedAt = now.AddHours(-3)
                },
                new Cart
                {
                    Id = 3,
                    UserId = 7, // Farmer 3
                    TotalAmount = 85000m,
                    CreatedAt = now.AddHours(-12),
                    UpdatedAt = now.AddHours(-2)
                }
            );

            // Seed CartItems
            modelBuilder.Entity<CartItem>().HasData(
                // Cart 1 items
                new CartItem
                {
                    Id = 1,
                    CartId = 1,
                    ProductId = 1, // Hạt giống lúa ST25
                    Quantity = 2,
                    UnitPrice = 42000m, // Discounted price
                    TotalPrice = 84000m,
                    CreatedAt = now.AddDays(-2),
                    UpdatedAt = now.AddDays(-2)
                },
                new CartItem
                {
                    Id = 2,
                    CartId = 1,
                    ProductId = 2, // Hạt giống rau cải
                    Quantity = 1,
                    UnitPrice = 25000m,
                    TotalPrice = 25000m,
                    CreatedAt = now.AddHours(-6),
                    UpdatedAt = now.AddHours(-6)
                },
                new CartItem
                {
                    Id = 3,
                    CartId = 1,
                    ProductId = 4, // Phân NPK
                    Quantity = 1,
                    UnitPrice = 18000m,
                    TotalPrice = 18000m,
                    CreatedAt = now.AddHours(-6),
                    UpdatedAt = now.AddHours(-6)
                },
                // Cart 2 items
                new CartItem
                {
                    Id = 4,
                    CartId = 2,
                    ProductId = 8, // Máy phun thuốc
                    Quantity = 1,
                    UnitPrice = 420000m, // Discounted price
                    TotalPrice = 420000m,
                    CreatedAt = now.AddDays(-1),
                    UpdatedAt = now.AddDays(-1)
                },
                new CartItem
                {
                    Id = 5,
                    CartId = 2,
                    ProductId = 4, // Phân NPK
                    Quantity = 5,
                    UnitPrice = 18000m,
                    TotalPrice = 90000m,
                    CreatedAt = now.AddHours(-3),
                    UpdatedAt = now.AddHours(-3)
                },
                new CartItem
                {
                    Id = 6,
                    CartId = 2,
                    ProductId = 9, // Cuốc xới đất
                    Quantity = 2,
                    UnitPrice = 75000m,
                    TotalPrice = 150000m,
                    CreatedAt = now.AddHours(-3),
                    UpdatedAt = now.AddHours(-3)
                },
                // Cart 3 items
                new CartItem
                {
                    Id = 7,
                    CartId = 3,
                    ProductId = 6, // Thuốc trừ sâu
                    Quantity = 1,
                    UnitPrice = 85000m,
                    TotalPrice = 85000m,
                    CreatedAt = now.AddHours(-12),
                    UpdatedAt = now.AddHours(-12)
                }
            );

            // Seed Orders
            modelBuilder.Entity<Order>().HasData(
                new Order
                {
                    Id = 1,
                    UserId = 5, // Farmer 1
                    OrderNumber = "ORD-2024-001",
                    TotalAmount = 189000m,
                    Status = "delivered",
                    ShippingAddress = "Ấp 1, Xã Tân Phú, Huyện Châu Thành, Tỉnh An Giang",
                    PaymentMethod = "cod",
                    PaymentStatus = "paid",
                    PaidAt = now.AddDays(-15),
                    CreatedAt = now.AddDays(-20),
                    UpdatedAt = now.AddDays(-15)
                },
                new Order
                {
                    Id = 2,
                    UserId = 6, // Farmer 2
                    OrderNumber = "ORD-2024-002",
                    TotalAmount = 3450000m,
                    Status = "shipped",
                    ShippingAddress = "Ấp 2, Xã Long Phú, Huyện Phú Tân, Tỉnh An Giang",
                    PaymentMethod = "bank_transfer",
                    PaymentStatus = "paid",
                    PaidAt = now.AddDays(-8),
                    CreatedAt = now.AddDays(-10),
                    UpdatedAt = now.AddDays(-2)
                },
                new Order
                {
                    Id = 3,
                    UserId = 7, // Farmer 3
                    OrderNumber = "ORD-2024-003",
                    TotalAmount = 160000m,
                    Status = "processing",
                    ShippingAddress = "Ấp 3, Xã Vĩnh Hậu, Huyện Tân Hưng, Tỉnh Long An",
                    PaymentMethod = "cod",
                    PaymentStatus = "pending",
                    CreatedAt = now.AddDays(-3),
                    UpdatedAt = now.AddDays(-1)
                },
                new Order
                {
                    Id = 4,
                    UserId = 8, // Farmer 4
                    OrderNumber = "ORD-2024-004",
                    TotalAmount = 95000m,
                    Status = "pending",
                    ShippingAddress = "Ấp 4, Xã Đức Hòa, Huyện Đức Hòa, Tỉnh Long An",
                    PaymentMethod = "cod",
                    PaymentStatus = "pending",
                    CreatedAt = now.AddDays(-1),
                    UpdatedAt = now.AddDays(-1)
                }
            );

            // Seed OrderItems
            modelBuilder.Entity<OrderItem>().HasData(
                // Order 1 items
                new OrderItem
                {
                    Id = 1,
                    OrderId = 1,
                    ProductId = 1, // Hạt giống lúa ST25
                    Quantity = 3,
                    UnitPrice = 42000m,
                    TotalPrice = 126000m
                },
                new OrderItem
                {
                    Id = 2,
                    OrderId = 1,
                    ProductId = 2, // Hạt giống rau cải
                    Quantity = 1,
                    UnitPrice = 25000m,
                    TotalPrice = 25000m
                },
                new OrderItem
                {
                    Id = 3,
                    OrderId = 1,
                    ProductId = 4, // Phân NPK
                    Quantity = 2,
                    UnitPrice = 18000m,
                    TotalPrice = 36000m
                },
                new OrderItem
                {
                    Id = 4,
                    OrderId = 1,
                    ProductId = 9, // Cuốc xới đất
                    Quantity = 1,
                    UnitPrice = 75000m,
                    TotalPrice = 75000m
                },
                // Order 2 items
                new OrderItem
                {
                    Id = 5,
                    OrderId = 2,
                    ProductId = 10, // Máy cắt cỏ Honda
                    Quantity = 1,
                    UnitPrice = 3300000m,
                    TotalPrice = 3300000m
                },
                new OrderItem
                {
                    Id = 6,
                    OrderId = 2,
                    ProductId = 4, // Phân NPK
                    Quantity = 5,
                    UnitPrice = 18000m,
                    TotalPrice = 90000m
                },
                new OrderItem
                {
                    Id = 7,
                    OrderId = 2,
                    ProductId = 5, // Phân hữu cơ
                    Quantity = 3,
                    UnitPrice = 20000m,
                    TotalPrice = 60000m
                },
                // Order 3 items
                new OrderItem
                {
                    Id = 8,
                    OrderId = 3,
                    ProductId = 3, // Hạt giống cà chua
                    Quantity = 5,
                    UnitPrice = 32000m,
                    TotalPrice = 160000m
                },
                // Order 4 items
                new OrderItem
                {
                    Id = 9,
                    OrderId = 4,
                    ProductId = 7, // Thuốc diệt cỏ
                    Quantity = 1,
                    UnitPrice = 95000m,
                    TotalPrice = 95000m
                }
            );
        }
    }
}