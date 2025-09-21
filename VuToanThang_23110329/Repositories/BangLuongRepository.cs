using System;
using System.Collections.Generic;
using System.Data;
using VuToanThang_23110329.Data;
using VuToanThang_23110329.Models;

namespace VuToanThang_23110329.Repositories
{
    public class BangLuongRepository
    {
        public List<BangLuong> GetByPeriod(int nam, int thang)
        {
            var list = new List<BangLuong>();
            try
            {
                var parameters = new[]
                {
                    SqlHelper.CreateParameter("@Nam", nam),
                    SqlHelper.CreateParameter("@Thang", thang)
                };

                var dt = SqlHelper.ExecuteDataTable(@"
                    SELECT bl.*, nv.HoTen as TenNhanVien, nv.ChucDanh, nv.PhongBan
                    FROM BangLuong bl
                    JOIN NhanVien nv ON bl.MaNV = nv.MaNV
                    WHERE bl.Nam = @Nam AND bl.Thang = @Thang
                    ORDER BY nv.HoTen", parameters);
                
                foreach (DataRow row in dt.Rows)
                {
                    list.Add(MapFromDataRow(row));
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi lấy bảng lương: {ex.Message}", ex);
            }
            return list;
        }

        public BangLuong GetByEmployeeAndPeriod(int maNV, int nam, int thang)
        {
            try
            {
                var parameters = new[]
                {
                    SqlHelper.CreateParameter("@MaNV", maNV),
                    SqlHelper.CreateParameter("@Nam", nam),
                    SqlHelper.CreateParameter("@Thang", thang)
                };

                var dt = SqlHelper.ExecuteDataTable(@"
                    SELECT bl.*, nv.HoTen as TenNhanVien, nv.ChucDanh, nv.PhongBan
                    FROM BangLuong bl
                    JOIN NhanVien nv ON bl.MaNV = nv.MaNV
                    WHERE bl.MaNV = @MaNV AND bl.Nam = @Nam AND bl.Thang = @Thang", parameters);
                
                if (dt.Rows.Count > 0)
                    return MapFromDataRow(dt.Rows[0]);
                
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi lấy bảng lương nhân viên: {ex.Message}", ex);
            }
        }

        public OperationResult ChayBangLuong(int nam, int thang, decimal stdHours = 208, decimal otRate = 1.5m)
        {
            try
            {
                var parameters = new[]
                {
                    SqlHelper.CreateParameter("@Nam", nam),
                    SqlHelper.CreateParameter("@Thang", thang),
                    SqlHelper.CreateParameter("@StdHours", stdHours),
                    SqlHelper.CreateParameter("@OtRate", otRate)
                };

                SqlHelper.ExecuteNonQuery("sp_ChayBangLuong", parameters);

                return new OperationResult
                {
                    Success = true,
                    Message = $"Chạy bảng lương tháng {thang}/{nam} thành công!"
                };
            }
            catch (Exception ex)
            {
                return new OperationResult
                {
                    Success = false,
                    Message = $"Lỗi chạy bảng lương: {ex.Message}"
                };
            }
        }

        public OperationResult DongBangLuong(int nam, int thang)
        {
            try
            {
                var parameters = new[]
                {
                    SqlHelper.CreateParameter("@Nam", nam),
                    SqlHelper.CreateParameter("@Thang", thang)
                };

                SqlHelper.ExecuteNonQuery("sp_DongBangLuong", parameters);

                return new OperationResult
                {
                    Success = true,
                    Message = $"Đóng bảng lương tháng {thang}/{nam} thành công!"
                };
            }
            catch (Exception ex)
            {
                return new OperationResult
                {
                    Success = false,
                    Message = $"Lỗi đóng bảng lương: {ex.Message}"
                };
            }
        }

        public OperationResult UpdatePhuCapKhauTru(int maBangLuong, decimal phuCap, decimal khauTru, decimal thueBH)
        {
            try
            {
                var parameters = new[]
                {
                    SqlHelper.CreateParameter("@MaBangLuong", maBangLuong),
                    SqlHelper.CreateParameter("@PhuCap", phuCap),
                    SqlHelper.CreateParameter("@KhauTru", khauTru),
                    SqlHelper.CreateParameter("@ThueBH", thueBH)
                };

                SqlHelper.ExecuteNonQuery(@"
                    UPDATE BangLuong 
                    SET PhuCap = @PhuCap, 
                        KhauTru = @KhauTru, 
                        ThueBH = @ThueBH,
                        ThucLanh = LuongCoBan + (GioOT * (LuongCoBan/208) * 1.5) + @PhuCap - @KhauTru - @ThueBH,
                        NgayCapNhat = GETDATE()
                    WHERE MaBangLuong = @MaBangLuong AND TrangThai = N'Mo'", parameters);

                return new OperationResult
                {
                    Success = true,
                    Message = "Cập nhật phụ cấp/khấu trừ thành công!"
                };
            }
            catch (Exception ex)
            {
                return new OperationResult
                {
                    Success = false,
                    Message = $"Lỗi cập nhật: {ex.Message}"
                };
            }
        }

        public BangLuong GetByMaNVAndPeriod(int maNV, int nam, int thang)
        {
            return GetByEmployeeAndPeriod(maNV, nam, thang);
        }

        public OperationResult GetBangLuongByThangNam(int thang, int nam)
        {
            try
            {
                var parameters = new[]
                {
                    SqlHelper.CreateParameter("@Nam", nam),
                    SqlHelper.CreateParameter("@Thang", thang)
                };

                var dt = SqlHelper.ExecuteDataTable(@"
                    SELECT bl.*, nv.HoTen as TenNhanVien, nv.ChucDanh, nv.PhongBan
                    FROM BangLuong bl
                    JOIN NhanVien nv ON bl.MaNV = nv.MaNV
                    WHERE bl.Nam = @Nam AND bl.Thang = @Thang
                    ORDER BY nv.HoTen", parameters);

                return new OperationResult
                {
                    Success = true,
                    Data = dt,
                    Message = $"Lấy dữ liệu bảng lương tháng {thang}/{nam} thành công!"
                };
            }
            catch (Exception ex)
            {
                return new OperationResult
                {
                    Success = false,
                    Message = $"Lỗi lấy bảng lương: {ex.Message}"
                };
            }
        }

        private BangLuong MapFromDataRow(DataRow row)
        {
            return new BangLuong
            {
                MaBangLuong = row["MaBangLuong"] != DBNull.Value ? Convert.ToInt32(row["MaBangLuong"]) : 0,
                Nam = row["Nam"] != DBNull.Value ? Convert.ToInt32(row["Nam"]) : DateTime.Now.Year,
                Thang = row["Thang"] != DBNull.Value ? Convert.ToInt32(row["Thang"]) : DateTime.Now.Month,
                MaNV = row["MaNV"] != DBNull.Value ? Convert.ToInt32(row["MaNV"]) : 0,
                LuongCoBan = row["LuongCoBan"] != DBNull.Value ? Convert.ToDecimal(row["LuongCoBan"]) : 0m,
                TongGioCong = row["TongGioCong"] != DBNull.Value ? Convert.ToDecimal(row["TongGioCong"]) : 0m,
                GioOT = row["GioOT"] != DBNull.Value ? Convert.ToDecimal(row["GioOT"]) : 0m,
                PhuCap = row["PhuCap"] != DBNull.Value ? Convert.ToDecimal(row["PhuCap"]) : 0m,
                KhauTru = row["KhauTru"] != DBNull.Value ? Convert.ToDecimal(row["KhauTru"]) : 0m,
                ThueBH = row["ThueBH"] != DBNull.Value ? Convert.ToDecimal(row["ThueBH"]) : 0m,
                ThucLanh = row["ThucLanh"] != DBNull.Value ? Convert.ToDecimal(row["ThucLanh"]) : 0m,
                TrangThai = row["TrangThai"]?.ToString() ?? "",
                TenNhanVien = row["TenNhanVien"]?.ToString(),
                ChucDanh = row["ChucDanh"]?.ToString(),
                PhongBan = row["PhongBan"]?.ToString()
            };
        }
    }
}
