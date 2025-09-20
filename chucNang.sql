/* =========================================================
   Dự án DBMS - HR cho Siêu Thị Mini
   SQL Server (T-SQL) - Phiên bản tiếng Việt + Giải thích
   
   CÁC SỬA ĐỔI ĐÃ THỰC HIỆN:
   1. Bỏ ràng buộc CK_CaLam_Gio để hỗ trợ ca qua đêm (22:00-06:00)
   2. Sửa CTE OT trong sp_ChayBangLuong để tính giờ OT chính xác theo kỳ lương
   3. Sửa CTE Calc trong trigger tr_ChamCong_AIU_TinhCong xử lý ca qua đêm
   4. Thêm trigger tr_CaLam_CheckOverlap kiểm tra trùng lặp thời gian ca
   5. Thêm trường MoTa và KichHoat vào bảng CaLam
   6. Cập nhật các stored procedure CaLam để hỗ trợ trường mới
   ========================================================= */

------------------------------------------------------------
-- 0) Tạo CSDL
------------------------------------------------------------
IF DB_ID(N'QLNhanSuSieuThiMini') IS NULL
    CREATE DATABASE QLNhanSuSieuThiMini;
GO
USE QLNhanSuSieuThiMini;
GO


/* =========================================================
   1) NGUOIDUNG: Quản lý tài khoản đăng nhập & phân quyền
   ========================================================= */
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


/* =========================================================
   2) NHANVIEN: Hồ sơ nhân sự
   ========================================================= */
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
    PhongBan    NVARCHAR(80) NULL,
    ChucDanh    NVARCHAR(80) NULL,
    LuongCoBan  DECIMAL(12,2) NOT NULL,

    CONSTRAINT FK_NhanVien_NguoiDung
        FOREIGN KEY(MaNguoiDung) REFERENCES dbo.NguoiDung(MaNguoiDung)
        ON DELETE SET NULL
);
GO
-- RÀNG BUỘC / GIẢI THÍCH
ALTER TABLE dbo.NhanVien ADD CONSTRAINT UQ_NhanVien_DienThoai UNIQUE(DienThoai);
-- Số điện thoại không được trùng.

ALTER TABLE dbo.NhanVien ADD CONSTRAINT UQ_NhanVien_Email UNIQUE(Email);
-- Email không được trùng.

ALTER TABLE dbo.NhanVien ADD CONSTRAINT CK_NhanVien_TrangThai CHECK(TrangThai IN (N'DangLam',N'Nghi'));
-- Chỉ chấp nhận 2 trạng thái nhân viên.

ALTER TABLE dbo.NhanVien WITH NOCHECK 
  ADD CONSTRAINT CK_NhanVien_GioiTinh CHECK(GioiTinh IS NULL OR GioiTinh IN (N'Nam',N'Nu',N'Khac'));
-- Kiểm soát giá trị giới tính nếu có.

CREATE UNIQUE INDEX UQ_NhanVien_MaNguoiDung_NotNull
ON dbo.NhanVien(MaNguoiDung) WHERE MaNguoiDung IS NOT NULL;
-- Một tài khoản chỉ gắn cho một nhân viên.

CREATE INDEX IX_NhanVien_Phong_Chuc_TrangThai 
ON dbo.NhanVien(PhongBan,ChucDanh,TrangThai);
-- Tăng tốc lọc nhân sự theo phòng ban/chức danh.


/* =========================================================
   3) CALAM: Ca làm việc
   ========================================================= */
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


/* =========================================================
   4) LICHPHANCA: Lịch phân ca hàng ngày
   ========================================================= */
IF OBJECT_ID('dbo.LichPhanCa','U') IS NOT NULL DROP TABLE dbo.LichPhanCa;
GO
CREATE TABLE dbo.LichPhanCa (
    MaLich    INT IDENTITY(1,1) PRIMARY KEY,
    MaNV      INT NOT NULL,
    NgayLam   DATE NOT NULL,
    MaCa      INT NOT NULL,
    TrangThai NVARCHAR(12) NOT NULL 
        CONSTRAINT DF_LichPhanCa_TrangThai DEFAULT(N'DuKien'),

    CONSTRAINT FK_LichPhanCa_NhanVien FOREIGN KEY(MaNV) REFERENCES dbo.NhanVien(MaNV) ON DELETE CASCADE,
    CONSTRAINT FK_LichPhanCa_CaLam FOREIGN KEY(MaCa) REFERENCES dbo.CaLam(MaCa)
);
GO
-- RÀNG BUỘC / GIẢI THÍCH
ALTER TABLE dbo.LichPhanCa ADD CONSTRAINT UQ_LichPhanCa_MaNV_Ngay UNIQUE(MaNV,NgayLam);
-- Mỗi nhân viên chỉ có 1 ca/ngày.

ALTER TABLE dbo.LichPhanCa 
  ADD CONSTRAINT CK_LichPhanCa_TrangThai CHECK(TrangThai IN (N'DuKien',N'Khoa',N'Huy'));
-- Trạng thái lịch chỉ trong 3 giá trị hợp lệ.

CREATE INDEX IX_LichPhanCa_NgayLam ON dbo.LichPhanCa(NgayLam);
-- Tăng tốc truy vấn lịch theo ngày.


/* =========================================================
   5) CHAMCONG: Ghi nhận giờ làm thực tế
   ========================================================= */
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


/* =========================================================
   6) DONTU: Đơn nghỉ/OT
   ========================================================= */
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


/* =========================================================
   7) BANGLUONG: Lương theo tháng/năm
   ========================================================= */
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

------------------------------------------------------------
-- I) VIEWS
------------------------------------------------------------

-- 1) Tổng hợp công theo tháng
IF OBJECT_ID('dbo.vw_CongThang','V') IS NOT NULL DROP VIEW dbo.vw_CongThang;
GO
CREATE VIEW dbo.vw_CongThang
AS
SELECT 
    c.MaNV,
    YEAR(c.NgayLam)  AS Nam,
    MONTH(c.NgayLam) AS Thang,
    SUM(ISNULL(c.GioCong,0)) AS TongGioCong,
    SUM(CASE WHEN ISNULL(c.DiTrePhut,0)>0 THEN c.DiTrePhut ELSE 0 END) AS TongPhutDiTre,
    SUM(CASE WHEN ISNULL(c.VeSomPhut,0)>0 THEN c.VeSomPhut ELSE 0 END) AS TongPhutVeSom
FROM dbo.ChamCong c
GROUP BY c.MaNV, YEAR(c.NgayLam), MONTH(c.NgayLam);
GO

-- 2) Lịch + chấm công theo ngày
IF OBJECT_ID('dbo.vw_Lich_ChamCong_Ngay','V') IS NOT NULL DROP VIEW dbo.vw_Lich_ChamCong_Ngay;
GO
CREATE VIEW dbo.vw_Lich_ChamCong_Ngay
AS
SELECT 
    nv.MaNV, nv.HoTen, lpc.NgayLam,
    cl.TenCa, cl.GioBatDau, cl.GioKetThuc, cl.HeSoCa,
    lpc.TrangThai AS TrangThaiLich,
    cc.GioVao, cc.GioRa, cc.GioCong, cc.DiTrePhut, cc.VeSomPhut, cc.Khoa AS KhoaChamCong
