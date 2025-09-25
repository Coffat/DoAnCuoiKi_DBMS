using System;
using System.Drawing;
using System.Windows.Forms;

namespace VuToanThang_23110329.Forms
{
    partial class BaoCaoNhanSuForm
    {
        private System.ComponentModel.IContainer components = null;

        // UI Controls
        private TabControl tabControl;
        private DataGridView dgvTongQuan, dgvChamCong, dgvDonTu;
        private ComboBox cmbThang, cmbNam, cmbPhongBan, cmbTrangThai;
        private DateTimePicker dtpTuNgay, dtpDenNgay;
        private Button btnXuatBaoCao, btnLamMoi;
        private Panel pnlThongKe;
        private Label lblTongNV, lblDangLam, lblNghi, lblTongCong, lblTongDonTu, lblTitle;

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
            this.Text = "Báo cáo nhân sự";
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
            lblTitle = new Label { Text = "BÁO CÁO NHÂN SỰ", Font = new Font("Segoe UI", 20, FontStyle.Bold), ForeColor = Color.FromArgb(124, 77, 255), AutoSize = true };

            pnlThongKe = new Panel { BackColor = Color.FromArgb(60, 60, 60), BorderStyle = BorderStyle.FixedSingle };
            lblTongNV = CreateStatLabel("Tổng NV: 0", Color.White);
            lblDangLam = CreateStatLabel("Đang làm: 0", Color.LightGreen);
            lblNghi = CreateStatLabel("Nghỉ việc: 0", Color.LightCoral);
            lblTongCong = CreateStatLabel("Tổng công: 0h", Color.LightBlue);
            lblTongDonTu = CreateStatLabel("Tổng đơn từ: 0", Color.Orange);
            pnlThongKe.Controls.AddRange(new Control[] { lblTongNV, lblDangLam, lblNghi, lblTongCong, lblTongDonTu });

            tabControl = new TabControl { BackColor = Color.FromArgb(60, 60, 60), ForeColor = Color.White, Font = new Font("Segoe UI", 10, FontStyle.Bold) };
            var tabTongQuan = new TabPage("Tổng quan nhân viên") { BackColor = Color.FromArgb(50, 50, 50) };
            var tabChamCong = new TabPage("Báo cáo chấm công") { BackColor = Color.FromArgb(50, 50, 50) };
            var tabDonTu = new TabPage("Báo cáo đơn từ") { BackColor = Color.FromArgb(50, 50, 50) };

            CreateTab1Controls(tabTongQuan);
            CreateTab2Controls(tabChamCong);
            CreateTab3Controls(tabDonTu);
            tabControl.TabPages.AddRange(new TabPage[] { tabTongQuan, tabChamCong, tabDonTu });

            btnXuatBaoCao = CreateButton("Xuất báo cáo", Color.FromArgb(46, 125, 50));
            btnLamMoi = CreateButton("Làm mới", Color.FromArgb(96, 125, 139));

            this.Controls.AddRange(new Control[] { lblTitle, pnlThongKe, tabControl, btnXuatBaoCao, btnLamMoi });
        }

        private void CreateTab1Controls(TabPage tab) { /* Omitted for brevity */ }
        private void CreateTab2Controls(TabPage tab) { /* Omitted for brevity */ }
        private void CreateTab3Controls(TabPage tab) { /* Omitted for brevity */ }

        private void SetupEventHandlers()
        {
            btnXuatBaoCao.Click += btnXuatBaoCao_Click;
            btnLamMoi.Click += btnLamMoi_Click;
            tabControl.SelectedIndexChanged += tabControl_SelectedIndexChanged;
        }

        #region Layout Logic
        private void PerformLayoutLayout() { /* Omitted for brevity */ }
        #endregion

        #region Control Factory Methods
        private Label CreateStatLabel(string text, Color color) => new Label { Text = text, ForeColor = color, Font = new Font("Segoe UI", 12, FontStyle.Bold), AutoSize = true };
        private Button CreateButton(string text, Color backColor) => new Button { Text = text, BackColor = backColor, ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Size = new Size(120, 35), Font = new Font("Segoe UI", 9, FontStyle.Bold) };
        private ComboBox CreateComboBox() => new ComboBox { BackColor = Color.FromArgb(70, 70, 70), ForeColor = Color.White, FlatStyle = FlatStyle.Flat, DropDownStyle = ComboBoxStyle.DropDownList, Font = new Font("Segoe UI", 9) };
        private DateTimePicker CreateDatePicker() => new DateTimePicker { Format = DateTimePickerFormat.Short, Font = new Font("Segoe UI", 9) };
        private DataGridView CreateDataGridView() => new DataGridView { BackgroundColor = Color.FromArgb(60, 60, 60), ForeColor = Color.White, BorderStyle = BorderStyle.None, AllowUserToAddRows = false, AllowUserToDeleteRows = false, ReadOnly = true, SelectionMode = DataGridViewSelectionMode.FullRowSelect, MultiSelect = false, AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill, ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle { BackColor = Color.FromArgb(124, 77, 255), ForeColor = Color.White, Font = new Font("Segoe UI", 10.5F, FontStyle.Bold) }, DefaultCellStyle = new DataGridViewCellStyle { BackColor = Color.FromArgb(60, 60, 60), ForeColor = Color.White, SelectionBackColor = Color.FromArgb(124, 77, 255) } };
        private Label CreateLabel(string text) => new Label { Text = text, ForeColor = Color.White, AutoSize = true, Font = new Font("Segoe UI", 9) };
        #endregion

        #endregion
    }
}
