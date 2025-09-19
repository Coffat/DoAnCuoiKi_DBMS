using System;

namespace VuToanThang_23110329.Models
{
    public class ChamCong
    {
        public int MaChamCong { get; set; }
        public int MaNV { get; set; }
        public DateTime NgayCham { get; set; }
        public int? MaCa { get; set; }
        public DateTime? GioVao { get; set; }
        public DateTime? GioRa { get; set; }
        public int? SoPhutLam { get; set; }
        public int? SoPhutTangCa { get; set; }
        public int? SoPhutTre { get; set; }
        public int? SoPhutSom { get; set; }
        public string TrangThai { get; set; } // Present, Absent, Late, Early
        public string GhiChu { get; set; }
        public bool DaKhoa { get; set; }
        public DateTime NgayTao { get; set; }
        public DateTime? NgayCapNhat { get; set; }
        
        // Navigation properties (for display)
        public string TenNhanVien { get; set; }
        public string TenCa { get; set; }
    }
}