FROM dbo.LichPhanCa lpc
JOIN dbo.NhanVien nv ON nv.MaNV = lpc.MaNV
JOIN dbo.CaLam cl     ON cl.MaCa = lpc.MaCa
LEFT JOIN dbo.ChamCong cc ON cc.MaNV = lpc.MaNV AND cc.NgayLam = lpc.NgayLam;
GO

-- 10) Stored Procedures cho CaLam CRUD operations
IF OBJECT_ID('dbo.sp_CaLam_GetAll','P') IS NOT NULL DROP PROCEDURE dbo.sp_CaLam_GetAll;
GO
CREATE PROCEDURE dbo.sp_CaLam_GetAll
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT MaCa, TenCa, GioBatDau, GioKetThuc, HeSoCa, MoTa, KichHoat
    FROM dbo.CaLam 
    WHERE KichHoat = 1
    ORDER BY GioBatDau;
END
GO

IF OBJECT_ID('dbo.sp_CaLam_GetById','P') IS NOT NULL DROP PROCEDURE dbo.sp_CaLam_GetById;
GO
CREATE PROCEDURE dbo.sp_CaLam_GetById
    @MaCa INT
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT MaCa, TenCa, GioBatDau, GioKetThuc, HeSoCa, MoTa, KichHoat
    FROM dbo.CaLam 
    WHERE MaCa = @MaCa;
END
GO

IF OBJECT_ID('dbo.sp_CaLam_Insert','P') IS NOT NULL DROP PROCEDURE dbo.sp_CaLam_Insert;
GO
CREATE PROCEDURE dbo.sp_CaLam_Insert
    @TenCa       NVARCHAR(60),
    @GioBatDau   TIME(0),
    @GioKetThuc  TIME(0),
    @HeSoCa      DECIMAL(4,2),
    @MoTa        NVARCHAR(255) = NULL,
    @KichHoat    BIT = 1,
    @MaCa_OUT    INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    -- Kiểm tra validation
    IF @TenCa IS NULL OR LTRIM(RTRIM(@TenCa)) = ''
    BEGIN
        RAISERROR(N'Tên ca không được để trống.', 16, 1);
        RETURN;
    END

    -- Bỏ kiểm tra giờ bắt đầu < giờ kết thúc để hỗ trợ ca qua đêm
    -- Logic kiểm tra ca qua đêm sẽ được xử lý ở tầng ứng dụng

    IF @HeSoCa <= 0
    BEGIN
        RAISERROR(N'Hệ số ca phải lớn hơn 0.', 16, 1);
        RETURN;
    END

    -- Kiểm tra trùng tên ca
    IF EXISTS (SELECT 1 FROM dbo.CaLam WHERE TenCa = @TenCa)
    BEGIN
        RAISERROR(N'Tên ca đã tồn tại.', 16, 1);
        RETURN;
    END

    BEGIN TRAN;

    INSERT INTO dbo.CaLam (TenCa, GioBatDau, GioKetThuc, HeSoCa, MoTa, KichHoat)
    VALUES (@TenCa, @GioBatDau, @GioKetThuc, @HeSoCa, @MoTa, @KichHoat);

    SET @MaCa_OUT = SCOPE_IDENTITY();

    COMMIT;
END
GO

IF OBJECT_ID('dbo.sp_CaLam_Update','P') IS NOT NULL DROP PROCEDURE dbo.sp_CaLam_Update;
GO
CREATE PROCEDURE dbo.sp_CaLam_Update
    @MaCa        INT,
    @TenCa       NVARCHAR(60),
    @GioBatDau   TIME(0),
    @GioKetThuc  TIME(0),
    @HeSoCa      DECIMAL(4,2),
    @MoTa        NVARCHAR(255) = NULL,
    @KichHoat    BIT = 1
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    -- Kiểm tra ca làm tồn tại
    IF NOT EXISTS (SELECT 1 FROM dbo.CaLam WHERE MaCa = @MaCa)
    BEGIN
        RAISERROR(N'Ca làm không tồn tại.', 16, 1);
        RETURN;
    END

    -- Kiểm tra validation
    IF @TenCa IS NULL OR LTRIM(RTRIM(@TenCa)) = ''
    BEGIN
        RAISERROR(N'Tên ca không được để trống.', 16, 1);
        RETURN;
    END

    -- Bỏ kiểm tra giờ bắt đầu < giờ kết thúc để hỗ trợ ca qua đêm
    -- Logic kiểm tra ca qua đêm sẽ được xử lý ở tầng ứng dụng

    IF @HeSoCa <= 0
    BEGIN
        RAISERROR(N'Hệ số ca phải lớn hơn 0.', 16, 1);
        RETURN;
    END

    -- Kiểm tra trùng tên ca (trừ chính nó)
    IF EXISTS (SELECT 1 FROM dbo.CaLam WHERE TenCa = @TenCa AND MaCa <> @MaCa)
    BEGIN
        RAISERROR(N'Tên ca đã tồn tại.', 16, 1);
        RETURN;
    END

    BEGIN TRAN;

    UPDATE dbo.CaLam 
    SET TenCa = @TenCa,
        GioBatDau = @GioBatDau,
        GioKetThuc = @GioKetThuc,
        HeSoCa = @HeSoCa,
        MoTa = @MoTa,
        KichHoat = @KichHoat
    WHERE MaCa = @MaCa;

    COMMIT;
END
GO

IF OBJECT_ID('dbo.sp_CaLam_Delete','P') IS NOT NULL DROP PROCEDURE dbo.sp_CaLam_Delete;
GO
CREATE PROCEDURE dbo.sp_CaLam_Delete
    @MaCa INT
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    -- Kiểm tra ca làm tồn tại
    IF NOT EXISTS (SELECT 1 FROM dbo.CaLam WHERE MaCa = @MaCa)
    BEGIN
        RAISERROR(N'Ca làm không tồn tại.', 16, 1);
        RETURN;
    END

    -- Kiểm tra ca làm có đang được sử dụng trong lịch phân ca không
    IF EXISTS (SELECT 1 FROM dbo.LichPhanCa WHERE MaCa = @MaCa)
    BEGIN
        RAISERROR(N'Không thể xóa ca làm đang được sử dụng trong lịch phân ca.', 16, 1);
        RETURN;
    END

    BEGIN TRAN;

    DELETE FROM dbo.CaLam WHERE MaCa = @MaCa;

    COMMIT;
END
GO

-- Cấp quyền thực thi stored procedures cho CaLam
GRANT EXECUTE ON dbo.sp_CaLam_GetAll TO r_hr, r_quanly, r_nhanvien;
GRANT EXECUTE ON dbo.sp_CaLam_GetById TO r_hr, r_quanly, r_nhanvien;
GRANT EXECUTE ON dbo.sp_CaLam_Insert TO r_hr;
GRANT EXECUTE ON dbo.sp_CaLam_Update TO r_hr;
GRANT EXECUTE ON dbo.sp_CaLam_Delete TO r_hr;
GO

