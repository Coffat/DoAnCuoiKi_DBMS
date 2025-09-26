using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Guna.UI2.WinForms;

namespace VuToanThang_23110329.Forms
{
    public partial class frmNhanVien : Form
    {
        private string connectionString;
        private DataTable dtNhanVien;
        private DataTable dtPhongBan;
        private DataTable dtChucVu;
        private bool isEditing = false;
        private int currentMaNV = -1;
        private string currentUserRole = "";

        public frmNhanVien()
        {
            InitializeComponent();
            InitializeConnectionString();
        }

        public frmNhanVien(string userRole)
        {
            InitializeComponent();
            currentUserRole = userRole;
            InitializeConnectionString();
        }

        private void InitializeConnectionString()
        {
            try
            {
                var connectionStringSettings = System.Configuration.ConfigurationManager.ConnectionStrings["HrDb"];
                if (connectionStringSettings != null)
                {
                    connectionString = connectionStringSettings.ConnectionString;
                }
                else
                {
                    MessageBox.Show("Không tìm thấy connection string 'HrDb' trong App.config", "Lỗi cấu hình", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    connectionString = "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi khởi tạo connection string: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                connectionString = "";
            }
        }

        private void frmNhanVien_Load(object sender, EventArgs e)
        {
            // Lấy vai trò người dùng hiện tại
            GetCurrentUserRole();
            SetupDataGridView();
            LoadPhongBanChucVu();
            LoadData();
            SetPermissions();
        }

        private void GetCurrentUserRole()
        {
            // Nếu chưa có userRole được truyền vào, lấy từ session hoặc global variable
            if (string.IsNullOrEmpty(currentUserRole))
            {
                // Tạm thời set default là HR để test
                currentUserRole = "HR"; // Sẽ được lấy từ session thực tế
            }
        }

        private void SetPermissions()
        {
            // Phân quyền theo vai trò
            switch (currentUserRole)
            {
                case "HR":
                    // HR có toàn quyền
                    btnThem.Enabled = true;
                    btnSua.Enabled = true;
                    btnVoHieuHoa.Enabled = true;
                    break;
                case "QuanLy":
                case "KeToan":
                    // Quản lý và Kế toán chỉ xem
                    btnThem.Enabled = false;
                    btnSua.Enabled = false;
                    btnVoHieuHoa.Enabled = false;
                    break;
                default:
                    // Nhân viên thường chỉ xem
                    btnThem.Enabled = false;
                    btnSua.Enabled = false;
                    btnVoHieuHoa.Enabled = false;
                    break;
            }
        }

        private void SetupDataGridView()
        {
            dgvNhanVien.AutoGenerateColumns = false;
            dgvNhanVien.Columns.Clear();

            // Thêm các cột
            dgvNhanVien.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "MaNV",
                HeaderText = "Mã NV",
                DataPropertyName = "MaNV",
                Width = 80,
                ReadOnly = true
            });

            dgvNhanVien.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "HoTen",
                HeaderText = "Họ tên",
                DataPropertyName = "HoTen",
                Width = 150,
                ReadOnly = true
            });

