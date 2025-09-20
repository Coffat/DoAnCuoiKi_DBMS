using System;

namespace VuToanThang_23110329.Models
{
    public class CaLam
    {
        public int MaCa { get; set; }
        public string TenCa { get; set; }
        public TimeSpan GioBatDau { get; set; }
        public TimeSpan GioKetThuc { get; set; }
        public decimal HeSoCa { get; set; }
        public string MoTa { get; set; }
        public bool KichHoat { get; set; } = true;
        
        /// <summary>
        /// Trả về chuỗi hiển thị thời gian ca làm với định dạng phù hợp cho TimeSpan
        /// </summary>
        public string ThoiGianDisplay => $"{GioBatDau:hh\\:mm} - {GioKetThuc:hh\\:mm}";
        
        /// <summary>
        /// Trả về chuỗi hiển thị đầy đủ thông tin ca làm
        /// </summary>
        public string DisplayText => $"{TenCa} ({ThoiGianDisplay})";
        
        /// <summary>
        /// Kiểm tra xem ca này có phải ca qua đêm không
        /// </summary>
        public bool IsNightShift => GioKetThuc < GioBatDau;
    }
}
