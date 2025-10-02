using System;
using System.Drawing;
using System.Windows.Forms;
using Guna.UI2.WinForms;
using System.Data.SqlClient;
using System.Configuration;
using System.Linq;

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

            // ============================================
            // AUTHENTICATION Ở DATABASE LEVEL
            // ============================================
            // Sử dụng connection cố định, kiểm tra user/pass với bảng NguoiDung
            
            string connectionString = ConfigurationManager.ConnectionStrings["HrDb"].ConnectionString;

            // ============================================
            // KIỂM TRA THÔNG TIN ĐĂNG NHẬP VỚI BẢNG NGUOIDUNG
            // ============================================
            // Kết nối với connection cố định, kiểm tra user/pass với database
            
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    
                    // Kiểm tra username và password với bảng NguoiDung
                    // Giả sử password được hash bằng SHA256 hoặc plain text (tùy thiết kế)
                    string query = @"SELECT nd.MaNguoiDung, nd.VaiTro, nd.KichHoat, nv.MaNV, nv.HoTen, nd.MatKhauHash
                                    FROM dbo.NguoiDung nd
                                    LEFT JOIN dbo.NhanVien nv ON nd.MaNguoiDung = nv.MaNguoiDung
                                    WHERE nd.TenDangNhap = @username AND nd.KichHoat = 1";
                    
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@username", username);

                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        string matKhauHash = reader.GetString(5);
                        
                        // ✅ KIỂM TRA PASSWORD
                        // Nếu password trong DB là plain text, so sánh trực tiếp
                        // Nếu là hash, cần hash password input rồi so sánh
                        bool passwordValid = (matKhauHash == password) || 
                                            (System.Security.Cryptography.SHA256.Create()
                                             .ComputeHash(System.Text.Encoding.UTF8.GetBytes(password))
                                             .Aggregate("", (s, b) => s + b.ToString("x2")) == matKhauHash);
                        
                        if (!passwordValid)
                        {
                            MessageBox.Show("Tên đăng nhập hoặc mật khẩu không đúng.", "Lỗi đăng nhập", 
                                          MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        
                        bool kichHoat = reader.GetBoolean(2);
                        
                        if (!kichHoat)
                        {
                            MessageBox.Show("Tài khoản đã bị khóa. Vui lòng liên hệ quản trị viên.", 
                                          "Tài khoản bị khóa", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }

                        int maNguoiDung = reader.GetInt32(0);
                        string vaiTro = reader.GetString(1);
                        int maNV = reader.IsDBNull(3) ? 0 : reader.GetInt32(3);
                        string hoTen = reader.IsDBNull(4) ? username : reader.GetString(4);
                        
                        reader.Close();
                        
                        // ============================================
                        // LƯU CHUỖI KẾT NỐI VÀO GLOBALSTATE
                        // ============================================
                        // Từ giờ tất cả các form khác sẽ dùng chuỗi này
                        GlobalState.ConnectionString = connectionString;
                        GlobalState.UserRole = vaiTro;
                        GlobalState.Username = username;
                        
                        // Lưu thông tin người dùng vào session
                        UserSession.SetUser(maNguoiDung, maNV, username, hoTen, vaiTro);
                        
                        // Mở form chính với quyền tương ứng
                        frmMain mainForm = new frmMain(vaiTro);
                        this.Hide();
                        mainForm.ShowDialog();
                        this.Close();
                    }
                    else
                    {
                        // Không tìm thấy user trong bảng NguoiDung hoặc tài khoản bị khóa
                        MessageBox.Show("Tên đăng nhập hoặc mật khẩu không đúng.", 
                                      "Lỗi đăng nhập", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (SqlException ex)
                {
                    MessageBox.Show($"Lỗi kết nối cơ sở dữ liệu:\n{ex.Message}\n\n" +
                                  $"Error Code: {ex.Number}", 
                                  "Lỗi kết nối", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi không xác định:\n{ex.Message}", 
                                  "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
