/* =========================================================
   PHẦN 5: SECURITY VÀ TRIGGERS
   Dự án DBMS - HR cho Siêu Thị Mini
   ========================================================= */

USE QLNhanSuSieuThiMini;
GO

-- ============================================================================
-- sp_KiemTraQuyenTruyCap - ĐÃ XÓA
-- ============================================================================
-- LÝ DO XÓA:
-- 1. Code C# không sử dụng stored procedure này
-- 2. C# đã có PermissionManager.cs để kiểm tra quyền
-- 3. Database đã có roles (r_hr, r_quanly, r_ketoan, r_nhanvien)
-- 4. Tạo duplicate logic không cần thiết
-- ============================================================================

------------------------------------------------------------
-- IV) SECURITY: RBAC + DAC
------------------------------------------------------------


-- Tạo roles nếu chưa tồn tại
IF NOT EXISTS (SELECT 1 FROM sys.database_principals WHERE name = 'r_hr' AND type = 'R')
BEGIN
    CREATE ROLE r_hr;
    PRINT N'✓ Đã tạo role r_hr';
END
ELSE
    PRINT N'→ Role r_hr đã tồn tại';

IF NOT EXISTS (SELECT 1 FROM sys.database_principals WHERE name = 'r_quanly' AND type = 'R')
BEGIN
    CREATE ROLE r_quanly;
    PRINT N'✓ Đã tạo role r_quanly';
END
ELSE
    PRINT N'→ Role r_quanly đã tồn tại';

IF NOT EXISTS (SELECT 1 FROM sys.database_principals WHERE name = 'r_ketoan' AND type = 'R')
BEGIN
    CREATE ROLE r_ketoan;
    PRINT N'✓ Đã tạo role r_ketoan';
END
ELSE
    PRINT N'→ Role r_ketoan đã tồn tại';

IF NOT EXISTS (SELECT 1 FROM sys.database_principals WHERE name = 'r_nhanvien' AND type = 'R')
BEGIN
    CREATE ROLE r_nhanvien;
    PRINT N'✓ Đã tạo role r_nhanvien';
END
ELSE
    PRINT N'→ Role r_nhanvien đã tồn tại';

/* 2) DAC: Cấp quyền theo yêu cầu - MÔ HÌNH BẢO MẬT NÂNG CAO
   
   NGUYÊN TẮC: Tất cả thao tác INSERT/UPDATE/DELETE phải đi qua Stored Procedures
   Không cấp quyền trực tiếp trên bảng để đảm bảo:
   - Business logic được thực thi nhất quán
   - Validation được kiểm soát chặt chẽ
   - Audit trail chính xác
   - Bảo mật cao hơn
   
   CHỈ CẤP QUYỀN:
   - SELECT trên views/tables cho mục đích xem và báo cáo
   - EXECUTE trên Stored Procedures cho mọi thao tác thay đổi dữ liệu
*/

-- HR: Quyền xem và thực thi procedures
GRANT SELECT ON dbo.NguoiDung   TO r_hr;  -- ✅ CẦN THIẾT cho login và kiểm tra quyền
GRANT SELECT ON dbo.NhanVien    TO r_hr;
GRANT SELECT ON dbo.PhongBan    TO r_hr;  -- Quản lý phòng ban
GRANT SELECT ON dbo.ChucVu      TO r_hr;  -- Quản lý chức vụ
GRANT SELECT ON dbo.CaLam       TO r_hr;  -- Quản lý ca làm
GRANT SELECT ON dbo.LichPhanCa  TO r_hr;
GRANT SELECT ON dbo.vw_CongThang          TO r_hr;
GRANT SELECT ON dbo.vw_Lich_ChamCong_Ngay TO r_hr;
-- Quyền thực thi procedures quản lý nhân viên (HR và QuanLy)
GRANT EXECUTE ON dbo.sp_ThemMoiNhanVien  TO r_hr;
GRANT EXECUTE ON dbo.sp_ThemMoiNhanVien  TO r_quanly;
GRANT EXECUTE ON dbo.sp_TaoTaiKhoanDayDu TO r_hr;  -- Thêm quyền cho bảo mật 2 lớp
-- GRANT EXECUTE ON dbo.sp_CapNhatTaiKhoanDayDu TO r_hr; -- ❌ XÓA: Procedure không tồn tại
GRANT EXECUTE ON dbo.sp_XoaTaiKhoanDayDu TO r_hr;
GRANT EXECUTE ON dbo.sp_VoHieuHoaTaiKhoan TO r_hr;
GRANT EXECUTE ON dbo.sp_NguoiDung_DoiMatKhau TO r_hr;  -- Thêm quyền đổi mật khẩu
GRANT EXECUTE ON dbo.sp_NguoiDung_DoiMatKhauCaNhan TO r_hr; -- ✅ Đổi mật khẩu cá nhân

