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
            this.Size = new Size(900, 650);
            this.Text = "Lịch phân ca";
            this.Padding = new Padding(20);
            this.MinimumSize = new Size(900, 650);

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


        #endregion
    }
}
