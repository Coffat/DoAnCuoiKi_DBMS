# HƯỚNG DẪN SỬ DỤNG HỆ THỐNG QUẢN LÝ LỊCH PHÂN CA

## 📋 Tổng quan

Hệ thống quản lý lịch phân ca cho phép:
- ✅ Xem lịch làm việc theo tuần (7 ngày Mon-Sun)
- ✅ Sao chép lịch từ tuần trước sang tuần mới
- ✅ Khóa/Mở khóa lịch tuần để bảo vệ dữ liệu
- ✅ Đồng bộ tự động với đơn nghỉ phép
- ✅ Chống chồng lấn ca làm việc
- ✅ Hỗ trợ nhiều ca/ngày cho một nhân viên

---

## 🗄️ CÀI ĐẶT DATABASE

### Bước 1: Chạy script SQL

Chạy các file SQL theo thứ tự:

```sql
-- 1. Tạo database và tables
USE QLNhanSuSieuThiMini;
GO

-- 2. Chạy file mới: 06_LichPhanCa_Advanced.sql
-- File này bao gồm:
-- - TVF tvf_LichTheoTuan
-- - CRUD stored procedures
-- - sp_LichPhanCa_CloneWeek
-- - sp_LichPhanCa_KhoaTuan / MoKhoaTuan
-- - Trigger chặn sửa khi đã khóa
-- - Mở rộng sp_DuyetDonTu
```

### Bước 2: Kiểm tra cài đặt

```sql
-- Kiểm tra TVF
SELECT * FROM dbo.tvf_LichTheoTuan(1, '2025-01-06') -- MaNV=1, Thứ Hai

-- Kiểm tra stored procedures
EXEC sp_help 'sp_LichPhanCa_Insert'
EXEC sp_help 'sp_LichPhanCa_CloneWeek'
EXEC sp_help 'sp_LichPhanCa_KhoaTuan'

-- Kiểm tra trigger
SELECT name, is_disabled FROM sys.triggers 
WHERE name = 'tr_LichPhanCa_BlockChangeWhenLocked'

-- Kiểm tra index
EXEC sp_helpindex 'LichPhanCa'
```

---

## 💻 SỬ DỤNG ỨNG DỤNG WINFORMS

### Form 1: frmLichTuan (Quản lý lịch theo nhân viên)

**Mục đích:** Xem và chỉnh sửa lịch tuần cho từng nhân viên

#### Các chức năng:

1. **Chọn nhân viên và tuần**
   - Chọn nhân viên từ ComboBox
   - Dùng nút "< Tuần trước" / "Tuần sau >" để di chuyển

2. **Xem lịch tuần (7 dòng)**
   - Mỗi dòng = 1 ngày (Thứ 2 → Chủ nhật)
   - Cột "Ca làm": ComboBox chọn ca
   - Cột "Trạng thái": DuKien / Khoa / Huy
   - Cột "Ghi chú": Nhập ghi chú tùy ý

3. **Sao chép tuần trước**
   - Nút: "Sao chép tuần trước"
   - Chọn Yes: Ghi đè lịch hiện tại
   - Chọn No: Chỉ thêm vào ngày chưa có lịch

4. **Khóa tuần**
   - Nút: "Khóa tuần"
   - Chuyển tất cả lịch DuKien → Khoa
   - Lịch đã khóa không thể sửa/xóa

5. **Mở khóa tuần** (Chỉ HR/QuanLy)
   - Nút: "Mở khóa tuần"
   - Chuyển lịch Khoa → DuKien
   - Có ghi log audit

6. **Lưu thay đổi**
   - Nút: "Lưu"
   - Tự động Insert/Update/Delete theo thay đổi

#### Màu sắc:
- 🔴 **Đỏ nhạt**: Lịch đã khóa (không sửa được)
- ⚫ **Xám**: Lịch đã hủy (nghỉ phép)
- 🔵 **Xanh nhạt**: Cuối tuần (Thứ 7, CN)

---

### Form 2: frmPhanCa (Xem tổng quan lịch tuần)

**Mục đích:** Xem ma trận lịch tuần (Ca × Ngày)

