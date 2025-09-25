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
            this.SuspendLayout();
            
            // Form properties
            this.BackColor = Color.FromArgb(50, 50, 50);
            this.ForeColor = Color.White;
            this.Size = new Size(480, 600);
            this.Text = "Nhân viên";
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.ShowInTaskbar = false;
            this.Font = new Font("Segoe UI", 9);
            
            // Add preview controls for Designer
            var lblPreview = new Label
            {
                Text = "CHI TIẾT NHÂN VIÊN\n(Designer Preview)",
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                ForeColor = Color.FromArgb(124, 77, 255),
                Location = new Point(50, 30),
                AutoSize = true
            };
            
            var pnlPersonalInfo = new Panel
            {
                BackColor = Color.FromArgb(60, 60, 60),
                BorderStyle = BorderStyle.FixedSingle,
                Size = new Size(380, 150),
                Location = new Point(50, 100)
            };
            
            var lblPersonalInfo = new Label
            {
                Text = "👤 THÔNG TIN CÁ NHÂN\n\nHọ tên | Ngày sinh | Giới tính\nĐiện thoại | Email | Địa chỉ\nCCCD | Nơi cấp | Ngày cấp",
                ForeColor = Color.White,
                Location = new Point(20, 20),
                Size = new Size(340, 110),
                Font = new Font("Segoe UI", 9)
            };
            pnlPersonalInfo.Controls.Add(lblPersonalInfo);
            
            var pnlWorkInfo = new Panel
            {
                BackColor = Color.FromArgb(60, 60, 60),
                BorderStyle = BorderStyle.FixedSingle,
                Size = new Size(380, 120),
                Location = new Point(50, 270)
            };
            
            var lblWorkInfo = new Label
            {
                Text = "💼 THÔNG TIN CÔNG VIỆC\n\nNgày vào làm | Trạng thái\nPhòng ban | Chức danh | Lương cơ bản\nTài khoản đăng nhập | Vai trò",
                ForeColor = Color.White,
                Location = new Point(20, 20),
                Size = new Size(340, 80),
                Font = new Font("Segoe UI", 9)
            };
            pnlWorkInfo.Controls.Add(lblWorkInfo);
            
            var pnlButtons = new Panel
            {
                BackColor = Color.FromArgb(70, 70, 70),
                Size = new Size(380, 50),
                Location = new Point(50, 410)
            };
            
            var lblButtons = new Label
            {
                Text = "Buttons: Lưu | Hủy | Tạo tài khoản | Đặt lại mật khẩu",
                ForeColor = Color.White,
                Location = new Point(20, 15),
                AutoSize = true,
                Font = new Font("Segoe UI", 9)
            };
            pnlButtons.Controls.Add(lblButtons);
            
            this.Controls.AddRange(new Control[] { lblPreview, pnlPersonalInfo, pnlWorkInfo, pnlButtons });
            
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private void CreateControls()
        {
            // Title
            var lblTitle = new Label
            {
                Text = "QUẢN LÝ NHÂN VIÊN",
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
            int x = 25;
            int y = 15;
            int labelWidth = 120;
            int controlX = x + labelWidth + 10;
            int spacing = 30; // Reduced spacing from 40 to 30
            int sectionSpacing = 25; // Reduced section spacing

            // Title
            this.Controls[0].Location = new Point(x, y);
            y += 40; // Reduced from 50

            // Personal Information Section
            this.Controls[1].Location = new Point(x, y);
            y += sectionSpacing;

            // Personal info fields
            for (int i = 2; i < 14; i += 2) // Personal info: 6 fields x 2 controls each
            {
                this.Controls[i].Location = new Point(x, y + 3);
                this.Controls[i + 1].Location = new Point(controlX, y);
                y += spacing;
            }

            // Work Information Section
            this.Controls[14].Location = new Point(x, y + 5);
            y += sectionSpacing + 5;

            // Work info fields
            for (int i = 15; i < 25; i += 2) // Work info: 5 fields x 2 controls each
            {
                this.Controls[i].Location = new Point(x, y + 3);
                this.Controls[i + 1].Location = new Point(controlX, y);
                y += spacing;
            }

            // Account Information Section
            this.Controls[25].Location = new Point(x, y + 5);
            y += sectionSpacing + 5;

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

            // Buttons with better spacing
            y += 15;
            btnLuu.Location = new Point(controlX, y);
            btnHuy.Location = new Point(controlX + 130, y); // Better spacing between buttons

            // Adjust form height to be more compact
            this.Height = y + 70;
        }

        #endregion
    }
}
