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

        // UI Controls
        private ComboBox cmbThang, cmbNam;
        private NumericUpDown nudGioChuan, nudHeSoOT;
        private DataGridView dgvCongThang, dgvBangLuong;
        private Button btnXemCong, btnChayLuong, btnDongLuong, btnCapNhatPhuCap;
        private Panel pnlThongSo, pnlCongThang, pnlBangLuong;
        private TabControl tabControl;
        private Label lblTongNhanVien, lblTongLuong, lblTrangThai;

        public TinhLuongForm()
        {
            InitializeComponent();
            _bangLuongRepository = new BangLuongRepository();
            _chamCongRepository = new ChamCongRepository();
            InitializeForm();
        }


        private void CreateControls()
        {
            // Title
            var lblTitle = new Label
            {
                Text = "TÍNH LƯƠNG NHÂN VIÊN",
                Font = new Font("Segoe UI", 20, FontStyle.Bold),
                ForeColor = Color.FromArgb(124, 77, 255),
                AutoSize = true
            };

            // Parameters Panel
            pnlThongSo = new Panel
            {
                BackColor = Color.FromArgb(60, 60, 60),
                BorderStyle = BorderStyle.FixedSingle,
                Size = new Size(750, 100)
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

            var lblGioChuan = CreateLabel("Giờ chuẩn/tháng:");
            nudGioChuan = new NumericUpDown
            {
                Minimum = 100,
                Maximum = 300,
                Value = 208,
                DecimalPlaces = 1,
                BackColor = Color.FromArgb(70, 70, 70),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 9)
            };

            var lblHeSoOT = CreateLabel("Hệ số OT:");
            nudHeSoOT = new NumericUpDown
            {
                Minimum = 1,
                Maximum = 3,
                Value = 1.5m,
                DecimalPlaces = 2,
                Increment = 0.1m,
                BackColor = Color.FromArgb(70, 70, 70),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 9)
            };

            btnXemCong = CreateButton("Xem công tháng", Color.FromArgb(33, 150, 243));
            btnChayLuong = CreateButton("Chạy bảng lương", Color.FromArgb(46, 125, 50));
            btnDongLuong = CreateButton("Đóng bảng lương", Color.FromArgb(244, 67, 54));

            pnlThongSo.Controls.AddRange(new Control[] {
                lblThang, cmbThang, lblNam, cmbNam,
                lblGioChuan, nudGioChuan, lblHeSoOT, nudHeSoOT,
                btnXemCong, btnChayLuong, btnDongLuong
            });

            // Tab Control
            tabControl = new TabControl
            {
                BackColor = Color.FromArgb(60, 60, 60),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 10, FontStyle.Bold)
            };

            // Tab 1: Monthly Attendance Summary
            var tabCongThang = new TabPage("Tổng hợp công tháng")
            {
                BackColor = Color.FromArgb(50, 50, 50),
                ForeColor = Color.White
            };

            // Tab 2: Payroll
            var tabBangLuong = new TabPage("Bảng lương")
            {
                BackColor = Color.FromArgb(50, 50, 50),
                ForeColor = Color.White
            };

            CreateTab1Controls(tabCongThang);
            CreateTab2Controls(tabBangLuong);

            tabControl.TabPages.AddRange(new TabPage[] { tabCongThang, tabBangLuong });

            this.Controls.AddRange(new Control[] { lblTitle, pnlThongSo, tabControl });
        }

        private void CreateTab1Controls(TabPage tab)
        {
            // Attendance Summary Grid - responsive size
            dgvCongThang = CreateDataGridView();
            dgvCongThang.Location = new Point(20, 20);
            dgvCongThang.Size = new Size(700, 400); // Smaller default size
            dgvCongThang.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom;

            var lblInfo = new Label
            {
                Text = "Tổng hợp công tháng - Dữ liệu từ view vw_CongThang",
                ForeColor = Color.LightGray,
                Font = new Font("Segoe UI", 10, FontStyle.Italic),
                Location = new Point(20, 430),
                AutoSize = true,
                Anchor = AnchorStyles.Bottom | AnchorStyles.Left
            };

            tab.Controls.AddRange(new Control[] { dgvCongThang, lblInfo });
        }

        private void CreateTab2Controls(TabPage tab)
        {
            // Summary Panel - responsive
            var pnlSummary = new Panel
            {
                BackColor = Color.FromArgb(60, 60, 60),
                BorderStyle = BorderStyle.FixedSingle,
                Size = new Size(700, 60),
                Location = new Point(20, 20),
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right
            };

            lblTongNhanVien = CreateStatLabel("Tổng nhân viên: 0", Color.White);
            lblTongLuong = CreateStatLabel("Tổng lương: 0 VNĐ", Color.LightGreen);
            lblTrangThai = CreateStatLabel("Trạng thái: Chưa chạy", Color.Orange);

            pnlSummary.Controls.AddRange(new Control[] { lblTongNhanVien, lblTongLuong, lblTrangThai });

            // Payroll Grid - responsive
            dgvBangLuong = CreateDataGridView();
            dgvBangLuong.Location = new Point(20, 100);
            dgvBangLuong.Size = new Size(700, 300);
            dgvBangLuong.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom;

            // Update Allowance Panel - responsive
            var pnlCapNhat = new Panel
            {
                BackColor = Color.FromArgb(60, 60, 60),
                BorderStyle = BorderStyle.FixedSingle,
                Size = new Size(700, 80),
                Location = new Point(20, 420),
                Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right
            };

            var lblCapNhat = CreateLabel("Cập nhật phụ cấp/khấu trừ:");
            btnCapNhatPhuCap = CreateButton("Cập nhật hàng loạt", Color.FromArgb(156, 39, 176));

            pnlCapNhat.Controls.AddRange(new Control[] { lblCapNhat, btnCapNhatPhuCap });

            tab.Controls.AddRange(new Control[] { pnlSummary, dgvBangLuong, pnlCapNhat });

            // Layout summary controls
            lblTongNhanVien.Location = new Point(20, 20);
            lblTongLuong.Location = new Point(200, 20);
            lblTrangThai.Location = new Point(450, 20);

            // Layout update controls
            lblCapNhat.Location = new Point(20, 15);
            btnCapNhatPhuCap.Location = new Point(20, 35);
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

            // Title
            this.Controls[0].Location = new Point(20, 20);

            // Parameters Panel - responsive
            if (pnlThongSo != null)
            {
                pnlThongSo.Location = new Point(20, 70);
                pnlThongSo.Size = new Size(Math.Max(formWidth, 600), 100);

                // Adaptive parameter layout
                if (formWidth < 600) // Small screen - vertical stacking
                {
                    LayoutParametersVertical();
                    pnlThongSo.Height = 140;
                }
                else if (formWidth < 900) // Medium screen - compact
                {
                    LayoutParametersCompact();
                    pnlThongSo.Height = 100;
                }
                else // Large screen - full layout
                {
                    LayoutParametersFull();
                    pnlThongSo.Height = 100;
                }
            }

            // Tab Control - responsive
            if (tabControl != null)
            {
                tabControl.Location = new Point(20, pnlThongSo != null ? pnlThongSo.Bottom + 20 : 190);
                tabControl.Size = new Size(Math.Max(formWidth, 600), Math.Max(formHeight - (pnlThongSo != null ? pnlThongSo.Bottom + 40 : 210), 400));
                
                // Layout tab contents
                LayoutTabContents();
            }
        }

        private void LayoutParametersVertical()
        {
            // Stack parameters vertically for small screens
            var controls = pnlThongSo.Controls.Cast<Control>().ToArray();
            
            // Row 1: Month and Year
            controls[0].Location = new Point(20, 15); // lblThang
            cmbThang.Location = new Point(80, 13);
            cmbThang.Size = new Size(60, 25);
            
            controls[2].Location = new Point(150, 15); // lblNam
            cmbNam.Location = new Point(190, 13);
            cmbNam.Size = new Size(70, 25);

            // Row 2: Parameters
            controls[4].Location = new Point(20, 45); // lblGioChuan
            nudGioChuan.Location = new Point(130, 43);
            nudGioChuan.Size = new Size(70, 25);

            controls[6].Location = new Point(210, 45); // lblHeSoOT
            nudHeSoOT.Location = new Point(270, 43);
            nudHeSoOT.Size = new Size(70, 25);

            // Row 3: Buttons
            btnXemCong.Location = new Point(20, 75);
            btnXemCong.Size = new Size(100, 25);
            btnChayLuong.Location = new Point(130, 75);
            btnChayLuong.Size = new Size(100, 25);
            btnDongLuong.Location = new Point(240, 75);
            btnDongLuong.Size = new Size(100, 25);
        }

        private void LayoutParametersCompact()
        {
            // Compact horizontal layout for medium screens
            var controls = pnlThongSo.Controls.Cast<Control>().ToArray();
            
            // First row
            controls[0].Location = new Point(20, 15); // lblThang
            cmbThang.Location = new Point(20, 35);
            cmbThang.Size = new Size(60, 25);
            
            controls[2].Location = new Point(90, 15); // lblNam
            cmbNam.Location = new Point(90, 35);
            cmbNam.Size = new Size(60, 25);

            controls[4].Location = new Point(160, 15); // lblGioChuan
            nudGioChuan.Location = new Point(160, 35);
            nudGioChuan.Size = new Size(70, 25);

            controls[6].Location = new Point(240, 15); // lblHeSoOT
            nudHeSoOT.Location = new Point(240, 35);
            nudHeSoOT.Size = new Size(70, 25);

            // Buttons
            btnXemCong.Location = new Point(320, 25);
            btnXemCong.Size = new Size(90, 25);
            btnChayLuong.Location = new Point(420, 25);
            btnChayLuong.Size = new Size(90, 25);
            btnDongLuong.Location = new Point(520, 25);
            btnDongLuong.Size = new Size(90, 25);
        }

        private void LayoutParametersFull()
        {
            // Full horizontal layout for large screens
            var controls = pnlThongSo.Controls.Cast<Control>().ToArray();
            
            // First row
            controls[0].Location = new Point(20, 15); // lblThang
            cmbThang.Location = new Point(20, 35);
            cmbThang.Size = new Size(80, 25);
            
            controls[2].Location = new Point(120, 15); // lblNam
            cmbNam.Location = new Point(120, 35);
            cmbNam.Size = new Size(80, 25);

            controls[4].Location = new Point(220, 15); // lblGioChuan
            nudGioChuan.Location = new Point(220, 35);
            nudGioChuan.Size = new Size(80, 25);

            controls[6].Location = new Point(320, 15); // lblHeSoOT
            nudHeSoOT.Location = new Point(320, 35);
            nudHeSoOT.Size = new Size(80, 25);

            // Buttons
            btnXemCong.Location = new Point(450, 25);
            btnChayLuong.Location = new Point(590, 25);
            btnDongLuong.Location = new Point(730, 25);
        }

        private void LayoutTabContents()
        {
            if (tabControl?.TabPages == null) return;

            foreach (TabPage tab in tabControl.TabPages)
            {
                int tabWidth = tab.ClientSize.Width - 40;
                int tabHeight = tab.ClientSize.Height - 40;

                if (tab.Text == "Tổng hợp công tháng")
                {
                    // Layout attendance tab
                    if (dgvCongThang != null)
                    {
                        dgvCongThang.Size = new Size(Math.Max(tabWidth, 400), Math.Max(tabHeight - 60, 300));
                    }
                }
                else if (tab.Text == "Bảng lương")
                {
                    // Layout payroll tab
                    foreach (Control ctrl in tab.Controls)
                    {
                        if (ctrl is Panel panel)
                        {
                            if (panel.Location.Y == 20) // Summary panel
                            {
                                panel.Size = new Size(Math.Max(tabWidth, 400), 60);
                            }
                            else if (panel.Location.Y > 400) // Update panel
                            {
                                panel.Location = new Point(20, Math.Max(tabHeight - 60, 420));
                                panel.Size = new Size(Math.Max(tabWidth, 400), 60);
                            }
                        }
                        else if (ctrl == dgvBangLuong)
                        {
                            dgvBangLuong.Size = new Size(Math.Max(tabWidth, 400), Math.Max(tabHeight - 180, 250));
                        }
                    }
                }
            }
        }

        private void SetupEventHandlers()
        {
            btnXemCong.Click += btnXemCong_Click;
            btnChayLuong.Click += btnChayLuong_Click;
            btnDongLuong.Click += btnDongLuong_Click;
            btnCapNhatPhuCap.Click += btnCapNhatPhuCap_Click;

            cmbThang.SelectedIndexChanged += (s, e) => LoadPayrollData();
            cmbNam.SelectedIndexChanged += (s, e) => LoadPayrollData();

            dgvBangLuong.CellDoubleClick += dgvBangLuong_CellDoubleClick;
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

                // Create controls first
                CreateControls();
                LayoutControls();
                SetupEventHandlers();
                
                // Then load data
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

    // Enhanced forms for payroll management
    public partial class CapNhatPhuCapForm : Form
    {
        private readonly int _nam, _thang;
        private readonly BangLuongRepository _repository;
        private DataGridView dgvNhanVien;
        private NumericUpDown nudPhuCap, nudKhauTru, nudThueBH;
        private Button btnApplyAll, btnApplySelected;

        public CapNhatPhuCapForm(int nam, int thang)
        {
            InitializeComponent();
            _nam = nam;
            _thang = thang;
            _repository = new BangLuongRepository();
            this.Text = $"Cập nhật phụ cấp - {thang}/{nam}";
            this.Size = new Size(800, 600);
            this.BackColor = Color.FromArgb(50, 50, 50);
            CreateControls();
            LoadData();
        }

        private void CreateControls()
        {
            var lblTitle = new Label
            {
                Text = $"CẬP NHẬT PHỤ CẤP/KHẤU TRỪ - {_thang}/{_nam}",
                ForeColor = Color.FromArgb(124, 77, 255),
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                Location = new Point(20, 20),
                AutoSize = true
            };

            // Input panel
            var pnlInput = new Panel
            {
                BackColor = Color.FromArgb(60, 60, 60),
                BorderStyle = BorderStyle.FixedSingle,
                Size = new Size(760, 80),
                Location = new Point(20, 60)
            };

            var lblPhuCap = new Label { Text = "Phụ cấp:", ForeColor = Color.White, Location = new Point(20, 20), AutoSize = true };
            nudPhuCap = new NumericUpDown
            {
                Maximum = 10000000,
                DecimalPlaces = 0,
                Increment = 50000,
                BackColor = Color.FromArgb(70, 70, 70),
                ForeColor = Color.White,
                Size = new Size(100, 25),
                Location = new Point(20, 40)
            };

            var lblKhauTru = new Label { Text = "Khấu trừ:", ForeColor = Color.White, Location = new Point(140, 20), AutoSize = true };
            nudKhauTru = new NumericUpDown
            {
                Maximum = 10000000,
                DecimalPlaces = 0,
                Increment = 50000,
                BackColor = Color.FromArgb(70, 70, 70),
                ForeColor = Color.White,
                Size = new Size(100, 25),
                Location = new Point(140, 40)
            };

            var lblThueBH = new Label { Text = "Thuế BH:", ForeColor = Color.White, Location = new Point(260, 20), AutoSize = true };
            nudThueBH = new NumericUpDown
            {
                Maximum = 10000000,
                DecimalPlaces = 0,
                Increment = 10000,
                BackColor = Color.FromArgb(70, 70, 70),
                ForeColor = Color.White,
                Size = new Size(100, 25),
                Location = new Point(260, 40)
            };

            btnApplyAll = new Button
            {
                Text = "Áp dụng tất cả",
                BackColor = Color.FromArgb(46, 125, 50),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Size = new Size(120, 35),
                Location = new Point(400, 25)
            };

            btnApplySelected = new Button
            {
                Text = "Áp dụng đã chọn",
                BackColor = Color.FromArgb(33, 150, 243),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Size = new Size(120, 35),
                Location = new Point(530, 25)
            };

            pnlInput.Controls.AddRange(new Control[] { lblPhuCap, nudPhuCap, lblKhauTru, nudKhauTru, lblThueBH, nudThueBH, btnApplyAll, btnApplySelected });

            // DataGridView
            dgvNhanVien = new DataGridView
            {
                BackgroundColor = Color.FromArgb(60, 60, 60),
                ForeColor = Color.White,
                BorderStyle = BorderStyle.None,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                ReadOnly = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = true,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                Size = new Size(760, 400),
                Location = new Point(20, 160)
            };

            var btnClose = new Button
            {
                Text = "Đóng",
                BackColor = Color.FromArgb(158, 158, 158),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Size = new Size(80, 35),
                Location = new Point(700, 570),
                DialogResult = DialogResult.Cancel
            };

            this.Controls.AddRange(new Control[] { lblTitle, pnlInput, dgvNhanVien, btnClose });

            // Event handlers
            btnApplyAll.Click += BtnApplyAll_Click;
            btnApplySelected.Click += BtnApplySelected_Click;
        }

        private void LoadData()
        {
            try
            {
                var bangLuongs = _repository.GetByPeriod(_nam, _thang);
                dgvNhanVien.DataSource = bangLuongs;

                if (dgvNhanVien.Columns.Count > 0)
                {
                    dgvNhanVien.Columns["MaBangLuong"].Visible = false;
                    dgvNhanVien.Columns["MaNV"].Visible = false;
                    dgvNhanVien.Columns["Nam"].Visible = false;
                    dgvNhanVien.Columns["Thang"].Visible = false;
                    dgvNhanVien.Columns["TenNhanVien"].HeaderText = "Nhân viên";
                    dgvNhanVien.Columns["ChucDanh"].HeaderText = "Chức danh";
                    dgvNhanVien.Columns["PhuCap"].HeaderText = "Phụ cấp";
                    dgvNhanVien.Columns["KhauTru"].HeaderText = "Khấu trừ";
                    dgvNhanVien.Columns["ThueBH"].HeaderText = "Thuế BH";
                    dgvNhanVien.Columns["ThucLanh"].HeaderText = "Thực lãnh";

                    // Make editable columns
                    dgvNhanVien.Columns["PhuCap"].ReadOnly = false;
                    dgvNhanVien.Columns["KhauTru"].ReadOnly = false;
                    dgvNhanVien.Columns["ThueBH"].ReadOnly = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải dữ liệu: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnApplyAll_Click(object sender, EventArgs e)
        {
            ApplyToRows(dgvNhanVien.Rows.Cast<DataGridViewRow>().ToList());
        }

        private void BtnApplySelected_Click(object sender, EventArgs e)
        {
            ApplyToRows(dgvNhanVien.SelectedRows.Cast<DataGridViewRow>().ToList());
        }

        private void ApplyToRows(List<DataGridViewRow> rows)
        {
            if (rows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn ít nhất một nhân viên!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var result = MessageBox.Show($"Bạn có chắc chắn muốn cập nhật {rows.Count} nhân viên?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                try
                {
                    int updated = 0;
                    foreach (DataGridViewRow row in rows)
                    {
                        var bangLuong = (BangLuong)row.DataBoundItem;
                        var updateResult = _repository.UpdatePhuCapKhauTru(bangLuong.MaBangLuong, nudPhuCap.Value, nudKhauTru.Value, nudThueBH.Value);
                        if (updateResult.Success) updated++;
                    }

                    MessageBox.Show($"Đã cập nhật thành công {updated}/{rows.Count} nhân viên!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadData(); // Refresh data
                    this.DialogResult = DialogResult.OK;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi cập nhật: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }

    public partial class ChinhSuaLuongForm : Form
    {
        private readonly BangLuong _bangLuong;
        private readonly BangLuongRepository _repository;
        private NumericUpDown nudPhuCap, nudKhauTru, nudThueBH;
        private Label lblThucLanh;

        public ChinhSuaLuongForm(BangLuong bangLuong)
        {
            InitializeComponent();
            _bangLuong = bangLuong;
            _repository = new BangLuongRepository();
            this.Text = $"Chỉnh sửa lương - {bangLuong.TenNhanVien}";
            this.Size = new Size(500, 400);
            this.BackColor = Color.FromArgb(50, 50, 50);
            CreateControls();
            LoadData();
        }

        private void CreateControls()
        {
            var lblTitle = new Label
            {
                Text = $"CHỈNH SỬA LƯƠNG - {_bangLuong.TenNhanVien}",
                ForeColor = Color.FromArgb(124, 77, 255),
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                Location = new Point(20, 20),
                AutoSize = true
            };

            // Employee info panel
            var pnlInfo = new Panel
            {
                BackColor = Color.FromArgb(60, 60, 60),
                BorderStyle = BorderStyle.FixedSingle,
                Size = new Size(460, 100),
                Location = new Point(20, 60)
            };

            var lblNhanVien = new Label { Text = $"Nhân viên: {_bangLuong.TenNhanVien}", ForeColor = Color.White, Location = new Point(20, 15), AutoSize = true };
            var lblChucDanh = new Label { Text = $"Chức danh: {_bangLuong.ChucDanh}", ForeColor = Color.White, Location = new Point(20, 35), AutoSize = true };
            var lblLuongCB = new Label { Text = $"Lương cơ bản: {_bangLuong.LuongCoBan:N0} VNĐ", ForeColor = Color.White, Location = new Point(20, 55), AutoSize = true };
            var lblGioCong = new Label { Text = $"Giờ công: {_bangLuong.TongGioCong:F2} | OT: {_bangLuong.GioOT:F2}", ForeColor = Color.White, Location = new Point(250, 55), AutoSize = true };

            pnlInfo.Controls.AddRange(new Control[] { lblNhanVien, lblChucDanh, lblLuongCB, lblGioCong });

            // Edit panel
            var pnlEdit = new Panel
            {
                BackColor = Color.FromArgb(60, 60, 60),
                BorderStyle = BorderStyle.FixedSingle,
                Size = new Size(460, 120),
                Location = new Point(20, 180)
            };

            var lblPhuCap = new Label { Text = "Phụ cấp:", ForeColor = Color.White, Location = new Point(20, 20), AutoSize = true };
            nudPhuCap = new NumericUpDown
            {
                Maximum = 10000000,
                DecimalPlaces = 0,
                Increment = 50000,
                BackColor = Color.FromArgb(70, 70, 70),
                ForeColor = Color.White,
                Size = new Size(120, 25),
                Location = new Point(100, 18)
            };

            var lblKhauTru = new Label { Text = "Khấu trừ:", ForeColor = Color.White, Location = new Point(20, 50), AutoSize = true };
            nudKhauTru = new NumericUpDown
            {
                Maximum = 10000000,
                DecimalPlaces = 0,
                Increment = 50000,
                BackColor = Color.FromArgb(70, 70, 70),
                ForeColor = Color.White,
                Size = new Size(120, 25),
                Location = new Point(100, 48)
            };

            var lblThueBH = new Label { Text = "Thuế BH:", ForeColor = Color.White, Location = new Point(20, 80), AutoSize = true };
            nudThueBH = new NumericUpDown
            {
                Maximum = 10000000,
                DecimalPlaces = 0,
                Increment = 10000,
                BackColor = Color.FromArgb(70, 70, 70),
                ForeColor = Color.White,
                Size = new Size(120, 25),
                Location = new Point(100, 78)
            };

            lblThucLanh = new Label 
            { 
                Text = $"Thực lãnh: {_bangLuong.ThucLanh:N0} VNĐ", 
                ForeColor = Color.LightGreen, 
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                Location = new Point(250, 50), 
                AutoSize = true 
            };

            pnlEdit.Controls.AddRange(new Control[] { lblPhuCap, nudPhuCap, lblKhauTru, nudKhauTru, lblThueBH, nudThueBH, lblThucLanh });

            // Buttons
            var btnSave = new Button
            {
                Text = "Lưu",
                BackColor = Color.FromArgb(46, 125, 50),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Size = new Size(80, 35),
                Location = new Point(300, 320),
                DialogResult = DialogResult.OK
            };

            var btnClose = new Button
            {
                Text = "Hủy",
                BackColor = Color.FromArgb(158, 158, 158),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Size = new Size(80, 35),
                Location = new Point(400, 320),
                DialogResult = DialogResult.Cancel
            };

            this.Controls.AddRange(new Control[] { lblTitle, pnlInfo, pnlEdit, btnSave, btnClose });

            // Event handlers
            btnSave.Click += BtnSave_Click;
            nudPhuCap.ValueChanged += UpdateThucLanh;
            nudKhauTru.ValueChanged += UpdateThucLanh;
            nudThueBH.ValueChanged += UpdateThucLanh;
        }

        private void LoadData()
        {
            nudPhuCap.Value = _bangLuong.PhuCap;
            nudKhauTru.Value = _bangLuong.KhauTru;
            nudThueBH.Value = _bangLuong.ThueBH;
        }

        private void UpdateThucLanh(object sender, EventArgs e)
        {
            // Calculate new ThucLanh
            decimal luongGoc = _bangLuong.LuongCoBan + (_bangLuong.GioOT * _bangLuong.LuongCoBan / 208 * 1.5m);
            decimal thucLanh = luongGoc + nudPhuCap.Value - nudKhauTru.Value - nudThueBH.Value;
            lblThucLanh.Text = $"Thực lãnh: {thucLanh:N0} VNĐ";
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                var result = _repository.UpdatePhuCapKhauTru(_bangLuong.MaBangLuong, nudPhuCap.Value, nudKhauTru.Value, nudThueBH.Value);
                if (result.Success)
                {
                    MessageBox.Show("Cập nhật thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show(result.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.DialogResult = DialogResult.None;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi cập nhật: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.DialogResult = DialogResult.None;
            }
        }
    }
}
