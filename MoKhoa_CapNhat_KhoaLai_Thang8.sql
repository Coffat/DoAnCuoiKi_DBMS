-- SCRIPT MỞ KHÓA, CẬP NHẬT VÀ KHÓA LẠI CÔNG THÁNG 8/2024
-- Sử dụng khi đã khóa công trước khi chạy dữ liệu từ 1/7

USE QLNhanSuSieuThiMini;
GO

PRINT N'=== BẮT ĐẦU QUY TRÌNH MỞ KHÓA VÀ CẬP NHẬT CÔNG THÁNG 8/2024 ===';
PRINT N'';

-- BƯỚC 1: Kiểm tra trạng thái hiện tại
PRINT N'BƯỚC 1: Kiểm tra trạng thái công tháng 8/2024 trước khi mở khóa';
PRINT N'--------------------------------------------------------------';

SELECT 
    COUNT(*) as TongSoLichPhanCa,
    SUM(CASE WHEN TrangThai = N'Khoa' THEN 1 ELSE 0 END) as SoLichDaKhoa,
    SUM(CASE WHEN TrangThai = N'DuKien' THEN 1 ELSE 0 END) as SoLichDuKien
FROM LichPhanCa 
WHERE NgayLam >= '2024-08-01' AND NgayLam <= '2024-08-31';

SELECT 
    COUNT(*) as TongSoChamCong,
    SUM(CASE WHEN Khoa = 1 THEN 1 ELSE 0 END) as SoChamCongDaKhoa,
    SUM(CASE WHEN Khoa = 0 THEN 1 ELSE 0 END) as SoChamCongChuaKhoa
FROM ChamCong 
WHERE NgayLam >= '2024-08-01' AND NgayLam <= '2024-08-31';

PRINT N'';

-- BƯỚC 2: Mở khóa công tháng 8/2024
PRINT N'BƯỚC 2: Mở khóa công tháng 8/2024';
PRINT N'----------------------------------';

EXEC sp_MoKhoaCongThang @Nam = 2024, @Thang = 8;

PRINT N'';

-- BƯỚC 3: Kiểm tra sau khi mở khóa
PRINT N'BƯỚC 3: Kiểm tra trạng thái sau khi mở khóa';
PRINT N'------------------------------------------';

SELECT 
    COUNT(*) as TongSoLichPhanCa,
    SUM(CASE WHEN TrangThai = N'Khoa' THEN 1 ELSE 0 END) as SoLichDaKhoa,
    SUM(CASE WHEN TrangThai = N'DuKien' THEN 1 ELSE 0 END) as SoLichDuKien
FROM LichPhanCa 
WHERE NgayLam >= '2024-08-01' AND NgayLam <= '2024-08-31';

SELECT 
    COUNT(*) as TongSoChamCong,
    SUM(CASE WHEN Khoa = 1 THEN 1 ELSE 0 END) as SoChamCongDaKhoa,
    SUM(CASE WHEN Khoa = 0 THEN 1 ELSE 0 END) as SoChamCongChuaKhoa
FROM ChamCong 
WHERE NgayLam >= '2024-08-01' AND NgayLam <= '2024-08-31';

PRINT N'';

-- BƯỚC 4: Cập nhật lại dữ liệu chấm công (trigger sẽ tự động tính toán)
PRINT N'BƯỚC 4: Cập nhật lại dữ liệu chấm công để trigger tính toán lại';
PRINT N'--------------------------------------------------------------';

-- Force update để trigger tính toán lại GioCong, DiTrePhut, VeSomPhut
UPDATE ChamCong 
SET GioVao = GioVao  -- Dummy update để trigger chạy
WHERE NgayLam >= '2024-08-01' AND NgayLam <= '2024-08-31'
  AND GioVao IS NOT NULL;

PRINT N'Đã cập nhật ' + CAST(@@ROWCOUNT AS NVARCHAR) + N' bản ghi chấm công';
PRINT N'';

