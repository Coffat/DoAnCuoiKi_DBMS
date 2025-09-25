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
            this.SuspendLayout();
            
            // Form properties
            this.BackColor = Color.FromArgb(50, 50, 50);
            this.Size = new Size(1000, 700);
            this.Text = "Tính lương";
            this.Padding = new Padding(20);
            this.MinimumSize = new Size(900, 650);
            this.AutoScaleMode = AutoScaleMode.Dpi;
            this.StartPosition = FormStartPosition.CenterParent;
            
            // Add preview controls for Designer
            var lblPreview = new Label
            {
                Text = "TÍNH LƯƠNG NHÂN VIÊN\n(Designer Preview)",
                Font = new Font("Segoe UI", 18, FontStyle.Bold),
                ForeColor = Color.FromArgb(124, 77, 255),
                Location = new Point(50, 50),
                AutoSize = true
            };
            
            var pnlPreview = new Panel
            {
                BackColor = Color.FromArgb(60, 60, 60),
                BorderStyle = BorderStyle.FixedSingle,
                Size = new Size(900, 100),
                Location = new Point(50, 120)
            };
            
            var lblPreviewInfo = new Label
            {
                Text = "Thông số tính lương: Tháng/Năm | Buttons: Chạy lương, Đóng lương\nTabControl: Công tháng | Bảng lương",
                ForeColor = Color.White,
                Location = new Point(20, 20),
                Size = new Size(860, 60),
                Font = new Font("Segoe UI", 10)
            };
            pnlPreview.Controls.Add(lblPreviewInfo);
            
            this.Controls.AddRange(new Control[] { lblPreview, pnlPreview });

            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion
    }
}