-- Thêm quyền INSERT/UPDATE cho các bảng cần thiết
GRANT INSERT, UPDATE ON dbo.NguoiDung TO r_hr;
GRANT INSERT, UPDATE ON dbo.NhanVien TO r_hr;
GRANT EXECUTE ON dbo.sp_DuyetDonTu       TO r_hr;
GRANT EXECUTE ON dbo.sp_KhoaCongThang    TO r_hr;
GRANT EXECUTE ON dbo.sp_MoKhoaCongThang  TO r_hr;
-- CaLam stored procedures
GRANT EXECUTE ON dbo.sp_CaLam_GetAll TO r_hr, r_quanly, r_nhanvien;
GRANT EXECUTE ON dbo.sp_CaLam_GetById TO r_hr, r_quanly, r_nhanvien;
GRANT EXECUTE ON dbo.sp_CaLam_Insert TO r_hr;
GRANT EXECUTE ON dbo.sp_CaLam_Update TO r_hr;
GRANT EXECUTE ON dbo.sp_CaLam_Delete TO r_hr;

-- QuanLy: Quyền xem và thực thi procedures (KHÔNG cấp quyền UPDATE trực tiếp)
GRANT SELECT ON dbo.NguoiDung  TO r_quanly;  -- ✅ CẦN THIẾT cho login và kiểm tra quyền
GRANT SELECT ON dbo.NhanVien TO r_quanly;
GRANT SELECT ON dbo.PhongBan TO r_quanly;  -- Xem danh sách phòng ban
GRANT SELECT ON dbo.ChucVu   TO r_quanly;  -- Xem danh sách chức vụ
GRANT SELECT ON dbo.CaLam    TO r_quanly;  -- Xem ca làm
GRANT SELECT ON dbo.LichPhanCa TO r_quanly;
GRANT SELECT ON dbo.ChamCong TO r_quanly;
GRANT SELECT ON dbo.DonTu    TO r_quanly;  -- Xem đơn từ để duyệt
GRANT SELECT ON dbo.vw_Lich_ChamCong_Ngay TO r_quanly;
GRANT EXECUTE ON dbo.sp_DuyetDonTu TO r_quanly; -- duyệt qua proc để đảm bảo side-effects

-- KeToan: Chỉ SELECT và EXECUTE procedures (KHÔNG cấp quyền INSERT/UPDATE trực tiếp)
GRANT SELECT ON dbo.NguoiDung  TO r_ketoan;  -- ✅ CẦN THIẾT cho login và kiểm tra quyền
GRANT SELECT ON dbo.NhanVien   TO r_ketoan;  -- Cần xem thông tin nhân viên để tính lương
GRANT SELECT ON dbo.ChamCong TO r_ketoan;
GRANT SELECT ON dbo.vw_CongThang TO r_ketoan;
GRANT SELECT ON dbo.BangLuong TO r_ketoan;
GRANT EXECUTE ON dbo.sp_ChayBangLuong TO r_ketoan;
GRANT EXECUTE ON dbo.sp_DongBangLuong TO r_ketoan;

