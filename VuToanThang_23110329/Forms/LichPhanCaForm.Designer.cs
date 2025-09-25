using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace VuToanThang_23110329.Forms
{
    partial class LichPhanCaForm
    {
        private System.ComponentModel.IContainer components = null;

        // UI Controls
        private DataGridView dgvLichPhanCa;
        private DateTimePicker dtpTuNgay, dtpDenNgay, dtpNgayLam;
        private ComboBox cmbNhanVien, cmbCaLam, cmbTrangThai;
        private Button btnThem, btnSua, btnXoa, btnLuu, btnHuy, btnLamMoi, btnTimKiem, btnTaoLichTuan, btnXoaTimKiem;
        private Panel pnlThongTin, pnlFilter;
        private TextBox txtTimKiem;
        private Label lblTitle;

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
            this.Text = "Lịch phân ca";
            this.Padding = new Padding(20);
            this.MinimumSize = new Size(900, 650);

            CreateControls();
            this.Resize += (sender, e) => PerformLayoutLayout();
            this.Load += (sender, e) => PerformLayoutLayout(); // Initial layout
        }

        private void CreateControls()
        {
            lblTitle = new Label
            {
                Text = "LỊCH PHÂN CA",
                Font = new Font("Segoe UI", 20, FontStyle.Bold),
                ForeColor = Color.FromArgb(124, 77, 255),
                AutoSize = true
            };

            pnlFilter = new Panel { BackColor = Color.FromArgb(60, 60, 60), BorderStyle = BorderStyle.FixedSingle };
            dtpTuNgay = CreateDatePicker(DateTime.Now.AddDays(-7));
            dtpDenNgay = CreateDatePicker(DateTime.Now.AddDays(7));
            txtTimKiem = CreateTextBox("Nhập ID hoặc tên nhân viên...");
            btnTimKiem = CreateButton("Tìm kiếm", Color.FromArgb(33, 150, 243));
            btnXoaTimKiem = CreateButton("Xóa", Color.FromArgb(158, 158, 158));
            btnTaoLichTuan = CreateButton("Tạo lịch tuần", Color.FromArgb(76, 175, 80));

            pnlFilter.Controls.AddRange(new Control[] { CreateLabel("Từ ngày:"), dtpTuNgay, CreateLabel("Đến ngày:"), dtpDenNgay, CreateLabel("Tìm kiếm NV:"), txtTimKiem, btnTimKiem, btnXoaTimKiem, btnTaoLichTuan });

            btnThem = CreateButton("Thêm", Color.FromArgb(46, 125, 50));
            btnSua = CreateButton("Sửa", Color.FromArgb(255, 152, 0));
            btnXoa = CreateButton("Xóa", Color.FromArgb(244, 67, 54));
            btnLuu = CreateButton("Lưu", Color.FromArgb(33, 150, 243));
            btnHuy = CreateButton("Hủy", Color.FromArgb(158, 158, 158));
            btnLamMoi = CreateButton("Làm mới", Color.FromArgb(96, 125, 139));

            dgvLichPhanCa = CreateDataGridView();
            pnlThongTin = new Panel { BackColor = Color.FromArgb(60, 60, 60), BorderStyle = BorderStyle.FixedSingle, Padding = new Padding(15) };
            CreateInfoControls();

            this.Controls.AddRange(new Control[] { lblTitle, pnlFilter, btnThem, btnSua, btnXoa, btnLuu, btnHuy, btnLamMoi, dgvLichPhanCa, pnlThongTin });

            // Assign event handlers
            AssignEventHandlers();
        }

        private void CreateInfoControls()
        {
            var lblThongTin = new Label { Text = "THÔNG TIN LỊCH PHÂN CA", Font = new Font("Segoe UI", 14, FontStyle.Bold), ForeColor = Color.FromArgb(124, 77, 255), AutoSize = true };
            cmbNhanVien = CreateComboBox();
            cmbCaLam = CreateComboBox();
            dtpNgayLam = CreateDatePicker(DateTime.Now);
            cmbTrangThai = CreateComboBox();
            cmbTrangThai.Items.AddRange(new[] { "DuKien", "Khoa", "Huy" });

            pnlThongTin.Controls.AddRange(new Control[] { lblThongTin, CreateLabel("Nhân viên:"), cmbNhanVien, CreateLabel("Ca làm:"), cmbCaLam, CreateLabel("Ngày làm:"), dtpNgayLam, CreateLabel("Trạng thái:"), cmbTrangThai });
        }

        private void AssignEventHandlers()
        {
            txtTimKiem.Enter += txtTimKiem_Enter;
            txtTimKiem.Leave += txtTimKiem_Leave;
            btnTimKiem.Click += btnTimKiem_Click;
            btnXoaTimKiem.Click += btnXoaTimKiem_Click;
            btnTaoLichTuan.Click += btnTaoLichTuan_Click;
            txtTimKiem.KeyDown += txtTimKiem_KeyDown;
            btnThem.Click += btnThem_Click;
            btnSua.Click += btnSua_Click;
            btnXoa.Click += btnXoa_Click;
            btnLuu.Click += btnLuu_Click;
            btnHuy.Click += btnHuy_Click;
            btnLamMoi.Click += btnLamMoi_Click;
            dgvLichPhanCa.SelectionChanged += dgvLichPhanCa_SelectionChanged;
        }

        // Layout Logic
        private void PerformLayoutLayout()
        {
            int formWidth = this.ClientSize.Width;
            int formHeight = this.ClientSize.Height;

            lblTitle.Location = new Point(20, 20);

            pnlFilter.Location = new Point(20, 60);
            pnlFilter.Size = new Size(formWidth - 40, 80);
            LayoutFilterPanel();

            LayoutActionButtons();
            LayoutMainContent(formWidth, formHeight);
        }

        private void LayoutFilterPanel()
        {
            int x = 10, y = 20;
            Control[] controls = pnlFilter.Controls.OfType<Control>().OrderBy(c => c.TabIndex).ToArray();
            foreach (Control control in controls)
            {
                if (control is Label) { control.Location = new Point(x, y + 5); x += control.Width + 5; }
                else if (control is DateTimePicker) { control.Location = new Point(x, y); control.Size = new Size(120, 25); x += 130; }
                else if (control is TextBox) { control.Location = new Point(x, y); control.Size = new Size(200, 25); x += 210; }
                else if (control is Button) { control.Location = new Point(x, y); control.Size = (control.Text == "Xóa") ? new Size(60, 30) : new Size(100, 30); x += control.Width + 10; }
            }
        }

        private void LayoutActionButtons()
        {
            var buttons = this.Controls.OfType<Button>().Where(b => b != btnTimKiem && b != btnTaoLichTuan && b != btnXoaTimKiem).ToList();
            int x = 20, y = 150;
            foreach (var button in buttons) { button.Location = new Point(x, y); button.Size = new Size(80, 30); x += 90; }
        }

        private void LayoutMainContent(int formWidth, int formHeight)
        {
            int contentY = 190, availableWidth = formWidth - 40, availableHeight = formHeight - contentY - 20;
            if (formWidth < 700)
            {
                dgvLichPhanCa.Location = new Point(20, contentY);
                dgvLichPhanCa.Size = new Size(availableWidth, (int)(availableHeight * 0.6));
                pnlThongTin.Location = new Point(20, dgvLichPhanCa.Bottom + 10);
                pnlThongTin.Size = new Size(availableWidth, (int)(availableHeight * 0.35));
            }
            else
            {
                int dgvWidth = (formWidth < 1000) ? (int)(availableWidth * 0.65) : (int)(availableWidth * 0.70);
                dgvLichPhanCa.Location = new Point(20, contentY);
                dgvLichPhanCa.Size = new Size(dgvWidth, availableHeight);
                pnlThongTin.Location = new Point(dgvLichPhanCa.Right + 20, contentY);
                pnlThongTin.Size = new Size(availableWidth - dgvWidth - 20, availableHeight);
            }
            LayoutInfoPanelControls();
        }

        private void LayoutInfoPanelControls()
        {
            int y = 20, spacing = 35, labelWidth = 80, controlWidth = Math.Max(150, pnlThongTin.Width - labelWidth - 30);
            Control[] controls = pnlThongTin.Controls.OfType<Control>().OrderBy(c => c.TabIndex).ToArray();
            if (controls.Length > 0) { controls[0].Location = new Point(10, y); controls[0].Size = new Size(pnlThongTin.Width - 20, 25); y += 40; }
            for (int i = 1; i < controls.Length; i += 2)
            {
                if (i + 1 < controls.Length)
                {
                    controls[i].Location = new Point(10, y); controls[i].Size = new Size(labelWidth, 20);
                    controls[i + 1].Location = new Point(labelWidth + 10, y - 3); controls[i + 1].Size = new Size(controlWidth, 25);
                    y += spacing;
                }
            }
        }

        #region Control Factory Methods
        private Button CreateButton(string text, Color backColor) => new Button { Text = text, BackColor = backColor, ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Size = new Size(100, 35), Font = new Font("Segoe UI", 9, FontStyle.Bold) };
        private TextBox CreateTextBox(string placeholder) => new TextBox { BackColor = Color.FromArgb(70, 70, 70), ForeColor = Color.Gray, BorderStyle = BorderStyle.FixedSingle, Font = new Font("Segoe UI", 9), Text = placeholder, Width = 200 };
        private ComboBox CreateComboBox() => new ComboBox { BackColor = Color.FromArgb(70, 70, 70), ForeColor = Color.White, FlatStyle = FlatStyle.Flat, DropDownStyle = ComboBoxStyle.DropDownList, Font = new Font("Segoe UI", 9) };
        private DateTimePicker CreateDatePicker(DateTime value) => new DateTimePicker { Format = DateTimePickerFormat.Short, Font = new Font("Segoe UI", 9), Value = value };
        private Label CreateLabel(string text) => new Label { Text = text, ForeColor = Color.White, AutoSize = true, Font = new Font("Segoe UI", 9) };
        private DataGridView CreateDataGridView() => new DataGridView { BackgroundColor = Color.FromArgb(60, 60, 60), ForeColor = Color.White, BorderStyle = BorderStyle.None, AllowUserToAddRows = false, AllowUserToDeleteRows = false, ReadOnly = true, SelectionMode = DataGridViewSelectionMode.FullRowSelect, MultiSelect = false, AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill, ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle { BackColor = Color.FromArgb(124, 77, 255), ForeColor = Color.White, Font = new Font("Segoe UI", 10.5F, FontStyle.Bold) }, DefaultCellStyle = new DataGridViewCellStyle { BackColor = Color.FromArgb(60, 60, 60), ForeColor = Color.White, SelectionBackColor = Color.FromArgb(124, 77, 255) } };
        #endregion

        #endregion
    }
}
