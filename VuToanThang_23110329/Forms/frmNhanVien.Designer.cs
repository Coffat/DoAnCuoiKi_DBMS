using System;
using System.Windows.Forms;

namespace VuToanThang_23110329.Forms
{
    partial class frmNhanVien
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
            this.pnlMain = new Guna.UI2.WinForms.Guna2Panel();
            this.pnlHeader = new Guna.UI2.WinForms.Guna2Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.pnlContent = new Guna.UI2.WinForms.Guna2Panel();
            this.pnlLeft = new Guna.UI2.WinForms.Guna2Panel();
            this.pnlRight = new Guna.UI2.WinForms.Guna2Panel();
            this.dgvNhanVien = new Guna.UI2.WinForms.Guna2DataGridView();
            this.pnlToolbar = new Guna.UI2.WinForms.Guna2Panel();
            this.btnThem = new Guna.UI2.WinForms.Guna2Button();
            this.btnSua = new Guna.UI2.WinForms.Guna2Button();
            this.btnVoHieuHoa = new Guna.UI2.WinForms.Guna2Button();
            this.btnLamMoi = new Guna.UI2.WinForms.Guna2Button();
            this.txtTimKiem = new Guna.UI2.WinForms.Guna2TextBox();
            this.cmbPhongBan = new Guna.UI2.WinForms.Guna2ComboBox();
            this.cmbTrangThai = new Guna.UI2.WinForms.Guna2ComboBox();
            
            // Form details
            this.pnlFormDetails = new Guna.UI2.WinForms.Guna2Panel();
            this.txtHoTen = new Guna.UI2.WinForms.Guna2TextBox();
            this.dtpNgaySinh = new Guna.UI2.WinForms.Guna2DateTimePicker();
            this.cmbGioiTinh = new Guna.UI2.WinForms.Guna2ComboBox();
            this.txtDienThoai = new Guna.UI2.WinForms.Guna2TextBox();
            this.txtEmail = new Guna.UI2.WinForms.Guna2TextBox();
            this.txtDiaChi = new Guna.UI2.WinForms.Guna2TextBox();
            this.dtpNgayVaoLam = new Guna.UI2.WinForms.Guna2DateTimePicker();
            this.cmbPhongBanForm = new Guna.UI2.WinForms.Guna2ComboBox();
            this.cmbChucVu = new Guna.UI2.WinForms.Guna2ComboBox();
            this.numLuongCoBan = new Guna.UI2.WinForms.Guna2NumericUpDown();
            this.btnLuu = new Guna.UI2.WinForms.Guna2Button();
            this.btnHuy = new Guna.UI2.WinForms.Guna2Button();
            
