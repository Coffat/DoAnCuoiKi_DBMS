# HR Management System - Hệ thống quản lý nhân sự siêu thị mini

## Mô tả
Ứng dụng WinForms C# (.NET Framework 4.7.2) quản lý nhân sự với giao diện đẹp sử dụng Guna.UI2.WinForms và kết nối SQL Server.

## Công nghệ sử dụng
- **Framework**: .NET Framework 4.7.2 (WinForms)
- **UI Library**: Guna.UI2.WinForms
- **Database**: SQL Server với ADO.NET (System.Data.SqlClient)
- **Authentication**: SQL Server Authentication (sa/1234)

## Cấu trúc dự án
```
VuToanThang_23110329/
├── Data/
│   └── SqlHelper.cs                 # ADO.NET wrapper
├── Forms/
│   ├── LoginForm.cs/.Designer.cs    # Form đăng nhập
│   ├── MainForm.cs/.Designer.cs     # Form chính với menu
│   └── NhanVienForm.cs              # Form quản lý nhân viên
├── Models/
│   ├── NguoiDung.cs                 # Model người dùng
│   ├── NhanVien.cs                  # Model nhân viên
│   ├── CaLam.cs                     # Model ca làm
│   ├── LichPhanCa.cs                # Model lịch phân ca
│   ├── ChamCong.cs                  # Model chấm công
│   ├── DonTu.cs                     # Model đơn từ
│   ├── BangLuong.cs                 # Model bảng lương
│   └── ViewModels.cs                # View models và result models
├── Repositories/
│   ├── AuthRepository.cs            # Repository xác thực
│   ├── NhanVienRepository.cs        # Repository nhân viên
│   └── CaLamRepository.cs           # Repository ca làm
├── App.config                       # Cấu hình kết nối DB
├── packages.config                  # NuGet packages
└── Program.cs                       # Entry point
```

## Cài đặt và chạy ứng dụng

### 1. Yêu cầu hệ thống
- Visual Studio 2019 hoặc cao hơn
- .NET Framework 4.7.2
- SQL Server (LocalDB, Express, hoặc Full)
- NuGet Package Manager

### 2. Cài đặt NuGet Packages
Mở Package Manager Console trong Visual Studio và chạy:
```powershell
Install-Package Guna.UI2.WinForms -Version 2.0.4.6
Install-Package System.Data.SqlClient -Version 4.8.5
```

Hoặc restore packages từ packages.config:
```powershell
Update-Package -reinstall
```

### 3. Cấu hình cơ sở dữ liệu

#### Tạo database và tables
```sql
-- Tạo database
CREATE DATABASE QLNhanSuSieuThiMini;
USE QLNhanSuSieuThiMini;

-- Tạo các bảng cơ bản (ví dụ)
CREATE TABLE NguoiDung (
    MaNguoiDung INT IDENTITY(1,1) PRIMARY KEY,
    TenDangNhap NVARCHAR(50) UNIQUE NOT NULL,
    MatKhau NVARCHAR(255) NOT NULL,
    VaiTro NVARCHAR(20) NOT NULL, -- HR, QuanLy, KeToan, NhanVien
    MaNV INT NULL,
    TrangThai BIT DEFAULT 1,
    NgayTao DATETIME DEFAULT GETDATE()
);

CREATE TABLE NhanVien (
    MaNV INT IDENTITY(1,1) PRIMARY KEY,
    HoTen NVARCHAR(100) NOT NULL,
    CCCD NVARCHAR(12) UNIQUE NOT NULL,
    SoDienThoai NVARCHAR(15),
    Email NVARCHAR(100),
    DiaChi NVARCHAR(255),
    NgaySinh DATE NOT NULL,
    GioiTinh NVARCHAR(10) NOT NULL,
    NgayVaoLam DATE NOT NULL,
    ChucVu NVARCHAR(50),
    PhongBan NVARCHAR(50),
    LuongCoBan DECIMAL(18,2) DEFAULT 0,
    PhuCapChucVu DECIMAL(18,2) DEFAULT 0,
    PhuCapKhac DECIMAL(18,2) DEFAULT 0,
    TrangThai NVARCHAR(20) DEFAULT 'Active',
    MaQuanLy INT NULL,
    NgayTao DATETIME DEFAULT GETDATE(),
    NgayCapNhat DATETIME NULL
);

-- Tạo tài khoản admin mặc định
INSERT INTO NguoiDung (TenDangNhap, MatKhau, VaiTro, TrangThai)
VALUES ('admin', 'admin123', 'HR', 1);
```

