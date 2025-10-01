# 🚀 CÀI ĐẶT NHANH - BẢO MẬT 2 LỚP

## 📦 Tổng Quan

Hệ thống bảo mật 2 lớp đã được triển khai thành công với các thành phần:

### Phía SQL Server:
- ✅ 4 Stored Procedures quản lý tài khoản đầy đủ
- ✅ Tự động tạo SQL Login + Database User + Role
- ✅ Đồng bộ mật khẩu, vai trò, trạng thái kích hoạt

### Phía Ứng Dụng C#:
- ✅ `GlobalState.cs` - Lưu chuỗi kết nối động
- ✅ `frmLogin.cs` - Đăng nhập với SQL Authentication
- ✅ Mỗi user có connection string riêng

---

## ⚡ CÀI ĐẶT NHANH (5 BƯỚC)

### Bước 1: Chạy Script SQL

Mở SQL Server Management Studio và chạy các file theo thứ tự:

```sql
-- 1. Tạo database và bảng (nếu chưa có)
:r "01_TaoDatabase.sql"

-- 2. Tạo views và functions (nếu chưa có)
:r "02_ChucNang.sql"

-- 3. Tạo stored procedures cơ bản (nếu chưa có)
:r "03_StoredProcedures.sql"

-- 4. Tạo stored procedures nâng cao (ĐÃ BỔ SUNG BAO MẬT 2 LỚP)
:r "04_StoredProcedures_Advanced.sql"

-- 5. Tạo security và triggers (nếu chưa có)
:r "05_Security_Triggers.sql"
```

### Bước 2: Tạo Tài Khoản Demo

```sql
-- Chạy file demo để tạo 5 tài khoản mẫu
:r "DEMO_TAO_TAI_KHOAN.sql"
```

Kết quả: 5 tài khoản đã được tạo:
- `hr_mai` / `HR@2024` (HR)
- `quanly_nam` / `QL@2024` (Quản lý)
- `ketoan_hoa` / `KT@2024` (Kế toán)
- `nhanvien_binh` / `NV@2024` (Nhân viên)
- `nhanvien_lan` / `NV@2024` (Nhân viên)

### Bước 3: Build Ứng Dụng

1. Mở project trong Visual Studio
2. Kiểm tra các file mới đã được thêm:
   - `GlobalState.cs` ✅
   - `frmLogin.cs` (đã cập nhật) ✅
   - `UserSession.cs` (đã cập nhật) ✅

3. Build project: `Ctrl+Shift+B`

### Bước 4: Cấu Hình Server Name (nếu cần)

Nếu SQL Server không phải `localhost`, cập nhật trong `GlobalState.cs`:

```csharp
public static string ServerName { get; set; } = "YOUR_SERVER_NAME";
// Ví dụ: "DESKTOP-ABC\SQLEXPRESS"
```

Hoặc cập nhật trong `App.config` (chỉ để tham khảo, không bắt buộc):

```xml
<connectionStrings>
    <add name="HrDb"
         connectionString="Data Source=YOUR_SERVER_NAME;Initial Catalog=QLNhanSuSieuThiMini;..."
         providerName="System.Data.SqlClient" />
</connectionStrings>
```

### Bước 5: Chạy và Test

1. Khởi động ứng dụng
2. Đăng nhập với tài khoản HR:
   - **Username:** `hr_mai`
   - **Password:** `HR@2024`
3. Kiểm tra các chức năng theo quyền

---

## 🔍 KIỂM TRA HỆ THỐNG

### Kiểm Tra SQL Logins

```sql
-- Liệt kê tất cả SQL Logins đã tạo
SELECT 
    name AS [Tên Đăng Nhập],
    CASE is_disabled WHEN 0 THEN N'✓ Hoạt động' ELSE N'✗ Bị khóa' END AS [Trạng Thái],
    create_date AS [Ngày Tạo]
FROM sys.server_principals
WHERE type = 'S'
  AND name LIKE '%hr_%' OR name LIKE '%quanly_%' 
  OR name LIKE '%ketoan_%' OR name LIKE '%nhanvien_%'
ORDER BY name;
```

