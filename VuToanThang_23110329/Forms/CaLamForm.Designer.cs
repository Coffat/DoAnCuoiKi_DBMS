using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace VuToanThang_23110329.Forms
{
    partial class CaLamForm
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
            this.BackColor = Color.FromArgb(50, 50, 50);
            this.Size = new Size(1200, 800);
            this.Text = "Quản lý ca làm";
            this.Padding = new Padding(20);

            CreateControls();
            LayoutControls();
            SetupEventHandlers();
        }

        private void CreateControls()
        {
            // Title
            var lblTitle = new Label
            {
                Text = "QUẢN LÝ CA LÀM",
                Font = new Font("Segoe UI", 20, FontStyle.Bold),
                ForeColor = Color.FromArgb(124, 77, 255),
                AutoSize = true
            };

            // Search
            var lblSearch = new Label { Text = "Tìm kiếm:", ForeColor = Color.White, AutoSize = true };
            txtSearch = new TextBox { Width = 300, BackColor = Color.FromArgb(60, 60, 60), ForeColor = Color.White, BorderStyle = BorderStyle.FixedSingle };

            // Buttons
            btnThem = CreateButton("Thêm", Color.FromArgb(46, 125, 50));
            btnSua = CreateButton("Sửa", Color.FromArgb(255, 152, 0));
            btnXoa = CreateButton("Xóa", Color.FromArgb(244, 67, 54));
            btnLuu = CreateButton("Lưu", Color.FromArgb(33, 150, 243));
            btnHuy = CreateButton("Hủy", Color.FromArgb(158, 158, 158));
            btnLamMoi = CreateButton("Làm mới", Color.FromArgb(96, 125, 139));

            btnLuu.Enabled = false;
            btnHuy.Enabled = false;

            // DataGridView
            dgvCaLam = new DataGridView
            {
                BackgroundColor = Color.FromArgb(60, 60, 60),
                ForeColor = Color.White,
                BorderStyle = BorderStyle.None,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                ReadOnly = true,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            };

            dgvCaLam.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(124, 77, 255);
            dgvCaLam.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvCaLam.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10.5F, FontStyle.Bold);
            dgvCaLam.DefaultCellStyle.BackColor = Color.FromArgb(60, 60, 60);
            dgvCaLam.DefaultCellStyle.ForeColor = Color.White;
            dgvCaLam.DefaultCellStyle.SelectionBackColor = Color.FromArgb(124, 77, 255);

            // Information Panel
            pnlThongTin = new Panel
            {
                BackColor = Color.FromArgb(60, 60, 60),
                BorderStyle = BorderStyle.FixedSingle,
                Padding = new Padding(15)
            };

            CreateInfoControls();

            this.Controls.AddRange(new Control[] { lblTitle, lblSearch, txtSearch, btnThem, btnSua, btnXoa, btnLuu, btnHuy, btnLamMoi, dgvCaLam, pnlThongTin });
        }

        private void CreateInfoControls()
        {
            var lblThongTin = new Label
            {
                Text = "THÔNG TIN CA LÀM",
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                ForeColor = Color.FromArgb(124, 77, 255),
                AutoSize = true
            };

            txtTenCa = CreateTextBox();
            dtpGioBatDau = CreateTimePicker();
            dtpGioKetThuc = CreateTimePicker();
            txtHeSoCa = CreateTextBox();
            txtMoTa = CreateTextBox();
            txtMoTa.Multiline = true;
            txtMoTa.Height = 60;
            chkKichHoat = new CheckBox
            {
                Text = "Kích hoạt",
                ForeColor = Color.White,
                AutoSize = true,
                Checked = true
            };

            pnlThongTin.Controls.AddRange(new Control[] {
                lblThongTin,
                CreateLabel("Tên ca:"), txtTenCa,
                CreateLabel("Giờ bắt đầu:"), dtpGioBatDau,
                CreateLabel("Giờ kết thúc:"), dtpGioKetThuc,
                CreateLabel("Hệ số ca:"), txtHeSoCa,
                CreateLabel("Mô tả:"), txtMoTa,
                chkKichHoat
            });
        }

        private Button CreateButton(string text, Color backColor)
        {
            return new Button
            {
                Text = text,
                BackColor = backColor,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Size = new Size(80, 35),
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

        private DateTimePicker CreateTimePicker()
        {
            return new DateTimePicker
            {
                Format = DateTimePickerFormat.Time,
                ShowUpDown = true,
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
            this.Controls[0].Location = new Point(20, 20);

            // Search
            this.Controls[1].Location = new Point(20, 70);
            txtSearch.Location = new Point(100, 68);

            // Buttons
            int btnY = 110;
            btnThem.Location = new Point(20, btnY);
            btnSua.Location = new Point(110, btnY);
            btnXoa.Location = new Point(200, btnY);
            btnLuu.Location = new Point(290, btnY);
            btnHuy.Location = new Point(380, btnY);
            btnLamMoi.Location = new Point(470, btnY);

            // DataGridView
            dgvCaLam.Location = new Point(20, 160);
            dgvCaLam.Size = new Size(700, 400);

            // Information Panel
            pnlThongTin.Location = new Point(740, 160);
            pnlThongTin.Size = new Size(420, 400);

            LayoutInfoControls();
        }

        private void LayoutInfoControls()
        {
            int y = 10;
            int labelWidth = 100;
            int controlWidth = 250;
            int spacing = 40;

            var controls = pnlThongTin.Controls.Cast<Control>().ToArray();
            
            // Title
            controls[0].Location = new Point(10, y);
            y += 40;

            // Layout pairs of label and control
            for (int i = 1; i < controls.Length; i += 2)
            {
                if (i + 1 < controls.Length)
                {
                    controls[i].Location = new Point(10, y);
                    controls[i].Size = new Size(labelWidth, 20);
                    controls[i + 1].Location = new Point(120, y - 3);
                    controls[i + 1].Size = new Size(controlWidth, controls[i + 1] == txtMoTa ? 60 : 23);
                    y += controls[i + 1] == txtMoTa ? 70 : spacing;
                }
            }

            // Handle checkbox separately
            if (controls.Length > 0 && controls[controls.Length - 1] is CheckBox)
            {
                controls[controls.Length - 1].Location = new Point(10, y);
            }
        }

        #endregion
    }
}
