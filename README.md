# 🏢 HỆ THỐNG QUẢN LÝ NHÂN SỰ SIÊU THỊ MINI

## 📋 Mô tả dự án

Hệ thống quản lý nhân sự toàn diện cho siêu thị mini, được phát triển bằng .NET Framework 4.7.2 WinForms với SQL Server. Hệ thống cung cấp đầy đủ các chức năng quản lý từ nhân viên, ca làm việc, chấm công đến tính lương và báo cáo với giao diện hiện đại và bảo mật cao.

## 🎯 Tính năng chính

### 👥 **Quản lý nhân viên**
- ✅ Thêm, sửa, xóa thông tin nhân viên
- ✅ Tạo tài khoản đăng nhập tự động
- ✅ Phân quyền theo vai trò (HR, Quản lý, Kế toán, Nhân viên)
- ✅ Quản lý thông tin cá nhân và công việc

### ⏰ **Quản lý ca làm việc**
- ✅ Định nghĩa ca làm việc với giờ bắt đầu/kết thúc
- ✅ Thiết lập hệ số ca (ca đêm, ca lễ)
- ✅ Kích hoạt/vô hiệu hóa ca làm việc
- ✅ Mô tả chi tiết cho từng ca

### 📅 **Lịch phân ca**
- ✅ Phân ca cho nhân viên theo ngày/tuần/tháng
- ✅ Xem lịch làm việc cá nhân
- ✅ Khóa/mở lịch phân ca
- ✅ Ghi chú cho từng ca làm việc

### ⏱️ **Chấm công**
- ✅ Ghi nhận giờ vào/ra làm việc
- ✅ Tự động tính toán giờ công, đi trễ, về sớm
- ✅ Khóa công theo kỳ (tháng)
- ✅ Xem báo cáo chấm công chi tiết
- ✅ Tích hợp với lịch phân ca

### 📝 **Quản lý đơn từ**
- ✅ Đơn xin nghỉ phép/nghỉ việc
- ✅ Đơn xin làm thêm giờ (OT)
- ✅ Quy trình duyệt đơn từ
- ✅ Thông báo trạng thái đơn từ
- ✅ Duyệt hàng loạt (HR)

### 💰 **Tính lương**
- ✅ Tính lương tự động theo công thức
- ✅ Tích hợp giờ công và giờ OT
- ✅ Quản lý phụ cấp, khấu trừ
- ✅ Tính thuế và bảo hiểm
- ✅ Khóa/mở bảng lương theo kỳ

### 🧾 **Phiếu lương**
- ✅ Xem phiếu lương cá nhân
- ✅ In phiếu lương định dạng chuẩn
- ✅ Chi tiết các khoản thu/chi
- ✅ Lịch sử lương theo tháng

### 📊 **Báo cáo**
- ✅ **Báo cáo nhân sự**: Tổng quan, chấm công, đơn từ
- ✅ **Báo cáo lương**: Theo tháng, năm, so sánh
- ✅ Thống kê trực quan với biểu đồ
- ✅ Xuất báo cáo Excel (placeholder)

## 🛠️ Công nghệ sử dụng

### **Frontend**
- .NET Framework 4.7.2
- Windows Forms (WinForms)
- Guna.UI2.WinForms (Modern UI Components)

### **Backend**
- ADO.NET cho kết nối database
- SQL Server 2019+
- Stored Procedures & Functions
- Views & Triggers

### **Database**
- SQL Server với schema đầy đủ
- Row-Level Security (RLS)
- ACID Transactions
- Trigger-based calculations

### **Security**
- Role-based Access Control (RBAC)
- Password hashing (Base64 demo)
- Session management
- Data protection triggers

## 🗄️ Cấu trúc database

### **Bảng chính**
```sql
- NguoiDung: Quản lý tài khoản đăng nhập
- NhanVien: Thông tin nhân viên
- CaLam: Định nghĩa ca làm việc
- LichPhanCa: Lịch phân ca nhân viên
- ChamCong: Dữ liệu chấm công
- DonTu: Đơn từ nghỉ phép/OT
- BangLuong: Bảng lương nhân viên
```

### **Views (6 views)**
```sql
- vw_CongThang: Tổng hợp công theo tháng
- vw_BangLuong_ChiTiet: Bảng lương chi tiết
- vw_NhanVien_Full: Thông tin nhân viên đầy đủ
- vw_DonTu_ChiTiet: Đơn từ chi tiết
- vw_BaoCaoNhanSu: Báo cáo nhân sự
- vw_Lich_ChamCong_Ngay: Lịch và chấm công
```

### **Functions**
```sql
- fn_KhungCa: Lấy khung giờ ca làm việc
- fn_SoPhutDuong: Tính số phút dương
- fn_rls_NhanVien: Row-Level Security
```

