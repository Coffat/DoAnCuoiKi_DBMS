using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using Guna.UI2.WinForms;

namespace VuToanThang_23110329.Forms
{
    public partial class frmChamCong : Form
    {
        private string connectionString;
        private int currentUserId;
        private string currentUserRole;
        
        public frmChamCong()
        {
            InitializeComponent();
            InitializeConnectionString();
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
            // Get current user info from session
            currentUserId = UserSession.MaNV > 0 ? UserSession.MaNV : 1;
            currentUserRole = UserSession.VaiTro ?? "NhanVien";
        }

        private void frmChamCong_Load(object sender, EventArgs e)
        {
            SetupComboBoxes();
            LoadCurrentStatus();
            ConfigureRoleCapabilities();
        }

        private void SetupComboBoxes()
        {
            // Setup month combobox
            cmbThang.Items.Clear();
            cmbThangKhoa.Items.Clear();
            for (int i = 1; i <= 12; i++)
            {
                string month = i.ToString("00");
                cmbThang.Items.Add(month);
                cmbThangKhoa.Items.Add(month);
            }
            cmbThang.SelectedIndex = DateTime.Now.Month - 1;
            cmbThangKhoa.SelectedIndex = DateTime.Now.Month - 1;

            // Setup year combobox
            cmbNam.Items.Clear();
            cmbNamKhoa.Items.Clear();
            int currentYear = DateTime.Now.Year;
            for (int i = currentYear - 2; i <= currentYear + 1; i++)
            {
                cmbNam.Items.Add(i.ToString());
                cmbNamKhoa.Items.Add(i.ToString());
            }
            cmbNam.SelectedItem = currentYear.ToString();
            cmbNamKhoa.SelectedItem = currentYear.ToString();
        }

        private void ConfigureRoleCapabilities()
        {
            // Only HR and QuanLy can lock attendance periods
            bool canLockAttendance = currentUserRole == "HR" || currentUserRole == "QuanLy";
            tabKhoaCong.Enabled = canLockAttendance;
            
            if (!canLockAttendance)
            {
                tabControl.TabPages.Remove(tabKhoaCong);
            }
        }

        private void LoadCurrentStatus()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    DateTime today = DateTime.Now.Date;
                    
                    // Check if there's a schedule for today using view
                    string scheduleQuery = @"
                        SELECT TenCa, GioBatDau, GioKetThuc, TrangThaiLich
                        FROM dbo.vw_Lich_ChamCong_Ngay
                        WHERE MaNV = @MaNV AND NgayLam = @NgayLam";
                    
                    using (SqlCommand cmd = new SqlCommand(scheduleQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaNV", currentUserId);
                        cmd.Parameters.AddWithValue("@NgayLam", today);
                        
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                string tenCa = reader["TenCa"].ToString();
                                TimeSpan gioBatDau = (TimeSpan)reader["GioBatDau"];
                                TimeSpan gioKetThuc = (TimeSpan)reader["GioKetThuc"];
                                
                                lblThongTinCa.Text = $"Ca: {tenCa} ({gioBatDau:hh\\:mm} - {gioKetThuc:hh\\:mm})";
                            }
                            else
                            {
                                lblThongTinCa.Text = "Không có ca làm việc hôm nay";
                                btnCheckIn.Enabled = false;
                                btnCheckOut.Enabled = false;
                                return;
                            }
                        }
                    }
                    
                    // Check current attendance status
                    string attendanceQuery = @"
                        SELECT GioVao, GioRa, Khoa
                        FROM dbo.ChamCong
                        WHERE MaNV = @MaNV AND NgayLam = @NgayLam";
                    
