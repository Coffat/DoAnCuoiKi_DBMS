using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Guna.UI2.WinForms;

namespace VuToanThang_23110329.Forms
{
    public partial class frmThongTinCaNhan : Form
    {
        private string connectionString;
        private int currentUserId;
        private bool isEditing = false;
        
        public frmThongTinCaNhan()
        {
            InitializeComponent();
            InitializeConnectionString();
        }

        private void InitializeConnectionString()
        {
            connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            // Get current user info (assuming stored in static class)
            currentUserId = 1; // TODO: Get from session
        }

        private void frmThongTinCaNhan_Load(object sender, EventArgs e)
        {
            LoadPersonalData();
            SetEditingMode(false);
        }

        private void LoadPersonalData()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = @"
                        SELECT nv.HoTen, nv.DienThoai, nv.Email, nv.DiaChi, 
                               pb.TenPhongBan, cv.TenChucVu, nv.LuongCoBan, nv.NgayVaoLam
                        FROM dbo.NhanVien nv
                        LEFT JOIN dbo.PhongBan pb ON nv.MaPhongBan = pb.MaPhongBan
                        LEFT JOIN dbo.ChucVu cv ON nv.MaChucVu = cv.MaChucVu
                        WHERE nv.MaNV = @MaNV";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaNV", currentUserId);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                txtHoTen.Text = reader["HoTen"]?.ToString() ?? "";
                                txtDienThoai.Text = reader["DienThoai"]?.ToString() ?? "";
                                txtEmail.Text = reader["Email"]?.ToString() ?? "";
                                txtDiaChi.Text = reader["DiaChi"]?.ToString() ?? "";
                                txtPhongBan.Text = reader["TenPhongBan"]?.ToString() ?? "";
                                txtChucDanh.Text = reader["TenChucVu"]?.ToString() ?? "";
                                txtLuongCoBan.Text = reader["LuongCoBan"]?.ToString() ?? "";
                                
