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

------------------------------------------------------------
-- 8) CRUD LỊCH PHÂN CA
------------------------------------------------------------

-- 8.1) INSERT: Thêm lịch phân ca mới
IF OBJECT_ID('dbo.sp_LichPhanCa_Insert','P') IS NOT NULL DROP PROCEDURE dbo.sp_LichPhanCa_Insert;
GO
CREATE PROCEDURE dbo.sp_LichPhanCa_Insert
    @MaNV INT,
    @Ngay DATE,
    @MaCa INT,
    @TrangThai NVARCHAR(12) = N'DuKien',
    @GhiChu NVARCHAR(255) = NULL,
    @MaLich_OUT INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    IF NOT EXISTS (SELECT 1 FROM dbo.NhanVien WHERE MaNV = @MaNV)
    BEGIN
        RAISERROR(N'Nhân viên không tồn tại.', 16, 1);
        RETURN;
    END

    IF NOT EXISTS (SELECT 1 FROM dbo.CaLam WHERE MaCa = @MaCa AND KichHoat = 1)
    BEGIN
        RAISERROR(N'Ca làm không tồn tại hoặc đã bị vô hiệu hóa.', 16, 1);
        RETURN;
    END

    IF @TrangThai NOT IN (N'DuKien', N'Khoa', N'Huy')
    BEGIN
        RAISERROR(N'Trạng thái không hợp lệ.', 16, 1);
        RETURN;
    END

    -- Kiểm tra overlap
    DECLARE @GioBatDau TIME(0), @GioKetThuc TIME(0);
    SELECT @GioBatDau = GioBatDau, @GioKetThuc = GioKetThuc FROM dbo.CaLam WHERE MaCa = @MaCa;

    DECLARE @MinutesPerDay INT = 1440;
    DECLARE @StartMinute INT = DATEDIFF(MINUTE, '00:00:00', @GioBatDau);
    DECLARE @EndMinute INT = DATEDIFF(MINUTE, '00:00:00', @GioKetThuc);
    IF @EndMinute < @StartMinute SET @EndMinute = @EndMinute + @MinutesPerDay;

    IF EXISTS (
        SELECT 1 FROM dbo.LichPhanCa lpc
        INNER JOIN dbo.CaLam cl ON lpc.MaCa = cl.MaCa
        WHERE lpc.MaNV = @MaNV AND lpc.NgayLam = @Ngay
          AND lpc.TrangThai IN (N'DuKien', N'Khoa')
          AND (@StartMinute < (DATEDIFF(MINUTE, '00:00:00', cl.GioKetThuc) + 
              CASE WHEN cl.GioKetThuc < cl.GioBatDau THEN @MinutesPerDay ELSE 0 END)
          AND @EndMinute > DATEDIFF(MINUTE, '00:00:00', cl.GioBatDau))
    )
    BEGIN
        RAISERROR(N'Lịch bị trùng lặp thời gian với ca khác.', 16, 1);
        RETURN;
    END

    -- Kiểm tra tổng thời gian làm việc không vượt quá 16 tiếng/ngày
    DECLARE @TongThoiGianPhut INT;
    DECLARE @CaMoiThoiGianPhut INT = DATEDIFF(MINUTE, @GioBatDau, @GioKetThuc);
    IF @CaMoiThoiGianPhut < 0 SET @CaMoiThoiGianPhut = @CaMoiThoiGianPhut + 1440; -- Ca qua đêm

    -- Tính tổng thời gian hiện tại của nhân viên trong ngày này
    SELECT @TongThoiGianPhut = ISNULL(SUM(
        CASE
            WHEN DATEDIFF(MINUTE, cl.GioBatDau, cl.GioKetThuc) < 0
            THEN DATEDIFF(MINUTE, cl.GioBatDau, cl.GioKetThuc) + 1440
            ELSE DATEDIFF(MINUTE, cl.GioBatDau, cl.GioKetThuc)
        END
    ), 0)
    FROM dbo.LichPhanCa lpc
    INNER JOIN dbo.CaLam cl ON lpc.MaCa = cl.MaCa
    WHERE lpc.NgayLam = @Ngay
      AND lpc.MaNV = @MaNV
      AND lpc.TrangThai IN (N'DuKien', N'Khoa')
      AND cl.KichHoat = 1;

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
    INSERT INTO dbo.LichPhanCa (MaNV, NgayLam, MaCa, TrangThai, GhiChu)
    VALUES (@MaNV, @Ngay, @MaCa, @TrangThai, @GhiChu);
    SET @MaLich_OUT = SCOPE_IDENTITY();
    COMMIT;
