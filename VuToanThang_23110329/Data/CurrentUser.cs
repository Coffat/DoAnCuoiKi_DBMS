using VuToanThang_23110329.Models;

namespace VuToanThang_23110329.Data
{
    /// <summary>
    /// Static class to manage current logged-in user session
    /// Updated: 2025-09-19
    /// </summary>
    public static class CurrentUser
    {
        public static NguoiDung User { get; set; }
        public static NhanVien Employee { get; set; }

        // Role checking properties
        public static bool IsHR => User?.VaiTro?.ToUpper() == "HR";
        public static bool IsQuanLy => User?.VaiTro?.ToUpper() == "QUANLY";
        public static bool IsKeToan => User?.VaiTro?.ToUpper() == "KETOAN";
        public static bool IsNhanVien => User?.VaiTro?.ToUpper() == "NHANVIEN";

        // Check if user is logged in
        public static bool IsLoggedIn => User != null;

        // Get current employee ID
        public static int? CurrentEmployeeId 
        { 
            get { return Employee?.MaNV; } 
        }

        /// <summary>
        /// Clear current user session
        /// </summary>
        public static void Clear()
        {
            User = null;
            Employee = null;
        }

        /// <summary>
        /// Set current user session
        /// </summary>
        public static void SetUser(NguoiDung user, NhanVien employee = null)
        {
            User = user;
            Employee = employee;
        }

        /// <summary>
        /// Check if current user has permission for specific action
        /// </summary>
        /// <param name="action">Action to check permission for</param>
        /// <returns>True if user has permission, false otherwise</returns>
        public static bool HasPermission(string action)
        {
            if (!IsLoggedIn) return false;

            switch (action.ToUpper())
            {
                case "MANAGE_EMPLOYEES":
                    return IsHR;
                
                case "MANAGE_SHIFTS":
                    return IsHR;
                
                case "MANAGE_SCHEDULE":
                    return IsHR || IsQuanLy;
                
                case "MANAGE_ATTENDANCE":
                    return IsHR || IsQuanLy;
                
                case "APPROVE_REQUESTS":
                    return IsHR || IsQuanLy;
                
                case "MANAGE_PAYROLL":
                    return IsKeToan;
                
                case "VIEW_REPORTS":
                    return IsHR || IsQuanLy || IsKeToan;
                
                case "SUBMIT_REQUESTS":
                    return IsNhanVien || IsHR || IsQuanLy || IsKeToan;
                
                case "VIEW_OWN_DATA":
                    return true; // All logged-in users can view their own data
                
                default:
                    return false;
            }
        }

        /// <summary>
        /// Get display name for current user
        /// </summary>
        public static string GetDisplayName()
        {
            return Employee?.HoTen ?? User?.TenDangNhap ?? "Unknown User";
        }

        /// <summary>
        /// Get role display name in Vietnamese
        /// </summary>
        public static string GetRoleDisplayName()
        {
            switch (User?.VaiTro?.ToUpper())
            {
                case "HR": return "Nhân sự";
                case "QUANLY": return "Quản lý";
                case "KETOAN": return "Kế toán";
                case "NHANVIEN": return "Nhân viên";
                default: return User?.VaiTro ?? "Không xác định";
            }
        }
    }
}
