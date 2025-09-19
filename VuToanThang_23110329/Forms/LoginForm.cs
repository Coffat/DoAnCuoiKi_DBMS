using System;
using System.Windows.Forms;
using VuToanThang_23110329.Models;
using VuToanThang_23110329.Repositories;

namespace VuToanThang_23110329.Forms
{
    public partial class LoginForm : Form
    {
        private readonly AuthRepository _authRepository;

        public LoginForm()
        {
            InitializeComponent();
            _authRepository = new AuthRepository();
            InitializeForm();
        }

        private void InitializeForm()
        {
            // Set form properties
            this.KeyPreview = true;
            
            // Set default focus
            txtUsername.Focus();
            
            // Load saved credentials if remember me was checked
            LoadSavedCredentials();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            PerformLogin();
        }

        private void LoginForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                PerformLogin();
            }
            else if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void PerformLogin()
        {
            // Validate input
            if (string.IsNullOrWhiteSpace(txtUsername.Text))
            {
                ShowMessage("Vui lòng nhập tên đăng nhập!", "Thông báo", MessageBoxIcon.Warning);
                txtUsername.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                ShowMessage("Vui lòng nhập mật khẩu!", "Thông báo", MessageBoxIcon.Warning);
                txtPassword.Focus();
                return;
            }

            // Disable login button during authentication
            btnLogin.Enabled = false;
            btnLogin.Text = "Đang đăng nhập...";

            try
            {
                // Perform authentication
                var result = _authRepository.Login(txtUsername.Text.Trim(), txtPassword.Text);

                if (result.Success)
                {
                    // Save credentials if remember me is checked
                    if (chkRememberMe.Checked)
                    {
                        SaveCredentials();
                    }
                    else
                    {
                        ClearSavedCredentials();
                    }

                    // Store current user information in a static class for global access
                    VuToanThang_23110329.Data.CurrentUser.SetUser(result.User, result.Employee);

                    ShowMessage(result.Message, "Thành công", MessageBoxIcon.Information);

                    // Open main form
                    var mainForm = new MainForm();
                    this.Hide();
                    mainForm.ShowDialog();
                    this.Close();
                }
                else
                {
                    ShowMessage(result.Message, "Lỗi đăng nhập", MessageBoxIcon.Error);
                    txtPassword.Clear();
                    txtUsername.Focus();
                }
            }
            catch (Exception ex)
            {
                ShowMessage($"Lỗi kết nối: {ex.Message}", "Lỗi", MessageBoxIcon.Error);
            }
            finally
            {
                // Re-enable login button
                btnLogin.Enabled = true;
                btnLogin.Text = "ĐĂNG NHẬP";
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void ShowMessage(string message, string title, MessageBoxIcon icon)
        {
            MessageBox.Show(message, title, MessageBoxButtons.OK, icon);
        }

        private void SaveCredentials()
        {
            try
            {
                Properties.Settings.Default.SavedUsername = txtUsername.Text;
                Properties.Settings.Default.SavedPassword = txtPassword.Text; // In production, this should be encrypted
                Properties.Settings.Default.RememberMe = chkRememberMe.Checked;
                Properties.Settings.Default.Save();
            }
            catch (Exception ex)
            {
                // Log error but don't show to user
                System.Diagnostics.Debug.WriteLine($"Error saving credentials: {ex.Message}");
            }
        }

        private void LoadSavedCredentials()
        {
            try
            {
                if (Properties.Settings.Default.RememberMe)
                {
                    txtUsername.Text = Properties.Settings.Default.SavedUsername;
                    txtPassword.Text = Properties.Settings.Default.SavedPassword;
                    chkRememberMe.Checked = true;
                }
            }
            catch (Exception ex)
            {
                // Log error but don't show to user
                System.Diagnostics.Debug.WriteLine($"Error loading credentials: {ex.Message}");
            }
        }

        private void ClearSavedCredentials()
        {
            try
            {
                Properties.Settings.Default.SavedUsername = "";
                Properties.Settings.Default.SavedPassword = "";
                Properties.Settings.Default.RememberMe = false;
                Properties.Settings.Default.Save();
            }
            catch (Exception ex)
            {
                // Log error but don't show to user
                System.Diagnostics.Debug.WriteLine($"Error clearing credentials: {ex.Message}");
            }
        }
    }

}