------------------------------------------------------------
-- IV) SECURITY: RBAC + DAC + (tùy chọn) RLS
------------------------------------------------------------
-- II) FUNCTIONS
------------------------------------------------------------

-- 1) Trả về khung ca của NV trong ngày (giờ bắt đầu/kết thúc, hệ số)
IF OBJECT_ID('dbo.fn_KhungCa','IF') IS NOT NULL DROP FUNCTION dbo.fn_KhungCa;
GO
CREATE FUNCTION dbo.fn_KhungCa(@MaNV INT, @Ngay DATE)
RETURNS TABLE
AS
RETURN
(
    SELECT TOP(1)
        cl.GioBatDau AS GioBatDau,
        cl.GioKetThuc AS GioKetThuc,
        cl.HeSoCa     AS HeSoCa
    FROM dbo.LichPhanCa l
    JOIN dbo.CaLam cl ON cl.MaCa = l.MaCa
    WHERE l.MaNV = @MaNV AND l.NgayLam = @Ngay
);
GO

-- 2) Số phút dương (âm => 0), tiện tính đi trễ/về sớm
IF OBJECT_ID('dbo.fn_SoPhutDuong','FN') IS NOT NULL DROP FUNCTION dbo.fn_SoPhutDuong;
GO
CREATE FUNCTION dbo.fn_SoPhutDuong(@A DATETIME2(0), @B DATETIME2(0))
RETURNS INT
AS
BEGIN
    IF @A IS NULL OR @B IS NULL RETURN NULL;
    DECLARE @m INT = DATEDIFF(MINUTE, @A, @B);
    RETURN CASE WHEN @m > 0 THEN @m ELSE 0 END;
END
GO

/* ---------- (Tùy chọn) Row-Level Security cho Employee ----------
   Ứng dụng cần đặt MaNV phiên làm việc:
       EXEC dbo.sp_SetSessionContextNhanVien @MaNV = <id>;
   Nhân viên chỉ nhìn thấy bản ghi MaNV = giá trị trong SESSION_CONTEXT,
   còn HR/QuanLy/KeToan nhìn thấy tất cả.
------------------------------------------------------------------ */

-- 3) Predicate RLS theo MaNV
IF OBJECT_ID('dbo.fn_rls_NhanVien','IF') IS NOT NULL DROP FUNCTION dbo.fn_rls_NhanVien;
GO
CREATE FUNCTION dbo.fn_rls_NhanVien(@MaNV INT)
RETURNS TABLE
WITH SCHEMABINDING
AS
RETURN
(
    SELECT 1 AS Allow
    WHERE TRY_CONVERT(INT, SESSION_CONTEXT(N'MaNV')) = @MaNV
       OR IS_ROLEMEMBER(N'r_hr') = 1
       OR IS_ROLEMEMBER(N'r_quanly') = 1
       OR IS_ROLEMEMBER(N'r_ketoan') = 1
);
GO



------------------------------------------------------------
-- III) STORED PROCEDURES (ACID)
------------------------------------------------------------

-- 0) Ghi MaNV vào SESSION_CONTEXT (phục vụ RLS)
IF OBJECT_ID('dbo.sp_SetSessionContextNhanVien','P') IS NOT NULL DROP PROCEDURE dbo.sp_SetSessionContextNhanVien;
GO
CREATE PROCEDURE dbo.sp_SetSessionContextNhanVien
    @MaNV INT
AS
BEGIN
    SET NOCOUNT ON;
    EXEC sys.sp_set_session_context @key=N'MaNV', @value=@MaNV, @read_only=0;
END
GO


-- 1) Thêm mới nhân viên (tùy chọn tạo tài khoản kèm theo)
IF OBJECT_ID('dbo.sp_ThemMoiNhanVien','P') IS NOT NULL DROP PROCEDURE dbo.sp_ThemMoiNhanVien;
GO
CREATE PROCEDURE dbo.sp_ThemMoiNhanVien
    @HoTen       NVARCHAR(120),
    @NgaySinh    DATE         = NULL,
    @GioiTinh    NVARCHAR(10) = NULL,
    @DienThoai   NVARCHAR(20) = NULL,
    @Email       NVARCHAR(120)= NULL,
    @DiaChi      NVARCHAR(255)= NULL,
    @NgayVaoLam  DATE         = NULL,
    @PhongBan    NVARCHAR(80) = NULL,
    @ChucDanh    NVARCHAR(80) = NULL,
    @LuongCoBan  DECIMAL(12,2),
    -- tạo tài khoản (tùy chọn)
    @TaoTaiKhoan BIT = 0,
    @TenDangNhap NVARCHAR(50) = NULL,
    @MatKhauHash NVARCHAR(200)= NULL,
    @VaiTro      NVARCHAR(20) = N'NhanVien', -- HR/QuanLy/KeToan/NhanVien
    @MaNV_OUT    INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    IF @NgayVaoLam IS NULL SET @NgayVaoLam = CONVERT(date, GETDATE());

    BEGIN TRAN;

    DECLARE @MaNguoiDung INT = NULL;

    IF @TaoTaiKhoan = 1
    BEGIN
        IF EXISTS (SELECT 1 FROM dbo.NguoiDung WHERE TenDangNhap = @TenDangNhap)
        BEGIN
            RAISERROR(N'Tên đăng nhập đã tồn tại.', 16, 1);
            ROLLBACK; RETURN;
        END

        INSERT INTO dbo.NguoiDung(TenDangNhap,MatKhauHash,VaiTro,KichHoat)
        VALUES(@TenDangNhap,@MatKhauHash,@VaiTro,1);

        SET @MaNguoiDung = SCOPE_IDENTITY();
    END

    INSERT INTO dbo.NhanVien
    (MaNguoiDung,HoTen,NgaySinh,GioiTinh,DienThoai,Email,DiaChi,NgayVaoLam,TrangThai,PhongBan,ChucDanh,LuongCoBan)
    VALUES
    (@MaNguoiDung,@HoTen,@NgaySinh,@GioiTinh,@DienThoai,@Email,@DiaChi,@NgayVaoLam,N'DangLam',@PhongBan,@ChucDanh,@LuongCoBan);

    SET @MaNV_OUT = SCOPE_IDENTITY();

    COMMIT;
END
GO


