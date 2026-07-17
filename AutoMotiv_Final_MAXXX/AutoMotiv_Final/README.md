# AutoMotiv – Web Bán Xe Ô Tô

ASP.NET Core 8 MVC + Entity Framework Core + SQL Server

---

## ✅ Cách chạy 3 lệnh (chạy TẤT CẢ từ thư mục chứa Web.csproj)

```powershell
# Bước 1 – Đặt đúng thư mục (thư mục chứa Web.csproj)
cd AutoMotiveSolution

# Bước 2 – Cài dotnet-ef tool (chỉ cần làm 1 lần)
dotnet tool install --global dotnet-ef

# Bước 3 – 3 lệnh chính (KHÔNG cần flag gì thêm)
dotnet ef migrations add InitialCreate
dotnet ef database update
dotnet publish -c Release -o ./publish
```

---

## 🗄️ Cấu hình Database

### Local (SQL Server LocalDB)
File `appsettings.Development.json` – đã cấu hình sẵn, chạy ngay:
```json
"DefaultConnection": "Server=(localdb)\\MSSQLLocalDB;Database=AutoMotivDb;Trusted_Connection=True;TrustServerCertificate=True"
```
> Dùng SQL Server Express? Đổi `(localdb)\\MSSQLLocalDB` → `localhost\\SQLEXPRESS`

### Somee.com (Production)
Mở `appsettings.json`, thay `YOUR_DB_USER` và `YOUR_DB_PASS`:
```json
"DefaultConnection": "workstation id=...;user id=YOUR_DB_USER;pwd=YOUR_DB_PASS;..."
```

---

## 👤 Tài khoản mặc định
| Username | Password | Quyền |
|----------|----------|-------|
| `admin` | `admin123` | Quản trị viên |
| `customer` | `admin123` | Khách hàng |

---

## 🚗 Phân khúc xe
`Sedan` · `SUV` · `MPV` · `Hatchback` · `Pickup` · `Crossover` · `Xe điện`

---

## 📦 Upload lên Somee.com
1. Chạy `dotnet publish -c Release -o ./publish`
2. Upload **toàn bộ** thư mục `publish/` lên Somee File Manager
3. Đặt **Application Root** = thư mục `publish/`
4. Cập nhật connection string trong `appsettings.json`
