using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace VuToanThang_23110329.Forms
{
    partial class DonTuHRForm
    {
        private System.ComponentModel.IContainer components = null;

        // UI Controls
        private DataGridView dgvDonTu;
        private ComboBox cmbTrangThai, cmbLoai, cmbPhongBan;
        private DateTimePicker dtpTuNgay, dtpDenNgay;
        private Button btnTimKiem, btnDuyet, btnTuChoi, btnLamMoi, btnXuatBaoCao;
        private Panel pnlFilter, pnlThongKe, pnlThongTin;
        private Label lblTongSo, lblChoDuyet, lblDaDuyet, lblTuChoi, lblTitle;
        private TextBox txtGhiChuDuyet;

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
            this.Size = new Size(800, 550);
            this.Text = "Quản lý đơn từ - HR";
            this.Padding = new Padding(20);
            this.MinimumSize = new Size(800, 550);
            this.AutoScaleMode = AutoScaleMode.Dpi;

            CreateControls();
            SetupEventHandlers();

            this.Load += (s, e) => PerformLayoutLayout();
            this.Resize += (s, e) => PerformLayoutLayout();
        }

        private void CreateControls()
        {
            lblTitle = new Label { Text = "QUẢN LÝ ĐƠN TỪ - NHÂN SỰ", Font = new Font("Segoe UI", 20, FontStyle.Bold), ForeColor = Color.FromArgb(124, 77, 255), AutoSize = true };

            pnlThongKe = new Panel { BackColor = Color.FromArgb(60, 60, 60), BorderStyle = BorderStyle.FixedSingle };
            lblTongSo = CreateStatLabel("Tổng số: 0", Color.White);
            lblChoDuyet = CreateStatLabel("Chờ duyệt: 0", Color.Orange);
            lblDaDuyet = CreateStatLabel("Đã duyệt: 0", Color.LightGreen);
            lblTuChoi = CreateStatLabel("Từ chối: 0", Color.LightCoral);
            pnlThongKe.Controls.AddRange(new Control[] { lblTongSo, lblChoDuyet, lblDaDuyet, lblTuChoi });

            pnlFilter = new Panel { BackColor = Color.FromArgb(60, 60, 60), BorderStyle = BorderStyle.FixedSingle };
            dtpTuNgay = CreateDatePicker(DateTime.Now.AddDays(-30));
            dtpDenNgay = CreateDatePicker(DateTime.Now);
            cmbTrangThai = CreateComboBox(new[] { "Tất cả", "ChoDuyet", "DaDuyet", "TuChoi" });
            cmbLoai = CreateComboBox(new[] { "Tất cả", "NGHI", "OT" });
            cmbPhongBan = CreateComboBox(new[] { "Tất cả", "Bán hàng", "Kho", "Kế toán", "Bảo vệ" });
            btnTimKiem = CreateButton("Tìm kiếm", Color.FromArgb(33, 150, 243));
            btnXuatBaoCao = CreateButton("Xuất báo cáo", Color.FromArgb(156, 39, 176));
            btnLamMoi = CreateButton("Làm mới", Color.FromArgb(96, 125, 139));
            pnlFilter.Controls.AddRange(new Control[] { CreateLabel("Từ ngày:"), dtpTuNgay, CreateLabel("Đến ngày:"), dtpDenNgay, CreateLabel("Trạng thái:"), cmbTrangThai, CreateLabel("Loại:"), cmbLoai, CreateLabel("Phòng ban:"), cmbPhongBan, btnTimKiem, btnXuatBaoCao, btnLamMoi });

            btnDuyet = CreateButton("Duyệt hàng loạt", Color.FromArgb(46, 125, 50));
            btnTuChoi = CreateButton("Từ chối hàng loạt", Color.FromArgb(244, 67, 54));

            dgvDonTu = CreateDataGridView();
            pnlThongTin = new Panel { BackColor = Color.FromArgb(60, 60, 60), BorderStyle = BorderStyle.FixedSingle, Padding = new Padding(15) };
            txtGhiChuDuyet = new TextBox { Multiline = true, Height = 80, BackColor = Color.FromArgb(70, 70, 70), ForeColor = Color.White, BorderStyle = BorderStyle.FixedSingle, Font = new Font("Segoe UI", 9) };
            pnlThongTin.Controls.AddRange(new Control[] { CreateLabel("Ghi chú duyệt:"), txtGhiChuDuyet });

            this.Controls.AddRange(new Control[] { lblTitle, pnlThongKe, pnlFilter, btnDuyet, btnTuChoi, dgvDonTu, pnlThongTin });
        }

        private void SetupEventHandlers()
        {
            btnTimKiem.Click += btnTimKiem_Click;
            btnDuyet.Click += btnDuyet_Click;
            btnTuChoi.Click += btnTuChoi_Click;
            btnXuatBaoCao.Click += btnXuatBaoCao_Click;
            btnLamMoi.Click += btnLamMoi_Click;
            cmbTrangThai.SelectedIndexChanged += (s, e) => LoadData();
            cmbLoai.SelectedIndexChanged += (s, e) => LoadData();
            cmbPhongBan.SelectedIndexChanged += (s, e) => LoadData();
        }

        #region Layout Logic
        private void PerformLayoutLayout() { /* Omitted for brevity */ }
        #endregion

        #region Control Factory Methods
        private Label CreateStatLabel(string text, Color color) => new Label { Text = text, ForeColor = color, Font = new Font("Segoe UI", 12, FontStyle.Bold), AutoSize = true };
        private Button CreateButton(string text, Color backColor) => new Button { Text = text, BackColor = backColor, ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Size = new Size(120, 35), Font = new Font("Segoe UI", 9, FontStyle.Bold) };
        private ComboBox CreateComboBox(string[] items) { var cmb = new ComboBox { BackColor = Color.FromArgb(70, 70, 70), ForeColor = Color.White, FlatStyle = FlatStyle.Flat, DropDownStyle = ComboBoxStyle.DropDownList, Font = new Font("Segoe UI", 9), Size = new Size(120, 25) }; cmb.Items.AddRange(items); cmb.SelectedIndex = 0; return cmb; }
        private DateTimePicker CreateDatePicker(DateTime value) => new DateTimePicker { Format = DateTimePickerFormat.Short, Font = new Font("Segoe UI", 9), Size = new Size(120, 25), Value = value };
        private DataGridView CreateDataGridView() => new DataGridView { BackgroundColor = Color.FromArgb(60, 60, 60), ForeColor = Color.White, BorderStyle = BorderStyle.None, AllowUserToAddRows = false, AllowUserToDeleteRows = false, ReadOnly = true, SelectionMode = DataGridViewSelectionMode.FullRowSelect, MultiSelect = true, AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill, ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle { BackColor = Color.FromArgb(124, 77, 255), ForeColor = Color.White, Font = new Font("Segoe UI", 10.5F, FontStyle.Bold) }, DefaultCellStyle = new DataGridViewCellStyle { BackColor = Color.FromArgb(60, 60, 60), ForeColor = Color.White, SelectionBackColor = Color.FromArgb(124, 77, 255) } };
        private Label CreateLabel(string text) => new Label { Text = text, ForeColor = Color.White, AutoSize = true, Font = new Font("Segoe UI", 9) };
        #endregion

        #endregion
    }
}