#### Hiển thị:
- **Hàng**: Các ca làm việc (Sáng, Chiều, Tối, etc.)
- **Cột**: 7 ngày trong tuần (Mon-Sun)
- **Ô**: Danh sách nhân viên được phân ca
  - 🔒 Icon khóa: Lịch đã khóa
  - ❌ Icon X: Lịch đã hủy

#### Chức năng:
- Xem nhanh ai làm ca nào trong tuần
- Phát hiện ca thiếu người hoặc quá tải
- Không cho phép chỉnh sửa (chỉ xem)

---

## 🔧 CÁC STORED PROCEDURES

### 1. CRUD Lịch Phân Ca

#### sp_LichPhanCa_Insert
```sql
DECLARE @MaLich INT;
EXEC dbo.sp_LichPhanCa_Insert
    @MaNV = 1,
    @Ngay = '2025-01-06',
    @MaCa = 1,
    @TrangThai = N'DuKien',
    @GhiChu = N'Lịch thường',
    @MaLich_OUT = @MaLich OUTPUT;
SELECT @MaLich AS MaLichMoi;
```

**Validation:**
- ✅ Kiểm tra nhân viên tồn tại
- ✅ Kiểm tra ca làm tồn tại và active
- ✅ Chống overlap thời gian ca
- ✅ Hỗ trợ ca qua đêm

#### sp_LichPhanCa_Update
```sql
EXEC dbo.sp_LichPhanCa_Update
    @Id = 123,
    @MaCa = 2,
    @TrangThai = NULL, -- Giữ nguyên
    @GhiChu = N'Cập nhật ghi chú';
```

**Lưu ý:**
- Không sửa được lịch đã khóa (trừ GhiChu)
- Kiểm tra overlap khi đổi ca

#### sp_LichPhanCa_Delete
```sql
EXEC dbo.sp_LichPhanCa_Delete @Id = 123;
```

**Lưu ý:**
- Không xóa được lịch đã khóa

#### sp_LichPhanCa_GetByNhanVien
```sql
EXEC dbo.sp_LichPhanCa_GetByNhanVien
    @MaNV = 1,
    @FromDate = '2025-01-01',
    @ToDate = '2025-01-31';
```

---

### 2. Sao chép tuần

#### sp_LichPhanCa_CloneWeek
```sql
-- Sao chép từ tuần 30/12-05/01 sang tuần 06/01-12/01
EXEC dbo.sp_LichPhanCa_CloneWeek
    @NgayBatDauFrom = '2024-12-30', -- Thứ Hai tuần nguồn
    @NgayBatDauTo = '2025-01-06',   -- Thứ Hai tuần đích
    @Overwrite = 0;                 -- 0 = không ghi đè, 1 = ghi đè
```

**Logic:**
- Copy tất cả lịch có TrangThai = DuKien hoặc Khoa
- Lịch mới luôn có TrangThai = DuKien
- Nếu @Overwrite = 0: Bỏ qua ngày đã có lịch
- Nếu @Overwrite = 1: Xóa lịch cũ trước khi copy

**Kết quả:**
```sql
-- Trả về số lịch đã copy
SoLichDaCopy
--------------
42
```

---

### 3. Khóa/Mở khóa tuần

#### sp_LichPhanCa_KhoaTuan
```sql
-- Khóa lịch tuần cho nhân viên MaNV=1
EXEC dbo.sp_LichPhanCa_KhoaTuan
    @MaNV = 1,
    @NgayBatDau = '2025-01-06'; -- Thứ Hai
```

**Hiệu ứng:**
- Chuyển tất cả lịch DuKien → Khoa (7 ngày)
- Lịch đã khóa không thể sửa/xóa (trigger chặn)

#### sp_LichPhanCa_MoKhoaTuan
```sql
-- Mở khóa lịch tuần (chỉ HR/QuanLy)
EXEC dbo.sp_LichPhanCa_MoKhoaTuan
    @MaNV = 1,
    @NgayBatDau = '2025-01-06',
    @MaNguoiDung = 10; -- MaNguoiDung của người thực hiện
```

