using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using VuToanThang_23110329.Data;
using VuToanThang_23110329.Models;

namespace VuToanThang_23110329.Repositories
{
    public class NhanVienRepository
    {
        public List<NhanVien> GetAll()
        {
            var list = new List<NhanVien>();
            try
            {
                var dt = SqlHelper.ExecuteDataTable("SELECT * FROM NhanVien ORDER BY HoTen");
                
                foreach (DataRow row in dt.Rows)
                {
                    list.Add(MapFromDataRow(row));
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi lấy danh sách nhân viên: {ex.Message}", ex);
            }
            return list;
        }

        public NhanVien GetById(int maNV)
        {
            try
            {
                var parameters = new[] { SqlHelper.CreateParameter("@MaNV", maNV) };
                var dt = SqlHelper.ExecuteDataTable("SELECT * FROM NhanVien WHERE MaNV = @MaNV", parameters);
                
                if (dt.Rows.Count > 0)
                    return MapFromDataRow(dt.Rows[0]);
                
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi lấy thông tin nhân viên: {ex.Message}", ex);
            }
        }

        public OperationResult Insert(ThemMoiNhanVienParams param)
        {
            try
            {
                // Hash password if creating account
                string matKhauHash = null;
                if (param.TaoTaiKhoan && !string.IsNullOrEmpty(param.MatKhauHash))
                {
                    matKhauHash = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(param.MatKhauHash));
                }

                var parameters = new[]
                {
                    SqlHelper.CreateParameter("@HoTen", param.HoTen),
                    SqlHelper.CreateParameter("@NgaySinh", param.NgaySinh),
                    SqlHelper.CreateParameter("@GioiTinh", param.GioiTinh),
                    SqlHelper.CreateParameter("@DienThoai", param.DienThoai),
                    SqlHelper.CreateParameter("@Email", param.Email),
                    SqlHelper.CreateParameter("@DiaChi", param.DiaChi),
                    SqlHelper.CreateParameter("@NgayVaoLam", param.NgayVaoLam),
                    SqlHelper.CreateParameter("@PhongBan", param.PhongBan),
                    SqlHelper.CreateParameter("@ChucDanh", param.ChucDanh),
                    SqlHelper.CreateParameter("@LuongCoBan", param.LuongCoBan),
                    SqlHelper.CreateParameter("@TaoTaiKhoan", param.TaoTaiKhoan ? 1 : 0),
                    SqlHelper.CreateParameter("@TenDangNhap", param.TenDangNhap),
                    SqlHelper.CreateParameter("@MatKhauHash", matKhauHash),
                    SqlHelper.CreateParameter("@VaiTro", param.VaiTro ?? "NhanVien"),
                    SqlHelper.CreateOutputParameter("@MaNV_OUT", SqlDbType.Int)
                };

                SqlHelper.ExecuteNonQuery("sp_ThemMoiNhanVien", parameters);
                
                var newMaNV = Convert.ToInt32(parameters[parameters.Length - 1].Value);
                
                return new OperationResult
                {
                    Success = true,
                    Message = param.TaoTaiKhoan ? "Thêm nhân viên và tạo tài khoản thành công!" : "Thêm nhân viên thành công!",
                    Data = newMaNV
                };
            }
            catch (Exception ex)
            {
                return new OperationResult
                {
                    Success = false,
                    Message = $"Lỗi thêm nhân viên: {ex.Message}"
                };
            }
        }