-- 2) Duyệt/Từ chối đơn từ (atomic)
--    - NGHI: cập nhật/ghi dòng ChamCong các ngày trùng lịch về 0 giờ công.
--    - OT: đánh dấu đã duyệt; giờ OT cộng khi chạy lương.
IF OBJECT_ID('dbo.sp_DuyetDonTu','P') IS NOT NULL DROP PROCEDURE dbo.sp_DuyetDonTu;
GO
CREATE PROCEDURE dbo.sp_DuyetDonTu
    @MaDon        INT,
    @MaNguoiDuyet INT,
    @ChapNhan     BIT      -- 1 = duyệt, 0 = từ chối
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    DECLARE @Loai NVARCHAR(10), @MaNV INT, @Tu DATETIME2(0), @Den DATETIME2(0), @SoGio DECIMAL(5,2);

    BEGIN TRAN;

    SELECT @Loai=Loai, @MaNV=MaNV, @Tu=TuLuc, @Den=DenLuc, @SoGio=SoGio
    FROM dbo.DonTu WITH (UPDLOCK, ROWLOCK)
    WHERE MaDon = @MaDon;

    IF @@ROWCOUNT = 0
    BEGIN
        RAISERROR(N'Không tìm thấy đơn từ.',16,1);
        ROLLBACK; RETURN;
    END

    IF @ChapNhan = 1
    BEGIN
        UPDATE dbo.DonTu
        SET TrangThai = N'DaDuyet',
            DuyetBoi  = @MaNguoiDuyet,
            SoGio     = ISNULL(@SoGio, CAST(DATEDIFF(MINUTE,@Tu,@Den)/60.0 AS DECIMAL(5,2)))
        WHERE MaDon = @MaDon;

        IF @Loai = N'NGHI'
        BEGIN
            ;WITH Dates AS (
                SELECT CAST(@Tu AS DATE) d
                UNION ALL
                SELECT DATEADD(DAY,1,d) FROM Dates WHERE d < CAST(@Den AS DATE)
            )
            MERGE dbo.ChamCong AS T
            USING (
                SELECT @MaNV AS MaNV, d AS NgayLam FROM Dates
                WHERE EXISTS (SELECT 1 FROM dbo.LichPhanCa l WHERE l.MaNV=@MaNV AND l.NgayLam=d)
            ) AS S
            ON (T.MaNV=S.MaNV AND T.NgayLam=S.NgayLam)
            WHEN MATCHED THEN
                UPDATE SET GioVao=NULL, GioRa=NULL, GioCong=0, DiTrePhut=0, VeSomPhut=0
            WHEN NOT MATCHED THEN
                INSERT(MaNV,NgayLam,GioVao,GioRa,GioCong,DiTrePhut,VeSomPhut,Khoa)
                VALUES(@MaNV, S.NgayLam, NULL, NULL, 0, 0, 0, 0)
            OPTION (MAXRECURSION 366);
        END
    END
    ELSE
    BEGIN
        UPDATE dbo.DonTu
        SET TrangThai = N'TuChoi',
            DuyetBoi  = @MaNguoiDuyet
        WHERE MaDon = @MaDon;
    END

    COMMIT;
END
GO


-- 3) Khóa công kỳ (tháng/năm): LichPhanCa.TrangThai='Khoa', ChamCong.Khoa=1
IF OBJECT_ID('dbo.sp_KhoaCongThang','P') IS NOT NULL DROP PROCEDURE dbo.sp_KhoaCongThang;
GO
CREATE PROCEDURE dbo.sp_KhoaCongThang
    @Nam   INT,
    @Thang INT
AS
BEGIN
    SET NOCOUNT ON; SET XACT_ABORT ON;

    DECLARE @D0 DATE = DATEFROMPARTS(@Nam,@Thang,1);
    DECLARE @D1 DATE = EOMONTH(@D0);

    BEGIN TRAN;

    UPDATE dbo.LichPhanCa
      SET TrangThai = N'Khoa'
    WHERE NgayLam BETWEEN @D0 AND @D1 AND TrangThai <> N'Khoa';

    UPDATE dbo.ChamCong
      SET Khoa = 1
    WHERE NgayLam BETWEEN @D0 AND @D1 AND Khoa = 0;

    COMMIT;
END
GO


-- 4) Chạy bảng lương (Serializable): upsert BangLuong ở trạng thái 'Mo'
IF OBJECT_ID('dbo.sp_ChayBangLuong','P') IS NOT NULL DROP PROCEDURE dbo.sp_ChayBangLuong;
GO
CREATE PROCEDURE dbo.sp_ChayBangLuong
    @Nam INT,
    @Thang INT,
    @StdHours DECIMAL(7,2) = 208,  -- giờ chuẩn/tháng
    @OtRate   DECIMAL(4,2) = 1.5   -- hệ số OT
AS
BEGIN
    SET NOCOUNT ON; SET XACT_ABORT ON;

    DECLARE @D0 DATE = DATEFROMPARTS(@Nam,@Thang,1);
    DECLARE @D1 DATE = EOMONTH(@D0);

    SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;
    BEGIN TRAN;

    -- (Consistency) Bảo đảm đã khóa công
    IF EXISTS (SELECT 1 FROM dbo.LichPhanCa WHERE NgayLam BETWEEN @D0 AND @D1 AND TrangThai <> N'Khoa')
       OR EXISTS (SELECT 1 FROM dbo.ChamCong   WHERE NgayLam BETWEEN @D0 AND @D1 AND Khoa = 0)
    BEGIN
        RAISERROR(N'Chưa khóa công kỳ %d-%02d.',16,1,@Nam,@Thang);
        ROLLBACK; RETURN;
    END

    ;WITH Cong AS (
        SELECT MaNV, SUM(ISNULL(GioCong,0)) AS TongGioCong
        FROM dbo.ChamCong
        WHERE NgayLam BETWEEN @D0 AND @D1
        GROUP BY MaNV
    ),
    OT AS (
        SELECT
            r.MaNV,
            SUM(
                CASE
                    WHEN r.Loai = N'OT' AND r.TrangThai = N'DaDuyet'
                    THEN
                        -- Tính toán khoảng thời gian OT thực sự nằm trong tháng
                        CAST(
                            DATEDIFF(
                                MINUTE,
                                -- Lấy thời điểm bắt đầu muộn hơn (giữa TuLuc và đầu tháng)
                                CASE WHEN r.TuLuc > @D0 THEN r.TuLuc ELSE @D0 END,
                                -- Lấy thời điểm kết thúc sớm hơn (giữa DenLuc và cuối tháng)
                                CASE WHEN r.DenLuc < DATEADD(DAY, 1, @D1) THEN r.DenLuc ELSE DATEADD(DAY, 1, @D1) END
                            ) / 60.0
                        AS DECIMAL(7,2))
                    ELSE 0
                END
            ) AS GioOT
        FROM dbo.DonTu r
        -- Lấy tất cả đơn từ có khoảng thời gian giao với tháng đang xét
        WHERE r.TuLuc < DATEADD(DAY, 1, @D1) AND r.DenLuc >= @D0
        GROUP BY r.MaNV
    ),
    Src AS (
        SELECT nv.MaNV,
               nv.LuongCoBan,
               ISNULL(c.TongGioCong,0) AS TongGioCong,
               ISNULL(o.GioOT,0)       AS GioOT
        FROM dbo.NhanVien nv
        LEFT JOIN Cong c ON c.MaNV = nv.MaNV
        LEFT JOIN OT   o ON o.MaNV = nv.MaNV
    )
    MERGE dbo.BangLuong AS T
    USING Src AS S
    ON (T.Nam=@Nam AND T.Thang=@Thang AND T.MaNV=S.MaNV)
    WHEN MATCHED AND T.TrangThai=N'Mo' THEN
        UPDATE SET 
          T.LuongCoBan  = S.LuongCoBan,
          T.TongGioCong = S.TongGioCong,
          T.GioOT       = S.GioOT,
          T.ThucLanh    = S.LuongCoBan 
                         + (CASE WHEN @StdHours>0 THEN (S.GioOT*(S.LuongCoBan/@StdHours)*@OtRate) ELSE 0 END)
                         + T.PhuCap - T.KhauTru - T.ThueBH
    WHEN NOT MATCHED THEN
        INSERT (Nam,Thang,MaNV,LuongCoBan,TongGioCong,GioOT,PhuCap,KhauTru,ThueBH,ThucLanh,TrangThai)
        VALUES(@Nam,@Thang,S.MaNV,S.LuongCoBan,S.TongGioCong,S.GioOT,0,0,0,
               S.LuongCoBan + (CASE WHEN @StdHours>0 THEN (S.GioOT*(S.LuongCoBan/@StdHours)*@OtRate) ELSE 0 END),
               N'Mo');

    COMMIT;