**Kiểm tra quyền:**
- Chỉ HR hoặc QuanLy mới được mở khóa
- Ghi log vào GhiChu: "[Mở khóa bởi 10 lúc 2025-01-06 10:30:00]"

---

### 4. TVF xem tuần

#### tvf_LichTheoTuan
```sql
-- Xem lịch tuần cho nhân viên MaNV=1
SELECT * FROM dbo.tvf_LichTheoTuan(1, '2025-01-06')
ORDER BY Ngay;
```

**Kết quả (7 dòng):**
| Ngay       | MaNV | IdLich | MaCa | TrangThai | GhiChu | TenCa  | GioBatDau | GioKetThuc | HeSoCa |
|------------|------|--------|------|-----------|--------|--------|-----------|------------|--------|
| 2025-01-06 | 1    | 101    | 1    | DuKien    | ...    | Sáng   | 08:00     | 12:00      | 1.0    |
| 2025-01-07 | 1    | 102    | 2    | DuKien    | ...    | Chiều  | 13:00     | 17:00      | 1.0    |
| 2025-01-08 | 1    | NULL   | NULL | NULL      | NULL   | NULL   | NULL      | NULL       | NULL   |
| ...        | ...  | ...    | ...  | ...       | ...    | ...    | ...       | ...        | ...    |

**Lưu ý:**
- Luôn trả về 7 dòng (Mon-Sun)
- Ngày không có lịch: IdLich = NULL

---

### 5. Đồng bộ đơn nghỉ

#### sp_DuyetDonTu (đã mở rộng)
```sql
-- Duyệt đơn nghỉ phép
EXEC dbo.sp_DuyetDonTu
    @MaDon = 50,
    @MaNguoiDuyet = 10,
    @ChapNhan = 1; -- 1 = duyệt, 0 = từ chối
```

**Hiệu ứng khi duyệt đơn NGHI:**
1. Cập nhật ChamCong: GioVao=NULL, GioRa=NULL, GioCong=0
2. **Cập nhật LichPhanCa**: TrangThai → Huy
3. Ghi chú: "[Nghỉ phép - Đơn #50]"

**Bypass trigger:**
- Dùng SESSION_CONTEXT('SkipTrigger') = 1
- Cho phép cập nhật lịch đã khóa khi duyệt đơn

---

## 🔒 TRIGGER BẢO VỆ DỮ LIỆU

### tr_LichPhanCa_BlockChangeWhenLocked

**Chức năng:**
- Chặn UPDATE/DELETE lịch có TrangThai = 'Khoa'
- Cho phép bypass qua SESSION_CONTEXT

**Test:**
```sql
-- Tạo lịch và khóa
INSERT INTO LichPhanCa (MaNV, NgayLam, MaCa, TrangThai)
VALUES (1, '2025-01-06', 1, N'Khoa');

-- Thử sửa → LỖI
UPDATE LichPhanCa SET MaCa = 2 WHERE MaLich = 123;
-- Msg: Không thể sửa lịch đã khóa

-- Thử xóa → LỖI
DELETE FROM LichPhanCa WHERE MaLich = 123;
-- Msg: Không thể xóa lịch đã khóa

-- Bypass (chỉ trong stored procedure)
EXEC sp_set_session_context @key = N'SkipTrigger', @value = 1;
UPDATE LichPhanCa SET MaCa = 2 WHERE MaLich = 123; -- OK
EXEC sp_set_session_context @key = N'SkipTrigger', @value = 0;
```

---

## ✅ ACCEPTANCE CHECKLIST

