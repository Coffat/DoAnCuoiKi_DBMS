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

    -- Kiểm tra overlap với các ca khác (di chuyển từ trigger)
    DECLARE @MinutesPerDay INT = 1440; -- 24*60

    IF EXISTS (
        SELECT 1
        FROM dbo.CaLam c2
        WHERE c2.KichHoat = 1
          AND c2.TenCa NOT LIKE N'%Hành chính%' AND c2.TenCa NOT LIKE N'%Part-time%'
          AND @KichHoat = 1
          AND @TenCa NOT LIKE N'%Hành chính%' AND @TenCa NOT LIKE N'%Part-time%'
          -- Tính toán overlap
          AND (
              -- Ca mới bắt đầu trước khi ca cũ kết thúc
              CAST(DATEDIFF(MINUTE, '00:00:00', @GioBatDau) AS INT) < CAST(DATEDIFF(MINUTE, '00:00:00', c2.GioKetThuc) AS INT)
                  + CASE WHEN c2.GioKetThuc < c2.GioBatDau THEN @MinutesPerDay ELSE 0 END
              -- Và ca mới kết thúc sau khi ca cũ bắt đầu
              AND CAST(DATEDIFF(MINUTE, '00:00:00', @GioKetThuc) AS INT)
                  + CASE WHEN @GioKetThuc < @GioBatDau THEN @MinutesPerDay ELSE 0 END > CAST(DATEDIFF(MINUTE, '00:00:00', c2.GioBatDau) AS INT)
          )
    )
    BEGIN
        RAISERROR(N'Thời gian của ca làm bị trùng lặp với một ca khác.', 16, 1);
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

    -- Kiểm tra overlap với các ca khác (di chuyển từ trigger)
    DECLARE @MinutesPerDay INT = 1440; -- 24*60
    DECLARE @TenCaCu NVARCHAR(60), @KichHoatCu BIT;

    -- Lấy thông tin ca cũ
    SELECT @TenCaCu = TenCa, @KichHoatCu = KichHoat
    FROM dbo.CaLam WHERE MaCa = @MaCa;

    IF EXISTS (
        SELECT 1
        FROM dbo.CaLam c2
        WHERE c2.MaCa <> @MaCa AND c2.KichHoat = 1
          AND c2.TenCa NOT LIKE N'%Hành chính%' AND c2.TenCa NOT LIKE N'%Part-time%'
          AND @KichHoat = 1
          AND @TenCa NOT LIKE N'%Hành chính%' AND @TenCa NOT LIKE N'%Part-time%'
          -- Tính toán overlap
          AND (
              -- Ca mới bắt đầu trước khi ca cũ kết thúc
              CAST(DATEDIFF(MINUTE, '00:00:00', @GioBatDau) AS INT) < CAST(DATEDIFF(MINUTE, '00:00:00', c2.GioKetThuc) AS INT)
                  + CASE WHEN c2.GioKetThuc < c2.GioBatDau THEN @MinutesPerDay ELSE 0 END
              -- Và ca mới kết thúc sau khi ca cũ bắt đầu
              AND CAST(DATEDIFF(MINUTE, '00:00:00', @GioKetThuc) AS INT)
                  + CASE WHEN @GioKetThuc < @GioBatDau THEN @MinutesPerDay ELSE 0 END > CAST(DATEDIFF(MINUTE, '00:00:00', c2.GioBatDau) AS INT)
          )
    )
    BEGIN
        RAISERROR(N'Thời gian của ca làm bị trùng lặp với một ca khác.', 16, 1);
        RETURN;
    END

    -- Kiểm tra tổng thời gian làm việc không vượt quá 16 tiếng/ngày
    DECLARE @TongThoiGianPhut INT = 0;
    DECLARE @CaMoiThoiGianPhut INT = DATEDIFF(MINUTE, @GioBatDau, @GioKetThuc);
    IF @CaMoiThoiGianPhut < 0 SET @CaMoiThoiGianPhut = @CaMoiThoiGianPhut + 1440; -- Ca qua đêm

    -- Tính tổng thời gian hiện tại của nhân viên trong ngày (không bao gồm ca đang cập nhật)
    SELECT @TongThoiGianPhut = ISNULL(SUM(
        CASE
            WHEN DATEDIFF(MINUTE, cl.GioBatDau, cl.GioKetThuc) < 0
            THEN DATEDIFF(MINUTE, cl.GioBatDau, cl.GioKetThuc) + 1440
            ELSE DATEDIFF(MINUTE, cl.GioBatDau, cl.GioKetThuc)
        END
    ), 0)
    FROM dbo.LichPhanCa lpc
    INNER JOIN dbo.CaLam cl ON lpc.MaCa = cl.MaCa
    WHERE lpc.NgayLam = CAST(GETDATE() AS DATE)
      AND lpc.MaNV IN (SELECT DISTINCT MaNV FROM dbo.LichPhanCa WHERE MaCa = @MaCa)
      AND lpc.TrangThai IN (N'DuKien', N'Mo')
      AND cl.KichHoat = 1
      AND lpc.MaCa <> @MaCa; -- Không tính ca đang được cập nhật

    -- Cộng thêm thời gian ca mới
    SET @TongThoiGianPhut = @TongThoiGianPhut + @CaMoiThoiGianPhut;

    -- Kiểm tra tổng thời gian không vượt quá 16 tiếng (960 phút)
    IF @TongThoiGianPhut > 960
    BEGIN
        DECLARE @TongGio INT = @TongThoiGianPhut / 60;
        DECLARE @TongPhut INT = @TongThoiGianPhut % 60;
        RAISERROR(N'Tổng thời gian làm việc trong ngày (%d giờ %d phút) vượt quá 16 tiếng cho phép.',
                 16, 1, @TongGio, @TongPhut);
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
    @MaPhongBan  INT          = NULL, -- Thay đổi từ NVARCHAR(80) thành INT
    @MaChucVu    INT          = NULL, -- Thay đổi từ NVARCHAR(80) thành INT
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
    (MaNguoiDung,HoTen,NgaySinh,GioiTinh,DienThoai,Email,DiaChi,NgayVaoLam,TrangThai,MaPhongBan,MaChucVu,LuongCoBan)
    VALUES
    (@MaNguoiDung,@HoTen,@NgaySinh,@GioiTinh,@DienThoai,@Email,@DiaChi,@NgayVaoLam,N'DangLam',@MaPhongBan,@MaChucVu,@LuongCoBan);

    SET @MaNV_OUT = SCOPE_IDENTITY();

    COMMIT;