                    using (SqlCommand cmd = new SqlCommand(attendanceQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaNV", currentUserId);
                        cmd.Parameters.AddWithValue("@NgayLam", today);
                        
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                bool isLocked = Convert.ToBoolean(reader["Khoa"]);
                                if (isLocked)
                                {
                                    lblTrangThai.Text = "Công đã bị khóa";
                                    lblTrangThai.ForeColor = Color.Red;
                                    btnCheckIn.Enabled = false;
                                    btnCheckOut.Enabled = false;
                                }
                                else
                                {
                                    DateTime? gioVao = reader["GioVao"] as DateTime?;
                                    DateTime? gioRa = reader["GioRa"] as DateTime?;
                                    
                                    if (gioVao.HasValue && gioRa.HasValue)
                                    {
                                        lblTrangThai.Text = "Đã hoàn thành chấm công";
                                        lblTrangThai.ForeColor = Color.Green;
                                        lblCheckInTime.Text = $"Giờ vào: {gioVao.Value:HH:mm}";
                                        lblCheckOutTime.Text = $"Giờ ra: {gioRa.Value:HH:mm}";
                                        btnCheckIn.Enabled = false;
                                        btnCheckOut.Enabled = false;
                                    }
                                    else if (gioVao.HasValue)
                                    {
                                        lblTrangThai.Text = "Đã check in - Chờ check out";
                                        lblTrangThai.ForeColor = Color.Orange;
                                        lblCheckInTime.Text = $"Giờ vào: {gioVao.Value:HH:mm}";
                                        lblCheckOutTime.Text = "Giờ ra: Chưa có";
                                        btnCheckIn.Enabled = false;
                                        btnCheckOut.Enabled = true;
                                    }
                                    else
                                    {
                                        lblTrangThai.Text = "Chưa check in";
                                        lblTrangThai.ForeColor = Color.Blue;
                                        lblCheckInTime.Text = "Giờ vào: Chưa có";
                                        lblCheckOutTime.Text = "Giờ ra: Chưa có";
                                        btnCheckIn.Enabled = true;
                                        btnCheckOut.Enabled = false;
                                    }
                                }
                            }
                            else
                            {
                                lblTrangThai.Text = "Chưa check in";
                                lblTrangThai.ForeColor = Color.Blue;
                                lblCheckInTime.Text = "Giờ vào: Chưa có";
                                lblCheckOutTime.Text = "Giờ ra: Chưa có";
                                btnCheckIn.Enabled = true;
                                btnCheckOut.Enabled = false;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải trạng thái: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCheckIn_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    DateTime now = DateTime.Now;
                    
                    // Sử dụng stored procedure sp_CheckIn
                    using (SqlCommand cmd = new SqlCommand("dbo.sp_CheckIn", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@MaNV", currentUserId);
                        
                        cmd.ExecuteNonQuery();
                        
                        MessageBox.Show($"Check in thành công lúc {now:HH:mm}", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadCurrentStatus();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi check in: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCheckOut_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    DateTime now = DateTime.Now;
                    
                    // Sử dụng stored procedure sp_CheckOut
                    using (SqlCommand cmd = new SqlCommand("dbo.sp_CheckOut", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@MaNV", currentUserId);
                        
                        cmd.ExecuteNonQuery();
                        
                        MessageBox.Show($"Check out thành công lúc {now:HH:mm}", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadCurrentStatus();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi check out: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            LoadCurrentStatus();
        }

        private void tabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl.SelectedTab == tabLichSu)
            {
                LoadAttendanceHistory();
            }
            else if (tabControl.SelectedTab == tabKhoaCong)
            {
                LoadLockStatus();
            }
        }

        private void LoadAttendanceHistory()
        {
            if (cmbThang.SelectedItem == null || cmbNam.SelectedItem == null)
                return;
                
            try
            {
                int month = int.Parse(cmbThang.SelectedItem.ToString());
                int year = int.Parse(cmbNam.SelectedItem.ToString());
                
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = @"
                        SELECT NgayLam, GioVao, GioRa, GioCong, DiTrePhut, VeSomPhut
                        FROM dbo.ChamCong
                        WHERE MaNV = @MaNV 
                        AND YEAR(NgayLam) = @Nam 
                        AND MONTH(NgayLam) = @Thang
                        ORDER BY NgayLam DESC";
                    
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaNV", currentUserId);
                        cmd.Parameters.AddWithValue("@Nam", year);
                        cmd.Parameters.AddWithValue("@Thang", month);
                        
                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        
                        dgvLichSu.DataSource = dt;
                        
                        // Setup columns
                        if (dgvLichSu.Columns.Count > 0)
                        {
                            dgvLichSu.Columns["NgayLam"].HeaderText = "Ngày";
                            dgvLichSu.Columns["GioVao"].HeaderText = "Giờ vào";
                            dgvLichSu.Columns["GioRa"].HeaderText = "Giờ ra";
                            dgvLichSu.Columns["GioCong"].HeaderText = "Giờ công";
                            dgvLichSu.Columns["DiTrePhut"].HeaderText = "Đi trễ (phút)";
                            dgvLichSu.Columns["VeSomPhut"].HeaderText = "Về sớm (phút)";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải lịch sử: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadLockStatus()
        {
            try
            {
                int month = int.Parse(cmbThangKhoa.SelectedItem.ToString());
                int year = int.Parse(cmbNamKhoa.SelectedItem.ToString());
                
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = @"
                        SELECT COUNT(*) as SoLuong
                        FROM dbo.ChamCong
                        WHERE YEAR(NgayLam) = @Nam 
                        AND MONTH(NgayLam) = @Thang
                        AND Khoa = 1";
                    
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Nam", year);
                        cmd.Parameters.AddWithValue("@Thang", month);
                        
                        int lockedCount = Convert.ToInt32(cmd.ExecuteScalar());
                        
                        if (lockedCount > 0)
                        {
                            lblTrangThaiKhoa.Text = $"Công tháng {month}/{year} đã được khóa ({lockedCount} bản ghi)";
                            lblTrangThaiKhoa.ForeColor = Color.Red;
                            btnKhoaCong.Enabled = false;
                        }
                        else
                        {
                            lblTrangThaiKhoa.Text = $"Công tháng {month}/{year} chưa được khóa";
                            lblTrangThaiKhoa.ForeColor = Color.Blue;
                            btnKhoaCong.Enabled = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi kiểm tra trạng thái khóa: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnKhoaCong_Click(object sender, EventArgs e)
        {
            int month = int.Parse(cmbThangKhoa.SelectedItem.ToString());
            int year = int.Parse(cmbNamKhoa.SelectedItem.ToString());
            
            DialogResult result = MessageBox.Show($"Bạn có chắc chắn muốn khóa công tháng {month}/{year}?", 
                "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            
            if (result == DialogResult.Yes)
            {
                try
                {
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        conn.Open();
                        
                        // Sử dụng stored procedure sp_KhoaCongThang
                        using (SqlCommand cmd = new SqlCommand("dbo.sp_KhoaCongThang", conn))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@Nam", year);
                            cmd.Parameters.AddWithValue("@Thang", month);
                            
                            cmd.ExecuteNonQuery();
                            
                            MessageBox.Show($"Khóa công thành công cho tháng {month}/{year}!", 
                                "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            
                            LoadLockStatus();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi khi khóa công: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnXemLichSu_Click(object sender, EventArgs e)
        {
            LoadAttendanceHistory();
        }

        private void cmbThang_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl.SelectedTab == tabLichSu)
            {
                LoadAttendanceHistory();
            }
        }

        private void cmbNam_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl.SelectedTab == tabLichSu)
            {
                LoadAttendanceHistory();
            }
        }
    }
}
