# 📝 HƯỚNG DẪN CẬP NHẬT CÁC FORM ĐỂ DÙNG GLOBALSTATE

## Tổng Quan

Sau khi triển khai bảo mật 2 lớp, **TẤT CẢ** các form trong ứng dụng cần được cập nhật để sử dụng chuỗi kết nối động từ `GlobalState.ConnectionString` thay vì đọc từ `App.config`.

---

## 🔄 Pattern Thay Đổi

### CŨ (Đọc từ App.config):

```csharp
using System.Configuration;

// ...

private void LoadData()
{
    // ❌ CŨ: Đọc từ App.config với tài khoản sa cố định
    string connectionString = ConfigurationManager.ConnectionStrings["HrDb"].ConnectionString;
    
    using (SqlConnection conn = new SqlConnection(connectionString))
    {
        // ... code xử lý
    }
}
```

### MỚI (Đọc từ GlobalState):

```csharp
// KHÔNG CẦN: using System.Configuration;

// ...

private void LoadData()
{
    // ✅ MỚI: Đọc từ GlobalState với tài khoản của user đang đăng nhập
    string connectionString = GlobalState.ConnectionString;
    
    // Kiểm tra có connection chưa (optional nhưng nên có)
    if (string.IsNullOrEmpty(connectionString))
    {
        MessageBox.Show("Chưa có kết nối. Vui lòng đăng nhập lại.", 
                       "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        return;
    }
    
    using (SqlConnection conn = new SqlConnection(connectionString))
    {
        // ... code xử lý (giữ nguyên)
    }
}
```

---

## 📋 DANH SÁCH CÁC FORM CẦN CẬP NHẬT

Dưới đây là danh sách các form trong project và cách cập nhật:

### 1. frmNhanVien.cs - Quản Lý Nhân Viên

**Tìm:**
```csharp
string connectionString = ConfigurationManager.ConnectionStrings["HrDb"].ConnectionString;
```

**Thay bằng:**
```csharp
string connectionString = GlobalState.ConnectionString;
```

**Áp dụng cho các method:**
- `LoadData()`
- `btnAdd_Click()`
- `btnEdit_Click()`
- `btnDelete_Click()`
- `btnSearch_Click()`

### 2. frmChamCong.cs - Quản Lý Chấm Công

**Tìm:**
```csharp
var cs = ConfigurationManager.ConnectionStrings["HrDb"];
string connectionString = cs.ConnectionString;
```

**Thay bằng:**
```csharp
string connectionString = GlobalState.ConnectionString;
```

**Áp dụng cho các method:**
- `LoadData()`
- `btnCheckIn_Click()`
- `btnCheckOut_Click()`
- `btnSave_Click()`

### 3. frmLichTuan.cs / frmPhanCa.cs - Quản Lý Lịch

**Tìm:**
```csharp
string connectionString = ConfigurationManager.ConnectionStrings["HrDb"].ConnectionString;
```

**Thay bằng:**
```csharp
string connectionString = GlobalState.ConnectionString;
```

**Áp dụng cho:**
- `LoadWeekSchedule()`
- `SaveSchedule()`
- `CloneWeek()`

### 4. frmBangLuong.cs - Quản Lý Bảng Lương

**Tìm:**
```csharp
string connectionString = ConfigurationManager.ConnectionStrings["HrDb"].ConnectionString;
```

**Thay bằng:**
```csharp
string connectionString = GlobalState.ConnectionString;
```

**Áp dụng cho:**
- `LoadPayroll()`
- `CalculatePayroll()`
- `ClosePayroll()`

### 5. frmDuyetDonTu.cs - Duyệt Đơn Từ

**Tìm:**
```csharp
string connectionString = ConfigurationManager.ConnectionStrings["HrDb"].ConnectionString;
```

**Thay bằng:**
```csharp
string connectionString = GlobalState.ConnectionString;
```

**Áp dụng cho:**
- `LoadPendingRequests()`
- `btnApprove_Click()`
- `btnReject_Click()`

### 6. frmTaoDonTu.cs - Tạo Đơn Từ

**Tìm:**
```csharp
string connectionString = ConfigurationManager.ConnectionStrings["HrDb"].ConnectionString;
```

**Thay bằng:**
```csharp
string connectionString = GlobalState.ConnectionString;
```

