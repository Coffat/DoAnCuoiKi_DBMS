using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using Guna.UI2.WinForms;

namespace VuToanThang_23110329.Forms
{
    public partial class frmQuanLyNguoiDung : Form
    {
        private string connectionString;
        private DataTable dtNguoiDung;
        private bool isEditing = false;
        private int currentMaNguoiDung = -1;

        public frmQuanLyNguoiDung()
        {
            InitializeComponent();
            InitializeConnectionString();
        }

        private void InitializeConnectionString()
        {
            // Sử dụng GlobalState.ConnectionString nếu có, nếu không thì dùng App.config
            if (GlobalState.HasConnection())
            {
                connectionString = GlobalState.ConnectionString;
            }
            else
            {
                var cs = System.Configuration.ConfigurationManager.ConnectionStrings["HrDb"];
                connectionString = cs?.ConnectionString ?? "";
            }
        }

        private void frmQuanLyNguoiDung_Load(object sender, EventArgs e)
        {
            SetupDataGridView();
            LoadData();
        }

        private void SetPermissions()
        {
            // Debug: Hiển thị vai trò hiện tại
            lblTrangThai.Text = $"Vai trò hiện tại: {GlobalState.UserRole}";
            
            // Chỉ cho phép HR truy cập form này
            if (GlobalState.UserRole != "HR")
            {
                MessageBox.Show($"Bạn không có quyền truy cập chức năng này! Vai trò hiện tại: {GlobalState.UserRole}", "Thông báo", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.Close();
                return;
            }
        }

        private void SetupDataGridView()
        {
            // Guna2DataGridView tự động setup columns từ DataSource
            dgvNguoiDung.AutoGenerateColumns = true;
        }

        private void LoadData()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string sql = @"
                        SELECT 
                            nd.MaNguoiDung,
                            nd.TenDangNhap,
                            ISNULL(nv.HoTen, N'Chưa có thông tin') as HoTen,
                            nd.VaiTro,
                            nd.KichHoat,
                            ISNULL(pb.TenPhongBan, N'Chưa phân công') as TenPhongBan,
                            ISNULL(nv.NgayVaoLam, GETDATE()) as NgayVaoLam
                        FROM dbo.NguoiDung nd
                        LEFT JOIN dbo.NhanVien nv ON nd.MaNguoiDung = nv.MaNguoiDung
                        LEFT JOIN dbo.PhongBan pb ON nv.MaPhongBan = pb.MaPhongBan
                        ORDER BY nd.MaNguoiDung DESC";

                    SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);
                    dtNguoiDung = new DataTable();
                    adapter.Fill(dtNguoiDung);
                    
                    dgvNguoiDung.DataSource = dtNguoiDung;
                    
                    lblTrangThai.Text = $"Tổng số tài khoản: {dtNguoiDung.Rows.Count}";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải dữ liệu: {ex.Message}", "Lỗi", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (currentMaNguoiDung == -1)
            {
                ThemTaiKhoan();
            }
            else
            {
                CapNhatTaiKhoan();
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            ClearForm();
            LoadData();
        }