END
GO


-- 5) Đóng bảng lương: chuyển 'Mo' → 'Dong'
IF OBJECT_ID('dbo.sp_DongBangLuong','P') IS NOT NULL DROP PROCEDURE dbo.sp_DongBangLuong;
GO
CREATE PROCEDURE dbo.sp_DongBangLuong
    @Nam INT, @Thang INT
AS
BEGIN
    SET NOCOUNT ON; SET XACT_ABORT ON;
    BEGIN TRAN;
      UPDATE dbo.BangLuong SET TrangThai=N'Dong'
      WHERE Nam=@Nam AND Thang=@Thang AND TrangThai=N'Mo';
    COMMIT;
END
GO

-- 6) Cập nhật thông tin nhân viên (với kiểm tra role)
IF OBJECT_ID('dbo.sp_CapNhatNhanVien','P') IS NOT NULL DROP PROCEDURE dbo.sp_CapNhatNhanVien;
GO
CREATE PROCEDURE dbo.sp_CapNhatNhanVien
    @MaNV        INT,
    @HoTen       NVARCHAR(120),
    @NgaySinh    DATE         = NULL,
    @GioiTinh    NVARCHAR(10) = NULL,
    @DienThoai   NVARCHAR(20) = NULL,
    @Email       NVARCHAR(120)= NULL,
    @DiaChi      NVARCHAR(255)= NULL,
    @NgayVaoLam  DATE         = NULL,
    @TrangThai   NVARCHAR(10) = NULL,
    @PhongBan    NVARCHAR(80) = NULL,
    @ChucDanh    NVARCHAR(80) = NULL,
    @LuongCoBan  DECIMAL(12,2)= NULL,
    @MaNguoiDung INT          = NULL  -- ID của người thực hiện thao tác
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    -- Kiểm tra quyền truy cập
    DECLARE @VaiTro NVARCHAR(20);
    IF @MaNguoiDung IS NOT NULL
    BEGIN
        SELECT @VaiTro = VaiTro FROM dbo.NguoiDung WHERE MaNguoiDung = @MaNguoiDung AND KichHoat = 1;
        
        IF @VaiTro IS NULL
        BEGIN
            RAISERROR(N'Người dùng không tồn tại hoặc đã bị khóa.', 16, 1);
            RETURN;
        END
        
        -- Chỉ HR mới được cập nhật thông tin nhân viên
        IF @VaiTro NOT IN (N'HR')
        BEGIN
            RAISERROR(N'Bạn không có quyền cập nhật thông tin nhân viên.', 16, 1);
            RETURN;
        END
    END

    -- Kiểm tra nhân viên tồn tại
    IF NOT EXISTS (SELECT 1 FROM dbo.NhanVien WHERE MaNV = @MaNV)
    BEGIN
        RAISERROR(N'Nhân viên không tồn tại.', 16, 1);
        RETURN;
    END

    BEGIN TRAN;

    UPDATE dbo.NhanVien 
    SET HoTen = ISNULL(@HoTen, HoTen),
        NgaySinh = ISNULL(@NgaySinh, NgaySinh),
        GioiTinh = ISNULL(@GioiTinh, GioiTinh),
        DienThoai = ISNULL(@DienThoai, DienThoai),
        Email = ISNULL(@Email, Email),
        DiaChi = ISNULL(@DiaChi, DiaChi),
        NgayVaoLam = ISNULL(@NgayVaoLam, NgayVaoLam),
        TrangThai = ISNULL(@TrangThai, TrangThai),
        PhongBan = ISNULL(@PhongBan, PhongBan),
        ChucDanh = ISNULL(@ChucDanh, ChucDanh),
        LuongCoBan = ISNULL(@LuongCoBan, LuongCoBan)
    WHERE MaNV = @MaNV;

    COMMIT;
END
GO

-- 7) Xóa nhân viên (soft delete - chuyển trạng thái thành 'Nghi')
IF OBJECT_ID('dbo.sp_XoaNhanVien','P') IS NOT NULL DROP PROCEDURE dbo.sp_XoaNhanVien;
GO
CREATE PROCEDURE dbo.sp_XoaNhanVien
    @MaNV        INT,
    @MaNguoiDung INT = NULL,  -- ID của người thực hiện thao tác
    @LyDoXoa     NVARCHAR(255) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    -- Kiểm tra quyền truy cập
    DECLARE @VaiTro NVARCHAR(20);
    IF @MaNguoiDung IS NOT NULL
    BEGIN
        SELECT @VaiTro = VaiTro FROM dbo.NguoiDung WHERE MaNguoiDung = @MaNguoiDung AND KichHoat = 1;
        
        IF @VaiTro IS NULL
        BEGIN
            RAISERROR(N'Người dùng không tồn tại hoặc đã bị khóa.', 16, 1);
            RETURN;
        END
        
        -- Chỉ HR mới được xóa nhân viên
        IF @VaiTro NOT IN (N'HR')
        BEGIN
            RAISERROR(N'Bạn không có quyền xóa nhân viên.', 16, 1);
            RETURN;
        END
    END

    -- Kiểm tra nhân viên tồn tại
    DECLARE @TrangThaiHienTai NVARCHAR(10);
    SELECT @TrangThaiHienTai = TrangThai FROM dbo.NhanVien WHERE MaNV = @MaNV;
    
    IF @TrangThaiHienTai IS NULL
    BEGIN
        RAISERROR(N'Nhân viên không tồn tại.', 16, 1);
        RETURN;
    END

    IF @TrangThaiHienTai = N'Nghi'
    BEGIN
        RAISERROR(N'Nhân viên đã ở trạng thái nghỉ việc.', 16, 1);
        RETURN;
    END

    BEGIN TRAN;

    -- Cập nhật trạng thái nhân viên thành 'Nghi'
    UPDATE dbo.NhanVien 
    SET TrangThai = N'Nghi'
    WHERE MaNV = @MaNV;

    -- Hủy các lịch phân ca trong tương lai
    UPDATE dbo.LichPhanCa 
    SET TrangThai = N'Huy'
    WHERE MaNV = @MaNV 
      AND NgayLam > GETDATE() 
      AND TrangThai = N'DuKien';

    COMMIT;
