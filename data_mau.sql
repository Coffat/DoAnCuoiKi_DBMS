/* ===============================================================================
   DỮ LIỆU MẪU TỔNG HỢP - HỆ THỐNG QUẢN LÝ NHÂN SỰ SIÊU THỊ MINI
   
   File này bao gồm TẤT CẢ dữ liệu mẫu:
   - Phòng ban, Chức vụ, Ca làm
   - Người dùng và Nhân viên (9 người)
   - Lịch phân ca tự động (từ 1/7/2025 đến hôm nay)
   - Chấm công tự động cho các tháng đã khóa
   - Đơn từ mẫu (6 đơn)
   - Bảng lương tự động (các tháng đã qua)
   
   CÁCH SỬ DỤNG:
   1. Đã chạy các file: 01, 02, 03, 04, 05
   2. Chạy file này để có dữ liệu mẫu đầy đủ
   3. Đăng nhập với username/password: giamdoc/123 hoặc hr_manager/123
   
   =============================================================================== */

USE QLNhanSuSieuThiMini;
GO

SET NOCOUNT ON;

PRINT N'====================================================================';
PRINT N'BẮT ĐẦU TẠO DỮ LIỆU MẪU - ' + CONVERT(NVARCHAR, GETDATE(), 120);
PRINT N'User hiện tại: ' + USER_NAME();
PRINT N'====================================================================';

SET XACT_ABORT ON;
BEGIN TRAN;

-- TẮT TRIGGER (chỉ tắt trigger chặn khóa, giữ trigger tính công để tự động tính GioCong/DiTre/VeSom)
-- ✅ Đã xóa: tr_LichPhanCa_NoEditWhenKhoa (trùng lặp)
ALTER TABLE dbo.LichPhanCa DISABLE TRIGGER tr_LichPhanCa_BlockChangeWhenLocked;
ALTER TABLE dbo.ChamCong DISABLE TRIGGER tr_ChamCong_BlockWhenLocked_U;
ALTER TABLE dbo.ChamCong DISABLE TRIGGER tr_ChamCong_BlockWhenLocked_D;
ALTER TABLE dbo.NhanVien DISABLE TRIGGER tr_NhanVien_ToggleAccount;

-- XÓA DỮ LIỆU CŨ
PRINT N'[1/7] Xóa dữ liệu cũ...';
DELETE FROM dbo.DonTu; DELETE FROM dbo.BangLuong; DELETE FROM dbo.ChamCong;
DELETE FROM dbo.LichPhanCa; DELETE FROM dbo.NhanVien; DELETE FROM dbo.NguoiDung;
DELETE FROM dbo.PhongBan; DELETE FROM dbo.ChucVu; DELETE FROM dbo.CaLam;

-- ✅ KHÔNG RESET IDENTITY - Để ID bắt đầu từ 1 tự nhiên
-- Khi bảng rỗng và không RESEED, IDENTITY sẽ tự động bắt đầu từ 1
PRINT N'✓ Bảng rỗng - ID sẽ tự động bắt đầu từ 1';

-- TẠO DANH MỤC
PRINT N'[2/7] Tạo danh mục...';
INSERT INTO dbo.PhongBan (TenPhongBan, MoTa, KichHoat) VALUES 
(N'Ban Giám đốc', N'Điều hành chung', 1), (N'Phòng Nhân sự', N'Quản lý nhân sự', 1),
(N'Phòng Kế toán', N'Kế toán tài chính', 1), (N'Bộ phận Bán hàng', N'Bán hàng', 1),
(N'Bộ phận Kho', N'Quản lý kho', 1), (N'Bộ phận Thu ngân', N'Thu ngân', 1);
PRINT N'✓ Phòng ban: ' + CAST(@@ROWCOUNT AS NVARCHAR) + 
      N' (MaPhongBan: ' + CAST(IDENT_CURRENT('dbo.PhongBan') AS NVARCHAR) + N')';

