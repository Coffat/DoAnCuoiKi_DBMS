using System;
using System.Drawing;
using System.Windows.Forms;
using VuToanThang_23110329.Data;

namespace VuToanThang_23110329.Forms
{
    public partial class MainForm : Form
    {
        private Form currentChildForm;

        public MainForm()
        {
            InitializeComponent();
            InitializeMainForm();
        }

        private void InitializeMainForm()
        {
            // Set user information
            SetUserInfo();
            
            // Configure menu based on user role
            ConfigureMenuByRole();
            
            // Test database connection
            TestDatabaseConnection();
        }

        private void SetUserInfo()
        {
            if (CurrentUser.User != null)
            {
                lblUserName.Text = CurrentUser.Employee?.HoTen ?? CurrentUser.User.TenDangNhap;
                lblUserRole.Text = GetRoleDisplayName(CurrentUser.User.VaiTro);
                
                // Set welcome message
                lblWelcome.Text = $"Chào mừng {lblUserName.Text}!";
            }
        }

        private string GetRoleDisplayName(string role)
        {
            switch (role?.ToUpper())
            {
                case "HR": return "Nhân sự";
                case "QUANLY": return "Quản lý";
                case "KETOAN": return "Kế toán";
                case "NHANVIEN": return "Nhân viên";
                default: return role;
            }
        }

        private void ConfigureMenuByRole()
        {
            // Hide all buttons first
            HideAllMenuButtons();

            // Show buttons based on role
            btnDashboard.Visible = true; // Always visible
            btnLogout.Visible = true; // Always visible

            if (CurrentUser.IsHR)
            {
                // HR can access everything
                ShowAllMenuButtons();
            }
            else if (CurrentUser.IsQuanLy)
            {
                // Manager can access schedule, attendance, and approval
                btnLichPhanCa.Visible = true;
                btnChamCong.Visible = true;
                btnDonTu.Visible = true;
                btnBaoCaoNhanSu.Visible = true;
            }
            else if (CurrentUser.IsKeToan)
            {
                // Accountant can access payroll functions
                btnTinhLuong.Visible = true;
                btnBangLuong.Visible = true;
                btnBaoCaoLuong.Visible = true;
            }
            else if (CurrentUser.IsNhanVien)
            {
                // Employee can only view their own data
                btnLichPhanCa.Visible = true;
                btnChamCong.Visible = true;
                btnDonTu.Visible = true;
                btnPhieuLuong.Visible = true;
            }
        }

        private void HideAllMenuButtons()
        {
            btnNhanVien.Visible = false;
            btnCaLam.Visible = false;
            btnLichPhanCa.Visible = false;
            btnChamCong.Visible = false;
            btnDonTu.Visible = false;
            btnTinhLuong.Visible = false;
            btnBangLuong.Visible = false;
            btnPhieuLuong.Visible = false;
            btnBaoCaoNhanSu.Visible = false;
            btnBaoCaoLuong.Visible = false;
        }

        private void ShowAllMenuButtons()
        {
            btnNhanVien.Visible = true;
            btnCaLam.Visible = true;
            btnLichPhanCa.Visible = true;
            btnChamCong.Visible = true;
            btnDonTu.Visible = true;
            btnTinhLuong.Visible = true;
            btnBangLuong.Visible = true;
            btnPhieuLuong.Visible = true;
            btnBaoCaoNhanSu.Visible = true;
            btnBaoCaoLuong.Visible = true;
        }

        private void TestDatabaseConnection()
        {
            try
            {
                if (!SqlHelper.TestConnection())
                {
                    ShowMessage("Không thể kết nối đến cơ sở dữ liệu!", "Cảnh báo", MessageBoxIcon.Warning);
                }
                
                // Test form creation
                TestFormCreation();
            }
            catch (Exception ex)
            {
                ShowMessage($"Lỗi kết nối cơ sở dữ liệu: {ex.Message}", "Lỗi", MessageBoxIcon.Error);
            }
        }

