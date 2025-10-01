/* =========================================================
   DEMO: TẠO TÀI KHOẢN MẪU CHO HỆ THỐNG BẢO MẬT 2 LỚP
   
   File này tạo các tài khoản mẫu để test hệ thống.
   Mỗi tài khoản sẽ có:
   - Bản ghi trong NguoiDung + NhanVien
   - SQL Server Login
   - Database User
   - Role membership
   ========================================================= */

USE QLNhanSuSieuThiMini;
GO

PRINT N'';
PRINT N'========================================';
PRINT N'   TẠO TÀI KHOẢN MẪU - BẢO MẬT 2 LỚP';
PRINT N'========================================';
PRINT N'';

-- Lưu ý: Cần đảm bảo đã có PhongBan và ChucVu trước khi chạy
-- Kiểm tra
IF NOT EXISTS (SELECT 1 FROM dbo.PhongBan WHERE MaPhongBan = 1)
BEGIN
    INSERT INTO dbo.PhongBan (TenPhongBan, MoTa, KichHoat)
    VALUES 
        (N'Nhân Sự', N'Phòng Quản lý nhân sự', 1),
        (N'Kế Toán', N'Phòng Kế toán tài chính', 1),
        (N'Kinh Doanh', N'Phòng Kinh doanh', 1),
        (N'Kho Vận', N'Phòng Quản lý kho và vận chuyển', 1);
    PRINT N'✓ Đã tạo các phòng ban mẫu';
END

IF NOT EXISTS (SELECT 1 FROM dbo.ChucVu WHERE MaChucVu = 1)
BEGIN
    INSERT INTO dbo.ChucVu (TenChucVu, MoTa, KichHoat)
    VALUES 
        (N'Trưởng phòng', N'Quản lý phòng ban', 1),
        (N'Phó phòng', N'Phó quản lý phòng ban', 1),
        (N'Nhân viên', N'Nhân viên thực hiện công việc', 1),
        (N'Thực tập', N'Nhân viên thực tập', 1);
    PRINT N'✓ Đã tạo các chức vụ mẫu';
END

PRINT N'';
PRINT N'Bắt đầu tạo tài khoản...';
PRINT N'';

------------------------------------------------------------
-- 1) TÀI KHOẢN HR (Quản lý nhân sự)
------------------------------------------------------------
DECLARE @MaNV INT;

PRINT N'[1/5] Tạo tài khoản HR...';
EXEC dbo.sp_TaoTaiKhoanDayDu
    @HoTen = N'Nguyễn Thị Mai',
    @NgaySinh = '1988-03-15',
    @GioiTinh = N'Nu',
    @DienThoai = '0901234567',
    @Email = 'nguyenthimai@company.com',
    @DiaChi = N'123 Nguyễn Văn Cừ, Quận 5, TP.HCM',
    @NgayVaoLam = '2020-01-10',
    @MaPhongBan = 1,  -- Nhân Sự
    @MaChucVu = 1,    -- Trưởng phòng
    @LuongCoBan = 15000000,
    @TenDangNhap = 'hr_mai',
    @MatKhau = 'HR@2024',
    @VaiTro = N'HR',
    @MaNV_OUT = @MaNV OUTPUT;

PRINT N'  ✓ Tài khoản HR: hr_mai / HR@2024';
PRINT N'  → Vai trò: HR (Quản lý nhân sự)';
PRINT N'  → Quyền: Quản lý nhân viên, lịch phân ca, duyệt đơn từ';
PRINT N'';

------------------------------------------------------------
-- 2) TÀI KHOẢN QUẢN LÝ (Store Manager)
------------------------------------------------------------
PRINT N'[2/5] Tạo tài khoản Quản lý...';
EXEC dbo.sp_TaoTaiKhoanDayDu
    @HoTen = N'Trần Văn Nam',
    @NgaySinh = '1985-07-22',
    @GioiTinh = N'Nam',
    @DienThoai = '0912345678',
    @Email = 'tranvannam@company.com',
    @DiaChi = N'456 Lê Văn Sỹ, Quận 3, TP.HCM',
    @NgayVaoLam = '2018-05-20',
    @MaPhongBan = 3,  -- Kinh Doanh
    @MaChucVu = 1,    -- Trưởng phòng
    @LuongCoBan = 18000000,
    @TenDangNhap = 'quanly_nam',
    @MatKhau = 'QL@2024',
    @VaiTro = N'QuanLy',
    @MaNV_OUT = @MaNV OUTPUT;

PRINT N'  ✓ Tài khoản Quản lý: quanly_nam / QL@2024';
PRINT N'  → Vai trò: QuanLy (Quản lý cửa hàng)';
PRINT N'  → Quyền: Xem nhân viên, quản lý lịch, duyệt đơn từ, xem báo cáo';
PRINT N'';

