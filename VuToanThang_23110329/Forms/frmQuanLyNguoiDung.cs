using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

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
            SetPermissions();
        }

        private void SetPermissions()
        {
            // Chỉ HR mới có quyền quản lý người dùng
            string userRole = UserSession.VaiTro ?? "NhanVien";
            
            if (userRole != "HR")
            {
                MessageBox.Show("Bạn không có quyền truy cập chức năng này!", "Thông báo", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.Close();
                return;
            }
        }

        private void SetupDataGridView()
        {
            dgvNguoiDung.Columns.Clear();
            
            dgvNguoiDung.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "MaNguoiDung",
                HeaderText = "Mã ND",
                DataPropertyName = "MaNguoiDung",
                Width = 60,
                ReadOnly = true
            });

            dgvNguoiDung.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "TenDangNhap",
                HeaderText = "Tên đăng nhập",
                DataPropertyName = "TenDangNhap",
                Width = 120,
                ReadOnly = true
            });

            dgvNguoiDung.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "HoTen",
                HeaderText = "Họ tên",
                DataPropertyName = "HoTen",
                Width = 150,
                ReadOnly = true
            });

            dgvNguoiDung.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "VaiTro",
                HeaderText = "Vai trò",
                DataPropertyName = "VaiTro",
                Width = 80,
                ReadOnly = true
            });

            dgvNguoiDung.Columns.Add(new DataGridViewCheckBoxColumn
            {
                Name = "KichHoat",
                HeaderText = "Kích hoạt",
                DataPropertyName = "KichHoat",
                Width = 80,
                ReadOnly = true
            });

            dgvNguoiDung.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "NgayTao",
                HeaderText = "Ngày tạo",
                DataPropertyName = "NgayTao",
                Width = 120,
                ReadOnly = true
            });

            dgvNguoiDung.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "PhongBan",
                HeaderText = "Phòng ban",
                DataPropertyName = "TenPhongBan",
                Width = 100,
                ReadOnly = true
            });

            dgvNguoiDung.AutoGenerateColumns = false;
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
                            nv.HoTen,
                            nd.VaiTro,
                            nd.KichHoat,
                            nd.NgayTao,
                            pb.TenPhongBan
                        FROM dbo.NguoiDung nd
                        LEFT JOIN dbo.NhanVien nv ON nd.MaNguoiDung = nv.MaNguoiDung
                        LEFT JOIN dbo.PhongBan pb ON nv.MaPhongBan = pb.MaPhongBan
                        ORDER BY nd.NgayTao DESC";

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
            ClearForm();
            isEditing = false;
            txtTenDangNhap.Focus();
            
            lblTrangThai.Text = "Nhập thông tin tài khoản mới";
        }

        private void ClearForm()
        {
            txtHoTen.Text = "";
            txtTenDangNhap.Text = "";
            txtMatKhau.Text = "";
            cmbVaiTro.SelectedIndex = -1;
            chkKichHoat.Checked = true;
            currentMaNguoiDung = -1;
            isEditing = false;
            UpdateButtonStates();
        }

        private void btnCapNhat_Click(object sender, EventArgs e)
        {
            if (!isEditing)
            {
                // Thêm mới
                ThemTaiKhoan();
            }
            else
            {
                // Cập nhật
                CapNhatTaiKhoan();
            }
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
                        
                        // Thông tin nhân viên cơ bản
                        cmd.Parameters.AddWithValue("@HoTen", txtHoTen.Text.Trim());
                        cmd.Parameters.AddWithValue("@NgaySinh", DBNull.Value);
                        cmd.Parameters.AddWithValue("@GioiTinh", DBNull.Value);
                        cmd.Parameters.AddWithValue("@DienThoai", DBNull.Value);
                        cmd.Parameters.AddWithValue("@Email", DBNull.Value);
                        cmd.Parameters.AddWithValue("@DiaChi", DBNull.Value);
                        cmd.Parameters.AddWithValue("@NgayVaoLam", DateTime.Now);
                        cmd.Parameters.AddWithValue("@MaPhongBan", DBNull.Value);
                        cmd.Parameters.AddWithValue("@MaChucVu", DBNull.Value);
                        cmd.Parameters.AddWithValue("@LuongCoBan", 0);
                        
                        // Thông tin tài khoản
                        cmd.Parameters.AddWithValue("@TenDangNhap", txtTenDangNhap.Text.Trim());
                        cmd.Parameters.AddWithValue("@MatKhau", txtMatKhau.Text);
                        cmd.Parameters.AddWithValue("@VaiTro", cmbVaiTro.Text);
                        
                        SqlParameter outputParam = new SqlParameter("@MaNV_OUT", SqlDbType.Int)
                        {
                            Direction = ParameterDirection.Output
                        };
                        cmd.Parameters.Add(outputParam);
                        
                        cmd.ExecuteNonQuery();
                        
                        MessageBox.Show("Tạo tài khoản thành công!", "Thành công", 
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        
                        LoadData();
                        ClearForm();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tạo tài khoản: {ex.Message}", "Lỗi", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CapNhatTaiKhoan()
        {
            if (currentMaNguoiDung <= 0) return;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    
                    // Cập nhật thông tin cơ bản
                    string sql = @"
                        UPDATE nd SET VaiTro = @VaiTro, KichHoat = @KichHoat
                        FROM dbo.NguoiDung nd
                        WHERE nd.MaNguoiDung = @MaNguoiDung;
                        
                        UPDATE nv SET HoTen = @HoTen
                        FROM dbo.NhanVien nv
                        WHERE nv.MaNguoiDung = @MaNguoiDung;";
                    
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaNguoiDung", currentMaNguoiDung);
                        cmd.Parameters.AddWithValue("@VaiTro", cmbVaiTro.Text);
                        cmd.Parameters.AddWithValue("@KichHoat", chkKichHoat.Checked);
                        cmd.Parameters.AddWithValue("@HoTen", txtHoTen.Text.Trim());
                        
                        cmd.ExecuteNonQuery();
                    }
                    
                    MessageBox.Show("Cập nhật thành công!", "Thành công", 
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    
                    LoadData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi cập nhật: {ex.Message}", "Lỗi", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDoiMatKhau_Click(object sender, EventArgs e)
        {
            if (currentMaNguoiDung <= 0 || string.IsNullOrEmpty(txtMatKhau.Text))
            {
                MessageBox.Show("Vui lòng nhập mật khẩu mới!", "Thông báo", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("dbo.sp_NguoiDung_DoiMatKhau", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@MaNguoiDung", currentMaNguoiDung);
                        cmd.Parameters.AddWithValue("@MatKhauMoi", txtMatKhau.Text);
                        
                        cmd.ExecuteNonQuery();
                        
                        MessageBox.Show("Đổi mật khẩu thành công!", "Thành công", 
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        
                        txtMatKhau.Text = "";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi đổi mật khẩu: {ex.Message}", "Lỗi", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
                        using (SqlCommand cmd = new SqlCommand("dbo.sp_VoHieuHoaTaiKhoan", conn))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@MaNguoiDung", currentMaNguoiDung);
                            cmd.Parameters.AddWithValue("@KichHoat", !currentStatus);
                            
                            cmd.ExecuteNonQuery();
                            
                            MessageBox.Show($"Đã {action} tài khoản thành công!", "Thành công", 
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                            
                            LoadData();
                        }
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
    }
}