        private void dgvNguoiDung_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvNguoiDung.CurrentRow != null)
            {
                DataRowView row = dgvNguoiDung.CurrentRow.DataBoundItem as DataRowView;
                if (row != null)
                {
                    currentMaNguoiDung = Convert.ToInt32(row["MaNguoiDung"]);
                    txtTenDangNhap.Text = row["TenDangNhap"].ToString();
                    txtHoTen.Text = row["HoTen"].ToString();
                    cmbVaiTro.Text = row["VaiTro"].ToString();
                    chkKichHoat.Checked = Convert.ToBoolean(row["KichHoat"]);
                    
                    // Xóa mật khẩu khi chọn user khác
                    txtMatKhau.Text = "";
                    
                    isEditing = true;
                    UpdateButtonStates();
                }
            }
        }

        private void UpdateButtonStates()
        {
            bool hasSelection = dgvNguoiDung.CurrentRow != null;
            
            btnCapNhat.Enabled = hasSelection && isEditing;
            btnDoiMatKhau.Enabled = hasSelection && isEditing;
            btnKhoaMo.Enabled = hasSelection && isEditing;
            btnXoa.Enabled = hasSelection && isEditing;
        }

        private void btnTaiLai_Click(object sender, EventArgs e)
        {
            LoadData();
            ClearForm();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            isEditing = false;
            currentMaNguoiDung = -1;
            ClearForm();
            txtTenDangNhap.Enabled = true;
            txtMatKhau.Enabled = true;
            
            // Cập nhật UI cho chế độ thêm mới
            lblFormTitle.Text = "➕ Thêm người dùng mới";
            lblTrangThai.Text = "Nhập đầy đủ thông tin và nhấn Lưu";
            pnlFormButtons.Visible = true;
            btnLuu.Text = "✓ Thêm mới";
            
            // Focus vào textbox đầu tiên
            txtHoTen.Focus();
        }

        private void ClearForm()
        {
            txtHoTen.Clear();
            txtTenDangNhap.Clear();
            txtMatKhau.Clear();
            cmbVaiTro.SelectedIndex = -1;
            chkKichHoat.Checked = true;
            
            // Reset UI
            lblFormTitle.Text = "Thông tin chi tiết";
            lblTrangThai.Text = "";
            pnlFormButtons.Visible = false;
            txtTenDangNhap.Enabled = true;
            txtMatKhau.Enabled = true;
            isEditing = false;
            currentMaNguoiDung = -1;
        }

        private void btnCapNhat_Click(object sender, EventArgs e)
        {
            if (dgvNguoiDung.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn tài khoản cần cập nhật!", "Thông báo", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Chuyển sang chế độ chỉnh sửa
            isEditing = true;
            DataGridViewRow selectedRow = dgvNguoiDung.SelectedRows[0];
            currentMaNguoiDung = Convert.ToInt32(selectedRow.Cells["MaNguoiDung"].Value);
            
            txtTenDangNhap.Enabled = false; // Không cho sửa tên đăng nhập
            txtMatKhau.Enabled = false;     // Không cho sửa mật khẩu ở đây
            
            // Cập nhật UI cho chế độ chỉnh sửa
            lblFormTitle.Text = "✏️ Chỉnh sửa thông tin";
            lblTrangThai.Text = "Sửa thông tin và nhấn Lưu";
            pnlFormButtons.Visible = true;
            btnLuu.Text = "✓ Cập nhật";
        }

        private void btnKhoaMo_Click(object sender, EventArgs e)
        {
            if (currentMaNguoiDung <= 0) return;

            bool currentStatus = chkKichHoat.Checked;
            string action = currentStatus ? "khóa" : "mở khóa";
            
            DialogResult result = MessageBox.Show($"Bạn có chắc muốn {action} tài khoản này?", 
                "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            
            if (result == DialogResult.Yes)
            {
                try
                {
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        conn.Open();
                        
                        // Lấy MaNV từ MaNguoiDung
                        string getMaNV = "SELECT MaNV FROM dbo.NhanVien WHERE MaNguoiDung = @MaNguoiDung";
                        int maNV = 0;
                        
                        using (SqlCommand cmd = new SqlCommand(getMaNV, conn))
                        {
                            cmd.Parameters.AddWithValue("@MaNguoiDung", currentMaNguoiDung);
                            object result_obj = cmd.ExecuteScalar();
                            if (result_obj != null)
                                maNV = Convert.ToInt32(result_obj);
                        }
                        
                        if (maNV > 0)
                        {
                            using (SqlCommand cmd = new SqlCommand("dbo.sp_VoHieuHoaTaiKhoan", conn))
                            {
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.Parameters.AddWithValue("@MaNV", maNV);
                                cmd.Parameters.AddWithValue("@KichHoat", !currentStatus);
                                
                                cmd.ExecuteNonQuery();
                            }
                        }
                        else
                        {
                            MessageBox.Show("Không tìm thấy nhân viên tương ứng.", "Lỗi", 
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        
                        MessageBox.Show($"Đã {action} tài khoản thành công!", "Thành công", 
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        
                        LoadData();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi khi {action} tài khoản: {ex.Message}", "Lỗi", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (currentMaNguoiDung <= 0) return;

            DialogResult result = MessageBox.Show(
                "CẢNH BÁO: Thao tác này sẽ xóa hoàn toàn tài khoản và tất cả dữ liệu liên quan!\n\n" +
                "Bạn có chắc chắn muốn xóa?", 
                "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            
            if (result == DialogResult.Yes)
            {
                try
                {
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        conn.Open();
                        
                        // Lấy MaNV trước khi xóa
                        string getMaNV = "SELECT MaNV FROM dbo.NhanVien WHERE MaNguoiDung = @MaNguoiDung";
                        int maNV = 0;
                        
                        using (SqlCommand cmd = new SqlCommand(getMaNV, conn))
                        {
                            cmd.Parameters.AddWithValue("@MaNguoiDung", currentMaNguoiDung);
                            object result_obj = cmd.ExecuteScalar();
                            if (result_obj != null)
                                maNV = Convert.ToInt32(result_obj);
                        }
                        
                        if (maNV > 0)
                        {
                            using (SqlCommand cmd = new SqlCommand("dbo.sp_XoaTaiKhoanDayDu", conn))
                            {
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.Parameters.AddWithValue("@MaNV", maNV);
                                
                                cmd.ExecuteNonQuery();
                                
                                MessageBox.Show("Xóa tài khoản thành công!", "Thành công", 
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                                
                                LoadData();
                                ClearForm();
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi khi xóa tài khoản: {ex.Message}", "Lỗi", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(txtHoTen.Text))
            {
                MessageBox.Show("Vui lòng nhập họ tên!", "Thông báo", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtHoTen.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtTenDangNhap.Text))
            {
                MessageBox.Show("Vui lòng nhập tên đăng nhập!", "Thông báo", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenDangNhap.Focus();
                return false;
            }

            if (!isEditing && string.IsNullOrWhiteSpace(txtMatKhau.Text))
            {
                MessageBox.Show("Vui lòng nhập mật khẩu!", "Thông báo", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMatKhau.Focus();
                return false;
            }

            if (cmbVaiTro.SelectedIndex == -1)
            {
                MessageBox.Show("Vui lòng chọn vai trò!", "Thông báo", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbVaiTro.Focus();
                return false;
            }

            return true;
        }

        private void ThemTaiKhoan()
        {
            if (!ValidateInput()) return;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("dbo.sp_TaoTaiKhoanDayDu", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@HoTen", txtHoTen.Text.Trim());
                        cmd.Parameters.AddWithValue("@NgaySinh", DBNull.Value);
                        cmd.Parameters.AddWithValue("@GioiTinh", DBNull.Value);
                        cmd.Parameters.AddWithValue("@DienThoai", DBNull.Value);
                        cmd.Parameters.AddWithValue("@Email", DBNull.Value);
                        cmd.Parameters.AddWithValue("@DiaChi", DBNull.Value);
                        cmd.Parameters.AddWithValue("@NgayVaoLam", DateTime.Now);
                        cmd.Parameters.AddWithValue("@MaPhongBan", DBNull.Value);
                        cmd.Parameters.AddWithValue("@MaChucVu", DBNull.Value);
                        cmd.Parameters.AddWithValue("@LuongCoBan", 8000000); // Lương cơ bản mặc định
                        cmd.Parameters.AddWithValue("@TenDangNhap", txtTenDangNhap.Text.Trim());
                        cmd.Parameters.AddWithValue("@MatKhau", txtMatKhau.Text);
                        cmd.Parameters.AddWithValue("@VaiTro", cmbVaiTro.SelectedItem.ToString());
                        
                        // Output parameter
                        SqlParameter outputParam = new SqlParameter("@MaNV_OUT", SqlDbType.Int);
                        outputParam.Direction = ParameterDirection.Output;
                        cmd.Parameters.Add(outputParam);

                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Thêm tài khoản thành công!", "Thông báo", 
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        
                        LoadData();
                        ClearForm();
                    }
                }
            }
            catch (Exception ex)
            {
                // Debug: Hiển thị chi tiết lỗi
                string errorDetails = $"Lỗi khi thêm tài khoản:\n{ex.Message}";
                if (ex.InnerException != null)
                {
                    errorDetails += $"\n\nInner Exception: {ex.InnerException.Message}";
                }
                MessageBox.Show(errorDetails, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CapNhatTaiKhoan()
        {
            if (!ValidateInput()) return;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string sql = @"
                        UPDATE dbo.NguoiDung 
                        SET VaiTro = @VaiTro, KichHoat = @KichHoat
                        WHERE MaNguoiDung = @MaNguoiDung;
                        
                        UPDATE dbo.NhanVien 
                        SET HoTen = @HoTen
                        WHERE MaNguoiDung = @MaNguoiDung";

                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaNguoiDung", currentMaNguoiDung);
                        cmd.Parameters.AddWithValue("@HoTen", txtHoTen.Text.Trim());
                        cmd.Parameters.AddWithValue("@VaiTro", cmbVaiTro.SelectedItem.ToString());
                        cmd.Parameters.AddWithValue("@KichHoat", chkKichHoat.Checked);

                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Cập nhật tài khoản thành công!", "Thông báo", 
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        
                        LoadData();
                        ClearForm();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi cập nhật tài khoản: {ex.Message}", "Lỗi", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDoiMatKhau_Click(object sender, EventArgs e)
        {
            if (dgvNguoiDung.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn tài khoản cần đổi mật khẩu!", "Thông báo", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Tạo form đơn giản để nhập mật khẩu
            Form inputForm = new Form()
            {
                Width = 400,
                Height = 200,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                Text = "Đổi mật khẩu",
                StartPosition = FormStartPosition.CenterParent,
                MaximizeBox = false,
                MinimizeBox = false
            };
            
            Label lblPrompt = new Label() { Left = 20, Top = 20, Text = "Nhập mật khẩu mới:", Width = 150 };
            TextBox txtNewPassword = new TextBox() { Left = 20, Top = 50, Width = 340, PasswordChar = '*' };
            Button btnOK = new Button() { Text = "OK", Left = 200, Width = 80, Top = 90, DialogResult = DialogResult.OK };
            Button btnCancel = new Button() { Text = "Hủy", Left = 290, Width = 80, Top = 90, DialogResult = DialogResult.Cancel };
            
            inputForm.Controls.Add(lblPrompt);
            inputForm.Controls.Add(txtNewPassword);
            inputForm.Controls.Add(btnOK);
            inputForm.Controls.Add(btnCancel);
            inputForm.AcceptButton = btnOK;
            inputForm.CancelButton = btnCancel;
            
            if (inputForm.ShowDialog() != DialogResult.OK || string.IsNullOrWhiteSpace(txtNewPassword.Text))
            {
                return;
            }
            
            string newPassword = txtNewPassword.Text;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("dbo.sp_NguoiDung_DoiMatKhau", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@MaNguoiDung", currentMaNguoiDung);
                        cmd.Parameters.AddWithValue("@MatKhauCu", ""); // Để trống vì HR có quyền đổi
                        cmd.Parameters.AddWithValue("@MatKhauMoi", newPassword);

                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Đổi mật khẩu thành công!", "Thông báo", 
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi đổi mật khẩu: {ex.Message}", "Lỗi", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
