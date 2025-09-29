-- IF DB_ID(N'QLNhanSuSieuThiMini') IS NOT NULL
-- BEGIN
--     ALTER DATABASE QLNhanSuSieuThiMini SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
--     DROP DATABASE QLNhanSuSieuThiMini;
--     PRINT N'Đã xóa database cũ QLNhanSuSieuThiMini';
-- END

-- CREATE DATABASE QLNhanSuSieuThiMini;
-- PRINT N'Đã tạo database QLNhanSuSieuThiMini';
-- GO

-- USE QLNhanSuSieuThiMini;
-- PRINT N'Đã chuyển sang database QLNhanSuSieuThiMini';
-- GO

--1,User
IF OBJECT_ID('dbo.NguoiDung','U') IS NOT NULL DROP TABLE dbo.NguoiDung;
GO
CREATE TABLE dbo.NguoiDung (
    MaNguoiDung   INT IDENTITY(1,1) PRIMARY KEY,   -- Khóa chính, tự tăng
    TenDangNhap   NVARCHAR(50)  NOT NULL,          -- Bắt buộc, duy nhất
    MatKhauHash   NVARCHAR(200) NOT NULL,          -- Bắt buộc
    VaiTro        NVARCHAR(20)  NOT NULL,          -- HR / QuanLy / KeToan / NhanVien
    KichHoat      BIT NOT NULL 
        CONSTRAINT DF_NguoiDung_KichHoat DEFAULT(1) -- Mặc định đang hoạt động
);
GO
-- RÀNG BUỘC / GIẢI THÍCH
ALTER TABLE dbo.NguoiDung
  ADD CONSTRAINT UQ_NguoiDung_TenDangNhap UNIQUE(TenDangNhap);
-- TenDangNhap duy nhất để tránh trùng tài khoản.

ALTER TABLE dbo.NguoiDung
  ADD CONSTRAINT CK_NguoiDung_VaiTro CHECK(VaiTro IN (N'HR',N'QuanLy',N'KeToan',N'NhanVien'));
-- Đảm bảo VaiTro chỉ nằm trong tập hợp cho phép.


--1.1) PHONGBAN: Chuẩn hóa phòng ban
IF OBJECT_ID('dbo.PhongBan','U') IS NOT NULL DROP TABLE dbo.PhongBan;
GO
CREATE TABLE dbo.PhongBan (
    MaPhongBan INT IDENTITY(1,1) PRIMARY KEY,
    TenPhongBan NVARCHAR(80) NOT NULL,
    MoTa NVARCHAR(255),
    KichHoat BIT DEFAULT 1,
    CONSTRAINT UQ_PhongBan_TenPhongBan UNIQUE(TenPhongBan)
);
GO

--1.2) CHUCVU: Chuẩn hóa chức vụ
IF OBJECT_ID('dbo.ChucVu','U') IS NOT NULL DROP TABLE dbo.ChucVu;
GO
CREATE TABLE dbo.ChucVu (
    MaChucVu INT IDENTITY(1,1) PRIMARY KEY,
    TenChucVu NVARCHAR(80) NOT NULL,
    MoTa NVARCHAR(255),
    KichHoat BIT DEFAULT 1,
    CONSTRAINT UQ_ChucVu_TenChucVu UNIQUE(TenChucVu)
);
GO

