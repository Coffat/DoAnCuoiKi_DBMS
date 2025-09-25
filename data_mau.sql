/* =========================================================
   SCRIPT CUỐI CÙNG (SỬA LỖI TRIGGER KHI XÓA)
   Chạy file này để có một database hoàn chỉnh.
   ========================================================= */

USE QLNhanSuSieuThiMini;
GO

BEGIN TRAN;

------------------------------------------------------------
-- BƯỚC 0: TẮT TRIGGER, XÓA SẠCH DỮ LIỆU CŨ, BẬT LẠI TRIGGER
------------------------------------------------------------
PRINT N'Tạm thời tắt các trigger để xóa dữ liệu...';
ALTER TABLE dbo.ChamCong DISABLE TRIGGER tr_ChamCong_BlockWhenLocked_D;
ALTER TABLE dbo.NhanVien DISABLE TRIGGER tr_NhanVien_ToggleAccount;

PRINT N'Xóa dữ liệu cũ từ các bảng...';
-- Xóa từ các bảng con có khóa ngoại trước
DELETE FROM dbo.DonTu;
DELETE FROM dbo.BangLuong;
DELETE FROM dbo.ChamCong;
DELETE FROM dbo.LichPhanCa;
-- Xóa bảng cha sau khi bảng con đã được xóa
DELETE FROM dbo.NhanVien;
-- Bây giờ mới có thể xóa các bảng gốc
DELETE FROM dbo.NguoiDung;
DELETE FROM dbo.PhongBan;
DELETE FROM dbo.ChucVu;
DELETE FROM dbo.CaLam;
-- Đặt đoạn code này sau các lệnh DELETE
PRINT N'Reset lại các cột IDENTITY về 1...';
DBCC CHECKIDENT ('dbo.PhongBan', RESEED, 0);
DBCC CHECKIDENT ('dbo.ChucVu', RESEED, 0);
DBCC CHECKIDENT ('dbo.CaLam', RESEED, 0);
DBCC CHECKIDENT ('dbo.NguoiDung', RESEED, 0);
DBCC CHECKIDENT ('dbo.NhanVien', RESEED, 0);
DBCC CHECKIDENT ('dbo.LichPhanCa', RESEED, 0);
DBCC CHECKIDENT ('dbo.ChamCong', RESEED, 0);
DBCC CHECKIDENT ('dbo.DonTu', RESEED, 0);
DBCC CHECKIDENT ('dbo.BangLuong', RESEED, 0);
PRINT N'Đã reset xong.';
PRINT N'Bật lại các trigger...';
ALTER TABLE dbo.ChamCong ENABLE TRIGGER tr_ChamCong_BlockWhenLocked_D;
ALTER TABLE dbo.NhanVien ENABLE TRIGGER tr_NhanVien_ToggleAccount;
PRINT N'Đã xóa xong dữ liệu cũ và bật lại trigger.';


------------------------------------------------------------
-- BƯỚC 0.1: CẬP NHẬT LẠI STORED PROCEDURE
------------------------------------------------------------
PRINT N'Cập nhật lại Stored Procedure sp_KhoaCongThang...';
IF OBJECT_ID('dbo.sp_KhoaCongThang','P') IS NOT NULL DROP PROCEDURE dbo.sp_KhoaCongThang;
GO
CREATE PROCEDURE dbo.sp_KhoaCongThang @Nam INT, @Thang INT AS
BEGIN
    SET NOCOUNT ON; SET XACT_ABORT ON;
    DECLARE @D0 DATE = DATEFROMPARTS(@Nam,@Thang,1);
    DECLARE @D1 DATE = EOMONTH(@D0);
    EXEC sp_set_session_context 'SkipTrigger', '1';
    BEGIN TRAN;
    UPDATE dbo.LichPhanCa SET TrangThai = N'Khoa' WHERE NgayLam BETWEEN @D0 AND @D1 AND TrangThai <> N'Khoa';
    UPDATE dbo.ChamCong SET Khoa = 1 WHERE NgayLam BETWEEN @D0 AND @D1 AND Khoa = 0;
    COMMIT;
    EXEC sp_set_session_context 'SkipTrigger', '0';
