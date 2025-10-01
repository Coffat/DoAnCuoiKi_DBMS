# 🔐 HƯỚNG DẪN TRIỂN KHAI BẢO MẬT 2 LỚP

## Tổng Quan

Tài liệu này hướng dẫn cách triển khai **mô hình bảo mật 2 lớp** cho hệ thống quản lý nhân sự, nơi mỗi tài khoản người dùng có:

1. **Lớp Application**: Bản ghi trong bảng `NguoiDung`
2. **Lớp Database**: SQL Server Login + Database User + Role Membership

## 🏗️ Kiến Trúc Hệ Thống

```
┌─────────────────────────────────────────────────────┐
│           NGƯỜI DÙNG ĐĂNG NHẬP                      │
│       (Username: nhanvien01, Password: ****)        │
└──────────────────┬──────────────────────────────────┘
                   │
                   ▼
┌─────────────────────────────────────────────────────┐
│         LỚP 1: XÁC THỰC SQL SERVER                  │
│  ┌─────────────────────────────────────────────┐   │
│  │ SQL Server Login Authentication             │   │
│  │ - Kiểm tra Login tồn tại                    │   │
│  │ - Xác thực mật khẩu                        │   │
│  │ - Login disabled/enabled?                   │   │
│  └─────────────────────────────────────────────┘   │
└──────────────────┬──────────────────────────────────┘
                   │ ✓ Đăng nhập thành công
                   ▼
┌─────────────────────────────────────────────────────┐
│         LỚP 2: PHÂN QUYỀN APPLICATION               │
│  ┌─────────────────────────────────────────────┐   │
│  │ Database User & Role Membership             │   │
│  │ - Kiểm tra bảng NguoiDung                   │   │
│  │ - Kiểm tra KichHoat = 1                     │   │
│  │ - Lấy VaiTro (HR/QuanLy/KeToan/NhanVien)   │   │
│  │ - Áp dụng quyền theo Role                   │   │
│  └─────────────────────────────────────────────┘   │
└──────────────────┬──────────────────────────────────┘
                   │
                   ▼
┌─────────────────────────────────────────────────────┐
│    TẤT CẢ TRUY VẤN CHẠY VỚI QUYỀN CỦA USER          │
│         (Connection String động)                    │
└─────────────────────────────────────────────────────┘
```

---

## 📋 PHẦN 1: PHÍA SQL SERVER

### Bước 1: Chạy Script Tạo Stored Procedures

File: `04_StoredProcedures_Advanced.sql` (đã được bổ sung)

Các stored procedures mới:

#### 1.1. `sp_TaoTaiKhoanDayDu` - Tạo Tài Khoản Hoàn Chỉnh

```sql
EXEC dbo.sp_TaoTaiKhoanDayDu
    @HoTen = N'Nguyễn Văn A',
    @NgaySinh = '1990-01-15',
    @GioiTinh = N'Nam',
    @DienThoai = '0912345678',
    @Email = 'nguyenvana@company.com',
    @DiaChi = N'123 Đường ABC, TP.HCM',
    @NgayVaoLam = '2024-01-01',
    @MaPhongBan = 1,
    @MaChucVu = 2,
    @LuongCoBan = 10000000,
    @TenDangNhap = 'nhanvien01',
    @MatKhau = 'Pass@123',  -- Mật khẩu gốc
    @VaiTro = N'NhanVien',
    @MaNV_OUT = NULL;
```

**Thao tác thực hiện:**
1. Tạo bản ghi trong `NguoiDung` với hash SHA2_256
2. Tạo bản ghi trong `NhanVien`
3. Tạo SQL Server Login: `CREATE LOGIN [nhanvien01] WITH PASSWORD = 'Pass@123'`
4. Tạo Database User: `CREATE USER [nhanvien01] FOR LOGIN [nhanvien01]`
5. Thêm vào Role: `ALTER ROLE r_nhanvien ADD MEMBER [nhanvien01]`

#### 1.2. `sp_CapNhatTaiKhoanDayDu` - Cập Nhật Tài Khoản

```sql
EXEC dbo.sp_CapNhatTaiKhoanDayDu
    @MaNV = 10,
    @HoTen = N'Nguyễn Văn A (Updated)',
    @NgaySinh = '1990-01-15',
    @GioiTinh = N'Nam',
    @DienThoai = '0912345678',
    @Email = 'nguyenvana@company.com',
    @DiaChi = N'456 Đường XYZ, TP.HCM',
    @MaPhongBan = 2,
    @MaChucVu = 3,
    @LuongCoBan = 12000000,
    @VaiTro = N'QuanLy',  -- Đổi vai trò từ NhanVien -> QuanLy
    @MatKhauMoi = 'NewPass@456';  -- Đổi mật khẩu (hoặc NULL nếu không đổi)
```