--2) NHANVIEN: Hồ sơ nhân sự (đã chuẩn hóa)
IF OBJECT_ID('dbo.NhanVien','U') IS NOT NULL DROP TABLE dbo.NhanVien;
GO
CREATE TABLE dbo.NhanVien (
    MaNV        INT IDENTITY(1,1) PRIMARY KEY,     -- Khóa chính
    MaNguoiDung INT NULL,                          -- FK → NguoiDung, có thể NULL
    HoTen       NVARCHAR(120) NOT NULL,
    NgaySinh    DATE NULL,
    GioiTinh    NVARCHAR(10) NULL,                 -- 'Nam'/'Nu'/'Khac'
    DienThoai   NVARCHAR(20) NULL,
    Email       NVARCHAR(120) NULL,
    DiaChi      NVARCHAR(255) NULL,
    NgayVaoLam  DATE NOT NULL 
        CONSTRAINT DF_NhanVien_NgayVaoLam DEFAULT(CONVERT(date,GETDATE())),
    TrangThai   NVARCHAR(10) NOT NULL 
        CONSTRAINT DF_NhanVien_TrangThai DEFAULT(N'DangLam'),
    MaPhongBan  INT NULL,                          -- FK → PhongBan, thay thế cột PhongBan cũ
    MaChucVu    INT NULL,                          -- FK → ChucVu, thay thế cột ChucDanh cũ
    LuongCoBan  DECIMAL(12,2) NOT NULL,

    CONSTRAINT FK_NhanVien_NguoiDung
        FOREIGN KEY(MaNguoiDung) REFERENCES dbo.NguoiDung(MaNguoiDung)
        ON DELETE SET NULL,
    CONSTRAINT FK_NhanVien_PhongBan
        FOREIGN KEY(MaPhongBan) REFERENCES dbo.PhongBan(MaPhongBan)
        ON DELETE SET NULL,
    CONSTRAINT FK_NhanVien_ChucVu
        FOREIGN KEY(MaChucVu) REFERENCES dbo.ChucVu(MaChucVu)
        ON DELETE SET NULL
);
GO
-- RÀNG BUỘC / GIẢI THÍCH
-- Thay thế UNIQUE constraints bằng Filtered Unique Index để xử lý NULL values
DROP INDEX IF EXISTS UQ_NhanVien_DienThoai ON dbo.NhanVien;
DROP INDEX IF EXISTS UQ_NhanVien_Email ON dbo.NhanVien;
GO

CREATE UNIQUE INDEX UQ_NhanVien_DienThoai_NotNull
ON dbo.NhanVien(DienThoai)
WHERE DienThoai IS NOT NULL;
-- Số điện thoại không được trùng nếu có cung cấp.

CREATE UNIQUE INDEX UQ_NhanVien_Email_NotNull
ON dbo.NhanVien(Email)
WHERE Email IS NOT NULL;
-- Email không được trùng nếu có cung cấp.

ALTER TABLE dbo.NhanVien ADD CONSTRAINT CK_NhanVien_TrangThai CHECK(TrangThai IN (N'DangLam',N'Nghi'));
-- Chỉ chấp nhận 2 trạng thái nhân viên.

ALTER TABLE dbo.NhanVien WITH NOCHECK 
  ADD CONSTRAINT CK_NhanVien_GioiTinh CHECK(GioiTinh IS NULL OR GioiTinh IN (N'Nam',N'Nu',N'Khac'));
-- Kiểm soát giá trị giới tính nếu có.

-- Cập nhật index để sử dụng các cột mới
DROP INDEX IF EXISTS IX_NhanVien_Phong_Chuc_TrangThai ON dbo.NhanVien;
GO

CREATE INDEX IX_NhanVien_PhongBan_ChucVu_TrangThai 
ON dbo.NhanVien(MaPhongBan, MaChucVu, TrangThai);
-- Tăng tốc lọc nhân sự theo phòng ban/chức vụ.


--3) CALAM: Ca làm việc
IF OBJECT_ID('dbo.CaLam','U') IS NOT NULL DROP TABLE dbo.CaLam;
GO
CREATE TABLE dbo.CaLam (
    MaCa        INT IDENTITY(1,1) PRIMARY KEY,
    TenCa       NVARCHAR(60) NOT NULL,    -- VD: 'Sang','Chieu','Toi'
    GioBatDau   TIME(0) NOT NULL,
    GioKetThuc  TIME(0) NOT NULL,
    HeSoCa      DECIMAL(4,2) NOT NULL CONSTRAINT DF_CaLam_HeSoCa DEFAULT(1.00),
    MoTa        NVARCHAR(255) NULL,       -- Mô tả chi tiết ca làm
    KichHoat    BIT NOT NULL CONSTRAINT DF_CaLam_KichHoat DEFAULT(1) -- Trạng thái kích hoạt
);
GO
-- RÀNG BUỘC / GIẢI THÍCH
-- Đã bỏ ràng buộc CK_CaLam_Gio để hỗ trợ ca qua đêm (VD: 22:00 - 06:00)
-- Logic xử lý ca qua đêm sẽ được thực hiện ở tầng ứng dụng và trong các trigger/stored procedure


