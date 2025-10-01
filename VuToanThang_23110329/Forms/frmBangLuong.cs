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
            // Get current user role from session
            currentUserRole = UserSession.VaiTro ?? "KeToan";
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
                        SELECT 
                            nv.MaNV,
                            nv.HoTen,
                            ISNULL(pb.TenPhongBan, N'Chưa phân công') as TenPhongBan,
                            ISNULL(cv.TenChucVu, N'Chưa phân công') as TenChucVu,
                            ISNULL(nv.LuongCoBan, 0) as LuongCoBan,
                            ISNULL(ct.TongGioCong, 0) as TongGioCong,
                            (
                                SELECT COUNT(DISTINCT cc.NgayLam)
                                FROM dbo.ChamCong cc
                                WHERE cc.MaNV = nv.MaNV 
                                  AND YEAR(cc.NgayLam) = @Nam 
                                  AND MONTH(cc.NgayLam) = @Thang
                            ) as SoNgayLam,
                            ISNULL(ct.TongPhutDiTre, 0) as TongDiTre,
                            ISNULL(ct.TongPhutVeSom, 0) as TongVeSom
                        FROM dbo.NhanVien nv
                        LEFT JOIN dbo.PhongBan pb ON pb.MaPhongBan = nv.MaPhongBan
                        LEFT JOIN dbo.ChucVu cv ON cv.MaChucVu = nv.MaChucVu
                        LEFT JOIN dbo.vw_CongThang ct ON ct.MaNV = nv.MaNV 
                            AND ct.Nam = @Nam AND ct.Thang = @Thang
                        WHERE nv.TrangThai = N'DangLam'
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
                        SELECT 
                            bl.MaNV,
                            nv.HoTen,
                            ISNULL(pb.TenPhongBan, N'Chưa phân công') as TenPhongBan,
                            ISNULL(cv.TenChucVu, N'Chưa phân công') as TenChucVu,
                            ISNULL(bl.LuongCoBan, 0) as LuongCoBan,
                            ISNULL(bl.TongGioCong, 0) as TongGioCong,
                            ISNULL(bl.GioOT, 0) as GioOT,
                            ISNULL(bl.PhuCap, 0) as PhuCap,
                            ISNULL(bl.KhauTru, 0) as KhauTru,
                            ISNULL(bl.ThueBH, 0) as ThueBH,
                            ISNULL(bl.ThucLanh, 0) as ThucLanh
                        FROM dbo.BangLuong bl
                        INNER JOIN dbo.NhanVien nv ON nv.MaNV = bl.MaNV
                        LEFT JOIN dbo.PhongBan pb ON pb.MaPhongBan = nv.MaPhongBan
                        LEFT JOIN dbo.ChucVu cv ON cv.MaChucVu = nv.MaChucVu
                        WHERE bl.Nam = @Nam AND bl.Thang = @Thang
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
                            dgvBangLuong.Columns["GioOT"].HeaderText = "Giờ OT";
                            dgvBangLuong.Columns["PhuCap"].HeaderText = "Phụ cấp";
                            dgvBangLuong.Columns["KhauTru"].HeaderText = "Khấu trừ";
                            dgvBangLuong.Columns["ThueBH"].HeaderText = "Thuế BH";
                            dgvBangLuong.Columns["ThucLanh"].HeaderText = "Thực lãnh";
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
                totalSalary += Convert.ToDecimal(row["ThucLanh"]);
                totalAllowance += Convert.ToDecimal(row["PhuCap"]);
                totalDeduction += Convert.ToDecimal(row["KhauTru"]) + Convert.ToDecimal(row["ThueBH"]);
            }
            
            lblTongNhanVien.Text = $"Tổng nhân viên: {totalEmployees}";
            lblTongLuong.Text = $"Tổng lương: {totalSalary:N0} VNĐ";
            lblTongPhuCap.Text = $"Tổng phụ cấp: {totalAllowance:N0} VNĐ";
            lblTongKhauTru.Text = $"Tổng khấu trừ: {totalDeduction:N0} VNĐ";
        }

        private void btnChayLuong_Click(object sender, EventArgs e)
        {
            if (cmbThang.SelectedItem == null || cmbNam.SelectedItem == null)
            {
                MessageBox.Show("Vui lòng chọn tháng và năm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            
            // ✅ Kiểm tra quyền
            if (UserSession.VaiTro != "KeToan")
            {
                MessageBox.Show("Chỉ kế toán mới có quyền chạy bảng lương.", 
                    "Không có quyền", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            
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
                        using (SqlCommand cmd = new SqlCommand("dbo.sp_ChayBangLuong", conn))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@Nam", year);
                            cmd.Parameters.AddWithValue("@Thang", month);
                            
                            cmd.ExecuteNonQuery();
                            
                            MessageBox.Show($"Chạy bảng lương thành công cho tháng {month}/{year}!", 
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
            if (cmbThang.SelectedItem == null || cmbNam.SelectedItem == null)
            {
                MessageBox.Show("Vui lòng chọn tháng và năm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            
            // ✅ Kiểm tra quyền
            if (UserSession.VaiTro != "KeToan")
            {
                MessageBox.Show("Chỉ kế toán mới có quyền đóng bảng lương.", 
                    "Không có quyền", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            
            int month = int.Parse(cmbThang.SelectedItem.ToString());
            int year = int.Parse(cmbNam.SelectedItem.ToString());
            
            DialogResult result = MessageBox.Show($"Bạn có chắc chắn muốn đóng bảng lương tháng {month}/{year}?\nSau khi đóng sẽ không thể chỉnh sửa!", 
                "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            
            if (result == DialogResult.Yes)
            {
                try
                {
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        conn.Open();
                        using (SqlCommand cmd = new SqlCommand("dbo.sp_DongBangLuong", conn))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@Nam", year);
                            cmd.Parameters.AddWithValue("@Thang", month);
                            
                            cmd.ExecuteNonQuery();
                            
                            MessageBox.Show($"Đóng bảng lương thành công cho tháng {month}/{year}!", 
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
                                cmd.Parameters.AddWithValue("@Thang", cmbThang.SelectedItem != null ? int.Parse(cmbThang.SelectedItem.ToString()) : DateTime.Now.Month);
                                cmd.Parameters.AddWithValue("@Nam", cmbNam.SelectedItem != null ? int.Parse(cmbNam.SelectedItem.ToString()) : DateTime.Now.Year);
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