END
GO

-- 8) Khôi phục nhân viên (chuyển từ 'Nghi' về 'DangLam')
IF OBJECT_ID('dbo.sp_KhoiPhucNhanVien','P') IS NOT NULL DROP PROCEDURE dbo.sp_KhoiPhucNhanVien;
GO
CREATE PROCEDURE dbo.sp_KhoiPhucNhanVien
    @MaNV        INT,
    @MaNguoiDung INT = NULL  -- ID của người thực hiện thao tác
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    -- Kiểm tra quyền truy cập
    DECLARE @VaiTro NVARCHAR(20);
    IF @MaNguoiDung IS NOT NULL
    BEGIN
        SELECT @VaiTro = VaiTro FROM dbo.NguoiDung WHERE MaNguoiDung = @MaNguoiDung AND KichHoat = 1;
        
        IF @VaiTro IS NULL
        BEGIN
            RAISERROR(N'Người dùng không tồn tại hoặc đã bị khóa.', 16, 1);
            RETURN;
        END
        
        -- Chỉ HR mới được khôi phục nhân viên
        IF @VaiTro NOT IN (N'HR')
        BEGIN
            RAISERROR(N'Bạn không có quyền khôi phục nhân viên.', 16, 1);
            RETURN;
        END
    END

    -- Kiểm tra nhân viên tồn tại và đang ở trạng thái 'Nghi'
    DECLARE @TrangThaiHienTai NVARCHAR(10);
    SELECT @TrangThaiHienTai = TrangThai FROM dbo.NhanVien WHERE MaNV = @MaNV;
    
    IF @TrangThaiHienTai IS NULL
    BEGIN
        RAISERROR(N'Nhân viên không tồn tại.', 16, 1);
        RETURN;
    END

    IF @TrangThaiHienTai != N'Nghi'
    BEGIN
        RAISERROR(N'Nhân viên không ở trạng thái nghỉ việc.', 16, 1);
        RETURN;
    END

    BEGIN TRAN;

    -- Khôi phục trạng thái nhân viên về 'DangLam'
    UPDATE dbo.NhanVien 
    SET TrangThai = N'DangLam'
    WHERE MaNV = @MaNV;

    COMMIT;
END
GO

-- 9) Kiểm tra quyền truy cập của người dùng
IF OBJECT_ID('dbo.sp_KiemTraQuyenTruyCap','P') IS NOT NULL DROP PROCEDURE dbo.sp_KiemTraQuyenTruyCap;
GO
CREATE PROCEDURE dbo.sp_KiemTraQuyenTruyCap
    @MaNguoiDung INT,
    @ChucNang    NVARCHAR(50),  -- 'MANAGE_EMPLOYEES', 'MANAGE_PAYROLL', etc.
    @CoQuyen     BIT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    
    DECLARE @VaiTro NVARCHAR(20);
    SET @CoQuyen = 0;
    
    -- Lấy vai trò của người dùng
    SELECT @VaiTro = VaiTro 
    FROM dbo.NguoiDung 
    WHERE MaNguoiDung = @MaNguoiDung AND KichHoat = 1;
    
    IF @VaiTro IS NULL RETURN;
    
    -- Kiểm tra quyền theo chức năng
    IF @ChucNang = 'MANAGE_EMPLOYEES' AND @VaiTro = N'HR'
        SET @CoQuyen = 1;
    ELSE IF @ChucNang = 'MANAGE_SHIFTS' AND @VaiTro = N'HR'
        SET @CoQuyen = 1;
    ELSE IF @ChucNang = 'MANAGE_SCHEDULE' AND @VaiTro IN (N'HR', N'QuanLy')
        SET @CoQuyen = 1;
    ELSE IF @ChucNang = 'MANAGE_ATTENDANCE' AND @VaiTro IN (N'HR', N'QuanLy')
        SET @CoQuyen = 1;
    ELSE IF @ChucNang = 'APPROVE_REQUESTS' AND @VaiTro IN (N'HR', N'QuanLy')
        SET @CoQuyen = 1;
    ELSE IF @ChucNang = 'MANAGE_PAYROLL' AND @VaiTro = N'KeToan'
        SET @CoQuyen = 1;
    ELSE IF @ChucNang = 'VIEW_REPORTS' AND @VaiTro IN (N'HR', N'QuanLy', N'KeToan')
        SET @CoQuyen = 1;
    ELSE IF @ChucNang = 'SUBMIT_REQUESTS'
        SET @CoQuyen = 1;  -- Tất cả nhân viên đều có thể gửi đơn từ
    ELSE IF @ChucNang = 'VIEW_OWN_DATA'
        SET @CoQuyen = 1;  -- Tất cả đều có thể xem dữ liệu của mình
END
GO



------------------------------------------------------------
-- IV) SECURITY: RBAC + DAC + (tùy chọn) RLS
------------------------------------------------------------

-- 1) Tạo ROLE (idempotent)
IF NOT EXISTS (SELECT 1 FROM sys.database_principals WHERE name=N'r_hr')       CREATE ROLE r_hr;
IF NOT EXISTS (SELECT 1 FROM sys.database_principals WHERE name=N'r_quanly')   CREATE ROLE r_quanly;
IF NOT EXISTS (SELECT 1 FROM sys.database_principals WHERE name=N'r_ketoan')   CREATE ROLE r_ketoan;
IF NOT EXISTS (SELECT 1 FROM sys.database_principals WHERE name=N'r_nhanvien') CREATE ROLE r_nhanvien;
GO

/* 2) DAC: Cấp quyền theo yêu cầu
   HR: INSERT/UPDATE/DELETE NhanVien, LichPhanCa; EXEC các proc liên quan
   Store Manager: SELECT LichPhanCa; UPDATE hạn chế ChamCong; (Duyệt đơn qua PROC)
   Accountant: SELECT ChamCong/vw_CongThang; INSERT/UPDATE BangLuong; EXEC tính/chốt lương
   Employee: INSERT DonTu; SELECT các bảng qua RLS (xem phần 3)
*/

-- HR
GRANT SELECT, INSERT, UPDATE, DELETE ON dbo.NhanVien    TO r_hr;
GRANT SELECT, INSERT, UPDATE, DELETE ON dbo.LichPhanCa  TO r_hr;
GRANT SELECT               ON dbo.vw_CongThang          TO r_hr;
GRANT SELECT               ON dbo.vw_Lich_ChamCong_Ngay TO r_hr;
GRANT EXECUTE ON dbo.sp_ThemMoiNhanVien  TO r_hr;
GRANT EXECUTE ON dbo.sp_DuyetDonTu       TO r_hr;
GRANT EXECUTE ON dbo.sp_KhoaCongThang    TO r_hr;

-- Store Manager (không UPDATE trực tiếp trạng thái DonTu)
GRANT SELECT ON dbo.LichPhanCa TO r_quanly;
GRANT UPDATE (GioVao, GioRa, GioCong, DiTrePhut, VeSomPhut) ON dbo.ChamCong TO r_quanly;
GRANT SELECT ON dbo.vw_Lich_ChamCong_Ngay TO r_quanly;
GRANT EXECUTE ON dbo.sp_DuyetDonTu TO r_quanly; -- duyệt qua proc để đảm bảo side-effects

