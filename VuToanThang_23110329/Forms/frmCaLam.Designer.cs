using System;
using System.Windows.Forms;

namespace VuToanThang_23110329.Forms
{
    partial class frmCaLam
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
            this.pnlMain = new Guna.UI2.WinForms.Guna2Panel();
            this.pnlHeader = new Guna.UI2.WinForms.Guna2Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.pnlSearch = new Guna.UI2.WinForms.Guna2Panel();
            this.txtTimKiem = new Guna.UI2.WinForms.Guna2TextBox();
            this.pnlButtons = new Guna.UI2.WinForms.Guna2Panel();
            this.btnThem = new Guna.UI2.WinForms.Guna2Button();
            this.btnSua = new Guna.UI2.WinForms.Guna2Button();
            this.btnXoa = new Guna.UI2.WinForms.Guna2Button();
            this.btnLuu = new Guna.UI2.WinForms.Guna2Button();
            this.btnHuy = new Guna.UI2.WinForms.Guna2Button();
            this.btnLamMoi = new Guna.UI2.WinForms.Guna2Button();
            this.pnlContent = new Guna.UI2.WinForms.Guna2Panel();
            this.dgvCaLam = new Guna.UI2.WinForms.Guna2DataGridView();
            this.pnlForm = new Guna.UI2.WinForms.Guna2Panel();
            this.lblMaCa = new System.Windows.Forms.Label();
            this.txtMaCa = new Guna.UI2.WinForms.Guna2TextBox();
            this.lblTenCa = new System.Windows.Forms.Label();
            this.txtTenCa = new Guna.UI2.WinForms.Guna2TextBox();
            this.lblGioBatDau = new System.Windows.Forms.Label();
            this.dtpGioBatDau = new Guna.UI2.WinForms.Guna2DateTimePicker();
            this.lblGioKetThuc = new System.Windows.Forms.Label();
            this.dtpGioKetThuc = new Guna.UI2.WinForms.Guna2DateTimePicker();
            this.lblHeSoCa = new System.Windows.Forms.Label();
            this.txtHeSoCa = new Guna.UI2.WinForms.Guna2TextBox();
            this.lblMoTa = new System.Windows.Forms.Label();
            this.txtMoTa = new Guna.UI2.WinForms.Guna2TextBox();
            this.chkKichHoat = new Guna.UI2.WinForms.Guna2CheckBox();
            this.pnlMain.SuspendLayout();
            this.pnlHeader.SuspendLayout();
            this.pnlSearch.SuspendLayout();
            this.pnlButtons.SuspendLayout();
            this.pnlContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCaLam)).BeginInit();
            this.pnlForm.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlMain
            // 
            this.pnlMain.BackColor = System.Drawing.Color.White;
            this.pnlMain.Controls.Add(this.pnlContent);
            this.pnlMain.Controls.Add(this.pnlButtons);
            this.pnlMain.Controls.Add(this.pnlSearch);
            this.pnlMain.Controls.Add(this.pnlHeader);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 0);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(1200, 700);
            this.pnlMain.TabIndex = 0;
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.White;
            this.pnlHeader.Controls.Add(this.lblTitle);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(1200, 60);
            this.pnlHeader.TabIndex = 0;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblTitle.Location = new System.Drawing.Point(30, 15);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(194, 32);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Quản lý Ca làm";
            // 
            // pnlSearch
            // 
            this.pnlSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(250)))));
            this.pnlSearch.BorderRadius = 12;
            this.pnlSearch.Controls.Add(this.txtTimKiem);
            this.pnlSearch.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlSearch.Location = new System.Drawing.Point(0, 60);
            this.pnlSearch.Margin = new System.Windows.Forms.Padding(20);
            this.pnlSearch.Name = "pnlSearch";
            this.pnlSearch.Padding = new System.Windows.Forms.Padding(30, 15, 30, 15);
            this.pnlSearch.Size = new System.Drawing.Size(1200, 60);
            this.pnlSearch.TabIndex = 1;
            // 
            // txtTimKiem
            // 
            this.txtTimKiem.BorderRadius = 12;
            this.txtTimKiem.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtTimKiem.DefaultText = "";
            this.txtTimKiem.FillColor = System.Drawing.Color.White;
            this.txtTimKiem.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtTimKiem.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtTimKiem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtTimKiem.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtTimKiem.Location = new System.Drawing.Point(30, 15);
            this.txtTimKiem.Name = "txtTimKiem";
            this.txtTimKiem.PasswordChar = '\0';
            this.txtTimKiem.PlaceholderText = "Tìm kiếm ca làm...";
            this.txtTimKiem.SelectedText = "";
            this.txtTimKiem.Size = new System.Drawing.Size(300, 30);
            this.txtTimKiem.TabIndex = 0;
            this.txtTimKiem.TextChanged += new System.EventHandler(this.txtTimKiem_TextChanged);
            // 
            // pnlButtons
            // 
            this.pnlButtons.BackColor = System.Drawing.Color.White;
            this.pnlButtons.Controls.Add(this.btnLamMoi);
            this.pnlButtons.Controls.Add(this.btnHuy);
            this.pnlButtons.Controls.Add(this.btnLuu);
            this.pnlButtons.Controls.Add(this.btnXoa);
            this.pnlButtons.Controls.Add(this.btnSua);
            this.pnlButtons.Controls.Add(this.btnThem);
            this.pnlButtons.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlButtons.Location = new System.Drawing.Point(0, 120);
            this.pnlButtons.Name = "pnlButtons";
            this.pnlButtons.Padding = new System.Windows.Forms.Padding(30, 10, 30, 10);
            this.pnlButtons.Size = new System.Drawing.Size(1200, 60);
            this.pnlButtons.TabIndex = 2;
            // 
            // btnThem
            // 
            this.btnThem.BorderRadius = 12;
            this.btnThem.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(167)))), ((int)(((byte)(69)))));
            this.btnThem.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnThem.ForeColor = System.Drawing.Color.White;
            this.btnThem.Location = new System.Drawing.Point(30, 15);
            this.btnThem.Name = "btnThem";
            this.btnThem.Size = new System.Drawing.Size(100, 35);
            this.btnThem.TabIndex = 0;
            this.btnThem.Text = "➕ Thêm";
            this.btnThem.Click += new System.EventHandler(this.btnThem_Click);
            // 
            // btnSua
            // 
            this.btnSua.BorderRadius = 12;
            this.btnSua.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(193)))), ((int)(((byte)(7)))));
            this.btnSua.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnSua.ForeColor = System.Drawing.Color.White;
            this.btnSua.Location = new System.Drawing.Point(140, 15);
            this.btnSua.Name = "btnSua";
            this.btnSua.Size = new System.Drawing.Size(100, 35);
            this.btnSua.TabIndex = 1;
            this.btnSua.Text = "✏️ Sửa";
            this.btnSua.Click += new System.EventHandler(this.btnSua_Click);
            // 
            // btnXoa
            // 
            this.btnXoa.BorderRadius = 12;
            this.btnXoa.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(53)))), ((int)(((byte)(69)))));
            this.btnXoa.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnXoa.ForeColor = System.Drawing.Color.White;
            this.btnXoa.Location = new System.Drawing.Point(250, 15);
            this.btnXoa.Name = "btnXoa";
            this.btnXoa.Size = new System.Drawing.Size(100, 35);
            this.btnXoa.TabIndex = 2;
            this.btnXoa.Text = "🗑️ Xóa";
            this.btnXoa.Click += new System.EventHandler(this.btnXoa_Click);
            // 
            // btnLuu
            // 
            this.btnLuu.BorderRadius = 12;
            this.btnLuu.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.btnLuu.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnLuu.ForeColor = System.Drawing.Color.White;
            this.btnLuu.Location = new System.Drawing.Point(360, 15);
            this.btnLuu.Name = "btnLuu";
            this.btnLuu.Size = new System.Drawing.Size(100, 35);
            this.btnLuu.TabIndex = 3;
            this.btnLuu.Text = "💾 Lưu";
            this.btnLuu.Click += new System.EventHandler(this.btnLuu_Click);
            // 
            // btnHuy
            // 
            this.btnHuy.BorderRadius = 12;
            this.btnHuy.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(117)))), ((int)(((byte)(125)))));
            this.btnHuy.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnHuy.ForeColor = System.Drawing.Color.White;
            this.btnHuy.Location = new System.Drawing.Point(470, 15);
            this.btnHuy.Name = "btnHuy";
            this.btnHuy.Size = new System.Drawing.Size(100, 35);
            this.btnHuy.TabIndex = 4;
            this.btnHuy.Text = "❌ Hủy";
            this.btnHuy.Click += new System.EventHandler(this.btnHuy_Click);
            // 
            // btnLamMoi
            // 
            this.btnLamMoi.BorderRadius = 12;
            this.btnLamMoi.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(111)))), ((int)(((byte)(66)))), ((int)(((byte)(193)))));
            this.btnLamMoi.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnLamMoi.ForeColor = System.Drawing.Color.White;
            this.btnLamMoi.Location = new System.Drawing.Point(580, 15);
            this.btnLamMoi.Name = "btnLamMoi";
            this.btnLamMoi.Size = new System.Drawing.Size(100, 35);
            this.btnLamMoi.TabIndex = 5;
            this.btnLamMoi.Text = "🔄 Làm mới";
            this.btnLamMoi.Click += new System.EventHandler(this.btnLamMoi_Click);
            // 
            // pnlContent
            // 
            this.pnlContent.BackColor = System.Drawing.Color.White;
            this.pnlContent.Controls.Add(this.pnlForm);
            this.pnlContent.Controls.Add(this.dgvCaLam);
            this.pnlContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlContent.Location = new System.Drawing.Point(0, 180);
            this.pnlContent.Name = "pnlContent";
            this.pnlContent.Padding = new System.Windows.Forms.Padding(30, 0, 30, 30);
            this.pnlContent.Size = new System.Drawing.Size(1200, 520);
            this.pnlContent.TabIndex = 3;
            // 
            // dgvCaLam
            // 
            this.dgvCaLam.AllowUserToAddRows = false;
            this.dgvCaLam.AllowUserToDeleteRows = false;
            this.dgvCaLam.BackgroundColor = System.Drawing.Color.White;
            this.dgvCaLam.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvCaLam.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCaLam.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvCaLam.Location = new System.Drawing.Point(30, 0);
            this.dgvCaLam.Name = "dgvCaLam";
            this.dgvCaLam.ReadOnly = true;
            this.dgvCaLam.RowHeadersVisible = false;
            this.dgvCaLam.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvCaLam.Size = new System.Drawing.Size(1140, 340);
            this.dgvCaLam.TabIndex = 0;
            this.dgvCaLam.Theme = Guna.UI2.WinForms.Enums.DataGridViewPresetThemes.Light;
            this.dgvCaLam.SelectionChanged += new System.EventHandler(this.dgvCaLam_SelectionChanged);
            // 
            // pnlForm
            // 
            this.pnlForm.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(250)))));
            this.pnlForm.BorderRadius = 12;
            this.pnlForm.Controls.Add(this.chkKichHoat);
            this.pnlForm.Controls.Add(this.txtMoTa);
            this.pnlForm.Controls.Add(this.lblMoTa);
            this.pnlForm.Controls.Add(this.txtHeSoCa);
            this.pnlForm.Controls.Add(this.lblHeSoCa);
            this.pnlForm.Controls.Add(this.dtpGioKetThuc);
            this.pnlForm.Controls.Add(this.lblGioKetThuc);
            this.pnlForm.Controls.Add(this.dtpGioBatDau);
            this.pnlForm.Controls.Add(this.lblGioBatDau);
            this.pnlForm.Controls.Add(this.txtTenCa);
            this.pnlForm.Controls.Add(this.lblTenCa);
            this.pnlForm.Controls.Add(this.txtMaCa);
            this.pnlForm.Controls.Add(this.lblMaCa);
            this.pnlForm.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlForm.Location = new System.Drawing.Point(30, 340);
            this.pnlForm.Name = "pnlForm";
            this.pnlForm.Padding = new System.Windows.Forms.Padding(20);
            this.pnlForm.Size = new System.Drawing.Size(1140, 150);
            this.pnlForm.TabIndex = 1;
            // 
            // lblMaCa
            // 
            this.lblMaCa.AutoSize = true;
            this.lblMaCa.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblMaCa.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblMaCa.Location = new System.Drawing.Point(20, 25);
            this.lblMaCa.Name = "lblMaCa";
            this.lblMaCa.Size = new System.Drawing.Size(51, 19);
            this.lblMaCa.TabIndex = 0;
            this.lblMaCa.Text = "Mã ca:";
            // 
            // txtMaCa
            // 
            this.txtMaCa.BorderRadius = 8;
            this.txtMaCa.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtMaCa.DefaultText = "";
            this.txtMaCa.FillColor = System.Drawing.Color.White;
            this.txtMaCa.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtMaCa.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtMaCa.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtMaCa.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtMaCa.Location = new System.Drawing.Point(20, 50);
            this.txtMaCa.Name = "txtMaCa";
            this.txtMaCa.PasswordChar = '\0';
            this.txtMaCa.PlaceholderText = "Nhập mã ca";
            this.txtMaCa.ReadOnly = true;
            this.txtMaCa.SelectedText = "";
            this.txtMaCa.Size = new System.Drawing.Size(120, 30);
            this.txtMaCa.TabIndex = 1;
            // 
            // lblTenCa
            // 
            this.lblTenCa.AutoSize = true;
            this.lblTenCa.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblTenCa.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblTenCa.Location = new System.Drawing.Point(160, 25);
            this.lblTenCa.Name = "lblTenCa";
            this.lblTenCa.Size = new System.Drawing.Size(56, 19);
            this.lblTenCa.TabIndex = 2;
            this.lblTenCa.Text = "Tên ca:";
            // 
            // txtTenCa
            // 
            this.txtTenCa.BorderRadius = 8;
            this.txtTenCa.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtTenCa.DefaultText = "";
            this.txtTenCa.FillColor = System.Drawing.Color.White;
            this.txtTenCa.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtTenCa.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtTenCa.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtTenCa.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtTenCa.Location = new System.Drawing.Point(160, 50);
            this.txtTenCa.Name = "txtTenCa";
            this.txtTenCa.PasswordChar = '\0';
            this.txtTenCa.PlaceholderText = "Nhập tên ca";
            this.txtTenCa.SelectedText = "";
            this.txtTenCa.Size = new System.Drawing.Size(150, 30);
            this.txtTenCa.TabIndex = 3;
            // 
            // lblGioBatDau
            // 
            this.lblGioBatDau.AutoSize = true;
            this.lblGioBatDau.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblGioBatDau.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblGioBatDau.Location = new System.Drawing.Point(330, 25);
            this.lblGioBatDau.Name = "lblGioBatDau";
            this.lblGioBatDau.Size = new System.Drawing.Size(84, 19);
            this.lblGioBatDau.TabIndex = 4;
            this.lblGioBatDau.Text = "Giờ bắt đầu:";
            // 
            // dtpGioBatDau
            // 
            this.dtpGioBatDau.BorderRadius = 8;
            this.dtpGioBatDau.Checked = true;
            this.dtpGioBatDau.FillColor = System.Drawing.Color.White;
            this.dtpGioBatDau.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.dtpGioBatDau.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.dtpGioBatDau.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dtpGioBatDau.Location = new System.Drawing.Point(330, 50);
            this.dtpGioBatDau.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.dtpGioBatDau.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtpGioBatDau.Name = "dtpGioBatDau";
            this.dtpGioBatDau.ShowUpDown = true;
            this.dtpGioBatDau.Size = new System.Drawing.Size(120, 30);
            this.dtpGioBatDau.TabIndex = 5;
            this.dtpGioBatDau.Value = new System.DateTime(2024, 1, 1, 8, 0, 0, 0);
            // 
            // lblGioKetThuc
            // 
            this.lblGioKetThuc.AutoSize = true;
            this.lblGioKetThuc.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblGioKetThuc.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblGioKetThuc.Location = new System.Drawing.Point(470, 25);
            this.lblGioKetThuc.Name = "lblGioKetThuc";
            this.lblGioKetThuc.Size = new System.Drawing.Size(89, 19);
            this.lblGioKetThuc.TabIndex = 6;
            this.lblGioKetThuc.Text = "Giờ kết thúc:";
            // 
            // dtpGioKetThuc
            // 
            this.dtpGioKetThuc.BorderRadius = 8;
            this.dtpGioKetThuc.Checked = true;
            this.dtpGioKetThuc.FillColor = System.Drawing.Color.White;
            this.dtpGioKetThuc.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.dtpGioKetThuc.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.dtpGioKetThuc.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dtpGioKetThuc.Location = new System.Drawing.Point(470, 50);
            this.dtpGioKetThuc.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.dtpGioKetThuc.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtpGioKetThuc.Name = "dtpGioKetThuc";
            this.dtpGioKetThuc.ShowUpDown = true;
            this.dtpGioKetThuc.Size = new System.Drawing.Size(120, 30);
            this.dtpGioKetThuc.TabIndex = 7;
            this.dtpGioKetThuc.Value = new System.DateTime(2024, 1, 1, 17, 0, 0, 0);
            // 
            // lblHeSoCa
            // 
            this.lblHeSoCa.AutoSize = true;
            this.lblHeSoCa.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblHeSoCa.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblHeSoCa.Location = new System.Drawing.Point(610, 25);
            this.lblHeSoCa.Name = "lblHeSoCa";
            this.lblHeSoCa.Size = new System.Drawing.Size(69, 19);
            this.lblHeSoCa.TabIndex = 8;
            this.lblHeSoCa.Text = "Hệ số ca:";
            // 
            // txtHeSoCa
            // 
            this.txtHeSoCa.BorderRadius = 8;
            this.txtHeSoCa.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtHeSoCa.DefaultText = "1.0";
            this.txtHeSoCa.FillColor = System.Drawing.Color.White;
            this.txtHeSoCa.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtHeSoCa.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtHeSoCa.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtHeSoCa.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtHeSoCa.Location = new System.Drawing.Point(610, 50);
            this.txtHeSoCa.Name = "txtHeSoCa";
            this.txtHeSoCa.PasswordChar = '\0';
            this.txtHeSoCa.PlaceholderText = "VD: 1.0, 1.5";
            this.txtHeSoCa.SelectedText = "";
            this.txtHeSoCa.Size = new System.Drawing.Size(100, 30);
            this.txtHeSoCa.TabIndex = 9;
            // 
            // lblMoTa
            // 
            this.lblMoTa.AutoSize = true;
            this.lblMoTa.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblMoTa.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblMoTa.Location = new System.Drawing.Point(20, 95);
            this.lblMoTa.Name = "lblMoTa";
            this.lblMoTa.Size = new System.Drawing.Size(50, 19);
            this.lblMoTa.TabIndex = 10;
            this.lblMoTa.Text = "Mô tả:";
            // 
            // txtMoTa
            // 
            this.txtMoTa.BorderRadius = 8;
            this.txtMoTa.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtMoTa.DefaultText = "";
            this.txtMoTa.FillColor = System.Drawing.Color.White;
            this.txtMoTa.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtMoTa.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtMoTa.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtMoTa.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtMoTa.Location = new System.Drawing.Point(80, 90);
            this.txtMoTa.Name = "txtMoTa";
            this.txtMoTa.PasswordChar = '\0';
            this.txtMoTa.PlaceholderText = "Nhập mô tả ca làm";
            this.txtMoTa.SelectedText = "";
            this.txtMoTa.Size = new System.Drawing.Size(400, 30);
            this.txtMoTa.TabIndex = 11;
            // 
            // chkKichHoat
            // 
            this.chkKichHoat.AutoSize = true;
            this.chkKichHoat.CheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.chkKichHoat.CheckedState.BorderRadius = 0;
            this.chkKichHoat.CheckedState.BorderThickness = 0;
            this.chkKichHoat.CheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.chkKichHoat.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.chkKichHoat.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.chkKichHoat.Location = new System.Drawing.Point(500, 95);
            this.chkKichHoat.Name = "chkKichHoat";
            this.chkKichHoat.Size = new System.Drawing.Size(90, 23);
            this.chkKichHoat.TabIndex = 12;
            this.chkKichHoat.Text = "Kích hoạt";
            this.chkKichHoat.UncheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(137)))), ((int)(((byte)(149)))));
            this.chkKichHoat.UncheckedState.BorderRadius = 0;
            this.chkKichHoat.UncheckedState.BorderThickness = 0;
            this.chkKichHoat.UncheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(137)))), ((int)(((byte)(149)))));
            // 
            // frmCaLam
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1200, 700);
            this.Controls.Add(this.pnlMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmCaLam";
            this.Text = "Quản lý Ca làm";
            this.Load += new System.EventHandler(this.frmCaLam_Load);
            this.pnlMain.ResumeLayout(false);
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.pnlSearch.ResumeLayout(false);
            this.pnlButtons.ResumeLayout(false);
            this.pnlContent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCaLam)).EndInit();
            this.pnlForm.ResumeLayout(false);
            this.pnlForm.PerformLayout();
            this.ResumeLayout(false);
        }

        private Guna.UI2.WinForms.Guna2Panel pnlMain;
        private Guna.UI2.WinForms.Guna2Panel pnlHeader;
        private System.Windows.Forms.Label lblTitle;
        private Guna.UI2.WinForms.Guna2Panel pnlSearch;
        private Guna.UI2.WinForms.Guna2TextBox txtTimKiem;
        private Guna.UI2.WinForms.Guna2Panel pnlButtons;
        private Guna.UI2.WinForms.Guna2Button btnThem;
        private Guna.UI2.WinForms.Guna2Button btnSua;
        private Guna.UI2.WinForms.Guna2Button btnXoa;
        private Guna.UI2.WinForms.Guna2Button btnLuu;
        private Guna.UI2.WinForms.Guna2Button btnHuy;
        private Guna.UI2.WinForms.Guna2Button btnLamMoi;
        private Guna.UI2.WinForms.Guna2Panel pnlContent;
        private Guna.UI2.WinForms.Guna2DataGridView dgvCaLam;
        private Guna.UI2.WinForms.Guna2Panel pnlForm;
        private System.Windows.Forms.Label lblMaCa;
        private Guna.UI2.WinForms.Guna2TextBox txtMaCa;
        private System.Windows.Forms.Label lblTenCa;
        private Guna.UI2.WinForms.Guna2TextBox txtTenCa;
        private System.Windows.Forms.Label lblGioBatDau;
        private Guna.UI2.WinForms.Guna2DateTimePicker dtpGioBatDau;
        private System.Windows.Forms.Label lblGioKetThuc;
        private Guna.UI2.WinForms.Guna2DateTimePicker dtpGioKetThuc;
        private System.Windows.Forms.Label lblHeSoCa;
        private Guna.UI2.WinForms.Guna2TextBox txtHeSoCa;
        private System.Windows.Forms.Label lblMoTa;
        private Guna.UI2.WinForms.Guna2TextBox txtMoTa;
        private Guna.UI2.WinForms.Guna2CheckBox chkKichHoat;
    }
}