------------------------------------------------------------
-- 3) TÀI KHOẢN KẾ TOÁN
------------------------------------------------------------
PRINT N'[3/5] Tạo tài khoản Kế toán...';
EXEC dbo.sp_TaoTaiKhoanDayDu
    @HoTen = N'Lê Thị Hoa',
    @NgaySinh = '1990-11-05',
    @GioiTinh = N'Nu',
    @DienThoai = '0923456789',
    @Email = 'lethihoa@company.com',
    @DiaChi = N'789 Võ Văn Tần, Quận 3, TP.HCM',
    @NgayVaoLam = '2021-03-01',
    @MaPhongBan = 2,  -- Kế Toán
    @MaChucVu = 1,    -- Trưởng phòng
    @LuongCoBan = 14000000,
    @TenDangNhap = 'ketoan_hoa',
    @MatKhau = 'KT@2024',
    @VaiTro = N'KeToan',
    @MaNV_OUT = @MaNV OUTPUT;

PRINT N'  ✓ Tài khoản Kế toán: ketoan_hoa / KT@2024';
PRINT N'  → Vai trò: KeToan (Kế toán viên)';
PRINT N'  → Quyền: Xem công, tính lương, chốt bảng lương, xem báo cáo';
PRINT N'';

------------------------------------------------------------
-- 4) TÀI KHOẢN NHÂN VIÊN 1
------------------------------------------------------------
PRINT N'[4/5] Tạo tài khoản Nhân viên 1...';
EXEC dbo.sp_TaoTaiKhoanDayDu
    @HoTen = N'Phạm Văn Bình',
    @NgaySinh = '1995-02-14',
    @GioiTinh = N'Nam',
    @DienThoai = '0934567890',
    @Email = 'phamvanbinh@company.com',
    @DiaChi = N'321 Điện Biên Phủ, Quận 10, TP.HCM',
    @NgayVaoLam = '2023-01-15',
    @MaPhongBan = 4,  -- Kho Vận
    @MaChucVu = 3,    -- Nhân viên
    @LuongCoBan = 8000000,
    @TenDangNhap = 'nhanvien_binh',
    @MatKhau = 'NV@2024',
    @VaiTro = N'NhanVien',
    @MaNV_OUT = @MaNV OUTPUT;

PRINT N'  ✓ Tài khoản Nhân viên: nhanvien_binh / NV@2024';
PRINT N'  → Vai trò: NhanVien';
PRINT N'  → Quyền: Xem lịch làm việc, chấm công, gửi đơn từ, xem lương cá nhân';
PRINT N'';

------------------------------------------------------------
-- 5) TÀI KHOẢN NHÂN VIÊN 2
------------------------------------------------------------
PRINT N'[5/5] Tạo tài khoản Nhân viên 2...';
EXEC dbo.sp_TaoTaiKhoanDayDu
    @HoTen = N'Hoàng Thị Lan',
    @NgaySinh = '1998-09-28',
    @GioiTinh = N'Nu',
    @DienThoai = '0945678901',
    @Email = 'hoangthilan@company.com',
    @DiaChi = N'654 Cách Mạng Tháng 8, Quận 10, TP.HCM',
    @NgayVaoLam = '2023-06-01',
    @MaPhongBan = 3,  -- Kinh Doanh
    @MaChucVu = 3,    -- Nhân viên
    @LuongCoBan = 7500000,
    @TenDangNhap = 'nhanvien_lan',
    @MatKhau = 'NV@2024',
    @VaiTro = N'NhanVien',
    @MaNV_OUT = @MaNV OUTPUT;

PRINT N'  ✓ Tài khoản Nhân viên: nhanvien_lan / NV@2024';
PRINT N'  → Vai trò: NhanVien';
PRINT N'  → Quyền: Xem lịch làm việc, chấm công, gửi đơn từ, xem lương cá nhân';
PRINT N'';

------------------------------------------------------------
-- KIỂM TRA KẾT QUẢ
------------------------------------------------------------
PRINT N'';
PRINT N'========================================';
PRINT N'   KIỂM TRA TÀI KHOẢN ĐÃ TẠO';
PRINT N'========================================';
PRINT N'';

-- Liệt kê SQL Logins
PRINT N'SQL Server Logins đã tạo:';
SELECT 
    sp.name AS [Tên Đăng Nhập],
    sp.type_desc AS [Loại],
    CASE sp.is_disabled WHEN 0 THEN N'Kích hoạt' ELSE N'Bị khóa' END AS [Trạng Thái],
    sp.create_date AS [Ngày Tạo]
