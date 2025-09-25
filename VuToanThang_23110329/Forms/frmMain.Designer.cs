using System;
using System.Windows.Forms;

namespace VuToanThang_23110329.Forms
{
    partial class frmMain
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
            if (disposing)
            {
                if (components != null)
            {
                components.Dispose();
                }
                // Dispose dynamic controls
                if (pnlContent != null)
                {
                    foreach (Control control in pnlContent.Controls)
                    {
                        if (control is IDisposable disposable)
                        {
                            disposable.Dispose();
                        }
                    }
                }
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
            this.pnlMain = new Guna.UI2.WinForms.Guna2Panel();
            this.pnlSidebar = new Guna.UI2.WinForms.Guna2Panel();
            this.pnlHeader = new Guna.UI2.WinForms.Guna2Panel();
            this.pnlContent = new Guna.UI2.WinForms.Guna2Panel();
            this.lblUserInfo = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.lblWelcome = new System.Windows.Forms.Label();
            this.btnDashboard = new Guna.UI2.WinForms.Guna2Button();
            this.btnQuanLy = new Guna.UI2.WinForms.Guna2Button();
            this.btnDanhMuc = new Guna.UI2.WinForms.Guna2Button();
            this.btnNghiepVu = new Guna.UI2.WinForms.Guna2Button();
            this.btnTienLuong = new Guna.UI2.WinForms.Guna2Button();
            this.btnCaNhan = new Guna.UI2.WinForms.Guna2Button();
            this.btnChamCong = new Guna.UI2.WinForms.Guna2Button();
            this.btnBaoCao = new Guna.UI2.WinForms.Guna2Button();
            this.btnDangXuat = new Guna.UI2.WinForms.Guna2Button();
            this.pnlLogo = new Guna.UI2.WinForms.Guna2Panel();
            this.lblLogo = new System.Windows.Forms.Label();
            this.pnlMain.SuspendLayout();
            this.pnlSidebar.SuspendLayout();
            this.pnlHeader.SuspendLayout();
            this.pnlContent.SuspendLayout();
            this.pnlLogo.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlMain
            // 
            this.pnlMain.BorderRadius = 0;
            this.pnlMain.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.pnlMain.Location = new System.Drawing.Point(0, 0);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(1200, 700);
            this.pnlMain.TabIndex = 0;
            // 
            // pnlSidebar
            // 
            this.pnlSidebar.BorderRadius = 0;
            this.pnlSidebar.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.pnlSidebar.Location = new System.Drawing.Point(0, 0);
            this.pnlSidebar.Name = "pnlSidebar";
            this.pnlSidebar.Size = new System.Drawing.Size(250, 700);
            this.pnlSidebar.TabIndex = 1;
            // 
            // pnlHeader
            // 
            this.pnlHeader.BorderRadius = 0;
            this.pnlHeader.FillColor = System.Drawing.Color.White;
            this.pnlHeader.Location = new System.Drawing.Point(250, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.ShadowDecoration.BorderRadius = 0;
            this.pnlHeader.ShadowDecoration.Color = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(10)))));
            this.pnlHeader.ShadowDecoration.Depth = 5;
            this.pnlHeader.ShadowDecoration.Enabled = true;
            this.pnlHeader.Size = new System.Drawing.Size(950, 80);
            this.pnlHeader.TabIndex = 2;
            // 
            // pnlContent
            // 
            this.pnlContent.BorderRadius = 0;
            this.pnlContent.FillColor = System.Drawing.Color.White;
            this.pnlContent.Location = new System.Drawing.Point(250, 80);
            this.pnlContent.Name = "pnlContent";
            this.pnlContent.Size = new System.Drawing.Size(950, 620);
            this.pnlContent.TabIndex = 3;
            // 
            // lblUserInfo
            // 
            this.lblUserInfo.AutoSize = true;
            this.lblUserInfo.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUserInfo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.lblUserInfo.Location = new System.Drawing.Point(20, 20);
            this.lblUserInfo.Name = "lblUserInfo";
            this.lblUserInfo.Size = new System.Drawing.Size(100, 19);
            this.lblUserInfo.TabIndex = 0;
            this.lblUserInfo.Text = "Người dùng: ";
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatus.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.lblStatus.Location = new System.Drawing.Point(20, 50);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(50, 15);
            this.lblStatus.TabIndex = 1;
            this.lblStatus.Text = "Sẵn sàng";
            // 
            // lblWelcome
            // 
            this.lblWelcome.AutoSize = true;
            this.lblWelcome.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWelcome.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblWelcome.Location = new System.Drawing.Point(50, 50);
            this.lblWelcome.Name = "lblWelcome";
            this.lblWelcome.Size = new System.Drawing.Size(400, 32);
            this.lblWelcome.TabIndex = 0;
            this.lblWelcome.Text = "Chào mừng đến với hệ thống quản lý";
            // 
            // pnlLogo
            // 
            this.pnlLogo.BorderRadius = 0;
            this.pnlLogo.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(84)))), ((int)(((byte)(138)))), ((int)(((byte)(245)))));
            this.pnlLogo.Location = new System.Drawing.Point(0, 0);
            this.pnlLogo.Name = "pnlLogo";
            this.pnlLogo.Size = new System.Drawing.Size(250, 80);
            this.pnlLogo.TabIndex = 0;
            // 
            // lblLogo
            // 
            this.lblLogo.AutoSize = true;
            this.lblLogo.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLogo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.lblLogo.Location = new System.Drawing.Point(20, 25);
            this.lblLogo.Name = "lblLogo";
            this.lblLogo.Size = new System.Drawing.Size(200, 30);
            this.lblLogo.TabIndex = 0;
            this.lblLogo.Text = "HR Management";
            // 
            // btnDashboard
            // 
            this.btnDashboard.BorderRadius = 12;
            this.btnDashboard.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnDashboard.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnDashboard.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnDashboard.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnDashboard.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(20)))));
            this.btnDashboard.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDashboard.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.btnDashboard.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(30)))));
            this.btnDashboard.HoverState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.btnDashboard.Location = new System.Drawing.Point(10, 100);
            this.btnDashboard.Name = "btnDashboard";
            this.btnDashboard.Size = new System.Drawing.Size(230, 45);
            this.btnDashboard.TabIndex = 1;
            this.btnDashboard.Text = "📊 Tổng quan";
            this.btnDashboard.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.btnDashboard.Click += new System.EventHandler(this.btnDashboard_Click);
            // 
            // btnQuanLy
            // 
            this.btnQuanLy.BorderRadius = 12;
            this.btnQuanLy.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnQuanLy.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnQuanLy.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnQuanLy.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnQuanLy.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(20)))));
            this.btnQuanLy.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnQuanLy.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.btnQuanLy.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(30)))));
            this.btnQuanLy.HoverState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.btnQuanLy.Location = new System.Drawing.Point(10, 155);
            this.btnQuanLy.Name = "btnQuanLy";
            this.btnQuanLy.Size = new System.Drawing.Size(230, 45);
            this.btnQuanLy.TabIndex = 2;
            this.btnQuanLy.Text = "👥 Quản lý nhân sự";
            this.btnQuanLy.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.btnQuanLy.Click += new System.EventHandler(this.btnQuanLy_Click);
            // 
            // btnDanhMuc
            // 
            this.btnDanhMuc.BorderRadius = 12;
            this.btnDanhMuc.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnDanhMuc.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnDanhMuc.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnDanhMuc.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnDanhMuc.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(20)))));
            this.btnDanhMuc.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDanhMuc.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.btnDanhMuc.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(30)))));
            this.btnDanhMuc.HoverState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.btnDanhMuc.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDanhMuc.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.btnDanhMuc.Location = new System.Drawing.Point(10, 210);
            this.btnDanhMuc.Name = "btnDanhMuc";
            this.btnDanhMuc.Size = new System.Drawing.Size(230, 45);
            this.btnDanhMuc.TabIndex = 3;
            this.btnDanhMuc.Text = "📋 Danh mục";
            this.btnDanhMuc.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.btnDanhMuc.Click += new System.EventHandler(this.btnDanhMuc_Click);
            // 
            // btnNghiepVu
            // 
            this.btnNghiepVu.BorderRadius = 12;
            this.btnNghiepVu.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnNghiepVu.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnNghiepVu.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnNghiepVu.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnNghiepVu.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(20)))));
            this.btnNghiepVu.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNghiepVu.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.btnNghiepVu.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(30)))));
            this.btnNghiepVu.HoverState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.btnNghiepVu.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNghiepVu.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.btnNghiepVu.Location = new System.Drawing.Point(10, 265);
            this.btnNghiepVu.Name = "btnNghiepVu";
            this.btnNghiepVu.Size = new System.Drawing.Size(230, 45);
            this.btnNghiepVu.TabIndex = 4;
            this.btnNghiepVu.Text = "⚙️ Nghiệp vụ";
            this.btnNghiepVu.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.btnNghiepVu.Click += new System.EventHandler(this.btnNghiepVu_Click);
            // 
            // btnTienLuong
            // 
            this.btnTienLuong.BorderRadius = 12;
            this.btnTienLuong.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnTienLuong.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnTienLuong.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnTienLuong.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnTienLuong.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(20)))));
            this.btnTienLuong.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTienLuong.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.btnTienLuong.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(30)))));
            this.btnTienLuong.HoverState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.btnTienLuong.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTienLuong.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.btnTienLuong.Location = new System.Drawing.Point(10, 320);
            this.btnTienLuong.Name = "btnTienLuong";
            this.btnTienLuong.Size = new System.Drawing.Size(230, 45);
            this.btnTienLuong.TabIndex = 5;
            this.btnTienLuong.Text = "💰 Tiền lương";
            this.btnTienLuong.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.btnTienLuong.Click += new System.EventHandler(this.btnTienLuong_Click);
            // 
            // btnCaNhan
            // 
            this.btnCaNhan.BorderRadius = 12;
            this.btnCaNhan.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnCaNhan.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnCaNhan.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnCaNhan.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnCaNhan.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(20)))));
            this.btnCaNhan.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCaNhan.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.btnCaNhan.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(30)))));
            this.btnCaNhan.HoverState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.btnCaNhan.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCaNhan.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.btnCaNhan.Location = new System.Drawing.Point(10, 375);
            this.btnCaNhan.Name = "btnCaNhan";
            this.btnCaNhan.Size = new System.Drawing.Size(230, 45);
            this.btnCaNhan.TabIndex = 6;
            this.btnCaNhan.Text = "👤 Cá nhân";
            this.btnCaNhan.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.btnCaNhan.Click += new System.EventHandler(this.btnCaNhan_Click);
            // 
            // btnChamCong
            // 
            this.btnChamCong.BorderRadius = 12;
            this.btnChamCong.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnChamCong.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnChamCong.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnChamCong.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnChamCong.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(20)))));
            this.btnChamCong.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnChamCong.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.btnChamCong.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(30)))));
            this.btnChamCong.HoverState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.btnChamCong.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnChamCong.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.btnChamCong.Location = new System.Drawing.Point(10, 430);
            this.btnChamCong.Name = "btnChamCong";
            this.btnChamCong.Size = new System.Drawing.Size(230, 45);
            this.btnChamCong.TabIndex = 7;
            this.btnChamCong.Text = "⏰ Chấm công";
            this.btnChamCong.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.btnChamCong.Click += new System.EventHandler(this.btnChamCong_Click);
            // 
            // btnBaoCao
            // 
            this.btnBaoCao.BorderRadius = 12;
            this.btnBaoCao.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnBaoCao.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnBaoCao.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnBaoCao.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnBaoCao.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(20)))));
            this.btnBaoCao.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBaoCao.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.btnBaoCao.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(30)))));
            this.btnBaoCao.HoverState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.btnBaoCao.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBaoCao.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.btnBaoCao.Location = new System.Drawing.Point(10, 485);
            this.btnBaoCao.Name = "btnBaoCao";
            this.btnBaoCao.Size = new System.Drawing.Size(230, 45);
            this.btnBaoCao.TabIndex = 8;
            this.btnBaoCao.Text = "📊 Báo cáo";
            this.btnBaoCao.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.btnBaoCao.Click += new System.EventHandler(this.btnBaoCao_Click);
            // 
            // btnDangXuat
            // 
            this.btnDangXuat.BorderRadius = 12;
            this.btnDangXuat.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnDangXuat.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnDangXuat.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnDangXuat.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnDangXuat.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(53)))), ((int)(((byte)(69)))));
            this.btnDangXuat.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDangXuat.ForeColor = System.Drawing.Color.White;
            this.btnDangXuat.Location = new System.Drawing.Point(10, 600);
            this.btnDangXuat.Name = "btnDangXuat";
            this.btnDangXuat.Size = new System.Drawing.Size(230, 45);
            this.btnDangXuat.TabIndex = 9;
            this.btnDangXuat.Text = "🚪 Đăng xuất";
            this.btnDangXuat.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.btnDangXuat.Click += new System.EventHandler(this.btnDangXuat_Click);
            // 
            // frmMain
            // 
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.ClientSize = new System.Drawing.Size(1200, 700);
            this.Controls.Add(this.pnlMain);
            this.pnlMain.Controls.Add(this.pnlSidebar);
            this.pnlMain.Controls.Add(this.pnlHeader);
            this.pnlMain.Controls.Add(this.pnlContent);
            this.pnlSidebar.Controls.Add(this.pnlLogo);
            this.pnlSidebar.Controls.Add(this.btnDashboard);
            this.pnlSidebar.Controls.Add(this.btnQuanLy);
            this.pnlSidebar.Controls.Add(this.btnDanhMuc);
            this.pnlSidebar.Controls.Add(this.btnNghiepVu);
            this.pnlSidebar.Controls.Add(this.btnTienLuong);
            this.pnlSidebar.Controls.Add(this.btnCaNhan);
            this.pnlSidebar.Controls.Add(this.btnChamCong);
            this.pnlSidebar.Controls.Add(this.btnBaoCao);
            this.pnlSidebar.Controls.Add(this.btnDangXuat);
            this.pnlLogo.Controls.Add(this.lblLogo);
            this.pnlHeader.Controls.Add(this.lblUserInfo);
            this.pnlHeader.Controls.Add(this.lblStatus);
            this.pnlContent.Controls.Add(this.lblWelcome);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Hệ thống quản lý nhân sự";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.pnlMain.ResumeLayout(false);
            this.pnlSidebar.ResumeLayout(false);
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.pnlContent.ResumeLayout(false);
            this.pnlContent.PerformLayout();
            this.pnlLogo.ResumeLayout(false);
            this.pnlLogo.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2Panel pnlMain;
        private Guna.UI2.WinForms.Guna2Panel pnlSidebar;
        private Guna.UI2.WinForms.Guna2Panel pnlHeader;
        private Guna.UI2.WinForms.Guna2Panel pnlContent;
        private Guna.UI2.WinForms.Guna2Panel pnlLogo;
        private System.Windows.Forms.Label lblLogo;
        private System.Windows.Forms.Label lblUserInfo;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Label lblWelcome;
        private Guna.UI2.WinForms.Guna2Button btnDashboard;
        private Guna.UI2.WinForms.Guna2Button btnQuanLy;
        private Guna.UI2.WinForms.Guna2Button btnDanhMuc;
        private Guna.UI2.WinForms.Guna2Button btnNghiepVu;
        private Guna.UI2.WinForms.Guna2Button btnTienLuong;
        private Guna.UI2.WinForms.Guna2Button btnCaNhan;
        private Guna.UI2.WinForms.Guna2Button btnChamCong;
        private Guna.UI2.WinForms.Guna2Button btnBaoCao;
        private Guna.UI2.WinForms.Guna2Button btnDangXuat;
    }
}
