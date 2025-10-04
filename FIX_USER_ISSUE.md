# HƯỚNG DẪN SỬA LỖI: USERS KHÔNG XUẤT HIỆN TRONG SECURITY FOLDER

## VẤN ĐỀ
Sau khi chạy file `data_mau.sql`, dữ liệu được thêm vào bảng `NguoiDung` và `NhanVien` nhưng **không thấy users trong Security > Users** của database.

## NGUYÊN NHÂN
File `data_mau.sql` đã **override** stored procedure `sp_TaoTaiKhoanDayDu` với version đơn giản thiếu logic tạo Database User WITHOUT LOGIN. Version gốc trong file `04_StoredProcedures_Advanced.sql` có đầy đủ logic này.

## GIẢI PHÁP ĐÃ SỬA
Đã xóa phần override trong `data_mau.sql` (dòng 83-155) để sử dụng stored procedure gốc từ file `04_StoredProcedures_Advanced.sql`.

## CÁCH CHẠY LẠI

### Bước 1: Xóa database cũ (nếu muốn bắt đầu lại từ đầu)
```sql
USE master;
GO
IF DB_ID(N'QLNhanSuSieuThiMini') IS NOT NULL
BEGIN
    ALTER DATABASE QLNhanSuSieuThiMini SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE QLNhanSuSieuThiMini;
END
GO
CREATE DATABASE QLNhanSuSieuThiMini;
GO
```

### Bước 2: Chạy lại các file theo thứ tự
```powershell
# Trong SQL Server Management Studio (SSMS), chạy lần lượt:
1. 01_TaoDatabase.sql
2. 02_ChucNang.sql
3. 03_StoredProcedures.sql
4. 04_StoredProcedures_Advanced.sql
5. 05_Security_Triggers.sql
6. data_mau.sql  # ✅ File này đã được sửa
```

### Bước 3: Kiểm tra kết quả
Sau khi chạy xong, kiểm tra:

1. **Bảng NguoiDung có dữ liệu:**
```sql
USE QLNhanSuSieuThiMini;
SELECT * FROM dbo.NguoiDung;
```

2. **Database Users đã được tạo:**
```sql
-- Kiểm tra trong Security > Users của database
SELECT 
    name AS UserName,
    type_desc AS UserType,
    create_date AS CreateDate
FROM sys.database_principals
WHERE type = 'S'  -- S = SQL User
  AND name NOT IN ('dbo', 'guest', 'INFORMATION_SCHEMA', 'sys')
ORDER BY name;
```

3. **Users đã được gán vào roles:**
```sql
SELECT 
    dp.name AS UserName,
    r.name AS RoleName
FROM sys.database_role_members drm
JOIN sys.database_principals dp ON drm.member_principal_id = dp.principal_id
JOIN sys.database_principals r ON drm.role_principal_id = r.principal_id
WHERE dp.type = 'S'
ORDER BY dp.name, r.name;
```

## KẾT QUẢ MONG ĐỢI

Sau khi chạy xong, bạn sẽ thấy:

### 1. Trong Security > Users của database:
- `giamdoc` (role: r_quanly)
- `hr_manager` (role: r_hr)
- `ketoan01` (role: r_ketoan)
- `nv_hr_01` (role: r_nhanvien)
- `nv_banhang_01` (role: r_nhanvien)
- `nv_banhang_02` (role: r_nhanvien)
- `nv_kho_01` (role: r_nhanvien)
- `nv_thungan_01` (role: r_nhanvien)
- `nv_nghiviec` (role: r_nhanvien)

### 2. Trong bảng NguoiDung:
9 records với các username tương ứng

### 3. Trong bảng NhanVien:
9 records với thông tin nhân viên đầy đủ

## LƯU Ý QUAN TRỌNG

### Về Database Users WITHOUT LOGIN
- **KHÔNG** tạo SQL Server Login (không xuất hiện trong Security > Logins của server)
- **CHỈ** tạo Database User WITHOUT LOGIN (xuất hiện trong Security > Users của database)
- Users này được gán vào roles (r_hr, r_quanly, r_ketoan, r_nhanvien)
- Không cần quyền sysadmin để tạo

### Về ứng dụng C#
Ứng dụng C# **KHÔNG** sử dụng Database Users này để login. Thay vào đó:
1. Ứng dụng kết nối bằng connection string với tài khoản có quyền cao
2. Kiểm tra username/password trong bảng `NguoiDung`
3. Phân quyền dựa trên cột `VaiTro` trong bảng `NguoiDung`

Database Users WITHOUT LOGIN chỉ để:
- Hiển thị trong Security folder (yêu cầu của bài tập)
- Có thể áp dụng Row-Level Security nếu cần trong tương lai
- Tuân thủ best practices về security architecture

## KIỂM TRA NHANH

Chạy query này để xem tổng quan:
```sql
USE QLNhanSuSieuThiMini;

PRINT N'=== KIỂM TRA DỮ LIỆU ===';
PRINT N'';

PRINT N'1. Người dùng trong bảng:';
SELECT TenDangNhap, VaiTro, KichHoat FROM dbo.NguoiDung;

PRINT N'';
PRINT N'2. Database Users:';
SELECT name, type_desc, create_date 
FROM sys.database_principals 
WHERE type = 'S' AND name NOT IN ('dbo', 'guest', 'INFORMATION_SCHEMA', 'sys');

PRINT N'';
PRINT N'3. User-Role Mapping:';
SELECT 
    dp.name AS UserName,
    r.name AS RoleName
FROM sys.database_role_members drm
JOIN sys.database_principals dp ON drm.member_principal_id = dp.principal_id
JOIN sys.database_principals r ON drm.role_principal_id = r.principal_id
WHERE dp.type = 'S'
ORDER BY dp.name;
```

## HỖ TRỢ

Nếu vẫn gặp lỗi, kiểm tra:
1. User đang chạy script có quyền `db_owner` hoặc `dbo` trong database
2. Stored procedure `sp_TaoTaiKhoanDayDu` có clause `WITH EXECUTE AS 'dbo'`
3. Không có lỗi trong Messages tab của SSMS khi chạy `data_mau.sql`
