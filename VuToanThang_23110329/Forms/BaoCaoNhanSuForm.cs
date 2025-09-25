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
    public partial class BaoCaoNhanSuForm : Form
    {
        private readonly NhanVienRepository _nhanVienRepository;
        private readonly ChamCongRepository _chamCongRepository;
        private readonly DonTuRepository _donTuRepository;

        // UI Controls - khai báo để sử dụng trong code
        private ComboBox cmbPhongBan, cmbTrangThai, cmbThang, cmbNam;
        private DateTimePicker dtpTuNgay, dtpDenNgay;
        private Button btnXemBaoCaoCong, btnXemBaoCaoDonTu, btnXuatBaoCao, btnLamMoi;
        private DataGridView dgvTongQuan, dgvChamCong, dgvDonTu;
        private Label lblTongNV, lblDangLam, lblNghi, lblTongCong, lblTongDonTu;
        private TabControl tabControl;

        public BaoCaoNhanSuForm()
        {
            InitializeComponent();
            _nhanVienRepository = new NhanVienRepository();
            _chamCongRepository = new ChamCongRepository();
            _donTuRepository = new DonTuRepository();
            InitializeForm();
        }

        private void InitializeForm()
        {
            if (!VuToanThang_23110329.Data.CurrentUser.HasPermission("VIEW_REPORTS"))
            {
                ShowMessage("Bạn không có quyền truy cập báo cáo!", "Cảnh báo", MessageBoxIcon.Warning);
                this.Close();
                return;
            }

            LoadEmployeeOverview();
            UpdateStatistics();
        }

        private void LoadEmployeeOverview()
        {
            try
            {
                var nhanViens = _nhanVienRepository.GetAll();

                // Apply filters
                if (cmbPhongBan.SelectedIndex > 0)
                {
                    string selectedDept = cmbPhongBan.Text;
                    nhanViens = nhanViens.Where(nv => nv.PhongBan == selectedDept).ToList();
                }

                if (cmbTrangThai.SelectedIndex > 0)
                {
                    string selectedStatus = cmbTrangThai.Text;
                    nhanViens = nhanViens.Where(nv => nv.TrangThai == selectedStatus).ToList();
                }

                dgvTongQuan.DataSource = nhanViens;
                ConfigureEmployeeGrid();
            }
            catch (Exception ex)
            {
                ShowMessage($"Lỗi tải dữ liệu nhân viên: {ex.Message}", "Lỗi", MessageBoxIcon.Error);
            }
        }

        private void LoadAttendanceReport()
        {
            try
            {
                int thang = (int)cmbThang.SelectedItem;
                int nam = (int)cmbNam.SelectedItem;

                var congThang = _chamCongRepository.GetCongThang(nam, thang);
                
                // Join with employee info
                var nhanViens = _nhanVienRepository.GetAll();
                var reportData = from ct in congThang
                                join nv in nhanViens on ct.MaNV equals nv.MaNV
                                select new
                                {
                                    ct.MaNV,
                                    nv.HoTen,
                                    nv.PhongBan,
                                    nv.ChucDanh,
                                    ct.TongGioCong,
                                    ct.TongPhutDiTre,
                                    ct.TongPhutVeSom,
                                    TyLeCong = ct.TongGioCong / 208 * 100, // Assuming 208 standard hours
                                    DiemCong = ct.TongGioCong >= 208 ? "Đạt" : "Chưa đạt"
                                };

                dgvChamCong.DataSource = reportData.ToList();
                ConfigureAttendanceGrid();
            }
            catch (Exception ex)
            {
                ShowMessage($"Lỗi tải báo cáo chấm công: {ex.Message}", "Lỗi", MessageBoxIcon.Error);
            }
        }

        private void LoadRequestReport()
        {
            try
            {
                var donTus = _donTuRepository.GetAll()
                    .Where(dt => dt.TuLuc.Date >= dtpTuNgay.Value.Date && 
                                dt.TuLuc.Date <= dtpDenNgay.Value.Date)
                    .ToList();

                // Group by employee and status
                var reportData = donTus.GroupBy(dt => new { dt.MaNV, dt.TenNhanVien })
                    .Select(g => new
                    {
                        g.Key.MaNV,
                        g.Key.TenNhanVien,
                        TongDon = g.Count(),
                        DonNghi = g.Count(dt => dt.Loai == "NGHI"),
                        DonOT = g.Count(dt => dt.Loai == "OT"),
                        ChoDuyet = g.Count(dt => dt.TrangThai == "ChoDuyet"),
                        DaDuyet = g.Count(dt => dt.TrangThai == "DaDuyet"),
                        TuChoi = g.Count(dt => dt.TrangThai == "TuChoi"),
                        TongGioNghi = g.Where(dt => dt.Loai == "NGHI" && dt.TrangThai == "DaDuyet").Sum(dt => dt.SoGio ?? 0),
                        TongGioOT = g.Where(dt => dt.Loai == "OT" && dt.TrangThai == "DaDuyet").Sum(dt => dt.SoGio ?? 0)
                    })
                    .ToList();

                dgvDonTu.DataSource = reportData;
                ConfigureRequestGrid();
            }
            catch (Exception ex)
            {
                ShowMessage($"Lỗi tải báo cáo đơn từ: {ex.Message}", "Lỗi", MessageBoxIcon.Error);
            }
        }

        private void ConfigureEmployeeGrid()
        {
            if (dgvTongQuan.Columns.Count > 0)
            {
                dgvTongQuan.Columns["MaNV"].HeaderText = "Mã NV";
                dgvTongQuan.Columns["HoTen"].HeaderText = "Họ tên";
                dgvTongQuan.Columns["NgaySinh"].HeaderText = "Ngày sinh";
                dgvTongQuan.Columns["GioiTinh"].HeaderText = "Giới tính";
                dgvTongQuan.Columns["DienThoai"].HeaderText = "Điện thoại";
                dgvTongQuan.Columns["Email"].HeaderText = "Email";
                dgvTongQuan.Columns["PhongBan"].HeaderText = "Phòng ban";
                dgvTongQuan.Columns["ChucDanh"].HeaderText = "Chức danh";
                dgvTongQuan.Columns["NgayVaoLam"].HeaderText = "Ngày vào làm";
                dgvTongQuan.Columns["TrangThai"].HeaderText = "Trạng thái";
                dgvTongQuan.Columns["LuongCoBan"].HeaderText = "Lương CB";

                // Hide some columns
                if (dgvTongQuan.Columns["MaNguoiDung"] != null)
                    dgvTongQuan.Columns["MaNguoiDung"].Visible = false;
                if (dgvTongQuan.Columns["DiaChi"] != null)
                    dgvTongQuan.Columns["DiaChi"].Visible = false;

                // Format currency
                if (dgvTongQuan.Columns["LuongCoBan"] != null)
                {
                    dgvTongQuan.Columns["LuongCoBan"].DefaultCellStyle.Format = "N0";
                    dgvTongQuan.Columns["LuongCoBan"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                }

                // Color status column
                dgvTongQuan.CellFormatting += (s, e) =>
                {
                    if (dgvTongQuan.Columns[e.ColumnIndex].Name == "TrangThai")
                    {
                        switch (e.Value?.ToString())
                        {
                            case "DangLam":
                                e.CellStyle.ForeColor = Color.LightGreen;
                                break;
                            case "Nghi":
                                e.CellStyle.ForeColor = Color.LightCoral;
                                break;
                        }
                    }
                };
            }
        }

        private void ConfigureAttendanceGrid()
        {
            if (dgvChamCong.Columns.Count > 0)
            {
                dgvChamCong.Columns["MaNV"].HeaderText = "Mã NV";
                dgvChamCong.Columns["HoTen"].HeaderText = "Họ tên";
                dgvChamCong.Columns["PhongBan"].HeaderText = "Phòng ban";
                dgvChamCong.Columns["ChucDanh"].HeaderText = "Chức danh";
                dgvChamCong.Columns["TongGioCong"].HeaderText = "Tổng giờ công";
                dgvChamCong.Columns["TongPhutDiTre"].HeaderText = "Phút đi trễ";
                dgvChamCong.Columns["TongPhutVeSom"].HeaderText = "Phút về sớm";
                dgvChamCong.Columns["TyLeCong"].HeaderText = "Tỷ lệ công (%)";
                dgvChamCong.Columns["DiemCong"].HeaderText = "Đánh giá";

                // Format numeric columns
                dgvChamCong.Columns["TongGioCong"].DefaultCellStyle.Format = "F2";
                dgvChamCong.Columns["TyLeCong"].DefaultCellStyle.Format = "F1";
                
                // Color evaluation column
                dgvChamCong.CellFormatting += (s, e) =>
                {
                    if (dgvChamCong.Columns[e.ColumnIndex].Name == "DiemCong")
                    {
                        switch (e.Value?.ToString())
                        {
                            case "Đạt":
                                e.CellStyle.ForeColor = Color.LightGreen;
                                break;
                            case "Chưa đạt":
                                e.CellStyle.ForeColor = Color.Orange;
                                break;
                        }
                    }
                };
            }
        }

        private void ConfigureRequestGrid()
        {
            if (dgvDonTu.Columns.Count > 0)
            {
                dgvDonTu.Columns["MaNV"].HeaderText = "Mã NV";
                dgvDonTu.Columns["TenNhanVien"].HeaderText = "Nhân viên";
                dgvDonTu.Columns["TongDon"].HeaderText = "Tổng đơn";
                dgvDonTu.Columns["DonNghi"].HeaderText = "Đơn nghỉ";
                dgvDonTu.Columns["DonOT"].HeaderText = "Đơn OT";
                dgvDonTu.Columns["ChoDuyet"].HeaderText = "Chờ duyệt";
                dgvDonTu.Columns["DaDuyet"].HeaderText = "Đã duyệt";
                dgvDonTu.Columns["TuChoi"].HeaderText = "Từ chối";
                dgvDonTu.Columns["TongGioNghi"].HeaderText = "Tổng giờ nghỉ";
                dgvDonTu.Columns["TongGioOT"].HeaderText = "Tổng giờ OT";

                // Format hour columns
                dgvDonTu.Columns["TongGioNghi"].DefaultCellStyle.Format = "F1";
                dgvDonTu.Columns["TongGioOT"].DefaultCellStyle.Format = "F1";
            }
        }

        private void UpdateStatistics()
        {
            try
            {
                var nhanViens = _nhanVienRepository.GetAll();
                
                lblTongNV.Text = $"Tổng NV: {nhanViens.Count}";
                lblDangLam.Text = $"Đang làm: {nhanViens.Count(nv => nv.TrangThai == "DangLam")}";
                lblNghi.Text = $"Nghỉ việc: {nhanViens.Count(nv => nv.TrangThai == "Nghi")}";

                // Get current month attendance
                var congThang = _chamCongRepository.GetCongThang(DateTime.Now.Year, DateTime.Now.Month);
                decimal tongCong = congThang.Sum(ct => ct.TongGioCong);
                lblTongCong.Text = $"Tổng công: {tongCong:F0}h";

                // Get current month requests
                var donTus = _donTuRepository.GetAll()
                    .Where(dt => dt.TuLuc.Month == DateTime.Now.Month && dt.TuLuc.Year == DateTime.Now.Year)
                    .ToList();
                lblTongDonTu.Text = $"Tổng đơn từ: {donTus.Count}";
            }
            catch (Exception ex)
            {
                ShowMessage($"Lỗi cập nhật thống kê: {ex.Message}", "Lỗi", MessageBoxIcon.Error);
            }
        }

        private void ShowMessage(string message, string title, MessageBoxIcon icon)
        {
            MessageBox.Show(message, title, MessageBoxButtons.OK, icon);
        }

        // Event Handlers
        private void tabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (tabControl.SelectedIndex)
            {
                case 0:
                    LoadEmployeeOverview();
                    break;
                case 1:
                    LoadAttendanceReport();
                    break;
                case 2:
                    LoadRequestReport();
                    break;
            }
        }

        private void btnXemBaoCaoCong_Click(object sender, EventArgs e)
        {
            LoadAttendanceReport();
        }

        private void btnXemBaoCaoDonTu_Click(object sender, EventArgs e)
        {
            LoadRequestReport();
        }

        private void btnXuatBaoCao_Click(object sender, EventArgs e)
        {
            try
            {
                var activeTab = tabControl.SelectedIndex;
                DataGridView currentGrid = null;
                string fileName = "";

                switch (activeTab)
                {
                    case 0:
                        currentGrid = dgvTongQuan;
                        fileName = "BaoCaoTongQuanNhanSu";
                        break;
                    case 1:
                        currentGrid = dgvChamCong;
                        fileName = $"BaoCaoChamCong_{cmbThang.SelectedItem}_{cmbNam.SelectedItem}";
                        break;
                    case 2:
                        currentGrid = dgvDonTu;
                        fileName = $"BaoCaoDonTu_{dtpTuNgay.Value:yyyyMMdd}_{dtpDenNgay.Value:yyyyMMdd}";
                        break;
                }

                if (currentGrid?.Rows.Count == 0)
                {
                    ShowMessage("Không có dữ liệu để xuất!", "Cảnh báo", MessageBoxIcon.Warning);
                    return;
                }

                ExportToCSV(currentGrid, fileName);
            }
            catch (Exception ex)
            {
                ShowMessage($"Lỗi xuất báo cáo: {ex.Message}", "Lỗi", MessageBoxIcon.Error);
            }
        }

        private void ExportToCSV(DataGridView dgv, string fileName)
        {
            var csv = new System.Text.StringBuilder();
            
            // Header
            var headers = new List<string>();
            foreach (DataGridViewColumn col in dgv.Columns)
            {
                if (col.Visible)
                    headers.Add($"\"{col.HeaderText}\"");
            }
            csv.AppendLine(string.Join(",", headers));

            // Data rows
            foreach (DataGridViewRow row in dgv.Rows)
            {
                if (row.IsNewRow) continue;
                
                var values = new List<string>();
                foreach (DataGridViewColumn col in dgv.Columns)
                {
                    if (col.Visible)
                    {
                        var cellValue = row.Cells[col.Index].Value?.ToString() ?? "";
                        values.Add($"\"{cellValue}\"");
                    }
                }
                csv.AppendLine(string.Join(",", values));
            }

            // Save file dialog
            using (var saveDialog = new SaveFileDialog())
            {
                saveDialog.Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*";
                saveDialog.FileName = $"{fileName}_{DateTime.Now:yyyyMMdd}.csv";
                
                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    System.IO.File.WriteAllText(saveDialog.FileName, csv.ToString(), System.Text.Encoding.UTF8);
                    ShowMessage($"Xuất file thành công: {saveDialog.FileName}", "Thành công", MessageBoxIcon.Information);
                }
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            UpdateStatistics();
            switch (tabControl.SelectedIndex)
            {
                case 0:
                    LoadEmployeeOverview();
                    break;
                case 1:
                    LoadAttendanceReport();
                    break;
                case 2:
                    LoadRequestReport();
                    break;
            }
        }
    }
}
