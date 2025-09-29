using System;
using System.Drawing;
using System.Windows.Forms;
using Guna.UI2.WinForms;
using System.Data.SqlClient;
using System.Configuration;

namespace VuToanThang_23110329.Forms
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            txtUsername.Focus();
            // Thêm event handlers cho các controls mới
            lblForgotPassword.Click += LblForgotPassword_Click;
            chkRememberMe.CheckedChanged += ChkRememberMe_CheckedChanged;
            // Phím tắt và hiển thị (Enter/Esc xử lý qua KeyDown & btnExit Click)
            // Đảm bảo panel chứa form nhập liệu hiển thị phía trước
            pnlForm.Visible = true;
            pnlForm.BringToFront();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Vui lòng nhập tên đăng nhập và mật khẩu.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var cs = ConfigurationManager.ConnectionStrings["HrDb"];
            if (cs == null)
            {
                MessageBox.Show("Không tìm thấy chuỗi kết nối 'HrDb' trong App.config.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            string connectionString = cs.ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"SELECT nd.MaNguoiDung, nd.VaiTro, nd.KichHoat, nv.MaNV, nv.HoTen 
                                FROM dbo.NguoiDung nd
                                LEFT JOIN dbo.NhanVien nv ON nd.MaNguoiDung = nv.MaNguoiDung
                                WHERE nd.TenDangNhap = @username AND nd.MatKhauHash = @password";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@password", password);

                try
                {
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        bool kichHoat = reader.GetBoolean(2);
                        if (kichHoat)
                        {
                            int maNguoiDung = reader.GetInt32(0);
                            string vaiTro = reader.GetString(1);
                            int maNV = reader.IsDBNull(3) ? 0 : reader.GetInt32(3);
                            string hoTen = reader.IsDBNull(4) ? username : reader.GetString(4);
                            
                            // Lưu thông tin vào session
                            UserSession.SetUser(maNguoiDung, maNV, username, hoTen, vaiTro);
                            
                            // Mở frmMain với vai trò
                            frmMain mainForm = new frmMain(vaiTro);
                            this.Hide();
                            mainForm.ShowDialog();
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("Tài khoản đã bị khóa.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Tên đăng nhập hoặc mật khẩu không đúng.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi kết nối cơ sở dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void txtUsername_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnLogin.PerformClick();
            }
        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnLogin.PerformClick();
            }
        }

        private void LblForgotPassword_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Tính năng quên mật khẩu sẽ được phát triển trong phiên bản tiếp theo.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void ChkRememberMe_CheckedChanged(object sender, EventArgs e)
        {
            // Xử lý logic ghi nhớ đăng nhập
            if (chkRememberMe.Checked)
            {
                // Có thể lưu thông tin đăng nhập vào registry hoặc file config
                MessageBox.Show("Tính năng ghi nhớ đăng nhập đã được kích hoạt.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