**Thao tác thực hiện:**
1. Cập nhật bảng `NhanVien` và `NguoiDung`
2. Nếu có `@MatKhauMoi`: `ALTER LOGIN [nhanvien01] WITH PASSWORD = 'NewPass@456'`
3. Nếu đổi vai trò:
   - Xóa khỏi Role cũ: `ALTER ROLE r_nhanvien DROP MEMBER [nhanvien01]`
   - Thêm vào Role mới: `ALTER ROLE r_quanly ADD MEMBER [nhanvien01]`

#### 1.3. `sp_XoaTaiKhoanDayDu` - Xóa Tài Khoản Hoàn Toàn

```sql
EXEC dbo.sp_XoaTaiKhoanDayDu @MaNV = 10;
```

**Thao tác thực hiện:**
1. Xóa Database User: `DROP USER [nhanvien01]`
2. Xóa SQL Login: `DROP LOGIN [nhanvien01]`
3. Xóa bản ghi trong `NhanVien` và `NguoiDung`

#### 1.4. `sp_VoHieuHoaTaiKhoan` - Vô Hiệu Hóa/Kích Hoạt

```sql
-- Vô hiệu hóa tài khoản
EXEC dbo.sp_VoHieuHoaTaiKhoan @MaNV = 10, @KichHoat = 0;

-- Kích hoạt lại tài khoản
EXEC dbo.sp_VoHieuHoaTaiKhoan @MaNV = 10, @KichHoat = 1;
```

**Thao tác thực hiện:**
1. Cập nhật `NguoiDung.KichHoat`
2. `ALTER LOGIN [nhanvien01] ENABLE/DISABLE`

---

## 💻 PHẦN 2: PHÍA ỨNG DỤNG WINFORMS

### Bước 2: Các Class Đã Được Tạo

#### 2.1. `GlobalState.cs` - Lưu Trữ Chuỗi Kết Nối Động

```csharp
public static class GlobalState
{
    public static string ConnectionString { get; set; }
    public static string ServerName { get; set; } = "localhost";
    public static string DatabaseName { get; set; } = "QLNhanSuSieuThiMini";
    
    public static void Clear() { ... }
    public static bool HasConnection() { ... }
}
```

#### 2.2. `frmLogin.cs` - Đăng Nhập với Xác Thực Động

**Luồng đăng nhập mới:**

```csharp
private void btnLogin_Click(object sender, EventArgs e)
{
    string username = txtUsername.Text.Trim();
    string password = txtPassword.Text;
    
    // TẠO CHUỖI KẾT NỐI ĐỘNG
    SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
    builder.DataSource = GlobalState.ServerName;
    builder.InitialCatalog = GlobalState.DatabaseName;
    builder.UserID = username;  // ← Sử dụng username người dùng nhập
    builder.Password = password;  // ← Sử dụng password người dùng nhập
    builder.TrustServerCertificate = true;
    
    string dynamicConnectionString = builder.ConnectionString;
    
    using (SqlConnection conn = new SqlConnection(dynamicConnectionString))
    {
        try
        {
            conn.Open();  // ← Nếu thành công = SQL Login hợp lệ
            
            // Lấy thông tin từ bảng NguoiDung
            string query = @"SELECT nd.MaNguoiDung, nd.VaiTro, nd.KichHoat, 
                                    nv.MaNV, nv.HoTen 
                             FROM dbo.NguoiDung nd
                             LEFT JOIN dbo.NhanVien nv ON nd.MaNguoiDung = nv.MaNguoiDung
                             WHERE nd.TenDangNhap = @username";
            
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@username", username);
            SqlDataReader reader = cmd.ExecuteReader();
            
            if (reader.Read())
            {
                bool kichHoat = reader.GetBoolean(2);
                if (!kichHoat)
                {
                    MessageBox.Show("Tài khoản đã bị khóa.");
                    return;
                }
                
                // LƯU CHUỖI KẾT NỐI VÀO GLOBALSTATE
                GlobalState.ConnectionString = dynamicConnectionString;
                
                // Lưu thông tin user
                UserSession.SetUser(maNguoiDung, maNV, username, hoTen, vaiTro);
                
                // Mở form chính
                frmMain mainForm = new frmMain(vaiTro);
                this.Hide();
                mainForm.ShowDialog();
                this.Close();
            }
        }
        catch (SqlException ex)
        {
            if (ex.Number == 18456)  // Login failed
            {
                MessageBox.Show("Tên đăng nhập hoặc mật khẩu không đúng.");
            }
        }
    }
}
```

