namespace VuToanThang_23110329.Forms
{
    partial class frmBangLuong
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.pnlMain = new System.Windows.Forms.Panel();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabCongThang = new System.Windows.Forms.TabPage();
            this.dgvCongThang = new Guna.UI2.WinForms.Guna2DataGridView();
            this.tabBangLuong = new System.Windows.Forms.TabPage();
            this.dgvBangLuong = new Guna.UI2.WinForms.Guna2DataGridView();
            this.pnlSummary = new System.Windows.Forms.Panel();
            this.lblTongKhauTru = new System.Windows.Forms.Label();
            this.lblTongPhuCap = new System.Windows.Forms.Label();
            this.lblTongLuong = new System.Windows.Forms.Label();
            this.lblTongNhanVien = new System.Windows.Forms.Label();
            this.pnlButtons = new System.Windows.Forms.Panel();
            this.btnLamMoi = new Guna.UI2.WinForms.Guna2Button();
            this.btnCapNhatPhuCap = new Guna.UI2.WinForms.Guna2Button();
            this.btnDongLuong = new Guna.UI2.WinForms.Guna2Button();
            this.btnChayLuong = new Guna.UI2.WinForms.Guna2Button();
            this.pnlFilter = new System.Windows.Forms.Panel();
            this.cmbNam = new Guna.UI2.WinForms.Guna2ComboBox();
            this.lblNam = new System.Windows.Forms.Label();
            this.cmbThang = new Guna.UI2.WinForms.Guna2ComboBox();
            this.lblThang = new System.Windows.Forms.Label();
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.pnlMain.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.tabCongThang.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCongThang)).BeginInit();
            this.tabBangLuong.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBangLuong)).BeginInit();
            this.pnlSummary.SuspendLayout();
            this.pnlButtons.SuspendLayout();
            this.pnlFilter.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlMain
            // 
            this.pnlMain.BackColor = System.Drawing.Color.White;
            this.pnlMain.Controls.Add(this.tabControl);
            this.pnlMain.Controls.Add(this.pnlButtons);
            this.pnlMain.Controls.Add(this.pnlFilter);
            this.pnlMain.Controls.Add(this.pnlHeader);
            this.pnlMain.Controls.Add(this.lblTitle);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 0);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Padding = new System.Windows.Forms.Padding(20);
            this.pnlMain.Size = new System.Drawing.Size(1200, 700);
            this.pnlMain.TabIndex = 0;
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabCongThang);
            this.tabControl.Controls.Add(this.tabBangLuong);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point(20, 160);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(1160, 520);
            this.tabControl.TabIndex = 4;
            this.tabControl.SelectedIndexChanged += new System.EventHandler(this.tabControl_SelectedIndexChanged);
            // 
            // tabCongThang
            // 
            this.tabCongThang.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(250)))));
            this.tabCongThang.Controls.Add(this.dgvCongThang);
            this.tabCongThang.Location = new System.Drawing.Point(4, 22);
            this.tabCongThang.Name = "tabCongThang";
            this.tabCongThang.Padding = new System.Windows.Forms.Padding(3);
            this.tabCongThang.Size = new System.Drawing.Size(1152, 494);
            this.tabCongThang.TabIndex = 0;
            this.tabCongThang.Text = "Tổng hợp công tháng";
            // 
            // dgvCongThang
            // 
            this.dgvCongThang.AllowUserToAddRows = false;
            this.dgvCongThang.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(250)))));
            this.dgvCongThang.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvCongThang.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvCongThang.ColumnHeadersHeight = 40;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 9F);
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvCongThang.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgvCongThang.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvCongThang.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.dgvCongThang.Location = new System.Drawing.Point(3, 3);
            this.dgvCongThang.MultiSelect = false;
            this.dgvCongThang.Name = "dgvCongThang";
            this.dgvCongThang.ReadOnly = true;
            this.dgvCongThang.RowHeadersVisible = false;
            this.dgvCongThang.Size = new System.Drawing.Size(1146, 488);
            this.dgvCongThang.TabIndex = 0;
            this.dgvCongThang.ThemeStyle.AlternatingRowsStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(250)))));
            this.dgvCongThang.ThemeStyle.AlternatingRowsStyle.Font = null;
            this.dgvCongThang.ThemeStyle.AlternatingRowsStyle.ForeColor = System.Drawing.Color.Empty;
            this.dgvCongThang.ThemeStyle.AlternatingRowsStyle.SelectionBackColor = System.Drawing.Color.Empty;
            this.dgvCongThang.ThemeStyle.AlternatingRowsStyle.SelectionForeColor = System.Drawing.Color.Empty;
            this.dgvCongThang.ThemeStyle.BackColor = System.Drawing.Color.White;
            this.dgvCongThang.ThemeStyle.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.dgvCongThang.ThemeStyle.HeaderStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.dgvCongThang.ThemeStyle.HeaderStyle.BorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgvCongThang.ThemeStyle.HeaderStyle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.dgvCongThang.ThemeStyle.HeaderStyle.ForeColor = System.Drawing.Color.White;
            this.dgvCongThang.ThemeStyle.HeaderStyle.HeaightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvCongThang.ThemeStyle.HeaderStyle.Height = 40;
            this.dgvCongThang.ThemeStyle.ReadOnly = true;
            this.dgvCongThang.ThemeStyle.RowsStyle.BackColor = System.Drawing.Color.White;
            this.dgvCongThang.ThemeStyle.RowsStyle.BorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvCongThang.ThemeStyle.RowsStyle.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.dgvCongThang.ThemeStyle.RowsStyle.ForeColor = System.Drawing.Color.Black;
            this.dgvCongThang.ThemeStyle.RowsStyle.Height = 22;
            this.dgvCongThang.ThemeStyle.RowsStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.dgvCongThang.ThemeStyle.RowsStyle.SelectionForeColor = System.Drawing.Color.Black;
            // 
            // tabBangLuong
            // 
            this.tabBangLuong.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(250)))));
            this.tabBangLuong.Controls.Add(this.dgvBangLuong);
            this.tabBangLuong.Controls.Add(this.pnlSummary);
            this.tabBangLuong.Location = new System.Drawing.Point(4, 22);
            this.tabBangLuong.Name = "tabBangLuong";
            this.tabBangLuong.Padding = new System.Windows.Forms.Padding(3);
            this.tabBangLuong.Size = new System.Drawing.Size(1152, 494);
            this.tabBangLuong.TabIndex = 1;
            this.tabBangLuong.Text = "Bảng lương";
            // 
            // dgvBangLuong
            // 
            this.dgvBangLuong.AllowUserToAddRows = false;
            this.dgvBangLuong.AllowUserToDeleteRows = false;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(250)))));
            this.dgvBangLuong.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle4;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvBangLuong.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.dgvBangLuong.ColumnHeadersHeight = 40;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Segoe UI", 9F);
            dataGridViewCellStyle6.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvBangLuong.DefaultCellStyle = dataGridViewCellStyle6;
            this.dgvBangLuong.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvBangLuong.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.dgvBangLuong.Location = new System.Drawing.Point(3, 63);
            this.dgvBangLuong.MultiSelect = false;
            this.dgvBangLuong.Name = "dgvBangLuong";
            this.dgvBangLuong.ReadOnly = true;
            this.dgvBangLuong.RowHeadersVisible = false;
            this.dgvBangLuong.Size = new System.Drawing.Size(1146, 428);
            this.dgvBangLuong.TabIndex = 1;
            this.dgvBangLuong.ThemeStyle.AlternatingRowsStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(250)))));
            this.dgvBangLuong.ThemeStyle.AlternatingRowsStyle.Font = null;
            this.dgvBangLuong.ThemeStyle.AlternatingRowsStyle.ForeColor = System.Drawing.Color.Empty;
            this.dgvBangLuong.ThemeStyle.AlternatingRowsStyle.SelectionBackColor = System.Drawing.Color.Empty;
            this.dgvBangLuong.ThemeStyle.AlternatingRowsStyle.SelectionForeColor = System.Drawing.Color.Empty;
            this.dgvBangLuong.ThemeStyle.BackColor = System.Drawing.Color.White;
            this.dgvBangLuong.ThemeStyle.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.dgvBangLuong.ThemeStyle.HeaderStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.dgvBangLuong.ThemeStyle.HeaderStyle.BorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgvBangLuong.ThemeStyle.HeaderStyle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.dgvBangLuong.ThemeStyle.HeaderStyle.ForeColor = System.Drawing.Color.White;
            this.dgvBangLuong.ThemeStyle.HeaderStyle.HeaightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvBangLuong.ThemeStyle.HeaderStyle.Height = 40;
            this.dgvBangLuong.ThemeStyle.ReadOnly = true;
            this.dgvBangLuong.ThemeStyle.RowsStyle.BackColor = System.Drawing.Color.White;
            this.dgvBangLuong.ThemeStyle.RowsStyle.BorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvBangLuong.ThemeStyle.RowsStyle.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.dgvBangLuong.ThemeStyle.RowsStyle.ForeColor = System.Drawing.Color.Black;
            this.dgvBangLuong.ThemeStyle.RowsStyle.Height = 22;
            this.dgvBangLuong.ThemeStyle.RowsStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.dgvBangLuong.ThemeStyle.RowsStyle.SelectionForeColor = System.Drawing.Color.Black;
            // 
            // pnlSummary
            // 
            this.pnlSummary.BackColor = System.Drawing.Color.White;
            this.pnlSummary.Controls.Add(this.lblTongKhauTru);
            this.pnlSummary.Controls.Add(this.lblTongPhuCap);
            this.pnlSummary.Controls.Add(this.lblTongLuong);
            this.pnlSummary.Controls.Add(this.lblTongNhanVien);
            this.pnlSummary.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlSummary.Location = new System.Drawing.Point(3, 3);
            this.pnlSummary.Name = "pnlSummary";
            this.pnlSummary.Size = new System.Drawing.Size(1146, 60);
            this.pnlSummary.TabIndex = 0;
            // 
            // lblTongKhauTru
            // 
            this.lblTongKhauTru.AutoSize = true;
            this.lblTongKhauTru.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblTongKhauTru.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(53)))), ((int)(((byte)(69)))));
            this.lblTongKhauTru.Location = new System.Drawing.Point(600, 20);
            this.lblTongKhauTru.Name = "lblTongKhauTru";
            this.lblTongKhauTru.Size = new System.Drawing.Size(111, 19);
            this.lblTongKhauTru.TabIndex = 3;
            this.lblTongKhauTru.Text = "Tổng khấu trừ: 0";
            // 
            // lblTongPhuCap
            // 
            this.lblTongPhuCap.AutoSize = true;
            this.lblTongPhuCap.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblTongPhuCap.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(193)))), ((int)(((byte)(7)))));
            this.lblTongPhuCap.Location = new System.Drawing.Point(400, 20);
            this.lblTongPhuCap.Name = "lblTongPhuCap";
            this.lblTongPhuCap.Size = new System.Drawing.Size(108, 19);
            this.lblTongPhuCap.TabIndex = 2;
            this.lblTongPhuCap.Text = "Tổng phụ cấp: 0";
            // 
            // lblTongLuong
            // 
            this.lblTongLuong.AutoSize = true;
            this.lblTongLuong.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblTongLuong.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(167)))), ((int)(((byte)(69)))));
            this.lblTongLuong.Location = new System.Drawing.Point(200, 20);
            this.lblTongLuong.Name = "lblTongLuong";
            this.lblTongLuong.Size = new System.Drawing.Size(94, 19);
            this.lblTongLuong.TabIndex = 1;
            this.lblTongLuong.Text = "Tổng lương: 0";
            // 
            // lblTongNhanVien
            // 
            this.lblTongNhanVien.AutoSize = true;
            this.lblTongNhanVien.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblTongNhanVien.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblTongNhanVien.Location = new System.Drawing.Point(20, 20);
            this.lblTongNhanVien.Name = "lblTongNhanVien";
            this.lblTongNhanVien.Size = new System.Drawing.Size(119, 19);
            this.lblTongNhanVien.TabIndex = 0;
            this.lblTongNhanVien.Text = "Tổng nhân viên: 0";
            // 
            // pnlButtons
            // 
            this.pnlButtons.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(250)))));
            this.pnlButtons.Controls.Add(this.btnLamMoi);
            this.pnlButtons.Controls.Add(this.btnCapNhatPhuCap);
            this.pnlButtons.Controls.Add(this.btnDongLuong);
            this.pnlButtons.Controls.Add(this.btnChayLuong);
            this.pnlButtons.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlButtons.Location = new System.Drawing.Point(20, 110);
            this.pnlButtons.Name = "pnlButtons";
            this.pnlButtons.Size = new System.Drawing.Size(1160, 50);
            this.pnlButtons.TabIndex = 3;
            // 
            // btnLamMoi
            // 
            this.btnLamMoi.BorderRadius = 12;
            this.btnLamMoi.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(117)))), ((int)(((byte)(125)))));
            this.btnLamMoi.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnLamMoi.ForeColor = System.Drawing.Color.White;
            this.btnLamMoi.Location = new System.Drawing.Point(410, 10);
            this.btnLamMoi.Name = "btnLamMoi";
            this.btnLamMoi.Size = new System.Drawing.Size(100, 30);
            this.btnLamMoi.TabIndex = 3;
            this.btnLamMoi.Text = "Làm mới";
            this.btnLamMoi.Click += new System.EventHandler(this.btnLamMoi_Click);
            // 
            // btnCapNhatPhuCap
            // 
            this.btnCapNhatPhuCap.BorderRadius = 12;
            this.btnCapNhatPhuCap.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(193)))), ((int)(((byte)(7)))));
            this.btnCapNhatPhuCap.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnCapNhatPhuCap.ForeColor = System.Drawing.Color.White;
            this.btnCapNhatPhuCap.Location = new System.Drawing.Point(280, 10);
            this.btnCapNhatPhuCap.Name = "btnCapNhatPhuCap";
            this.btnCapNhatPhuCap.Size = new System.Drawing.Size(120, 30);
            this.btnCapNhatPhuCap.TabIndex = 2;
            this.btnCapNhatPhuCap.Text = "Cập nhật phụ cấp";
            this.btnCapNhatPhuCap.Click += new System.EventHandler(this.btnCapNhatPhuCap_Click);
            // 
            // btnDongLuong
            // 
            this.btnDongLuong.BorderRadius = 12;
            this.btnDongLuong.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(53)))), ((int)(((byte)(69)))));
            this.btnDongLuong.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnDongLuong.ForeColor = System.Drawing.Color.White;
            this.btnDongLuong.Location = new System.Drawing.Point(150, 10);
            this.btnDongLuong.Name = "btnDongLuong";
            this.btnDongLuong.Size = new System.Drawing.Size(120, 30);
            this.btnDongLuong.TabIndex = 1;
            this.btnDongLuong.Text = "Đóng bảng lương";
            this.btnDongLuong.Click += new System.EventHandler(this.btnDongLuong_Click);
            // 
            // btnChayLuong
            // 
            this.btnChayLuong.BorderRadius = 12;
            this.btnChayLuong.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(167)))), ((int)(((byte)(69)))));
            this.btnChayLuong.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnChayLuong.ForeColor = System.Drawing.Color.White;
            this.btnChayLuong.Location = new System.Drawing.Point(20, 10);
            this.btnChayLuong.Name = "btnChayLuong";
            this.btnChayLuong.Size = new System.Drawing.Size(120, 30);
            this.btnChayLuong.TabIndex = 0;
            this.btnChayLuong.Text = "Chạy bảng lương";
            this.btnChayLuong.Click += new System.EventHandler(this.btnChayLuong_Click);
            // 
            // pnlFilter
            // 
            this.pnlFilter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(250)))));
            this.pnlFilter.Controls.Add(this.cmbNam);
            this.pnlFilter.Controls.Add(this.lblNam);
            this.pnlFilter.Controls.Add(this.cmbThang);
            this.pnlFilter.Controls.Add(this.lblThang);
            this.pnlFilter.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlFilter.Location = new System.Drawing.Point(20, 50);
            this.pnlFilter.Name = "pnlFilter";
            this.pnlFilter.Size = new System.Drawing.Size(1160, 60);
            this.pnlFilter.TabIndex = 2;
            // 
            // cmbNam
            // 
            this.cmbNam.BackColor = System.Drawing.Color.Transparent;
            this.cmbNam.BorderRadius = 8;
            this.cmbNam.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbNam.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbNam.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cmbNam.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cmbNam.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cmbNam.ForeColor = System.Drawing.Color.Black;
            this.cmbNam.ItemHeight = 30;
            this.cmbNam.Location = new System.Drawing.Point(250, 15);
            this.cmbNam.Name = "cmbNam";
            this.cmbNam.Size = new System.Drawing.Size(100, 36);
            this.cmbNam.TabIndex = 3;
            this.cmbNam.SelectedIndexChanged += new System.EventHandler(this.cmbNam_SelectedIndexChanged);
            // 
            // lblNam
            // 
            this.lblNam.AutoSize = true;
            this.lblNam.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblNam.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblNam.Location = new System.Drawing.Point(200, 20);
            this.lblNam.Name = "lblNam";
            this.lblNam.Size = new System.Drawing.Size(41, 19);
            this.lblNam.TabIndex = 2;
            this.lblNam.Text = "Năm:";
            // 
            // cmbThang
            // 
            this.cmbThang.BackColor = System.Drawing.Color.Transparent;
            this.cmbThang.BorderRadius = 8;
            this.cmbThang.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbThang.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbThang.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cmbThang.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cmbThang.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cmbThang.ForeColor = System.Drawing.Color.Black;
            this.cmbThang.ItemHeight = 30;
            this.cmbThang.Location = new System.Drawing.Point(80, 15);
            this.cmbThang.Name = "cmbThang";
            this.cmbThang.Size = new System.Drawing.Size(100, 36);
            this.cmbThang.TabIndex = 1;
            this.cmbThang.SelectedIndexChanged += new System.EventHandler(this.cmbThang_SelectedIndexChanged);
            // 
            // lblThang
            // 
            this.lblThang.AutoSize = true;
            this.lblThang.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblThang.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblThang.Location = new System.Drawing.Point(20, 20);
            this.lblThang.Name = "lblThang";
            this.lblThang.Size = new System.Drawing.Size(50, 19);
            this.lblThang.TabIndex = 0;
            this.lblThang.Text = "Tháng:";
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(1200, 5);
            this.pnlHeader.TabIndex = 1;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.lblTitle.Location = new System.Drawing.Point(20, 20);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(134, 30);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Bảng lương";
            // 
            // frmBangLuong
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1200, 700);
            this.Controls.Add(this.pnlMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmBangLuong";
            this.Text = "Bảng lương";
            this.Load += new System.EventHandler(this.frmBangLuong_Load);
            this.pnlMain.ResumeLayout(false);
            this.pnlMain.PerformLayout();
            this.tabControl.ResumeLayout(false);
            this.tabCongThang.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCongThang)).EndInit();
            this.tabBangLuong.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvBangLuong)).EndInit();
            this.pnlSummary.ResumeLayout(false);
            this.pnlSummary.PerformLayout();
            this.pnlButtons.ResumeLayout(false);
            this.pnlFilter.ResumeLayout(false);
            this.pnlFilter.PerformLayout();
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Panel pnlFilter;
        private System.Windows.Forms.Label lblThang;
        private Guna.UI2.WinForms.Guna2ComboBox cmbThang;
        private System.Windows.Forms.Label lblNam;
        private Guna.UI2.WinForms.Guna2ComboBox cmbNam;
        private System.Windows.Forms.Panel pnlButtons;
        private Guna.UI2.WinForms.Guna2Button btnChayLuong;
        private Guna.UI2.WinForms.Guna2Button btnDongLuong;
        private Guna.UI2.WinForms.Guna2Button btnCapNhatPhuCap;
        private Guna.UI2.WinForms.Guna2Button btnLamMoi;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabCongThang;
        private Guna.UI2.WinForms.Guna2DataGridView dgvCongThang;
        private System.Windows.Forms.TabPage tabBangLuong;
        private Guna.UI2.WinForms.Guna2DataGridView dgvBangLuong;
        private System.Windows.Forms.Panel pnlSummary;
        private System.Windows.Forms.Label lblTongNhanVien;
        private System.Windows.Forms.Label lblTongLuong;
        private System.Windows.Forms.Label lblTongPhuCap;
        private System.Windows.Forms.Label lblTongKhauTru;
    }
}
