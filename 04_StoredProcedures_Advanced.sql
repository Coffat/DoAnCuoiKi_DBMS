/* =========================================================
   PHẦN 4: STORED PROCEDURES NÂNG CAO
   Dự án DBMS - HR cho Siêu Thị Mini
   ========================================================= */

USE QLNhanSuSieuThiMini;
GO

-- 3) Duyệt/Từ chối đơn từ (atomic) - ĐÃ MỞ RỘNG ĐỂ ĐỒNG BỘ LICHPHANCA
IF OBJECT_ID('dbo.sp_DuyetDonTu','P') IS NOT NULL DROP PROCEDURE dbo.sp_DuyetDonTu;
GO
CREATE PROCEDURE dbo.sp_DuyetDonTu
    @MaDon        INT,
    @MaNguoiDuyet INT,
    @ChapNhan     BIT      -- 1 = duyệt, 0 = từ chối
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    DECLARE @Loai NVARCHAR(10), @MaNV INT, @Tu DATETIME2(0), @Den DATETIME2(0), @SoGio DECIMAL(5,2);

    BEGIN TRAN;

    SELECT @Loai=Loai, @MaNV=MaNV, @Tu=TuLuc, @Den=DenLuc, @SoGio=SoGio
    FROM dbo.DonTu WITH (UPDLOCK, ROWLOCK)
    WHERE MaDon = @MaDon;

    IF @@ROWCOUNT = 0
    BEGIN
        RAISERROR(N'Không tìm thấy đơn từ.',16,1);
        ROLLBACK; RETURN;
    END

    IF @ChapNhan = 1
    BEGIN
        UPDATE dbo.DonTu
        SET TrangThai = N'DaDuyet',
            DuyetBoi  = @MaNguoiDuyet,
            SoGio     = ISNULL(@SoGio, CAST(DATEDIFF(MINUTE,@Tu,@Den)/60.0 AS DECIMAL(5,2)))
        WHERE MaDon = @MaDon;

        IF @Loai = N'NGHI'
        BEGIN
            -- Set SESSION_CONTEXT để bypass trigger khi cần
            EXEC sp_set_session_context @key = N'SkipTrigger', @value = 1;

            -- Cập nhật ChamCong
            ;WITH Dates AS (
                SELECT CAST(@Tu AS DATE) d
                UNION ALL
                SELECT DATEADD(DAY,1,d) FROM Dates WHERE d < CAST(@Den AS DATE)
            )
            MERGE dbo.ChamCong AS T
            USING (
                SELECT @MaNV AS MaNV, d AS NgayLam FROM Dates
                WHERE EXISTS (SELECT 1 FROM dbo.LichPhanCa l WHERE l.MaNV=@MaNV AND l.NgayLam=d)
            ) AS S
            ON (T.MaNV=S.MaNV AND T.NgayLam=S.NgayLam)
            WHEN MATCHED THEN
                UPDATE SET GioVao=NULL, GioRa=NULL, GioCong=0, DiTrePhut=0, VeSomPhut=0
            WHEN NOT MATCHED THEN
                INSERT(MaNV,NgayLam,GioVao,GioRa,GioCong,DiTrePhut,VeSomPhut,Khoa)
                VALUES(@MaNV, S.NgayLam, NULL, NULL, 0, 0, 0, 0)
            OPTION (MAXRECURSION 366);

            -- Cập nhật LichPhanCa: chuyển sang trạng thái Huy
            ;WITH Dates AS (
                SELECT CAST(@Tu AS DATE) d
                UNION ALL
                SELECT DATEADD(DAY,1,d) FROM Dates WHERE d < CAST(@Den AS DATE)
            )
            UPDATE lpc
            SET lpc.TrangThai = N'Huy',
                lpc.GhiChu = ISNULL(lpc.GhiChu, '') + N' [Nghỉ phép - Đơn #' + CAST(@MaDon AS NVARCHAR) + N']'
            FROM dbo.LichPhanCa lpc
            INNER JOIN Dates ON lpc.NgayLam = Dates.d
            WHERE lpc.MaNV = @MaNV
              AND lpc.TrangThai IN (N'DuKien', N'Khoa')
            OPTION (MAXRECURSION 366);

            -- Reset SESSION_CONTEXT
            EXEC sp_set_session_context @key = N'SkipTrigger', @value = 0;
        END
    END
    ELSE
    BEGIN
        UPDATE dbo.DonTu
        SET TrangThai = N'TuChoi',
            DuyetBoi  = @MaNguoiDuyet
        WHERE MaDon = @MaDon;
    END

    COMMIT;
END
GO

-- 4) Khóa công kỳ (tháng/năm): LichPhanCa.TrangThai='Khoa', ChamCong.Khoa=1
IF OBJECT_ID('dbo.sp_KhoaCongThang','P') IS NOT NULL DROP PROCEDURE dbo.sp_KhoaCongThang;
GO
CREATE PROCEDURE dbo.sp_KhoaCongThang
    @Nam   INT,
    @Thang INT
AS
BEGIN
    SET NOCOUNT ON; SET XACT_ABORT ON;

    DECLARE @D0 DATE = DATEFROMPARTS(@Nam,@Thang,1);
    DECLARE @D1 DATE = EOMONTH(@D0);

    BEGIN TRAN;

    UPDATE dbo.LichPhanCa
      SET TrangThai = N'Khoa'
    WHERE NgayLam BETWEEN @D0 AND @D1 AND TrangThai <> N'Khoa';

    UPDATE dbo.ChamCong
      SET Khoa = 1
    WHERE NgayLam BETWEEN @D0 AND @D1 AND Khoa = 0;

    COMMIT;
END
GO

-- 4.1) Mở khóa công kỳ (tháng/năm): LichPhanCa.TrangThai='DuKien', ChamCong.Khoa=0
IF OBJECT_ID('dbo.sp_MoKhoaCongThang','P') IS NOT NULL DROP PROCEDURE dbo.sp_MoKhoaCongThang;
GO
CREATE PROCEDURE dbo.sp_MoKhoaCongThang
    @Nam   INT,
    @Thang INT
