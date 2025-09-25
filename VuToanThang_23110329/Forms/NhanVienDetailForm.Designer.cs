using System;
using System.Drawing;
using System.Windows.Forms;
using VuToanThang_23110329.Controls;

namespace VuToanThang_23110329.Forms
{
    partial class NhanVienDetailForm
    {
        private System.ComponentModel.IContainer components = null;

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
            this.ForeColor = Color.White;
            this.Size = new Size(480, 600);
            this.Text = "Nhân viên";
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.ShowInTaskbar = false;
            this.Font = new Font("Segoe UI", 9);
            
            // Add preview controls for Designer
            var lblPreview = new Label
            {
                Text = "CHI TIẾT NHÂN VIÊN\n(Designer Preview)",
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                ForeColor = Color.FromArgb(124, 77, 255),
                Location = new Point(50, 30),
                AutoSize = true
            };
            
            var pnlPersonalInfo = new Panel
            {
                BackColor = Color.FromArgb(60, 60, 60),
                BorderStyle = BorderStyle.FixedSingle,
                Size = new Size(380, 150),
                Location = new Point(50, 100)
            };
            
            var lblPersonalInfo = new Label
            {
                Text = "👤 THÔNG TIN CÁ NHÂN\n\nHọ tên | Ngày sinh | Giới tính\nĐiện thoại | Email | Địa chỉ\nCCCD | Nơi cấp | Ngày cấp",
                ForeColor = Color.White,
                Location = new Point(20, 20),
                Size = new Size(340, 110),
                Font = new Font("Segoe UI", 9)
            };
            pnlPersonalInfo.Controls.Add(lblPersonalInfo);
            
            var pnlWorkInfo = new Panel
            {
                BackColor = Color.FromArgb(60, 60, 60),
                BorderStyle = BorderStyle.FixedSingle,
                Size = new Size(380, 120),
                Location = new Point(50, 270)
            };
            
            var lblWorkInfo = new Label
            {
                Text = "💼 THÔNG TIN CÔNG VIỆC\n\nNgày vào làm | Trạng thái\nPhòng ban | Chức danh | Lương cơ bản\nTài khoản đăng nhập | Vai trò",
                ForeColor = Color.White,
                Location = new Point(20, 20),
                Size = new Size(340, 80),
                Font = new Font("Segoe UI", 9)
            };
            pnlWorkInfo.Controls.Add(lblWorkInfo);
            
            var pnlButtons = new Panel
            {
                BackColor = Color.FromArgb(70, 70, 70),
                Size = new Size(380, 50),
                Location = new Point(50, 410)
            };
            
            var lblButtons = new Label
            {
                Text = "Buttons: Lưu | Hủy | Tạo tài khoản | Đặt lại mật khẩu",
                ForeColor = Color.White,
                Location = new Point(20, 15),
                AutoSize = true,
                Font = new Font("Segoe UI", 9)
            };
            pnlButtons.Controls.Add(lblButtons);
            
            this.Controls.AddRange(new Control[] { lblPreview, pnlPersonalInfo, pnlWorkInfo, pnlButtons });
            
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion
    }
}
