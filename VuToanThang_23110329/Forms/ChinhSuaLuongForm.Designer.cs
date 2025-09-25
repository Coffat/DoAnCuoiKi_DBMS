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
            this.BackColor = Color.FromArgb(50, 50, 50);
            this.Size = new Size(500, 400);
            this.Text = "Chỉnh sửa lương";
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.StartPosition = FormStartPosition.CenterParent;

            CreateControls();
            this.SuspendLayout();
            this.ResumeLayout(false);
        }

        private void CreateControls()
        {
            var lblTitle = new Label
            {
                Text = "CHỈNH SỬA LƯƠNG",
                ForeColor = Color.FromArgb(124, 77, 255),
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                Location = new Point(20, 20),
                AutoSize = true
            };

            // Employee info panel
            var pnlInfo = new Panel
            {
                BackColor = Color.FromArgb(60, 60, 60),
                BorderStyle = BorderStyle.FixedSingle,
                Size = new Size(460, 100),
                Location = new Point(20, 60)
            };

            var lblNhanVien = new Label { Text = "Nhân viên:", ForeColor = Color.White, Location = new Point(20, 15), AutoSize = true };
            var lblChucDanh = new Label { Text = "Chức danh:", ForeColor = Color.White, Location = new Point(20, 35), AutoSize = true };
            var lblLuongCB = new Label { Text = "Lương cơ bản:", ForeColor = Color.White, Location = new Point(20, 55), AutoSize = true };
            var lblGioCong = new Label { Text = "Giờ công:", ForeColor = Color.White, Location = new Point(250, 55), AutoSize = true };

            pnlInfo.Controls.AddRange(new Control[] { lblNhanVien, lblChucDanh, lblLuongCB, lblGioCong });

            // Edit panel
            var pnlEdit = new Panel
            {
                BackColor = Color.FromArgb(60, 60, 60),
                BorderStyle = BorderStyle.FixedSingle,
                Size = new Size(460, 120),
                Location = new Point(20, 180)
            };

            var lblPhuCap = new Label { Text = "Phụ cấp:", ForeColor = Color.White, Location = new Point(20, 20), AutoSize = true };
            nudPhuCap = new NumericUpDown
            {
                Maximum = 10000000,
                DecimalPlaces = 0,
                Increment = 50000,
                BackColor = Color.FromArgb(70, 70, 70),
                ForeColor = Color.White,
                Size = new Size(120, 25),
                Location = new Point(100, 17)
            };

            var lblKhauTru = new Label { Text = "Khấu trừ:", ForeColor = Color.White, Location = new Point(240, 20), AutoSize = true };
            nudKhauTru = new NumericUpDown
            {
                Maximum = 10000000,
                DecimalPlaces = 0,
                Increment = 50000,
                BackColor = Color.FromArgb(70, 70, 70),
                ForeColor = Color.White,
                Size = new Size(120, 25),
                Location = new Point(320, 17)
            };

            var lblThueBH = new Label { Text = "Thuế BH:", ForeColor = Color.White, Location = new Point(20, 50), AutoSize = true };
            nudThueBH = new NumericUpDown
            {
                Maximum = 10000000,
                DecimalPlaces = 0,
                Increment = 10000,
                BackColor = Color.FromArgb(70, 70, 70),
                ForeColor = Color.White,
                Size = new Size(120, 25),
                Location = new Point(100, 47)
            };

            lblThucLanh = new Label
            {
                Text = "Thực lãnh: 0 VNĐ",
                ForeColor = Color.FromArgb(124, 77, 255),
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                Location = new Point(240, 50),
                AutoSize = true
            };

            pnlEdit.Controls.AddRange(new Control[] { lblPhuCap, nudPhuCap, lblKhauTru, nudKhauTru, lblThueBH, nudThueBH, lblThucLanh });

            // Buttons
            btnSave = new Button
            {
                Text = "Lưu",
                BackColor = Color.FromArgb(124, 77, 255),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Size = new Size(100, 35),
                Location = new Point(280, 320),
                DialogResult = DialogResult.OK,
                Name = "btnSave"
            };

            btnCancel = new Button
            {
                Text = "Hủy",
                BackColor = Color.FromArgb(80, 80, 80),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Size = new Size(100, 35),
                Location = new Point(390, 320),
                DialogResult = DialogResult.Cancel,
                Name = "btnCancel"
            };

            this.Controls.AddRange(new Control[] { lblTitle, pnlInfo, pnlEdit, btnSave, btnCancel });
        }

        #endregion
    }
}
