/* =========================================================
   PHẦN 2: VIEWS, FUNCTIONS, PROCEDURES, TRIGGERS, SECURITY
   Dự án DBMS - HR cho Siêu Thị Mini
   ========================================================= */

USE QLNhanSuSieuThiMini;
GO

------------------------------------------------------------
-- I) VIEWS
------------------------------------------------------------

-- 1) Tổng hợp công theo tháng
IF OBJECT_ID('dbo.vw_CongThang','V') IS NOT NULL DROP VIEW dbo.vw_CongThang;
GO
CREATE VIEW dbo.vw_CongThang
AS
SELECT 
    c.MaNV,
    YEAR(c.NgayLam)  AS Nam,
    MONTH(c.NgayLam) AS Thang,
    SUM(ISNULL(c.GioCong,0)) AS TongGioCong,
    SUM(CASE WHEN ISNULL(c.DiTrePhut,0)>0 THEN c.DiTrePhut ELSE 0 END) AS TongPhutDiTre,
    SUM(CASE WHEN ISNULL(c.VeSomPhut,0)>0 THEN c.VeSomPhut ELSE 0 END) AS TongPhutVeSom
FROM dbo.ChamCong c
GROUP BY c.MaNV, YEAR(c.NgayLam), MONTH(c.NgayLam);
GO

-- 2) Lịch + chấm công theo ngày
IF OBJECT_ID('dbo.vw_Lich_ChamCong_Ngay','V') IS NOT NULL DROP VIEW dbo.vw_Lich_ChamCong_Ngay;
GO
CREATE VIEW dbo.vw_Lich_ChamCong_Ngay
AS
SELECT 
    nv.MaNV, nv.HoTen, lpc.NgayLam,
    cl.TenCa, cl.GioBatDau, cl.GioKetThuc, cl.HeSoCa,
    lpc.TrangThai AS TrangThaiLich,
    cc.GioVao, cc.GioRa, cc.GioCong, cc.DiTrePhut, cc.VeSomPhut, cc.Khoa AS KhoaChamCong
FROM dbo.LichPhanCa lpc
JOIN dbo.NhanVien nv ON nv.MaNV = lpc.MaNV
JOIN dbo.CaLam cl     ON cl.MaCa = lpc.MaCa
LEFT JOIN dbo.ChamCong cc ON cc.MaNV = lpc.MaNV AND cc.NgayLam = lpc.NgayLam;
GO

-- 3) Thông tin nhân viên đầy đủ (bao gồm phòng ban và chức vụ)
IF OBJECT_ID('dbo.vw_NhanVien_Full','V') IS NOT NULL DROP VIEW dbo.vw_NhanVien_Full;
GO
CREATE VIEW dbo.vw_NhanVien_Full
AS
SELECT
    nv.MaNV,
    nv.MaNguoiDung,
    nv.HoTen,
    nv.NgaySinh,
    nv.GioiTinh,
    nv.DienThoai,
    nv.Email,
    nv.DiaChi,
    nv.NgayVaoLam,
    nv.TrangThai,
    nv.LuongCoBan,
    -- Thông tin phòng ban
    pb.MaPhongBan,
    pb.TenPhongBan,
    pb.MoTa AS MoTaPhongBan,
    pb.KichHoat AS PhongBanKichHoat,
    -- Thông tin chức vụ
    cv.MaChucVu,
    cv.TenChucVu,
    cv.MoTa AS MoTaChucVu,
    cv.KichHoat AS ChucVuKichHoat
FROM dbo.NhanVien nv
LEFT JOIN dbo.PhongBan pb ON pb.MaPhongBan = nv.MaPhongBan
LEFT JOIN dbo.ChucVu cv ON cv.MaChucVu = nv.MaChucVu;
GO

-- 4) Báo cáo nhân sự theo phòng ban và chức vụ
IF OBJECT_ID('dbo.vw_BaoCaoNhanSu','V') IS NOT NULL DROP VIEW dbo.vw_BaoCaoNhanSu;
GO
CREATE VIEW dbo.vw_BaoCaoNhanSu
AS
SELECT
    pb.TenPhongBan,
    cv.TenChucVu,
    COUNT(*) AS SoLuongNhanVien,
    AVG(nv.LuongCoBan) AS LuongTrungBinh,
    MIN(nv.LuongCoBan) AS LuongThapNhat,
    MAX(nv.LuongCoBan) AS LuongCaoNhat,
    COUNT(CASE WHEN nv.TrangThai = N'DangLam' THEN 1 END) AS DangLamViec,
    COUNT(CASE WHEN nv.TrangThai = N'Nghi' THEN 1 END) AS DaNghiViec
