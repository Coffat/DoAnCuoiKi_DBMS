using System;

namespace VuToanThang_23110329.Models
{
    public class BangLuong
    {
        public int MaBangLuong { get; set; }
        public int MaNV { get; set; }
        public int Nam { get; set; }
        public int Thang { get; set; }
        public int SoNgayLam { get; set; }
        public int SoGioLam { get; set; }
        public int SoGioTangCa { get; set; }
        public decimal LuongCoBan { get; set; }
        public decimal LuongTangCa { get; set; }
        public decimal PhuCapChucVu { get; set; }
        public decimal PhuCapKhac { get; set; }
        public decimal TongPhuCap { get; set; }
        public decimal KhauTruBHXH { get; set; }
        public decimal KhauTruBHYT { get; set; }
        public decimal KhauTruBHTN { get; set; }
        public decimal KhauTruThue { get; set; }
        public decimal KhauTruKhac { get; set; }
        public decimal TongKhauTru { get; set; }
        public decimal TongLuong { get; set; }
        public decimal NetPay { get; set; }
        public string TrangThai { get; set; } // Draft, Approved, Paid
        public bool DaDong { get; set; }
        public DateTime NgayTao { get; set; }
        public DateTime? NgayCapNhat { get; set; }
        
        // Navigation properties (for display)
        public string TenNhanVien { get; set; }
        public string ChucVu { get; set; }
        public string PhongBan { get; set; }
    }
}
