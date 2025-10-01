using System;

namespace VuToanThang_23110329
{
    /// <summary>
    /// Lớp tĩnh lưu trữ trạng thái toàn cục của ứng dụng
    /// Đặc biệt lưu chuỗi kết nối động dựa trên thông tin đăng nhập của người dùng
    /// </summary>
    public static class GlobalState
    {
        /// <summary>
        /// Chuỗi kết nối động được tạo sau khi người dùng đăng nhập thành công
        /// Chuỗi này sử dụng thông tin đăng nhập của chính người dùng (UserID + Password)
        /// thay vì tài khoản sa cố định
        /// </summary>
        public static string ConnectionString { get; set; }

        /// <summary>
        /// Vai trò của người dùng hiện tại (HR, QuanLy, KeToan, NhanVien)
        /// </summary>
        public static string UserRole { get; set; }

        /// <summary>
        /// Tên đăng nhập của người dùng hiện tại
        /// </summary>
        public static string Username { get; set; }

        /// <summary>
        /// Tên server SQL (Data Source)
        /// </summary>
        public static string ServerName { get; set; } = "localhost";

        /// <summary>
        /// Tên database
        /// </summary>
        public static string DatabaseName { get; set; } = "QLNhanSuSieuThiMini";

        /// <summary>
        /// Xóa thông tin kết nối khi đăng xuất
        /// </summary>
        public static void Clear()
        {
            ConnectionString = string.Empty;
            UserRole = string.Empty;
            Username = string.Empty;
        }

        /// <summary>
        /// Kiểm tra xem đã có chuỗi kết nối chưa
        /// </summary>
        public static bool HasConnection()
        {
            return !string.IsNullOrEmpty(ConnectionString);
        }
    }
}
