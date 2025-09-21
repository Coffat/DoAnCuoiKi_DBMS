/* =========================================================
   DỮ LIỆU MẪU ĐẦY ĐỦ - THÁNG 7 ĐẾN 21/9/2025
   Hệ thống Quản lý Nhân sự Siêu thị Mini
   ========================================================= */

USE QLNhanSuSieuThiMini;
GO

BEGIN TRANSACTION;

BEGIN TRY
    -- Tắt trigger để insert dữ liệu mẫu
    EXEC sys.sp_set_session_context @key=N'SkipTrigger', @value=1;
    
    -- Xóa dữ liệu cũ
    DELETE FROM dbo.BangLuong;
    DELETE FROM dbo.DonTu;
    DELETE FROM dbo.ChamCong;
    DELETE FROM dbo.LichPhanCa;
    
    -- Reset identity
    DBCC CHECKIDENT ('dbo.LichPhanCa', RESEED, 0);
    DBCC CHECKIDENT ('dbo.ChamCong', RESEED, 0);
    DBCC CHECKIDENT ('dbo.DonTu', RESEED, 0);
    DBCC CHECKIDENT ('dbo.BangLuong', RESEED, 0);
    
    PRINT N'Đã xóa dữ liệu cũ và reset identity.';

    -- 1. LỊCH PHÂN CA TỪ 1/7 ĐẾN 21/9/2025
    PRINT N'Đang tạo lịch phân ca từ 1/7 đến 21/9/2025...';
    
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
                WHEN MaNV = 9 THEN 5       -- Part-time: Ca Part-time Sáng (5-6h)
                ELSE 2 
            END AS MaCaDefault 
        FROM dbo.NhanVien WHERE TrangThai=N'DangLam' AND MaNV <= 9
    ),
    -- Tạo danh sách ngày từ 1/7 đến 21/9/2025
    dates AS (
        SELECT CAST('2025-07-01' AS DATE) AS Ngay
        UNION ALL
        SELECT DATEADD(DAY, 1, Ngay) FROM dates WHERE Ngay < '2025-09-21'
    )
    INSERT INTO dbo.LichPhanCa (MaNV, NgayLam, MaCa, TrangThai)
    SELECT
        nv.MaNV,
        d.Ngay,
        -- Thay đổi ca một số ngày để thực tế hơn
        CASE 
            WHEN DATEPART(weekday, d.Ngay) = 1 THEN nv.MaCaDefault -- Chủ nhật: ca bình thường
            WHEN DATEPART(weekday, d.Ngay) = 7 AND nv.MaNV IN (5,8) THEN 1 -- Thứ 7: thu ngân chuyển ca sáng
            WHEN d.Ngay IN ('2025-07-15', '2025-08-15', '2025-09-15') AND nv.MaNV = 6 THEN 2 -- Ngày 15: kho chuyển ca chiều
            ELSE nv.MaCaDefault
        END,
        -- Trạng thái theo tháng
        CASE 
            WHEN MONTH(d.Ngay) = 7 THEN N'Khoa'     -- Tháng 7: đã khóa
            WHEN MONTH(d.Ngay) = 8 THEN N'Khoa'     -- Tháng 8: đã khóa
            ELSE N'DuKien'                           -- Tháng 9: dự kiến
        END AS TrangThai
    FROM NhanVienCa nv
    CROSS JOIN dates d
    WHERE 
        -- Loại bỏ một số ngày nghỉ không đều để thực tế hơn
        NOT (DATEPART(weekday, d.Ngay) = 1 AND (nv.MaNV + DAY(d.Ngay)) % 4 = 0) -- Một số chủ nhật nghỉ
        AND NOT (d.Ngay IN ('2025-07-20', '2025-08-10', '2025-09-02') AND nv.MaNV IN (5,8)) -- Một số ngày nghỉ cá nhân
    OPTION (MAXRECURSION 100);
    
    DECLARE @LichRows INT = @@ROWCOUNT;
    PRINT N'Đã tạo ' + CAST(@LichRows AS NVARCHAR) + N' bản ghi lịch phân ca.';

    -- 2. CHẤM CÔNG CHO THÁNG 7 VÀ 8 (ĐÃ KHÓA)
    PRINT N'Đang tạo dữ liệu chấm công tháng 7-8...';
    
    INSERT INTO dbo.ChamCong (MaNV, NgayLam, GioVao, GioRa, Khoa)
    SELECT
        lpc.MaNV,
        lpc.NgayLam,
        -- Giờ vào: có thể đi trễ 0-15 phút
        DATEADD(MINUTE, 
            (ABS(CHECKSUM(NEWID())) % 16), -- 0-15 phút trễ
            DATETIMEFROMPARTS(YEAR(lpc.NgayLam), MONTH(lpc.NgayLam), DAY(lpc.NgayLam), 
                DATEPART(hour, cl.GioBatDau), DATEPART(minute, cl.GioBatDau), 0, 0)
        ),
        -- Giờ ra: có thể về sớm/muộn 0-20 phút
        DATEADD(MINUTE, 
            (ABS(CHECKSUM(NEWID())) % 41) - 20, -- -20 đến +20 phút
            DATEADD(DAY, IIF(cl.GioKetThuc < cl.GioBatDau, 1, 0), -- Xử lý ca qua đêm
                DATETIMEFROMPARTS(YEAR(lpc.NgayLam), MONTH(lpc.NgayLam), DAY(lpc.NgayLam), 
                    DATEPART(hour, cl.GioKetThuc), DATEPART(minute, cl.GioKetThuc), 0, 0)
            )
        ),
        1 AS Khoa -- Đã khóa
    FROM dbo.LichPhanCa lpc
    JOIN dbo.CaLam cl ON cl.MaCa = lpc.MaCa
    WHERE lpc.TrangThai = N'Khoa' 
      AND lpc.NgayLam BETWEEN '2025-07-01' AND '2025-08-31';
    
    DECLARE @ChamCongRows INT = @@ROWCOUNT;
    PRINT N'Đã tạo ' + CAST(@ChamCongRows AS NVARCHAR) + N' bản ghi chấm công.';

    -- 3. CHẤM CÔNG CHO THÁNG 9 (CHƯA KHÓA) - CHỈ ĐẾN NGÀY 21
    PRINT N'Đang tạo dữ liệu chấm công tháng 9 (đến ngày 21)...';
    
    INSERT INTO dbo.ChamCong (MaNV, NgayLam, GioVao, GioRa, Khoa)
    SELECT
        lpc.MaNV,
        lpc.NgayLam,
        -- Giờ vào cho tháng 9
        DATEADD(MINUTE, 
            (ABS(CHECKSUM(NEWID())) % 11), -- 0-10 phút trễ (tốt hơn tháng trước)
            DATETIMEFROMPARTS(YEAR(lpc.NgayLam), MONTH(lpc.NgayLam), DAY(lpc.NgayLam), 
                DATEPART(hour, cl.GioBatDau), DATEPART(minute, cl.GioBatDau), 0, 0)
        ),
        -- Giờ ra cho tháng 9
        DATEADD(MINUTE, 
            (ABS(CHECKSUM(NEWID())) % 31) - 15, -- -15 đến +15 phút
            DATEADD(DAY, IIF(cl.GioKetThuc < cl.GioBatDau, 1, 0),
                DATETIMEFROMPARTS(YEAR(lpc.NgayLam), MONTH(lpc.NgayLam), DAY(lpc.NgayLam), 
                    DATEPART(hour, cl.GioKetThuc), DATEPART(minute, cl.GioKetThuc), 0, 0)
            )
        ),
        0 AS Khoa -- Chưa khóa
    FROM dbo.LichPhanCa lpc
    JOIN dbo.CaLam cl ON cl.MaCa = lpc.MaCa
    WHERE lpc.TrangThai = N'DuKien' 
      AND lpc.NgayLam BETWEEN '2025-09-01' AND '2025-09-21';
    
    DECLARE @ChamCong9Rows INT = @@ROWCOUNT;
    PRINT N'Đã tạo ' + CAST(@ChamCong9Rows AS NVARCHAR) + N' bản ghi chấm công tháng 9.';

    -- 4. ĐơN TỪ THỰC TẾ
    PRINT N'Đang tạo đơn từ...';
    
    SET IDENTITY_INSERT dbo.DonTu ON;
    INSERT INTO dbo.DonTu (MaDon, MaNV, Loai, TuLuc, DenLuc, SoGio, LyDo, TrangThai, DuyetBoi) VALUES
    -- Tháng 7
    (1, 5, N'NGHI', '2025-07-10 14:00:00', '2025-07-10 22:00:00', 8.0, N'Việc gia đình khẩn cấp', N'DaDuyet', 3),
    (2, 6, N'OT', '2025-07-15 14:00:00', '2025-07-15 18:00:00', 4.0, N'Hỗ trợ kiểm kho cuối tuần', N'DaDuyet', 3),
    (3, 7, N'OT', '2025-07-20 06:00:00', '2025-07-20 10:00:00', 4.0, N'Tăng cường an ninh sáng sớm', N'DaDuyet', 4),
    -- Tháng 8  
    (4, 2, N'OT', '2025-08-31 17:00:00', '2025-09-01 01:00:00', 8.0, N'Hoàn thành báo cáo tài chính cuối tháng', N'DaDuyet', 1),
    (5, 8, N'NGHI', '2025-08-15 14:00:00', '2025-08-16 22:00:00', 16.0, N'Nghỉ ốm có giấy bác sĩ', N'DaDuyet', 4),
    (6, 9, N'OT', '2025-08-25 06:00:00', '2025-08-25 12:00:00', 6.0, N'Hỗ trợ nhập hàng đặc biệt', N'DaDuyet', 3),
    -- Tháng 9
    (7, 5, N'NGHI', '2025-09-05 14:00:00', '2025-09-05 22:00:00', 8.0, N'Đi khám sức khỏe định kỳ', N'DaDuyet', 4),
    (8, 6, N'OT', '2025-09-10 14:00:00', '2025-09-10 20:00:00', 6.0, N'Hỗ trợ sắp xếp kho mới', N'ChoDuyet', NULL),
    (9, 7, N'NGHI', '2025-09-22 22:00:00', '2025-09-23 06:00:00', 8.0, N'Nghỉ phép năm', N'ChoDuyet', NULL),
    (10, 1, N'OT', '2025-09-15 17:00:00', '2025-09-15 21:00:00', 4.0, N'Hoàn thiện báo cáo nhân sự', N'DaDuyet', NULL);
    
    SET IDENTITY_INSERT dbo.DonTu OFF;
    PRINT N'Đã tạo 10 đơn từ.';

    -- 5. BẢNG LƯƠNG
    PRINT N'Đang tạo bảng lương...';
    
    INSERT INTO dbo.BangLuong (Nam, Thang, MaNV, LuongCoBan, TongGioCong, GioOT, PhuCap, KhauTru, ThueBH, ThucLanh, TrangThai) VALUES
    -- Tháng 7/2025 - Đã đóng
    (2025, 7, 1, 20000000.00, 184.00, 4.00, 500000.00, 0.00, 2100000.00, 19230769.23, N'Dong'),
    (2025, 7, 2, 18000000.00, 176.00, 0.00, 200000.00, 0.00, 1890000.00, 16310000.00, N'Dong'),
    (2025, 7, 3, 16000000.00, 192.00, 0.00, 300000.00, 100000.00, 1680000.00, 14520000.00, N'Dong'),
    (2025, 7, 4, 16000000.00, 168.00, 0.00, 300000.00, 0.00, 1680000.00, 14620000.00, N'Dong'),
    (2025, 7, 5, 7500000.00, 160.00, 0.00, 150000.00, 50000.00, 787500.00, 6812500.00, N'Dong'),
    (2025, 7, 6, 8000000.00, 184.00, 4.00, 100000.00, 0.00, 840000.00, 7476923.08, N'Dong'),
    (2025, 7, 7, 8500000.00, 192.00, 4.00, 250000.00, 0.00, 892500.00, 8553846.15, N'Dong'),
    (2025, 7, 8, 7500000.00, 152.00, 0.00, 100000.00, 0.00, 787500.00, 6812500.00, N'Dong'),
    (2025, 7, 9, 5000000.00, 96.00, 6.00, 50000.00, 0.00, 525000.00, 4756410.26, N'Dong'),
    -- Tháng 8/2025 - Đã đóng
    (2025, 8, 1, 20000000.00, 176.00, 0.00, 500000.00, 0.00, 2100000.00, 18400000.00, N'Dong'),
    (2025, 8, 2, 18000000.00, 168.00, 8.00, 200000.00, 0.00, 1890000.00, 17076923.08, N'Dong'),
    (2025, 8, 3, 16000000.00, 184.00, 0.00, 300000.00, 50000.00, 1680000.00, 14570000.00, N'Dong'),
    (2025, 8, 4, 16000000.00, 176.00, 0.00, 300000.00, 0.00, 1680000.00, 14620000.00, N'Dong'),
    (2025, 8, 5, 7500000.00, 144.00, 0.00, 150000.00, 100000.00, 787500.00, 6762500.00, N'Dong'),
    (2025, 8, 6, 8000000.00, 168.00, 0.00, 100000.00, 0.00, 840000.00, 7260000.00, N'Dong'),
    (2025, 8, 7, 8500000.00, 184.00, 0.00, 250000.00, 0.00, 892500.00, 7857500.00, N'Dong'),
    (2025, 8, 8, 7500000.00, 160.00, 0.00, 100000.00, 0.00, 787500.00, 6812500.00, N'Dong'),
    (2025, 8, 9, 5000000.00, 88.00, 6.00, 50000.00, 0.00, 525000.00, 4717948.72, N'Dong'),
    -- Tháng 9/2025 - Đang mở (chưa tính lương)
    (2025, 9, 1, 20000000.00, 0.00, 4.00, 0.00, 0.00, 0.00, 0.00, N'Mo'),
    (2025, 9, 2, 18000000.00, 0.00, 0.00, 0.00, 0.00, 0.00, 0.00, N'Mo'),
    (2025, 9, 3, 16000000.00, 0.00, 0.00, 0.00, 0.00, 0.00, 0.00, N'Mo'),
    (2025, 9, 4, 16000000.00, 0.00, 0.00, 0.00, 0.00, 0.00, 0.00, N'Mo'),
    (2025, 9, 5, 7500000.00, 0.00, 0.00, 0.00, 0.00, 0.00, 0.00, N'Mo'),
    (2025, 9, 6, 8000000.00, 0.00, 6.00, 0.00, 0.00, 0.00, 0.00, N'Mo'),
    (2025, 9, 7, 8500000.00, 0.00, 0.00, 0.00, 0.00, 0.00, 0.00, N'Mo'),
    (2025, 9, 8, 7500000.00, 0.00, 0.00, 0.00, 0.00, 0.00, 0.00, N'Mo'),
    (2025, 9, 9, 5000000.00, 0.00, 0.00, 0.00, 0.00, 0.00, 0.00, N'Mo');
    
    PRINT N'Đã tạo bảng lương 3 tháng.';

    -- Kích hoạt lại trigger
    EXEC sys.sp_set_session_context @key=N'SkipTrigger', @value=0;
    
    -- Cập nhật để trigger tính toán
    PRINT N'Đang cập nhật để trigger tính toán...';
    UPDATE dbo.ChamCong SET GioVao = GioVao WHERE NgayLam >= '2025-07-01';
    
    -- Hiển thị thống kê
    PRINT N'=== THỐNG KÊ DỮ LIỆU ===';
    SELECT 
        MONTH(NgayLam) as Thang,
        COUNT(*) as SoLichPhanCa
    FROM dbo.LichPhanCa 
    WHERE NgayLam BETWEEN '2025-07-01' AND '2025-09-21'
    GROUP BY MONTH(NgayLam)
    ORDER BY Thang;
    
    SELECT 
        MONTH(NgayLam) as Thang,
        COUNT(*) as SoChamCong
    FROM dbo.ChamCong 
    WHERE NgayLam BETWEEN '2025-07-01' AND '2025-09-21'
    GROUP BY MONTH(NgayLam)
    ORDER BY Thang;

    COMMIT TRANSACTION;
    PRINT N'HOÀN THÀNH! Dữ liệu từ 1/7 đến 21/9/2025 đã được tạo thành công.';

END TRY
BEGIN CATCH
    IF @@TRANCOUNT > 0
        ROLLBACK TRANSACTION;
    PRINT N'Lỗi: ' + ERROR_MESSAGE();
    THROW;
END CATCH;
GO
