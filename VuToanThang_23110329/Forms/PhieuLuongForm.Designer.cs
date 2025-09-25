using System;
using System.Drawing;
using System.Windows.Forms;

namespace VuToanThang_23110329.Forms
{
    partial class PhieuLuongForm
    {
        private System.ComponentModel.IContainer components = null;

        // UI Controls
        private ComboBox cmbThang, cmbNam;
        private Button btnXemPhieu, btnInPhieu, btnLamMoi;
        private Panel pnlPhieuLuong, pnlFilter;
        private Label lblThongTinNV, lblThongTinLuong, lblTinhToan, lblKetQua, lblTitle;

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
            this.Size = new Size(900, 800);
            this.Text = "Phiếu lương";
            this.Padding = new Padding(20);
            this.MinimumSize = new Size(800, 700);
            this.AutoScaleMode = AutoScaleMode.Dpi;
            this.StartPosition = FormStartPosition.CenterParent;
            
            // Add preview controls for Designer
            var lblPreview = new Label
            {
                Text = "PHIẾU LƯƠNG CỦA TÔI\n(Designer Preview)",
                Font = new Font("Segoe UI", 18, FontStyle.Bold),
                ForeColor = Color.FromArgb(124, 77, 255),
                Location = new Point(50, 50),
                AutoSize = true
            };
            
            var pnlFilter = new Panel
            {
                BackColor = Color.FromArgb(60, 60, 60),
                BorderStyle = BorderStyle.FixedSingle,
                Size = new Size(800, 80),
                Location = new Point(50, 120)
            };
            
            var lblFilter = new Label
            {
                Text = "📅 Chọn kỳ lương: Tháng | Năm | Button: Xem phiếu lương",
                ForeColor = Color.White,
                Location = new Point(20, 30),
                AutoSize = true,
                Font = new Font("Segoe UI", 10)
            };
            pnlFilter.Controls.Add(lblFilter);
            
            var pnlPayslip = new Panel
            {
                BackColor = Color.FromArgb(60, 60, 60),
                BorderStyle = BorderStyle.FixedSingle,
                Size = new Size(800, 500),
                Location = new Point(50, 220)
            };
            
            var lblPayslip = new Label
            {
                Text = "💰 PHIẾU LƯƠNG CHI TIẾT\n\nThông tin nhân viên: Họ tên, Mã NV, Chức danh\nThông tin lương:\n- Lương cơ bản, Số giờ công, Giờ OT\n- Phụ cấp, Khấu trừ, Thuế BH\n- Thực lãnh\n\nButtons: In phiếu lương | Xuất PDF | Gửi email\n\nTính năng: Preview trước khi in, Lưu lịch sử xem",
                ForeColor = Color.White,
                Location = new Point(20, 50),
                Size = new Size(760, 400),
                Font = new Font("Segoe UI", 11)
            };
            pnlPayslip.Controls.Add(lblPayslip);
            
            this.Controls.AddRange(new Control[] { lblPreview, pnlFilter, pnlPayslip });
            
            this.ResumeLayout(false);
            this.PerformLayout();
            
            this.Load += (s, e) => PerformLayoutLayout();
            this.Resize += (s, e) => PerformLayoutLayout();
        }

        private void CreateControls()
        {
            lblTitle = new Label { Text = "PHIẾU LƯƠNG CỦA TÔI", Font = new Font("Segoe UI", 20, FontStyle.Bold), ForeColor = Color.FromArgb(124, 77, 255), AutoSize = true };

            pnlFilter = new Panel { BackColor = Color.FromArgb(60, 60, 60), BorderStyle = BorderStyle.FixedSingle };
            cmbThang = CreateComboBox();
            for (int i = 1; i <= 12; i++) cmbThang.Items.Add(i);
            cmbThang.SelectedItem = DateTime.Now.Month;
            cmbNam = CreateComboBox();
            for (int i = DateTime.Now.Year - 2; i <= DateTime.Now.Year + 1; i++) cmbNam.Items.Add(i);
            cmbNam.SelectedItem = DateTime.Now.Year;
            btnXemPhieu = CreateButton("Xem phiếu lương", Color.FromArgb(33, 150, 243));
            btnInPhieu = CreateButton("In phiếu lương", Color.FromArgb(46, 125, 50));
            btnLamMoi = CreateButton("Làm mới", Color.FromArgb(96, 125, 139));
            pnlFilter.Controls.AddRange(new Control[] { CreateLabel("Tháng:"), cmbThang, CreateLabel("Năm:"), cmbNam, btnXemPhieu, btnInPhieu, btnLamMoi });

            pnlPhieuLuong = new Panel { BackColor = Color.White, BorderStyle = BorderStyle.FixedSingle, AutoScroll = true };
            CreatePayslipContent();

            this.Controls.AddRange(new Control[] { lblTitle, pnlFilter, pnlPhieuLuong });
        }

        private void CreatePayslipContent() { /* Omitted for brevity, contains label creation */ }

        private void SetupEventHandlers()
        {
            btnXemPhieu.Click += btnXemPhieu_Click;
            btnInPhieu.Click += btnInPhieu_Click;
            btnLamMoi.Click += btnLamMoi_Click;
        }

        #region Layout Logic
        private void PerformLayoutLayout() { /* Omitted for brevity */ }
        #endregion

        #region Control Factory Methods
        private Button CreateButton(string text, Color backColor) => new Button { Text = text, BackColor = backColor, ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Size = new Size(120, 35), Font = new Font("Segoe UI", 9, FontStyle.Bold) };
        private ComboBox CreateComboBox() => new ComboBox { BackColor = Color.FromArgb(70, 70, 70), ForeColor = Color.White, FlatStyle = FlatStyle.Flat, DropDownStyle = ComboBoxStyle.DropDownList, Font = new Font("Segoe UI", 9), Size = new Size(80, 25) };
        private Label CreateLabel(string text) => new Label { Text = text, ForeColor = Color.White, AutoSize = true, Font = new Font("Segoe UI", 9) };
        #endregion

        #endregion
    }
}
