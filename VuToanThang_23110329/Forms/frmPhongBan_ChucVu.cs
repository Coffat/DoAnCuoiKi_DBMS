using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using Guna.UI2.WinForms;

namespace VuToanThang_23110329.Forms
{
    public partial class frmPhongBan_ChucVu : Form
    {
        private string connectionString;
        private DataTable dtPhongBan;
        private DataTable dtChucVu;
        private bool editingPhongBan = false;
        private bool editingChucVu = false;

        public frmPhongBan_ChucVu()
        {
            InitializeComponent();
            InitializeConnectionString();
        }

        private void frmPhongBan_ChucVu_Load(object sender, EventArgs e)
        {
            SetupDataGridViews();
            LoadData();
            ClearForm(); // Initialize button states
            
            // Đảm bảo các nút hiển thị và ở vị trí đúng
            btnLuu.BringToFront();
            btnHuy.BringToFront();
            btnLuu.Visible = true;
            btnHuy.Visible = true;
        }

        private void btnThemPhongBan_Click(object sender, EventArgs e)
        {
            // Disconnect từ DataGridView selection
            dgvPhongBan.ClearSelection();
            dgvPhongBan.CurrentCell = null;
            
            // Clear databinding
            txtMaPhongBan.DataBindings.Clear();
            txtTenPhongBan.DataBindings.Clear();
            txtMoTaPhongBan.DataBindings.Clear();
            
            // Clear và disable txtMaPhongBan (vì là IDENTITY)
            txtMaPhongBan.Text = "";
            txtMaPhongBan.Enabled = false; // Không cho nhập mã khi thêm mới
            txtMaPhongBan.BackColor = System.Drawing.SystemColors.Control; // Màu xám
            
            // Clear các field khác
            txtTenPhongBan.Text = "";
            txtMoTaPhongBan.Text = "";
            
            editingPhongBan = true;
            
            // Enable/disable buttons for editing mode
            btnLuu.Enabled = true;
            btnLuu.Visible = true;
            btnHuy.Enabled = true;
            btnHuy.Visible = true;
            btnThemPhongBan.Enabled = false;
            btnSuaPhongBan.Enabled = false;
            btnXoaPhongBan.Enabled = false;
            
            // Debug message removed
            
            // Focus on first input
            txtTenPhongBan.Focus();
        }

        private void btnSuaPhongBan_Click(object sender, EventArgs e)
        {
            if (dgvPhongBan.CurrentRow == null)
            {
                MessageBox.Show("Chọn một phòng ban để sửa.");
                return;
            }
            editingPhongBan = true;
            
            // Enable txtMaPhongBan khi sửa (chỉ để hiển thị, không cho edit)
            txtMaPhongBan.Enabled = true;
            txtMaPhongBan.ReadOnly = true; // Chỉ đọc, không cho sửa mã
            txtMaPhongBan.BackColor = System.Drawing.SystemColors.Control; // Màu xám để báo hiệu không edit được
            
            DataRowView row = dgvPhongBan.CurrentRow.DataBoundItem as DataRowView;
            if (row != null)
            {
                txtMaPhongBan.Text = row["MaPhongBan"].ToString();
                txtTenPhongBan.Text = row["TenPhongBan"].ToString();
                txtMoTaPhongBan.Text = row["MoTa"].ToString();
            }
            
            // Enable/disable buttons for editing mode
            btnLuu.Enabled = true;
            btnHuy.Enabled = true;
            btnThemPhongBan.Enabled = false;
            btnSuaPhongBan.Enabled = false;
            btnXoaPhongBan.Enabled = false;
            
            // Focus on first input
            txtTenPhongBan.Focus();
        }

