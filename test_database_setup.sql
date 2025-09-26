-- Script kiểm tra và setup database
USE QLNhanSuSieuThiMini;
GO

-- Kiểm tra database có tồn tại không
SELECT DB_NAME() AS CurrentDatabase;

-- Kiểm tra các bảng có tồn tại không
SELECT 
    TABLE_NAME,
    TABLE_TYPE
FROM INFORMATION_SCHEMA.TABLES 
WHERE TABLE_SCHEMA = 'dbo'
ORDER BY TABLE_NAME;

-- Kiểm tra dữ liệu trong bảng PhongBan
PRINT '=== KIỂM TRA DỮ LIỆU PHÒNG BAN ==='
SELECT 'PhongBan' AS TableName, COUNT(*) AS RowCount FROM dbo.PhongBan;
SELECT MaPhongBan, TenPhongBan, KichHoat FROM dbo.PhongBan;

-- Kiểm tra dữ liệu trong bảng ChucVu  
PRINT '=== KIỂM TRA DỮ LIỆU CHỨC VỤ ==='
SELECT 'ChucVu' AS TableName, COUNT(*) AS RowCount FROM dbo.ChucVu;
SELECT MaChucVu, TenChucVu, KichHoat FROM dbo.ChucVu;

-- Kiểm tra dữ liệu trong bảng NhanVien
PRINT '=== KIỂM TRA DỮ LIỆU NHÂN VIÊN ==='
SELECT 'NhanVien' AS TableName, COUNT(*) AS RowCount FROM dbo.NhanVien;
SELECT TOP 5 MaNV, HoTen, TenPhongBan, TenChucVu, TrangThai FROM dbo.vw_NhanVien_Full;

-- Kiểm tra stored procedure
PRINT '=== KIỂM TRA STORED PROCEDURES ==='
SELECT 
    ROUTINE_NAME,
    ROUTINE_TYPE
FROM INFORMATION_SCHEMA.ROUTINES 
WHERE ROUTINE_SCHEMA = 'dbo' 
AND ROUTINE_NAME IN ('sp_GetPhongBanChucVu', 'sp_GetNhanVienFull', 'sp_ThemMoiNhanVien', 'sp_UpdateNhanVienWithPhongBanChucVu');

-- Test chạy stored procedure
PRINT '=== TEST sp_GetPhongBanChucVu ==='
EXEC sp_GetPhongBanChucVu;

PRINT '=== TEST sp_GetNhanVienFull ==='
EXEC sp_GetNhanVienFull;

-- Kiểm tra tài khoản đăng nhập
PRINT '=== KIỂM TRA TÀI KHOẢN ĐĂNG NHẬP ==='
SELECT TenDangNhap, VaiTro, KichHoat FROM dbo.NguoiDung;
