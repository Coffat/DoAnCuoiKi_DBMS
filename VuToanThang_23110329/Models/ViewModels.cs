using System;

namespace VuToanThang_23110329.Models
{
    // View Models for database views
    public class vw_CongThang
    {
        public int MaNV { get; set; }
        public int Nam { get; set; }
        public int Thang { get; set; }
        public decimal TongGioCong { get; set; }
        public int TongPhutDiTre { get; set; }
        public int TongPhutVeSom { get; set; }
    }

    public class vw_Lich_ChamCong_Ngay
    {
        public int MaNV { get; set; }
        public string HoTen { get; set; }
        public DateTime NgayLam { get; set; }
        public string TenCa { get; set; }
        public TimeSpan GioBatDau { get; set; }
        public TimeSpan GioKetThuc { get; set; }
        public decimal HeSoCa { get; set; }
        public string TrangThaiLich { get; set; }
        public DateTime? GioVao { get; set; }
        public DateTime? GioRa { get; set; }
        public decimal? GioCong { get; set; }
        public int? DiTrePhut { get; set; }
        public int? VeSomPhut { get; set; }
        public bool? KhoaChamCong { get; set; }
    }

    // Function result models
    public class fn_KhungCa_Result
    {
        public TimeSpan GioBatDau { get; set; }
        public TimeSpan GioKetThuc { get; set; }
        public decimal HeSoCa { get; set; }
    }

    // Login result model
    public class LoginResult
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public NguoiDung User { get; set; }
        public NhanVien Employee { get; set; }
    }

}
