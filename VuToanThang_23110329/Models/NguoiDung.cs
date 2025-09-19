using System;

namespace VuToanThang_23110329.Models
{
    public class NguoiDung
    {
        public int MaNguoiDung { get; set; }
        public string TenDangNhap { get; set; }
        public string MatKhau { get; set; }
        public string VaiTro { get; set; } // HR, QuanLy, KeToan, NhanVien
        public int? MaNV { get; set; }
        public bool TrangThai { get; set; }
        public DateTime NgayTao { get; set; }
    }
}
