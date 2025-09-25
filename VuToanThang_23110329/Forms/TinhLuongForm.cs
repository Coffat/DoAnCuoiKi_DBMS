using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using VuToanThang_23110329.Data;
using VuToanThang_23110329.Models;
using VuToanThang_23110329.Repositories;

namespace VuToanThang_23110329.Forms
{
    public partial class TinhLuongForm : Form
    {
        private readonly BangLuongRepository _bangLuongRepository;
        private readonly ChamCongRepository _chamCongRepository;

        public TinhLuongForm()
        {
            InitializeComponent();
            _bangLuongRepository = new BangLuongRepository();
            _chamCongRepository = new ChamCongRepository();
            InitializeForm();
        }

        private void InitializeForm()
        {
            try
            {
                if (!VuToanThang_23110329.Data.CurrentUser.HasPermission("MANAGE_PAYROLL"))
                {
                    ShowMessage("Bạn không có quyền truy cập chức năng này!", "Cảnh báo", MessageBoxIcon.Warning);
                    this.Close();
                    return;
                }

                LoadAttendanceData();
                LoadPayrollData();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khởi tạo form: {ex.Message}\n\nStack trace: {ex.StackTrace}", 
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadAttendanceData()
        {
            try
            {
                if (cmbThang?.SelectedItem == null || cmbNam?.SelectedItem == null)
                {
                    return; // Controls not ready yet
                }

                int thang = (int)cmbThang.SelectedItem;
                int nam = (int)cmbNam.SelectedItem;

                var congThang = _chamCongRepository.GetCongThang(nam, thang);
                if (dgvCongThang != null)
                {
                    dgvCongThang.DataSource = congThang;
                    ConfigureAttendanceGrid();
                }
            }
            catch (Exception ex)
            {
                ShowMessage($"Lỗi tải dữ liệu công tháng: {ex.Message}", "Lỗi", MessageBoxIcon.Error);
            }
        }

        private void LoadPayrollData()
        {
            try
            {
                if (cmbThang?.SelectedItem == null || cmbNam?.SelectedItem == null)
                {
                    return; // Controls not ready yet
                }

                int thang = (int)cmbThang.SelectedItem;
                int nam = (int)cmbNam.SelectedItem;

                var bangLuong = _bangLuongRepository.GetByPeriod(nam, thang);
                if (dgvBangLuong != null)
                {
                    dgvBangLuong.DataSource = bangLuong;
                    ConfigurePayrollGrid();
                    UpdatePayrollSummary(bangLuong);
                }
            }
            catch (Exception ex)
            {
                ShowMessage($"Lỗi tải dữ liệu bảng lương: {ex.Message}", "Lỗi", MessageBoxIcon.Error);
            }
        }

        private void ConfigureAttendanceGrid()
        {
            if (dgvCongThang.Columns.Count > 0)
            {
                dgvCongThang.Columns["MaNV"].HeaderText = "Mã NV";
                dgvCongThang.Columns["Nam"].HeaderText = "Năm";
                dgvCongThang.Columns["Thang"].HeaderText = "Tháng";
                dgvCongThang.Columns["TongGioCong"].HeaderText = "Tổng giờ công";
                dgvCongThang.Columns["TongPhutDiTre"].HeaderText = "Tổng phút đi trễ";
                dgvCongThang.Columns["TongPhutVeSom"].HeaderText = "Tổng phút về sớm";

                // Format numeric columns
                dgvCongThang.Columns["TongGioCong"].DefaultCellStyle.Format = "F2";
                dgvCongThang.Columns["TongGioCong"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }
        }

        private void ConfigurePayrollGrid()
        {
            if (dgvBangLuong.Columns.Count > 0)
            {
                dgvBangLuong.Columns["MaBangLuong"].HeaderText = "Mã BL";
                dgvBangLuong.Columns["TenNhanVien"].HeaderText = "Nhân viên";
                dgvBangLuong.Columns["ChucDanh"].HeaderText = "Chức danh";
                dgvBangLuong.Columns["LuongCoBan"].HeaderText = "Lương CB";
                dgvBangLuong.Columns["TongGioCong"].HeaderText = "Giờ công";
                dgvBangLuong.Columns["GioOT"].HeaderText = "Giờ OT";
                dgvBangLuong.Columns["PhuCap"].HeaderText = "Phụ cấp";
                dgvBangLuong.Columns["KhauTru"].HeaderText = "Khấu trừ";
                dgvBangLuong.Columns["ThueBH"].HeaderText = "Thuế BH";
                dgvBangLuong.Columns["ThucLanh"].HeaderText = "Thực lãnh";
                dgvBangLuong.Columns["TrangThai"].HeaderText = "Trạng thái";

                // Hide some columns
                if (dgvBangLuong.Columns["MaNV"] != null)
                    dgvBangLuong.Columns["MaNV"].Visible = false;
                if (dgvBangLuong.Columns["Nam"] != null)
                    dgvBangLuong.Columns["Nam"].Visible = false;
                if (dgvBangLuong.Columns["Thang"] != null)
                    dgvBangLuong.Columns["Thang"].Visible = false;
                if (dgvBangLuong.Columns["PhongBan"] != null)
                    dgvBangLuong.Columns["PhongBan"].Visible = false;

                // Format currency columns
                var currencyColumns = new[] { "LuongCoBan", "PhuCap", "KhauTru", "ThueBH", "ThucLanh" };
                foreach (var col in currencyColumns)
                {
                    if (dgvBangLuong.Columns[col] != null)
                    {
                        dgvBangLuong.Columns[col].DefaultCellStyle.Format = "N0";
                        dgvBangLuong.Columns[col].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    }
                }

                // Format hour columns
                var hourColumns = new[] { "TongGioCong", "GioOT" };
                foreach (var col in hourColumns)
                {
                    if (dgvBangLuong.Columns[col] != null)
                    {
                        dgvBangLuong.Columns[col].DefaultCellStyle.Format = "F2";
                        dgvBangLuong.Columns[col].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    }
                }

                // Color status column
                dgvBangLuong.CellFormatting += (s, e) =>
                {
                    if (dgvBangLuong.Columns[e.ColumnIndex].Name == "TrangThai")
                    {
                        switch (e.Value?.ToString())
                        {
                            case "Mo":
                                e.CellStyle.ForeColor = Color.Orange;
                                break;
                            case "Dong":
                                e.CellStyle.ForeColor = Color.LightGreen;
                                break;
                        }
                    }
                };
            }
        }

        private void UpdatePayrollSummary(System.Collections.Generic.List<BangLuong> bangLuongs)
        {
            lblTongNhanVien.Text = $"Tổng nhân viên: {bangLuongs.Count}";
            
            decimal tongLuong = bangLuongs.Sum(bl => bl.ThucLanh);
            lblTongLuong.Text = $"Tổng lương: {tongLuong:N0} VNĐ";

            string trangThai = bangLuongs.Count > 0 ? 
                (bangLuongs.All(bl => bl.TrangThai == "Dong") ? "Đã đóng" : 
                 bangLuongs.Any(bl => bl.TrangThai == "Mo") ? "Đang mở" : "Chưa chạy") : 
                "Chưa chạy";
            
            lblTrangThai.Text = $"Trạng thái: {trangThai}";
            lblTrangThai.ForeColor = trangThai == "Đã đóng" ? Color.LightGreen : 
                                   trangThai == "Đang mở" ? Color.Orange : Color.Gray;

            // Enable/disable buttons based on status
            btnChayLuong.Enabled = trangThai != "Đã đóng";
            btnDongLuong.Enabled = trangThai == "Đang mở";
            btnCapNhatPhuCap.Enabled = trangThai == "Đang mở";
        }

        private void ShowMessage(string message, string title, MessageBoxIcon icon)
        {
            MessageBox.Show(message, title, MessageBoxButtons.OK, icon);
        }

        // Event Handlers
        private void btnXemCong_Click(object sender, EventArgs e)
        {
            LoadAttendanceData();
            tabControl.SelectedIndex = 0; // Switch to attendance tab
        }

        private void btnChayLuong_Click(object sender, EventArgs e)
        {
            if (cmbThang?.SelectedItem == null || cmbNam?.SelectedItem == null)
            {
                ShowMessage("Vui lòng chọn tháng và năm!", "Cảnh báo", MessageBoxIcon.Warning);
                return;
            }

            int thang = (int)cmbThang.SelectedItem;
            int nam = (int)cmbNam.SelectedItem;

            var result = MessageBox.Show(
                $"Bạn có chắc chắn muốn chạy bảng lương tháng {thang}/{nam}?\n\n" +
                $"Giờ chuẩn: {nudGioChuan.Value}\n" +
                $"Hệ số OT: {nudHeSoOT.Value}",
                "Xác nhận chạy bảng lương",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result == DialogResult.Yes)
            {
                try
                {
                    var payrollResult = _bangLuongRepository.ChayBangLuong(
                        nam, thang, nudGioChuan.Value, nudHeSoOT.Value);

                    if (payrollResult.Success)
                    {
                        ShowMessage(payrollResult.Message, "Thành công", MessageBoxIcon.Information);
                        LoadPayrollData();
                        tabControl.SelectedIndex = 1; // Switch to payroll tab
                    }
                    else
                    {
                        ShowMessage(payrollResult.Message, "Lỗi", MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    ShowMessage($"Lỗi chạy bảng lương: {ex.Message}", "Lỗi", MessageBoxIcon.Error);
                }
            }
        }

        private void btnDongLuong_Click(object sender, EventArgs e)
        {
            if (cmbThang?.SelectedItem == null || cmbNam?.SelectedItem == null)
            {
                ShowMessage("Vui lòng chọn tháng và năm!", "Cảnh báo", MessageBoxIcon.Warning);
                return;
            }

            int thang = (int)cmbThang.SelectedItem;
            int nam = (int)cmbNam.SelectedItem;

            var result = MessageBox.Show(
                $"Bạn có chắc chắn muốn đóng bảng lương tháng {thang}/{nam}?\n\n" +
                "Sau khi đóng, không thể chỉnh sửa bảng lương này!",
                "Xác nhận đóng bảng lương",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            if (result == DialogResult.Yes)
            {
                try
                {
                    var closeResult = _bangLuongRepository.DongBangLuong(nam, thang);

                    if (closeResult.Success)
                    {
                        ShowMessage(closeResult.Message, "Thành công", MessageBoxIcon.Information);
                        LoadPayrollData();
                    }
                    else
                    {
                        ShowMessage(closeResult.Message, "Lỗi", MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    ShowMessage($"Lỗi đóng bảng lương: {ex.Message}", "Lỗi", MessageBoxIcon.Error);
                }
            }
        }

        private void btnCapNhatPhuCap_Click(object sender, EventArgs e)
        {
            if (cmbThang?.SelectedItem == null || cmbNam?.SelectedItem == null)
            {
                ShowMessage("Vui lòng chọn tháng và năm!", "Cảnh báo", MessageBoxIcon.Warning);
                return;
            }

            var form = new CapNhatPhuCapForm((int)cmbNam.SelectedItem, (int)cmbThang.SelectedItem);
            if (form.ShowDialog() == DialogResult.OK)
            {
                LoadPayrollData();
            }
        }

        private void dgvBangLuong_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dgvBangLuong.SelectedRows.Count > 0)
            {
                var selectedBL = (BangLuong)dgvBangLuong.SelectedRows[0].DataBoundItem;
                if (selectedBL.TrangThai == "Mo")
                {
                    var form = new ChinhSuaLuongForm(selectedBL);
                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        LoadPayrollData();
                    }
                }
                else
                {
                    ShowMessage("Bảng lương đã đóng, không thể chỉnh sửa!", "Thông báo", MessageBoxIcon.Information);
                }
            }
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            PerformLayout();
        }
    }

    /*
     * NOTE: CapNhatPhuCapForm và ChinhSuaLuongForm đã được chuyển thành các file riêng biệt:
     * - CapNhatPhuCapForm.cs và CapNhatPhuCapForm.Designer.cs
     * - ChinhSuaLuongForm.cs và ChinhSuaLuongForm.Designer.cs
     * 
     * Các class này trước đây được định nghĩa trong file này nhưng đã được tách ra
     * để tuân thủ WinForms best practices và tránh lỗi duplicate definitions.
     * 
     * Chức năng:
     * - CapNhatPhuCapForm: Bulk update phụ cấp/khấu trừ cho tất cả nhân viên
     * - ChinhSuaLuongForm: Chỉnh sửa lương cá nhân với real-time calculation
     */
}
