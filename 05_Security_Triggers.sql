/* =========================================================
   PHẦN 5: SECURITY VÀ TRIGGERS
   Dự án DBMS - HR cho Siêu Thị Mini
   ========================================================= */

USE QLNhanSuSieuThiMini;
GO

-- Thêm các stored procedures còn thiếu

IF OBJECT_ID('dbo.sp_KiemTraQuyenTruyCap','P') IS NOT NULL DROP PROCEDURE dbo.sp_KiemTraQuyenTruyCap;
GO
CREATE PROCEDURE dbo.sp_KiemTraQuyenTruyCap
    @MaNguoiDung INT,
    @ChucNang    NVARCHAR(50),
    @CoQuyen     BIT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    
    DECLARE @VaiTro NVARCHAR(20);
    SET @CoQuyen = 0;
    
    SELECT @VaiTro = VaiTro 
    FROM dbo.NguoiDung 
    WHERE MaNguoiDung = @MaNguoiDung AND KichHoat = 1;
    
    IF @VaiTro IS NULL RETURN;
    
    IF @ChucNang = 'MANAGE_EMPLOYEES' AND @VaiTro = N'HR'
        SET @CoQuyen = 1;
    ELSE IF @ChucNang = 'MANAGE_SHIFTS' AND @VaiTro = N'HR'
        SET @CoQuyen = 1;
    ELSE IF @ChucNang = 'MANAGE_SCHEDULE' AND @VaiTro IN (N'HR', N'QuanLy')
        SET @CoQuyen = 1;
    ELSE IF @ChucNang = 'MANAGE_ATTENDANCE' AND @VaiTro IN (N'HR', N'QuanLy')
        SET @CoQuyen = 1;
    ELSE IF @ChucNang = 'APPROVE_REQUESTS' AND @VaiTro IN (N'HR', N'QuanLy')
        SET @CoQuyen = 1;
    ELSE IF @ChucNang = 'MANAGE_PAYROLL' AND @VaiTro = N'KeToan'
        SET @CoQuyen = 1;
    ELSE IF @ChucNang = 'VIEW_REPORTS' AND @VaiTro IN (N'HR', N'QuanLy', N'KeToan')
        SET @CoQuyen = 1;
    ELSE IF @ChucNang = 'SUBMIT_REQUESTS'
        SET @CoQuyen = 1;
    ELSE IF @ChucNang = 'VIEW_OWN_DATA'
        SET @CoQuyen = 1;
END
GO

------------------------------------------------------------
-- IV) SECURITY: RBAC + DAC
------------------------------------------------------------

-- 1) Xóa và tạo lại ROLE (để đảm bảo sạch)
IF EXISTS (SELECT 1 FROM sys.database_principals WHERE name=N'r_hr' AND type='R')       DROP ROLE r_hr;
IF EXISTS (SELECT 1 FROM sys.database_principals WHERE name=N'r_quanly' AND type='R')   DROP ROLE r_quanly;
IF EXISTS (SELECT 1 FROM sys.database_principals WHERE name=N'r_ketoan' AND type='R')   DROP ROLE r_ketoan;
IF EXISTS (SELECT 1 FROM sys.database_principals WHERE name=N'r_nhanvien' AND type='R') DROP ROLE r_nhanvien;

CREATE ROLE r_hr;
CREATE ROLE r_quanly;
CREATE ROLE r_ketoan;
CREATE ROLE r_nhanvien;
PRINT N'Đã tạo lại các database roles: r_hr, r_quanly, r_ketoan, r_nhanvien';
GO

/* 2) DAC: Cấp quyền theo yêu cầu
   HR: INSERT/UPDATE/DELETE NhanVien, LichPhanCa; EXEC các proc liên quan
   Store Manager: SELECT LichPhanCa; UPDATE hạn chế ChamCong; (Duyệt đơn qua PROC)
   Accountant: SELECT ChamCong/vw_CongThang; INSERT/UPDATE BangLuong; EXEC tính/chốt lương
   Employee: INSERT DonTu; SELECT các bảng với quyền hạn chế
*/

