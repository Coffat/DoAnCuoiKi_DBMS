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
            this.Text = "Chi tiết lịch phân ca";
            this.Size = new Size(500, 450);
            this.BackColor = Color.FromArgb(50, 50, 50);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            
            CreateControls();
            LayoutControls();
            
            this.SuspendLayout();
            this.ResumeLayout(false);
        }

        // UI Controls
        private Label lblTitle, lblNhanVien, lblCaLam, lblNgayLam, lblTrangThai, lblShiftInfo;
        private ComboBox cmbNhanVien, cmbCaLam, cmbTrangThai;
        private DateTimePicker dtpNgayLam;
        private Button btnLuu, btnHuy;
        private Panel pnlMain, pnlButtons;

        private void CreateControls()
        {
            // Title
            lblTitle = new Label
            {
                Text = "THÔNG TIN LỊCH PHÂN CA",
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                ForeColor = Color.FromArgb(124, 77, 255),
                AutoSize = true
            };

            // Main Panel
            pnlMain = new Panel
            {
                BackColor = Color.FromArgb(60, 60, 60),
                BorderStyle = BorderStyle.FixedSingle,
                Padding = new Padding(20)
            };

            // Labels
            lblNhanVien = CreateLabel("Nhân viên:");
            lblCaLam = CreateLabel("Ca làm:");
            lblNgayLam = CreateLabel("Ngày làm:");
            lblTrangThai = CreateLabel("Trạng thái:");

            // Shift info label (initially hidden)
            lblShiftInfo = new Label
            {
                ForeColor = Color.FromArgb(124, 77, 255),
                AutoSize = true,
                Font = new Font("Segoe UI", 8.5F, FontStyle.Italic),
                Visible = false
            };

            // Controls
            cmbNhanVien = CreateComboBox();
            cmbCaLam = CreateComboBox();
            cmbCaLam.SelectedIndexChanged += cmbCaLam_SelectedIndexChanged;
            
            dtpNgayLam = CreateDatePicker();
            
            cmbTrangThai = CreateComboBox();
            cmbTrangThai.Items.AddRange(new[] { "Mo", "Khoa" });
            cmbTrangThai.SelectedIndex = 0;

            // Add controls to main panel
            pnlMain.Controls.AddRange(new Control[] {
                lblNhanVien, cmbNhanVien,
                lblCaLam, cmbCaLam, lblShiftInfo,
                lblNgayLam, dtpNgayLam,
                lblTrangThai, cmbTrangThai
            });

            // Button Panel
            pnlButtons = new Panel
            {
                Height = 60,
                Dock = DockStyle.Bottom,
                BackColor = Color.FromArgb(50, 50, 50)
            };

            btnLuu = CreateButton("Lưu", Color.FromArgb(46, 125, 50));
            btnHuy = CreateButton("Hủy", Color.FromArgb(158, 158, 158));

            btnLuu.Click += btnLuu_Click;
            btnHuy.Click += btnHuy_Click;

            pnlButtons.Controls.AddRange(new Control[] { btnLuu, btnHuy });

            this.Controls.AddRange(new Control[] { lblTitle, pnlMain, pnlButtons });
        }

        private Button CreateButton(string text, Color backColor)
        {
            return new Button
            {
                Text = text,
                BackColor = backColor,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Size = new Size(100, 35),
                Font = new Font("Segoe UI", 9, FontStyle.Bold)
            };
        }

        private TextBox CreateTextBox()
        {
            return new TextBox
            {
                BackColor = Color.FromArgb(70, 70, 70),
                ForeColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                Font = new Font("Segoe UI", 9)
            };
        }

        private ComboBox CreateComboBox()
        {
            return new ComboBox
            {
                BackColor = Color.FromArgb(70, 70, 70),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                DropDownStyle = ComboBoxStyle.DropDownList,
                Font = new Font("Segoe UI", 9)
            };
        }

        private DateTimePicker CreateDatePicker()
        {
            return new DateTimePicker
            {
                Format = DateTimePickerFormat.Short,
                Font = new Font("Segoe UI", 9)
            };
        }

        private Label CreateLabel(string text)
        {
            return new Label
            {
                Text = text,
                ForeColor = Color.White,
                AutoSize = true,
                Font = new Font("Segoe UI", 9, FontStyle.Bold)
            };
        }

        private void LayoutControls()
        {
            // Title
            lblTitle.Location = new Point(20, 20);

            // Main Panel
            pnlMain.Location = new Point(20, 60);
            pnlMain.Size = new Size(this.ClientSize.Width - 40, this.ClientSize.Height - 140);

            // Layout controls in main panel
            int y = 20;
            int labelWidth = 100;
            int controlWidth = pnlMain.ClientSize.Width - labelWidth - 60;
            int spacing = 45;

            // Employee
            lblNhanVien.Location = new Point(20, y);
            cmbNhanVien.Location = new Point(labelWidth + 20, y - 3);
            cmbNhanVien.Size = new Size(controlWidth, 23);
            y += spacing;

            // Shift
            lblCaLam.Location = new Point(20, y);
            cmbCaLam.Location = new Point(labelWidth + 20, y - 3);
            cmbCaLam.Size = new Size(controlWidth, 23);
            y += 25;

            // Shift info
            lblShiftInfo.Location = new Point(labelWidth + 20, y);
            y += spacing;

            // Date
            lblNgayLam.Location = new Point(20, y);
            dtpNgayLam.Location = new Point(labelWidth + 20, y - 3);
            dtpNgayLam.Size = new Size(controlWidth, 23);
            y += spacing;

            // Status
            lblTrangThai.Location = new Point(20, y);
            cmbTrangThai.Location = new Point(labelWidth + 20, y - 3);
            cmbTrangThai.Size = new Size(controlWidth, 23);
            y += spacing;

            // Button panel layout
            btnHuy.Location = new Point(this.ClientSize.Width - 120, 15);
            btnLuu.Location = new Point(this.ClientSize.Width - 230, 15);
        }

        #endregion
    }
}
