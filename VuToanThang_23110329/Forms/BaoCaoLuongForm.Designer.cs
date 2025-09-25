using System;
using System.Drawing;
using System.Windows.Forms;

namespace VuToanThang_23110329.Forms
{
    partial class BaoCaoLuongForm
    {
        private System.ComponentModel.IContainer components = null;

        // UI Controls
        private TabControl tabControl;
        private DataGridView dgvBaoCaoThang, dgvBaoCaoNam, dgvSoSanh;
        private ComboBox cmbThang, cmbNam, cmbNamSoSanh, cmbPhongBan;
        private Button btnXemBaoCao, btnXuatExcel, btnLamMoi;
        private Panel pnlThongKe;
        private Label lblTongLuong, lblLuongTB, lblCaoNhat, lblThapNhat, lblTongNV, lblTitle;

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
            this.Size = new Size(900, 650);
            this.Text = "Báo cáo lương";
            this.Padding = new Padding(20);
            this.MinimumSize = new Size(900, 650);
            this.StartPosition = FormStartPosition.CenterParent;
            
            // Add preview controls for Designer
            var lblPreview = new Label
            {
                Text = "BÁO CÁO LƯƠNG\n(Designer Preview)",
                Font = new Font("Segoe UI", 18, FontStyle.Bold),
                ForeColor = Color.FromArgb(124, 77, 255),
                Location = new Point(50, 50),
                AutoSize = true
            };
            
            var pnlStats = new Panel
            {
                BackColor = Color.FromArgb(60, 60, 60),
                BorderStyle = BorderStyle.FixedSingle,
                Size = new Size(800, 100),
                Location = new Point(50, 120)
            };
            
            var lblStats = new Label
            {
                Text = "Thống kê: Tổng lương | Trung bình | Cao nhất | Thấp nhất | Tổng nhân viên",
                ForeColor = Color.White,
                Location = new Point(20, 40),
                AutoSize = true,
                Font = new Font("Segoe UI", 10)
            };
            pnlStats.Controls.Add(lblStats);
            
            var pnlReport = new Panel
            {
                BackColor = Color.FromArgb(60, 60, 60),
                BorderStyle = BorderStyle.FixedSingle,
                Size = new Size(800, 350),
                Location = new Point(50, 240)
            };
            
            var lblReport = new Label
            {
                Text = "DataGridView: Báo cáo chi tiết lương nhân viên\nButtons: Xuất Excel | In báo cáo",
                ForeColor = Color.White,
                Location = new Point(20, 150),
                AutoSize = true,
                Font = new Font("Segoe UI", 12)
            };
            pnlReport.Controls.Add(lblReport);
            
            this.Controls.AddRange(new Control[] { lblPreview, pnlStats, pnlReport });

            this.ResumeLayout(false);
            this.PerformLayout();
            
            this.Load += (s, e) => PerformLayoutLayout();
            this.Resize += (s, e) => PerformLayoutLayout();
        }

        private void CreateControls()
        {
            lblTitle = new Label { Text = "BÁO CÁO LƯƠNG", Font = new Font("Segoe UI", 20, FontStyle.Bold), ForeColor = Color.FromArgb(124, 77, 255), AutoSize = true };

            pnlThongKe = new Panel { BackColor = Color.FromArgb(60, 60, 60), BorderStyle = BorderStyle.FixedSingle };
            lblTongNV = CreateStatLabel("Tổng NV: 0", Color.White);
            lblTongLuong = CreateStatLabel("Tổng lương: 0 VNĐ", Color.LightGreen);
            lblLuongTB = CreateStatLabel("Lương TB: 0 VNĐ", Color.LightBlue);
            lblCaoNhat = CreateStatLabel("Cao nhất: 0 VNĐ", Color.Orange);
            lblThapNhat = CreateStatLabel("Thấp nhất: 0 VNĐ", Color.LightCoral);
            pnlThongKe.Controls.AddRange(new Control[] { lblTongNV, lblTongLuong, lblLuongTB, lblCaoNhat, lblThapNhat });

            tabControl = new TabControl { BackColor = Color.FromArgb(60, 60, 60), ForeColor = Color.White, Font = new Font("Segoe UI", 10, FontStyle.Bold) };
            var tabThang = new TabPage("Báo cáo theo tháng") { BackColor = Color.FromArgb(50, 50, 50) };
            var tabNam = new TabPage("Báo cáo theo năm") { BackColor = Color.FromArgb(50, 50, 50) };
            var tabSoSanh = new TabPage("So sánh lương") { BackColor = Color.FromArgb(50, 50, 50) };

            CreateTab1Controls(tabThang);
            CreateTab2Controls(tabNam);
            CreateTab3Controls(tabSoSanh);
            tabControl.TabPages.AddRange(new TabPage[] { tabThang, tabNam, tabSoSanh });

            btnXuatExcel = CreateButton("Xuất Excel", Color.FromArgb(46, 125, 50));
            btnLamMoi = CreateButton("Làm mới", Color.FromArgb(96, 125, 139));

            this.Controls.AddRange(new Control[] { lblTitle, pnlThongKe, tabControl, btnXuatExcel, btnLamMoi });
        }

        private void CreateTab1Controls(TabPage tab) { /* Omitted for brevity */ }
        private void CreateTab2Controls(TabPage tab) { /* Omitted for brevity */ }
        private void CreateTab3Controls(TabPage tab) { /* Omitted for brevity */ }

        private void SetupEventHandlers()
        {
            btnXemBaoCao.Click += btnXemBaoCao_Click;
            btnXuatExcel.Click += btnXuatExcel_Click;
            btnLamMoi.Click += btnLamMoi_Click;
            tabControl.SelectedIndexChanged += tabControl_SelectedIndexChanged;
        }

        #region Layout Logic
        private void PerformLayoutLayout() { /* Omitted for brevity */ }
        #endregion

        #region Control Factory Methods
        private Label CreateStatLabel(string text, Color color) => new Label { Text = text, ForeColor = color, Font = new Font("Segoe UI", 11, FontStyle.Bold), AutoSize = true };
        private Button CreateButton(string text, Color backColor) => new Button { Text = text, BackColor = backColor, ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Size = new Size(120, 35), Font = new Font("Segoe UI", 9, FontStyle.Bold) };
        private ComboBox CreateComboBox() => new ComboBox { BackColor = Color.FromArgb(70, 70, 70), ForeColor = Color.White, FlatStyle = FlatStyle.Flat, DropDownStyle = ComboBoxStyle.DropDownList, Font = new Font("Segoe UI", 9), Size = new Size(80, 25) };
        private DataGridView CreateDataGridView() => new DataGridView { BackgroundColor = Color.FromArgb(60, 60, 60), ForeColor = Color.White, BorderStyle = BorderStyle.None, AllowUserToAddRows = false, AllowUserToDeleteRows = false, ReadOnly = true, SelectionMode = DataGridViewSelectionMode.FullRowSelect, MultiSelect = false, AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill, ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle { BackColor = Color.FromArgb(124, 77, 255), ForeColor = Color.White, Font = new Font("Segoe UI", 10.5F, FontStyle.Bold) }, DefaultCellStyle = new DataGridViewCellStyle { BackColor = Color.FromArgb(60, 60, 60), ForeColor = Color.White, SelectionBackColor = Color.FromArgb(124, 77, 255) } };
        private Label CreateLabel(string text) => new Label { Text = text, ForeColor = Color.White, AutoSize = true, Font = new Font("Segoe UI", 9) };
        #endregion

        #endregion
    }
}
