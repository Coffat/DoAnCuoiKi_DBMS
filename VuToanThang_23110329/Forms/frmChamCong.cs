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
            connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            // Get current user info (assuming stored in static class)
            currentUserId = 1; // TODO: Get from session
            currentUserRole = "NhanVien"; // TODO: Get from session
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
                    
                    // Check if there's a schedule for today
                    string scheduleQuery = @"
                        SELECT lpc.MaCa, cl.TenCa, cl.GioBatDau, cl.GioKetThuc
                        FROM dbo.LichPhanCa lpc
                        INNER JOIN dbo.CaLam cl ON lpc.MaCa = cl.MaCa
                        WHERE lpc.MaNV = @MaNV AND lpc.NgayLam = @NgayLam AND lpc.TrangThai = N'DuKien'";
                    
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
                    DateTime today = now.Date;
                    
                    string query = @"
                        IF EXISTS (SELECT 1 FROM dbo.ChamCong WHERE MaNV = @MaNV AND NgayLam = @NgayLam)
                            UPDATE dbo.ChamCong SET GioVao = @GioVao WHERE MaNV = @MaNV AND NgayLam = @NgayLam
                        ELSE
                            INSERT INTO dbo.ChamCong (MaNV, NgayLam, GioVao) VALUES (@MaNV, @NgayLam, @GioVao)";
                    
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaNV", currentUserId);
                        cmd.Parameters.AddWithValue("@NgayLam", today);
                        cmd.Parameters.AddWithValue("@GioVao", now);
                        
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
                    DateTime today = now.Date;
                    
                    string query = @"
                        UPDATE dbo.ChamCong 
                        SET GioRa = @GioRa, GioCong = DATEDIFF(MINUTE, GioVao, @GioRa) / 60.0
                        WHERE MaNV = @MaNV AND NgayLam = @NgayLam AND GioVao IS NOT NULL";
                    
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaNV", currentUserId);
                        cmd.Parameters.AddWithValue("@NgayLam", today);
                        cmd.Parameters.AddWithValue("@GioRa", now);
                        
                        int rowsAffected = cmd.ExecuteNonQuery();
                        
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show($"Check out thành công lúc {now:HH:mm}", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadCurrentStatus();
                        }
                        else
                        {
                            MessageBox.Show("Không thể check out. Vui lòng check in trước!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
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
                        string query = @"
                            UPDATE dbo.ChamCong 
                            SET Khoa = 1
                            WHERE YEAR(NgayLam) = @Nam 
                            AND MONTH(NgayLam) = @Thang";
                        
                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@Nam", year);
                            cmd.Parameters.AddWithValue("@Thang", month);
                            
                            int rowsAffected = cmd.ExecuteNonQuery();
                            
                            MessageBox.Show($"Khóa công thành công! Đã khóa {rowsAffected} bản ghi.", 
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
