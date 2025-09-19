using System;

namespace VuToanThang_23110329.Models
{
    /// <summary>
    /// Parameters for sp_ThemMoiNhanVien stored procedure
    /// </summary>
    public class ThemMoiNhanVienParams
    {
        public string HoTen { get; set; }
        public DateTime NgaySinh { get; set; }
        public string GioiTinh { get; set; }
        public string DienThoai { get; set; }
        public string Email { get; set; }
        public string DiaChi { get; set; }
        public DateTime NgayVaoLam { get; set; }
        public string PhongBan { get; set; }
        public string ChucDanh { get; set; }
        public decimal LuongCoBan { get; set; }
        
        // Account creation parameters
        public bool TaoTaiKhoan { get; set; }
        public string TenDangNhap { get; set; }
        public string MatKhau { get; set; }
        public string VaiTro { get; set; }
    }
}
