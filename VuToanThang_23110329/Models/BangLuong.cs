using System;

namespace VuToanThang_23110329.Models
{
    public class BangLuong
    {
        public int MaBangLuong { get; set; }
        public int Nam { get; set; }
        public int Thang { get; set; }
        public int MaNV { get; set; }
        public decimal LuongCoBan { get; set; }
        public decimal TongGioCong { get; set; }
        public decimal GioOT { get; set; }
        public decimal PhuCap { get; set; }
        public decimal KhauTru { get; set; }
        public decimal ThueBH { get; set; }
        public decimal ThucLanh { get; set; }
        public string TrangThai { get; set; } // Mo, Dong
        
        // Navigation properties (for display)
        public string TenNhanVien { get; set; }
        public string ChucDanh { get; set; }
        public string PhongBan { get; set; }
    }
}