AS
BEGIN
    SET NOCOUNT ON; SET XACT_ABORT ON;

    DECLARE @D0 DATE = DATEFROMPARTS(@Nam,@Thang,1);
    DECLARE @D1 DATE = EOMONTH(@D0);

    BEGIN TRAN;

    UPDATE dbo.LichPhanCa
      SET TrangThai = N'DuKien'
    WHERE NgayLam BETWEEN @D0 AND @D1 AND TrangThai = N'Khoa';

    UPDATE dbo.ChamCong
      SET Khoa = 0
    WHERE NgayLam BETWEEN @D0 AND @D1 AND Khoa = 1;

    COMMIT;
    
    PRINT N'Đã mở khóa công tháng ' + CAST(@Thang AS NVARCHAR) + '/' + CAST(@Nam AS NVARCHAR);
END
GO

-- 5) Chạy bảng lương (Serializable): upsert BangLuong ở trạng thái 'Mo'
IF OBJECT_ID('dbo.sp_ChayBangLuong','P') IS NOT NULL DROP PROCEDURE dbo.sp_ChayBangLuong;
GO
CREATE PROCEDURE dbo.sp_ChayBangLuong
    @Nam INT,
    @Thang INT,
    @StdHours DECIMAL(7,2) = 208,  -- giờ chuẩn/tháng
    @OtRate   DECIMAL(4,2) = 1.5,  -- hệ số OT
    @PhatTre  DECIMAL(5,2) = 0     -- % khấu trừ mỗi phút trễ/sớm (ví dụ: 0.01 = 1% lương/giờ cho mỗi phút)
AS
BEGIN
    SET NOCOUNT ON; SET XACT_ABORT ON;

    DECLARE @D0 DATE = DATEFROMPARTS(@Nam,@Thang,1);
    DECLARE @D1 DATE = EOMONTH(@D0);

    SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;
    BEGIN TRAN;

    -- (Consistency) Bảo đảm đã khóa công
    IF EXISTS (SELECT 1 FROM dbo.LichPhanCa WHERE NgayLam BETWEEN @D0 AND @D1 AND TrangThai <> N'Khoa')
       OR EXISTS (SELECT 1 FROM dbo.ChamCong   WHERE NgayLam BETWEEN @D0 AND @D1 AND Khoa = 0)
    BEGIN
        RAISERROR(N'Chưa khóa công kỳ %d-%02d.',16,1,@Nam,@Thang);
        ROLLBACK; RETURN;
    END

    ;WITH Cong AS (
        SELECT MaNV,
               SUM(ISNULL(TongGioCong,0)) AS TongGioCong,
               SUM(ISNULL(TongPhutDiTre,0) + ISNULL(TongPhutVeSom,0)) AS TongPhutPhat
        FROM dbo.vw_CongThang
        WHERE Nam = @Nam AND Thang = @Thang
        GROUP BY MaNV
    ),
    OT AS (
        SELECT
            r.MaNV,
            SUM(
                CASE
                    WHEN r.Loai = N'OT' AND r.TrangThai = N'DaDuyet'
                    THEN
                        -- Tính toán khoảng thời gian OT thực sự nằm trong tháng
                        CAST(
                            DATEDIFF(
                                MINUTE,
                                -- Lấy thời điểm bắt đầu muộn hơn (giữa TuLuc và đầu tháng)
                                CASE WHEN r.TuLuc > @D0 THEN r.TuLuc ELSE @D0 END,
                                -- Lấy thời điểm kết thúc sớm hơn (giữa DenLuc và cuối tháng)
                                CASE WHEN r.DenLuc < DATEADD(DAY, 1, @D1) THEN r.DenLuc ELSE DATEADD(DAY, 1, @D1) END
                            ) / 60.0
                        AS DECIMAL(7,2))
                    ELSE 0
                END
            ) AS GioOT
        FROM dbo.DonTu r
        -- Lấy tất cả đơn từ có khoảng thời gian giao với tháng đang xét
        WHERE r.TuLuc < DATEADD(DAY, 1, @D1) AND r.DenLuc >= @D0
        GROUP BY r.MaNV
    ),
    Src AS (
        SELECT nv.MaNV,
               nv.LuongCoBan,
               ISNULL(c.TongGioCong,0) AS TongGioCong,
               ISNULL(o.GioOT,0)       AS GioOT,
               ISNULL(c.TongPhutPhat,0) AS TongPhutPhat
        FROM dbo.NhanVien nv
        LEFT JOIN Cong c ON c.MaNV = nv.MaNV
        LEFT JOIN OT   o ON o.MaNV = nv.MaNV
    )
    MERGE dbo.BangLuong AS T
    USING Src AS S
    ON (T.Nam=@Nam AND T.Thang=@Thang AND T.MaNV=S.MaNV)
    WHEN MATCHED AND T.TrangThai=N'Mo' THEN
        UPDATE SET
          T.LuongCoBan  = S.LuongCoBan,
          T.TongGioCong = S.TongGioCong,
          T.GioOT       = S.GioOT,
          T.KhauTru     = T.KhauTru + (S.TongPhutPhat * (S.LuongCoBan / @StdHours / 60) * @PhatTre),
          T.ThucLanh    = (CASE WHEN @StdHours>0 THEN (S.TongGioCong*(S.LuongCoBan/@StdHours)) ELSE S.LuongCoBan END)
                         + (CASE WHEN @StdHours>0 THEN (S.GioOT*(S.LuongCoBan/@StdHours)*@OtRate) ELSE 0 END)
                         + T.PhuCap - T.KhauTru - T.ThueBH
    WHEN NOT MATCHED THEN
        INSERT (Nam,Thang,MaNV,LuongCoBan,TongGioCong,GioOT,PhuCap,KhauTru,ThueBH,ThucLanh,TrangThai)
        VALUES(@Nam,@Thang,S.MaNV,S.LuongCoBan,S.TongGioCong,S.GioOT,0,
               (S.TongPhutPhat * (S.LuongCoBan / @StdHours / 60) * @PhatTre),
               0,
               (CASE WHEN @StdHours>0 THEN (S.TongGioCong*(S.LuongCoBan/@StdHours)) ELSE S.LuongCoBan END)
               + (CASE WHEN @StdHours>0 THEN (S.GioOT*(S.LuongCoBan/@StdHours)*@OtRate) ELSE 0 END),
               N'Mo');

    COMMIT;
