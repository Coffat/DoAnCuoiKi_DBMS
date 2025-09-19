using System;

namespace VuToanThang_23110329.Models
{
    public class ChamCong
    {
        public int MaChamCong { get; set; }
        public int MaNV { get; set; }
        public DateTime NgayLam { get; set; }
        public DateTime? GioVao { get; set; }
        public DateTime? GioRa { get; set; }
        public decimal? GioCong { get; set; }
        public int? DiTrePhut { get; set; }
        public int? VeSomPhut { get; set; }
        public bool Khoa { get; set; }
        
        // Navigation properties (for display)
        public string TenNhanVien { get; set; }
        public string TenCa { get; set; }
    }
}