-- Accountant
GRANT SELECT ON dbo.ChamCong TO r_ketoan;
GRANT SELECT ON dbo.vw_CongThang TO r_ketoan;
GRANT SELECT, INSERT, UPDATE ON dbo.BangLuong TO r_ketoan;
GRANT EXECUTE ON dbo.sp_ChayBangLuong TO r_ketoan;
GRANT EXECUTE ON dbo.sp_DongBangLuong TO r_ketoan;

-- Employee
GRANT INSERT ON dbo.DonTu TO r_nhanvien;
GRANT SELECT ON dbo.LichPhanCa TO r_nhanvien;
GRANT SELECT ON dbo.ChamCong   TO r_nhanvien;
GRANT SELECT ON dbo.DonTu      TO r_nhanvien;
GRANT SELECT ON dbo.BangLuong  TO r_nhanvien;
GO


/* 3) (Tùy chọn) RLS: Nhân viên chỉ xem bản ghi của chính họ.
   BẬT chính sách → áp dụng FILTER PREDICATE theo cột MaNV ở 4 bảng.
   Muốn tắt tạm thời: ALTER SECURITY POLICY ... WITH (STATE = OFF);
*/
IF OBJECT_ID('dbo.Policy_RLS_NhanVien','SO') IS NOT NULL DROP SECURITY POLICY dbo.Policy_RLS_NhanVien;
GO
CREATE SECURITY POLICY dbo.Policy_RLS_NhanVien
ADD FILTER PREDICATE dbo.fn_rls_NhanVien(MaNV) ON dbo.LichPhanCa,
ADD FILTER PREDICATE dbo.fn_rls_NhanVien(MaNV) ON dbo.ChamCong,
ADD FILTER PREDICATE dbo.fn_rls_NhanVien(MaNV) ON dbo.DonTu,
ADD FILTER PREDICATE dbo.fn_rls_NhanVien(MaNV) ON dbo.BangLuong
WITH (STATE = ON);
GO



------------------------------------------------------------
-- V) TRIGGERS (có công tắc bỏ qua: SESSION_CONTEXT('SkipTrigger') = 1)
------------------------------------------------------------

-- 1) CHAMCONG: Tự tính GioCong/DiTre/VeSom khi có GioVao & GioRa
IF OBJECT_ID('dbo.tr_ChamCong_AIU_TinhCong','TR') IS NOT NULL DROP TRIGGER dbo.tr_ChamCong_AIU_TinhCong;
GO
CREATE TRIGGER dbo.tr_ChamCong_AIU_TinhCong
ON dbo.ChamCong
AFTER INSERT, UPDATE
AS
BEGIN
    SET NOCOUNT ON;
    IF TRY_CONVERT(INT, SESSION_CONTEXT(N'SkipTrigger')) = 1 RETURN;

    ;WITH I AS (
        SELECT i.MaChamCong, i.MaNV, i.NgayLam, i.GioVao, i.GioRa
        FROM inserted i
    ),
    Calc AS (
        SELECT
            I.MaChamCong,
            CASE
              WHEN I.GioVao IS NOT NULL AND I.GioRa IS NOT NULL
              THEN CAST(DATEDIFF(MINUTE, I.GioVao, I.GioRa)/60.0 AS DECIMAL(5,2))
              ELSE NULL
            END AS GioCongCalc,
            
            -- Ghép ngày làm với giờ bắt đầu ca để so sánh chính xác
            IIF(I.GioVao IS NULL OR KC.GioBatDau IS NULL, NULL,
                dbo.fn_SoPhutDuong(
                    DATETIMEFROMPARTS(YEAR(I.NgayLam), MONTH(I.NgayLam), DAY(I.NgayLam),
                                    DATEPART(hour, KC.GioBatDau), DATEPART(minute, KC.GioBatDau), 0, 0),
                    I.GioVao
                )
            ) AS DiTreCalc,

            -- Với ca qua đêm, giờ kết thúc thuộc về ngày hôm sau
            IIF(I.GioRa IS NULL OR KC.GioKetThuc IS NULL, NULL,
                dbo.fn_SoPhutDuong(
                    I.GioRa,
                    DATETIMEFROMPARTS(
                        YEAR(DATEADD(DAY, IIF(KC.GioKetThuc < KC.GioBatDau, 1, 0), I.NgayLam)),
                        MONTH(DATEADD(DAY, IIF(KC.GioKetThuc < KC.GioBatDau, 1, 0), I.NgayLam)),
                        DAY(DATEADD(DAY, IIF(KC.GioKetThuc < KC.GioBatDau, 1, 0), I.NgayLam)),
                        DATEPART(hour, KC.GioKetThuc), DATEPART(minute, KC.GioKetThuc), 0, 0
                    )
                )
            ) AS VeSomCalc
        FROM I
        OUTER APPLY dbo.fn_KhungCa(I.MaNV, I.NgayLam) KC
    )
    UPDATE c
       SET c.GioCong   = COALESCE(Calc.GioCongCalc, c.GioCong),
           c.DiTrePhut = COALESCE(Calc.DiTreCalc,  c.DiTrePhut),
           c.VeSomPhut = COALESCE(Calc.VeSomCalc,  c.VeSomPhut)
    FROM dbo.ChamCong c
    JOIN Calc ON Calc.MaChamCong = c.MaChamCong;
END
GO

-- 2) CHAMCONG: Chặn UPDATE/DELETE khi đã khóa (Khoa = 1)
IF OBJECT_ID('dbo.tr_ChamCong_BlockWhenLocked_U','TR') IS NOT NULL DROP TRIGGER dbo.tr_ChamCong_BlockWhenLocked_U;
GO
CREATE TRIGGER dbo.tr_ChamCong_BlockWhenLocked_U
ON dbo.ChamCong
AFTER UPDATE
AS
BEGIN
    SET NOCOUNT ON;
    IF TRY_CONVERT(INT, SESSION_CONTEXT(N'SkipTrigger')) = 1 RETURN;

    IF EXISTS (
        SELECT 1
        FROM inserted i
        JOIN deleted d ON d.MaChamCong = i.MaChamCong
        JOIN dbo.ChamCong c ON c.MaChamCong = d.MaChamCong
        WHERE c.Khoa = 1
          AND (UPDATE(GioVao) OR UPDATE(GioRa) OR UPDATE(GioCong)
               OR UPDATE(DiTrePhut) OR UPDATE(VeSomPhut) OR UPDATE(NgayLam) OR UPDATE(MaNV))
    )
    BEGIN
        RAISERROR(N'Chấm công đã khóa, không được chỉnh sửa.', 16, 1);
        ROLLBACK TRANSACTION;
        RETURN;
    END
END
GO