-- NhanVien: Chỉ SELECT và EXECUTE procedures (KHÔNG cấp quyền INSERT trực tiếp)
GRANT SELECT ON dbo.NguoiDung  TO r_nhanvien;  -- ✅ CẦN THIẾT cho login và kiểm tra quyền
GRANT SELECT ON dbo.NhanVien   TO r_nhanvien;  -- Xem thông tin cá nhân
GRANT SELECT ON dbo.PhongBan   TO r_nhanvien;  -- Xem thông tin phòng ban
GRANT SELECT ON dbo.ChucVu     TO r_nhanvien;  -- Xem thông tin chức vụ
GRANT SELECT ON dbo.CaLam      TO r_nhanvien;  -- Xem ca làm
GRANT SELECT ON dbo.LichPhanCa TO r_nhanvien;
GRANT SELECT ON dbo.ChamCong   TO r_nhanvien;
GRANT SELECT ON dbo.DonTu      TO r_nhanvien;
GRANT SELECT ON dbo.BangLuong  TO r_nhanvien;
GRANT EXECUTE ON dbo.sp_DonTu_Insert TO r_nhanvien;  -- Tạo đơn từ qua SP
GRANT EXECUTE ON dbo.sp_CheckIn TO r_nhanvien;       -- Check in qua SP
GRANT EXECUTE ON dbo.sp_CheckOut TO r_nhanvien;      -- Check out qua SP
GO

-- ============================================================================
-- CẤP QUYỀN EXECUTE CHO CÁC STORED PROCEDURES QUAN TRỌNG
-- ============================================================================

-- Cấp quyền cho HR (Nhân sự)
GRANT EXECUTE ON dbo.sp_TaoTaiKhoanDayDu TO r_hr;
GRANT EXECUTE ON dbo.sp_ThemMoiNhanVien TO r_hr;
GRANT EXECUTE ON dbo.sp_XoaTaiKhoanDayDu TO r_hr;
GRANT EXECUTE ON dbo.sp_VoHieuHoaTaiKhoan TO r_hr;
-- sp_CapNhatTaiKhoanDayDu đã bị xóa vì không sử dụng trong C#
GRANT EXECUTE ON dbo.sp_PhongBan_GetAll TO r_hr;
GRANT EXECUTE ON dbo.sp_ChucVu_GetAll TO r_hr;
-- sp_NhanVien_GetAll đã bị xóa vì không sử dụng trong C#
GRANT EXECUTE ON dbo.sp_NhanVien_UpdateThongTinCaNhan TO r_hr;
GRANT EXECUTE ON dbo.sp_NhanVien_Delete TO r_hr;
GRANT EXECUTE ON dbo.sp_NhanVien_UpdateTrangThai TO r_hr;

-- Cấp quyền cho Quản lý (tất cả quyền)
GRANT EXECUTE ON dbo.sp_TaoTaiKhoanDayDu TO r_quanly;
GRANT EXECUTE ON dbo.sp_ThemMoiNhanVien TO r_quanly;
GRANT EXECUTE ON dbo.sp_PhongBan_GetAll TO r_quanly;
GRANT EXECUTE ON dbo.sp_ChucVu_GetAll TO r_quanly;
-- sp_NhanVien_GetAll đã bị xóa vì không sử dụng trong C#
GRANT EXECUTE ON dbo.sp_NhanVien_UpdateThongTinCaNhan TO r_quanly;
GRANT EXECUTE ON dbo.sp_NhanVien_Delete TO r_quanly;
GRANT EXECUTE ON dbo.sp_NhanVien_UpdateTrangThai TO r_quanly;
GRANT EXECUTE ON dbo.sp_KhoaCongThang TO r_quanly;
GRANT EXECUTE ON dbo.sp_ChayBangLuong TO r_quanly;
GRANT EXECUTE ON dbo.sp_NguoiDung_DoiMatKhauCaNhan TO r_quanly; -- ✅ Đổi mật khẩu cá nhân

-- Cấp quyền cho Kế toán
GRANT EXECUTE ON dbo.sp_KhoaCongThang TO r_ketoan;
GRANT EXECUTE ON dbo.sp_ChayBangLuong TO r_ketoan;
GRANT EXECUTE ON dbo.sp_NguoiDung_DoiMatKhauCaNhan TO r_ketoan; -- ✅ Đổi mật khẩu cá nhân

