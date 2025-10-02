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

-- 3) TVF xem lịch theo tuần (7 dòng Mon-Sun)
IF OBJECT_ID('dbo.tvf_LichTheoTuan','IF') IS NOT NULL DROP FUNCTION dbo.tvf_LichTheoTuan;
GO
CREATE FUNCTION dbo.tvf_LichTheoTuan
(
    @MaNV INT,
    @NgayBatDau DATE  -- Phải là thứ Hai
)
RETURNS TABLE
AS
RETURN
(
    WITH Days AS (
        SELECT 0 AS DayOffset UNION ALL SELECT 1 UNION ALL SELECT 2 UNION ALL SELECT 3
        UNION ALL SELECT 4 UNION ALL SELECT 5 UNION ALL SELECT 6
    )
    SELECT 
        DATEADD(DAY, d.DayOffset, @NgayBatDau) AS Ngay,
        @MaNV AS MaNV,
        lpc.MaLich AS IdLich,
        lpc.MaCa,
        lpc.TrangThai,
        lpc.GhiChu,
        cl.TenCa,
        cl.GioBatDau,
        cl.GioKetThuc,
        cl.HeSoCa
    FROM Days d
    LEFT JOIN dbo.LichPhanCa lpc ON lpc.MaNV = @MaNV 
        AND lpc.NgayLam = DATEADD(DAY, d.DayOffset, @NgayBatDau)
    LEFT JOIN dbo.CaLam cl ON cl.MaCa = lpc.MaCa
);
GO

-- 4) Tính tổng lương thực nhận của nhân viên trong khoảng thời gian
IF OBJECT_ID('dbo.fn_TongLuongThang','FN') IS NOT NULL DROP FUNCTION dbo.fn_TongLuongThang;
GO
CREATE FUNCTION dbo.fn_TongLuongThang(@MaNV INT, @Nam INT, @Thang INT)
RETURNS DECIMAL(15,2)
AS
BEGIN
    DECLARE @TongLuong DECIMAL(15,2) = 0;
    
    SELECT @TongLuong = ISNULL(SUM(ThucLanh), 0)
    FROM dbo.BangLuong 
    WHERE MaNV = @MaNV AND Nam = @Nam AND Thang = @Thang AND TrangThai = N'DaThanhToan';
    
    RETURN @TongLuong;
END
GO

-- 5) Tính số ngày đi làm thực tế trong tháng
IF OBJECT_ID('dbo.fn_SoNgayLamViec','FN') IS NOT NULL DROP FUNCTION dbo.fn_SoNgayLamViec;
GO
CREATE FUNCTION dbo.fn_SoNgayLamViec(@MaNV INT, @Nam INT, @Thang INT)
RETURNS INT
AS
BEGIN
    DECLARE @SoNgay INT = 0;
    
    SELECT @SoNgay = COUNT(DISTINCT NgayLam)
    FROM dbo.ChamCong 
    WHERE MaNV = @MaNV 
    AND YEAR(NgayLam) = @Nam 
    AND MONTH(NgayLam) = @Thang
    AND GioCong > 0;
    
    RETURN @SoNgay;
END
GO

-- 6) Tính tỷ lệ đi trễ của nhân viên (%)
IF OBJECT_ID('dbo.fn_TyLeDiTre','FN') IS NOT NULL DROP FUNCTION dbo.fn_TyLeDiTre;
GO
CREATE FUNCTION dbo.fn_TyLeDiTre(@MaNV INT, @Nam INT, @Thang INT)
RETURNS DECIMAL(5,2)
AS
BEGIN
    DECLARE @TongNgay INT = 0;
    DECLARE @NgayDiTre INT = 0;
    DECLARE @TyLe DECIMAL(5,2) = 0;
    
    SELECT 
        @TongNgay = COUNT(*),
        @NgayDiTre = COUNT(CASE WHEN DiTrePhut > 0 THEN 1 END)
    FROM dbo.ChamCong 
    WHERE MaNV = @MaNV 
    AND YEAR(NgayLam) = @Nam 
    AND MONTH(NgayLam) = @Thang;
    
    IF @TongNgay > 0
        SET @TyLe = (@NgayDiTre * 100.0) / @TongNgay;
    
    RETURN @TyLe;
END
GO

-- 7) Tính tuổi của nhân viên
IF OBJECT_ID('dbo.fn_TinhTuoi','FN') IS NOT NULL DROP FUNCTION dbo.fn_TinhTuoi;
GO
CREATE FUNCTION dbo.fn_TinhTuoi(@NgaySinh DATE)
RETURNS INT
AS
BEGIN
    RETURN DATEDIFF(YEAR, @NgaySinh, GETDATE()) - 
           CASE WHEN DATEADD(YEAR, DATEDIFF(YEAR, @NgaySinh, GETDATE()), @NgaySinh) > GETDATE() 
                THEN 1 ELSE 0 END;
