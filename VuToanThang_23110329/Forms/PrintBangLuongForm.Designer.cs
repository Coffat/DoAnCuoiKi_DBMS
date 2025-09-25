using System;
using System.Drawing;
using System.Windows.Forms;

namespace VuToanThang_23110329.Forms
{
    partial class PrintBangLuongForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.SuspendLayout();
            
            // Form properties
            this.Text = "In bảng lương";
            this.Size = new Size(1000, 700);
            this.BackColor = Color.White;
            this.StartPosition = FormStartPosition.CenterParent;
            this.WindowState = FormWindowState.Maximized;
            
            // Add preview controls for Designer (with white background for print preview)
            var lblPreview = new Label
            {
                Text = "IN BẢNG LƯƠNG\n(Designer Preview)",
                Font = new Font("Segoe UI", 18, FontStyle.Bold),
                ForeColor = Color.FromArgb(124, 77, 255),
                Location = new Point(50, 50),
                AutoSize = true
            };
            
            var pnlHeader = new Panel
            {
                BackColor = Color.FromArgb(240, 240, 240),
                BorderStyle = BorderStyle.FixedSingle,
                Size = new Size(900, 100),
                Location = new Point(50, 120)
            };
            
            var lblHeader = new Label
            {
                Text = "🏢 CÔNG TY TNHH SIÊU THỊ MINI\n\nBÁO CÁO BẢNG LƯƠNG THÁNG [MM/YYYY]\nNgày in: [Date] | Người in: [User]",
                ForeColor = Color.Black,
                Location = new Point(20, 20),
                Size = new Size(860, 60),
                Font = new Font("Segoe UI", 11, FontStyle.Bold)
            };
            pnlHeader.Controls.Add(lblHeader);
            
            var pnlContent = new Panel
            {
                BackColor = Color.FromArgb(250, 250, 250),
                BorderStyle = BorderStyle.FixedSingle,
                Size = new Size(900, 400),
                Location = new Point(50, 240)
            };
            
            var lblContent = new Label
            {
                Text = "📊 NỘI DUNG BẢNG LƯƠNG\n\nTable Headers: STT | Mã NV | Họ tên | Chức danh | Lương CB | Giờ công | OT | Phụ cấp | Khấu trừ | Thực lãnh\n\nData Rows: [Employee salary data]\n\nTổng cộng: [Total amount]\nSố nhân viên: [Employee count]\nTrung bình: [Average salary]",
                ForeColor = Color.Black,
                Location = new Point(20, 50),
                Size = new Size(860, 300),
                Font = new Font("Segoe UI", 10)
            };
            pnlContent.Controls.Add(lblContent);
            
            var pnlButtons = new Panel
            {
                BackColor = Color.FromArgb(230, 230, 230),
                Size = new Size(900, 50),
                Location = new Point(50, 660)
            };
            
            var lblButtons = new Label
            {
                Text = "Buttons: In | Xuất PDF | Xem trước | Đóng | Cài đặt trang",
                ForeColor = Color.Black,
                Location = new Point(20, 15),
                AutoSize = true,
                Font = new Font("Segoe UI", 10)
            };
            pnlButtons.Controls.Add(lblButtons);
            
            this.Controls.AddRange(new Control[] { lblPreview, pnlHeader, pnlContent, pnlButtons });
            
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion
    }
}