        public OperationResult Update(NhanVien nhanVien)
        {
            try
            {
                var parameters = new[]
                {
                    SqlHelper.CreateParameter("@MaNV", nhanVien.MaNV),
                    SqlHelper.CreateParameter("@HoTen", nhanVien.HoTen),
                    SqlHelper.CreateParameter("@CCCD", nhanVien.CCCD),
                    SqlHelper.CreateParameter("@SoDienThoai", nhanVien.SoDienThoai),
                    SqlHelper.CreateParameter("@Email", nhanVien.Email),
                    SqlHelper.CreateParameter("@DiaChi", nhanVien.DiaChi),
                    SqlHelper.CreateParameter("@NgaySinh", nhanVien.NgaySinh),
                    SqlHelper.CreateParameter("@GioiTinh", nhanVien.GioiTinh),
                    SqlHelper.CreateParameter("@ChucVu", nhanVien.ChucVu),
                    SqlHelper.CreateParameter("@PhongBan", nhanVien.PhongBan),
                    SqlHelper.CreateParameter("@LuongCoBan", nhanVien.LuongCoBan),
                    SqlHelper.CreateParameter("@PhuCapChucVu", nhanVien.PhuCapChucVu),
                    SqlHelper.CreateParameter("@PhuCapKhac", nhanVien.PhuCapKhac),
                    SqlHelper.CreateParameter("@TrangThai", nhanVien.TrangThai),
                    SqlHelper.CreateParameter("@MaQuanLy", nhanVien.MaQuanLy)
                };

                SqlHelper.ExecuteNonQuery(@"
                    UPDATE NhanVien SET 
                        HoTen = @HoTen, CCCD = @CCCD, SoDienThoai = @SoDienThoai, Email = @Email, 
                        DiaChi = @DiaChi, NgaySinh = @NgaySinh, GioiTinh = @GioiTinh, ChucVu = @ChucVu, 
                        PhongBan = @PhongBan, LuongCoBan = @LuongCoBan, PhuCapChucVu = @PhuCapChucVu, 
                        PhuCapKhac = @PhuCapKhac, TrangThai = @TrangThai, MaQuanLy = @MaQuanLy, 
                        NgayCapNhat = GETDATE()
                    WHERE MaNV = @MaNV", parameters);

                return new OperationResult
                {
                    Success = true,
                    Message = "Cập nhật nhân viên thành công!"
                };
            }
            catch (Exception ex)
            {
                return new OperationResult
                {
                    Success = false,
                    Message = $"Lỗi cập nhật nhân viên: {ex.Message}"
                };
            }
        }

        public OperationResult Delete(int maNV)
        {
            try
            {
                var parameters = new[] { SqlHelper.CreateParameter("@MaNV", maNV) };
                
                SqlHelper.ExecuteNonQuery("UPDATE NhanVien SET TrangThai = 'Terminated', NgayCapNhat = GETDATE() WHERE MaNV = @MaNV", parameters);

                return new OperationResult
                {
                    Success = true,
                    Message = "Xóa nhân viên thành công!"
                };
            }
            catch (Exception ex)
            {
                return new OperationResult
                {
                    Success = false,
                    Message = $"Lỗi xóa nhân viên: {ex.Message}"
                };
            }
        }

        public List<NhanVien> GetByRLS()
        {
            var list = new List<NhanVien>();
            try
            {
                var dt = SqlHelper.ExecuteDataTable("SELECT * FROM fn_rls_NhanVien() ORDER BY HoTen");
                
                foreach (DataRow row in dt.Rows)
                {
                    list.Add(MapFromDataRow(row));
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi lấy danh sách nhân viên (RLS): {ex.Message}", ex);
            }
            return list;
        }

        private NhanVien MapFromDataRow(DataRow row)
        {
            return new NhanVien
            {
                MaNV = Convert.ToInt32(row["MaNV"]),
                MaNguoiDung = row["MaNguoiDung"] != DBNull.Value ? Convert.ToInt32(row["MaNguoiDung"]) : (int?)null,
                HoTen = row["HoTen"].ToString(),
                NgaySinh = row["NgaySinh"] != DBNull.Value ? Convert.ToDateTime(row["NgaySinh"]) : (DateTime?)null,
                GioiTinh = row["GioiTinh"]?.ToString(),
                DienThoai = row["DienThoai"]?.ToString(),
                Email = row["Email"]?.ToString(),
                DiaChi = row["DiaChi"]?.ToString(),
                NgayVaoLam = Convert.ToDateTime(row["NgayVaoLam"]),
                TrangThai = row["TrangThai"].ToString(),
                PhongBan = row["PhongBan"]?.ToString(),
                ChucDanh = row["ChucDanh"]?.ToString(),
                LuongCoBan = Convert.ToDecimal(row["LuongCoBan"])
            };
        }
    }
}
