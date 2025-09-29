using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using Guna.UI2.WinForms;

namespace VuToanThang_23110329.Forms
{
    public partial class frmBangLuong : Form
    {
        private string connectionString;
        private string currentUserRole;
        
        public frmBangLuong()
        {
            InitializeComponent();
            InitializeConnectionString();
        }

        private void InitializeConnectionString()
        {
            connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            // Get current user role (assuming stored in static class)
            currentUserRole = "KeToan"; // TODO: Get from session
        }

        private void frmBangLuong_Load(object sender, EventArgs e)
        {
            SetupComboBoxes();
            ConfigureRoleCapabilities();
            LoadData();
        }

        private void SetupComboBoxes()
        {
            // Setup month combobox
            cmbThang.Items.Clear();
            for (int i = 1; i <= 12; i++)
            {
                string month = i.ToString("00");
                cmbThang.Items.Add(month);
            }
            cmbThang.SelectedIndex = DateTime.Now.Month - 1;

            // Setup year combobox
            cmbNam.Items.Clear();
            int currentYear = DateTime.Now.Year;
            for (int i = currentYear - 2; i <= currentYear + 1; i++)
            {
                cmbNam.Items.Add(i.ToString());
            }
            cmbNam.SelectedItem = currentYear.ToString();
        }

        private void ConfigureRoleCapabilities()
        {
            // Only KeToan and HR can run payroll
            bool canRunPayroll = currentUserRole == "KeToan" || currentUserRole == "HR";
            btnChayLuong.Enabled = canRunPayroll;
            btnDongLuong.Enabled = canRunPayroll;
            btnCapNhatPhuCap.Enabled = canRunPayroll;
        }

        private void LoadData()
        {
            LoadAttendanceSummary();
            LoadPayrollData();
        }