END
GO

-- 6) Đóng bảng lương: chuyển 'Mo' → 'Dong'
IF OBJECT_ID('dbo.sp_DongBangLuong','P') IS NOT NULL DROP PROCEDURE dbo.sp_DongBangLuong;
GO
CREATE PROCEDURE dbo.sp_DongBangLuong
    @Nam INT, @Thang INT
AS
BEGIN
    SET NOCOUNT ON; SET XACT_ABORT ON;
    BEGIN TRAN;
      UPDATE dbo.BangLuong SET TrangThai=N'Dong'
      WHERE Nam=@Nam AND Thang=@Thang AND TrangThai=N'Mo';
    COMMIT;
END
GO

-- 7) Check In - Nhân viên chấm công vào
IF OBJECT_ID('dbo.sp_CheckIn','P') IS NOT NULL DROP PROCEDURE dbo.sp_CheckIn;
GO
CREATE PROCEDURE dbo.sp_CheckIn
    @MaNV INT,
    @NgayLam DATE = NULL,
    @GioVao DATETIME2(0) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    -- Mặc định là ngày hôm nay và giờ hiện tại
    IF @NgayLam IS NULL SET @NgayLam = CAST(GETDATE() AS DATE);
    IF @GioVao IS NULL SET @GioVao = GETDATE();

    DECLARE @GioBatDau TIME(0), @GioKetThuc TIME(0), @TenCa NVARCHAR(60);
    DECLARE @GioCheckIn TIME(0) = CAST(@GioVao AS TIME(0));
    DECLARE @GioBatDauToday DATETIME2(0), @GioSomNhat DATETIME2(0);

    BEGIN TRAN;

    -- Kiểm tra nhân viên có lịch làm việc hôm nay không và lấy thông tin ca
    SELECT @GioBatDau = cl.GioBatDau, @GioKetThuc = cl.GioKetThuc, @TenCa = cl.TenCa
    FROM dbo.LichPhanCa lpc
    INNER JOIN dbo.CaLam cl ON lpc.MaCa = cl.MaCa
    WHERE lpc.MaNV = @MaNV AND lpc.NgayLam = @NgayLam AND lpc.TrangThai IN (N'DuKien', N'Mo');

    IF @GioBatDau IS NULL
    BEGIN
        RAISERROR(N'Nhân viên không có lịch làm việc hôm nay hoặc lịch đã bị khóa.', 16, 1);
        ROLLBACK; RETURN;
    END

    -- Tính toán giờ bắt đầu ca hôm nay và giờ sớm nhất được check in (15 phút trước)
    SET @GioBatDauToday = DATETIMEFROMPARTS(YEAR(@NgayLam), MONTH(@NgayLam), DAY(@NgayLam), 
                                           DATEPART(HOUR, @GioBatDau), DATEPART(MINUTE, @GioBatDau), 0, 0);
    SET @GioSomNhat = DATEADD(MINUTE, -15, @GioBatDauToday);

    -- Kiểm tra check in không được sớm quá 15 phút
    IF @GioVao < @GioSomNhat
    BEGIN
        DECLARE @ThongBaoSom NVARCHAR(200) = N'Chỉ được check in sớm tối đa 15 phút trước giờ bắt đầu ca. ' +
                                           N'Ca ' + @TenCa + N' bắt đầu lúc ' + FORMAT(@GioBatDau, 'HH:mm') + 
                                           N'. Có thể check in từ ' + FORMAT(@GioSomNhat, 'HH:mm') + N'.';
        RAISERROR(@ThongBaoSom, 16, 1);
        ROLLBACK; RETURN;
    END

    -- Kiểm tra đã check in chưa
    IF EXISTS (
        SELECT 1 FROM dbo.ChamCong 
        WHERE MaNV = @MaNV AND NgayLam = @NgayLam AND GioVao IS NOT NULL
    )
    BEGIN
        RAISERROR(N'Nhân viên đã check in hôm nay rồi.', 16, 1);
        ROLLBACK; RETURN;
    END

    -- Kiểm tra công đã bị khóa chưa
    IF EXISTS (
        SELECT 1 FROM dbo.ChamCong 
        WHERE MaNV = @MaNV AND NgayLam = @NgayLam AND Khoa = 1
    )
    BEGIN
        RAISERROR(N'Công kỳ này đã bị khóa, không thể check in.', 16, 1);
        ROLLBACK; RETURN;
    END

    -- Upsert vào bảng ChamCong
    MERGE dbo.ChamCong AS T
    USING (SELECT @MaNV AS MaNV, @NgayLam AS NgayLam, @GioVao AS GioVao) AS S
    ON (T.MaNV = S.MaNV AND T.NgayLam = S.NgayLam)
    WHEN MATCHED THEN
        UPDATE SET GioVao = S.GioVao
    WHEN NOT MATCHED THEN
        INSERT (MaNV, NgayLam, GioVao, GioRa, GioCong, DiTrePhut, VeSomPhut, Khoa)
        VALUES (S.MaNV, S.NgayLam, S.GioVao, NULL, 0, 0, 0, 0);

    COMMIT;
    
    -- Trả về thông tin check in
    SELECT 
        @MaNV AS MaNV,
        @NgayLam AS NgayLam,
        @GioVao AS GioVao,
        N'Check in thành công' AS ThongBao;
END
GO

