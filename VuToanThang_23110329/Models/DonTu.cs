using System;

namespace VuToanThang_23110329.Models
{
    public class DonTu
    {
        public int MaDon { get; set; }
        public int MaNV { get; set; }
        public string LoaiDon { get; set; } // Nghi, TangCa, DiTre, VeSom, Khac
        public DateTime NgayGui { get; set; }
        public DateTime? NgayBatDau { get; set; }
        public DateTime? NgayKetThuc { get; set; }
        public string LyDo { get; set; }
        public string TrangThai { get; set; } // ChoDuyet, DaDuyet, TuChoi
        public int? MaNguoiDuyet { get; set; }
        public DateTime? NgayDuyet { get; set; }
        public string GhiChuDuyet { get; set; }
        public DateTime NgayTao { get; set; }
        public DateTime? NgayCapNhat { get; set; }
        
        // Navigation properties (for display)
        public string TenNhanVien { get; set; }
        public string TenNguoiDuyet { get; set; }
    }
}