        private void LoadAttendanceSummary()
        {
            try
            {
                int month = int.Parse(cmbThang.SelectedItem.ToString());
                int year = int.Parse(cmbNam.SelectedItem.ToString());
                
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = @"
                        SELECT 
                            nv.MaNV,
                            nv.HoTen,
                            pb.TenPhongBan,
                            cv.TenChucVu,
                            nv.LuongCoBan,
                            ISNULL(SUM(cc.GioCong), 0) as TongGioCong,
                            ISNULL(COUNT(cc.NgayLam), 0) as SoNgayLam,
                            ISNULL(SUM(cc.DiTrePhut), 0) as TongDiTre,
                            ISNULL(SUM(cc.VeSomPhut), 0) as TongVeSom
                        FROM dbo.NhanVien nv
                        LEFT JOIN dbo.PhongBan pb ON nv.MaPhongBan = pb.MaPhongBan
                        LEFT JOIN dbo.ChucVu cv ON nv.MaChucVu = cv.MaChucVu
                        LEFT JOIN dbo.ChamCong cc ON nv.MaNV = cc.MaNV 
                            AND YEAR(cc.NgayLam) = @Nam 
                            AND MONTH(cc.NgayLam) = @Thang
                            AND cc.Khoa = 1
                        WHERE nv.TrangThai = N'DangLam'
                        GROUP BY nv.MaNV, nv.HoTen, pb.TenPhongBan, cv.TenChucVu, nv.LuongCoBan
                        ORDER BY nv.HoTen";
                    
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Nam", year);
                        cmd.Parameters.AddWithValue("@Thang", month);
                        
                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        
                        dgvCongThang.DataSource = dt;
                        
                        // Setup columns
                        if (dgvCongThang.Columns.Count > 0)
                        {
                            dgvCongThang.Columns["MaNV"].HeaderText = "Mã NV";
                            dgvCongThang.Columns["HoTen"].HeaderText = "Họ tên";
                            dgvCongThang.Columns["TenPhongBan"].HeaderText = "Phòng ban";
                            dgvCongThang.Columns["TenChucVu"].HeaderText = "Chức vụ";
                            dgvCongThang.Columns["LuongCoBan"].HeaderText = "Lương cơ bản";
                            dgvCongThang.Columns["TongGioCong"].HeaderText = "Tổng giờ công";
                            dgvCongThang.Columns["SoNgayLam"].HeaderText = "Số ngày làm";
                            dgvCongThang.Columns["TongDiTre"].HeaderText = "Tổng đi trễ (phút)";
                            dgvCongThang.Columns["TongVeSom"].HeaderText = "Tổng về sớm (phút)";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải dữ liệu công: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadPayrollData()
        {
            try
            {
                int month = int.Parse(cmbThang.SelectedItem.ToString());
                int year = int.Parse(cmbNam.SelectedItem.ToString());
                
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = @"
                        SELECT 
                            nv.MaNV,
                            nv.HoTen,
                            pb.TenPhongBan,
                            cv.TenChucVu,
                            nv.LuongCoBan,
                            ISNULL(SUM(cc.GioCong), 0) as TongGioCong,
                            ISNULL(SUM(cc.GioCong) * nv.LuongCoBan / 160, 0) as LuongThucTe,
                            ISNULL(SUM(cc.GioCong) * nv.LuongCoBan / 160 * 0.105, 0) as BHXH,
                            ISNULL(SUM(cc.GioCong) * nv.LuongCoBan / 160 * 0.015, 0) as BHYT,
                            ISNULL(SUM(cc.GioCong) * nv.LuongCoBan / 160 * 0.01, 0) as BHTN,
                            ISNULL(SUM(cc.GioCong) * nv.LuongCoBan / 160 * 0.1, 0) as ThueTNCN,
                            ISNULL(SUM(cc.GioCong) * nv.LuongCoBan / 160 * 0.8, 0) as LuongNhan
                        FROM dbo.NhanVien nv
                        LEFT JOIN dbo.PhongBan pb ON nv.MaPhongBan = pb.MaPhongBan
                        LEFT JOIN dbo.ChucVu cv ON nv.MaChucVu = cv.MaChucVu
                        LEFT JOIN dbo.ChamCong cc ON nv.MaNV = cc.MaNV 
                            AND YEAR(cc.NgayLam) = @Nam 
                            AND MONTH(cc.NgayLam) = @Thang
                            AND cc.Khoa = 1
                        WHERE nv.TrangThai = N'DangLam'
                        GROUP BY nv.MaNV, nv.HoTen, pb.TenPhongBan, cv.TenChucVu, nv.LuongCoBan
                        ORDER BY nv.HoTen";
                    
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Nam", year);
                        cmd.Parameters.AddWithValue("@Thang", month);
                        
                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        
                        dgvBangLuong.DataSource = dt;
                        
                        // Setup columns
                        if (dgvBangLuong.Columns.Count > 0)
                        {
                            dgvBangLuong.Columns["MaNV"].HeaderText = "Mã NV";
                            dgvBangLuong.Columns["HoTen"].HeaderText = "Họ tên";
                            dgvBangLuong.Columns["TenPhongBan"].HeaderText = "Phòng ban";
                            dgvBangLuong.Columns["TenChucVu"].HeaderText = "Chức vụ";
                            dgvBangLuong.Columns["LuongCoBan"].HeaderText = "Lương cơ bản";
                            dgvBangLuong.Columns["TongGioCong"].HeaderText = "Tổng giờ công";
                            dgvBangLuong.Columns["LuongThucTe"].HeaderText = "Lương thực tế";
                            dgvBangLuong.Columns["BHXH"].HeaderText = "BHXH";
                            dgvBangLuong.Columns["BHYT"].HeaderText = "BHYT";
                            dgvBangLuong.Columns["BHTN"].HeaderText = "BHTN";
                            dgvBangLuong.Columns["ThueTNCN"].HeaderText = "Thuế TNCN";
                            dgvBangLuong.Columns["LuongNhan"].HeaderText = "Lương nhận";
                        }
                        
                        UpdateSummaryLabels(dt);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải dữ liệu lương: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateSummaryLabels(DataTable dt)
        {
            int totalEmployees = dt.Rows.Count;
            decimal totalSalary = 0;
            decimal totalAllowance = 0;
            decimal totalDeduction = 0;
            
            foreach (DataRow row in dt.Rows)
            {
                totalSalary += Convert.ToDecimal(row["LuongThucTe"]);
                totalDeduction += Convert.ToDecimal(row["BHXH"]) + Convert.ToDecimal(row["BHYT"]) + 
                                 Convert.ToDecimal(row["BHTN"]) + Convert.ToDecimal(row["ThueTNCN"]);
            }
            
            lblTongNhanVien.Text = $"Tổng nhân viên: {totalEmployees}";
            lblTongLuong.Text = $"Tổng lương: {totalSalary:N0} VNĐ";
            lblTongPhuCap.Text = $"Tổng phụ cấp: {totalAllowance:N0} VNĐ";
            lblTongKhauTru.Text = $"Tổng khấu trừ: {totalDeduction:N0} VNĐ";
        }

        private void btnChayLuong_Click(object sender, EventArgs e)
        {
            int month = int.Parse(cmbThang.SelectedItem.ToString());
            int year = int.Parse(cmbNam.SelectedItem.ToString());
            
            DialogResult result = MessageBox.Show($"Bạn có chắc chắn muốn chạy bảng lương tháng {month}/{year}?", 
                "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            
            if (result == DialogResult.Yes)
            {
                try
                {
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        conn.Open();
                        
                        // Check if attendance is locked for this month
                        string checkQuery = @"
                            SELECT COUNT(*) 
                            FROM dbo.ChamCong 
                            WHERE YEAR(NgayLam) = @Nam 
                            AND MONTH(NgayLam) = @Thang 
                            AND Khoa = 0";
                        
                        using (SqlCommand checkCmd = new SqlCommand(checkQuery, conn))
                        {
                            checkCmd.Parameters.AddWithValue("@Nam", year);
                            checkCmd.Parameters.AddWithValue("@Thang", month);
                            
                            int unlockedCount = Convert.ToInt32(checkCmd.ExecuteScalar());
                            
                            if (unlockedCount > 0)
                            {
                                MessageBox.Show($"Không thể chạy lương! Còn {unlockedCount} bản ghi chấm công chưa được khóa.", 
                                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                        }
                        
                        // Calculate and insert payroll data
                        string payrollQuery = @"
                            INSERT INTO dbo.BangLuong (MaNV, Thang, Nam, LuongCoBan, TongGioCong, LuongThucTe, BHXH, BHYT, BHTN, ThueTNCN, LuongNhan, TrangThai)
                            SELECT 
                                nv.MaNV,
                                @Thang,
                                @Nam,
                                nv.LuongCoBan,
                                ISNULL(SUM(cc.GioCong), 0),
                                ISNULL(SUM(cc.GioCong) * nv.LuongCoBan / 160, 0),
                                ISNULL(SUM(cc.GioCong) * nv.LuongCoBan / 160 * 0.105, 0),
                                ISNULL(SUM(cc.GioCong) * nv.LuongCoBan / 160 * 0.015, 0),
                                ISNULL(SUM(cc.GioCong) * nv.LuongCoBan / 160 * 0.01, 0),
                                ISNULL(SUM(cc.GioCong) * nv.LuongCoBan / 160 * 0.1, 0),
                                ISNULL(SUM(cc.GioCong) * nv.LuongCoBan / 160 * 0.8, 0),
                                N'ChuaDong'
                            FROM dbo.NhanVien nv
                            LEFT JOIN dbo.ChamCong cc ON nv.MaNV = cc.MaNV 
                                AND YEAR(cc.NgayLam) = @Nam 
                                AND MONTH(cc.NgayLam) = @Thang
                                AND cc.Khoa = 1
                            WHERE nv.TrangThai = N'DangLam'
                            GROUP BY nv.MaNV, nv.LuongCoBan
                            HAVING NOT EXISTS (
                                SELECT 1 FROM dbo.BangLuong bl 
                                WHERE bl.MaNV = nv.MaNV AND bl.Thang = @Thang AND bl.Nam = @Nam
                            )";
                        
                        using (SqlCommand cmd = new SqlCommand(payrollQuery, conn))
                        {
                            cmd.Parameters.AddWithValue("@Thang", month);
                            cmd.Parameters.AddWithValue("@Nam", year);
                            
                            int rowsAffected = cmd.ExecuteNonQuery();
                            
                            MessageBox.Show($"Chạy bảng lương thành công! Đã tạo {rowsAffected} bản ghi lương.", 
                                "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            
                            LoadData();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi khi chạy bảng lương: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnDongLuong_Click(object sender, EventArgs e)
        {
            int month = int.Parse(cmbThang.SelectedItem.ToString());
            int year = int.Parse(cmbNam.SelectedItem.ToString());
            
            DialogResult result = MessageBox.Show($"Bạn có chắc chắn muốn đóng bảng lương tháng {month}/{year}?", 
                "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            
            if (result == DialogResult.Yes)
            {
                try
                {
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        conn.Open();
                        string query = @"
                            UPDATE dbo.BangLuong 
                            SET TrangThai = N'DaDong'
                            WHERE Thang = @Thang AND Nam = @Nam AND TrangThai = N'ChuaDong'";
                        
                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@Thang", month);
                            cmd.Parameters.AddWithValue("@Nam", year);
                            
                            int rowsAffected = cmd.ExecuteNonQuery();
                            
                            MessageBox.Show($"Đóng bảng lương thành công! Đã đóng {rowsAffected} bản ghi lương.", 
                                "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            
                            LoadData();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi khi đóng bảng lương: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnCapNhatPhuCap_Click(object sender, EventArgs e)
        {
            using (var dialog = new AllowanceDialog())
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        using (SqlConnection conn = new SqlConnection(connectionString))
                        {
                            conn.Open();
                            string query = @"
                                UPDATE dbo.BangLuong 
                                SET PhuCap = @PhuCap
                                WHERE MaNV = @MaNV AND Thang = @Thang AND Nam = @Nam";
                            
                            using (SqlCommand cmd = new SqlCommand(query, conn))
                            {
                                cmd.Parameters.AddWithValue("@MaNV", dialog.EmployeeId);
                                cmd.Parameters.AddWithValue("@Thang", int.Parse(cmbThang.SelectedItem.ToString()));
                                cmd.Parameters.AddWithValue("@Nam", int.Parse(cmbNam.SelectedItem.ToString()));
                                cmd.Parameters.AddWithValue("@PhuCap", dialog.AllowanceAmount);
                                
                                int rowsAffected = cmd.ExecuteNonQuery();
                                
                                if (rowsAffected > 0)
                                {
                                    MessageBox.Show("Cập nhật phụ cấp thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    LoadData();
                                }
                                else
                                {
                                    MessageBox.Show("Không tìm thấy bản ghi lương để cập nhật!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Lỗi khi cập nhật phụ cấp: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void cmbThang_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        private void cmbNam_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        private void tabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadData();
        }
    }

    // Simple dialog for updating allowance
    public class AllowanceDialog : Form
    {
        private TextBox txtEmployeeId;
        private TextBox txtAllowanceAmount;
        private Button btnOK;
        private Button btnCancel;

        public int EmployeeId => int.Parse(txtEmployeeId.Text);
        public decimal AllowanceAmount => decimal.Parse(txtAllowanceAmount.Text);

        public AllowanceDialog()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.Text = "Cập nhật phụ cấp";
            this.Size = new Size(400, 200);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            var lblEmployeeId = new Label { Text = "Mã nhân viên:", Location = new Point(20, 20), Size = new Size(100, 20) };
            txtEmployeeId = new TextBox { Location = new Point(130, 18), Size = new Size(200, 20) };

            var lblAllowance = new Label { Text = "Số tiền phụ cấp:", Location = new Point(20, 50), Size = new Size(100, 20) };
            txtAllowanceAmount = new TextBox { Location = new Point(130, 48), Size = new Size(200, 20) };

            btnOK = new Button { Text = "OK", Location = new Point(200, 100), Size = new Size(75, 25), DialogResult = DialogResult.OK };
            btnCancel = new Button { Text = "Hủy", Location = new Point(280, 100), Size = new Size(75, 25), DialogResult = DialogResult.Cancel };

            btnOK.Click += (s, e) => {
                if (string.IsNullOrEmpty(txtEmployeeId.Text) || string.IsNullOrEmpty(txtAllowanceAmount.Text))
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (!int.TryParse(txtEmployeeId.Text, out _) || !decimal.TryParse(txtAllowanceAmount.Text, out _))
                {
                    MessageBox.Show("Vui lòng nhập đúng định dạng số!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                DialogResult = DialogResult.OK;
                Close();
            };

            this.Controls.AddRange(new Control[] { lblEmployeeId, txtEmployeeId, lblAllowance, txtAllowanceAmount, btnOK, btnCancel });
        }
        }
    }
}
