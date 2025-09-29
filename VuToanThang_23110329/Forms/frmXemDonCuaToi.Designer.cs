namespace VuToanThang_23110329.Forms
{
    partial class frmXemDonCuaToi
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
            this.pnlFilter = new System.Windows.Forms.Panel();
            this.lblTrangThai = new System.Windows.Forms.Label();
            this.cmbTrangThai = new Guna.UI2.WinForms.Guna2ComboBox();
            this.lblLoaiDon = new System.Windows.Forms.Label();
            this.cmbLoaiDon = new Guna.UI2.WinForms.Guna2ComboBox();
            this.btnLamMoi = new Guna.UI2.WinForms.Guna2Button();
            this.dgvDonTu = new Guna.UI2.WinForms.Guna2DataGridView();
            this.pnlMain.SuspendLayout();
            this.pnlFilter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDonTu)).BeginInit();
            this.SuspendLayout();
            
            // pnlMain
            this.pnlMain.BackColor = System.Drawing.Color.White;
            this.pnlMain.Controls.Add(this.dgvDonTu);
            this.pnlMain.Controls.Add(this.pnlFilter);
            this.pnlMain.Controls.Add(this.lblTitle);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 0);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Padding = new System.Windows.Forms.Padding(20, 20, 20, 20);
            this.pnlMain.Size = new System.Drawing.Size(1200, 700);
            this.pnlMain.TabIndex = 0;
            
            // lblTitle
            this.lblTitle.AutoSize = true;
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(94, 148, 255);
            this.lblTitle.Location = new System.Drawing.Point(20, 20);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Padding = new System.Windows.Forms.Padding(0, 0, 0, 10);
            this.lblTitle.Size = new System.Drawing.Size(180, 40);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Đơn từ của tôi";
            
            // pnlFilter
            this.pnlFilter.BackColor = System.Drawing.Color.FromArgb(248, 249, 250);
            this.pnlFilter.Controls.Add(this.btnLamMoi);
            this.pnlFilter.Controls.Add(this.cmbLoaiDon);
            this.pnlFilter.Controls.Add(this.lblLoaiDon);
            this.pnlFilter.Controls.Add(this.cmbTrangThai);
            this.pnlFilter.Controls.Add(this.lblTrangThai);
            this.pnlFilter.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlFilter.Location = new System.Drawing.Point(20, 60);
            this.pnlFilter.Name = "pnlFilter";
            this.pnlFilter.Size = new System.Drawing.Size(1160, 60);
            this.pnlFilter.TabIndex = 1;
            
            // lblTrangThai
            this.lblTrangThai.AutoSize = true;
            this.lblTrangThai.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblTrangThai.ForeColor = System.Drawing.Color.FromArgb(64, 64, 64);
            this.lblTrangThai.Location = new System.Drawing.Point(20, 20);
            this.lblTrangThai.Name = "lblTrangThai";
            this.lblTrangThai.Size = new System.Drawing.Size(73, 19);
            this.lblTrangThai.TabIndex = 0;
            this.lblTrangThai.Text = "Trạng thái:";
            
            // cmbTrangThai
            this.cmbTrangThai.BackColor = System.Drawing.Color.Transparent;
            this.cmbTrangThai.BorderRadius = 8;
            this.cmbTrangThai.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbTrangThai.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTrangThai.FillColor = System.Drawing.Color.White;
            this.cmbTrangThai.FocusedColor = System.Drawing.Color.FromArgb(94, 148, 255);
            this.cmbTrangThai.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cmbTrangThai.ForeColor = System.Drawing.Color.Black;
            this.cmbTrangThai.ItemHeight = 30;
            this.cmbTrangThai.Location = new System.Drawing.Point(100, 15);
            this.cmbTrangThai.Name = "cmbTrangThai";
            this.cmbTrangThai.Size = new System.Drawing.Size(150, 36);
            this.cmbTrangThai.TabIndex = 1;
            this.cmbTrangThai.SelectedIndexChanged += new System.EventHandler(this.cmbTrangThai_SelectedIndexChanged);
            
            // lblLoaiDon
            this.lblLoaiDon.AutoSize = true;
            this.lblLoaiDon.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblLoaiDon.ForeColor = System.Drawing.Color.FromArgb(64, 64, 64);
            this.lblLoaiDon.Location = new System.Drawing.Point(270, 20);
            this.lblLoaiDon.Name = "lblLoaiDon";
            this.lblLoaiDon.Size = new System.Drawing.Size(64, 19);
            this.lblLoaiDon.TabIndex = 2;
            this.lblLoaiDon.Text = "Loại đơn:";
            
            // cmbLoaiDon
            this.cmbLoaiDon.BackColor = System.Drawing.Color.Transparent;
            this.cmbLoaiDon.BorderRadius = 8;
            this.cmbLoaiDon.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbLoaiDon.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLoaiDon.FillColor = System.Drawing.Color.White;
            this.cmbLoaiDon.FocusedColor = System.Drawing.Color.FromArgb(94, 148, 255);
            this.cmbLoaiDon.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cmbLoaiDon.ForeColor = System.Drawing.Color.Black;
            this.cmbLoaiDon.ItemHeight = 30;
            this.cmbLoaiDon.Location = new System.Drawing.Point(340, 15);
            this.cmbLoaiDon.Name = "cmbLoaiDon";
            this.cmbLoaiDon.Size = new System.Drawing.Size(150, 36);
            this.cmbLoaiDon.TabIndex = 3;
            this.cmbLoaiDon.SelectedIndexChanged += new System.EventHandler(this.cmbLoaiDon_SelectedIndexChanged);
            
            // btnLamMoi
            this.btnLamMoi.BorderRadius = 12;
            this.btnLamMoi.FillColor = System.Drawing.Color.FromArgb(108, 117, 125);
            this.btnLamMoi.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnLamMoi.ForeColor = System.Drawing.Color.White;
            this.btnLamMoi.Location = new System.Drawing.Point(510, 15);
            this.btnLamMoi.Name = "btnLamMoi";
            this.btnLamMoi.Size = new System.Drawing.Size(100, 30);
            this.btnLamMoi.TabIndex = 4;
            this.btnLamMoi.Text = "Làm mới";
            this.btnLamMoi.Click += new System.EventHandler(this.btnLamMoi_Click);
            
            // dgvDonTu
            this.dgvDonTu.AllowUserToAddRows = false;
            this.dgvDonTu.AllowUserToDeleteRows = false;
            this.dgvDonTu.BackgroundColor = System.Drawing.Color.White;
            this.dgvDonTu.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvDonTu.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(94, 148, 255);
            this.dgvDonTu.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.dgvDonTu.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.White;
            this.dgvDonTu.ColumnHeadersHeight = 40;
            this.dgvDonTu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvDonTu.Location = new System.Drawing.Point(20, 120);
            this.dgvDonTu.MultiSelect = false;
            this.dgvDonTu.Name = "dgvDonTu";
            this.dgvDonTu.ReadOnly = true;
            this.dgvDonTu.RowHeadersVisible = false;
            this.dgvDonTu.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDonTu.Size = new System.Drawing.Size(1160, 560);
            this.dgvDonTu.TabIndex = 2;
            
            // frmXemDonCuaToi
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1200, 700);
            this.Controls.Add(this.pnlMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmXemDonCuaToi";
            this.Text = "Đơn từ của tôi";
            this.Load += new System.EventHandler(this.frmXemDonCuaToi_Load);
            this.pnlMain.ResumeLayout(false);
            this.pnlMain.PerformLayout();
            this.pnlFilter.ResumeLayout(false);
            this.pnlFilter.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDonTu)).EndInit();
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Panel pnlFilter;
        private System.Windows.Forms.Label lblTrangThai;
        private Guna.UI2.WinForms.Guna2ComboBox cmbTrangThai;
        private System.Windows.Forms.Label lblLoaiDon;
        private Guna.UI2.WinForms.Guna2ComboBox cmbLoaiDon;
        private Guna.UI2.WinForms.Guna2Button btnLamMoi;
        private Guna.UI2.WinForms.Guna2DataGridView dgvDonTu;
    }
}