-- Cấp quyền cho Nhân viên (chỉ xem thông tin cá nhân)
GRANT EXECUTE ON dbo.sp_NhanVien_GetThongTinCaNhan TO r_nhanvien;
GRANT EXECUTE ON dbo.sp_NhanVien_UpdateThongTinCaNhan TO r_nhanvien;
-- GRANT EXECUTE ON dbo.sp_NguoiDung_DoiMatKhau TO r_nhanvien; -- ❌ XÓA: Chỉ HR được dùng
GRANT EXECUTE ON dbo.sp_NguoiDung_DoiMatKhauCaNhan TO r_nhanvien; -- ✅ MỚI: Đổi mật khẩu cá nhân

PRINT N'✅ ĐÃ CẤP QUYỀN EXECUTE CHO CÁC STORED PROCEDURES';

-- ============================================================================
-- LƯU Ý: STORED PROCEDURES SỬ DỤNG DATABASE USERS WITHOUT LOGIN
-- ============================================================================
-- Không cần quyền server-level vì không tạo SQL Logins
-- Chỉ tạo Database Users với "WITHOUT LOGIN"
-- Users sẽ xuất hiện trong Security > Users của database

PRINT N'✅ SỬ DỤNG DATABASE USERS WITHOUT LOGIN - KHÔNG CẦN QUYỀN ADMIN';

PRINT N'✅ ĐÃ CẤP QUYỀN THEO MÔ HÌNH BẢO MẬT NÂNG CAO';
PRINT N'   - Tất cả thao tác thay đổi dữ liệu phải qua Stored Procedures';
PRINT N'   - Chỉ cấp quyền SELECT trực tiếp cho mục đích xem/báo cáo';
PRINT N'';
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

-- 4) LICHPHANCA: Trigger đã được gộp vào tr_LichPhanCa_BlockChangeWhenLocked (xem phía dưới)
-- Đã xóa trigger tr_LichPhanCa_NoEditWhenKhoa vì trùng lặp chức năng

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

-- Quyền xem danh sách nhân viên (tất cả role đều cần để hiển thị)
-- HR: Toàn quyền CRUD nhân viên
-- QuanLy: Toàn quyền CRUD nhân viên (như HR)
-- KeToan: Chỉ xem nhân viên (để tính lương)
-- NhanVien: Chỉ xem thông tin cá nhân
GRANT EXECUTE ON dbo.sp_GetNhanVienFull TO r_hr;
GRANT EXECUTE ON dbo.sp_GetNhanVienFull TO r_quanly;
GRANT EXECUTE ON dbo.sp_GetNhanVienFull TO r_ketoan;
GRANT EXECUTE ON dbo.sp_GetNhanVienFull TO r_nhanvien;

-- Quyền cho sp_UpdateNhanVienWithPhongBanChucVu (HR và QuanLy đều được cập nhật)
GRANT EXECUTE ON dbo.sp_UpdateNhanVienWithPhongBanChucVu TO r_hr;
GRANT EXECUTE ON dbo.sp_UpdateNhanVienWithPhongBanChucVu TO r_quanly;

-- Quyền cho CRUD LichPhanCa
GRANT EXECUTE ON dbo.sp_LichPhanCa_Insert TO r_hr, r_quanly;
GRANT EXECUTE ON dbo.sp_LichPhanCa_Update TO r_hr, r_quanly;
GRANT EXECUTE ON dbo.sp_LichPhanCa_Delete TO r_hr, r_quanly;
GRANT EXECUTE ON dbo.sp_LichPhanCa_GetByNhanVien TO r_hr, r_quanly, r_nhanvien;

-- Quyền cho quản lý lịch tuần
GRANT EXECUTE ON dbo.sp_LichPhanCa_CloneWeek TO r_hr, r_quanly;
GRANT EXECUTE ON dbo.sp_LichPhanCa_KhoaTuan TO r_hr, r_quanly;
GRANT EXECUTE ON dbo.sp_LichPhanCa_MoKhoaTuan TO r_hr, r_quanly;

-- Quyền xem TVF
GRANT SELECT ON dbo.tvf_LichTheoTuan TO r_hr, r_quanly, r_ketoan, r_nhanvien;

-- Quyền cho CRUD PhongBan và ChucVu
-- ✅ Đã bổ sung sp_PhongBan_GetAll và sp_ChucVu_GetAll
GRANT EXECUTE ON dbo.sp_PhongBan_GetAll TO r_hr, r_quanly, r_ketoan, r_nhanvien;
GRANT EXECUTE ON dbo.sp_PhongBan_Insert TO r_hr;
GRANT EXECUTE ON dbo.sp_PhongBan_Update TO r_hr;
GRANT EXECUTE ON dbo.sp_PhongBan_Delete TO r_hr;

