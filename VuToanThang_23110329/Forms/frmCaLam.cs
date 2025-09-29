using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using Guna.UI2.WinForms;

namespace VuToanThang_23110329.Forms
{
    public partial class frmCaLam : Form
    {
        private string connectionString;
        private bool isEditing = false;
        private int currentMaCa = 0;
        private string userRole = "";


        public frmCaLam()
        {
            InitializeComponent();
            InitializeConnectionString();
            SetupDataGridView();
            SetButtonStates(false);
        }

        public frmCaLam(string role)
        {
            InitializeComponent();
            InitializeConnectionString();
            userRole = role;
            SetupDataGridView();
            SetButtonStates(false);
            SetupRolePermissions();
            LoadData();
            ClearForm();
            chkKichHoat.Checked = true;
        }

        private void InitializeConnectionString()
        {
            var cs = System.Configuration.ConfigurationManager.ConnectionStrings["HrDb"];
            if (cs == null)
            {
                MessageBox.Show("Không tìm thấy chuỗi kết nối 'HrDb' trong App.config.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                connectionString = string.Empty;
            }
            else
            {
                connectionString = cs.ConnectionString;
            }
        }

        private void frmCaLam_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void SetupDataGridView()
        {
            dgvCaLam.AutoGenerateColumns = false;
            dgvCaLam.Columns.Clear();

            // Cấu hình các cột
            dgvCaLam.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "MaCa",
                HeaderText = "Mã Ca",
                DataPropertyName = "MaCa",
                Width = 80,
                ReadOnly = true
            });

