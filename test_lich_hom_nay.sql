/* =========================================================
   THÊM LỊCH LÀM VIỆC CHO HÔM NAY ĐỂ TEST CHECK IN/OUT
   ========================================================= */

USE QLNhanSuSieuThiMini;
GO

-- Thêm lịch làm việc cho hôm nay (2025-09-21) cho tất cả nhân viên đang làm
DECLARE @NgayHomNay DATE = CAST(GETDATE() AS DATE);

PRINT N'Thêm lịch làm việc cho ngày: ' + CAST(@NgayHomNay AS NVARCHAR(10));

-- Xóa lịch cũ nếu có
DELETE FROM LichPhanCa WHERE NgayLam = @NgayHomNay;

-- Thêm lịch mới cho từng nhân viên
INSERT INTO LichPhanCa (MaNV, NgayLam, MaCa, TrangThai, GhiChu) VALUES
(1, @NgayHomNay, 4, N'Mo', N'Ca hành chính - HR'),           -- Bích Hằng - HR
(2, @NgayHomNay, 4, N'Mo', N'Ca hành chính - Kế toán'),      -- Văn An - Kế toán  
(3, @NgayHomNay, 1, N'Mo', N'Ca sáng - Quản lý'),            -- Minh Tuấn - Quản lý
(4, @NgayHomNay, 2, N'Mo', N'Ca chiều - Quản lý'),           -- Kim Chi - Quản lý
(5, @NgayHomNay, 2, N'Mo', N'Ca chiều - Thu ngân'),          -- Văn Dũng - Thu ngân
(6, @NgayHomNay, 1, N'Mo', N'Ca sáng - Kho hàng'),           -- Mỹ Linh - Kho
(7, @NgayHomNay, 3, N'Mo', N'Ca đêm - Bảo vệ'),              -- Tiến Mạnh - Bảo vệ
(8, @NgayHomNay, 2, N'Mo', N'Ca chiều - Thu ngân');          -- Thảo - Thu ngân (nếu có tài khoản)

PRINT N'Đã thêm ' + CAST(@@ROWCOUNT AS NVARCHAR) + N' lịch làm việc cho hôm nay';

-- Kiểm tra kết quả
SELECT 
    lpc.MaLich,
    nv.HoTen,
    lpc.MaNV,
    lpc.NgayLam,
    cl.TenCa,
    cl.GioBatDau,
    cl.GioKetThuc,
    lpc.TrangThai,
    lpc.GhiChu
FROM LichPhanCa lpc
JOIN NhanVien nv ON lpc.MaNV = nv.MaNV
JOIN CaLam cl ON lpc.MaCa = cl.MaCa
WHERE lpc.NgayLam = @NgayHomNay
ORDER BY lpc.MaNV;

PRINT N'=== HOÀN TẤT THÊM LỊCH LÀM VIỆC HÔM NAY ===';
