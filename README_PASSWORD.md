# ⚠️ QUAN TRỌNG: MẬT KHẨU PLAIN TEXT

## Tóm tắt nhanh

**Tất cả tài khoản có mật khẩu: `1` (plain text - KHÔNG hash)**

### Tài khoản test
```
giamdoc / 1
hr_manager / 1
ketoan01 / 1
nv_hr_01 / 1
nv_banhang_01 / 1
nv_banhang_02 / 1
nv_kho_01 / 1
nv_thungan_01 / 1
nv_nghiviec / 1 (đã nghỉ việc - tài khoản bị khóa)
```

## Kiểm tra trong database

```sql
USE QLNhanSuSieuThiMini;

-- Xem mật khẩu (sẽ thấy '1' plain text)
SELECT TenDangNhap, MatKhauHash, VaiTro, KichHoat 
FROM dbo.NguoiDung;

-- Kết quả mong đợi:
-- TenDangNhap   | MatKhauHash | VaiTro   | KichHoat
-- giamdoc       | 1           | QuanLy   | 1
-- hr_manager    | 1           | HR       | 1
-- ketoan01      | 1           | KeToan   | 1
-- ...
```

## Code C# để login

```csharp
// ✅ ĐÚNG: So sánh plain text trực tiếp
string query = @"
    SELECT MaNguoiDung, TenDangNhap, VaiTro, KichHoat 
    FROM NguoiDung 
    WHERE TenDangNhap = @username 
      AND MatKhauHash = @password 
      AND KichHoat = 1";

using (SqlCommand cmd = new SqlCommand(query, connection))
{
    cmd.Parameters.AddWithValue("@username", txtUsername.Text);
    cmd.Parameters.AddWithValue("@password", txtPassword.Text); // Không hash
    
    SqlDataReader reader = cmd.ExecuteReader();
    if (reader.Read())
    {
        // Login thành công
        string role = reader["VaiTro"].ToString();
        // ...
    }
}
```

```csharp
// ❌ SAI: Không cần hash password
string hashedPassword = HashPassword(txtPassword.Text); // KHÔNG CẦN
```

## Files đã thay đổi

1. **04_StoredProcedures_Advanced.sql**
   - Dòng 710: Comment dòng hash
   - Dòng 741: Đổi `@HashedPassword` → `@MatKhau`

2. **data_mau.sql**
   - Tất cả password đổi từ `'123'` → `'1'`

## Cảnh báo bảo mật

⚠️ **Plain text password CHỈ dùng cho test/development!**

### Để chuyển sang production (hash password):

1. **Sửa file 04_StoredProcedures_Advanced.sql:**
```sql
-- Dòng 710: Uncomment
DECLARE @HashedPassword VARBINARY(32) = HASHBYTES('SHA2_256', @MatKhau);

-- Dòng 741: Đổi lại
@MatKhauHash = @HashedPassword,  -- Thay vì @MatKhau
```

2. **Cập nhật C# để hash password:**
```csharp
using System.Security.Cryptography;
using System.Text;

public static string HashPassword(string password)
{
    using (SHA256 sha256 = SHA256.Create())
    {
        byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
        return Convert.ToBase64String(bytes);
    }
}

// Khi login:
string hashedPassword = HashPassword(txtPassword.Text);
// So sánh với MatKhauHash trong database
```

3. **Chạy lại từ đầu:**
```
01_TaoDatabase.sql
02_ChucNang.sql
03_StoredProcedures.sql
04_StoredProcedures_Advanced.sql  ← Đã sửa
05_Security_Triggers.sql
data_mau.sql
```

## Tại sao lưu plain text?

**Ưu điểm (cho test):**
- ✅ Dễ debug: Xem trực tiếp password trong database
- ✅ Dễ test: Không cần implement hash function
- ✅ Đơn giản: Không lo lỗi hash không khớp

**Nhược điểm (cho production):**
- ❌ Không an toàn: Ai có quyền truy cập database đều thấy password
- ❌ Vi phạm best practices
- ❌ Không đạt chuẩn bảo mật

## Kết luận

Hiện tại hệ thống đang dùng **plain text password** để dễ test. 

Trước khi deploy production, **BẮT BUỘC** phải chuyển sang hash password!
