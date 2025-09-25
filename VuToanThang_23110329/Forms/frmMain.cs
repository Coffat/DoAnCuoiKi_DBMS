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
            lblUserInfo.Text = $"Người dùng: {userRole}";
            lblStatus.Text = "Sẵn sàng";
            lblWelcome.Text = $"Chào mừng {userRole} đến với hệ thống quản lý nhân sự!";
        }

        // Sidebar Navigation Event Handlers
        private void btnDashboard_Click(object sender, EventArgs e)
        {
            SetActiveButton(btnDashboard);
            lblWelcome.Text = "📊 Tổng quan hệ thống";
            lblStatus.Text = "Đang xem tổng quan";
            // TODO: Load dashboard content
        }

        private void btnQuanLy_Click(object sender, EventArgs e)
        {
            SetActiveButton(btnQuanLy);
            lblWelcome.Text = "👥 Quản lý nhân sự";
            lblStatus.Text = "Đang quản lý nhân sự";
            // TODO: Load management forms
        }

        private void btnDanhMuc_Click(object sender, EventArgs e)
        {
            SetActiveButton(btnDanhMuc);
            lblWelcome.Text = "📋 Danh mục hệ thống";
            lblStatus.Text = "Đang quản lý danh mục";
            // TODO: Load category forms
        }

        private void btnNghiepVu_Click(object sender, EventArgs e)
        {
            SetActiveButton(btnNghiepVu);
            lblWelcome.Text = "⚙️ Nghiệp vụ";
            lblStatus.Text = "Đang xử lý nghiệp vụ";
            // TODO: Load business forms
        }

        private void btnTienLuong_Click(object sender, EventArgs e)
        {
            SetActiveButton(btnTienLuong);
            lblWelcome.Text = "💰 Quản lý tiền lương";
            lblStatus.Text = "Đang quản lý lương";
            // TODO: Load salary forms
        }

        private void btnCaNhan_Click(object sender, EventArgs e)
        {
            SetActiveButton(btnCaNhan);
            lblWelcome.Text = "👤 Thông tin cá nhân";
            lblStatus.Text = "Đang xem thông tin cá nhân";
            // TODO: Load personal forms
        }

        private void btnChamCong_Click(object sender, EventArgs e)
        {
            SetActiveButton(btnChamCong);
            lblWelcome.Text = "⏰ Chấm công";
            lblStatus.Text = "Đang chấm công";
            // TODO: Load attendance forms
        }

        private void btnBaoCao_Click(object sender, EventArgs e)
        {
            SetActiveButton(btnBaoCao);
            lblWelcome.Text = "📊 Báo cáo thống kê";
            lblStatus.Text = "Đang xem báo cáo";
            // TODO: Load report forms
        }

        private void btnDangXuat_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc muốn đăng xuất?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.Close();
                new frmLogin().Show();
            }
        }

        private void SetupMenuForRole(string role)
        {
            // Mặc định ẩn tất cả buttons
            btnQuanLy.Visible = false;
            btnDanhMuc.Visible = false;
            btnNghiepVu.Visible = false;
            btnTienLuong.Visible = false;
            btnCaNhan.Visible = false;
            btnChamCong.Visible = false;
            btnBaoCao.Visible = false;

            switch (role.ToLower())
            {
                case "quanly": // Giám đốc - tất cả quyền
                    btnQuanLy.Visible = true;
                    btnDanhMuc.Visible = true;
                    btnNghiepVu.Visible = true;
                    btnTienLuong.Visible = true;
                    btnCaNhan.Visible = true;
                    btnChamCong.Visible = true;
                    btnBaoCao.Visible = true;
                    break;

                case "hr": // HR - quản lý nhân sự
                    btnQuanLy.Visible = true;
                    btnDanhMuc.Visible = true;
                    btnNghiepVu.Visible = true;
                    btnCaNhan.Visible = true;
                    btnChamCong.Visible = true;
                    break;

                case "ketoan": // Kế toán - quản lý lương
                    btnTienLuong.Visible = true;
                    btnBaoCao.Visible = true;
                    btnCaNhan.Visible = true;
                    break;

                case "nhanvien": // Nhân viên - quyền cơ bản
                    btnNghiepVu.Visible = true;
                    btnCaNhan.Visible = true;
                    btnChamCong.Visible = true;
                    break;

                default:
                    // Nếu vai trò không xác định, chỉ hiển thị cá nhân
                    btnCaNhan.Visible = true;
                    break;
            }
        }

        private void SetActiveButton(Guna.UI2.WinForms.Guna2Button activeButton)
        {
            // Reset all buttons to default state
            Guna.UI2.WinForms.Guna2Button[] buttons = { btnDashboard, btnQuanLy, btnDanhMuc, btnNghiepVu, btnTienLuong, btnCaNhan, btnChamCong, btnBaoCao };
            
            foreach (var btn in buttons)
            {
                btn.FillColor = System.Drawing.Color.Transparent;
                btn.ForeColor = System.Drawing.Color.White;
            }

            // Set active button
            activeButton.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(84)))), ((int)(((byte)(138)))), ((int)(((byte)(245)))));
            activeButton.ForeColor = System.Drawing.Color.White;
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
