using Guna.UI2.WinForms;

namespace VuToanThang_23110329.Forms
{
    partial class frmQuanLyNguoiDung
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
            this.pnlMain = new Guna2Panel();
            this.pnlHeader = new Guna2Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.pnlContent = new Guna2Panel();
            this.pnlLeft = new Guna2Panel();
            this.dgvNguoiDung = new Guna2DataGridView();
            this.pnlToolbar = new Guna2Panel();
            this.btnThem = new Guna2Button();
            this.btnCapNhat = new Guna2Button();
            this.btnDoiMatKhau = new Guna2Button();
            this.btnKhoaMo = new Guna2Button();
            this.btnXoa = new Guna2Button();
            this.btnTaiLai = new Guna2Button();
            this.pnlRight = new Guna2Panel();
            this.pnlFormDetails = new Guna2Panel();
            this.lblFormTitle = new System.Windows.Forms.Label();
            this.lblHoTen = new System.Windows.Forms.Label();
            this.txtHoTen = new Guna2TextBox();
            this.lblTenDangNhap = new System.Windows.Forms.Label();
            this.txtTenDangNhap = new Guna2TextBox();
            this.lblMatKhau = new System.Windows.Forms.Label();
            this.txtMatKhau = new Guna2TextBox();
            this.lblVaiTro = new System.Windows.Forms.Label();
            this.cmbVaiTro = new Guna2ComboBox();
            this.chkKichHoat = new Guna2CheckBox();
            this.pnlFormButtons = new Guna2Panel();
            this.btnLuu = new Guna2Button();
            this.btnHuy = new Guna2Button();
            this.lblTrangThai = new System.Windows.Forms.Label();
            
