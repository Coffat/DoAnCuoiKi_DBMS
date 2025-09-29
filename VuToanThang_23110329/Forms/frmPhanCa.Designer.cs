namespace VuToanThang_23110329.Forms
{
    partial class frmPhanCa
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            this.lblTitle = new System.Windows.Forms.Label();
            this.pnlFilter = new System.Windows.Forms.Panel();
            this.lblNhanVien = new System.Windows.Forms.Label();
            this.cboNhanVien = new Guna.UI2.WinForms.Guna2ComboBox();
            this.lblTuNgay = new System.Windows.Forms.Label();
            this.dtpTuNgay = new Guna.UI2.WinForms.Guna2DateTimePicker();
            this.lblDenNgay = new System.Windows.Forms.Label();
            this.dtpDenNgay = new Guna.UI2.WinForms.Guna2DateTimePicker();
            this.lblTimKiem = new System.Windows.Forms.Label();
            this.txtTimKiem = new Guna.UI2.WinForms.Guna2TextBox();
            this.pnlButtons = new System.Windows.Forms.Panel();
            this.btnThem = new Guna.UI2.WinForms.Guna2Button();
            this.btnSua = new Guna.UI2.WinForms.Guna2Button();
            this.btnXoa = new Guna.UI2.WinForms.Guna2Button();
            this.btnLuu = new Guna.UI2.WinForms.Guna2Button();
            this.btnHuy = new Guna.UI2.WinForms.Guna2Button();
            this.btnLamMoi = new Guna.UI2.WinForms.Guna2Button();
            this.btnTaoLichTuan = new Guna.UI2.WinForms.Guna2Button();
            this.pnlMain = new System.Windows.Forms.Panel();
            this.pnlTitle = new System.Windows.Forms.Panel();
            this.pnlContent = new System.Windows.Forms.Panel();
            this.pnlCalendar = new System.Windows.Forms.Panel();
            this.dgvTuan = new Guna.UI2.WinForms.Guna2DataGridView();
            this.pnlCalHeader = new System.Windows.Forms.Panel();
            this.lblWeek = new System.Windows.Forms.Label();
            this.btnPrevWeek = new Guna.UI2.WinForms.Guna2Button();
            this.btnNextWeek = new Guna.UI2.WinForms.Guna2Button();
            this.pnlInfo = new System.Windows.Forms.Panel();
            this.lblThongTinCa = new System.Windows.Forms.Label();
            this.lblChiTietLich = new System.Windows.Forms.Label();
            this.pnlFilter.SuspendLayout();
            this.pnlButtons.SuspendLayout();
            this.pnlMain.SuspendLayout();
            this.pnlContent.SuspendLayout();
            this.pnlCalendar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTuan)).BeginInit();
            this.pnlCalHeader.SuspendLayout();
            this.pnlInfo.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.lblTitle.Location = new System.Drawing.Point(13, 10);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(226, 30);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Quản lý Lịch phân ca";
            // 
            // pnlFilter
            // 
            this.pnlFilter.BackColor = System.Drawing.Color.White;
            this.pnlFilter.Controls.Add(this.lblNhanVien);
            this.pnlFilter.Controls.Add(this.cboNhanVien);
            this.pnlFilter.Controls.Add(this.lblTuNgay);
            this.pnlFilter.Controls.Add(this.dtpTuNgay);
            this.pnlFilter.Controls.Add(this.lblDenNgay);
            this.pnlFilter.Controls.Add(this.dtpDenNgay);
            this.pnlFilter.Controls.Add(this.lblTimKiem);
            this.pnlFilter.Controls.Add(this.txtTimKiem);
            this.pnlFilter.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlFilter.Location = new System.Drawing.Point(20, 70);
            this.pnlFilter.Name = "pnlFilter";
            this.pnlFilter.Size = new System.Drawing.Size(1160, 60);
            this.pnlFilter.TabIndex = 1;
            // 
            // lblNhanVien
            // 
            this.lblNhanVien.AutoSize = true;
            this.lblNhanVien.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNhanVien.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblNhanVien.Location = new System.Drawing.Point(720, 20);
            this.lblNhanVien.Name = "lblNhanVien";
            this.lblNhanVien.Size = new System.Drawing.Size(74, 19);
            this.lblNhanVien.TabIndex = 6;
            this.lblNhanVien.Text = "Nhân viên:";
            // 
            // cboNhanVien
            // 
            this.cboNhanVien.BackColor = System.Drawing.Color.Transparent;
            this.cboNhanVien.BorderRadius = 8;
            this.cboNhanVien.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboNhanVien.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboNhanVien.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(124)))), ((int)(((byte)(77)))), ((int)(((byte)(255)))));
            this.cboNhanVien.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(124)))), ((int)(((byte)(77)))), ((int)(((byte)(255)))));
            this.cboNhanVien.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cboNhanVien.ForeColor = System.Drawing.Color.Black;
            this.cboNhanVien.ItemHeight = 24;
            this.cboNhanVien.Location = new System.Drawing.Point(795, 15);
            this.cboNhanVien.Name = "cboNhanVien";
            this.cboNhanVien.Size = new System.Drawing.Size(220, 30);
            this.cboNhanVien.TabIndex = 7;
            this.cboNhanVien.SelectedIndexChanged += new System.EventHandler(this.cboNhanVien_SelectedIndexChanged);
            // 
            // lblTuNgay
            // 
            this.lblTuNgay.AutoSize = true;
            this.lblTuNgay.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTuNgay.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblTuNgay.Location = new System.Drawing.Point(20, 20);
            this.lblTuNgay.Name = "lblTuNgay";
            this.lblTuNgay.Size = new System.Drawing.Size(61, 19);
            this.lblTuNgay.TabIndex = 0;
            this.lblTuNgay.Text = "Từ ngày:";
            // 
            // dtpTuNgay
            // 
            this.dtpTuNgay.BorderRadius = 8;
            this.dtpTuNgay.Checked = true;
            this.dtpTuNgay.FillColor = System.Drawing.Color.White;
            this.dtpTuNgay.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpTuNgay.ForeColor = System.Drawing.Color.Black;
            this.dtpTuNgay.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpTuNgay.Location = new System.Drawing.Point(80, 15);
            this.dtpTuNgay.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.dtpTuNgay.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtpTuNgay.Name = "dtpTuNgay";
            this.dtpTuNgay.Size = new System.Drawing.Size(120, 30);
            this.dtpTuNgay.TabIndex = 1;
            this.dtpTuNgay.Value = new System.DateTime(2025, 9, 30, 1, 33, 5, 313);
            // 
            // lblDenNgay
            // 
            this.lblDenNgay.AutoSize = true;
            this.lblDenNgay.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDenNgay.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblDenNgay.Location = new System.Drawing.Point(220, 20);
            this.lblDenNgay.Name = "lblDenNgay";
            this.lblDenNgay.Size = new System.Drawing.Size(71, 19);
            this.lblDenNgay.TabIndex = 2;
            this.lblDenNgay.Text = "Đến ngày:";
            // 
            // dtpDenNgay
            // 
            this.dtpDenNgay.BorderRadius = 8;
            this.dtpDenNgay.Checked = true;
            this.dtpDenNgay.FillColor = System.Drawing.Color.White;
            this.dtpDenNgay.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpDenNgay.ForeColor = System.Drawing.Color.Black;
            this.dtpDenNgay.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDenNgay.Location = new System.Drawing.Point(285, 15);
            this.dtpDenNgay.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.dtpDenNgay.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtpDenNgay.Name = "dtpDenNgay";
            this.dtpDenNgay.Size = new System.Drawing.Size(120, 30);
            this.dtpDenNgay.TabIndex = 3;
            this.dtpDenNgay.Value = new System.DateTime(2025, 9, 30, 1, 33, 5, 330);
            // 
            // lblTimKiem
            // 
            this.lblTimKiem.AutoSize = true;
            this.lblTimKiem.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTimKiem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblTimKiem.Location = new System.Drawing.Point(425, 20);
            this.lblTimKiem.Name = "lblTimKiem";
            this.lblTimKiem.Size = new System.Drawing.Size(67, 19);
            this.lblTimKiem.TabIndex = 4;
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
            this.txtTimKiem.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtTimKiem.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(124)))), ((int)(((byte)(77)))), ((int)(((byte)(255)))));
            this.txtTimKiem.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTimKiem.ForeColor = System.Drawing.Color.Black;
            this.txtTimKiem.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(124)))), ((int)(((byte)(77)))), ((int)(((byte)(255)))));
            this.txtTimKiem.Location = new System.Drawing.Point(495, 15);
            this.txtTimKiem.Name = "txtTimKiem";
            this.txtTimKiem.PlaceholderText = "Nhập ID hoặc tên nhân viên...";
            this.txtTimKiem.SelectedText = "";
            this.txtTimKiem.Size = new System.Drawing.Size(200, 30);
            this.txtTimKiem.TabIndex = 5;
            // 
            // pnlButtons
            // 
            this.pnlButtons.BackColor = System.Drawing.Color.White;
            this.pnlButtons.Controls.Add(this.lblTitle);
            this.pnlButtons.Controls.Add(this.btnThem);
            this.pnlButtons.Controls.Add(this.btnSua);
            this.pnlButtons.Controls.Add(this.btnXoa);
            this.pnlButtons.Controls.Add(this.btnLuu);
            this.pnlButtons.Controls.Add(this.btnHuy);
            this.pnlButtons.Controls.Add(this.btnLamMoi);
            this.pnlButtons.Controls.Add(this.btnTaoLichTuan);
            this.pnlButtons.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlButtons.Location = new System.Drawing.Point(20, 20);
            this.pnlButtons.Name = "pnlButtons";
            this.pnlButtons.Size = new System.Drawing.Size(1160, 50);
            this.pnlButtons.TabIndex = 2;
            this.pnlButtons.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlButtons_Paint);
            // 
            // btnThem
            // 
            this.btnThem.BorderRadius = 12;
            this.btnThem.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnThem.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnThem.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnThem.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnThem.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(167)))), ((int)(((byte)(69)))));
            this.btnThem.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnThem.ForeColor = System.Drawing.Color.White;
            this.btnThem.Location = new System.Drawing.Point(666, 11);
            this.btnThem.Name = "btnThem";
            this.btnThem.Size = new System.Drawing.Size(80, 30);
            this.btnThem.TabIndex = 0;
            this.btnThem.Text = "Thêm";
            // 
            // btnSua
            // 
            this.btnSua.BorderRadius = 12;
            this.btnSua.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnSua.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnSua.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnSua.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnSua.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(193)))), ((int)(((byte)(7)))));
            this.btnSua.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnSua.ForeColor = System.Drawing.Color.White;
            this.btnSua.Location = new System.Drawing.Point(752, 11);
            this.btnSua.Name = "btnSua";
            this.btnSua.Size = new System.Drawing.Size(80, 30);
            this.btnSua.TabIndex = 1;
            this.btnSua.Text = "Sửa";
            // 
            // btnXoa
            // 
            this.btnXoa.BorderRadius = 12;
            this.btnXoa.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnXoa.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnXoa.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnXoa.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnXoa.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(53)))), ((int)(((byte)(69)))));
            this.btnXoa.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnXoa.ForeColor = System.Drawing.Color.White;
            this.btnXoa.Location = new System.Drawing.Point(838, 10);
            this.btnXoa.Name = "btnXoa";
            this.btnXoa.Size = new System.Drawing.Size(80, 30);
            this.btnXoa.TabIndex = 2;
            this.btnXoa.Text = "Xóa";
            // 
            // btnLuu
            // 
            this.btnLuu.BorderRadius = 12;
            this.btnLuu.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnLuu.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnLuu.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnLuu.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnLuu.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnLuu.ForeColor = System.Drawing.Color.White;
            this.btnLuu.Location = new System.Drawing.Point(290, 10);
            this.btnLuu.Name = "btnLuu";
            this.btnLuu.Size = new System.Drawing.Size(80, 30);
            this.btnLuu.TabIndex = 3;
            this.btnLuu.Text = "Lưu";
            // 
            // btnHuy
            // 
            this.btnHuy.BorderRadius = 12;
            this.btnHuy.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnHuy.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnHuy.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnHuy.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnHuy.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(117)))), ((int)(((byte)(125)))));
            this.btnHuy.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnHuy.ForeColor = System.Drawing.Color.White;
            this.btnHuy.Location = new System.Drawing.Point(380, 10);
            this.btnHuy.Name = "btnHuy";
            this.btnHuy.Size = new System.Drawing.Size(80, 30);
            this.btnHuy.TabIndex = 4;
            this.btnHuy.Text = "Hủy";
            // 
            // btnLamMoi
            // 
            this.btnLamMoi.BorderRadius = 12;
            this.btnLamMoi.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnLamMoi.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnLamMoi.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnLamMoi.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnLamMoi.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(111)))), ((int)(((byte)(66)))), ((int)(((byte)(193)))));
            this.btnLamMoi.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnLamMoi.ForeColor = System.Drawing.Color.White;
            this.btnLamMoi.Location = new System.Drawing.Point(470, 10);
            this.btnLamMoi.Name = "btnLamMoi";
            this.btnLamMoi.Size = new System.Drawing.Size(80, 30);
            this.btnLamMoi.TabIndex = 5;
            this.btnLamMoi.Text = "Làm mới";
            // 
            // btnTaoLichTuan
            // 
            this.btnTaoLichTuan.BorderRadius = 12;
            this.btnTaoLichTuan.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnTaoLichTuan.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnTaoLichTuan.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnTaoLichTuan.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnTaoLichTuan.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnTaoLichTuan.ForeColor = System.Drawing.Color.White;
            this.btnTaoLichTuan.Location = new System.Drawing.Point(560, 10);
            this.btnTaoLichTuan.Name = "btnTaoLichTuan";
            this.btnTaoLichTuan.Size = new System.Drawing.Size(100, 30);
            this.btnTaoLichTuan.TabIndex = 6;
            this.btnTaoLichTuan.Text = "Tạo lịch tuần";
            // 
            // pnlMain
            // 
            this.pnlMain.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.pnlMain.Controls.Add(this.pnlTitle);
            this.pnlMain.Controls.Add(this.pnlFilter);
            this.pnlMain.Controls.Add(this.pnlButtons);
            this.pnlMain.Controls.Add(this.pnlContent);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 0);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Padding = new System.Windows.Forms.Padding(20);
            this.pnlMain.Size = new System.Drawing.Size(1200, 700);
            this.pnlMain.TabIndex = 0;
            // 
            // pnlTitle
            // 
            this.pnlTitle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.pnlTitle.Location = new System.Drawing.Point(0, 0);
            this.pnlTitle.Name = "pnlTitle";
            this.pnlTitle.Size = new System.Drawing.Size(1200, 10);
            this.pnlTitle.TabIndex = 3;
            // 
            // pnlContent
            // 
            this.pnlContent.BackColor = System.Drawing.Color.White;
            this.pnlContent.Controls.Add(this.pnlCalendar);
            this.pnlContent.Controls.Add(this.pnlInfo);
            this.pnlContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlContent.Location = new System.Drawing.Point(20, 20);
            this.pnlContent.Name = "pnlContent";
            this.pnlContent.Size = new System.Drawing.Size(1160, 660);
            this.pnlContent.TabIndex = 4;
            // 
            // pnlCalendar
            // 
            this.pnlCalendar.BackColor = System.Drawing.Color.White;
            this.pnlCalendar.Controls.Add(this.dgvTuan);
            this.pnlCalendar.Controls.Add(this.pnlCalHeader);
            this.pnlCalendar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlCalendar.Location = new System.Drawing.Point(0, 0);
            this.pnlCalendar.Name = "pnlCalendar";
            this.pnlCalendar.Padding = new System.Windows.Forms.Padding(10, 0, 10, 10);
            this.pnlCalendar.Size = new System.Drawing.Size(800, 660);
            this.pnlCalendar.TabIndex = 2;
            // 
            // dgvTuan
            // 
            this.dgvTuan.AllowUserToAddRows = false;
            this.dgvTuan.AllowUserToDeleteRows = false;
            this.dgvTuan.AllowUserToResizeRows = false;
            dataGridViewCellStyle10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(250)))));
            this.dgvTuan.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle10;
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle11.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle11.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle11.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle11.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle11.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvTuan.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle11;
            this.dgvTuan.ColumnHeadersHeight = 40;
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle12.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle12.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle12.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle12.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle12.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle12.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvTuan.DefaultCellStyle = dataGridViewCellStyle12;
            this.dgvTuan.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvTuan.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.dgvTuan.Location = new System.Drawing.Point(10, 50);
            this.dgvTuan.MultiSelect = false;
            this.dgvTuan.Name = "dgvTuan";
            this.dgvTuan.RowHeadersVisible = false;
            this.dgvTuan.RowTemplate.Height = 40;
            this.dgvTuan.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgvTuan.Size = new System.Drawing.Size(780, 600);
            this.dgvTuan.TabIndex = 3;
            this.dgvTuan.ThemeStyle.AlternatingRowsStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(250)))));
            this.dgvTuan.ThemeStyle.AlternatingRowsStyle.Font = null;
            this.dgvTuan.ThemeStyle.AlternatingRowsStyle.ForeColor = System.Drawing.Color.Empty;
            this.dgvTuan.ThemeStyle.AlternatingRowsStyle.SelectionBackColor = System.Drawing.Color.Empty;
            this.dgvTuan.ThemeStyle.AlternatingRowsStyle.SelectionForeColor = System.Drawing.Color.Empty;
            this.dgvTuan.ThemeStyle.BackColor = System.Drawing.Color.White;
            this.dgvTuan.ThemeStyle.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.dgvTuan.ThemeStyle.HeaderStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.dgvTuan.ThemeStyle.HeaderStyle.BorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgvTuan.ThemeStyle.HeaderStyle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.dgvTuan.ThemeStyle.HeaderStyle.ForeColor = System.Drawing.Color.White;
            this.dgvTuan.ThemeStyle.HeaderStyle.HeaightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvTuan.ThemeStyle.HeaderStyle.Height = 40;
            this.dgvTuan.ThemeStyle.ReadOnly = false;
            this.dgvTuan.ThemeStyle.RowsStyle.BackColor = System.Drawing.Color.White;
            this.dgvTuan.ThemeStyle.RowsStyle.BorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvTuan.ThemeStyle.RowsStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvTuan.ThemeStyle.RowsStyle.ForeColor = System.Drawing.Color.Black;
            this.dgvTuan.ThemeStyle.RowsStyle.Height = 40;
            this.dgvTuan.ThemeStyle.RowsStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.dgvTuan.ThemeStyle.RowsStyle.SelectionForeColor = System.Drawing.Color.White;
            this.dgvTuan.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvTuan_CellClick);
            this.dgvTuan.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvTuan_CellDoubleClick);
            this.dgvTuan.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvTuan_CellFormatting);
            // 
            // pnlCalHeader
            // 
            this.pnlCalHeader.BackColor = System.Drawing.Color.White;
            this.pnlCalHeader.Controls.Add(this.lblWeek);
            this.pnlCalHeader.Controls.Add(this.btnPrevWeek);
            this.pnlCalHeader.Controls.Add(this.btnNextWeek);
            this.pnlCalHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlCalHeader.Location = new System.Drawing.Point(10, 0);
            this.pnlCalHeader.Name = "pnlCalHeader";
            this.pnlCalHeader.Size = new System.Drawing.Size(780, 50);
            this.pnlCalHeader.TabIndex = 5;
            // 
            // lblWeek
            // 
            this.lblWeek.AutoSize = true;
            this.lblWeek.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWeek.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.lblWeek.Location = new System.Drawing.Point(20, 15);
            this.lblWeek.Name = "lblWeek";
            this.lblWeek.Size = new System.Drawing.Size(155, 21);
            this.lblWeek.TabIndex = 0;
            this.lblWeek.Text = "Tuần: 01/01 - 07/01";
            // 
            // btnPrevWeek
            // 
            this.btnPrevWeek.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(150)))), ((int)(((byte)(243)))));
            this.btnPrevWeek.BorderRadius = 8;
            this.btnPrevWeek.BorderThickness = 1;
            this.btnPrevWeek.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnPrevWeek.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnPrevWeek.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnPrevWeek.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnPrevWeek.FillColor = System.Drawing.Color.White;
            this.btnPrevWeek.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnPrevWeek.ForeColor = System.Drawing.Color.Black;
            this.btnPrevWeek.Location = new System.Drawing.Point(600, 10);
            this.btnPrevWeek.Name = "btnPrevWeek";
            this.btnPrevWeek.Size = new System.Drawing.Size(90, 30);
            this.btnPrevWeek.TabIndex = 1;
            this.btnPrevWeek.Text = "← Tuần trước";
            this.btnPrevWeek.Click += new System.EventHandler(this.btnPrevWeek_Click);
            // 
            // btnNextWeek
            // 
            this.btnNextWeek.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(150)))), ((int)(((byte)(243)))));
            this.btnNextWeek.BorderRadius = 8;
            this.btnNextWeek.BorderThickness = 1;
            this.btnNextWeek.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnNextWeek.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnNextWeek.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnNextWeek.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnNextWeek.FillColor = System.Drawing.Color.White;
            this.btnNextWeek.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnNextWeek.ForeColor = System.Drawing.Color.Black;
            this.btnNextWeek.Location = new System.Drawing.Point(696, 10);
            this.btnNextWeek.Name = "btnNextWeek";
            this.btnNextWeek.Size = new System.Drawing.Size(90, 30);
            this.btnNextWeek.TabIndex = 2;
            this.btnNextWeek.Text = "Tuần sau →";
            this.btnNextWeek.Click += new System.EventHandler(this.btnNextWeek_Click);
            // 
            // pnlInfo
            // 
            this.pnlInfo.BackColor = System.Drawing.Color.White;
            this.pnlInfo.Controls.Add(this.lblThongTinCa);
            this.pnlInfo.Controls.Add(this.lblChiTietLich);
            this.pnlInfo.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlInfo.Location = new System.Drawing.Point(800, 0);
            this.pnlInfo.Name = "pnlInfo";
            this.pnlInfo.Size = new System.Drawing.Size(360, 660);
            this.pnlInfo.TabIndex = 1;
            // 
            // lblThongTinCa
            // 
            this.lblThongTinCa.AutoSize = true;
            this.lblThongTinCa.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblThongTinCa.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.lblThongTinCa.Location = new System.Drawing.Point(20, 20);
            this.lblThongTinCa.Name = "lblThongTinCa";
            this.lblThongTinCa.Size = new System.Drawing.Size(109, 21);
            this.lblThongTinCa.TabIndex = 0;
            this.lblThongTinCa.Text = "Thông tin ca:";
            // 
            // lblChiTietLich
            // 
            this.lblChiTietLich.AutoSize = true;
            this.lblChiTietLich.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblChiTietLich.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblChiTietLich.Location = new System.Drawing.Point(20, 50);
            this.lblChiTietLich.Name = "lblChiTietLich";
            this.lblChiTietLich.Size = new System.Drawing.Size(80, 19);
            this.lblChiTietLich.TabIndex = 1;
            this.lblChiTietLich.Text = "Chi tiết lịch:";
            // 
            // frmPhanCa
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.ClientSize = new System.Drawing.Size(1200, 700);
            this.Controls.Add(this.pnlMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmPhanCa";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Quản lý Lịch phân ca";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmPhanCa_Load);
            this.pnlFilter.ResumeLayout(false);
            this.pnlFilter.PerformLayout();
            this.pnlButtons.ResumeLayout(false);
            this.pnlButtons.PerformLayout();
            this.pnlMain.ResumeLayout(false);
            this.pnlContent.ResumeLayout(false);
            this.pnlCalendar.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvTuan)).EndInit();
            this.pnlCalHeader.ResumeLayout(false);
            this.pnlCalHeader.PerformLayout();
            this.pnlInfo.ResumeLayout(false);
            this.pnlInfo.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Panel pnlFilter;
        private System.Windows.Forms.Label lblTuNgay;
        private Guna.UI2.WinForms.Guna2DateTimePicker dtpTuNgay;
        private System.Windows.Forms.Label lblDenNgay;
        private Guna.UI2.WinForms.Guna2DateTimePicker dtpDenNgay;
        private System.Windows.Forms.Label lblTimKiem;
        private Guna.UI2.WinForms.Guna2TextBox txtTimKiem;
        private System.Windows.Forms.Panel pnlButtons;
        private Guna.UI2.WinForms.Guna2Button btnThem;
        private Guna.UI2.WinForms.Guna2Button btnSua;
        private Guna.UI2.WinForms.Guna2Button btnXoa;
        private Guna.UI2.WinForms.Guna2Button btnLuu;
        private Guna.UI2.WinForms.Guna2Button btnHuy;
        private Guna.UI2.WinForms.Guna2Button btnLamMoi;
        private Guna.UI2.WinForms.Guna2Button btnTaoLichTuan;
        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.Panel pnlTitle;
        private System.Windows.Forms.Panel pnlContent;
        private System.Windows.Forms.Panel pnlCalendar;
        private System.Windows.Forms.Panel pnlInfo;
        private System.Windows.Forms.Label lblThongTinCa;
        private System.Windows.Forms.Label lblChiTietLich;
        private System.Windows.Forms.Label lblNhanVien;
        private Guna.UI2.WinForms.Guna2ComboBox cboNhanVien;
        private System.Windows.Forms.Label lblWeek;
        private Guna.UI2.WinForms.Guna2Button btnPrevWeek;
        private Guna.UI2.WinForms.Guna2Button btnNextWeek;
        private Guna.UI2.WinForms.Guna2DataGridView dgvTuan;
        private System.Windows.Forms.Panel pnlCalHeader;
    }
}
