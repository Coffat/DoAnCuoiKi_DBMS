using System;
using System.Data;
using System.Data.SqlClient;
using VuToanThang_23110329.Data;
using VuToanThang_23110329.Models;

namespace VuToanThang_23110329.Repositories
{
    public class AuthRepository
    {
        public LoginResult Login(string username, string password)
        {
            try
            {
                var parameters = new[]
                {
                    SqlHelper.CreateParameter("@TenDangNhap", username),
                    SqlHelper.CreateParameter("@MatKhau", password)
                };

                var dt = SqlHelper.ExecuteDataTable(@"
                    SELECT nd.MaNguoiDung, nd.TenDangNhap, nd.VaiTro, nd.MaNV, nd.TrangThai,
                           nv.HoTen, nv.ChucVu, nv.PhongBan
                    FROM NguoiDung nd
                    LEFT JOIN NhanVien nv ON nd.MaNV = nv.MaNV
                    WHERE nd.TenDangNhap = @TenDangNhap AND nd.MatKhau = @MatKhau AND nd.TrangThai = 1", 
                    parameters);

                if (dt.Rows.Count == 0)
                {
                    return new LoginResult
                    {
                        Success = false,
                        Message = "Tên đăng nhập hoặc mật khẩu không đúng!"
                    };
                }

                var row = dt.Rows[0];
                var user = new NguoiDung
                {
                    MaNguoiDung = Convert.ToInt32(row["MaNguoiDung"]),
                    TenDangNhap = row["TenDangNhap"].ToString(),
                    VaiTro = row["VaiTro"].ToString(),
                    MaNV = row["MaNV"] != DBNull.Value ? Convert.ToInt32(row["MaNV"]) : (int?)null,
                    TrangThai = Convert.ToBoolean(row["TrangThai"])
                };

                NhanVien employee = null;
                if (user.MaNV.HasValue)
                {
                    employee = new NhanVien
                    {
                        MaNV = user.MaNV.Value,
                        HoTen = row["HoTen"]?.ToString(),
                        ChucVu = row["ChucVu"]?.ToString(),
                        PhongBan = row["PhongBan"]?.ToString()
                    };

                    // Set session context for employee
                    SetSessionContext(user.MaNV.Value);
                }

                return new LoginResult
                {
                    Success = true,
                    Message = "Đăng nhập thành công!",
                    User = user,
                    Employee = employee
                };
            }
            catch (Exception ex)
            {
                return new LoginResult
                {
                    Success = false,
                    Message = $"Lỗi đăng nhập: {ex.Message}"
                };
            }
        }

        public void SetSessionContext(int maNV)
        {
            try
            {
                var parameters = new[]
                {
                    SqlHelper.CreateParameter("@MaNV", maNV)
                };

                SqlHelper.ExecuteNonQuery("sp_SetSessionContextNhanVien", parameters);
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi thiết lập session context: {ex.Message}", ex);
            }
        }

        public OperationResult CreateUserAccount(string tenDangNhap, string matKhau, string vaiTro, int? maNV = null)
        {
            try
            {
                // Check if username already exists
                var checkParams = new[]
                {
                    SqlHelper.CreateParameter("@TenDangNhap", tenDangNhap)
                };

                var existingUser = SqlHelper.ExecuteScalar("SELECT COUNT(*) FROM NguoiDung WHERE TenDangNhap = @TenDangNhap", checkParams);
                
                if (Convert.ToInt32(existingUser) > 0)
                {
                    return new OperationResult
                    {
                        Success = false,
                        Message = "Tên đăng nhập đã tồn tại!"
                    };
                }

                var parameters = new[]
                {
                    SqlHelper.CreateParameter("@TenDangNhap", tenDangNhap),
                    SqlHelper.CreateParameter("@MatKhau", matKhau),
                    SqlHelper.CreateParameter("@VaiTro", vaiTro),
                    SqlHelper.CreateParameter("@MaNV", maNV)
                };

                SqlHelper.ExecuteNonQuery(@"
                    INSERT INTO NguoiDung (TenDangNhap, MatKhau, VaiTro, MaNV, TrangThai, NgayTao)
                    VALUES (@TenDangNhap, @MatKhau, @VaiTro, @MaNV, 1, GETDATE())", parameters);

                return new OperationResult
                {
                    Success = true,
                    Message = "Tạo tài khoản thành công!"
                };
            }
            catch (Exception ex)
            {
                return new OperationResult
                {
                    Success = false,
                    Message = $"Lỗi tạo tài khoản: {ex.Message}"
                };
            }
        }
    }
}