-- HR
GRANT SELECT, INSERT, UPDATE, DELETE ON dbo.NhanVien    TO r_hr;
GRANT SELECT, INSERT, UPDATE, DELETE ON dbo.LichPhanCa  TO r_hr;
GRANT SELECT               ON dbo.vw_CongThang          TO r_hr;
GRANT SELECT               ON dbo.vw_Lich_ChamCong_Ngay TO r_hr;
GRANT EXECUTE ON dbo.sp_ThemMoiNhanVien  TO r_hr;
GRANT EXECUTE ON dbo.sp_DuyetDonTu       TO r_hr;
GRANT EXECUTE ON dbo.sp_KhoaCongThang    TO r_hr;
GRANT EXECUTE ON dbo.sp_MoKhoaCongThang  TO r_hr;
-- CaLam stored procedures
GRANT EXECUTE ON dbo.sp_CaLam_GetAll TO r_hr, r_quanly, r_nhanvien;
GRANT EXECUTE ON dbo.sp_CaLam_GetById TO r_hr, r_quanly, r_nhanvien;
GRANT EXECUTE ON dbo.sp_CaLam_Insert TO r_hr;
GRANT EXECUTE ON dbo.sp_CaLam_Update TO r_hr;
GRANT EXECUTE ON dbo.sp_CaLam_Delete TO r_hr;

-- Store Manager (không UPDATE trực tiếp trạng thái DonTu)
GRANT SELECT ON dbo.LichPhanCa TO r_quanly;
GRANT UPDATE (GioVao, GioRa, GioCong, DiTrePhut, VeSomPhut) ON dbo.ChamCong TO r_quanly;
GRANT SELECT ON dbo.vw_Lich_ChamCong_Ngay TO r_quanly;
GRANT EXECUTE ON dbo.sp_DuyetDonTu TO r_quanly; -- duyệt qua proc để đảm bảo side-effects

-- Accountant
GRANT SELECT ON dbo.ChamCong TO r_ketoan;
GRANT SELECT ON dbo.vw_CongThang TO r_ketoan;
GRANT SELECT, INSERT, UPDATE ON dbo.BangLuong TO r_ketoan;
GRANT EXECUTE ON dbo.sp_ChayBangLuong TO r_ketoan;
GRANT EXECUTE ON dbo.sp_DongBangLuong TO r_ketoan;

-- Employee
GRANT INSERT ON dbo.DonTu TO r_nhanvien;
GRANT SELECT ON dbo.LichPhanCa TO r_nhanvien;
GRANT SELECT ON dbo.ChamCong   TO r_nhanvien;
GRANT SELECT ON dbo.DonTu      TO r_nhanvien;
GRANT SELECT ON dbo.BangLuong  TO r_nhanvien;
GO

------------------------------------------------------------
-- V) TRIGGERS (có công tắc bỏ qua: SESSION_CONTEXT('SkipTrigger') = 1)
------------------------------------------------------------

-- 1) CHAMCONG: Tự tính GioCong/DiTre/VeSom khi có GioVao & GioRa
IF OBJECT_ID('dbo.tr_ChamCong_AIU_TinhCong','TR') IS NOT NULL DROP TRIGGER dbo.tr_ChamCong_AIU_TinhCong;
GO
CREATE TRIGGER dbo.tr_ChamCong_AIU_TinhCong
ON dbo.ChamCong
AFTER INSERT, UPDATE
AS
BEGIN
    SET NOCOUNT ON;
    IF TRY_CONVERT(INT, SESSION_CONTEXT(N'SkipTrigger')) = 1 RETURN;

    ;WITH I AS (
        SELECT i.MaChamCong, i.MaNV, i.NgayLam, i.GioVao, i.GioRa
        FROM inserted i
    ),
    Calc AS (
        SELECT
            I.MaChamCong,
            CASE
              WHEN I.GioVao IS NOT NULL AND I.GioRa IS NOT NULL
              THEN CAST(DATEDIFF(MINUTE, I.GioVao, I.GioRa)/60.0 AS DECIMAL(5,2))
              ELSE NULL
            END AS GioCongCalc,
            
            -- Ghép ngày làm với giờ bắt đầu ca để so sánh chính xác
            IIF(I.GioVao IS NULL OR KC.GioBatDau IS NULL, NULL,
                dbo.fn_SoPhutDuong(
                    DATETIMEFROMPARTS(YEAR(I.NgayLam), MONTH(I.NgayLam), DAY(I.NgayLam),
                                    DATEPART(hour, KC.GioBatDau), DATEPART(minute, KC.GioBatDau), 0, 0),
                    I.GioVao
                )
            ) AS DiTreCalc,

            -- Với ca qua đêm, giờ kết thúc thuộc về ngày hôm sau
            IIF(I.GioRa IS NULL OR KC.GioKetThuc IS NULL, NULL,
                dbo.fn_SoPhutDuong(
                    I.GioRa,
                    DATETIMEFROMPARTS(
                        YEAR(DATEADD(DAY, IIF(KC.GioKetThuc < KC.GioBatDau, 1, 0), I.NgayLam)),
                        MONTH(DATEADD(DAY, IIF(KC.GioKetThuc < KC.GioBatDau, 1, 0), I.NgayLam)),
                        DAY(DATEADD(DAY, IIF(KC.GioKetThuc < KC.GioBatDau, 1, 0), I.NgayLam)),
                        DATEPART(hour, KC.GioKetThuc), DATEPART(minute, KC.GioKetThuc), 0, 0
                    )
                )
            ) AS VeSomCalc
        FROM I
        OUTER APPLY dbo.fn_KhungCa(I.MaNV, I.NgayLam) KC
    )
    UPDATE c
       SET c.GioCong   = COALESCE(Calc.GioCongCalc, c.GioCong),
           c.DiTrePhut = COALESCE(Calc.DiTreCalc,  c.DiTrePhut),
           c.VeSomPhut = COALESCE(Calc.VeSomCalc,  c.VeSomPhut)
    FROM dbo.ChamCong c
    JOIN Calc ON Calc.MaChamCong = c.MaChamCong;
