using System;
using System.Drawing;
using System.Windows.Forms;

namespace VuToanThang_23110329.Forms
{
    partial class DonTuDetailForm
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
            this.Text = "Chi tiết đơn từ";
            this.Size = new Size(500, 400);
            this.BackColor = Color.FromArgb(50, 50, 50);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            
            // Add preview controls for Designer
            var lblPreview = new Label
            {
                Text = "CHI TIẾT ĐƠN TỪ\n(Designer Preview)",
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                ForeColor = Color.FromArgb(124, 77, 255),
                Location = new Point(50, 50),
                AutoSize = true
            };
            
            var pnlInfo = new Panel
            {
                BackColor = Color.FromArgb(60, 60, 60),
                BorderStyle = BorderStyle.FixedSingle,
                Size = new Size(400, 120),
                Location = new Point(50, 120)
            };
            
            var lblInfo = new Label
            {
                Text = "📋 THÔNG TIN ĐƠN TỪ\n\nLoại đơn: Nghỉ phép / Làm thêm giờ\nTừ lúc - Đến lúc: Thời gian chi tiết\nSố giờ: Tổng thời gian\nLý do: Mô tả chi tiết\nTrạng thái: Chờ duyệt / Đã duyệt / Từ chối",
                ForeColor = Color.White,
                Location = new Point(20, 20),
                Size = new Size(360, 80),
                Font = new Font("Segoe UI", 9)
            };
            pnlInfo.Controls.Add(lblInfo);
            
            var pnlButtons = new Panel
            {
                BackColor = Color.FromArgb(70, 70, 70),
                Size = new Size(400, 50),
                Location = new Point(50, 260)
            };
            
            var lblButtons = new Label
            {
                Text = "Buttons: Đóng | Duyệt (nếu có quyền) | Từ chối (nếu có quyền)",
                ForeColor = Color.White,
                Location = new Point(20, 15),
                AutoSize = true,
                Font = new Font("Segoe UI", 9)
            };
            pnlButtons.Controls.Add(lblButtons);
            
            this.Controls.AddRange(new Control[] { lblPreview, pnlInfo, pnlButtons });
            
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion
    }
}