-- BƯỚC 5: Kiểm tra dữ liệu sau khi cập nhật
PRINT N'BƯỚC 5: Kiểm tra dữ liệu công sau khi cập nhật';
PRINT N'---------------------------------------------';

SELECT 
    MaNV,
    COUNT(*) as SoNgayLam,
    SUM(ISNULL(GioCong, 0)) as TongGioCong,
    SUM(ISNULL(DiTrePhut, 0)) as TongPhutDiTre,
    SUM(ISNULL(VeSomPhut, 0)) as TongPhutVeSom
FROM ChamCong 
WHERE NgayLam >= '2024-08-01' AND NgayLam <= '2024-08-31'
GROUP BY MaNV
ORDER BY MaNV;

PRINT N'';

-- BƯỚC 6: Chạy lại bảng lương tháng 8/2024
PRINT N'BƯỚC 6: Chạy lại bảng lương tháng 8/2024';
PRINT N'---------------------------------------';

EXEC sp_ChayBangLuong @Nam = 2024, @Thang = 8;

PRINT N'';

-- BƯỚC 7: Kiểm tra bảng lương sau khi chạy lại
PRINT N'BƯỚC 7: Kiểm tra bảng lương sau khi chạy lại';
PRINT N'-------------------------------------------';

SELECT 
    bl.MaNV,
    nv.HoTen,
    bl.LuongCoBan,
    bl.TongGioCong,
    bl.GioOT,
    bl.PhuCap,
    bl.KhauTru,
    bl.ThueBH,
    bl.ThucLanh,
    bl.TrangThai
FROM BangLuong bl
JOIN NhanVien nv ON bl.MaNV = nv.MaNV
WHERE bl.Nam = 2024 AND bl.Thang = 8
ORDER BY bl.MaNV;

PRINT N'';

-- BƯỚC 8: Khóa lại công tháng 8/2024
PRINT N'BƯỚC 8: Khóa lại công tháng 8/2024';
PRINT N'----------------------------------';

EXEC sp_KhoaCongThang @Nam = 2024, @Thang = 8;

PRINT N'';

-- BƯỚC 9: Kiểm tra cuối cùng
PRINT N'BƯỚC 9: Kiểm tra trạng thái cuối cùng sau khi khóa lại';
PRINT N'----------------------------------------------------';

SELECT 
    COUNT(*) as TongSoLichPhanCa,
    SUM(CASE WHEN TrangThai = N'Khoa' THEN 1 ELSE 0 END) as SoLichDaKhoa,
    SUM(CASE WHEN TrangThai = N'DuKien' THEN 1 ELSE 0 END) as SoLichDuKien
FROM LichPhanCa 
WHERE NgayLam >= '2024-08-01' AND NgayLam <= '2024-08-31';

SELECT 
    COUNT(*) as TongSoChamCong,
    SUM(CASE WHEN Khoa = 1 THEN 1 ELSE 0 END) as SoChamCongDaKhoa,
    SUM(CASE WHEN Khoa = 0 THEN 1 ELSE 0 END) as SoChamCongChuaKhoa
FROM ChamCong 
WHERE NgayLam >= '2024-08-01' AND NgayLam <= '2024-08-31';

PRINT N'';
PRINT N'=== HOÀN THÀNH QUY TRÌNH MỞ KHÓA VÀ CẬP NHẬT CÔNG THÁNG 8/2024 ===';
PRINT N'';
PRINT N'Các bước đã thực hiện:';
PRINT N'1. ✅ Kiểm tra trạng thái ban đầu';
PRINT N'2. ✅ Mở khóa công tháng 8/2024';
PRINT N'3. ✅ Cập nhật dữ liệu chấm công (trigger tính toán lại)';
PRINT N'4. ✅ Chạy lại bảng lương tháng 8/2024';
PRINT N'5. ✅ Khóa lại công tháng 8/2024';
PRINT N'';
PRINT N'Bây giờ dữ liệu công và lương tháng 8/2024 đã được cập nhật chính xác!';
