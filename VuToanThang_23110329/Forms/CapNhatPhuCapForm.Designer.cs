using System;
using System.Drawing;
using System.Windows.Forms;

namespace VuToanThang_23110329.Forms
{
    partial class CapNhatPhuCapForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        // UI Controls
        private DataGridView dgvNhanVien;
        private NumericUpDown nudPhuCap, nudKhauTru, nudThueBH;
        private Button btnApplyAll, btnApplySelected;

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
            this.Size = new Size(800, 600);
            this.Text = "Cập nhật phụ cấp";
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.StartPosition = FormStartPosition.CenterParent;

            // Add preview controls for Designer
            var lblPreview = new Label
            {
                Text = "CẬP NHẬT PHỤ CẤP/KHẤU TRỪ\n(Designer Preview)",
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                ForeColor = Color.FromArgb(124, 77, 255),
                Location = new Point(50, 50),
                AutoSize = true
            };
            
            var pnlInputPreview = new Panel
            {
                BackColor = Color.FromArgb(60, 60, 60),
                BorderStyle = BorderStyle.FixedSingle,
                Size = new Size(700, 100),
                Location = new Point(50, 120)
            };
            
            var lblInputPreview = new Label
            {
                Text = "💰 NHẬP THÔNG TIN CẬP NHẬT\n\nPhụ cấp: NumericUpDown | Khấu trừ: NumericUpDown | Thuế BH: NumericUpDown\nButtons: Áp dụng tất cả | Áp dụng đã chọn",
                ForeColor = Color.White,
                Location = new Point(20, 20),
                Size = new Size(660, 60),
                Font = new Font("Segoe UI", 10)
            };
            pnlInputPreview.Controls.Add(lblInputPreview);
            
            var pnlDataPreview = new Panel
            {
                BackColor = Color.FromArgb(60, 60, 60),
                BorderStyle = BorderStyle.FixedSingle,
                Size = new Size(700, 300),
                Location = new Point(50, 240)
            };
            
            var lblDataPreview = new Label
            {
                Text = "📋 DANH SÁCH NHÂN VIÊN (DataGridView)\n\nColumns: ☑️ | Mã NV | Họ tên | Chức danh | Lương CB | Phụ cấp hiện tại | Khấu trừ hiện tại | Thuế BH hiện tại\n\nTính năng:\n- Chọn nhiều nhân viên để cập nhật hàng loạt\n- Xem thông tin lương hiện tại\n- Áp dụng giá trị mới cho tất cả hoặc nhân viên được chọn\n- Tự động tính toán và cập nhật database",
                ForeColor = Color.White,
                Location = new Point(20, 30),
                Size = new Size(660, 240),
                Font = new Font("Segoe UI", 10)
            };
            pnlDataPreview.Controls.Add(lblDataPreview);
            
            var pnlButtonsPreview = new Panel
            {
                BackColor = Color.FromArgb(70, 70, 70),
                Size = new Size(700, 50),
                Location = new Point(50, 560)
            };
            
            var lblButtonsPreview = new Label
            {
                Text = "Buttons: Đóng | Làm mới dữ liệu",
                ForeColor = Color.White,
                Location = new Point(20, 15),
                AutoSize = true,
                Font = new Font("Segoe UI", 10)
            };
            pnlButtonsPreview.Controls.Add(lblButtonsPreview);
            
            this.Controls.AddRange(new Control[] { lblPreview, pnlInputPreview, pnlDataPreview, pnlButtonsPreview });

            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion
    }
}
