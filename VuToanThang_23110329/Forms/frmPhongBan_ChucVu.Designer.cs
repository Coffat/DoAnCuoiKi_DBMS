namespace VuToanThang_23110329.Forms
{
    partial class frmPhongBan_ChucVu
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
            this.pnlMain = new System.Windows.Forms.Panel();
            this.pnlTitle = new System.Windows.Forms.Panel();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPhongBan = new System.Windows.Forms.TabPage();
            this.tabChucVu = new System.Windows.Forms.TabPage();
            this.pnlPhongBan = new System.Windows.Forms.Panel();
            this.dgvPhongBan = new Guna.UI2.WinForms.Guna2DataGridView();
            this.lblMaPhongBan = new System.Windows.Forms.Label();
            this.txtMaPhongBan = new Guna.UI2.WinForms.Guna2TextBox();
            this.lblTenPhongBan = new System.Windows.Forms.Label();
            this.txtTenPhongBan = new Guna.UI2.WinForms.Guna2TextBox();
            this.lblMoTaPhongBan = new System.Windows.Forms.Label();
            this.txtMoTaPhongBan = new Guna.UI2.WinForms.Guna2TextBox();
            this.btnThemPhongBan = new Guna.UI2.WinForms.Guna2Button();
            this.btnSuaPhongBan = new Guna.UI2.WinForms.Guna2Button();
            this.btnXoaPhongBan = new Guna.UI2.WinForms.Guna2Button();
            this.btnLuu = new Guna.UI2.WinForms.Guna2Button();
            this.btnHuy = new Guna.UI2.WinForms.Guna2Button();
            this.btnLamMoi = new Guna.UI2.WinForms.Guna2Button();
            this.pnlChucVu = new System.Windows.Forms.Panel();
            this.dgvChucVu = new Guna.UI2.WinForms.Guna2DataGridView();
            this.lblMaChucVu = new System.Windows.Forms.Label();
            this.txtMaChucVu = new Guna.UI2.WinForms.Guna2TextBox();
            this.lblTenChucVu = new System.Windows.Forms.Label();
            this.txtTenChucVu = new Guna.UI2.WinForms.Guna2TextBox();
            this.lblMoTaChucVu = new System.Windows.Forms.Label();
            this.txtMoTaChucVu = new Guna.UI2.WinForms.Guna2TextBox();
            this.btnThemChucVu = new Guna.UI2.WinForms.Guna2Button();
            this.btnSuaChucVu = new Guna.UI2.WinForms.Guna2Button();
            this.btnXoaChucVu = new Guna.UI2.WinForms.Guna2Button();
            this.pnlMain.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.tabPhongBan.SuspendLayout();
            this.tabChucVu.SuspendLayout();
            this.pnlPhongBan.SuspendLayout();
            this.pnlChucVu.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.lblTitle.Location = new System.Drawing.Point(20, 20);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(220, 30);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Quản lý Phòng ban và Chức vụ";
            // 
            // pnlMain
            // 
            this.pnlMain.BackColor = System.Drawing.Color.White;
            this.pnlMain.Controls.Add(this.tabControl);
            this.pnlMain.Controls.Add(this.lblTitle);
            this.pnlMain.Controls.Add(this.pnlTitle);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 0);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(1200, 700);
            this.pnlMain.TabIndex = 0;
            // 
            // pnlTitle
            // 
            this.pnlTitle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.pnlTitle.Location = new System.Drawing.Point(0, 0);
            this.pnlTitle.Name = "pnlTitle";
            this.pnlTitle.Size = new System.Drawing.Size(1200, 10);
            this.pnlTitle.TabIndex = 1;
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabPhongBan);
            this.tabControl.Controls.Add(this.tabChucVu);
            this.tabControl.Location = new System.Drawing.Point(20, 60);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(1160, 620);
            this.tabControl.TabIndex = 2;
            // 
            // tabPhongBan
            // 
            this.tabPhongBan.BackColor = System.Drawing.Color.White;
            this.tabPhongBan.Controls.Add(this.pnlPhongBan);
            this.tabPhongBan.Controls.Add(this.btnLamMoi);
            this.tabPhongBan.Controls.Add(this.btnHuy);
            this.tabPhongBan.Controls.Add(this.btnLuu);
            this.tabPhongBan.Location = new System.Drawing.Point(4, 22);
            this.tabPhongBan.Name = "tabPhongBan";
            this.tabPhongBan.Padding = new System.Windows.Forms.Padding(3);
            this.tabPhongBan.Size = new System.Drawing.Size(1152, 594);
            this.tabPhongBan.TabIndex = 0;
            this.tabPhongBan.Text = "Phòng ban";
            // 
            // tabChucVu
            // 
            this.tabChucVu.BackColor = System.Drawing.Color.White;
            this.tabChucVu.Controls.Add(this.pnlChucVu);
            this.tabChucVu.Location = new System.Drawing.Point(4, 22);
            this.tabChucVu.Name = "tabChucVu";
            this.tabChucVu.Padding = new System.Windows.Forms.Padding(3);
            this.tabChucVu.Size = new System.Drawing.Size(1152, 594);
            this.tabChucVu.TabIndex = 1;
            this.tabChucVu.Text = "Chức vụ";
            // 
            // pnlPhongBan
            // 
            this.pnlPhongBan.BackColor = System.Drawing.Color.White;
            this.pnlPhongBan.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.pnlPhongBan.Controls.Add(this.lblMaPhongBan);
            this.pnlPhongBan.Controls.Add(this.txtMaPhongBan);
            this.pnlPhongBan.Controls.Add(this.lblTenPhongBan);
            this.pnlPhongBan.Controls.Add(this.txtTenPhongBan);
            this.pnlPhongBan.Controls.Add(this.lblMoTaPhongBan);
            this.pnlPhongBan.Controls.Add(this.txtMoTaPhongBan);
            this.pnlPhongBan.Controls.Add(this.btnThemPhongBan);
            this.pnlPhongBan.Controls.Add(this.btnSuaPhongBan);
            this.pnlPhongBan.Controls.Add(this.btnXoaPhongBan);
            this.pnlPhongBan.Controls.Add(this.dgvPhongBan);
            this.pnlPhongBan.Location = new System.Drawing.Point(20, 20);
            this.pnlPhongBan.Name = "pnlPhongBan";
            this.pnlPhongBan.Size = new System.Drawing.Size(1112, 554);
            this.pnlPhongBan.TabIndex = 0;
            // 
            // dgvPhongBan
            // 
            this.dgvPhongBan.AllowUserToAddRows = false;
            this.dgvPhongBan.AllowUserToDeleteRows = false;
            this.dgvPhongBan.BackgroundColor = System.Drawing.Color.White;
            this.dgvPhongBan.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvPhongBan.ColumnHeadersHeight = 40;
            this.dgvPhongBan.EnableHeadersVisualStyles = false;
            this.dgvPhongBan.Location = new System.Drawing.Point(20, 120);
            this.dgvPhongBan.Name = "dgvPhongBan";
            this.dgvPhongBan.ReadOnly = true;
            this.dgvPhongBan.RowHeadersVisible = false;
            this.dgvPhongBan.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvPhongBan.Size = new System.Drawing.Size(1070, 410);
            this.dgvPhongBan.TabIndex = 12;
            // 
            // lblMaPhongBan
            // 
            this.lblMaPhongBan.AutoSize = true;
            this.lblMaPhongBan.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMaPhongBan.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblMaPhongBan.Location = new System.Drawing.Point(20, 20);
            this.lblMaPhongBan.Name = "lblMaPhongBan";
            this.lblMaPhongBan.Size = new System.Drawing.Size(70, 19);
            this.lblMaPhongBan.TabIndex = 0;
            this.lblMaPhongBan.Text = "Mã phòng ban:";
            // 
            // txtMaPhongBan
            // 
            this.txtMaPhongBan.BorderRadius = 8;
            this.txtMaPhongBan.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtMaPhongBan.DefaultText = "";
            this.txtMaPhongBan.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtMaPhongBan.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtMaPhongBan.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtMaPhongBan.DisabledState.Parent = this.txtMaPhongBan;
            this.txtMaPhongBan.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtMaPhongBan.FillColor = System.Drawing.Color.White;
            this.txtMaPhongBan.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(124)))), ((int)(((byte)(77)))), ((int)(((byte)(255)))));
            this.txtMaPhongBan.FocusedState.Parent = this.txtMaPhongBan;
            this.txtMaPhongBan.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMaPhongBan.ForeColor = System.Drawing.Color.Black;
            this.txtMaPhongBan.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(124)))), ((int)(((byte)(77)))), ((int)(((byte)(255)))));
            this.txtMaPhongBan.HoverState.Parent = this.txtMaPhongBan;
            this.txtMaPhongBan.Location = new System.Drawing.Point(160, 15);
            this.txtMaPhongBan.Name = "txtMaPhongBan";
            this.txtMaPhongBan.PasswordChar = '\0';
            this.txtMaPhongBan.PlaceholderText = "";
            this.txtMaPhongBan.SelectedText = "";
            this.txtMaPhongBan.Size = new System.Drawing.Size(100, 30);
            this.txtMaPhongBan.TabIndex = 1;
            // 
            // lblTenPhongBan
            // 
            this.lblTenPhongBan.AutoSize = true;
            this.lblTenPhongBan.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTenPhongBan.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblTenPhongBan.Location = new System.Drawing.Point(220, 20);
            this.lblTenPhongBan.Name = "lblTenPhongBan";
            this.lblTenPhongBan.Size = new System.Drawing.Size(80, 19);
            this.lblTenPhongBan.TabIndex = 2;
            this.lblTenPhongBan.Text = "Tên phòng ban:";
            // 
            // txtTenPhongBan
            // 
            this.txtTenPhongBan.BorderRadius = 8;
            this.txtTenPhongBan.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtTenPhongBan.DefaultText = "";
            this.txtTenPhongBan.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtTenPhongBan.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtTenPhongBan.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtTenPhongBan.DisabledState.Parent = this.txtTenPhongBan;
            this.txtTenPhongBan.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtTenPhongBan.FillColor = System.Drawing.Color.White;
            this.txtTenPhongBan.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(124)))), ((int)(((byte)(77)))), ((int)(((byte)(255)))));
            this.txtTenPhongBan.FocusedState.Parent = this.txtTenPhongBan;
            this.txtTenPhongBan.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTenPhongBan.ForeColor = System.Drawing.Color.Black;
            this.txtTenPhongBan.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(124)))), ((int)(((byte)(77)))), ((int)(((byte)(255)))));
            this.txtTenPhongBan.HoverState.Parent = this.txtTenPhongBan;
            this.txtTenPhongBan.Location = new System.Drawing.Point(360, 15);
            this.txtTenPhongBan.Name = "txtTenPhongBan";
            this.txtTenPhongBan.PasswordChar = '\0';
            this.txtTenPhongBan.PlaceholderText = "";
            this.txtTenPhongBan.SelectedText = "";
            this.txtTenPhongBan.Size = new System.Drawing.Size(200, 30);
            this.txtTenPhongBan.TabIndex = 3;
            // 
            // lblMoTaPhongBan
            // 
            this.lblMoTaPhongBan.AutoSize = true;
            this.lblMoTaPhongBan.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMoTaPhongBan.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblMoTaPhongBan.Location = new System.Drawing.Point(530, 20);
            this.lblMoTaPhongBan.Name = "lblMoTaPhongBan";
            this.lblMoTaPhongBan.Size = new System.Drawing.Size(50, 19);
            this.lblMoTaPhongBan.TabIndex = 4;
            this.lblMoTaPhongBan.Text = "Mô tả:";
            // 
            // txtMoTaPhongBan
            // 
            this.txtMoTaPhongBan.BorderRadius = 8;
            this.txtMoTaPhongBan.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtMoTaPhongBan.DefaultText = "";
            this.txtMoTaPhongBan.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtMoTaPhongBan.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtMoTaPhongBan.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtMoTaPhongBan.DisabledState.Parent = this.txtMoTaPhongBan;
            this.txtMoTaPhongBan.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtMoTaPhongBan.FillColor = System.Drawing.Color.White;
            this.txtMoTaPhongBan.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(124)))), ((int)(((byte)(77)))), ((int)(((byte)(255)))));
            this.txtMoTaPhongBan.FocusedState.Parent = this.txtMoTaPhongBan;
            this.txtMoTaPhongBan.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMoTaPhongBan.ForeColor = System.Drawing.Color.Black;
            this.txtMoTaPhongBan.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(124)))), ((int)(((byte)(77)))), ((int)(((byte)(255)))));
            this.txtMoTaPhongBan.HoverState.Parent = this.txtMoTaPhongBan;
            this.txtMoTaPhongBan.Location = new System.Drawing.Point(590, 15);
            this.txtMoTaPhongBan.Name = "txtMoTaPhongBan";
            this.txtMoTaPhongBan.PasswordChar = '\0';
            this.txtMoTaPhongBan.PlaceholderText = "";
            this.txtMoTaPhongBan.SelectedText = "";
            this.txtMoTaPhongBan.Size = new System.Drawing.Size(300, 30);
            this.txtMoTaPhongBan.TabIndex = 5;
            // 
            // btnThemPhongBan
            // 
            this.btnThemPhongBan.BorderRadius = 12;
            this.btnThemPhongBan.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnThemPhongBan.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnThemPhongBan.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnThemPhongBan.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnThemPhongBan.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(175)))), ((int)(((byte)(80)))));
            this.btnThemPhongBan.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnThemPhongBan.ForeColor = System.Drawing.Color.White;
            this.btnThemPhongBan.Location = new System.Drawing.Point(20, 70);
            this.btnThemPhongBan.Name = "btnThemPhongBan";
            this.btnThemPhongBan.Size = new System.Drawing.Size(80, 30);
            this.btnThemPhongBan.TabIndex = 6;
            this.btnThemPhongBan.Text = "Thêm";
            // 
            // btnSuaPhongBan
            // 
            this.btnSuaPhongBan.BorderRadius = 12;
            this.btnSuaPhongBan.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnSuaPhongBan.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnSuaPhongBan.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnSuaPhongBan.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnSuaPhongBan.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(193)))), ((int)(((byte)(7)))));
            this.btnSuaPhongBan.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSuaPhongBan.ForeColor = System.Drawing.Color.White;
            this.btnSuaPhongBan.Location = new System.Drawing.Point(110, 70);
            this.btnSuaPhongBan.Name = "btnSuaPhongBan";
            this.btnSuaPhongBan.Size = new System.Drawing.Size(80, 30);
            this.btnSuaPhongBan.TabIndex = 7;
            this.btnSuaPhongBan.Text = "Sửa";
            // 
            // btnXoaPhongBan
            // 
            this.btnXoaPhongBan.BorderRadius = 12;
            this.btnXoaPhongBan.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnXoaPhongBan.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnXoaPhongBan.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnXoaPhongBan.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnXoaPhongBan.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(67)))), ((int)(((byte)(54)))));
            this.btnXoaPhongBan.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnXoaPhongBan.ForeColor = System.Drawing.Color.White;
            this.btnXoaPhongBan.Location = new System.Drawing.Point(200, 70);
            this.btnXoaPhongBan.Name = "btnXoaPhongBan";
            this.btnXoaPhongBan.Size = new System.Drawing.Size(80, 30);
            this.btnXoaPhongBan.TabIndex = 8;
            this.btnXoaPhongBan.Text = "Xóa";
            // 
            // btnLuu
            // 
            this.btnLuu.BorderRadius = 8;
            this.btnLuu.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnLuu.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnLuu.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnLuu.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnLuu.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(150)))), ((int)(((byte)(243)))));
            this.btnLuu.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLuu.ForeColor = System.Drawing.Color.White;
            this.btnLuu.Location = new System.Drawing.Point(290, 70);
            this.btnLuu.Name = "btnLuu";
            this.btnLuu.Size = new System.Drawing.Size(80, 30);
            this.btnLuu.TabIndex = 9;
            this.btnLuu.Text = "Lưu";
            // 
            // btnHuy
            // 
            this.btnHuy.BorderRadius = 8;
            this.btnHuy.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnHuy.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnHuy.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnHuy.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnHuy.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(193)))), ((int)(((byte)(7)))));
            this.btnHuy.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnHuy.ForeColor = System.Drawing.Color.White;
            this.btnHuy.Location = new System.Drawing.Point(380, 70);
            this.btnHuy.Name = "btnHuy";
            this.btnHuy.Size = new System.Drawing.Size(80, 30);
            this.btnHuy.TabIndex = 10;
            this.btnHuy.Text = "Hủy";
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
            this.btnLamMoi.Location = new System.Drawing.Point(470, 70);
            this.btnLamMoi.Name = "btnLamMoi";
            this.btnLamMoi.Size = new System.Drawing.Size(80, 30);
            this.btnLamMoi.TabIndex = 11;
            this.btnLamMoi.Text = "Làm mới";
            // 
            // pnlChucVu
            // 
            this.pnlChucVu.BackColor = System.Drawing.Color.White;
            this.pnlChucVu.Controls.Add(this.lblMaChucVu);
            this.pnlChucVu.Controls.Add(this.txtMaChucVu);
            this.pnlChucVu.Controls.Add(this.lblTenChucVu);
            this.pnlChucVu.Controls.Add(this.txtTenChucVu);
            this.pnlChucVu.Controls.Add(this.lblMoTaChucVu);
            this.pnlChucVu.Controls.Add(this.txtMoTaChucVu);
            this.pnlChucVu.Controls.Add(this.btnThemChucVu);
            this.pnlChucVu.Controls.Add(this.btnSuaChucVu);
            this.pnlChucVu.Controls.Add(this.btnXoaChucVu);
            this.pnlChucVu.Controls.Add(this.dgvChucVu);
            this.pnlChucVu.Location = new System.Drawing.Point(20, 20);
            this.pnlChucVu.Name = "pnlChucVu";
            this.pnlChucVu.Size = new System.Drawing.Size(1112, 554);
            this.pnlChucVu.TabIndex = 0;
            // 
            // dgvChucVu
            // 
            this.dgvChucVu.AllowUserToAddRows = false;
            this.dgvChucVu.AllowUserToDeleteRows = false;
            this.dgvChucVu.BackgroundColor = System.Drawing.Color.White;
            this.dgvChucVu.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvChucVu.ColumnHeadersHeight = 40;
            this.dgvChucVu.EnableHeadersVisualStyles = false;
            this.dgvChucVu.Location = new System.Drawing.Point(20, 120);
            this.dgvChucVu.Name = "dgvChucVu";
            this.dgvChucVu.ReadOnly = true;
            this.dgvChucVu.RowHeadersVisible = false;
            this.dgvChucVu.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvChucVu.Size = new System.Drawing.Size(1070, 410);
            this.dgvChucVu.TabIndex = 12;
            // 
            // lblMaChucVu
            // 
            this.lblMaChucVu.AutoSize = true;
            this.lblMaChucVu.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMaChucVu.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblMaChucVu.Location = new System.Drawing.Point(20, 20);
            this.lblMaChucVu.Name = "lblMaChucVu";
            this.lblMaChucVu.Size = new System.Drawing.Size(65, 19);
            this.lblMaChucVu.TabIndex = 0;
            this.lblMaChucVu.Text = "Mã chức vụ:";
            // 
            // txtMaChucVu
            // 
            this.txtMaChucVu.BorderRadius = 8;
            this.txtMaChucVu.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtMaChucVu.DefaultText = "";
            this.txtMaChucVu.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtMaChucVu.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtMaChucVu.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtMaChucVu.DisabledState.Parent = this.txtMaChucVu;
            this.txtMaChucVu.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtMaChucVu.FillColor = System.Drawing.Color.White;
            this.txtMaChucVu.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(124)))), ((int)(((byte)(77)))), ((int)(((byte)(255)))));
            this.txtMaChucVu.FocusedState.Parent = this.txtMaChucVu;
            this.txtMaChucVu.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMaChucVu.ForeColor = System.Drawing.Color.Black;
            this.txtMaChucVu.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(124)))), ((int)(((byte)(77)))), ((int)(((byte)(255)))));
            this.txtMaChucVu.HoverState.Parent = this.txtMaChucVu;
            this.txtMaChucVu.Location = new System.Drawing.Point(95, 15);
            this.txtMaChucVu.Name = "txtMaChucVu";
            this.txtMaChucVu.PasswordChar = '\0';
            this.txtMaChucVu.PlaceholderText = "";
            this.txtMaChucVu.SelectedText = "";
            this.txtMaChucVu.Size = new System.Drawing.Size(100, 30);
            this.txtMaChucVu.TabIndex = 1;
            // 
            // lblTenChucVu
            // 
            this.lblTenChucVu.AutoSize = true;
            this.lblTenChucVu.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTenChucVu.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblTenChucVu.Location = new System.Drawing.Point(215, 20);
            this.lblTenChucVu.Name = "lblTenChucVu";
            this.lblTenChucVu.Size = new System.Drawing.Size(75, 19);
            this.lblTenChucVu.TabIndex = 2;
            this.lblTenChucVu.Text = "Tên chức vụ:";
            // 
            // txtTenChucVu
            // 
            this.txtTenChucVu.BorderRadius = 8;
            this.txtTenChucVu.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtTenChucVu.DefaultText = "";
            this.txtTenChucVu.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtTenChucVu.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtTenChucVu.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtTenChucVu.DisabledState.Parent = this.txtTenChucVu;
            this.txtTenChucVu.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtTenChucVu.FillColor = System.Drawing.Color.White;
            this.txtTenChucVu.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(124)))), ((int)(((byte)(77)))), ((int)(((byte)(255)))));
            this.txtTenChucVu.FocusedState.Parent = this.txtTenChucVu;
            this.txtTenChucVu.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTenChucVu.ForeColor = System.Drawing.Color.Black;
            this.txtTenChucVu.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(124)))), ((int)(((byte)(77)))), ((int)(((byte)(255)))));
            this.txtTenChucVu.HoverState.Parent = this.txtTenChucVu;
            this.txtTenChucVu.Location = new System.Drawing.Point(300, 15);
            this.txtTenChucVu.Name = "txtTenChucVu";
            this.txtTenChucVu.PasswordChar = '\0';
            this.txtTenChucVu.PlaceholderText = "";
            this.txtTenChucVu.SelectedText = "";
            this.txtTenChucVu.Size = new System.Drawing.Size(200, 30);
            this.txtTenChucVu.TabIndex = 3;
            // 
            // lblMoTaChucVu
            // 
            this.lblMoTaChucVu.AutoSize = true;
            this.lblMoTaChucVu.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMoTaChucVu.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblMoTaChucVu.Location = new System.Drawing.Point(520, 20);
            this.lblMoTaChucVu.Name = "lblMoTaChucVu";
            this.lblMoTaChucVu.Size = new System.Drawing.Size(50, 19);
            this.lblMoTaChucVu.TabIndex = 4;
            this.lblMoTaChucVu.Text = "Mô tả:";
            // 
            // txtMoTaChucVu
            // 
            this.txtMoTaChucVu.BorderRadius = 8;
            this.txtMoTaChucVu.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtMoTaChucVu.DefaultText = "";
            this.txtMoTaChucVu.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtMoTaChucVu.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtMoTaChucVu.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtMoTaChucVu.DisabledState.Parent = this.txtMoTaChucVu;
            this.txtMoTaChucVu.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtMoTaChucVu.FillColor = System.Drawing.Color.White;
            this.txtMoTaChucVu.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(124)))), ((int)(((byte)(77)))), ((int)(((byte)(255)))));
            this.txtMoTaChucVu.FocusedState.Parent = this.txtMoTaChucVu;
            this.txtMoTaChucVu.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMoTaChucVu.ForeColor = System.Drawing.Color.Black;
            this.txtMoTaChucVu.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(124)))), ((int)(((byte)(77)))), ((int)(((byte)(255)))));
            this.txtMoTaChucVu.HoverState.Parent = this.txtMoTaChucVu;
            this.txtMoTaChucVu.Location = new System.Drawing.Point(580, 15);
            this.txtMoTaChucVu.Name = "txtMoTaChucVu";
            this.txtMoTaChucVu.PasswordChar = '\0';
            this.txtMoTaChucVu.PlaceholderText = "";
            this.txtMoTaChucVu.SelectedText = "";
            this.txtMoTaChucVu.Size = new System.Drawing.Size(300, 30);
            this.txtMoTaChucVu.TabIndex = 5;
            // 
            // btnThemChucVu
            // 
            this.btnThemChucVu.BorderRadius = 12;
            this.btnThemChucVu.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnThemChucVu.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnThemChucVu.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnThemChucVu.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnThemChucVu.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(175)))), ((int)(((byte)(80)))));
            this.btnThemChucVu.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnThemChucVu.ForeColor = System.Drawing.Color.White;
            this.btnThemChucVu.Location = new System.Drawing.Point(20, 70);
            this.btnThemChucVu.Name = "btnThemChucVu";
            this.btnThemChucVu.Size = new System.Drawing.Size(80, 30);
            this.btnThemChucVu.TabIndex = 6;
            this.btnThemChucVu.Text = "Thêm";
            // 
            // btnSuaChucVu
            // 
            this.btnSuaChucVu.BorderRadius = 12;
            this.btnSuaChucVu.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnSuaChucVu.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnSuaChucVu.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnSuaChucVu.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnSuaChucVu.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(193)))), ((int)(((byte)(7)))));
            this.btnSuaChucVu.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSuaChucVu.ForeColor = System.Drawing.Color.White;
            this.btnSuaChucVu.Location = new System.Drawing.Point(110, 70);
            this.btnSuaChucVu.Name = "btnSuaChucVu";
            this.btnSuaChucVu.Size = new System.Drawing.Size(80, 30);
            this.btnSuaChucVu.TabIndex = 7;
            this.btnSuaChucVu.Text = "Sửa";
            // 
            // btnXoaChucVu
            // 
            this.btnXoaChucVu.BorderRadius = 12;
            this.btnXoaChucVu.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnXoaChucVu.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnXoaChucVu.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnXoaChucVu.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnXoaChucVu.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(67)))), ((int)(((byte)(54)))));
            this.btnXoaChucVu.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnXoaChucVu.ForeColor = System.Drawing.Color.White;
            this.btnXoaChucVu.Location = new System.Drawing.Point(200, 70);
            this.btnXoaChucVu.Name = "btnXoaChucVu";
            this.btnXoaChucVu.Size = new System.Drawing.Size(80, 30);
            this.btnXoaChucVu.TabIndex = 8;
            this.btnXoaChucVu.Text = "Xóa";
            // 
            // frmPhongBan_ChucVu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1200, 700);
            this.Controls.Add(this.pnlMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmPhongBan_ChucVu";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Quản lý Phòng ban và Chức vụ";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.pnlMain.ResumeLayout(false);
            this.pnlMain.PerformLayout();
            this.tabControl.ResumeLayout(false);
            this.tabPhongBan.ResumeLayout(false);
            this.tabChucVu.ResumeLayout(false);
            this.pnlPhongBan.ResumeLayout(false);
            this.pnlPhongBan.PerformLayout();
            this.pnlChucVu.ResumeLayout(false);
            this.pnlChucVu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.Panel pnlTitle;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPhongBan;
        private System.Windows.Forms.TabPage tabChucVu;
        private System.Windows.Forms.Panel pnlPhongBan;
        private System.Windows.Forms.Label lblMaPhongBan;
        private Guna.UI2.WinForms.Guna2TextBox txtMaPhongBan;
        private System.Windows.Forms.Label lblTenPhongBan;
        private Guna.UI2.WinForms.Guna2TextBox txtTenPhongBan;
        private System.Windows.Forms.Label lblMoTaPhongBan;
        private Guna.UI2.WinForms.Guna2TextBox txtMoTaPhongBan;
        private Guna.UI2.WinForms.Guna2Button btnThemPhongBan;
        private Guna.UI2.WinForms.Guna2Button btnSuaPhongBan;
        private Guna.UI2.WinForms.Guna2Button btnXoaPhongBan;
        private Guna.UI2.WinForms.Guna2Button btnLuu;
        private Guna.UI2.WinForms.Guna2Button btnHuy;
        private Guna.UI2.WinForms.Guna2Button btnLamMoi;
        private System.Windows.Forms.Panel pnlChucVu;
        private System.Windows.Forms.Label lblMaChucVu;
        private Guna.UI2.WinForms.Guna2TextBox txtMaChucVu;
        private System.Windows.Forms.Label lblTenChucVu;
        private Guna.UI2.WinForms.Guna2TextBox txtTenChucVu;
        private System.Windows.Forms.Label lblMoTaChucVu;
        private Guna.UI2.WinForms.Guna2TextBox txtMoTaChucVu;
        private Guna.UI2.WinForms.Guna2Button btnThemChucVu;
        private Guna.UI2.WinForms.Guna2Button btnSuaChucVu;
        private Guna.UI2.WinForms.Guna2Button btnXoaChucVu;
        private Guna.UI2.WinForms.Guna2DataGridView dgvPhongBan;
        private Guna.UI2.WinForms.Guna2DataGridView dgvChucVu;
    }
}
