using System;
using System.Drawing;
using System.Windows.Forms;

namespace VuToanThang_23110329.Forms
{
    partial class LichPhanCaDetailForm
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
            this.Text = "Chi tiết lịch phân ca";
            this.Size = new Size(500, 450);
            this.BackColor = Color.FromArgb(50, 50, 50);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            
            // Add preview controls for Designer
            var lblPreview = new Label
            {
                Text = "CHI TIẾT LỊCH PHÂN CA\n(Designer Preview)",
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                ForeColor = Color.FromArgb(124, 77, 255),
                Location = new Point(50, 30),
                AutoSize = true
            };
            
            var pnlScheduleInfo = new Panel
            {
                BackColor = Color.FromArgb(60, 60, 60),
                BorderStyle = BorderStyle.FixedSingle,
                Size = new Size(400, 150),
                Location = new Point(50, 100)
            };
            
            var lblScheduleInfo = new Label
            {
                Text = "📅 THÔNG TIN LỊCH PHÂN CA\n\nNhân viên: ComboBox chọn nhân viên\nNgày làm: DateTimePicker\nCa làm: ComboBox chọn ca (Sáng/Chiều/Đêm/Hành chính)\nTrạng thái: ComboBox (Dự kiến/Khóa/Hủy)",
                ForeColor = Color.White,
                Location = new Point(20, 20),
                Size = new Size(360, 110),
                Font = new Font("Segoe UI", 9)
            };
            pnlScheduleInfo.Controls.Add(lblScheduleInfo);
            
            var pnlButtons = new Panel
            {
                BackColor = Color.FromArgb(70, 70, 70),
                Size = new Size(400, 50),
                Location = new Point(50, 270)
            };
            
            var lblButtons = new Label
            {
                Text = "Buttons: Lưu | Hủy | Kiểm tra xung đột ca",
                ForeColor = Color.White,
                Location = new Point(20, 15),
                AutoSize = true,
                Font = new Font("Segoe UI", 9)
            };
            pnlButtons.Controls.Add(lblButtons);
            
            this.Controls.AddRange(new Control[] { lblPreview, pnlScheduleInfo, pnlButtons });
            
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion
    }
}
