-- Test script để kiểm tra stored procedures
USE QLNhanSuSieuThiMini;
GO

-- Kiểm tra stored procedure sp_GetPhongBanChucVu có tồn tại không
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_GetPhongBanChucVu]') AND type in (N'P', N'PC'))
    PRINT 'Stored procedure sp_GetPhongBanChucVu tồn tại'
ELSE
    PRINT 'LỖI: Stored procedure sp_GetPhongBanChucVu KHÔNG tồn tại'

-- Kiểm tra stored procedure sp_GetNhanVienFull có tồn tại không
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_GetNhanVienFull]') AND type in (N'P', N'PC'))
    PRINT 'Stored procedure sp_GetNhanVienFull tồn tại'
ELSE
    PRINT 'LỖI: Stored procedure sp_GetNhanVienFull KHÔNG tồn tại'

-- Test chạy sp_GetPhongBanChucVu
PRINT '=== TEST sp_GetPhongBanChucVu ==='
EXEC sp_GetPhongBanChucVu;

-- Kiểm tra dữ liệu trong các bảng
PRINT '=== KIỂM TRA DỮ LIỆU ==='
SELECT 'PhongBan' AS TableName, COUNT(*) AS RowCount FROM dbo.PhongBan
UNION ALL
SELECT 'ChucVu' AS TableName, COUNT(*) AS RowCount FROM dbo.ChucVu
UNION ALL
SELECT 'NhanVien' AS TableName, COUNT(*) AS RowCount FROM dbo.NhanVien;

-- Hiển thị dữ liệu phòng ban
PRINT '=== DỮ LIỆU PHÒNG BAN ==='
SELECT MaPhongBan, TenPhongBan, KichHoat FROM dbo.PhongBan;

-- Hiển thị dữ liệu chức vụ
PRINT '=== DỮ LIỆU CHỨC VỤ ==='
SELECT MaChucVu, TenChucVu, KichHoat FROM dbo.ChucVu;