        private void TestFormCreation()
        {
            try
            {
                // Test creating each form to see if there are any issues
                var testForms = new Dictionary<string, Func<Form>>
                {
                    {"NhanVienForm", () => new NhanVienForm()},
                    {"CaLamForm", () => new CaLamForm()},
                    {"LichPhanCaForm", () => new LichPhanCaForm()},
                    {"ChamCongForm", () => new ChamCongForm()},
                    {"BaoCaoLuongForm", () => new BaoCaoLuongForm()},
                    {"BaoCaoNhanSuForm", () => new BaoCaoNhanSuForm()},
                    {"PhieuLuongForm", () => new PhieuLuongForm()},
                    {"TinhLuongForm", () => new TinhLuongForm()},
                    {"BangLuongForm", () => new BangLuongForm()},
                    {"DonTuNVForm", () => new DonTuNVForm()},
                    {"DonTuSMForm", () => new DonTuSMForm()},
                    {"DonTuHRForm", () => new DonTuHRForm()}
                };

                foreach (var kvp in testForms)
                {
                    try
                    {
                        var form = kvp.Value();
                        form.Dispose(); // Dispose immediately after creation test
                    }
                    catch (Exception ex)
                    {
                        ShowMessage($"Lỗi tạo {kvp.Key}: {ex.Message}", "Lỗi Form", MessageBoxIcon.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                ShowMessage($"Lỗi test form: {ex.Message}", "Lỗi", MessageBoxIcon.Error);
            }
        }

        private void MenuButton_Click(object sender, EventArgs e)
        {
            var button = sender as Guna.UI2.WinForms.Guna2Button;
            if (button == null) return;

            // Reset all button colors
            ResetMenuButtonColors();
            
            // Set active button color
            button.FillColor = Color.FromArgb(124, 77, 255);
            button.Font = new Font("Segoe UI", 10.5F, FontStyle.Bold);

            // Open corresponding form
            OpenChildForm(button);
        }

        private void ResetMenuButtonColors()
        {
            var buttons = new[] { btnDashboard, btnNhanVien, btnCaLam, btnLichPhanCa, btnChamCong, 
                                btnDonTu, btnTinhLuong, btnBangLuong, btnPhieuLuong, btnBaoCaoNhanSu, btnBaoCaoLuong };

            foreach (var btn in buttons)
            {
                btn.FillColor = Color.Transparent;
                btn.Font = new Font("Segoe UI", 10.5F, FontStyle.Regular);
            }
        }

        private void OpenChildForm(Guna.UI2.WinForms.Guna2Button menuButton)
        {
            Form childForm = null;
            string formTitle = "";

            try
            {
                switch (menuButton.Name)
                {
                    case "btnDashboard":
                        // Show dashboard content in main panel
                        ShowDashboard();
                        formTitle = "Trang chủ";
                        return;

                    case "btnNhanVien":
                        try
                        {
                            childForm = new NhanVienForm();
                            formTitle = "Quản lý nhân viên";
                        }
                        catch (Exception ex)
                        {
                            ShowMessage($"Lỗi tạo form NhanVien: {ex.Message}", "Lỗi", MessageBoxIcon.Error);
                            return;
                        }
                        break;

                    case "btnCaLam":
                        try
                        {
                            childForm = new CaLamForm();
                            formTitle = "Quản lý ca làm";
                        }
                        catch (Exception ex)
                        {
                            ShowMessage($"Lỗi tạo form CaLam: {ex.Message}", "Lỗi", MessageBoxIcon.Error);
                            return;
                        }
                        break;

                    case "btnLichPhanCa":
                        try
                        {
                            childForm = new LichPhanCaForm();
                            formTitle = "Lịch phân ca";
                        }
                        catch (Exception ex)
                        {
                            ShowMessage($"Lỗi tạo form LichPhanCa: {ex.Message}", "Lỗi", MessageBoxIcon.Error);
                            return;
                        }
                        break;

                    case "btnChamCong":
                        try
                        {
                            childForm = new ChamCongForm();
                            formTitle = "Chấm công";
                        }
                        catch (Exception ex)
                        {
                            ShowMessage($"Lỗi tạo form ChamCong: {ex.Message}", "Lỗi", MessageBoxIcon.Error);
                            return;
                        }
                        break;

                    case "btnDonTu":
                        if (CurrentUser.IsNhanVien)
                            childForm = new DonTuNVForm();
                        else if (CurrentUser.IsQuanLy)
                            childForm = new DonTuSMForm();
                        else
                            childForm = new DonTuHRForm();
                        formTitle = "Quản lý đơn từ";
                        break;

                    case "btnTinhLuong":
                        childForm = new TinhLuongForm();
                        formTitle = "Tính lương";
                        break;

                    case "btnBangLuong":
                        childForm = new BangLuongForm();
                        formTitle = "Bảng lương";
                        break;

                    case "btnPhieuLuong":
                        childForm = new PhieuLuongForm();
                        formTitle = "Phiếu lương";
                        break;

                    case "btnBaoCaoNhanSu":
                        childForm = new BaoCaoNhanSuForm();
                        formTitle = "Báo cáo nhân sự";
                        break;

                    case "btnBaoCaoLuong":
                        try
                        {
                            childForm = new BaoCaoLuongForm();
                            formTitle = "Báo cáo lương";
                        }
                        catch (Exception ex)
                        {
                            ShowMessage($"Lỗi tạo form BaoCaoLuong: {ex.Message}", "Lỗi", MessageBoxIcon.Error);
                            return;
                        }
                        break;

                    default:
                        ShowMessage($"Không tìm thấy form cho chức năng: {menuButton.Name}", "Lỗi", MessageBoxIcon.Error);
                        return;
                }

                if (childForm != null)
                {
                    OpenFormInPanel(childForm, formTitle);
                }
            }
            catch (Exception ex)
            {
                ShowMessage($"Lỗi mở form: {ex.Message}", "Lỗi", MessageBoxIcon.Error);
            }
        }

        private void ShowDashboard()
        {
            // Clear current content
            pnlContent.Controls.Clear();
            
            // Create dashboard content
            var dashboardPanel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.FromArgb(50, 50, 50),
                Padding = new Padding(20)
            };

            var welcomeLabel = new Label
            {
                Text = $"Chào mừng {lblUserName.Text}!",
                Font = new Font("Segoe UI", 24, FontStyle.Bold),
                ForeColor = Color.White,
                AutoSize = true,
                Location = new Point(50, 50)
            };

            var roleLabel = new Label
            {
                Text = $"Vai trò: {lblUserRole.Text}",
                Font = new Font("Segoe UI", 14),
                ForeColor = Color.Gray,
                AutoSize = true,
                Location = new Point(50, 100)
            };

            var timeLabel = new Label
            {
                Text = $"Thời gian: {DateTime.Now:dd/MM/yyyy HH:mm:ss}",
                Font = new Font("Segoe UI", 12),
                ForeColor = Color.LightGray,
                AutoSize = true,
                Location = new Point(50, 130)
            };

            dashboardPanel.Controls.AddRange(new Control[] { welcomeLabel, roleLabel, timeLabel });
            pnlContent.Controls.Add(dashboardPanel);
            
            lblCurrentForm.Text = "Trang chủ";
        }

        private void OpenFormInPanel(Form childForm, string title)
        {
            // Close current child form
            currentChildForm?.Close();

            // Configure child form
            currentChildForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;

            // Clear and add to content panel
            pnlContent.Controls.Clear();
            pnlContent.Controls.Add(childForm);
            
            // Update title
            lblCurrentForm.Text = title;
            
            // Show form
            childForm.Show();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnMaximize_Click(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Maximized)
            {
                this.WindowState = FormWindowState.Normal;
                btnMaximize.Text = "□";
            }
            else
            {
                this.WindowState = FormWindowState.Maximized;
                btnMaximize.Text = "❐";
            }
        }

        private void btnMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Bạn có chắc chắn muốn đăng xuất?", "Xác nhận", 
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                // Clear current user
                CurrentUser.Clear();
                
                // Close current form and show login
                this.Hide();
                var loginForm = new LoginForm();
                loginForm.ShowDialog();
                this.Close();
            }
        }

        private void ShowMessage(string message, string title, MessageBoxIcon icon)
        {
            MessageBox.Show(message, title, MessageBoxButtons.OK, icon);
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            currentChildForm?.Close();
            base.OnFormClosed(e);
        }
    }

}