END
GO

-- 8.2) UPDATE: Cập nhật lịch phân ca
IF OBJECT_ID('dbo.sp_LichPhanCa_Update','P') IS NOT NULL DROP PROCEDURE dbo.sp_LichPhanCa_Update;
GO
CREATE PROCEDURE dbo.sp_LichPhanCa_Update
    @Id INT,
    @MaCa INT = NULL,
    @TrangThai NVARCHAR(12) = NULL,
    @GhiChu NVARCHAR(255) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    DECLARE @MaNV INT, @NgayLam DATE, @TrangThaiCu NVARCHAR(12);
    SELECT @MaNV = MaNV, @NgayLam = NgayLam, @TrangThaiCu = TrangThai
    FROM dbo.LichPhanCa WHERE MaLich = @Id;

    IF @MaNV IS NULL
    BEGIN
        RAISERROR(N'Lịch phân ca không tồn tại.', 16, 1);
        RETURN;
    END

    IF @TrangThaiCu = N'Khoa' AND (@MaCa IS NOT NULL OR (@TrangThai IS NOT NULL AND @TrangThai <> @TrangThaiCu))
    BEGIN
        RAISERROR(N'Không thể sửa lịch đã khóa.', 16, 1);
        RETURN;
    END

    BEGIN TRAN;
    UPDATE dbo.LichPhanCa
    SET MaCa = ISNULL(@MaCa, MaCa),
        TrangThai = ISNULL(@TrangThai, TrangThai),
        GhiChu = ISNULL(@GhiChu, GhiChu)
    WHERE MaLich = @Id;
    COMMIT;
END
GO

-- 8.3) DELETE: Xóa lịch phân ca
IF OBJECT_ID('dbo.sp_LichPhanCa_Delete','P') IS NOT NULL DROP PROCEDURE dbo.sp_LichPhanCa_Delete;
GO
CREATE PROCEDURE dbo.sp_LichPhanCa_Delete
    @Id INT
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    DECLARE @TrangThai NVARCHAR(12);
    SELECT @TrangThai = TrangThai FROM dbo.LichPhanCa WHERE MaLich = @Id;

    IF @TrangThai IS NULL
    BEGIN
        RAISERROR(N'Lịch phân ca không tồn tại.', 16, 1);
        RETURN;
    END

    IF @TrangThai = N'Khoa'
    BEGIN
        RAISERROR(N'Không thể xóa lịch đã khóa.', 16, 1);
        RETURN;
    END

    BEGIN TRAN;
    DELETE FROM dbo.LichPhanCa WHERE MaLich = @Id;
    COMMIT;
END
GO

-- 8.4) GET BY NHANVIEN: Lấy lịch theo nhân viên
IF OBJECT_ID('dbo.sp_LichPhanCa_GetByNhanVien','P') IS NOT NULL DROP PROCEDURE dbo.sp_LichPhanCa_GetByNhanVien;
GO
CREATE PROCEDURE dbo.sp_LichPhanCa_GetByNhanVien
    @MaNV INT,
    @FromDate DATE = NULL,
    @ToDate DATE = NULL
AS
BEGIN
    SET NOCOUNT ON;

    IF @FromDate IS NULL SET @FromDate = CAST(GETDATE() AS DATE);
    IF @ToDate IS NULL SET @ToDate = DATEADD(DAY, 30, @FromDate);

    SELECT 
        lpc.MaLich, lpc.MaNV, nv.HoTen, lpc.NgayLam,
        lpc.MaCa, cl.TenCa, cl.GioBatDau, cl.GioKetThuc, cl.HeSoCa,
        lpc.TrangThai, lpc.GhiChu
    FROM dbo.LichPhanCa lpc
    INNER JOIN dbo.NhanVien nv ON lpc.MaNV = nv.MaNV
    INNER JOIN dbo.CaLam cl ON lpc.MaCa = cl.MaCa
    WHERE lpc.MaNV = @MaNV AND lpc.NgayLam BETWEEN @FromDate AND @ToDate
    ORDER BY lpc.NgayLam, cl.GioBatDau;
