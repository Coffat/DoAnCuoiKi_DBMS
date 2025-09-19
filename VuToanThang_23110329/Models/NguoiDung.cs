using System;

namespace VuToanThang_23110329.Models
{
    public class NguoiDung
    {
        public int MaNguoiDung { get; set; }
        public string TenDangNhap { get; set; }
        public string MatKhauHash { get; set; }
        public string VaiTro { get; set; } // HR, QuanLy, KeToan, NhanVien
        public bool KichHoat { get; set; }
    }
}