INSERT INTO dbo.ChucVu (TenChucVu, MoTa, KichHoat) VALUES 
(N'Giám đốc', N'Giám đốc điều hành', 1), (N'Trưởng phòng', N'Trưởng phòng', 1),
(N'Nhân viên Nhân sự', N'NV nhân sự', 1), (N'Kế toán viên', N'NV kế toán', 1),
(N'Nhân viên Bán hàng', N'NV bán hàng', 1), (N'Nhân viên Kho', N'NV kho', 1),
(N'Nhân viên Thu ngân', N'NV thu ngân', 1);
PRINT N'✓ Chức vụ: ' + CAST(@@ROWCOUNT AS NVARCHAR) + 
      N' (MaChucVu: ' + CAST(IDENT_CURRENT('dbo.ChucVu') AS NVARCHAR) + N')';

INSERT INTO dbo.CaLam (TenCa, GioBatDau, GioKetThuc, HeSoCa, MoTa, KichHoat) VALUES 
(N'Ca Sáng', '06:00', '14:00', 1.0, N'Ca sáng', 1),
(N'Ca Chiều', '14:00', '22:00', 1.0, N'Ca chiều', 1),
(N'Ca Đêm (Qua ngày)', '22:00', '06:00', 1.5, N'Ca qua đêm', 1),
(N'Ca Hành chính', '08:00', '17:00', 1.0, N'Ca hành chính', 1);
PRINT N'✓ Ca làm: ' + CAST(@@ROWCOUNT AS NVARCHAR);

-- ✅ SỬA: Không override sp_TaoTaiKhoanDayDu nữa
-- Sử dụng stored procedure gốc từ file 04_StoredProcedures_Advanced.sql
-- Procedure đó có đầy đủ logic tạo Database User WITHOUT LOGIN

-- NGƯỜI DÙNG VÀ NHÂN VIÊN (Tạo qua SP để tự động tạo SQL Login + Role)
PRINT N'[3/7] Tạo người dùng và nhân viên (với SQL Login + Role)...';

-- Debug: Kiểm tra PhongBan và ChucVu đã tạo chưa
SELECT 'PhongBan' as TableName, MaPhongBan, TenPhongBan FROM dbo.PhongBan;
SELECT 'ChucVu' as TableName, MaChucVu, TenChucVu FROM dbo.ChucVu;

-- Tạo từng tài khoản qua sp_TaoTaiKhoanDayDu
-- Thứ tự tham số: @HoTen, @NgaySinh, @GioiTinh, @DienThoai, @Email, @DiaChi, @NgayVaoLam, @MaPhongBan, @MaChucVu, @LuongCoBan, @TenDangNhap, @MatKhau, @VaiTro

-- ✅ SỬA: Lấy ID thực tế từ database thay vì hardcode
DECLARE @MaNV_Out INT;
DECLARE @PhongBan_GiamDoc INT, @PhongBan_NhanSu INT, @PhongBan_KeToan INT;
DECLARE @PhongBan_BanHang INT, @PhongBan_Kho INT, @PhongBan_ThuNgan INT;
DECLARE @ChucVu_GiamDoc INT, @ChucVu_TruongPhong INT, @ChucVu_NhanSu INT;
DECLARE @ChucVu_KeToan INT, @ChucVu_BanHang INT, @ChucVu_Kho INT, @ChucVu_ThuNgan INT;

-- Lấy ID thực tế từ database
SELECT @PhongBan_GiamDoc = MaPhongBan FROM dbo.PhongBan WHERE TenPhongBan = N'Ban Giám đốc';
SELECT @PhongBan_NhanSu = MaPhongBan FROM dbo.PhongBan WHERE TenPhongBan = N'Phòng Nhân sự';
SELECT @PhongBan_KeToan = MaPhongBan FROM dbo.PhongBan WHERE TenPhongBan = N'Phòng Kế toán';
SELECT @PhongBan_BanHang = MaPhongBan FROM dbo.PhongBan WHERE TenPhongBan = N'Bộ phận Bán hàng';
SELECT @PhongBan_Kho = MaPhongBan FROM dbo.PhongBan WHERE TenPhongBan = N'Bộ phận Kho';
SELECT @PhongBan_ThuNgan = MaPhongBan FROM dbo.PhongBan WHERE TenPhongBan = N'Bộ phận Thu ngân';