**Áp dụng cho:**
- `btnSubmit_Click()`
- `LoadEmployeeInfo()`

### 7. frmThongTinCaNhan.cs - Thông Tin Cá Nhân

**Tìm:**
```csharp
string connectionString = ConfigurationManager.ConnectionStrings["HrDb"].ConnectionString;
```

**Thay bằng:**
```csharp
string connectionString = GlobalState.ConnectionString;
```

**Áp dụng cho:**
- `LoadPersonalInfo()`
- `btnSave_Click()`

### 8. frmXemDonCuaToi.cs - Xem Đơn Của Tôi

**Tìm:**
```csharp
string connectionString = ConfigurationManager.ConnectionStrings["HrDb"].ConnectionString;
```

**Thay bằng:**
```csharp
string connectionString = GlobalState.ConnectionString;
```

**Áp dụng cho:**
- `LoadMyRequests()`

### 9. Các Form Quản Lý Danh Mục

Tương tự cho:
- `frmPhongBan.cs`
- `frmChucVu.cs`
- `frmCaLam.cs`

---

## 🔍 VÍ DỤ CHI TIẾT

### Ví Dụ 1: frmNhanVien.cs - Method LoadData()

#### TRƯỚC:

```csharp
private void LoadData()
{
    try
    {
        string connectionString = ConfigurationManager.ConnectionStrings["HrDb"].ConnectionString;
        
        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            string query = "SELECT * FROM dbo.vw_NhanVien_Full ORDER BY MaNV";
            SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            
            dgvNhanVien.DataSource = dt;
        }
    }
    catch (Exception ex)
    {
        MessageBox.Show("Lỗi tải dữ liệu: " + ex.Message);
    }
}
```

#### SAU:

```csharp
private void LoadData()
{
    try
    {
        // ✅ Dùng GlobalState thay vì ConfigurationManager
        string connectionString = GlobalState.ConnectionString;
        
        // ✅ Kiểm tra connection string có hợp lệ không
        if (string.IsNullOrEmpty(connectionString))
        {
            MessageBox.Show("Chưa có kết nối. Vui lòng đăng nhập lại.", 
                           "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }
        
        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            string query = "SELECT * FROM dbo.vw_NhanVien_Full ORDER BY MaNV";
            SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            
            dgvNhanVien.DataSource = dt;
        }
    }
    catch (SqlException ex)
    {
        // ✅ Xử lý lỗi permission nếu user không có quyền
        if (ex.Number == 229)  // Permission denied
        {
            MessageBox.Show("Bạn không có quyền truy cập chức năng này.", 
                           "Lỗi phân quyền", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        else
        {
            MessageBox.Show("Lỗi tải dữ liệu: " + ex.Message, 
                           "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
    catch (Exception ex)
    {
        MessageBox.Show("Lỗi không xác định: " + ex.Message, 
                       "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
    }
}
```

### Ví Dụ 2: frmChamCong.cs - Method CheckIn()

#### TRƯỚC:

```csharp
private void btnCheckIn_Click(object sender, EventArgs e)
{
    try
    {
        string connectionString = ConfigurationManager.ConnectionStrings["HrDb"].ConnectionString;
        
        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            conn.Open();
            
            SqlCommand cmd = new SqlCommand("dbo.sp_CheckIn", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@MaNV", UserSession.MaNV);
            cmd.Parameters.AddWithValue("@NgayLam", DateTime.Today);
            cmd.Parameters.AddWithValue("@GioVao", DateTime.Now);
            
            cmd.ExecuteNonQuery();
            
            MessageBox.Show("Check in thành công!");
            LoadData();
        }
    }
    catch (Exception ex)
    {
        MessageBox.Show("Lỗi check in: " + ex.Message);
    }
}
```

#### SAU:

