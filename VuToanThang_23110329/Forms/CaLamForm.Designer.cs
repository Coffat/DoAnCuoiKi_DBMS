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
            this.Dock = DockStyle.Fill;
            this.Text = "Quản lý ca làm";
            this.Padding = new Padding(20);
            this.MinimumSize = new Size(800, 600);

            CreateControls();
            LayoutControls();
            this.Resize += CaLamForm_Resize;
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

            // Set up button event handlers immediately after button creation
            btnThem.Click += btnThem_Click;
            btnSua.Click += btnSua_Click;
            btnXoa.Click += btnXoa_Click;
            btnLuu.Click += btnLuu_Click;
            btnHuy.Click += btnHuy_Click;
            btnLamMoi.Click += btnLamMoi_Click;

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
            txtMoTa = CreateMultilineTextBox();
            chkKichHoat = CreateCheckBox();

            pnlThongTin.Controls.AddRange(new Control[] {
                lblThongTin,
                CreateLabel("Tên ca:"), txtTenCa,
                CreateLabel("Giờ bắt đầu:"), dtpGioBatDau,
                CreateLabel("Giờ kết thúc:"), dtpGioKetThuc,
                CreateLabel("Hệ số ca:"), txtHeSoCa,
                CreateLabel("Mô tả:"), txtMoTa,
                CreateLabel("Kích hoạt:"), chkKichHoat
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
        
        private TextBox CreateMultilineTextBox()
        {
            return new TextBox
            {
                BackColor = Color.FromArgb(70, 70, 70),
                ForeColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                Font = new Font("Segoe UI", 9),
                Multiline = true,
                ScrollBars = ScrollBars.Vertical
            };
        }
        
        private CheckBox CreateCheckBox()
        {
            return new CheckBox
            {
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 9),
                Checked = true
            };
        }

        private void LayoutControls()
        {
            PerformLayout();
        }

        private void CaLamForm_Resize(object sender, EventArgs e)
        {
            PerformLayout();
        }

        private void PerformLayout()
        {
            if (this.Controls.Count == 0) return;

            int formWidth = this.ClientSize.Width - 40; // Account for padding
            int formHeight = this.ClientSize.Height - 40;

            // Title
            this.Controls[0].Location = new Point(20, 20);

            // Search
            this.Controls[1].Location = new Point(20, 70);
            txtSearch.Location = new Point(100, 68);
            txtSearch.Width = Math.Min(300, formWidth - 120);

            // Buttons - make them responsive
            int btnY = 110;
            int btnSpacing = 90;
            btnThem.Location = new Point(20, btnY);
            btnSua.Location = new Point(20 + btnSpacing, btnY);
            btnXoa.Location = new Point(20 + btnSpacing * 2, btnY);
            btnLuu.Location = new Point(20 + btnSpacing * 3, btnY);
            btnHuy.Location = new Point(20 + btnSpacing * 4, btnY);
            btnLamMoi.Location = new Point(20 + btnSpacing * 5, btnY);

            // Calculate available space for grid and panel
            int availableWidth = formWidth;
            int availableHeight = formHeight - 160; // Space above controls
            
            // Responsive layout: stack vertically on small screens, side-by-side on larger screens
            if (formWidth < 900) // Vertical layout for smaller screens
            {
                // DataGridView takes full width, half height
                dgvCaLam.Location = new Point(20, 160);
                dgvCaLam.Size = new Size(availableWidth, availableHeight / 2 - 10);

                // Information Panel below the grid
                pnlThongTin.Location = new Point(20, 160 + availableHeight / 2);
                pnlThongTin.Size = new Size(availableWidth, availableHeight / 2 - 10);
            }
            else // Horizontal layout for larger screens
            {
                // DataGridView takes 60% of width
                int gridWidth = (int)(availableWidth * 0.6);
                dgvCaLam.Location = new Point(20, 160);
                dgvCaLam.Size = new Size(gridWidth, availableHeight);

                // Information Panel takes remaining 40% minus margin
                int panelWidth = availableWidth - gridWidth - 20;
                pnlThongTin.Location = new Point(20 + gridWidth + 20, 160);
                pnlThongTin.Size = new Size(panelWidth, availableHeight);
            }

            LayoutInfoControls();
        }

        private void LayoutInfoControls()
        {
            if (pnlThongTin.Controls.Count == 0) return;

            int y = 10;
            int panelWidth = pnlThongTin.ClientSize.Width - 30; // Account for padding
            int labelWidth = Math.Min(100, panelWidth / 3);
            int controlWidth = Math.Max(150, panelWidth - labelWidth - 20);
            int spacing = 50; // Tăng khoảng cách để chửa các control mới

            var controls = pnlThongTin.Controls.Cast<Control>().ToArray();
            
            // Title
            controls[0].Location = new Point(10, y);
            y += 50;

            // Layout pairs of label and control
            for (int i = 1; i < controls.Length; i += 2)
            {
                if (i + 1 < controls.Length)
                {
                    controls[i].Location = new Point(10, y);
                    controls[i].Size = new Size(labelWidth, 20);
                    
                    // Đặc biệt xử lý cho TextBox mô tả (multiline)
                    if (controls[i + 1] == txtMoTa)
                    {
                        controls[i + 1].Location = new Point(labelWidth + 15, y - 3);
                        controls[i + 1].Size = new Size(controlWidth, 60); // Cao hơn cho multiline
                        y += 80; // Khoảng cách lớn hơn
                    }
                    else
                    {
                        controls[i + 1].Location = new Point(labelWidth + 15, y - 3);
                        controls[i + 1].Size = new Size(controlWidth, 23);
                        y += spacing;
                    }
                }
            }
        }

        #endregion
    }
}