### Test 1: Clone tuần
```sql
-- Setup: Tạo lịch tuần 30/12-05/01
INSERT INTO LichPhanCa (MaNV, NgayLam, MaCa, TrangThai)
VALUES 
    (1, '2024-12-30', 1, N'DuKien'),
    (1, '2024-12-31', 2, N'DuKien'),
    (1, '2025-01-01', 1, N'DuKien'),
    (1, '2025-01-02', 2, N'DuKien'),
    (1, '2025-01-03', 1, N'DuKien'),
    (1, '2025-01-04', 2, N'DuKien'),
    (1, '2025-01-05', 1, N'DuKien');

-- Test: Clone sang tuần 06/01-12/01
EXEC dbo.sp_LichPhanCa_CloneWeek
    @NgayBatDauFrom = '2024-12-30',
    @NgayBatDauTo = '2025-01-06',
    @Overwrite = 0;

-- Verify: Kiểm tra 7 ngày mới
SELECT * FROM LichPhanCa 
WHERE MaNV = 1 AND NgayLam BETWEEN '2025-01-06' AND '2025-01-12'
ORDER BY NgayLam;
-- Expected: 7 dòng, TrangThai = DuKien
```

✅ **PASS** nếu:
- Clone đủ 7 ngày
- Trạng thái = DuKien
- Không đè ngày đã tồn tại (nếu Overwrite=0)

---

### Test 2: Chống overlap ca
```sql
-- Setup: Tạo ca Sáng (08:00-12:00)
INSERT INTO CaLam (TenCa, GioBatDau, GioKetThuc, HeSoCa, KichHoat)
VALUES (N'Sáng', '08:00', '12:00', 1.0, 1);

-- Setup: Tạo ca Sáng 2 (10:00-14:00) - OVERLAP
INSERT INTO CaLam (TenCa, GioBatDau, GioKetThuc, HeSoCa, KichHoat)
VALUES (N'Sáng 2', '10:00', '14:00', 1.0, 1);

-- Test: Thêm lịch ca Sáng
EXEC dbo.sp_LichPhanCa_Insert
    @MaNV = 1, @Ngay = '2025-01-06', @MaCa = 1, @TrangThai = N'DuKien',
    @GhiChu = NULL, @MaLich_OUT = NULL;
-- OK

-- Test: Thêm lịch ca Sáng 2 (cùng ngày) → LỖI
EXEC dbo.sp_LichPhanCa_Insert
    @MaNV = 1, @Ngay = '2025-01-06', @MaCa = 2, @TrangThai = N'DuKien',
    @GhiChu = NULL, @MaLich_OUT = NULL;
-- Expected: Lỗi "Lịch bị trùng lặp thời gian"
```

✅ **PASS** nếu:
- Chặn insert ca overlap
- Cho phép insert ca không overlap

---

### Test 3: Khóa tuần
```sql
-- Setup: Tạo lịch tuần DuKien
-- (như Test 1)

-- Test: Khóa tuần
EXEC dbo.sp_LichPhanCa_KhoaTuan
    @MaNV = 1,
    @NgayBatDau = '2025-01-06';

-- Verify: Kiểm tra trạng thái
SELECT NgayLam, TrangThai FROM LichPhanCa
WHERE MaNV = 1 AND NgayLam BETWEEN '2025-01-06' AND '2025-01-12';
-- Expected: Tất cả TrangThai = Khoa

-- Test: Thử sửa lịch đã khóa → LỖI
UPDATE LichPhanCa SET MaCa = 2 
WHERE MaNV = 1 AND NgayLam = '2025-01-06';
-- Expected: Lỗi "Không thể sửa lịch đã khóa"
```

✅ **PASS** nếu:
- Khóa đủ 7 ngày
- Trigger chặn sửa/xóa

---

### Test 4: Duyệt đơn nghỉ → Đồng bộ lịch
```sql
-- Setup: Tạo lịch tuần
-- Setup: Tạo đơn nghỉ 08/01-10/01
INSERT INTO DonTu (MaNV, Loai, TuLuc, DenLuc, LyDo, TrangThai)
VALUES (1, N'NGHI', '2025-01-08 00:00', '2025-01-10 23:59', N'Nghỉ phép', N'ChoDuyet');

-- Test: Duyệt đơn
EXEC dbo.sp_DuyetDonTu @MaDon = 1, @MaNguoiDuyet = 10, @ChapNhan = 1;

-- Verify: Kiểm tra LichPhanCa
SELECT NgayLam, TrangThai, GhiChu FROM LichPhanCa
WHERE MaNV = 1 AND NgayLam BETWEEN '2025-01-08' AND '2025-01-10';
-- Expected: TrangThai = Huy, GhiChu chứa "[Nghỉ phép - Đơn #1]"

-- Verify: Kiểm tra ChamCong
SELECT NgayLam, GioVao, GioRa, GioCong FROM ChamCong
WHERE MaNV = 1 AND NgayLam BETWEEN '2025-01-08' AND '2025-01-10';
-- Expected: GioVao=NULL, GioRa=NULL, GioCong=0
```

