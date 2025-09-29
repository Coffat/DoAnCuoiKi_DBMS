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

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (_isReadOnlyForRole)
            {
                MessageBox.Show("Bạn không có quyền tạo lịch.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            // Open dialog or inline add behavior
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (_isReadOnlyForRole)
            {
                MessageBox.Show("Bạn không có quyền sửa lịch.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (_isReadOnlyForRole)
            {
                MessageBox.Show("Bạn không có quyền xóa lịch.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (_isReadOnlyForRole)
            {
                MessageBox.Show("Bạn không có quyền lưu thay đổi.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            // Persist changes
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            // TODO: Cancel logic here
        }

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
            // Auto-generate weekly schedule based on templates/rules
        }

        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            // TODO: Search logic here
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
        }

        private void ClearForm()
        {
            // TODO: Clear form logic here
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
                Width = 120,
                ReadOnly = true
            };
            dgvTuan.Columns.Add(colShift);
            BuildWeekColumns();
            // Light theme for readability
            dgvTuan.DefaultCellStyle.ForeColor = Color.Black;
            dgvTuan.DefaultCellStyle.BackColor = Color.White;
            dgvTuan.DefaultCellStyle.SelectionBackColor = Color.FromArgb(210, 227, 252); // Light blue
            dgvTuan.DefaultCellStyle.SelectionForeColor = Color.Black;
            dgvTuan.AllowUserToResizeColumns = false;
            dgvTuan.AllowUserToResizeRows = false;
            dgvTuan.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dgvTuan.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dgvTuan.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            dgvTuan.RowTemplate.Height = 48;
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
                    Width = 130,
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

                            // Màu sắc theo trạng thái
                            if (info.Any(x => x.TrangThai == "Khoa"))
                                dgvTuan.Rows[rowIndex].Cells[colIndex].Style.BackColor = Color.FromArgb(255, 230, 230);
                            else if (info.Any(x => x.TrangThai == "Huy"))
                                dgvTuan.Rows[rowIndex].Cells[colIndex].Style.BackColor = Color.FromArgb(220, 220, 220);
                        }
                        else
                        {
                            dgvTuan.Rows[rowIndex].Cells[colIndex].Value = "";
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
            btnThem.Enabled = !_isReadOnlyForRole;
            btnSua.Enabled = !_isReadOnlyForRole;
            btnXoa.Enabled = !_isReadOnlyForRole;
            btnLuu.Enabled = !_isReadOnlyForRole;
            btnHuy.Enabled = !_isReadOnlyForRole;
            btnTaoLichTuan.Enabled = !_isReadOnlyForRole;
            dgvTuan.ReadOnly = _isReadOnlyForRole;
        }

        private void InitializeEmployeeCombo()
        {
            cboNhanVien.Items.Clear();
            cboNhanVien.Items.Add("Tất cả nhân viên");
            
            if (string.IsNullOrEmpty(_connectionString)) return;
            
            try
            {
                using (var conn = new SqlConnection(_connectionString))
                using (var cmd = new SqlCommand("SELECT MaNV, HoTen FROM dbo.NhanVien WHERE TrangThai = N'DangLam' ORDER BY HoTen", conn))
                {
                    conn.Open();
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int maNV = reader.GetInt32(0);
                            string hoTen = reader.GetString(1);
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
            if (e.RowIndex < 0 || e.ColumnIndex <= 0) return;
            if (_isReadOnlyForRole) return;
            // Show quick select of shift assignment or employee
        }

        private void dgvTuan_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex <= 0) return;
            if (_isReadOnlyForRole) return;
            // Open detail editor dialog
        }

        private void dgvTuan_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;
            if (e.ColumnIndex == 0) return; // shift column
            // Example: color weekends
            DateTime day = _currentWeekStartDate.AddDays(e.ColumnIndex - 1);
            if (day.DayOfWeek == DayOfWeek.Saturday || day.DayOfWeek == DayOfWeek.Sunday)
            {
                e.CellStyle.BackColor = Color.FromArgb(40, 40, 60);
            }
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
            if (string.IsNullOrEmpty(_connectionString)) return;
            using (var conn = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand("SELECT MaCa, TenCa, GioBatDau, GioKetThuc FROM dbo.CaLam ORDER BY MaCa", conn))
            {
                conn.Open();
                using (var rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        _shifts.Add(new Shift
                        {
                            MaCa = rd.GetInt32(0),
                            TenCa = rd.GetString(1),
                            GioBatDau = (TimeSpan)rd[2],
                            GioKetThuc = (TimeSpan)rd[3]
                        });
                    }
                }
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

            using (var conn = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand(@"
                SELECT lpc.MaCa, lpc.NgayLam, nv.HoTen, lpc.TrangThai
                FROM dbo.LichPhanCa lpc
                INNER JOIN dbo.NhanVien nv ON nv.MaNV = lpc.MaNV
                WHERE lpc.NgayLam BETWEEN @D0 AND @D1
                ORDER BY lpc.NgayLam, lpc.MaCa, nv.HoTen
            ", conn))
            {
                cmd.Parameters.AddWithValue("@D0", fromDate.Date);
                cmd.Parameters.AddWithValue("@D1", toDate.Date);
                conn.Open();
                using (var rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        int maCa = rd.GetInt32(0);
                        DateTime ngay = rd.GetDateTime(1).Date;
                        string hoTen = rd.GetString(2);
                        string trangThai = rd.GetString(3);
                        var key = (maCa, ngay);
                        if (!map.TryGetValue(key, out var list))
                        {
                            list = new List<AssignmentInfo>();
                            map[key] = list;
                        }
                        list.Add(new AssignmentInfo { HoTen = hoTen, TrangThai = trangThai });
                    }
                }
            }

            return map;
        }

        private void pnlButtons_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
