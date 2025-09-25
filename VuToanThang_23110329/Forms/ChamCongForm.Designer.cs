using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace VuToanThang_23110329.Forms
{
    partial class ChamCongForm
    {
        private System.ComponentModel.IContainer components = null;

        // UI Controls
        private DataGridView dgvChamCong, dgvLichChamCong;
        private DateTimePicker dtpTuNgay, dtpDenNgay, dtpGioVao, dtpGioRa;
        private ComboBox cmbNhanVien, cmbThangKhoa, cmbNamKhoa;
        private TextBox txtGhiChu;
        private Button btnTimKiem, btnCapNhat, btnKhoaCong, btnMoKhoaCong, btnLamMoi, btnXuatBaoCao;
        private Button btnCheckIn, btnCheckOut, btnRefreshStatus;
        private Panel pnlFilter, pnlThongTin, pnlCheckInOut;
        private TabControl tabControl;
        private Label lblTongGioCong, lblDiTre, lblVeSom;
        private Label lblTrangThaiHienTai, lblThongTinCa, lblThongTinChamCong;
        private Label lblTitle;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            if (disposing && _refreshTimer != null)
            {
                _refreshTimer.Stop();
                _refreshTimer.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.SuspendLayout();
            
            // Form properties
            this.BackColor = Color.FromArgb(50, 50, 50);
            this.Size = new Size(900, 650);
            this.Text = "Quản lý Chấm công";
            this.Padding = new Padding(20);
            this.MinimumSize = new Size(900, 650);
            this.StartPosition = FormStartPosition.CenterParent;
            
            // Add preview controls for Designer
            var lblPreview = new Label
            {
                Text = "QUẢN LÝ CHẤM CÔNG\n(Designer Preview)",
                Font = new Font("Segoe UI", 18, FontStyle.Bold),
                ForeColor = Color.FromArgb(124, 77, 255),
                Location = new Point(50, 50),
                AutoSize = true
            };
            
            var pnlCheckInOut = new Panel
            {
                BackColor = Color.FromArgb(60, 60, 60),
                BorderStyle = BorderStyle.FixedSingle,
                Size = new Size(300, 120),
                Location = new Point(50, 120)
            };
            
            var lblCheckInOut = new Label
            {
                Text = "🕐 CHECK IN/OUT\nTrạng thái: Chưa vào ca\nButtons: Check In | Check Out",
                ForeColor = Color.White,
                Location = new Point(20, 20),
                Size = new Size(260, 80),
                Font = new Font("Segoe UI", 10)
            };
            pnlCheckInOut.Controls.Add(lblCheckInOut);
            
            var pnlFilter = new Panel
            {
                BackColor = Color.FromArgb(60, 60, 60),
                BorderStyle = BorderStyle.FixedSingle,
                Size = new Size(500, 120),
                Location = new Point(370, 120)
            };
            
            var lblFilter = new Label
            {
                Text = "BỘ LỌC CHẤM CÔNG\nTháng/Năm | Nhân viên | Trạng thái\nDataGridView: Danh sách chấm công",
                ForeColor = Color.White,
                Location = new Point(20, 20),
                Size = new Size(460, 80),
                Font = new Font("Segoe UI", 10)
            };
            pnlFilter.Controls.Add(lblFilter);
            
            this.Controls.AddRange(new Control[] { lblPreview, pnlCheckInOut, pnlFilter });

            this.ResumeLayout(false);
            this.PerformLayout();
            SetupEventHandlers();

            this.Load += (sender, e) => PerformLayoutLayout();
            this.Resize += (sender, e) => PerformLayoutLayout();
        }

        private void CreateControls()
        {
            lblTitle = new Label { Text = "QUẢN LÝ CHẤM CÔNG", Font = new Font("Segoe UI", 20, FontStyle.Bold), ForeColor = Color.FromArgb(124, 77, 255), AutoSize = true };
            tabControl = new TabControl { Font = new Font("Segoe UI", 10, FontStyle.Bold) };

            var tabChamCong = new TabPage("Chấm công hàng ngày") { BackColor = Color.FromArgb(50, 50, 50) };
            var tabLichChamCong = new TabPage("Lịch & chấm công") { BackColor = Color.FromArgb(50, 50, 50) };
            var tabCheckInOut = new TabPage("Check In/Out") { BackColor = Color.FromArgb(50, 50, 50) };
            var tabKhoaCong = new TabPage("Khóa công kỳ") { BackColor = Color.FromArgb(50, 50, 50) };

            CreateTab1Controls(tabChamCong);
            CreateTab2Controls(tabLichChamCong);
            CreateTab3Controls(tabCheckInOut);
            CreateTab4Controls(tabKhoaCong);

            tabControl.TabPages.AddRange(new TabPage[] { tabChamCong, tabLichChamCong, tabCheckInOut, tabKhoaCong });
            this.Controls.AddRange(new Control[] { lblTitle, tabControl });
        }

        private void CreateTab1Controls(TabPage tab)
        {
            pnlFilter = new Panel { BackColor = Color.FromArgb(60, 60, 60), BorderStyle = BorderStyle.FixedSingle };
            dtpTuNgay = CreateDatePicker(DateTime.Now.AddDays(-7));
            dtpDenNgay = CreateDatePicker(DateTime.Now);
            cmbNhanVien = CreateComboBox();
            btnTimKiem = CreateButton("Tìm kiếm", Color.FromArgb(33, 150, 243));
            btnLamMoi = CreateButton("Làm mới", Color.FromArgb(96, 125, 139));
            pnlFilter.Controls.AddRange(new Control[] { CreateLabel("Từ ngày:"), dtpTuNgay, CreateLabel("Đến ngày:"), dtpDenNgay, CreateLabel("Nhân viên:"), cmbNhanVien, btnTimKiem, btnLamMoi });

            dgvChamCong = CreateDataGridView();
            pnlThongTin = new Panel { BackColor = Color.FromArgb(60, 60, 60), BorderStyle = BorderStyle.FixedSingle, Padding = new Padding(15) };
            CreateInfoControls();

            tab.Controls.AddRange(new Control[] { pnlFilter, dgvChamCong, pnlThongTin });
        }

        private void CreateTab2Controls(TabPage tab)
        {
            dgvLichChamCong = CreateDataGridView();
            var lblTitle2 = new Label { Text = "LỊCH PHÂN CA & CHẤM CÔNG", Font = new Font("Segoe UI", 14, FontStyle.Bold), ForeColor = Color.FromArgb(124, 77, 255), AutoSize = true };
            tab.Controls.AddRange(new Control[] { lblTitle2, dgvLichChamCong });
        }

        private void CreateTab3Controls(TabPage tab)
        {
            var lblTitle3 = new Label { Text = "CHECK IN / CHECK OUT", Font = new Font("Segoe UI", 18, FontStyle.Bold), ForeColor = Color.FromArgb(124, 77, 255), AutoSize = true };
            pnlCheckInOut = new Panel { BackColor = Color.FromArgb(60, 60, 60), BorderStyle = BorderStyle.FixedSingle };
            lblTrangThaiHienTai = new Label { Text = "Đang tải trạng thái...", Font = new Font("Segoe UI", 14, FontStyle.Bold), ForeColor = Color.White, AutoSize = true };
            lblThongTinCa = new Label { Text = "Thông tin ca: Đang tải...", Font = new Font("Segoe UI", 12), ForeColor = Color.LightGray, AutoSize = true };
            lblThongTinChamCong = new Label { Text = "Chấm công: Đang tải...", Font = new Font("Segoe UI", 12), ForeColor = Color.LightGray, AutoSize = true };
            btnCheckIn = new Button { Text = "CHECK IN", BackColor = Color.FromArgb(46, 125, 50), ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Size = new Size(120, 50), Font = new Font("Segoe UI", 12, FontStyle.Bold) };
            btnCheckOut = new Button { Text = "CHECK OUT", BackColor = Color.FromArgb(244, 67, 54), ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Size = new Size(120, 50), Font = new Font("Segoe UI", 12, FontStyle.Bold) };
            btnRefreshStatus = CreateButton("Làm mới", Color.FromArgb(96, 125, 139));
            var lblHuongDan = new Label { Text = "• Check In: Chấm công vào khi bắt đầu ca làm việc...", Font = new Font("Segoe UI", 10), ForeColor = Color.Orange, AutoSize = true };
            pnlCheckInOut.Controls.AddRange(new Control[] { lblTrangThaiHienTai, lblThongTinCa, lblThongTinChamCong, btnCheckIn, btnCheckOut, btnRefreshStatus, lblHuongDan });
            tab.Controls.AddRange(new Control[] { lblTitle3, pnlCheckInOut });
        }

        private void CreateTab4Controls(TabPage tab)
        {
            var lblTitle4 = new Label { Text = "KHÓA CÔNG KỲ", Font = new Font("Segoe UI", 16, FontStyle.Bold), ForeColor = Color.FromArgb(124, 77, 255), AutoSize = true };
            cmbThangKhoa = CreateComboBox();
            for (int i = 1; i <= 12; i++) cmbThangKhoa.Items.Add(i);
            cmbNamKhoa = CreateComboBox();
            for (int i = DateTime.Now.Year - 2; i <= DateTime.Now.Year + 1; i++) cmbNamKhoa.Items.Add(i);
            btnKhoaCong = CreateButton("Khóa công", Color.FromArgb(244, 67, 54));
            btnMoKhoaCong = CreateButton("Mở khóa", Color.FromArgb(76, 175, 80));
            var lblWarning = new Label { Text = "⚠️ Cảnh báo: Sau khi khóa công, không thể chỉnh sửa dữ liệu chấm công trong kỳ này!", ForeColor = Color.Orange, Font = new Font("Segoe UI", 10, FontStyle.Bold), AutoSize = true };
            tab.Controls.AddRange(new Control[] { lblTitle4, CreateLabel("Tháng:"), cmbThangKhoa, CreateLabel("Năm:"), cmbNamKhoa, btnKhoaCong, btnMoKhoaCong, lblWarning });
        }

        private void CreateInfoControls()
        {
            var lblThongTin = new Label { Text = "THÔNG TIN CHẤM CÔNG", Font = new Font("Segoe UI", 12, FontStyle.Bold), ForeColor = Color.FromArgb(124, 77, 255), AutoSize = true };
            dtpGioVao = CreateTimePicker();
            dtpGioRa = CreateTimePicker();
            txtGhiChu = new TextBox { Multiline = true, Height = 60, BackColor = Color.FromArgb(70, 70, 70), ForeColor = Color.White, BorderStyle = BorderStyle.FixedSingle, Font = new Font("Segoe UI", 9) };
            btnCapNhat = CreateButton("Cập nhật", Color.FromArgb(46, 125, 50));
            lblTongGioCong = CreateLabel("Tổng giờ công: 0", Color.LightGreen);
            lblDiTre = CreateLabel("Đi trễ: 0 phút", Color.Orange);
            lblVeSom = CreateLabel("Về sớm: 0 phút", Color.Orange);
            pnlThongTin.Controls.AddRange(new Control[] { lblThongTin, CreateLabel("Giờ vào:"), dtpGioVao, CreateLabel("Giờ ra:"), dtpGioRa, CreateLabel("Ghi chú:"), txtGhiChu, btnCapNhat, lblTongGioCong, lblDiTre, lblVeSom });
        }

        private void SetupEventHandlers()
        {
            dgvChamCong.SelectionChanged += dgvChamCong_SelectionChanged;
            btnTimKiem.Click += btnTimKiem_Click;
            btnLamMoi.Click += btnLamMoi_Click;
            btnCapNhat.Click += btnCapNhat_Click;
            btnKhoaCong.Click += btnKhoaCong_Click;
            btnMoKhoaCong.Click += btnMoKhoaCong_Click;
            btnCheckIn.Click += btnCheckIn_Click;
            btnCheckOut.Click += btnCheckOut_Click;
            btnRefreshStatus.Click += btnRefreshStatus_Click;
        }

        #region Layout Logic
        private void PerformLayoutLayout()
        {
            int formWidth = this.ClientSize.Width;
            int formHeight = this.ClientSize.Height;

            lblTitle.Location = new Point(20, 20);
            tabControl.Location = new Point(20, 70);
            tabControl.Size = new Size(formWidth - 40, formHeight - 90);

            if (pnlFilter != null) { pnlFilter.Size = new Size(tabControl.Width - 40, pnlFilter.Height); LayoutFilterPanel(pnlFilter.Width); }
            LayoutTabContents();
        }

        private void LayoutFilterPanel(int panelWidth)
        {
            if (panelWidth < 550) { pnlFilter.Height = 120; LayoutFilterControlsVertical(); }
            else if (panelWidth < 750) { pnlFilter.Height = 80; LayoutFilterControlsCompact(); }
            else { pnlFilter.Height = 80; LayoutFilterControlsFull(); }
        }

        private void LayoutFilterControlsVertical()
        {
            Control[] ctrls = pnlFilter.Controls.OfType<Control>().ToArray();
            ctrls[0].Location = new Point(10, 10); dtpTuNgay.Location = new Point(80, 8); dtpTuNgay.Size = new Size(100, 25);
            ctrls[2].Location = new Point(190, 10); dtpDenNgay.Location = new Point(260, 8); dtpDenNgay.Size = new Size(100, 25);
            ctrls[4].Location = new Point(10, 40); cmbNhanVien.Location = new Point(80, 38); cmbNhanVien.Size = new Size(150, 25);
            btnTimKiem.Location = new Point(240, 38); btnLamMoi.Location = new Point(330, 38);
        }

        private void LayoutFilterControlsCompact()
        {
            Control[] ctrls = pnlFilter.Controls.OfType<Control>().ToArray();
            ctrls[0].Location = new Point(10, 15); dtpTuNgay.Location = new Point(10, 35); dtpTuNgay.Size = new Size(100, 25);
            ctrls[2].Location = new Point(120, 15); dtpDenNgay.Location = new Point(120, 35); dtpDenNgay.Size = new Size(100, 25);
            ctrls[4].Location = new Point(230, 15); cmbNhanVien.Location = new Point(230, 35); cmbNhanVien.Size = new Size(150, 25);
            btnTimKiem.Location = new Point(390, 33); btnLamMoi.Location = new Point(480, 33);
        }

        private void LayoutFilterControlsFull()
        {
            Control[] ctrls = pnlFilter.Controls.OfType<Control>().ToArray();
            ctrls[0].Location = new Point(10, 25); dtpTuNgay.Location = new Point(80, 23); dtpTuNgay.Size = new Size(120, 25);
            ctrls[2].Location = new Point(210, 25); dtpDenNgay.Location = new Point(280, 23); dtpDenNgay.Size = new Size(120, 25);
            ctrls[4].Location = new Point(410, 25); cmbNhanVien.Location = new Point(480, 23); cmbNhanVien.Size = new Size(200, 25);
            btnTimKiem.Location = new Point(690, 21); btnLamMoi.Location = new Point(780, 21);
        }

        private void LayoutTabContents()
        {
            int tabWidth = tabControl.Width - 40;
            int filterHeight = pnlFilter.Height;
            int contentY = filterHeight + 40;
            int tabHeight = tabControl.Height - contentY - 20;

            if (tabWidth < 700)
            {
                dgvChamCong.Location = new Point(20, contentY); dgvChamCong.Size = new Size(tabWidth, (int)(tabHeight * 0.6));
                pnlThongTin.Location = new Point(20, dgvChamCong.Bottom + 10); pnlThongTin.Size = new Size(tabWidth, (int)(tabHeight * 0.35));
            }
            else
            {
                dgvChamCong.Location = new Point(20, contentY); dgvChamCong.Size = new Size((int)(tabWidth * 0.6), tabHeight);
                pnlThongTin.Location = new Point(dgvChamCong.Right + 20, contentY); pnlThongTin.Size = new Size((int)(tabWidth * 0.35), tabHeight);
            }
            dgvLichChamCong.Location = new Point(20, 20); dgvLichChamCong.Size = new Size(tabWidth, tabControl.Height - 60);
            pnlCheckInOut.Size = new Size(Math.Min(600, tabWidth), 400); pnlCheckInOut.Location = new Point(20, 20);
            LayoutInfoControls();
        }

        private void LayoutInfoControls()
        {
            Control[] ctrls = pnlThongTin.Controls.OfType<Control>().ToArray();
            int y = 10; int pnlWidth = pnlThongTin.Width;
            ctrls[0].Location = new Point(10, y); y += 40;
            ctrls[1].Location = new Point(10, y); dtpGioVao.Location = new Point(80, y - 2); dtpGioVao.Width = pnlWidth - 100; y += 35;
            ctrls[3].Location = new Point(10, y); dtpGioRa.Location = new Point(80, y - 2); dtpGioRa.Width = pnlWidth - 100; y += 35;
            ctrls[5].Location = new Point(10, y); txtGhiChu.Location = new Point(10, y + 25); txtGhiChu.Size = new Size(pnlWidth - 40, 60); y += 95;
            btnCapNhat.Location = new Point(10, y); y += 45;
            lblTongGioCong.Location = new Point(10, y); y += 25;
            lblDiTre.Location = new Point(10, y); y += 25;
            lblVeSom.Location = new Point(10, y);
        }
        #endregion

        #region Control Factory Methods
        private Button CreateButton(string text, Color backColor) => new Button { Text = text, BackColor = backColor, ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Size = new Size(80, 30), Font = new Font("Segoe UI", 9, FontStyle.Bold) };
        private ComboBox CreateComboBox() => new ComboBox { BackColor = Color.FromArgb(70, 70, 70), ForeColor = Color.White, FlatStyle = FlatStyle.Flat, DropDownStyle = ComboBoxStyle.DropDownList, Font = new Font("Segoe UI", 9) };
        private DateTimePicker CreateDatePicker(DateTime value) => new DateTimePicker { Format = DateTimePickerFormat.Short, Font = new Font("Segoe UI", 9), Size = new Size(120, 25), Value = value };
        private DateTimePicker CreateTimePicker() => new DateTimePicker { Format = DateTimePickerFormat.Time, ShowUpDown = true, Font = new Font("Segoe UI", 9) };
        private Label CreateLabel(string text, Color? color = null) => new Label { Text = text, ForeColor = color ?? Color.White, AutoSize = true, Font = new Font("Segoe UI", 9) };
        private DataGridView CreateDataGridView() => new DataGridView { BackgroundColor = Color.FromArgb(60, 60, 60), ForeColor = Color.White, BorderStyle = BorderStyle.None, AllowUserToAddRows = false, AllowUserToDeleteRows = false, ReadOnly = true, SelectionMode = DataGridViewSelectionMode.FullRowSelect, MultiSelect = false, AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill, ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle { BackColor = Color.FromArgb(124, 77, 255), ForeColor = Color.White, Font = new Font("Segoe UI", 10.5F, FontStyle.Bold) }, DefaultCellStyle = new DataGridViewCellStyle { BackColor = Color.FromArgb(60, 60, 60), ForeColor = Color.White, SelectionBackColor = Color.FromArgb(124, 77, 255) } };
        #endregion

        #endregion
    }
}
