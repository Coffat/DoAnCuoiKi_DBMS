/* =========================================================
   DỮ LIỆU MẪU TỔNG HỢP - HỆ THỐNG QUẢN LÝ NHÂN SỰ SIÊU THỊ MINI
   
   Gom tất cả dữ liệu mẫu từ 3 file:
   1. duLieuMau.sql - Dữ liệu cơ bản (NguoiDung, NhanVien, CaLam)
   2. duLieuMau_DayDu_7_9.sql - Lịch phân ca và chấm công từ 1/7-21/9/2025
   3. test_lich_hom_nay.sql - Lịch làm việc hôm nay để test
   
   CẬP NHẬT: Đã xóa hoàn toàn RLS (Row Level Security)
   - Tất cả user có thể xem dữ liệu theo role permissions
   - Không còn hạn chế theo MaNV cá nhân
   - Sử dụng RBAC (Role-Based Access Control) thuần túy
   ========================================================= */

USE QLNhanSuSieuThiMini;
GO

PRINT N'=== BẮT ĐẦU CHẠY DỮ LIỆU MẪU TỔNG HỢP ===';
PRINT N'';

BEGIN TRANSACTION; -- Bắt đầu transaction để đảm bảo nhất quán

BEGIN TRY

    -- Tạm tắt trigger để insert dữ liệu mẫu
    EXEC sys.sp_set_session_context @key=N'SkipTrigger', @value=1;
    PRINT N'✅ Set SkipTrigger to 1 successful.';

    -- Xóa dữ liệu cũ để tránh trùng lặp khóa chính/duy nhất
    PRINT N'🗑️  Đang xóa dữ liệu cũ...';
    DELETE FROM dbo.BangLuong;
    DELETE FROM dbo.DonTu;
    DELETE FROM dbo.ChamCong;
    DELETE FROM dbo.LichPhanCa;
    DELETE FROM dbo.NhanVien;
    DELETE FROM dbo.NguoiDung;
    DELETE FROM dbo.CaLam;
    PRINT N'✅ Deleted existing data from all tables.';

    -- Reset identity seed cho các bảng
    DBCC CHECKIDENT ('dbo.NguoiDung', RESEED, 0);
    DBCC CHECKIDENT ('dbo.NhanVien', RESEED, 0);
    DBCC CHECKIDENT ('dbo.CaLam', RESEED, 0);
    DBCC CHECKIDENT ('dbo.LichPhanCa', RESEED, 0);
    DBCC CHECKIDENT ('dbo.ChamCong', RESEED, 0);
    DBCC CHECKIDENT ('dbo.DonTu', RESEED, 0);
    DBCC CHECKIDENT ('dbo.BangLuong', RESEED, 0);
    PRINT N'✅ Reset identity seeds.';
    PRINT N'';

    -- =====================================================
    -- PHẦN 1: DỮ LIỆU CƠ BẢN (từ duLieuMau.sql)
    -- =====================================================
    PRINT N'📋 PHẦN 1: THÊM DỮ LIỆU CƠ BẢN';
    PRINT N'================================';

    -- Đảm bảo tất cả IDENTITY_INSERT đều OFF trước khi bắt đầu (ignore errors)
    BEGIN TRY SET IDENTITY_INSERT dbo.NguoiDung OFF; END TRY BEGIN CATCH END CATCH;
    BEGIN TRY SET IDENTITY_INSERT dbo.NhanVien OFF; END TRY BEGIN CATCH END CATCH;
    BEGIN TRY SET IDENTITY_INSERT dbo.CaLam OFF; END TRY BEGIN CATCH END CATCH;
    BEGIN TRY SET IDENTITY_INSERT dbo.LichPhanCa OFF; END TRY BEGIN CATCH END CATCH;
    BEGIN TRY SET IDENTITY_INSERT dbo.ChamCong OFF; END TRY BEGIN CATCH END CATCH;
    BEGIN TRY SET IDENTITY_INSERT dbo.DonTu OFF; END TRY BEGIN CATCH END CATCH;
    BEGIN TRY SET IDENTITY_INSERT dbo.BangLuong OFF; END TRY BEGIN CATCH END CATCH;

    -- 1. BẢNG CALAM (CA LÀM VIỆC)
    PRINT N'1️⃣  Thêm dữ liệu CaLam...';
    SET IDENTITY_INSERT dbo.CaLam ON;
    INSERT INTO dbo.CaLam (MaCa, TenCa, GioBatDau, GioKetThuc, HeSoCa, MoTa, KichHoat) VALUES
    (1, N'Ca Sáng',      '06:00:00', '14:00:00', 1.0, N'Ca làm việc buổi sáng, giao ca lúc 14:00.', 1),
    (2, N'Ca Chiều',     '14:00:00', '22:00:00', 1.0, N'Ca làm việc buổi chiều, giao ca lúc 22:00.', 1),
    (3, N'Ca Đêm',       '22:00:00', '06:00:00', 1.5, N'Ca qua đêm, có phụ cấp ca đêm.', 1),
    (4, N'Ca Hành chính','08:00:00', '17:00:00', 1.0, N'Ca dành cho HR/Kế toán, giờ hành chính (overlap với ca sáng).', 1),
    (5, N'Ca Part-time Sáng','05:00:00', '06:00:00', 1.0, N'Ca làm thêm sáng sớm cho sinh viên (trước ca sáng).', 1),
    (6, N'Ca Part-time Tối','01:00:00', '05:00:00', 1.2, N'Ca làm thêm tối muộn cho sinh viên (sau ca đêm).', 1);
    
    DECLARE @CaLamRows INT = @@ROWCOUNT;
    SET IDENTITY_INSERT dbo.CaLam OFF;
    PRINT N'✅ Inserted ' + CAST(@CaLamRows AS NVARCHAR) + N' rows into CaLam.';

    -- 2. BẢNG NGUOIDUNG (TÀI KHOẢN)
    PRINT N'2️⃣  Thêm dữ liệu NguoiDung...';
    SET IDENTITY_INSERT dbo.NguoiDung ON;
    INSERT INTO dbo.NguoiDung (MaNguoiDung, TenDangNhap, MatKhauHash, VaiTro, KichHoat) VALUES
    (1, 'bichhang',    '1234', N'HR',       1),
    (2, 'vanan',       '1234', N'KeToan',   1),
    (3, 'minhtuan',    '1234', N'QuanLy',   1),
    (4, 'kimchi',      '1234', N'QuanLy',   1),
    (5, 'vandzung',    '1234', N'NhanVien', 1),
    (6, 'mylinh',      '1234', N'NhanVien', 1),
    (7, 'tienmanh',    '1234', N'NhanVien', 1),
    (8, 'anhthu',      '1234', N'NhanVien', 0);
    
    DECLARE @NguoiDungRows INT = @@ROWCOUNT;
    SET IDENTITY_INSERT dbo.NguoiDung OFF;
    PRINT N'✅ Inserted ' + CAST(@NguoiDungRows AS NVARCHAR) + N' rows into NguoiDung.';

    -- 3. BẢNG NHANVIEN
    PRINT N'3️⃣  Thêm dữ liệu NhanVien...';
    SET IDENTITY_INSERT dbo.NhanVien ON;
    INSERT INTO dbo.NhanVien (MaNV, MaNguoiDung, HoTen, NgaySinh, GioiTinh, DienThoai, Email, DiaChi, NgayVaoLam, TrangThai, PhongBan, ChucDanh, LuongCoBan) VALUES
    (1, 1, N'Nguyễn Thị Bích Hằng', '1985-03-15', N'Nu',  '0901234567', 'bichhang@company.com',  N'123 Đường ABC, Quận 1, TP.HCM',     '2020-01-15', N'DangLam', N'HR',       N'Trưởng phòng HR',    12000000),
    (2, 2, N'Trần Văn An',          '1988-07-22', N'Nam', '0912345678', 'vanan@company.com',     N'456 Đường DEF, Quận 3, TP.HCM',     '2020-02-01', N'DangLam', N'KeToan',   N'Kế toán trưởng',     11000000),
    (3, 3, N'Lê Minh Tuấn',         '1990-11-08', N'Nam', '0923456789', 'minhtuan@company.com',  N'789 Đường GHI, Quận 5, TP.HCM',     '2021-03-10', N'DangLam', N'BanHang',  N'Quản lý ca sáng',    9000000),
    (4, 4, N'Phạm Kim Chi',         '1992-05-18', N'Nu',  '0934567890', 'kimchi@company.com',    N'321 Đường JKL, Quận 7, TP.HCM',     '2021-06-15', N'DangLam', N'BanHang',  N'Quản lý ca chiều',   9000000),
    (5, 5, N'Hoàng Văn Dũng',       '1995-09-25', N'Nam', '0945678901', 'vandzung@company.com',  N'654 Đường MNO, Quận 10, TP.HCM',    '2022-01-20', N'DangLam', N'BanHang',  N'Thu ngân',           6000000),
    (6, 6, N'Võ Thị Mỹ Linh',       '1993-12-03', N'Nu',  '0956789012', 'mylinh@company.com',    N'987 Đường PQR, Quận Bình Thạnh',    '2022-04-01', N'DangLam', N'KhoHang',  N'Nhân viên kho',      5500000),
    (7, 7, N'Đỗ Tiến Mạnh',         '1991-08-14', N'Nam', '0967890123', 'tienmanh@company.com',  N'147 Đường STU, Quận Gò Vấp',        '2022-07-10', N'DangLam', N'BaoVe',    N'Bảo vệ',             5000000),
    (8, 8, N'Nguyễn Anh Thư',       '1996-02-28', N'Nu',  '0978901234', 'anhthu@company.com',    N'258 Đường VWX, Quận Tân Bình',      '2023-01-15', N'Nghi', N'BanHang', N'Thu ngân',           6000000);
    
    DECLARE @NhanVienRows INT = @@ROWCOUNT;
    SET IDENTITY_INSERT dbo.NhanVien OFF;
    PRINT N'✅ Inserted ' + CAST(@NhanVienRows AS NVARCHAR) + N' rows into NhanVien.';

    -- 4. DỮ LIỆU ĐƠN TỪ MẪU
    PRINT N'4️⃣  Thêm dữ liệu DonTu...';
    SET IDENTITY_INSERT dbo.DonTu ON;
    INSERT INTO dbo.DonTu (MaDon, MaNV, Loai, TuLuc, DenLuc, SoGio, LyDo, TrangThai, DuyetBoi) VALUES
    (1, 5, N'NGHI', '2024-08-15 08:00:00', '2024-08-15 17:00:00', 8.0, N'Đi khám bệnh định kỳ', N'DaDuyet', 3),
    (2, 6, N'OT',   '2024-08-20 17:00:00', '2024-08-20 20:00:00', 3.0, N'Hoàn thành báo cáo tồn kho', N'DaDuyet', 4),
    (3, 7, N'NGHI', '2024-08-25 22:00:00', '2024-08-26 06:00:00', 8.0, N'Có việc gia đình khẩn cấp', N'ChoDuyet', NULL);
    
    DECLARE @DonTuRows INT = @@ROWCOUNT;
    SET IDENTITY_INSERT dbo.DonTu OFF;
    PRINT N'✅ Inserted ' + CAST(@DonTuRows AS NVARCHAR) + N' rows into DonTu.';
    PRINT N'';

    -- =====================================================
    -- PHẦN 2: LỊCH PHÂN CA VÀ CHẤM CÔNG ĐẦY ĐỦ (từ duLieuMau_DayDu_7_9.sql)
    -- =====================================================
    PRINT N'📅 PHẦN 2: LỊCH PHÂN CA VÀ CHẤM CÔNG ĐẦY ĐỦ';
    PRINT N'==========================================';
    PRINT N'Đang tạo lịch phân ca từ 1/7 đến 21/9/2025...';
    
    -- 1. LỊCH PHÂN CA TỪ 1/7 ĐẾN 21/9/2025
    ;WITH 
    -- Phân ca cho từng nhân viên
    NhanVienCa AS (
        SELECT MaNV, 
            CASE 
                WHEN MaNV IN (1,2) THEN 4  -- HR/KeToan: Ca Hành chính (8-17h)
                WHEN MaNV IN (3,4) THEN 1  -- QuanLy: Ca Sáng (6-14h)
                WHEN MaNV IN (5,8) THEN 2  -- Thu ngân: Ca Chiều (14-22h)
                WHEN MaNV = 6 THEN 1       -- Kho hàng: Ca Sáng (6-14h)
                WHEN MaNV = 7 THEN 3       -- Bảo vệ: Ca Đêm (22-6h)
                ELSE 2 
            END AS MaCaDefault 
        FROM dbo.NhanVien WHERE TrangThai=N'DangLam' AND MaNV <= 8
    ),
    -- Tạo danh sách ngày từ 1/7 đến 21/9/2025
    dates AS (
        SELECT CAST('2025-07-01' AS DATE) AS Ngay
        UNION ALL
        SELECT DATEADD(day, 1, Ngay)
        FROM dates 
        WHERE Ngay < '2025-09-21'
    )
    INSERT INTO dbo.LichPhanCa (MaNV, NgayLam, MaCa, TrangThai)
    SELECT 
        nvc.MaNV,
        d.Ngay,
        -- Thay đổi ca một số ngày để đa dạng
        CASE 
            WHEN DATEPART(weekday, d.Ngay) = 1 THEN -- Chủ nhật: Ít người làm
                CASE WHEN nvc.MaNV IN (3,5,7) THEN nvc.MaCaDefault ELSE nvc.MaCaDefault END
            WHEN DATEPART(weekday, d.Ngay) = 7 THEN -- Thứ 7: Tăng cường
                CASE WHEN nvc.MaNV = 5 THEN 1 ELSE nvc.MaCaDefault END -- Thu ngân chuyển ca sáng
            WHEN d.Ngay IN ('2025-08-15', '2025-09-02') THEN -- Ngày nghỉ lễ
                CASE WHEN nvc.MaNV IN (3,5,7) THEN nvc.MaCaDefault ELSE nvc.MaCaDefault END
            ELSE nvc.MaCaDefault
        END,
        N'DuKien'
    FROM dates d
    CROSS JOIN NhanVienCa nvc
    WHERE 
        -- Chỉ tạo lịch cho những ngày nhân viên thực sự làm việc
        CASE 
            WHEN DATEPART(weekday, d.Ngay) = 1 THEN -- Chủ nhật: Chỉ một số người làm
                CASE WHEN nvc.MaNV IN (3,5,7) THEN 1 ELSE 0 END
            WHEN d.Ngay IN ('2025-08-15', '2025-09-02') THEN -- Ngày nghỉ lễ: Chỉ một số người làm
                CASE WHEN nvc.MaNV IN (3,5,7) THEN 1 ELSE 0 END
            ELSE 1 -- Các ngày khác: Tất cả làm việc
        END = 1
        AND nvc.MaCaDefault IS NOT NULL
    OPTION (MAXRECURSION 0);

    DECLARE @LichPhanCaRows INT = @@ROWCOUNT;
    PRINT N'✅ Inserted ' + CAST(@LichPhanCaRows AS NVARCHAR) + N' rows into LichPhanCa.';

    -- 2. CHẤM CÔNG TỪ 1/7 ĐẾN 21/9/2025
    PRINT N'⏰ Đang tạo dữ liệu chấm công...';
    
    ;WITH ChamCongData AS (
        SELECT 
            lpc.MaNV,
            lpc.NgayLam,
            lpc.MaCa,
            cl.GioBatDau,
            cl.GioKetThuc,
            cl.HeSoCa,
            -- Tạo giờ vào ngẫu nhiên (có thể đi trễ 0-15 phút)
            DATEADD(MINUTE, 
                CASE 
                    WHEN ABS(CHECKSUM(NEWID())) % 100 < 85 THEN ABS(CHECKSUM(NEWID())) % 16 -- 85% đi trễ 0-15 phút
                    WHEN ABS(CHECKSUM(NEWID())) % 100 < 95 THEN ABS(CHECKSUM(NEWID())) % 31 -- 10% đi trễ 16-30 phút  
                    ELSE ABS(CHECKSUM(NEWID())) % 61 -- 5% đi trễ 31-60 phút
                END,
                DATETIME2FROMPARTS(YEAR(lpc.NgayLam), MONTH(lpc.NgayLam), DAY(lpc.NgayLam), 
                                  DATEPART(HOUR, cl.GioBatDau), DATEPART(MINUTE, cl.GioBatDau), 0, 0, 0)
            ) AS GioVao,
            -- Tạo giờ ra ngẫu nhiên (có thể về sớm 0-10 phút hoặc muộn 0-30 phút)
            DATEADD(MINUTE, 
                CASE 
                    WHEN ABS(CHECKSUM(NEWID())) % 100 < 70 THEN -(ABS(CHECKSUM(NEWID())) % 11) -- 70% về sớm 0-10 phút
                    ELSE ABS(CHECKSUM(NEWID())) % 31 -- 30% về muộn 0-30 phút
                END,
                CASE 
                    WHEN cl.GioKetThuc < cl.GioBatDau THEN -- Ca qua đêm
                        DATETIME2FROMPARTS(YEAR(DATEADD(DAY, 1, lpc.NgayLam)), MONTH(DATEADD(DAY, 1, lpc.NgayLam)), DAY(DATEADD(DAY, 1, lpc.NgayLam)), 
                                          DATEPART(HOUR, cl.GioKetThuc), DATEPART(MINUTE, cl.GioKetThuc), 0, 0, 0)
                    ELSE
                        DATETIME2FROMPARTS(YEAR(lpc.NgayLam), MONTH(lpc.NgayLam), DAY(lpc.NgayLam), 
                                          DATEPART(HOUR, cl.GioKetThuc), DATEPART(MINUTE, cl.GioKetThuc), 0, 0, 0)
                END
            ) AS GioRa
        FROM dbo.LichPhanCa lpc
        JOIN dbo.CaLam cl ON lpc.MaCa = cl.MaCa
        WHERE lpc.NgayLam >= '2025-07-01' AND lpc.NgayLam <= '2025-09-21'
          AND lpc.TrangThai = N'DuKien'
          AND ABS(CHECKSUM(NEWID())) % 100 < 98 -- 98% có mặt, 2% vắng mặt
    )
    INSERT INTO dbo.ChamCong (MaNV, NgayLam, GioVao, GioRa, Khoa)
    SELECT MaNV, NgayLam, GioVao, GioRa, 0
    FROM ChamCongData;

    DECLARE @ChamCongRows INT = @@ROWCOUNT;
    PRINT N'✅ Inserted ' + CAST(@ChamCongRows AS NVARCHAR) + N' rows into ChamCong.';
    
    -- Bật lại trigger để tính toán dữ liệu chấm công
    PRINT N'🔄 Đang bật trigger và tính toán dữ liệu chấm công...';
    EXEC sys.sp_set_session_context @key=N'SkipTrigger', @value=0;
    
    UPDATE dbo.ChamCong 
    SET GioVao = GioVao -- Trigger sẽ tự động tính lại GioCong, DiTrePhut, VeSomPhut
    WHERE NgayLam >= '2025-07-01' AND NgayLam <= '2025-09-21' AND Khoa = 0;
    
    -- Tắt lại trigger cho các thao tác tiếp theo
    EXEC sys.sp_set_session_context @key=N'SkipTrigger', @value=1;
    
    -- Kiểm tra dữ liệu sau khi trigger chạy
    DECLARE @NullGioCong INT = (SELECT COUNT(*) FROM dbo.ChamCong WHERE NgayLam >= '2025-07-01' AND GioCong IS NULL);
    IF @NullGioCong > 0
        PRINT N'⚠️  Cảnh báo: Có ' + CAST(@NullGioCong AS NVARCHAR) + N' bản ghi chấm công chưa được tính GioCong.';
    ELSE
        PRINT N'✅ Tất cả dữ liệu chấm công đã được tính toán đầy đủ.';
    
    PRINT N'';

    -- =====================================================
    -- PHẦN 3: LỊCH LÀM VIỆC HÔM NAY (từ test_lich_hom_nay.sql)
    -- =====================================================
    PRINT N'🗓️  PHẦN 3: LỊCH LÀM VIỆC HÔM NAY';
    PRINT N'==============================';
    
    DECLARE @NgayHomNay DATE = CAST(GETDATE() AS DATE);
    PRINT N'Thêm lịch làm việc cho ngày: ' + CAST(@NgayHomNay AS NVARCHAR(10));

    -- Xóa lịch hôm nay nếu đã có (tránh trùng lặp)
    DELETE FROM LichPhanCa WHERE NgayLam = @NgayHomNay;

    -- Thêm lịch mới cho từng nhân viên hôm nay
    INSERT INTO LichPhanCa (MaNV, NgayLam, MaCa, TrangThai) VALUES
    (1, @NgayHomNay, 4, N'DuKien'),           -- Bích Hằng - HR - Ca hành chính
    (2, @NgayHomNay, 4, N'DuKien'),           -- Văn An - Kế toán - Ca hành chính
    (3, @NgayHomNay, 1, N'DuKien'),           -- Minh Tuấn - Quản lý - Ca sáng
    (4, @NgayHomNay, 2, N'DuKien'),           -- Kim Chi - Quản lý - Ca chiều
    (5, @NgayHomNay, 2, N'DuKien'),           -- Văn Dũng - Thu ngân - Ca chiều
    (6, @NgayHomNay, 1, N'DuKien'),           -- Mỹ Linh - Kho - Ca sáng
    (7, @NgayHomNay, 3, N'DuKien');           -- Tiến Mạnh - Bảo vệ - Ca đêm

    DECLARE @LichHomNayRows INT = @@ROWCOUNT;
    PRINT N'✅ Inserted ' + CAST(@LichHomNayRows AS NVARCHAR) + N' rows for today schedule.';
    PRINT N'';

    -- =====================================================
    -- PHẦN 4: TẠO BẢNG LƯƠNG MẪU
    -- =====================================================
    PRINT N'💰 PHẦN 4: TẠO BẢNG LƯƠNG MẪU';
    PRINT N'============================';
    
    -- Khóa công trước khi chạy bảng lương
    PRINT N'Khóa công tháng 7/2025...';
    EXEC sp_KhoaCongThang @Nam = 2025, @Thang = 7;
    
    PRINT N'Khóa công tháng 8/2025...';
    EXEC sp_KhoaCongThang @Nam = 2025, @Thang = 8;
    
    PRINT N'Khóa công tháng 9/2025...';
    EXEC sp_KhoaCongThang @Nam = 2025, @Thang = 9;
    
    -- Chạy bảng lương cho tháng 7, 8, 9/2025
    PRINT N'Đang chạy bảng lương tháng 7/2025...';
    EXEC sp_ChayBangLuong @Nam = 2025, @Thang = 7;
    
    PRINT N'Đang chạy bảng lương tháng 8/2025...';
    EXEC sp_ChayBangLuong @Nam = 2025, @Thang = 8;
    
    PRINT N'Đang chạy bảng lương tháng 9/2025...';
    EXEC sp_ChayBangLuong @Nam = 2025, @Thang = 9;
    
    SELECT @NguoiDungRows = COUNT(*) FROM dbo.BangLuong;
    PRINT N'✅ Created ' + CAST(@NguoiDungRows AS NVARCHAR) + N' payroll records.';
    
    -- Kiểm tra chi tiết dữ liệu bảng lương
    DECLARE @ZeroSalary INT = (SELECT COUNT(*) FROM dbo.BangLuong WHERE ThucLanh = 0 OR ThucLanh IS NULL);
    IF @ZeroSalary > 0
        PRINT N'⚠️  Cảnh báo: Có ' + CAST(@ZeroSalary AS NVARCHAR) + N' bản ghi bảng lương có ThucLanh = 0 hoặc NULL.';
    ELSE
        PRINT N'✅ Tất cả bản ghi bảng lương đều có ThucLanh > 0.';
    
    -- Hiển thị mẫu dữ liệu bảng lương
    PRINT N'📋 Mẫu dữ liệu bảng lương tháng 7/2025:';
    SELECT TOP 3 
        bl.MaNV, nv.HoTen, bl.TongGioCong, bl.GioOT, bl.ThucLanh
    FROM dbo.BangLuong bl
    JOIN dbo.NhanVien nv ON bl.MaNV = nv.MaNV
    WHERE bl.Nam = 2025 AND bl.Thang = 7
    ORDER BY bl.MaNV;
    
    PRINT N'';

    -- Bật lại trigger
    EXEC sys.sp_set_session_context @key=N'SkipTrigger', @value=0;
    PRINT N'✅ Set SkipTrigger to 0 (enabled triggers).';
    PRINT N'';

    -- =====================================================
    -- KIỂM TRA KẾT QUẢ CUỐI CÙNG
    -- =====================================================
    PRINT N'📊 KIỂM TRA KẾT QUẢ CUỐI CÙNG';
    PRINT N'============================';
    
    SELECT 
        'CaLam' as TableName, 
        COUNT(*) as RecordCount,
        MIN(CAST(MaCa AS NVARCHAR) + ' - ' + TenCa) as SampleData
    FROM dbo.CaLam
    UNION ALL
    SELECT 
        'NguoiDung' as TableName, 
        COUNT(*) as RecordCount,
        MIN(TenDangNhap + ' (' + VaiTro + ')') as SampleData
    FROM dbo.NguoiDung
    UNION ALL
    SELECT 
        'NhanVien' as TableName, 
        COUNT(*) as RecordCount,
        MIN(HoTen + ' - ' + PhongBan) as SampleData
    FROM dbo.NhanVien
    UNION ALL
    SELECT 
        'LichPhanCa' as TableName, 
        COUNT(*) as RecordCount,
        MIN(CAST(NgayLam AS NVARCHAR) + ' - MaNV:' + CAST(MaNV AS NVARCHAR)) as SampleData
    FROM dbo.LichPhanCa
    UNION ALL
    SELECT 
        'ChamCong' as TableName, 
        COUNT(*) as RecordCount,
        MIN(CAST(NgayLam AS NVARCHAR) + ' - MaNV:' + CAST(MaNV AS NVARCHAR)) as SampleData
    FROM dbo.ChamCong
    UNION ALL
    SELECT 
        'DonTu' as TableName, 
        COUNT(*) as RecordCount,
        MIN(Loai + ' - ' + TrangThai) as SampleData
    FROM dbo.DonTu
    UNION ALL
    SELECT 
        'BangLuong' as TableName, 
        COUNT(*) as RecordCount,
        MIN(CAST(Nam AS NVARCHAR) + '/' + CAST(Thang AS NVARCHAR) + ' - MaNV:' + CAST(MaNV AS NVARCHAR)) as SampleData
    FROM dbo.BangLuong;

    COMMIT TRANSACTION;
    
    PRINT N'';
    PRINT N'🎉 HOÀN TẤT CHẠY DỮ LIỆU MẪU TỔNG HỢP THÀNH CÔNG!';
    PRINT N'';
    PRINT N'📋 Tóm tắt dữ liệu đã tạo:';
    PRINT N'• CaLam: 6 ca làm việc (bao gồm ca qua đêm, hành chính, part-time)';
    PRINT N'• NguoiDung: 8 tài khoản (HR, KeToan, QuanLy, NhanVien)';
    PRINT N'• NhanVien: 8 nhân viên (7 đang làm, 1 nghỉ việc)';
    PRINT N'• LichPhanCa: Lịch từ 1/7-21/9/2025 + lịch hôm nay';
    PRINT N'• ChamCong: Dữ liệu chấm công với giờ vào/ra thực tế';
    PRINT N'• DonTu: 3 đơn từ mẫu (nghỉ phép, OT)';
    PRINT N'• BangLuong: Bảng lương tháng 7,8,9/2025';
    PRINT N'';
    PRINT N'✅ Database sẵn sàng để sử dụng!';

END TRY
BEGIN CATCH
    ROLLBACK TRANSACTION;
    
    PRINT N'❌ LỖI KHI CHẠY DỮ LIỆU MẪU:';
    PRINT N'Error Number: ' + CAST(ERROR_NUMBER() AS NVARCHAR);
    PRINT N'Error Message: ' + ERROR_MESSAGE();
    PRINT N'Error Line: ' + CAST(ERROR_LINE() AS NVARCHAR);
    
    -- Bật lại trigger trong trường hợp lỗi
    EXEC sys.sp_set_session_context @key=N'SkipTrigger', @value=0;
    
    THROW;
END CATCH;
