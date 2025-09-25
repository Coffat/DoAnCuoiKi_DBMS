using System;
using System.Drawing;
using System.Windows.Forms;

namespace VuToanThang_23110329.Forms
{
    partial class ChiTietBangLuongForm
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
            this.Text = "Chi tiết bảng lương";
            this.Size = new Size(600, 500);
            this.BackColor = Color.FromArgb(50, 50, 50);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            
            // Add preview controls for Designer
            var lblPreview = new Label
            {
                Text = "CHI TIẾT BẢNG LƯƠNG\n(Designer Preview)",
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                ForeColor = Color.FromArgb(124, 77, 255),
                Location = new Point(50, 30),
                AutoSize = true
            };
            
            var pnlEmployeeInfo = new Panel
            {
                BackColor = Color.FromArgb(60, 60, 60),
                BorderStyle = BorderStyle.FixedSingle,
                Size = new Size(500, 100),
                Location = new Point(50, 100)
            };
            
            var lblEmployeeInfo = new Label
            {
                Text = "👤 THÔNG TIN NHÂN VIÊN\n\nHọ tên: [Tên nhân viên] | Mã NV: [ID]\nChức danh: [Position] | Phòng ban: [Department]",
                ForeColor = Color.White,
                Location = new Point(20, 20),
                Size = new Size(460, 60),
                Font = new Font("Segoe UI", 9)
            };
            pnlEmployeeInfo.Controls.Add(lblEmployeeInfo);
            
            var pnlSalaryDetail = new Panel
            {
                BackColor = Color.FromArgb(60, 60, 60),
                BorderStyle = BorderStyle.FixedSingle,
                Size = new Size(500, 200),
                Location = new Point(50, 220)
            };
            
            var lblSalaryDetail = new Label
            {
                Text = "💰 CHI TIẾT LƯƠNG THÁNG [MM/YYYY]\n\nLương cơ bản: [Amount] VNĐ\nTổng giờ công: [Hours] giờ\nGiờ làm thêm: [OT Hours] giờ\nPhụ cấp: [Allowance] VNĐ\nKhấu trừ: [Deduction] VNĐ\nThuế & BH: [Tax] VNĐ\n\n➤ THỰC LÃNH: [Net Salary] VNĐ",
                ForeColor = Color.White,
                Location = new Point(20, 20),
                Size = new Size(460, 160),
                Font = new Font("Segoe UI", 10)
            };
            pnlSalaryDetail.Controls.Add(lblSalaryDetail);
            
            var pnlButtons = new Panel
            {
                BackColor = Color.FromArgb(70, 70, 70),
                Size = new Size(500, 50),
                Location = new Point(50, 440)
            };
            
            var lblButtons = new Label
            {
                Text = "Buttons: In phiếu lương | Xuất PDF | Đóng",
                ForeColor = Color.White,
                Location = new Point(20, 15),
                AutoSize = true,
                Font = new Font("Segoe UI", 9)
            };
            pnlButtons.Controls.Add(lblButtons);
            
            this.Controls.AddRange(new Control[] { lblPreview, pnlEmployeeInfo, pnlSalaryDetail, pnlButtons });
            
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion
    }
}
