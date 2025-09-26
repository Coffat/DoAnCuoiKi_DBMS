using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

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

        private void SetupDataGridView()
        {
            // Xóa các cột hiện tại
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
                Name = "NgayVaoLam",
                HeaderText = "Ngày vào làm",
                DataPropertyName = "NgayVaoLam",
                Width = 100,
                ReadOnly = true
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
                MessageBox.Show($"Lỗi khi tải phòng ban và chức vụ: {ex.Message}\n\nĐang thử phương pháp dự phòng...", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                LoadPhongBanChucVuFallback();
            }
        }

        private void LoadPhongBanChucVuFallback()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    
                    // Load phòng ban trực tiếp
                    using (SqlCommand cmd = new SqlCommand("SELECT MaPhongBan, TenPhongBan FROM dbo.PhongBan WHERE KichHoat = 1 ORDER BY TenPhongBan", conn))
                    {
                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            dtPhongBan = new DataTable();
                            adapter.Fill(dtPhongBan);
                            
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
                        }
                    }
                    
                    // Load chức vụ trực tiếp
                    using (SqlCommand cmd = new SqlCommand("SELECT MaChucVu, TenChucVu FROM dbo.ChucVu WHERE KichHoat = 1 ORDER BY TenChucVu", conn))
                    {
                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            dtChucVu = new DataTable();
                            adapter.Fill(dtChucVu);
                            
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
                MessageBox.Show($"Lỗi khi tải dữ liệu dự phòng: {ex.Message}\n\nVui lòng kiểm tra database và dữ liệu mẫu.", "Lỗi nghiêm trọng", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                            dgvNhanVien.DataSource = dtNhanVien;
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

            string searchText = txtTimKiem.Text.ToLower();
            string phongBanFilter = cmbPhongBan.SelectedValue?.ToString();
            string trangThaiFilter = cmbTrangThai.SelectedItem?.ToString();

            DataView dv = new DataView(dtNhanVien);
            string filterExpression = "";

            // Tìm kiếm theo tên
            if (!string.IsNullOrEmpty(searchText))
            {
                filterExpression += $"HoTen LIKE '%{searchText}%'";
            }

            // Lọc theo phòng ban
            if (!string.IsNullOrEmpty(phongBanFilter) && phongBanFilter != "System.DBNull")
            {
                if (!string.IsNullOrEmpty(filterExpression))
                    filterExpression += " AND ";
                filterExpression += $"MaPhongBan = {phongBanFilter}";
            }

            // Lọc theo trạng thái
            if (!string.IsNullOrEmpty(trangThaiFilter) && trangThaiFilter != "Tất cả")
            {
                if (!string.IsNullOrEmpty(filterExpression))
                    filterExpression += " AND ";
                filterExpression += $"TrangThai = '{trangThaiFilter}'";
            }

            dv.RowFilter = filterExpression;
            dgvNhanVien.DataSource = dv;
        }

        private void SetPermissions()
        {
            bool isHR = currentUserRole == "HR";
            bool isQuanLy = currentUserRole == "QuanLy";
            bool isKeToan = currentUserRole == "KeToan";

            // HR và QuanLy có toàn quyền CRUD nhân viên
            if (isHR || isQuanLy)
            {
                btnThem.Enabled = true;
                btnSua.Enabled = true;
                btnVoHieuHoa.Enabled = true;
            }
            // Kế toán chỉ xem (không được thêm/sửa/xóa)
            else if (isKeToan)
            {
                btnThem.Enabled = false;
                btnSua.Enabled = false;
                btnVoHieuHoa.Enabled = false;
            }
            // Nhân viên không có quyền
            else
            {
                btnThem.Enabled = false;
                btnSua.Enabled = false;
                btnVoHieuHoa.Enabled = false;
            }

            btnLamMoi.Enabled = true;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            isEditing = false;
            currentMaNV = -1;
            ClearForm();
            pnlRight.Visible = true;
            txtHoTen.Focus();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
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
            if (dgvNhanVien.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn nhân viên cần vô hiệu hóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("Bạn có chắc chắn muốn vô hiệu hóa nhân viên này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                int maNV = Convert.ToInt32(dgvNhanVien.SelectedRows[0].Cells["MaNV"].Value);
                UpdateEmployeeStatus(maNV, "Nghỉ việc");
            }
        }

        private void UpdateEmployeeStatus(int maNV, string trangThai)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("UPDATE dbo.NhanVien SET TrangThai = @TrangThai WHERE MaNV = @MaNV", conn))
                    {
                        cmd.Parameters.AddWithValue("@TrangThai", trangThai);
                        cmd.Parameters.AddWithValue("@MaNV", maNV);
                        cmd.ExecuteNonQuery();
                    }
                }
                LoadData();
                MessageBox.Show("Cập nhật trạng thái nhân viên thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi cập nhật trạng thái: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

            if (isEditing)
                UpdateEmployee();
            else
                AddNewEmployee();
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            pnlRight.Visible = false;
            ClearForm();
        }

        private bool ValidateForm()
        {
            if (string.IsNullOrWhiteSpace(txtHoTen.Text))
            {
                MessageBox.Show("Vui lòng nhập họ tên.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtHoTen.Focus();
                return false;
            }

            if (dtpNgayVaoLam.Value > DateTime.Now)
            {
                MessageBox.Show("Ngày vào làm không được lớn hơn ngày hiện tại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dtpNgayVaoLam.Focus();
                return false;
            }

            if (numLuongCoBan.Value <= 0)
            {
                MessageBox.Show("Lương cơ bản phải lớn hơn 0.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                numLuongCoBan.Focus();
                return false;
            }

            return true;
        }

        private void AddNewEmployee()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("sp_ThemMoiNhanVien", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@HoTen", txtHoTen.Text.Trim());
                        cmd.Parameters.AddWithValue("@NgaySinh", dtpNgaySinh.Value);
                        cmd.Parameters.AddWithValue("@GioiTinh", cmbGioiTinh.SelectedItem?.ToString() ?? "");
                        cmd.Parameters.AddWithValue("@DienThoai", txtDienThoai.Text.Trim());
                        cmd.Parameters.AddWithValue("@Email", txtEmail.Text.Trim());
                        cmd.Parameters.AddWithValue("@DiaChi", txtDiaChi.Text.Trim());
                        cmd.Parameters.AddWithValue("@NgayVaoLam", dtpNgayVaoLam.Value);
                        cmd.Parameters.AddWithValue("@LuongCoBan", numLuongCoBan.Value);
                        cmd.Parameters.AddWithValue("@MaPhongBan", cmbPhongBanForm.SelectedValue == DBNull.Value ? (object)DBNull.Value : cmbPhongBanForm.SelectedValue);
                        cmd.Parameters.AddWithValue("@MaChucVu", cmbChucVu.SelectedValue == DBNull.Value ? (object)DBNull.Value : cmbChucVu.SelectedValue);

                        cmd.ExecuteNonQuery();
                    }
                }
                LoadData();
                pnlRight.Visible = false;
                ClearForm();
                MessageBox.Show("Thêm nhân viên mới thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi thêm nhân viên: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateEmployee()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("sp_UpdateNhanVienWithPhongBanChucVu", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@MaNV", currentMaNV);
                        cmd.Parameters.AddWithValue("@HoTen", txtHoTen.Text.Trim());
                        cmd.Parameters.AddWithValue("@NgaySinh", dtpNgaySinh.Value);
                        cmd.Parameters.AddWithValue("@GioiTinh", cmbGioiTinh.SelectedItem?.ToString() ?? "");
                        cmd.Parameters.AddWithValue("@DienThoai", txtDienThoai.Text.Trim());
                        cmd.Parameters.AddWithValue("@Email", txtEmail.Text.Trim());
                        cmd.Parameters.AddWithValue("@DiaChi", txtDiaChi.Text.Trim());
                        cmd.Parameters.AddWithValue("@NgayVaoLam", dtpNgayVaoLam.Value);
                        cmd.Parameters.AddWithValue("@LuongCoBan", numLuongCoBan.Value);
                        cmd.Parameters.AddWithValue("@MaPhongBan", cmbPhongBanForm.SelectedValue == DBNull.Value ? (object)DBNull.Value : cmbPhongBanForm.SelectedValue);
                        cmd.Parameters.AddWithValue("@MaChucVu", cmbChucVu.SelectedValue == DBNull.Value ? (object)DBNull.Value : cmbChucVu.SelectedValue);

                        cmd.ExecuteNonQuery();
                    }
                }
                LoadData();
                pnlRight.Visible = false;
                ClearForm();
                MessageBox.Show("Cập nhật thông tin nhân viên thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi cập nhật nhân viên: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvNhanVien_SelectionChanged(object sender, EventArgs e)
        {
            // Có thể thêm logic xử lý khi chọn row
        }

        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            FilterData();
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
                    using (SqlCommand cmd = new SqlCommand("SELECT * FROM dbo.NhanVien WHERE MaNV = @MaNV", conn))
                    {
                        cmd.Parameters.AddWithValue("@MaNV", maNV);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                txtHoTen.Text = reader["HoTen"].ToString();
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
