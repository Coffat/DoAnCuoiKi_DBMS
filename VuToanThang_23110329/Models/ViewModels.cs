using System;

namespace VuToanThang_23110329.Models
{
    // View Models for database views
    public class vw_CongThang
    {
        public int MaNV { get; set; }
        public string TenNhanVien { get; set; }
        public string PhongBan { get; set; }
        public int Nam { get; set; }
        public int Thang { get; set; }
        public int SoNgayLam { get; set; }
        public int SoGioLam { get; set; }
        public int SoGioTangCa { get; set; }
        public int SoLanTre { get; set; }
        public int SoLanSom { get; set; }
        public int SoNgayNghi { get; set; }
    }

    public class vw_Lich_ChamCong_Ngay
    {
        public int MaNV { get; set; }
        public string TenNhanVien { get; set; }
        public DateTime NgayLam { get; set; }
        public string TenCa { get; set; }
        public TimeSpan? GioBatDau { get; set; }
        public TimeSpan? GioKetThuc { get; set; }
        public DateTime? GioVao { get; set; }
        public DateTime? GioRa { get; set; }
        public string TrangThaiChamCong { get; set; }
        public int? SoPhutTre { get; set; }
        public int? SoPhutSom { get; set; }
        public int? SoPhutTangCa { get; set; }
    }

    // Function result models
    public class fn_KhungCa_Result
    {
        public int MaCa { get; set; }
        public string TenCa { get; set; }
        public TimeSpan GioBatDau { get; set; }
        public TimeSpan GioKetThuc { get; set; }
        public string TrangThai { get; set; }
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
}
