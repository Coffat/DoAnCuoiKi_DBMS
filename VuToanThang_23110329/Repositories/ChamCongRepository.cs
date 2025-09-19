using System;
using System.Collections.Generic;
using System.Data;
using VuToanThang_23110329.Data;
using VuToanThang_23110329.Models;

namespace VuToanThang_23110329.Repositories
{
    public class ChamCongRepository
    {
        public List<ChamCong> GetByPeriod(DateTime tuNgay, DateTime denNgay)
        {
            var list = new List<ChamCong>();
            try
            {
                var parameters = new[]
                {
                    SqlHelper.CreateParameter("@TuNgay", tuNgay),
                    SqlHelper.CreateParameter("@DenNgay", denNgay)
                };

                var dt = SqlHelper.ExecuteDataTable(@"
                    SELECT cc.*, nv.HoTen as TenNhanVien
                    FROM ChamCong cc
                    JOIN NhanVien nv ON cc.MaNV = nv.MaNV
                    WHERE cc.NgayLam BETWEEN @TuNgay AND @DenNgay
                    ORDER BY cc.NgayLam DESC, nv.HoTen", parameters);
                
                foreach (DataRow row in dt.Rows)
                {
                    list.Add(MapFromDataRow(row));
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi lấy dữ liệu chấm công: {ex.Message}", ex);
            }
            return list;
        }

        public List<ChamCong> GetByEmployee(int maNV, DateTime tuNgay, DateTime denNgay)
        {
            var list = new List<ChamCong>();
            try
            {
                var parameters = new[]
                {
                    SqlHelper.CreateParameter("@MaNV", maNV),
                    SqlHelper.CreateParameter("@TuNgay", tuNgay),
                    SqlHelper.CreateParameter("@DenNgay", denNgay)
                };

                var dt = SqlHelper.ExecuteDataTable(@"
                    SELECT cc.*, nv.HoTen as TenNhanVien
                    FROM ChamCong cc
                    JOIN NhanVien nv ON cc.MaNV = nv.MaNV
                    WHERE cc.MaNV = @MaNV AND cc.NgayLam BETWEEN @TuNgay AND @DenNgay
                    ORDER BY cc.NgayLam DESC", parameters);
                
                foreach (DataRow row in dt.Rows)
                {
                    list.Add(MapFromDataRow(row));
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi lấy chấm công nhân viên: {ex.Message}", ex);
            }
            return list;
        }

        public List<vw_Lich_ChamCong_Ngay> GetLichChamCongNgay(DateTime tuNgay, DateTime denNgay)
        {
            var list = new List<vw_Lich_ChamCong_Ngay>();
            try
            {
                var parameters = new[]
                {
                    SqlHelper.CreateParameter("@TuNgay", tuNgay),
                    SqlHelper.CreateParameter("@DenNgay", denNgay)
                };

                var dt = SqlHelper.ExecuteDataTable(@"
                    SELECT * FROM vw_Lich_ChamCong_Ngay
                    WHERE NgayLam BETWEEN @TuNgay AND @DenNgay
                    ORDER BY NgayLam DESC, HoTen", parameters);
                
                foreach (DataRow row in dt.Rows)
                {
                    list.Add(MapLichChamCongFromDataRow(row));
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi lấy lịch chấm công: {ex.Message}", ex);
            }
            return list;
        }

        public OperationResult UpdateChamCong(int maChamCong, DateTime? gioVao, DateTime? gioRa, string ghiChu = null)
        {
            try
            {
                var parameters = new[]
                {
                    SqlHelper.CreateParameter("@MaChamCong", maChamCong),
                    SqlHelper.CreateParameter("@GioVao", gioVao),
                    SqlHelper.CreateParameter("@GioRa", gioRa),
                    SqlHelper.CreateParameter("@GhiChu", ghiChu)
                };

                SqlHelper.ExecuteNonQuery(@"
                    UPDATE ChamCong 
                    SET GioVao = @GioVao, 
                        GioRa = @GioRa, 
                        GhiChu = @GhiChu,
                        NgayCapNhat = GETDATE()
                    WHERE MaChamCong = @MaChamCong AND Khoa = 0", parameters);

                return new OperationResult
                {
                    Success = true,
                    Message = "Cập nhật chấm công thành công!"
                };
            }
            catch (Exception ex)
            {
                return new OperationResult
                {
                    Success = false,
                    Message = $"Lỗi cập nhật chấm công: {ex.Message}"
                };
            }
        }

        public OperationResult KhoaCongThang(int nam, int thang)
        {
            try
            {
                var parameters = new[]
                {
                    SqlHelper.CreateParameter("@Nam", nam),
                    SqlHelper.CreateParameter("@Thang", thang)
                };

                SqlHelper.ExecuteNonQuery("sp_KhoaCongThang", parameters);

                return new OperationResult
                {
                    Success = true,
                    Message = $"Khóa công tháng {thang}/{nam} thành công!"
                };
            }
            catch (Exception ex)
            {
                return new OperationResult
                {
                    Success = false,
                    Message = $"Lỗi khóa công: {ex.Message}"
                };
            }
        }

        public List<vw_CongThang> GetCongThang(int nam, int thang)
        {
            var list = new List<vw_CongThang>();
            try
            {
                var parameters = new[]
                {
                    SqlHelper.CreateParameter("@Nam", nam),
                    SqlHelper.CreateParameter("@Thang", thang)
                };

                var dt = SqlHelper.ExecuteDataTable(@"
                    SELECT * FROM vw_CongThang
                    WHERE Nam = @Nam AND Thang = @Thang
                    ORDER BY MaNV", parameters);
                
                foreach (DataRow row in dt.Rows)
                {
                    list.Add(new vw_CongThang
                    {
                        MaNV = Convert.ToInt32(row["MaNV"]),
                        Nam = Convert.ToInt32(row["Nam"]),
                        Thang = Convert.ToInt32(row["Thang"]),
                        TongGioCong = Convert.ToDecimal(row["TongGioCong"]),
                        TongPhutDiTre = Convert.ToInt32(row["TongPhutDiTre"]),
                        TongPhutVeSom = Convert.ToInt32(row["TongPhutVeSom"])
                    });
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi lấy tổng hợp công tháng: {ex.Message}", ex);
            }
            return list;
        }

        private ChamCong MapFromDataRow(DataRow row)
        {
            return new ChamCong
            {
                MaChamCong = Convert.ToInt32(row["MaChamCong"]),
                MaNV = Convert.ToInt32(row["MaNV"]),
                NgayLam = Convert.ToDateTime(row["NgayLam"]),
                GioVao = row["GioVao"] != DBNull.Value ? Convert.ToDateTime(row["GioVao"]) : (DateTime?)null,
                GioRa = row["GioRa"] != DBNull.Value ? Convert.ToDateTime(row["GioRa"]) : (DateTime?)null,
                GioCong = row["GioCong"] != DBNull.Value ? Convert.ToDecimal(row["GioCong"]) : (decimal?)null,
                DiTrePhut = row["DiTrePhut"] != DBNull.Value ? Convert.ToInt32(row["DiTrePhut"]) : (int?)null,
                VeSomPhut = row["VeSomPhut"] != DBNull.Value ? Convert.ToInt32(row["VeSomPhut"]) : (int?)null,
                Khoa = Convert.ToBoolean(row["Khoa"]),
                TenNhanVien = row["TenNhanVien"]?.ToString()
            };
        }

        private vw_Lich_ChamCong_Ngay MapLichChamCongFromDataRow(DataRow row)
        {
            return new vw_Lich_ChamCong_Ngay
            {
                MaNV = Convert.ToInt32(row["MaNV"]),
                HoTen = row["HoTen"].ToString(),
                NgayLam = Convert.ToDateTime(row["NgayLam"]),
                TenCa = row["TenCa"]?.ToString(),
                GioBatDau = TimeSpan.Parse(row["GioBatDau"].ToString()),
                GioKetThuc = TimeSpan.Parse(row["GioKetThuc"].ToString()),
                HeSoCa = Convert.ToDecimal(row["HeSoCa"]),
                TrangThaiLich = row["TrangThaiLich"]?.ToString(),
                GioVao = row["GioVao"] != DBNull.Value ? Convert.ToDateTime(row["GioVao"]) : (DateTime?)null,
                GioRa = row["GioRa"] != DBNull.Value ? Convert.ToDateTime(row["GioRa"]) : (DateTime?)null,
                GioCong = row["GioCong"] != DBNull.Value ? Convert.ToDecimal(row["GioCong"]) : (decimal?)null,
                DiTrePhut = row["DiTrePhut"] != DBNull.Value ? Convert.ToInt32(row["DiTrePhut"]) : (int?)null,
                VeSomPhut = row["VeSomPhut"] != DBNull.Value ? Convert.ToInt32(row["VeSomPhut"]) : (int?)null,
                KhoaChamCong = row["KhoaChamCong"] != DBNull.Value ? Convert.ToBoolean(row["KhoaChamCong"]) : (bool?)null
            };
        }
    }
}