SELECT @ChucVu_GiamDoc = MaChucVu FROM dbo.ChucVu WHERE TenChucVu = N'Giám đốc';
SELECT @ChucVu_TruongPhong = MaChucVu FROM dbo.ChucVu WHERE TenChucVu = N'Trưởng phòng';
SELECT @ChucVu_NhanSu = MaChucVu FROM dbo.ChucVu WHERE TenChucVu = N'Nhân viên Nhân sự';
SELECT @ChucVu_KeToan = MaChucVu FROM dbo.ChucVu WHERE TenChucVu = N'Kế toán viên';
SELECT @ChucVu_BanHang = MaChucVu FROM dbo.ChucVu WHERE TenChucVu = N'Nhân viên Bán hàng';
SELECT @ChucVu_Kho = MaChucVu FROM dbo.ChucVu WHERE TenChucVu = N'Nhân viên Kho';
SELECT @ChucVu_ThuNgan = MaChucVu FROM dbo.ChucVu WHERE TenChucVu = N'Nhân viên Thu ngân';

-- Debug: Hiển thị các ID được lấy
PRINT N'=== DEBUG: ID được lấy ===';
PRINT N'@PhongBan_GiamDoc: ' + CAST(ISNULL(@PhongBan_GiamDoc, -1) AS NVARCHAR);
PRINT N'@PhongBan_NhanSu: ' + CAST(ISNULL(@PhongBan_NhanSu, -1) AS NVARCHAR);
PRINT N'@PhongBan_KeToan: ' + CAST(ISNULL(@PhongBan_KeToan, -1) AS NVARCHAR);
PRINT N'@PhongBan_BanHang: ' + CAST(ISNULL(@PhongBan_BanHang, -1) AS NVARCHAR);
PRINT N'@PhongBan_Kho: ' + CAST(ISNULL(@PhongBan_Kho, -1) AS NVARCHAR);
PRINT N'@PhongBan_ThuNgan: ' + CAST(ISNULL(@PhongBan_ThuNgan, -1) AS NVARCHAR);
PRINT N'@ChucVu_GiamDoc: ' + CAST(ISNULL(@ChucVu_GiamDoc, -1) AS NVARCHAR);
PRINT N'@ChucVu_TruongPhong: ' + CAST(ISNULL(@ChucVu_TruongPhong, -1) AS NVARCHAR);
PRINT N'@ChucVu_NhanSu: ' + CAST(ISNULL(@ChucVu_NhanSu, -1) AS NVARCHAR);
PRINT N'@ChucVu_KeToan: ' + CAST(ISNULL(@ChucVu_KeToan, -1) AS NVARCHAR);
PRINT N'@ChucVu_BanHang: ' + CAST(ISNULL(@ChucVu_BanHang, -1) AS NVARCHAR);
PRINT N'@ChucVu_Kho: ' + CAST(ISNULL(@ChucVu_Kho, -1) AS NVARCHAR);
PRINT N'@ChucVu_ThuNgan: ' + CAST(ISNULL(@ChucVu_ThuNgan, -1) AS NVARCHAR);

-- ✅ VALIDATION: Kiểm tra tất cả ID đều được tìm thấy
IF @PhongBan_ThuNgan IS NULL
BEGIN
    RAISERROR(N'❌ Không tìm thấy phòng ban "Bộ phận Thu ngân"', 16, 1);
    RETURN;
END

IF @ChucVu_ThuNgan IS NULL  
BEGIN
    RAISERROR(N'❌ Không tìm thấy chức vụ "Nhân viên Thu ngân"', 16, 1);
    RETURN;
END

