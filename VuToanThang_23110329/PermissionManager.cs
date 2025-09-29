using System;

namespace VuToanThang_23110329
{
    /// <summary>
    /// Quản lý phân quyền tập trung cho toàn hệ thống
    /// </summary>
    public static class PermissionManager
    {
        /// <summary>
        /// Kiểm tra người dùng có quyền quản lý nhân viên không
        /// </summary>
        public static bool CanManageEmployees()
        {
            return UserSession.VaiTro == "HR" || UserSession.VaiTro == "QuanLy";
        }

        /// <summary>
        /// Kiểm tra người dùng có quyền khóa công không
        /// </summary>
        public static bool CanLockAttendance()
        {
            return UserSession.VaiTro == "HR" || UserSession.VaiTro == "QuanLy";
        }

        /// <summary>
        /// Kiểm tra người dùng có quyền quản lý bảng lương không
        /// </summary>
        public static bool CanManagePayroll()
        {
            return UserSession.VaiTro == "KeToan";
        }

        /// <summary>
        /// Kiểm tra người dùng có quyền duyệt đơn từ không
        /// </summary>
        public static bool CanApproveRequests()
        {
            return UserSession.VaiTro == "HR" || UserSession.VaiTro == "QuanLy";
        }

        /// <summary>
        /// Kiểm tra người dùng có quyền quản lý ca làm việc không
        /// </summary>
        public static bool CanManageShifts()
        {
            return UserSession.VaiTro == "HR";
        }

        /// <summary>
        /// Kiểm tra người dùng có quyền quản lý phòng ban & chức vụ không
        /// </summary>
        public static bool CanManageDepartments()
        {
            return UserSession.VaiTro == "HR" || UserSession.VaiTro == "QuanLy";
        }

        /// <summary>
        /// Kiểm tra người dùng có quyền xem báo cáo nhân sự không
        /// </summary>
        public static bool CanViewHRReports()
        {
            return UserSession.VaiTro == "HR" || UserSession.VaiTro == "QuanLy";
        }

        /// <summary>
        /// Kiểm tra người dùng có quyền xem báo cáo lương không
        /// </summary>
        public static bool CanViewPayrollReports()
        {
            return UserSession.VaiTro == "KeToan" || UserSession.VaiTro == "QuanLy";
        }

        /// <summary>
        /// Kiểm tra người dùng có quyền quản lý lịch phân ca không
        /// </summary>
        public static bool CanManageSchedule()
        {
            return UserSession.VaiTro == "HR" || UserSession.VaiTro == "QuanLy";
        }

        /// <summary>
        /// Kiểm tra người dùng có quyền mở khóa lịch phân ca không
        /// </summary>
        public static bool CanUnlockSchedule()
        {
            return UserSession.VaiTro == "HR" || UserSession.VaiTro == "QuanLy";
        }

        /// <summary>
        /// Lấy tên hiển thị của vai trò
        /// </summary>
        public static string GetRoleDisplayName(string role)
        {
            switch (role)
            {
                case "QuanLy":
                    return "Quản lý";
                case "HR":
                    return "Nhân sự";
                case "KeToan":
                    return "Kế toán";
                case "NhanVien":
                    return "Nhân viên";
                default:
                    return role;
            }
        }

        /// <summary>
        /// Kiểm tra đăng nhập và hiển thị thông báo nếu chưa đăng nhập
        /// </summary>
        public static bool CheckLoginAndShowMessage()
        {
            if (!UserSession.IsLoggedIn)
            {
                System.Windows.Forms.MessageBox.Show(
                    "Vui lòng đăng nhập để sử dụng chức năng này.",
                    "Chưa đăng nhập",
                    System.Windows.Forms.MessageBoxButtons.OK,
                    System.Windows.Forms.MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        /// <summary>
        /// Kiểm tra quyền và hiển thị thông báo nếu không có quyền
        /// </summary>
        public static bool CheckPermissionAndShowMessage(Func<bool> permissionCheck, string featureName = "chức năng này")
        {
            if (!CheckLoginAndShowMessage())
                return false;

            if (!permissionCheck())
            {
                System.Windows.Forms.MessageBox.Show(
                    $"Bạn không có quyền truy cập {featureName}.\n\nVai trò của bạn: {GetRoleDisplayName(UserSession.VaiTro)}",
                    "Không có quyền",
                    System.Windows.Forms.MessageBoxButtons.OK,
                    System.Windows.Forms.MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }
    }
}
