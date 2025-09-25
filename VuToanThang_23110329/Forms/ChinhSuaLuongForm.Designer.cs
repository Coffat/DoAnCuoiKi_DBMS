using System;
using System.Drawing;
using System.Windows.Forms;

namespace VuToanThang_23110329.Forms
{
    partial class ChinhSuaLuongForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        // UI Controls
        private NumericUpDown nudPhuCap, nudKhauTru, nudThueBH;
        private Label lblThucLanh;
        private Button btnSave, btnCancel;

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
            this.BackColor = Color.FromArgb(50, 50, 50);
            this.Size = new Size(500, 400);
            this.Text = "Chỉnh sửa lương";
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.StartPosition = FormStartPosition.CenterParent;

            // Add preview controls for Designer
            var lblPreview = new Label
            {
                Text = "CHỈNH SỬA LƯƠNG\n(Designer Preview)",
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                ForeColor = Color.FromArgb(124, 77, 255),
                Location = new Point(50, 30),
                AutoSize = true
            };
            
            var pnlEmployeeInfo = new Panel
            {
                BackColor = Color.FromArgb(60, 60, 60),
                BorderStyle = BorderStyle.FixedSingle,
                Size = new Size(400, 80),
                Location = new Point(50, 100)
            };
            
            var lblEmployeeInfo = new Label
            {
                Text = "👤 THÔNG TIN NHÂN VIÊN\n\nNhân viên: [Tên] | Chức danh: [Position]\nLương cơ bản: [Amount] | Giờ công: [Hours]",
                ForeColor = Color.White,
                Location = new Point(20, 15),
                Size = new Size(360, 50),
                Font = new Font("Segoe UI", 9)
            };
            pnlEmployeeInfo.Controls.Add(lblEmployeeInfo);
            
            var pnlEditSalary = new Panel
            {
                BackColor = Color.FromArgb(60, 60, 60),
                BorderStyle = BorderStyle.FixedSingle,
                Size = new Size(400, 100),
                Location = new Point(50, 200)
            };
            
            var lblEditSalary = new Label
            {
                Text = "💰 CHỈNH SỬA LƯƠNG\n\nPhụ cấp: NumericUpDown | Khấu trừ: NumericUpDown\nThuế BH: NumericUpDown | ➤ Thực lãnh: [Calculated]",
                ForeColor = Color.White,
                Location = new Point(20, 20),
                Size = new Size(360, 60),
                Font = new Font("Segoe UI", 9)
            };
            pnlEditSalary.Controls.Add(lblEditSalary);
            
            var pnlButtons = new Panel
            {
                BackColor = Color.FromArgb(70, 70, 70),
                Size = new Size(400, 50),
                Location = new Point(50, 320)
            };
            
            var lblButtons = new Label
            {
                Text = "Buttons: Lưu (cập nhật database) | Hủy (đóng form)",
                ForeColor = Color.White,
                Location = new Point(20, 15),
                AutoSize = true,
                Font = new Font("Segoe UI", 9)
            };
            pnlButtons.Controls.Add(lblButtons);
            
            this.Controls.AddRange(new Control[] { lblPreview, pnlEmployeeInfo, pnlEditSalary, pnlButtons });

            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion
    }
}