            dgvCaLam.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "TenCa",
                HeaderText = "Tên Ca",
                DataPropertyName = "TenCa",
                Width = 150,
                ReadOnly = true
            });

            dgvCaLam.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "GioBatDau",
                HeaderText = "Giờ Bắt Đầu",
                DataPropertyName = "GioBatDau",
                Width = 120,
                ReadOnly = true,
                DefaultCellStyle = new DataGridViewCellStyle() { Format = "hh\\:mm" }
            });

            dgvCaLam.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "GioKetThuc",
                HeaderText = "Giờ Kết Thúc",
                DataPropertyName = "GioKetThuc",
                Width = 120,
                ReadOnly = true,
                DefaultCellStyle = new DataGridViewCellStyle() { Format = "hh\\:mm" }
            });

            dgvCaLam.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "HeSoCa",
                HeaderText = "Hệ Số Ca",
                DataPropertyName = "HeSoCa",
                Width = 100,
                ReadOnly = true,
                DefaultCellStyle = new DataGridViewCellStyle() { Format = "F1" }
            });

            dgvCaLam.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "MoTa",
                HeaderText = "Mô Tả",
                DataPropertyName = "MoTa",
                Width = 200,
                ReadOnly = true
            });

            dgvCaLam.Columns.Add(new DataGridViewCheckBoxColumn()
            {
                Name = "KichHoat",
                HeaderText = "Kích Hoạt",
                DataPropertyName = "KichHoat",
                Width = 100,
                ReadOnly = true
            });

            // Cấu hình giao diện DataGridView
            dgvCaLam.EnableHeadersVisualStyles = false;
            dgvCaLam.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(94, 148, 255);
            dgvCaLam.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvCaLam.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            dgvCaLam.ColumnHeadersHeight = 40;
            dgvCaLam.RowTemplate.Height = 35;
            dgvCaLam.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 249, 250);
            dgvCaLam.DefaultCellStyle.SelectionBackColor = Color.FromArgb(94, 148, 255);
            dgvCaLam.DefaultCellStyle.SelectionForeColor = Color.White;
        }

        private void LoadData()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_CaLam_GetAll", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        
                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        
                        dgvCaLam.DataSource = dt;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải dữ liệu: {ex.Message}", "Lỗi", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvCaLam_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvCaLam.CurrentRow != null && !isEditing)
            {
                LoadShiftToForm();
                SetButtonStates(true);
            }
        }

        private void dgvCaLam_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            // Xử lý lỗi định dạng dữ liệu trong DataGridView
            e.ThrowException = false;
            
            // Log lỗi (có thể thêm vào log file)
            Console.WriteLine($"DataGridView Error: {e.Exception.Message}");
            
            // Hiển thị giá trị mặc định cho cell bị lỗi
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                dgvCaLam.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = "N/A";
            }
        }

        private void LoadShiftToForm()
        {
            if (dgvCaLam.CurrentRow != null)
            {
                DataRowView row = (DataRowView)dgvCaLam.CurrentRow.DataBoundItem;
                
                currentMaCa = Convert.ToInt32(row["MaCa"]);
                txtMaCa.Text = row["MaCa"].ToString();
                txtTenCa.Text = row["TenCa"].ToString();
                
                if (row["GioBatDau"] != DBNull.Value)
                {
                    dtpGioBatDau.Value = DateTime.Today.Add((TimeSpan)row["GioBatDau"]);
                }
                
                if (row["GioKetThuc"] != DBNull.Value)
                {
                    dtpGioKetThuc.Value = DateTime.Today.Add((TimeSpan)row["GioKetThuc"]);
                }
                
                txtHeSoCa.Text = row["HeSoCa"].ToString();
                txtMoTa.Text = row["MoTa"].ToString();
                chkKichHoat.Checked = Convert.ToBoolean(row["KichHoat"]);
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            ClearForm();
            SetEditMode(true);
            txtTenCa.Focus();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (dgvCaLam.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn ca làm cần sửa!", "Thông báo", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            
            SetEditMode(true);
            txtTenCa.Focus();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dgvCaLam.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn ca làm cần xóa!", "Thông báo", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("Bạn có chắc chắn muốn xóa ca làm này?", "Xác nhận", 
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                DeleteShift();
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (ValidateInput())
            {
                if (currentMaCa == 0)
                {
                    InsertShift();
                }
                else
                {
                    UpdateShift();
                }
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            SetEditMode(false);
            LoadShiftToForm();
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            LoadData();
            ClearForm();
            SetEditMode(false);
        }

        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            SearchShifts();
        }

        private void SearchShifts()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_CaLam_GetAll", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        
                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        
                        // Lọc dữ liệu theo từ khóa tìm kiếm
                        if (!string.IsNullOrEmpty(txtTimKiem.Text))
                        {
                            DataView dv = dt.DefaultView;
                            dv.RowFilter = $"TenCa LIKE '%{txtTimKiem.Text}%' OR MoTa LIKE '%{txtTimKiem.Text}%'";
                            dgvCaLam.DataSource = dv;
                        }
                        else
                        {
                            dgvCaLam.DataSource = dt;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tìm kiếm: {ex.Message}", "Lỗi", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void InsertShift()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_CaLam_Insert", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        
                        // Thêm mã ca nếu có
                        if (!string.IsNullOrEmpty(txtMaCa.Text.Trim()))
                        {
                            cmd.Parameters.AddWithValue("@MaCa", int.Parse(txtMaCa.Text.Trim()));
                        }
                        
                        cmd.Parameters.AddWithValue("@TenCa", txtTenCa.Text.Trim());
                        cmd.Parameters.AddWithValue("@GioBatDau", dtpGioBatDau.Value.TimeOfDay);
                        cmd.Parameters.AddWithValue("@GioKetThuc", dtpGioKetThuc.Value.TimeOfDay);
                        cmd.Parameters.AddWithValue("@HeSoCa", decimal.Parse(txtHeSoCa.Text));
                        cmd.Parameters.AddWithValue("@MoTa", txtMoTa.Text.Trim());
                        cmd.Parameters.AddWithValue("@KichHoat", chkKichHoat.Checked);
                        
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        
                        MessageBox.Show("Thêm ca làm thành công!", "Thông báo", 
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        
                        LoadData();
                        SetEditMode(false);
                        ClearForm();
                    }
                }
            }
            catch (SqlException ex) when (ex.Number == 229) // Permission denied
            {
                MessageBox.Show("Bạn không có quyền thêm ca làm. Chỉ HR mới có quyền này.", "Không có quyền", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi thêm ca làm: {ex.Message}", "Lỗi", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateShift()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_CaLam_Update", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        
                        cmd.Parameters.AddWithValue("@MaCa", currentMaCa);
                        cmd.Parameters.AddWithValue("@TenCa", txtTenCa.Text.Trim());
                        cmd.Parameters.AddWithValue("@GioBatDau", dtpGioBatDau.Value.TimeOfDay);
                        cmd.Parameters.AddWithValue("@GioKetThuc", dtpGioKetThuc.Value.TimeOfDay);
                        cmd.Parameters.AddWithValue("@HeSoCa", decimal.Parse(txtHeSoCa.Text));
                        cmd.Parameters.AddWithValue("@MoTa", txtMoTa.Text.Trim());
                        cmd.Parameters.AddWithValue("@KichHoat", chkKichHoat.Checked);
                        
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        
                        MessageBox.Show("Cập nhật ca làm thành công!", "Thông báo", 
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        
                        LoadData();
                        SetEditMode(false);
                    }
                }
            }
            catch (SqlException ex) when (ex.Number == 229) // Permission denied
            {
                MessageBox.Show("Bạn không có quyền cập nhật ca làm. Chỉ HR mới có quyền này.", "Không có quyền", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi cập nhật ca làm: {ex.Message}", "Lỗi", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DeleteShift()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_CaLam_Delete", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@MaCa", currentMaCa);
                        
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        
                        MessageBox.Show("Xóa ca làm thành công!", "Thông báo", 
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        
                        LoadData();
                        ClearForm();
                        SetEditMode(false);
                    }
                }
            }
            catch (SqlException ex) when (ex.Number == 229) // Permission denied
            {
                MessageBox.Show("Bạn không có quyền xóa ca làm. Chỉ HR mới có quyền này.", "Không có quyền", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi xóa ca làm: {ex.Message}", "Lỗi", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ValidateInput()
        {
            // Kiểm tra mã ca (chỉ khi thêm mới)
            if (currentMaCa == 0 && !string.IsNullOrWhiteSpace(txtMaCa.Text))
            {
                if (!int.TryParse(txtMaCa.Text.Trim(), out int maCa) || maCa <= 0)
                {
                    MessageBox.Show("Mã ca phải là số nguyên dương!", "Thông báo", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtMaCa.Focus();
                    return false;
                }
            }
            
            // Kiểm tra tên ca
            if (string.IsNullOrWhiteSpace(txtTenCa.Text))
            {
                MessageBox.Show("Vui lòng nhập tên ca!", "Thông báo", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenCa.Focus();
                return false;
            }

            // Kiểm tra hệ số ca
            if (!decimal.TryParse(txtHeSoCa.Text, out decimal heSo) || heSo <= 0)
            {
                MessageBox.Show("Hệ số ca phải là số dương!", "Thông báo", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtHeSoCa.Focus();
                return false;
            }

            // Kiểm tra thời gian
            if (dtpGioBatDau.Value.TimeOfDay >= dtpGioKetThuc.Value.TimeOfDay)
            {
                MessageBox.Show("Giờ kết thúc phải sau giờ bắt đầu!", "Thông báo", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dtpGioKetThuc.Focus();
                return false;
            }

            return true;
        }

        private void ClearForm()
        {
            currentMaCa = 0;
            txtMaCa.Text = "";
            txtTenCa.Text = "";
            dtpGioBatDau.Value = DateTime.Today.AddHours(8);
            dtpGioKetThuc.Value = DateTime.Today.AddHours(17);
            txtHeSoCa.Text = "1.0";
            txtMoTa.Text = "";
            chkKichHoat.Checked = true;
        }

        private void SetEditMode(bool editing)
        {
            isEditing = editing;
            
            // Chỉ cho phép edit nếu là HR
            if (userRole != "HR")
            {
                editing = false;
            }
            
            // Enable/Disable form controls
            txtMaCa.Enabled = editing;
            txtTenCa.Enabled = editing;
            dtpGioBatDau.Enabled = editing;
            dtpGioKetThuc.Enabled = editing;
            txtHeSoCa.Enabled = editing;
            txtMoTa.Enabled = editing;
            chkKichHoat.Enabled = editing;
            
            // Quản lý ReadOnly cho txtMaCa
            if (editing)
            {
                // Khi thêm mới: Cho phép nhập mã ca
                if (currentMaCa == 0)
                {
                    txtMaCa.ReadOnly = false;
                }
                else
                {
                    // Khi sửa: Không cho phép sửa mã ca (khóa chính)
                    txtMaCa.ReadOnly = true;
                }
            }
            
            // Enable/Disable buttons theo role
            if (userRole == "HR")
            {
                btnThem.Enabled = !editing;
                btnSua.Enabled = !editing && dgvCaLam.CurrentRow != null;
                btnXoa.Enabled = !editing && dgvCaLam.CurrentRow != null;
                btnLuu.Enabled = editing;
                btnHuy.Enabled = editing;
            }
            else
            {
                // QuanLy và role khác - chỉ xem
                btnThem.Enabled = false;
                btnSua.Enabled = false;
                btnXoa.Enabled = false;
                btnLuu.Enabled = false;
                btnHuy.Enabled = false;
            }
            
            btnLamMoi.Enabled = !editing;
            dgvCaLam.Enabled = !editing;
            txtTimKiem.Enabled = !editing;
        }

        private void SetButtonStates(bool hasSelection)
        {
            if (!isEditing && userRole == "HR")
            {
                btnSua.Enabled = hasSelection;
                btnXoa.Enabled = hasSelection;
            }
            else if (userRole != "HR")
            {
                // QuanLy và role khác - chỉ xem
                btnSua.Enabled = false;
                btnXoa.Enabled = false;
            }
        }

        private void SetupRolePermissions()
        {
            // Dựa vào phân quyền SQL:
            // r_hr: CRUD đầy đủ
            // r_quanly, r_nhanvien: Chỉ xem
            
            if (userRole == "HR")
            {
                // HR có quyền CRUD đầy đủ theo SQL
                btnThem.Enabled = true;
                btnSua.Enabled = false;
                btnXoa.Enabled = false;
                btnLuu.Enabled = false;
                btnHuy.Enabled = false;
                btnLamMoi.Enabled = true;
            }
            else
            {
                // QuanLy và NhanVien chỉ có quyền xem theo SQL
                btnThem.Enabled = false;
                btnSua.Enabled = false;
                btnXoa.Enabled = false;
                btnLuu.Enabled = false;
                btnHuy.Enabled = false;
                btnLamMoi.Enabled = true;
                
                // Disable tất cả input controls
                txtMaCa.Enabled = false;
                txtTenCa.Enabled = false;
                dtpGioBatDau.Enabled = false;
                dtpGioKetThuc.Enabled = false;
                txtHeSoCa.Enabled = false;
                txtMoTa.Enabled = false;
                chkKichHoat.Enabled = false;
            }
        }
    }
}