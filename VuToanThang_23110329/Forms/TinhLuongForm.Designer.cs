using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace VuToanThang_23110329.Forms
{
    partial class TinhLuongForm
    {
        private System.ComponentModel.IContainer components = null;

        // UI Controls
        private ComboBox cmbThang, cmbNam;
        private NumericUpDown nudGioChuan, nudHeSoOT;
        private DataGridView dgvCongThang, dgvBangLuong;
        private Button btnXemCong, btnChayLuong, btnDongLuong, btnCapNhatPhuCap;
        private Panel pnlThongSo;
        private TabControl tabControl;
        private Label lblTongNhanVien, lblTongLuong, lblTrangThai, lblTitle;

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
            this.Size = new Size(1000, 700);
            this.Text = "Tính lương";
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
            lblTitle = new Label { Text = "TÍNH LƯƠNG NHÂN VIÊN", Font = new Font("Segoe UI", 20, FontStyle.Bold), ForeColor = Color.FromArgb(124, 77, 255), AutoSize = true };

            pnlThongSo = new Panel { BackColor = Color.FromArgb(60, 60, 60), BorderStyle = BorderStyle.FixedSingle };
            cmbThang = CreateComboBox();
            for (int i = 1; i <= 12; i++) cmbThang.Items.Add(i);
            cmbThang.SelectedItem = DateTime.Now.Month;
            cmbNam = CreateComboBox();
            for (int i = DateTime.Now.Year - 2; i <= DateTime.Now.Year + 1; i++) cmbNam.Items.Add(i);
            cmbNam.SelectedItem = DateTime.Now.Year;
            nudGioChuan = new NumericUpDown { Minimum = 100, Maximum = 300, Value = 208, DecimalPlaces = 1, BackColor = Color.FromArgb(70, 70, 70), ForeColor = Color.White, Font = new Font("Segoe UI", 9) };
            nudHeSoOT = new NumericUpDown { Minimum = 1, Maximum = 3, Value = 1.5m, DecimalPlaces = 2, Increment = 0.1m, BackColor = Color.FromArgb(70, 70, 70), ForeColor = Color.White, Font = new Font("Segoe UI", 9) };
            btnXemCong = CreateButton("Xem công tháng", Color.FromArgb(33, 150, 243));
            btnChayLuong = CreateButton("Chạy bảng lương", Color.FromArgb(46, 125, 50));
            btnDongLuong = CreateButton("Đóng bảng lương", Color.FromArgb(244, 67, 54));
            pnlThongSo.Controls.AddRange(new Control[] { CreateLabel("Tháng:"), cmbThang, CreateLabel("Năm:"), cmbNam, CreateLabel("Giờ chuẩn/tháng:"), nudGioChuan, CreateLabel("Hệ số OT:"), nudHeSoOT, btnXemCong, btnChayLuong, btnDongLuong });

            tabControl = new TabControl { BackColor = Color.FromArgb(60, 60, 60), ForeColor = Color.White, Font = new Font("Segoe UI", 10, FontStyle.Bold) };
            var tabCongThang = new TabPage("Tổng hợp công tháng") { BackColor = Color.FromArgb(50, 50, 50) };
            var tabBangLuong = new TabPage("Bảng lương") { BackColor = Color.FromArgb(50, 50, 50) };
            CreateTab1Controls(tabCongThang);
            CreateTab2Controls(tabBangLuong);
            tabControl.TabPages.AddRange(new TabPage[] { tabCongThang, tabBangLuong });

            this.Controls.AddRange(new Control[] { lblTitle, pnlThongSo, tabControl });
        }

        private void CreateTab1Controls(TabPage tab) { /* Omitted for brevity */ }
        private void CreateTab2Controls(TabPage tab) { /* Omitted for brevity */ }

        private void SetupEventHandlers()
        {
            btnXemCong.Click += btnXemCong_Click;
            btnChayLuong.Click += btnChayLuong_Click;
            btnDongLuong.Click += btnDongLuong_Click;
            btnCapNhatPhuCap.Click += btnCapNhatPhuCap_Click;
            cmbThang.SelectedIndexChanged += (s, e) => LoadPayrollData();
            cmbNam.SelectedIndexChanged += (s, e) => LoadPayrollData();
            dgvBangLuong.CellDoubleClick += dgvBangLuong_CellDoubleClick;
        }

        #region Layout Logic
        private void PerformLayoutLayout() { /* Omitted for brevity */ }
        #endregion

        #region Control Factory Methods
        private Label CreateStatLabel(string text, Color color) => new Label { Text = text, ForeColor = color, Font = new Font("Segoe UI", 12, FontStyle.Bold), AutoSize = true };
        private Button CreateButton(string text, Color backColor) => new Button { Text = text, BackColor = backColor, ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Size = new Size(120, 35), Font = new Font("Segoe UI", 9, FontStyle.Bold) };
        private ComboBox CreateComboBox() => new ComboBox { BackColor = Color.FromArgb(70, 70, 70), ForeColor = Color.White, FlatStyle = FlatStyle.Flat, DropDownStyle = ComboBoxStyle.DropDownList, Font = new Font("Segoe UI", 9), Size = new Size(80, 25) };
        private DataGridView CreateDataGridView() => new DataGridView { BackgroundColor = Color.FromArgb(60, 60, 60), ForeColor = Color.White, BorderStyle = BorderStyle.None, AllowUserToAddRows = false, AllowUserToDeleteRows = false, ReadOnly = true, SelectionMode = DataGridViewSelectionMode.FullRowSelect, MultiSelect = false, AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill, ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle { BackColor = Color.FromArgb(124, 77, 255), ForeColor = Color.White, Font = new Font("Segoe UI", 10.5F, FontStyle.Bold) }, DefaultCellStyle = new DataGridViewCellStyle { BackColor = Color.FromArgb(60, 60, 60), ForeColor = Color.White, SelectionBackColor = Color.FromArgb(124, 77, 255) } };
        private Label CreateLabel(string text) => new Label { Text = text, ForeColor = Color.White, AutoSize = true, Font = new Font("Segoe UI", 9) };
        #endregion

        #endregion
    }
}
