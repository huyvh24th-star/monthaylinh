using Core.Database.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Web.Data
{
    /// <summary>
    /// Factory dùng ở design-time để các lệnh sau chạy được KHÔNG cần flag:
    ///   dotnet ef migrations add InitialCreate
    ///   dotnet ef database update
    /// EF Core tự tìm class này khi chạy migration.
    /// </summary>
    public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            // Đọc connection string từ appsettings (ưu tiên Development)
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true)
                .AddJsonFile("appsettings.Development.json", optional: true)
                .AddEnvironmentVariables()
                .Build();

            var connectionString = config.GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException(
                    "Không tìm thấy 'DefaultConnection' trong appsettings.json. " +
                    "Kiểm tra file appsettings.Development.json.");

            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseSqlServer(connectionString);

            return new ApplicationDbContext(optionsBuilder.Options);
        }
    }
}
