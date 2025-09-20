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