IF OBJECT_ID('dbo.tr_ChamCong_BlockWhenLocked_D','TR') IS NOT NULL DROP TRIGGER dbo.tr_ChamCong_BlockWhenLocked_D;
GO
CREATE TRIGGER dbo.tr_ChamCong_BlockWhenLocked_D
ON dbo.ChamCong
AFTER DELETE
AS
BEGIN
    SET NOCOUNT ON;
    IF TRY_CONVERT(INT, SESSION_CONTEXT(N'SkipTrigger')) = 1 RETURN;

    IF EXISTS (SELECT 1 FROM deleted WHERE Khoa = 1)
    BEGIN
        RAISERROR(N'Chấm công đã khóa, không được xóa.', 16, 1);
        ROLLBACK TRANSACTION;
        RETURN;
    END
END
GO

-- 3) LICHPHANCA: Chặn đổi MaNV/NgayLam/MaCa khi lịch đã Khoa
IF OBJECT_ID('dbo.tr_LichPhanCa_NoEditWhenKhoa','TR') IS NOT NULL DROP TRIGGER dbo.tr_LichPhanCa_NoEditWhenKhoa;
GO
CREATE TRIGGER dbo.tr_LichPhanCa_NoEditWhenKhoa
ON dbo.LichPhanCa
AFTER UPDATE
AS
BEGIN
    SET NOCOUNT ON;
    IF TRY_CONVERT(INT, SESSION_CONTEXT(N'SkipTrigger')) = 1 RETURN;

    IF EXISTS (
        SELECT 1
        FROM inserted i
        JOIN deleted d ON d.MaLich = i.MaLich
        WHERE d.TrangThai = N'Khoa'
          AND ( ISNULL(i.MaNV,0)   <> ISNULL(d.MaNV,0)
             OR ISNULL(i.MaCa,0)   <> ISNULL(d.MaCa,0)
             OR ISNULL(i.NgayLam,'1900-01-01') <> ISNULL(d.NgayLam,'1900-01-01') )
    )
    BEGIN
        RAISERROR(N'Lịch đã khóa, không được thay đổi nhân sự/ca/ngày.', 16, 1);
        ROLLBACK TRANSACTION;
        RETURN;
    END
END
GO

-- 4) BANGLUONG: Chặn UPDATE/DELETE khi TrangThai = 'Dong'
IF OBJECT_ID('dbo.tr_BangLuong_NoEditWhenDong_U','TR') IS NOT NULL DROP TRIGGER dbo.tr_BangLuong_NoEditWhenDong_U;
GO
CREATE TRIGGER dbo.tr_BangLuong_NoEditWhenDong_U
ON dbo.BangLuong
AFTER UPDATE
AS
BEGIN
    SET NOCOUNT ON;
    IF TRY_CONVERT(INT, SESSION_CONTEXT(N'SkipTrigger')) = 1 RETURN;

    IF EXISTS (
        SELECT 1
        FROM inserted i
        JOIN deleted d ON d.MaBangLuong = i.MaBangLuong
        WHERE d.TrangThai = N'Dong'
    )
    BEGIN
        RAISERROR(N'Bảng lương đã chốt (Đóng), không được chỉnh sửa.', 16, 1);
        ROLLBACK TRANSACTION;
        RETURN;
    END
END
GO

IF OBJECT_ID('dbo.tr_BangLuong_NoEditWhenDong_D','TR') IS NOT NULL DROP TRIGGER dbo.tr_BangLuong_NoEditWhenDong_D;
GO
CREATE TRIGGER dbo.tr_BangLuong_NoEditWhenDong_D
ON dbo.BangLuong
AFTER DELETE
AS
BEGIN
    SET NOCOUNT ON;
    IF TRY_CONVERT(INT, SESSION_CONTEXT(N'SkipTrigger')) = 1 RETURN;

    IF EXISTS (SELECT 1 FROM deleted WHERE TrangThai = N'Dong')
    BEGIN
        RAISERROR(N'Bảng lương đã chốt (Đóng), không được xóa.', 16, 1);
        ROLLBACK TRANSACTION;
        RETURN;
    END
END
GO

-- 5) NHANVIEN: Đồng bộ kích hoạt tài khoản theo trạng thái NV
IF OBJECT_ID('dbo.tr_NhanVien_ToggleAccount','TR') IS NOT NULL DROP TRIGGER dbo.tr_NhanVien_ToggleAccount;
GO
CREATE TRIGGER dbo.tr_NhanVien_ToggleAccount
ON dbo.NhanVien
AFTER UPDATE
AS
BEGIN
    SET NOCOUNT ON;
    IF TRY_CONVERT(INT, SESSION_CONTEXT(N'SkipTrigger')) = 1 RETURN;

    -- Khóa tài khoản khi NV nghỉ
    UPDATE nd
    SET nd.KichHoat = 0
    FROM dbo.NguoiDung nd
    JOIN inserted i   ON i.MaNguoiDung = nd.MaNguoiDung
    JOIN deleted  d   ON d.MaNV = i.MaNV
    WHERE i.TrangThai = N'Nghi' AND d.TrangThai <> N'Nghi' AND i.MaNguoiDung IS NOT NULL;

    -- Mở tài khoản lại khi NV quay về Đang làm
    UPDATE nd
    SET nd.KichHoat = 1
    FROM dbo.NguoiDung nd
    JOIN inserted i   ON i.MaNguoiDung = nd.MaNguoiDung
    JOIN deleted  d   ON d.MaNV = i.MaNV
    WHERE i.TrangThai = N'DangLam' AND d.TrangThai <> N'DangLam' AND i.MaNguoiDung IS NOT NULL;
END
GO

-- 6) CALAM: Kiểm tra trùng lặp thời gian ca làm việc
IF OBJECT_ID('dbo.tr_CaLam_CheckOverlap','TR') IS NOT NULL DROP TRIGGER dbo.tr_CaLam_CheckOverlap;
GO
CREATE TRIGGER dbo.tr_CaLam_CheckOverlap
ON dbo.CaLam
AFTER INSERT, UPDATE
AS
BEGIN
    SET NOCOUNT ON;
    IF TRY_CONVERT(INT, SESSION_CONTEXT(N'SkipTrigger')) = 1 RETURN;

    -- Kiểm tra trùng lặp thời gian giữa các ca làm (chưa xử lý ca qua đêm)
    IF EXISTS (
        SELECT 1
        FROM dbo.CaLam t1
        JOIN dbo.CaLam t2 ON t1.MaCa <> t2.MaCa -- So sánh với các ca khác
        WHERE
            t1.MaCa IN (SELECT MaCa FROM inserted) -- Chỉ kiểm tra các ca vừa được thêm/sửa
            AND t1.KichHoat = 1 AND t2.KichHoat = 1 -- Chỉ kiểm tra các ca đang kích hoạt
            AND t1.GioBatDau < t2.GioKetThuc
            AND t1.GioKetThuc > t2.GioBatDau
            -- Lưu ý: Logic này chưa xử lý ca qua đêm, cần cải tiến thêm
    )
    BEGIN
        RAISERROR (N'Thời gian của ca làm bị trùng lặp với một ca khác.', 16, 1);
        ROLLBACK TRANSACTION;
        RETURN;
    END
END
GO
