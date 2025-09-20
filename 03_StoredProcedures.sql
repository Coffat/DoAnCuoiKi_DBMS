/* =========================================================
   PHẦN 3: STORED PROCEDURES
   Dự án DBMS - HR cho Siêu Thị Mini
   ========================================================= */

USE QLNhanSuSieuThiMini;
GO

------------------------------------------------------------
-- III) STORED PROCEDURES (ACID)
------------------------------------------------------------

-- 1) Stored Procedures cho CaLam CRUD operations
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

-- 2) Thêm mới nhân viên (tùy chọn tạo tài khoản kèm theo)
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

PRINT N'=== HOÀN TẤT TẠO STORED PROCEDURES CƠ BẢN ===';
PRINT N'Tiếp theo chạy file: 04_StoredProcedures_Advanced.sql';
