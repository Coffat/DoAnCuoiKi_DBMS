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

        public OperationResult Insert(NhanVien nhanVien, bool createAccount = false, string tenDangNhap = "", string matKhau = "", string vaiTro = "NhanVien")
        {
            try
            {
                if (createAccount)
                {
                    // Use stored procedure to create employee with account
                    var parameters = new[]
                    {
                        SqlHelper.CreateParameter("@HoTen", nhanVien.HoTen),
                        SqlHelper.CreateParameter("@CCCD", nhanVien.CCCD),
                        SqlHelper.CreateParameter("@SoDienThoai", nhanVien.SoDienThoai),
                        SqlHelper.CreateParameter("@Email", nhanVien.Email),
                        SqlHelper.CreateParameter("@DiaChi", nhanVien.DiaChi),
                        SqlHelper.CreateParameter("@NgaySinh", nhanVien.NgaySinh),
                        SqlHelper.CreateParameter("@GioiTinh", nhanVien.GioiTinh),
                        SqlHelper.CreateParameter("@NgayVaoLam", nhanVien.NgayVaoLam),
                        SqlHelper.CreateParameter("@ChucVu", nhanVien.ChucVu),
                        SqlHelper.CreateParameter("@PhongBan", nhanVien.PhongBan),
                        SqlHelper.CreateParameter("@LuongCoBan", nhanVien.LuongCoBan),
                        SqlHelper.CreateParameter("@PhuCapChucVu", nhanVien.PhuCapChucVu),
                        SqlHelper.CreateParameter("@PhuCapKhac", nhanVien.PhuCapKhac),
                        SqlHelper.CreateParameter("@MaQuanLy", nhanVien.MaQuanLy),
                        SqlHelper.CreateParameter("@TenDangNhap", tenDangNhap),
                        SqlHelper.CreateParameter("@MatKhau", matKhau),
                        SqlHelper.CreateParameter("@VaiTro", vaiTro),
                        SqlHelper.CreateOutputParameter("@MaNV_OUT", SqlDbType.Int)
                    };

                    SqlHelper.ExecuteNonQuery("sp_ThemMoiNhanVien", parameters);
                    
                    var newMaNV = Convert.ToInt32(parameters[parameters.Length - 1].Value);
                    
                    return new OperationResult
                    {
                        Success = true,
                        Message = "Thêm nhân viên và tạo tài khoản thành công!",
                        Data = newMaNV
                    };
                }
                else
                {
                    // Regular insert without account creation
                    var parameters = new[]
                    {
                        SqlHelper.CreateParameter("@HoTen", nhanVien.HoTen),
                        SqlHelper.CreateParameter("@CCCD", nhanVien.CCCD),
                        SqlHelper.CreateParameter("@SoDienThoai", nhanVien.SoDienThoai),
                        SqlHelper.CreateParameter("@Email", nhanVien.Email),
                        SqlHelper.CreateParameter("@DiaChi", nhanVien.DiaChi),
                        SqlHelper.CreateParameter("@NgaySinh", nhanVien.NgaySinh),
                        SqlHelper.CreateParameter("@GioiTinh", nhanVien.GioiTinh),
                        SqlHelper.CreateParameter("@NgayVaoLam", nhanVien.NgayVaoLam),
                        SqlHelper.CreateParameter("@ChucVu", nhanVien.ChucVu),
                        SqlHelper.CreateParameter("@PhongBan", nhanVien.PhongBan),
                        SqlHelper.CreateParameter("@LuongCoBan", nhanVien.LuongCoBan),
                        SqlHelper.CreateParameter("@PhuCapChucVu", nhanVien.PhuCapChucVu),
                        SqlHelper.CreateParameter("@PhuCapKhac", nhanVien.PhuCapKhac),
                        SqlHelper.CreateParameter("@MaQuanLy", nhanVien.MaQuanLy)
                    };

                    SqlHelper.ExecuteNonQuery(@"
                        INSERT INTO NhanVien (HoTen, CCCD, SoDienThoai, Email, DiaChi, NgaySinh, GioiTinh, 
                                            NgayVaoLam, ChucVu, PhongBan, LuongCoBan, PhuCapChucVu, PhuCapKhac, 
                                            TrangThai, MaQuanLy, NgayTao)
                        VALUES (@HoTen, @CCCD, @SoDienThoai, @Email, @DiaChi, @NgaySinh, @GioiTinh, 
                               @NgayVaoLam, @ChucVu, @PhongBan, @LuongCoBan, @PhuCapChucVu, @PhuCapKhac, 
                               'Active', @MaQuanLy, GETDATE())", parameters);

                    return new OperationResult
                    {
                        Success = true,
                        Message = "Thêm nhân viên thành công!"
                    };
                }
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
                HoTen = row["HoTen"].ToString(),
                CCCD = row["CCCD"].ToString(),
                SoDienThoai = row["SoDienThoai"]?.ToString(),
                Email = row["Email"]?.ToString(),
                DiaChi = row["DiaChi"]?.ToString(),
                NgaySinh = Convert.ToDateTime(row["NgaySinh"]),
                GioiTinh = row["GioiTinh"].ToString(),
                NgayVaoLam = Convert.ToDateTime(row["NgayVaoLam"]),
                ChucVu = row["ChucVu"]?.ToString(),
                PhongBan = row["PhongBan"]?.ToString(),
                LuongCoBan = Convert.ToDecimal(row["LuongCoBan"]),
                PhuCapChucVu = row["PhuCapChucVu"] != DBNull.Value ? Convert.ToDecimal(row["PhuCapChucVu"]) : 0,
                PhuCapKhac = row["PhuCapKhac"] != DBNull.Value ? Convert.ToDecimal(row["PhuCapKhac"]) : 0,
                TrangThai = row["TrangThai"].ToString(),
                MaQuanLy = row["MaQuanLy"] != DBNull.Value ? Convert.ToInt32(row["MaQuanLy"]) : (int?)null,
                NgayTao = Convert.ToDateTime(row["NgayTao"]),
                NgayCapNhat = row["NgayCapNhat"] != DBNull.Value ? Convert.ToDateTime(row["NgayCapNhat"]) : (DateTime?)null
            };
        }
    }
}
