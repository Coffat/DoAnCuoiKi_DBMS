namespace VuToanThang_23110329.Forms
{
    partial class frmChamCong
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
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabCheckInOut = new System.Windows.Forms.TabPage();
            this.pnlCheckInOut = new System.Windows.Forms.Panel();
            this.lblTrangThai = new System.Windows.Forms.Label();
            this.lblThongTinCa = new System.Windows.Forms.Label();
            this.lblCheckInTime = new System.Windows.Forms.Label();
            this.lblCheckOutTime = new System.Windows.Forms.Label();
            this.btnCheckIn = new Guna.UI2.WinForms.Guna2Button();
            this.btnCheckOut = new Guna.UI2.WinForms.Guna2Button();
            this.btnLamMoi = new Guna.UI2.WinForms.Guna2Button();
            this.tabLichSu = new System.Windows.Forms.TabPage();
            this.dgvLichSu = new Guna.UI2.WinForms.Guna2DataGridView();
            this.pnlFilter = new System.Windows.Forms.Panel();
            this.lblThang = new System.Windows.Forms.Label();
            this.cmbThang = new Guna.UI2.WinForms.Guna2ComboBox();
            this.lblNam = new System.Windows.Forms.Label();
            this.cmbNam = new Guna.UI2.WinForms.Guna2ComboBox();
            this.btnXemLichSu = new Guna.UI2.WinForms.Guna2Button();
            this.tabKhoaCong = new System.Windows.Forms.TabPage();
            this.pnlKhoaCong = new System.Windows.Forms.Panel();
            this.lblThangKhoa = new System.Windows.Forms.Label();
            this.cmbThangKhoa = new Guna.UI2.WinForms.Guna2ComboBox();
            this.lblNamKhoa = new System.Windows.Forms.Label();
            this.cmbNamKhoa = new Guna.UI2.WinForms.Guna2ComboBox();
            this.btnKhoaCong = new Guna.UI2.WinForms.Guna2Button();
            this.lblTrangThaiKhoa = new System.Windows.Forms.Label();
            this.pnlMain.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.tabCheckInOut.SuspendLayout();
            this.pnlCheckInOut.SuspendLayout();
            this.tabLichSu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLichSu)).BeginInit();
            this.pnlFilter.SuspendLayout();
            this.tabKhoaCong.SuspendLayout();
            this.pnlKhoaCong.SuspendLayout();
            this.SuspendLayout();
            
            // pnlMain
            this.pnlMain.BackColor = System.Drawing.Color.White;
            this.pnlMain.Controls.Add(this.tabControl);
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
            this.lblTitle.Size = new System.Drawing.Size(120, 30);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Chấm công";
            
            // pnlHeader
            this.pnlHeader.BackColor = System.Drawing.Color.FromArgb(94, 148, 255);
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(1200, 5);
            this.pnlHeader.TabIndex = 1;
            
            // tabControl
            this.tabControl.Controls.Add(this.tabCheckInOut);
            this.tabControl.Controls.Add(this.tabLichSu);
            this.tabControl.Controls.Add(this.tabKhoaCong);
            this.tabControl.Location = new System.Drawing.Point(20, 60);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(1160, 620);
            this.tabControl.TabIndex = 2;
            this.tabControl.SelectedIndexChanged += new System.EventHandler(this.tabControl_SelectedIndexChanged);
            
            // tabCheckInOut
            this.tabCheckInOut.BackColor = System.Drawing.Color.FromArgb(248, 249, 250);
            this.tabCheckInOut.Controls.Add(this.pnlCheckInOut);
            this.tabCheckInOut.Location = new System.Drawing.Point(4, 22);
            this.tabCheckInOut.Name = "tabCheckInOut";
            this.tabCheckInOut.Padding = new System.Windows.Forms.Padding(3);
            this.tabCheckInOut.Size = new System.Drawing.Size(1152, 594);
            this.tabCheckInOut.TabIndex = 0;
            this.tabCheckInOut.Text = "Check In/Out";
            
            // pnlCheckInOut
            this.pnlCheckInOut.BackColor = System.Drawing.Color.White;
            this.pnlCheckInOut.Controls.Add(this.lblTrangThaiKhoa);
            this.pnlCheckInOut.Controls.Add(this.btnLamMoi);
            this.pnlCheckInOut.Controls.Add(this.btnCheckOut);
            this.pnlCheckInOut.Controls.Add(this.btnCheckIn);
            this.pnlCheckInOut.Controls.Add(this.lblCheckOutTime);
            this.pnlCheckInOut.Controls.Add(this.lblCheckInTime);
            this.pnlCheckInOut.Controls.Add(this.lblThongTinCa);
            this.pnlCheckInOut.Controls.Add(this.lblTrangThai);
            this.pnlCheckInOut.Location = new System.Drawing.Point(20, 20);
            this.pnlCheckInOut.Name = "pnlCheckInOut";
            this.pnlCheckInOut.Size = new System.Drawing.Size(1112, 554);
            this.pnlCheckInOut.TabIndex = 0;
            
            // lblTrangThai
            this.lblTrangThai.AutoSize = true;
            this.lblTrangThai.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblTrangThai.ForeColor = System.Drawing.Color.FromArgb(64, 64, 64);
            this.lblTrangThai.Location = new System.Drawing.Point(20, 20);
            this.lblTrangThai.Name = "lblTrangThai";
            this.lblTrangThai.Size = new System.Drawing.Size(200, 25);
            this.lblTrangThai.TabIndex = 0;
            this.lblTrangThai.Text = "Đang tải trạng thái...";
            
            // lblThongTinCa
            this.lblThongTinCa.AutoSize = true;
            this.lblThongTinCa.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.lblThongTinCa.ForeColor = System.Drawing.Color.FromArgb(64, 64, 64);
            this.lblThongTinCa.Location = new System.Drawing.Point(20, 60);
            this.lblThongTinCa.Name = "lblThongTinCa";
            this.lblThongTinCa.Size = new System.Drawing.Size(100, 21);
            this.lblThongTinCa.TabIndex = 1;
            this.lblThongTinCa.Text = "Thông tin ca:";
            
            // lblCheckInTime
            this.lblCheckInTime.AutoSize = true;
            this.lblCheckInTime.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblCheckInTime.ForeColor = System.Drawing.Color.FromArgb(64, 64, 64);
            this.lblCheckInTime.Location = new System.Drawing.Point(20, 200);
            this.lblCheckInTime.Name = "lblCheckInTime";
            this.lblCheckInTime.Size = new System.Drawing.Size(100, 19);
            this.lblCheckInTime.TabIndex = 2;
            this.lblCheckInTime.Text = "Giờ vào: Chưa có";
            
            // lblCheckOutTime
            this.lblCheckOutTime.AutoSize = true;
            this.lblCheckOutTime.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblCheckOutTime.ForeColor = System.Drawing.Color.FromArgb(64, 64, 64);
            this.lblCheckOutTime.Location = new System.Drawing.Point(20, 230);
            this.lblCheckOutTime.Name = "lblCheckOutTime";
            this.lblCheckOutTime.Size = new System.Drawing.Size(100, 19);
            this.lblCheckOutTime.TabIndex = 3;
            this.lblCheckOutTime.Text = "Giờ ra: Chưa có";
            
            // btnCheckIn
            this.btnCheckIn.BorderRadius = 12;
            this.btnCheckIn.FillColor = System.Drawing.Color.FromArgb(40, 167, 69);
            this.btnCheckIn.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnCheckIn.ForeColor = System.Drawing.Color.White;
            this.btnCheckIn.Location = new System.Drawing.Point(100, 120);
            this.btnCheckIn.Name = "btnCheckIn";
            this.btnCheckIn.Size = new System.Drawing.Size(120, 50);
            this.btnCheckIn.TabIndex = 4;
            this.btnCheckIn.Text = "Check In";
            this.btnCheckIn.Click += new System.EventHandler(this.btnCheckIn_Click);
            
            // btnCheckOut
            this.btnCheckOut.BorderRadius = 12;
            this.btnCheckOut.FillColor = System.Drawing.Color.FromArgb(220, 53, 69);
            this.btnCheckOut.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnCheckOut.ForeColor = System.Drawing.Color.White;
            this.btnCheckOut.Location = new System.Drawing.Point(250, 120);
            this.btnCheckOut.Name = "btnCheckOut";
            this.btnCheckOut.Size = new System.Drawing.Size(120, 50);
            this.btnCheckOut.TabIndex = 5;
            this.btnCheckOut.Text = "Check Out";
            this.btnCheckOut.Click += new System.EventHandler(this.btnCheckOut_Click);
            
            // btnLamMoi
            this.btnLamMoi.BorderRadius = 12;
            this.btnLamMoi.FillColor = System.Drawing.Color.FromArgb(108, 117, 125);
            this.btnLamMoi.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnLamMoi.ForeColor = System.Drawing.Color.White;
            this.btnLamMoi.Location = new System.Drawing.Point(400, 130);
            this.btnLamMoi.Name = "btnLamMoi";
            this.btnLamMoi.Size = new System.Drawing.Size(100, 30);
            this.btnLamMoi.TabIndex = 6;
            this.btnLamMoi.Text = "Làm mới";
            this.btnLamMoi.Click += new System.EventHandler(this.btnLamMoi_Click);
            
            // tabLichSu
            this.tabLichSu.BackColor = System.Drawing.Color.FromArgb(248, 249, 250);
            this.tabLichSu.Controls.Add(this.dgvLichSu);
            this.tabLichSu.Controls.Add(this.pnlFilter);
            this.tabLichSu.Location = new System.Drawing.Point(4, 22);
            this.tabLichSu.Name = "tabLichSu";
            this.tabLichSu.Padding = new System.Windows.Forms.Padding(3);
            this.tabLichSu.Size = new System.Drawing.Size(1152, 594);
            this.tabLichSu.TabIndex = 1;
            this.tabLichSu.Text = "Lịch sử chấm công";
            
            // pnlFilter
            this.pnlFilter.BackColor = System.Drawing.Color.White;
            this.pnlFilter.Controls.Add(this.btnXemLichSu);
            this.pnlFilter.Controls.Add(this.cmbNam);
            this.pnlFilter.Controls.Add(this.lblNam);
            this.pnlFilter.Controls.Add(this.cmbThang);
            this.pnlFilter.Controls.Add(this.lblThang);
            this.pnlFilter.Location = new System.Drawing.Point(20, 20);
            this.pnlFilter.Name = "pnlFilter";
            this.pnlFilter.Size = new System.Drawing.Size(1112, 60);
            this.pnlFilter.TabIndex = 0;
            
            // lblThang
            this.lblThang.AutoSize = true;
            this.lblThang.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblThang.ForeColor = System.Drawing.Color.FromArgb(64, 64, 64);
            this.lblThang.Location = new System.Drawing.Point(20, 20);
            this.lblThang.Name = "lblThang";
            this.lblThang.Size = new System.Drawing.Size(45, 19);
            this.lblThang.TabIndex = 0;
            this.lblThang.Text = "Tháng:";
            
            // cmbThang
            this.cmbThang.BackColor = System.Drawing.Color.Transparent;
            this.cmbThang.BorderRadius = 8;
            this.cmbThang.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbThang.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbThang.FillColor = System.Drawing.Color.White;
            this.cmbThang.FocusedColor = System.Drawing.Color.FromArgb(94, 148, 255);
            this.cmbThang.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cmbThang.ForeColor = System.Drawing.Color.Black;
            this.cmbThang.ItemHeight = 30;
            this.cmbThang.Location = new System.Drawing.Point(80, 15);
            this.cmbThang.Name = "cmbThang";
            this.cmbThang.Size = new System.Drawing.Size(100, 36);
            this.cmbThang.TabIndex = 1;
            this.cmbThang.SelectedIndexChanged += new System.EventHandler(this.cmbThang_SelectedIndexChanged);
            
            // lblNam
            this.lblNam.AutoSize = true;
            this.lblNam.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblNam.ForeColor = System.Drawing.Color.FromArgb(64, 64, 64);
            this.lblNam.Location = new System.Drawing.Point(200, 20);
            this.lblNam.Name = "lblNam";
            this.lblNam.Size = new System.Drawing.Size(35, 19);
            this.lblNam.TabIndex = 2;
            this.lblNam.Text = "Năm:";
            
            // cmbNam
            this.cmbNam.BackColor = System.Drawing.Color.Transparent;
            this.cmbNam.BorderRadius = 8;
            this.cmbNam.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbNam.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbNam.FillColor = System.Drawing.Color.White;
            this.cmbNam.FocusedColor = System.Drawing.Color.FromArgb(94, 148, 255);
            this.cmbNam.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cmbNam.ForeColor = System.Drawing.Color.Black;
            this.cmbNam.ItemHeight = 30;
            this.cmbNam.Location = new System.Drawing.Point(250, 15);
            this.cmbNam.Name = "cmbNam";
            this.cmbNam.Size = new System.Drawing.Size(100, 36);
            this.cmbNam.TabIndex = 3;
            this.cmbNam.SelectedIndexChanged += new System.EventHandler(this.cmbNam_SelectedIndexChanged);
            
            // btnXemLichSu
            this.btnXemLichSu.BorderRadius = 12;
            this.btnXemLichSu.FillColor = System.Drawing.Color.FromArgb(94, 148, 255);
            this.btnXemLichSu.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnXemLichSu.ForeColor = System.Drawing.Color.White;
            this.btnXemLichSu.Location = new System.Drawing.Point(380, 15);
            this.btnXemLichSu.Name = "btnXemLichSu";
            this.btnXemLichSu.Size = new System.Drawing.Size(100, 36);
            this.btnXemLichSu.TabIndex = 4;
            this.btnXemLichSu.Text = "Xem lịch sử";
            this.btnXemLichSu.Click += new System.EventHandler(this.btnXemLichSu_Click);
            
            // dgvLichSu
            this.dgvLichSu.AllowUserToAddRows = false;
            this.dgvLichSu.AllowUserToDeleteRows = false;
            this.dgvLichSu.BackgroundColor = System.Drawing.Color.White;
            this.dgvLichSu.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvLichSu.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(94, 148, 255);
            this.dgvLichSu.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.dgvLichSu.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.White;
            this.dgvLichSu.ColumnHeadersDefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(94, 148, 255);
            this.dgvLichSu.ColumnHeadersHeight = 40;
            this.dgvLichSu.DefaultCellStyle.BackColor = System.Drawing.Color.White;
            this.dgvLichSu.DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.dgvLichSu.DefaultCellStyle.ForeColor = System.Drawing.Color.Black;
            this.dgvLichSu.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(231, 229, 255);
            this.dgvLichSu.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.Black;
            this.dgvLichSu.Location = new System.Drawing.Point(20, 90);
            this.dgvLichSu.MultiSelect = false;
            this.dgvLichSu.Name = "dgvLichSu";
            this.dgvLichSu.ReadOnly = true;
            this.dgvLichSu.RowHeadersVisible = false;
            this.dgvLichSu.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvLichSu.Size = new System.Drawing.Size(1112, 490);
            this.dgvLichSu.TabIndex = 1;
            this.dgvLichSu.ThemeStyle.AlternatingRowsStyle.BackColor = System.Drawing.Color.FromArgb(248, 249, 250);
            this.dgvLichSu.ThemeStyle.AlternatingRowsStyle.Font = null;
            this.dgvLichSu.ThemeStyle.AlternatingRowsStyle.ForeColor = System.Drawing.Color.Empty;
            this.dgvLichSu.ThemeStyle.AlternatingRowsStyle.SelectionBackColor = System.Drawing.Color.Empty;
            this.dgvLichSu.ThemeStyle.AlternatingRowsStyle.SelectionForeColor = System.Drawing.Color.Empty;
            this.dgvLichSu.ThemeStyle.BackColor = System.Drawing.Color.White;
            this.dgvLichSu.ThemeStyle.GridColor = System.Drawing.Color.FromArgb(231, 229, 255);
            this.dgvLichSu.ThemeStyle.HeaderStyle.BackColor = System.Drawing.Color.FromArgb(94, 148, 255);
            this.dgvLichSu.ThemeStyle.HeaderStyle.BorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgvLichSu.ThemeStyle.HeaderStyle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.dgvLichSu.ThemeStyle.HeaderStyle.ForeColor = System.Drawing.Color.White;
            this.dgvLichSu.ThemeStyle.HeaderStyle.HeaightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvLichSu.ThemeStyle.HeaderStyle.Height = 40;
            this.dgvLichSu.ThemeStyle.ReadOnly = true;
            this.dgvLichSu.ThemeStyle.RowsStyle.BackColor = System.Drawing.Color.White;
            this.dgvLichSu.ThemeStyle.RowsStyle.BorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvLichSu.ThemeStyle.RowsStyle.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.dgvLichSu.ThemeStyle.RowsStyle.ForeColor = System.Drawing.Color.Black;
            this.dgvLichSu.ThemeStyle.RowsStyle.Height = 22;
            this.dgvLichSu.ThemeStyle.RowsStyle.SelectionBackColor = System.Drawing.Color.FromArgb(231, 229, 255);
            this.dgvLichSu.ThemeStyle.RowsStyle.SelectionForeColor = System.Drawing.Color.Black;
            
            // tabKhoaCong
            this.tabKhoaCong.BackColor = System.Drawing.Color.FromArgb(248, 249, 250);
            this.tabKhoaCong.Controls.Add(this.pnlKhoaCong);
            this.tabKhoaCong.Location = new System.Drawing.Point(4, 22);
            this.tabKhoaCong.Name = "tabKhoaCong";
            this.tabKhoaCong.Padding = new System.Windows.Forms.Padding(3);
            this.tabKhoaCong.Size = new System.Drawing.Size(1152, 594);
            this.tabKhoaCong.TabIndex = 2;
            this.tabKhoaCong.Text = "Khóa công kỳ";
            
            // pnlKhoaCong
            this.pnlKhoaCong.BackColor = System.Drawing.Color.White;
            this.pnlKhoaCong.Controls.Add(this.lblTrangThaiKhoa);
            this.pnlKhoaCong.Controls.Add(this.btnKhoaCong);
            this.pnlKhoaCong.Controls.Add(this.cmbNamKhoa);
            this.pnlKhoaCong.Controls.Add(this.lblNamKhoa);
            this.pnlKhoaCong.Controls.Add(this.cmbThangKhoa);
            this.pnlKhoaCong.Controls.Add(this.lblThangKhoa);
            this.pnlKhoaCong.Location = new System.Drawing.Point(20, 20);
            this.pnlKhoaCong.Name = "pnlKhoaCong";
            this.pnlKhoaCong.Size = new System.Drawing.Size(1112, 554);
            this.pnlKhoaCong.TabIndex = 0;
            
            // lblThangKhoa
            this.lblThangKhoa.AutoSize = true;
            this.lblThangKhoa.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblThangKhoa.ForeColor = System.Drawing.Color.FromArgb(64, 64, 64);
            this.lblThangKhoa.Location = new System.Drawing.Point(20, 20);
            this.lblThangKhoa.Name = "lblThangKhoa";
            this.lblThangKhoa.Size = new System.Drawing.Size(55, 21);
            this.lblThangKhoa.TabIndex = 0;
            this.lblThangKhoa.Text = "Tháng:";
            
            // cmbThangKhoa
            this.cmbThangKhoa.BackColor = System.Drawing.Color.Transparent;
            this.cmbThangKhoa.BorderRadius = 8;
            this.cmbThangKhoa.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbThangKhoa.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbThangKhoa.FillColor = System.Drawing.Color.White;
            this.cmbThangKhoa.FocusedColor = System.Drawing.Color.FromArgb(94, 148, 255);
            this.cmbThangKhoa.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbThangKhoa.ForeColor = System.Drawing.Color.Black;
            this.cmbThangKhoa.ItemHeight = 30;
            this.cmbThangKhoa.Location = new System.Drawing.Point(100, 15);
            this.cmbThangKhoa.Name = "cmbThangKhoa";
            this.cmbThangKhoa.Size = new System.Drawing.Size(120, 36);
            this.cmbThangKhoa.TabIndex = 1;
            
            // lblNamKhoa
            this.lblNamKhoa.AutoSize = true;
            this.lblNamKhoa.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblNamKhoa.ForeColor = System.Drawing.Color.FromArgb(64, 64, 64);
            this.lblNamKhoa.Location = new System.Drawing.Point(250, 20);
            this.lblNamKhoa.Name = "lblNamKhoa";
            this.lblNamKhoa.Size = new System.Drawing.Size(45, 21);
            this.lblNamKhoa.TabIndex = 2;
            this.lblNamKhoa.Text = "Năm:";
            
            // cmbNamKhoa
            this.cmbNamKhoa.BackColor = System.Drawing.Color.Transparent;
            this.cmbNamKhoa.BorderRadius = 8;
            this.cmbNamKhoa.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbNamKhoa.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbNamKhoa.FillColor = System.Drawing.Color.White;
            this.cmbNamKhoa.FocusedColor = System.Drawing.Color.FromArgb(94, 148, 255);
            this.cmbNamKhoa.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbNamKhoa.ForeColor = System.Drawing.Color.Black;
            this.cmbNamKhoa.ItemHeight = 30;
            this.cmbNamKhoa.Location = new System.Drawing.Point(310, 15);
            this.cmbNamKhoa.Name = "cmbNamKhoa";
            this.cmbNamKhoa.Size = new System.Drawing.Size(120, 36);
            this.cmbNamKhoa.TabIndex = 3;
            
            // btnKhoaCong
            this.btnKhoaCong.BorderRadius = 12;
            this.btnKhoaCong.FillColor = System.Drawing.Color.FromArgb(255, 193, 7);
            this.btnKhoaCong.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnKhoaCong.ForeColor = System.Drawing.Color.White;
            this.btnKhoaCong.Location = new System.Drawing.Point(100, 80);
            this.btnKhoaCong.Name = "btnKhoaCong";
            this.btnKhoaCong.Size = new System.Drawing.Size(150, 50);
            this.btnKhoaCong.TabIndex = 4;
            this.btnKhoaCong.Text = "Khóa công kỳ";
            this.btnKhoaCong.Click += new System.EventHandler(this.btnKhoaCong_Click);
            
            // lblTrangThaiKhoa
            this.lblTrangThaiKhoa.AutoSize = true;
            this.lblTrangThaiKhoa.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblTrangThaiKhoa.ForeColor = System.Drawing.Color.FromArgb(64, 64, 64);
            this.lblTrangThaiKhoa.Location = new System.Drawing.Point(20, 150);
            this.lblTrangThaiKhoa.Name = "lblTrangThaiKhoa";
            this.lblTrangThaiKhoa.Size = new System.Drawing.Size(200, 19);
            this.lblTrangThaiKhoa.TabIndex = 5;
            this.lblTrangThaiKhoa.Text = "Trạng thái khóa công sẽ hiển thị ở đây";
            
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1200, 700);
            this.Controls.Add(this.pnlMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmChamCong";
            this.Text = "Chấm công";
            
            this.pnlMain.ResumeLayout(false);
            this.pnlMain.PerformLayout();
            this.tabControl.ResumeLayout(false);
            this.tabCheckInOut.ResumeLayout(false);
            this.pnlCheckInOut.ResumeLayout(false);
            this.pnlCheckInOut.PerformLayout();
            this.tabLichSu.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvLichSu)).EndInit();
            this.pnlFilter.ResumeLayout(false);
            this.pnlFilter.PerformLayout();
            this.tabKhoaCong.ResumeLayout(false);
            this.pnlKhoaCong.ResumeLayout(false);
            this.pnlKhoaCong.PerformLayout();
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabCheckInOut;
        private System.Windows.Forms.Panel pnlCheckInOut;
        private System.Windows.Forms.Label lblTrangThai;
        private System.Windows.Forms.Label lblThongTinCa;
        private System.Windows.Forms.Label lblCheckInTime;
        private System.Windows.Forms.Label lblCheckOutTime;
        private Guna.UI2.WinForms.Guna2Button btnCheckIn;
        private Guna.UI2.WinForms.Guna2Button btnCheckOut;
        private Guna.UI2.WinForms.Guna2Button btnLamMoi;
        private System.Windows.Forms.TabPage tabLichSu;
        private Guna.UI2.WinForms.Guna2DataGridView dgvLichSu;
        private System.Windows.Forms.Panel pnlFilter;
        private System.Windows.Forms.Label lblThang;
        private Guna.UI2.WinForms.Guna2ComboBox cmbThang;
        private System.Windows.Forms.Label lblNam;
        private Guna.UI2.WinForms.Guna2ComboBox cmbNam;
        private Guna.UI2.WinForms.Guna2Button btnXemLichSu;
        private System.Windows.Forms.TabPage tabKhoaCong;
        private System.Windows.Forms.Panel pnlKhoaCong;
        private System.Windows.Forms.Label lblThangKhoa;
        private Guna.UI2.WinForms.Guna2ComboBox cmbThangKhoa;
        private System.Windows.Forms.Label lblNamKhoa;
        private Guna.UI2.WinForms.Guna2ComboBox cmbNamKhoa;
        private Guna.UI2.WinForms.Guna2Button btnKhoaCong;
        private System.Windows.Forms.Label lblTrangThaiKhoa;
    }
}
