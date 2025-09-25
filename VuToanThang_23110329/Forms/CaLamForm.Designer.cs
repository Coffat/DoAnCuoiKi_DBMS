using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace VuToanThang_23110329.Forms
{
    partial class CaLamForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.BackColor = Color.FromArgb(50, 50, 50);
            this.Dock = DockStyle.Fill;
            this.Text = "Quản lý ca làm";
            this.Padding = new Padding(20);
            this.MinimumSize = new Size(800, 600);

            CreateControls();
            SetupEventHandlers();

            this.Load += (s, e) => PerformLayoutLayout();
            this.Resize += (s, e) => PerformLayoutLayout();
        }

        private void CreateControls()
        {
            var lblTitle = new Label { Text = "QUẢN LÝ CA LÀM", Font = new Font("Segoe UI", 20, FontStyle.Bold), ForeColor = Color.FromArgb(124, 77, 255), AutoSize = true };
            txtSearch = new TextBox { Width = 300, BackColor = Color.FromArgb(60, 60, 60), ForeColor = Color.White, BorderStyle = BorderStyle.FixedSingle };

            btnThem = CreateButton("Thêm", Color.FromArgb(46, 125, 50));
            btnSua = CreateButton("Sửa", Color.FromArgb(255, 152, 0));
            btnXoa = CreateButton("Xóa", Color.FromArgb(244, 67, 54));
            btnLuu = CreateButton("Lưu", Color.FromArgb(33, 150, 243), false);
            btnHuy = CreateButton("Hủy", Color.FromArgb(158, 158, 158), false);
            btnLamMoi = CreateButton("Làm mới", Color.FromArgb(96, 125, 139));

            dgvCaLam = CreateDataGridView();
            pnlThongTin = new Panel { BackColor = Color.FromArgb(60, 60, 60), BorderStyle = BorderStyle.FixedSingle, Padding = new Padding(15) };
            CreateInfoControls();

            this.Controls.AddRange(new Control[] { lblTitle, CreateLabel("Tìm kiếm:"), txtSearch, btnThem, btnSua, btnXoa, btnLuu, btnHuy, btnLamMoi, dgvCaLam, pnlThongTin });
        }

        private void CreateInfoControls()
        {
            var lblThongTin = new Label { Text = "THÔNG TIN CA LÀM", Font = new Font("Segoe UI", 14, FontStyle.Bold), ForeColor = Color.FromArgb(124, 77, 255), AutoSize = true };
            txtTenCa = CreateTextBox();
            dtpGioBatDau = CreateTimePicker();
            dtpGioKetThuc = CreateTimePicker();
            txtHeSoCa = CreateTextBox();
            txtMoTa = new TextBox { Multiline = true, ScrollBars = ScrollBars.Vertical, BackColor = Color.FromArgb(70, 70, 70), ForeColor = Color.White, BorderStyle = BorderStyle.FixedSingle, Font = new Font("Segoe UI", 9) };
            chkKichHoat = new CheckBox { ForeColor = Color.White, Font = new Font("Segoe UI", 9), Checked = true };

            pnlThongTin.Controls.AddRange(new Control[] { lblThongTin, CreateLabel("Tên ca:"), txtTenCa, CreateLabel("Giờ bắt đầu:"), dtpGioBatDau, CreateLabel("Giờ kết thúc:"), dtpGioKetThuc, CreateLabel("Hệ số ca:"), txtHeSoCa, CreateLabel("Mô tả:"), txtMoTa, CreateLabel("Kích hoạt:"), chkKichHoat });
        }

        private void SetupEventHandlers()
        {
            txtSearch.TextChanged += txtSearch_TextChanged;
            dgvCaLam.SelectionChanged += dgvCaLam_SelectionChanged;
            btnThem.Click += btnThem_Click;
            btnSua.Click += btnSua_Click;
            btnXoa.Click += btnXoa_Click;
            btnLuu.Click += btnLuu_Click;
            btnHuy.Click += btnHuy_Click;
            btnLamMoi.Click += btnLamMoi_Click;
        }

        #region Layout Logic
        private void PerformLayoutLayout() { /* Omitted for brevity */ }
        #endregion

        #region Control Factory Methods
        private Button CreateButton(string text, Color backColor, bool enabled = true) => new Button { Text = text, BackColor = backColor, ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Size = new Size(80, 35), Font = new Font("Segoe UI", 9, FontStyle.Bold), Enabled = enabled };
        private TextBox CreateTextBox() => new TextBox { BackColor = Color.FromArgb(70, 70, 70), ForeColor = Color.White, BorderStyle = BorderStyle.FixedSingle, Font = new Font("Segoe UI", 9) };
        private DateTimePicker CreateTimePicker() => new DateTimePicker { Format = DateTimePickerFormat.Time, ShowUpDown = true, Font = new Font("Segoe UI", 9) };
        private DataGridView CreateDataGridView() => new DataGridView { BackgroundColor = Color.FromArgb(60, 60, 60), ForeColor = Color.White, BorderStyle = BorderStyle.None, AllowUserToAddRows = false, AllowUserToDeleteRows = false, ReadOnly = true, SelectionMode = DataGridViewSelectionMode.FullRowSelect, MultiSelect = false, AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill, ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle { BackColor = Color.FromArgb(124, 77, 255), ForeColor = Color.White, Font = new Font("Segoe UI", 10.5F, FontStyle.Bold) }, DefaultCellStyle = new DataGridViewCellStyle { BackColor = Color.FromArgb(60, 60, 60), ForeColor = Color.White, SelectionBackColor = Color.FromArgb(124, 77, 255) } };
        private Label CreateLabel(string text) => new Label { Text = text, ForeColor = Color.White, AutoSize = true, Font = new Font("Segoe UI", 9) };
        #endregion

        #endregion
    }
}
