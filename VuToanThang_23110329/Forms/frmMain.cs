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
            
            // Hiển thị menu con cho HR, QuanLy và KeToan
            if (userRole == "HR" || userRole == "QuanLy" || userRole == "KeToan")
            {
                ShowSubMenu("Quản lý nhân sự", new string[] 
                { 
                    "Hồ sơ Nhân viên", 
                    "Lịch phân ca", 
                    "Duyệt Đơn từ" 
                });
            }
            else
            {
                MessageBox.Show($"Bạn không có quyền truy cập chức năng này.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnDanhMuc_Click(object sender, EventArgs e)
        {
            SetActiveButton(btnDanhMuc);
            lblWelcome.Text = "📋 Danh mục hệ thống";
            lblStatus.Text = "Đang quản lý danh mục";
            
            // Hiển thị menu con cho HR
            if (userRole == "HR")
            {
                ShowSubMenu("Danh mục", new string[] 
                { 
                    "Phòng ban & Chức vụ" 
                });
            }
        }

        private void btnCaLam_Click(object sender, EventArgs e)
        {
            SetActiveButton(btnCaLam);
            lblWelcome.Text = "⏰ Quản lý ca làm việc";
            lblStatus.Text = "Đang quản lý ca làm việc";
            
            OpenFormInPanel(new frmCaLam(userRole));
        }

        private void btnNghiepVu_Click(object sender, EventArgs e)
        {
            SetActiveButton(btnNghiepVu);
            lblWelcome.Text = "⚙️ Nghiệp vụ";
            lblStatus.Text = "Đang xử lý nghiệp vụ";
            
            // Hiển thị menu con theo vai trò
            if (userRole == "QuanLy")
            {
                ShowSubMenu("Nghiệp vụ Quản lý", new string[] 
                { 
                    "Xem Lịch phân ca", 
                    "Duyệt Đơn từ" 
                });
            }
        }

        private void btnTienLuong_Click(object sender, EventArgs e)
        {
            SetActiveButton(btnTienLuong);
            lblWelcome.Text = "💰 Quản lý tiền lương";
            lblStatus.Text = "Đang quản lý lương";
            
            // Hiển thị menu con cho Kế toán
            if (userRole == "KeToan")
            {
                ShowSubMenu("Tiền lương", new string[] 
                { 
                    "Quản lý Bảng lương" 
                });
            }
        }

        private void btnCaNhan_Click(object sender, EventArgs e)
        {
            SetActiveButton(btnCaNhan);
            lblWelcome.Text = "👤 Thông tin cá nhân";
            lblStatus.Text = "Đang xem thông tin cá nhân";
            
            // Hiển thị menu con theo vai trò
            if (userRole == "NhanVien")
            {
                ShowSubMenu("Cá nhân", new string[] 
                { 
                    "Thông tin của tôi", 
                    "Gửi Đơn từ" 
                });
            }
            else
            {
                ShowSubMenu("Cá nhân", new string[] 
                { 
                    "Đổi mật khẩu", 
                    "Đăng xuất", 
                    "Thoát" 
                });
            }
        }

        private void btnChamCong_Click(object sender, EventArgs e)
        {
            SetActiveButton(btnChamCong);
            lblWelcome.Text = "⏰ Chấm công";
            lblStatus.Text = "Đang chấm công";
            
            // Hiển thị menu con cho Nhân viên
            if (userRole == "NhanVien")
            {
                ShowSubMenu("Chấm công", new string[] 
                { 
                    "Check-in / Check-out" 
                });
            }
        }

        private void btnBaoCao_Click(object sender, EventArgs e)
        {
            SetActiveButton(btnBaoCao);
            lblWelcome.Text = "📊 Báo cáo thống kê";
            lblStatus.Text = "Đang xem báo cáo";
            
            // Hiển thị menu con theo vai trò
            if (userRole == "HR")
            {
                ShowSubMenu("Báo cáo HR", new string[] 
                { 
                    "Báo cáo Nhân sự", 
                    "Bảng công chi tiết" 
                });
            }
            else if (userRole == "KeToan")
            {
                ShowSubMenu("Báo cáo Kế toán", new string[] 
                { 
                    "Xem Bảng công tháng", 
                    "Báo cáo Lương" 
                });
            }
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
            btnCaLam.Visible = false;
            btnNghiepVu.Visible = false;
            btnTienLuong.Visible = false;
            btnCaNhan.Visible = false;
            btnChamCong.Visible = false;
            btnBaoCao.Visible = false;

            switch (role)
            {
                case "QuanLy": // Giám đốc - có quyền cao nhất
                    btnQuanLy.Visible = true;      // Quản lý nhân sự
                    btnDanhMuc.Visible = true;     // Danh mục
                    btnCaLam.Visible = true;       // Quản lý ca làm
                    btnNghiepVu.Visible = true;    // Nghiệp vụ
                    btnTienLuong.Visible = true;   // Tiền lương
                    btnBaoCao.Visible = true;      // Báo cáo
                    btnCaNhan.Visible = true;      // Cá nhân
                    break;
                    
                case "HR": // Trưởng phòng nhân sự
                    btnQuanLy.Visible = true;      // Quản lý nhân sự
                    btnDanhMuc.Visible = true;     // Danh mục
                    btnCaLam.Visible = true;       // Quản lý ca làm
                    btnBaoCao.Visible = true;      // Báo cáo
                    btnCaNhan.Visible = true;      // Cá nhân
                    break;
                    
                case "KeToan": // Kế toán
                    btnQuanLy.Visible = true;      // Quản lý nhân sự (để xem nhân viên)
                    btnTienLuong.Visible = true;   // Tiền lương
                    btnBaoCao.Visible = true;      // Báo cáo
                    btnCaNhan.Visible = true;      // Cá nhân
                    break;
                    
                case "NhanVien": // Nhân viên thường
                    btnCaNhan.Visible = true;      // Cá nhân
                    btnChamCong.Visible = true;    // Chấm công
                    break;
                    
                default: // Mặc định cho nhân viên
                    btnCaNhan.Visible = true;
                    break;
            }
        }

        private void SetActiveButton(Guna.UI2.WinForms.Guna2Button activeButton)
        {
            // Reset all buttons to default state
            Guna.UI2.WinForms.Guna2Button[] buttons = { btnDashboard, btnQuanLy, btnDanhMuc, btnCaLam, btnNghiepVu, btnTienLuong, btnCaNhan, btnChamCong, btnBaoCao };
            
            foreach (var btn in buttons)
            {
                btn.FillColor = System.Drawing.Color.White;
                btn.ForeColor = System.Drawing.Color.Black; // Black text
            }

            // Set active button
            activeButton.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(53)))), ((int)(((byte)(69))))); // Red background
            activeButton.ForeColor = System.Drawing.Color.White; // White text
        }

        private void ShowSubMenu(string title, string[] menuItems)
        {
            // Dispose controls cũ trước khi xóa
            foreach (Control control in pnlContent.Controls)
            {
                if (control is IDisposable disposable)
                {
                    disposable.Dispose();
                }
            }
            pnlContent.Controls.Clear();
            
            // Tạo label tiêu đề
            Label lblTitle = new Label();
            lblTitle.Name = "lblSubMenuTitle";
            lblTitle.Text = title;
            lblTitle.Font = new Font("Segoe UI", 16, FontStyle.Bold);
            lblTitle.ForeColor = Color.FromArgb(64, 64, 64);
            lblTitle.Location = new Point(50, 50);
            lblTitle.AutoSize = true;
            pnlContent.Controls.Add(lblTitle);
            
            // Tạo các button menu con
            int yPos = 100;
            for (int i = 0; i < menuItems.Length; i++)
            {
                string item = menuItems[i];
                Guna.UI2.WinForms.Guna2Button btn = new Guna.UI2.WinForms.Guna2Button();
                btn.Name = $"btnSubMenu_{i}";
                btn.Text = item;
                btn.Font = new Font("Segoe UI", 12, FontStyle.Regular);
                btn.ForeColor = Color.FromArgb(64, 64, 64);
                btn.FillColor = Color.FromArgb(248, 249, 250);
                btn.BorderRadius = 8;
                btn.Size = new Size(300, 45);
                btn.Location = new Point(50, yPos);
                btn.TextAlign = HorizontalAlignment.Left;
                btn.Tag = item; // Lưu tên chức năng vào Tag
                btn.Click += BtnSubMenu_Click;
                pnlContent.Controls.Add(btn);
                yPos += 60;
            }
        }

        private void BtnSubMenu_Click(object sender, EventArgs e)
        {
            if (sender is Guna.UI2.WinForms.Guna2Button btn && btn.Tag is string functionName)
            {
                switch (functionName)
                {
                    case "Hồ sơ Nhân viên":
                        OpenFormInPanel(new frmNhanVien(userRole));
                        lblStatus.Text = "Đang quản lý hồ sơ nhân viên";
                        break;
                    case "Ca làm việc":
                        OpenFormInPanel(new frmCaLam(userRole));
                        lblStatus.Text = "Đang quản lý ca làm việc";
                        break;
                    case "Phòng ban & Chức vụ":
                        MessageBox.Show($"Mở chức năng: {functionName}", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        break;
                    default:
                        MessageBox.Show($"Mở chức năng: {functionName}", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        break;
                }
            }
        }

        private void OpenFormInPanel(Form childForm)
        {
            // Dispose controls cũ trước khi xóa
            foreach (Control control in pnlContent.Controls)
            {
                if (control is IDisposable disposable)
                {
                    disposable.Dispose();
                }
            }
            pnlContent.Controls.Clear();
            
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            pnlContent.Controls.Add(childForm);
            childForm.Show();
        }
    }
}