FROM dbo.NhanVien nv
LEFT JOIN dbo.PhongBan pb ON pb.MaPhongBan = nv.MaPhongBan
LEFT JOIN dbo.ChucVu cv ON cv.MaChucVu = nv.MaChucVu
GROUP BY pb.TenPhongBan, cv.TenChucVu;
GO

-- 5) Đơn từ chi tiết (hiển thị tên nhân viên và người duyệt)
IF OBJECT_ID('dbo.vw_DonTu_ChiTiet','V') IS NOT NULL DROP VIEW dbo.vw_DonTu_ChiTiet;
GO
CREATE VIEW dbo.vw_DonTu_ChiTiet
AS
SELECT
    dt.MaDon,
    dt.MaNV,
    nv.HoTen AS TenNhanVien,
    dt.Loai,
    dt.TuLuc,
    dt.DenLuc,
    dt.SoGio,
    dt.LyDo,
    dt.TrangThai,
    dt.DuyetBoi AS MaNguoiDuyet,
    nd.TenDangNhap AS TenNguoiDuyet
FROM dbo.DonTu dt
JOIN dbo.NhanVien nv ON dt.MaNV = nv.MaNV
LEFT JOIN dbo.NguoiDung nd ON dt.DuyetBoi = nd.MaNguoiDung;
GO

-- 6) Bảng lương chi tiết (hiển thị tên nhân viên, phòng ban, chức danh)
IF OBJECT_ID('dbo.vw_BangLuong_ChiTiet','V') IS NOT NULL DROP VIEW dbo.vw_BangLuong_ChiTiet;
GO
CREATE VIEW dbo.vw_BangLuong_ChiTiet
AS
SELECT
    bl.MaBangLuong,
    bl.Nam,
    bl.Thang,
    bl.MaNV,
    nv.HoTen,
    pb.TenPhongBan AS PhongBan,
    cv.TenChucVu AS ChucDanh,
    bl.LuongCoBan,
    bl.TongGioCong,
    bl.GioOT,
    bl.PhuCap,
    bl.KhauTru,
    bl.ThueBH,
    bl.ThucLanh,
    bl.TrangThai
FROM dbo.BangLuong bl
JOIN dbo.NhanVien nv ON bl.MaNV = nv.MaNV
LEFT JOIN dbo.PhongBan pb ON pb.MaPhongBan = nv.MaPhongBan
LEFT JOIN dbo.ChucVu cv ON cv.MaChucVu = nv.MaChucVu;
GO

------------------------------------------------------------
-- II) FUNCTIONS
------------------------------------------------------------

-- 1) Trả về khung ca của NV trong ngày (giờ bắt đầu/kết thúc, hệ số)
IF OBJECT_ID('dbo.fn_KhungCa','IF') IS NOT NULL DROP FUNCTION dbo.fn_KhungCa;
GO
CREATE FUNCTION dbo.fn_KhungCa(@MaNV INT, @Ngay DATE)
RETURNS TABLE
AS
RETURN
(
    SELECT TOP(1)
        cl.GioBatDau AS GioBatDau,
        cl.GioKetThuc AS GioKetThuc,
        cl.HeSoCa     AS HeSoCa
    FROM dbo.LichPhanCa l
    JOIN dbo.CaLam cl ON cl.MaCa = l.MaCa
    WHERE l.MaNV = @MaNV AND l.NgayLam = @Ngay
);
GO

-- 2) Số phút dương (âm => 0), tiện tính đi trễ/về sớm
IF OBJECT_ID('dbo.fn_SoPhutDuong','FN') IS NOT NULL DROP FUNCTION dbo.fn_SoPhutDuong;
GO
CREATE FUNCTION dbo.fn_SoPhutDuong(@A DATETIME2(0), @B DATETIME2(0))
RETURNS INT
AS
BEGIN
    IF @A IS NULL OR @B IS NULL RETURN NULL;
    DECLARE @m INT = DATEDIFF(MINUTE, @A, @B);
    RETURN CASE WHEN @m > 0 THEN @m ELSE 0 END;
END
GO

PRINT N'=== HOÀN TẤT TẠO VIEWS VÀ FUNCTIONS ===';
PRINT N'Tiếp theo sẽ tạo stored procedures...';
