using System;
using System.Collections.Generic;
using System.Data;
using VuToanThang_23110329.Data;
using VuToanThang_23110329.Models;

namespace VuToanThang_23110329.Repositories
{
    public class CaLamRepository
    {
        public List<CaLam> GetAll()
        {
            var list = new List<CaLam>();
            try
            {
                var dt = SqlHelper.ExecuteDataTable("SELECT * FROM CaLam ORDER BY GioBatDau");
                
                foreach (DataRow row in dt.Rows)
                {
                    list.Add(MapFromDataRow(row));
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi lấy danh sách ca làm: {ex.Message}", ex);
            }
            return list;
        }

        public fn_KhungCa_Result GetKhungCa(int maNV, DateTime ngay)
        {
            try
            {
                var parameters = new[] 
                { 
                    SqlHelper.CreateParameter("@MaNV", maNV),
                    SqlHelper.CreateParameter("@Ngay", ngay) 
                };
                var dt = SqlHelper.ExecuteDataTable("SELECT * FROM fn_KhungCa(@MaNV, @Ngay)", parameters);
                
                if (dt.Rows.Count > 0)
                {
                    var row = dt.Rows[0];
                    return new fn_KhungCa_Result
                    {
                        GioBatDau = TimeSpan.Parse(row["GioBatDau"].ToString()),
                        GioKetThuc = TimeSpan.Parse(row["GioKetThuc"].ToString()),
                        HeSoCa = Convert.ToDecimal(row["HeSoCa"])
                    };
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi lấy khung ca: {ex.Message}", ex);
            }
        }

        public CaLam GetById(int maCa)
        {
            try
            {
                var parameters = new[] { SqlHelper.CreateParameter("@MaCa", maCa) };
                var dt = SqlHelper.ExecuteDataTable("SELECT * FROM CaLam WHERE MaCa = @MaCa", parameters);
                
                if (dt.Rows.Count > 0)
                    return MapFromDataRow(dt.Rows[0]);
                
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi lấy thông tin ca làm: {ex.Message}", ex);
            }
        }

        public OperationResult Insert(CaLam caLam)
        {
            try
            {
                var parameters = new[]
                {
                    SqlHelper.CreateParameter("@TenCa", caLam.TenCa),
                    SqlHelper.CreateParameter("@GioBatDau", caLam.GioBatDau),
                    SqlHelper.CreateParameter("@GioKetThuc", caLam.GioKetThuc),
                    SqlHelper.CreateParameter("@HeSoCa", caLam.HeSoCa)
                };

                SqlHelper.ExecuteNonQuery(@"
                    INSERT INTO CaLam (TenCa, GioBatDau, GioKetThuc, HeSoCa)
                    VALUES (@TenCa, @GioBatDau, @GioKetThuc, @HeSoCa)", parameters);

                return new OperationResult
                {
                    Success = true,
                    Message = "Thêm ca làm thành công!"
                };
            }
            catch (Exception ex)
            {
                return new OperationResult
                {
                    Success = false,
                    Message = $"Lỗi thêm ca làm: {ex.Message}"
                };
            }
        }

        public OperationResult Update(CaLam caLam)
        {
            try
            {
                var parameters = new[]
                {
                    SqlHelper.CreateParameter("@MaCa", caLam.MaCa),
                    SqlHelper.CreateParameter("@TenCa", caLam.TenCa),
                    SqlHelper.CreateParameter("@GioBatDau", caLam.GioBatDau),
                    SqlHelper.CreateParameter("@GioKetThuc", caLam.GioKetThuc),
                    SqlHelper.CreateParameter("@HeSoCa", caLam.HeSoCa)
                };

                SqlHelper.ExecuteNonQuery(@"
                    UPDATE CaLam SET 
                        TenCa = @TenCa, GioBatDau = @GioBatDau, GioKetThuc = @GioKetThuc, HeSoCa = @HeSoCa
                    WHERE MaCa = @MaCa", parameters);

                return new OperationResult
                {
                    Success = true,
                    Message = "Cập nhật ca làm thành công!"
                };
            }
            catch (Exception ex)
            {
                return new OperationResult
                {
                    Success = false,
                    Message = $"Lỗi cập nhật ca làm: {ex.Message}"
                };
            }
        }

        public OperationResult Delete(int maCa)
        {
            try
            {
                var parameters = new[] { SqlHelper.CreateParameter("@MaCa", maCa) };
                
                SqlHelper.ExecuteNonQuery("DELETE FROM CaLam WHERE MaCa = @MaCa", parameters);

                return new OperationResult
                {
                    Success = true,
                    Message = "Xóa ca làm thành công!"
                };
            }
            catch (Exception ex)
            {
                return new OperationResult
                {
                    Success = false,
                    Message = $"Lỗi xóa ca làm: {ex.Message}"
                };
            }
        }

        private CaLam MapFromDataRow(DataRow row)
        {
            return new CaLam
            {
                MaCa = Convert.ToInt32(row["MaCa"]),
                TenCa = row["TenCa"].ToString(),
                GioBatDau = TimeSpan.Parse(row["GioBatDau"].ToString()),
                GioKetThuc = TimeSpan.Parse(row["GioKetThuc"].ToString()),
                HeSoCa = Convert.ToDecimal(row["HeSoCa"])
            };
        }
    }
}
