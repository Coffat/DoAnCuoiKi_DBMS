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
            this.pnlContent = new System.Windows.Forms.Panel();
            this.pnlForm = new System.Windows.Forms.Panel();
            this.txtLyDo = new Guna.UI2.WinForms.Guna2TextBox();
            this.lblLyDo = new System.Windows.Forms.Label();
            this.txtSoGio = new Guna.UI2.WinForms.Guna2TextBox();
            this.lblSoGio = new System.Windows.Forms.Label();
            this.dtpDenLuc = new Guna.UI2.WinForms.Guna2DateTimePicker();
            this.lblDenLuc = new System.Windows.Forms.Label();
            this.dtpTuLuc = new Guna.UI2.WinForms.Guna2DateTimePicker();
            this.lblTuLuc = new System.Windows.Forms.Label();
            this.cmbLoaiDon = new Guna.UI2.WinForms.Guna2ComboBox();
            this.lblLoaiDon = new System.Windows.Forms.Label();
            this.pnlButtons = new System.Windows.Forms.Panel();
            this.btnLamMoi = new Guna.UI2.WinForms.Guna2Button();
            this.btnHuy = new Guna.UI2.WinForms.Guna2Button();
            this.btnTaoDon = new Guna.UI2.WinForms.Guna2Button();
            this.lblTitle = new System.Windows.Forms.Label();
            this.pnlMain.SuspendLayout();
            this.pnlContent.SuspendLayout();
            this.pnlForm.SuspendLayout();
            this.pnlButtons.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlMain
            // 
            this.pnlMain.BackColor = System.Drawing.Color.White;
            this.pnlMain.Controls.Add(this.pnlContent);
            this.pnlMain.Controls.Add(this.pnlButtons);
            this.pnlMain.Controls.Add(this.lblTitle);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 0);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Padding = new System.Windows.Forms.Padding(20);
            this.pnlMain.Size = new System.Drawing.Size(1200, 700);
            this.pnlMain.TabIndex = 0;
            // 
            // pnlContent
            // 
            this.pnlContent.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(250)))));
            this.pnlContent.Controls.Add(this.pnlForm);
            this.pnlContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlContent.Location = new System.Drawing.Point(20, 60);
            this.pnlContent.Name = "pnlContent";
            this.pnlContent.Padding = new System.Windows.Forms.Padding(20);
            this.pnlContent.Size = new System.Drawing.Size(1160, 570);
            this.pnlContent.TabIndex = 1;
            // 
            // pnlForm
            // 
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
            this.pnlForm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlForm.Location = new System.Drawing.Point(20, 20);
            this.pnlForm.Name = "pnlForm";
            this.pnlForm.Padding = new System.Windows.Forms.Padding(30);
            this.pnlForm.Size = new System.Drawing.Size(1120, 530);
            this.pnlForm.TabIndex = 0;
            // 
            // txtLyDo
            // 
            this.txtLyDo.BorderRadius = 8;
            this.txtLyDo.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtLyDo.DefaultText = "";
            this.txtLyDo.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtLyDo.ForeColor = System.Drawing.Color.Black;
            this.txtLyDo.Location = new System.Drawing.Point(180, 275);
            this.txtLyDo.Multiline = true;
            this.txtLyDo.Name = "txtLyDo";
            this.txtLyDo.PlaceholderText = "Nhập lý do nghỉ phép hoặc làm thêm giờ";
            this.txtLyDo.SelectedText = "";
            this.txtLyDo.Size = new System.Drawing.Size(500, 80);
            this.txtLyDo.TabIndex = 9;
            // 
            // lblLyDo
            // 
            this.lblLyDo.AutoSize = true;
            this.lblLyDo.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblLyDo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblLyDo.Location = new System.Drawing.Point(30, 280);
            this.lblLyDo.Name = "lblLyDo";
            this.lblLyDo.Size = new System.Drawing.Size(50, 19);
            this.lblLyDo.TabIndex = 8;
            this.lblLyDo.Text = "Lý do:";
            // 
            // txtSoGio
            // 
            this.txtSoGio.BorderRadius = 8;
            this.txtSoGio.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtSoGio.DefaultText = "";
            this.txtSoGio.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtSoGio.ForeColor = System.Drawing.Color.Black;
            this.txtSoGio.Location = new System.Drawing.Point(180, 215);
            this.txtSoGio.Name = "txtSoGio";
            this.txtSoGio.PlaceholderText = "Tự động tính";
            this.txtSoGio.ReadOnly = true;
            this.txtSoGio.SelectedText = "";
            this.txtSoGio.Size = new System.Drawing.Size(150, 36);
            this.txtSoGio.TabIndex = 7;
            // 
            // lblSoGio
            // 
            this.lblSoGio.AutoSize = true;
            this.lblSoGio.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblSoGio.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblSoGio.Location = new System.Drawing.Point(30, 220);
            this.lblSoGio.Name = "lblSoGio";
            this.lblSoGio.Size = new System.Drawing.Size(56, 19);
            this.lblSoGio.TabIndex = 6;
            this.lblSoGio.Text = "Số giờ:";
            // 
            // dtpDenLuc
            // 
            this.dtpDenLuc.BorderRadius = 8;
            this.dtpDenLuc.Checked = true;
            this.dtpDenLuc.FillColor = System.Drawing.Color.White;
            this.dtpDenLuc.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.dtpDenLuc.ForeColor = System.Drawing.Color.Black;
            this.dtpDenLuc.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDenLuc.Location = new System.Drawing.Point(180, 155);
            this.dtpDenLuc.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.dtpDenLuc.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtpDenLuc.Name = "dtpDenLuc";
            this.dtpDenLuc.Size = new System.Drawing.Size(300, 36);
            this.dtpDenLuc.TabIndex = 5;
            this.dtpDenLuc.Value = new System.DateTime(2025, 9, 30, 0, 0, 0, 0);
            this.dtpDenLuc.ValueChanged += new System.EventHandler(this.dtpDenLuc_ValueChanged);
            // 
            // lblDenLuc
            // 
            this.lblDenLuc.AutoSize = true;
            this.lblDenLuc.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblDenLuc.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblDenLuc.Location = new System.Drawing.Point(30, 160);
            this.lblDenLuc.Name = "lblDenLuc";
            this.lblDenLuc.Size = new System.Drawing.Size(62, 19);
            this.lblDenLuc.TabIndex = 4;
            this.lblDenLuc.Text = "Đến lúc:";
            // 
            // dtpTuLuc
            // 
            this.dtpTuLuc.BorderRadius = 8;
            this.dtpTuLuc.Checked = true;
            this.dtpTuLuc.FillColor = System.Drawing.Color.White;
            this.dtpTuLuc.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.dtpTuLuc.ForeColor = System.Drawing.Color.Black;
            this.dtpTuLuc.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpTuLuc.Location = new System.Drawing.Point(180, 95);
            this.dtpTuLuc.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.dtpTuLuc.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtpTuLuc.Name = "dtpTuLuc";
            this.dtpTuLuc.Size = new System.Drawing.Size(300, 36);
            this.dtpTuLuc.TabIndex = 3;
            this.dtpTuLuc.Value = new System.DateTime(2025, 9, 30, 0, 0, 0, 0);
            this.dtpTuLuc.ValueChanged += new System.EventHandler(this.dtpTuLuc_ValueChanged);
            // 
            // lblTuLuc
            // 
            this.lblTuLuc.AutoSize = true;
            this.lblTuLuc.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblTuLuc.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblTuLuc.Location = new System.Drawing.Point(30, 100);
            this.lblTuLuc.Name = "lblTuLuc";
            this.lblTuLuc.Size = new System.Drawing.Size(53, 19);
            this.lblTuLuc.TabIndex = 2;
            this.lblTuLuc.Text = "Từ lúc:";
            // 
            // cmbLoaiDon
            // 
            this.cmbLoaiDon.BackColor = System.Drawing.Color.Transparent;
            this.cmbLoaiDon.BorderRadius = 8;
            this.cmbLoaiDon.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbLoaiDon.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLoaiDon.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cmbLoaiDon.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cmbLoaiDon.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbLoaiDon.ForeColor = System.Drawing.Color.Black;
            this.cmbLoaiDon.ItemHeight = 30;
            this.cmbLoaiDon.Location = new System.Drawing.Point(180, 35);
            this.cmbLoaiDon.Name = "cmbLoaiDon";
            this.cmbLoaiDon.Size = new System.Drawing.Size(300, 36);
            this.cmbLoaiDon.TabIndex = 1;
            this.cmbLoaiDon.SelectedIndexChanged += new System.EventHandler(this.cmbLoaiDon_SelectedIndexChanged);
            // 
            // lblLoaiDon
            // 
            this.lblLoaiDon.AutoSize = true;
            this.lblLoaiDon.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblLoaiDon.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblLoaiDon.Location = new System.Drawing.Point(30, 40);
            this.lblLoaiDon.Name = "lblLoaiDon";
            this.lblLoaiDon.Size = new System.Drawing.Size(71, 19);
            this.lblLoaiDon.TabIndex = 0;
            this.lblLoaiDon.Text = "Loại đơn:";
            // 
            // pnlButtons
            // 
            this.pnlButtons.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(250)))));
            this.pnlButtons.Controls.Add(this.btnLamMoi);
            this.pnlButtons.Controls.Add(this.btnHuy);
            this.pnlButtons.Controls.Add(this.btnTaoDon);
            this.pnlButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlButtons.Location = new System.Drawing.Point(20, 630);
            this.pnlButtons.Name = "pnlButtons";
            this.pnlButtons.Size = new System.Drawing.Size(1160, 50);
            this.pnlButtons.TabIndex = 2;
            // 
            // btnLamMoi
            // 
            this.btnLamMoi.BorderRadius = 12;
            this.btnLamMoi.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(117)))), ((int)(((byte)(125)))));
            this.btnLamMoi.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnLamMoi.ForeColor = System.Drawing.Color.White;
            this.btnLamMoi.Location = new System.Drawing.Point(260, 10);
            this.btnLamMoi.Name = "btnLamMoi";
            this.btnLamMoi.Size = new System.Drawing.Size(100, 30);
            this.btnLamMoi.TabIndex = 2;
            this.btnLamMoi.Text = "Làm mới";
            this.btnLamMoi.Click += new System.EventHandler(this.btnLamMoi_Click);
            // 
            // btnHuy
            // 
            this.btnHuy.BorderRadius = 12;
            this.btnHuy.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(53)))), ((int)(((byte)(69)))));
            this.btnHuy.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnHuy.ForeColor = System.Drawing.Color.White;
            this.btnHuy.Location = new System.Drawing.Point(150, 10);
            this.btnHuy.Name = "btnHuy";
            this.btnHuy.Size = new System.Drawing.Size(100, 30);
            this.btnHuy.TabIndex = 1;
            this.btnHuy.Text = "Hủy";
            this.btnHuy.Click += new System.EventHandler(this.btnHuy_Click);
            // 
            // btnTaoDon
            // 
            this.btnTaoDon.BorderRadius = 12;
            this.btnTaoDon.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(167)))), ((int)(((byte)(69)))));
            this.btnTaoDon.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnTaoDon.ForeColor = System.Drawing.Color.White;
            this.btnTaoDon.Location = new System.Drawing.Point(20, 10);
            this.btnTaoDon.Name = "btnTaoDon";
            this.btnTaoDon.Size = new System.Drawing.Size(120, 30);
            this.btnTaoDon.TabIndex = 0;
            this.btnTaoDon.Text = "Tạo đơn";
            this.btnTaoDon.Click += new System.EventHandler(this.btnTaoDon_Click);
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.lblTitle.Location = new System.Drawing.Point(20, 20);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Padding = new System.Windows.Forms.Padding(0, 0, 0, 10);
            this.lblTitle.Size = new System.Drawing.Size(130, 40);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Tạo Đơn từ";
            // 
            // frmTaoDonTu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1200, 700);
            this.Controls.Add(this.pnlMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmTaoDonTu";
            this.Text = "Tạo Đơn từ";
            this.Load += new System.EventHandler(this.frmTaoDonTu_Load);
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