### **Stored Procedures (35+ SPs)**
```sql
# Quản lý nhân viên
- sp_ThemMoiNhanVien, sp_NhanVien_Delete, sp_NhanVien_UpdateTrangThai
- sp_NhanVien_GetThongTinCaNhan, sp_NhanVien_UpdateThongTinCaNhan

# Phân ca và chấm công
- sp_LichPhanCa_Insert/Update/Delete, sp_LichPhanCa_CloneWeek
- sp_CheckIn, sp_CheckOut, sp_KhoaCongThang

# Đơn từ và lương
- sp_DuyetDonTu, sp_ChayBangLuong, sp_DongBangLuong

# Bảo mật
- sp_NguoiDung_DoiMatKhau
```

## 📁 Cấu trúc project

```
VuToanThang_23110329/
├── Data/
│   ├── SqlHelper.cs          # Database helper
│   └── CurrentUser.cs        # Session management
├── Models/
│   ├── NguoiDung.cs         # User model
│   ├── NhanVien.cs          # Employee model
│   ├── CaLam.cs             # Shift model
│   ├── LichPhanCa.cs        # Schedule model
│   ├── ChamCong.cs          # Attendance model
│   ├── DonTu.cs             # Request model
│   ├── BangLuong.cs         # Payroll model
│   └── ViewModels.cs        # View models
├── Repositories/
│   ├── AuthRepository.cs     # Authentication
│   ├── NhanVienRepository.cs # Employee operations
│   ├── CaLamRepository.cs    # Shift operations
│   ├── LichPhanCaRepository.cs # Schedule operations
│   ├── ChamCongRepository.cs # Attendance operations
│   ├── DonTuRepository.cs    # Request operations
│   └── BangLuongRepository.cs # Payroll operations
├── Forms/
│   ├── LoginForm.cs         # Login interface
│   ├── MainForm.cs          # Main dashboard
│   ├── NhanVienForm.cs      # Employee management
│   ├── CaLamForm.cs         # Shift management
│   ├── LichPhanCaForm.cs    # Schedule management
│   ├── ChamCongForm.cs      # Attendance management
│   ├── DonTuNVForm.cs       # Employee requests
│   ├── DonTuSMForm.cs       # Manager approval
│   ├── DonTuHRForm.cs       # HR management
│   ├── TinhLuongForm.cs     # Payroll calculation
│   ├── BangLuongForm.cs     # Payroll viewing
│   ├── PhieuLuongForm.cs    # Payslip viewing
│   ├── BaoCaoNhanSuForm.cs  # HR reports
│   └── BaoCaoLuongForm.cs   # Payroll reports
└── App.config               # Configuration
```

## 🚀 Cài đặt và chạy

### **Yêu cầu hệ thống**
- Windows 10/11
- .NET Framework 4.7.2+
- SQL Server 2019+
- Visual Studio 2019+ (để phát triển)

### **Bước 1: Chuẩn bị Database**
```bash
# Chạy các file SQL theo thứ tự:
1. 01_TaoDatabase.sql        # Tạo database và các bảng
2. 02_ChucNang.sql           # Tạo views, functions
3. 03_StoredProcedures.sql   # Tạo stored procedures cơ bản
4. 04_StoredProcedures_Advanced.sql  # Stored procedures nâng cao
5. 05_Security_Triggers.sql  # Triggers và bảo mật
6. data_mau.sql             # Dữ liệu mẫu (tùy chọn)
```

### **Bước 2: Cấu hình kết nối**
Cập nhật connection string trong `App.config`:
```xml
<connectionStrings>
    <add name="HrDb"
         connectionString="Data Source=localhost;Initial Catalog=QLNhanSuSieuThiMini;User ID=sa;Password=1234;TrustServerCertificate=True"
         providerName="System.Data.SqlClient" />
</connectionStrings>
```

### **Bước 3: Build và chạy**
```bash
# Mở solution trong Visual Studio
# Build solution (Ctrl+Shift+B)
# Chạy ứng dụng (F5)
```

### **Bước 4: Đăng nhập**
Tài khoản mặc định (sau khi chạy data_mau.sql):
- **Giám đốc**: `giamdoc` / `123`
- **HR Manager**: `hr_manager` / `123`
- **Kế toán**: `ketoan01` / `123`
- **Nhân viên**: `nv_banhang_01` / `123`

(Tất cả tài khoản đều có mật khẩu: `123`)

## 👥 Phân quyền hệ thống

### 🔑 **HR (Nhân sự)**
- ✅ Toàn quyền quản lý nhân viên
- ✅ Quản lý ca làm việc
- ✅ Phân ca cho nhân viên
- ✅ Duyệt đơn từ
- ✅ Xem tất cả báo cáo