            dgvNhanVien.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "NgaySinh",
                HeaderText = "Ngày sinh",
                DataPropertyName = "NgaySinh",
                Width = 100,
                ReadOnly = true,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "dd/MM/yyyy" }
            });

            dgvNhanVien.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "GioiTinh",
                HeaderText = "Giới tính",
                DataPropertyName = "GioiTinh",
                Width = 80,
                ReadOnly = true
            });

            dgvNhanVien.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "DienThoai",
                HeaderText = "Điện thoại",
                DataPropertyName = "DienThoai",
                Width = 120,
                ReadOnly = true
            });

            dgvNhanVien.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Email",
                HeaderText = "Email",
                DataPropertyName = "Email",
                Width = 150,
                ReadOnly = true
            });

            dgvNhanVien.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "TenPhongBan",
                HeaderText = "Phòng ban",
                DataPropertyName = "TenPhongBan",
                Width = 120,
                ReadOnly = true
            });

            dgvNhanVien.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "TenChucVu",
                HeaderText = "Chức vụ",
                DataPropertyName = "TenChucVu",
                Width = 120,
                ReadOnly = true
            });

            dgvNhanVien.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "LuongCoBan",
                HeaderText = "Lương cơ bản",
                DataPropertyName = "LuongCoBan",
                Width = 120,
                ReadOnly = true,
                DefaultCellStyle = new DataGridViewCellStyle 
                { 
                    Format = "N0",
                    Alignment = DataGridViewContentAlignment.MiddleRight
                }
            });

            dgvNhanVien.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "NgayVaoLam",
                HeaderText = "Ngày vào làm",
                DataPropertyName = "NgayVaoLam",
                Width = 120,
                ReadOnly = true,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "dd/MM/yyyy" }
            });

            dgvNhanVien.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "TrangThai",
                HeaderText = "Trạng thái",
                DataPropertyName = "TrangThai",
                Width = 100,
                ReadOnly = true
            });
        }

        private void LoadPhongBanChucVu()
        {
            if (string.IsNullOrEmpty(connectionString))
            {
                MessageBox.Show("Connection string chưa được khởi tạo.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("sp_GetPhongBanChucVu", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            DataSet ds = new DataSet();
                            adapter.Fill(ds);

                            // Kiểm tra có dữ liệu không
                            if (ds.Tables.Count < 2)
                            {
                                MessageBox.Show("Stored procedure không trả về đủ dữ liệu. Vui lòng kiểm tra database.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }

                            // Load phòng ban
                            dtPhongBan = ds.Tables[0];
                            if (dtPhongBan.Rows.Count == 0)
                            {
                                MessageBox.Show("Không có dữ liệu phòng ban. Vui lòng thêm dữ liệu mẫu.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                            
                            var phongBanBinding = dtPhongBan.Copy();
                            var newRowPB = phongBanBinding.NewRow();
                            newRowPB["MaPhongBan"] = DBNull.Value;
                            newRowPB["TenPhongBan"] = "-- Chọn phòng ban --";
                            phongBanBinding.Rows.InsertAt(newRowPB, 0);

                            cmbPhongBan.DataSource = phongBanBinding.Copy();
                            cmbPhongBan.DisplayMember = "TenPhongBan";
                            cmbPhongBan.ValueMember = "MaPhongBan";
                            if (cmbPhongBan.Items.Count > 0)
                                cmbPhongBan.SelectedIndex = 0;

                            cmbPhongBanForm.DataSource = phongBanBinding.Copy();
                            cmbPhongBanForm.DisplayMember = "TenPhongBan";
                            cmbPhongBanForm.ValueMember = "MaPhongBan";
                            if (cmbPhongBanForm.Items.Count > 0)
                                cmbPhongBanForm.SelectedIndex = 0;

                            // Load chức vụ
                            dtChucVu = ds.Tables[1];
                            if (dtChucVu.Rows.Count == 0)
                            {
                                MessageBox.Show("Không có dữ liệu chức vụ. Vui lòng thêm dữ liệu mẫu.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                            
                            var chucVuBinding = dtChucVu.Copy();
                            var newRowCV = chucVuBinding.NewRow();
                            newRowCV["MaChucVu"] = DBNull.Value;
                            newRowCV["TenChucVu"] = "-- Chọn chức vụ --";
                            chucVuBinding.Rows.InsertAt(newRowCV, 0);

                            cmbChucVu.DataSource = chucVuBinding;
                            cmbChucVu.DisplayMember = "TenChucVu";
                            cmbChucVu.ValueMember = "MaChucVu";
                            if (cmbChucVu.Items.Count > 0)
                                cmbChucVu.SelectedIndex = 0;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải phòng ban và chức vụ: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadData()
        {
            if (string.IsNullOrEmpty(connectionString))
            {
                MessageBox.Show("Connection string chưa được khởi tạo.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("sp_GetNhanVienFull", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            dtNhanVien = new DataTable();
                            adapter.Fill(dtNhanVien);
                            FilterData();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải dữ liệu nhân viên: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FilterData()
        {
            if (dtNhanVien == null) return;

            string filter = "1=1";

            // Lọc theo tìm kiếm
            if (!string.IsNullOrEmpty(txtTimKiem.Text.Trim()))
            {
                string searchText = txtTimKiem.Text.Trim();
                filter += $" AND (HoTen LIKE '%{searchText}%' OR DienThoai LIKE '%{searchText}%' OR Email LIKE '%{searchText}%')";
            }

            // Lọc theo phòng ban
            if (cmbPhongBan.SelectedValue != null && cmbPhongBan.SelectedValue != DBNull.Value)
            {
                filter += $" AND MaPhongBan = {cmbPhongBan.SelectedValue}";
            }

            // Lọc theo trạng thái
            if (cmbTrangThai.SelectedIndex > 0)
            {
                string trangThai = cmbTrangThai.SelectedIndex == 1 ? "DangLam" : "Nghi";
                filter += $" AND TrangThai = '{trangThai}'";
            }

            DataView dv = dtNhanVien.DefaultView;
            dv.RowFilter = filter;
            dgvNhanVien.DataSource = dv.ToTable();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (currentUserRole != "HR")
            {
                MessageBox.Show("Bạn không có quyền thêm nhân viên.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            isEditing = false;
            currentMaNV = -1;
            ClearForm();
            pnlRight.Visible = true;
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (currentUserRole != "HR")
            {
                MessageBox.Show("Bạn không có quyền sửa thông tin nhân viên.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (dgvNhanVien.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn nhân viên cần sửa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            isEditing = true;
            currentMaNV = Convert.ToInt32(dgvNhanVien.SelectedRows[0].Cells["MaNV"].Value);
            LoadEmployeeToForm(currentMaNV);
            pnlRight.Visible = true;
        }

        private void btnVoHieuHoa_Click(object sender, EventArgs e)
        {
            if (currentUserRole != "HR")
            {
                MessageBox.Show("Bạn không có quyền vô hiệu hóa nhân viên.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (dgvNhanVien.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn nhân viên cần vô hiệu hóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int maNV = Convert.ToInt32(dgvNhanVien.SelectedRows[0].Cells["MaNV"].Value);
            string hoTen = dgvNhanVien.SelectedRows[0].Cells["HoTen"].Value.ToString();
            string trangThai = dgvNhanVien.SelectedRows[0].Cells["TrangThai"].Value.ToString();

            string action = trangThai == "DangLam" ? "vô hiệu hóa" : "kích hoạt lại";
            string newStatus = trangThai == "DangLam" ? "Nghi" : "DangLam";

            if (MessageBox.Show($"Bạn có chắc chắn muốn {action} nhân viên {hoTen}?", "Xác nhận", 
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        conn.Open();
                        using (SqlCommand cmd = new SqlCommand("UPDATE NhanVien SET TrangThai = @TrangThai WHERE MaNV = @MaNV", conn))
                        {
                            cmd.Parameters.AddWithValue("@TrangThai", newStatus);
                            cmd.Parameters.AddWithValue("@MaNV", maNV);
                            cmd.ExecuteNonQuery();
                        }
                    }
                    MessageBox.Show($"Đã {action} nhân viên thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi khi {action} nhân viên: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            LoadData();
            txtTimKiem.Text = "";
            if (cmbPhongBan.Items.Count > 0)
                cmbPhongBan.SelectedIndex = 0;
            if (cmbTrangThai.Items.Count > 0)
                cmbTrangThai.SelectedIndex = 0;
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (!ValidateForm()) return;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    
                    if (isEditing)
                    {
                        // Cập nhật nhân viên
                        UpdateEmployee(conn);
                    }
                    else
                    {
                        // Thêm mới nhân viên
                        AddNewEmployee(conn);
                    }
                }

                MessageBox.Show(isEditing ? "Cập nhật nhân viên thành công." : "Thêm mới nhân viên thành công.", 
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
                LoadData();
                pnlRight.Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi lưu thông tin nhân viên: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AddNewEmployee(SqlConnection conn)
        {
            using (SqlCommand cmd = new SqlCommand("sp_ThemMoiNhanVien", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                
                cmd.Parameters.AddWithValue("@HoTen", txtHoTen.Text.Trim());
                cmd.Parameters.AddWithValue("@NgaySinh", dtpNgaySinh.Value.Date);
                cmd.Parameters.AddWithValue("@GioiTinh", cmbGioiTinh.Text == "" ? (object)DBNull.Value : cmbGioiTinh.Text);
                cmd.Parameters.AddWithValue("@DienThoai", string.IsNullOrEmpty(txtDienThoai.Text.Trim()) ? (object)DBNull.Value : txtDienThoai.Text.Trim());
                cmd.Parameters.AddWithValue("@Email", string.IsNullOrEmpty(txtEmail.Text.Trim()) ? (object)DBNull.Value : txtEmail.Text.Trim());
                cmd.Parameters.AddWithValue("@DiaChi", string.IsNullOrEmpty(txtDiaChi.Text.Trim()) ? (object)DBNull.Value : txtDiaChi.Text.Trim());
                cmd.Parameters.AddWithValue("@NgayVaoLam", dtpNgayVaoLam.Value.Date);
                cmd.Parameters.AddWithValue("@MaPhongBan", cmbPhongBanForm.SelectedValue == DBNull.Value ? (object)DBNull.Value : cmbPhongBanForm.SelectedValue);
                cmd.Parameters.AddWithValue("@MaChucVu", cmbChucVu.SelectedValue == DBNull.Value ? (object)DBNull.Value : cmbChucVu.SelectedValue);
                cmd.Parameters.AddWithValue("@LuongCoBan", numLuongCoBan.Value);
                cmd.Parameters.AddWithValue("@TaoTaiKhoan", 0); // Không tạo tài khoản mặc định
                
                SqlParameter outputParam = new SqlParameter("@MaNV_OUT", SqlDbType.Int);
                outputParam.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(outputParam);
                
                cmd.ExecuteNonQuery();
            }
        }

        private void UpdateEmployee(SqlConnection conn)
        {
            // Cập nhật thông tin cơ bản
            using (SqlCommand cmd = new SqlCommand(@"
                UPDATE NhanVien SET 
                    HoTen = @HoTen,
                    NgaySinh = @NgaySinh,
                    GioiTinh = @GioiTinh,
                    DienThoai = @DienThoai,
                    Email = @Email,
                    DiaChi = @DiaChi,
                    NgayVaoLam = @NgayVaoLam,
                    LuongCoBan = @LuongCoBan
                WHERE MaNV = @MaNV", conn))
            {
                cmd.Parameters.AddWithValue("@MaNV", currentMaNV);
                cmd.Parameters.AddWithValue("@HoTen", txtHoTen.Text.Trim());
                cmd.Parameters.AddWithValue("@NgaySinh", dtpNgaySinh.Value.Date);
                cmd.Parameters.AddWithValue("@GioiTinh", cmbGioiTinh.Text == "" ? (object)DBNull.Value : cmbGioiTinh.Text);
                cmd.Parameters.AddWithValue("@DienThoai", string.IsNullOrEmpty(txtDienThoai.Text.Trim()) ? (object)DBNull.Value : txtDienThoai.Text.Trim());
                cmd.Parameters.AddWithValue("@Email", string.IsNullOrEmpty(txtEmail.Text.Trim()) ? (object)DBNull.Value : txtEmail.Text.Trim());
                cmd.Parameters.AddWithValue("@DiaChi", string.IsNullOrEmpty(txtDiaChi.Text.Trim()) ? (object)DBNull.Value : txtDiaChi.Text.Trim());
                cmd.Parameters.AddWithValue("@NgayVaoLam", dtpNgayVaoLam.Value.Date);
                cmd.Parameters.AddWithValue("@LuongCoBan", numLuongCoBan.Value);
                
                cmd.ExecuteNonQuery();
            }

            // Cập nhật phòng ban và chức vụ
            using (SqlCommand cmd = new SqlCommand("sp_UpdateNhanVienWithPhongBanChucVu", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@MaNV", currentMaNV);
                cmd.Parameters.AddWithValue("@MaPhongBan", cmbPhongBanForm.SelectedValue == DBNull.Value ? (object)DBNull.Value : cmbPhongBanForm.SelectedValue);
                cmd.Parameters.AddWithValue("@MaChucVu", cmbChucVu.SelectedValue == DBNull.Value ? (object)DBNull.Value : cmbChucVu.SelectedValue);
                
                cmd.ExecuteNonQuery();
            }
        }

        private bool ValidateForm()
        {
            if (string.IsNullOrEmpty(txtHoTen.Text.Trim()))
            {
                MessageBox.Show("Vui lòng nhập họ tên nhân viên.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtHoTen.Focus();
                return false;
            }

            if (numLuongCoBan.Value <= 0)
            {
                MessageBox.Show("Lương cơ bản phải lớn hơn 0.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                numLuongCoBan.Focus();
                return false;
            }

            return true;
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            pnlRight.Visible = false;
            ClearForm();
        }

        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            FilterData();
        }

        private void dgvNhanVien_SelectionChanged(object sender, EventArgs e)
        {
            // Cập nhật trạng thái buttons dựa trên selection
            bool hasSelection = dgvNhanVien.SelectedRows.Count > 0;
            btnSua.Enabled = hasSelection && currentUserRole == "HR";
            btnVoHieuHoa.Enabled = hasSelection && currentUserRole == "HR";
        }

        private void cmbPhongBan_SelectedIndexChanged(object sender, EventArgs e)
        {
            FilterData();
        }

        private void cmbTrangThai_SelectedIndexChanged(object sender, EventArgs e)
        {
            FilterData();
        }

        private void ClearForm()
        {
            txtHoTen.Text = "";
            dtpNgaySinh.Value = DateTime.Now.AddYears(-25);
            if (cmbGioiTinh.Items.Count > 0)
                cmbGioiTinh.SelectedIndex = -1;
            txtDienThoai.Text = "";
            txtEmail.Text = "";
            txtDiaChi.Text = "";
            dtpNgayVaoLam.Value = DateTime.Now;
            numLuongCoBan.Value = 5000000;
            if (cmbPhongBanForm.Items.Count > 0)
                cmbPhongBanForm.SelectedIndex = 0;
            if (cmbChucVu.Items.Count > 0)
                cmbChucVu.SelectedIndex = 0;
        }

        private void LoadEmployeeToForm(int maNV)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("sp_GetNhanVienFull", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@MaNV", maNV);
                        
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                txtHoTen.Text = reader["HoTen"].ToString();
                                if (reader["NgaySinh"] != DBNull.Value)
                                    dtpNgaySinh.Value = Convert.ToDateTime(reader["NgaySinh"]);
                                cmbGioiTinh.Text = reader["GioiTinh"].ToString();
                                txtDienThoai.Text = reader["DienThoai"].ToString();
                                txtEmail.Text = reader["Email"].ToString();
                                txtDiaChi.Text = reader["DiaChi"].ToString();
                                dtpNgayVaoLam.Value = Convert.ToDateTime(reader["NgayVaoLam"]);
                                numLuongCoBan.Value = Convert.ToDecimal(reader["LuongCoBan"]);
                                
                                if (reader["MaPhongBan"] != DBNull.Value && cmbPhongBanForm.Items.Count > 0)
                                    cmbPhongBanForm.SelectedValue = reader["MaPhongBan"];
                                else if (cmbPhongBanForm.Items.Count > 0)
                                    cmbPhongBanForm.SelectedIndex = 0;
                                    
                                if (reader["MaChucVu"] != DBNull.Value && cmbChucVu.Items.Count > 0)
                                    cmbChucVu.SelectedValue = reader["MaChucVu"];
                                else if (cmbChucVu.Items.Count > 0)
                                    cmbChucVu.SelectedIndex = 0;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải thông tin nhân viên: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