✅ **PASS** nếu:
- ChamCong cập nhật nghỉ
- LichPhanCa chuyển Huy
- GhiChu ghi đúng

---

### Test 5: TVF trả đúng 7 dòng
```sql
-- Setup: Tạo lịch 3 ngày (Mon, Wed, Fri)
INSERT INTO LichPhanCa (MaNV, NgayLam, MaCa, TrangThai)
VALUES 
    (1, '2025-01-06', 1, N'DuKien'), -- Mon
    (1, '2025-01-08', 2, N'DuKien'), -- Wed
    (1, '2025-01-10', 1, N'DuKien'); -- Fri

-- Test: Query TVF
SELECT Ngay, IdLich, MaCa, TenCa FROM dbo.tvf_LichTheoTuan(1, '2025-01-06');

-- Expected: 7 dòng
-- Mon: IdLich=101, MaCa=1, TenCa='Sáng'
-- Tue: IdLich=NULL, MaCa=NULL, TenCa=NULL
-- Wed: IdLich=102, MaCa=2, TenCa='Chiều'
-- Thu: IdLich=NULL, MaCa=NULL, TenCa=NULL
-- Fri: IdLich=103, MaCa=1, TenCa='Sáng'
-- Sat: IdLich=NULL, MaCa=NULL, TenCa=NULL
-- Sun: IdLich=NULL, MaCa=NULL, TenCa=NULL
```

✅ **PASS** nếu:
- Luôn trả về 7 dòng
- Ngày không có lịch: NULL

---

### Test 6: Tốc độ (Index hoạt động)
```sql
-- Setup: Insert 10,000 lịch
DECLARE @i INT = 0;
WHILE @i < 10000
BEGIN
    INSERT INTO LichPhanCa (MaNV, NgayLam, MaCa, TrangThai)
    VALUES (@i % 100 + 1, DATEADD(DAY, @i % 365, '2025-01-01'), (@i % 3) + 1, N'DuKien');
    SET @i = @i + 1;
END

-- Test: Query với index
SET STATISTICS IO ON;
SET STATISTICS TIME ON;

SELECT * FROM LichPhanCa
WHERE MaNV = 50 AND NgayLam BETWEEN '2025-01-01' AND '2025-12-31';

-- Expected: 
-- Logical reads < 10
-- CPU time < 10ms
```

✅ **PASS** nếu:
- Query < 10ms
- Index IX_LichPhanCa_MaNV_NgayLam được sử dụng

---

## 🎯 LUỒNG HOẠT ĐỘNG TỔNG

### Tuần 1: Tạo lịch mới
1. HR mở **frmLichTuan**
2. Chọn nhân viên
3. Chọn từng ngày, chọn ca từ ComboBox
4. Nhấn **Lưu**
5. Kiểm tra lại trên **frmPhanCa** (ma trận tuần)

### Tuần 2-N: Sao chép tuần trước
1. HR mở **frmLichTuan**
2. Chọn nhân viên
3. Chuyển sang tuần mới
4. Nhấn **Sao chép tuần trước**
5. Chọn Yes/No (ghi đè hay không)
6. Điều chỉnh nếu cần
7. Nhấn **Lưu**

### Nhân viên gửi đơn nghỉ
1. Nhân viên mở form **Tạo đơn từ**
2. Chọn loại NGHI, nhập ngày, lý do
3. Gửi đơn

### HR/QL duyệt đơn
1. HR/QL mở form **Duyệt đơn từ**
2. Chọn đơn, nhấn **Duyệt**
3. Hệ thống tự động:
   - Cập nhật ChamCong
   - Cập nhật LichPhanCa → Huy