END
GO
PRINT N'Đã cập nhật xong Stored Procedure.';

------------------------------------------------------------
-- 1. DỮ LIỆU CHO CÁC BẢNG DANH MỤC
------------------------------------------------------------
PRINT N'Chèn dữ liệu Phòng Ban...';
INSERT INTO dbo.PhongBan (TenPhongBan) VALUES (N'Ban Giám đốc'),(N'Phòng Nhân sự'),(N'Phòng Kế toán'),(N'Bộ phận Bán hàng'),(N'Bộ phận Kho'),(N'Bộ phận Thu ngân');
PRINT N'Chèn dữ liệu Chức Vụ...';
INSERT INTO dbo.ChucVu (TenChucVu) VALUES (N'Giám đốc'),(N'Trưởng phòng'),(N'Nhân viên Nhân sự'),(N'Kế toán viên'),(N'Nhân viên Bán hàng'),(N'Nhân viên Kho'),(N'Nhân viên Thu ngân');
PRINT N'Chèn dữ liệu Ca Làm...';
INSERT INTO dbo.CaLam (TenCa, GioBatDau, GioKetThuc, HeSoCa) VALUES (N'Ca Sáng', '06:00:00', '14:00:00', 1.0),(N'Ca Chiều', '14:00:00', '22:00:00', 1.0),(N'Ca Đêm (Qua ngày)', '22:00:00', '06:00:00', 1.5),(N'Ca Hành chính', '08:00:00', '17:00:00', 1.0);

------------------------------------------------------------
-- 2. DỮ LIỆU NGƯỜI DÙNG VÀ NHÂN VIÊN
------------------------------------------------------------
PRINT N'Chèn dữ liệu Người Dùng...';
INSERT INTO dbo.NguoiDung (TenDangNhap, MatKhauHash, VaiTro, KichHoat) VALUES
('giamdoc', '123', N'QuanLy', 1),('hr_manager', '123', N'HR', 1),('ketoan01', '123', N'KeToan', 1),('nv_banhang_01', '123', N'NhanVien', 1),('nv_kho_01', '123', N'NhanVien', 1),('nv_thungan_01', '123', N'NhanVien', 1),('nv_hr_01', '123', N'NhanVien', 1),('nv_banhang_02', '123', N'NhanVien', 1),('nv_nghiviec', '123', N'NhanVien', 0);

PRINT N'Chèn dữ liệu Nhân Viên...';
INSERT INTO dbo.NhanVien (MaNguoiDung, HoTen, NgaySinh, GioiTinh, DienThoai, Email, DiaChi, NgayVaoLam, TrangThai, MaPhongBan, MaChucVu, LuongCoBan)
SELECT
    (SELECT MaNguoiDung FROM dbo.NguoiDung WHERE TenDangNhap = 'giamdoc'), N'Nguyễn Văn An', '1980-05-20', N'Nam', '0901112221', 'giamdoc@minimart.com', N'123 Lê Lợi, Q1, TPHCM', '2020-01-15', N'DangLam',
    (SELECT MaPhongBan FROM dbo.PhongBan WHERE TenPhongBan = N'Ban Giám đốc'), (SELECT MaChucVu FROM dbo.ChucVu WHERE TenChucVu = N'Giám đốc'), 50000000
UNION ALL SELECT (SELECT MaNguoiDung FROM dbo.NguoiDung WHERE TenDangNhap = 'hr_manager'), N'Trần Thị Bích', '1988-10-02', N'Nu', '0901112222', 'hr.manager@minimart.com', N'456 Nguyễn Trãi, Q5, TPHCM', '2021-03-10', N'DangLam',
    (SELECT MaPhongBan FROM dbo.PhongBan WHERE TenPhongBan = N'Phòng Nhân sự'), (SELECT MaChucVu FROM dbo.ChucVu WHERE TenChucVu = N'Trưởng phòng'), 25000000
