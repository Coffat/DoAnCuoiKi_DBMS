using System;
using System.Drawing;
using System.Windows.Forms;
using VuToanThang_23110329.Controls;

namespace VuToanThang_23110329.Forms
{
    partial class NhanVienDetailForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.BackColor = Color.FromArgb(50, 50, 50);
            this.ForeColor = Color.White;
            this.Size = new Size(500, 700);
            this.Text = _isEditMode ? "Chỉnh sửa nhân viên" : "Thêm nhân viên mới";
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.ShowInTaskbar = false;
            this.Font = new Font("Segoe UI", 9);

            CreateControls();
            LayoutControls();
        }

        private void CreateControls()
        {
            // Title
            var lblTitle = new Label
            {
                Text = _isEditMode ? "CHỈNH SỬA NHÂN VIÊN" : "THÊM NHÂN VIÊN MỚI",
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                ForeColor = Color.FromArgb(124, 77, 255),
                AutoSize = true
            };

            // Personal Information Section
            var lblPersonal = new Label
            {
                Text = "THÔNG TIN CÁ NHÂN",
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                ForeColor = Color.FromArgb(124, 77, 255),
                AutoSize = true
            };

            txtHoTen = CreateTextBox();
            dtpNgaySinh = CreateDateTimePicker();
            cmbGioiTinh = CreateComboBox(new[] { "Nam", "Nữ", "Khác" });
            txtDienThoai = CreateTextBox();
            txtEmail = CreateTextBox();
            txtDiaChi = CreateTextBox();

            // Work Information Section
            var lblWork = new Label
            {
                Text = "THÔNG TIN CÔNG VIỆC",
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                ForeColor = Color.FromArgb(124, 77, 255),
                AutoSize = true
            };

            dtpNgayVaoLam = CreateDateTimePicker();
            cmbTrangThai = CreateComboBox(new[] { "DangLam", "Nghi" });
            txtPhongBan = CreateTextBox();
            txtChucDanh = CreateTextBox();
            txtLuongCoBan = CreateTextBox();

            // Account Information Section (only for add mode)
            var lblAccount = new Label
            {
                Text = "THÔNG TIN TÀI KHOẢN",
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                ForeColor = Color.FromArgb(124, 77, 255),
                AutoSize = true
            };

            chkTaoTaiKhoan = new CheckBox
            {
                Text = "Tạo tài khoản đăng nhập",
                ForeColor = Color.White,
                AutoSize = true,
                Font = new Font("Segoe UI", 9)
            };

            txtTenDangNhap = CreateTextBox();
            txtMatKhau = CreateTextBox();
            txtMatKhau.UseSystemPasswordChar = true;
            cmbVaiTro = CreateComboBox(new[] { "NhanVien", "HR", "QuanLy", "KeToan" });

            // Modern Buttons
            btnLuu = ButtonFactory.CreateSuccessButton("Lưu", ButtonFactory.LargeSize);
            btnHuy = ButtonFactory.CreateSecondaryButton("Hủy", ButtonFactory.LargeSize);

            // Add all controls to form
            this.Controls.AddRange(new Control[] {
                lblTitle,
                lblPersonal,
                CreateLabel("Họ tên:"), txtHoTen,
                CreateLabel("Ngày sinh:"), dtpNgaySinh,
                CreateLabel("Giới tính:"), cmbGioiTinh,
                CreateLabel("Điện thoại:"), txtDienThoai,
                CreateLabel("Email:"), txtEmail,
                CreateLabel("Địa chỉ:"), txtDiaChi,
                lblWork,
                CreateLabel("Ngày vào làm:"), dtpNgayVaoLam,
                CreateLabel("Trạng thái:"), cmbTrangThai,
                CreateLabel("Phòng ban:"), txtPhongBan,
                CreateLabel("Chức danh:"), txtChucDanh,
                CreateLabel("Lương cơ bản:"), txtLuongCoBan,
                lblAccount,
                chkTaoTaiKhoan,
                CreateLabel("Tên đăng nhập:"), txtTenDangNhap,
                CreateLabel("Mật khẩu:"), txtMatKhau,
                CreateLabel("Vai trò:"), cmbVaiTro,
                btnLuu, btnHuy
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
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                UseVisualStyleBackColor = false
            };
        }

        private TextBox CreateTextBox()
        {
            return new TextBox
            {
                BackColor = Color.FromArgb(70, 70, 70),
                ForeColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                Font = new Font("Segoe UI", 9),
                Size = new Size(250, 23)
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
                Font = new Font("Segoe UI", 9),
                Size = new Size(250, 23)
            };
            cmb.Items.AddRange(items);
            return cmb;
        }

        private DateTimePicker CreateDateTimePicker()
        {
            return new DateTimePicker
            {
                Format = DateTimePickerFormat.Short,
                Font = new Font("Segoe UI", 9),
                Size = new Size(250, 23)
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
            int x = 30;
            int y = 20;
            int labelWidth = 120;
            int controlX = x + labelWidth + 10;
            int spacing = 40;

            // Title
            this.Controls[0].Location = new Point(x, y);
            y += 50;

            // Personal Information Section
            this.Controls[1].Location = new Point(x, y);
            y += 35;

            // Personal info fields
            for (int i = 2; i < 14; i += 2) // Personal info: 6 fields x 2 controls each
            {
                this.Controls[i].Location = new Point(x, y + 3);
                this.Controls[i + 1].Location = new Point(controlX, y);
                y += spacing;
            }

            // Work Information Section
            this.Controls[14].Location = new Point(x, y);
            y += 35;

            // Work info fields
            for (int i = 15; i < 25; i += 2) // Work info: 5 fields x 2 controls each
            {
                this.Controls[i].Location = new Point(x, y + 3);
                this.Controls[i + 1].Location = new Point(controlX, y);
                y += spacing;
            }

            // Account Information Section
            this.Controls[25].Location = new Point(x, y);
            y += 35;

            // Account checkbox
            chkTaoTaiKhoan.Location = new Point(x, y);
            y += spacing;

            // Account fields
            for (int i = 27; i < 33; i += 2) // Account info: 3 fields x 2 controls each
            {
                this.Controls[i].Location = new Point(x, y + 3);
                this.Controls[i + 1].Location = new Point(controlX, y);
                y += spacing;
            }

            // Buttons
            y += 20;
            btnLuu.Location = new Point(controlX, y);
            btnHuy.Location = new Point(controlX + 110, y);

            // Adjust form height
            this.Height = y + 80;
        }

        #endregion
    }
}
