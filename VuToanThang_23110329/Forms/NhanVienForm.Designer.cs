using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using VuToanThang_23110329.Controls;

namespace VuToanThang_23110329.Forms
{
    partial class NhanVienForm
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
            this.Size = new Size(1400, 900);
            this.Text = "Quản lý nhân viên";
            this.Padding = new Padding(20);

            CreateControls();
            LayoutControls();
        }

        private void CreateControls()
        {
            // Title
            var lblTitle = new Label
            {
                Text = "QUẢN LÝ NHÂN VIÊN",
                Font = new Font("Segoe UI", 20, FontStyle.Bold),
                ForeColor = Color.FromArgb(124, 77, 255),
                AutoSize = true
            };

            // Search
            var lblSearch = new Label { Text = "Tìm kiếm:", ForeColor = Color.White, AutoSize = true };
            txtSearch = new TextBox { Width = 300, BackColor = Color.FromArgb(60, 60, 60), ForeColor = Color.White, BorderStyle = BorderStyle.FixedSingle };

            // Status Filter
            var lblFilter = new Label { Text = "Lọc:", ForeColor = Color.White, AutoSize = true };
            cmbFilterTrangThai = CreateComboBox(new[] { "Tất cả", "DangLam", "Nghi" });
            cmbFilterTrangThai.SelectedIndex = 0;

            // Modern Buttons
            btnThem = ButtonFactory.CreateSuccessButton("Thêm");
            btnSua = ButtonFactory.CreateWarningButton("Sửa");
            btnXoa = ButtonFactory.CreateDangerButton("Xóa");
            btnKhoiPhuc = ButtonFactory.CreateInfoButton("Khôi phục");
            btnLamMoi = ButtonFactory.CreateSecondaryButton("Làm mới");

            // DataGridView
            dgvNhanVien = new DataGridView
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

            dgvNhanVien.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(124, 77, 255);
            dgvNhanVien.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvNhanVien.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10.5F, FontStyle.Bold);
            dgvNhanVien.DefaultCellStyle.BackColor = Color.FromArgb(60, 60, 60);
            dgvNhanVien.DefaultCellStyle.ForeColor = Color.White;
            dgvNhanVien.DefaultCellStyle.SelectionBackColor = Color.FromArgb(124, 77, 255);

            this.Controls.AddRange(new Control[] { lblTitle, lblSearch, txtSearch, lblFilter, cmbFilterTrangThai, btnThem, btnSua, btnXoa, btnKhoiPhuc, btnLamMoi, dgvNhanVien });
        }

        private void CreateInfoControls()
        {
            var lblThongTin = new Label
            {
                Text = "THÔNG TIN NHÂN VIÊN",
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                ForeColor = Color.FromArgb(124, 77, 255),
                AutoSize = true
            };

            // Personal Info
            txtHoTen = CreateTextBox();
            dtpNgaySinh = CreateDateTimePicker();
            cmbGioiTinh = CreateComboBox(new[] { "Nam", "Nữ", "Khác" });
            txtDienThoai = CreateTextBox();
            txtEmail = CreateTextBox();
            txtDiaChi = CreateTextBox();

            // Work Info
            dtpNgayVaoLam = CreateDateTimePicker();
            cmbTrangThai = CreateComboBox(new[] { "DangLam", "Nghi" });
            txtPhongBan = CreateTextBox();
            txtChucDanh = CreateTextBox();
            txtLuongCoBan = CreateTextBox();

            // Account Info
            chkTaoTaiKhoan = new CheckBox
            {
                Text = "Tạo tài khoản đăng nhập",
                ForeColor = Color.White,
                AutoSize = true
            };
            txtTenDangNhap = CreateTextBox();
            txtMatKhau = CreateTextBox();
            txtMatKhau.UseSystemPasswordChar = true;
            cmbVaiTro = CreateComboBox(new[] { "NhanVien", "HR", "QuanLy", "KeToan" });

            txtTenDangNhap.Enabled = false;
            txtMatKhau.Enabled = false;
            cmbVaiTro.Enabled = false;

            pnlThongTin.Controls.AddRange(new Control[] {
                lblThongTin,
                CreateLabel("Họ tên:"), txtHoTen,
                CreateLabel("Ngày sinh:"), dtpNgaySinh,
                CreateLabel("Giới tính:"), cmbGioiTinh,
                CreateLabel("Điện thoại:"), txtDienThoai,
                CreateLabel("Email:"), txtEmail,
                CreateLabel("Địa chỉ:"), txtDiaChi,
                CreateLabel("Ngày vào làm:"), dtpNgayVaoLam,
                CreateLabel("Trạng thái:"), cmbTrangThai,
                CreateLabel("Phòng ban:"), txtPhongBan,
                CreateLabel("Chức danh:"), txtChucDanh,
                CreateLabel("Lương cơ bản:"), txtLuongCoBan,
                chkTaoTaiKhoan,
                CreateLabel("Tên đăng nhập:"), txtTenDangNhap,
                CreateLabel("Mật khẩu:"), txtMatKhau,
                CreateLabel("Vai trò:"), cmbVaiTro
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

        private ComboBox CreateComboBox(string[] items)
        {
            var cmb = new ComboBox
            {
                BackColor = Color.FromArgb(70, 70, 70),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                DropDownStyle = ComboBoxStyle.DropDownList,
                Font = new Font("Segoe UI", 9)
            };
            cmb.Items.AddRange(items);
            return cmb;
        }

        private DateTimePicker CreateDateTimePicker()
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
            this.Controls[0].Location = new Point(20, 20);

            // Search
            this.Controls[1].Location = new Point(20, 70);
            txtSearch.Location = new Point(100, 68);

            // Status Filter
            this.Controls[3].Location = new Point(420, 70); // lblFilter
            cmbFilterTrangThai.Location = new Point(460, 68);

            // Buttons
            int btnY = 110;
            btnThem.Location = new Point(20, btnY);
            btnSua.Location = new Point(110, btnY);
            btnXoa.Location = new Point(200, btnY);
            btnKhoiPhuc.Location = new Point(290, btnY);
            btnLamMoi.Location = new Point(380, btnY);

            // DataGridView - expanded to use full width
            dgvNhanVien.Location = new Point(20, 160);
            dgvNhanVien.Size = new Size(this.Width - 60, this.Height - 200);
            dgvNhanVien.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        }

        private void LayoutInfoControls()
        {
            int y = 10;
            int labelWidth = 120;
            int controlWidth = 200;
            int spacing = 35;

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
                    controls[i + 1].Location = new Point(140, y - 3);
                    controls[i + 1].Size = new Size(controlWidth, 23);
                    y += spacing;
                }
            }
        }

        #endregion
    }
}
