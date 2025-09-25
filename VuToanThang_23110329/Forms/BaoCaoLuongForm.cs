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
    public partial class BaoCaoLuongForm : Form
    {
        private readonly BangLuongRepository _bangLuongRepository;
        private readonly NhanVienRepository _nhanVienRepository;

        public BaoCaoLuongForm()
        {
            InitializeComponent();
            _bangLuongRepository = new BangLuongRepository();
            _nhanVienRepository = new NhanVienRepository();
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

            LoadMonthlyReport();
        }

        private void LoadMonthlyReport()
        {
            try
            {
                int thang = (int)cmbThang.SelectedItem;
                int nam = (int)cmbNam.SelectedItem;

                var bangLuongs = _bangLuongRepository.GetByPeriod(nam, thang);

                if (cmbPhongBan.SelectedIndex > 0)
                {
                    string selectedDept = cmbPhongBan.Text;
                    bangLuongs = bangLuongs.Where(bl => bl.PhongBan == selectedDept).ToList();
                }

                dgvBaoCaoThang.DataSource = bangLuongs;
                ConfigurePayrollGrid(dgvBaoCaoThang);
                UpdateStatistics(bangLuongs);
            }
            catch (Exception ex)
            {
                ShowMessage($"Lỗi tải báo cáo tháng: {ex.Message}", "Lỗi", MessageBoxIcon.Error);
            }
        }

        private void ConfigurePayrollGrid(DataGridView dgv)
        {
            if (dgv.Columns.Count > 0)
            {
                dgv.Columns["TenNhanVien"].HeaderText = "Nhân viên";
                dgv.Columns["ChucDanh"].HeaderText = "Chức danh";
                dgv.Columns["PhongBan"].HeaderText = "Phòng ban";
                dgv.Columns["LuongCoBan"].HeaderText = "Lương CB";
                dgv.Columns["TongGioCong"].HeaderText = "Giờ công";
                dgv.Columns["GioOT"].HeaderText = "Giờ OT";
                dgv.Columns["PhuCap"].HeaderText = "Phụ cấp";
                dgv.Columns["KhauTru"].HeaderText = "Khấu trừ";
                dgv.Columns["ThucLanh"].HeaderText = "Thực lãnh";
                dgv.Columns["TrangThai"].HeaderText = "Trạng thái";

                // Hide columns
                if (dgv.Columns["MaBangLuong"] != null) dgv.Columns["MaBangLuong"].Visible = false;
                if (dgv.Columns["MaNV"] != null) dgv.Columns["MaNV"].Visible = false;
                if (dgv.Columns["Nam"] != null) dgv.Columns["Nam"].Visible = false;
                if (dgv.Columns["Thang"] != null) dgv.Columns["Thang"].Visible = false;
                if (dgv.Columns["ThueBH"] != null) dgv.Columns["ThueBH"].Visible = false;

                // Format currency
                var currencyColumns = new[] { "LuongCoBan", "PhuCap", "KhauTru", "ThucLanh" };
                foreach (var col in currencyColumns)
                {
                    if (dgv.Columns[col] != null)
                    {
                        dgv.Columns[col].DefaultCellStyle.Format = "N0";
                        dgv.Columns[col].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    }
                }

                // Format hours
                var hourColumns = new[] { "TongGioCong", "GioOT" };
                foreach (var col in hourColumns)
                {
                    if (dgv.Columns[col] != null)
                    {
                        dgv.Columns[col].DefaultCellStyle.Format = "F2";
                        dgv.Columns[col].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    }
                }
            }
        }

        private void UpdateStatistics(System.Collections.Generic.List<BangLuong> bangLuongs)
        {
            lblTongNV.Text = $"Tổng NV: {bangLuongs.Count}";
            
            decimal tongLuong = bangLuongs.Sum(bl => bl.ThucLanh);
            lblTongLuong.Text = $"Tổng lương: {tongLuong:N0} VNĐ";

            decimal luongTB = bangLuongs.Count > 0 ? tongLuong / bangLuongs.Count : 0;
            lblLuongTB.Text = $"Lương TB: {luongTB:N0} VNĐ";

            decimal caoNhat = bangLuongs.Count > 0 ? bangLuongs.Max(bl => bl.ThucLanh) : 0;
            lblCaoNhat.Text = $"Cao nhất: {caoNhat:N0} VNĐ";

            decimal thapNhat = bangLuongs.Count > 0 ? bangLuongs.Min(bl => bl.ThucLanh) : 0;
            lblThapNhat.Text = $"Thấp nhất: {thapNhat:N0} VNĐ";
        }

        private void ShowMessage(string message, string title, MessageBoxIcon icon)
        {
            MessageBox.Show(message, title, MessageBoxButtons.OK, icon);
        }

        // Event Handlers
        private void btnXemBaoCao_Click(object sender, EventArgs e)
        {
            LoadMonthlyReport();
        }

        private void btnXuatExcel_Click(object sender, EventArgs e)
        {
            try
            {
                var activeTab = tabControl.SelectedIndex;
                DataGridView currentGrid = null;
                string fileName = "";

                switch (activeTab)
                {
                    case 0:
                        currentGrid = dgvBaoCaoThang;
                        fileName = $"BaoCaoLuongThang_{cmbThang.SelectedItem}_{cmbNam.SelectedItem}";
                        break;
                    case 1:
                        currentGrid = dgvBaoCaoNam;
                        fileName = $"BaoCaoLuongNam_{cmbNam.SelectedItem}";
                        break;
                    case 2:
                        currentGrid = dgvSoSanh;
                        fileName = $"SoSanhLuong_{DateTime.Now:yyyyMMdd}";
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
                ShowMessage($"Lỗi xuất Excel: {ex.Message}", "Lỗi", MessageBoxIcon.Error);
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
            LoadMonthlyReport();
        }

        private void tabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (tabControl.SelectedIndex)
            {
                case 0:
                    LoadMonthlyReport();
                    break;
                case 1:
                    LoadYearlyReport();
                    break;
                case 2:
                    LoadComparisonReport();
                    break;
            }
        }

        private void LoadYearlyReport()
        {
            try
            {
                int nam = (int)cmbNam.SelectedItem;
                
                // Tạo báo cáo theo 12 tháng trong năm
                var yearlyData = new List<object>();
                
                for (int thang = 1; thang <= 12; thang++)
                {
                    var bangLuongs = _bangLuongRepository.GetByPeriod(nam, thang);
                    
                    if (bangLuongs.Count > 0)
                    {
                        yearlyData.Add(new
                        {
                            Thang = $"Tháng {thang:00}",
                            SoNhanVien = bangLuongs.Count,
                            TongLuong = bangLuongs.Sum(bl => bl.ThucLanh),
                            LuongTrungBinh = bangLuongs.Average(bl => bl.ThucLanh),
                            LuongCaoNhat = bangLuongs.Max(bl => bl.ThucLanh),
                            LuongThapNhat = bangLuongs.Min(bl => bl.ThucLanh),
                            TongGioCong = bangLuongs.Sum(bl => bl.TongGioCong),
                            TongGioOT = bangLuongs.Sum(bl => bl.GioOT)
                        });
                    }
                }

                dgvBaoCaoNam.DataSource = yearlyData;
                ConfigureYearlyGrid();
            }
            catch (Exception ex)
            {
                ShowMessage($"Lỗi tải báo cáo năm: {ex.Message}", "Lỗi", MessageBoxIcon.Error);
            }
        }

        private void LoadComparisonReport()
        {
            try
            {
                int nam = (int)cmbNam.SelectedItem;
                int thang = (int)cmbThang.SelectedItem;
                
                // So sánh với tháng trước và cùng kỳ năm trước
                var currentMonth = _bangLuongRepository.GetByPeriod(nam, thang);
                var previousMonth = _bangLuongRepository.GetByPeriod(nam, thang == 1 ? 12 : thang - 1);
                var sameMonthLastYear = _bangLuongRepository.GetByPeriod(nam - 1, thang);

                var comparisonData = new List<object>();

                // Dữ liệu tháng hiện tại
                if (currentMonth.Count > 0)
                {
                    comparisonData.Add(new
                    {
                        KyBaoCao = $"Tháng {thang:00}/{nam}",
                        SoNhanVien = currentMonth.Count,
                        TongLuong = currentMonth.Sum(bl => bl.ThucLanh),
                        LuongTrungBinh = currentMonth.Average(bl => bl.ThucLanh),
                        TongGioCong = currentMonth.Sum(bl => bl.TongGioCong),
                        LoaiBaoCao = "Hiện tại"
                    });
                }

                // Dữ liệu tháng trước
                if (previousMonth.Count > 0)
                {
                    comparisonData.Add(new
                    {
                        KyBaoCao = $"Tháng {(thang == 1 ? 12 : thang - 1):00}/{(thang == 1 ? nam - 1 : nam)}",
                        SoNhanVien = previousMonth.Count,
                        TongLuong = previousMonth.Sum(bl => bl.ThucLanh),
                        LuongTrungBinh = previousMonth.Average(bl => bl.ThucLanh),
                        TongGioCong = previousMonth.Sum(bl => bl.TongGioCong),
                        LoaiBaoCao = "Tháng trước"
                    });
                }

                // Dữ liệu cùng kỳ năm trước
                if (sameMonthLastYear.Count > 0)
                {
                    comparisonData.Add(new
                    {
                        KyBaoCao = $"Tháng {thang:00}/{nam - 1}",
                        SoNhanVien = sameMonthLastYear.Count,
                        TongLuong = sameMonthLastYear.Sum(bl => bl.ThucLanh),
                        LuongTrungBinh = sameMonthLastYear.Average(bl => bl.ThucLanh),
                        TongGioCong = sameMonthLastYear.Sum(bl => bl.TongGioCong),
                        LoaiBaoCao = "Cùng kỳ năm trước"
                    });
                }

                dgvSoSanh.DataSource = comparisonData;
                ConfigureComparisonGrid();
            }
            catch (Exception ex)
            {
                ShowMessage($"Lỗi tải báo cáo so sánh: {ex.Message}", "Lỗi", MessageBoxIcon.Error);
            }
        }

        private void ConfigureYearlyGrid()
        {
            if (dgvBaoCaoNam.Columns.Count > 0)
            {
                dgvBaoCaoNam.Columns["Thang"].HeaderText = "Tháng";
                dgvBaoCaoNam.Columns["SoNhanVien"].HeaderText = "Số NV";
                dgvBaoCaoNam.Columns["TongLuong"].HeaderText = "Tổng lương";
                dgvBaoCaoNam.Columns["LuongTrungBinh"].HeaderText = "Lương TB";
                dgvBaoCaoNam.Columns["LuongCaoNhat"].HeaderText = "Cao nhất";
                dgvBaoCaoNam.Columns["LuongThapNhat"].HeaderText = "Thấp nhất";
                dgvBaoCaoNam.Columns["TongGioCong"].HeaderText = "Tổng giờ công";
                dgvBaoCaoNam.Columns["TongGioOT"].HeaderText = "Tổng giờ OT";

                // Format currency columns
                var currencyColumns = new[] { "TongLuong", "LuongTrungBinh", "LuongCaoNhat", "LuongThapNhat" };
                foreach (var col in currencyColumns)
                {
                    if (dgvBaoCaoNam.Columns[col] != null)
                    {
                        dgvBaoCaoNam.Columns[col].DefaultCellStyle.Format = "N0";
                        dgvBaoCaoNam.Columns[col].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    }
                }

                // Format hour columns
                var hourColumns = new[] { "TongGioCong", "TongGioOT" };
                foreach (var col in hourColumns)
                {
                    if (dgvBaoCaoNam.Columns[col] != null)
                    {
                        dgvBaoCaoNam.Columns[col].DefaultCellStyle.Format = "F2";
                        dgvBaoCaoNam.Columns[col].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    }
                }
            }
        }

        private void ConfigureComparisonGrid()
        {
            if (dgvSoSanh.Columns.Count > 0)
            {
                dgvSoSanh.Columns["KyBaoCao"].HeaderText = "Kỳ báo cáo";
                dgvSoSanh.Columns["LoaiBaoCao"].HeaderText = "Loại";
                dgvSoSanh.Columns["SoNhanVien"].HeaderText = "Số NV";
                dgvSoSanh.Columns["TongLuong"].HeaderText = "Tổng lương";
                dgvSoSanh.Columns["LuongTrungBinh"].HeaderText = "Lương TB";
                dgvSoSanh.Columns["TongGioCong"].HeaderText = "Tổng giờ công";

                // Format currency columns
                var currencyColumns = new[] { "TongLuong", "LuongTrungBinh" };
                foreach (var col in currencyColumns)
                {
                    if (dgvSoSanh.Columns[col] != null)
                    {
                        dgvSoSanh.Columns[col].DefaultCellStyle.Format = "N0";
                        dgvSoSanh.Columns[col].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    }
                }

                // Format hour columns
                if (dgvSoSanh.Columns["TongGioCong"] != null)
                {
                    dgvSoSanh.Columns["TongGioCong"].DefaultCellStyle.Format = "F2";
                    dgvSoSanh.Columns["TongGioCong"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                }

                // Color rows by type
                dgvSoSanh.CellFormatting += (s, e) =>
                {
                    if (dgvSoSanh.Columns[e.ColumnIndex].Name == "LoaiBaoCao")
                    {
                        switch (e.Value?.ToString())
                        {
                            case "Hiện tại":
                                e.CellStyle.ForeColor = Color.LightGreen;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                                break;
                            case "Tháng trước":
                                e.CellStyle.ForeColor = Color.Orange;
                                break;
                            case "Cùng kỳ năm trước":
                                e.CellStyle.ForeColor = Color.LightBlue;
                                break;
                        }
                    }
                };
            }
        }
    }
}
