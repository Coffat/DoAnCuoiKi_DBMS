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

        // UI Controls
        private TabControl tabControl;
        private DataGridView dgvBaoCaoThang, dgvBaoCaoNam, dgvSoSanh;
        private ComboBox cmbThang, cmbNam, cmbNamSoSanh, cmbPhongBan;
        private Button btnXemBaoCao, btnXuatExcel, btnLamMoi;
        private Panel pnlThongKe;
        private Label lblTongLuong, lblLuongTB, lblCaoNhat, lblThapNhat, lblTongNV;

        public BaoCaoLuongForm()
        {
            InitializeComponent();
            _bangLuongRepository = new BangLuongRepository();
            _nhanVienRepository = new NhanVienRepository();
            InitializeForm();
        }


        private void CreateControls()
        {
            // Title
            var lblTitle = new Label
            {
                Text = "BÁO CÁO LƯƠNG",
                Font = new Font("Segoe UI", 20, FontStyle.Bold),
                ForeColor = Color.FromArgb(124, 77, 255),
                AutoSize = true
            };

            // Statistics Panel
            pnlThongKe = new Panel
            {
                BackColor = Color.FromArgb(60, 60, 60),
                BorderStyle = BorderStyle.FixedSingle,
                Size = new Size(800, 80)
            };

            lblTongNV = CreateStatLabel("Tổng NV: 0", Color.White);
            lblTongLuong = CreateStatLabel("Tổng lương: 0 VNĐ", Color.LightGreen);
            lblLuongTB = CreateStatLabel("Lương TB: 0 VNĐ", Color.LightBlue);
            lblCaoNhat = CreateStatLabel("Cao nhất: 0 VNĐ", Color.Orange);
            lblThapNhat = CreateStatLabel("Thấp nhất: 0 VNĐ", Color.LightCoral);

            pnlThongKe.Controls.AddRange(new Control[] { lblTongNV, lblTongLuong, lblLuongTB, lblCaoNhat, lblThapNhat });

            // Tab Control
            tabControl = new TabControl
            {
                BackColor = Color.FromArgb(60, 60, 60),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 10, FontStyle.Bold)
            };

            var tabThang = new TabPage("Báo cáo theo tháng") { BackColor = Color.FromArgb(50, 50, 50), ForeColor = Color.White };
            var tabNam = new TabPage("Báo cáo theo năm") { BackColor = Color.FromArgb(50, 50, 50), ForeColor = Color.White };
            var tabSoSanh = new TabPage("So sánh lương") { BackColor = Color.FromArgb(50, 50, 50), ForeColor = Color.White };

            CreateTab1Controls(tabThang);
            CreateTab2Controls(tabNam);
            CreateTab3Controls(tabSoSanh);

            tabControl.TabPages.AddRange(new TabPage[] { tabThang, tabNam, tabSoSanh });

            btnXuatExcel = CreateButton("Xuất Excel", Color.FromArgb(46, 125, 50));
            btnLamMoi = CreateButton("Làm mới", Color.FromArgb(96, 125, 139));

            this.Controls.AddRange(new Control[] { lblTitle, pnlThongKe, tabControl, btnXuatExcel, btnLamMoi });
        }

        private void CreateTab1Controls(TabPage tab)
        {
            var pnlFilter = new Panel
            {
                BackColor = Color.FromArgb(60, 60, 60),
                BorderStyle = BorderStyle.FixedSingle,
                Size = new Size(800, 60),
                Location = new Point(20, 20)
            };

            cmbThang = CreateComboBox();
            for (int i = 1; i <= 12; i++) cmbThang.Items.Add(i);
            cmbThang.SelectedItem = DateTime.Now.Month;

            cmbNam = CreateComboBox();
            for (int i = DateTime.Now.Year - 2; i <= DateTime.Now.Year + 1; i++) cmbNam.Items.Add(i);
            cmbNam.SelectedItem = DateTime.Now.Year;

            cmbPhongBan = CreateComboBox();
            cmbPhongBan.Items.AddRange(new[] { "Tất cả", "Bán hàng", "Kho", "Kế toán", "Bảo vệ" });
            cmbPhongBan.SelectedIndex = 0;

            btnXemBaoCao = CreateButton("Xem báo cáo", Color.FromArgb(33, 150, 243));

            pnlFilter.Controls.AddRange(new Control[] { 
                CreateLabel("Tháng:"), cmbThang, CreateLabel("Năm:"), cmbNam,
                CreateLabel("Phòng ban:"), cmbPhongBan, btnXemBaoCao 
            });

            dgvBaoCaoThang = CreateDataGridView();
            dgvBaoCaoThang.Location = new Point(20, 100);
            dgvBaoCaoThang.Size = new Size(800, 350);

            tab.Controls.AddRange(new Control[] { pnlFilter, dgvBaoCaoThang });

            // Layout
            pnlFilter.Controls[0].Location = new Point(20, 20);
            cmbThang.Location = new Point(80, 18);
            pnlFilter.Controls[2].Location = new Point(180, 20);
            cmbNam.Location = new Point(220, 18);
            pnlFilter.Controls[4].Location = new Point(320, 20);
            cmbPhongBan.Location = new Point(400, 18);
            btnXemBaoCao.Location = new Point(550, 16);
        }

        private void CreateTab2Controls(TabPage tab)
        {
            dgvBaoCaoNam = CreateDataGridView();
            dgvBaoCaoNam.Location = new Point(20, 20);
            dgvBaoCaoNam.Size = new Size(800, 400);
            tab.Controls.Add(dgvBaoCaoNam);
        }

        private void CreateTab3Controls(TabPage tab)
        {
            dgvSoSanh = CreateDataGridView();
            dgvSoSanh.Location = new Point(20, 20);
            dgvSoSanh.Size = new Size(800, 400);
            tab.Controls.Add(dgvSoSanh);
        }

        private Label CreateStatLabel(string text, Color color)
        {
            return new Label
            {
                Text = text,
                ForeColor = color,
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                AutoSize = true
            };
        }

        private Button CreateButton(string text, Color backColor)
        {
            return new Button
            {
                Text = text,
                BackColor = backColor,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Size = new Size(120, 35),
                Font = new Font("Segoe UI", 9, FontStyle.Bold)
            };
        }

        private ComboBox CreateComboBox()
        {
            return new ComboBox
            {
                BackColor = Color.FromArgb(70, 70, 70),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                DropDownStyle = ComboBoxStyle.DropDownList,
                Font = new Font("Segoe UI", 9),
                Size = new Size(80, 25)
            };
        }

        private DataGridView CreateDataGridView()
        {
            var dgv = new DataGridView
            {
                BackgroundColor = Color.FromArgb(60, 60, 60),
                ForeColor = Color.White,
                BorderStyle = BorderStyle.None,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                ReadOnly = true,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            };

            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(124, 77, 255);
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10.5F, FontStyle.Bold);
            dgv.DefaultCellStyle.BackColor = Color.FromArgb(60, 60, 60);
            dgv.DefaultCellStyle.ForeColor = Color.White;
            dgv.DefaultCellStyle.SelectionBackColor = Color.FromArgb(124, 77, 255);

            return dgv;
        }

        private Label CreateLabel(string text)
        {
            return new Label
            {
                Text = text,
                ForeColor = Color.White,
                AutoSize = true,
                Font = new Font("Segoe UI", 9)
            };
        }

        private void LayoutControls()
        {
            PerformLayout();
        }

        public void PerformLayout()
        {
            if (this.Controls.Count == 0) return;

            int formWidth = this.ClientSize.Width - 40;
            int formHeight = this.ClientSize.Height - 40;

            this.Controls[0].Location = new Point(20, 20);
            
            // Statistics Panel - responsive
            pnlThongKe.Location = new Point(20, 70);
            pnlThongKe.Size = new Size(formWidth, 80);
            
            // Adaptive statistics layout
            if (formWidth < 600) // Small screen - stack vertically
            {
                LayoutSalaryStatisticsVertical();
                pnlThongKe.Height = 120;
            }
            else if (formWidth < 900) // Medium screen - compact horizontal
            {
                LayoutSalaryStatisticsCompact();
                pnlThongKe.Height = 80;
            }
            else // Large screen - full horizontal
            {
                LayoutSalaryStatisticsFull();
                pnlThongKe.Height = 80;
            }

            // Tab Control - responsive
            tabControl.Location = new Point(20, pnlThongKe.Bottom + 20);
            tabControl.Size = new Size(formWidth, Math.Max(formHeight - (pnlThongKe.Bottom + 80), 400));

            // Action Buttons
            int buttonY = tabControl.Bottom + 10;
            btnXuatExcel.Location = new Point(20, buttonY);
            btnLamMoi.Location = new Point(150, buttonY);
        }

        private void LayoutSalaryStatisticsVertical()
        {
            // Stack salary statistics vertically for small screens
            lblTongNV.Location = new Point(20, 15);
            lblTongLuong.Location = new Point(200, 15);
            
            lblLuongTB.Location = new Point(20, 45);
            lblCaoNhat.Location = new Point(200, 45);
            lblThapNhat.Location = new Point(20, 75);
        }

        private void LayoutSalaryStatisticsCompact()
        {
            // Compact horizontal layout for medium screens
            int availableWidth = pnlThongKe.Width - 40;
            int spacing = Math.Max(120, availableWidth / 6);
            
            lblTongNV.Location = new Point(20, 30);
            lblTongLuong.Location = new Point(20 + spacing, 30);
            lblLuongTB.Location = new Point(20 + spacing * 2, 30);
            lblCaoNhat.Location = new Point(20 + spacing * 3, 30);
            lblThapNhat.Location = new Point(20 + spacing * 4, 30);
        }

        private void LayoutSalaryStatisticsFull()
        {
            // Full horizontal layout for large screens
            lblTongNV.Location = new Point(20, 30);
            lblTongLuong.Location = new Point(150, 30);
            lblLuongTB.Location = new Point(350, 30);
            lblCaoNhat.Location = new Point(550, 30);
            lblThapNhat.Location = new Point(750, 30);
        }

        private void SetupEventHandlers()
        {
            btnXemBaoCao.Click += btnXemBaoCao_Click;
            btnXuatExcel.Click += btnXuatExcel_Click;
            btnLamMoi.Click += btnLamMoi_Click;
            tabControl.SelectedIndexChanged += tabControl_SelectedIndexChanged;
        }

        private void InitializeForm()
        {
            if (!VuToanThang_23110329.Data.CurrentUser.HasPermission("VIEW_REPORTS"))
            {
                ShowMessage("Bạn không có quyền truy cập báo cáo!", "Cảnh báo", MessageBoxIcon.Warning);
                this.Close();
                return;
            }

            CreateControls();
            LayoutControls();
            SetupEventHandlers();
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
