using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Guna.UI2.WinForms;
using VuToanThang_23110329;

namespace VuToanThang_23110329.Forms
{
    public partial class frmPhanCa : Form
    {
        private DateTime _currentWeekStartDate;
        private bool _isReadOnlyForRole;
        private string _connectionString;

        private class Shift
        {
            public int MaCa { get; set; }
            public string TenCa { get; set; }
            public TimeSpan GioBatDau { get; set; }
            public TimeSpan GioKetThuc { get; set; }
        }

        private List<Shift> _shifts = new List<Shift>();

        public frmPhanCa()
        {
            InitializeComponent();
        }

        private void frmPhanCa_Load(object sender, EventArgs e)
        {
            InitializeWeekRange(DateTime.Today);
            ConfigureRoleCapabilities();
            InitializeEmployeeCombo();
            SetupDataGridView();
            InitializeConnection();
            LoadShifts();
            LoadData();
        }

        // Đã Ẩn các button này - không dùng nữa
        private void btnThem_Click(object sender, EventArgs e) { }
        private void btnSua_Click(object sender, EventArgs e) { }
        private void btnXoa_Click(object sender, EventArgs e) { }
        private void btnLuu_Click(object sender, EventArgs e) { }
        private void btnHuy_Click(object sender, EventArgs e) { }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void btnTaoLichTuan_Click(object sender, EventArgs e)
        {
            if (_isReadOnlyForRole)
            {
                MessageBox.Show("Bạn không có quyền tạo lịch tuần.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                // Mở form frmLichTuan để tạo lịch chi tiết
                var frm = new frmLichTuan();
                frm.ShowDialog();
                LoadData(); // Refresh sau khi đóng
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi mở form Lịch tuần:\n\n{ex.Message}", 
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            // Tìm kiếm theo tên nhân viên (filter trong PopulateWeekData)
            LoadData();
        }

        private void dtpTuNgay_ValueChanged(object sender, EventArgs e)
        {
            InitializeWeekRange(dtpTuNgay.Value);
            LoadData();
        }

        private void dtpDenNgay_ValueChanged(object sender, EventArgs e)
        {
            // keep within week range or reload if needed
            LoadData();
        }

        private void LoadData()
        {
            // Bind week label
            lblWeek.Text = $"Tuần: {_currentWeekStartDate:dd/MM} - {_currentWeekStartDate.AddDays(6):dd/MM}";
            // Build or refresh grid columns and data
            BuildWeekColumns();
            PopulateWeekData();
            // Đảm bảo DataGridView hiển thị đúng
            EnsureDataGridViewLayout();
        }

        private void EnsureDataGridViewLayout()
        {
            // Refresh layout để đảm bảo không bị che phủ
            dgvTuan.Refresh();
            dgvTuan.Invalidate();
            this.Refresh();
            
            // Đảm bảo tất cả cell có style đồng nhất
            EnsureUniformCellStyles();
        }

        private void EnsureUniformCellStyles()
        {
            // Reset tất cả cell styles để đảm bảo đồng nhất
            foreach (DataGridViewRow row in dgvTuan.Rows)
            {
                foreach (DataGridViewCell cell in row.Cells)
                {
                    if (cell.ColumnIndex > 0) // Bỏ qua cột đầu tiên (Ca)
                    {
                        cell.Style.BackColor = Color.White;
                        cell.Style.ForeColor = Color.Black;
                    }
                }
            }
        }

        private void ClearForm()
        {
            txtTimKiem.Clear();
            LoadData();
        }

        private void SetupDataGridView()
        {
            dgvTuan.AutoGenerateColumns = false;
            dgvTuan.Columns.Clear();
            // first column: shift name or employee depending on view
            var colShift = new DataGridViewTextBoxColumn
            {
                Name = "colShift",
                HeaderText = "Ca",
                Width = 100,
                ReadOnly = true
            };
            dgvTuan.Columns.Add(colShift);
            BuildWeekColumns();
            // Đảm bảo tất cả cell có style đồng nhất
            dgvTuan.DefaultCellStyle.ForeColor = Color.Black;
            dgvTuan.DefaultCellStyle.BackColor = Color.White;
            dgvTuan.DefaultCellStyle.SelectionBackColor = Color.FromArgb(210, 227, 252); // Light blue
            dgvTuan.DefaultCellStyle.SelectionForeColor = Color.Black;
            dgvTuan.AlternatingRowsDefaultCellStyle.BackColor = Color.White;
            dgvTuan.AlternatingRowsDefaultCellStyle.ForeColor = Color.Black;
            dgvTuan.AllowUserToResizeColumns = true;
            dgvTuan.AllowUserToResizeRows = false;
            dgvTuan.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dgvTuan.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dgvTuan.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            dgvTuan.RowTemplate.Height = 48;
            dgvTuan.ScrollBars = ScrollBars.Both;
            // Dock được thiết lập trong Designer, không cần set lại
        }

        private void BuildWeekColumns()
        {
            // remove existing day columns
            while (dgvTuan.Columns.Count > 1)
            {
                dgvTuan.Columns.RemoveAt(dgvTuan.Columns.Count - 1);
            }
            for (int i = 0; i < 7; i++)
            {
                DateTime day = _currentWeekStartDate.AddDays(i);
                var col = new DataGridViewTextBoxColumn
                {
                    Name = $"col_{day:yyyyMMdd}",
                    HeaderText = day.ToString("ddd\n dd/MM"),
                    Width = 110,
                    ReadOnly = _isReadOnlyForRole
                };
                dgvTuan.Columns.Add(col);
            }
        }

        private void PopulateWeekData()
        {
            dgvTuan.Rows.Clear();
            // Nếu chưa tải danh sách ca từ DB, hãy tải
            if (_shifts.Count == 0)
            {
                LoadShifts();
            }

            // Lấy phân công trong tuần: key = (MaCa, Ngay)
            var weekAssignments = GetWeekAssignments(_currentWeekStartDate, _currentWeekStartDate.AddDays(6));

            foreach (var shift in _shifts)
            {
                int rowIndex = dgvTuan.Rows.Add();
                dgvTuan.Rows[rowIndex].Cells[0].Value = $"{shift.TenCa} ({shift.GioBatDau:hh\\:mm}-{shift.GioKetThuc:hh\\:mm})";

                for (int colIndex = 1; colIndex < dgvTuan.Columns.Count; colIndex++)
                {
                    var col = dgvTuan.Columns[colIndex];
                    // Parse ngày từ tên cột: col_yyyyMMdd
                    if (DateTime.TryParseExact(col.Name.Replace("col_", ""), "yyyyMMdd", null, System.Globalization.DateTimeStyles.None, out var day))
                    {
                        var key = (shift.MaCa, day.Date);
                        if (weekAssignments.TryGetValue(key, out var info) && info.Count > 0)
                        {
                            // Hiển thị tên nhân viên và trạng thái
                            string cellText = string.Join("\n", info.Select(x => 
                            {
                                string status = "";
                                if (x.TrangThai == "Khoa") status = " 🔒";
                                else if (x.TrangThai == "Huy") status = " ❌";
                                return x.HoTen + status;
                            }));
                            dgvTuan.Rows[rowIndex].Cells[colIndex].Value = cellText;

                            // Đảm bảo tất cả cell có màu nền giống nhau
                            dgvTuan.Rows[rowIndex].Cells[colIndex].Style.BackColor = Color.White;
                            dgvTuan.Rows[rowIndex].Cells[colIndex].Style.ForeColor = Color.Black;
                        }
                        else
                        {
                            dgvTuan.Rows[rowIndex].Cells[colIndex].Value = "";
                            // Đảm bảo cell trống cũng có màu nền giống nhau
                            dgvTuan.Rows[rowIndex].Cells[colIndex].Style.BackColor = Color.White;
                            dgvTuan.Rows[rowIndex].Cells[colIndex].Style.ForeColor = Color.Black;
                        }
                    }
                }
            }
        }

        private void InitializeWeekRange(DateTime anyDateInWeek)
        {
            // set to Monday as start
            int diffToMonday = ((int)anyDateInWeek.DayOfWeek + 6) % 7; // Monday=0
            _currentWeekStartDate = anyDateInWeek.Date.AddDays(-diffToMonday);
            dtpTuNgay.Value = _currentWeekStartDate;
            dtpDenNgay.Value = _currentWeekStartDate.AddDays(6);
        }

        private void ConfigureRoleCapabilities()
        {
            // Kiểm tra quyền từ UserSession
            string role = UserSession.VaiTro ?? "NhanVien";
            _isReadOnlyForRole = !(role == "HR" || role == "QuanLy");
            
            // Ẩn các button không dùng - chỉ giữ btnTaoLichTuan và btnLamMoi
            btnThem.Visible = false;
            btnSua.Visible = false;
            btnXoa.Visible = false;
            btnLuu.Visible = false;
            btnHuy.Visible = false;
            
            // Chỉ cho phép HR/QuanLy mở form lịch tuần
            btnTaoLichTuan.Enabled = !_isReadOnlyForRole;
            btnLamMoi.Enabled = true; // Tất cả đều xem được
            
            dgvTuan.ReadOnly = true; // View-only grid
        }

        private void InitializeEmployeeCombo()
        {
            cboNhanVien.Items.Clear();
            cboNhanVien.Items.Add("Tất cả nhân viên");
            
            if (string.IsNullOrEmpty(_connectionString)) return;
            
            try
            {
                using (var conn = new SqlConnection(_connectionString))
                using (var cmd = new SqlCommand("SELECT MaNV, HoTen FROM dbo.vw_NhanVien_Full WHERE TrangThai = N'DangLam' ORDER BY HoTen", conn))
                {
                    conn.Open();
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int maNV = Convert.ToInt32(reader["MaNV"]);
                            string hoTen = reader["HoTen"].ToString();
                            cboNhanVien.Items.Add($"{maNV} - {hoTen}");
                        }
                    }
                }
                if (cboNhanVien.Items.Count > 0)
                    cboNhanVien.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải danh sách nhân viên: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cboNhanVien_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        private void btnPrevWeek_Click(object sender, EventArgs e)
        {
            InitializeWeekRange(_currentWeekStartDate.AddDays(-7));
            LoadData();
        }

        private void btnNextWeek_Click(object sender, EventArgs e)
        {
            InitializeWeekRange(_currentWeekStartDate.AddDays(7));
            LoadData();
        }

        private void dgvTuan_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // View-only - không làm gì
        }

        private void dgvTuan_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex <= 0) return;
            
