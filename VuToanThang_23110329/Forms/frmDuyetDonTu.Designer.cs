namespace VuToanThang_23110329.Forms
{
    partial class frmDuyetDonTu
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
            this.components = new System.ComponentModel.Container();
            this.lblTitle = new System.Windows.Forms.Label();
            this.pnlFilter = new System.Windows.Forms.Panel();
            this.lblTimKiem = new System.Windows.Forms.Label();
            this.txtTimKiem = new Guna.UI2.WinForms.Guna2TextBox();
            this.lblTrangThai = new System.Windows.Forms.Label();
            this.cmbTrangThai = new Guna.UI2.WinForms.Guna2ComboBox();
            this.lblLoaiDon = new System.Windows.Forms.Label();
            this.cmbLoaiDon = new Guna.UI2.WinForms.Guna2ComboBox();
            this.pnlButtons = new System.Windows.Forms.Panel();
            this.btnDuyet = new Guna.UI2.WinForms.Guna2Button();
            this.btnTuChoi = new Guna.UI2.WinForms.Guna2Button();
            this.btnLamMoi = new Guna.UI2.WinForms.Guna2Button();
            this.pnlMain = new System.Windows.Forms.Panel();
            this.pnlTitle = new System.Windows.Forms.Panel();
            this.pnlContent = new System.Windows.Forms.Panel();
            this.pnlForm = new System.Windows.Forms.Panel();
            this.lblMaDon = new System.Windows.Forms.Label();
            this.txtMaDon = new Guna.UI2.WinForms.Guna2TextBox();
            this.lblNhanVien = new System.Windows.Forms.Label();
            this.txtNhanVien = new Guna.UI2.WinForms.Guna2TextBox();
            this.lblLoaiDonForm = new System.Windows.Forms.Label();
            this.txtLoaiDonForm = new Guna.UI2.WinForms.Guna2TextBox();
            this.lblTuLuc = new System.Windows.Forms.Label();
            this.dtpTuLuc = new Guna.UI2.WinForms.Guna2DateTimePicker();
            this.lblDenLuc = new System.Windows.Forms.Label();
            this.dtpDenLuc = new Guna.UI2.WinForms.Guna2DateTimePicker();
            this.lblSoGio = new System.Windows.Forms.Label();
            this.txtSoGio = new Guna.UI2.WinForms.Guna2TextBox();
            this.lblLyDo = new System.Windows.Forms.Label();
            this.txtLyDo = new Guna.UI2.WinForms.Guna2TextBox();
            this.lblTrangThaiForm = new System.Windows.Forms.Label();
            this.txtTrangThaiForm = new Guna.UI2.WinForms.Guna2TextBox();
            this.pnlMain.SuspendLayout();
            this.pnlFilter.SuspendLayout();
            this.pnlButtons.SuspendLayout();
            this.pnlContent.SuspendLayout();
            this.pnlForm.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(124)))), ((int)(((byte)(77)))), ((int)(((byte)(255)))));
            this.lblTitle.Location = new System.Drawing.Point(20, 20);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(160, 30);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Duyệt Đơn từ";
            // 
            // pnlFilter
            // 
            this.pnlFilter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.pnlFilter.Controls.Add(this.lblTimKiem);
            this.pnlFilter.Controls.Add(this.txtTimKiem);
            this.pnlFilter.Controls.Add(this.lblTrangThai);
            this.pnlFilter.Controls.Add(this.cmbTrangThai);
            this.pnlFilter.Controls.Add(this.lblLoaiDon);
            this.pnlFilter.Controls.Add(this.cmbLoaiDon);
            this.pnlFilter.Location = new System.Drawing.Point(20, 60);
            this.pnlFilter.Name = "pnlFilter";
            this.pnlFilter.Size = new System.Drawing.Size(1160, 60);
            this.pnlFilter.TabIndex = 1;
            // 
            // lblTimKiem
            // 
            this.lblTimKiem.AutoSize = true;
            this.lblTimKiem.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTimKiem.ForeColor = System.Drawing.Color.White;
            this.lblTimKiem.Location = new System.Drawing.Point(20, 20);
            this.lblTimKiem.Name = "lblTimKiem";
            this.lblTimKiem.Size = new System.Drawing.Size(60, 19);
            this.lblTimKiem.TabIndex = 0;
            this.lblTimKiem.Text = "Tìm kiếm:";
            // 
            // txtTimKiem
            // 
            this.txtTimKiem.BorderRadius = 8;
            this.txtTimKiem.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtTimKiem.DefaultText = "";
            this.txtTimKiem.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtTimKiem.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtTimKiem.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtTimKiem.DisabledState.Parent = this.txtTimKiem;
            this.txtTimKiem.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtTimKiem.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.txtTimKiem.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(124)))), ((int)(((byte)(77)))), ((int)(((byte)(255)))));
            this.txtTimKiem.FocusedState.Parent = this.txtTimKiem;
            this.txtTimKiem.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTimKiem.ForeColor = System.Drawing.Color.White;
            this.txtTimKiem.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(124)))), ((int)(((byte)(77)))), ((int)(((byte)(255)))));
            this.txtTimKiem.HoverState.Parent = this.txtTimKiem;
            this.txtTimKiem.Location = new System.Drawing.Point(90, 15);
            this.txtTimKiem.Name = "txtTimKiem";
            this.txtTimKiem.PasswordChar = '\0';
            this.txtTimKiem.PlaceholderText = "Nhập tên nhân viên để tìm...";
            this.txtTimKiem.SelectedText = "";
            this.txtTimKiem.Size = new System.Drawing.Size(200, 30);
            this.txtTimKiem.TabIndex = 1;
            // 
            // lblTrangThai
            // 
            this.lblTrangThai.AutoSize = true;
            this.lblTrangThai.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTrangThai.ForeColor = System.Drawing.Color.White;
            this.lblTrangThai.Location = new System.Drawing.Point(320, 20);
            this.lblTrangThai.Name = "lblTrangThai";
            this.lblTrangThai.Size = new System.Drawing.Size(65, 19);
            this.lblTrangThai.TabIndex = 2;
            this.lblTrangThai.Text = "Trạng thái:";
            // 
            // cmbTrangThai
            // 
            this.cmbTrangThai.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.cmbTrangThai.BorderRadius = 8;
            this.cmbTrangThai.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbTrangThai.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTrangThai.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.cmbTrangThai.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(124)))), ((int)(((byte)(77)))), ((int)(((byte)(255)))));
            this.cmbTrangThai.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(124)))), ((int)(((byte)(77)))), ((int)(((byte)(255)))));
            this.cmbTrangThai.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbTrangThai.ForeColor = System.Drawing.Color.White;
            this.cmbTrangThai.ItemHeight = 30;
            this.cmbTrangThai.Location = new System.Drawing.Point(395, 15);
            this.cmbTrangThai.Name = "cmbTrangThai";
            this.cmbTrangThai.Size = new System.Drawing.Size(120, 30);
            this.cmbTrangThai.TabIndex = 3;
            // 
            // lblLoaiDon
            // 
            this.lblLoaiDon.AutoSize = true;
            this.lblLoaiDon.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLoaiDon.ForeColor = System.Drawing.Color.White;
            this.lblLoaiDon.Location = new System.Drawing.Point(540, 20);
            this.lblLoaiDon.Name = "lblLoaiDon";
            this.lblLoaiDon.Size = new System.Drawing.Size(55, 19);
            this.lblLoaiDon.TabIndex = 4;
            this.lblLoaiDon.Text = "Loại đơn:";
            // 
            // cmbLoaiDon
            // 
            this.cmbLoaiDon.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.cmbLoaiDon.BorderRadius = 8;
            this.cmbLoaiDon.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbLoaiDon.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLoaiDon.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.cmbLoaiDon.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(124)))), ((int)(((byte)(77)))), ((int)(((byte)(255)))));
            this.cmbLoaiDon.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(124)))), ((int)(((byte)(77)))), ((int)(((byte)(255)))));
            this.cmbLoaiDon.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbLoaiDon.ForeColor = System.Drawing.Color.White;
            this.cmbLoaiDon.ItemHeight = 30;
            this.cmbLoaiDon.Location = new System.Drawing.Point(605, 15);
            this.cmbLoaiDon.Name = "cmbLoaiDon";
            this.cmbLoaiDon.Size = new System.Drawing.Size(120, 30);
            this.cmbLoaiDon.TabIndex = 5;
            // 
            // pnlButtons
            // 
            this.pnlButtons.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.pnlButtons.Controls.Add(this.btnDuyet);
            this.pnlButtons.Controls.Add(this.btnTuChoi);
            this.pnlButtons.Controls.Add(this.btnLamMoi);
            this.pnlButtons.Location = new System.Drawing.Point(20, 130);
            this.pnlButtons.Name = "pnlButtons";
            this.pnlButtons.Size = new System.Drawing.Size(1160, 50);
            this.pnlButtons.TabIndex = 2;
            // 
            // btnDuyet
            // 
            this.btnDuyet.BorderRadius = 8;
            this.btnDuyet.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnDuyet.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnDuyet.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnDuyet.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnDuyet.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(175)))), ((int)(((byte)(80)))));
            this.btnDuyet.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDuyet.ForeColor = System.Drawing.Color.White;
            this.btnDuyet.Location = new System.Drawing.Point(20, 10);
            this.btnDuyet.Name = "btnDuyet";
            this.btnDuyet.Size = new System.Drawing.Size(80, 30);
            this.btnDuyet.TabIndex = 0;
            this.btnDuyet.Text = "Duyệt";
            // 
            // btnTuChoi
            // 
            this.btnTuChoi.BorderRadius = 8;
            this.btnTuChoi.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnTuChoi.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnTuChoi.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnTuChoi.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnTuChoi.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(67)))), ((int)(((byte)(54)))));
            this.btnTuChoi.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTuChoi.ForeColor = System.Drawing.Color.White;
            this.btnTuChoi.Location = new System.Drawing.Point(110, 10);
            this.btnTuChoi.Name = "btnTuChoi";
            this.btnTuChoi.Size = new System.Drawing.Size(80, 30);
            this.btnTuChoi.TabIndex = 1;
            this.btnTuChoi.Text = "Từ chối";
            // 
            // btnLamMoi
            // 
            this.btnLamMoi.BorderRadius = 8;
            this.btnLamMoi.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnLamMoi.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnLamMoi.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnLamMoi.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnLamMoi.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(156)))), ((int)(((byte)(39)))), ((int)(((byte)(176)))));
            this.btnLamMoi.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLamMoi.ForeColor = System.Drawing.Color.White;
            this.btnLamMoi.Location = new System.Drawing.Point(200, 10);
            this.btnLamMoi.Name = "btnLamMoi";
            this.btnLamMoi.Size = new System.Drawing.Size(80, 30);
            this.btnLamMoi.TabIndex = 2;
            this.btnLamMoi.Text = "Làm mới";
            // 
            // pnlMain
            // 
            this.pnlMain.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 0);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(1200, 700);
            this.pnlMain.TabIndex = 0;
            // 
            // pnlTitle
            // 
            this.pnlTitle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(124)))), ((int)(((byte)(77)))), ((int)(((byte)(255)))));
            this.pnlTitle.Location = new System.Drawing.Point(0, 0);
            this.pnlTitle.Name = "pnlTitle";
            this.pnlTitle.Size = new System.Drawing.Size(1200, 10);
            this.pnlTitle.TabIndex = 3;
            // 
            // pnlContent
            // 
            this.pnlContent.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.pnlContent.Controls.Add(this.pnlForm);
            this.pnlContent.Location = new System.Drawing.Point(20, 190);
            this.pnlContent.Name = "pnlContent";
            this.pnlContent.Size = new System.Drawing.Size(1160, 490);
            this.pnlContent.TabIndex = 4;
            // 
            // pnlForm
            // 
            this.pnlForm.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.pnlForm.Controls.Add(this.lblMaDon);
            this.pnlForm.Controls.Add(this.txtMaDon);
            this.pnlForm.Controls.Add(this.lblNhanVien);
            this.pnlForm.Controls.Add(this.txtNhanVien);
            this.pnlForm.Controls.Add(this.lblLoaiDonForm);
            this.pnlForm.Controls.Add(this.txtLoaiDonForm);
            this.pnlForm.Controls.Add(this.lblTuLuc);
            this.pnlForm.Controls.Add(this.dtpTuLuc);
            this.pnlForm.Controls.Add(this.lblDenLuc);
            this.pnlForm.Controls.Add(this.dtpDenLuc);
            this.pnlForm.Controls.Add(this.lblSoGio);
            this.pnlForm.Controls.Add(this.txtSoGio);
            this.pnlForm.Controls.Add(this.lblLyDo);
            this.pnlForm.Controls.Add(this.txtLyDo);
            this.pnlForm.Controls.Add(this.lblTrangThaiForm);
            this.pnlForm.Controls.Add(this.txtTrangThaiForm);
            this.pnlForm.Location = new System.Drawing.Point(0, 410);
            this.pnlForm.Name = "pnlForm";
            this.pnlForm.Size = new System.Drawing.Size(1160, 80);
            this.pnlForm.TabIndex = 1;
            // 
            // lblMaDon
            // 
            this.lblMaDon.AutoSize = true;
            this.lblMaDon.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMaDon.ForeColor = System.Drawing.Color.White;
            this.lblMaDon.Location = new System.Drawing.Point(20, 15);
            this.lblMaDon.Name = "lblMaDon";
            this.lblMaDon.Size = new System.Drawing.Size(45, 15);
            this.lblMaDon.TabIndex = 0;
            this.lblMaDon.Text = "Mã đơn:";
            // 
            // txtMaDon
            // 
            this.txtMaDon.BorderRadius = 6;
            this.txtMaDon.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtMaDon.DefaultText = "";
            this.txtMaDon.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtMaDon.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtMaDon.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtMaDon.DisabledState.Parent = this.txtMaDon;
            this.txtMaDon.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtMaDon.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
            this.txtMaDon.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(124)))), ((int)(((byte)(77)))), ((int)(((byte)(255)))));
            this.txtMaDon.FocusedState.Parent = this.txtMaDon;
            this.txtMaDon.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMaDon.ForeColor = System.Drawing.Color.White;
            this.txtMaDon.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(124)))), ((int)(((byte)(77)))), ((int)(((byte)(255)))));
            this.txtMaDon.HoverState.Parent = this.txtMaDon;
            this.txtMaDon.Location = new System.Drawing.Point(75, 10);
            this.txtMaDon.Name = "txtMaDon";
            this.txtMaDon.PasswordChar = '\0';
            this.txtMaDon.PlaceholderText = "";
            this.txtMaDon.SelectedText = "";
            this.txtMaDon.Size = new System.Drawing.Size(80, 25);
            this.txtMaDon.TabIndex = 1;
            // 
            // lblNhanVien
            // 
            this.lblNhanVien.AutoSize = true;
            this.lblNhanVien.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNhanVien.ForeColor = System.Drawing.Color.White;
            this.lblNhanVien.Location = new System.Drawing.Point(175, 15);
            this.lblNhanVien.Name = "lblNhanVien";
            this.lblNhanVien.Size = new System.Drawing.Size(60, 15);
            this.lblNhanVien.TabIndex = 2;
            this.lblNhanVien.Text = "Nhân viên:";
            // 
            // txtNhanVien
            // 
            this.txtNhanVien.BorderRadius = 6;
            this.txtNhanVien.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtNhanVien.DefaultText = "";
            this.txtNhanVien.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtNhanVien.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtNhanVien.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtNhanVien.DisabledState.Parent = this.txtNhanVien;
            this.txtNhanVien.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtNhanVien.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
            this.txtNhanVien.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(124)))), ((int)(((byte)(77)))), ((int)(((byte)(255)))));
            this.txtNhanVien.FocusedState.Parent = this.txtNhanVien;
            this.txtNhanVien.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNhanVien.ForeColor = System.Drawing.Color.White;
            this.txtNhanVien.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(124)))), ((int)(((byte)(77)))), ((int)(((byte)(255)))));
            this.txtNhanVien.HoverState.Parent = this.txtNhanVien;
            this.txtNhanVien.Location = new System.Drawing.Point(245, 10);
            this.txtNhanVien.Name = "txtNhanVien";
            this.txtNhanVien.PasswordChar = '\0';
            this.txtNhanVien.PlaceholderText = "";
            this.txtNhanVien.SelectedText = "";
            this.txtNhanVien.Size = new System.Drawing.Size(120, 25);
            this.txtNhanVien.TabIndex = 3;
            // 
            // lblLoaiDonForm
            // 
            this.lblLoaiDonForm.AutoSize = true;
            this.lblLoaiDonForm.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLoaiDonForm.ForeColor = System.Drawing.Color.White;
            this.lblLoaiDonForm.Location = new System.Drawing.Point(385, 15);
            this.lblLoaiDonForm.Name = "lblLoaiDonForm";
            this.lblLoaiDonForm.Size = new System.Drawing.Size(55, 15);
            this.lblLoaiDonForm.TabIndex = 4;
            this.lblLoaiDonForm.Text = "Loại đơn:";
            // 
            // txtLoaiDonForm
            // 
            this.txtLoaiDonForm.BorderRadius = 6;
            this.txtLoaiDonForm.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtLoaiDonForm.DefaultText = "";
            this.txtLoaiDonForm.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtLoaiDonForm.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtLoaiDonForm.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtLoaiDonForm.DisabledState.Parent = this.txtLoaiDonForm;
            this.txtLoaiDonForm.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtLoaiDonForm.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
            this.txtLoaiDonForm.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(124)))), ((int)(((byte)(77)))), ((int)(((byte)(255)))));
            this.txtLoaiDonForm.FocusedState.Parent = this.txtLoaiDonForm;
            this.txtLoaiDonForm.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLoaiDonForm.ForeColor = System.Drawing.Color.White;
            this.txtLoaiDonForm.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(124)))), ((int)(((byte)(77)))), ((int)(((byte)(255)))));
            this.txtLoaiDonForm.HoverState.Parent = this.txtLoaiDonForm;
            this.txtLoaiDonForm.Location = new System.Drawing.Point(450, 10);
            this.txtLoaiDonForm.Name = "txtLoaiDonForm";
            this.txtLoaiDonForm.PasswordChar = '\0';
            this.txtLoaiDonForm.PlaceholderText = "";
            this.txtLoaiDonForm.SelectedText = "";
            this.txtLoaiDonForm.Size = new System.Drawing.Size(80, 25);
            this.txtLoaiDonForm.TabIndex = 5;
            // 
            // lblTuLuc
            // 
            this.lblTuLuc.AutoSize = true;
            this.lblTuLuc.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTuLuc.ForeColor = System.Drawing.Color.White;
            this.lblTuLuc.Location = new System.Drawing.Point(550, 15);
            this.lblTuLuc.Name = "lblTuLuc";
            this.lblTuLuc.Size = new System.Drawing.Size(40, 15);
            this.lblTuLuc.TabIndex = 6;
            this.lblTuLuc.Text = "Từ lúc:";
            // 
            // dtpTuLuc
            // 
            this.dtpTuLuc.BorderRadius = 6;
            this.dtpTuLuc.CheckedState.Parent = this.dtpTuLuc;
            this.dtpTuLuc.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
            this.dtpTuLuc.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpTuLuc.ForeColor = System.Drawing.Color.White;
            this.dtpTuLuc.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpTuLuc.Location = new System.Drawing.Point(600, 10);
            this.dtpTuLuc.Name = "dtpTuLuc";
            this.dtpTuLuc.Size = new System.Drawing.Size(100, 25);
            this.dtpTuLuc.TabIndex = 7;
            // 
            // lblDenLuc
            // 
            this.lblDenLuc.AutoSize = true;
            this.lblDenLuc.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDenLuc.ForeColor = System.Drawing.Color.White;
            this.lblDenLuc.Location = new System.Drawing.Point(720, 15);
            this.lblDenLuc.Name = "lblDenLuc";
            this.lblDenLuc.Size = new System.Drawing.Size(45, 15);
            this.lblDenLuc.TabIndex = 8;
            this.lblDenLuc.Text = "Đến lúc:";
            // 
            // dtpDenLuc
            // 
            this.dtpDenLuc.BorderRadius = 6;
            this.dtpDenLuc.CheckedState.Parent = this.dtpDenLuc;
            this.dtpDenLuc.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
            this.dtpDenLuc.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpDenLuc.ForeColor = System.Drawing.Color.White;
            this.dtpDenLuc.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDenLuc.Location = new System.Drawing.Point(775, 10);
            this.dtpDenLuc.Name = "dtpDenLuc";
            this.dtpDenLuc.Size = new System.Drawing.Size(100, 25);
            this.dtpDenLuc.TabIndex = 9;
            // 
            // lblSoGio
            // 
            this.lblSoGio.AutoSize = true;
            this.lblSoGio.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSoGio.ForeColor = System.Drawing.Color.White;
            this.lblSoGio.Location = new System.Drawing.Point(895, 15);
            this.lblSoGio.Name = "lblSoGio";
            this.lblSoGio.Size = new System.Drawing.Size(40, 15);
            this.lblSoGio.TabIndex = 10;
            this.lblSoGio.Text = "Số giờ:";
            // 
            // txtSoGio
            // 
            this.txtSoGio.BorderRadius = 6;
            this.txtSoGio.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtSoGio.DefaultText = "";
            this.txtSoGio.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtSoGio.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtSoGio.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtSoGio.DisabledState.Parent = this.txtSoGio;
            this.txtSoGio.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtSoGio.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
            this.txtSoGio.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(124)))), ((int)(((byte)(77)))), ((int)(((byte)(255)))));
            this.txtSoGio.FocusedState.Parent = this.txtSoGio;
            this.txtSoGio.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSoGio.ForeColor = System.Drawing.Color.White;
            this.txtSoGio.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(124)))), ((int)(((byte)(77)))), ((int)(((byte)(255)))));
            this.txtSoGio.HoverState.Parent = this.txtSoGio;
            this.txtSoGio.Location = new System.Drawing.Point(945, 10);
            this.txtSoGio.Name = "txtSoGio";
            this.txtSoGio.PasswordChar = '\0';
            this.txtSoGio.PlaceholderText = "";
            this.txtSoGio.SelectedText = "";
            this.txtSoGio.Size = new System.Drawing.Size(80, 25);
            this.txtSoGio.TabIndex = 11;
            // 
            // lblLyDo
            // 
            this.lblLyDo.AutoSize = true;
            this.lblLyDo.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLyDo.ForeColor = System.Drawing.Color.White;
            this.lblLyDo.Location = new System.Drawing.Point(20, 45);
            this.lblLyDo.Name = "lblLyDo";
            this.lblLyDo.Size = new System.Drawing.Size(40, 15);
            this.lblLyDo.TabIndex = 12;
            this.lblLyDo.Text = "Lý do:";
            // 
            // txtLyDo
            // 
            this.txtLyDo.BorderRadius = 6;
            this.txtLyDo.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtLyDo.DefaultText = "";
            this.txtLyDo.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtLyDo.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtLyDo.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtLyDo.DisabledState.Parent = this.txtLyDo;
            this.txtLyDo.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtLyDo.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
            this.txtLyDo.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(124)))), ((int)(((byte)(77)))), ((int)(((byte)(255)))));
            this.txtLyDo.FocusedState.Parent = this.txtLyDo;
            this.txtLyDo.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLyDo.ForeColor = System.Drawing.Color.White;
            this.txtLyDo.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(124)))), ((int)(((byte)(77)))), ((int)(((byte)(255)))));
            this.txtLyDo.HoverState.Parent = this.txtLyDo;
            this.txtLyDo.Location = new System.Drawing.Point(70, 40);
            this.txtLyDo.Name = "txtLyDo";
            this.txtLyDo.PasswordChar = '\0';
            this.txtLyDo.PlaceholderText = "";
            this.txtLyDo.SelectedText = "";
            this.txtLyDo.Size = new System.Drawing.Size(200, 25);
            this.txtLyDo.TabIndex = 13;
            // 
            // lblTrangThaiForm
            // 
            this.lblTrangThaiForm.AutoSize = true;
            this.lblTrangThaiForm.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTrangThaiForm.ForeColor = System.Drawing.Color.White;
            this.lblTrangThaiForm.Location = new System.Drawing.Point(290, 45);
            this.lblTrangThaiForm.Name = "lblTrangThaiForm";
            this.lblTrangThaiForm.Size = new System.Drawing.Size(65, 15);
            this.lblTrangThaiForm.TabIndex = 14;
            this.lblTrangThaiForm.Text = "Trạng thái:";
            // 
            // txtTrangThaiForm
            // 
            this.txtTrangThaiForm.BorderRadius = 6;
            this.txtTrangThaiForm.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtTrangThaiForm.DefaultText = "";
            this.txtTrangThaiForm.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtTrangThaiForm.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtTrangThaiForm.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtTrangThaiForm.DisabledState.Parent = this.txtTrangThaiForm;
            this.txtTrangThaiForm.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtTrangThaiForm.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
            this.txtTrangThaiForm.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(124)))), ((int)(((byte)(77)))), ((int)(((byte)(255)))));
            this.txtTrangThaiForm.FocusedState.Parent = this.txtTrangThaiForm;
            this.txtTrangThaiForm.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTrangThaiForm.ForeColor = System.Drawing.Color.White;
            this.txtTrangThaiForm.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(124)))), ((int)(((byte)(77)))), ((int)(((byte)(255)))));
            this.txtTrangThaiForm.HoverState.Parent = this.txtTrangThaiForm;
            this.txtTrangThaiForm.Location = new System.Drawing.Point(365, 40);
            this.txtTrangThaiForm.Name = "txtTrangThaiForm";
            this.txtTrangThaiForm.PasswordChar = '\0';
            this.txtTrangThaiForm.PlaceholderText = "";
            this.txtTrangThaiForm.SelectedText = "";
            this.txtTrangThaiForm.Size = new System.Drawing.Size(100, 25);
            this.txtTrangThaiForm.TabIndex = 15;
            // 
            // frmDuyetDonTu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.ClientSize = new System.Drawing.Size(1200, 700);
            this.Controls.Add(this.pnlTitle);
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.pnlFilter);
            this.Controls.Add(this.pnlButtons);
            this.Controls.Add(this.pnlContent);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmDuyetDonTu";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Duyệt Đơn từ";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.pnlMain.ResumeLayout(false);
            this.pnlMain.PerformLayout();
            this.pnlFilter.ResumeLayout(false);
            this.pnlFilter.PerformLayout();
            this.pnlButtons.ResumeLayout(false);
            this.pnlContent.ResumeLayout(false);
            this.pnlForm.ResumeLayout(false);
            this.pnlForm.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Panel pnlFilter;
        private System.Windows.Forms.Label lblTimKiem;
        private Guna.UI2.WinForms.Guna2TextBox txtTimKiem;
        private System.Windows.Forms.Label lblTrangThai;
        private Guna.UI2.WinForms.Guna2ComboBox cmbTrangThai;
        private System.Windows.Forms.Label lblLoaiDon;
        private Guna.UI2.WinForms.Guna2ComboBox cmbLoaiDon;
        private System.Windows.Forms.Panel pnlButtons;
        private Guna.UI2.WinForms.Guna2Button btnDuyet;
        private Guna.UI2.WinForms.Guna2Button btnTuChoi;
        private Guna.UI2.WinForms.Guna2Button btnLamMoi;
        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.Panel pnlTitle;
        private System.Windows.Forms.Panel pnlContent;
        private System.Windows.Forms.Panel pnlForm;
        private System.Windows.Forms.Label lblMaDon;
        private Guna.UI2.WinForms.Guna2TextBox txtMaDon;
        private System.Windows.Forms.Label lblNhanVien;
        private Guna.UI2.WinForms.Guna2TextBox txtNhanVien;
        private System.Windows.Forms.Label lblLoaiDonForm;
        private Guna.UI2.WinForms.Guna2TextBox txtLoaiDonForm;
        private System.Windows.Forms.Label lblTuLuc;
        private Guna.UI2.WinForms.Guna2DateTimePicker dtpTuLuc;
        private System.Windows.Forms.Label lblDenLuc;
        private Guna.UI2.WinForms.Guna2DateTimePicker dtpDenLuc;
        private System.Windows.Forms.Label lblSoGio;
        private Guna.UI2.WinForms.Guna2TextBox txtSoGio;
        private System.Windows.Forms.Label lblLyDo;
        private Guna.UI2.WinForms.Guna2TextBox txtLyDo;
        private System.Windows.Forms.Label lblTrangThaiForm;
        private Guna.UI2.WinForms.Guna2TextBox txtTrangThaiForm;
    }
}