-- 8) Check Out - Nhân viên chấm công ra
IF OBJECT_ID('dbo.sp_CheckOut','P') IS NOT NULL DROP PROCEDURE dbo.sp_CheckOut;
GO
CREATE PROCEDURE dbo.sp_CheckOut
    @MaNV INT,
    @NgayLam DATE = NULL,
    @GioRa DATETIME2(0) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    -- Mặc định là ngày hôm nay và giờ hiện tại
    IF @NgayLam IS NULL SET @NgayLam = CAST(GETDATE() AS DATE);
    IF @GioRa IS NULL SET @GioRa = GETDATE();

    BEGIN TRAN;

    -- Kiểm tra đã check in chưa
    IF NOT EXISTS (
        SELECT 1 FROM dbo.ChamCong 
        WHERE MaNV = @MaNV AND NgayLam = @NgayLam AND GioVao IS NOT NULL
    )
    BEGIN
        RAISERROR(N'Nhân viên chưa check in hôm nay.', 16, 1);
        ROLLBACK; RETURN;
    END

    -- Kiểm tra đã check out chưa
    IF EXISTS (
        SELECT 1 FROM dbo.ChamCong 
        WHERE MaNV = @MaNV AND NgayLam = @NgayLam AND GioRa IS NOT NULL
    )
    BEGIN
        RAISERROR(N'Nhân viên đã check out hôm nay rồi.', 16, 1);
        ROLLBACK; RETURN;
    END

    -- Kiểm tra công đã bị khóa chưa
    IF EXISTS (
        SELECT 1 FROM dbo.ChamCong 
        WHERE MaNV = @MaNV AND NgayLam = @NgayLam AND Khoa = 1
    )
    BEGIN
        RAISERROR(N'Công kỳ này đã bị khóa, không thể check out.', 16, 1);
        ROLLBACK; RETURN;
    END

    -- Kiểm tra giờ ra phải sau giờ vào
    DECLARE @GioVao DATETIME2(0);
    SELECT @GioVao = GioVao FROM dbo.ChamCong 
    WHERE MaNV = @MaNV AND NgayLam = @NgayLam;

    IF @GioRa <= @GioVao
    BEGIN
        RAISERROR(N'Giờ check out phải sau giờ check in.', 16, 1);
        ROLLBACK; RETURN;
    END

    -- Cập nhật giờ ra
    UPDATE dbo.ChamCong 
    SET GioRa = @GioRa
    WHERE MaNV = @MaNV AND NgayLam = @NgayLam;

    COMMIT;
    
    -- Trả về thông tin check out
    SELECT 
        @MaNV AS MaNV,
        @NgayLam AS NgayLam,
        @GioVao AS GioVao,
        @GioRa AS GioRa,
        N'Check out thành công' AS ThongBao;
END
GO

-- 9) Lấy trạng thái check in/out của nhân viên hôm nay
IF OBJECT_ID('dbo.sp_GetTrangThaiChamCong','P') IS NOT NULL DROP PROCEDURE dbo.sp_GetTrangThaiChamCong;
GO
CREATE PROCEDURE dbo.sp_GetTrangThaiChamCong
    @MaNV INT,
    @NgayLam DATE = NULL
AS
BEGIN
    SET NOCOUNT ON;

    IF @NgayLam IS NULL SET @NgayLam = CAST(GETDATE() AS DATE);

    DECLARE @GioHienTai DATETIME2(0) = GETDATE();

    SELECT 
        cc.MaNV,
        cc.NgayLam,
        cc.GioVao,
        cc.GioRa,
        cc.GioCong,
        cc.DiTrePhut,
        cc.VeSomPhut,
        cc.Khoa,
        lpc.MaCa,
        cl.TenCa,
        cl.GioBatDau,
        cl.GioKetThuc,
        lpc.TrangThai AS TrangThaiLich,
        -- Tính giờ sớm nhất được check in (15 phút trước giờ bắt đầu ca)
        DATEADD(MINUTE, -15, 
            DATETIMEFROMPARTS(YEAR(@NgayLam), MONTH(@NgayLam), DAY(@NgayLam), 
                             DATEPART(HOUR, cl.GioBatDau), DATEPART(MINUTE, cl.GioBatDau), 0, 0)
        ) AS GioSomNhatCheckIn,
        CASE 
            WHEN cc.GioVao IS NULL THEN N'ChuaCheckIn'
            WHEN cc.GioRa IS NULL THEN N'DaCheckIn'
            ELSE N'DaCheckOut'
        END AS TrangThaiChamCong,
        CASE 
            WHEN lpc.MaLich IS NULL THEN N'KhongCoLich'
            WHEN lpc.TrangThai = N'Khoa' THEN N'LichDaKhoa'
            WHEN cc.Khoa = 1 THEN N'CongDaKhoa'
            WHEN cc.GioVao IS NULL THEN 
                CASE 
                    WHEN @GioHienTai < DATEADD(MINUTE, -15, 
                        DATETIMEFROMPARTS(YEAR(@NgayLam), MONTH(@NgayLam), DAY(@NgayLam), 
                                         DATEPART(HOUR, cl.GioBatDau), DATEPART(MINUTE, cl.GioBatDau), 0, 0))
                    THEN N'ChuaDenGioCheckIn'
                    ELSE N'CoTheCheckIn'
                END
            WHEN cc.GioRa IS NULL THEN N'CoTheCheckOut'
            ELSE N'DaHoanThanh'
        END AS TrangThaiHanhDong
    FROM dbo.ChamCong cc
    RIGHT JOIN dbo.LichPhanCa lpc ON cc.MaNV = lpc.MaNV AND cc.NgayLam = lpc.NgayLam
    LEFT JOIN dbo.CaLam cl ON lpc.MaCa = cl.MaCa
    WHERE lpc.MaNV = @MaNV AND lpc.NgayLam = @NgayLam;

    -- Nếu không có lịch làm việc hôm nay
    IF @@ROWCOUNT = 0
    BEGIN
        SELECT 
            @MaNV AS MaNV,
            @NgayLam AS NgayLam,
            NULL AS GioVao,
            NULL AS GioRa,
            NULL AS GioCong,
            NULL AS DiTrePhut,
            NULL AS VeSomPhut,
            NULL AS Khoa,
            NULL AS MaCa,
            NULL AS TenCa,
            NULL AS GioBatDau,
            NULL AS GioKetThuc,
            NULL AS GioSomNhatCheckIn,
            NULL AS TrangThaiLich,
            N'KhongCoLich' AS TrangThaiChamCong,
            N'KhongCoLich' AS TrangThaiHanhDong;
    END
