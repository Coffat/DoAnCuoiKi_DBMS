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

#### Tạo database và schema
```sql
-- Tạo database
CREATE DATABASE QLNhanSuSieuThiMini;
USE QLNhanSuSieuThiMini;

-- Tạo các bảng theo schema thực tế
CREATE TABLE NguoiDung (
    MaNguoiDung INT IDENTITY(1,1) PRIMARY KEY,
    TenDangNhap NVARCHAR(50) UNIQUE NOT NULL,
    MatKhauHash NVARCHAR(255) NOT NULL,
    VaiTro NVARCHAR(20) NOT NULL, -- HR, QuanLy, KeToan, NhanVien
    KichHoat BIT DEFAULT 1,
    NgayTao DATETIME DEFAULT GETDATE()
);

CREATE TABLE NhanVien (
    MaNV INT IDENTITY(1,1) PRIMARY KEY,
    MaNguoiDung INT NULL,
    HoTen NVARCHAR(120) NOT NULL,
    NgaySinh DATE NULL,
    GioiTinh NVARCHAR(10) NULL,
    DienThoai NVARCHAR(20) NULL,
    Email NVARCHAR(120) NULL,
    DiaChi NVARCHAR(255) NULL,
    NgayVaoLam DATE NOT NULL,
    TrangThai NVARCHAR(20) DEFAULT N'DangLam', -- DangLam, Nghi
    PhongBan NVARCHAR(80) NULL,
    ChucDanh NVARCHAR(80) NULL,
    LuongCoBan DECIMAL(12,2) DEFAULT 0,
    NgayTao DATETIME DEFAULT GETDATE(),
    NgayCapNhat DATETIME NULL,
    FOREIGN KEY (MaNguoiDung) REFERENCES NguoiDung(MaNguoiDung)
);

CREATE TABLE CaLam (
    MaCa INT IDENTITY(1,1) PRIMARY KEY,
    TenCa NVARCHAR(60) NOT NULL,    -- VD: 'Sang','Chieu','Toi'
    GioBatDau TIME(0) NOT NULL,
    GioKetThuc TIME(0) NOT NULL,
    HeSoCa DECIMAL(4,2) NOT NULL DEFAULT(1.00)
);

CREATE TABLE LichPhanCa (
    MaLich INT IDENTITY(1,1) PRIMARY KEY,
    MaNV INT NOT NULL,
    NgayLam DATE NOT NULL,
    MaCa INT NOT NULL,
    TrangThai NVARCHAR(12) NOT NULL DEFAULT(N'DuKien'),

    CONSTRAINT FK_LichPhanCa_NhanVien FOREIGN KEY(MaNV) REFERENCES NhanVien(MaNV) ON DELETE CASCADE,
    CONSTRAINT FK_LichPhanCa_CaLam FOREIGN KEY(MaCa) REFERENCES CaLam(MaCa)
);

CREATE TABLE ChamCong (
    MaChamCong INT IDENTITY(1,1) PRIMARY KEY,
    MaNV INT NOT NULL,
    NgayLam DATE NOT NULL,
    GioVao DATETIME2(0) NULL,
    GioRa DATETIME2(0) NULL,
    GioCong DECIMAL(5,2) NULL,
    DiTrePhut INT NULL,
    VeSomPhut INT NULL,
    Khoa BIT NOT NULL DEFAULT(0),

    CONSTRAINT FK_ChamCong_NhanVien FOREIGN KEY(MaNV) REFERENCES NhanVien(MaNV) ON DELETE CASCADE
);

CREATE TABLE DonTu (
    MaDon INT IDENTITY(1,1) PRIMARY KEY,
    MaNV INT NOT NULL,
    Loai NVARCHAR(10) NOT NULL,   -- 'NGHI' hoặc 'OT'
    TuLuc DATETIME2(0) NOT NULL,
    DenLuc DATETIME2(0) NOT NULL,
    SoGio DECIMAL(5,2) NULL,
    LyDo NVARCHAR(255) NULL,
    TrangThai NVARCHAR(10) NOT NULL DEFAULT(N'ChoDuyet'),
    DuyetBoi INT NULL,

    CONSTRAINT FK_DonTu_NhanVien FOREIGN KEY(MaNV) REFERENCES NhanVien(MaNV) ON DELETE CASCADE,
    CONSTRAINT FK_DonTu_DuyetBoi FOREIGN KEY(DuyetBoi) REFERENCES NguoiDung(MaNguoiDung)
);

CREATE TABLE BangLuong (
    MaBangLuong INT IDENTITY(1,1) PRIMARY KEY,
    Nam INT NOT NULL,
    Thang INT NOT NULL,
    MaNV INT NOT NULL,
    LuongCoBan DECIMAL(12,2) NOT NULL,
    TongGioCong DECIMAL(7,2) NOT NULL,
    GioOT DECIMAL(7,2) NOT NULL,
    PhuCap DECIMAL(12,2) NOT NULL DEFAULT(0),
    KhauTru DECIMAL(12,2) NOT NULL DEFAULT(0),
    ThueBH DECIMAL(12,2) NOT NULL DEFAULT(0),
    ThucLanh DECIMAL(12,2) NOT NULL,
    TrangThai NVARCHAR(10) NOT NULL DEFAULT(N'Mo'),

    CONSTRAINT FK_BangLuong_NhanVien FOREIGN KEY(MaNV) REFERENCES NhanVien(MaNV) ON DELETE CASCADE
);

-- Tạo tài khoản admin mặc định (password: admin123)
-- Xóa user cũ nếu có
DELETE FROM NguoiDung WHERE TenDangNhap = 'admin';

-- Tạo user admin với plain text password (để test)
INSERT INTO NguoiDung (TenDangNhap, MatKhauHash, VaiTro, KichHoat)
VALUES ('admin', 'admin123', 'HR', 1);

-- Tạo dữ liệu mẫu ca làm (lưu ý: constraint GioBatDau < GioKetThuc)
INSERT INTO CaLam (TenCa, GioBatDau, GioKetThuc, HeSoCa) VALUES
(N'Ca Sáng', '08:00:00', '16:00:00', 1.0),
(N'Ca Chiều', '16:00:00', '22:00:00', 1.2),
(N'Ca Tối', '22:00:00', '23:59:59', 1.5);
```

#### Chạy script Views, Functions, Procedures và Security
Sau khi tạo schema cơ bản, hãy chạy toàn bộ script SQL mà bạn đã cung cấp để tạo:
- Views: `vw_CongThang`, `vw_Lich_ChamCong_Ngay`
- Functions: `fn_KhungCa`, `fn_SoPhutDuong`, `fn_rls_NhanVien`
- Stored Procedures: `sp_SetSessionContextNhanVien`, `sp_ThemMoiNhanVien`, `sp_DuyetDonTu`, `sp_KhoaCongThang`, `sp_ChayBangLuong`, `sp_DongBangLuong`
- Security: Roles và RLS policies
- Triggers: Tự động tính toán và bảo vệ dữ liệu

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