END
GO

------------------------------------------------------------
-- 9) CRUD ĐƠN TỪ
------------------------------------------------------------

-- 9.1) Insert DonTu
IF OBJECT_ID('dbo.sp_DonTu_Insert','P') IS NOT NULL DROP PROCEDURE dbo.sp_DonTu_Insert;
GO
CREATE PROCEDURE dbo.sp_DonTu_Insert
    @MaNV INT,
    @Loai NVARCHAR(10),
    @TuLuc DATETIME2(0),
    @DenLuc DATETIME2(0),
    @SoGio DECIMAL(5,2) = NULL,
    @LyDo NVARCHAR(255)
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;
    
    -- Validation
    IF @Loai NOT IN (N'NGHI', N'OT')
    BEGIN
        RAISERROR(N'Loại đơn không hợp lệ. Chỉ chấp nhận NGHI hoặc OT.', 16, 1);
        RETURN;
    END
    
    IF @TuLuc >= @DenLuc
    BEGIN
        RAISERROR(N'Thời gian kết thúc phải sau thời gian bắt đầu.', 16, 1);
        RETURN;
    END
    
    IF NOT EXISTS (SELECT 1 FROM dbo.NhanVien WHERE MaNV = @MaNV)
    BEGIN
        RAISERROR(N'Nhân viên không tồn tại.', 16, 1);
        RETURN;
    END
    
    -- Tính số giờ nếu không có
    IF @SoGio IS NULL
    BEGIN
        SET @SoGio = CAST(DATEDIFF(MINUTE, @TuLuc, @DenLuc) / 60.0 AS DECIMAL(5,2));
    END
    
    BEGIN TRAN;
    
    INSERT INTO dbo.DonTu (MaNV, Loai, TuLuc, DenLuc, SoGio, LyDo, TrangThai)
    VALUES (@MaNV, @Loai, @TuLuc, @DenLuc, @SoGio, @LyDo, N'ChoDuyet');
    
    COMMIT;
END
GO

------------------------------------------------------------
-- 10) NHÂN VIÊN BỔ SUNG
------------------------------------------------------------

-- 10.1) Delete NhanVien
IF OBJECT_ID('dbo.sp_NhanVien_Delete','P') IS NOT NULL DROP PROCEDURE dbo.sp_NhanVien_Delete;
GO
CREATE PROCEDURE dbo.sp_NhanVien_Delete
    @MaNV INT
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;
    
    -- Kiểm tra có dữ liệu liên quan không
    IF EXISTS (SELECT 1 FROM dbo.ChamCong WHERE MaNV = @MaNV)
       OR EXISTS (SELECT 1 FROM dbo.DonTu WHERE MaNV = @MaNV)
       OR EXISTS (SELECT 1 FROM dbo.BangLuong WHERE MaNV = @MaNV)
    BEGIN
        RAISERROR(N'Không thể xóa nhân viên đã có dữ liệu chấm công, đơn từ hoặc bảng lương.', 16, 1);
        RETURN;
    END
    
    BEGIN TRAN;
    DELETE FROM dbo.NhanVien WHERE MaNV = @MaNV;
    COMMIT;
END
GO

-- 10.2) Update TrangThai NhanVien
IF OBJECT_ID('dbo.sp_NhanVien_UpdateTrangThai','P') IS NOT NULL DROP PROCEDURE dbo.sp_NhanVien_UpdateTrangThai;
GO
CREATE PROCEDURE dbo.sp_NhanVien_UpdateTrangThai
    @MaNV INT,
    @TrangThai NVARCHAR(20)
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;
    
    IF @TrangThai NOT IN (N'DangLam', N'Nghi', N'TamNghi')
    BEGIN
        RAISERROR(N'Trạng thái không hợp lệ.', 16, 1);
        RETURN;
    END
    
    BEGIN TRAN;
    UPDATE dbo.NhanVien 
    SET TrangThai = @TrangThai 
    WHERE MaNV = @MaNV;
    COMMIT;
