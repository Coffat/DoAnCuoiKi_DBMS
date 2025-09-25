/* =========================================================
   DỮ LIỆU MẪU CƠ BẢN - PHIÊN BẢN ĐƠN GIẢN
   Chỉ bao gồm dữ liệu cần thiết để test hệ thống
   ========================================================= */

USE QLNhanSuSieuThiMini;
GO

PRINT N'=== BẮT ĐẦU CHẠY DỮ LIỆU MẪU CƠ BẢN ===';
PRINT N'';

BEGIN TRANSACTION;

BEGIN TRY

    -- Tạm tắt trigger để insert dữ liệu mẫu
    EXEC sys.sp_set_session_context @key=N'SkipTrigger', @value=1;
    PRINT N'✅ Set SkipTrigger to 1 successful.';

    -- Xóa dữ liệu cũ
    PRINT N'🗑️  Đang xóa dữ liệu cũ...';
    DELETE FROM dbo.BangLuong;
    DELETE FROM dbo.DonTu;
    DELETE FROM dbo.ChamCong;
    DELETE FROM dbo.LichPhanCa;
    DELETE FROM dbo.NhanVien;
    DELETE FROM dbo.NguoiDung;
    DELETE FROM dbo.CaLam;
    PRINT N'✅ Deleted existing data from all tables.';

    -- Reset identity seed
    DBCC CHECKIDENT ('dbo.NguoiDung', RESEED, 0);
    DBCC CHECKIDENT ('dbo.NhanVien', RESEED, 0);
    DBCC CHECKIDENT ('dbo.CaLam', RESEED, 0);
    DBCC CHECKIDENT ('dbo.LichPhanCa', RESEED, 0);
    DBCC CHECKIDENT ('dbo.ChamCong', RESEED, 0);
    DBCC CHECKIDENT ('dbo.DonTu', RESEED, 0);
    DBCC CHECKIDENT ('dbo.BangLuong', RESEED, 0);
    PRINT N'✅ Reset identity seeds.';
    PRINT N'';

    -- 1. BẢNG CALAM
    PRINT N'1️⃣  Thêm dữ liệu CaLam...';
    SET IDENTITY_INSERT dbo.CaLam ON;
    INSERT INTO dbo.CaLam (MaCa, TenCa, GioBatDau, GioKetThuc, HeSoCa, MoTa, KichHoat) VALUES
    (1, N'Ca Sáng',      '06:00:00', '14:00:00', 1.0, N'Ca làm việc buổi sáng, giao ca lúc 14:00.', 1),
    (2, N'Ca Chiều',     '14:00:00', '22:00:00', 1.0, N'Ca làm việc buổi chiều, giao ca lúc 22:00.', 1),
    (3, N'Ca Đêm',       '22:00:00', '06:00:00', 1.5, N'Ca qua đêm, có phụ cấp ca đêm.', 1),
    (4, N'Ca Hành chính','08:00:00', '17:00:00', 1.0, N'Ca dành cho HR/Kế toán, giờ hành chính.', 1);
    
    DECLARE @CaLamRows INT = @@ROWCOUNT;
    SET IDENTITY_INSERT dbo.CaLam OFF;
    PRINT N'✅ Inserted ' + CAST(@CaLamRows AS NVARCHAR) + N' rows into CaLam.';

    -- 2. BẢNG NGUOIDUNG
    PRINT N'2️⃣  Thêm dữ liệu NguoiDung...';
    SET IDENTITY_INSERT dbo.NguoiDung ON;
    INSERT INTO dbo.NguoiDung (MaNguoiDung, TenDangNhap, MatKhauHash, VaiTro, KichHoat) VALUES
    (1, 'bichhang',    '1234', N'HR',       1),
    (2, 'vanan',       '1234', N'KeToan',   1),
    (3, 'minhtuan',    '1234', N'QuanLy',   1),
    (4, 'kimchi',      '1234', N'QuanLy',   1),
    (5, 'vandzung',    '1234', N'NhanVien', 1),
    (6, 'mylinh',      '1234', N'NhanVien', 1),
    (7, 'tienmanh',    '1234', N'NhanVien', 1);
    
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
    (7, 7, N'Đỗ Tiến Mạnh',         '1991-08-14', N'Nam', '0967890123', 'tienmanh@company.com',  N'147 Đường STU, Quận Gò Vấp',        '2022-07-10', N'DangLam', N'BaoVe',    N'Bảo vệ',             5000000);
    
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

    -- 5. LỊCH LÀM VIỆC HÔM NAY
    PRINT N'5️⃣  Thêm lịch làm việc hôm nay...';
    DECLARE @NgayHomNay DATE = CAST(GETDATE() AS DATE);
    PRINT N'Thêm lịch làm việc cho ngày: ' + CAST(@NgayHomNay AS NVARCHAR(10));

    -- Xóa lịch hôm nay nếu đã có
    DELETE FROM LichPhanCa WHERE NgayLam = @NgayHomNay;

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

    -- Bật lại trigger
    EXEC sys.sp_set_session_context @key=N'SkipTrigger', @value=0;
    PRINT N'✅ Set SkipTrigger to 0 (enabled triggers).';
    PRINT N'';

    -- KIỂM TRA KẾT QUẢ
    PRINT N'📊 KIỂM TRA KẾT QUẢ';
    PRINT N'==================';
    
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
        'DonTu' as TableName, 
        COUNT(*) as RecordCount,
        MIN(Loai + ' - ' + TrangThai) as SampleData
    FROM dbo.DonTu;

    COMMIT TRANSACTION;
    
    PRINT N'';
    PRINT N'🎉 HOÀN TẤT CHẠY DỮ LIỆU MẪU CƠ BẢN THÀNH CÔNG!';
    PRINT N'';
    PRINT N'📋 Tóm tắt dữ liệu đã tạo:';
    PRINT N'• CaLam: 4 ca làm việc cơ bản';
    PRINT N'• NguoiDung: 7 tài khoản test';
    PRINT N'• NhanVien: 7 nhân viên đang làm việc';
    PRINT N'• LichPhanCa: Lịch làm việc hôm nay';
    PRINT N'• DonTu: 3 đơn từ mẫu';
    PRINT N'';
    PRINT N'✅ Database sẵn sàng để test các chức năng cơ bản!';
    PRINT N'';
    PRINT N'🔑 Tài khoản test:';
    PRINT N'• bichhang/1234 (HR)';
    PRINT N'• vanan/1234 (KeToan)';
    PRINT N'• minhtuan/1234 (QuanLy)';
    PRINT N'• vandzung/1234 (NhanVien)';

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