FROM sys.server_principals sp
WHERE sp.type = 'S'
  AND sp.name IN ('hr_mai', 'quanly_nam', 'ketoan_hoa', 'nhanvien_binh', 'nhanvien_lan')
ORDER BY sp.create_date;

PRINT N'';
PRINT N'Database Users và Roles:';
SELECT 
    dp.name AS [Tên User],
    STRING_AGG(USER_NAME(drm.role_principal_id), ', ') AS [Roles],
    nd.VaiTro AS [Vai Trò App]
FROM sys.database_principals dp
LEFT JOIN sys.database_role_members drm ON dp.principal_id = drm.member_principal_id
LEFT JOIN dbo.NguoiDung nd ON nd.TenDangNhap = dp.name
WHERE dp.type = 'S'
  AND dp.name IN ('hr_mai', 'quanly_nam', 'ketoan_hoa', 'nhanvien_binh', 'nhanvien_lan')
GROUP BY dp.name, nd.VaiTro
ORDER BY dp.name;

PRINT N'';
PRINT N'Thông tin nhân viên:';
SELECT 
    nv.MaNV,
    nv.HoTen,
    nd.TenDangNhap,
    nd.VaiTro,
    pb.TenPhongBan,
    cv.TenChucVu,
    nv.LuongCoBan,
    CASE nd.KichHoat WHEN 1 THEN N'Hoạt động' ELSE N'Bị khóa' END AS [Trạng Thái]
FROM dbo.NhanVien nv
JOIN dbo.NguoiDung nd ON nv.MaNguoiDung = nd.MaNguoiDung
LEFT JOIN dbo.PhongBan pb ON nv.MaPhongBan = pb.MaPhongBan
LEFT JOIN dbo.ChucVu cv ON nv.MaChucVu = cv.MaChucVu
WHERE nd.TenDangNhap IN ('hr_mai', 'quanly_nam', 'ketoan_hoa', 'nhanvien_binh', 'nhanvien_lan')
ORDER BY nv.MaNV;

PRINT N'';
PRINT N'========================================';
PRINT N'   HƯỚNG DẪN SỬ DỤNG';
PRINT N'========================================';
PRINT N'';
PRINT N'BÂY GIỜ BẠN CÓ THỂ ĐĂNG NHẬP VÀO ỨNG DỤNG VỚI CÁC TÀI KHOẢN SAU:';
PRINT N'';
PRINT N'1. HR (Quản lý nhân sự):';
PRINT N'   Username: hr_mai';
PRINT N'   Password: HR@2024';
PRINT N'';
PRINT N'2. Quản lý (Store Manager):';
PRINT N'   Username: quanly_nam';
PRINT N'   Password: QL@2024';
PRINT N'';
PRINT N'3. Kế toán:';
PRINT N'   Username: ketoan_hoa';
PRINT N'   Password: KT@2024';
PRINT N'';
PRINT N'4. Nhân viên 1:';
PRINT N'   Username: nhanvien_binh';
PRINT N'   Password: NV@2024';
PRINT N'';
PRINT N'5. Nhân viên 2:';
PRINT N'   Username: nhanvien_lan';
PRINT N'   Password: NV@2024';
PRINT N'';
PRINT N'========================================';
PRINT N'   TEST THAY ĐỔI MẬT KHẨU';
PRINT N'========================================';
PRINT N'';
PRINT N'Ví dụ đổi mật khẩu cho nhanvien_binh:';
PRINT N'';
PRINT N'EXEC dbo.sp_CapNhatTaiKhoanDayDu';
PRINT N'    @MaNV = (SELECT MaNV FROM NhanVien nv JOIN NguoiDung nd ON nv.MaNguoiDung = nd.MaNguoiDung WHERE nd.TenDangNhap = ''nhanvien_binh''),';
PRINT N'    @HoTen = N''Phạm Văn Bình'',';
PRINT N'    @NgaySinh = ''1995-02-14'',';
PRINT N'    @GioiTinh = N''Nam'',';
PRINT N'    @DienThoai = ''0934567890'',';
PRINT N'    @Email = ''phamvanbinh@company.com'',';
PRINT N'    @DiaChi = N''321 Điện Biên Phủ, Quận 10, TP.HCM'',';
PRINT N'    @MaPhongBan = 4,';
PRINT N'    @MaChucVu = 3,';
PRINT N'    @LuongCoBan = 8000000,';
PRINT N'    @VaiTro = N''NhanVien'',';
PRINT N'    @MatKhauMoi = ''NewPass@2024'';  -- Mật khẩu mới';
PRINT N'';
PRINT N'========================================';
PRINT N'   HOÀN TẤT!';
PRINT N'========================================';
GO
