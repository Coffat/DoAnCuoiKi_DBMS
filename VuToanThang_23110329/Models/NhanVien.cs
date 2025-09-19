using System;

namespace VuToanThang_23110329.Models
{
    public class NhanVien
    {
        public int MaNV { get; set; }
        public string HoTen { get; set; }
        public string CCCD { get; set; }
        public string SoDienThoai { get; set; }
        public string Email { get; set; }
        public string DiaChi { get; set; }
        public DateTime NgaySinh { get; set; }
        public string GioiTinh { get; set; }
        public DateTime NgayVaoLam { get; set; }
        public string ChucVu { get; set; }
        public string PhongBan { get; set; }
        public decimal LuongCoBan { get; set; }
        public decimal PhuCapChucVu { get; set; }
        public decimal PhuCapKhac { get; set; }
        public string TrangThai { get; set; } // Active, Inactive, Terminated
        public int? MaQuanLy { get; set; }
        public DateTime NgayTao { get; set; }
        public DateTime? NgayCapNhat { get; set; }
    }
}