```csharp
private void btnCheckIn_Click(object sender, EventArgs e)
{
    try
    {
        // ✅ Dùng GlobalState
        string connectionString = GlobalState.ConnectionString;
        
        if (string.IsNullOrEmpty(connectionString))
        {
            MessageBox.Show("Chưa có kết nối. Vui lòng đăng nhập lại.", 
                           "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }
        
        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            conn.Open();
            
            SqlCommand cmd = new SqlCommand("dbo.sp_CheckIn", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@MaNV", UserSession.MaNV);
            cmd.Parameters.AddWithValue("@NgayLam", DateTime.Today);
            cmd.Parameters.AddWithValue("@GioVao", DateTime.Now);
            
            cmd.ExecuteNonQuery();
            
            MessageBox.Show("Check in thành công!", 
                           "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
            LoadData();
        }
    }
    catch (SqlException ex)
    {
        // ✅ Xử lý các lỗi nghiệp vụ từ stored procedure
        MessageBox.Show($"Lỗi check in: {ex.Message}", 
                       "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
    }
    catch (Exception ex)
    {
        MessageBox.Show($"Lỗi không xác định: {ex.Message}", 
                       "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
    }
}
```

---

## 🛠️ CÔNG CỤ TÌM VÀ THAY NHANH

### Trong Visual Studio:

1. Mở **Find and Replace** (`Ctrl+H`)
2. **Find what:**
   ```
   ConfigurationManager.ConnectionStrings["HrDb"].ConnectionString
   ```
3. **Replace with:**
   ```
   GlobalState.ConnectionString
   ```
4. **Look in:** Current Project
5. Click **Replace All**

### Kiểm Tra Lại:

Sau khi thay, search lại để đảm bảo không còn:
```
ConfigurationManager.ConnectionStrings
```

Nếu còn, kiểm tra và thay thủ công.

---

## ✅ CHECKLIST CẬP NHẬT

Đánh dấu các form đã cập nhật:

- [ ] frmNhanVien.cs
- [ ] frmChamCong.cs
- [ ] frmLichTuan.cs
- [ ] frmPhanCa.cs
- [ ] frmBangLuong.cs
- [ ] frmDuyetDonTu.cs
- [ ] frmTaoDonTu.cs
- [ ] frmThongTinCaNhan.cs
- [ ] frmXemDonCuaToi.cs
- [ ] frmPhongBan.cs
- [ ] frmChucVu.cs
- [ ] frmCaLam.cs
- [ ] frmMain.cs (nếu có kết nối database)

---

## 🧪 KIỂM TRA SAU KHI CẬP NHẬT

### 1. Build Project

```
Ctrl+Shift+B
```

Đảm bảo không có lỗi compile.

### 2. Test Đăng Nhập

1. Chạy ứng dụng
2. Đăng nhập với `hr_mai` / `HR@2024`
3. Thử mở từng form và kiểm tra:
   - Data load được không?
   - Thao tác thêm/sửa/xóa hoạt động không?
   - Có lỗi permission không?

### 3. Test Phân Quyền

1. Đăng nhập với `nhanvien_binh` / `NV@2024`
2. Thử truy cập các form không có quyền
3. Đảm bảo hiển thị lỗi permission đúng

### 4. Test Đăng Xuất và Đăng Nhập Lại

1. Đăng xuất
2. Đăng nhập với tài khoản khác
3. Kiểm tra data hiển thị đúng theo quyền

---

## ⚠️ LƯU Ý

### 1. Không Xóa App.config

Giữ lại file `App.config` để tham khảo server name và database name. Nhưng **không sử dụng** trong code nữa.

### 2. Kiểm Tra using Statements

Sau khi thay, có thể xóa:
```csharp
using System.Configuration;
```
nếu không dùng nữa (nhưng không bắt buộc).

### 3. Error Handling

Nên thêm xử lý cho lỗi permission (SQL Error 229):
```csharp
catch (SqlException ex)
{
    if (ex.Number == 229)  // Permission denied
    {
        MessageBox.Show("Bạn không có quyền thực hiện thao tác này.");
    }
    else
    {
        MessageBox.Show($"Lỗi: {ex.Message}");
    }
}
```

### 4. Null Check

Luôn kiểm tra `GlobalState.ConnectionString` trước khi dùng:
```csharp
if (string.IsNullOrEmpty(GlobalState.ConnectionString))
{
    MessageBox.Show("Vui lòng đăng nhập lại.");
    return;
}
```

---

## 📊 THỐNG KÊ

Tổng số file cần cập nhật: **~12-15 files**  
Thời gian ước tính: **30-60 phút**  
Độ khó: **⭐⭐☆☆☆ (Dễ - chỉ cần Find & Replace)**

---

**Mẹo:** Sử dụng Find & Replace toàn project sẽ tiết kiệm thời gian hơn nhiều so với sửa từng file!