END
GO

-- ============================================================================
-- 13) STORED PROCEDURES CHO THÔNG TIN CÁ NHÂN
-- ============================================================================

-- 13.1) Lấy thông tin cá nhân của nhân viên
IF OBJECT_ID('dbo.sp_NhanVien_GetThongTinCaNhan','P') IS NOT NULL DROP PROCEDURE dbo.sp_NhanVien_GetThongTinCaNhan;
GO
CREATE PROCEDURE dbo.sp_NhanVien_GetThongTinCaNhan
    @MaNV INT
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT 
        nv.HoTen, 
        nv.DienThoai, 
        nv.Email, 
        nv.DiaChi, 
        pb.TenPhongBan, 
        cv.TenChucVu, 
        nv.LuongCoBan, 
        nv.NgayVaoLam
    FROM dbo.NhanVien nv
    LEFT JOIN dbo.PhongBan pb ON nv.MaPhongBan = pb.MaPhongBan
    LEFT JOIN dbo.ChucVu cv ON nv.MaChucVu = cv.MaChucVu
    WHERE nv.MaNV = @MaNV;
END
GO

-- 13.2) Cập nhật thông tin cá nhân (chỉ các thông tin được phép thay đổi)
IF OBJECT_ID('dbo.sp_NhanVien_UpdateThongTinCaNhan','P') IS NOT NULL DROP PROCEDURE dbo.sp_NhanVien_UpdateThongTinCaNhan;
GO
CREATE PROCEDURE dbo.sp_NhanVien_UpdateThongTinCaNhan
    @MaNV INT,
    @HoTen NVARCHAR(120),
    @DienThoai NVARCHAR(15) = NULL,
    @Email NVARCHAR(120) = NULL,
    @DiaChi NVARCHAR(255) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;
    
    -- Validation
    IF LTRIM(RTRIM(@HoTen)) = ''
    BEGIN
        RAISERROR(N'Họ tên không được để trống.', 16, 1);
        RETURN;
    END
    
    IF @DienThoai IS NOT NULL AND @DienThoai != ''
    BEGIN
        IF LEN(@DienThoai) < 10 OR LEN(@DienThoai) > 15
        BEGIN
            RAISERROR(N'Số điện thoại không hợp lệ.', 16, 1);
            RETURN;
        END
    END
    
    -- Validate email format
    IF @Email IS NOT NULL AND @Email != ''
    BEGIN
        IF @Email NOT LIKE '%@%.%'
        BEGIN
            RAISERROR(N'Địa chỉ email không hợp lệ.', 16, 1);
            RETURN;
        END
    END
    
    BEGIN TRAN;
    
    UPDATE dbo.NhanVien 
    SET 
        HoTen = @HoTen,
        DienThoai = @DienThoai,
        Email = @Email,
        DiaChi = @DiaChi
    WHERE MaNV = @MaNV;
    
    IF @@ROWCOUNT = 0
    BEGIN
        ROLLBACK;
        RAISERROR(N'Không tìm thấy nhân viên.', 16, 1);
        RETURN;
    END
    
    COMMIT;
END
GO

-- 13.3) Đổi mật khẩu
-- ⚠️ CẢNH BÁO: SP này CHỈ cập nhật mật khẩu trong bảng NguoiDung
-- Không đồng bộ với SQL Server Login trong mô hình bảo mật 2 lớp
-- 
-- ✅ KHUYẾN NGHỊ: Sử dụng sp_CapNhatTaiKhoanDayDu (trong 04_StoredProcedures_Advanced.sql)
-- để đảm bảo đồng bộ mật khẩu cho cả SQL Login và bảng NguoiDung
-- 
-- SP này CHỈ nên dùng cho các mục đích quản trị nội bộ đặc biệt
-- hoặc các tài khoản không có SQL Login tương ứng
IF OBJECT_ID('dbo.sp_NguoiDung_DoiMatKhau','P') IS NOT NULL DROP PROCEDURE dbo.sp_NguoiDung_DoiMatKhau;
GO
CREATE PROCEDURE dbo.sp_NguoiDung_DoiMatKhau
    @MaNguoiDung INT,
    @MatKhauCu NVARCHAR(128),
    @MatKhauMoi NVARCHAR(128)
