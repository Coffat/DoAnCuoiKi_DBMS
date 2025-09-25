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
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.mnuHeThong = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuDoiMatKhau = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuDangXuat = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuThoat = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuQuanLy = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuHoSoNhanVien = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuLichPhanCa = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuDuyetDonTu = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuDanhMuc = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuCaLamViec = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuPhongBanChucVu = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuNghiepVu = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuXemLichPhanCa = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuTienLuong = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuQuanLyBangLuong = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuCaNhan = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuThongTinCuaToi = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuGuiDonTu = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuChamCong = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuCheckInOut = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuBaoCao = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuBaoCaoNhanSu = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuBangCongChiTiet = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuBangCongThang = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuBaoCaoLuong = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuTroGiup = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuGioiThieu = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.lblStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblUserInfo = new System.Windows.Forms.ToolStripStatusLabel();
            this.pnlContent = new System.Windows.Forms.Panel();
            this.pnlTitle = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.menuStrip.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuHeThong,
            this.mnuQuanLy,
            this.mnuDanhMuc,
            this.mnuNghiepVu,
            this.mnuTienLuong,
            this.mnuCaNhan,
            this.mnuChamCong,
            this.mnuBaoCao,
            this.mnuTroGiup});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(1200, 24);
            this.menuStrip.TabIndex = 0;
            this.menuStrip.Text = "MenuStrip";
            // 
            // mnuHeThong
            // 
            this.mnuHeThong.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuDoiMatKhau,
            this.mnuDangXuat,
            this.mnuThoat});
            this.mnuHeThong.ForeColor = System.Drawing.Color.White;
            this.mnuHeThong.Name = "mnuHeThong";
            this.mnuHeThong.Size = new System.Drawing.Size(67, 20);
            this.mnuHeThong.Text = "Hệ thống";
            // 
            // mnuDoiMatKhau
            // 
            this.mnuDoiMatKhau.ForeColor = System.Drawing.Color.White;
            this.mnuDoiMatKhau.Name = "mnuDoiMatKhau";
            this.mnuDoiMatKhau.Size = new System.Drawing.Size(180, 22);
            this.mnuDoiMatKhau.Text = "Đổi mật khẩu";
            // 
            // mnuDangXuat
            // 
            this.mnuDangXuat.ForeColor = System.Drawing.Color.White;
            this.mnuDangXuat.Name = "mnuDangXuat";
            this.mnuDangXuat.Size = new System.Drawing.Size(180, 22);
            this.mnuDangXuat.Text = "Đăng xuất";
            // 
            // mnuThoat
            // 
            this.mnuThoat.ForeColor = System.Drawing.Color.White;
            this.mnuThoat.Name = "mnuThoat";
            this.mnuThoat.Size = new System.Drawing.Size(180, 22);
            this.mnuThoat.Text = "Thoát";
            // 
            // mnuQuanLy
            // 
            this.mnuQuanLy.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuHoSoNhanVien,
            this.mnuLichPhanCa,
            this.mnuDuyetDonTu});
            this.mnuQuanLy.ForeColor = System.Drawing.Color.White;
            this.mnuQuanLy.Name = "mnuQuanLy";
            this.mnuQuanLy.Size = new System.Drawing.Size(61, 20);
            this.mnuQuanLy.Text = "Quản lý";
            // 
            // mnuHoSoNhanVien
            // 
            this.mnuHoSoNhanVien.ForeColor = System.Drawing.Color.White;
            this.mnuHoSoNhanVien.Name = "mnuHoSoNhanVien";
            this.mnuHoSoNhanVien.Size = new System.Drawing.Size(180, 22);
            this.mnuHoSoNhanVien.Text = "Hồ sơ Nhân viên";
            // 
            // mnuLichPhanCa
            // 
            this.mnuLichPhanCa.ForeColor = System.Drawing.Color.White;
            this.mnuLichPhanCa.Name = "mnuLichPhanCa";
            this.mnuLichPhanCa.Size = new System.Drawing.Size(180, 22);
            this.mnuLichPhanCa.Text = "Lịch phân ca";
            // 
            // mnuDuyetDonTu
            // 
            this.mnuDuyetDonTu.ForeColor = System.Drawing.Color.White;
            this.mnuDuyetDonTu.Name = "mnuDuyetDonTu";
            this.mnuDuyetDonTu.Size = new System.Drawing.Size(180, 22);
            this.mnuDuyetDonTu.Text = "Duyệt Đơn từ";
            // 
            // mnuDanhMuc
            // 
            this.mnuDanhMuc.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuCaLamViec,
            this.mnuPhongBanChucVu});
            this.mnuDanhMuc.ForeColor = System.Drawing.Color.White;
            this.mnuDanhMuc.Name = "mnuDanhMuc";
            this.mnuDanhMuc.Size = new System.Drawing.Size(71, 20);
            this.mnuDanhMuc.Text = "Danh mục";
            // 
            // mnuCaLamViec
            // 
            this.mnuCaLamViec.ForeColor = System.Drawing.Color.White;
            this.mnuCaLamViec.Name = "mnuCaLamViec";
            this.mnuCaLamViec.Size = new System.Drawing.Size(180, 22);
            this.mnuCaLamViec.Text = "Ca làm việc";
            // 
            // mnuPhongBanChucVu
            // 
            this.mnuPhongBanChucVu.ForeColor = System.Drawing.Color.White;
            this.mnuPhongBanChucVu.Name = "mnuPhongBanChucVu";
            this.mnuPhongBanChucVu.Size = new System.Drawing.Size(180, 22);
            this.mnuPhongBanChucVu.Text = "Phòng ban & Chức vụ";
            // 
            // mnuNghiepVu
            // 
            this.mnuNghiepVu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuXemLichPhanCa,
            this.mnuDuyetDonTu});
            this.mnuNghiepVu.ForeColor = System.Drawing.Color.White;
            this.mnuNghiepVu.Name = "mnuNghiepVu";
            this.mnuNghiepVu.Size = new System.Drawing.Size(71, 20);
            this.mnuNghiepVu.Text = "Nghiệp vụ";
            // 
            // mnuXemLichPhanCa
            // 
            this.mnuXemLichPhanCa.ForeColor = System.Drawing.Color.White;
            this.mnuXemLichPhanCa.Name = "mnuXemLichPhanCa";
            this.mnuXemLichPhanCa.Size = new System.Drawing.Size(180, 22);
            this.mnuXemLichPhanCa.Text = "Xem Lịch phân ca";
            // 
            // mnuTienLuong
            // 
            this.mnuTienLuong.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuQuanLyBangLuong});
            this.mnuTienLuong.ForeColor = System.Drawing.Color.White;
            this.mnuTienLuong.Name = "mnuTienLuong";
            this.mnuTienLuong.Size = new System.Drawing.Size(76, 20);
            this.mnuTienLuong.Text = "Tiền lương";
            // 
            // mnuQuanLyBangLuong
            // 
            this.mnuQuanLyBangLuong.ForeColor = System.Drawing.Color.White;
            this.mnuQuanLyBangLuong.Name = "mnuQuanLyBangLuong";
            this.mnuQuanLyBangLuong.Size = new System.Drawing.Size(180, 22);
            this.mnuQuanLyBangLuong.Text = "Quản lý Bảng lương";
            // 
            // mnuCaNhan
            // 
            this.mnuCaNhan.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuThongTinCuaToi,
            this.mnuGuiDonTu});
            this.mnuCaNhan.ForeColor = System.Drawing.Color.White;
            this.mnuCaNhan.Name = "mnuCaNhan";
            this.mnuCaNhan.Size = new System.Drawing.Size(59, 20);
            this.mnuCaNhan.Text = "Cá nhân";
            // 
            // mnuThongTinCuaToi
            // 
            this.mnuThongTinCuaToi.ForeColor = System.Drawing.Color.White;
            this.mnuThongTinCuaToi.Name = "mnuThongTinCuaToi";
            this.mnuThongTinCuaToi.Size = new System.Drawing.Size(180, 22);
            this.mnuThongTinCuaToi.Text = "Thông tin của tôi";
            // 
            // mnuGuiDonTu
            // 
            this.mnuGuiDonTu.ForeColor = System.Drawing.Color.White;
            this.mnuGuiDonTu.Name = "mnuGuiDonTu";
            this.mnuGuiDonTu.Size = new System.Drawing.Size(180, 22);
            this.mnuGuiDonTu.Text = "Gửi Đơn từ";
            // 
            // mnuChamCong
            // 
            this.mnuChamCong.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuCheckInOut});
            this.mnuChamCong.ForeColor = System.Drawing.Color.White;
            this.mnuChamCong.Name = "mnuChamCong";
            this.mnuChamCong.Size = new System.Drawing.Size(77, 20);
            this.mnuChamCong.Text = "Chấm công";
            // 
            // mnuCheckInOut
            // 
            this.mnuCheckInOut.ForeColor = System.Drawing.Color.White;
            this.mnuCheckInOut.Name = "mnuCheckInOut";
            this.mnuCheckInOut.Size = new System.Drawing.Size(180, 22);
            this.mnuCheckInOut.Text = "Check-in / Check-out";
            // 
            // mnuBaoCao
            // 
            this.mnuBaoCao.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuBaoCaoNhanSu,
            this.mnuBangCongChiTiet,
            this.mnuBangCongThang,
            this.mnuBaoCaoLuong});
            this.mnuBaoCao.ForeColor = System.Drawing.Color.White;
            this.mnuBaoCao.Name = "mnuBaoCao";
            this.mnuBaoCao.Size = new System.Drawing.Size(63, 20);
            this.mnuBaoCao.Text = "Báo cáo";
            // 
            // mnuBaoCaoNhanSu
            // 
            this.mnuBaoCaoNhanSu.ForeColor = System.Drawing.Color.White;
            this.mnuBaoCaoNhanSu.Name = "mnuBaoCaoNhanSu";
            this.mnuBaoCaoNhanSu.Size = new System.Drawing.Size(180, 22);
            this.mnuBaoCaoNhanSu.Text = "Báo cáo Nhân sự";
            // 
            // mnuBangCongChiTiet
            // 
            this.mnuBangCongChiTiet.ForeColor = System.Drawing.Color.White;
            this.mnuBangCongChiTiet.Name = "mnuBangCongChiTiet";
            this.mnuBangCongChiTiet.Size = new System.Drawing.Size(180, 22);
            this.mnuBangCongChiTiet.Text = "Bảng công chi tiết";
            // 
            // mnuBangCongThang
            // 
            this.mnuBangCongThang.ForeColor = System.Drawing.Color.White;
            this.mnuBangCongThang.Name = "mnuBangCongThang";
            this.mnuBangCongThang.Size = new System.Drawing.Size(180, 22);
            this.mnuBangCongThang.Text = "Xem Bảng công tháng";
            // 
            // mnuBaoCaoLuong
            // 
            this.mnuBaoCaoLuong.ForeColor = System.Drawing.Color.White;
            this.mnuBaoCaoLuong.Name = "mnuBaoCaoLuong";
            this.mnuBaoCaoLuong.Size = new System.Drawing.Size(180, 22);
            this.mnuBaoCaoLuong.Text = "Báo cáo Lương";
            // 
            // mnuTroGiup
            // 
            this.mnuTroGiup.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuGioiThieu});
            this.mnuTroGiup.ForeColor = System.Drawing.Color.White;
            this.mnuTroGiup.Name = "mnuTroGiup";
            this.mnuTroGiup.Size = new System.Drawing.Size(64, 20);
            this.mnuTroGiup.Text = "Trợ giúp";
            // 
            // mnuGioiThieu
            // 
            this.mnuGioiThieu.ForeColor = System.Drawing.Color.White;
            this.mnuGioiThieu.Name = "mnuGioiThieu";
            this.mnuGioiThieu.Size = new System.Drawing.Size(180, 22);
            this.mnuGioiThieu.Text = "Giới thiệu";
            // 
            // statusStrip
            // 
            this.statusStrip.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblStatus,
            this.lblUserInfo});
            this.statusStrip.Location = new System.Drawing.Point(0, 671);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(1200, 22);
            this.statusStrip.TabIndex = 1;
            this.statusStrip.Text = "StatusStrip";
            // 
            // lblStatus
            // 
            this.lblStatus.ForeColor = System.Drawing.Color.White;
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(100, 17);
            this.lblStatus.Text = "Sẵn sàng";
            // 
            // lblUserInfo
            // 
            this.lblUserInfo.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.lblUserInfo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(124)))), ((int)(((byte)(77)))), ((int)(((byte)(255)))));
            this.lblUserInfo.Name = "lblUserInfo";
            this.lblUserInfo.Size = new System.Drawing.Size(100, 17);
            this.lblUserInfo.Text = "Chưa đăng nhập";
            // 
            // pnlContent
            // 
            this.pnlContent.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.pnlContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlContent.Location = new System.Drawing.Point(0, 49);
            this.pnlContent.Name = "pnlContent";
            this.pnlContent.Size = new System.Drawing.Size(1200, 622);
            this.pnlContent.TabIndex = 2;
            // 
            // pnlTitle
            // 
            this.pnlTitle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(124)))), ((int)(((byte)(77)))), ((int)(((byte)(255)))));
            this.pnlTitle.Location = new System.Drawing.Point(0, 25);
            this.pnlTitle.Name = "pnlTitle";
            this.pnlTitle.Size = new System.Drawing.Size(1200, 24);
            this.pnlTitle.TabIndex = 3;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(20, 30);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(250, 25);
            this.lblTitle.TabIndex = 4;
            this.lblTitle.Text = "Hệ thống Quản lý Nhân sự Siêu thị Mini";
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.ClientSize = new System.Drawing.Size(1200, 700);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.pnlTitle);
            this.Controls.Add(this.pnlContent);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.menuStrip);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip;
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Hệ thống Quản lý Nhân sự Siêu thị Mini";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem mnuHeThong;
        private System.Windows.Forms.ToolStripMenuItem mnuDoiMatKhau;
        private System.Windows.Forms.ToolStripMenuItem mnuDangXuat;
        private System.Windows.Forms.ToolStripMenuItem mnuThoat;
        private System.Windows.Forms.ToolStripMenuItem mnuQuanLy;
        private System.Windows.Forms.ToolStripMenuItem mnuHoSoNhanVien;
        private System.Windows.Forms.ToolStripMenuItem mnuLichPhanCa;
        private System.Windows.Forms.ToolStripMenuItem mnuDuyetDonTu;
        private System.Windows.Forms.ToolStripMenuItem mnuDanhMuc;
        private System.Windows.Forms.ToolStripMenuItem mnuCaLamViec;
        private System.Windows.Forms.ToolStripMenuItem mnuPhongBanChucVu;
        private System.Windows.Forms.ToolStripMenuItem mnuNghiepVu;
        private System.Windows.Forms.ToolStripMenuItem mnuXemLichPhanCa;
        private System.Windows.Forms.ToolStripMenuItem mnuTienLuong;
        private System.Windows.Forms.ToolStripMenuItem mnuQuanLyBangLuong;
        private System.Windows.Forms.ToolStripMenuItem mnuCaNhan;
        private System.Windows.Forms.ToolStripMenuItem mnuThongTinCuaToi;
        private System.Windows.Forms.ToolStripMenuItem mnuGuiDonTu;
        private System.Windows.Forms.ToolStripMenuItem mnuChamCong;
        private System.Windows.Forms.ToolStripMenuItem mnuCheckInOut;
        private System.Windows.Forms.ToolStripMenuItem mnuBaoCao;
        private System.Windows.Forms.ToolStripMenuItem mnuBaoCaoNhanSu;
        private System.Windows.Forms.ToolStripMenuItem mnuBangCongChiTiet;
        private System.Windows.Forms.ToolStripMenuItem mnuBangCongThang;
        private System.Windows.Forms.ToolStripMenuItem mnuBaoCaoLuong;
        private System.Windows.Forms.ToolStripMenuItem mnuTroGiup;
        private System.Windows.Forms.ToolStripMenuItem mnuGioiThieu;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel lblStatus;
        private System.Windows.Forms.ToolStripStatusLabel lblUserInfo;
        private System.Windows.Forms.Panel pnlContent;
        private System.Windows.Forms.Panel pnlTitle;
        private System.Windows.Forms.Label lblTitle;
    }
}