### Bước 3: Cập Nhật Các Form Khác

**TẤT CẢ các form khác cần đổi từ:**

```csharp
// CŨ: Đọc từ App.config
string connectionString = ConfigurationManager.ConnectionStrings["HrDb"].ConnectionString;
```

**Thành:**

```csharp
// MỚI: Đọc từ GlobalState
string connectionString = GlobalState.ConnectionString;
```

**Ví dụ trong `frmNhanVien.cs`:**

```csharp
private void LoadData()
{
    // Thay vì đọc từ App.config
    // string connString = ConfigurationManager.ConnectionStrings["HrDb"].ConnectionString;
    
    // Dùng chuỗi kết nối động
    string connString = GlobalState.ConnectionString;
    
    using (SqlConnection conn = new SqlConnection(connString))
    {
        // ... code như cũ
    }
}
```

---

## 🔄 PHẦN 3: QUY TRÌNH SỬ DỤNG

### 3.1. Tạo Tài Khoản Mới

**Trong SQL Server Management Studio:**

```sql
DECLARE @MaNV INT;

EXEC dbo.sp_TaoTaiKhoanDayDu
    @HoTen = N'Trần Thị B',
    @NgaySinh = '1995-06-20',
    @GioiTinh = N'Nu',
    @DienThoai = '0987654321',
    @Email = 'tranthib@company.com',
    @DiaChi = N'789 Đường DEF, Hà Nội',
    @NgayVaoLam = '2024-06-01',
    @MaPhongBan = 2,
    @MaChucVu = 1,
    @LuongCoBan = 8000000,
    @TenDangNhap = 'hr_tranthib',
    @MatKhau = 'HR@2024',
    @VaiTro = N'HR',
    @MaNV_OUT = @MaNV OUTPUT;

SELECT @MaNV AS MaNhanVienMoi;
```

**Kết quả:**
- Tạo Login: `hr_tranthib` với password `HR@2024`
- Tạo User: `hr_tranthib` trong database `QLNhanSuSieuThiMini`
- Thêm vào Role: `r_hr`
- Người dùng có thể đăng nhập vào ứng dụng với username `hr_tranthib` và password `HR@2024`

### 3.2. Đăng Nhập Vào Ứng Dụng

1. Mở ứng dụng WinForms
2. Nhập:
   - **Username:** `hr_tranthib`
   - **Password:** `HR@2024`
3. Click **Đăng nhập**

**Điều gì xảy ra:**
- Ứng dụng tạo chuỗi kết nối với `UserID=hr_tranthib, Password=HR@2024`
- SQL Server xác thực Login
- Ứng dụng kiểm tra bảng `NguoiDung` → lấy vai trò `HR`
- Lưu chuỗi kết nối vào `GlobalState.ConnectionString`
- Mở `frmMain` với quyền HR

### 3.3. Đổi Mật Khẩu

```sql
EXEC dbo.sp_CapNhatTaiKhoanDayDu
    @MaNV = 15,
    @HoTen = N'Trần Thị B',
    @NgaySinh = '1995-06-20',
    @GioiTinh = N'Nu',
    @DienThoai = '0987654321',
    @Email = 'tranthib@company.com',
    @DiaChi = N'789 Đường DEF, Hà Nội',
    @MaPhongBan = 2,
    @MaChucVu = 1,
    @LuongCoBan = 8000000,
    @VaiTro = N'HR',
    @MatKhauMoi = 'NewHR@2025';  -- ← Mật khẩu mới
```

Lần sau đăng nhập phải dùng password mới: `NewHR@2025`

### 3.4. Khóa/Mở Khóa Tài Khoản

```sql
-- Khóa tài khoản (không cho đăng nhập)
EXEC dbo.sp_VoHieuHoaTaiKhoan @MaNV = 15, @KichHoat = 0;

-- Kích hoạt lại
EXEC dbo.sp_VoHieuHoaTaiKhoan @MaNV = 15, @KichHoat = 1;
```

Khi tài khoản bị khóa:
- SQL Login bị `DISABLE`
- Người dùng không thể đăng nhập vào SQL Server
- Nếu đã đăng nhập rồi, sẽ bị chặn khi kiểm tra `NguoiDung.KichHoat = 0`

---

## 🛡️ PHẦN 4: BẢO MẬT & ƯU ĐIỂM

### Ưu Điểm của Mô Hình 2 Lớp