--4) LICHPHANCA: Lịch phân ca hàng ngày
IF OBJECT_ID('dbo.LichPhanCa','U') IS NOT NULL DROP TABLE dbo.LichPhanCa;
GO
CREATE TABLE dbo.LichPhanCa (
    MaLich    INT IDENTITY(1,1) PRIMARY KEY,
    MaNV      INT NOT NULL,
    NgayLam   DATE NOT NULL,
    MaCa      INT NOT NULL,
    TrangThai NVARCHAR(12) NOT NULL 
        CONSTRAINT DF_LichPhanCa_TrangThai DEFAULT(N'DuKien'),
    GhiChu    NVARCHAR(255) NULL,  -- Thêm cột ghi chú

    CONSTRAINT FK_LichPhanCa_NhanVien FOREIGN KEY(MaNV) REFERENCES dbo.NhanVien(MaNV) ON DELETE CASCADE,
    CONSTRAINT FK_LichPhanCa_CaLam FOREIGN KEY(MaCa) REFERENCES dbo.CaLam(MaCa)
);
GO
-- RÀNG BUỘC / GIẢI THÍCH
-- KHÔNG tạo UNIQUE(MaNV, NgayLam) để cho phép nhiều ca/ngày
-- ALTER TABLE dbo.LichPhanCa ADD CONSTRAINT UQ_LichPhanCa_MaNV_Ngay UNIQUE(MaNV,NgayLam);

ALTER TABLE dbo.LichPhanCa 
  ADD CONSTRAINT CK_LichPhanCa_TrangThai CHECK(TrangThai IN (N'DuKien',N'Khoa',N'Huy'));
-- Trạng thái lịch chỉ trong 3 giá trị hợp lệ.

CREATE INDEX IX_LichPhanCa_NgayLam ON dbo.LichPhanCa(NgayLam);
-- Tăng tốc truy vấn lịch theo ngày.

CREATE INDEX IX_LichPhanCa_MaNV_NgayLam ON dbo.LichPhanCa(MaNV, NgayLam);
-- Tăng tốc truy vấn lịch theo nhân viên và ngày (cho TVF tvf_LichTheoTuan).


--5) CHAMCONG: Ghi nhận giờ làm thực tế
IF OBJECT_ID('dbo.ChamCong','U') IS NOT NULL DROP TABLE dbo.ChamCong;
GO
CREATE TABLE dbo.ChamCong (
    MaChamCong INT IDENTITY(1,1) PRIMARY KEY,
    MaNV       INT NOT NULL,
    NgayLam    DATE NOT NULL,
    GioVao     DATETIME2(0) NULL,
    GioRa      DATETIME2(0) NULL,
    GioCong    DECIMAL(5,2) NULL,
    DiTrePhut  INT NULL,
    VeSomPhut  INT NULL,
    Khoa       BIT NOT NULL CONSTRAINT DF_ChamCong_Khoa DEFAULT(0),

    CONSTRAINT FK_ChamCong_NhanVien FOREIGN KEY(MaNV) REFERENCES dbo.NhanVien(MaNV) ON DELETE CASCADE
);
GO
-- RÀNG BUỘC / GIẢI THÍCH
ALTER TABLE dbo.ChamCong ADD CONSTRAINT UQ_ChamCong_MaNV_Ngay UNIQUE(MaNV,NgayLam);
-- Một nhân viên chỉ có một dòng chấm công/ngày.

ALTER TABLE dbo.ChamCong ADD CONSTRAINT CK_ChamCong_GioVaoRa CHECK(GioVao IS NULL OR GioRa IS NULL OR GioVao < GioRa);
-- Đảm bảo giờ vào < giờ ra.

CREATE INDEX IX_ChamCong_MaNV_Ngay ON dbo.ChamCong(MaNV,NgayLam);
-- Tăng tốc tìm chấm công theo nhân viên & ngày.


--6) DONTU: Đơn nghỉ/OT
IF OBJECT_ID('dbo.DonTu','U') IS NOT NULL DROP TABLE dbo.DonTu;
GO
CREATE TABLE dbo.DonTu (
    MaDon     INT IDENTITY(1,1) PRIMARY KEY,
    MaNV      INT NOT NULL,
    Loai      NVARCHAR(10) NOT NULL,   -- 'NGHI' hoặc 'OT'
    TuLuc     DATETIME2(0) NOT NULL,
    DenLuc    DATETIME2(0) NOT NULL,
    SoGio     DECIMAL(5,2) NULL,
    LyDo      NVARCHAR(255) NULL,
    TrangThai NVARCHAR(10) NOT NULL CONSTRAINT DF_DonTu_TrangThai DEFAULT(N'ChoDuyet'),
    DuyetBoi  INT NULL,

    CONSTRAINT FK_DonTu_NhanVien FOREIGN KEY(MaNV) REFERENCES dbo.NhanVien(MaNV) ON DELETE CASCADE,
    CONSTRAINT FK_DonTu_DuyetBoi FOREIGN KEY(DuyetBoi) REFERENCES dbo.NguoiDung(MaNguoiDung)
);
GO
-- RÀNG BUỘC / GIẢI THÍCH
ALTER TABLE dbo.DonTu ADD CONSTRAINT CK_DonTu_Loai CHECK(Loai IN (N'NGHI',N'OT'));
-- Đảm bảo chỉ có 2 loại đơn.

