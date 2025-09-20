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

        // UI Controls
        private TabControl tabControl;
        private DataGridView dgvTongQuan, dgvChamCong, dgvDonTu;
        private ComboBox cmbThang, cmbNam, cmbPhongBan, cmbTrangThai;
        private DateTimePicker dtpTuNgay, dtpDenNgay;
        private Button btnXuatBaoCao, btnLamMoi, btnTimKiem;
        private Panel pnlThongKe;
        private Label lblTongNV, lblDangLam, lblNghi, lblTongCong, lblTongDonTu;

        public BaoCaoNhanSuForm()
        {
            InitializeComponent();
            _nhanVienRepository = new NhanVienRepository();
            _chamCongRepository = new ChamCongRepository();
            _donTuRepository = new DonTuRepository();
            InitializeForm();
        }


        private void CreateControls()
        {
            // Title
            var lblTitle = new Label
            {
                Text = "BÁO CÁO NHÂN SỰ",
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
            lblDangLam = CreateStatLabel("Đang làm: 0", Color.LightGreen);
            lblNghi = CreateStatLabel("Nghỉ việc: 0", Color.LightCoral);
            lblTongCong = CreateStatLabel("Tổng công: 0h", Color.LightBlue);
            lblTongDonTu = CreateStatLabel("Tổng đơn từ: 0", Color.Orange);

            pnlThongKe.Controls.AddRange(new Control[] { lblTongNV, lblDangLam, lblNghi, lblTongCong, lblTongDonTu });

            // Tab Control
            tabControl = new TabControl
            {
                BackColor = Color.FromArgb(60, 60, 60),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 10, FontStyle.Bold)
            };

            // Tab 1: Employee Overview
            var tabTongQuan = new TabPage("Tổng quan nhân viên")
            {
                BackColor = Color.FromArgb(50, 50, 50),
                ForeColor = Color.White
            };

            // Tab 2: Attendance Report
            var tabChamCong = new TabPage("Báo cáo chấm công")
            {
                BackColor = Color.FromArgb(50, 50, 50),
                ForeColor = Color.White
            };

            // Tab 3: Request Report
            var tabDonTu = new TabPage("Báo cáo đơn từ")
            {
                BackColor = Color.FromArgb(50, 50, 50),
                ForeColor = Color.White
            };

            CreateTab1Controls(tabTongQuan);
            CreateTab2Controls(tabChamCong);
            CreateTab3Controls(tabDonTu);

            tabControl.TabPages.AddRange(new TabPage[] { tabTongQuan, tabChamCong, tabDonTu });

            // Action Buttons
            btnXuatBaoCao = CreateButton("Xuất báo cáo", Color.FromArgb(46, 125, 50));
            btnLamMoi = CreateButton("Làm mới", Color.FromArgb(96, 125, 139));

            this.Controls.AddRange(new Control[] { lblTitle, pnlThongKe, tabControl, btnXuatBaoCao, btnLamMoi });
        }

        private void CreateTab1Controls(TabPage tab)
        {
            // Filter Panel - responsive size
            var pnlFilter1 = new Panel
            {
                BackColor = Color.FromArgb(60, 60, 60),
                BorderStyle = BorderStyle.FixedSingle,
                Size = new Size(800, 60), // Reduced from 1320 to 800
                Location = new Point(20, 20)
            };

            var lblPhongBan = CreateLabel("Phòng ban:");
            cmbPhongBan = CreateComboBox();
            cmbPhongBan.Items.AddRange(new[] { "Tất cả", "Bán hàng", "Kho", "Kế toán", "Bảo vệ" });
            cmbPhongBan.SelectedIndex = 0;

            var lblTrangThai = CreateLabel("Trạng thái:");
            cmbTrangThai = CreateComboBox();
            cmbTrangThai.Items.AddRange(new[] { "Tất cả", "DangLam", "Nghi" });
            cmbTrangThai.SelectedIndex = 0;

            var btnTimKiem1 = CreateButton("Tìm kiếm", Color.FromArgb(33, 150, 243));

            pnlFilter1.Controls.AddRange(new Control[] { lblPhongBan, cmbPhongBan, lblTrangThai, cmbTrangThai, btnTimKiem1 });

            // Employee Overview Grid - responsive size
            dgvTongQuan = CreateDataGridView();
            dgvTongQuan.Location = new Point(20, 100);
            dgvTongQuan.Size = new Size(800, 400); // Reduced from 1320x450 to 800x400

            tab.Controls.AddRange(new Control[] { pnlFilter1, dgvTongQuan });

            // Layout filter controls
            lblPhongBan.Location = new Point(20, 20);
            cmbPhongBan.Location = new Point(100, 18);
            cmbPhongBan.Size = new Size(150, 25);

            lblTrangThai.Location = new Point(270, 20);
            cmbTrangThai.Location = new Point(350, 18);
            cmbTrangThai.Size = new Size(120, 25);

            btnTimKiem1.Location = new Point(490, 16);
        }

        private void CreateTab2Controls(TabPage tab)
        {
            // Filter Panel - responsive size
            var pnlFilter2 = new Panel
            {
                BackColor = Color.FromArgb(60, 60, 60),
                BorderStyle = BorderStyle.FixedSingle,
                Size = new Size(800, 60), // Reduced from 1320 to 800
                Location = new Point(20, 20)
            };

            var lblThang = CreateLabel("Tháng:");
            cmbThang = CreateComboBox();
            for (int i = 1; i <= 12; i++)
                cmbThang.Items.Add(i);
            cmbThang.SelectedItem = DateTime.Now.Month;

            var lblNam = CreateLabel("Năm:");
            cmbNam = CreateComboBox();
            for (int i = DateTime.Now.Year - 2; i <= DateTime.Now.Year + 1; i++)
                cmbNam.Items.Add(i);
            cmbNam.SelectedItem = DateTime.Now.Year;

            var btnTimKiem2 = CreateButton("Xem báo cáo", Color.FromArgb(33, 150, 243));

            pnlFilter2.Controls.AddRange(new Control[] { lblThang, cmbThang, lblNam, cmbNam, btnTimKiem2 });

            // Attendance Report Grid - responsive size
            dgvChamCong = CreateDataGridView();
            dgvChamCong.Location = new Point(20, 100);
            dgvChamCong.Size = new Size(800, 400); // Reduced from 1320x450 to 800x400

            tab.Controls.AddRange(new Control[] { pnlFilter2, dgvChamCong });

            // Layout filter controls
            lblThang.Location = new Point(20, 20);
            cmbThang.Location = new Point(80, 18);
            cmbThang.Size = new Size(80, 25);

            lblNam.Location = new Point(180, 20);
            cmbNam.Location = new Point(220, 18);
            cmbNam.Size = new Size(80, 25);

            btnTimKiem2.Location = new Point(320, 16);
            btnTimKiem2.Click += btnXemBaoCaoCong_Click;
        }

        private void CreateTab3Controls(TabPage tab)
        {
            // Filter Panel - responsive size
            var pnlFilter3 = new Panel
            {
                BackColor = Color.FromArgb(60, 60, 60),
                BorderStyle = BorderStyle.FixedSingle,
                Size = new Size(800, 60), // Reduced from 1320 to 800
                Location = new Point(20, 20)
            };

            var lblTuNgay = CreateLabel("Từ ngày:");
            dtpTuNgay = CreateDatePicker();
            dtpTuNgay.Value = DateTime.Now.AddDays(-30);

            var lblDenNgay = CreateLabel("Đến ngày:");
            dtpDenNgay = CreateDatePicker();

            var btnTimKiem3 = CreateButton("Xem báo cáo", Color.FromArgb(33, 150, 243));

            pnlFilter3.Controls.AddRange(new Control[] { lblTuNgay, dtpTuNgay, lblDenNgay, dtpDenNgay, btnTimKiem3 });

            // Request Report Grid - responsive size
            dgvDonTu = CreateDataGridView();
            dgvDonTu.Location = new Point(20, 100);
            dgvDonTu.Size = new Size(800, 400); // Reduced from 1320x450 to 800x400

            tab.Controls.AddRange(new Control[] { pnlFilter3, dgvDonTu });

            // Layout filter controls
            lblTuNgay.Location = new Point(20, 20);
            dtpTuNgay.Location = new Point(80, 18);
            dtpTuNgay.Size = new Size(120, 25);

            lblDenNgay.Location = new Point(220, 20);
            dtpDenNgay.Location = new Point(290, 18);
            dtpDenNgay.Size = new Size(120, 25);

            btnTimKiem3.Location = new Point(430, 16);
            btnTimKiem3.Click += btnXemBaoCaoDonTu_Click;
        }

        private Label CreateStatLabel(string text, Color color)
        {
            return new Label
            {
                Text = text,
                ForeColor = color,
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
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
                Font = new Font("Segoe UI", 9)
            };
        }

        private DateTimePicker CreateDatePicker()
        {
            return new DateTimePicker
            {
                Format = DateTimePickerFormat.Short,
                Font = new Font("Segoe UI", 9)
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

            // Title
            this.Controls[0].Location = new Point(20, 20);

            // Statistics Panel - responsive
            pnlThongKe.Location = new Point(20, 70);
            pnlThongKe.Size = new Size(formWidth, 80);

            // Adaptive statistics layout
            if (formWidth < 600) // Small screen - stack vertically
            {
                LayoutStatisticsVertical();
                pnlThongKe.Height = 100;
            }
            else if (formWidth < 900) // Medium screen - compact horizontal
            {
                LayoutStatisticsCompact();
                pnlThongKe.Height = 80;
            }
            else // Large screen - full horizontal
            {
                LayoutStatisticsFull();
                pnlThongKe.Height = 80;
            }

            // Tab Control - responsive with larger minimum size
            tabControl.Location = new Point(20, pnlThongKe.Bottom + 20);
            tabControl.Size = new Size(formWidth, Math.Max(formHeight - (pnlThongKe.Bottom + 80), 550)); // Increased from 400 to 550

            // Layout tab contents responsively
            LayoutTabContents();

            // Action Buttons - position them properly even if they go outside visible area
            int buttonY = Math.Min(tabControl.Bottom + 10, formHeight - 30); // Ensure buttons are visible
            btnXuatBaoCao.Location = new Point(20, buttonY);
            btnLamMoi.Location = new Point(150, buttonY);
        }

        private void LayoutTabContents()
        {
            if (tabControl?.TabPages == null) return;

            int tabWidth = tabControl.Width - 40;
            int tabHeight = tabControl.Height - 100; // Account for tab headers and padding
            
            // Ensure minimum sizes
            tabWidth = Math.Max(tabWidth, 600);
            tabHeight = Math.Max(tabHeight, 400);

            // Layout each tab's contents
            foreach (TabPage tab in tabControl.TabPages)
            {
                if (tab.Controls.Count >= 2) // Should have filter panel and DataGridView
                {
                    // Filter Panel - make responsive
                    var filterPanel = tab.Controls[0] as Panel;
                    if (filterPanel != null)
                    {
                        filterPanel.Size = new Size(tabWidth, 60);
                    }

                    // DataGridView - make responsive
                    var dataGridView = tab.Controls[1] as DataGridView;
                    if (dataGridView != null)
                    {
                        dataGridView.Location = new Point(20, 100);
                        dataGridView.Size = new Size(tabWidth, Math.Max(tabHeight - 120, 350)); // Min 350px height
                    }
                }
            }
        }

        private void LayoutStatisticsVertical()
        {
            // Stack statistics vertically for small screens
            lblTongNV.Location = new Point(20, 15);
            lblDangLam.Location = new Point(150, 15);
            lblNghi.Location = new Point(280, 15);
            
            lblTongCong.Location = new Point(20, 45);
            lblTongDonTu.Location = new Point(150, 45);
        }

        private void LayoutStatisticsCompact()
        {
            // Compact horizontal layout for medium screens
            int availableWidth = pnlThongKe.Width - 40;
            int spacing = Math.Max(100, availableWidth / 6);
            
            lblTongNV.Location = new Point(20, 30);
            lblDangLam.Location = new Point(20 + spacing, 30);
            lblNghi.Location = new Point(20 + spacing * 2, 30);
            lblTongCong.Location = new Point(20 + spacing * 3, 30);
            lblTongDonTu.Location = new Point(20 + spacing * 4, 30);
        }

        private void LayoutStatisticsFull()
        {
            // Full horizontal layout for large screens
            lblTongNV.Location = new Point(20, 30);
            lblDangLam.Location = new Point(150, 30);
            lblNghi.Location = new Point(280, 30);
            lblTongCong.Location = new Point(410, 30);
            lblTongDonTu.Location = new Point(540, 30);
        }

        private void SetupEventHandlers()
        {
            btnXuatBaoCao.Click += btnXuatBaoCao_Click;
            btnLamMoi.Click += btnLamMoi_Click;

            // Tab change event
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