UNION ALL SELECT (SELECT MaNguoiDung FROM dbo.NguoiDung WHERE TenDangNhap = 'ketoan01'), N'Lê Văn Cường', '1992-07-11', N'Nam', '0901112223', 'ketoan01@minimart.com', N'789 CMT8, Q3, TPHCM', '2022-09-01', N'DangLam',
    (SELECT MaPhongBan FROM dbo.PhongBan WHERE TenPhongBan = N'Phòng Kế toán'), (SELECT MaChucVu FROM dbo.ChucVu WHERE TenChucVu = N'Kế toán viên'), 15000000
UNION ALL SELECT (SELECT MaNguoiDung FROM dbo.NguoiDung WHERE TenDangNhap = 'nv_banhang_01'), N'Phạm Thị Dung', '1998-11-30', N'Nu', '0901112224', 'dungpt@minimart.com', N'111 HBT, Q1, TPHCM', '2023-05-20', N'DangLam',
    (SELECT MaPhongBan FROM dbo.PhongBan WHERE TenPhongBan = N'Bộ phận Bán hàng'), (SELECT MaChucVu FROM dbo.ChucVu WHERE TenChucVu = N'Nhân viên Bán hàng'), 8500000
UNION ALL SELECT (SELECT MaNguoiDung FROM dbo.NguoiDung WHERE TenDangNhap = 'nv_kho_01'), N'Hoàng Văn Em', '2000-02-15', N'Nam', '0901112225', 'emhv@minimart.com', N'222 Võ Văn Tần, Q3, TPHCM', '2024-08-18', N'DangLam',
    (SELECT MaPhongBan FROM dbo.PhongBan WHERE TenPhongBan = N'Bộ phận Kho'), (SELECT MaChucVu FROM dbo.ChucVu WHERE TenChucVu = N'Nhân viên Kho'), 8000000
UNION ALL SELECT (SELECT MaNguoiDung FROM dbo.NguoiDung WHERE TenDangNhap = 'nv_thungan_01'), N'Vũ Thị Giang', '1999-06-25', N'Nu', '0901112226', 'giangvt@minimart.com', N'333 ĐBP, Bình Thạnh, TPHCM', '2025-07-01', N'DangLam',
    (SELECT MaPhongBan FROM dbo.PhongBan WHERE TenPhongBan = N'Bộ phận Thu ngân'), (SELECT MaChucVu FROM dbo.ChucVu WHERE TenChucVu = N'Nhân viên Thu ngân'), 8200000
UNION ALL SELECT (SELECT MaNguoiDung FROM dbo.NguoiDung WHERE TenDangNhap = 'nv_hr_01'), N'Ngô Thị Lan', '1995-04-12', N'Nu', '0901112228', 'lannt@minimart.com', N'555 Phan Xích Long, PN, TPHCM', '2025-07-15', N'DangLam',
    (SELECT MaPhongBan FROM dbo.PhongBan WHERE TenPhongBan = N'Phòng Nhân sự'), (SELECT MaChucVu FROM dbo.ChucVu WHERE TenChucVu = N'Nhân viên Nhân sự'), 12000000
UNION ALL SELECT (SELECT MaNguoiDung FROM dbo.NguoiDung WHERE TenDangNhap = 'nv_banhang_02'), N'Lý Thị Mai', '2001-08-08', N'Nu', '0901112229', 'mailt@minimart.com', N'666 Hoàng Diệu, Q4, TPHCM', '2025-08-01', N'DangLam',
    (SELECT MaPhongBan FROM dbo.PhongBan WHERE TenPhongBan = N'Bộ phận Bán hàng'), (SELECT MaChucVu FROM dbo.ChucVu WHERE TenChucVu = N'Nhân viên Bán hàng'), 8500000
UNION ALL SELECT (SELECT MaNguoiDung FROM dbo.NguoiDung WHERE TenDangNhap = 'nv_nghiviec'), N'Đỗ Văn Hùng', '1995-01-01', N'Nam', '0901112227', 'hungdv@minimart.com', N'444 XVNT, Bình Thạnh, TPHCM', '2023-01-01', N'Nghi',
    (SELECT MaPhongBan FROM dbo.PhongBan WHERE TenPhongBan = N'Bộ phận Bán hàng'), (SELECT MaChucVu FROM dbo.ChucVu WHERE TenChucVu = N'Nhân viên Bán hàng'), 8000000;

