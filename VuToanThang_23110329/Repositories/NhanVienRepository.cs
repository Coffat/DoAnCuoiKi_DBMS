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
                // Check permission using CurrentUser
                if (!CurrentUser.HasPermission("MANAGE_EMPLOYEES"))
                {
                    return new OperationResult
                    {
                        Success = false,
                        Message = "Bạn không có quyền cập nhật thông tin nhân viên."
                    };
                }

                var parameters = new[]
                {
                    SqlHelper.CreateParameter("@MaNV", nhanVien.MaNV),
                    SqlHelper.CreateParameter("@HoTen", nhanVien.HoTen),
                    SqlHelper.CreateParameter("@NgaySinh", nhanVien.NgaySinh),
                    SqlHelper.CreateParameter("@GioiTinh", nhanVien.GioiTinh),
                    SqlHelper.CreateParameter("@DienThoai", nhanVien.DienThoai),
                    SqlHelper.CreateParameter("@Email", nhanVien.Email),
                    SqlHelper.CreateParameter("@DiaChi", nhanVien.DiaChi),
                    SqlHelper.CreateParameter("@NgayVaoLam", nhanVien.NgayVaoLam),
                    SqlHelper.CreateParameter("@TrangThai", nhanVien.TrangThai),
                    SqlHelper.CreateParameter("@PhongBan", nhanVien.PhongBan),
                    SqlHelper.CreateParameter("@ChucDanh", nhanVien.ChucDanh),
                    SqlHelper.CreateParameter("@LuongCoBan", nhanVien.LuongCoBan),
                    SqlHelper.CreateParameter("@MaNguoiDung", CurrentUser.User?.MaNguoiDung)
                };

                SqlHelper.ExecuteNonQuery("sp_CapNhatNhanVien", parameters);

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
                // Check permission using CurrentUser
                if (!CurrentUser.HasPermission("MANAGE_EMPLOYEES"))
                {
                    return new OperationResult
                    {
                        Success = false,
                        Message = "Bạn không có quyền xóa nhân viên."
                    };
                }

                var parameters = new[] 
                { 
                    SqlHelper.CreateParameter("@MaNV", maNV),
                    SqlHelper.CreateParameter("@MaNguoiDung", CurrentUser.User?.MaNguoiDung)
                };
                
                SqlHelper.ExecuteNonQuery("sp_XoaNhanVien", parameters);

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

        /// <summary>
        /// Restore employee from 'Nghi' status back to 'DangLam'
        /// </summary>
        public OperationResult Restore(int maNV)
        {
            try
            {
                // Check permission using CurrentUser
                if (!CurrentUser.HasPermission("MANAGE_EMPLOYEES"))
                {
                    return new OperationResult
                    {
                        Success = false,
                        Message = "Bạn không có quyền khôi phục nhân viên."
                    };
                }

                var parameters = new[] 
                { 
                    SqlHelper.CreateParameter("@MaNV", maNV),
                    SqlHelper.CreateParameter("@MaNguoiDung", CurrentUser.User?.MaNguoiDung)
                };
                
                SqlHelper.ExecuteNonQuery("sp_KhoiPhucNhanVien", parameters);

                return new OperationResult
                {
                    Success = true,
                    Message = "Khôi phục nhân viên thành công!"
                };
            }
            catch (Exception ex)
            {
                return new OperationResult
                {
                    Success = false,
                    Message = $"Lỗi khôi phục nhân viên: {ex.Message}"
                };
            }
        }

        /// <summary>
        /// Check if current user has permission for specific action
        /// </summary>
        public bool CheckPermission(string action)
        {
            try
            {
                if (CurrentUser.User == null) return false;

                var parameters = new[]
                {
                    SqlHelper.CreateParameter("@MaNguoiDung", CurrentUser.User.MaNguoiDung),
                    SqlHelper.CreateParameter("@ChucNang", action),
                    SqlHelper.CreateOutputParameter("@CoQuyen", SqlDbType.Bit)
                };

                SqlHelper.ExecuteNonQuery("sp_KiemTraQuyenTruyCap", parameters);
                
                return Convert.ToBoolean(parameters[2].Value);
            }
            catch
            {
                return false;
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

        public NhanVien GetByMaNV(int maNV)
        {
            return GetById(maNV);
        }

        private NhanVien MapFromDataRow(DataRow row)
        {
            int.TryParse(row["MaNV"]?.ToString(), out int maNV);

            int? maNguoiDung = null;
            if (int.TryParse(row["MaNguoiDung"]?.ToString(), out int tempMaNguoiDung))
            {
                maNguoiDung = tempMaNguoiDung;
            }

            DateTime? ngaySinh = null;
            if (DateTime.TryParse(row["NgaySinh"]?.ToString(), out DateTime tempNgaySinh))
            {
                ngaySinh = tempNgaySinh;
            }

            DateTime.TryParse(row["NgayVaoLam"]?.ToString(), out DateTime ngayVaoLam);
            if (ngayVaoLam == DateTime.MinValue)
            {
                ngayVaoLam = DateTime.Now; // Or some other sensible default
            }

            decimal.TryParse(row["LuongCoBan"]?.ToString(), out decimal luongCoBan);

            return new NhanVien
            {
                MaNV = maNV,
                MaNguoiDung = maNguoiDung,
                HoTen = row["HoTen"]?.ToString(),
                NgaySinh = ngaySinh,
                GioiTinh = row["GioiTinh"]?.ToString(),
                DienThoai = row["DienThoai"]?.ToString(),
                Email = row["Email"]?.ToString(),
                DiaChi = row["DiaChi"]?.ToString(),
                NgayVaoLam = ngayVaoLam,
                TrangThai = row["TrangThai"]?.ToString(),
                PhongBan = row["PhongBan"]?.ToString(),
                ChucDanh = row["ChucDanh"]?.ToString(),
                LuongCoBan = luongCoBan
            };
        }
    }
}