### 👔 **Quản lý (Store Manager)**
- ✅ Xem thông tin nhân viên
- ✅ Quản lý lịch phân ca
- ✅ Chấm công và khóa công
- ✅ Duyệt đơn từ
- ✅ Báo cáo nhân sự

### 💼 **Kế toán (Accountant)**
- ✅ Tính lương nhân viên
- ✅ Quản lý bảng lương
- ✅ Khóa/mở kỳ lương
- ✅ Báo cáo lương
- ✅ Xuất phiếu lương

### 👤 **Nhân viên (Employee)**
- ✅ Xem lịch làm việc cá nhân
- ✅ Xem chấm công cá nhân
- ✅ Gửi đơn từ nghỉ phép/OT
- ✅ Xem phiếu lương cá nhân

## 🎨 Giao diện

### **Đặc điểm UI/UX**
- 🌙 Dark theme hiện đại
- 🎨 Màu chủ đạo: #7C4DFF (Purple)
- 📱 Layout responsive
- 🔍 Tìm kiếm và lọc dữ liệu
- 📊 Thống kê trực quan
- 🖨️ In ấn và xuất báo cáo

### **Navigation**
- Menu sidebar theo vai trò
- Breadcrumb navigation
- Quick access buttons
- Status indicators

## 🔧 Tính năng nâng cao

### **Bảo mật**
- Password hashing
- Session timeout
- Role-based permissions
- Data encryption (planned)

### **Hiệu suất**
- Connection pooling
- Stored procedure optimization
- Lazy loading
- Caching (planned)

### **Tích hợp**
- Export to Excel (placeholder)
- Print functionality
- Email notifications (planned)
- API endpoints (planned)

## 🐛 Troubleshooting

### **Lỗi kết nối database**
```
Kiểm tra:
1. SQL Server đang chạy
2. Connection string đúng
3. Database đã được tạo
4. User có quyền truy cập
```

### **Lỗi đăng nhập**
```
Kiểm tra:
1. Tài khoản đã được tạo trong database
2. Mật khẩu đúng định dạng
3. Vai trò được gán đúng
```

### **Lỗi phân quyền**
```
Kiểm tra:
1. User role trong database
2. Permissions được cấp đúng
3. Session context được set
```

## 📞 Hỗ trợ

### **Liên hệ**
- **Tác giả**: Vũ Toàn Thắng
- **MSSV**: 23110329
- **Email**: [email@example.com]
- **GitHub**: [github.com/username]

### **Báo lỗi**
Vui lòng tạo issue trên GitHub với thông tin:
- Mô tả lỗi chi tiết
- Các bước tái hiện
- Screenshot (nếu có)
- Log files

## 📄 License

Dự án này được phát triển cho mục đích học tập và nghiên cứu.

---

## 🎯 Roadmap

### **Version 2.0 (Planned)**
- [ ] Web-based interface
- [ ] Mobile app
- [ ] Advanced reporting
- [ ] Integration APIs
- [ ] Cloud deployment
- [ ] Real-time notifications

### **Version 1.1 (Current)**
- [x] Complete WinForms interface
- [x] Full CRUD operations
- [x] Role-based security
- [x] Comprehensive reporting
- [x] Database optimization

---

## 📦 Files quan trọng

### **SQL Scripts (Chạy theo thứ tự)**
1. `01_TaoDatabase.sql` - Tạo database, bảng, ràng buộc
2. `02_ChucNang.sql` - Views và functions
3. `03_StoredProcedures.sql` - 35+ stored procedures (bao gồm SPs cho thông tin cá nhân)
4. `04_StoredProcedures_Advanced.sql` - SPs nâng cao (lịch phân ca, chấm công)
5. `05_Security_Triggers.sql` - Triggers bảo mật
6. `data_mau.sql` - **Dữ liệu mẫu tổng hợp đầy đủ** (9 nhân viên, lịch từ 7/2025-nay)

### **Documentation**
- `README.md` - Hướng dẫn tổng quan
- `SETUP_PACKAGES.md` - Hướng dẫn cài đặt packages
- `BAO_CAO_KIEM_TRA_FORMS.md` - Báo cáo kiểm tra và cải thiện forms
- `HUONG_DAN_LICH_PHANCA.md` - Hướng dẫn sử dụng module lịch phân ca

### **Application**
- `VuToanThang_23110329.sln` - Solution Visual Studio
- `VuToanThang_23110329/` - Mã nguồn ứng dụng WinForms

---

*Cập nhật lần cuối: 30/09/2025*  
*Phiên bản: 1.1 - Production Ready*