END
GO

------------------------------------------------------------
-- 10) SAO CHÉP LỊCH TUẦN
------------------------------------------------------------

IF OBJECT_ID('dbo.sp_LichPhanCa_CloneWeek','P') IS NOT NULL DROP PROCEDURE dbo.sp_LichPhanCa_CloneWeek;
GO
CREATE PROCEDURE dbo.sp_LichPhanCa_CloneWeek
    @NgayBatDauFrom DATE,  -- Thứ Hai tuần nguồn
    @NgayBatDauTo DATE,    -- Thứ Hai tuần đích
    @Overwrite BIT = 0     -- 1 = xóa lịch cũ trước khi copy
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    -- Kiểm tra cả 2 ngày đều là thứ Hai
    IF DATEPART(WEEKDAY, @NgayBatDauFrom) <> 2 OR DATEPART(WEEKDAY, @NgayBatDauTo) <> 2
    BEGIN
        RAISERROR(N'Cả hai ngày phải là thứ Hai (Monday).', 16, 1);
        RETURN;
    END

    DECLARE @NgayKetThucFrom DATE = DATEADD(DAY, 6, @NgayBatDauFrom);
    DECLARE @NgayKetThucTo DATE = DATEADD(DAY, 6, @NgayBatDauTo);

    BEGIN TRAN;

    -- Nếu Overwrite = 1, xóa lịch tuần đích trước
    IF @Overwrite = 1
    BEGIN
        DELETE FROM dbo.LichPhanCa
        WHERE NgayLam BETWEEN @NgayBatDauTo AND @NgayKetThucTo
          AND TrangThai = N'DuKien';
    END

    -- Copy lịch từ tuần nguồn sang tuần đích
    INSERT INTO dbo.LichPhanCa (MaNV, NgayLam, MaCa, TrangThai, GhiChu)
    SELECT 
        lpc.MaNV,
        DATEADD(DAY, DATEDIFF(DAY, @NgayBatDauFrom, lpc.NgayLam), @NgayBatDauTo) AS NgayLamMoi,
        lpc.MaCa,
        N'DuKien' AS TrangThai,
        lpc.GhiChu
    FROM dbo.LichPhanCa lpc
    WHERE lpc.NgayLam BETWEEN @NgayBatDauFrom AND @NgayKetThucFrom
      AND lpc.TrangThai IN (N'DuKien', N'Khoa')
      AND (@Overwrite = 1 OR NOT EXISTS (
          SELECT 1 FROM dbo.LichPhanCa lpc2
          WHERE lpc2.MaNV = lpc.MaNV
            AND lpc2.NgayLam = DATEADD(DAY, DATEDIFF(DAY, @NgayBatDauFrom, lpc.NgayLam), @NgayBatDauTo)
            AND lpc2.MaCa = lpc.MaCa
      ));

    DECLARE @RowsInserted INT = @@ROWCOUNT;
    COMMIT;

    SELECT @RowsInserted AS SoLichDaCopy;
END
GO

------------------------------------------------------------
-- 11) KHÓA/MỞ KHÓA LỊCH THEO TUẦN
------------------------------------------------------------

-- 11.1) Khóa lịch tuần
IF OBJECT_ID('dbo.sp_LichPhanCa_KhoaTuan','P') IS NOT NULL DROP PROCEDURE dbo.sp_LichPhanCa_KhoaTuan;
GO
CREATE PROCEDURE dbo.sp_LichPhanCa_KhoaTuan
    @MaNV INT,
    @NgayBatDau DATE  -- Thứ Hai
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    IF DATEPART(WEEKDAY, @NgayBatDau) <> 2
    BEGIN
        RAISERROR(N'Ngày bắt đầu phải là thứ Hai (Monday).', 16, 1);
        RETURN;
    END

    DECLARE @NgayKetThuc DATE = DATEADD(DAY, 6, @NgayBatDau);

    BEGIN TRAN;

    EXEC sp_set_session_context @key = N'SkipTrigger', @value = 0;

    UPDATE dbo.LichPhanCa
    SET TrangThai = N'Khoa'
    WHERE MaNV = @MaNV
      AND NgayLam BETWEEN @NgayBatDau AND @NgayKetThuc
      AND TrangThai = N'DuKien';

    DECLARE @RowsUpdated INT = @@ROWCOUNT;
    COMMIT;

    SELECT @RowsUpdated AS SoLichDaKhoa;
END
GO

-- 11.2) Mở khóa lịch tuần
IF OBJECT_ID('dbo.sp_LichPhanCa_MoKhoaTuan','P') IS NOT NULL DROP PROCEDURE dbo.sp_LichPhanCa_MoKhoaTuan;
GO
CREATE PROCEDURE dbo.sp_LichPhanCa_MoKhoaTuan
    @MaNV INT,
    @NgayBatDau DATE,
    @MaNguoiDung INT
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    -- Kiểm tra quyền
    DECLARE @VaiTro NVARCHAR(20);
    SELECT @VaiTro = VaiTro FROM dbo.NguoiDung WHERE MaNguoiDung = @MaNguoiDung;

    IF @VaiTro NOT IN (N'HR', N'QuanLy')
    BEGIN
        RAISERROR(N'Chỉ HR hoặc Quản lý mới có quyền mở khóa lịch.', 16, 1);
        RETURN;
    END

    IF DATEPART(WEEKDAY, @NgayBatDau) <> 2
    BEGIN
        RAISERROR(N'Ngày bắt đầu phải là thứ Hai (Monday).', 16, 1);
        RETURN;
    END

    DECLARE @NgayKetThuc DATE = DATEADD(DAY, 6, @NgayBatDau);

    BEGIN TRAN;

    EXEC sp_set_session_context @key = N'SkipTrigger', @value = 1;

    UPDATE dbo.LichPhanCa
    SET TrangThai = N'DuKien',
        GhiChu = ISNULL(GhiChu, '') + N' [Mở khóa bởi ' + CAST(@MaNguoiDung AS NVARCHAR) + N' lúc ' + CONVERT(NVARCHAR, GETDATE(), 120) + N']'
    WHERE MaNV = @MaNV
      AND NgayLam BETWEEN @NgayBatDau AND @NgayKetThuc
      AND TrangThai = N'Khoa';

    DECLARE @RowsUpdated INT = @@ROWCOUNT;

    EXEC sp_set_session_context @key = N'SkipTrigger', @value = 0;

    COMMIT;

    SELECT @RowsUpdated AS SoLichDaMoKhoa;