EXEC dbo.sp_TaoTaiKhoanDayDu N'Nguyễn Văn An', '1980-05-20', N'Nam', '0901112221', 'giamdoc@minimart.com', N'123 Lê Lợi, Q1', '2020-01-15', @PhongBan_GiamDoc, @ChucVu_GiamDoc, 50000000, 'giamdoc', '1', N'QuanLy', @MaNV_Out OUTPUT;
PRINT N'✓ 1. giamdoc (QuanLy) - MaNV: ' + CAST(@MaNV_Out AS NVARCHAR);

EXEC dbo.sp_TaoTaiKhoanDayDu N'Trần Thị Bích', '1988-10-02', N'Nu', '0901112222', 'hr.manager@minimart.com', N'456 Nguyễn Trãi, Q5', '2021-03-10', @PhongBan_NhanSu, @ChucVu_TruongPhong, 25000000, 'hr_manager', '1', N'HR', @MaNV_Out OUTPUT;
PRINT N'✓ 2. hr_manager (HR) - MaNV: ' + CAST(@MaNV_Out AS NVARCHAR);

EXEC dbo.sp_TaoTaiKhoanDayDu N'Lê Văn Cường', '1992-07-11', N'Nam', '0901112223', 'ketoan01@minimart.com', N'789 CMT8, Q3', '2022-09-01', @PhongBan_KeToan, @ChucVu_KeToan, 15000000, 'ketoan01', '1', N'KeToan', @MaNV_Out OUTPUT;
PRINT N'✓ 3. ketoan01 (KeToan) - MaNV: ' + CAST(@MaNV_Out AS NVARCHAR);

EXEC dbo.sp_TaoTaiKhoanDayDu N'Ngô Thị Lan', '1995-04-12', N'Nu', '0901112228', 'lannt@minimart.com', N'555 Phan Xích Long', '2023-06-15', @PhongBan_NhanSu, @ChucVu_NhanSu, 12000000, 'nv_hr_01', '1', N'NhanVien', @MaNV_Out OUTPUT;
PRINT N'✓ 4. nv_hr_01 (NhanVien) - MaNV: ' + CAST(@MaNV_Out AS NVARCHAR);

EXEC dbo.sp_TaoTaiKhoanDayDu N'Phạm Thị Dung', '1998-11-30', N'Nu', '0901112224', 'dungpt@minimart.com', N'111 HBT, Q1', '2023-05-20', @PhongBan_BanHang, @ChucVu_BanHang, 8500000, 'nv_banhang_01', '1', N'NhanVien', @MaNV_Out OUTPUT;
PRINT N'✓ 5. nv_banhang_01 (NhanVien) - MaNV: ' + CAST(@MaNV_Out AS NVARCHAR);

EXEC dbo.sp_TaoTaiKhoanDayDu N'Lý Thị Mai', '2001-08-08', N'Nu', '0901112229', 'mailt@minimart.com', N'666 Hoàng Diệu, Q4', '2024-01-15', @PhongBan_BanHang, @ChucVu_BanHang, 8500000, 'nv_banhang_02', '1', N'NhanVien', @MaNV_Out OUTPUT;
PRINT N'✓ 6. nv_banhang_02 (NhanVien) - MaNV: ' + CAST(@MaNV_Out AS NVARCHAR);

EXEC dbo.sp_TaoTaiKhoanDayDu N'Hoàng Văn Em', '2000-02-15', N'Nam', '0901112225', 'emhv@minimart.com', N'222 Võ Văn Tần, Q3', '2024-02-01', @PhongBan_Kho, @ChucVu_Kho, 8000000, 'nv_kho_01', '1', N'NhanVien', @MaNV_Out OUTPUT;
PRINT N'✓ 7. nv_kho_01 (NhanVien) - MaNV: ' + CAST(@MaNV_Out AS NVARCHAR);