END
GO

-- 2) CALAM: Kiểm tra trùng lặp thời gian ca làm việc (hỗ trợ ca qua đêm)
-- LOGIC ĐÃ ĐƯỢC DI CHUYỂN VÀO STORED PROCEDURES sp_CaLam_Insert VÀ sp_CaLam_Update
-- XÓA TRIGGER ĐỂ GIẢM PHỨC TẠP HỆ THỐNG
--
-- LOGIC MỚI:
-- - Nhân viên được làm nhiều ca trong 1 ngày nhưng phải hợp lý
-- - Tổng thời gian làm việc không vượt quá 16 tiếng/ngày (960 phút)
-- - Không được làm 2 ca cùng loại trong cùng ngày (ví dụ: 2 ca sáng)
-- - Vẫn kiểm tra overlap thời gian giữa các ca
-- - Cho phép overlap hợp lý giữa ca chính và ca hành chính/part-time
-- - Hỗ trợ đầy đủ ca qua đêm với logic tính toán bằng phút từ midnight


-- 3) CHAMCONG: Chặn UPDATE/DELETE khi đã khóa (Khoa = 1)
IF OBJECT_ID('dbo.tr_ChamCong_BlockWhenLocked_U','TR') IS NOT NULL DROP TRIGGER dbo.tr_ChamCong_BlockWhenLocked_U;
GO
CREATE TRIGGER dbo.tr_ChamCong_BlockWhenLocked_U
ON dbo.ChamCong
AFTER UPDATE
AS
BEGIN
    SET NOCOUNT ON;
    IF TRY_CONVERT(INT, SESSION_CONTEXT(N'SkipTrigger')) = 1 RETURN;

    IF EXISTS (
        SELECT 1
        FROM inserted i
        JOIN deleted d ON d.MaChamCong = i.MaChamCong
        JOIN dbo.ChamCong c ON c.MaChamCong = d.MaChamCong
        WHERE c.Khoa = 1
          AND (UPDATE(GioVao) OR UPDATE(GioRa) OR UPDATE(GioCong)
               OR UPDATE(DiTrePhut) OR UPDATE(VeSomPhut) OR UPDATE(NgayLam) OR UPDATE(MaNV))
    )
    BEGIN
        RAISERROR(N'Chấm công đã khóa, không được chỉnh sửa.', 16, 1);
        ROLLBACK TRANSACTION;
        RETURN;
    END
END
GO

IF OBJECT_ID('dbo.tr_ChamCong_BlockWhenLocked_D','TR') IS NOT NULL DROP TRIGGER dbo.tr_ChamCong_BlockWhenLocked_D;
GO
CREATE TRIGGER dbo.tr_ChamCong_BlockWhenLocked_D
ON dbo.ChamCong
AFTER DELETE
AS
BEGIN
    SET NOCOUNT ON;
    IF TRY_CONVERT(INT, SESSION_CONTEXT(N'SkipTrigger')) = 1 RETURN;

    IF EXISTS (SELECT 1 FROM deleted WHERE Khoa = 1)
    BEGIN
        RAISERROR(N'Chấm công đã khóa, không được xóa.', 16, 1);
        ROLLBACK TRANSACTION;
        RETURN;
    END
END
GO

