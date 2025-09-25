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
        }

        #endregion
    }
}
