using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace VuToanThang_23110329.Forms
{
    partial class DonTuNVForm
    {
        private System.ComponentModel.IContainer components = null;

        // UI Controls
        private DataGridView dgvDonTu;
        private ComboBox cmbLoai, cmbTrangThai;
        private DateTimePicker dtpTuLuc, dtpDenLuc;
        private TextBox txtLyDo, txtSoGio;
        private Button btnThem, btnSua, btnXoa, btnLuu, btnHuy, btnLamMoi;
        private Panel pnlThongTin;
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
            this.SuspendLayout();
            
            // Form properties
            this.BackColor = Color.FromArgb(50, 50, 50);
            this.Size = new Size(800, 550);
            this.Text = "Quản lý đơn từ - Nhân viên";
            this.Padding = new Padding(20);
            this.MinimumSize = new Size(800, 550);
            this.AutoScaleMode = AutoScaleMode.Dpi;
            this.StartPosition = FormStartPosition.CenterParent;
            
            // Add preview controls for Designer
            var lblPreview = new Label
            {
                Text = "QUẢN LÝ ĐỚN TỪ - NHÂN VIÊN\n(Designer Preview)",
                Font = new Font("Segoe UI", 18, FontStyle.Bold),
                ForeColor = Color.FromArgb(124, 77, 255),
                Location = new Point(50, 50),
                AutoSize = true
            };
            
            var pnlActions = new Panel
            {
                BackColor = Color.FromArgb(60, 60, 60),
                BorderStyle = BorderStyle.FixedSingle,
                Size = new Size(700, 80),
                Location = new Point(50, 120)
            };
            
            var lblActions = new Label
            {
                Text = "📝 Buttons: Tạo đơn nghỉ | Tạo đơn OT | Xem chi tiết | Hủy đơn",
                ForeColor = Color.White,
                Location = new Point(20, 30),
                AutoSize = true,
                Font = new Font("Segoe UI", 10)
            };
            pnlActions.Controls.Add(lblActions);
            
            var pnlContent = new Panel
            {
                BackColor = Color.FromArgb(60, 60, 60),
                BorderStyle = BorderStyle.FixedSingle,
                Size = new Size(700, 280),
                Location = new Point(50, 220)
            };
            
            var lblContent = new Label
            {
                Text = "📋 DANH SÁCH ĐƠN TỪ CỦA TÔI\n\nDataGridView: Đơn từ đã tạo\nColumns: Loại đơn | Từ lúc | Đến lúc | Số giờ | Lý do | Trạng thái\n\nInfo Panel: Chi tiết đơn từ được chọn\nTrạng thái: Chờ duyệt | Đã duyệt | Từ chối",
                ForeColor = Color.White,
                Location = new Point(20, 50),
                Size = new Size(660, 180),
                Font = new Font("Segoe UI", 11)
            };
            pnlContent.Controls.Add(lblContent);
            
            this.Controls.AddRange(new Control[] { lblPreview, pnlActions, pnlContent });
            
            this.ResumeLayout(false);
            this.PerformLayout();
            
            this.Load += (s, e) => PerformLayoutLayout();
            this.Resize += (s, e) => PerformLayoutLayout();
        }

        private void CreateControls()
        {
            lblTitle = new Label { Text = "ĐƠN TỪ CỦA TÔI", Font = new Font("Segoe UI", 20, FontStyle.Bold), ForeColor = Color.FromArgb(124, 77, 255), AutoSize = true };

            cmbTrangThai = CreateComboBox(new[] { "Tất cả", "ChoDuyet", "DaDuyet", "TuChoi" });

            btnThem = CreateButton("Tạo đơn mới", Color.FromArgb(46, 125, 50));
            btnSua = CreateButton("Sửa", Color.FromArgb(255, 152, 0));
            btnXoa = CreateButton("Xóa", Color.FromArgb(244, 67, 54));
            btnLuu = CreateButton("Lưu", Color.FromArgb(33, 150, 243), false);
            btnHuy = CreateButton("Hủy", Color.FromArgb(158, 158, 158), false);
            btnLamMoi = CreateButton("Làm mới", Color.FromArgb(96, 125, 139));

            dgvDonTu = CreateDataGridView();
            pnlThongTin = new Panel { BackColor = Color.FromArgb(60, 60, 60), BorderStyle = BorderStyle.FixedSingle, Padding = new Padding(15) };
            CreateInfoControls();

            this.Controls.AddRange(new Control[] { lblTitle, CreateLabel("Lọc theo trạng thái:"), cmbTrangThai, btnThem, btnSua, btnXoa, btnLuu, btnHuy, btnLamMoi, dgvDonTu, pnlThongTin });
        }

        private void CreateInfoControls()
        {
            var lblThongTin = new Label { Text = "THÔNG TIN ĐƠN TỪ", Font = new Font("Segoe UI", 14, FontStyle.Bold), ForeColor = Color.FromArgb(124, 77, 255), AutoSize = true };
            cmbLoai = CreateComboBox(new[] { "NGHI", "OT" });
            dtpTuLuc = CreateDateTimePicker();
            dtpDenLuc = CreateDateTimePicker();
            txtSoGio = new TextBox { BackColor = Color.FromArgb(70, 70, 70), ForeColor = Color.White, BorderStyle = BorderStyle.FixedSingle, Font = new Font("Segoe UI", 9), Text = "8" };
            txtLyDo = new TextBox { BackColor = Color.FromArgb(70, 70, 70), ForeColor = Color.White, BorderStyle = BorderStyle.FixedSingle, Font = new Font("Segoe UI", 9), Multiline = true, Height = 80 };

            pnlThongTin.Controls.AddRange(new Control[] { lblThongTin, CreateLabel("Loại đơn:"), cmbLoai, CreateLabel("Từ lúc:"), dtpTuLuc, CreateLabel("Đến lúc:"), dtpDenLuc, CreateLabel("Số giờ:"), txtSoGio, CreateLabel("Lý do:"), txtLyDo });
        }

        private void SetupEventHandlers()
        {
            dgvDonTu.SelectionChanged += dgvDonTu_SelectionChanged;
            cmbTrangThai.SelectedIndexChanged += cmbTrangThai_SelectedIndexChanged;
            cmbLoai.SelectedIndexChanged += cmbLoai_SelectedIndexChanged;
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
        private Button CreateButton(string text, Color backColor, bool enabled = true) => new Button { Text = text, BackColor = backColor, ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Size = new Size(100, 35), Font = new Font("Segoe UI", 9, FontStyle.Bold), Enabled = enabled };
        private ComboBox CreateComboBox(string[] items) { var cmb = new ComboBox { BackColor = Color.FromArgb(70, 70, 70), ForeColor = Color.White, FlatStyle = FlatStyle.Flat, DropDownStyle = ComboBoxStyle.DropDownList, Font = new Font("Segoe UI", 9) }; cmb.Items.AddRange(items); cmb.SelectedIndex = 0; return cmb; }
        private DateTimePicker CreateDateTimePicker() => new DateTimePicker { Format = DateTimePickerFormat.Custom, CustomFormat = "dd/MM/yyyy HH:mm", Font = new Font("Segoe UI", 9) };
        private DataGridView CreateDataGridView() => new DataGridView { BackgroundColor = Color.FromArgb(60, 60, 60), ForeColor = Color.White, BorderStyle = BorderStyle.None, AllowUserToAddRows = false, AllowUserToDeleteRows = false, ReadOnly = true, SelectionMode = DataGridViewSelectionMode.FullRowSelect, MultiSelect = false, AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill, ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle { BackColor = Color.FromArgb(124, 77, 255), ForeColor = Color.White, Font = new Font("Segoe UI", 10.5F, FontStyle.Bold) }, DefaultCellStyle = new DataGridViewCellStyle { BackColor = Color.FromArgb(60, 60, 60), ForeColor = Color.White, SelectionBackColor = Color.FromArgb(124, 77, 255) } };
        private Label CreateLabel(string text) => new Label { Text = text, ForeColor = Color.White, AutoSize = true, Font = new Font("Segoe UI", 9) };
        #endregion

        #endregion
    }
}
