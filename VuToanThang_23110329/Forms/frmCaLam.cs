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
        private string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        private bool isEditing = false;
        private int currentMaCa = 0;

        public frmCaLam()
        {
            InitializeComponent();
            SetupDataGridView();
            SetButtonStates(false);
        }

        private void frmCaLam_Load(object sender, EventArgs e)
        {
            LoadData();
            ClearForm();
            chkKichHoat.Checked = true;
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
                DefaultCellStyle = new DataGridViewCellStyle() { Format = "HH:mm" }
            });

            dgvCaLam.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "GioKetThuc",
                HeaderText = "Giờ Kết Thúc",
                DataPropertyName = "GioKetThuc",
                Width = 120,
                ReadOnly = true,
                DefaultCellStyle = new DataGridViewCellStyle() { Format = "HH:mm" }
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
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi xóa ca làm: {ex.Message}", "Lỗi", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ValidateInput()
        {
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
            
            // Enable/Disable form controls
            txtTenCa.Enabled = editing;
            dtpGioBatDau.Enabled = editing;
            dtpGioKetThuc.Enabled = editing;
            txtHeSoCa.Enabled = editing;
            txtMoTa.Enabled = editing;
            chkKichHoat.Enabled = editing;
            
            // Enable/Disable buttons
            btnThem.Enabled = !editing;
            btnSua.Enabled = !editing && dgvCaLam.CurrentRow != null;
            btnXoa.Enabled = !editing && dgvCaLam.CurrentRow != null;
            btnLuu.Enabled = editing;
            btnHuy.Enabled = editing;
            btnLamMoi.Enabled = !editing;
            
            dgvCaLam.Enabled = !editing;
            txtTimKiem.Enabled = !editing;
        }

        private void SetButtonStates(bool hasSelection)
        {
            if (!isEditing)
            {
                btnSua.Enabled = hasSelection;
                btnXoa.Enabled = hasSelection;
            }
        }
    }
}