### Kiểm Tra Database Users và Roles

```sql
-- Xem User và Role membership
SELECT 
    dp.name AS [User],
    STRING_AGG(USER_NAME(drm.role_principal_id), ', ') AS [Roles]
FROM sys.database_principals dp
LEFT JOIN sys.database_role_members drm ON dp.principal_id = drm.member_principal_id
WHERE dp.type = 'S'
  AND dp.name NOT LIKE '##%'
GROUP BY dp.name
ORDER BY dp.name;
```

### Kiểm Tra Ứng Dụng

1. **Test đăng nhập thành công:**
   - Đăng nhập với `hr_mai` / `HR@2024`
   - Kiểm tra menu có đầy đủ quyền HR

2. **Test đăng nhập sai mật khẩu:**
   - Đăng nhập với `hr_mai` / `WrongPassword`
   - Phải hiển thị: "Tên đăng nhập hoặc mật khẩu không đúng"

3. **Test tài khoản bị khóa:**
   ```sql
   -- Khóa tài khoản
   EXEC dbo.sp_VoHieuHoaTaiKhoan @MaNV = (SELECT MaNV FROM NhanVien nv JOIN NguoiDung nd ON nv.MaNguoiDung = nd.MaNguoiDung WHERE nd.TenDangNhap = 'nhanvien_binh'), @KichHoat = 0;
   ```
   - Đăng nhập với `nhanvien_binh` / `NV@2024`
   - Phải hiển thị: "Tài khoản đã bị khóa"

---

## 📚 TÀI LIỆU CHI TIẾT

- **HUONG_DAN_BAO_MAT_2_LOP.md**: Tài liệu đầy đủ về kiến trúc, cài đặt, sử dụng
- **DEMO_TAO_TAI_KHOAN.sql**: Script tạo tài khoản demo
- **04_StoredProcedures_Advanced.sql**: Stored procedures quản lý tài khoản

---

## 🛠️ CÁC THAO TÁC THƯỜNG DÙNG

### Tạo Tài Khoản Mới

```sql
DECLARE @MaNV INT;

EXEC dbo.sp_TaoTaiKhoanDayDu
    @HoTen = N'Họ Tên Nhân Viên',
    @NgaySinh = '2000-01-01',
    @GioiTinh = N'Nam',
    @DienThoai = '0900000000',
    @Email = 'email@company.com',
    @DiaChi = N'Địa chỉ',
    @NgayVaoLam = GETDATE(),
    @MaPhongBan = 1,
    @MaChucVu = 3,
    @LuongCoBan = 8000000,
    @TenDangNhap = 'username',
    @MatKhau = 'Password@123',
    @VaiTro = N'NhanVien',  -- HR / QuanLy / KeToan / NhanVien
    @MaNV_OUT = @MaNV OUTPUT;

SELECT @MaNV AS MaNhanVienMoi;
```

### Đổi Mật Khẩu

```sql
EXEC dbo.sp_CapNhatTaiKhoanDayDu
    @MaNV = 10,
    @HoTen = N'Tên hiện tại',
    @NgaySinh = '2000-01-01',
    @GioiTinh = N'Nam',
    @DienThoai = '0900000000',
    @Email = 'email@company.com',
    @DiaChi = N'Địa chỉ',
    @MaPhongBan = 1,
    @MaChucVu = 3,
    @LuongCoBan = 8000000,
    @VaiTro = N'NhanVien',
    @MatKhauMoi = 'NewPassword@2024';  -- ← Mật khẩu mới
```

### Đổi Vai Trò

```sql
EXEC dbo.sp_CapNhatTaiKhoanDayDu
    @MaNV = 10,
    -- ... (các thông tin khác giữ nguyên)
    @VaiTro = N'QuanLy',  -- ← Đổi từ NhanVien sang QuanLy
    @MatKhauMoi = NULL;   -- Không đổi mật khẩu
```

### Khóa/Mở Khóa Tài Khoản

