/* =========================================================
   PHẦN 4: STORED PROCEDURES NÂNG CAO
   Dự án DBMS - HR cho Siêu Thị Mini
   ========================================================= */

USE QLNhanSuSieuThiMini;
GO

-- 3) Duyệt/Từ chối đơn từ (atomic)
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

-- 5) Chạy bảng lương (Serializable): upsert BangLuong ở trạng thái 'Mo'
IF OBJECT_ID('dbo.sp_ChayBangLuong','P') IS NOT NULL DROP PROCEDURE dbo.sp_ChayBangLuong;
GO
CREATE PROCEDURE dbo.sp_ChayBangLuong
    @Nam INT,
    @Thang INT,
    @StdHours DECIMAL(7,2) = 208,  -- giờ chuẩn/tháng
    @OtRate   DECIMAL(4,2) = 1.5   -- hệ số OT
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
        SELECT MaNV, SUM(ISNULL(GioCong,0)) AS TongGioCong
        FROM dbo.ChamCong
        WHERE NgayLam BETWEEN @D0 AND @D1
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
               ISNULL(o.GioOT,0)       AS GioOT
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
          T.ThucLanh    = S.LuongCoBan 
                         + (CASE WHEN @StdHours>0 THEN (S.GioOT*(S.LuongCoBan/@StdHours)*@OtRate) ELSE 0 END)
                         + T.PhuCap - T.KhauTru - T.ThueBH
    WHEN NOT MATCHED THEN
        INSERT (Nam,Thang,MaNV,LuongCoBan,TongGioCong,GioOT,PhuCap,KhauTru,ThueBH,ThucLanh,TrangThai)
        VALUES(@Nam,@Thang,S.MaNV,S.LuongCoBan,S.TongGioCong,S.GioOT,0,0,0,
               S.LuongCoBan + (CASE WHEN @StdHours>0 THEN (S.GioOT*(S.LuongCoBan/@StdHours)*@OtRate) ELSE 0 END),
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

PRINT N'=== HOÀN TẤT TẠO STORED PROCEDURES NÂNG CAO ===';
PRINT N'Tiếp theo chạy file: 05_Security_Triggers.sql';
