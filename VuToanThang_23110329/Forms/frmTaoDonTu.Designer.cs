namespace VuToanThang_23110329.Forms
{
    partial class frmTaoDonTu
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
            this.pnlContent = new System.Windows.Forms.Panel();
            this.pnlForm = new System.Windows.Forms.Panel();
            this.lblLoaiDon = new System.Windows.Forms.Label();
            this.cmbLoaiDon = new Guna.UI2.WinForms.Guna2ComboBox();
            this.lblTuLuc = new System.Windows.Forms.Label();
            this.dtpTuLuc = new Guna.UI2.WinForms.Guna2DateTimePicker();
            this.lblDenLuc = new System.Windows.Forms.Label();
            this.dtpDenLuc = new Guna.UI2.WinForms.Guna2DateTimePicker();
            this.lblSoGio = new System.Windows.Forms.Label();
            this.txtSoGio = new Guna.UI2.WinForms.Guna2TextBox();
            this.lblLyDo = new System.Windows.Forms.Label();
            this.txtLyDo = new Guna.UI2.WinForms.Guna2TextBox();
            this.pnlButtons = new System.Windows.Forms.Panel();
            this.btnTaoDon = new Guna.UI2.WinForms.Guna2Button();
            this.btnHuy = new Guna.UI2.WinForms.Guna2Button();
            this.btnLamMoi = new Guna.UI2.WinForms.Guna2Button();
            this.pnlMain.SuspendLayout();
            this.pnlContent.SuspendLayout();
            this.pnlForm.SuspendLayout();
            this.pnlButtons.SuspendLayout();
            this.SuspendLayout();
            
            // pnlMain
            this.pnlMain.BackColor = System.Drawing.Color.White;
            this.pnlMain.Controls.Add(this.pnlButtons);
            this.pnlMain.Controls.Add(this.pnlContent);
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
            this.lblTitle.Size = new System.Drawing.Size(140, 30);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Tạo Đơn từ";
            
            // pnlHeader
            this.pnlHeader.BackColor = System.Drawing.Color.FromArgb(94, 148, 255);
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(1200, 5);
            this.pnlHeader.TabIndex = 1;
            
            // pnlContent
            this.pnlContent.BackColor = System.Drawing.Color.FromArgb(248, 249, 250);
            this.pnlContent.Controls.Add(this.pnlForm);
            this.pnlContent.Location = new System.Drawing.Point(20, 60);
            this.pnlContent.Name = "pnlContent";
            this.pnlContent.Size = new System.Drawing.Size(1160, 580);
            this.pnlContent.TabIndex = 2;
            
            // pnlForm
            this.pnlForm.BackColor = System.Drawing.Color.White;
            this.pnlForm.Controls.Add(this.txtLyDo);
            this.pnlForm.Controls.Add(this.lblLyDo);
            this.pnlForm.Controls.Add(this.txtSoGio);
            this.pnlForm.Controls.Add(this.lblSoGio);
            this.pnlForm.Controls.Add(this.dtpDenLuc);
            this.pnlForm.Controls.Add(this.lblDenLuc);
            this.pnlForm.Controls.Add(this.dtpTuLuc);
            this.pnlForm.Controls.Add(this.lblTuLuc);
            this.pnlForm.Controls.Add(this.cmbLoaiDon);
            this.pnlForm.Controls.Add(this.lblLoaiDon);
            this.pnlForm.Location = new System.Drawing.Point(20, 20);
            this.pnlForm.Name = "pnlForm";
            this.pnlForm.Size = new System.Drawing.Size(1120, 540);
            this.pnlForm.TabIndex = 0;
            
            // lblLoaiDon
            this.lblLoaiDon.AutoSize = true;
            this.lblLoaiDon.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblLoaiDon.ForeColor = System.Drawing.Color.FromArgb(64, 64, 64);
            this.lblLoaiDon.Location = new System.Drawing.Point(20, 20);
            this.lblLoaiDon.Name = "lblLoaiDon";
            this.lblLoaiDon.Size = new System.Drawing.Size(80, 21);
            this.lblLoaiDon.TabIndex = 0;
            this.lblLoaiDon.Text = "Loại đơn:";
            
            // cmbLoaiDon
            this.cmbLoaiDon.BackColor = System.Drawing.Color.Transparent;
            this.cmbLoaiDon.BorderRadius = 8;
            this.cmbLoaiDon.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbLoaiDon.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLoaiDon.FillColor = System.Drawing.Color.White;
            this.cmbLoaiDon.FocusedColor = System.Drawing.Color.FromArgb(94, 148, 255);
            this.cmbLoaiDon.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.cmbLoaiDon.ForeColor = System.Drawing.Color.Black;
            this.cmbLoaiDon.ItemHeight = 35;
            this.cmbLoaiDon.Location = new System.Drawing.Point(120, 15);
            this.cmbLoaiDon.Name = "cmbLoaiDon";
            this.cmbLoaiDon.Size = new System.Drawing.Size(200, 35);
            this.cmbLoaiDon.TabIndex = 1;
            this.cmbLoaiDon.SelectedIndexChanged += new System.EventHandler(this.cmbLoaiDon_SelectedIndexChanged);
            
            // lblTuLuc
            this.lblTuLuc.AutoSize = true;
            this.lblTuLuc.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblTuLuc.ForeColor = System.Drawing.Color.FromArgb(64, 64, 64);
            this.lblTuLuc.Location = new System.Drawing.Point(20, 80);
            this.lblTuLuc.Name = "lblTuLuc";
            this.lblTuLuc.Size = new System.Drawing.Size(60, 21);
            this.lblTuLuc.TabIndex = 2;
            this.lblTuLuc.Text = "Từ lúc:";
            
            // dtpTuLuc
            this.dtpTuLuc.BorderRadius = 8;
            this.dtpTuLuc.CheckedState.Parent = this.dtpTuLuc;
            this.dtpTuLuc.FillColor = System.Drawing.Color.White;
            this.dtpTuLuc.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.dtpTuLuc.ForeColor = System.Drawing.Color.Black;
            this.dtpTuLuc.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpTuLuc.Location = new System.Drawing.Point(120, 75);
            this.dtpTuLuc.Name = "dtpTuLuc";
            this.dtpTuLuc.Size = new System.Drawing.Size(200, 35);
            this.dtpTuLuc.TabIndex = 3;
            this.dtpTuLuc.ValueChanged += new System.EventHandler(this.dtpTuLuc_ValueChanged);
            
            // lblDenLuc
            this.lblDenLuc.AutoSize = true;
            this.lblDenLuc.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblDenLuc.ForeColor = System.Drawing.Color.FromArgb(64, 64, 64);
            this.lblDenLuc.Location = new System.Drawing.Point(20, 140);
            this.lblDenLuc.Name = "lblDenLuc";
            this.lblDenLuc.Size = new System.Drawing.Size(70, 21);
            this.lblDenLuc.TabIndex = 4;
            this.lblDenLuc.Text = "Đến lúc:";
            
            // dtpDenLuc
            this.dtpDenLuc.BorderRadius = 8;
            this.dtpDenLuc.CheckedState.Parent = this.dtpDenLuc;
            this.dtpDenLuc.FillColor = System.Drawing.Color.White;
            this.dtpDenLuc.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.dtpDenLuc.ForeColor = System.Drawing.Color.Black;
            this.dtpDenLuc.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDenLuc.Location = new System.Drawing.Point(120, 135);
            this.dtpDenLuc.Name = "dtpDenLuc";
            this.dtpDenLuc.Size = new System.Drawing.Size(200, 35);
            this.dtpDenLuc.TabIndex = 5;
            this.dtpDenLuc.ValueChanged += new System.EventHandler(this.dtpDenLuc_ValueChanged);
            
            // lblSoGio
            this.lblSoGio.AutoSize = true;
            this.lblSoGio.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblSoGio.ForeColor = System.Drawing.Color.FromArgb(64, 64, 64);
            this.lblSoGio.Location = new System.Drawing.Point(20, 200);
            this.lblSoGio.Name = "lblSoGio";
            this.lblSoGio.Size = new System.Drawing.Size(60, 21);
            this.lblSoGio.TabIndex = 6;
            this.lblSoGio.Text = "Số giờ:";
            
            // txtSoGio
            this.txtSoGio.BorderRadius = 8;
            this.txtSoGio.FillColor = System.Drawing.Color.White;
            this.txtSoGio.FocusedState.BorderColor = System.Drawing.Color.FromArgb(94, 148, 255);
            this.txtSoGio.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtSoGio.ForeColor = System.Drawing.Color.Black;
            this.txtSoGio.Location = new System.Drawing.Point(120, 195);
            this.txtSoGio.Name = "txtSoGio";
            this.txtSoGio.PlaceholderText = "Nhập số giờ (chỉ cho tăng ca)";
            this.txtSoGio.Size = new System.Drawing.Size(200, 35);
            this.txtSoGio.TabIndex = 7;
            this.txtSoGio.TextChanged += new System.EventHandler(this.txtSoGio_TextChanged);
            
            // lblLyDo
            this.lblLyDo.AutoSize = true;
            this.lblLyDo.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblLyDo.ForeColor = System.Drawing.Color.FromArgb(64, 64, 64);
            this.lblLyDo.Location = new System.Drawing.Point(20, 260);
            this.lblLyDo.Name = "lblLyDo";
            this.lblLyDo.Size = new System.Drawing.Size(55, 21);
            this.lblLyDo.TabIndex = 8;
            this.lblLyDo.Text = "Lý do:";
            
            // txtLyDo
            this.txtLyDo.BorderRadius = 8;
            this.txtLyDo.FillColor = System.Drawing.Color.White;
            this.txtLyDo.FocusedState.BorderColor = System.Drawing.Color.FromArgb(94, 148, 255);
            this.txtLyDo.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtLyDo.ForeColor = System.Drawing.Color.Black;
            this.txtLyDo.Location = new System.Drawing.Point(120, 255);
            this.txtLyDo.Multiline = true;
            this.txtLyDo.Name = "txtLyDo";
            this.txtLyDo.PlaceholderText = "Nhập lý do nghỉ phép hoặc tăng ca...";
            this.txtLyDo.Size = new System.Drawing.Size(400, 100);
            this.txtLyDo.TabIndex = 9;
            this.txtLyDo.TextChanged += new System.EventHandler(this.txtLyDo_TextChanged);
            
            // pnlButtons
            this.pnlButtons.BackColor = System.Drawing.Color.FromArgb(248, 249, 250);
            this.pnlButtons.Controls.Add(this.btnLamMoi);
            this.pnlButtons.Controls.Add(this.btnHuy);
            this.pnlButtons.Controls.Add(this.btnTaoDon);
            this.pnlButtons.Location = new System.Drawing.Point(20, 650);
            this.pnlButtons.Name = "pnlButtons";
            this.pnlButtons.Size = new System.Drawing.Size(1160, 50);
            this.pnlButtons.TabIndex = 3;
            
            // btnTaoDon
            this.btnTaoDon.BorderRadius = 12;
            this.btnTaoDon.FillColor = System.Drawing.Color.FromArgb(40, 167, 69);
            this.btnTaoDon.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnTaoDon.ForeColor = System.Drawing.Color.White;
            this.btnTaoDon.Location = new System.Drawing.Point(20, 10);
            this.btnTaoDon.Name = "btnTaoDon";
            this.btnTaoDon.Size = new System.Drawing.Size(100, 30);
            this.btnTaoDon.TabIndex = 0;
            this.btnTaoDon.Text = "Tạo đơn";
            this.btnTaoDon.Click += new System.EventHandler(this.btnTaoDon_Click);
            
            // btnHuy
            this.btnHuy.BorderRadius = 12;
            this.btnHuy.FillColor = System.Drawing.Color.FromArgb(220, 53, 69);
            this.btnHuy.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnHuy.ForeColor = System.Drawing.Color.White;
            this.btnHuy.Location = new System.Drawing.Point(130, 10);
            this.btnHuy.Name = "btnHuy";
            this.btnHuy.Size = new System.Drawing.Size(100, 30);
            this.btnHuy.TabIndex = 1;
            this.btnHuy.Text = "Hủy";
            this.btnHuy.Click += new System.EventHandler(this.btnHuy_Click);
            
            // btnLamMoi
            this.btnLamMoi.BorderRadius = 12;
            this.btnLamMoi.FillColor = System.Drawing.Color.FromArgb(108, 117, 125);
            this.btnLamMoi.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnLamMoi.ForeColor = System.Drawing.Color.White;
            this.btnLamMoi.Location = new System.Drawing.Point(240, 10);
            this.btnLamMoi.Name = "btnLamMoi";
            this.btnLamMoi.Size = new System.Drawing.Size(100, 30);
            this.btnLamMoi.TabIndex = 2;
            this.btnLamMoi.Text = "Làm mới";
            this.btnLamMoi.Click += new System.EventHandler(this.btnLamMoi_Click);
            
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1200, 700);
            this.Controls.Add(this.pnlMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmTaoDonTu";
            this.Text = "Tạo Đơn từ";
            
            this.pnlMain.ResumeLayout(false);
            this.pnlMain.PerformLayout();
            this.pnlContent.ResumeLayout(false);
            this.pnlForm.ResumeLayout(false);
            this.pnlForm.PerformLayout();
            this.pnlButtons.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Panel pnlContent;
        private System.Windows.Forms.Panel pnlForm;
        private System.Windows.Forms.Label lblLoaiDon;
        private Guna.UI2.WinForms.Guna2ComboBox cmbLoaiDon;
        private System.Windows.Forms.Label lblTuLuc;
        private Guna.UI2.WinForms.Guna2DateTimePicker dtpTuLuc;
        private System.Windows.Forms.Label lblDenLuc;
        private Guna.UI2.WinForms.Guna2DateTimePicker dtpDenLuc;
        private System.Windows.Forms.Label lblSoGio;
        private Guna.UI2.WinForms.Guna2TextBox txtSoGio;
        private System.Windows.Forms.Label lblLyDo;
        private Guna.UI2.WinForms.Guna2TextBox txtLyDo;
        private System.Windows.Forms.Panel pnlButtons;
        private Guna.UI2.WinForms.Guna2Button btnTaoDon;
        private Guna.UI2.WinForms.Guna2Button btnHuy;
        private Guna.UI2.WinForms.Guna2Button btnLamMoi;
    }
}