ALTER TABLE dbo.DonTu ADD CONSTRAINT CK_DonTu_TrangThai CHECK(TrangThai IN (N'ChoDuyet',N'DaDuyet',N'TuChoi'));
-- Trạng thái đơn trong 3 giá trị hợp lệ.

ALTER TABLE dbo.DonTu ADD CONSTRAINT CK_DonTu_ThoiGian CHECK(TuLuc <= DenLuc);
-- Thời gian bắt đầu phải trước hoặc bằng thời gian kết thúc.

CREATE INDEX IX_DonTu_MaNV_TrangThai ON dbo.DonTu(MaNV,TrangThai);
-- Hỗ trợ duyệt đơn theo nhân viên và trạng thái.

CREATE INDEX IX_DonTu_Loai_ThoiGian ON dbo.DonTu(Loai,TuLuc,DenLuc);
-- Hỗ trợ tra cứu đơn theo loại & thời gian.


--7) BANGLUONG: Lương theo tháng/năm
IF OBJECT_ID('dbo.BangLuong','U') IS NOT NULL DROP TABLE dbo.BangLuong;
GO
CREATE TABLE dbo.BangLuong (
    MaBangLuong INT IDENTITY(1,1) PRIMARY KEY,
    Nam         INT NOT NULL,
    Thang       INT NOT NULL,
    MaNV        INT NOT NULL,
    LuongCoBan  DECIMAL(12,2) NOT NULL,
    TongGioCong DECIMAL(7,2) NOT NULL,
    GioOT       DECIMAL(7,2) NOT NULL,
    PhuCap      DECIMAL(12,2) NOT NULL CONSTRAINT DF_BangLuong_PhuCap DEFAULT(0),
    KhauTru     DECIMAL(12,2) NOT NULL CONSTRAINT DF_BangLuong_KhauTru DEFAULT(0),
    ThueBH      DECIMAL(12,2) NOT NULL CONSTRAINT DF_BangLuong_ThueBH DEFAULT(0),
    ThucLanh    DECIMAL(12,2) NOT NULL,
    TrangThai   NVARCHAR(10) NOT NULL CONSTRAINT DF_BangLuong_TrangThai DEFAULT(N'Mo'),

    CONSTRAINT FK_BangLuong_NhanVien FOREIGN KEY(MaNV) REFERENCES dbo.NhanVien(MaNV) ON DELETE CASCADE
);
GO
-- RÀNG BUỘC / GIẢI THÍCH
ALTER TABLE dbo.BangLuong ADD CONSTRAINT UQ_BangLuong_Ky UNIQUE(Nam,Thang,MaNV);
-- Mỗi nhân viên chỉ có 1 bảng lương/tháng.

ALTER TABLE dbo.BangLuong ADD CONSTRAINT CK_BangLuong_Thang CHECK(Thang BETWEEN 1 AND 12);
-- Tháng chỉ từ 1 đến 12.

ALTER TABLE dbo.BangLuong ADD CONSTRAINT CK_BangLuong_TrangThai CHECK(TrangThai IN (N'Mo',N'Dong'));
-- Trạng thái bảng lương: mở hoặc đã đóng.

CREATE INDEX IX_BangLuong_Ky ON dbo.BangLuong(Nam,Thang);
-- Hỗ trợ tìm bảng lương theo kỳ.

-- HOÀN TẤT TẠO DATABASE VÀ TABLES

PRINT N'=== HOÀN TẤT TẠO DATABASE VÀ TABLES ===';
PRINT N'Database QLNhanSuSieuThiMini và tất cả tables đã được tạo thành công!';
PRINT N'Tiếp theo chạy file: 02_ChucNang.sql để tạo views, functions, procedures, triggers';
GO
