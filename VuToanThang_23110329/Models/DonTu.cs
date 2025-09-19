using System;

namespace VuToanThang_23110329.Models
{
    public class DonTu
    {
        public int MaDon { get; set; }
        public int MaNV { get; set; }
        public string Loai { get; set; } // NGHI, OT
        public DateTime TuLuc { get; set; }
        public DateTime DenLuc { get; set; }
        public decimal? SoGio { get; set; }
        public string LyDo { get; set; }
        public string TrangThai { get; set; } // ChoDuyet, DaDuyet, TuChoi
        public int? DuyetBoi { get; set; }
        
        // Navigation properties (for display)
        public string TenNhanVien { get; set; }
        public string TenNguoiDuyet { get; set; }
    }
}