GRANT EXECUTE ON dbo.sp_ChucVu_GetAll TO r_hr, r_quanly, r_ketoan, r_nhanvien;
GRANT EXECUTE ON dbo.sp_ChucVu_Insert TO r_hr;
GRANT EXECUTE ON dbo.sp_ChucVu_Update TO r_hr;
GRANT EXECUTE ON dbo.sp_ChucVu_Delete TO r_hr;

-- Quyền cho DonTu
GRANT EXECUTE ON dbo.sp_DonTu_Insert TO r_nhanvien, r_hr, r_quanly;

-- Quyền cho NhanVien bổ sung
GRANT EXECUTE ON dbo.sp_NhanVien_Delete TO r_hr;
GRANT EXECUTE ON dbo.sp_NhanVien_UpdateTrangThai TO r_hr, r_quanly;

------------------------------------------------------------
-- VI) TRIGGER CHẶN SỬA LỊCH ĐÃ KHÓA
------------------------------------------------------------

-- Trigger chặn UPDATE/DELETE lịch khi TrangThai = 'Khoa'
IF OBJECT_ID('dbo.tr_LichPhanCa_BlockChangeWhenLocked','TR') IS NOT NULL 
    DROP TRIGGER dbo.tr_LichPhanCa_BlockChangeWhenLocked;
GO
CREATE TRIGGER dbo.tr_LichPhanCa_BlockChangeWhenLocked
ON dbo.LichPhanCa
AFTER UPDATE, DELETE
AS
BEGIN
    SET NOCOUNT ON;
    
    -- Cho phép bypass qua SESSION_CONTEXT
    IF TRY_CONVERT(INT, SESSION_CONTEXT(N'SkipTrigger')) = 1 RETURN;

    -- Kiểm tra UPDATE
    IF EXISTS (SELECT 1 FROM inserted)
    BEGIN
        IF EXISTS (
            SELECT 1
            FROM deleted d
            WHERE d.TrangThai = N'Khoa'
              AND EXISTS (
                  SELECT 1 FROM inserted i 
                  WHERE i.MaLich = d.MaLich
                    AND (i.MaNV <> d.MaNV OR i.NgayLam <> d.NgayLam OR i.MaCa <> d.MaCa)
              )
        )
        BEGIN
            RAISERROR(N'Không thể sửa lịch đã khóa. Chỉ có thể cập nhật ghi chú hoặc mở khóa trước.', 16, 1);
            ROLLBACK TRANSACTION;
            RETURN;
        END
    END

    -- Kiểm tra DELETE
    IF EXISTS (SELECT 1 FROM deleted WHERE TrangThai = N'Khoa')
    BEGIN
        RAISERROR(N'Không thể xóa lịch đã khóa. Phải mở khóa trước.', 16, 1);
        ROLLBACK TRANSACTION;
        RETURN;
    END
END
GO

PRINT N'Đã tạo trigger tr_LichPhanCa_BlockChangeWhenLocked';

------------------------------------------------------------
-- VII) HOÀN TẤT KHỞI TẠO
------------------------------------------------------------

PRINT N'';
PRINT N'=== HOÀN TẤT KHỞI TẠO SCHEMA ===';
PRINT N'Database QLNhanSuSieuThiMini đã sẵn sàng!';
PRINT N'';
PRINT N'Đã thêm:';
PRINT N'✓ TVF tvf_LichTheoTuan';
PRINT N'✓ CRUD LichPhanCa (Insert/Update/Delete/GetByNhanVien)';
PRINT N'✓ sp_LichPhanCa_CloneWeek';
PRINT N'✓ sp_LichPhanCa_KhoaTuan / MoKhoaTuan';
PRINT N'✓ Trigger tr_LichPhanCa_BlockChangeWhenLocked';
PRINT N'✓ sp_DuyetDonTu (đã mở rộng đồng bộ LichPhanCa)';
PRINT N'';
PRINT N'Chạy file data_mau.sql để thêm dữ liệu mẫu.';
GO