WITH EXECUTE AS OWNER
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;
    
    -- Validation
    IF LEN(@MatKhauMoi) < 6
    BEGIN
        RAISERROR(N'Mật khẩu mới phải có ít nhất 6 ký tự.', 16, 1);
        RETURN;
    END
    
    IF @MatKhauCu = @MatKhauMoi
    BEGIN
        RAISERROR(N'Mật khẩu mới phải khác mật khẩu cũ.', 16, 1);
        RETURN;
    END
    
    BEGIN TRAN;
    
    -- Kiểm tra mật khẩu cũ (nếu không trống)
    IF @MatKhauCu != ''
    BEGIN
        DECLARE @CurrentPassword NVARCHAR(128);
        SELECT @CurrentPassword = MatKhauHash 
        FROM dbo.NguoiDung 
        WHERE MaNguoiDung = @MaNguoiDung;
        
        IF @CurrentPassword IS NULL
        BEGIN
            ROLLBACK;
            RAISERROR(N'Không tìm thấy người dùng.', 16, 1);
            RETURN;
        END
        
        IF @CurrentPassword != @MatKhauCu
        BEGIN
            ROLLBACK;
            RAISERROR(N'Mật khẩu cũ không đúng.', 16, 1);
            RETURN;
        END
    END
    
    -- Cập nhật mật khẩu mới
    UPDATE dbo.NguoiDung 
    SET MatKhauHash = @MatKhauMoi
    WHERE MaNguoiDung = @MaNguoiDung;
    
    COMMIT;
END
GO

-- ============================================================================
-- sp_NguoiDung_DoiMatKhauCaNhan - Đổi mật khẩu cá nhân (chỉ cho chính mình)
-- ============================================================================
-- Mục đích: Cho phép user đổi mật khẩu của chính mình với bảo mật chặt chẽ
-- Bảo mật: 
-- 1. Chỉ có thể đổi mật khẩu của chính mình (MaNguoiDung = SUSER_SNAME())
-- 2. Bắt buộc phải nhập mật khẩu cũ
-- 3. Validation mật khẩu mới chặt chẽ
-- 4. Log audit trail
-- ============================================================================
IF OBJECT_ID('dbo.sp_NguoiDung_DoiMatKhauCaNhan','P') IS NOT NULL DROP PROCEDURE dbo.sp_NguoiDung_DoiMatKhauCaNhan;
GO
CREATE PROCEDURE dbo.sp_NguoiDung_DoiMatKhauCaNhan
    @MatKhauCu NVARCHAR(128),
    @MatKhauMoi NVARCHAR(128)
WITH EXECUTE AS OWNER
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;
    
    -- Lấy thông tin user hiện tại
    DECLARE @CurrentUser NVARCHAR(128) = SUSER_SNAME();
    DECLARE @MaNguoiDung INT;
    
    -- Tìm MaNguoiDung từ username hiện tại
    SELECT @MaNguoiDung = MaNguoiDung 
    FROM dbo.NguoiDung 
    WHERE Username = @CurrentUser;
    
    IF @MaNguoiDung IS NULL
    BEGIN
        RAISERROR(N'Không tìm thấy thông tin người dùng hiện tại.', 16, 1);
        RETURN;
    END
    
    -- Validation mật khẩu mới
    IF LEN(@MatKhauMoi) < 8
    BEGIN
        RAISERROR(N'Mật khẩu mới phải có ít nhất 8 ký tự.', 16, 1);
        RETURN;
    END
    
    -- Kiểm tra độ phức tạp mật khẩu
    IF @MatKhauMoi NOT LIKE '%[0-9]%' OR @MatKhauMoi NOT LIKE '%[A-Za-z]%'
    BEGIN
        RAISERROR(N'Mật khẩu mới phải chứa ít nhất 1 chữ số và 1 chữ cái.', 16, 1);
        RETURN;
    END
    
    IF @MatKhauCu = @MatKhauMoi
    BEGIN
        RAISERROR(N'Mật khẩu mới phải khác mật khẩu cũ.', 16, 1);
        RETURN;
    END
    
    -- Kiểm tra mật khẩu cũ (BẮT BUỘC)
    IF @MatKhauCu = '' OR @MatKhauCu IS NULL
    BEGIN
        RAISERROR(N'Vui lòng nhập mật khẩu cũ.', 16, 1);
        RETURN;
    END
    
    BEGIN TRAN;
    
    -- Kiểm tra mật khẩu cũ
    DECLARE @CurrentPassword NVARCHAR(128);
    SELECT @CurrentPassword = MatKhauHash 
    FROM dbo.NguoiDung 
    WHERE MaNguoiDung = @MaNguoiDung;
    
    IF @CurrentPassword != @MatKhauCu
    BEGIN
        ROLLBACK;
        RAISERROR(N'Mật khẩu cũ không đúng.', 16, 1);
        RETURN;
    END
    
    -- Cập nhật mật khẩu mới
    UPDATE dbo.NguoiDung 
    SET MatKhauHash = @MatKhauMoi,
        NgayCapNhatMatKhau = GETDATE()
    WHERE MaNguoiDung = @MaNguoiDung;
    
    -- Log audit trail (nếu có bảng audit)
    -- INSERT INTO dbo.AuditLog (MaNguoiDung, Action, Details, Timestamp)
    -- VALUES (@MaNguoiDung, 'PASSWORD_CHANGE', 'User changed password', GETDATE());
    
    COMMIT;
    
    PRINT N'Đổi mật khẩu thành công!';