-- 4) LICHPHANCA: Chặn đổi MaNV/NgayLam/MaCa khi lịch đã Khoa
IF OBJECT_ID('dbo.tr_LichPhanCa_NoEditWhenKhoa','TR') IS NOT NULL DROP TRIGGER dbo.tr_LichPhanCa_NoEditWhenKhoa;
GO
CREATE TRIGGER dbo.tr_LichPhanCa_NoEditWhenKhoa
ON dbo.LichPhanCa
AFTER UPDATE
AS
BEGIN
    SET NOCOUNT ON;
    IF TRY_CONVERT(INT, SESSION_CONTEXT(N'SkipTrigger')) = 1 RETURN;

    IF EXISTS (
        SELECT 1
        FROM inserted i
        JOIN deleted d ON d.MaLich = i.MaLich
        WHERE d.TrangThai = N'Khoa'
          AND ( ISNULL(i.MaNV,0)   <> ISNULL(d.MaNV,0)
             OR ISNULL(i.MaCa,0)   <> ISNULL(d.MaCa,0)
             OR ISNULL(i.NgayLam,'1900-01-01') <> ISNULL(d.NgayLam,'1900-01-01') )
    )
    BEGIN
        RAISERROR(N'Lịch đã khóa, không được thay đổi nhân sự/ca/ngày.', 16, 1);
        ROLLBACK TRANSACTION;
        RETURN;
    END
END
GO

-- 5) BANGLUONG: Chặn UPDATE/DELETE khi TrangThai = 'Dong'
IF OBJECT_ID('dbo.tr_BangLuong_NoEditWhenDong_U','TR') IS NOT NULL DROP TRIGGER dbo.tr_BangLuong_NoEditWhenDong_U;
GO
CREATE TRIGGER dbo.tr_BangLuong_NoEditWhenDong_U
ON dbo.BangLuong
AFTER UPDATE
AS
BEGIN
    SET NOCOUNT ON;
    IF TRY_CONVERT(INT, SESSION_CONTEXT(N'SkipTrigger')) = 1 RETURN;

    IF EXISTS (
        SELECT 1
        FROM inserted i
        JOIN deleted d ON d.MaBangLuong = i.MaBangLuong
        WHERE d.TrangThai = N'Dong'
    )
    BEGIN
        RAISERROR(N'Bảng lương đã chốt (Đóng), không được chỉnh sửa.', 16, 1);
        ROLLBACK TRANSACTION;
        RETURN;
    END
END
GO

IF OBJECT_ID('dbo.tr_BangLuong_NoEditWhenDong_D','TR') IS NOT NULL DROP TRIGGER dbo.tr_BangLuong_NoEditWhenDong_D;
GO
CREATE TRIGGER dbo.tr_BangLuong_NoEditWhenDong_D
ON dbo.BangLuong
AFTER DELETE
AS
BEGIN
    SET NOCOUNT ON;
    IF TRY_CONVERT(INT, SESSION_CONTEXT(N'SkipTrigger')) = 1 RETURN;

    IF EXISTS (SELECT 1 FROM deleted WHERE TrangThai = N'Dong')
    BEGIN
        RAISERROR(N'Bảng lương đã chốt (Đóng), không được xóa.', 16, 1);
        ROLLBACK TRANSACTION;
        RETURN;
    END
END
GO

-- 6) NHANVIEN: Đồng bộ kích hoạt tài khoản theo trạng thái NV
IF OBJECT_ID('dbo.tr_NhanVien_ToggleAccount','TR') IS NOT NULL DROP TRIGGER dbo.tr_NhanVien_ToggleAccount;
GO
CREATE TRIGGER dbo.tr_NhanVien_ToggleAccount
ON dbo.NhanVien
AFTER UPDATE
AS
BEGIN
    SET NOCOUNT ON;
    IF TRY_CONVERT(INT, SESSION_CONTEXT(N'SkipTrigger')) = 1 RETURN;

    -- Khóa tài khoản khi NV nghỉ
    UPDATE nd
    SET nd.KichHoat = 0
    FROM dbo.NguoiDung nd
    JOIN inserted i   ON i.MaNguoiDung = nd.MaNguoiDung
    JOIN deleted  d   ON d.MaNV = i.MaNV
    WHERE i.TrangThai = N'Nghi' AND d.TrangThai <> N'Nghi' AND i.MaNguoiDung IS NOT NULL;

    -- Mở tài khoản lại khi NV quay về Đang làm
    UPDATE nd
    SET nd.KichHoat = 1
    FROM dbo.NguoiDung nd
    JOIN inserted i   ON i.MaNguoiDung = nd.MaNguoiDung
    JOIN deleted  d   ON d.MaNV = i.MaNV
    WHERE i.TrangThai = N'DangLam' AND d.TrangThai <> N'DangLam' AND i.MaNguoiDung IS NOT NULL;