END
GO

PRINT N'=== HOÀN TẤT TẠO STORED PROCEDURES CƠ BẢN ===';
PRINT N'Tiếp theo chạy file: 04_StoredProcedures_Advanced.sql';

-- 3) QUẢN LÝ PHONG BAN
IF OBJECT_ID('dbo.sp_PhongBan_Insert','P') IS NOT NULL DROP PROCEDURE dbo.sp_PhongBan_Insert;
GO
CREATE PROCEDURE dbo.sp_PhongBan_Insert
    @TenPhongBan NVARCHAR(80),
    @MoTa NVARCHAR(255) = NULL,
    @KichHoat BIT = 1,
    @MaPhongBan_OUT INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    -- Kiểm tra validation
    IF @TenPhongBan IS NULL OR LTRIM(RTRIM(@TenPhongBan)) = ''
    BEGIN
        RAISERROR(N'Tên phòng ban không được để trống.', 16, 1);
        RETURN;
    END

    -- Kiểm tra trùng tên
    IF EXISTS (SELECT 1 FROM dbo.PhongBan WHERE TenPhongBan = @TenPhongBan)
    BEGIN
        RAISERROR(N'Tên phòng ban đã tồn tại.', 16, 1);
        RETURN;
    END

    BEGIN TRAN;

    INSERT INTO dbo.PhongBan (TenPhongBan, MoTa, KichHoat)
    VALUES (@TenPhongBan, @MoTa, @KichHoat);

    SET @MaPhongBan_OUT = SCOPE_IDENTITY();

    COMMIT;
END
GO

IF OBJECT_ID('dbo.sp_PhongBan_Update','P') IS NOT NULL DROP PROCEDURE dbo.sp_PhongBan_Update;
GO
CREATE PROCEDURE dbo.sp_PhongBan_Update
    @MaPhongBan INT,
    @TenPhongBan NVARCHAR(80),
    @MoTa NVARCHAR(255) = NULL,
    @KichHoat BIT = 1
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    -- Kiểm tra phòng ban tồn tại
    IF NOT EXISTS (SELECT 1 FROM dbo.PhongBan WHERE MaPhongBan = @MaPhongBan)
    BEGIN
        RAISERROR(N'Phòng ban không tồn tại.', 16, 1);
        RETURN;
    END

    -- Kiểm tra validation
    IF @TenPhongBan IS NULL OR LTRIM(RTRIM(@TenPhongBan)) = ''
    BEGIN
        RAISERROR(N'Tên phòng ban không được để trống.', 16, 1);
        RETURN;
    END

    -- Kiểm tra trùng tên (trừ chính nó)
    IF EXISTS (SELECT 1 FROM dbo.PhongBan WHERE TenPhongBan = @TenPhongBan AND MaPhongBan <> @MaPhongBan)
    BEGIN
        RAISERROR(N'Tên phòng ban đã tồn tại.', 16, 1);
        RETURN;
    END

    BEGIN TRAN;

    UPDATE dbo.PhongBan
    SET TenPhongBan = @TenPhongBan,
        MoTa = @MoTa,
        KichHoat = @KichHoat
    WHERE MaPhongBan = @MaPhongBan;

    COMMIT;
