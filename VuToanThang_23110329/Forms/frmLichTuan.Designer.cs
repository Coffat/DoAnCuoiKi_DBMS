namespace VuToanThang_23110329.Forms
{
    partial class frmLichTuan
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
            this.pnlTop = new Guna.UI2.WinForms.Guna2Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.pnlFilter = new Guna.UI2.WinForms.Guna2Panel();
            this.btnNextWeek = new Guna.UI2.WinForms.Guna2Button();
            this.btnPrevWeek = new Guna.UI2.WinForms.Guna2Button();
            this.lblWeekInfo = new System.Windows.Forms.Label();
            this.cboNhanVien = new Guna.UI2.WinForms.Guna2ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.pnlButtons = new Guna.UI2.WinForms.Guna2Panel();
            this.btnDong = new Guna.UI2.WinForms.Guna2Button();
            this.btnLamMoi = new Guna.UI2.WinForms.Guna2Button();
            this.btnLuu = new Guna.UI2.WinForms.Guna2Button();
            this.btnMoKhoaTuan = new Guna.UI2.WinForms.Guna2Button();
            this.btnKhoaTuan = new Guna.UI2.WinForms.Guna2Button();
            this.btnSaoChepTuan = new Guna.UI2.WinForms.Guna2Button();
            this.pnlMain = new Guna.UI2.WinForms.Guna2Panel();
            this.dgvLichTuan = new System.Windows.Forms.DataGridView();
            this.pnlTop.SuspendLayout();
            this.pnlFilter.SuspendLayout();
            this.pnlButtons.SuspendLayout();
            this.pnlMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLichTuan)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlTop
            // 
            this.pnlTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(70)))));
            this.pnlTop.Controls.Add(this.lblTitle);
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Location = new System.Drawing.Point(0, 0);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Size = new System.Drawing.Size(1200, 60);
            this.pnlTop.TabIndex = 0;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(20, 15);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(296, 32);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "QUẢN LÝ LỊCH TUẦN";
            // 
            // pnlFilter
            // 
            this.pnlFilter.BackColor = System.Drawing.Color.White;
            this.pnlFilter.Controls.Add(this.btnNextWeek);
            this.pnlFilter.Controls.Add(this.btnPrevWeek);
            this.pnlFilter.Controls.Add(this.lblWeekInfo);
            this.pnlFilter.Controls.Add(this.cboNhanVien);
            this.pnlFilter.Controls.Add(this.label1);
            this.pnlFilter.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlFilter.Location = new System.Drawing.Point(0, 60);
            this.pnlFilter.Name = "pnlFilter";
            this.pnlFilter.Padding = new System.Windows.Forms.Padding(20, 10, 20, 10);
            this.pnlFilter.Size = new System.Drawing.Size(1200, 80);
            this.pnlFilter.TabIndex = 1;
            // 
            // btnNextWeek
            // 
            this.btnNextWeek.BorderRadius = 5;
            this.btnNextWeek.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnNextWeek.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnNextWeek.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnNextWeek.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnNextWeek.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(90)))));
            this.btnNextWeek.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnNextWeek.ForeColor = System.Drawing.Color.White;
            this.btnNextWeek.Location = new System.Drawing.Point(850, 20);
            this.btnNextWeek.Name = "btnNextWeek";
            this.btnNextWeek.Size = new System.Drawing.Size(120, 40);
            this.btnNextWeek.TabIndex = 4;
            this.btnNextWeek.Text = "Tuần sau >";
            this.btnNextWeek.Click += new System.EventHandler(this.btnNextWeek_Click);
            // 
            // btnPrevWeek
            // 
            this.btnPrevWeek.BorderRadius = 5;
            this.btnPrevWeek.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnPrevWeek.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnPrevWeek.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnPrevWeek.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnPrevWeek.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(90)))));
            this.btnPrevWeek.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnPrevWeek.ForeColor = System.Drawing.Color.White;
            this.btnPrevWeek.Location = new System.Drawing.Point(550, 20);
            this.btnPrevWeek.Name = "btnPrevWeek";
            this.btnPrevWeek.Size = new System.Drawing.Size(120, 40);
            this.btnPrevWeek.TabIndex = 3;
            this.btnPrevWeek.Text = "< Tuần trước";
            this.btnPrevWeek.Click += new System.EventHandler(this.btnPrevWeek_Click);
            // 
            // lblWeekInfo
            // 
            this.lblWeekInfo.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblWeekInfo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(70)))));
            this.lblWeekInfo.Location = new System.Drawing.Point(676, 20);
            this.lblWeekInfo.Name = "lblWeekInfo";
            this.lblWeekInfo.Size = new System.Drawing.Size(168, 40);
            this.lblWeekInfo.TabIndex = 2;
            this.lblWeekInfo.Text = "Tuần: 01/01 - 07/01";
            this.lblWeekInfo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cboNhanVien
            // 
            this.cboNhanVien.BackColor = System.Drawing.Color.Transparent;
            this.cboNhanVien.BorderRadius = 5;
            this.cboNhanVien.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboNhanVien.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboNhanVien.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cboNhanVien.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cboNhanVien.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cboNhanVien.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(88)))), ((int)(((byte)(112)))));
            this.cboNhanVien.ItemHeight = 30;
            this.cboNhanVien.Location = new System.Drawing.Point(120, 20);
            this.cboNhanVien.Name = "cboNhanVien";
            this.cboNhanVien.Size = new System.Drawing.Size(400, 36);
            this.cboNhanVien.TabIndex = 1;
            this.cboNhanVien.SelectedIndexChanged += new System.EventHandler(this.cboNhanVien_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(70)))));
            this.label1.Location = new System.Drawing.Point(20, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 19);
            this.label1.TabIndex = 0;
            this.label1.Text = "Nhân viên:";
            // 
            // pnlButtons
            // 
            this.pnlButtons.BackColor = System.Drawing.Color.White;
            this.pnlButtons.Controls.Add(this.btnDong);
            this.pnlButtons.Controls.Add(this.btnLamMoi);
            this.pnlButtons.Controls.Add(this.btnLuu);
            this.pnlButtons.Controls.Add(this.btnMoKhoaTuan);
            this.pnlButtons.Controls.Add(this.btnKhoaTuan);
            this.pnlButtons.Controls.Add(this.btnSaoChepTuan);
            this.pnlButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlButtons.Location = new System.Drawing.Point(0, 620);
            this.pnlButtons.Name = "pnlButtons";
            this.pnlButtons.Padding = new System.Windows.Forms.Padding(20, 10, 20, 10);
            this.pnlButtons.Size = new System.Drawing.Size(1200, 80);
            this.pnlButtons.TabIndex = 2;
            // 
            // btnDong
            // 
            this.btnDong.BorderRadius = 5;
            this.btnDong.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnDong.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnDong.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnDong.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnDong.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(120)))));
            this.btnDong.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnDong.ForeColor = System.Drawing.Color.White;
            this.btnDong.Location = new System.Drawing.Point(1050, 20);
            this.btnDong.Name = "btnDong";
            this.btnDong.Size = new System.Drawing.Size(120, 45);
            this.btnDong.TabIndex = 5;
            this.btnDong.Text = "Đóng";
            this.btnDong.Click += new System.EventHandler(this.btnDong_Click);
            // 
            // btnLamMoi
            // 
            this.btnLamMoi.BorderRadius = 5;
            this.btnLamMoi.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnLamMoi.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnLamMoi.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnLamMoi.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnLamMoi.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(90)))));
            this.btnLamMoi.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnLamMoi.ForeColor = System.Drawing.Color.White;
            this.btnLamMoi.Location = new System.Drawing.Point(900, 20);
            this.btnLamMoi.Name = "btnLamMoi";
            this.btnLamMoi.Size = new System.Drawing.Size(120, 45);
            this.btnLamMoi.TabIndex = 4;
            this.btnLamMoi.Text = "Làm mới";
            this.btnLamMoi.Click += new System.EventHandler(this.btnLamMoi_Click);
            // 
            // btnLuu
            // 
            this.btnLuu.BorderRadius = 5;
            this.btnLuu.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnLuu.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnLuu.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnLuu.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnLuu.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(167)))), ((int)(((byte)(69)))));
            this.btnLuu.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnLuu.ForeColor = System.Drawing.Color.White;
            this.btnLuu.Location = new System.Drawing.Point(750, 20);
            this.btnLuu.Name = "btnLuu";
            this.btnLuu.Size = new System.Drawing.Size(120, 45);
            this.btnLuu.TabIndex = 3;
            this.btnLuu.Text = "Lưu";
            this.btnLuu.Click += new System.EventHandler(this.btnLuu_Click);
            // 
            // btnMoKhoaTuan
            // 
            this.btnMoKhoaTuan.BorderRadius = 5;
            this.btnMoKhoaTuan.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnMoKhoaTuan.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnMoKhoaTuan.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnMoKhoaTuan.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnMoKhoaTuan.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(152)))), ((int)(((byte)(0)))));
            this.btnMoKhoaTuan.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnMoKhoaTuan.ForeColor = System.Drawing.Color.White;
            this.btnMoKhoaTuan.Location = new System.Drawing.Point(400, 20);
            this.btnMoKhoaTuan.Name = "btnMoKhoaTuan";
            this.btnMoKhoaTuan.Size = new System.Drawing.Size(150, 45);
            this.btnMoKhoaTuan.TabIndex = 2;
            this.btnMoKhoaTuan.Text = "Mở khóa tuần";
            this.btnMoKhoaTuan.Click += new System.EventHandler(this.btnMoKhoaTuan_Click);
            // 
            // btnKhoaTuan
            // 
            this.btnKhoaTuan.BorderRadius = 5;
            this.btnKhoaTuan.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnKhoaTuan.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnKhoaTuan.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnKhoaTuan.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnKhoaTuan.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(53)))), ((int)(((byte)(69)))));
            this.btnKhoaTuan.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnKhoaTuan.ForeColor = System.Drawing.Color.White;
            this.btnKhoaTuan.Location = new System.Drawing.Point(220, 20);
            this.btnKhoaTuan.Name = "btnKhoaTuan";
            this.btnKhoaTuan.Size = new System.Drawing.Size(150, 45);
            this.btnKhoaTuan.TabIndex = 1;
            this.btnKhoaTuan.Text = "Khóa tuần";
            this.btnKhoaTuan.Click += new System.EventHandler(this.btnKhoaTuan_Click);
            // 
            // btnSaoChepTuan
            // 
            this.btnSaoChepTuan.BorderRadius = 5;
            this.btnSaoChepTuan.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnSaoChepTuan.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnSaoChepTuan.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnSaoChepTuan.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnSaoChepTuan.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(123)))), ((int)(((byte)(255)))));
            this.btnSaoChepTuan.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnSaoChepTuan.ForeColor = System.Drawing.Color.White;
            this.btnSaoChepTuan.Location = new System.Drawing.Point(20, 20);
            this.btnSaoChepTuan.Name = "btnSaoChepTuan";
            this.btnSaoChepTuan.Size = new System.Drawing.Size(170, 45);
            this.btnSaoChepTuan.TabIndex = 0;
            this.btnSaoChepTuan.Text = "Sao chép tuần trước";
            this.btnSaoChepTuan.Click += new System.EventHandler(this.btnSaoChepTuan_Click);
            // 
            // pnlMain
            // 
            this.pnlMain.BackColor = System.Drawing.Color.White;
            this.pnlMain.Controls.Add(this.dgvLichTuan);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 140);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Padding = new System.Windows.Forms.Padding(20);
            this.pnlMain.Size = new System.Drawing.Size(1200, 480);
            this.pnlMain.TabIndex = 3;
            // 
            // dgvLichTuan
            // 
            this.dgvLichTuan.AllowUserToAddRows = false;
            this.dgvLichTuan.AllowUserToDeleteRows = false;
            this.dgvLichTuan.BackgroundColor = System.Drawing.Color.White;
            this.dgvLichTuan.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvLichTuan.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvLichTuan.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvLichTuan.Location = new System.Drawing.Point(20, 20);
            this.dgvLichTuan.Name = "dgvLichTuan";
            this.dgvLichTuan.RowHeadersWidth = 51;
            this.dgvLichTuan.RowTemplate.Height = 35;
            this.dgvLichTuan.Size = new System.Drawing.Size(1160, 440);
            this.dgvLichTuan.TabIndex = 0;
            // 
            // frmLichTuan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1200, 700);
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.pnlButtons);
            this.Controls.Add(this.pnlFilter);
            this.Controls.Add(this.pnlTop);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "frmLichTuan";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Quản lý lịch tuần";
            this.Load += new System.EventHandler(this.frmLichTuan_Load);
            this.pnlTop.ResumeLayout(false);
            this.pnlTop.PerformLayout();
            this.pnlFilter.ResumeLayout(false);
            this.pnlFilter.PerformLayout();
            this.pnlButtons.ResumeLayout(false);
            this.pnlMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvLichTuan)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2Panel pnlTop;
        private System.Windows.Forms.Label lblTitle;
        private Guna.UI2.WinForms.Guna2Panel pnlFilter;
        private Guna.UI2.WinForms.Guna2ComboBox cboNhanVien;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblWeekInfo;
        private Guna.UI2.WinForms.Guna2Button btnPrevWeek;
        private Guna.UI2.WinForms.Guna2Button btnNextWeek;
        private Guna.UI2.WinForms.Guna2Panel pnlButtons;
        private Guna.UI2.WinForms.Guna2Button btnSaoChepTuan;
        private Guna.UI2.WinForms.Guna2Button btnKhoaTuan;
        private Guna.UI2.WinForms.Guna2Button btnMoKhoaTuan;
        private Guna.UI2.WinForms.Guna2Button btnLuu;
        private Guna.UI2.WinForms.Guna2Button btnLamMoi;
        private Guna.UI2.WinForms.Guna2Button btnDong;
        private Guna.UI2.WinForms.Guna2Panel pnlMain;
        private System.Windows.Forms.DataGridView dgvLichTuan;
    }
}
