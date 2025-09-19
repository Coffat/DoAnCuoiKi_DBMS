using System;

namespace VuToanThang_23110329.Models
{
    public class LichPhanCa
    {
        public int MaLich { get; set; }
        public int MaNV { get; set; }
        public int MaCa { get; set; }
        public DateTime NgayLam { get; set; }
        public string TrangThai { get; set; } // Scheduled, Completed, Cancelled
        public string GhiChu { get; set; }
        public int MaNguoiTao { get; set; }
        public DateTime NgayTao { get; set; }
        public DateTime? NgayCapNhat { get; set; }
        
        // Navigation properties (for display)
        public string TenNhanVien { get; set; }
        public string TenCa { get; set; }
        public TimeSpan GioBatDau { get; set; }
        public TimeSpan GioKetThuc { get; set; }
    }
}
