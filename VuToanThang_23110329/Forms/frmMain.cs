using System;
using System.Drawing;
using System.Windows.Forms;
using Guna.UI2.WinForms;

namespace VuToanThang_23110329.Forms
{
    public partial class frmMain : Form
    {
        private string userRole;

        public frmMain()
        {
            InitializeComponent();
        }

        public frmMain(string vaiTro)
        {
            InitializeComponent();
            userRole = vaiTro;
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                if (MessageBox.Show("Bạn có chắc muốn thoát ứng dụng?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    e.Cancel = true;
                }
            }
            base.OnFormClosing(e);
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            SetupMenuForRole(userRole);
        }

        private void mnuHeThong_DoiMatKhau_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Chức năng đổi mật khẩu đang được phát triển.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void mnuHeThong_DangXuat_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc muốn đăng xuất?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.Close();
                new frmLogin().Show();
            }
        }

        private void mnuHeThong_Thoat_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc muốn thoát ứng dụng?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void mnuQuanLy_HoSoNhanVien_Click(object sender, EventArgs e)
        {
            // OpenFormInPanel(new frmNhanVien());
            MessageBox.Show("Mở form quản lý hồ sơ nhân viên.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void mnuQuanLy_LichPhanCa_Click(object sender, EventArgs e)
        {
            // OpenFormInPanel(new frmPhanCa());
            MessageBox.Show("Mở form quản lý lịch phân ca.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void mnuQuanLy_DuyetDonTu_Click(object sender, EventArgs e)
        {
            // OpenFormInPanel(new frmDuyetDonTu());
            MessageBox.Show("Mở form duyệt đơn từ.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void mnuDanhMuc_CaLamViec_Click(object sender, EventArgs e)
        {
            // OpenFormInPanel(new frmCaLam());
            MessageBox.Show("Mở form quản lý ca làm việc.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void mnuDanhMuc_PhongBanChucVu_Click(object sender, EventArgs e)
        {
            // OpenFormInPanel(new frmPhongBan_ChucVu());
            MessageBox.Show("Mở form quản lý phòng ban và chức vụ.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void mnuNghiepVu_XemLichPhanCa_Click(object sender, EventArgs e)
        {
            // OpenFormInPanel(new frmXemLichPhanCa());
            MessageBox.Show("Mở form xem lịch phân ca.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void mnuNghiepVu_DuyetDonTu_Click(object sender, EventArgs e)
        {
            // OpenFormInPanel(new frmDuyetDonTu());
            MessageBox.Show("Mở form duyệt đơn từ.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void mnuTienLuong_QuanLyBangLuong_Click(object sender, EventArgs e)
        {
            // OpenFormInPanel(new frmBangLuong());
            MessageBox.Show("Mở form quản lý bảng lương.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void mnuCaNhan_ThongTinCuaToi_Click(object sender, EventArgs e)
        {
            // OpenFormInPanel(new frmThongTinCaNhan());
            MessageBox.Show("Mở form thông tin cá nhân.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void mnuCaNhan_GuiDonTu_Click(object sender, EventArgs e)
        {
            // OpenFormInPanel(new frmTaoDonTu());
            MessageBox.Show("Mở form gửi đơn từ.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void mnuChamCong_CheckInOut_Click(object sender, EventArgs e)
        {
            // OpenFormInPanel(new frmChamCong());
            MessageBox.Show("Mở form chấm công check in/out.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void mnuBaoCao_BaoCaoNhanSu_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Mở báo cáo nhân sự.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void mnuBaoCao_BangCongChiTiet_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Mở báo cáo bảng công chi tiết.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void mnuBaoCao_BangCongThang_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Mở báo cáo bảng công tháng.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void mnuBaoCao_BaoCaoLuong_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Mở báo cáo lương.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void mnuTroGiup_GioiThieu_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Hệ thống quản lý nhân sự siêu thị mini\nPhiên bản 1.0\nĐồ án cuối kỳ DBMS", "Giới thiệu", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void SetupMenuForRole(string role)
        {
            // Mặc định ẩn tất cả menu con
            mnuQuanLy.Visible = false;
            mnuDanhMuc.Visible = false;
            mnuNghiepVu.Visible = false;
            mnuTienLuong.Visible = false;
            mnuCaNhan.Visible = false;
            mnuChamCong.Visible = false;
            mnuBaoCao.Visible = false;

            switch (role.ToLower())
            {
                case "quanly": // Giám đốc - tất cả quyền
                    mnuQuanLy.Visible = true;
                    mnuDanhMuc.Visible = true;
                    mnuNghiepVu.Visible = true;
                    mnuTienLuong.Visible = true;
                    mnuCaNhan.Visible = true;
                    mnuChamCong.Visible = true;
                    mnuBaoCao.Visible = true;
                    break;

                case "hr": // HR - quản lý nhân sự
                    mnuQuanLy.Visible = true;
                    mnuDanhMuc.Visible = true;
                    mnuNghiepVu.Visible = true;
                    mnuCaNhan.Visible = true;
                    mnuChamCong.Visible = true;
                    break;

                case "ketoan": // Kế toán - quản lý lương
                    mnuTienLuong.Visible = true;
                    mnuBaoCao.Visible = true;
                    mnuCaNhan.Visible = true;
                    break;

                case "nhanvien": // Nhân viên - quyền cơ bản
                    mnuNghiepVu.Visible = true;
                    mnuCaNhan.Visible = true;
                    mnuChamCong.Visible = true;
                    break;

                default:
                    // Nếu vai trò không xác định, chỉ hiển thị cá nhân
                    mnuCaNhan.Visible = true;
                    break;
            }
        }

        private void OpenFormInPanel(Form childForm)
        {
            pnlContent.Controls.Clear();
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            pnlContent.Controls.Add(childForm);
            childForm.Show();
        }
    }
}
