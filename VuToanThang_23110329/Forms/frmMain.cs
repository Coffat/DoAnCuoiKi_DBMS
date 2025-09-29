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
                    "Phòng ban & Chức vụ",  // ✅ Thêm lại
                    "Lịch phân ca", 
                    "Duyệt Đơn từ" 
                });
            }
            else
            {
                MessageBox.Show($"Bạn không có quyền truy cập chức năng này.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        // ✅ Đã XOÁ: Tab Danh mục (trùng lặp với tab Quản lý)
        private void btnDanhMuc_Click(object sender, EventArgs e)
        {
            // Chuyển hướng sang tab Quản lý
            btnQuanLy_Click(sender, e);
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
                    "Duyệt Đơn từ" 
                });
            }
            else if (userRole == "NhanVien")
            {
                ShowSubMenu("Đơn từ", new string[] 
                { 
                    "Tạo đơn từ",
                    "Xem đơn của tôi"
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
            
            // Mở trực tiếp form thông tin cá nhân
            OpenFormInPanel(new frmThongTinCaNhan());
        }

        private void btnChamCong_Click(object sender, EventArgs e)
        {
            SetActiveButton(btnChamCong);
            lblWelcome.Text = "⏰ Chấm công";
            lblStatus.Text = "Đang chấm công";
            
            // Mở form chấm công trực tiếp
            OpenFormInPanel(new frmChamCong());
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
            btnDanhMuc.Visible = false;  // ✅ Ẩn luôn, không dùng nữa
            btnCaLam.Visible = false;
            btnNghiepVu.Visible = false;
            btnTienLuong.Visible = false;
            btnCaNhan.Visible = false;
            btnChamCong.Visible = false;
            switch (role)
            {
                case "QuanLy": // Giám đốc - có quyền cao nhất
                    btnQuanLy.Visible = true;      // Quản lý nhân sự
                    btnCaLam.Visible = true;       // Ca làm việc
                    btnBaoCao.Visible = true;      // Báo cáo
                    btnChamCong.Visible = true;    // Chấm công
                    btnCaNhan.Visible = true;      // Cá nhân
                    break;

                case "HR": // Nhân viên HR
                    btnQuanLy.Visible = true;      // Quản lý nhân sự
                    btnNghiepVu.Visible = true;    // Nghiệp vụ (Duyệt đơn từ)
                    btnCaLam.Visible = true;       // Ca làm việc
                    btnBaoCao.Visible = true;      // Báo cáo
                    btnChamCong.Visible = true;    // Chấm công
                    btnCaNhan.Visible = true;      // Cá nhân
                    break;

                case "KeToan": // Kế toán
                    btnQuanLy.Visible = true;      // Quản lý nhân sự (để xem nhân viên)
                    btnTienLuong.Visible = true;   // Tiền lương
                    btnBaoCao.Visible = true;      // Báo cáo
                    btnChamCong.Visible = true;    // Chấm công
                    btnCaNhan.Visible = true;      // Cá nhân
                    break;

                case "NhanVien": // Nhân viên thường
                    btnChamCong.Visible = true;    // Chấm công
                    btnNghiepVu.Visible = true;    // Đơn từ
                    btnCaNhan.Visible = true;      // Cá nhân
                    break;

                default: // Mặc định cho nhân viên
                    btnCaNhan.Visible = true;
                    break;
            }
    }            

        private void SetActiveButton(Guna.UI2.WinForms.Guna2Button activeButton)
        {
            // Reset all buttons to default state
            Guna.UI2.WinForms.Guna2Button[] buttons = { btnDashboard, btnQuanLy, btnCaLam, btnNghiepVu, btnTienLuong, btnCaNhan, btnChamCong, btnBaoCao }; // ✅ Đã xoá btnDanhMuc
            
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
                    case "Phòng ban & Chức vụ":
                        OpenFormInPanel(new frmPhongBan_ChucVu());
                        lblStatus.Text = "Đang quản lý phòng ban & chức vụ";
                        break;
                    case "Lịch phân ca":
                        OpenFormInPanel(new frmPhanCa());
                        lblStatus.Text = "Đang quản lý lịch phân ca";
                        break;
                    case "Xem Lịch phân ca":
                        OpenFormInPanel(new frmPhanCa());
                        lblStatus.Text = "Đang xem lịch phân ca";
                        break;
                   case "Ca làm việc":
                       OpenFormInPanel(new frmCaLam(userRole));
                       lblStatus.Text = "Đang quản lý ca làm việc";
                       break;
                   case "Duyệt đơn từ":
                   case "Duyệt Đơn từ":
                       OpenFormInPanel(new frmDuyetDonTu());
                       lblStatus.Text = "Đang duyệt đơn từ";
                       break;
                   case "Tạo đơn từ":
                   case "Gửi Đơn từ":
                       OpenFormInPanel(new frmTaoDonTu());
                       lblStatus.Text = "Đang tạo đơn từ";
                       break;
                   case "Xem đơn của tôi":
                       OpenFormInPanel(new frmXemDonCuaToi());
                       lblStatus.Text = "Đang xem đơn từ của tôi";
                       break;
                   case "Thông tin cá nhân":
                   case "Thông tin của tôi":
                       OpenFormInPanel(new frmThongTinCaNhan());
                       lblStatus.Text = "Đang xem thông tin cá nhân";
                       break;
                   case "Chấm công":
                   case "Check-in / Check-out":
                       OpenFormInPanel(new frmChamCong());
                       lblStatus.Text = "Đang chấm công";
                       break;
                   case "Bảng lương":
                   case "Quản lý Bảng lương":
                       OpenFormInPanel(new frmBangLuong());
                       lblStatus.Text = "Đang quản lý bảng lương";
                       break;
                   case "Báo cáo Nhân sự":
                   case "Bảng công chi tiết":
                   case "Xem Bảng công tháng":
                   case "Báo cáo Lương":
                       // Tạm thời hiển thị form báo cáo đơn giản
                       var frmBaoCao = new Form
                       {
                           Text = functionName,
                           FormBorderStyle = FormBorderStyle.None,
                           Dock = DockStyle.Fill,
                           BackColor = Color.White
                       };
                       var lblBaoCao = new Label
                       {
                           Text = $"📊 {functionName}\\n\\nChức năng đang được phát triển.\\nVui lòng quay lại sau.",
                           Font = new Font("Segoe UI", 16, FontStyle.Bold),
                           ForeColor = Color.FromArgb(94, 148, 255),
                           TextAlign = ContentAlignment.MiddleCenter,
                           Dock = DockStyle.Fill
                       };
                       frmBaoCao.Controls.Add(lblBaoCao);
                       OpenFormInPanel(frmBaoCao);
                       lblStatus.Text = $"Đang xem {functionName}";
                       break;
                   case "Đổi mật khẩu":
                       MessageBox.Show("Chức năng đổi mật khẩu đang được phát triển.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                       break;
                   case "Đăng xuất":
                       btnDangXuat_Click(sender, e);
                       break;
                   case "Thoát":
                       this.Close();
                       break;
                   default:
                       MessageBox.Show($"Chức năng '{functionName}' chưa được triển khai.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