            this.pnlMain.SuspendLayout();
            this.pnlHeader.SuspendLayout();
            this.pnlContent.SuspendLayout();
            this.pnlLeft.SuspendLayout();
            this.pnlRight.SuspendLayout();
            this.pnlToolbar.SuspendLayout();
            this.pnlFormDetails.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvNhanVien)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numLuongCoBan)).BeginInit();
            this.SuspendLayout();
            
            // pnlMain
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.FillColor = System.Drawing.Color.White;
            this.pnlMain.Location = new System.Drawing.Point(0, 0);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(1200, 700);
            this.pnlMain.TabIndex = 0;
            
            // pnlHeader
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(1200, 60);
            this.pnlHeader.TabIndex = 0;
            
            // lblTitle
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(20, 15);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(280, 32);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Quản lý Hồ sơ Nhân viên";
            
            // pnlContent
            this.pnlContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlContent.FillColor = System.Drawing.Color.White;
            this.pnlContent.Location = new System.Drawing.Point(0, 60);
            this.pnlContent.Name = "pnlContent";
            this.pnlContent.Padding = new System.Windows.Forms.Padding(10);
            this.pnlContent.Size = new System.Drawing.Size(1200, 640);
            this.pnlContent.TabIndex = 1;
            
            // Continue with remaining controls...
            // pnlLeft - DataGridView và toolbar
            this.pnlLeft.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlLeft.FillColor = System.Drawing.Color.White;
            this.pnlLeft.Name = "pnlLeft";
            this.pnlLeft.Padding = new System.Windows.Forms.Padding(5);
            
            // pnlRight - Form nhập liệu
            this.pnlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlRight.FillColor = System.Drawing.Color.FromArgb(248, 249, 250);
            this.pnlRight.Name = "pnlRight";
            this.pnlRight.Size = new System.Drawing.Size(400, 620);
            this.pnlRight.Padding = new System.Windows.Forms.Padding(10);
            this.pnlRight.Visible = false;
            
            // pnlToolbar
            this.pnlToolbar.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlToolbar.FillColor = System.Drawing.Color.White;
            this.pnlToolbar.Name = "pnlToolbar";
            this.pnlToolbar.Size = new System.Drawing.Size(780, 60);
            this.pnlToolbar.Padding = new System.Windows.Forms.Padding(10);
            
            // Buttons
            this.btnThem.Size = new System.Drawing.Size(100, 35);
            this.btnThem.Location = new System.Drawing.Point(10, 12);
            this.btnThem.Text = "Thêm mới";
            this.btnThem.FillColor = System.Drawing.Color.FromArgb(40, 167, 69);
            this.btnThem.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnThem.ForeColor = System.Drawing.Color.White;
            this.btnThem.BorderRadius = 8;
            this.btnThem.Click += new System.EventHandler(this.btnThem_Click);
            
            this.btnSua.Size = new System.Drawing.Size(100, 35);
            this.btnSua.Location = new System.Drawing.Point(120, 12);
            this.btnSua.Text = "Cập nhật";
            this.btnSua.FillColor = System.Drawing.Color.FromArgb(255, 193, 7);
            this.btnSua.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnSua.ForeColor = System.Drawing.Color.Black;
            this.btnSua.BorderRadius = 8;
            this.btnSua.Click += new System.EventHandler(this.btnSua_Click);
            
            this.btnVoHieuHoa.Size = new System.Drawing.Size(100, 35);
            this.btnVoHieuHoa.Location = new System.Drawing.Point(230, 12);
            this.btnVoHieuHoa.Text = "Vô hiệu hóa";
            this.btnVoHieuHoa.FillColor = System.Drawing.Color.FromArgb(220, 53, 69);
            this.btnVoHieuHoa.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnVoHieuHoa.ForeColor = System.Drawing.Color.White;
            this.btnVoHieuHoa.BorderRadius = 8;
            this.btnVoHieuHoa.Click += new System.EventHandler(this.btnVoHieuHoa_Click);
            
            this.btnLamMoi.Size = new System.Drawing.Size(100, 35);
            this.btnLamMoi.Location = new System.Drawing.Point(340, 12);
            this.btnLamMoi.Text = "Làm mới";
            this.btnLamMoi.FillColor = System.Drawing.Color.FromArgb(108, 117, 125);
            this.btnLamMoi.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnLamMoi.ForeColor = System.Drawing.Color.White;
            this.btnLamMoi.BorderRadius = 8;
            this.btnLamMoi.Click += new System.EventHandler(this.btnLamMoi_Click);
            
            // Search and filter controls
            this.txtTimKiem.Size = new System.Drawing.Size(200, 35);
            this.txtTimKiem.Location = new System.Drawing.Point(460, 12);
            this.txtTimKiem.PlaceholderText = "Tìm kiếm nhân viên...";
            this.txtTimKiem.BorderRadius = 8;
            this.txtTimKiem.TextChanged += new System.EventHandler(this.txtTimKiem_TextChanged);
            
            this.cmbPhongBan.Size = new System.Drawing.Size(150, 35);
            this.cmbPhongBan.Location = new System.Drawing.Point(670, 12);
            this.cmbPhongBan.BorderRadius = 8;
            this.cmbPhongBan.SelectedIndexChanged += new System.EventHandler(this.cmbPhongBan_SelectedIndexChanged);
            
            this.cmbTrangThai.Size = new System.Drawing.Size(120, 35);
            this.cmbTrangThai.Location = new System.Drawing.Point(830, 12);
            this.cmbTrangThai.BorderRadius = 8;
            this.cmbTrangThai.Items.AddRange(new object[] { "Tất cả", "Đang làm", "Nghỉ việc" });
            this.cmbTrangThai.SelectedIndex = 0;
            this.cmbTrangThai.SelectedIndexChanged += new System.EventHandler(this.cmbTrangThai_SelectedIndexChanged);
            
            // DataGridView
            this.dgvNhanVien.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvNhanVien.Name = "dgvNhanVien";
            this.dgvNhanVien.AllowUserToAddRows = false;
            this.dgvNhanVien.AllowUserToDeleteRows = false;
            this.dgvNhanVien.ReadOnly = true;
            this.dgvNhanVien.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvNhanVien.MultiSelect = false;
            this.dgvNhanVien.BackgroundColor = System.Drawing.Color.White;
            this.dgvNhanVien.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvNhanVien.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvNhanVien.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(94, 148, 255);
            this.dgvNhanVien.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.White;
            this.dgvNhanVien.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.dgvNhanVien.ColumnHeadersHeight = 40;
            this.dgvNhanVien.DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.dgvNhanVien.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(231, 229, 255);
            this.dgvNhanVien.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.Black;
            this.dgvNhanVien.RowTemplate.Height = 35;
            this.dgvNhanVien.SelectionChanged += new System.EventHandler(this.dgvNhanVien_SelectionChanged);
            
            // Add controls to panels
            this.pnlLeft.Controls.Add(this.dgvNhanVien);
            this.pnlLeft.Controls.Add(this.pnlToolbar);
            this.pnlToolbar.Controls.Add(this.btnThem);
            this.pnlToolbar.Controls.Add(this.btnSua);
            this.pnlToolbar.Controls.Add(this.btnVoHieuHoa);
            this.pnlToolbar.Controls.Add(this.btnLamMoi);
            this.pnlToolbar.Controls.Add(this.txtTimKiem);
            this.pnlToolbar.Controls.Add(this.cmbPhongBan);
            this.pnlToolbar.Controls.Add(this.cmbTrangThai);
            
            // Add controls to panels
            this.pnlMain.Controls.Add(this.pnlContent);
            this.pnlMain.Controls.Add(this.pnlHeader);
            this.pnlHeader.Controls.Add(this.lblTitle);
            this.pnlContent.Controls.Add(this.pnlLeft);
            this.pnlContent.Controls.Add(this.pnlRight);
            
            // Form details setup
            this.pnlFormDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlFormDetails.FillColor = System.Drawing.Color.FromArgb(248, 249, 250);
            this.pnlFormDetails.Name = "pnlFormDetails";
            this.pnlFormDetails.Padding = new System.Windows.Forms.Padding(15);
            
            // Form controls với labels
            var lblHoTen = new System.Windows.Forms.Label();
            lblHoTen.Text = "Họ tên *";
            lblHoTen.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            lblHoTen.Location = new System.Drawing.Point(15, 15);
            lblHoTen.Size = new System.Drawing.Size(100, 20);
            
            this.txtHoTen.Location = new System.Drawing.Point(15, 35);
            this.txtHoTen.Size = new System.Drawing.Size(350, 36);
            this.txtHoTen.BorderRadius = 8;
            this.txtHoTen.PlaceholderText = "Nhập họ tên nhân viên";
            
            var lblNgaySinh = new System.Windows.Forms.Label();
            lblNgaySinh.Text = "Ngày sinh";
            lblNgaySinh.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            lblNgaySinh.Location = new System.Drawing.Point(15, 80);
            lblNgaySinh.Size = new System.Drawing.Size(100, 20);
            
            this.dtpNgaySinh.Location = new System.Drawing.Point(15, 100);
            this.dtpNgaySinh.Size = new System.Drawing.Size(170, 36);
            this.dtpNgaySinh.BorderRadius = 8;
            this.dtpNgaySinh.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpNgaySinh.CustomFormat = "dd/MM/yyyy";
            
            var lblGioiTinh = new System.Windows.Forms.Label();
            lblGioiTinh.Text = "Giới tính";
            lblGioiTinh.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            lblGioiTinh.Location = new System.Drawing.Point(195, 80);
            lblGioiTinh.Size = new System.Drawing.Size(100, 20);
            
            this.cmbGioiTinh.Location = new System.Drawing.Point(195, 100);
            this.cmbGioiTinh.Size = new System.Drawing.Size(170, 36);
            this.cmbGioiTinh.BorderRadius = 8;
            this.cmbGioiTinh.Items.AddRange(new object[] { "Nam", "Nữ", "Khác" });
            
            var lblDienThoai = new System.Windows.Forms.Label();
            lblDienThoai.Text = "Điện thoại";
            lblDienThoai.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            lblDienThoai.Location = new System.Drawing.Point(15, 145);
            lblDienThoai.Size = new System.Drawing.Size(100, 20);
            
            this.txtDienThoai.Location = new System.Drawing.Point(15, 165);
            this.txtDienThoai.Size = new System.Drawing.Size(170, 36);
            this.txtDienThoai.BorderRadius = 8;
            this.txtDienThoai.PlaceholderText = "Số điện thoại";
            
            var lblEmail = new System.Windows.Forms.Label();
            lblEmail.Text = "Email";
            lblEmail.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            lblEmail.Location = new System.Drawing.Point(195, 145);
            lblEmail.Size = new System.Drawing.Size(100, 20);
            
            this.txtEmail.Location = new System.Drawing.Point(195, 165);
            this.txtEmail.Size = new System.Drawing.Size(170, 36);
            this.txtEmail.BorderRadius = 8;
            this.txtEmail.PlaceholderText = "Email";
            
            var lblDiaChi = new System.Windows.Forms.Label();
            lblDiaChi.Text = "Địa chỉ";
            lblDiaChi.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            lblDiaChi.Location = new System.Drawing.Point(15, 210);
            lblDiaChi.Size = new System.Drawing.Size(100, 20);
            
            this.txtDiaChi.Location = new System.Drawing.Point(15, 230);
            this.txtDiaChi.Size = new System.Drawing.Size(350, 36);
            this.txtDiaChi.BorderRadius = 8;
            this.txtDiaChi.PlaceholderText = "Địa chỉ";
            
            var lblNgayVaoLam = new System.Windows.Forms.Label();
            lblNgayVaoLam.Text = "Ngày vào làm *";
            lblNgayVaoLam.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            lblNgayVaoLam.Location = new System.Drawing.Point(15, 275);
            lblNgayVaoLam.Size = new System.Drawing.Size(100, 20);
            
            this.dtpNgayVaoLam.Location = new System.Drawing.Point(15, 295);
            this.dtpNgayVaoLam.Size = new System.Drawing.Size(170, 36);
            this.dtpNgayVaoLam.BorderRadius = 8;
            this.dtpNgayVaoLam.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpNgayVaoLam.CustomFormat = "dd/MM/yyyy";
            this.dtpNgayVaoLam.Value = DateTime.Now;
            
            var lblLuongCoBan = new System.Windows.Forms.Label();
            lblLuongCoBan.Text = "Lương cơ bản *";
            lblLuongCoBan.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            lblLuongCoBan.Location = new System.Drawing.Point(195, 275);
            lblLuongCoBan.Size = new System.Drawing.Size(100, 20);
            
            this.numLuongCoBan.Location = new System.Drawing.Point(195, 295);
            this.numLuongCoBan.Size = new System.Drawing.Size(170, 36);
            this.numLuongCoBan.BorderRadius = 8;
            this.numLuongCoBan.Maximum = 999999999;
            this.numLuongCoBan.ThousandsSeparator = true;
            this.numLuongCoBan.Value = 5000000;
            
            var lblPhongBan = new System.Windows.Forms.Label();
            lblPhongBan.Text = "Phòng ban";
            lblPhongBan.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            lblPhongBan.Location = new System.Drawing.Point(15, 340);
            lblPhongBan.Size = new System.Drawing.Size(100, 20);
            
            this.cmbPhongBanForm.Location = new System.Drawing.Point(15, 360);
            this.cmbPhongBanForm.Size = new System.Drawing.Size(170, 36);
            this.cmbPhongBanForm.BorderRadius = 8;
            
            var lblChucVu = new System.Windows.Forms.Label();
            lblChucVu.Text = "Chức vụ";
            lblChucVu.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            lblChucVu.Location = new System.Drawing.Point(195, 340);
            lblChucVu.Size = new System.Drawing.Size(100, 20);
            
            this.cmbChucVu.Location = new System.Drawing.Point(195, 360);
            this.cmbChucVu.Size = new System.Drawing.Size(170, 36);
            this.cmbChucVu.BorderRadius = 8;
            
            // Action buttons
            this.btnLuu.Location = new System.Drawing.Point(15, 420);
            this.btnLuu.Size = new System.Drawing.Size(170, 40);
            this.btnLuu.Text = "Lưu";
            this.btnLuu.FillColor = System.Drawing.Color.FromArgb(40, 167, 69);
            this.btnLuu.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnLuu.ForeColor = System.Drawing.Color.White;
            this.btnLuu.BorderRadius = 8;
            this.btnLuu.Click += new System.EventHandler(this.btnLuu_Click);
            
            this.btnHuy.Location = new System.Drawing.Point(195, 420);
            this.btnHuy.Size = new System.Drawing.Size(170, 40);
            this.btnHuy.Text = "Hủy";
            this.btnHuy.FillColor = System.Drawing.Color.FromArgb(108, 117, 125);
            this.btnHuy.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnHuy.ForeColor = System.Drawing.Color.White;
            this.btnHuy.BorderRadius = 8;
            this.btnHuy.Click += new System.EventHandler(this.btnHuy_Click);
            
            // Add controls to form details panel
            this.pnlFormDetails.Controls.Add(lblHoTen);
            this.pnlFormDetails.Controls.Add(this.txtHoTen);
            this.pnlFormDetails.Controls.Add(lblNgaySinh);
            this.pnlFormDetails.Controls.Add(this.dtpNgaySinh);
            this.pnlFormDetails.Controls.Add(lblGioiTinh);
            this.pnlFormDetails.Controls.Add(this.cmbGioiTinh);
            this.pnlFormDetails.Controls.Add(lblDienThoai);
            this.pnlFormDetails.Controls.Add(this.txtDienThoai);
            this.pnlFormDetails.Controls.Add(lblEmail);
            this.pnlFormDetails.Controls.Add(this.txtEmail);
            this.pnlFormDetails.Controls.Add(lblDiaChi);
            this.pnlFormDetails.Controls.Add(this.txtDiaChi);
            this.pnlFormDetails.Controls.Add(lblNgayVaoLam);
            this.pnlFormDetails.Controls.Add(this.dtpNgayVaoLam);
            this.pnlFormDetails.Controls.Add(lblLuongCoBan);
            this.pnlFormDetails.Controls.Add(this.numLuongCoBan);
            this.pnlFormDetails.Controls.Add(lblPhongBan);
            this.pnlFormDetails.Controls.Add(this.cmbPhongBanForm);
            this.pnlFormDetails.Controls.Add(lblChucVu);
            this.pnlFormDetails.Controls.Add(this.cmbChucVu);
            this.pnlFormDetails.Controls.Add(this.btnLuu);
            this.pnlFormDetails.Controls.Add(this.btnHuy);
            
            this.pnlRight.Controls.Add(this.pnlFormDetails);
            
            this.Controls.Add(this.pnlMain);
            this.Name = "frmNhanVien";
            this.Text = "Quản lý Nhân viên";
            this.Size = new System.Drawing.Size(1200, 700);
            
            this.pnlMain.ResumeLayout(false);
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.pnlContent.ResumeLayout(false);
            this.pnlLeft.ResumeLayout(false);
            this.pnlRight.ResumeLayout(false);
            this.pnlToolbar.ResumeLayout(false);
            this.pnlFormDetails.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvNhanVien)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numLuongCoBan)).EndInit();
            this.ResumeLayout(false);
        }

        #endregion

        private Guna.UI2.WinForms.Guna2Panel pnlMain;
        private Guna.UI2.WinForms.Guna2Panel pnlHeader;
        private System.Windows.Forms.Label lblTitle;
        private Guna.UI2.WinForms.Guna2Panel pnlContent;
        private Guna.UI2.WinForms.Guna2Panel pnlLeft;
        private Guna.UI2.WinForms.Guna2Panel pnlRight;
        private Guna.UI2.WinForms.Guna2DataGridView dgvNhanVien;
        private Guna.UI2.WinForms.Guna2Panel pnlToolbar;
        private Guna.UI2.WinForms.Guna2Button btnThem;
        private Guna.UI2.WinForms.Guna2Button btnSua;
        private Guna.UI2.WinForms.Guna2Button btnVoHieuHoa;
        private Guna.UI2.WinForms.Guna2Button btnLamMoi;
        private Guna.UI2.WinForms.Guna2TextBox txtTimKiem;
        private Guna.UI2.WinForms.Guna2ComboBox cmbPhongBan;
        private Guna.UI2.WinForms.Guna2ComboBox cmbTrangThai;
        private Guna.UI2.WinForms.Guna2Panel pnlFormDetails;
        private Guna.UI2.WinForms.Guna2TextBox txtHoTen;
        private Guna.UI2.WinForms.Guna2DateTimePicker dtpNgaySinh;
        private Guna.UI2.WinForms.Guna2ComboBox cmbGioiTinh;
        private Guna.UI2.WinForms.Guna2TextBox txtDienThoai;
        private Guna.UI2.WinForms.Guna2TextBox txtEmail;
        private Guna.UI2.WinForms.Guna2TextBox txtDiaChi;
        private Guna.UI2.WinForms.Guna2DateTimePicker dtpNgayVaoLam;
        private Guna.UI2.WinForms.Guna2ComboBox cmbPhongBanForm;
        private Guna.UI2.WinForms.Guna2ComboBox cmbChucVu;
        private Guna.UI2.WinForms.Guna2NumericUpDown numLuongCoBan;
        private Guna.UI2.WinForms.Guna2Button btnLuu;
        private Guna.UI2.WinForms.Guna2Button btnHuy;
    }
}
