namespace VuToanThang_23110329.Forms
{
    partial class MainForm
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
            this.components = new System.ComponentModel.Container();
            this.guna2BorderlessForm1 = new Guna.UI2.WinForms.Guna2BorderlessForm(this.components);
            this.pnlSidebar = new Guna.UI2.WinForms.Guna2Panel();
            this.btnLogout = new Guna.UI2.WinForms.Guna2Button();
            this.btnBaoCaoLuong = new Guna.UI2.WinForms.Guna2Button();
            this.btnBaoCaoNhanSu = new Guna.UI2.WinForms.Guna2Button();
            this.btnPhieuLuong = new Guna.UI2.WinForms.Guna2Button();
            this.btnBangLuong = new Guna.UI2.WinForms.Guna2Button();
            this.btnTinhLuong = new Guna.UI2.WinForms.Guna2Button();
            this.btnDonTu = new Guna.UI2.WinForms.Guna2Button();
            this.btnChamCong = new Guna.UI2.WinForms.Guna2Button();
            this.btnLichPhanCa = new Guna.UI2.WinForms.Guna2Button();
            this.btnCaLam = new Guna.UI2.WinForms.Guna2Button();
            this.btnNhanVien = new Guna.UI2.WinForms.Guna2Button();
            this.btnDashboard = new Guna.UI2.WinForms.Guna2Button();
            this.pnlUserInfo = new Guna.UI2.WinForms.Guna2Panel();
            this.lblUserRole = new System.Windows.Forms.Label();
            this.lblUserName = new System.Windows.Forms.Label();
            this.picUserAvatar = new Guna.UI2.WinForms.Guna2CirclePictureBox();
            this.pnlLogo = new Guna.UI2.WinForms.Guna2Panel();
            this.lblAppTitle = new System.Windows.Forms.Label();
            this.pnlTopbar = new Guna.UI2.WinForms.Guna2Panel();
            this.btnMinimize = new Guna.UI2.WinForms.Guna2Button();
            this.btnMaximize = new Guna.UI2.WinForms.Guna2Button();
            this.btnClose = new Guna.UI2.WinForms.Guna2Button();
            this.lblCurrentForm = new System.Windows.Forms.Label();
            this.pnlContent = new Guna.UI2.WinForms.Guna2Panel();
            this.lblWelcome = new System.Windows.Forms.Label();
            this.guna2DragControl1 = new Guna.UI2.WinForms.Guna2DragControl(this.components);
            this.pnlSidebar.SuspendLayout();
            this.pnlUserInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picUserAvatar)).BeginInit();
            this.pnlLogo.SuspendLayout();
            this.pnlTopbar.SuspendLayout();
            this.pnlContent.SuspendLayout();
            this.SuspendLayout();

            // Add preview title for Designer
            var lblPreview = new System.Windows.Forms.Label
            {
                Text = "MAIN FORM - HR MANAGEMENT SYSTEM\n(Designer Preview)",
                Font = new System.Drawing.Font("Segoe UI", 18, System.Drawing.FontStyle.Bold),
                ForeColor = System.Drawing.Color.FromArgb(124, 77, 255),
                Location = new System.Drawing.Point(300, 20),
                AutoSize = true
            };

            var pnlPreviewLeft = new Guna.UI2.WinForms.Guna2Panel
            {
                BackColor = System.Drawing.Color.FromArgb(60, 60, 60),
                BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle,
                Size = new System.Drawing.Size(260, 200),
                Location = new System.Drawing.Point(300, 80)
            };

            var lblPreviewLeft = new System.Windows.Forms.Label
            {
                Text = "📱 SIDEBAR MENU\n\n🏠 Trang chủ\n👥 Nhân viên\n🕐 Ca làm\n📅 Lịch phân ca\n⏰ Chấm công\n📝 Đơn từ\n🧮 Tính lương\n💰 Bảng lương\n🧾 Phiếu lương\n📈 Báo cáo nhân sự\n📊 Báo cáo lương\n🚪 Đăng xuất",
                ForeColor = System.Drawing.Color.White,
                Location = new System.Drawing.Point(10, 10),
                Size = new System.Drawing.Size(240, 180),
                Font = new System.Drawing.Font("Segoe UI", 9)
            };
            pnlPreviewLeft.Controls.Add(lblPreviewLeft);

            var pnlPreviewTop = new Guna.UI2.WinForms.Guna2Panel
            {
                BackColor = System.Drawing.Color.FromArgb(60, 60, 60),
                BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle,
                Size = new System.Drawing.Size(400, 60),
                Location = new System.Drawing.Point(580, 80)
            };

            var lblPreviewTop = new System.Windows.Forms.Label
            {
                Text = "🔧 TOPBAR CONTROLS\n\n× □ − (Close/Maximize/Minimize) | Current Form Title | Drag to move",
                ForeColor = System.Drawing.Color.White,
                Location = new System.Drawing.Point(10, 10),
                Size = new System.Drawing.Size(380, 40),
                Font = new System.Drawing.Font("Segoe UI", 9)
            };
            pnlPreviewTop.Controls.Add(lblPreviewTop);

            var pnlPreviewContent = new Guna.UI2.WinForms.Guna2Panel
            {
                BackColor = System.Drawing.Color.FromArgb(60, 60, 60),
                BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle,
                Size = new System.Drawing.Size(400, 300),
                Location = new System.Drawing.Point(580, 160)
            };

            var lblPreviewContent = new System.Windows.Forms.Label
            {
                Text = "📋 MAIN CONTENT AREA\n\nDynamic content area where child forms are loaded:\n• Dashboard with statistics\n• Employee management\n• Shift scheduling\n• Time tracking\n• Request approval\n• Payroll calculation\n• Reports and analytics\n\nSize: 920x750px (responsive)",
                ForeColor = System.Drawing.Color.White,
                Location = new System.Drawing.Point(10, 10),
                Size = new System.Drawing.Size(380, 280),
                Font = new System.Drawing.Font("Segoe UI", 9)
            };
            pnlPreviewContent.Controls.Add(lblPreviewContent);

            var pnlPreviewUser = new Guna.UI2.WinForms.Guna2Panel
            {
                BackColor = System.Drawing.Color.FromArgb(70, 70, 70),
                Size = new System.Drawing.Size(260, 80),
                Location = new System.Drawing.Point(300, 300)
            };

            var lblPreviewUser = new System.Windows.Forms.Label
            {
                Text = "👤 USER INFO PANEL\n\nAvatar | Username | Role\n\n📍 Logo Panel\nHR Management System\n(Purple brand color #7C4DFF)",
                ForeColor = System.Drawing.Color.White,
                Location = new System.Drawing.Point(10, 10),
                Size = new System.Drawing.Size(240, 60),
                Font = new System.Drawing.Font("Segoe UI", 9)
            };
            pnlPreviewUser.Controls.Add(lblPreviewUser);

            this.Controls.AddRange(new Control[] { lblPreview, pnlPreviewLeft, pnlPreviewTop, pnlPreviewContent, pnlPreviewUser });

            // guna2BorderlessForm1
            this.guna2BorderlessForm1.BorderRadius = 10;
            this.guna2BorderlessForm1.ContainerControl = this;
            this.guna2BorderlessForm1.DockIndicatorTransparencyValue = 0.6D;
            this.guna2BorderlessForm1.TransparentWhileDrag = true;

            // pnlSidebar
            this.pnlSidebar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.pnlSidebar.Controls.Add(this.btnLogout);
            this.pnlSidebar.Controls.Add(this.btnBaoCaoLuong);
            this.pnlSidebar.Controls.Add(this.btnBaoCaoNhanSu);
            this.pnlSidebar.Controls.Add(this.btnPhieuLuong);
            this.pnlSidebar.Controls.Add(this.btnBangLuong);
            this.pnlSidebar.Controls.Add(this.btnTinhLuong);
            this.pnlSidebar.Controls.Add(this.btnDonTu);
            this.pnlSidebar.Controls.Add(this.btnChamCong);
            this.pnlSidebar.Controls.Add(this.btnLichPhanCa);
            this.pnlSidebar.Controls.Add(this.btnCaLam);
            this.pnlSidebar.Controls.Add(this.btnNhanVien);
            this.pnlSidebar.Controls.Add(this.btnDashboard);
            this.pnlSidebar.Controls.Add(this.pnlUserInfo);
            this.pnlSidebar.Controls.Add(this.pnlLogo);
            this.pnlSidebar.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlSidebar.Location = new System.Drawing.Point(0, 0);
            this.pnlSidebar.Name = "pnlSidebar";
            this.pnlSidebar.Size = new System.Drawing.Size(280, 800);
            this.pnlSidebar.TabIndex = 0;

            // Configure all buttons with similar styling
            ConfigureMenuButton(this.btnDashboard, "btnDashboard", "🏠 Trang chủ", 150, true);
            ConfigureMenuButton(this.btnNhanVien, "btnNhanVien", "👥 Nhân viên", 200, false);
            ConfigureMenuButton(this.btnCaLam, "btnCaLam", "🕐 Ca làm", 250, false);
            ConfigureMenuButton(this.btnLichPhanCa, "btnLichPhanCa", "📅 Lịch phân ca", 300, false);
            ConfigureMenuButton(this.btnChamCong, "btnChamCong", "⏰ Chấm công", 350, false);
            ConfigureMenuButton(this.btnDonTu, "btnDonTu", "📝 Đơn từ", 400, false);
            ConfigureMenuButton(this.btnTinhLuong, "btnTinhLuong", "🧮 Tính lương", 450, false);
            ConfigureMenuButton(this.btnBangLuong, "btnBangLuong", "💰 Bảng lương", 500, false);
            ConfigureMenuButton(this.btnPhieuLuong, "btnPhieuLuong", "🧾 Phiếu lương", 550, false);
            ConfigureMenuButton(this.btnBaoCaoNhanSu, "btnBaoCaoNhanSu", "📈 Báo cáo nhân sự", 600, false);
            ConfigureMenuButton(this.btnBaoCaoLuong, "btnBaoCaoLuong", "📊 Báo cáo lương", 650, false);

            // btnLogout
            this.btnLogout.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnLogout.BorderRadius = 8;
            this.btnLogout.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(17)))), ((int)(((byte)(35)))));
            this.btnLogout.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Bold);
            this.btnLogout.ForeColor = System.Drawing.Color.White;
            this.btnLogout.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(212)))), ((int)(((byte)(0)))), ((int)(((byte)(15)))));
            this.btnLogout.Location = new System.Drawing.Point(20, 740);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Size = new System.Drawing.Size(240, 45);
            this.btnLogout.TabIndex = 13;
            this.btnLogout.Text = "🚪 Đăng xuất";
            this.btnLogout.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.btnLogout.Click += new System.EventHandler(this.btnLogout_Click);

            // pnlUserInfo
            this.pnlUserInfo.Controls.Add(this.lblUserRole);
            this.pnlUserInfo.Controls.Add(this.lblUserName);
            this.pnlUserInfo.Controls.Add(this.picUserAvatar);
            this.pnlUserInfo.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlUserInfo.Location = new System.Drawing.Point(0, 80);
            this.pnlUserInfo.Name = "pnlUserInfo";
            this.pnlUserInfo.Size = new System.Drawing.Size(280, 60);
            this.pnlUserInfo.TabIndex = 1;

            // picUserAvatar
            this.picUserAvatar.BackColor = System.Drawing.Color.Transparent;
            this.picUserAvatar.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(124)))), ((int)(((byte)(77)))), ((int)(((byte)(255)))));
            this.picUserAvatar.ImageRotate = 0F;
            this.picUserAvatar.Location = new System.Drawing.Point(20, 10);
            this.picUserAvatar.Name = "picUserAvatar";
            this.picUserAvatar.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle;
            this.picUserAvatar.Size = new System.Drawing.Size(40, 40);
            this.picUserAvatar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picUserAvatar.TabIndex = 2;
            this.picUserAvatar.TabStop = false;

            // lblUserName
            this.lblUserName.AutoSize = true;
            this.lblUserName.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Bold);
            this.lblUserName.ForeColor = System.Drawing.Color.White;
            this.lblUserName.Location = new System.Drawing.Point(70, 15);
            this.lblUserName.Name = "lblUserName";
            this.lblUserName.Size = new System.Drawing.Size(89, 19);
            this.lblUserName.TabIndex = 1;
            this.lblUserName.Text = "Tên hiển thị";

            // lblUserRole
            this.lblUserRole.AutoSize = true;
            this.lblUserRole.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblUserRole.ForeColor = System.Drawing.Color.Gray;
            this.lblUserRole.Location = new System.Drawing.Point(70, 35);
            this.lblUserRole.Name = "lblUserRole";
            this.lblUserRole.Size = new System.Drawing.Size(44, 15);
            this.lblUserRole.TabIndex = 0;
            this.lblUserRole.Text = "Vai trò";

            // pnlLogo
            this.pnlLogo.Controls.Add(this.lblAppTitle);
            this.pnlLogo.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlLogo.Location = new System.Drawing.Point(0, 0);
            this.pnlLogo.Name = "pnlLogo";
            this.pnlLogo.Size = new System.Drawing.Size(280, 80);
            this.pnlLogo.TabIndex = 0;

            // lblAppTitle
            this.lblAppTitle.AutoSize = true;
            this.lblAppTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblAppTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(124)))), ((int)(((byte)(77)))), ((int)(((byte)(255)))));
            this.lblAppTitle.Location = new System.Drawing.Point(20, 25);
            this.lblAppTitle.Name = "lblAppTitle";
            this.lblAppTitle.Size = new System.Drawing.Size(240, 30);
            this.lblAppTitle.TabIndex = 0;
            this.lblAppTitle.Text = "HR Management System";

            // pnlTopbar
            this.pnlTopbar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.pnlTopbar.Controls.Add(this.btnMinimize);
            this.pnlTopbar.Controls.Add(this.btnMaximize);
            this.pnlTopbar.Controls.Add(this.btnClose);
            this.pnlTopbar.Controls.Add(this.lblCurrentForm);
            this.pnlTopbar.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTopbar.Location = new System.Drawing.Point(280, 0);
            this.pnlTopbar.Name = "pnlTopbar";
            this.pnlTopbar.Size = new System.Drawing.Size(920, 50);
            this.pnlTopbar.TabIndex = 1;

            // Configure window control buttons
            ConfigureWindowButton(this.btnClose, "×", 870, System.Drawing.Color.FromArgb(232, 17, 35));
            ConfigureWindowButton(this.btnMaximize, "□", 825, System.Drawing.Color.FromArgb(60, 60, 60));
            ConfigureWindowButton(this.btnMinimize, "−", 780, System.Drawing.Color.FromArgb(60, 60, 60));

            // lblCurrentForm
            this.lblCurrentForm.AutoSize = true;
            this.lblCurrentForm.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblCurrentForm.ForeColor = System.Drawing.Color.White;
            this.lblCurrentForm.Location = new System.Drawing.Point(20, 15);
            this.lblCurrentForm.Name = "lblCurrentForm";
            this.lblCurrentForm.Size = new System.Drawing.Size(95, 25);
            this.lblCurrentForm.TabIndex = 0;
            this.lblCurrentForm.Text = "Trang chủ";

            // pnlContent
            this.pnlContent.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.pnlContent.Controls.Add(this.lblWelcome);
            this.pnlContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlContent.Location = new System.Drawing.Point(280, 50);
            this.pnlContent.Name = "pnlContent";
            this.pnlContent.Padding = new System.Windows.Forms.Padding(20);
            this.pnlContent.Size = new System.Drawing.Size(920, 750);
            this.pnlContent.TabIndex = 2;

            // lblWelcome
            this.lblWelcome.AutoSize = true;
            this.lblWelcome.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblWelcome.ForeColor = System.Drawing.Color.White;
            this.lblWelcome.Location = new System.Drawing.Point(50, 50);
            this.lblWelcome.Name = "lblWelcome";
            this.lblWelcome.Size = new System.Drawing.Size(400, 32);
            this.lblWelcome.TabIndex = 0;
            this.lblWelcome.Text = "Chào mừng đến với hệ thống HR!";

            // guna2DragControl1
            this.guna2DragControl1.DockIndicatorTransparencyValue = 0.6D;
            this.guna2DragControl1.TargetControl = this.pnlTopbar;
            this.guna2DragControl1.UseTransparentDrag = true;

            // MainForm
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.MinimumSize = new System.Drawing.Size(1024, 768);
            this.Size = new System.Drawing.Size(1200, 800);
            this.Controls.Add(this.pnlContent);
            this.Controls.Add(this.pnlTopbar);
            this.Controls.Add(this.pnlSidebar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "HR Management System";
            this.WindowState = System.Windows.Forms.FormWindowState.Normal;
            this.pnlSidebar.ResumeLayout(false);
            this.pnlUserInfo.ResumeLayout(false);
            this.pnlUserInfo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picUserAvatar)).EndInit();
            this.pnlLogo.ResumeLayout(false);
            this.pnlLogo.PerformLayout();
            this.pnlTopbar.ResumeLayout(false);
            this.pnlTopbar.PerformLayout();
            this.pnlContent.ResumeLayout(false);
            this.pnlContent.PerformLayout();
            this.ResumeLayout(false);
        }
        }

        private void ConfigureMenuButton(Guna.UI2.WinForms.Guna2Button button, string name, string text, int top, bool isActive)
        {
            button.Name = name;
            button.BorderRadius = 8;
            button.FillColor = isActive ? System.Drawing.Color.FromArgb(124, 77, 255) : System.Drawing.Color.Transparent;
            button.Font = new System.Drawing.Font("Segoe UI", 10.5F, isActive ? System.Drawing.FontStyle.Bold : System.Drawing.FontStyle.Regular);
            button.ForeColor = System.Drawing.Color.White;
            button.HoverState.FillColor = System.Drawing.Color.FromArgb(124, 77, 255);
            button.Location = new System.Drawing.Point(20, top);
            button.Size = new System.Drawing.Size(240, 45);
            button.Text = text;
            button.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            button.Click += new System.EventHandler(this.MenuButton_Click);
        }

        private void ConfigureWindowButton(Guna.UI2.WinForms.Guna2Button button, string text, int left, System.Drawing.Color hoverColor)
        {
            button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            button.BackColor = System.Drawing.Color.Transparent;
            button.BorderRadius = 5;
            button.FillColor = System.Drawing.Color.Transparent;
            button.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            button.ForeColor = System.Drawing.Color.White;
            button.HoverState.FillColor = hoverColor;
            button.Location = new System.Drawing.Point(left, 10);
            button.Size = new System.Drawing.Size(30, 30);
            button.Text = text;
            
            // Add event handlers
            if (text == "×")
                button.Click += new System.EventHandler(this.btnClose_Click);
            else if (text == "□")
                button.Click += new System.EventHandler(this.btnMaximize_Click);
            else if (text == "−")
                button.Click += new System.EventHandler(this.btnMinimize_Click);
        }

        //#endregion

        private Guna.UI2.WinForms.Guna2BorderlessForm guna2BorderlessForm1;
        private Guna.UI2.WinForms.Guna2Panel pnlSidebar;
        private Guna.UI2.WinForms.Guna2Panel pnlTopbar;
        private Guna.UI2.WinForms.Guna2Panel pnlContent;
        private Guna.UI2.WinForms.Guna2Panel pnlLogo;
        private Guna.UI2.WinForms.Guna2Panel pnlUserInfo;
        private System.Windows.Forms.Label lblAppTitle;
        private Guna.UI2.WinForms.Guna2CirclePictureBox picUserAvatar;
        private System.Windows.Forms.Label lblUserName;
        private System.Windows.Forms.Label lblUserRole;
        private Guna.UI2.WinForms.Guna2Button btnDashboard;
        private Guna.UI2.WinForms.Guna2Button btnNhanVien;
        private Guna.UI2.WinForms.Guna2Button btnCaLam;
        private Guna.UI2.WinForms.Guna2Button btnLichPhanCa;
        private Guna.UI2.WinForms.Guna2Button btnChamCong;
        private Guna.UI2.WinForms.Guna2Button btnDonTu;
        private Guna.UI2.WinForms.Guna2Button btnTinhLuong;
        private Guna.UI2.WinForms.Guna2Button btnBangLuong;
        private Guna.UI2.WinForms.Guna2Button btnPhieuLuong;
        private Guna.UI2.WinForms.Guna2Button btnBaoCaoNhanSu;
        private Guna.UI2.WinForms.Guna2Button btnBaoCaoLuong;
        private Guna.UI2.WinForms.Guna2Button btnLogout;
        private System.Windows.Forms.Label lblCurrentForm;
        private Guna.UI2.WinForms.Guna2Button btnClose;
        private Guna.UI2.WinForms.Guna2Button btnMaximize;
        private Guna.UI2.WinForms.Guna2Button btnMinimize;
        private System.Windows.Forms.Label lblWelcome;
        private Guna.UI2.WinForms.Guna2DragControl guna2DragControl1;
    }
}
