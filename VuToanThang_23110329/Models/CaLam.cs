using System;

namespace VuToanThang_23110329.Models
{
    public class CaLam
    {
        public int MaCa { get; set; }
        public string TenCa { get; set; }
        public TimeSpan GioBatDau { get; set; }
        public TimeSpan GioKetThuc { get; set; }
        public int SoGioLam { get; set; }
        public TimeSpan? GioNghiGiua { get; set; }
        public int? PhutNghiGiua { get; set; }
        public string MoTa { get; set; }
        public bool TrangThai { get; set; }
        public DateTime NgayTao { get; set; }
        public DateTime? NgayCapNhat { get; set; }
    }
}
