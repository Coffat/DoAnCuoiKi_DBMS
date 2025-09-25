# HƯỚNG DẪN SỬ DỤNG DỮ LIỆU MẪU

## 📋 Tổng quan

File `duLieuMau_TongHop.sql` là file dữ liệu mẫu tổng hợp gom từ 3 file gốc:

1. **duLieuMau.sql** - Dữ liệu cơ bản (NguoiDung, NhanVien, CaLam, DonTu)
2. **duLieuMau_DayDu_7_9.sql** - Lịch phân ca và chấm công từ 1/7-21/9/2025
3. **test_lich_hom_nay.sql** - Lịch làm việc hôm nay để test Check In/Out

## 🚀 Cách sử dụng

### Bước 1: Tạo Database và Schema
```sql
-- Chạy các file theo thứ tự:
1. 01_TaoDatabase.sql
2. 02_ChucNang.sql  
3. 03_StoredProcedures.sql
4. 04_StoredProcedures_Advanced.sql
5. 05_Security_Triggers.sql
```

### Bước 2: Chạy dữ liệu mẫu tổng hợp
```sql
-- Chỉ cần chạy 1 file duy nhất:
EXEC duLieuMau_TongHop.sql
```

## 📊 Dữ liệu được tạo

### 👥 **NguoiDung & NhanVien (8 người)**
| ID | Tên đăng nhập | Họ tên | Vai trò | Phòng ban | Trạng thái |
|----|---------------|--------|---------|-----------|------------|
| 1 | bichhang | Nguyễn Thị Bích Hằng | HR | HR | Đang làm |
| 2 | vanan | Trần Văn An | KeToan | KeToan | Đang làm |
| 3 | minhtuan | Lê Minh Tuấn | QuanLy | BanHang | Đang làm |
| 4 | kimchi | Phạm Kim Chi | QuanLy | BanHang | Đang làm |
| 5 | vandzung | Hoàng Văn Dũng | NhanVien | BanHang | Đang làm |
| 6 | mylinh | Võ Thị Mỹ Linh | NhanVien | KhoHang | Đang làm |
| 7 | tienmanh | Đỗ Tiến Mạnh | NhanVien | BaoVe | Đang làm |
| 8 | anhthu | Nguyễn Anh Thư | NhanVien | BanHang | Nghỉ việc |

**Mật khẩu tất cả tài khoản:** `1234`

### ⏰ **CaLam (6 ca làm việc)**
| ID | Tên ca | Giờ | Hệ số | Mô tả |
|----|--------|-----|-------|-------|
| 1 | Ca Sáng | 06:00-14:00 | 1.0 | Ca làm việc buổi sáng |
| 2 | Ca Chiều | 14:00-22:00 | 1.0 | Ca làm việc buổi chiều |
| 3 | Ca Đêm | 22:00-06:00 | 1.5 | Ca qua đêm, có phụ cấp |
| 4 | Ca Hành chính | 08:00-17:00 | 1.0 | Dành cho HR/Kế toán |
| 5 | Ca Part-time Sáng | 05:00-06:00 | 1.0 | Ca làm thêm sáng sớm |
| 6 | Ca Part-time Tối | 01:00-05:00 | 1.2 | Ca làm thêm tối muộn |

### 📅 **Phân ca mặc định**
- **HR/Kế toán (1,2):** Ca Hành chính (8-17h)
- **Quản lý (3,4):** Ca Sáng (6-14h) 
- **Thu ngân (5,8):** Ca Chiều (14-22h)
- **Kho hàng (6):** Ca Sáng (6-14h)
- **Bảo vệ (7):** Ca Đêm (22-6h)

### 📊 **Dữ liệu được tạo**
- **LichPhanCa:** ~1,500+ bản ghi (1/7-21/9/2025)
- **ChamCong:** ~1,400+ bản ghi (92% attendance rate)
- **DonTu:** 3 đơn từ mẫu
- **BangLuong:** 3 tháng (7,8,9/2025)

## 🔧 Tính năng đặc biệt

### ✅ **Dữ liệu thực tế**
- Giờ vào/ra có độ lệch ngẫu nhiên (đi trễ, về sớm)
- 92% attendance rate (8% vắng mặt)
- Đa dạng ca làm việc (bao gồm ca qua đêm)
- Lịch nghỉ Chủ nhật và ngày lễ

### ✅ **Lịch hôm nay**
- Tự động tạo lịch làm việc cho ngày hiện tại
- Sẵn sàng test chức năng Check In/Out

### ✅ **Bảng lương tự động**
- Tự động tính toán từ dữ liệu chấm công
- Bao gồm OT, phụ cấp, khấu trừ

## 🛡️ An toàn dữ liệu

### ✅ **Transaction Safety**
- Sử dụng BEGIN TRANSACTION/COMMIT
- Rollback tự động khi có lỗi
- Tắt trigger khi insert để tránh conflict

### ✅ **Clean Data**
- Xóa dữ liệu cũ trước khi insert
- Reset identity seeds
- Kiểm tra kết quả cuối cùng

## 🎯 Test Cases

### 1. **Đăng nhập**
```
Username: bichhang, Password: 1234 (HR)
Username: minhtuan, Password: 1234 (QuanLy)  
Username: vandzung, Password: 1234 (NhanVien)
```

### 2. **Check In/Out hôm nay**
- Tất cả nhân viên đều có lịch làm việc hôm nay
- Test được ngay sau khi chạy dữ liệu

### 3. **Báo cáo**
- Dữ liệu 3 tháng để test báo cáo
- Đa dạng ca làm việc và OT

## ⚠️ Lưu ý quan trọng

1. **Chạy đúng thứ tự:** Schema trước, dữ liệu sau
2. **Backup trước:** Luôn backup database trước khi chạy
3. **Kiểm tra kết quả:** File sẽ hiển thị summary cuối cùng
4. **Lỗi rollback:** Nếu có lỗi, transaction sẽ tự động rollback

## 📞 Hỗ trợ

Nếu gặp lỗi khi chạy dữ liệu mẫu:

1. Kiểm tra database đã tạo chưa
2. Kiểm tra stored procedures đã có chưa  
3. Xem error message trong output
4. Chạy lại từ đầu nếu cần

---

**🎉 Chúc bạn sử dụng hệ thống thành công!**