#### 1. **Audit Trail Tự Động**
Mỗi thao tác trong database đều ghi nhận đúng người thực hiện:
```sql
SELECT 
    SUSER_SNAME() AS CurrentLoginName,  -- Tên SQL Login
    USER_NAME() AS CurrentDatabaseUser   -- Tên Database User
```

#### 2. **Phân Quyền Chặt Chẽ**
- Mỗi người dùng chỉ có quyền theo Role của mình
- Không cần kiểm tra quyền trong code C#
- SQL Server tự động chặn truy cập trái phép

#### 3. **Không Lưu Mật Khẩu sa trong App.config**
- File `App.config` chỉ chứa tên server và database
- Không có thông tin nhạy cảm
- Mỗi session có chuỗi kết nối riêng

#### 4. **Khả Năng Audit & Compliance**
- Có thể bật `SQL Server Audit` để ghi log tất cả thao tác
- Biết chính xác ai đã làm gì, khi nào
- Đáp ứng yêu cầu ISO 27001, SOC 2

### Bảo Mật Nâng Cao

#### 1. Encryption at Rest
Có thể bật TDE (Transparent Data Encryption) để mã hóa dữ liệu trên đĩa.

#### 2. Encryption in Transit
Sử dụng SSL/TLS cho kết nối SQL Server:
```csharp
builder.Encrypt = true;
builder.TrustServerCertificate = false;  // Trong môi trường production
```

#### 3. Row-Level Security
Có thể thêm RLS để người dùng chỉ thấy dữ liệu của chính họ:
```sql
CREATE SECURITY POLICY NhanVien_Policy
ADD FILTER PREDICATE dbo.fn_FilterNhanVien(MaNV)
ON dbo.NhanVien;
```

---

## ⚠️ PHẦN 5: LƯU Ý & GIẢI QUYẾT SỰ CỐ

### Lưu Ý Quan Trọng

1. **Mật khẩu phải đủ mạnh:**
   - Ít nhất 8 ký tự
   - Có chữ hoa, chữ thường, số, ký tự đặc biệt
   - Ví dụ: `Pass@123`, `HR@2024`

2. **Tài khoản sa vẫn cần thiết:**
   - Chỉ dùng để chạy các script khởi tạo/bảo trì
   - Không dùng trong ứng dụng thường ngày

3. **Backup định kỳ:**
   - Backup cả database lẫn master database (chứa Login)
   - Script backup Login:
   ```sql
   SELECT 'CREATE LOGIN ' + QUOTENAME(name) + ' WITH PASSWORD_HASH = ' + 
          sys.fn_varbintohexstr(password_hash) + ' HASHED;'
   FROM sys.sql_logins
   WHERE name LIKE '%nhanvien%' OR name LIKE '%hr_%';
   ```

### Xử Lý Sự Cố

#### Sự cố 1: Quên Mật Khẩu

**Giải pháp:** Dùng tài khoản sa để reset:

```sql
-- Kết nối bằng sa
ALTER LOGIN [nhanvien01] WITH PASSWORD = 'TempPass@123';

-- Thông báo cho user mật khẩu tạm
-- User đăng nhập và đổi lại mật khẩu mới
```

#### Sự cố 2: Login Bị Khóa Nhầm

```sql
-- Kiểm tra trạng thái
SELECT name, is_disabled FROM sys.server_principals WHERE name = 'nhanvien01';

-- Mở khóa
ALTER LOGIN [nhanvien01] ENABLE;
```

#### Sự cố 3: Orphaned User (User không có Login)

```sql
-- Kiểm tra
SELECT dp.name AS UserName, sp.name AS LoginName
FROM sys.database_principals dp
LEFT JOIN sys.server_principals sp ON dp.sid = sp.sid
WHERE dp.type = 'S' AND sp.name IS NULL;

-- Sửa
ALTER USER [nhanvien01] WITH LOGIN = [nhanvien01];
```

#### Sự cố 4: Không Thể Đăng Nhập

**Checklist:**
1. ✅ SQL Server Authentication đã bật chưa?
   ```sql
   -- Kiểm tra
   SELECT SERVERPROPERTY('IsIntegratedSecurityOnly');
   -- Nếu = 1 → chỉ Windows Auth
   -- Cần chuyển sang Mixed Mode
   ```

2. ✅ Login tồn tại không?
   ```sql
   SELECT name, is_disabled FROM sys.server_principals WHERE name = 'nhanvien01';
   ```

3. ✅ Database User tồn tại không?
   ```sql
   SELECT name FROM sys.database_principals WHERE name = 'nhanvien01';
   ```

