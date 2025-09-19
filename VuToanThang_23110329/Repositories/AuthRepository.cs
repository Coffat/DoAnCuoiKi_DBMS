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
                // Simple password hash for demo (in production, use proper hashing)
                string passwordHash = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(password));
                
                var parameters = new[]
                {
                    SqlHelper.CreateParameter("@TenDangNhap", username),
                    SqlHelper.CreateParameter("@MatKhauHash", passwordHash)
                };

                var dt = SqlHelper.ExecuteDataTable(@"
                    SELECT nd.MaNguoiDung, nd.TenDangNhap, nd.VaiTro, nd.KichHoat,
                           nv.MaNV, nv.HoTen, nv.ChucDanh, nv.PhongBan
                    FROM NguoiDung nd
                    LEFT JOIN NhanVien nv ON nd.MaNguoiDung = nv.MaNguoiDung
                    WHERE nd.TenDangNhap = @TenDangNhap AND nd.MatKhauHash = @MatKhauHash AND nd.KichHoat = 1", 
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
                    KichHoat = Convert.ToBoolean(row["KichHoat"])
                };

                NhanVien employee = null;
                if (row["MaNV"] != DBNull.Value)
                {
                    int maNV = Convert.ToInt32(row["MaNV"]);
                    employee = new NhanVien
                    {
                        MaNV = maNV,
                        MaNguoiDung = user.MaNguoiDung,
                        HoTen = row["HoTen"]?.ToString(),
                        ChucDanh = row["ChucDanh"]?.ToString(),
                        PhongBan = row["PhongBan"]?.ToString()
                    };

                    // Set session context for employee
                    SetSessionContext(maNV);
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
