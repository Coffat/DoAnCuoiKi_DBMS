using System;
using System.Drawing;
using System.Windows.Forms;

namespace VuToanThang_23110329.Forms
{
    partial class PrintPayslipForm
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
            this.Text = "In phiếu lương";
            this.Size = new Size(900, 700);
            this.BackColor = Color.White;
            this.StartPosition = FormStartPosition.CenterParent;
            this.WindowState = FormWindowState.Maximized;
            
            // Add preview controls for Designer (with white background for print preview)
            var lblPreview = new Label
            {
                Text = "IN PHIẾU LƯƠNG\n(Designer Preview)",
                Font = new Font("Segoe UI", 18, FontStyle.Bold),
                ForeColor = Color.FromArgb(124, 77, 255),
                Location = new Point(50, 50),
                AutoSize = true
            };
            
            var pnlHeader = new Panel
            {
                BackColor = Color.FromArgb(240, 240, 240),
                BorderStyle = BorderStyle.FixedSingle,
                Size = new Size(800, 120),
                Location = new Point(50, 120)
            };
            
            var lblHeader = new Label
            {
                Text = "🏢 CÔNG TY TNHH SIÊU THỊ MINI\n\nPHIẾU LƯƠNG THÁNG [MM/YYYY]\nNhân viên: [Employee Name] | Mã NV: [ID]\nChức danh: [Position] | Phòng ban: [Department]",
                ForeColor = Color.Black,
                Location = new Point(20, 20),
                Size = new Size(760, 80),
                Font = new Font("Segoe UI", 10, FontStyle.Bold)
            };
            pnlHeader.Controls.Add(lblHeader);
            
            var pnlSalaryDetail = new Panel
            {
                BackColor = Color.FromArgb(250, 250, 250),
                BorderStyle = BorderStyle.FixedSingle,
                Size = new Size(800, 300),
                Location = new Point(50, 260)
            };
            
            var lblSalaryDetail = new Label
            {
                Text = "💰 CHI TIẾT LƯƠNG\n\nLương cơ bản: [Basic Salary] VNĐ\nSố ngày công: [Work Days] ngày\nTổng giờ công: [Total Hours] giờ\nGiờ làm thêm: [OT Hours] giờ\nLương OT: [OT Amount] VNĐ\n\nPhụ cấp: [Allowance] VNĐ\nKhấu trừ: [Deduction] VNĐ\nThuế TNCN & BH: [Tax] VNĐ\n\n═══════════════════════════\n➤ TỔNG THỰC LÃNH: [Net Salary] VNĐ\n═══════════════════════════",
                ForeColor = Color.Black,
                Location = new Point(20, 20),
                Size = new Size(760, 260),
                Font = new Font("Segoe UI", 11)
            };
            pnlSalaryDetail.Controls.Add(lblSalaryDetail);
            
            var pnlButtons = new Panel
            {
                BackColor = Color.FromArgb(230, 230, 230),
                Size = new Size(800, 50),
                Location = new Point(50, 580)
            };
            
            var lblButtons = new Label
            {
                Text = "Buttons: In phiếu | Xuất PDF | Gửi email | Xem trước | Đóng",
                ForeColor = Color.Black,
                Location = new Point(20, 15),
                AutoSize = true,
                Font = new Font("Segoe UI", 10)
            };
            pnlButtons.Controls.Add(lblButtons);
            
            this.Controls.AddRange(new Control[] { lblPreview, pnlHeader, pnlSalaryDetail, pnlButtons });
            
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion
    }
}