END
GO

-- 8) TVF - Danh sách nhân viên theo phòng ban với thông tin chi tiết
IF OBJECT_ID('dbo.tvf_NhanVienTheoPhongBan','IF') IS NOT NULL DROP FUNCTION dbo.tvf_NhanVienTheoPhongBan;
GO
CREATE FUNCTION dbo.tvf_NhanVienTheoPhongBan(@MaPhongBan INT)
RETURNS TABLE
AS
RETURN
(
    SELECT 
        nv.MaNV,
        nv.HoTen,
        nv.NgaySinh,
        dbo.fn_TinhTuoi(nv.NgaySinh) AS Tuoi,
        nv.GioiTinh,
        nv.DienThoai,
        nv.Email,
        nv.NgayVaoLam,
        DATEDIFF(DAY, nv.NgayVaoLam, GETDATE()) AS SoNgayLamViec,
        nv.LuongCoBan,
        cv.TenChucVu,
        nv.TrangThai
    FROM dbo.NhanVien nv
    LEFT JOIN dbo.ChucVu cv ON cv.MaChucVu = nv.MaChucVu
    WHERE nv.MaPhongBan = @MaPhongBan
);
GO

-- 9) TVF - Báo cáo chấm công chi tiết theo tháng
IF OBJECT_ID('dbo.tvf_BaoCaoChamCongThang','IF') IS NOT NULL DROP FUNCTION dbo.tvf_BaoCaoChamCongThang;
GO
CREATE FUNCTION dbo.tvf_BaoCaoChamCongThang(@Nam INT, @Thang INT)
RETURNS TABLE
AS
RETURN
(
    SELECT 
        nv.MaNV,
        nv.HoTen,
        pb.TenPhongBan,
        cv.TenChucVu,
        dbo.fn_SoNgayLamViec(nv.MaNV, @Nam, @Thang) AS SoNgayLam,
        ISNULL(SUM(cc.GioCong), 0) AS TongGioCong,
        ISNULL(SUM(cc.DiTrePhut), 0) AS TongPhutDiTre,
        ISNULL(SUM(cc.VeSomPhut), 0) AS TongPhutVeSom,
        dbo.fn_TyLeDiTre(nv.MaNV, @Nam, @Thang) AS TyLeDiTre,
        COUNT(cc.NgayLam) AS SoLanChamCong
    FROM dbo.NhanVien nv
    LEFT JOIN dbo.PhongBan pb ON pb.MaPhongBan = nv.MaPhongBan
    LEFT JOIN dbo.ChucVu cv ON cv.MaChucVu = nv.MaChucVu
    LEFT JOIN dbo.ChamCong cc ON cc.MaNV = nv.MaNV 
        AND YEAR(cc.NgayLam) = @Nam 
        AND MONTH(cc.NgayLam) = @Thang
    WHERE nv.TrangThai = N'DangLam'
    GROUP BY nv.MaNV, nv.HoTen, pb.TenPhongBan, cv.TenChucVu
);
GO

-- 10) TVF - Lịch sử đơn từ của nhân viên
IF OBJECT_ID('dbo.tvf_LichSuDonTuNhanVien','IF') IS NOT NULL DROP FUNCTION dbo.tvf_LichSuDonTuNhanVien;
GO
CREATE FUNCTION dbo.tvf_LichSuDonTuNhanVien(@MaNV INT, @SoThangGanNhat INT = 6)
RETURNS TABLE
AS
RETURN
(
    SELECT 
        dt.MaDon,
        dt.Loai,
        dt.TuLuc,
        dt.DenLuc,
        dt.SoGio,
        dt.LyDo,
        dt.TrangThai,
        dt.TuLuc AS NgayTao,  -- ✅ SỬA: Sử dụng TuLuc thay cho NgayTao
        nd.TenDangNhap AS NguoiDuyet,
        DATEDIFF(DAY, dt.TuLuc, GETDATE()) AS SoNgayTuTao  -- ✅ SỬA: Sử dụng TuLuc
    FROM dbo.DonTu dt
    LEFT JOIN dbo.NguoiDung nd ON nd.MaNguoiDung = dt.DuyetBoi
    WHERE dt.MaNV = @MaNV
    AND dt.TuLuc >= DATEADD(MONTH, -@SoThangGanNhat, GETDATE())  -- ✅ SỬA: Sử dụng TuLuc
);
GO

PRINT N'=== HOÀN TẤT TẠO VIEWS VÀ FUNCTIONS ===';
PRINT N'Đã bổ sung thêm 4 scalar functions và 3 table-valued functions';
PRINT N'Tổng cộng: 5 scalar functions và 5 table-valued functions';
PRINT N'Tiếp theo sẽ tạo stored procedures...';
