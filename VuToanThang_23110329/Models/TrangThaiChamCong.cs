using System;

namespace VuToanThang_23110329.Models
{
    public class TrangThaiChamCong
    {
        public int MaNV { get; set; }
        public DateTime NgayLam { get; set; }
        public DateTime? GioVao { get; set; }
        public DateTime? GioRa { get; set; }
        public decimal? GioCong { get; set; }
        public int? DiTrePhut { get; set; }
        public int? VeSomPhut { get; set; }
        public bool? Khoa { get; set; }
        public int? MaCa { get; set; }
        public string TenCa { get; set; }
        public TimeSpan? GioBatDau { get; set; }
        public TimeSpan? GioKetThuc { get; set; }
        public string TrangThaiLich { get; set; }
        public string TrangThaiChamCongHienTai { get; set; }
        public string TrangThaiHanhDong { get; set; }
        public DateTime? GioSomNhatCheckIn { get; set; }

        // Computed properties for UI
        public bool CoTheCheckIn => TrangThaiHanhDong == "CoTheCheckIn";
        public bool CoTheCheckOut => TrangThaiHanhDong == "CoTheCheckOut";
        public bool DaHoanThanh => TrangThaiHanhDong == "DaHoanThanh";
        public bool KhongCoLich => TrangThaiHanhDong == "KhongCoLich";
        public bool DaKhoa => TrangThaiHanhDong == "LichDaKhoa" || TrangThaiHanhDong == "CongDaKhoa";
        public bool ChuaDenGioCheckIn => TrangThaiHanhDong == "ChuaDenGioCheckIn";

        public string ThoiGianCa
        {
            get
            {
                if (GioBatDau.HasValue && GioKetThuc.HasValue)
                {
                    return $"{GioBatDau.Value:hh\\:mm} - {GioKetThuc.Value:hh\\:mm}";
                }
                return "";
            }
        }

        public string ThongTinChamCong
        {
            get
            {
                if (GioVao.HasValue && GioRa.HasValue)
                {
                    return $"Vào: {GioVao.Value:HH:mm} - Ra: {GioRa.Value:HH:mm}";
                }
                else if (GioVao.HasValue)
                {
                    return $"Vào: {GioVao.Value:HH:mm} - Chưa ra";
                }
                return "Chưa chấm công";
            }
        }

        public string TrangThaiDisplay
        {
            get
            {
                switch (TrangThaiChamCongHienTai)
                {
                    case "ChuaCheckIn": return "Chưa check in";
                    case "DaCheckIn": return "Đã check in";
                    case "DaCheckOut": return "Đã check out";
                    default: return TrangThaiChamCongHienTai ?? "Không xác định";
                }
            }
        }
    }
}
