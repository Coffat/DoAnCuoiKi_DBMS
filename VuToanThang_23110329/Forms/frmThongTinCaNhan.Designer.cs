namespace VuToanThang_23110329.Forms
{
    partial class frmThongTinCaNhan
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

        private void InitializeComponent()
        {
            this.pnlMain = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.pnlContent = new System.Windows.Forms.Panel();
            this.pnlForm = new System.Windows.Forms.Panel();
            this.lblHoTen = new System.Windows.Forms.Label();
            this.txtHoTen = new Guna.UI2.WinForms.Guna2TextBox();
            this.lblDienThoai = new System.Windows.Forms.Label();
            this.txtDienThoai = new Guna.UI2.WinForms.Guna2TextBox();
            this.lblEmail = new System.Windows.Forms.Label();
            this.txtEmail = new Guna.UI2.WinForms.Guna2TextBox();
            this.lblDiaChi = new System.Windows.Forms.Label();
            this.txtDiaChi = new Guna.UI2.WinForms.Guna2TextBox();
            this.lblPhongBan = new System.Windows.Forms.Label();
            this.txtPhongBan = new Guna.UI2.WinForms.Guna2TextBox();
            this.lblChucDanh = new System.Windows.Forms.Label();
            this.txtChucDanh = new Guna.UI2.WinForms.Guna2TextBox();
            this.lblLuongCoBan = new System.Windows.Forms.Label();
            this.txtLuongCoBan = new Guna.UI2.WinForms.Guna2TextBox();
            this.lblNgayVaoLam = new System.Windows.Forms.Label();
            this.dtpNgayVaoLam = new Guna.UI2.WinForms.Guna2DateTimePicker();
            this.pnlButtons = new System.Windows.Forms.Panel();
            this.btnCapNhat = new Guna.UI2.WinForms.Guna2Button();
            this.btnDoiMatKhau = new Guna.UI2.WinForms.Guna2Button();
            this.btnLamMoi = new Guna.UI2.WinForms.Guna2Button();
            this.pnlMain.SuspendLayout();
            this.pnlContent.SuspendLayout();
            this.pnlForm.SuspendLayout();
            this.pnlButtons.SuspendLayout();
            this.SuspendLayout();
            
            // pnlMain
            this.pnlMain.BackColor = System.Drawing.Color.White;
            this.pnlMain.Controls.Add(this.pnlButtons);
            this.pnlMain.Controls.Add(this.pnlContent);
            this.pnlMain.Controls.Add(this.pnlHeader);
            this.pnlMain.Controls.Add(this.lblTitle);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 0);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(1200, 700);
            this.pnlMain.TabIndex = 0;
            
            // lblTitle
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(94, 148, 255);
            this.lblTitle.Location = new System.Drawing.Point(20, 20);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(180, 30);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Thông tin cá nhân";
            
            // pnlHeader
            this.pnlHeader.BackColor = System.Drawing.Color.FromArgb(94, 148, 255);
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(1200, 5);
            this.pnlHeader.TabIndex = 1;
            
            // pnlContent
            this.pnlContent.BackColor = System.Drawing.Color.FromArgb(248, 249, 250);
            this.pnlContent.Controls.Add(this.pnlForm);
            this.pnlContent.Location = new System.Drawing.Point(20, 60);
            this.pnlContent.Name = "pnlContent";
            this.pnlContent.Size = new System.Drawing.Size(1160, 580);
            this.pnlContent.TabIndex = 2;
            
            // pnlForm
            this.pnlForm.BackColor = System.Drawing.Color.White;
            this.pnlForm.Controls.Add(this.dtpNgayVaoLam);
            this.pnlForm.Controls.Add(this.lblNgayVaoLam);
            this.pnlForm.Controls.Add(this.txtLuongCoBan);
            this.pnlForm.Controls.Add(this.lblLuongCoBan);
            this.pnlForm.Controls.Add(this.txtChucDanh);
            this.pnlForm.Controls.Add(this.lblChucDanh);
            this.pnlForm.Controls.Add(this.txtPhongBan);
            this.pnlForm.Controls.Add(this.lblPhongBan);
            this.pnlForm.Controls.Add(this.txtDiaChi);
            this.pnlForm.Controls.Add(this.lblDiaChi);
            this.pnlForm.Controls.Add(this.txtEmail);
            this.pnlForm.Controls.Add(this.lblEmail);
            this.pnlForm.Controls.Add(this.txtDienThoai);
            this.pnlForm.Controls.Add(this.lblDienThoai);
            this.pnlForm.Controls.Add(this.txtHoTen);
            this.pnlForm.Controls.Add(this.lblHoTen);
            this.pnlForm.Location = new System.Drawing.Point(20, 20);
            this.pnlForm.Name = "pnlForm";
            this.pnlForm.Size = new System.Drawing.Size(1120, 540);
            this.pnlForm.TabIndex = 0;
            
            // lblHoTen
            this.lblHoTen.AutoSize = true;
            this.lblHoTen.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblHoTen.ForeColor = System.Drawing.Color.FromArgb(64, 64, 64);
            this.lblHoTen.Location = new System.Drawing.Point(20, 20);
            this.lblHoTen.Name = "lblHoTen";
            this.lblHoTen.Size = new System.Drawing.Size(55, 21);
            this.lblHoTen.TabIndex = 0;
            this.lblHoTen.Text = "Họ tên:";
            
            // txtHoTen
            this.txtHoTen.BorderRadius = 8;
            this.txtHoTen.FillColor = System.Drawing.Color.White;
            this.txtHoTen.FocusedState.BorderColor = System.Drawing.Color.FromArgb(94, 148, 255);
            this.txtHoTen.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtHoTen.ForeColor = System.Drawing.Color.Black;
            this.txtHoTen.Location = new System.Drawing.Point(120, 15);
            this.txtHoTen.Name = "txtHoTen";
            this.txtHoTen.PlaceholderText = "Nhập họ và tên";
            this.txtHoTen.Size = new System.Drawing.Size(250, 35);
            this.txtHoTen.TabIndex = 1;
            this.txtHoTen.TextChanged += new System.EventHandler(this.txtHoTen_TextChanged);
            
            // lblDienThoai
            this.lblDienThoai.AutoSize = true;
            this.lblDienThoai.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblDienThoai.ForeColor = System.Drawing.Color.FromArgb(64, 64, 64);
            this.lblDienThoai.Location = new System.Drawing.Point(20, 70);
            this.lblDienThoai.Name = "lblDienThoai";
            this.lblDienThoai.Size = new System.Drawing.Size(75, 21);
            this.lblDienThoai.TabIndex = 2;
            this.lblDienThoai.Text = "Điện thoại:";
            
            // txtDienThoai
            this.txtDienThoai.BorderRadius = 8;
            this.txtDienThoai.FillColor = System.Drawing.Color.White;
            this.txtDienThoai.FocusedState.BorderColor = System.Drawing.Color.FromArgb(94, 148, 255);
            this.txtDienThoai.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtDienThoai.ForeColor = System.Drawing.Color.Black;
            this.txtDienThoai.Location = new System.Drawing.Point(120, 65);
            this.txtDienThoai.Name = "txtDienThoai";
            this.txtDienThoai.PlaceholderText = "Nhập số điện thoại";
            this.txtDienThoai.Size = new System.Drawing.Size(200, 35);
            this.txtDienThoai.TabIndex = 3;
            this.txtDienThoai.TextChanged += new System.EventHandler(this.txtDienThoai_TextChanged);
            
            // lblEmail
            this.lblEmail.AutoSize = true;
            this.lblEmail.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblEmail.ForeColor = System.Drawing.Color.FromArgb(64, 64, 64);
            this.lblEmail.Location = new System.Drawing.Point(20, 120);
            this.lblEmail.Name = "lblEmail";
            this.lblEmail.Size = new System.Drawing.Size(45, 21);
            this.lblEmail.TabIndex = 4;
            this.lblEmail.Text = "Email:";
            
            // txtEmail
            this.txtEmail.BorderRadius = 8;
            this.txtEmail.FillColor = System.Drawing.Color.White;
            this.txtEmail.FocusedState.BorderColor = System.Drawing.Color.FromArgb(94, 148, 255);
            this.txtEmail.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtEmail.ForeColor = System.Drawing.Color.Black;
            this.txtEmail.Location = new System.Drawing.Point(120, 115);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.PlaceholderText = "Nhập địa chỉ email";
            this.txtEmail.Size = new System.Drawing.Size(300, 35);
            this.txtEmail.TabIndex = 5;
            this.txtEmail.TextChanged += new System.EventHandler(this.txtEmail_TextChanged);
            
            // lblDiaChi
            this.lblDiaChi.AutoSize = true;
            this.lblDiaChi.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblDiaChi.ForeColor = System.Drawing.Color.FromArgb(64, 64, 64);
            this.lblDiaChi.Location = new System.Drawing.Point(20, 170);
            this.lblDiaChi.Name = "lblDiaChi";
            this.lblDiaChi.Size = new System.Drawing.Size(50, 21);
            this.lblDiaChi.TabIndex = 6;
            this.lblDiaChi.Text = "Địa chỉ:";
            
            // txtDiaChi
            this.txtDiaChi.BorderRadius = 8;
            this.txtDiaChi.FillColor = System.Drawing.Color.White;
            this.txtDiaChi.FocusedState.BorderColor = System.Drawing.Color.FromArgb(94, 148, 255);
            this.txtDiaChi.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtDiaChi.ForeColor = System.Drawing.Color.Black;
            this.txtDiaChi.Location = new System.Drawing.Point(120, 165);
            this.txtDiaChi.Multiline = true;
            this.txtDiaChi.Name = "txtDiaChi";
            this.txtDiaChi.PlaceholderText = "Nhập địa chỉ";
            this.txtDiaChi.Size = new System.Drawing.Size(400, 60);
            this.txtDiaChi.TabIndex = 7;
            this.txtDiaChi.TextChanged += new System.EventHandler(this.txtDiaChi_TextChanged);
            
            // lblPhongBan
            this.lblPhongBan.AutoSize = true;
            this.lblPhongBan.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblPhongBan.ForeColor = System.Drawing.Color.FromArgb(64, 64, 64);
            this.lblPhongBan.Location = new System.Drawing.Point(20, 250);
            this.lblPhongBan.Name = "lblPhongBan";
            this.lblPhongBan.Size = new System.Drawing.Size(70, 21);
            this.lblPhongBan.TabIndex = 8;
            this.lblPhongBan.Text = "Phòng ban:";
            
            // txtPhongBan
            this.txtPhongBan.BorderRadius = 8;
            this.txtPhongBan.FillColor = System.Drawing.Color.FromArgb(248, 249, 250);
            this.txtPhongBan.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtPhongBan.ForeColor = System.Drawing.Color.FromArgb(108, 117, 125);
            this.txtPhongBan.Location = new System.Drawing.Point(120, 245);
            this.txtPhongBan.Name = "txtPhongBan";
            this.txtPhongBan.ReadOnly = true;
            this.txtPhongBan.Size = new System.Drawing.Size(200, 35);
            this.txtPhongBan.TabIndex = 9;
            
            // lblChucDanh
            this.lblChucDanh.AutoSize = true;
            this.lblChucDanh.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblChucDanh.ForeColor = System.Drawing.Color.FromArgb(64, 64, 64);
            this.lblChucDanh.Location = new System.Drawing.Point(20, 300);
            this.lblChucDanh.Name = "lblChucDanh";
            this.lblChucDanh.Size = new System.Drawing.Size(75, 21);
            this.lblChucDanh.TabIndex = 10;
            this.lblChucDanh.Text = "Chức danh:";
            
            // txtChucDanh
            this.txtChucDanh.BorderRadius = 8;
            this.txtChucDanh.FillColor = System.Drawing.Color.FromArgb(248, 249, 250);
            this.txtChucDanh.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtChucDanh.ForeColor = System.Drawing.Color.FromArgb(108, 117, 125);
            this.txtChucDanh.Location = new System.Drawing.Point(120, 295);
            this.txtChucDanh.Name = "txtChucDanh";
            this.txtChucDanh.ReadOnly = true;
            this.txtChucDanh.Size = new System.Drawing.Size(200, 35);
            this.txtChucDanh.TabIndex = 11;
            
            // lblLuongCoBan
            this.lblLuongCoBan.AutoSize = true;
            this.lblLuongCoBan.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblLuongCoBan.ForeColor = System.Drawing.Color.FromArgb(64, 64, 64);
            this.lblLuongCoBan.Location = new System.Drawing.Point(20, 350);
            this.lblLuongCoBan.Name = "lblLuongCoBan";
            this.lblLuongCoBan.Size = new System.Drawing.Size(90, 21);
            this.lblLuongCoBan.TabIndex = 12;
            this.lblLuongCoBan.Text = "Lương cơ bản:";
            
            // txtLuongCoBan
            this.txtLuongCoBan.BorderRadius = 8;
            this.txtLuongCoBan.FillColor = System.Drawing.Color.FromArgb(248, 249, 250);
            this.txtLuongCoBan.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtLuongCoBan.ForeColor = System.Drawing.Color.FromArgb(108, 117, 125);
            this.txtLuongCoBan.Location = new System.Drawing.Point(120, 345);
            this.txtLuongCoBan.Name = "txtLuongCoBan";
            this.txtLuongCoBan.ReadOnly = true;
            this.txtLuongCoBan.Size = new System.Drawing.Size(200, 35);
            this.txtLuongCoBan.TabIndex = 13;
            
            // lblNgayVaoLam
            this.lblNgayVaoLam.AutoSize = true;
            this.lblNgayVaoLam.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblNgayVaoLam.ForeColor = System.Drawing.Color.FromArgb(64, 64, 64);
            this.lblNgayVaoLam.Location = new System.Drawing.Point(20, 400);
            this.lblNgayVaoLam.Name = "lblNgayVaoLam";
            this.lblNgayVaoLam.Size = new System.Drawing.Size(90, 21);
            this.lblNgayVaoLam.TabIndex = 14;
            this.lblNgayVaoLam.Text = "Ngày vào làm:";
            
            // dtpNgayVaoLam
            this.dtpNgayVaoLam.BorderRadius = 8;
            this.dtpNgayVaoLam.CheckedState.Parent = this.dtpNgayVaoLam;
            this.dtpNgayVaoLam.Enabled = false;
            this.dtpNgayVaoLam.FillColor = System.Drawing.Color.FromArgb(248, 249, 250);
            this.dtpNgayVaoLam.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.dtpNgayVaoLam.ForeColor = System.Drawing.Color.FromArgb(108, 117, 125);
            this.dtpNgayVaoLam.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpNgayVaoLam.Location = new System.Drawing.Point(120, 395);
            this.dtpNgayVaoLam.Name = "dtpNgayVaoLam";
            this.dtpNgayVaoLam.Size = new System.Drawing.Size(200, 35);
            this.dtpNgayVaoLam.TabIndex = 15;
            
            // pnlButtons
            this.pnlButtons.BackColor = System.Drawing.Color.FromArgb(248, 249, 250);
            this.pnlButtons.Controls.Add(this.btnLamMoi);
            this.pnlButtons.Controls.Add(this.btnDoiMatKhau);
            this.pnlButtons.Controls.Add(this.btnCapNhat);
            this.pnlButtons.Location = new System.Drawing.Point(20, 650);
            this.pnlButtons.Name = "pnlButtons";
            this.pnlButtons.Size = new System.Drawing.Size(1160, 50);
            this.pnlButtons.TabIndex = 3;
            
            // btnCapNhat
            this.btnCapNhat.BorderRadius = 12;
            this.btnCapNhat.FillColor = System.Drawing.Color.FromArgb(40, 167, 69);
            this.btnCapNhat.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnCapNhat.ForeColor = System.Drawing.Color.White;
            this.btnCapNhat.Location = new System.Drawing.Point(20, 10);
            this.btnCapNhat.Name = "btnCapNhat";
            this.btnCapNhat.Size = new System.Drawing.Size(120, 30);
            this.btnCapNhat.TabIndex = 0;
            this.btnCapNhat.Text = "Cập nhật thông tin";
            this.btnCapNhat.Click += new System.EventHandler(this.btnCapNhat_Click);
            
            // btnDoiMatKhau
            this.btnDoiMatKhau.BorderRadius = 12;
            this.btnDoiMatKhau.FillColor = System.Drawing.Color.FromArgb(255, 193, 7);
            this.btnDoiMatKhau.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnDoiMatKhau.ForeColor = System.Drawing.Color.White;
            this.btnDoiMatKhau.Location = new System.Drawing.Point(150, 10);
            this.btnDoiMatKhau.Name = "btnDoiMatKhau";
            this.btnDoiMatKhau.Size = new System.Drawing.Size(120, 30);
            this.btnDoiMatKhau.TabIndex = 1;
            this.btnDoiMatKhau.Text = "Đổi mật khẩu";
            this.btnDoiMatKhau.Click += new System.EventHandler(this.btnDoiMatKhau_Click);
            
            // btnLamMoi
            this.btnLamMoi.BorderRadius = 12;
            this.btnLamMoi.FillColor = System.Drawing.Color.FromArgb(108, 117, 125);
            this.btnLamMoi.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnLamMoi.ForeColor = System.Drawing.Color.White;
            this.btnLamMoi.Location = new System.Drawing.Point(280, 10);
            this.btnLamMoi.Name = "btnLamMoi";
            this.btnLamMoi.Size = new System.Drawing.Size(100, 30);
            this.btnLamMoi.TabIndex = 2;
            this.btnLamMoi.Text = "Làm mới";
            this.btnLamMoi.Click += new System.EventHandler(this.btnLamMoi_Click);
            
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1200, 700);
            this.Controls.Add(this.pnlMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmThongTinCaNhan";
            this.Text = "Thông tin cá nhân";
            
            this.pnlMain.ResumeLayout(false);
            this.pnlMain.PerformLayout();
            this.pnlContent.ResumeLayout(false);
            this.pnlForm.ResumeLayout(false);
            this.pnlForm.PerformLayout();
            this.pnlButtons.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Panel pnlContent;
        private System.Windows.Forms.Panel pnlForm;
        private System.Windows.Forms.Label lblHoTen;
        private Guna.UI2.WinForms.Guna2TextBox txtHoTen;
        private System.Windows.Forms.Label lblDienThoai;
        private Guna.UI2.WinForms.Guna2TextBox txtDienThoai;
        private System.Windows.Forms.Label lblEmail;
        private Guna.UI2.WinForms.Guna2TextBox txtEmail;
        private System.Windows.Forms.Label lblDiaChi;
        private Guna.UI2.WinForms.Guna2TextBox txtDiaChi;
        private System.Windows.Forms.Label lblPhongBan;
        private Guna.UI2.WinForms.Guna2TextBox txtPhongBan;
        private System.Windows.Forms.Label lblChucDanh;
        private Guna.UI2.WinForms.Guna2TextBox txtChucDanh;
        private System.Windows.Forms.Label lblLuongCoBan;
        private Guna.UI2.WinForms.Guna2TextBox txtLuongCoBan;
        private System.Windows.Forms.Label lblNgayVaoLam;
        private Guna.UI2.WinForms.Guna2DateTimePicker dtpNgayVaoLam;
        private System.Windows.Forms.Panel pnlButtons;
        private Guna.UI2.WinForms.Guna2Button btnCapNhat;
        private Guna.UI2.WinForms.Guna2Button btnDoiMatKhau;
        private Guna.UI2.WinForms.Guna2Button btnLamMoi;
    }
}
