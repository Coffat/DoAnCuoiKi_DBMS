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

    // Common result model for operations
    public class OperationResult
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
    }

    // Stored procedure parameter models
    public class ThemMoiNhanVienParams
    {
        public string HoTen { get; set; }
        public DateTime? NgaySinh { get; set; }
        public string GioiTinh { get; set; }
        public string DienThoai { get; set; }
        public string Email { get; set; }
        public string DiaChi { get; set; }
        public DateTime? NgayVaoLam { get; set; }
        public string PhongBan { get; set; }
        public string ChucDanh { get; set; }
        public decimal LuongCoBan { get; set; }
        public bool TaoTaiKhoan { get; set; }
        public string TenDangNhap { get; set; }
        public string MatKhauHash { get; set; }
        public string VaiTro { get; set; }
    }
}