END
GO

------------------------------------------------------------
-- 12) QUẢN LÝ TÀI KHOẢN 2 LỚP (SQL SERVER LOGIN + DATABASE USER)
------------------------------------------------------------

/*
   MÔ HÌNH BẢO MẬT 2 LỚP:
   - Lớp 1: Tài khoản trong bảng NguoiDung (application level)
   - Lớp 2: SQL Server Login + Database User (database level)
   
   Mỗi tài khoản sẽ có:
   1. Bản ghi trong bảng NguoiDung
   2. SQL Server Login để xác thực
   3. Database User ánh xạ với Login
   4. Role membership tương ứng với vai trò
*/

-- 12.1) TẠO TÀI KHOẢN ĐẦY ĐỦ (Application + SQL Server Level)
IF OBJECT_ID('dbo.sp_TaoTaiKhoanDayDu','P') IS NOT NULL DROP PROCEDURE dbo.sp_TaoTaiKhoanDayDu;
GO
CREATE PROCEDURE dbo.sp_TaoTaiKhoanDayDu
    -- Các tham số cho bảng NhanVien
    @HoTen NVARCHAR(120),
    @NgaySinh DATE = NULL,
    @GioiTinh NVARCHAR(10) = NULL,
    @DienThoai NVARCHAR(20) = NULL,
    @Email NVARCHAR(120) = NULL,
    @DiaChi NVARCHAR(255) = NULL,
    @NgayVaoLam DATE = NULL,
    @MaPhongBan INT = NULL,
    @MaChucVu INT = NULL,
    @LuongCoBan DECIMAL(12,2),
    -- Các tham số cho tài khoản
    @TenDangNhap NVARCHAR(50),
    @MatKhau NVARCHAR(200),  -- Nhận mật khẩu gốc, chưa mã hóa
    @VaiTro NVARCHAR(20),
    @MaNV_OUT INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    BEGIN TRY
        BEGIN TRAN;

        DECLARE @SqlCmd NVARCHAR(MAX);
        DECLARE @HashedPassword VARBINARY(32) = HASHBYTES('SHA2_256', @MatKhau);
        DECLARE @MaNV_Temp INT;

        -- BƯỚC 1: Kiểm tra tên đăng nhập đã tồn tại chưa
        IF EXISTS (SELECT 1 FROM dbo.NguoiDung WHERE TenDangNhap = @TenDangNhap)
        BEGIN
            RAISERROR(N'Tên đăng nhập đã tồn tại trong bảng NguoiDung.', 16, 1);
            RETURN;
        END

        -- Kiểm tra SQL Login đã tồn tại chưa
        IF EXISTS (SELECT 1 FROM sys.server_principals WHERE name = @TenDangNhap AND type = 'S')
        BEGIN
            RAISERROR(N'Tên đăng nhập đã tồn tại trong SQL Server.', 16, 1);
            RETURN;
        END

        -- BƯỚC 2: Thêm nhân viên và người dùng vào các bảng
        EXEC dbo.sp_ThemMoiNhanVien
            @HoTen = @HoTen,
            @NgaySinh = @NgaySinh,
            @GioiTinh = @GioiTinh,
            @DienThoai = @DienThoai,
            @Email = @Email,
            @DiaChi = @DiaChi,
            @NgayVaoLam = @NgayVaoLam,
            @MaPhongBan = @MaPhongBan,
            @MaChucVu = @MaChucVu,
            @LuongCoBan = @LuongCoBan,
            @TaoTaiKhoan = 1,
            @TenDangNhap = @TenDangNhap,
            @MatKhauHash = @HashedPassword,
            @VaiTro = @VaiTro,
            @MaNV_OUT = @MaNV_Temp OUTPUT;

        SET @MaNV_OUT = @MaNV_Temp;

        -- BƯỚC 3: Tạo SQL Login cho người dùng mới
        SET @SqlCmd = N'CREATE LOGIN ' + QUOTENAME(@TenDangNhap) + 
                      N' WITH PASSWORD = ' + QUOTENAME(@MatKhau, '''') + 
                      N', DEFAULT_DATABASE = QLNhanSuSieuThiMini, CHECK_POLICY = OFF';
        EXEC sp_executesql @SqlCmd;

        -- BƯỚC 4: Tạo Database User và ánh xạ với Login vừa tạo
        SET @SqlCmd = N'USE QLNhanSuSieuThiMini; CREATE USER ' + QUOTENAME(@TenDangNhap) + 
                      N' FOR LOGIN ' + QUOTENAME(@TenDangNhap);
        EXEC sp_executesql @SqlCmd;

        -- BƯỚC 5: Thêm User vào Role tương ứng
        DECLARE @RoleName SYSNAME = 
            CASE @VaiTro
                WHEN N'HR' THEN N'r_hr'
                WHEN N'QuanLy' THEN N'r_quanly'
                WHEN N'KeToan' THEN N'r_ketoan'
                ELSE N'r_nhanvien'
            END;
        
        SET @SqlCmd = N'ALTER ROLE ' + QUOTENAME(@RoleName) + N' ADD MEMBER ' + QUOTENAME(@TenDangNhap);
        EXEC sp_executesql @SqlCmd;

        COMMIT;
        
        PRINT N'Đã tạo tài khoản đầy đủ cho: ' + @TenDangNhap + N' (Vai trò: ' + @VaiTro + N')';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK;
        
        DECLARE @ErrorMsg NVARCHAR(4000) = ERROR_MESSAGE();
        DECLARE @ErrorSeverity INT = ERROR_SEVERITY();
        DECLARE @ErrorState INT = ERROR_STATE();
        
        RAISERROR(@ErrorMsg, @ErrorSeverity, @ErrorState);
    END CATCH
END
GO

-- 12.2) CẬP NHẬT TÀI KHOẢN ĐẦY ĐỦ
IF OBJECT_ID('dbo.sp_CapNhatTaiKhoanDayDu','P') IS NOT NULL DROP PROCEDURE dbo.sp_CapNhatTaiKhoanDayDu;
GO
CREATE PROCEDURE dbo.sp_CapNhatTaiKhoanDayDu
    @MaNV INT,
    @HoTen NVARCHAR(120),
    @NgaySinh DATE = NULL,
    @GioiTinh NVARCHAR(10) = NULL,
    @DienThoai NVARCHAR(20) = NULL,
    @Email NVARCHAR(120) = NULL,
    @DiaChi NVARCHAR(255) = NULL,
    @MaPhongBan INT = NULL,
    @MaChucVu INT = NULL,
    @LuongCoBan DECIMAL(12,2),
    @VaiTro NVARCHAR(20),
    @MatKhauMoi NVARCHAR(200) = NULL  -- Để NULL nếu không muốn đổi mật khẩu
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    BEGIN TRY
        DECLARE @TenDangNhap NVARCHAR(50), @VaiTroCu NVARCHAR(20), @MaNguoiDung INT;
        
        SELECT @TenDangNhap = nd.TenDangNhap, @VaiTroCu = nd.VaiTro, @MaNguoiDung = nd.MaNguoiDung
        FROM dbo.NhanVien nv 
        JOIN dbo.NguoiDung nd ON nv.MaNguoiDung = nd.MaNguoiDung
        WHERE nv.MaNV = @MaNV;

        IF @TenDangNhap IS NULL
        BEGIN
            RAISERROR(N'Không tìm thấy tài khoản cho nhân viên này.', 16, 1);
            RETURN;
        END

        BEGIN TRAN;

        -- BƯỚC 1: Cập nhật thông tin trong bảng NhanVien
        UPDATE dbo.NhanVien
        SET HoTen = @HoTen,
            NgaySinh = @NgaySinh,
            GioiTinh = @GioiTinh,
            DienThoai = @DienThoai,
            Email = @Email,
            DiaChi = @DiaChi,
            MaPhongBan = @MaPhongBan,
            MaChucVu = @MaChucVu,
            LuongCoBan = @LuongCoBan
        WHERE MaNV = @MaNV;

        -- Cập nhật vai trò trong bảng NguoiDung
        UPDATE dbo.NguoiDung
        SET VaiTro = @VaiTro
        WHERE MaNguoiDung = @MaNguoiDung;

        -- BƯỚC 2: Nếu có mật khẩu mới, cập nhật mật khẩu
        IF @MatKhauMoi IS NOT NULL AND LTRIM(RTRIM(@MatKhauMoi)) <> ''
        BEGIN
            DECLARE @SqlCmd NVARCHAR(MAX);
            
            -- Cập nhật SQL Login password
            SET @SqlCmd = N'ALTER LOGIN ' + QUOTENAME(@TenDangNhap) + 
                          N' WITH PASSWORD = ' + QUOTENAME(@MatKhauMoi, '''');
            EXEC sp_executesql @SqlCmd;
            
            -- Cập nhật hash trong bảng NguoiDung
            UPDATE dbo.NguoiDung 
            SET MatKhauHash = HASHBYTES('SHA2_256', @MatKhauMoi) 
            WHERE MaNguoiDung = @MaNguoiDung;
        END

        -- BƯỚC 3: Nếu vai trò thay đổi, cập nhật Role membership
        IF @VaiTroCu <> @VaiTro
        BEGIN
            DECLARE @RoleCu SYSNAME = 
                CASE @VaiTroCu
                    WHEN N'HR' THEN N'r_hr'
                    WHEN N'QuanLy' THEN N'r_quanly'
                    WHEN N'KeToan' THEN N'r_ketoan'
                    ELSE N'r_nhanvien'
                END;
                
            DECLARE @RoleMoi SYSNAME = 
                CASE @VaiTro
                    WHEN N'HR' THEN N'r_hr'
                    WHEN N'QuanLy' THEN N'r_quanly'
                    WHEN N'KeToan' THEN N'r_ketoan'
                    ELSE N'r_nhanvien'
                END;

            -- Xóa khỏi role cũ
            SET @SqlCmd = N'ALTER ROLE ' + QUOTENAME(@RoleCu) + N' DROP MEMBER ' + QUOTENAME(@TenDangNhap);
            EXEC sp_executesql @SqlCmd;
            
            -- Thêm vào role mới
            SET @SqlCmd = N'ALTER ROLE ' + QUOTENAME(@RoleMoi) + N' ADD MEMBER ' + QUOTENAME(@TenDangNhap);
            EXEC sp_executesql @SqlCmd;
            
            PRINT N'Đã chuyển vai trò từ ' + @VaiTroCu + N' sang ' + @VaiTro + N' cho: ' + @TenDangNhap;
        END

        COMMIT;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK;
        
        DECLARE @ErrorMsg NVARCHAR(4000) = ERROR_MESSAGE();
        DECLARE @ErrorSeverity INT = ERROR_SEVERITY();
        DECLARE @ErrorState INT = ERROR_STATE();
        
        RAISERROR(@ErrorMsg, @ErrorSeverity, @ErrorState);
    END CATCH
END
GO

-- 12.3) XÓA TÀI KHOẢN ĐẦY ĐỦ
IF OBJECT_ID('dbo.sp_XoaTaiKhoanDayDu','P') IS NOT NULL DROP PROCEDURE dbo.sp_XoaTaiKhoanDayDu;
GO
CREATE PROCEDURE dbo.sp_XoaTaiKhoanDayDu
    @MaNV INT
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    BEGIN TRY
        DECLARE @TenDangNhap NVARCHAR(50), @MaNguoiDung INT;
        
        SELECT @TenDangNhap = nd.TenDangNhap, @MaNguoiDung = nv.MaNguoiDung
        FROM dbo.NhanVien nv 
        JOIN dbo.NguoiDung nd ON nv.MaNguoiDung = nd.MaNguoiDung
        WHERE nv.MaNV = @MaNV;

        IF @TenDangNhap IS NULL
        BEGIN
            RAISERROR(N'Không tìm thấy tài khoản để xóa.', 16, 1);
            RETURN;
        END

        BEGIN TRAN;

        DECLARE @SqlCmd NVARCHAR(MAX);

        -- BƯỚC 1: Xóa Database User (phải xóa trước Login)
        SET @SqlCmd = N'IF EXISTS (SELECT 1 FROM sys.database_principals WHERE name = ''' + 
                      @TenDangNhap + ''' AND type = ''S'') ' +
                      N'DROP USER ' + QUOTENAME(@TenDangNhap);
        EXEC sp_executesql @SqlCmd;

        -- BƯỚC 2: Xóa SQL Login
        SET @SqlCmd = N'IF EXISTS (SELECT 1 FROM sys.server_principals WHERE name = ''' + 
                      @TenDangNhap + ''' AND type = ''S'') ' +
                      N'DROP LOGIN ' + QUOTENAME(@TenDangNhap);
        EXEC sp_executesql @SqlCmd;
        
        -- BƯỚC 3: Xóa dữ liệu trong các bảng (CASCADE sẽ tự động xử lý)
        DELETE FROM dbo.NhanVien WHERE MaNV = @MaNV;
        DELETE FROM dbo.NguoiDung WHERE MaNguoiDung = @MaNguoiDung;

        COMMIT;
        
        PRINT N'Đã xóa hoàn toàn tài khoản: ' + @TenDangNhap;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK;
        
        DECLARE @ErrorMsg NVARCHAR(4000) = ERROR_MESSAGE();
        DECLARE @ErrorSeverity INT = ERROR_SEVERITY();
        DECLARE @ErrorState INT = ERROR_STATE();
        
        RAISERROR(@ErrorMsg, @ErrorSeverity, @ErrorState);
    END CATCH
END
GO

-- 12.4) VÔ HIỆU HÓA/KÍCH HOẠT TÀI KHOẢN (DISABLE/ENABLE LOGIN)
IF OBJECT_ID('dbo.sp_VoHieuHoaTaiKhoan','P') IS NOT NULL DROP PROCEDURE dbo.sp_VoHieuHoaTaiKhoan;
GO
CREATE PROCEDURE dbo.sp_VoHieuHoaTaiKhoan
    @MaNV INT,
    @KichHoat BIT  -- 1 = Enable, 0 = Disable
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    BEGIN TRY
        DECLARE @TenDangNhap NVARCHAR(50), @MaNguoiDung INT;
        
        SELECT @TenDangNhap = nd.TenDangNhap, @MaNguoiDung = nd.MaNguoiDung
        FROM dbo.NhanVien nv 
        JOIN dbo.NguoiDung nd ON nv.MaNguoiDung = nd.MaNguoiDung
        WHERE nv.MaNV = @MaNV;

        IF @TenDangNhap IS NULL
        BEGIN
            RAISERROR(N'Không tìm thấy tài khoản.', 16, 1);
            RETURN;
        END

        BEGIN TRAN;

        -- Cập nhật trạng thái trong bảng NguoiDung
        UPDATE dbo.NguoiDung
        SET KichHoat = @KichHoat
        WHERE MaNguoiDung = @MaNguoiDung;

        -- Enable/Disable SQL Login
        DECLARE @SqlCmd NVARCHAR(MAX);
        IF @KichHoat = 1
        BEGIN
            SET @SqlCmd = N'ALTER LOGIN ' + QUOTENAME(@TenDangNhap) + N' ENABLE';
            PRINT N'Đã kích hoạt tài khoản: ' + @TenDangNhap;
        END
        ELSE
        BEGIN
            SET @SqlCmd = N'ALTER LOGIN ' + QUOTENAME(@TenDangNhap) + N' DISABLE';
            PRINT N'Đã vô hiệu hóa tài khoản: ' + @TenDangNhap;
        END
        
        EXEC sp_executesql @SqlCmd;

        COMMIT;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK;
        
        DECLARE @ErrorMsg NVARCHAR(4000) = ERROR_MESSAGE();
        DECLARE @ErrorSeverity INT = ERROR_SEVERITY();
        DECLARE @ErrorState INT = ERROR_STATE();
        
        RAISERROR(@ErrorMsg, @ErrorSeverity, @ErrorState);
    END CATCH
END
GO

PRINT N'';
PRINT N'=== HOÀN TẤT TẠO STORED PROCEDURES NÂNG CAO ===';
PRINT N'=== ĐÃ THÊM STORED PROCEDURES CHECK IN/CHECK OUT ===';
PRINT N'=== ĐÃ THÊM STORED PROCEDURES QUẢN LÝ LỊCH TUẦN ===';
PRINT N'=== ĐÃ THÊM STORED PROCEDURES QUẢN LÝ TÀI KHOẢN 2 LỚP ===';
PRINT N'';
PRINT N'Stored Procedures Quản Lý Tài Khoản 2 Lớp:';
PRINT N'  - sp_TaoTaiKhoanDayDu: Tạo tài khoản (App + SQL Login + User + Role)';
PRINT N'  - sp_CapNhatTaiKhoanDayDu: Cập nhật thông tin, đổi mật khẩu, đổi vai trò';
PRINT N'  - sp_XoaTaiKhoanDayDu: Xóa hoàn toàn tài khoản ở cả 2 lớp';
PRINT N'  - sp_VoHieuHoaTaiKhoan: Enable/Disable tài khoản';
PRINT N'';
PRINT N'Tiếp theo chạy file: 05_Security_Triggers.sql';
