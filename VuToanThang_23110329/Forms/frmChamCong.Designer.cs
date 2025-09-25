namespace VuToanThang_23110329.Forms
{
    partial class frmChamCong
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
            this.tabCheckInOut = new System.Windows.Forms.TabPage();
            this.tabLichSu = new System.Windows.Forms.TabPage();
            this.tabKhoaCong = new System.Windows.Forms.TabPage();
            this.pnlCheckInOut = new System.Windows.Forms.Panel();
            this.lblTrangThai = new System.Windows.Forms.Label();
            this.lblThongTinCa = new System.Windows.Forms.Label();
            this.btnCheckIn = new Guna.UI2.WinForms.Guna2Button();
            this.btnCheckOut = new Guna.UI2.WinForms.Guna2Button();
            this.btnLamMoi = new Guna.UI2.WinForms.Guna2Button();
            this.lblCheckInTime = new System.Windows.Forms.Label();
            this.lblCheckOutTime = new System.Windows.Forms.Label();
            this.pnlMain.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.tabCheckInOut.SuspendLayout();
            this.tabLichSu.SuspendLayout();
            this.tabKhoaCong.SuspendLayout();
            this.pnlCheckInOut.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(124)))), ((int)(((byte)(77)))), ((int)(((byte)(255)))));
            this.lblTitle.Location = new System.Drawing.Point(20, 20);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(120, 30);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Chấm công";
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
            this.pnlTitle.TabIndex = 1;
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabCheckInOut);
            this.tabControl.Controls.Add(this.tabLichSu);
            this.tabControl.Controls.Add(this.tabKhoaCong);
            this.tabControl.Location = new System.Drawing.Point(20, 60);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(1160, 620);
            this.tabControl.TabIndex = 2;
            // 
            // tabCheckInOut
            // 
            this.tabCheckInOut.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.tabCheckInOut.Controls.Add(this.pnlCheckInOut);
            this.tabCheckInOut.Location = new System.Drawing.Point(4, 22);
            this.tabCheckInOut.Name = "tabCheckInOut";
            this.tabCheckInOut.Padding = new System.Windows.Forms.Padding(3);
            this.tabCheckInOut.Size = new System.Drawing.Size(1152, 594);
            this.tabCheckInOut.TabIndex = 0;
            this.tabCheckInOut.Text = "Check In/Out";
            // 
            // tabLichSu
            // 
            this.tabLichSu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.tabLichSu.Location = new System.Drawing.Point(4, 22);
            this.tabLichSu.Name = "tabLichSu";
            this.tabLichSu.Padding = new System.Windows.Forms.Padding(3);
            this.tabLichSu.Size = new System.Drawing.Size(1152, 594);
            this.tabLichSu.TabIndex = 1;
            this.tabLichSu.Text = "Lịch sử chấm công";
            // 
            // tabKhoaCong
            // 
            this.tabKhoaCong.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.tabKhoaCong.Location = new System.Drawing.Point(4, 22);
            this.tabKhoaCong.Name = "tabKhoaCong";
            this.tabKhoaCong.Padding = new System.Windows.Forms.Padding(3);
            this.tabKhoaCong.Size = new System.Drawing.Size(1152, 594);
            this.tabKhoaCong.TabIndex = 2;
            this.tabKhoaCong.Text = "Khóa công kỳ";
            // 
            // pnlCheckInOut
            // 
            this.pnlCheckInOut.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.pnlCheckInOut.Controls.Add(this.lblTrangThai);
            this.pnlCheckInOut.Controls.Add(this.lblThongTinCa);
            this.pnlCheckInOut.Controls.Add(this.btnCheckIn);
            this.pnlCheckInOut.Controls.Add(this.btnCheckOut);
            this.pnlCheckInOut.Controls.Add(this.btnLamMoi);
            this.pnlCheckInOut.Controls.Add(this.lblCheckInTime);
            this.pnlCheckInOut.Controls.Add(this.lblCheckOutTime);
            this.pnlCheckInOut.Location = new System.Drawing.Point(20, 20);
            this.pnlCheckInOut.Name = "pnlCheckInOut";
            this.pnlCheckInOut.Size = new System.Drawing.Size(1112, 554);
            this.pnlCheckInOut.TabIndex = 0;
            // 
            // lblTrangThai
            // 
            this.lblTrangThai.AutoSize = true;
            this.lblTrangThai.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTrangThai.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(124)))), ((int)(((byte)(77)))), ((int)(((byte)(255)))));
            this.lblTrangThai.Location = new System.Drawing.Point(20, 20);
            this.lblTrangThai.Name = "lblTrangThai";
            this.lblTrangThai.Size = new System.Drawing.Size(100, 25);
            this.lblTrangThai.TabIndex = 0;
            this.lblTrangThai.Text = "Đang tải trạng thái...";
            // 
            // lblThongTinCa
            // 
            this.lblThongTinCa.AutoSize = true;
            this.lblThongTinCa.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblThongTinCa.ForeColor = System.Drawing.Color.White;
            this.lblThongTinCa.Location = new System.Drawing.Point(20, 60);
            this.lblThongTinCa.Name = "lblThongTinCa";
            this.lblThongTinCa.Size = new System.Drawing.Size(80, 21);
            this.lblThongTinCa.TabIndex = 1;
            this.lblThongTinCa.Text = "Thông tin ca:";
            // 
            // btnCheckIn
            // 
            this.btnCheckIn.BorderRadius = 12;
            this.btnCheckIn.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnCheckIn.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnCheckIn.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnCheckIn.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnCheckIn.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(175)))), ((int)(((byte)(80)))));
            this.btnCheckIn.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCheckIn.ForeColor = System.Drawing.Color.White;
            this.btnCheckIn.Location = new System.Drawing.Point(100, 120);
            this.btnCheckIn.Name = "btnCheckIn";
            this.btnCheckIn.Size = new System.Drawing.Size(120, 50);
            this.btnCheckIn.TabIndex = 2;
            this.btnCheckIn.Text = "Check In";
            // 
            // btnCheckOut
            // 
            this.btnCheckOut.BorderRadius = 12;
            this.btnCheckOut.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnCheckOut.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnCheckOut.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnCheckOut.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnCheckOut.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(67)))), ((int)(((byte)(54)))));
            this.btnCheckOut.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCheckOut.ForeColor = System.Drawing.Color.White;
            this.btnCheckOut.Location = new System.Drawing.Point(250, 120);
            this.btnCheckOut.Name = "btnCheckOut";
            this.btnCheckOut.Size = new System.Drawing.Size(120, 50);
            this.btnCheckOut.TabIndex = 3;
            this.btnCheckOut.Text = "Check Out";
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
            this.btnLamMoi.Location = new System.Drawing.Point(400, 130);
            this.btnLamMoi.Name = "btnLamMoi";
            this.btnLamMoi.Size = new System.Drawing.Size(80, 30);
            this.btnLamMoi.TabIndex = 4;
            this.btnLamMoi.Text = "Làm mới";
            // 
            // lblCheckInTime
            // 
            this.lblCheckInTime.AutoSize = true;
            this.lblCheckInTime.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCheckInTime.ForeColor = System.Drawing.Color.White;
            this.lblCheckInTime.Location = new System.Drawing.Point(20, 200);
            this.lblCheckInTime.Name = "lblCheckInTime";
            this.lblCheckInTime.Size = new System.Drawing.Size(80, 19);
            this.lblCheckInTime.TabIndex = 5;
            this.lblCheckInTime.Text = "Giờ vào: Chưa có";
            // 
            // lblCheckOutTime
            // 
            this.lblCheckOutTime.AutoSize = true;
            this.lblCheckOutTime.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCheckOutTime.ForeColor = System.Drawing.Color.White;
            this.lblCheckOutTime.Location = new System.Drawing.Point(20, 230);
            this.lblCheckOutTime.Name = "lblCheckOutTime";
            this.lblCheckOutTime.Size = new System.Drawing.Size(80, 19);
            this.lblCheckOutTime.TabIndex = 6;
            this.lblCheckOutTime.Text = "Giờ ra: Chưa có";
            // 
            // frmChamCong
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.ClientSize = new System.Drawing.Size(1200, 700);
            this.Controls.Add(this.pnlTitle);
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.tabControl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmChamCong";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Chấm công";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.pnlMain.ResumeLayout(false);
            this.pnlMain.PerformLayout();
            this.tabControl.ResumeLayout(false);
            this.tabCheckInOut.ResumeLayout(false);
            this.tabLichSu.ResumeLayout(false);
            this.tabKhoaCong.ResumeLayout(false);
            this.pnlCheckInOut.ResumeLayout(false);
            this.pnlCheckInOut.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.Panel pnlTitle;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabCheckInOut;
        private System.Windows.Forms.TabPage tabLichSu;
        private System.Windows.Forms.TabPage tabKhoaCong;
        private System.Windows.Forms.Panel pnlCheckInOut;
        private System.Windows.Forms.Label lblTrangThai;
        private System.Windows.Forms.Label lblThongTinCa;
        private Guna.UI2.WinForms.Guna2Button btnCheckIn;
        private Guna.UI2.WinForms.Guna2Button btnCheckOut;
        private Guna.UI2.WinForms.Guna2Button btnLamMoi;
        private System.Windows.Forms.Label lblCheckInTime;
        private System.Windows.Forms.Label lblCheckOutTime;
    }
}