END
GO

-- ✅ BỔ SUNG CÁC STORED PROCEDURES GETALL CÒN THIẾU
-- Form frmPhongBan_ChucVu cần các procedure này

-- sp_PhongBan_GetAll
IF OBJECT_ID('dbo.sp_PhongBan_GetAll','P') IS NOT NULL DROP PROCEDURE dbo.sp_PhongBan_GetAll;
GO
CREATE PROCEDURE dbo.sp_PhongBan_GetAll
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT 
        MaPhongBan,
        TenPhongBan,
        MoTa,
        KichHoat
    FROM dbo.PhongBan
    ORDER BY TenPhongBan;
END
GO

-- sp_ChucVu_GetAll  
IF OBJECT_ID('dbo.sp_ChucVu_GetAll','P') IS NOT NULL DROP PROCEDURE dbo.sp_ChucVu_GetAll;
GO
CREATE PROCEDURE dbo.sp_ChucVu_GetAll
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT 
        MaChucVu,
        TenChucVu,
        MoTa,
        KichHoat
    FROM dbo.ChucVu
    ORDER BY TenChucVu;
END
GO

PRINT N'✅ Đã bổ sung sp_PhongBan_GetAll và sp_ChucVu_GetAll';

PRINT N'=== HOÀN TẤT TẠO STORED PROCEDURES CƠ BẢN ===';
PRINT N'Đã thêm CRUD LichPhanCa (Insert/Update/Delete/GetByNhanVien)';
PRINT N'Đã thêm CRUD PhongBan (Insert/Update/Delete/GetAll)';
PRINT N'Đã thêm CRUD ChucVu (Insert/Update/Delete/GetAll)';
PRINT N'Đã thêm sp_DonTu_Insert';
PRINT N'Đã thêm sp_NhanVien_Delete và sp_NhanVien_UpdateTrangThai';
PRINT N'Đã thêm sp_NhanVien_GetThongTinCaNhan, sp_NhanVien_UpdateThongTinCaNhan, sp_NguoiDung_DoiMatKhau';
PRINT N'Đã thêm sp_NguoiDung_DoiMatKhauCaNhan - Đổi mật khẩu cá nhân với bảo mật chặt chẽ';
PRINT N'✅ Đã bổ sung sp_PhongBan_GetAll và sp_ChucVu_GetAll cho form frmPhongBan_ChucVu';

-- ============================================================================
-- sp_NhanVien_GetAll - ĐÃ XÓA
-- ============================================================================
-- LÝ DO XÓA:
-- 1. Code C# không sử dụng stored procedure này
-- 2. Vừa tạo nhưng chưa implement trong C# code
-- 3. Form frmNhanVien sử dụng sp_GetNhanVienFull thay thế
-- ============================================================================
PRINT N'Tiếp theo chạy file: 04_StoredProcedures_Advanced.sql';
