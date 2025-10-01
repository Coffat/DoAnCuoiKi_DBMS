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

            // ============================================
            // BẢO MẬT 2 LỚP: TẠO CHUỖI KẾT NỐI ĐỘNG
            // ============================================
            // Thay vì dùng tài khoản sa cố định, ta tạo chuỗi kết nối 
            // sử dụng chính thông tin đăng nhập của người dùng
            
            SqlConnectionStringBuilder connectionBuilder = new SqlConnectionStringBuilder();
            connectionBuilder.DataSource = GlobalState.ServerName;  // Mặc định: localhost
            connectionBuilder.InitialCatalog = GlobalState.DatabaseName;  // QLNhanSuSieuThiMini
            connectionBuilder.UserID = username;  // Sử dụng tên đăng nhập người dùng nhập
            connectionBuilder.Password = password;  // Sử dụng mật khẩu người dùng nhập
            connectionBuilder.TrustServerCertificate = true;
            connectionBuilder.ConnectTimeout = 10;

            string dynamicConnectionString = connectionBuilder.ConnectionString;

            // ============================================
            // THỬ KẾT NỐI VỚI THÔNG TIN ĐĂNG NHẬP
            // ============================================
            // Nếu SQL Server Login tồn tại và mật khẩu đúng, kết nối sẽ thành công
            // Đồng thời kiểm tra quyền trong bảng NguoiDung
            
            using (SqlConnection conn = new SqlConnection(dynamicConnectionString))
            {
                try
                {
                    conn.Open();
                    
                    // Nếu kết nối thành công, nghĩa là SQL Login hợp lệ
                    // Bây giờ lấy thông tin từ bảng NguoiDung
                    string query = @"SELECT nd.MaNguoiDung, nd.VaiTro, nd.KichHoat, nv.MaNV, nv.HoTen 
                                    FROM dbo.NguoiDung nd
                                    LEFT JOIN dbo.NhanVien nv ON nd.MaNguoiDung = nv.MaNguoiDung
                                    WHERE nd.TenDangNhap = @username";
                    
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@username", username);

                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
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
                        // LƯU CHUỖI KẾT NỐI ĐỘNG VÀO GLOBALSTATE
                        // ============================================
                        // Từ giờ tất cả các form khác sẽ dùng chuỗi này
                        GlobalState.ConnectionString = dynamicConnectionString;
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
                        // Trường hợp SQL Login hợp lệ nhưng không có trong bảng NguoiDung
                        // (không nên xảy ra nếu dùng sp_TaoTaiKhoanDayDu)
                        MessageBox.Show("Tài khoản không có quyền truy cập hệ thống.", 
                                      "Lỗi xác thực", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (SqlException ex)
                {
                    // Lỗi kết nối SQL = sai tên đăng nhập hoặc mật khẩu
                    if (ex.Number == 18456)  // Login failed
                    {
                        MessageBox.Show("Tên đăng nhập hoặc mật khẩu không đúng.\n\n" +
                                      "Lưu ý: Hệ thống sử dụng xác thực SQL Server Authentication.", 
                                      "Đăng nhập thất bại", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        MessageBox.Show($"Lỗi kết nối cơ sở dữ liệu:\n{ex.Message}\n\n" +
                                      $"Error Code: {ex.Number}", 
                                      "Lỗi kết nối", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
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
