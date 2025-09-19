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
    }
}
