using System;

namespace VuToanThang_23110329.Models
{
    public class NhanVien
    {
        public int MaNV { get; set; }
        public int? MaNguoiDung { get; set; }
        public string HoTen { get; set; }
        public DateTime? NgaySinh { get; set; }
        public string GioiTinh { get; set; }
        public string DienThoai { get; set; }
        public string Email { get; set; }
        public string DiaChi { get; set; }
        public DateTime NgayVaoLam { get; set; }
        public string TrangThai { get; set; } // DangLam, Nghi
        public string PhongBan { get; set; }
        public string ChucDanh { get; set; }
        public decimal LuongCoBan { get; set; }
    }
}
