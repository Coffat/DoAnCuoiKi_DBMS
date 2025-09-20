using System;
using System.Collections.Generic;
using System.Data;
using VuToanThang_23110329.Data;
using VuToanThang_23110329.Models;

namespace VuToanThang_23110329.Repositories
{
    public class LichPhanCaRepository
    {
        public List<LichPhanCa> GetByPeriod(DateTime tuNgay, DateTime denNgay)
        {
            var list = new List<LichPhanCa>();
            try
            {
                var parameters = new[]
                {
                    SqlHelper.CreateParameter("@TuNgay", tuNgay),
                    SqlHelper.CreateParameter("@DenNgay", denNgay)
                };

                var dt = SqlHelper.ExecuteDataTable(@"
                    SELECT lpc.*, nv.HoTen as TenNhanVien, cl.TenCa, cl.GioBatDau, cl.GioKetThuc, cl.HeSoCa
                    FROM LichPhanCa lpc
                    JOIN NhanVien nv ON lpc.MaNV = nv.MaNV
                    JOIN CaLam cl ON lpc.MaCa = cl.MaCa
                    WHERE lpc.NgayLam BETWEEN @TuNgay AND @DenNgay
                    ORDER BY lpc.NgayLam DESC, nv.HoTen", parameters);
                
                foreach (DataRow row in dt.Rows)
                {
                    list.Add(MapFromDataRow(row));
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi lấy lịch phân ca: {ex.Message}", ex);
            }
            return list;
        }

        public List<LichPhanCa> GetByEmployee(int maNV, DateTime tuNgay, DateTime denNgay)
        {
            var list = new List<LichPhanCa>();
            try
            {
                var parameters = new[]
                {
                    SqlHelper.CreateParameter("@MaNV", maNV),
                    SqlHelper.CreateParameter("@TuNgay", tuNgay),
                    SqlHelper.CreateParameter("@DenNgay", denNgay)
                };

                var dt = SqlHelper.ExecuteDataTable(@"
                    SELECT lpc.*, nv.HoTen as TenNhanVien, cl.TenCa, cl.GioBatDau, cl.GioKetThuc, cl.HeSoCa
                    FROM LichPhanCa lpc
                    JOIN NhanVien nv ON lpc.MaNV = nv.MaNV
                    JOIN CaLam cl ON lpc.MaCa = cl.MaCa
                    WHERE lpc.MaNV = @MaNV AND lpc.NgayLam BETWEEN @TuNgay AND @DenNgay
                    ORDER BY lpc.NgayLam DESC", parameters);
                
                foreach (DataRow row in dt.Rows)
                {
                    list.Add(MapFromDataRow(row));
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi lấy lịch phân ca nhân viên: {ex.Message}", ex);
            }
            return list;
        }

        public OperationResult Insert(LichPhanCa lichPhanCa)
        {
            try
            {
                var parameters = new[]
                {
                    SqlHelper.CreateParameter("@MaNV", lichPhanCa.MaNV),
                    SqlHelper.CreateParameter("@MaCa", lichPhanCa.MaCa),
                    SqlHelper.CreateParameter("@NgayLam", lichPhanCa.NgayLam),
                    SqlHelper.CreateParameter("@TrangThai", lichPhanCa.TrangThai ?? "DuKien")
                };

                SqlHelper.ExecuteNonQuery(@"
                    INSERT INTO LichPhanCa (MaNV, MaCa, NgayLam, TrangThai)
                    VALUES (@MaNV, @MaCa, @NgayLam, @TrangThai)", parameters);

                return new OperationResult
                {
                    Success = true,
                    Message = "Thêm lịch phân ca thành công!"
                };
            }
            catch (Exception ex)
            {
                return new OperationResult
                {
                    Success = false,
                    Message = $"Lỗi thêm lịch phân ca: {ex.Message}"
                };
            }
        }

        public OperationResult Update(LichPhanCa lichPhanCa)
        {
            try
            {
                var parameters = new[]
                {
                    SqlHelper.CreateParameter("@MaLich", lichPhanCa.MaLich),
                    SqlHelper.CreateParameter("@MaNV", lichPhanCa.MaNV),
                    SqlHelper.CreateParameter("@MaCa", lichPhanCa.MaCa),
                    SqlHelper.CreateParameter("@NgayLam", lichPhanCa.NgayLam),
                    SqlHelper.CreateParameter("@TrangThai", lichPhanCa.TrangThai)
                };

                SqlHelper.ExecuteNonQuery(@"
                    UPDATE LichPhanCa SET 
                        MaNV = @MaNV, MaCa = @MaCa, NgayLam = @NgayLam, 
                        TrangThai = @TrangThai
                    WHERE MaLich = @MaLich", parameters);

                return new OperationResult
                {
                    Success = true,
                    Message = "Cập nhật lịch phân ca thành công!"
                };
            }
            catch (Exception ex)
            {
                return new OperationResult
                {
                    Success = false,
                    Message = $"Lỗi cập nhật lịch phân ca: {ex.Message}"
                };
            }
        }

        public OperationResult Delete(int maLich)
        {
            try
            {
                var parameters = new[] { SqlHelper.CreateParameter("@MaLich", maLich) };
                
                SqlHelper.ExecuteNonQuery("DELETE FROM LichPhanCa WHERE MaLich = @MaLich", parameters);

                return new OperationResult
                {
                    Success = true,
                    Message = "Xóa lịch phân ca thành công!"
                };
            }
            catch (Exception ex)
            {
                return new OperationResult
                {
                    Success = false,
                    Message = $"Lỗi xóa lịch phân ca: {ex.Message}"
                };
            }
        }

        public OperationResult BulkInsert(List<LichPhanCa> danhSachLich)
        {
            try
            {
                foreach (var lich in danhSachLich)
                {
                    var result = Insert(lich);
                    if (!result.Success)
                        return result;
                }

                return new OperationResult
                {
                    Success = true,
                    Message = $"Thêm {danhSachLich.Count} lịch phân ca thành công!"
                };
            }
            catch (Exception ex)
            {
                return new OperationResult
                {
                    Success = false,
                    Message = $"Lỗi thêm hàng loạt lịch phân ca: {ex.Message}"
                };
            }
        }

        private LichPhanCa MapFromDataRow(DataRow row)
        {
            return new LichPhanCa
            {
                MaLich = Convert.ToInt32(row["MaLich"]),
                MaNV = Convert.ToInt32(row["MaNV"]),
                MaCa = Convert.ToInt32(row["MaCa"]),
                NgayLam = Convert.ToDateTime(row["NgayLam"]),
                TrangThai = row["TrangThai"]?.ToString(),
                TenNhanVien = row["TenNhanVien"]?.ToString(),
                TenCa = row["TenCa"]?.ToString(),
                GioBatDau = row["GioBatDau"] != DBNull.Value ? TimeSpan.Parse(row["GioBatDau"].ToString()) : TimeSpan.Zero,
                GioKetThuc = row["GioKetThuc"] != DBNull.Value ? TimeSpan.Parse(row["GioKetThuc"].ToString()) : TimeSpan.Zero,
                HeSoCa = row["HeSoCa"] != DBNull.Value ? Convert.ToDecimal(row["HeSoCa"]) : 1.0m
            };
        }
    }
}
