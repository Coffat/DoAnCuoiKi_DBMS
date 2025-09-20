/* =========================================================
   DỮ LIỆU MẪU CHO HỆ THỐNG QUẢN LÝ NHÂN SỰ SIÊU THỊ MINI
   
   CẬP NHẬT: Đã xóa hoàn toàn RLS (Row Level Security)
   - Tất cả user có thể xem dữ liệu theo role permissions
   - Không còn hạn chế theo MaNV cá nhân
   - Sử dụng RBAC (Role-Based Access Control) thuần túy
   ========================================================= */

USE QLNhanSuSieuThiMini;
GO

BEGIN TRANSACTION; -- Bắt đầu transaction để đảm bảo nhất quán

BEGIN TRY

    -- Tạm tắt trigger để insert dữ liệu mẫu
    EXEC sys.sp_set_session_context @key=N'SkipTrigger', @value=1;
    PRINT N'Set SkipTrigger to 1 successful.';

    -- Xóa dữ liệu cũ để tránh trùng lặp khóa chính/duy nhất
    DELETE FROM dbo.BangLuong;
    DELETE FROM dbo.DonTu;
    DELETE FROM dbo.ChamCong;
    DELETE FROM dbo.LichPhanCa;
    DELETE FROM dbo.NhanVien;
    DELETE FROM dbo.NguoiDung;
    DELETE FROM dbo.CaLam;
    PRINT N'Deleted existing data from all tables. Rows affected: ' + CAST(@@ROWCOUNT AS NVARCHAR);

    -- Reset identity seed cho các bảng
    DBCC CHECKIDENT ('dbo.NguoiDung', RESEED, 0);
    DBCC CHECKIDENT ('dbo.NhanVien', RESEED, 0);
    DBCC CHECKIDENT ('dbo.CaLam', RESEED, 0);
    DBCC CHECKIDENT ('dbo.LichPhanCa', RESEED, 0);
    DBCC CHECKIDENT ('dbo.ChamCong', RESEED, 0);
    DBCC CHECKIDENT ('dbo.DonTu', RESEED, 0);
    DBCC CHECKIDENT ('dbo.BangLuong', RESEED, 0);
    PRINT N'Reset identity seeds.';

    -- 1. BẢNG CALAM (CA LÀM VIỆC) - ĐIỀU CHỈNH THỜI GIAN TRÁNH OVERLAP
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
    PRINT N'Inserted into CaLam. Rows affected: ' + CAST(@CaLamRows AS NVARCHAR);
    
    -- Kiểm tra ngay sau khi insert
    SELECT @CaLamRows = COUNT(*) FROM dbo.CaLam;
    PRINT N'CaLam count after insert: ' + CAST(@CaLamRows AS NVARCHAR);

    -- 2. BẢNG NGUOIDUNG (TÀI KHOẢN)
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
    PRINT N'Inserted into NguoiDung. Rows affected: ' + CAST(@NguoiDungRows AS NVARCHAR);

    -- 3. BẢNG NHANVIEN (NHÂN VIÊN)
    SET IDENTITY_INSERT dbo.NhanVien ON;
    INSERT INTO dbo.NhanVien (MaNV, MaNguoiDung, HoTen, NgaySinh, GioiTinh, DienThoai, Email, DiaChi, NgayVaoLam, TrangThai, PhongBan, ChucDanh, LuongCoBan) VALUES
    (1, 1, N'Nguyễn Thị Bích Hằng', '1990-05-15', N'Nu', '0912345678', 'hang.hr@sieuthimini.com', N'123 Võ Văn Tần, Q.3, TP.HCM', '2022-01-10', N'DangLam', N'Phòng Nhân sự', N'Trưởng phòng nhân sự', 20000000),
    (2, 2, N'Trần Văn An',          '1992-08-20', N'Nam', '0987654321', 'an.ketoan@sieuthimini.com', N'456 Lê Lợi, Q.1, TP.HCM', '2022-03-15', N'DangLam', N'Phòng Kế toán', N'Kế toán tổng hợp', 18000000),
    (3, 3, N'Lê Minh Tuấn',         '1988-11-02', N'Nam', '0905112233', 'tuan.ql@sieuthimini.com', N'789 Nguyễn Trãi, Q.5, TP.HCM', '2021-07-01', N'DangLam', N'Ban Quản lý', N'Cửa hàng trưởng ca A', 16000000),
    (4, 4, N'Phạm Thị Kim Chi',     '1995-02-10', N'Nu', '0934556677', 'chi.ql@sieuthimini.com', N'101 Hai Bà Trưng, Q.1, TP.HCM', '2023-01-20', N'DangLam', N'Ban Quản lý', N'Cửa hàng trưởng ca B', 16000000),
    (5, 5, N'Hoàng Văn Dũng',       '2000-04-25', N'Nam', '0918273645', 'dung.nv@sieuthimini.com', N'234 CMT8, Q.Tân Bình, TP.HCM', '2024-05-10', N'DangLam', N'Quầy Thanh Toán', N'Nhân viên thu ngân', 7500000),
    (6, 6, N'Đặng Mỹ Linh',         '2002-09-12', N'Nu', '0977889900', 'linh.nv@sieuthimini.com', N'567 Trường Chinh, Q.12, TP.HCM', '2024-06-01', N'DangLam', N'Kho Hàng', N'Nhân viên kho', 8000000),
    (7, 7, N'Vũ Tiến Mạnh',         '1998-07-30', N'Nam', '0965432109', 'manh.nv@sieuthimini.com', N'890 Quang Trung, Q.Gò Vấp, TP.HCM', '2023-11-05', N'DangLam', N'An Ninh', N'Nhân viên bảo vệ', 8500000),
    (8, NULL, N'Bùi Thị Thảo',        '2001-01-18', N'Nu', '0909123456', 'thao.nv@sieuthimini.com', N'321 Phan Đình Phùng, Q.Phú Nhuận', '2024-02-15', N'DangLam', N'Quầy Thanh Toán', N'Nhân viên thu ngân', 7500000),
    (9, NULL, N'Lý Thành Trung',       '2003-03-03', N'Nam', '0945678901', 'trung.nv@sieuthimini.com', N'654 Âu Cơ, Q.Tân Phú, TP.HCM', '2024-08-01', N'DangLam', N'Kho Hàng', N'Nhân viên kho (Part-time)', 5000000),
    (10, 8, N'Mai Anh Thư',        '1999-12-25', N'Nu', '0911223344', 'thu.cuunv@sieuthimini.com', N'111 Lý Thường Kiệt, Q.10, TP.HCM', '2023-09-01', N'Nghi', N'Quầy Thanh Toán', N'Nhân viên thu ngân', 7500000);
    
    DECLARE @NhanVienRows INT = @@ROWCOUNT;
    SET IDENTITY_INSERT dbo.NhanVien OFF;
    PRINT N'Inserted into NhanVien. Rows affected: ' + CAST(@NhanVienRows AS NVARCHAR);

    -- 4. BẢNG LICHPHANCA (LỊCH PHÂN CA) - PHÂN CA HỢP LÝ CHO TỪNG NHÂN VIÊN
    ;WITH NhanVienCa AS (
        SELECT MaNV, 
            CASE 
                WHEN MaNV IN (1,2) THEN 4  -- HR/KeToan: Ca Hành chính
                WHEN MaNV IN (3,4) THEN 1  -- QuanLy: Ca Sáng  
                WHEN MaNV IN (5,8) THEN 2  -- Thu ngân: Ca Chiều
                WHEN MaNV = 6 THEN 1       -- Kho hàng: Ca Sáng
                WHEN MaNV = 7 THEN 3       -- Bảo vệ: Ca Đêm
                WHEN MaNV = 9 THEN 5       -- Part-time: Ca Part-time Sáng
                ELSE 2 
            END AS MaCaDefault 
        FROM dbo.NhanVien WHERE TrangThai=N'DangLam'
    ),
    dates AS (
        SELECT CAST('2025-08-01' AS DATE) AS Ngay
        UNION ALL
        SELECT DATEADD(DAY, 1, Ngay) FROM dates WHERE Ngay < '2025-09-30'
    )
    INSERT INTO dbo.LichPhanCa (MaNV, NgayLam, MaCa, TrangThai)
    SELECT
        nv.MaNV,
        d.Ngay,
        nv.MaCaDefault,
        CASE WHEN MONTH(d.Ngay) = 8 THEN N'Khoa' ELSE N'DuKien' END AS TrangThai
    FROM NhanVienCa nv
    CROSS JOIN dates d
    WHERE (d.Ngay < '2025-10-01') AND (DATEPART(weekday, d.Ngay) + nv.MaNV) % 7 <> 1 -- Tạo lịch không đều để thực tế hơn
    OPTION (MAXRECURSION 100);
    
    DECLARE @LichPhanCaRows INT = @@ROWCOUNT;
    PRINT N'Inserted into LichPhanCa. Rows affected: ' + CAST(@LichPhanCaRows AS NVARCHAR);

    -- 5. BẢNG CHAMCONG (CHẤM CÔNG)
    INSERT INTO dbo.ChamCong (MaNV, NgayLam, GioVao, GioRa, Khoa)
    SELECT
        lpc.MaNV,
        lpc.NgayLam,
        DATEADD(MINUTE, (ABS(CHECKSUM(NEWID())) % 20) - 10, DATETIMEFROMPARTS(YEAR(lpc.NgayLam), MONTH(lpc.NgayLam), DAY(lpc.NgayLam), DATEPART(hour, cl.GioBatDau), DATEPART(minute, cl.GioBatDau), 0, 0)),
        DATEADD(MINUTE, (ABS(CHECKSUM(NEWID())) % 20) - 10,
            DATEADD(DAY, IIF(cl.GioKetThuc < cl.GioBatDau, 1, 0),
                DATETIMEFROMPARTS(YEAR(lpc.NgayLam), MONTH(lpc.NgayLam), DAY(lpc.NgayLam), DATEPART(hour, cl.GioKetThuc), DATEPART(minute, cl.GioKetThuc), 0, 0)
            )
        ),
        1 AS Khoa
    FROM dbo.LichPhanCa lpc
    JOIN dbo.CaLam cl ON cl.MaCa = lpc.MaCa
    WHERE lpc.TrangThai = N'Khoa' AND lpc.NgayLam BETWEEN '2025-08-01' AND '2025-08-31';
    
    DECLARE @ChamCongRows INT = @@ROWCOUNT;
    PRINT N'Inserted into ChamCong. Rows affected: ' + CAST(@ChamCongRows AS NVARCHAR);

    -- 6. BẢNG DONTU (ĐƠN TỪ)
    SET IDENTITY_INSERT dbo.DonTu ON;
    INSERT INTO dbo.DonTu (MaDon, MaNV, Loai, TuLuc, DenLuc, SoGio, LyDo, TrangThai, DuyetBoi) VALUES
    (1, 5, N'NGHI', '2025-08-10 08:00:00', '2025-08-10 17:00:00', 8.0, N'Việc gia đình', N'DaDuyet', 3),
    (2, 6, N'NGHI', '2025-09-22 14:00:00', '2025-09-23 22:00:00', 16.0, N'Nghỉ ốm', N'ChoDuyet', NULL),
    (3, 7, N'OT', '2025-08-15 22:00:00', '2025-08-16 02:00:00', 4.0, N'Hỗ trợ kiểm kho đột xuất', N'DaDuyet', 3),
    (4, 8, N'NGHI', '2025-08-20 08:00:00', '2025-08-20 12:00:00', 4.0, N'Đi khám bệnh', N'TuChoi', 4),
    (5, 2, N'OT', '2025-08-31 17:00:00', '2025-09-01 01:00:00', 8.0, N'Hoàn thành báo cáo cuối tháng', N'DaDuyet', 1);
    
    DECLARE @DonTuRows INT = @@ROWCOUNT;
    SET IDENTITY_INSERT dbo.DonTu OFF;
    PRINT N'Inserted into DonTu. Rows affected: ' + CAST(@DonTuRows AS NVARCHAR);

    -- 7. BẢNG BANGLUONG (Dữ liệu lương tháng 7 đã đóng và tháng 8 đang mở)
    INSERT INTO dbo.BangLuong (Nam, Thang, MaNV, LuongCoBan, TongGioCong, GioOT, PhuCap, KhauTru, ThueBH, ThucLanh, TrangThai) VALUES
    -- Tháng 7/2025 - Đã đóng
    (2025, 7, 1, 20000000.00, 176.00, 5.00, 500000.00, 0.00, 2100000.00, 19176923.08, N'Dong'),
    (2025, 7, 2, 18000000.00, 168.00, 0.00, 0.00, 0.00, 1890000.00, 16110000.00, N'Dong'),
    (2025, 7, 3, 16000000.00, 184.00, 10.00, 0.00, 200000.00, 1680000.00, 15276923.08, N'Dong'),
    (2025, 7, 4, 16000000.00, 160.00, 0.00, 0.00, 0.00, 1680000.00, 14320000.00, N'Dong'),
    (2025, 7, 5, 7500000.00, 152.00, 0.00, 100000.00, 50000.00, 787500.00, 6762500.00, N'Dong'),
    (2025, 7, 6, 8000000.00, 176.00, 0.00, 0.00, 0.00, 840000.00, 7160000.00, N'Dong'),
    (2025, 7, 7, 8500000.00, 184.00, 8.00, 200000.00, 0.00, 892500.00, 8376923.08, N'Dong'),
    -- Tháng 8/2025 - Đang mở (chưa tính lương)
    (2025, 8, 1, 20000000.00, 0.00, 0.00, 0.00, 0.00, 0.00, 0.00, N'Mo'),
    (2025, 8, 2, 18000000.00, 0.00, 0.00, 0.00, 0.00, 0.00, 0.00, N'Mo'),
    (2025, 8, 3, 16000000.00, 0.00, 0.00, 0.00, 0.00, 0.00, 0.00, N'Mo'),
    (2025, 8, 4, 16000000.00, 0.00, 0.00, 0.00, 0.00, 0.00, 0.00, N'Mo'),
    (2025, 8, 5, 7500000.00, 0.00, 0.00, 0.00, 0.00, 0.00, 0.00, N'Mo'),
    (2025, 8, 6, 8000000.00, 0.00, 0.00, 0.00, 0.00, 0.00, 0.00, N'Mo'),
    (2025, 8, 7, 8500000.00, 0.00, 0.00, 0.00, 0.00, 0.00, 0.00, N'Mo'),
    (2025, 8, 8, 7500000.00, 0.00, 0.00, 0.00, 0.00, 0.00, 0.00, N'Mo'),
    (2025, 8, 9, 5000000.00, 0.00, 0.00, 0.00, 0.00, 0.00, 0.00, N'Mo');
    
    DECLARE @BangLuongRows INT = @@ROWCOUNT;
    PRINT N'Inserted into BangLuong. Rows affected: ' + CAST(@BangLuongRows AS NVARCHAR);

    -- Kích hoạt lại trigger
    EXEC sys.sp_set_session_context @key=N'SkipTrigger', @value=0;
    PRINT N'Set SkipTrigger to 0 successful.';

    -- Cập nhật lại chấm công để chạy trigger tính công (GioCong, DiTrePhut, VeSomPhut)
    PRINT N'Đang cập nhật lại dữ liệu chấm công cho tháng 8 để trigger tính toán...';
    UPDATE dbo.ChamCong SET GioVao = GioVao WHERE NgayLam BETWEEN '2025-08-01' AND '2025-08-31';
    PRINT N'Updated ChamCong for trigger calculation. Rows affected: ' + CAST(@@ROWCOUNT AS NVARCHAR);
    
    -- Hiển thị một số mẫu dữ liệu chấm công đã tính
    PRINT N'=== MẪU DỮ LIỆU CHẤM CÔNG ĐÃ TÍNH ===';
    SELECT TOP 5 
        nv.HoTen,
        cc.NgayLam,
        cl.TenCa,
        cc.GioVao,
        cc.GioRa,
        cc.GioCong,
        cc.DiTrePhut,
        cc.VeSomPhut
    FROM dbo.ChamCong cc
    JOIN dbo.NhanVien nv ON nv.MaNV = cc.MaNV
    JOIN dbo.LichPhanCa lpc ON lpc.MaNV = cc.MaNV AND lpc.NgayLam = cc.NgayLam
    JOIN dbo.CaLam cl ON cl.MaCa = lpc.MaCa
    WHERE cc.NgayLam BETWEEN '2025-08-01' AND '2025-08-05'
    ORDER BY cc.NgayLam, nv.HoTen;

    PRINT N'Dữ liệu mẫu đã được chuẩn bị thành công (KHÔNG CÒN RLS - Tất cả user có thể xem tất cả dữ liệu theo role).';

    -- Kiểm tra dữ liệu trong các bảng
    PRINT N'=== KIỂM TRA DỮ LIỆU TRONG CÁC BẢNG ===';
    
    DECLARE @Count INT;
    
    SELECT @Count = COUNT(*) FROM dbo.CaLam;
    PRINT N'CaLam: ' + CAST(@Count AS NVARCHAR) + N' records';
    
    SELECT @Count = COUNT(*) FROM dbo.NguoiDung;
    PRINT N'NguoiDung: ' + CAST(@Count AS NVARCHAR) + N' records';
    
    SELECT @Count = COUNT(*) FROM dbo.NhanVien;
    PRINT N'NhanVien: ' + CAST(@Count AS NVARCHAR) + N' records';
    
    SELECT @Count = COUNT(*) FROM dbo.LichPhanCa;
    PRINT N'LichPhanCa: ' + CAST(@Count AS NVARCHAR) + N' records';
    
    SELECT @Count = COUNT(*) FROM dbo.ChamCong;
    PRINT N'ChamCong: ' + CAST(@Count AS NVARCHAR) + N' records';
    
    SELECT @Count = COUNT(*) FROM dbo.DonTu;
    PRINT N'DonTu: ' + CAST(@Count AS NVARCHAR) + N' records';
    
    SELECT @Count = COUNT(*) FROM dbo.BangLuong;
    PRINT N'BangLuong: ' + CAST(@Count AS NVARCHAR) + N' records';

    -- Commit transaction nếu không có lỗi
    COMMIT TRANSACTION;
    PRINT N'----------------------------------------------------------';
    PRINT N'          GIAO DỊCH THÀNH CÔNG! DỮ LIỆU ĐÃ ĐƯỢC LƯU.';
    PRINT N'----------------------------------------------------------';
    PRINT N'';
    PRINT N'=== THÔNG TIN TÀI KHOẢN ĐĂNG NHẬP ===';
    PRINT N'HR:       bichhang / 1234';
    PRINT N'KeToan:   vanan / 1234';
    PRINT N'QuanLy:   minhtuan / 1234 hoặc kimchi / 1234';
    PRINT N'NhanVien: vandzung / 1234, mylinh / 1234, tienmanh / 1234';
    PRINT N'';
    PRINT N'=== TÌNH TRẠNG DỮ LIỆU ===';
    PRINT N'- Tháng 7/2025: Đã khóa công và đóng lương';
    PRINT N'- Tháng 8/2025: Đã khóa công, chưa tính lương (Mo)';
    PRINT N'- Tháng 9/2025: Lịch phân ca dự kiến, chưa khóa';
    PRINT N'';
    PRINT N'=== LƯU Ý QUAN TRỌNG ===';
    PRINT N'- ĐÃ XÓA HOÀN TOÀN RLS: Tất cả user xem được tất cả dữ liệu';
    PRINT N'- Phân quyền theo ROLE: HR > QuanLy > KeToan > NhanVien';
    PRINT N'- Trigger tự động tính: GioCong, DiTrePhut, VeSomPhut';
    PRINT N'- Hỗ trợ ca qua đêm: Ca Đêm (22:00-06:00)';
    PRINT N'- Cho phép overlap hợp lý: Ca Hành chính, Part-time';

END TRY
BEGIN CATCH
    -- Nếu có lỗi, rollback transaction
    IF @@TRANCOUNT > 0
        ROLLBACK TRANSACTION;
    PRINT N'Đã có lỗi xảy ra. Toàn bộ giao dịch đã được hủy bỏ.';
    THROW;
END CATCH;
GO
