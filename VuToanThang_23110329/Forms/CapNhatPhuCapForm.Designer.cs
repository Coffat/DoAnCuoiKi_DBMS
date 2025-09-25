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

        private void CreateControls()
        {
            var lblTitle = new Label
            {
                Text = "CẬP NHẬT PHỤ CẤP/KHẤU TRỪ",
                ForeColor = Color.FromArgb(124, 77, 255),
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                Location = new Point(20, 20),
                AutoSize = true
            };

            // Input panel
            var pnlInput = new Panel
            {
                BackColor = Color.FromArgb(60, 60, 60),
                BorderStyle = BorderStyle.FixedSingle,
                Size = new Size(760, 80),
                Location = new Point(20, 60)
            };

            var lblPhuCap = new Label { Text = "Phụ cấp:", ForeColor = Color.White, Location = new Point(20, 20), AutoSize = true };
            nudPhuCap = new NumericUpDown
            {
                Maximum = 10000000,
                DecimalPlaces = 0,
                Increment = 50000,
                BackColor = Color.FromArgb(70, 70, 70),
                ForeColor = Color.White,
                Size = new Size(100, 25),
                Location = new Point(20, 40)
            };

            var lblKhauTru = new Label { Text = "Khấu trừ:", ForeColor = Color.White, Location = new Point(150, 20), AutoSize = true };
            nudKhauTru = new NumericUpDown
            {
                Maximum = 10000000,
                DecimalPlaces = 0,
                Increment = 50000,
                BackColor = Color.FromArgb(70, 70, 70),
                ForeColor = Color.White,
                Size = new Size(100, 25),
                Location = new Point(150, 40)
            };

            var lblThueBH = new Label { Text = "Thuế BH:", ForeColor = Color.White, Location = new Point(280, 20), AutoSize = true };
            nudThueBH = new NumericUpDown
            {
                Maximum = 10000000,
                DecimalPlaces = 0,
                Increment = 10000,
                BackColor = Color.FromArgb(70, 70, 70),
                ForeColor = Color.White,
                Size = new Size(100, 25),
                Location = new Point(280, 40)
            };

            btnApplyAll = new Button
            {
                Text = "Áp dụng tất cả",
                BackColor = Color.FromArgb(124, 77, 255),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Size = new Size(120, 30),
                Location = new Point(420, 25),
                Name = "btnApplyAll"
            };

            btnApplySelected = new Button
            {
                Text = "Áp dụng đã chọn",
                BackColor = Color.FromArgb(80, 160, 80),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Size = new Size(120, 30),
                Location = new Point(550, 25),
                Name = "btnApplySelected"
            };

            pnlInput.Controls.AddRange(new Control[] { lblPhuCap, nudPhuCap, lblKhauTru, nudKhauTru, lblThueBH, nudThueBH, btnApplyAll, btnApplySelected });

            // DataGridView
            dgvNhanVien = new DataGridView
            {
                BackgroundColor = Color.FromArgb(60, 60, 60),
                ForeColor = Color.White,
                GridColor = Color.FromArgb(80, 80, 80),
                BorderStyle = BorderStyle.None,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = true,
                Size = new Size(760, 400),
                Location = new Point(20, 160),
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            };

            // Close button
            var btnClose = new Button
            {
                Text = "Đóng",
                BackColor = Color.FromArgb(80, 80, 80),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Size = new Size(100, 35),
                Location = new Point(680, 570),
                DialogResult = DialogResult.OK
            };

            this.Controls.AddRange(new Control[] { lblTitle, pnlInput, dgvNhanVien, btnClose });
        }

        #endregion
    }
}
