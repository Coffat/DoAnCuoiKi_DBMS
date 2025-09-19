using System;
using System.Collections.Generic;
using System.Data;
using VuToanThang_23110329.Data;
using VuToanThang_23110329.Models;

namespace VuToanThang_23110329.Repositories
{
    public class DonTuRepository
    {
        public List<DonTu> GetAll()
        {
            var list = new List<DonTu>();
            try
            {
                var dt = SqlHelper.ExecuteDataTable(@"
                    SELECT dt.*, nv.HoTen as TenNhanVien, nd.HoTen as TenNguoiDuyet
                    FROM DonTu dt
                    JOIN NhanVien nv ON dt.MaNV = nv.MaNV
                    LEFT JOIN NhanVien nd ON dt.DuyetBoi = nd.MaNV
                    ORDER BY dt.NgayTao DESC");
                
                foreach (DataRow row in dt.Rows)
                {
                    list.Add(MapFromDataRow(row));
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi lấy danh sách đơn từ: {ex.Message}", ex);
            }
            return list;
        }

        public List<DonTu> GetByEmployee(int maNV)
        {
            var list = new List<DonTu>();
            try
            {
                var parameters = new[] { SqlHelper.CreateParameter("@MaNV", maNV) };
                var dt = SqlHelper.ExecuteDataTable(@"
                    SELECT dt.*, nv.HoTen as TenNhanVien, nd.HoTen as TenNguoiDuyet
                    FROM DonTu dt
                    JOIN NhanVien nv ON dt.MaNV = nv.MaNV
                    LEFT JOIN NhanVien nd ON dt.DuyetBoi = nd.MaNV
                    WHERE dt.MaNV = @MaNV
                    ORDER BY dt.NgayTao DESC", parameters);
                
                foreach (DataRow row in dt.Rows)
                {
                    list.Add(MapFromDataRow(row));
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi lấy đơn từ của nhân viên: {ex.Message}", ex);
            }
            return list;
        }

        public OperationResult Insert(DonTu donTu)
        {
            try
            {
                var parameters = new[]
                {
                    SqlHelper.CreateParameter("@MaNV", donTu.MaNV),
                    SqlHelper.CreateParameter("@Loai", donTu.Loai),
                    SqlHelper.CreateParameter("@TuLuc", donTu.TuLuc),
                    SqlHelper.CreateParameter("@DenLuc", donTu.DenLuc),
                    SqlHelper.CreateParameter("@SoGio", donTu.SoGio),
                    SqlHelper.CreateParameter("@LyDo", donTu.LyDo)
                };

                SqlHelper.ExecuteNonQuery(@"
                    INSERT INTO DonTu (MaNV, Loai, TuLuc, DenLuc, SoGio, LyDo, TrangThai, NgayTao)
                    VALUES (@MaNV, @Loai, @TuLuc, @DenLuc, @SoGio, @LyDo, N'ChoDuyet', GETDATE())", parameters);

                return new OperationResult
                {
                    Success = true,
                    Message = "Gửi đơn từ thành công!"
                };
            }
            catch (Exception ex)
            {
                return new OperationResult
                {
                    Success = false,
                    Message = $"Lỗi gửi đơn từ: {ex.Message}"
                };
            }
        }

        public OperationResult DuyetDonTu(int maDon, int maNguoiDuyet, bool chapNhan)
        {
            try
            {
                var parameters = new[]
                {
                    SqlHelper.CreateParameter("@MaDon", maDon),
                    SqlHelper.CreateParameter("@MaNguoiDuyet", maNguoiDuyet),
                    SqlHelper.CreateParameter("@ChapNhan", chapNhan ? 1 : 0)
                };

                SqlHelper.ExecuteNonQuery("sp_DuyetDonTu", parameters);

                return new OperationResult
                {
                    Success = true,
                    Message = chapNhan ? "Duyệt đơn từ thành công!" : "Từ chối đơn từ thành công!"
                };
            }
            catch (Exception ex)
            {
                return new OperationResult
                {
                    Success = false,
                    Message = $"Lỗi duyệt đơn từ: {ex.Message}"
                };
            }
        }

        private DonTu MapFromDataRow(DataRow row)
        {
            return new DonTu
            {
                MaDon = Convert.ToInt32(row["MaDon"]),
                MaNV = Convert.ToInt32(row["MaNV"]),
                Loai = row["Loai"].ToString(),
                TuLuc = Convert.ToDateTime(row["TuLuc"]),
                DenLuc = Convert.ToDateTime(row["DenLuc"]),
                SoGio = row["SoGio"] != DBNull.Value ? Convert.ToDecimal(row["SoGio"]) : (decimal?)null,
                LyDo = row["LyDo"]?.ToString(),
                TrangThai = row["TrangThai"].ToString(),
                DuyetBoi = row["DuyetBoi"] != DBNull.Value ? Convert.ToInt32(row["DuyetBoi"]) : (int?)null,
                TenNhanVien = row["TenNhanVien"]?.ToString(),
                TenNguoiDuyet = row["TenNguoiDuyet"]?.ToString()
            };
        }
    }
}