END
GO

IF OBJECT_ID('dbo.sp_PhongBan_Delete','P') IS NOT NULL DROP PROCEDURE dbo.sp_PhongBan_Delete;
GO
CREATE PROCEDURE dbo.sp_PhongBan_Delete
    @MaPhongBan INT
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    -- Kiểm tra phòng ban tồn tại
    IF NOT EXISTS (SELECT 1 FROM dbo.PhongBan WHERE MaPhongBan = @MaPhongBan)
    BEGIN
        RAISERROR(N'Phòng ban không tồn tại.', 16, 1);
        RETURN;
    END

    -- Kiểm tra phòng ban có đang được sử dụng không
    IF EXISTS (SELECT 1 FROM dbo.NhanVien WHERE MaPhongBan = @MaPhongBan)
    BEGIN
        RAISERROR(N'Không thể xóa phòng ban đang được sử dụng bởi nhân viên.', 16, 1);
        RETURN;
    END

    BEGIN TRAN;

    DELETE FROM dbo.PhongBan WHERE MaPhongBan = @MaPhongBan;

    COMMIT;
END
GO

-- 4) QUẢN LÝ CHỨC VỤ
IF OBJECT_ID('dbo.sp_ChucVu_Insert','P') IS NOT NULL DROP PROCEDURE dbo.sp_ChucVu_Insert;
GO
CREATE PROCEDURE dbo.sp_ChucVu_Insert
    @TenChucVu NVARCHAR(80),
    @MoTa NVARCHAR(255) = NULL,
    @KichHoat BIT = 1,
    @MaChucVu_OUT INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    -- Kiểm tra validation
    IF @TenChucVu IS NULL OR LTRIM(RTRIM(@TenChucVu)) = ''
    BEGIN
        RAISERROR(N'Tên chức vụ không được để trống.', 16, 1);
        RETURN;
    END

    -- Kiểm tra trùng tên
    IF EXISTS (SELECT 1 FROM dbo.ChucVu WHERE TenChucVu = @TenChucVu)
    BEGIN
        RAISERROR(N'Tên chức vụ đã tồn tại.', 16, 1);
        RETURN;
    END

    BEGIN TRAN;

    INSERT INTO dbo.ChucVu (TenChucVu, MoTa, KichHoat)
    VALUES (@TenChucVu, @MoTa, @KichHoat);

    SET @MaChucVu_OUT = SCOPE_IDENTITY();

    COMMIT;
END
GO

IF OBJECT_ID('dbo.sp_ChucVu_Update','P') IS NOT NULL DROP PROCEDURE dbo.sp_ChucVu_Update;
GO
CREATE PROCEDURE dbo.sp_ChucVu_Update
    @MaChucVu INT,
    @TenChucVu NVARCHAR(80),
    @MoTa NVARCHAR(255) = NULL,
    @KichHoat BIT = 1
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    -- Kiểm tra chức vụ tồn tại
    IF NOT EXISTS (SELECT 1 FROM dbo.ChucVu WHERE MaChucVu = @MaChucVu)
    BEGIN
        RAISERROR(N'Chức vụ không tồn tại.', 16, 1);
        RETURN;
    END

    -- Kiểm tra validation
    IF @TenChucVu IS NULL OR LTRIM(RTRIM(@TenChucVu)) = ''
    BEGIN
        RAISERROR(N'Tên chức vụ không được để trống.', 16, 1);
        RETURN;
    END

    -- Kiểm tra trùng tên (trừ chính nó)
    IF EXISTS (SELECT 1 FROM dbo.ChucVu WHERE TenChucVu = @TenChucVu AND MaChucVu <> @MaChucVu)
    BEGIN
        RAISERROR(N'Tên chức vụ đã tồn tại.', 16, 1);
        RETURN;
    END

    BEGIN TRAN;

    UPDATE dbo.ChucVu
    SET TenChucVu = @TenChucVu,
        MoTa = @MoTa,
        KichHoat = @KichHoat
    WHERE MaChucVu = @MaChucVu;

    COMMIT;
END
GO