------------------------------------------------------------
-- 3. DỮ LIỆU LỊCH PHÂN CA VÀ CHẤM CÔNG
------------------------------------------------------------
PRINT N'Bắt đầu tạo dữ liệu Lịch Phân Ca và Chấm Công...';
DECLARE @CurrentDate DATE = '2025-07-01'; DECLARE @EndDate DATE = GETDATE();
WHILE @CurrentDate <= @EndDate
BEGIN
    INSERT INTO dbo.LichPhanCa (MaNV, NgayLam, MaCa, TrangThai) 
    SELECT MaNV, @CurrentDate, (SELECT MaCa FROM CaLam WHERE TenCa = N'Ca Hành chính'), IIF(MONTH(@CurrentDate) < MONTH(@EndDate), N'Khoa', N'DuKien') 
    FROM dbo.NhanVien WHERE DATEPART(weekday, @CurrentDate) NOT IN (1, 7) AND MaChucVu IN (SELECT MaChucVu FROM dbo.ChucVu WHERE TenChucVu IN (N'Giám đốc', N'Trưởng phòng', N'Kế toán viên', N'Nhân viên Nhân sự')) AND NgayVaoLam <= @CurrentDate AND TrangThai = 'DangLam';
    
    INSERT INTO dbo.LichPhanCa (MaNV, NgayLam, MaCa, TrangThai) SELECT MaNV, @CurrentDate, (SELECT MaCa FROM CaLam WHERE TenCa = N'Ca Sáng'), IIF(MONTH(@CurrentDate) < MONTH(@EndDate), N'Khoa', N'DuKien') FROM dbo.NhanVien WHERE HoTen = N'Phạm Thị Dung' AND NgayVaoLam <= @CurrentDate AND TrangThai = 'DangLam';
    INSERT INTO dbo.LichPhanCa (MaNV, NgayLam, MaCa, TrangThai) SELECT MaNV, @CurrentDate, (SELECT MaCa FROM CaLam WHERE TenCa = N'Ca Chiều'), IIF(MONTH(@CurrentDate) < MONTH(@EndDate), N'Khoa', N'DuKien') FROM dbo.NhanVien WHERE HoTen IN (N'Lý Thị Mai', N'Hoàng Văn Em') AND NgayVaoLam <= @CurrentDate AND TrangThai = 'DangLam';
    IF (DAY(@CurrentDate) % 2 = 0) INSERT INTO dbo.LichPhanCa (MaNV, NgayLam, MaCa, TrangThai) SELECT MaNV, @CurrentDate, (SELECT MaCa FROM CaLam WHERE TenCa = N'Ca Đêm (Qua ngày)'), IIF(MONTH(@CurrentDate) < MONTH(@EndDate), N'Khoa', N'DuKien') FROM dbo.NhanVien WHERE HoTen = N'Vũ Thị Giang' AND NgayVaoLam <= @CurrentDate AND TrangThai = 'DangLam';
    
    IF MONTH(@CurrentDate) < MONTH(@EndDate)
    BEGIN
        INSERT INTO dbo.ChamCong(MaNV, NgayLam, GioVao, GioRa)
        SELECT lpc.MaNV, lpc.NgayLam,
            DATEADD(minute, (lpc.MaNV % 7) - 3,  CAST(lpc.NgayLam AS DATETIME) + CAST(cl.GioBatDau AS DATETIME)),
            DATEADD(minute, (lpc.MaNV % 9) - 4,  CAST(DATEADD(day, IIF(cl.GioKetThuc < cl.GioBatDau, 1, 0), lpc.NgayLam) AS DATETIME) + CAST(cl.GioKetThuc AS DATETIME))
        FROM dbo.LichPhanCa lpc JOIN dbo.CaLam cl ON lpc.MaCa = cl.MaCa
        WHERE lpc.NgayLam = @CurrentDate;
    END
    SET @CurrentDate = DATEADD(day, 1, @CurrentDate);