```sql
-- Khóa
EXEC dbo.sp_VoHieuHoaTaiKhoan @MaNV = 10, @KichHoat = 0;

-- Mở khóa
EXEC dbo.sp_VoHieuHoaTaiKhoan @MaNV = 10, @KichHoat = 1;
```

### Xóa Tài Khoản

```sql
EXEC dbo.sp_XoaTaiKhoanDayDu @MaNV = 10;
```

---

## ⚠️ LƯU Ý QUAN TRỌNG

### 1. Yêu Cầu Mật Khẩu Mạnh

Mật khẩu phải thỏa mãn:
- Ít nhất 8 ký tự
- Có chữ HOA
- Có chữ thường
- Có số
- Có ký tự đặc biệt (@, #, $, %, &)

**Ví dụ hợp lệ:** `Pass@123`, `HR@2024`, `Admin#2024`

### 2. SQL Server Authentication

Đảm bảo SQL Server đã bật **Mixed Mode Authentication**:

1. Mở SSMS
2. Right-click Server → Properties
3. Security → SQL Server and Windows Authentication mode
4. Restart SQL Server Service

### 3. Firewall

Nếu ứng dụng kết nối từ máy khác, mở port 1433:

```powershell
# Windows Firewall
New-NetFirewallRule -DisplayName "SQL Server" -Direction Inbound -LocalPort 1433 -Protocol TCP -Action Allow
```

### 4. Backup Định Kỳ

```sql
-- Backup database
BACKUP DATABASE QLNhanSuSieuThiMini 
TO DISK = 'C:\Backup\QLNhanSuSieuThiMini.bak' 
WITH FORMAT;

-- Backup master (chứa Login information)
BACKUP DATABASE master 
TO DISK = 'C:\Backup\master.bak' 
WITH FORMAT;
```

---

## 🐛 XỬ LÝ SỰ CỐ

### Lỗi: "Login failed for user 'username'"

**Nguyên nhân:** Sai mật khẩu hoặc Login không tồn tại

**Giải pháp:**
```sql
-- Kiểm tra Login tồn tại
SELECT name, is_disabled FROM sys.server_principals WHERE name = 'username';

-- Nếu không tồn tại, tạo lại bằng sp_TaoTaiKhoanDayDu
-- Nếu có nhưng bị disable, enable lại
ALTER LOGIN [username] ENABLE;
```

### Lỗi: "Cannot open database"

**Nguyên nhân:** Database User không được map với Login

**Giải pháp:**
```sql
USE QLNhanSuSieuThiMini;
GO

-- Tạo User và map với Login
CREATE USER [username] FOR LOGIN [username];

-- Thêm vào Role
ALTER ROLE r_nhanvien ADD MEMBER [username];
```

### Lỗi: "The user does not have permission"

**Nguyên nhân:** User không có quyền trên bảng/SP

**Giải pháp:**
```sql
-- Kiểm tra Role membership
SELECT 
    dp.name AS UserName,
    STRING_AGG(USER_NAME(drm.role_principal_id), ', ') AS Roles
FROM sys.database_principals dp
LEFT JOIN sys.database_role_members drm ON dp.principal_id = drm.member_principal_id
WHERE dp.name = 'username'
GROUP BY dp.name;

-- Thêm vào Role phù hợp
ALTER ROLE r_nhanvien ADD MEMBER [username];
```

---

## 📞 HỖ TRỢ

Nếu gặp vấn đề, kiểm tra:

1. ✅ Đã chạy đầy đủ 5 file SQL chưa?
2. ✅ SQL Server đã bật Mixed Mode Authentication chưa?
3. ✅ Tài khoản sa có thể kết nối được không?
4. ✅ File `GlobalState.cs` đã được thêm vào project chưa?
5. ✅ Server name trong `GlobalState.cs` có đúng không?

---

**Tác giả:** Vũ Toàn Thắng - 23110329  
**Ngày cập nhật:** 02/10/2025  
**Phiên bản:** 2.0 - Two-Layer Security Model
