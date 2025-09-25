using System;
using System.Drawing;
using System.Windows.Forms;

namespace VuToanThang_23110329.Forms
{
    partial class TaoLichTuanForm
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
            this.Text = "Tạo lịch tuần";
            this.Size = new Size(1000, 700);
            this.BackColor = Color.FromArgb(50, 50, 50);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimumSize = new Size(1000, 700);
            
            // Add preview controls for Designer
            var lblPreview = new Label
            {
                Text = "TẠO LỊCH TUẦN\n(Designer Preview)",
                Font = new Font("Segoe UI", 18, FontStyle.Bold),
                ForeColor = Color.FromArgb(124, 77, 255),
                Location = new Point(50, 50),
                AutoSize = true
            };
            
            var pnlWeekSelector = new Panel
            {
                BackColor = Color.FromArgb(60, 60, 60),
                BorderStyle = BorderStyle.FixedSingle,
                Size = new Size(900, 80),
                Location = new Point(50, 120)
            };
            
            var lblWeekSelector = new Label
            {
                Text = "📅 Chọn tuần: Từ ngày | Đến ngày | Buttons: Tạo lịch | Lưu | Hủy",
                ForeColor = Color.White,
                Location = new Point(20, 30),
                AutoSize = true,
                Font = new Font("Segoe UI", 10)
            };
            pnlWeekSelector.Controls.Add(lblWeekSelector);
            
            var pnlWeekGrid = new Panel
            {
                BackColor = Color.FromArgb(60, 60, 60),
                BorderStyle = BorderStyle.FixedSingle,
                Size = new Size(900, 450),
                Location = new Point(50, 220)
            };
            
            var lblWeekGrid = new Label
            {
                Text = "📋 LỊCH TUẦN (DataGridView)\n\nColumns: Nhân viên | Thứ 2 | Thứ 3 | Thứ 4 | Thứ 5 | Thứ 6 | Thứ 7 | Chủ nhật\nRows: Danh sách nhân viên với dropdown chọn ca cho từng ngày\n\nTính năng: Tự động phân ca, Kiểm tra xung đột, Lưu lịch tuần",
                ForeColor = Color.White,
                Location = new Point(20, 50),
                Size = new Size(860, 350),
                Font = new Font("Segoe UI", 11)
            };
            pnlWeekGrid.Controls.Add(lblWeekGrid);
            
            this.Controls.AddRange(new Control[] { lblPreview, pnlWeekSelector, pnlWeekGrid });
            
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        // UI Controls
        private Label lblTitle, lblTuNgay, lblDenNgay;
        private DateTimePicker dtpTuNgay, dtpDenNgay;
        private DataGridView dgvLichTuan;
        private Button btnSave, btnCancel, btnLoadEmployees;
        private Panel pnlHeader, pnlButtons;

        private void CreateControls()
        {
            // Title
            lblTitle = new Label
            {
                Text = "TẠO LỊCH TUẦN",
                Font = new Font("Segoe UI", 18, FontStyle.Bold),
                ForeColor = Color.FromArgb(124, 77, 255),
                AutoSize = true
            };

            // Header Panel
            pnlHeader = new Panel
            {
                BackColor = Color.FromArgb(60, 60, 60),
                BorderStyle = BorderStyle.FixedSingle,
                Height = 80
            };

            // Date controls
            lblTuNgay = CreateLabel("Từ ngày:");
            dtpTuNgay = CreateDatePicker();
            
            lblDenNgay = CreateLabel("Đến ngày:");
            dtpDenNgay = CreateDatePicker();

            btnLoadEmployees = CreateButton("Tải danh sách NV", Color.FromArgb(33, 150, 243));
            btnLoadEmployees.Click += BtnLoadEmployees_Click;

            pnlHeader.Controls.AddRange(new Control[] { lblTuNgay, dtpTuNgay, lblDenNgay, dtpDenNgay, btnLoadEmployees });

            // DataGridView for weekly schedule
            dgvLichTuan = new DataGridView
            {
                BackgroundColor = Color.FromArgb(60, 60, 60),
                ForeColor = Color.White,
                BorderStyle = BorderStyle.None,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                SelectionMode = DataGridViewSelectionMode.CellSelect,
                MultiSelect = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                RowHeadersVisible = false
            };

            dgvLichTuan.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(124, 77, 255);
            dgvLichTuan.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvLichTuan.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dgvLichTuan.DefaultCellStyle.BackColor = Color.FromArgb(60, 60, 60);
            dgvLichTuan.DefaultCellStyle.ForeColor = Color.White;
            dgvLichTuan.DefaultCellStyle.SelectionBackColor = Color.FromArgb(124, 77, 255);

            // Button Panel
            pnlButtons = new Panel
            {
                Height = 60,
                Dock = DockStyle.Bottom
            };

            btnSave = CreateButton("Lưu lịch tuần", Color.FromArgb(46, 125, 50));
            btnCancel = CreateButton("Hủy", Color.FromArgb(158, 158, 158));

            btnSave.Click += btnSave_Click;
            btnCancel.Click += btnCancel_Click;

            pnlButtons.Controls.AddRange(new Control[] { btnSave, btnCancel });

            this.Controls.AddRange(new Control[] { lblTitle, pnlHeader, dgvLichTuan, pnlButtons });
        }

        private Button CreateButton(string text, Color backColor)
        {
            return new Button
            {
                Text = text,
                BackColor = backColor,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Size = new Size(150, 35),
                Font = new Font("Segoe UI", 9, FontStyle.Bold)
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
                Font = new Font("Segoe UI", 9)
            };
        }

        private void LayoutControls()
        {
            // Title
            lblTitle.Location = new Point(20, 20);

            // Header Panel
            pnlHeader.Location = new Point(20, 60);
            pnlHeader.Size = new Size(this.ClientSize.Width - 40, 80);

            // Layout header controls
            lblTuNgay.Location = new Point(10, 15);
            dtpTuNgay.Location = new Point(10, 35);
            dtpTuNgay.Size = new Size(120, 25);

            lblDenNgay.Location = new Point(150, 15);
            dtpDenNgay.Location = new Point(150, 35);
            dtpDenNgay.Size = new Size(120, 25);

            btnLoadEmployees.Location = new Point(290, 33);

            // DataGridView
            dgvLichTuan.Location = new Point(20, 160);
            dgvLichTuan.Size = new Size(this.ClientSize.Width - 40, this.ClientSize.Height - 240);

            // Button panel layout
            btnCancel.Location = new Point(this.ClientSize.Width - 180, 15);
            btnSave.Location = new Point(this.ClientSize.Width - 340, 15);
        }

        #endregion
    }
}