EXEC dbo.sp_TaoTaiKhoanDayDu N'Vũ Thị Giang', '1999-06-25', N'Nu', '0901112226', 'giangvt@minimart.com', N'333 ĐBP, Bình Thạnh', '2024-03-01', @PhongBan_ThuNgan, @ChucVu_ThuNgan, 8200000, 'nv_thungan_01', '1', N'NhanVien', @MaNV_Out OUTPUT;
PRINT N'✓ 8. nv_thungan_01 (NhanVien) - MaNV: ' + CAST(@MaNV_Out AS NVARCHAR);

-- Nhân viên đã nghỉ việc - cần xử lý riêng vì SP tự động tạo tài khoản active
EXEC dbo.sp_TaoTaiKhoanDayDu N'Đỗ Văn Hùng', '1995-01-01', N'Nam', '0901112227', 'hungdv@minimart.com', N'444 XVNT, Bình Thạnh', '2023-01-01', @PhongBan_BanHang, @ChucVu_BanHang, 8000000, 'nv_nghiviec', '1', N'NhanVien', @MaNV_Out OUTPUT;
-- Cập nhật trạng thái nghỉ việc
UPDATE dbo.NhanVien SET TrangThai = N'Nghi' WHERE MaNV = @MaNV_Out;
UPDATE dbo.NguoiDung SET KichHoat = 0 WHERE MaNguoiDung = (SELECT MaNguoiDung FROM dbo.NhanVien WHERE MaNV = @MaNV_Out);
-- ✅ SỬA: Không disable SQL Login vì sp_TaoTaiKhoanDayDu tạo Database User WITHOUT LOGIN
PRINT N'✓ 9. nv_nghiviec (Đã nghỉ việc - tài khoản bị khóa) - MaNV: ' + CAST(@MaNV_Out AS NVARCHAR);

PRINT N'✓ Đã tạo 9 tài khoản với SQL Login + Role đầy đủ';

-- LỊCH PHÂN CA VÀ CHẤM CÔNG
PRINT N'[4/7] Tạo lịch và chấm công tự động...';
DECLARE @Start DATE = '2025-07-01', @End DATE = GETDATE();
DECLARE @Current DATE = @Start;
DECLARE @CurMonth INT = MONTH(@End), @CurYear INT = YEAR(@End);

-- ✅ SỬA: Lấy MaCa thực tế từ database thay vì hardcode
DECLARE @CS INT, @CC INT, @CT INT, @CH INT;
SELECT @CS = MaCa FROM dbo.CaLam WHERE TenCa = N'Ca Sáng';
SELECT @CC = MaCa FROM dbo.CaLam WHERE TenCa = N'Ca Chiều';  
SELECT @CT = MaCa FROM dbo.CaLam WHERE TenCa = N'Ca Đêm (Qua ngày)';
SELECT @CH = MaCa FROM dbo.CaLam WHERE TenCa = N'Ca Hành chính';

PRINT N'=== DEBUG: MaCa được lấy ===';
PRINT N'@CS (Ca Sáng): ' + CAST(ISNULL(@CS, -1) AS NVARCHAR);
PRINT N'@CC (Ca Chiều): ' + CAST(ISNULL(@CC, -1) AS NVARCHAR);
PRINT N'@CT (Ca Đêm): ' + CAST(ISNULL(@CT, -1) AS NVARCHAR);
PRINT N'@CH (Ca Hành chính): ' + CAST(ISNULL(@CH, -1) AS NVARCHAR);

DECLARE @LC INT = 0, @CgC INT = 0;

