using System;

namespace VuToanThang_23110329
{
    /// <summary>
    /// Lưu trữ thông tin phiên đăng nhập của người dùng hiện tại
    /// </summary>
    public static class UserSession
    {
        public static int MaNguoiDung { get; set; }
        public static int MaNV { get; set; }
        public static string TenDangNhap { get; set; }
        public static string HoTen { get; set; }
        public static string VaiTro { get; set; }
        public static bool IsLoggedIn { get; set; }

        /// <summary>
        /// Đặt thông tin người dùng sau khi đăng nhập thành công
        /// </summary>
        public static void SetUser(int maNguoiDung, int maNV, string tenDangNhap, string hoTen, string vaiTro)
        {
            MaNguoiDung = maNguoiDung;
            MaNV = maNV;
            TenDangNhap = tenDangNhap;
            HoTen = hoTen;
            VaiTro = vaiTro;
            IsLoggedIn = true;
        }

        /// <summary>
        /// Xóa thông tin người dùng khi đăng xuất
        /// Đồng thời xóa chuỗi kết nối động trong GlobalState
        /// </summary>
        public static void Clear()
        {
            MaNguoiDung = 0;
            MaNV = 0;
            TenDangNhap = string.Empty;
            HoTen = string.Empty;
            VaiTro = string.Empty;
            IsLoggedIn = false;
            
            // Xóa chuỗi kết nối động để bảo mật
            GlobalState.Clear();
        }
    }
}