END
PRINT N'Đã tạo xong dữ liệu Lịch Phân Ca và Chấm Công.';

------------------------------------------------------------
-- 4. DỮ LIỆU ĐƠN TỪ
------------------------------------------------------------
PRINT N'Chèn dữ liệu Đơn Từ...';
INSERT INTO dbo.DonTu (MaNV, Loai, TuLuc, DenLuc, LyDo, TrangThai, DuyetBoi)
SELECT (SELECT MaNV FROM NhanVien WHERE HoTen = N'Phạm Thị Dung'), N'NGHI', '2025-07-15 06:00:00', '2025-07-15 14:00:00', N'Việc gia đình', N'DaDuyet', (SELECT MaNguoiDung FROM NguoiDung WHERE TenDangNhap = 'hr_manager')
UNION ALL SELECT (SELECT MaNV FROM NhanVien WHERE HoTen = N'Hoàng Văn Em'), N'OT', '2025-08-10 22:00:00', '2025-08-11 01:00:00', N'Kiểm kê kho gấp', N'DaDuyet', (SELECT MaNguoiDung FROM NguoiDung WHERE TenDangNhap = 'giamdoc')
UNION ALL SELECT (SELECT MaNV FROM NhanVien WHERE HoTen = N'Vũ Thị Giang'), N'NGHI', '2025-08-20 22:00:00', '2025-08-22 06:00:00', N'Du lịch', N'TuChoi', (SELECT MaNguoiDung FROM NguoiDung WHERE TenDangNhap = 'hr_manager')
UNION ALL SELECT (SELECT MaNV FROM NhanVien WHERE HoTen = N'Lê Văn Cường'), N'NGHI', '2025-09-29 08:00:00', '2025-09-29 17:00:00', N'Đi khám bệnh', N'ChoDuyet', NULL
UNION ALL SELECT (SELECT MaNV FROM NhanVien WHERE HoTen = N'Ngô Thị Lan'), N'NGHI', '2025-09-30 08:00:00', '2025-09-30 12:00:00', N'Làm giấy tờ', N'ChoDuyet', NULL
UNION ALL SELECT (SELECT MaNV FROM NhanVien WHERE HoTen = N'Lý Thị Mai'), N'OT', '2025-09-24 22:00:00', '2025-09-25 00:00:00', N'Hỗ trợ khuyến mãi', N'DaDuyet', (SELECT MaNguoiDung FROM NguoiDung WHERE TenDangNhap = 'giamdoc');

------------------------------------------------------------
-- 5. TỰ ĐỘNG CHẠY STORED PROCEDURE TÍNH LƯƠNG
------------------------------------------------------------
PRINT N'Bắt đầu thực thi Stored Procedures để tự động khóa công và tính lương...';
EXEC dbo.sp_KhoaCongThang @Nam = 2025, @Thang = 7;
EXEC dbo.sp_KhoaCongThang @Nam = 2025, @Thang = 8;
PRINT N'Hoàn tất khóa công cho tháng 7, 8.';
EXEC dbo.sp_ChayBangLuong @Nam = 2025, @Thang = 7;
EXEC dbo.sp_ChayBangLuong @Nam = 2025, @Thang = 8;
PRINT N'Hoàn tất chạy bảng lương cho tháng 7, 8.';

------------------------------------------------------------
-- 6. KIỂM TRA KẾT QUẢ
------------------------------------------------------------
PRINT N'Dữ liệu bảng lương đã được tạo. Dưới đây là kết quả:';
SELECT Nam, Thang, HoTen, PhongBan, ChucDanh, LuongCoBan, TongGioCong, GioOT, ThucLanh, TrangThai
FROM dbo.vw_BangLuong_ChiTiet 
WHERE (Nam = 2025 AND Thang IN (7, 8)) 
ORDER BY Nam, Thang, HoTen;
GO

COMMIT TRAN;

PRINT N'====== HOÀN TẤT TOÀN BỘ SCRIPT! ======';
GO