WHILE @Current <= @End
BEGIN
    DECLARE @Past BIT = CASE WHEN YEAR(@Current) < @CurYear OR (YEAR(@Current) = @CurYear AND MONTH(@Current) < @CurMonth) THEN 1 ELSE 0 END;
    DECLARE @TS NVARCHAR(12) = CASE WHEN @Past = 1 THEN N'Khoa' ELSE N'DuKien' END;
    
    IF DATEPART(WEEKDAY, @Current) NOT IN (1,7) 
    BEGIN
        INSERT INTO dbo.LichPhanCa (MaNV, NgayLam, MaCa, TrangThai)
        SELECT MaNV, @Current, @CH, @TS FROM dbo.NhanVien 
        WHERE MaChucVu IN (@ChucVu_GiamDoc, @ChucVu_TruongPhong, @ChucVu_NhanSu, @ChucVu_KeToan) AND NgayVaoLam <= @Current AND TrangThai = N'DangLam';
        SET @LC = @LC + @@ROWCOUNT;
    END
    
    INSERT INTO dbo.LichPhanCa (MaNV, NgayLam, MaCa, TrangThai)
    SELECT MaNV, @Current, @CS, @TS FROM dbo.NhanVien WHERE HoTen = N'Phạm Thị Dung' AND NgayVaoLam <= @Current AND TrangThai = N'DangLam';
    SET @LC = @LC + @@ROWCOUNT;
    
    INSERT INTO dbo.LichPhanCa (MaNV, NgayLam, MaCa, TrangThai)
    SELECT MaNV, @Current, @CC, @TS FROM dbo.NhanVien WHERE HoTen IN (N'Lý Thị Mai', N'Hoàng Văn Em') AND NgayVaoLam <= @Current AND TrangThai = N'DangLam';
    SET @LC = @LC + @@ROWCOUNT;
    
    IF DAY(@Current) % 2 = 0
    BEGIN
        INSERT INTO dbo.LichPhanCa (MaNV, NgayLam, MaCa, TrangThai)
        SELECT MaNV, @Current, @CT, @TS FROM dbo.NhanVien WHERE HoTen = N'Vũ Thị Giang' AND NgayVaoLam <= @Current AND TrangThai = N'DangLam';
        SET @LC = @LC + @@ROWCOUNT;
    END
    
    IF @Past = 1
    BEGIN
        INSERT INTO dbo.ChamCong (MaNV, NgayLam, GioVao, GioRa, Khoa)
        SELECT lpc.MaNV, lpc.NgayLam,
            DATEADD(MINUTE, (lpc.MaNV % 15) - 5, CAST(lpc.NgayLam AS DATETIME) + CAST(cl.GioBatDau AS DATETIME)),
            DATEADD(MINUTE, (lpc.MaNV % 15) - 10, CAST(DATEADD(DAY, IIF(cl.GioKetThuc < cl.GioBatDau, 1, 0), lpc.NgayLam) AS DATETIME) + CAST(cl.GioKetThuc AS DATETIME)),
            1
        FROM dbo.LichPhanCa lpc INNER JOIN dbo.CaLam cl ON lpc.MaCa = cl.MaCa
        WHERE lpc.NgayLam = @Current AND lpc.TrangThai = N'Khoa';
        SET @CgC = @CgC + @@ROWCOUNT;
    END
    
    SET @Current = DATEADD(DAY, 1, @Current);
END

PRINT N'✓ Lịch: ' + CAST(@LC AS NVARCHAR) + N', Chấm công: ' + CAST(@CgC AS NVARCHAR);

-- ĐƠN TỪ
PRINT N'[5/7] Tạo đơn từ...';
INSERT INTO dbo.DonTu (MaNV, Loai, TuLuc, DenLuc, SoGio, LyDo, TrangThai, DuyetBoi) VALUES
(5, N'NGHI', '2025-07-15 06:00', '2025-07-15 14:00', 8, N'Việc gia đình', N'DaDuyet', 2),
(7, N'OT', '2025-08-10 22:00', '2025-08-11 01:00', 3, N'Kiểm kê kho gấp', N'DaDuyet', 1),
(8, N'NGHI', '2025-08-20 22:00', '2025-08-22 06:00', 16, N'Du lịch', N'TuChoi', 2),
(3, N'NGHI', CAST(CONVERT(VARCHAR(10), GETDATE(), 120) + ' 08:00' AS DATETIME), CAST(CONVERT(VARCHAR(10), GETDATE(), 120) + ' 12:00' AS DATETIME), 4, N'Đi khám bệnh', N'ChoDuyet', NULL),
(4, N'NGHI', CAST(CONVERT(VARCHAR(10), GETDATE(), 120) + ' 13:00' AS DATETIME), CAST(CONVERT(VARCHAR(10), GETDATE(), 120) + ' 17:00' AS DATETIME), 4, N'Làm giấy tờ', N'ChoDuyet', NULL),
(6, N'OT', '2025-09-24 22:00', '2025-09-25 00:00', 2, N'Hỗ trợ khuyến mãi đêm', N'DaDuyet', 1);