                                if (reader["NgayVaoLam"] != DBNull.Value)
                                {
                                    dtpNgayVaoLam.Value = Convert.ToDateTime(reader["NgayVaoLam"]);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải thông tin cá nhân: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SetEditingMode(bool editing)
        {
            isEditing = editing;
            txtHoTen.ReadOnly = !editing;
            txtDienThoai.ReadOnly = !editing;
            txtEmail.ReadOnly = !editing;
            txtDiaChi.ReadOnly = !editing;
            
            if (editing)
            {
                txtHoTen.FillColor = System.Drawing.Color.White;
                txtDienThoai.FillColor = System.Drawing.Color.White;
                txtEmail.FillColor = System.Drawing.Color.White;
                txtDiaChi.FillColor = System.Drawing.Color.White;
                btnCapNhat.Text = "Lưu thay đổi";
            }
            else
            {
                txtHoTen.FillColor = System.Drawing.Color.FromArgb(248, 249, 250);
                txtDienThoai.FillColor = System.Drawing.Color.FromArgb(248, 249, 250);
                txtEmail.FillColor = System.Drawing.Color.FromArgb(248, 249, 250);
                txtDiaChi.FillColor = System.Drawing.Color.FromArgb(248, 249, 250);
                btnCapNhat.Text = "Cập nhật thông tin";
            }
        }

        private void btnCapNhat_Click(object sender, EventArgs e)
        {
            if (!isEditing)
            {
                SetEditingMode(true);
            }
            else
            {
                if (ValidateForm())
                {
                    UpdatePersonalInfo();
                }
            }
        }

        private bool ValidateForm()
        {
            // Validate name
            if (string.IsNullOrEmpty(txtHoTen.Text.Trim()))
            {
                MessageBox.Show("Vui lòng nhập họ tên!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtHoTen.Focus();
                return false;
            }

            // Validate phone
            if (!string.IsNullOrEmpty(txtDienThoai.Text))
            {
                if (!IsValidPhoneNumber(txtDienThoai.Text))
                {
                    MessageBox.Show("Số điện thoại không hợp lệ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtDienThoai.Focus();
                    return false;
                }
            }

            // Validate email
            if (!string.IsNullOrEmpty(txtEmail.Text))
            {
                if (!IsValidEmail(txtEmail.Text))
                {
                    MessageBox.Show("Địa chỉ email không hợp lệ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtEmail.Focus();
                    return false;
                }
            }

            return true;
        }

        private bool IsValidPhoneNumber(string phone)
        {
            // Vietnamese phone number pattern
            string pattern = @"^(0|\+84)[1-9][0-9]{8,9}$";
            return Regex.IsMatch(phone.Replace(" ", ""), pattern);
        }

        private bool IsValidEmail(string email)
        {
            string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            return Regex.IsMatch(email, pattern);
        }

        private void UpdatePersonalInfo()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = @"
                        UPDATE dbo.NhanVien 
                        SET HoTen = @HoTen, DienThoai = @DienThoai, Email = @Email, DiaChi = @DiaChi
                        WHERE MaNV = @MaNV";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaNV", currentUserId);
                        cmd.Parameters.AddWithValue("@HoTen", txtHoTen.Text.Trim());
                        cmd.Parameters.AddWithValue("@DienThoai", string.IsNullOrEmpty(txtDienThoai.Text) ? DBNull.Value : (object)txtDienThoai.Text.Trim());
                        cmd.Parameters.AddWithValue("@Email", string.IsNullOrEmpty(txtEmail.Text) ? DBNull.Value : (object)txtEmail.Text.Trim());
                        cmd.Parameters.AddWithValue("@DiaChi", string.IsNullOrEmpty(txtDiaChi.Text) ? DBNull.Value : (object)txtDiaChi.Text.Trim());

                        int rowsAffected = cmd.ExecuteNonQuery();
                        
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Cập nhật thông tin thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            SetEditingMode(false);
                        }
                        else
                        {
                            MessageBox.Show("Không thể cập nhật thông tin!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi cập nhật thông tin: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDoiMatKhau_Click(object sender, EventArgs e)
        {
            using (var dialog = new ChangePasswordDialog())
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        using (SqlConnection conn = new SqlConnection(connectionString))
                        {
                            conn.Open();
                            string query = @"
                                UPDATE dbo.NguoiDung 
                                SET MatKhau = @MatKhauMoi
                                WHERE MaNguoiDung = @MaNguoiDung AND MatKhau = @MatKhauCu";

                            using (SqlCommand cmd = new SqlCommand(query, conn))
                            {
                                cmd.Parameters.AddWithValue("@MaNguoiDung", currentUserId);
                                cmd.Parameters.AddWithValue("@MatKhauCu", dialog.OldPassword);
                                cmd.Parameters.AddWithValue("@MatKhauMoi", dialog.NewPassword);

                                int rowsAffected = cmd.ExecuteNonQuery();
                                
                                if (rowsAffected > 0)
                                {
                                    MessageBox.Show("Đổi mật khẩu thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                                else
                                {
                                    MessageBox.Show("Mật khẩu cũ không đúng!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Lỗi khi đổi mật khẩu: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            if (isEditing)
            {
                DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn hủy các thay đổi?", 
                    "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                
                if (result == DialogResult.Yes)
                {
                    LoadPersonalData();
                    SetEditingMode(false);
                }
            }
            else
            {
                LoadPersonalData();
            }
        }

        private void txtHoTen_TextChanged(object sender, EventArgs e)
        {
            // Limit text length
            if (txtHoTen.Text.Length > 100)
            {
                txtHoTen.Text = txtHoTen.Text.Substring(0, 100);
                txtHoTen.SelectionStart = txtHoTen.Text.Length;
            }
        }

        private void txtDienThoai_TextChanged(object sender, EventArgs e)
        {
            // Remove non-numeric characters except + at the beginning
            string text = txtDienThoai.Text;
            if (text.Length > 0 && !char.IsDigit(text[0]) && text[0] != '+')
            {
                txtDienThoai.Text = text.Substring(1);
                txtDienThoai.SelectionStart = txtDienThoai.Text.Length;
            }
            
            // Limit length
            if (txtDienThoai.Text.Length > 15)
            {
                txtDienThoai.Text = txtDienThoai.Text.Substring(0, 15);
                txtDienThoai.SelectionStart = txtDienThoai.Text.Length;
            }
        }

        private void txtEmail_TextChanged(object sender, EventArgs e)
        {
            // Limit text length
            if (txtEmail.Text.Length > 100)
            {
                txtEmail.Text = txtEmail.Text.Substring(0, 100);
                txtEmail.SelectionStart = txtEmail.Text.Length;
            }
        }

        private void txtDiaChi_TextChanged(object sender, EventArgs e)
        {
            // Limit text length
            if (txtDiaChi.Text.Length > 255)
            {
                txtDiaChi.Text = txtDiaChi.Text.Substring(0, 255);
                txtDiaChi.SelectionStart = txtDiaChi.Text.Length;
            }
        }
    }

    // Simple dialog for changing password
    public class ChangePasswordDialog : Form
    {
        private TextBox txtOldPassword;
        private TextBox txtNewPassword;
        private TextBox txtConfirmPassword;
        private Button btnOK;
        private Button btnCancel;

        public string OldPassword => txtOldPassword.Text;
        public string NewPassword => txtNewPassword.Text;

        public ChangePasswordDialog()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.Text = "Đổi mật khẩu";
            this.Size = new Size(400, 250);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            var lblOld = new Label { Text = "Mật khẩu cũ:", Location = new Point(20, 20), Size = new Size(100, 20) };
            txtOldPassword = new TextBox { Location = new Point(130, 18), Size = new Size(200, 20), UseSystemPasswordChar = true };

            var lblNew = new Label { Text = "Mật khẩu mới:", Location = new Point(20, 50), Size = new Size(100, 20) };
            txtNewPassword = new TextBox { Location = new Point(130, 48), Size = new Size(200, 20), UseSystemPasswordChar = true };

            var lblConfirm = new Label { Text = "Xác nhận:", Location = new Point(20, 80), Size = new Size(100, 20) };
            txtConfirmPassword = new TextBox { Location = new Point(130, 78), Size = new Size(200, 20), UseSystemPasswordChar = true };

            btnOK = new Button { Text = "OK", Location = new Point(200, 120), Size = new Size(75, 25), DialogResult = DialogResult.OK };
            btnCancel = new Button { Text = "Hủy", Location = new Point(280, 120), Size = new Size(75, 25), DialogResult = DialogResult.Cancel };

            btnOK.Click += (s, e) => {
                if (string.IsNullOrEmpty(txtOldPassword.Text) || string.IsNullOrEmpty(txtNewPassword.Text))
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (txtNewPassword.Text != txtConfirmPassword.Text)
                {
                    MessageBox.Show("Mật khẩu xác nhận không khớp!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (txtNewPassword.Text.Length < 6)
                {
                    MessageBox.Show("Mật khẩu phải có ít nhất 6 ký tự!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                DialogResult = DialogResult.OK;
                Close();
            };

            this.Controls.AddRange(new Control[] { lblOld, txtOldPassword, lblNew, txtNewPassword, lblConfirm, txtConfirmPassword, btnOK, btnCancel });
        }
    }
    }
}
