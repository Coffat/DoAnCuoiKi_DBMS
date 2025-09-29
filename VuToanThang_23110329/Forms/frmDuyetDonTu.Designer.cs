namespace VuToanThang_23110329.Forms
{
    partial class frmDuyetDonTu
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
            this.pnlMain = new System.Windows.Forms.Panel();
            this.dgvDonTu = new Guna.UI2.WinForms.Guna2DataGridView();
            this.pnlButtons = new System.Windows.Forms.Panel();
            this.btnLamMoi = new Guna.UI2.WinForms.Guna2Button();
            this.btnTuChoi = new Guna.UI2.WinForms.Guna2Button();
            this.btnDuyet = new Guna.UI2.WinForms.Guna2Button();
            this.pnlSearch = new System.Windows.Forms.Panel();
            this.cmbLoaiDon = new Guna.UI2.WinForms.Guna2ComboBox();
            this.lblLoaiDon = new System.Windows.Forms.Label();
            this.cmbTrangThai = new Guna.UI2.WinForms.Guna2ComboBox();
            this.lblTrangThai = new System.Windows.Forms.Label();
            this.txtTimKiem = new Guna.UI2.WinForms.Guna2TextBox();
            this.lblTimKiem = new System.Windows.Forms.Label();
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.pnlMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDonTu)).BeginInit();
            this.pnlButtons.SuspendLayout();
            this.pnlSearch.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlMain
            // 
            this.pnlMain.BackColor = System.Drawing.Color.White;
            this.pnlMain.Controls.Add(this.dgvDonTu);
            this.pnlMain.Controls.Add(this.pnlButtons);
            this.pnlMain.Controls.Add(this.pnlSearch);
            this.pnlMain.Controls.Add(this.pnlHeader);
            this.pnlMain.Controls.Add(this.lblTitle);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 0);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Padding = new System.Windows.Forms.Padding(20);
            this.pnlMain.Size = new System.Drawing.Size(1200, 700);
            this.pnlMain.TabIndex = 0;
            // 
            // dgvDonTu
            // 
            this.dgvDonTu.AllowUserToAddRows = false;
            this.dgvDonTu.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(250)))));
            this.dgvDonTu.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvDonTu.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvDonTu.ColumnHeadersHeight = 40;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 9F);
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvDonTu.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgvDonTu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvDonTu.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.dgvDonTu.Location = new System.Drawing.Point(20, 160);
            this.dgvDonTu.MultiSelect = false;
            this.dgvDonTu.Name = "dgvDonTu";
            this.dgvDonTu.ReadOnly = true;
            this.dgvDonTu.RowHeadersVisible = false;
            this.dgvDonTu.Size = new System.Drawing.Size(1160, 520);
            this.dgvDonTu.TabIndex = 4;
            this.dgvDonTu.ThemeStyle.AlternatingRowsStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(250)))));
            this.dgvDonTu.ThemeStyle.AlternatingRowsStyle.Font = null;
            this.dgvDonTu.ThemeStyle.AlternatingRowsStyle.ForeColor = System.Drawing.Color.Empty;
            this.dgvDonTu.ThemeStyle.AlternatingRowsStyle.SelectionBackColor = System.Drawing.Color.Empty;
            this.dgvDonTu.ThemeStyle.AlternatingRowsStyle.SelectionForeColor = System.Drawing.Color.Empty;
            this.dgvDonTu.ThemeStyle.BackColor = System.Drawing.Color.White;
            this.dgvDonTu.ThemeStyle.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.dgvDonTu.ThemeStyle.HeaderStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.dgvDonTu.ThemeStyle.HeaderStyle.BorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgvDonTu.ThemeStyle.HeaderStyle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.dgvDonTu.ThemeStyle.HeaderStyle.ForeColor = System.Drawing.Color.White;
            this.dgvDonTu.ThemeStyle.HeaderStyle.HeaightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvDonTu.ThemeStyle.HeaderStyle.Height = 40;
            this.dgvDonTu.ThemeStyle.ReadOnly = true;
            this.dgvDonTu.ThemeStyle.RowsStyle.BackColor = System.Drawing.Color.White;
            this.dgvDonTu.ThemeStyle.RowsStyle.BorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvDonTu.ThemeStyle.RowsStyle.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.dgvDonTu.ThemeStyle.RowsStyle.ForeColor = System.Drawing.Color.Black;
            this.dgvDonTu.ThemeStyle.RowsStyle.Height = 22;
            this.dgvDonTu.ThemeStyle.RowsStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.dgvDonTu.ThemeStyle.RowsStyle.SelectionForeColor = System.Drawing.Color.Black;
            this.dgvDonTu.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDonTu_CellContentClick);
            this.dgvDonTu.SelectionChanged += new System.EventHandler(this.dgvDonTu_SelectionChanged);
            // 
            // pnlButtons
            // 
            this.pnlButtons.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(250)))));
            this.pnlButtons.Controls.Add(this.btnLamMoi);
            this.pnlButtons.Controls.Add(this.btnTuChoi);
            this.pnlButtons.Controls.Add(this.btnDuyet);
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
            this.btnLamMoi.Location = new System.Drawing.Point(240, 10);
            this.btnLamMoi.Name = "btnLamMoi";
            this.btnLamMoi.Size = new System.Drawing.Size(100, 30);
            this.btnLamMoi.TabIndex = 2;
            this.btnLamMoi.Text = "Làm mới";
            this.btnLamMoi.Click += new System.EventHandler(this.btnLamMoi_Click);
            // 
            // btnTuChoi
            // 
            this.btnTuChoi.BorderRadius = 12;
            this.btnTuChoi.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(53)))), ((int)(((byte)(69)))));
            this.btnTuChoi.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnTuChoi.ForeColor = System.Drawing.Color.White;
            this.btnTuChoi.Location = new System.Drawing.Point(130, 10);
            this.btnTuChoi.Name = "btnTuChoi";
            this.btnTuChoi.Size = new System.Drawing.Size(100, 30);
            this.btnTuChoi.TabIndex = 1;
            this.btnTuChoi.Text = "Từ chối";
            this.btnTuChoi.Click += new System.EventHandler(this.btnTuChoi_Click);
            // 
            // btnDuyet
            // 
            this.btnDuyet.BorderRadius = 12;
            this.btnDuyet.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(167)))), ((int)(((byte)(69)))));
            this.btnDuyet.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnDuyet.ForeColor = System.Drawing.Color.White;
            this.btnDuyet.Location = new System.Drawing.Point(20, 10);
            this.btnDuyet.Name = "btnDuyet";
            this.btnDuyet.Size = new System.Drawing.Size(100, 30);
            this.btnDuyet.TabIndex = 0;
            this.btnDuyet.Text = "Duyệt";
            this.btnDuyet.Click += new System.EventHandler(this.btnDuyet_Click);
            // 
            // pnlSearch
            // 
            this.pnlSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(250)))));
            this.pnlSearch.Controls.Add(this.cmbLoaiDon);
            this.pnlSearch.Controls.Add(this.lblLoaiDon);
            this.pnlSearch.Controls.Add(this.cmbTrangThai);
            this.pnlSearch.Controls.Add(this.lblTrangThai);
            this.pnlSearch.Controls.Add(this.txtTimKiem);
            this.pnlSearch.Controls.Add(this.lblTimKiem);
            this.pnlSearch.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlSearch.Location = new System.Drawing.Point(20, 50);
            this.pnlSearch.Name = "pnlSearch";
            this.pnlSearch.Size = new System.Drawing.Size(1160, 60);
            this.pnlSearch.TabIndex = 2;
            // 
            // cmbLoaiDon
            // 
            this.cmbLoaiDon.BackColor = System.Drawing.Color.Transparent;
            this.cmbLoaiDon.BorderRadius = 8;
            this.cmbLoaiDon.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbLoaiDon.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLoaiDon.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cmbLoaiDon.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cmbLoaiDon.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cmbLoaiDon.ForeColor = System.Drawing.Color.Black;
            this.cmbLoaiDon.ItemHeight = 30;
            this.cmbLoaiDon.Location = new System.Drawing.Point(630, 15);
            this.cmbLoaiDon.Name = "cmbLoaiDon";
            this.cmbLoaiDon.Size = new System.Drawing.Size(120, 36);
            this.cmbLoaiDon.TabIndex = 5;
            this.cmbLoaiDon.SelectedIndexChanged += new System.EventHandler(this.cmbLoaiDon_SelectedIndexChanged);
            // 
            // lblLoaiDon
            // 
            this.lblLoaiDon.AutoSize = true;
            this.lblLoaiDon.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblLoaiDon.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblLoaiDon.Location = new System.Drawing.Point(560, 20);
            this.lblLoaiDon.Name = "lblLoaiDon";
            this.lblLoaiDon.Size = new System.Drawing.Size(65, 19);
            this.lblLoaiDon.TabIndex = 4;
            this.lblLoaiDon.Text = "Loại đơn:";
            // 
            // cmbTrangThai
            // 
            this.cmbTrangThai.BackColor = System.Drawing.Color.Transparent;
            this.cmbTrangThai.BorderRadius = 8;
            this.cmbTrangThai.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbTrangThai.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTrangThai.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cmbTrangThai.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cmbTrangThai.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cmbTrangThai.ForeColor = System.Drawing.Color.Black;
            this.cmbTrangThai.ItemHeight = 30;
            this.cmbTrangThai.Location = new System.Drawing.Point(400, 15);
            this.cmbTrangThai.Name = "cmbTrangThai";
            this.cmbTrangThai.Size = new System.Drawing.Size(140, 36);
            this.cmbTrangThai.TabIndex = 3;
            this.cmbTrangThai.SelectedIndexChanged += new System.EventHandler(this.cmbTrangThai_SelectedIndexChanged);
            // 
            // lblTrangThai
            // 
            this.lblTrangThai.AutoSize = true;
            this.lblTrangThai.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblTrangThai.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblTrangThai.Location = new System.Drawing.Point(320, 20);
            this.lblTrangThai.Name = "lblTrangThai";
            this.lblTrangThai.Size = new System.Drawing.Size(73, 19);
            this.lblTrangThai.TabIndex = 2;
            this.lblTrangThai.Text = "Trạng thái:";
            // 
            // txtTimKiem
            // 
            this.txtTimKiem.BorderRadius = 8;
            this.txtTimKiem.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtTimKiem.DefaultText = "";
            this.txtTimKiem.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtTimKiem.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtTimKiem.ForeColor = System.Drawing.Color.Black;
            this.txtTimKiem.Location = new System.Drawing.Point(100, 15);
            this.txtTimKiem.Name = "txtTimKiem";
            this.txtTimKiem.PlaceholderText = "Nhập tên nhân viên...";
            this.txtTimKiem.SelectedText = "";
            this.txtTimKiem.Size = new System.Drawing.Size(200, 30);
            this.txtTimKiem.TabIndex = 1;
            this.txtTimKiem.TextChanged += new System.EventHandler(this.txtTimKiem_TextChanged);
            // 
            // lblTimKiem
            // 
            this.lblTimKiem.AutoSize = true;
            this.lblTimKiem.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblTimKiem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblTimKiem.Location = new System.Drawing.Point(20, 20);
            this.lblTimKiem.Name = "lblTimKiem";
            this.lblTimKiem.Size = new System.Drawing.Size(67, 19);
            this.lblTimKiem.TabIndex = 0;
            this.lblTimKiem.Text = "Tìm kiếm:";
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
            this.lblTitle.Size = new System.Drawing.Size(154, 30);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Duyệt Đơn từ";
            // 
            // frmDuyetDonTu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1200, 700);
            this.Controls.Add(this.pnlMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmDuyetDonTu";
            this.Text = "Duyệt Đơn từ";
            this.Load += new System.EventHandler(this.frmDuyetDonTu_Load);
            this.pnlMain.ResumeLayout(false);
            this.pnlMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDonTu)).EndInit();
            this.pnlButtons.ResumeLayout(false);
            this.pnlSearch.ResumeLayout(false);
            this.pnlSearch.PerformLayout();
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Panel pnlSearch;
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
        private Guna.UI2.WinForms.Guna2DataGridView dgvDonTu;
    }
}