-- KHÓA CÔNG VÀ TÍNH LƯƠNG
PRINT N'[6/7] Khóa công và tính lương...';
DECLARE @M INT = 7, @Y INT = 2025;
WHILE (@Y < @CurYear) OR (@Y = @CurYear AND @M < @CurMonth)
BEGIN
    BEGIN TRY
        EXEC dbo.sp_KhoaCongThang @Nam = @Y, @Thang = @M;
        EXEC dbo.sp_ChayBangLuong @Nam = @Y, @Thang = @M, @StdHours = 160;
        PRINT N'✓ Tháng ' + CAST(@M AS NVARCHAR) + '/' + CAST(@Y AS NVARCHAR);
    END TRY BEGIN CATCH PRINT N'⚠ ' + ERROR_MESSAGE(); END CATCH
    SET @M = @M + 1; IF @M > 12 BEGIN SET @M = 1; SET @Y = @Y + 1; END
END

-- THỐNG KÊ
PRINT N'';
PRINT N'[7/7] Thống kê:';
PRINT N'─────────────────────────────────';
DECLARE @CNguoiDung INT, @CNhanVien INT, @CLich INT, @CChamCong INT, @CDonTu INT, @CBangLuong INT;
SELECT @CNguoiDung = COUNT(*) FROM dbo.NguoiDung;
SELECT @CNhanVien = COUNT(*) FROM dbo.NhanVien WHERE TrangThai = N'DangLam';
SELECT @CLich = COUNT(*) FROM dbo.LichPhanCa;
SELECT @CChamCong = COUNT(*) FROM dbo.ChamCong;
SELECT @CDonTu = COUNT(*) FROM dbo.DonTu;
SELECT @CBangLuong = COUNT(DISTINCT CAST(Nam AS NVARCHAR) + '-' + CAST(Thang AS NVARCHAR)) FROM dbo.BangLuong;
PRINT N'Người dùng: ' + CAST(@CNguoiDung AS NVARCHAR);
PRINT N'Nhân viên: ' + CAST(@CNhanVien AS NVARCHAR) + N'/9';
PRINT N'Lịch phân ca: ' + CAST(@CLich AS NVARCHAR);
PRINT N'Chấm công: ' + CAST(@CChamCong AS NVARCHAR);
PRINT N'Đơn từ: ' + CAST(@CDonTu AS NVARCHAR);
PRINT N'Bảng lương: ' + CAST(@CBangLuong AS NVARCHAR) + N' tháng';

-- BẬT LẠI TRIGGER
-- ✅ Đã xóa: tr_LichPhanCa_NoEditWhenKhoa (trùng lặp)
ALTER TABLE dbo.LichPhanCa ENABLE TRIGGER tr_LichPhanCa_BlockChangeWhenLocked;
ALTER TABLE dbo.ChamCong ENABLE TRIGGER tr_ChamCong_BlockWhenLocked_U;
ALTER TABLE dbo.ChamCong ENABLE TRIGGER tr_ChamCong_BlockWhenLocked_D;
ALTER TABLE dbo.NhanVien ENABLE TRIGGER tr_NhanVien_ToggleAccount;

COMMIT TRAN;

PRINT N'';
PRINT N'====================================================================';
PRINT N'✅ HOÀN TẤT! Mật khẩu tất cả tài khoản: 1';
PRINT N'✅ Tất cả ID bắt đầu từ 1';
PRINT N'====================================================================';
GO
