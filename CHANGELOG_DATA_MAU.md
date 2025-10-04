# THAY ĐỔI FILE DATA_MAU.SQL

## Ngày: 2025-10-04

### ✅ Thay đổi 1: Mật khẩu đơn giản (Plain text - Không hash)
**Trước:** 
- Mật khẩu `123` 
- Được hash bằng SHA2_256 trước khi lưu

**Sau:** 
- Mật khẩu `1`
- Lưu plain text (KHÔNG hash)

**Lý do:** Đơn giản hóa để test, dễ nhớ, dễ debug

**Thay đổi trong code:**
- File `04_StoredProcedures_Advanced.sql` (dòng 709-741)
- Comment dòng hash: `-- DECLARE @HashedPassword VARBINARY(32) = HASHBYTES('SHA2_256', @MatKhau);`
- Đổi `@MatKhauHash = @HashedPassword` → `@MatKhauHash = @MatKhau`

**Các tài khoản:**
- `giamdoc` / `1` (QuanLy)
- `hr_manager` / `1` (HR)
- `ketoan01` / `1` (KeToan)
- `nv_hr_01` / `1` (NhanVien)
- `nv_banhang_01` / `1` (NhanVien)
- `nv_banhang_02` / `1` (NhanVien)
- `nv_kho_01` / `1` (NhanVien)
- `nv_thungan_01` / `1` (NhanVien)
- `nv_nghiviec` / `1` (NhanVien - đã nghỉ việc)

### ✅ Thay đổi 2: ID bắt đầu từ 1
**Trước:** Sử dụng `DBCC CHECKIDENT(..., RESEED, 0)` khiến ID đầu tiên là 1 nhưng có thể gây nhầm lẫn

**Sau:** Không RESEED, để IDENTITY tự nhiên bắt đầu từ 1

**Kết quả:**
```
MaPhongBan: 1, 2, 3, 4, 5, 6
MaChucVu: 1, 2, 3, 4, 5, 6, 7
MaCa: 1, 2, 3, 4
MaNguoiDung: 1, 2, 3, 4, 5, 6, 7, 8, 9
MaNV: 1, 2, 3, 4, 5, 6, 7, 8, 9
```

### ✅ Thay đổi 3: Sửa lỗi Database Users không xuất hiện
**Vấn đề:** File `data_mau.sql` đã override stored procedure `sp_TaoTaiKhoanDayDu` với version thiếu logic tạo Database User

**Giải pháp:** Xóa phần override, sử dụng stored procedure gốc từ `04_StoredProcedures_Advanced.sql`

**Kết quả:** Database Users WITHOUT LOGIN sẽ xuất hiện trong Security > Users của database

## Cách kiểm tra

### 1. Kiểm tra mật khẩu
```sql
USE QLNhanSuSieuThiMini;
SELECT TenDangNhap, MatKhauHash FROM dbo.NguoiDung;
-- MatKhauHash sẽ là hash của "1"
```

### 2. Kiểm tra ID bắt đầu từ 1
```sql
SELECT MIN(MaPhongBan) AS MinID FROM dbo.PhongBan;  -- Kết quả: 1
SELECT MIN(MaChucVu) AS MinID FROM dbo.ChucVu;      -- Kết quả: 1
SELECT MIN(MaNguoiDung) AS MinID FROM dbo.NguoiDung; -- Kết quả: 1
SELECT MIN(MaNV) AS MinID FROM dbo.NhanVien;        -- Kết quả: 1
```

### 3. Kiểm tra Database Users
```sql
SELECT name, type_desc, create_date 
FROM sys.database_principals 
WHERE type = 'S' 
  AND name NOT IN ('dbo', 'guest', 'INFORMATION_SCHEMA', 'sys')
ORDER BY name;
-- Kết quả: 9 users (giamdoc, hr_manager, ketoan01, ...)
```

## Lưu ý quan trọng

### Về mật khẩu
- ⚠️ **QUAN TRỌNG:** Mật khẩu `1` được lưu **PLAIN TEXT** (không hash)
- Trong bảng `NguoiDung`, cột `MatKhauHash` lưu giá trị `'1'` (plain text)
- Ứng dụng C# so sánh trực tiếp: `if (inputPassword == dbPassword)`
- **Chỉ dùng cho môi trường test/development, KHÔNG dùng production!**

### Về Database Users
- Users được tạo với `WITHOUT LOGIN` (không cần SQL Server Login)
- Users xuất hiện trong Security > Users của database, không phải server
- Users được gán vào roles: r_hr, r_quanly, r_ketoan, r_nhanvien

### Về IDENTITY
- Khi bảng rỗng và không RESEED, IDENTITY tự động bắt đầu từ 1
- Không cần `DBCC CHECKIDENT(..., RESEED, 0)`
- Đơn giản và tự nhiên hơn

## Test login trong ứng dụng C#

```csharp
// Test với các tài khoản sau (password là plain text):
Username: giamdoc      Password: 1  (QuanLy)
Username: hr_manager   Password: 1  (HR)
Username: ketoan01     Password: 1  (KeToan)
Username: nv_hr_01     Password: 1  (NhanVien)

// Code C# để login (so sánh plain text):
string query = "SELECT * FROM NguoiDung WHERE TenDangNhap = @username AND MatKhauHash = @password";
// Không cần hash password, so sánh trực tiếp
```

## ⚠️ CẢNH BÁO BẢO MẬT

**Mật khẩu plain text CHỈ dùng cho test/development!**

Để chuyển sang production với mật khẩu hash:
1. Uncomment dòng 710 trong `04_StoredProcedures_Advanced.sql`
2. Đổi dòng 741: `@MatKhauHash = @HashedPassword`
3. Chạy lại từ đầu
4. Cập nhật C# để hash password trước khi so sánh

## Rollback (nếu cần quay lại version cũ)

Nếu muốn quay lại mật khẩu `123`:
1. Mở file `data_mau.sql`
2. Find: `'1'` (trong các dòng EXEC sp_TaoTaiKhoanDayDu)
3. Replace: `'123'`
4. Chạy lại từ đầu

Nếu muốn ID bắt đầu từ 0:
1. Uncomment các dòng `DBCC CHECKIDENT`
2. Đổi `RESEED, 0` thành `RESEED, -1`
3. Chạy lại từ đầu