4. ✅ User có trong Role không?
   ```sql
   SELECT USER_NAME(member_principal_id) AS UserName,
          USER_NAME(role_principal_id) AS RoleName
   FROM sys.database_role_members
   WHERE USER_NAME(member_principal_id) = 'nhanvien01';
   ```

---

## 📊 PHẦN 6: KIỂM TRA & GIÁM SÁT

### Kiểm Tra Tài Khoản Hiện Tại

```sql
-- Liệt kê tất cả Login đã tạo
SELECT 
    sp.name AS LoginName,
    sp.type_desc AS LoginType,
    sp.is_disabled AS IsDisabled,
    sp.create_date AS CreateDate,
    sp.modify_date AS LastModified
FROM sys.server_principals sp
WHERE sp.type = 'S'  -- SQL Authentication
  AND sp.name NOT LIKE '##%'  -- Loại bỏ system accounts
ORDER BY sp.name;

-- Liệt kê User và Role
SELECT 
    dp.name AS UserName,
    STRING_AGG(drm.role_principal_id, ',') AS RoleIDs,
    STRING_AGG(USER_NAME(drm.role_principal_id), ', ') AS RoleNames
FROM sys.database_principals dp
LEFT JOIN sys.database_role_members drm ON dp.principal_id = drm.member_principal_id
WHERE dp.type = 'S'
GROUP BY dp.name
ORDER BY dp.name;

-- Liệt kê quyền của từng User
SELECT 
    dp.name AS UserName,
    o.name AS ObjectName,
    p.permission_name,
    p.state_desc
FROM sys.database_permissions p
JOIN sys.database_principals dp ON p.grantee_principal_id = dp.principal_id
LEFT JOIN sys.objects o ON p.major_id = o.object_id
WHERE dp.type = 'S'
ORDER BY dp.name, o.name;
```

### Giám Sát Hoạt Động

```sql
-- Xem ai đang connect
SELECT 
    session_id,
    login_name,
    host_name,
    program_name,
    login_time,
    last_request_start_time
FROM sys.dm_exec_sessions
WHERE is_user_process = 1
  AND database_id = DB_ID('QLNhanSuSieuThiMini')
ORDER BY login_time DESC;

-- Xem câu lệnh đang chạy
SELECT 
    s.session_id,
    s.login_name,
    t.text AS QueryText,
    s.status,
    s.cpu_time,
    s.total_elapsed_time
FROM sys.dm_exec_sessions s
CROSS APPLY sys.dm_exec_sql_text(s.most_recent_sql_handle) t
WHERE s.is_user_process = 1
  AND s.database_id = DB_ID('QLNhanSuSieuThiMini');
```

---

## 🎯 PHẦN 7: KẾT LUẬN

### Tóm Tắt Quy Trình

1. **Khởi tạo:**
   - Chạy script SQL tạo stored procedures
   - Thêm `GlobalState.cs` và `UserSession.cs` vào project
   - Cập nhật `frmLogin.cs`

2. **Tạo tài khoản:**
   - Dùng `sp_TaoTaiKhoanDayDu` để tạo tài khoản mới
   - Thông báo username/password cho nhân viên

3. **Sử dụng:**
   - Nhân viên đăng nhập bằng username/password của họ
   - Mỗi thao tác trong database ghi nhận đúng người thực hiện
   - Quyền hạn được kiểm soát tự động bởi SQL Server

4. **Bảo trì:**
   - Dùng `sp_CapNhatTaiKhoanDayDu` để đổi thông tin/mật khẩu/vai trò
   - Dùng `sp_VoHieuHoaTaiKhoan` để khóa/mở khóa
   - Dùng `sp_XoaTaiKhoanDayDu` để xóa tài khoản

### Lợi Ích Đạt Được

✅ **Bảo mật:** Mỗi người dùng có credential riêng, không share account  
✅ **Audit:** Biết chính xác ai làm gì, khi nào  
✅ **Phân quyền:** Tự động theo Role, không cần code kiểm tra phức tạp  
✅ **Compliance:** Đáp ứng yêu cầu ISO 27001, SOC 2, GDPR  
✅ **Khôi phục:** Dễ dàng trace và rollback khi có sự cố  

### Tài Liệu Tham Khảo

- Microsoft Docs: SQL Server Security
- Microsoft Docs: Row-Level Security
- Microsoft Docs: Dynamic Data Masking
- OWASP: Database Security Cheat Sheet

---

**Tác giả:** Vũ Toàn Thắng - 23110329  
**Dự án:** Hệ Thống Quản Lý Nhân Sự Siêu Thị Mini  
**Ngày cập nhật:** 02/10/2025