        private void btnXoaPhongBan_Click(object sender, EventArgs e)
        {
            if (dgvPhongBan.CurrentRow == null)
            {
                MessageBox.Show("Chọn một phòng ban để xóa.");
                return;
            }
            int ma = Convert.ToInt32(dgvPhongBan.CurrentRow.Cells["MaPhongBan"].Value);
            if (MessageBox.Show("Xóa phòng ban này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;
            
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("dbo.sp_PhongBan_Delete", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@MaPhongBan", ma);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Xóa phòng ban thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnThemChucVu_Click(object sender, EventArgs e)
        {
            // Disconnect từ DataGridView selection
            dgvChucVu.ClearSelection();
            dgvChucVu.CurrentCell = null;
            
            // Clear databinding
            txtMaChucVu.DataBindings.Clear();
            txtTenChucVu.DataBindings.Clear();
            txtMoTaChucVu.DataBindings.Clear();
            
            // Clear và disable txtMaChucVu (vì là IDENTITY)
            txtMaChucVu.Text = "";
            txtMaChucVu.Enabled = false; // Không cho nhập mã khi thêm mới
            txtMaChucVu.BackColor = System.Drawing.SystemColors.Control; // Màu xám
            
            // Clear các field khác
            txtTenChucVu.Text = "";
            txtMoTaChucVu.Text = "";
            
            editingChucVu = true;
            
            // Enable/disable buttons for editing mode
            btnLuu.Enabled = true;
            btnHuy.Enabled = true;
            btnThemChucVu.Enabled = false;
            btnSuaChucVu.Enabled = false;
            btnXoaChucVu.Enabled = false;
            
            // Focus on first input
            txtTenChucVu.Focus();
        }

        private void btnSuaChucVu_Click(object sender, EventArgs e)
        {
            if (dgvChucVu.CurrentRow == null)
            {
                MessageBox.Show("Chọn một chức vụ để sửa.");
                return;
            }
            editingChucVu = true;
            
            // Enable txtMaChucVu khi sửa (chỉ để hiển thị, không cho edit)
            txtMaChucVu.Enabled = true;
            txtMaChucVu.ReadOnly = true; // Chỉ đọc, không cho sửa mã
            txtMaChucVu.BackColor = System.Drawing.SystemColors.Control; // Màu xám để báo hiệu không edit được
            
            DataRowView row = dgvChucVu.CurrentRow.DataBoundItem as DataRowView;
            if (row != null)
            {
                txtMaChucVu.Text = row["MaChucVu"].ToString();
                txtTenChucVu.Text = row["TenChucVu"].ToString();
                txtMoTaChucVu.Text = row["MoTa"].ToString();
            }
            
            // Enable/disable buttons for editing mode
            btnLuu.Enabled = true;
            btnHuy.Enabled = true;
            btnThemChucVu.Enabled = false;
            btnSuaChucVu.Enabled = false;
            btnXoaChucVu.Enabled = false;
            
            // Focus on first input
            txtTenChucVu.Focus();
        }

        private void btnXoaChucVu_Click(object sender, EventArgs e)
        {
            if (dgvChucVu.CurrentRow == null)
            {
                MessageBox.Show("Chọn một chức vụ để xóa.");
                return;
            }
            int ma = Convert.ToInt32(dgvChucVu.CurrentRow.Cells["MaChucVu"].Value);
            if (MessageBox.Show("Xóa chức vụ này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;
            
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("dbo.sp_ChucVu_Delete", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@MaChucVu", ma);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Xóa chức vụ thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            try
            {
                if (editingPhongBan)
                {
                    if (string.IsNullOrWhiteSpace(txtTenPhongBan.Text)) { MessageBox.Show("Nhập tên phòng ban."); return; }
                    
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        conn.Open();
                        // Kiểm tra Insert hay Update
                        bool isInsert = string.IsNullOrWhiteSpace(txtMaPhongBan.Text);
                        
                        if (isInsert)
                        {
                            // Insert - Dùng stored procedure với debug chi tiết
                            try
                            {
                                using (SqlCommand cmd = new SqlCommand("dbo.sp_PhongBan_Insert", conn))
                                {
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.Parameters.AddWithValue("@TenPhongBan", txtTenPhongBan.Text.Trim());
                                    cmd.Parameters.AddWithValue("@MoTa", string.IsNullOrWhiteSpace(txtMoTaPhongBan.Text) ? (object)DBNull.Value : txtMoTaPhongBan.Text.Trim());
                                    cmd.Parameters.AddWithValue("@KichHoat", true);
                                    
                                    // Thêm tham số OUTPUT
                                    var outputParam = new SqlParameter("@MaPhongBan_OUT", SqlDbType.Int)
                                    {
                                        Direction = ParameterDirection.Output
                                    };
                                    cmd.Parameters.Add(outputParam);
                                    
                                    cmd.ExecuteNonQuery();
                                    
                                    int newId = (int)outputParam.Value;
                                    MessageBox.Show($"Thêm phòng ban thành công! Mã: {newId}", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                            }
                            catch (SqlException sqlEx)
                            {
                                string detailError = $"SQL Error: {sqlEx.Message}\nError Number: {sqlEx.Number}\nSeverity: {sqlEx.Class}\nState: {sqlEx.State}";
                                if (sqlEx.Number == 2)
                                {
                                    detailError += "\n\nStored procedure 'sp_PhongBan_Insert' không tồn tại. Hãy chạy file '03_StoredProcedures.sql'";
                                }
                                MessageBox.Show(detailError, "Lỗi SQL Chi Tiết", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                throw; // Re-throw để outer catch xử lý
                            }
                        }
                        else
                        {
                            // Update
                            using (SqlCommand cmd = new SqlCommand("dbo.sp_PhongBan_Update", conn))
                            {
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.Parameters.AddWithValue("@MaPhongBan", Convert.ToInt32(txtMaPhongBan.Text));
                                cmd.Parameters.AddWithValue("@TenPhongBan", txtTenPhongBan.Text.Trim());
                                cmd.Parameters.AddWithValue("@MoTa", string.IsNullOrWhiteSpace(txtMoTaPhongBan.Text) ? (object)DBNull.Value : txtMoTaPhongBan.Text.Trim());
                                cmd.ExecuteNonQuery();
                                MessageBox.Show("Cập nhật phòng ban thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                    }
                    editingPhongBan = false;
                    LoadData();
                    ClearForm();
                    return;
                }

                if (editingChucVu)
                {
                    if (string.IsNullOrWhiteSpace(txtTenChucVu.Text)) { MessageBox.Show("Nhập tên chức vụ."); return; }
                    
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        conn.Open();
                        // Kiểm tra Insert hay Update
                        bool isInsert = string.IsNullOrWhiteSpace(txtMaChucVu.Text);
                        
                        if (isInsert)
                        {
                            // Insert - Dùng stored procedure
                            using (SqlCommand cmd = new SqlCommand("dbo.sp_ChucVu_Insert", conn))
                            {
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.Parameters.AddWithValue("@TenChucVu", txtTenChucVu.Text.Trim());
                                cmd.Parameters.AddWithValue("@MoTa", string.IsNullOrWhiteSpace(txtMoTaChucVu.Text) ? (object)DBNull.Value : txtMoTaChucVu.Text.Trim());
                                cmd.Parameters.AddWithValue("@KichHoat", true);
                                
                                // Output parameter để nhận MaChucVu mới
                                SqlParameter outputParam = new SqlParameter("@MaChucVu_OUT", SqlDbType.Int);
                                outputParam.Direction = ParameterDirection.Output;
                                cmd.Parameters.Add(outputParam);
                                
                                cmd.ExecuteNonQuery();
                                int newId = Convert.ToInt32(outputParam.Value);
                                MessageBox.Show($"Thêm chức vụ thành công! Mã: {newId}", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                        else
                        {
                            // Update
                            using (SqlCommand cmd = new SqlCommand("dbo.sp_ChucVu_Update", conn))
                            {
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.Parameters.AddWithValue("@MaChucVu", Convert.ToInt32(txtMaChucVu.Text));
                                cmd.Parameters.AddWithValue("@TenChucVu", txtTenChucVu.Text.Trim());
                                cmd.Parameters.AddWithValue("@MoTa", string.IsNullOrWhiteSpace(txtMoTaChucVu.Text) ? (object)DBNull.Value : txtMoTaChucVu.Text.Trim());
                                cmd.ExecuteNonQuery();
                                MessageBox.Show("Cập nhật chức vụ thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                    }
                    editingChucVu = false;
                    LoadData();
                    ClearForm();
                }
            }
            catch (Exception ex)
            {
                string errorMsg = $"Lỗi khi lưu dữ liệu: {ex.Message}";
                if (ex.Message.Contains("Could not find stored procedure"))
                {
                    errorMsg += "\n\nVui lòng chạy file '03_StoredProcedures.sql' để tạo các stored procedures cần thiết.";
                }
                MessageBox.Show(errorMsg, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            editingPhongBan = false;
            editingChucVu = false;
            ClearForm();
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            // TODO: Search logic here
        }

        private void tabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            // TODO: Tab changed logic here
        }

        private void LoadData()
        {
            if (string.IsNullOrEmpty(connectionString)) return;
            
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    
                    // Load PhongBan using stored procedure
                    using (SqlCommand cmd = new SqlCommand("dbo.sp_PhongBan_GetAll", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            dtPhongBan = new DataTable();
                            da.Fill(dtPhongBan);
                            dgvPhongBan.DataSource = dtPhongBan;
                        }
                    }
                    
                    // Load ChucVu using stored procedure
                    using (SqlCommand cmd = new SqlCommand("dbo.sp_ChucVu_GetAll", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            dtChucVu = new DataTable();
                            da.Fill(dtChucVu);
                            dgvChucVu.DataSource = dtChucVu;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải dữ liệu: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ClearForm()
        {
            txtMaPhongBan.Text = "";
            txtTenPhongBan.Text = "";
            txtMoTaPhongBan.Text = "";
            txtMaChucVu.Text = "";
            txtTenChucVu.Text = "";
            txtMoTaChucVu.Text = "";
            
            // Reset txtMaPhongBan về trạng thái bình thường
            txtMaPhongBan.Enabled = true;
            txtMaPhongBan.ReadOnly = false;
            txtMaPhongBan.BackColor = System.Drawing.SystemColors.Window;
            
            // Reset txtMaChucVu về trạng thái bình thường
            txtMaChucVu.Enabled = true;
            txtMaChucVu.ReadOnly = false;
            txtMaChucVu.BackColor = System.Drawing.SystemColors.Window;
            
            // Reset button states and visibility
            btnLuu.Enabled = false;
            btnLuu.Visible = true;  // Đảm bảo nút hiển thị
            btnHuy.Enabled = false;
            btnHuy.Visible = true;  // Đảm bảo nút hiển thị
            btnThemPhongBan.Enabled = true;
            btnSuaPhongBan.Enabled = true;
            btnXoaPhongBan.Enabled = true;
            btnThemChucVu.Enabled = true;
            btnSuaChucVu.Enabled = true;
            btnXoaChucVu.Enabled = true;
        }

        private void SetupDataGridViews()
        {
            // Phòng ban
            dgvPhongBan.AutoGenerateColumns = false;
            dgvPhongBan.Columns.Clear();
            dgvPhongBan.Columns.Add(new DataGridViewTextBoxColumn { Name = "MaPhongBan", HeaderText = "Mã", DataPropertyName = "MaPhongBan", Width = 80 });
            dgvPhongBan.Columns.Add(new DataGridViewTextBoxColumn { Name = "TenPhongBan", HeaderText = "Tên phòng ban", DataPropertyName = "TenPhongBan", Width = 250 });
            dgvPhongBan.Columns.Add(new DataGridViewTextBoxColumn { Name = "MoTa", HeaderText = "Mô tả", DataPropertyName = "MoTa", Width = 400 });
            dgvPhongBan.Columns.Add(new DataGridViewCheckBoxColumn { Name = "KichHoat", HeaderText = "Kích hoạt", DataPropertyName = "KichHoat", Width = 90 });

            // Chức vụ
            dgvChucVu.AutoGenerateColumns = false;
            dgvChucVu.Columns.Clear();
            dgvChucVu.Columns.Add(new DataGridViewTextBoxColumn { Name = "MaChucVu", HeaderText = "Mã", DataPropertyName = "MaChucVu", Width = 80 });
            dgvChucVu.Columns.Add(new DataGridViewTextBoxColumn { Name = "TenChucVu", HeaderText = "Tên chức vụ", DataPropertyName = "TenChucVu", Width = 250 });
            dgvChucVu.Columns.Add(new DataGridViewTextBoxColumn { Name = "MoTa", HeaderText = "Mô tả", DataPropertyName = "MoTa", Width = 400 });
            dgvChucVu.Columns.Add(new DataGridViewCheckBoxColumn { Name = "KichHoat", HeaderText = "Kích hoạt", DataPropertyName = "KichHoat", Width = 90 });
        }

        private void InitializeConnectionString()
        {
            try
            {
                var cs = System.Configuration.ConfigurationManager.ConnectionStrings["HrDb"];
                connectionString = cs?.ConnectionString ?? "";
            }
            catch
            {
                connectionString = "";
            }
        }
    }
}
