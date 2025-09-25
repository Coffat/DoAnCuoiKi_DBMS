using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace VuToanThang_23110329.Forms
{
    partial class DonTuSMForm
    {
        private System.ComponentModel.IContainer components = null;

        // UI Controls
        private DataGridView dgvDonTu;
        private ComboBox cmbTrangThai, cmbLoai;
        private DateTimePicker dtpTuNgay, dtpDenNgay;
        private TextBox txtLyDo, txtGhiChuDuyet;
        private Button btnTimKiem, btnDuyet, btnTuChoi, btnLamMoi, btnXemChiTiet;
        private Panel pnlFilter, pnlThongTin, pnlStats;
        private Label lblTongSo, lblChoDuyet, lblDaDuyet, lblTuChoi, lblTitle;

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
            this.SuspendLayout();
            
            // Form properties
            this.BackColor = Color.FromArgb(50, 50, 50);
            this.Size = new Size(800, 550);
            this.Text = "Quản lý đơn từ - Quản lý";
            this.Padding = new Padding(20);
            this.MinimumSize = new Size(800, 550);
            this.AutoScaleMode = AutoScaleMode.Dpi;
            this.StartPosition = FormStartPosition.CenterParent;
            
            // Add preview controls for Designer
            var lblPreview = new Label
            {
                Text = "QUẢN LÝ ĐỚN TỪ - QUẢN LÝ\n(Designer Preview)",
                Font = new Font("Segoe UI", 18, FontStyle.Bold),
                ForeColor = Color.FromArgb(124, 77, 255),
                Location = new Point(50, 50),
                AutoSize = true
            };
            
            var pnlStats = new Panel
            {
                BackColor = Color.FromArgb(60, 60, 60),
                BorderStyle = BorderStyle.FixedSingle,
                Size = new Size(700, 80),
                Location = new Point(50, 120)
            };
            
            var lblStats = new Label
            {
                Text = "📊 Thống kê: Chờ duyệt | Đã duyệt | Từ chối | Tổng đơn (Phòng ban của tôi)",
                ForeColor = Color.White,
                Location = new Point(20, 30),
                AutoSize = true,
                Font = new Font("Segoe UI", 10)
            };
            pnlStats.Controls.Add(lblStats);
            
            var pnlFilter = new Panel
            {
                BackColor = Color.FromArgb(60, 60, 60),
                BorderStyle = BorderStyle.FixedSingle,
                Size = new Size(700, 100),
                Location = new Point(50, 220)
            };
            
            var lblFilter = new Label
            {
                Text = "🔍 Bộ lọc: Loại đơn | Trạng thái | Từ ngày | Đến ngày | Nhân viên\nButtons: Duyệt | Từ chối | Xem chi tiết",
                ForeColor = Color.White,
                Location = new Point(20, 30),
                Size = new Size(660, 40),
                Font = new Font("Segoe UI", 10)
            };
            pnlFilter.Controls.Add(lblFilter);
            
            var pnlContent = new Panel
            {
                BackColor = Color.FromArgb(60, 60, 60),
                BorderStyle = BorderStyle.FixedSingle,
                Size = new Size(700, 150),
                Location = new Point(50, 340)
            };
            
            var lblContent = new Label
            {
                Text = "📋 DataGridView: Đơn từ nhân viên trong phòng ban\nInfo Panel: Chi tiết đơn từ và quyết định duyệt",
                ForeColor = Color.White,
                Location = new Point(20, 60),
                AutoSize = true,
                Font = new Font("Segoe UI", 12)
            };
            pnlContent.Controls.Add(lblContent);
            
            this.Controls.AddRange(new Control[] { lblPreview, pnlStats, pnlFilter, pnlContent });
            
            this.ResumeLayout(false);
            this.PerformLayout();
            
            this.Load += (s, e) => PerformLayoutLayout();
            this.Resize += (s, e) => PerformLayoutLayout();
        }

        private void CreateControls()
        {
            lblTitle = new Label { Text = "DUYỆT ĐƠN TỪ - QUẢN LÝ", Font = new Font("Segoe UI", 20, FontStyle.Bold), ForeColor = Color.FromArgb(124, 77, 255), AutoSize = true };

            pnlStats = new Panel { BackColor = Color.FromArgb(60, 60, 60), BorderStyle = BorderStyle.FixedSingle };
            lblTongSo = CreateStatLabel("Tổng số: 0", Color.White);
            lblChoDuyet = CreateStatLabel("Chờ duyệt: 0", Color.Orange);
            lblDaDuyet = CreateStatLabel("Đã duyệt: 0", Color.LightGreen);
            lblTuChoi = CreateStatLabel("Từ chối: 0", Color.LightCoral);
            pnlStats.Controls.AddRange(new Control[] { lblTongSo, lblChoDuyet, lblDaDuyet, lblTuChoi });

            pnlFilter = new Panel { BackColor = Color.FromArgb(60, 60, 60), BorderStyle = BorderStyle.FixedSingle };
            dtpTuNgay = CreateDatePicker(DateTime.Now.AddDays(-30));
            dtpDenNgay = CreateDatePicker(DateTime.Now);
            cmbTrangThai = CreateComboBox(new[] { "Tất cả", "ChoDuyet", "DaDuyet", "TuChoi" }, 1);
            cmbLoai = CreateComboBox(new[] { "Tất cả", "NGHI", "OT" }, 0);
            btnTimKiem = CreateButton("Tìm kiếm", Color.FromArgb(33, 150, 243));
            btnLamMoi = CreateButton("Làm mới", Color.FromArgb(96, 125, 139));
            pnlFilter.Controls.AddRange(new Control[] { CreateLabel("Từ ngày:"), dtpTuNgay, CreateLabel("Đến ngày:"), dtpDenNgay, CreateLabel("Trạng thái:"), cmbTrangThai, CreateLabel("Loại:"), cmbLoai, btnTimKiem, btnLamMoi });

            btnDuyet = CreateButton("Duyệt", Color.FromArgb(46, 125, 50), false);
            btnTuChoi = CreateButton("Từ chối", Color.FromArgb(244, 67, 54), false);
            btnXemChiTiet = CreateButton("Xem chi tiết", Color.FromArgb(156, 39, 176), false);

            dgvDonTu = CreateDataGridView();
            pnlThongTin = new Panel { BackColor = Color.FromArgb(60, 60, 60), BorderStyle = BorderStyle.FixedSingle, Padding = new Padding(15) };
            CreateInfoControls();

            this.Controls.AddRange(new Control[] { lblTitle, pnlStats, pnlFilter, btnDuyet, btnTuChoi, btnXemChiTiet, dgvDonTu, pnlThongTin });
        }

        private void CreateInfoControls()
        {
            var lblThongTin = new Label { Text = "CHI TIẾT ĐƠN TỪ", Font = new Font("Segoe UI", 14, FontStyle.Bold), ForeColor = Color.FromArgb(124, 77, 255), AutoSize = true };
            txtLyDo = CreateReadOnlyTextBox(true, 60);
            txtGhiChuDuyet = CreateTextBox(true, 60);

            pnlThongTin.Controls.AddRange(new Control[] {
                lblThongTin,
                CreateLabel("Nhân viên:"), CreateReadOnlyTextBox(),
                CreateLabel("Loại đơn:"), CreateReadOnlyTextBox(),
                CreateLabel("Thời gian:"), CreateReadOnlyTextBox(),
                CreateLabel("Số giờ:"), CreateReadOnlyTextBox(),
                CreateLabel("Lý do:"), txtLyDo,
                CreateLabel("Trạng thái:"), CreateReadOnlyTextBox(),
                CreateLabel("Ghi chú duyệt:"), txtGhiChuDuyet
            });
        }

        private void SetupEventHandlers()
        {
            dgvDonTu.SelectionChanged += dgvDonTu_SelectionChanged;
            btnTimKiem.Click += btnTimKiem_Click;
            btnLamMoi.Click += btnLamMoi_Click;
            btnDuyet.Click += btnDuyet_Click;
            btnTuChoi.Click += btnTuChoi_Click;
            btnXemChiTiet.Click += btnXemChiTiet_Click;
            cmbTrangThai.SelectedIndexChanged += (s, e) => LoadData();
            cmbLoai.SelectedIndexChanged += (s, e) => LoadData();
        }

        #region Layout Logic
        private void PerformLayoutLayout()
        {
            int formWidth = this.ClientSize.Width;
            lblTitle.Location = new Point(20, 20);

            pnlStats.Location = new Point(20, 70);
            pnlStats.Size = new Size(formWidth - 40, 60);
            if (formWidth < 600) { pnlStats.Height = 80; LayoutStatisticsVertical(); } else { pnlStats.Height = 60; LayoutStatisticsHorizontal(pnlStats.Width); }

            pnlFilter.Location = new Point(20, pnlStats.Bottom + 20);
            pnlFilter.Size = new Size(formWidth - 40, 80);
            if (formWidth < 700) { pnlFilter.Height = 120; LayoutFilterVertical(); } else if (formWidth < 1000) { pnlFilter.Height = 80; LayoutFilterCompact(); } else { pnlFilter.Height = 80; LayoutFilterFull(); }

            int contentY = pnlFilter.Bottom + 20;
            int contentHeight = this.ClientSize.Height - contentY - 20;
            if (formWidth < 800) { LayoutContentVertical(contentY, contentHeight, formWidth - 40); } else { LayoutContentSideBySide(contentY, contentHeight, formWidth - 40); }
        }

        private void LayoutStatisticsVertical() { lblTongSo.Location = new Point(20, 15); lblChoDuyet.Location = new Point(200, 15); lblDaDuyet.Location = new Point(20, 40); lblTuChoi.Location = new Point(200, 40); }
        private void LayoutStatisticsHorizontal(int panelWidth) { int spacing = Math.Max(120, panelWidth / 5); lblTongSo.Location = new Point(20, 20); lblChoDuyet.Location = new Point(20 + spacing, 20); lblDaDuyet.Location = new Point(20 + spacing * 2, 20); lblTuChoi.Location = new Point(20 + spacing * 3, 20); }
        private void LayoutFilterVertical() { /* Implementation omitted for brevity */ }
        private void LayoutFilterCompact() { /* Implementation omitted for brevity */ }
        private void LayoutFilterFull() { /* Implementation omitted for brevity */ }
        private void LayoutContentVertical(int y, int height, int width) { dgvDonTu.Location = new Point(20, y); dgvDonTu.Size = new Size(width, (int)(height * 0.6)); pnlThongTin.Location = new Point(20, dgvDonTu.Bottom + 10); pnlThongTin.Size = new Size(width, (int)(height * 0.35)); LayoutActionButtons(dgvDonTu.Location.Y - 40); }
        private void LayoutContentSideBySide(int y, int height, int width) { dgvDonTu.Location = new Point(20, y); dgvDonTu.Size = new Size((int)(width * 0.65), height); pnlThongTin.Location = new Point(dgvDonTu.Right + 20, y); pnlThongTin.Size = new Size((int)(width * 0.30), height); LayoutActionButtons(y - 40); }
        private void LayoutActionButtons(int y) { btnDuyet.Location = new Point(20, y); btnTuChoi.Location = new Point(130, y); btnXemChiTiet.Location = new Point(240, y); }
        #endregion

        #region Control Factory Methods
        private Label CreateStatLabel(string text, Color color) => new Label { Text = text, ForeColor = color, Font = new Font("Segoe UI", 12, FontStyle.Bold), AutoSize = true };
        private Button CreateButton(string text, Color backColor, bool enabled = true) => new Button { Text = text, BackColor = backColor, ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Size = new Size(100, 35), Font = new Font("Segoe UI", 9, FontStyle.Bold), Enabled = enabled };
        private TextBox CreateTextBox(bool multiline = false, int height = 23) => new TextBox { BackColor = Color.FromArgb(70, 70, 70), ForeColor = Color.White, BorderStyle = BorderStyle.FixedSingle, Font = new Font("Segoe UI", 9), Multiline = multiline, Height = height };
        private TextBox CreateReadOnlyTextBox(bool multiline = false, int height = 23) { var txt = CreateTextBox(multiline, height); txt.ReadOnly = true; txt.BackColor = Color.FromArgb(50, 50, 50); return txt; }
        private ComboBox CreateComboBox(string[] items, int selectedIndex) { var cmb = new ComboBox { BackColor = Color.FromArgb(70, 70, 70), ForeColor = Color.White, FlatStyle = FlatStyle.Flat, DropDownStyle = ComboBoxStyle.DropDownList, Font = new Font("Segoe UI", 9) }; cmb.Items.AddRange(items); cmb.SelectedIndex = selectedIndex; return cmb; }
        private DateTimePicker CreateDatePicker(DateTime value) => new DateTimePicker { Format = DateTimePickerFormat.Short, Font = new Font("Segoe UI", 9), Size = new Size(120, 25), Value = value };
        private Label CreateLabel(string text) => new Label { Text = text, ForeColor = Color.White, AutoSize = true, Font = new Font("Segoe UI", 9) };
        private DataGridView CreateDataGridView() => new DataGridView { BackgroundColor = Color.FromArgb(60, 60, 60), ForeColor = Color.White, BorderStyle = BorderStyle.None, AllowUserToAddRows = false, AllowUserToDeleteRows = false, ReadOnly = true, SelectionMode = DataGridViewSelectionMode.FullRowSelect, MultiSelect = false, AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill, ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle { BackColor = Color.FromArgb(124, 77, 255), ForeColor = Color.White, Font = new Font("Segoe UI", 10.5F, FontStyle.Bold) }, DefaultCellStyle = new DataGridViewCellStyle { BackColor = Color.FromArgb(60, 60, 60), ForeColor = Color.White, SelectionBackColor = Color.FromArgb(124, 77, 255) } };
        #endregion

        #endregion
    }
}