### Cuối tuần: Khóa lịch
1. HR mở **frmLichTuan**
2. Chọn nhân viên
3. Nhấn **Khóa tuần**
4. Lịch không thể sửa nữa

### Trong tuần: CheckIn/Out
1. Nhân viên CheckIn/Out bình thường
2. Dữ liệu lương lấy từ:
   - LichPhanCa (đã Khoa)
   - ChamCong (thực tế)

---

## 🐛 TROUBLESHOOTING

### Lỗi: "Không thể sửa lịch đã khóa"
**Nguyên nhân:** Lịch có TrangThai = 'Khoa'

**Giải pháp:**
1. Mở khóa tuần (nếu có quyền HR/QL)
2. Hoặc liên hệ HR để mở khóa

### Lỗi: "Lịch bị trùng lặp thời gian"
**Nguyên nhân:** Ca mới overlap với ca đã có

**Giải pháp:**
1. Kiểm tra lịch hiện tại
2. Xóa ca cũ hoặc chọn ca khác

### Lỗi: "Chỉ HR hoặc Quản lý mới có quyền mở khóa"
**Nguyên nhân:** User không có quyền

**Giải pháp:**
1. Đăng nhập bằng tài khoản HR/QuanLy
2. Hoặc liên hệ HR

### Lỗi: "Ngày bắt đầu phải là thứ Hai"
**Nguyên nhân:** Truyền sai ngày vào proc

**Giải pháp:**
```sql
-- Tính thứ Hai của tuần
DECLARE @NgayBatDau DATE = '2025-01-08'; -- Wed
DECLARE @DaysFromMonday INT = (DATEPART(WEEKDAY, @NgayBatDau) + 5) % 7;
SET @NgayBatDau = DATEADD(DAY, -@DaysFromMonday, @NgayBatDau);
-- Result: 2025-01-06 (Mon)
```

---

## 📊 PERFORMANCE TIPS

1. **Index được tạo tự động:**
   - `IX_LichPhanCa_MaNV_NgayLam`
   - `IX_LichPhanCa_NgayLam`

2. **Query tối ưu:**
   ```sql
   -- TỐT: Dùng index
   SELECT * FROM LichPhanCa 
   WHERE MaNV = 1 AND NgayLam BETWEEN '2025-01-01' AND '2025-01-31';
   
   -- XẤU: Không dùng index
   SELECT * FROM LichPhanCa 
   WHERE YEAR(NgayLam) = 2025 AND MONTH(NgayLam) = 1;
   ```

3. **Batch operations:**
   ```sql
   -- TỐT: Insert nhiều dòng cùng lúc
   INSERT INTO LichPhanCa (MaNV, NgayLam, MaCa, TrangThai)
   VALUES 
       (1, '2025-01-06', 1, N'DuKien'),
       (1, '2025-01-07', 2, N'DuKien'),
       (1, '2025-01-08', 1, N'DuKien');
   
   -- XẤU: Insert từng dòng
   INSERT INTO LichPhanCa ... VALUES (...);
   INSERT INTO LichPhanCa ... VALUES (...);
   INSERT INTO LichPhanCa ... VALUES (...);
   ```

---

## 📞 HỖ TRỢ

Nếu gặp vấn đề, vui lòng:
1. Kiểm tra log SQL Server
2. Kiểm tra quyền user
3. Kiểm tra trigger có bị disable không
4. Liên hệ admin hệ thống

---

## 📝 CHANGELOG

### Version 1.0 (2025-01-06)
- ✅ Tạo TVF tvf_LichTheoTuan
- ✅ Tạo CRUD stored procedures
- ✅ Tạo sp_LichPhanCa_CloneWeek
- ✅ Tạo sp_LichPhanCa_KhoaTuan / MoKhoaTuan
- ✅ Tạo trigger chặn sửa khi đã khóa
- ✅ Mở rộng sp_DuyetDonTu
- ✅ Tạo form frmLichTuan
- ✅ Cập nhật form frmPhanCa

---

**Hết tài liệu hướng dẫn**
