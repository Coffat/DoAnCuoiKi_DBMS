-- Test script để kiểm tra kết nối database
-- Chạy script này để đảm bảo database hoạt động

USE QLNhanSuSieuThiMini;
GO

-- Kiểm tra database có tồn tại không
SELECT DB_NAME() AS CurrentDatabase;

-- Kiểm tra các bảng chính
SELECT 
    TABLE_NAME,
    TABLE_TYPE
FROM INFORMATION_SCHEMA.TABLES 
WHERE TABLE_SCHEMA = 'dbo'
ORDER BY TABLE_NAME;

-- Kiểm tra stored procedures
SELECT 
    ROUTINE_NAME,
    ROUTINE_TYPE
FROM INFORMATION_SCHEMA.ROUTINES 
WHERE ROUTINE_SCHEMA = 'dbo'
ORDER BY ROUTINE_NAME;

-- Kiểm tra dữ liệu mẫu
SELECT COUNT(*) AS SoLuongNguoiDung FROM dbo.NguoiDung;
SELECT COUNT(*) AS SoLuongNhanVien FROM dbo.NhanVien;
SELECT COUNT(*) AS SoLuongPhongBan FROM dbo.PhongBan;
SELECT COUNT(*) AS SoLuongChucVu FROM dbo.ChucVu;
