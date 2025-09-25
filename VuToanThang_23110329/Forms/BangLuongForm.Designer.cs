using System;
using System.Drawing;
using System.Windows.Forms;

namespace VuToanThang_23110329.Forms
{
    partial class BangLuongForm
    {
        private System.ComponentModel.IContainer components = null;

        // UI Controls
        private DataGridView dgvBangLuong;
        private ComboBox cmbThang, cmbNam, cmbTrangThai;
        private Button btnTimKiem, btnXuatExcel, btnInBangLuong, btnLamMoi;
        private Panel pnlFilter, pnlSummary;
        private Label lblTongNhanVien, lblTongLuong, lblLuongTB, lblTitle;

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
            this.Size = new Size(900, 650);
            this.Text = "Bảng lương";
            this.Padding = new Padding(20);
            this.MinimumSize = new Size(900, 650);
            this.AutoScaleMode = AutoScaleMode.Dpi;

            CreateControls();
            SetupEventHandlers();

            this.Load += (s, e) => PerformLayoutLayout();
            this.Resize += (s, e) => PerformLayoutLayout();
        }

        private void CreateControls()
        {
            lblTitle = new Label { Text = "BẢNG LƯƠNG NHÂN VIÊN", Font = new Font("Segoe UI", 20, FontStyle.Bold), ForeColor = Color.FromArgb(124, 77, 255), AutoSize = true };

            pnlFilter = new Panel { BackColor = Color.FromArgb(60, 60, 60), BorderStyle = BorderStyle.FixedSingle };
            cmbThang = CreateComboBox();
            for (int i = 1; i <= 12; i++) cmbThang.Items.Add(i);
            cmbThang.SelectedItem = DateTime.Now.Month;
            cmbNam = CreateComboBox();
            for (int i = DateTime.Now.Year - 2; i <= DateTime.Now.Year + 1; i++) cmbNam.Items.Add(i);
            cmbNam.SelectedItem = DateTime.Now.Year;
            cmbTrangThai = CreateComboBox();
            cmbTrangThai.Items.AddRange(new[] { "Tất cả", "Mo", "Dong" });
            cmbTrangThai.SelectedIndex = 0;
            btnTimKiem = CreateButton("Tìm kiếm", Color.FromArgb(33, 150, 243));
            btnXuatExcel = CreateButton("Xuất Excel", Color.FromArgb(46, 125, 50));
            btnInBangLuong = CreateButton("In bảng lương", Color.FromArgb(156, 39, 176));
            btnLamMoi = CreateButton("Làm mới", Color.FromArgb(96, 125, 139));
            pnlFilter.Controls.AddRange(new Control[] { CreateLabel("Tháng:"), cmbThang, CreateLabel("Năm:"), cmbNam, CreateLabel("Trạng thái:"), cmbTrangThai, btnTimKiem, btnXuatExcel, btnInBangLuong, btnLamMoi });

            pnlSummary = new Panel { BackColor = Color.FromArgb(60, 60, 60), BorderStyle = BorderStyle.FixedSingle };
            lblTongNhanVien = CreateStatLabel("Tổng nhân viên: 0", Color.White);
            lblTongLuong = CreateStatLabel("Tổng chi lương: 0 VNĐ", Color.LightGreen);
            lblLuongTB = CreateStatLabel("Lương TB: 0 VNĐ", Color.LightBlue);
            pnlSummary.Controls.AddRange(new Control[] { lblTongNhanVien, lblTongLuong, lblLuongTB });

            dgvBangLuong = CreateDataGridView();

            this.Controls.AddRange(new Control[] { lblTitle, pnlFilter, pnlSummary, dgvBangLuong });
        }

        private void SetupEventHandlers()
        {
            btnTimKiem.Click += btnTimKiem_Click;
            btnXuatExcel.Click += btnXuatExcel_Click;
            btnInBangLuong.Click += btnInBangLuong_Click;
            btnLamMoi.Click += btnLamMoi_Click;
            dgvBangLuong.CellDoubleClick += dgvBangLuong_CellDoubleClick;
        }

        #region Layout Logic
        private void PerformLayoutLayout() { /* Omitted for brevity */ }
        #endregion

        #region Control Factory Methods
        private Label CreateStatLabel(string text, Color color) => new Label { Text = text, ForeColor = color, Font = new Font("Segoe UI", 12, FontStyle.Bold), AutoSize = true };
        private Button CreateButton(string text, Color backColor) => new Button { Text = text, BackColor = backColor, ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Size = new Size(100, 35), Font = new Font("Segoe UI", 9, FontStyle.Bold) };
        private ComboBox CreateComboBox() => new ComboBox { BackColor = Color.FromArgb(70, 70, 70), ForeColor = Color.White, FlatStyle = FlatStyle.Flat, DropDownStyle = ComboBoxStyle.DropDownList, Font = new Font("Segoe UI", 9), Size = new Size(80, 25) };
        private DataGridView CreateDataGridView() => new DataGridView { BackgroundColor = Color.FromArgb(60, 60, 60), ForeColor = Color.White, BorderStyle = BorderStyle.None, AllowUserToAddRows = false, AllowUserToDeleteRows = false, ReadOnly = true, SelectionMode = DataGridViewSelectionMode.FullRowSelect, MultiSelect = false, AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill, ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle { BackColor = Color.FromArgb(124, 77, 255), ForeColor = Color.White, Font = new Font("Segoe UI", 10.5F, FontStyle.Bold) }, DefaultCellStyle = new DataGridViewCellStyle { BackColor = Color.FromArgb(60, 60, 60), ForeColor = Color.White, SelectionBackColor = Color.FromArgb(124, 77, 255) } };
        private Label CreateLabel(string text) => new Label { Text = text, ForeColor = Color.White, AutoSize = true, Font = new Font("Segoe UI", 9) };
        #endregion

        #endregion
    }
}
