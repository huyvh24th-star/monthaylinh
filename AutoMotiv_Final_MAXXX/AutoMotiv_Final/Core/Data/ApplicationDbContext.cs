using System;
using Core.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace Core.Database.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Products { get; set; } = null!;
        public DbSet<Article> Articles { get; set; } = null!;
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Staff> Staffs { get; set; } = null!;
        public DbSet<Order> Orders { get; set; } = null!;
        public DbSet<OrderDetail> OrderDetails { get; set; } = null!;
        public DbSet<Feedback> Feedbacks { get; set; } = null!;
        public DbSet<Post> Posts { get; set; } = null!;
        public DbSet<PostComment> PostComments { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Username)
                .IsUnique();

            modelBuilder.Entity<Order>()
                .HasOne(o => o.User)
                .WithMany(u => u.Orders)
                .HasForeignKey(o => o.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<OrderDetail>()
                .HasOne(od => od.Order)
                .WithMany(o => o.OrderDetails)
                .HasForeignKey(od => od.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<OrderDetail>()
                .HasOne(od => od.Product)
                .WithMany(p => p.OrderDetails)
                .HasForeignKey(od => od.ProductId)
                .OnDelete(DeleteBehavior.Restrict);

            // FIX: UserId nullable → OnDelete SetNull (không cascade xóa feedback khi xóa user)
            modelBuilder.Entity<Feedback>()
                .HasOne(f => f.User)
                .WithMany(u => u.Feedbacks)
                .HasForeignKey(f => f.UserId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Post>()
                .HasOne(p => p.User)
                .WithMany()
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<PostComment>()
                .HasOne(c => c.Post)
                .WithMany(p => p.Comments)
                .HasForeignKey(c => c.PostId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<PostComment>()
                .HasOne(c => c.User)
                .WithMany()
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            // ----- Seed Data -----
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    Username = "admin",
                    PasswordHash = "admin123",
                    FullName = "Quản trị viên",
                    Phone = "0817139878",
                    Email = "admin@automotiv.vn",
                    Role = "Admin",
                    CreatedDate = new DateTime(2024, 1, 1)
                },
                new User
                {
                    Id = 2,
                    Username = "customer",
                    PasswordHash = "admin123",
                    FullName = "Khách hàng demo",
                    Phone = "0900000000",
                    Email = "customer@automotiv.vn",
                    Role = "Customer",
                    CreatedDate = new DateTime(2024, 1, 1)
                }
            );

            modelBuilder.Entity<Product>().HasData(
                new Product { Id = 1, Name = "Toyota Camry 2.5Q", Description = "Sedan hạng D cao cấp, động cơ 2.5L, hộp số tự động 8 cấp.", Price = 1_235_000_000m, Type = "Sedan", Image = "https://images.unsplash.com/photo-1549317661-bd32c8ce0db2?q=80&w=1000&auto=format&fit=crop", Featured = true, Stock = 5, CreatedDate = new DateTime(2024, 1, 1) },
                new Product { Id = 2, Name = "Honda CR-V e:HEV", Description = "SUV hybrid thế hệ mới, động cơ 2.0L hybrid.", Price = 1_109_000_000m, Type = "SUV", Image = "https://images.unsplash.com/photo-1568844293986-ca047c6a55a4?q=80&w=1000&auto=format&fit=crop", Featured = true, Stock = 8, CreatedDate = new DateTime(2024, 1, 1) },
                new Product { Id = 3, Name = "Hyundai Tucson 2.0 AWD", Description = "SUV cỡ vừa với hệ dẫn động 4 bánh toàn thời gian.", Price = 889_000_000m, Type = "SUV", Image = "https://images.unsplash.com/photo-1533473359331-0135ef1b58bf?q=80&w=1000&auto=format&fit=crop", Featured = false, Stock = 6, CreatedDate = new DateTime(2024, 1, 1) },
                new Product { Id = 4, Name = "Kia Carnival 2.2D Premium", Description = "MPV 8 chỗ sang trọng, động cơ diesel 2.2L.", Price = 1_349_000_000m, Type = "MPV", Image = "https://images.unsplash.com/photo-1609521263047-f8f205293f24?q=80&w=1000&auto=format&fit=crop", Featured = true, Stock = 3, CreatedDate = new DateTime(2024, 1, 1) },
                new Product { Id = 5, Name = "Mazda CX-5 2.5 Signature", Description = "SUV premium với động cơ Skyactiv-G 2.5L.", Price = 979_000_000m, Type = "SUV", Image = "https://images.unsplash.com/photo-1606664515524-ed2f786a0bd6?q=80&w=1000&auto=format&fit=crop", Featured = false, Stock = 7, CreatedDate = new DateTime(2024, 1, 1) },
                new Product { Id = 6, Name = "Ford Ranger Wildtrak 2.0L", Description = "Pickup bán tải mạnh mẽ, động cơ EcoBlue 2.0L bi-turbo.", Price = 879_000_000m, Type = "Pickup", Image = "https://images.unsplash.com/photo-1558618666-fcd25c85cd64?q=80&w=1000&auto=format&fit=crop", Featured = false, Stock = 4, CreatedDate = new DateTime(2024, 1, 1) },
                new Product { Id = 7, Name = "Honda City 1.5 RS", Description = "Sedan hạng B thể thao, động cơ VTEC Turbo 1.5L.", Price = 599_000_000m, Type = "Sedan", Image = "https://images.unsplash.com/photo-1541899481282-d53bffe3c35d?q=80&w=1000&auto=format&fit=crop", Featured = true, Stock = 10, CreatedDate = new DateTime(2024, 1, 1) },
                new Product { Id = 8, Name = "VinFast VF 8 Plus", Description = "SUV điện thuần túy, phạm vi 420km/lần sạc.", Price = 1_090_000_000m, Type = "Điện", Image = "https://images.unsplash.com/photo-1593941707882-a5bba14938c7?q=80&w=1000&auto=format&fit=crop", Featured = true, Stock = 5, CreatedDate = new DateTime(2024, 1, 1) }
            );

            modelBuilder.Entity<Article>().HasData(
                new Article { Id = 1, Title = "Top 5 mẫu SUV bán chạy nhất Việt Nam 2024", Category = "Đánh giá xe", Image = "https://images.unsplash.com/photo-1533473359331-0135ef1b58bf?q=80&w=1000&auto=format&fit=crop", Content = "Phân khúc SUV tiếp tục thống trị thị trường ô tô Việt Nam trong năm 2024...", CreatedDate = new DateTime(2024, 3, 10) },
                new Article { Id = 2, Title = "AutoMotiv khai trương showroom mới tại Hà Nội", Category = "Tin tức", Image = "https://images.unsplash.com/photo-1503376780353-7e6692767b70?q=80&w=1000&auto=format&fit=crop", Content = "AutoMotiv vui mừng thông báo khai trương showroom thứ 3 tại Hà Nội...", CreatedDate = new DateTime(2024, 4, 2) },
                new Article { Id = 3, Title = "Chính sách vay mua xe ưu đãi – Lãi suất từ 6.5%/năm", Category = "Khuyến mãi", Image = "https://images.unsplash.com/photo-1579621970563-ebec7560ff3e?q=80&w=1000&auto=format&fit=crop", Content = "AutoMotiv hợp tác với 10 ngân hàng lớn mang đến gói vay mua xe với lãi suất ưu đãi chỉ từ 6.5%/năm...", CreatedDate = new DateTime(2024, 5, 15) }
            );
        }
    }
}