            this.pnlMain.SuspendLayout();
            this.pnlHeader.SuspendLayout();
            this.pnlContent.SuspendLayout();
            this.pnlLeft.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvNguoiDung)).BeginInit();
            this.pnlToolbar.SuspendLayout();
            this.pnlRight.SuspendLayout();
            this.pnlFormDetails.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.pnlContent);
            this.pnlMain.Controls.Add(this.pnlHeader);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.FillColor = System.Drawing.Color.White;
            this.pnlMain.Location = new System.Drawing.Point(0, 0);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(1200, 700);
            this.pnlMain.TabIndex = 0;
            // 
            // pnlHeader
            // 
            this.pnlHeader.Controls.Add(this.lblTitle);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(1200, 60);
            this.pnlHeader.TabIndex = 0;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.BackColor = System.Drawing.Color.Transparent;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(20, 15);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(248, 32);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "👤 Quản lý người dùng";
            // 
            // pnlContent
            // 
            this.pnlContent.Controls.Add(this.pnlRight);
            this.pnlContent.Controls.Add(this.pnlLeft);
            this.pnlContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlContent.FillColor = System.Drawing.Color.White;
            this.pnlContent.Location = new System.Drawing.Point(0, 60);
            this.pnlContent.Name = "pnlContent";
            this.pnlContent.Padding = new System.Windows.Forms.Padding(10);
            this.pnlContent.Size = new System.Drawing.Size(1200, 640);
            this.pnlContent.TabIndex = 1;
            // 
            // pnlLeft
            // 
            this.pnlLeft.Controls.Add(this.dgvNguoiDung);
            this.pnlLeft.Controls.Add(this.pnlToolbar);
            this.pnlLeft.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlLeft.FillColor = System.Drawing.Color.White;
            this.pnlLeft.Location = new System.Drawing.Point(10, 10);
            this.pnlLeft.Name = "pnlLeft";
            this.pnlLeft.Size = new System.Drawing.Size(800, 620);
            this.pnlLeft.TabIndex = 0;
            // 
            // dgvNguoiDung
            // 
            this.dgvNguoiDung.AllowUserToAddRows = false;
            this.dgvNguoiDung.AllowUserToDeleteRows = false;
            this.dgvNguoiDung.BackgroundColor = System.Drawing.Color.White;
            this.dgvNguoiDung.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvNguoiDung.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvNguoiDung.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgvNguoiDung.ColumnHeadersHeight = 40;
            this.dgvNguoiDung.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvNguoiDung.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.dgvNguoiDung.Location = new System.Drawing.Point(0, 60);
            this.dgvNguoiDung.MultiSelect = false;
            this.dgvNguoiDung.Name = "dgvNguoiDung";
            this.dgvNguoiDung.ReadOnly = true;
            this.dgvNguoiDung.RowHeadersVisible = false;
            this.dgvNguoiDung.RowTemplate.Height = 35;
            this.dgvNguoiDung.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvNguoiDung.Size = new System.Drawing.Size(780, 560);
            this.dgvNguoiDung.TabIndex = 1;
            this.dgvNguoiDung.ThemeStyle.AlternatingRowsStyle.BackColor = System.Drawing.Color.White;
            this.dgvNguoiDung.ThemeStyle.AlternatingRowsStyle.Font = null;
            this.dgvNguoiDung.ThemeStyle.AlternatingRowsStyle.ForeColor = System.Drawing.Color.Empty;
            this.dgvNguoiDung.ThemeStyle.AlternatingRowsStyle.SelectionBackColor = System.Drawing.Color.Empty;
            this.dgvNguoiDung.ThemeStyle.AlternatingRowsStyle.SelectionForeColor = System.Drawing.Color.Empty;
            this.dgvNguoiDung.ThemeStyle.BackColor = System.Drawing.Color.White;
            this.dgvNguoiDung.ThemeStyle.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.dgvNguoiDung.ThemeStyle.HeaderStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(88)))), ((int)(((byte)(255)))));
            this.dgvNguoiDung.ThemeStyle.HeaderStyle.BorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgvNguoiDung.ThemeStyle.HeaderStyle.Font = new System.Drawing.Font("Segoe UI", 10.5F);
            this.dgvNguoiDung.ThemeStyle.HeaderStyle.ForeColor = System.Drawing.Color.White;
            this.dgvNguoiDung.ThemeStyle.HeaderStyle.HeaightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvNguoiDung.ThemeStyle.HeaderStyle.Height = 40;
            this.dgvNguoiDung.ThemeStyle.ReadOnly = true;
            this.dgvNguoiDung.ThemeStyle.RowsStyle.BackColor = System.Drawing.Color.White;
            this.dgvNguoiDung.ThemeStyle.RowsStyle.BorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvNguoiDung.ThemeStyle.RowsStyle.Font = new System.Drawing.Font("Segoe UI", 10.5F);
            this.dgvNguoiDung.ThemeStyle.RowsStyle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            this.dgvNguoiDung.ThemeStyle.RowsStyle.Height = 35;
            this.dgvNguoiDung.ThemeStyle.RowsStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.dgvNguoiDung.ThemeStyle.RowsStyle.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            this.dgvNguoiDung.SelectionChanged += new System.EventHandler(this.dgvNguoiDung_SelectionChanged);
            // 
            // pnlToolbar
            // 
            this.pnlToolbar.Controls.Add(this.btnTaiLai);
            this.pnlToolbar.Controls.Add(this.btnThem);
            this.pnlToolbar.Controls.Add(this.btnCapNhat);
            this.pnlToolbar.Controls.Add(this.btnDoiMatKhau);
            this.pnlToolbar.Controls.Add(this.btnKhoaMo);
            this.pnlToolbar.Controls.Add(this.btnXoa);
            this.pnlToolbar.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlToolbar.FillColor = System.Drawing.Color.White;
            this.pnlToolbar.Location = new System.Drawing.Point(0, 0);
            this.pnlToolbar.Name = "pnlToolbar";
            this.pnlToolbar.Size = new System.Drawing.Size(780, 60);
            this.pnlToolbar.TabIndex = 0;
            // 
            // btnTaiLai
            // 
            this.btnTaiLai.BorderRadius = 8;
            this.btnTaiLai.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.btnTaiLai.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnTaiLai.ForeColor = System.Drawing.Color.White;
            this.btnTaiLai.Location = new System.Drawing.Point(10, 15);
            this.btnTaiLai.Name = "btnTaiLai";
            this.btnTaiLai.Size = new System.Drawing.Size(80, 30);
            this.btnTaiLai.TabIndex = 0;
            this.btnTaiLai.Text = "🔄 Tải lại";
            this.btnTaiLai.Click += new System.EventHandler(this.btnTaiLai_Click);
            // 
            // btnThem
            // 
            this.btnThem.BorderRadius = 8;
            this.btnThem.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.btnThem.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnThem.ForeColor = System.Drawing.Color.White;
            this.btnThem.Location = new System.Drawing.Point(100, 15);
            this.btnThem.Name = "btnThem";
            this.btnThem.Size = new System.Drawing.Size(90, 30);
            this.btnThem.TabIndex = 1;
            this.btnThem.Text = "➕ Thêm";
            this.btnThem.Click += new System.EventHandler(this.btnThem_Click);
            // 
            // btnCapNhat
            // 
            this.btnCapNhat.BorderRadius = 8;
            this.btnCapNhat.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(193)))), ((int)(((byte)(7)))));
            this.btnCapNhat.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnCapNhat.ForeColor = System.Drawing.Color.White;
            this.btnCapNhat.Location = new System.Drawing.Point(200, 15);
            this.btnCapNhat.Name = "btnCapNhat";
            this.btnCapNhat.Size = new System.Drawing.Size(90, 30);
            this.btnCapNhat.TabIndex = 2;
            this.btnCapNhat.Text = "✏️ Cập nhật";
            this.btnCapNhat.Click += new System.EventHandler(this.btnCapNhat_Click);
            // 
            // btnDoiMatKhau
            // 
            this.btnDoiMatKhau.BorderRadius = 8;
            this.btnDoiMatKhau.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(162)))), ((int)(((byte)(184)))));
            this.btnDoiMatKhau.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnDoiMatKhau.ForeColor = System.Drawing.Color.White;
            this.btnDoiMatKhau.Location = new System.Drawing.Point(300, 15);
            this.btnDoiMatKhau.Name = "btnDoiMatKhau";
            this.btnDoiMatKhau.Size = new System.Drawing.Size(100, 30);
            this.btnDoiMatKhau.TabIndex = 3;
            this.btnDoiMatKhau.Text = "🔑 Đổi MK";
            this.btnDoiMatKhau.Click += new System.EventHandler(this.btnDoiMatKhau_Click);
            // 
            // btnKhoaMo
            // 
            this.btnKhoaMo.BorderRadius = 8;
            this.btnKhoaMo.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(117)))), ((int)(((byte)(125)))));
            this.btnKhoaMo.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnKhoaMo.ForeColor = System.Drawing.Color.White;
            this.btnKhoaMo.Location = new System.Drawing.Point(410, 15);
            this.btnKhoaMo.Name = "btnKhoaMo";
            this.btnKhoaMo.Size = new System.Drawing.Size(90, 30);
            this.btnKhoaMo.TabIndex = 4;
            this.btnKhoaMo.Text = "🔒 Khóa/Mở";
            this.btnKhoaMo.Click += new System.EventHandler(this.btnKhoaMo_Click);
            // 
            // btnXoa
            // 
            this.btnXoa.BorderRadius = 8;
            this.btnXoa.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(53)))), ((int)(((byte)(69)))));
            this.btnXoa.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnXoa.ForeColor = System.Drawing.Color.White;
            this.btnXoa.Location = new System.Drawing.Point(510, 15);
            this.btnXoa.Name = "btnXoa";
            this.btnXoa.Size = new System.Drawing.Size(80, 30);
            this.btnXoa.TabIndex = 5;
            this.btnXoa.Text = "🗑️ Xóa";
            this.btnXoa.Click += new System.EventHandler(this.btnXoa_Click);
            // 
            // pnlRight
            // 
            this.pnlRight.Controls.Add(this.pnlFormDetails);
            this.pnlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlRight.FillColor = System.Drawing.Color.White;
            this.pnlRight.Location = new System.Drawing.Point(850, 10);
            this.pnlRight.Name = "pnlRight";
            this.pnlRight.Size = new System.Drawing.Size(340, 620);
            this.pnlRight.TabIndex = 1;
            // 
            // pnlFormDetails
            // 
            this.pnlFormDetails.Controls.Add(this.lblFormTitle);
            this.pnlFormDetails.Controls.Add(this.lblHoTen);
            this.pnlFormDetails.Controls.Add(this.txtHoTen);
            this.pnlFormDetails.Controls.Add(this.lblTenDangNhap);
            this.pnlFormDetails.Controls.Add(this.txtTenDangNhap);
            this.pnlFormDetails.Controls.Add(this.lblMatKhau);
            this.pnlFormDetails.Controls.Add(this.txtMatKhau);
            this.pnlFormDetails.Controls.Add(this.lblVaiTro);
            this.pnlFormDetails.Controls.Add(this.cmbVaiTro);
            this.pnlFormDetails.Controls.Add(this.chkKichHoat);
            this.pnlFormDetails.Controls.Add(this.pnlFormButtons);
            this.pnlFormDetails.Controls.Add(this.lblTrangThai);
            this.pnlFormDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlFormDetails.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(250)))));
            this.pnlFormDetails.Location = new System.Drawing.Point(0, 0);
            this.pnlFormDetails.Name = "pnlFormDetails";
            this.pnlFormDetails.Padding = new System.Windows.Forms.Padding(20);
            this.pnlFormDetails.Size = new System.Drawing.Size(340, 620);
            this.pnlFormDetails.TabIndex = 0;
            // 
            // lblFormTitle
            // 
            this.lblFormTitle.AutoSize = true;
            this.lblFormTitle.BackColor = System.Drawing.Color.Transparent;
            this.lblFormTitle.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblFormTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.lblFormTitle.Location = new System.Drawing.Point(20, 20);
            this.lblFormTitle.Name = "lblFormTitle";
            this.lblFormTitle.Size = new System.Drawing.Size(156, 25);
            this.lblFormTitle.TabIndex = 0;
            this.lblFormTitle.Text = "Thông tin chi tiết";
            // 
            // lblHoTen
            // 
            this.lblHoTen.AutoSize = true;
            this.lblHoTen.BackColor = System.Drawing.Color.Transparent;
            this.lblHoTen.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblHoTen.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblHoTen.Location = new System.Drawing.Point(20, 60);
            this.lblHoTen.Name = "lblHoTen";
            this.lblHoTen.Size = new System.Drawing.Size(48, 15);
            this.lblHoTen.TabIndex = 1;
            this.lblHoTen.Text = "Họ tên";
            // 
            // txtHoTen
            // 
            this.txtHoTen.BorderRadius = 8;
            this.txtHoTen.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtHoTen.DefaultText = "";
            this.txtHoTen.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtHoTen.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtHoTen.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtHoTen.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtHoTen.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtHoTen.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtHoTen.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtHoTen.Location = new System.Drawing.Point(20, 80);
            this.txtHoTen.Name = "txtHoTen";
            this.txtHoTen.PasswordChar = '\0';
            this.txtHoTen.PlaceholderText = "Nhập họ tên";
            this.txtHoTen.SelectedText = "";
            this.txtHoTen.Size = new System.Drawing.Size(300, 40);
            this.txtHoTen.TabIndex = 2;
            // 
            // lblTenDangNhap
            // 
            this.lblTenDangNhap.AutoSize = true;
            this.lblTenDangNhap.BackColor = System.Drawing.Color.Transparent;
            this.lblTenDangNhap.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblTenDangNhap.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblTenDangNhap.Location = new System.Drawing.Point(20, 130);
            this.lblTenDangNhap.Name = "lblTenDangNhap";
            this.lblTenDangNhap.Size = new System.Drawing.Size(95, 15);
            this.lblTenDangNhap.TabIndex = 3;
            this.lblTenDangNhap.Text = "Tên đăng nhập";
            // 
            // txtTenDangNhap
            // 
            this.txtTenDangNhap.BorderRadius = 8;
            this.txtTenDangNhap.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtTenDangNhap.DefaultText = "";
            this.txtTenDangNhap.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtTenDangNhap.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtTenDangNhap.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtTenDangNhap.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtTenDangNhap.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtTenDangNhap.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtTenDangNhap.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtTenDangNhap.Location = new System.Drawing.Point(20, 150);
            this.txtTenDangNhap.Name = "txtTenDangNhap";
            this.txtTenDangNhap.PasswordChar = '\0';
            this.txtTenDangNhap.PlaceholderText = "Nhập tên đăng nhập";
            this.txtTenDangNhap.SelectedText = "";
            this.txtTenDangNhap.Size = new System.Drawing.Size(300, 40);
            this.txtTenDangNhap.TabIndex = 4;
            // 
            // txtMatKhau
            // 
            this.txtMatKhau.BorderRadius = 8;
            this.txtMatKhau.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtMatKhau.DefaultText = "";
            this.txtMatKhau.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtMatKhau.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtMatKhau.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtMatKhau.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            // 
            // lblMatKhau
            // 
            this.lblMatKhau.AutoSize = true;
            this.lblMatKhau.BackColor = System.Drawing.Color.Transparent;
            this.lblMatKhau.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblMatKhau.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblMatKhau.Location = new System.Drawing.Point(20, 200);
            this.lblMatKhau.Name = "lblMatKhau";
            this.lblMatKhau.Size = new System.Drawing.Size(62, 15);
            this.lblMatKhau.TabIndex = 5;
            this.lblMatKhau.Text = "Mật khẩu";
            // 
            // txtMatKhau
            // 
            this.txtMatKhau.BorderRadius = 8;
            this.txtMatKhau.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtMatKhau.DefaultText = "";
            this.txtMatKhau.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtMatKhau.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtMatKhau.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtMatKhau.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtMatKhau.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtMatKhau.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtMatKhau.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtMatKhau.Location = new System.Drawing.Point(20, 220);
            this.txtMatKhau.Name = "txtMatKhau";
            this.txtMatKhau.PasswordChar = '*';
            this.txtMatKhau.PlaceholderText = "Nhập mật khẩu";
            this.txtMatKhau.SelectedText = "";
            this.txtMatKhau.Size = new System.Drawing.Size(300, 40);
            this.txtMatKhau.TabIndex = 6;
            // 
            // lblVaiTro
            // 
            this.lblVaiTro.AutoSize = true;
            this.lblVaiTro.BackColor = System.Drawing.Color.Transparent;
            this.lblVaiTro.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblVaiTro.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblVaiTro.Location = new System.Drawing.Point(20, 270);
            this.lblVaiTro.Name = "lblVaiTro";
            this.lblVaiTro.Size = new System.Drawing.Size(47, 15);
            this.lblVaiTro.TabIndex = 7;
            this.lblVaiTro.Text = "Vai trò";
            // 
            // cmbVaiTro
            // 
            this.cmbVaiTro.BackColor = System.Drawing.Color.Transparent;
            this.cmbVaiTro.BorderRadius = 8;
            this.cmbVaiTro.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbVaiTro.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbVaiTro.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cmbVaiTro.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cmbVaiTro.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbVaiTro.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(88)))), ((int)(((byte)(112)))));
            this.cmbVaiTro.ItemHeight = 30;
            this.cmbVaiTro.Items.AddRange(new object[] {
            "HR",
            "QuanLy", 
            "KeToan",
            "NhanVien"});
            this.cmbVaiTro.Location = new System.Drawing.Point(20, 290);
            this.cmbVaiTro.Name = "cmbVaiTro";
            this.cmbVaiTro.Size = new System.Drawing.Size(300, 36);
            this.cmbVaiTro.TabIndex = 8;
            // 
            // chkKichHoat
            // 
            this.chkKichHoat.AutoSize = true;
            this.chkKichHoat.CheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.chkKichHoat.CheckedState.BorderRadius = 0;
            this.chkKichHoat.CheckedState.BorderThickness = 0;
            this.chkKichHoat.CheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.chkKichHoat.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.chkKichHoat.Location = new System.Drawing.Point(20, 340);
            this.chkKichHoat.Name = "chkKichHoat";
            this.chkKichHoat.Size = new System.Drawing.Size(89, 23);
            this.chkKichHoat.TabIndex = 9;
            this.chkKichHoat.Text = "Kích hoạt";
            this.chkKichHoat.UncheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(137)))), ((int)(((byte)(149)))));
            this.chkKichHoat.UncheckedState.BorderRadius = 0;
            this.chkKichHoat.UncheckedState.BorderThickness = 0;
            this.chkKichHoat.UncheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(137)))), ((int)(((byte)(149)))));
            // 
            // pnlFormButtons
            // 
            this.pnlFormButtons.Controls.Add(this.btnLuu);
            this.pnlFormButtons.Controls.Add(this.btnHuy);
            this.pnlFormButtons.FillColor = System.Drawing.Color.Transparent;
            this.pnlFormButtons.Location = new System.Drawing.Point(20, 380);
            this.pnlFormButtons.Name = "pnlFormButtons";
            this.pnlFormButtons.Size = new System.Drawing.Size(300, 50);
            this.pnlFormButtons.TabIndex = 10;
            this.pnlFormButtons.Visible = false;
            // 
            // btnLuu
            // 
            this.btnLuu.BorderRadius = 8;
            this.btnLuu.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.btnLuu.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnLuu.ForeColor = System.Drawing.Color.White;
            this.btnLuu.Location = new System.Drawing.Point(0, 10);
            this.btnLuu.Name = "btnLuu";
            this.btnLuu.Size = new System.Drawing.Size(120, 35);
            this.btnLuu.TabIndex = 0;
            this.btnLuu.Text = "✓ Lưu";
            this.btnLuu.Click += new System.EventHandler(this.btnLuu_Click);
            // 
            // btnHuy
            // 
            this.btnHuy.BorderRadius = 8;
            this.btnHuy.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(117)))), ((int)(((byte)(125)))));
            this.btnHuy.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnHuy.ForeColor = System.Drawing.Color.White;
            this.btnHuy.Location = new System.Drawing.Point(130, 10);
            this.btnHuy.Name = "btnHuy";
            this.btnHuy.Size = new System.Drawing.Size(120, 35);
            this.btnHuy.TabIndex = 1;
            this.btnHuy.Text = "✖ Hủy";
            this.btnHuy.Click += new System.EventHandler(this.btnHuy_Click);
            // 
            // lblTrangThai
            // 
            this.lblTrangThai.AutoSize = true;
            this.lblTrangThai.BackColor = System.Drawing.Color.Transparent;
            this.lblTrangThai.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblTrangThai.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.lblTrangThai.Location = new System.Drawing.Point(20, 450);
            this.lblTrangThai.Name = "lblTrangThai";
            this.lblTrangThai.Size = new System.Drawing.Size(0, 19);
            this.lblTrangThai.TabIndex = 11;
            // 
            // frmQuanLyNguoiDung
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1200, 700);
            this.Controls.Add(this.pnlMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmQuanLyNguoiDung";
            this.Text = "Quản Lý Người Dùng";
            this.Load += new System.EventHandler(this.frmQuanLyNguoiDung_Load);
            this.pnlMain.ResumeLayout(false);
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.pnlContent.ResumeLayout(false);
            this.pnlLeft.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvNguoiDung)).EndInit();
            this.pnlToolbar.ResumeLayout(false);
            this.pnlRight.ResumeLayout(false);
            this.pnlFormDetails.ResumeLayout(false);
            this.pnlFormDetails.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Guna2Panel pnlMain;
        private Guna2Panel pnlHeader;
        private System.Windows.Forms.Label lblTitle;
        private Guna2Panel pnlContent;
        private Guna2Panel pnlLeft;
        private Guna2DataGridView dgvNguoiDung;
        private Guna2Panel pnlToolbar;
        private Guna2Button btnTaiLai;
        private Guna2Button btnThem;
        private Guna2Button btnCapNhat;
        private Guna2Button btnDoiMatKhau;
        private Guna2Button btnKhoaMo;
        private Guna2Button btnXoa;
        private Guna2Panel pnlRight;
        private Guna2Panel pnlFormDetails;
        private System.Windows.Forms.Label lblFormTitle;
        private System.Windows.Forms.Label lblHoTen;
        private Guna2TextBox txtHoTen;
        private System.Windows.Forms.Label lblTenDangNhap;
        private Guna2TextBox txtTenDangNhap;
        private System.Windows.Forms.Label lblMatKhau;
        private Guna2TextBox txtMatKhau;
        private System.Windows.Forms.Label lblVaiTro;
        private Guna2ComboBox cmbVaiTro;
        private Guna2CheckBox chkKichHoat;
        private Guna2Panel pnlFormButtons;
        private Guna2Button btnLuu;
        private Guna2Button btnHuy;
        private System.Windows.Forms.Label lblTrangThai;
    }
}
