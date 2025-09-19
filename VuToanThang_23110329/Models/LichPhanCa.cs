using System;

namespace VuToanThang_23110329.Models
{
    public class LichPhanCa
    {
        public int MaLich { get; set; }
        public int MaNV { get; set; }
        public DateTime NgayLam { get; set; }
        public int MaCa { get; set; }
        public string TrangThai { get; set; } // Mo, Khoa
        public string GhiChu { get; set; }
        
        // Navigation properties (for display)
        public string TenNhanVien { get; set; }
        public string TenCa { get; set; }
        public TimeSpan GioBatDau { get; set; }
        public TimeSpan GioKetThuc { get; set; }
        public decimal HeSoCa { get; set; }
    }
}