            try
            {
                // Double-click để mở form lịch tuần chi tiết
                var frm = new frmLichTuan();
                frm.ShowDialog();
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi mở form Lịch tuần:\n\n{ex.Message}", 
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvTuan_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;
            if (e.ColumnIndex == 0) return; // shift column
            
            // Đảm bảo tất cả các cột có UI giống nhau - không phân biệt cuối tuần
            // Sử dụng màu nền mặc định cho tất cả các ngày
            e.CellStyle.BackColor = Color.White;
            e.CellStyle.ForeColor = Color.Black;
        }

        private void InitializeConnection()
        {
            try
            {
                var cs = System.Configuration.ConfigurationManager.ConnectionStrings["HrDb"];
                _connectionString = cs?.ConnectionString ?? string.Empty;
            }
            catch
            {
                _connectionString = string.Empty;
            }
        }

        private void LoadShifts()
        {
            _shifts.Clear();
            if (string.IsNullOrEmpty(_connectionString)) 
            {
                return;
            }
            
            try
            {
                using (var conn = new SqlConnection(_connectionString))
                using (var cmd = new SqlCommand("sp_CaLam_GetAll", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    conn.Open();
                    using (var rd = cmd.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            _shifts.Add(new Shift
                            {
                                MaCa = Convert.ToInt32(rd["MaCa"]),
                                TenCa = rd["TenCa"].ToString(),
                                GioBatDau = (TimeSpan)rd["GioBatDau"],
                                GioKetThuc = (TimeSpan)rd["GioKetThuc"]
                            });
                        }
                    }
                }
                
                // Debug: Đã load ca làm việc thành công
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi LoadShifts: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private class AssignmentInfo
        {
            public string HoTen { get; set; }
            public string TrangThai { get; set; }
        }

        private Dictionary<(int, DateTime), List<AssignmentInfo>> GetWeekAssignments(DateTime fromDate, DateTime toDate)
        {
            var map = new Dictionary<(int, DateTime), List<AssignmentInfo>>();
            if (string.IsNullOrEmpty(_connectionString)) return map;

            try
            {
                using (var conn = new SqlConnection(_connectionString))
                {
                    conn.Open();

                    // Kiểm tra dữ liệu có sẵn (chỉ khi cần debug)
                    // Debug code đã được tắt để tăng hiệu suất

                    using (var cmd = new SqlCommand(@"
                        SELECT lpc.MaCa, lpc.NgayLam, nv.HoTen, lpc.TrangThai
                        FROM dbo.LichPhanCa lpc
                        JOIN dbo.NhanVien nv ON nv.MaNV = lpc.MaNV
                        WHERE lpc.NgayLam BETWEEN @D0 AND @D1
                        ORDER BY lpc.NgayLam, lpc.MaCa, nv.HoTen
                    ", conn))
                    {
                        cmd.Parameters.AddWithValue("@D0", fromDate.Date);
                        cmd.Parameters.AddWithValue("@D1", toDate.Date);

                        int recordCount = 0;
                        using (var rd = cmd.ExecuteReader())
                        {
                            while (rd.Read())
                            {
                                recordCount++;
                                int maCa = Convert.ToInt32(rd["MaCa"]);
                                DateTime ngay = Convert.ToDateTime(rd["NgayLam"]).Date;
                                string hoTen = rd["HoTen"].ToString();
                                string trangThai = rd["TrangThai"].ToString();
                                var key = (maCa, ngay);
                                if (!map.TryGetValue(key, out var list))
                                {
                                    list = new List<AssignmentInfo>();
                                    map[key] = list;
                                }
                                list.Add(new AssignmentInfo { HoTen = hoTen, TrangThai = trangThai });
                            }
                        }

                        // Debug: Đã load lịch phân ca thành công
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải lịch: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return map;
        }

        private void pnlButtons_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
