namespace VuToanThang_23110329.Forms
{
    partial class frmBangLuong
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
            this.lblThang = new System.Windows.Forms.Label();
            this.cmbThang = new Guna.UI2.WinForms.Guna2ComboBox();
            this.lblNam = new System.Windows.Forms.Label();
            this.cmbNam = new Guna.UI2.WinForms.Guna2ComboBox();
            this.pnlButtons = new System.Windows.Forms.Panel();
            this.btnChayLuong = new Guna.UI2.WinForms.Guna2Button();
            this.btnDongLuong = new Guna.UI2.WinForms.Guna2Button();
            this.btnCapNhatPhuCap = new Guna.UI2.WinForms.Guna2Button();
            this.btnLamMoi = new Guna.UI2.WinForms.Guna2Button();
            this.pnlMain = new System.Windows.Forms.Panel();
            this.pnlTitle = new System.Windows.Forms.Panel();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabCongThang = new System.Windows.Forms.TabPage();
            this.tabBangLuong = new System.Windows.Forms.TabPage();
            this.pnlSummary = new System.Windows.Forms.Panel();
            this.lblTongNhanVien = new System.Windows.Forms.Label();
            this.lblTongLuong = new System.Windows.Forms.Label();
            this.lblTongPhuCap = new System.Windows.Forms.Label();
            this.lblTongKhauTru = new System.Windows.Forms.Label();
            this.pnlMain.SuspendLayout();
            this.pnlFilter.SuspendLayout();
            this.pnlButtons.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.tabCongThang.SuspendLayout();
            this.tabBangLuong.SuspendLayout();
            this.pnlSummary.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(124)))), ((int)(((byte)(77)))), ((int)(((byte)(255)))));
            this.lblTitle.Location = new System.Drawing.Point(20, 20);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(140, 30);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Bảng lương";
            // 
            // pnlFilter
            // 
            this.pnlFilter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.pnlFilter.Controls.Add(this.lblThang);
            this.pnlFilter.Controls.Add(this.cmbThang);
            this.pnlFilter.Controls.Add(this.lblNam);
            this.pnlFilter.Controls.Add(this.cmbNam);
            this.pnlFilter.Location = new System.Drawing.Point(20, 60);
            this.pnlFilter.Name = "pnlFilter";
            this.pnlFilter.Size = new System.Drawing.Size(1160, 60);
            this.pnlFilter.TabIndex = 1;
            // 
            // lblThang
            // 
            this.lblThang.AutoSize = true;
            this.lblThang.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblThang.ForeColor = System.Drawing.Color.White;
            this.lblThang.Location = new System.Drawing.Point(20, 20);
            this.lblThang.Name = "lblThang";
            this.lblThang.Size = new System.Drawing.Size(45, 19);
            this.lblThang.TabIndex = 0;
            this.lblThang.Text = "Tháng:";
            // 
            // cmbThang
            // 
            this.cmbThang.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.cmbThang.BorderRadius = 8;
            this.cmbThang.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbThang.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbThang.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.cmbThang.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(124)))), ((int)(((byte)(77)))), ((int)(((byte)(255)))));
            this.cmbThang.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(124)))), ((int)(((byte)(77)))), ((int)(((byte)(255)))));
            this.cmbThang.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbThang.ForeColor = System.Drawing.Color.White;
            this.cmbThang.ItemHeight = 30;
            this.cmbThang.Location = new System.Drawing.Point(75, 15);
            this.cmbThang.Name = "cmbThang";
            this.cmbThang.Size = new System.Drawing.Size(80, 30);
            this.cmbThang.TabIndex = 1;
            // 
            // lblNam
            // 
            this.lblNam.AutoSize = true;
            this.lblNam.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNam.ForeColor = System.Drawing.Color.White;
            this.lblNam.Location = new System.Drawing.Point(175, 20);
            this.lblNam.Name = "lblNam";
            this.lblNam.Size = new System.Drawing.Size(35, 19);
            this.lblNam.TabIndex = 2;
            this.lblNam.Text = "Năm:";
            // 
            // cmbNam
            // 
            this.cmbNam.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.cmbNam.BorderRadius = 8;
            this.cmbNam.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbNam.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbNam.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.cmbNam.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(124)))), ((int)(((byte)(77)))), ((int)(((byte)(255)))));
            this.cmbNam.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(124)))), ((int)(((byte)(77)))), ((int)(((byte)(255)))));
            this.cmbNam.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbNam.ForeColor = System.Drawing.Color.White;
            this.cmbNam.ItemHeight = 30;
            this.cmbNam.Location = new System.Drawing.Point(220, 15);
            this.cmbNam.Name = "cmbNam";
            this.cmbNam.Size = new System.Drawing.Size(80, 30);
            this.cmbNam.TabIndex = 3;
            // 
            // pnlButtons
            // 
            this.pnlButtons.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.pnlButtons.Controls.Add(this.btnChayLuong);
            this.pnlButtons.Controls.Add(this.btnDongLuong);
            this.pnlButtons.Controls.Add(this.btnCapNhatPhuCap);
            this.pnlButtons.Controls.Add(this.btnLamMoi);
            this.pnlButtons.Location = new System.Drawing.Point(20, 130);
            this.pnlButtons.Name = "pnlButtons";
            this.pnlButtons.Size = new System.Drawing.Size(1160, 50);
            this.pnlButtons.TabIndex = 2;
            // 
            // btnChayLuong
            // 
            this.btnChayLuong.BorderRadius = 8;
            this.btnChayLuong.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnChayLuong.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnChayLuong.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnChayLuong.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnChayLuong.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(175)))), ((int)(((byte)(80)))));
            this.btnChayLuong.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnChayLuong.ForeColor = System.Drawing.Color.White;
            this.btnChayLuong.Location = new System.Drawing.Point(20, 10);
            this.btnChayLuong.Name = "btnChayLuong";
            this.btnChayLuong.Size = new System.Drawing.Size(100, 30);
            this.btnChayLuong.TabIndex = 0;
            this.btnChayLuong.Text = "Chạy bảng lương";
            // 
            // btnDongLuong
            // 
            this.btnDongLuong.BorderRadius = 8;
            this.btnDongLuong.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnDongLuong.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnDongLuong.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnDongLuong.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnDongLuong.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(67)))), ((int)(((byte)(54)))));
            this.btnDongLuong.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDongLuong.ForeColor = System.Drawing.Color.White;
            this.btnDongLuong.Location = new System.Drawing.Point(130, 10);
            this.btnDongLuong.Name = "btnDongLuong";
            this.btnDongLuong.Size = new System.Drawing.Size(100, 30);
            this.btnDongLuong.TabIndex = 1;
            this.btnDongLuong.Text = "Đóng bảng lương";
            // 
            // btnCapNhatPhuCap
            // 
            this.btnCapNhatPhuCap.BorderRadius = 8;
            this.btnCapNhatPhuCap.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnCapNhatPhuCap.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnCapNhatPhuCap.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnCapNhatPhuCap.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnCapNhatPhuCap.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(193)))), ((int)(((byte)(7)))));
            this.btnCapNhatPhuCap.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCapNhatPhuCap.ForeColor = System.Drawing.Color.White;
            this.btnCapNhatPhuCap.Location = new System.Drawing.Point(240, 10);
            this.btnCapNhatPhuCap.Name = "btnCapNhatPhuCap";
            this.btnCapNhatPhuCap.Size = new System.Drawing.Size(120, 30);
            this.btnCapNhatPhuCap.TabIndex = 2;
            this.btnCapNhatPhuCap.Text = "Cập nhật phụ cấp";
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
            this.btnLamMoi.Location = new System.Drawing.Point(370, 10);
            this.btnLamMoi.Name = "btnLamMoi";
            this.btnLamMoi.Size = new System.Drawing.Size(80, 30);
            this.btnLamMoi.TabIndex = 3;
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
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabCongThang);
            this.tabControl.Controls.Add(this.tabBangLuong);
            this.tabControl.Location = new System.Drawing.Point(20, 190);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(1160, 490);
            this.tabControl.TabIndex = 4;
            // 
            // tabCongThang
            // 
            this.tabCongThang.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.tabCongThang.Location = new System.Drawing.Point(4, 22);
            this.tabCongThang.Name = "tabCongThang";
            this.tabCongThang.Padding = new System.Windows.Forms.Padding(3);
            this.tabCongThang.Size = new System.Drawing.Size(1152, 464);
            this.tabCongThang.TabIndex = 0;
            this.tabCongThang.Text = "Tổng hợp công tháng";
            // 
            // tabBangLuong
            // 
            this.tabBangLuong.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.tabBangLuong.Controls.Add(this.pnlSummary);
            this.tabBangLuong.Location = new System.Drawing.Point(4, 22);
            this.tabBangLuong.Name = "tabBangLuong";
            this.tabBangLuong.Padding = new System.Windows.Forms.Padding(3);
            this.tabBangLuong.Size = new System.Drawing.Size(1152, 464);
            this.tabBangLuong.TabIndex = 1;
            this.tabBangLuong.Text = "Bảng lương";
            // 
            // pnlSummary
            // 
            this.pnlSummary.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.pnlSummary.Controls.Add(this.lblTongNhanVien);
            this.pnlSummary.Controls.Add(this.lblTongLuong);
            this.pnlSummary.Controls.Add(this.lblTongPhuCap);
            this.pnlSummary.Controls.Add(this.lblTongKhauTru);
            this.pnlSummary.Location = new System.Drawing.Point(20, 20);
            this.pnlSummary.Name = "pnlSummary";
            this.pnlSummary.Size = new System.Drawing.Size(1112, 60);
            this.pnlSummary.TabIndex = 0;
            // 
            // lblTongNhanVien
            // 
            this.lblTongNhanVien.AutoSize = true;
            this.lblTongNhanVien.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTongNhanVien.ForeColor = System.Drawing.Color.White;
            this.lblTongNhanVien.Location = new System.Drawing.Point(20, 20);
            this.lblTongNhanVien.Name = "lblTongNhanVien";
            this.lblTongNhanVien.Size = new System.Drawing.Size(100, 19);
            this.lblTongNhanVien.TabIndex = 0;
            this.lblTongNhanVien.Text = "Tổng nhân viên: 0";
            // 
            // lblTongLuong
            // 
            this.lblTongLuong.AutoSize = true;
            this.lblTongLuong.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTongLuong.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(175)))), ((int)(((byte)(80)))));
            this.lblTongLuong.Location = new System.Drawing.Point(200, 20);
            this.lblTongLuong.Name = "lblTongLuong";
            this.lblTongLuong.Size = new System.Drawing.Size(80, 19);
            this.lblTongLuong.TabIndex = 1;
            this.lblTongLuong.Text = "Tổng lương: 0";
            // 
            // lblTongPhuCap
            // 
            this.lblTongPhuCap.AutoSize = true;
            this.lblTongPhuCap.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTongPhuCap.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(193)))), ((int)(((byte)(7)))));
            this.lblTongPhuCap.Location = new System.Drawing.Point(380, 20);
            this.lblTongPhuCap.Name = "lblTongPhuCap";
            this.lblTongPhuCap.Size = new System.Drawing.Size(80, 19);
            this.lblTongPhuCap.TabIndex = 2;
            this.lblTongPhuCap.Text = "Tổng phụ cấp: 0";
            // 
            // lblTongKhauTru
            // 
            this.lblTongKhauTru.AutoSize = true;
            this.lblTongKhauTru.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTongKhauTru.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(67)))), ((int)(((byte)(54)))));
            this.lblTongKhauTru.Location = new System.Drawing.Point(560, 20);
            this.lblTongKhauTru.Name = "lblTongKhauTru";
            this.lblTongKhauTru.Size = new System.Drawing.Size(90, 19);
            this.lblTongKhauTru.TabIndex = 3;
            this.lblTongKhauTru.Text = "Tổng khấu trừ: 0";
            // 
            // frmBangLuong
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
            this.Controls.Add(this.tabControl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmBangLuong";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Bảng lương";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.pnlMain.ResumeLayout(false);
            this.pnlMain.PerformLayout();
            this.pnlFilter.ResumeLayout(false);
            this.pnlFilter.PerformLayout();
            this.pnlButtons.ResumeLayout(false);
            this.tabControl.ResumeLayout(false);
            this.tabCongThang.ResumeLayout(false);
            this.tabBangLuong.ResumeLayout(false);
            this.pnlSummary.ResumeLayout(false);
            this.pnlSummary.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
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
        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.Panel pnlTitle;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabCongThang;
        private System.Windows.Forms.TabPage tabBangLuong;
        private System.Windows.Forms.Panel pnlSummary;
        private System.Windows.Forms.Label lblTongNhanVien;
        private System.Windows.Forms.Label lblTongLuong;
        private System.Windows.Forms.Label lblTongPhuCap;
        private System.Windows.Forms.Label lblTongKhauTru;
    }
}
