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
    public partial class BangLuongForm : Form
    {
        private readonly BangLuongRepository _bangLuongRepository;

        public BangLuongForm()
        {
            InitializeComponent();
            _bangLuongRepository = new BangLuongRepository();
            InitializeForm();
        }

        private void InitializeForm()
        {
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                int thang = (int)cmbThang.SelectedItem;
                int nam = (int)cmbNam.SelectedItem;

                var bangLuongs = _bangLuongRepository.GetByPeriod(nam, thang);

                // Filter by status if selected
                if (cmbTrangThai.SelectedIndex > 0)
                {
                    string selectedStatus = cmbTrangThai.Text;
                    bangLuongs = bangLuongs.Where(bl => bl.TrangThai == selectedStatus).ToList();
                }

                dgvBangLuong.DataSource = bangLuongs;
                ConfigureDataGridView();
                UpdateSummary(bangLuongs);
            }
            catch (Exception ex)
            {
                ShowMessage($"Lỗi tải dữ liệu: {ex.Message}", "Lỗi", MessageBoxIcon.Error);
            }
        }

        private void ConfigureDataGridView()
        {
            if (dgvBangLuong.Columns.Count > 0)
            {
                dgvBangLuong.Columns["MaBangLuong"].HeaderText = "Mã BL";
                dgvBangLuong.Columns["TenNhanVien"].HeaderText = "Nhân viên";
                dgvBangLuong.Columns["ChucDanh"].HeaderText = "Chức danh";
                dgvBangLuong.Columns["PhongBan"].HeaderText = "Phòng ban";
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

                // Color status and salary columns
                dgvBangLuong.CellFormatting += (s, e) =>
                {
                    if (dgvBangLuong.Columns[e.ColumnIndex].Name == "TrangThai")
                    {
                        switch (e.Value?.ToString())
                        {
                            case "Mo":
                                e.CellStyle.ForeColor = Color.Orange;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                                break;
                            case "Dong":
                                e.CellStyle.ForeColor = Color.LightGreen;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                                break;
                        }
                    }
                    else if (dgvBangLuong.Columns[e.ColumnIndex].Name == "ThucLanh")
                    {
                        e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                        e.CellStyle.ForeColor = Color.LightGreen;
                    }
                };
            }
        }

        private void UpdateSummary(System.Collections.Generic.List<BangLuong> bangLuongs)
        {
            lblTongNhanVien.Text = $"Tổng nhân viên: {bangLuongs.Count}";
            
            decimal tongLuong = bangLuongs.Sum(bl => bl.ThucLanh);
            lblTongLuong.Text = $"Tổng chi lương: {tongLuong:N0} VNĐ";

            decimal luongTB = bangLuongs.Count > 0 ? tongLuong / bangLuongs.Count : 0;
            lblLuongTB.Text = $"Lương TB: {luongTB:N0} VNĐ";
        }

        private void ShowMessage(string message, string title, MessageBoxIcon icon)
        {
            MessageBox.Show(message, title, MessageBoxButtons.OK, icon);
        }

        // Event Handlers
        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void btnXuatExcel_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvBangLuong.Rows.Count == 0)
                {
                    ShowMessage("Không có dữ liệu để xuất!", "Cảnh báo", MessageBoxIcon.Warning);
                    return;
                }

                // Tạo nội dung CSV đơn giản
                var csv = new System.Text.StringBuilder();
                
                // Header
                var headers = new List<string>();
                foreach (DataGridViewColumn col in dgvBangLuong.Columns)
                {
                    if (col.Visible)
                        headers.Add($"\"{col.HeaderText}\"");
                }
                csv.AppendLine(string.Join(",", headers));

                // Data rows
                foreach (DataGridViewRow row in dgvBangLuong.Rows)
                {
                    if (row.IsNewRow) continue;
                    
                    var values = new List<string>();
                    foreach (DataGridViewColumn col in dgvBangLuong.Columns)
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
                    saveDialog.FileName = $"BangLuong_{DateTime.Now:yyyyMMdd}.csv";
                    
                    if (saveDialog.ShowDialog() == DialogResult.OK)
                    {
                        System.IO.File.WriteAllText(saveDialog.FileName, csv.ToString(), System.Text.Encoding.UTF8);
                        ShowMessage($"Xuất file thành công: {saveDialog.FileName}", "Thành công", MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                ShowMessage($"Lỗi xuất Excel: {ex.Message}", "Lỗi", MessageBoxIcon.Error);
            }
        }

        private void btnInBangLuong_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvBangLuong.Rows.Count == 0)
                {
                    ShowMessage("Không có dữ liệu để in!", "Cảnh báo", MessageBoxIcon.Warning);
                    return;
                }

                // Tạo form preview để in
                var printForm = new PrintBangLuongForm(dgvBangLuong, (int)cmbThang.SelectedItem, (int)cmbNam.SelectedItem);
                printForm.ShowDialog();
            }
            catch (Exception ex)
            {
                ShowMessage($"Lỗi in bảng lương: {ex.Message}", "Lỗi", MessageBoxIcon.Error);
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void dgvBangLuong_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dgvBangLuong.SelectedRows.Count > 0)
            {
                var selectedBL = (BangLuong)dgvBangLuong.SelectedRows[0].DataBoundItem;
                var detailForm = new ChiTietBangLuongForm(selectedBL);
                detailForm.ShowDialog();
            }
        }
    }

    // Detail form for viewing payroll details
    public partial class ChiTietBangLuongForm : Form
    {
        public ChiTietBangLuongForm(BangLuong bangLuong)
        {
            InitializeComponent();
            LoadPayrollDetails(bangLuong);
        }


        private void LoadPayrollDetails(BangLuong bl)
        {
            var lblTitle = new Label
            {
                Text = $"CHI TIẾT LƯƠNG - {bl.TenNhanVien}",
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                ForeColor = Color.FromArgb(124, 77, 255),
                Location = new Point(20, 20),
                AutoSize = true
            };

            var details = $@"
Mã bảng lương: {bl.MaBangLuong}
Nhân viên: {bl.TenNhanVien}
Chức danh: {bl.ChucDanh}
Phòng ban: {bl.PhongBan}
Kỳ lương: {bl.Thang:00}/{bl.Nam}

=== THÔNG TIN LƯƠNG ===
Lương cơ bản: {bl.LuongCoBan:N0} VNĐ
Tổng giờ công: {bl.TongGioCong:F2} giờ
Giờ làm thêm: {bl.GioOT:F2} giờ

=== TÍNH TOÁN ===
Phụ cấp: {bl.PhuCap:N0} VNĐ
Khấu trừ: {bl.KhauTru:N0} VNĐ
Thuế & BH: {bl.ThueBH:N0} VNĐ

=== KẾT QUẢ ===
Thực lãnh: {bl.ThucLanh:N0} VNĐ
Trạng thái: {(bl.TrangThai == "Mo" ? "Đang mở" : "Đã đóng")}";

            var lblDetails = new Label
            {
                Text = details,
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 10),
                Location = new Point(20, 60),
                Size = new Size(540, 350),
                AutoSize = false
            };

            var btnClose = new Button
            {
                Text = "Đóng",
                BackColor = Color.FromArgb(158, 158, 158),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Size = new Size(80, 35),
                Location = new Point(250, 420)
            };

            btnClose.Click += (s, e) => this.Close();

            this.Controls.AddRange(new Control[] { lblTitle, lblDetails, btnClose });
        }
    }

    // Print form for payroll
    public partial class PrintBangLuongForm : Form
    {
        public PrintBangLuongForm(DataGridView dgv, int thang, int nam)
        {
            InitializeComponent();
            CreatePrintButtons();
            CreatePrintPreview(dgv, thang, nam);
        }

        private void CreatePrintButtons()
        {
            var btnPrint = new Button
            {
                Text = "In",
                BackColor = Color.FromArgb(46, 125, 50),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Size = new Size(80, 35),
                Location = new Point(20, 20)
            };

            var btnClose = new Button
            {
                Text = "Đóng",
                BackColor = Color.FromArgb(158, 158, 158),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Size = new Size(80, 35),
                Location = new Point(110, 20)
            };

            btnPrint.Click += (s, e) => {
                MessageBox.Show("Chức năng in thực tế cần cài đặt thêm thư viện in ấn!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            };

            btnClose.Click += (s, e) => this.Close();

            this.Controls.AddRange(new Control[] { btnPrint, btnClose });
        }

        private void CreatePrintPreview(DataGridView dgv, int thang, int nam)
        {
            var panel = new Panel
            {
                Size = new Size(800, 600),
                Location = new Point(50, 70),
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                AutoScroll = true
            };

            var content = new Label
            {
                Text = $"BẢNG LƯƠNG THÁNG {thang:00}/{nam}\n\n" +
                       "SIÊU THỊ MINI - HỆ THỐNG QUẢN LÝ NHÂN SỰ\n\n" +
                       $"Ngày in: {DateTime.Now:dd/MM/yyyy HH:mm}\n\n" +
                       "Nội dung bảng lương sẽ được hiển thị ở đây...\n" +
                       "(Cần implement thêm logic format dữ liệu từ DataGridView)",
                Font = new Font("Arial", 10),
                ForeColor = Color.Black,
                Location = new Point(20, 20),
                Size = new Size(750, 550),
                AutoSize = false
            };

            panel.Controls.Add(content);
            this.Controls.Add(panel);
        }
    }
}
