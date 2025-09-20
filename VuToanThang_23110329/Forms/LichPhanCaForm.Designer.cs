using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace VuToanThang_23110329.Forms
{
    partial class LichPhanCaForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        // UI Controls
        private DataGridView dgvLichPhanCa;
        private DateTimePicker dtpTuNgay, dtpDenNgay, dtpNgayLam;
        private ComboBox cmbNhanVien, cmbCaLam, cmbTrangThai;
        private Button btnThem, btnSua, btnXoa, btnLuu, btnHuy, btnLamMoi, btnTimKiem, btnTaoLichTuan;
        private Panel pnlThongTin, pnlFilter;

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
            this.Size = new Size(1400, 900);
            this.Text = "Lịch phân ca";
            this.Padding = new Padding(20);
            this.MinimumSize = new Size(1400, 900);

            CreateControls();
            LayoutControls();
            this.Resize += LichPhanCaForm_Resize;
        }

        private void CreateControls()
        {
            // Title
            var lblTitle = new Label
            {
                Text = "LỊCH PHÂN CA",
                Font = new Font("Segoe UI", 20, FontStyle.Bold),
                ForeColor = Color.FromArgb(124, 77, 255),
                AutoSize = true
            };

            // Filter Panel
            pnlFilter = new Panel
            {
                BackColor = Color.FromArgb(60, 60, 60),
                BorderStyle = BorderStyle.FixedSingle,
                Height = 80
            };

            var lblTuNgay = CreateLabel("Từ ngày:");
            dtpTuNgay = CreateDatePicker();
            dtpTuNgay.Value = DateTime.Now.AddDays(-7);

            var lblDenNgay = CreateLabel("Đến ngày:");
            dtpDenNgay = CreateDatePicker();
            dtpDenNgay.Value = DateTime.Now.AddDays(7);

            btnTimKiem = CreateButton("Tìm kiếm", Color.FromArgb(33, 150, 243));
            btnTaoLichTuan = CreateButton("Tạo lịch tuần", Color.FromArgb(76, 175, 80));

            // Set up event handlers for filter buttons
            btnTimKiem.Click += btnTimKiem_Click;
            btnTaoLichTuan.Click += btnTaoLichTuan_Click;

            pnlFilter.Controls.AddRange(new Control[] { lblTuNgay, dtpTuNgay, lblDenNgay, dtpDenNgay, btnTimKiem, btnTaoLichTuan });

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
            dgvLichPhanCa = new DataGridView
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

            dgvLichPhanCa.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(124, 77, 255);
            dgvLichPhanCa.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvLichPhanCa.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10.5F, FontStyle.Bold);
            dgvLichPhanCa.DefaultCellStyle.BackColor = Color.FromArgb(60, 60, 60);
            dgvLichPhanCa.DefaultCellStyle.ForeColor = Color.White;
            dgvLichPhanCa.DefaultCellStyle.SelectionBackColor = Color.FromArgb(124, 77, 255);

            // Set up DataGridView event handler
            dgvLichPhanCa.SelectionChanged += dgvLichPhanCa_SelectionChanged;

            // Information Panel
            pnlThongTin = new Panel
            {
                BackColor = Color.FromArgb(60, 60, 60),
                BorderStyle = BorderStyle.FixedSingle,
                Padding = new Padding(15)
            };

            CreateInfoControls();

            this.Controls.AddRange(new Control[] { lblTitle, pnlFilter, btnThem, btnSua, btnXoa, btnLuu, btnHuy, btnLamMoi, dgvLichPhanCa, pnlThongTin });
        }

        private void CreateInfoControls()
        {
            var lblThongTin = new Label
            {
                Text = "THÔNG TIN LỊCH PHÂN CA",
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                ForeColor = Color.FromArgb(124, 77, 255),
                AutoSize = true
            };

            cmbNhanVien = CreateComboBox();
            cmbCaLam = CreateComboBox();
            dtpNgayLam = CreateDatePicker();
            cmbTrangThai = CreateComboBox();
            cmbTrangThai.Items.AddRange(new[] { "DuKien", "Khoa", "Huy" });
            cmbTrangThai.SelectedIndex = 0;

            pnlThongTin.Controls.AddRange(new Control[] {
                lblThongTin,
                CreateLabel("Nhân viên:"), cmbNhanVien,
                CreateLabel("Ca làm:"), cmbCaLam,
                CreateLabel("Ngày làm:"), dtpNgayLam,
                CreateLabel("Trạng thái:"), cmbTrangThai
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
                Font = new Font("Segoe UI", 9)
            };
        }

        private void LayoutControls()
        {
            PerformLayout();
        }

        private void LichPhanCaForm_Resize(object sender, EventArgs e)
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

            // Filter Panel
            pnlFilter.Location = new Point(20, 70);
            pnlFilter.Size = new Size(Math.Min(1340, formWidth), 80);

            // Layout filter controls
            var filterControls = pnlFilter.Controls.Cast<Control>().ToArray();
            filterControls[0].Location = new Point(10, 15); // lblTuNgay
            dtpTuNgay.Location = new Point(10, 35);
            dtpTuNgay.Size = new Size(120, 25);

            filterControls[2].Location = new Point(150, 15); // lblDenNgay
            dtpDenNgay.Location = new Point(150, 35);
            dtpDenNgay.Size = new Size(120, 25);

            btnTimKiem.Location = new Point(290, 33);
            btnTaoLichTuan.Location = new Point(400, 33);

            // Buttons
            int btnY = 170;
            int btnSpacing = 110;
            btnThem.Location = new Point(20, btnY);
            btnSua.Location = new Point(20 + btnSpacing, btnY);
            btnXoa.Location = new Point(20 + btnSpacing * 2, btnY);
            btnLuu.Location = new Point(20 + btnSpacing * 3, btnY);
            btnHuy.Location = new Point(20 + btnSpacing * 4, btnY);
            btnLamMoi.Location = new Point(20 + btnSpacing * 5, btnY);

            // Calculate available space for grid and panel
            int availableWidth = formWidth;
            int availableHeight = formHeight - 220; // Space above controls
            
            // Responsive layout: stack vertically on small screens, side-by-side on larger screens
            if (formWidth < 1200) // Vertical layout for smaller screens
            {
                // DataGridView takes full width, 60% height
                dgvLichPhanCa.Location = new Point(20, 220);
                dgvLichPhanCa.Size = new Size(availableWidth, (int)(availableHeight * 0.6));

                // Information Panel below the grid
                pnlThongTin.Location = new Point(20, 220 + (int)(availableHeight * 0.6) + 10);
                pnlThongTin.Size = new Size(availableWidth, (int)(availableHeight * 0.4) - 10);
            }
            else // Horizontal layout for larger screens
            {
                // DataGridView takes 65% of width
                int gridWidth = (int)(availableWidth * 0.65);
                dgvLichPhanCa.Location = new Point(20, 220);
                dgvLichPhanCa.Size = new Size(gridWidth, availableHeight);

                // Information Panel takes remaining 35% minus margin
                int panelWidth = availableWidth - gridWidth - 20;
                pnlThongTin.Location = new Point(20 + gridWidth + 20, 220);
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
                    controls[i + 1].Location = new Point(labelWidth + 20, y - 3);
                    
                    controls[i + 1].Size = new Size(controlWidth, 23);
                    y += spacing;
                }
            }
        }

        #endregion
    }
}
