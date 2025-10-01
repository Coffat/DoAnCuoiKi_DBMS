# 📝 CHANGELOG - HỆ THỐNG BẢO MẬT 2 LỚP

## [2.0.0] - 2025-10-02

### 🎉 Tính năng mới

#### Bảo Mật 2 Lớp
- ✅ Mỗi người dùng có SQL Server Login riêng
- ✅ Xác thực ở cấp độ SQL Server
- ✅ Chuỗi kết nối động theo người đăng nhập
- ✅ Phân quyền tự động qua Database Roles

#### Stored Procedures Mới (04_StoredProcedures_Advanced.sql)
- ✅ `sp_TaoTaiKhoanDayDu`: Tạo tài khoản (App + SQL Login + User + Role)
- ✅ `sp_CapNhatTaiKhoanDayDu`: Cập nhật thông tin, đổi mật khẩu, đổi vai trò
- ✅ `sp_XoaTaiKhoanDayDu`: Xóa tài khoản ở cả 2 lớp
- ✅ `sp_VoHieuHoaTaiKhoan`: Enable/Disable tài khoản

#### Ứng Dụng C#
- ✅ `GlobalState.cs`: Class mới lưu chuỗi kết nối động
- ✅ `frmLogin.cs`: Cập nhật xác thực với SQL Server Authentication
- ✅ `UserSession.cs`: Đồng bộ với GlobalState khi logout

#### Tài Liệu
- ✅ `HUONG_DAN_BAO_MAT_2_LOP.md`: Tài liệu chi tiết 500+ dòng
- ✅ `CAI_DAT_BAO_MAT_2_LOP.md`: Quick start guide
- ✅ `CAP_NHAT_CAC_FORM.md`: Hướng dẫn cập nhật ứng dụng
- ✅ `DEMO_TAO_TAI_KHOAN.sql`: Script tạo 5 tài khoản demo

### 🔧 Cải tiến

#### Xóa Code Trùng Lặp
- ✅ Xóa `sp_PhongBan_*` trùng lặp (mục 9 trong 03_StoredProcedures.sql)
- ✅ Xóa `sp_ChucVu_*` trùng lặp (mục 10 trong 03_StoredProcedures.sql)
- ✅ Xóa trigger `tr_NhanVien_ToggleAccount` trùng lặp
- ✅ Giảm 190 dòng code thừa

#### Cải Thiện Phân Quyền (05_Security_Triggers.sql)
- ✅ Thu hồi quyền INSERT/UPDATE/DELETE trực tiếp trên bảng
- ✅ Chỉ cấp quyền SELECT và EXECUTE trên Stored Procedures
- ✅ Áp dụng Principle of Least Privilege
- ✅ Đảm bảo business logic được thực thi nhất quán

#### Cảnh Báo và Documentation
- ✅ Thêm cảnh báo cho `sp_NguoiDung_DoiMatKhau`
- ✅ Hướng dẫn rõ ràng SP nào nên dùng
- ✅ Comment giải thích chi tiết

### 📊 Thống kê

```
Dòng code SQL bổ sung:    +400 dòng
Dòng code SQL xóa:         -190 dòng
Dòng code C# bổ sung:      +120 dòng
Dòng tài liệu:             +2000 dòng
Files mới:                 8 files
Stored Procedures mới:     4 procedures
Classes C# mới:            1 class
```

### 🎯 Breaking Changes

#### Cách đăng nhập thay đổi
**Trước:**
```csharp
// Dùng tài khoản sa từ App.config
string connString = ConfigurationManager.ConnectionStrings["HrDb"].ConnectionString;
```

**Sau:**
```csharp
// Dùng username/password người dùng nhập
builder.UserID = username;
builder.Password = password;
GlobalState.ConnectionString = builder.ConnectionString;
```

#### Đổi mật khẩu
**Trước:** Dùng `sp_NguoiDung_DoiMatKhau` (chỉ cập nhật app)

**Sau:** Dùng `sp_CapNhatTaiKhoanDayDu` (đồng bộ cả SQL Login)

```sql
EXEC sp_CapNhatTaiKhoanDayDu
    @MaNV = 10,
    -- ... thông tin khác
    @MatKhauMoi = 'NewPassword@123';
```

### ⚠️ Migration Notes

#### Bước 1: Cập nhật SQL
```sql
:r "04_StoredProcedures_Advanced.sql"
:r "05_Security_Triggers.sql"  -- Chạy lại để cập nhật phân quyền
```

#### Bước 2: Tạo tài khoản demo
```sql
:r "DEMO_TAO_TAI_KHOAN.sql"
```

#### Bước 3: Cập nhật ứng dụng C#
- Thêm `GlobalState.cs`
- Cập nhật `frmLogin.cs`
- Cập nhật `UserSession.cs`
- Find & Replace trong tất cả form:
  - Find: `ConfigurationManager.ConnectionStrings["HrDb"].ConnectionString`
  - Replace: `GlobalState.ConnectionString`

#### Bước 4: Test
- Đăng nhập với `hr_mai` / `HR@2024`
- Kiểm tra các chức năng
- Test phân quyền

### 🐛 Bug Fixes

- ✅ Sửa lỗi mật khẩu app và SQL Login không đồng bộ
- ✅ Sửa lỗi có thể bypass business logic qua INSERT trực tiếp
- ✅ Sửa lỗi audit trail không chính xác (mọi thao tác đều ghi là 'sa')

### 📋 Checklist nâng cấp

- [x] Chạy script SQL mới
- [x] Tạo tài khoản demo
- [x] Thêm GlobalState.cs vào project
- [x] Cập nhật frmLogin.cs
- [x] Cập nhật UserSession.cs
- [ ] Cập nhật các form khác (12-15 files)
- [ ] Test đầy đủ các chức năng
- [ ] Deploy lên production

---

## [1.0.0] - 2024-01-01 (Baseline)

### Tính năng ban đầu
- ✅ Quản lý nhân viên
- ✅ Quản lý chấm công
- ✅ Quản lý lịch phân ca
- ✅ Quản lý bảng lương
- ✅ Quản lý đơn từ
- ✅ Phân quyền cơ bản (kiểm tra trong C#)
- ✅ Sử dụng tài khoản sa chung

### Hạn chế
- ❌ Tất cả user dùng chung tài khoản sa
- ❌ Không biết ai thực hiện thao tác gì
- ❌ Audit trail không chính xác
- ❌ Phân quyền phải check trong code C#
- ❌ Có thể bypass business logic

---

**Tác giả:** Vũ Toàn Thắng - 23110329  
**Repository:** DoAnCuoiKi_DBMS  
**License:** Educational Purpose