END
GO

-- 6) NHANVIEN: Đồng bộ kích hoạt tài khoản theo trạng thái NV
IF OBJECT_ID('dbo.tr_NhanVien_ToggleAccount','TR') IS NOT NULL DROP TRIGGER dbo.tr_NhanVien_ToggleAccount;
GO
CREATE TRIGGER dbo.tr_NhanVien_ToggleAccount
ON dbo.NhanVien
AFTER UPDATE
AS
BEGIN
    SET NOCOUNT ON;
    IF TRY_CONVERT(INT, SESSION_CONTEXT(N'SkipTrigger')) = 1 RETURN;

    -- Khóa tài khoản khi NV nghỉ
    UPDATE nd
    SET nd.KichHoat = 0
    FROM dbo.NguoiDung nd
    JOIN inserted i   ON i.MaNguoiDung = nd.MaNguoiDung
    JOIN deleted  d   ON d.MaNV = i.MaNV
    WHERE i.TrangThai = N'Nghi' AND d.TrangThai <> N'Nghi' AND i.MaNguoiDung IS NOT NULL;

    -- Mở tài khoản lại khi NV quay về Đang làm
    UPDATE nd
    SET nd.KichHoat = 1
    FROM dbo.NguoiDung nd
    JOIN inserted i   ON i.MaNguoiDung = nd.MaNguoiDung
    JOIN deleted  d   ON d.MaNV = i.MaNV
    WHERE i.TrangThai = N'DangLam' AND d.TrangThai <> N'DangLam' AND i.MaNguoiDung IS NOT NULL;
END
GO

------------------------------------------------------------
-- V) PHÂN QUYỀN CHO CÁC STORED PROCEDURES MỚI
------------------------------------------------------------

-- Quyền cho sp_PhongBan_Insert, sp_PhongBan_Update, sp_PhongBan_Delete
GRANT EXECUTE ON dbo.sp_PhongBan_Insert TO r_hr;
GRANT EXECUTE ON dbo.sp_PhongBan_Update TO r_hr;
GRANT EXECUTE ON dbo.sp_PhongBan_Delete TO r_hr;

-- Quyền cho sp_ChucVu_Insert, sp_ChucVu_Update, sp_ChucVu_Delete
GRANT EXECUTE ON dbo.sp_ChucVu_Insert TO r_hr;
GRANT EXECUTE ON dbo.sp_ChucVu_Update TO r_hr;
GRANT EXECUTE ON dbo.sp_ChucVu_Delete TO r_hr;

-- Quyền cho sp_GetPhongBanChucVu (tất cả role đều cần để hiển thị dropdown)
GRANT EXECUTE ON dbo.sp_GetPhongBanChucVu TO r_hr;
GRANT EXECUTE ON dbo.sp_GetPhongBanChucVu TO r_quanly;
GRANT EXECUTE ON dbo.sp_GetPhongBanChucVu TO r_ketoan;
GRANT EXECUTE ON dbo.sp_GetPhongBanChucVu TO r_nhanvien;

-- Quyền cho sp_GetNhanVienFull (tất cả role đều cần)
GRANT EXECUTE ON dbo.sp_GetNhanVienFull TO r_hr;
GRANT EXECUTE ON dbo.sp_GetNhanVienFull TO r_quanly;
GRANT EXECUTE ON dbo.sp_GetNhanVienFull TO r_ketoan;
GRANT EXECUTE ON dbo.sp_GetNhanVienFull TO r_nhanvien;

-- Quyền cho sp_UpdateNhanVienWithPhongBanChucVu
GRANT EXECUTE ON dbo.sp_UpdateNhanVienWithPhongBanChucVu TO r_hr;
GRANT EXECUTE ON dbo.sp_UpdateNhanVienWithPhongBanChucVu TO r_quanly;

------------------------------------------------------------
-- VI) HOÀN TẤT KHỞI TẠO
------------------------------------------------------------

PRINT N'=== HOÀN TẤT KHỞI TẠO SCHEMA ===';
PRINT N'Database QLNhanSuSieuThiMini đã sẵn sàng!';
PRINT N'Chạy file duLieuMau.sql để thêm dữ liệu mẫu.';
GO