#### Cấu hình connection string
File `App.config` đã được cấu hình với connection string:
```xml
<connectionStrings>
  <add name="HrDb"
       connectionString="Data Source=localhost;Initial Catalog=QLNhanSuSieuThiMini;User ID=sa;Password=1234;TrustServerCertificate=True"
       providerName="System.Data.SqlClient" />
</connectionStrings>
```

**Lưu ý**: Thay đổi `Data Source` và thông tin đăng nhập phù hợp với SQL Server của bạn.

### 4. Chạy ứng dụng
1. Mở solution trong Visual Studio
2. Build solution (Ctrl+Shift+B)
3. Chạy ứng dụng (F5)
4. Đăng nhập với tài khoản:
   - **Username**: admin
   - **Password**: admin123

## Chức năng chính

### Phân quyền theo vai trò
- **HR**: Toàn quyền quản lý nhân viên, ca làm, lịch, công, đơn từ, lương, báo cáo
- **Quản lý**: Xem lịch, cập nhật công, duyệt đơn từ nhanh
- **Kế toán**: Chạy/chốt lương, báo cáo chi phí
- **Nhân viên**: Xem lịch cá nhân, công, gửi đơn từ, xem phiếu lương

### Các form chính
1. **LoginForm**: Xác thực người dùng
2. **MainForm**: Menu chính với phân quyền
3. **NhanVienForm**: CRUD nhân viên + tạo tài khoản
4. **CaLamForm**: Quản lý ca làm việc
5. **LichPhanCaForm**: Phân ca và khóa lịch
6. **ChamCongForm**: Nhập giờ vào/ra, khóa công
7. **DonTuForm**: Gửi/duyệt đơn từ theo vai trò
8. **TinhLuongForm**: Chạy và chốt lương
9. **BangLuongForm**: Điều chỉnh phụ cấp/khấu trừ
10. **PhieuLuongForm**: Xem phiếu lương cá nhân
11. **BaoCaoForm**: Báo cáo nhân sự và lương

### Database Objects được sử dụng
- **Views**: `vw_CongThang`, `vw_Lich_ChamCong_Ngay`
- **Functions**: `fn_KhungCa`, `fn_SoPhutDuong`, `fn_rls_NhanVien`
- **Stored Procedures**:
  - `sp_SetSessionContextNhanVien`
  - `sp_ThemMoiNhanVien`
  - `sp_DuyetDonTu`
  - `sp_KhoaCongThang`
  - `sp_ChayBangLuong`
  - `sp_DongBangLuong`

## Giao diện
- **Theme**: Dark với màu chính #7C4DFF (tím)
- **Layout**: Panel trái (menu) + Panel phải (nội dung)
- **Font**: Segoe UI 10.5
- **Components**: Guna2 controls với bo góc và shadow
- **Responsive**: Hover effects mượt mà

## Troubleshooting

### Lỗi kết nối database
1. Kiểm tra SQL Server đã chạy
2. Xác nhận connection string trong App.config
3. Đảm bảo tài khoản sa có mật khẩu 1234
4. Kiểm tra firewall và port 1433

### Lỗi NuGet packages
```powershell
# Xóa folder packages và restore lại
Remove-Item packages -Recurse -Force
Update-Package -reinstall
```

### Lỗi build
1. Kiểm tra .NET Framework 4.7.2 đã cài đặt
2. Clean và Rebuild solution
3. Kiểm tra references trong project file

## Tác giả
- **Sinh viên**: Vũ Toàn Thắng
- **MSSV**: 23110329
- **Môn học**: Đồ án cuối kì - Quản lý cơ sở dữ liệu

## License
Dự án học tập - Không sử dụng cho mục đích thương mại.
