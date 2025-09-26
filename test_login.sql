-- Test script để kiểm tra đăng nhập
USE QLNhanSuSieuThiMini;
GO

-- Kiểm tra tài khoản HR
SELECT TenDangNhap, VaiTro, KichHoat 
FROM dbo.NguoiDung 
WHERE TenDangNhap = 'hr_manager';

-- Kiểm tra tài khoản QuanLy
SELECT TenDangNhap, VaiTro, KichHoat 
FROM dbo.NguoiDung 
WHERE TenDangNhap = 'giamdoc';

-- Kiểm tra tất cả tài khoản
SELECT TenDangNhap, VaiTro, KichHoat 
FROM dbo.NguoiDung 
ORDER BY VaiTro;
