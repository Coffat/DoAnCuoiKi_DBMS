USE QLNhanSuSieuThiMini;
GO

-- Drop và tạo lại sp_ThemMoiNhanVien với WITH EXECUTE AS OWNER
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
    @MaPhongBan  INT          = NULL,
    @MaChucVu    INT          = NULL,
    @LuongCoBan  DECIMAL(12,2),
    @TaoTaiKhoan BIT = 0,
    @TenDangNhap NVARCHAR(50) = NULL,
    @MatKhauHash NVARCHAR(200)= NULL,
    @VaiTro      NVARCHAR(20) = N'NhanVien',
    @MaNV_OUT    INT OUTPUT
WITH EXECUTE AS OWNER
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    PRINT N'✓ sp_ThemMoiNhanVien chạy với quyền OWNER';
    
    -- Tạm thời return để test
    SET @MaNV_OUT = 999;
    RETURN;
END
GO

PRINT N'✅ Đã sửa sp_ThemMoiNhanVien với WITH EXECUTE AS OWNER';