IF OBJECT_ID('dbo.sp_ChucVu_Delete','P') IS NOT NULL DROP PROCEDURE dbo.sp_ChucVu_Delete;
GO
CREATE PROCEDURE dbo.sp_ChucVu_Delete
    @MaChucVu INT
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    -- Kiểm tra chức vụ tồn tại
    IF NOT EXISTS (SELECT 1 FROM dbo.ChucVu WHERE MaChucVu = @MaChucVu)
    BEGIN
        RAISERROR(N'Chức vụ không tồn tại.', 16, 1);
        RETURN;
    END

    -- Kiểm tra chức vụ có đang được sử dụng không
    IF EXISTS (SELECT 1 FROM dbo.NhanVien WHERE MaChucVu = @MaChucVu)
    BEGIN
        RAISERROR(N'Không thể xóa chức vụ đang được sử dụng bởi nhân viên.', 16, 1);
        RETURN;
    END

    BEGIN TRAN;

    DELETE FROM dbo.ChucVu WHERE MaChucVu = @MaChucVu;

    COMMIT;
END
GO

-- 5) LẤY DANH SÁCH PHÒNG BAN VÀ CHỨC VỤ CHO DROPDOWN
IF OBJECT_ID('dbo.sp_GetPhongBanChucVu','P') IS NOT NULL DROP PROCEDURE dbo.sp_GetPhongBanChucVu;
GO
CREATE PROCEDURE dbo.sp_GetPhongBanChucVu
AS
BEGIN
    SET NOCOUNT ON;

    -- Lấy danh sách phòng ban đang kích hoạt
    SELECT
        MaPhongBan,
        TenPhongBan,
        MoTa
    FROM dbo.PhongBan
    WHERE KichHoat = 1
    ORDER BY TenPhongBan;

    -- Lấy danh sách chức vụ đang kích hoạt
    SELECT
        MaChucVu,
        TenChucVu,
        MoTa
    FROM dbo.ChucVu
    WHERE KichHoat = 1
    ORDER BY TenChucVu;
END
GO

-- 6) LẤY THÔNG TIN NHÂN VIÊN ĐẦY ĐỦ (TỪ VIEW)
IF OBJECT_ID('dbo.sp_GetNhanVienFull','P') IS NOT NULL DROP PROCEDURE dbo.sp_GetNhanVienFull;
GO
CREATE PROCEDURE dbo.sp_GetNhanVienFull
    @MaNV INT = NULL,
    @TrangThai NVARCHAR(10) = NULL
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        MaNV,
        MaNguoiDung,
        HoTen,
        NgaySinh,
        GioiTinh,
        DienThoai,
        Email,
        DiaChi,
        NgayVaoLam,
        TrangThai,
        LuongCoBan,
        MaPhongBan,
        TenPhongBan,
        MoTaPhongBan,
        MaChucVu,
        TenChucVu,
        MoTaChucVu
    FROM dbo.vw_NhanVien_Full
    WHERE (@MaNV IS NULL OR MaNV = @MaNV)
      AND (@TrangThai IS NULL OR TrangThai = @TrangThai)
    ORDER BY HoTen;
END
GO

-- 7) CẬP NHẬT THÔNG TIN NHÂN VIÊN VỚI PHÒNG BAN VÀ CHỨC VỤ
IF OBJECT_ID('dbo.sp_UpdateNhanVienWithPhongBanChucVu','P') IS NOT NULL DROP PROCEDURE dbo.sp_UpdateNhanVienWithPhongBanChucVu;
GO
CREATE PROCEDURE dbo.sp_UpdateNhanVienWithPhongBanChucVu
    @MaNV INT,
    @MaPhongBan INT = NULL,
    @MaChucVu INT = NULL
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    -- Kiểm tra nhân viên tồn tại
    IF NOT EXISTS (SELECT 1 FROM dbo.NhanVien WHERE MaNV = @MaNV)
    BEGIN
        RAISERROR(N'Nhân viên không tồn tại.', 16, 1);
        RETURN;
    END

    -- Kiểm tra phòng ban tồn tại (nếu có)
    IF @MaPhongBan IS NOT NULL AND NOT EXISTS (SELECT 1 FROM dbo.PhongBan WHERE MaPhongBan = @MaPhongBan)
    BEGIN
        RAISERROR(N'Phòng ban không tồn tại.', 16, 1);
        RETURN;
    END

    -- Kiểm tra chức vụ tồn tại (nếu có)
    IF @MaChucVu IS NOT NULL AND NOT EXISTS (SELECT 1 FROM dbo.ChucVu WHERE MaChucVu = @MaChucVu)
    BEGIN
        RAISERROR(N'Chức vụ không tồn tại.', 16, 1);
        RETURN;
    END

    BEGIN TRAN;

    UPDATE dbo.NhanVien
    SET MaPhongBan = @MaPhongBan,
        MaChucVu = @MaChucVu
    WHERE MaNV = @MaNV;

    COMMIT;
END
GO

PRINT N'=== HOÀN TẤT TẠO STORED PROCEDURES CƠ BẢN ===';
PRINT N'Tiếp theo chạy file: 04_StoredProcedures_Advanced.sql';
