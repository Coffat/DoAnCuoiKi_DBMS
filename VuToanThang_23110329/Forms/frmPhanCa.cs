using System;
using System.Drawing;
using System.Windows.Forms;
using Guna.UI2.WinForms;

namespace VuToanThang_23110329.Forms
{
    public partial class frmPhanCa : Form
    {
        private DateTime _currentWeekStartDate;
        private bool _isReadOnlyForRole;

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
            dgvTuan.DefaultCellStyle.ForeColor = Color.White;
            dgvTuan.DefaultCellStyle.BackColor = Color.FromArgb(35, 35, 35);
            dgvTuan.DefaultCellStyle.SelectionBackColor = Color.FromArgb(55, 55, 55);
            dgvTuan.DefaultCellStyle.SelectionForeColor = Color.White;
            dgvTuan.AllowUserToResizeColumns = false;
            dgvTuan.AllowUserToResizeRows = false;
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
                    Width = 90,
                    ReadOnly = _isReadOnlyForRole
                };
                dgvTuan.Columns.Add(col);
            }
        }

        private void PopulateWeekData()
        {
            dgvTuan.Rows.Clear();
            // Placeholder shifts; in real app load from DB `CaLam`
            var shifts = new[] { "Sáng (08:00-12:00)", "Chiều (13:00-17:00)", "Tối (18:00-22:00)" };
            foreach (var shift in shifts)
            {
                int rowIndex = dgvTuan.Rows.Add();
                dgvTuan.Rows[rowIndex].Cells[0].Value = shift;
                for (int i = 1; i < dgvTuan.Columns.Count; i++)
                {
                    dgvTuan.Rows[rowIndex].Cells[i].Value = "-"; // no assignment yet
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
            // TODO: determine role from session/auth; default read-only for non-HR
            string role = Environment.GetEnvironmentVariable("APP_USER_ROLE") ?? "HR";
            _isReadOnlyForRole = !(role == "HR");
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
            // Placeholder: load employees; replace with DB query later
            cboNhanVien.Items.Clear();
            cboNhanVien.Items.Add("Tất cả nhân viên");
            cboNhanVien.Items.Add("NV001 - Nguyễn Văn A");
            cboNhanVien.Items.Add("NV002 - Trần Thị B");
            cboNhanVien.StartIndex = 0;
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
    }
}
