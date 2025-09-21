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
    public partial class ChamCongForm : Form
    {
        private readonly ChamCongRepository _chamCongRepository;
        private readonly NhanVienRepository _nhanVienRepository;
        private ChamCong _currentChamCong;
        private bool _isEditing = false;

        // UI Controls
        private DataGridView dgvChamCong, dgvLichChamCong;
        private DateTimePicker dtpTuNgay, dtpDenNgay, dtpGioVao, dtpGioRa;
        private ComboBox cmbNhanVien, cmbThangKhoa, cmbNamKhoa;
        private TextBox txtGhiChu;
        private Button btnTimKiem, btnCapNhat, btnKhoaCong, btnLamMoi, btnXuatBaoCao;
        private Button btnCheckIn, btnCheckOut, btnRefreshStatus;
        private Panel pnlFilter, pnlThongTin, pnlCheckInOut;
        private TabControl tabControl;
        private Label lblTongGioCong, lblDiTre, lblVeSom;
        private Label lblTrangThaiHienTai, lblThongTinCa, lblThongTinChamCong;
        private TrangThaiChamCong _currentStatus;
        private System.Windows.Forms.Timer _refreshTimer;

        public ChamCongForm()
        {
            InitializeComponent();
            _chamCongRepository = new ChamCongRepository();
            _nhanVienRepository = new NhanVienRepository();
            CreateControls();
            LayoutControls();
            SetupEventHandlers();
            InitializeForm();
        }


        private void CreateControls()
        {
            // Title
            var lblTitle = new Label
            {
                Text = "QUẢN LÝ CHẤM CÔNG",
                Font = new Font("Segoe UI", 20, FontStyle.Bold),
                ForeColor = Color.FromArgb(124, 77, 255),
                AutoSize = true
            };

            // Tab Control
            tabControl = new TabControl
            {
                BackColor = Color.FromArgb(60, 60, 60),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 10, FontStyle.Bold)
            };

            // Tab 1: Attendance Records
            var tabChamCong = new TabPage("Chấm công hàng ngày")
            {
                BackColor = Color.FromArgb(50, 50, 50),
                ForeColor = Color.White
            };

            // Tab 2: Schedule & Attendance View
            var tabLichChamCong = new TabPage("Lịch & chấm công")
            {
                BackColor = Color.FromArgb(50, 50, 50),
                ForeColor = Color.White
            };

            // Tab 3: Check In/Out for Employees
            var tabCheckInOut = new TabPage("Check In/Out")
            {
                BackColor = Color.FromArgb(50, 50, 50),
                ForeColor = Color.White
            };

            // Tab 4: Lock Period
            var tabKhoaCong = new TabPage("Khóa công kỳ")
            {
                BackColor = Color.FromArgb(50, 50, 50),
                ForeColor = Color.White
            };

            CreateTab1Controls(tabChamCong);
            CreateTab2Controls(tabLichChamCong);
            CreateTab3Controls(tabCheckInOut);
            CreateTab4Controls(tabKhoaCong);

            tabControl.TabPages.AddRange(new TabPage[] { tabChamCong, tabLichChamCong, tabCheckInOut, tabKhoaCong });

            this.Controls.AddRange(new Control[] { lblTitle, tabControl });
        }

        private void CreateTab1Controls(TabPage tab)
        {
            // Filter Panel
            pnlFilter = new Panel
            {
                BackColor = Color.FromArgb(60, 60, 60),
                BorderStyle = BorderStyle.FixedSingle,
                Size = new Size(800, 80),
                Location = new Point(20, 20)
            };

            var lblTuNgay = CreateLabel("Từ ngày:");
            dtpTuNgay = CreateDatePicker();
            dtpTuNgay.Value = DateTime.Now.AddDays(-7);

            var lblDenNgay = CreateLabel("Đến ngày:");
            dtpDenNgay = CreateDatePicker();

            var lblNhanVien = CreateLabel("Nhân viên:");
            cmbNhanVien = CreateComboBox();

            btnTimKiem = CreateButton("Tìm kiếm", Color.FromArgb(33, 150, 243));
            btnLamMoi = CreateButton("Làm mới", Color.FromArgb(96, 125, 139));

            pnlFilter.Controls.AddRange(new Control[] { 
                lblTuNgay, dtpTuNgay, lblDenNgay, dtpDenNgay, 
                lblNhanVien, cmbNhanVien, btnTimKiem, btnLamMoi 
            });

            // DataGridView for attendance records
            dgvChamCong = CreateDataGridView();
            dgvChamCong.Location = new Point(20, 120);
            dgvChamCong.Size = new Size(500, 300);

            // Information Panel
            pnlThongTin = new Panel
            {
                BackColor = Color.FromArgb(60, 60, 60),
                BorderStyle = BorderStyle.FixedSingle,
                Padding = new Padding(15),
                Location = new Point(540, 120),
                Size = new Size(300, 300)
            };

            CreateInfoControls();

            tab.Controls.AddRange(new Control[] { pnlFilter, dgvChamCong, pnlThongTin });
        }

        private void CreateTab2Controls(TabPage tab)
        {
            // Schedule & Attendance Combined View
            dgvLichChamCong = CreateDataGridView();
            dgvLichChamCong.Location = new Point(20, 80);
            dgvLichChamCong.Size = new Size(800, 400);

            var lblTitle2 = new Label
            {
                Text = "LỊCH PHÂN CA & CHẤM CÔNG",
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                ForeColor = Color.FromArgb(124, 77, 255),
                AutoSize = true,
                Location = new Point(20, 20)
            };

            // Filter for this tab
            var pnlFilter2 = new Panel
            {
                BackColor = Color.FromArgb(60, 60, 60),
                BorderStyle = BorderStyle.FixedSingle,
                Size = new Size(1320, 50),
                Location = new Point(20, 50)
            };

            var dtpTuNgay2 = CreateDatePicker();
            dtpTuNgay2.Value = DateTime.Now.AddDays(-7);
            dtpTuNgay2.Location = new Point(80, 15);

            var dtpDenNgay2 = CreateDatePicker();
            dtpDenNgay2.Value = DateTime.Now;
            dtpDenNgay2.Location = new Point(220, 15);

            var btnTimKiem2 = CreateButton("Tìm kiếm", Color.FromArgb(33, 150, 243));
            btnTimKiem2.Location = new Point(360, 13);

            pnlFilter2.Controls.AddRange(new Control[] { 
                CreateLabelAt("Từ:", new Point(10, 18)), dtpTuNgay2,
                CreateLabelAt("Đến:", new Point(190, 18)), dtpDenNgay2, btnTimKiem2 
            });

            tab.Controls.AddRange(new Control[] { lblTitle2, pnlFilter2, dgvLichChamCong });
        }

        private void CreateTab3Controls(TabPage tab)
        {
            var lblTitle3 = new Label
            {
                Text = "CHECK IN / CHECK OUT",
                Font = new Font("Segoe UI", 18, FontStyle.Bold),
                ForeColor = Color.FromArgb(124, 77, 255),
                AutoSize = true,
                Location = new Point(20, 20)
            };

            // Panel chính cho check in/out
            pnlCheckInOut = new Panel
            {
                BackColor = Color.FromArgb(60, 60, 60),
                BorderStyle = BorderStyle.FixedSingle,
                Size = new Size(600, 400),
                Location = new Point(20, 70)
            };

            // Trạng thái hiện tại
            lblTrangThaiHienTai = new Label
            {
                Text = "Đang tải trạng thái...",
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                ForeColor = Color.White,
                AutoSize = true,
                Location = new Point(20, 20)
            };

            // Thông tin ca làm việc
            lblThongTinCa = new Label
            {
                Text = "Thông tin ca: Đang tải...",
                Font = new Font("Segoe UI", 12),
                ForeColor = Color.LightGray,
                AutoSize = true,
                Location = new Point(20, 60)
            };

            // Thông tin chấm công
            lblThongTinChamCong = new Label
            {
                Text = "Chấm công: Đang tải...",
                Font = new Font("Segoe UI", 12),
                ForeColor = Color.LightGray,
                AutoSize = true,
                Location = new Point(20, 90)
            };

            // Buttons
            btnCheckIn = new Button
            {
                Text = "CHECK IN",
                BackColor = Color.FromArgb(46, 125, 50),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Size = new Size(120, 50),
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                Location = new Point(50, 150)
            };

            btnCheckOut = new Button
            {
                Text = "CHECK OUT",
                BackColor = Color.FromArgb(244, 67, 54),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Size = new Size(120, 50),
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                Location = new Point(200, 150)
            };

            btnRefreshStatus = new Button
            {
                Text = "Làm mới",
                BackColor = Color.FromArgb(96, 125, 139),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Size = new Size(100, 35),
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Location = new Point(350, 165)
            };

            // Hướng dẫn
            var lblHuongDan = new Label
            {
                Text = "• Check In: Chấm công vào khi bắt đầu ca làm việc\n" +
                       "• Chỉ được check in sớm tối đa 15 phút trước giờ bắt đầu ca\n" +
                       "• Check Out: Chấm công ra khi kết thúc ca làm việc\n" +
                       "• Chỉ có thể check in/out trong ngày có lịch làm việc\n" +
                       "• Không thể thay đổi sau khi công đã bị khóa",
                Font = new Font("Segoe UI", 10),
                ForeColor = Color.Orange,
                Size = new Size(500, 120),
                Location = new Point(20, 250)
            };

            pnlCheckInOut.Controls.AddRange(new Control[] {
                lblTrangThaiHienTai, lblThongTinCa, lblThongTinChamCong,
                btnCheckIn, btnCheckOut, btnRefreshStatus, lblHuongDan
            });

            tab.Controls.AddRange(new Control[] { lblTitle3, pnlCheckInOut });
        }

        private void CreateTab4Controls(TabPage tab)
        {
            var lblTitle3 = new Label
            {
                Text = "KHÓA CÔNG KỲ",
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                ForeColor = Color.FromArgb(124, 77, 255),
                AutoSize = true,
                Location = new Point(20, 20)
            };

            var lblThang = CreateLabelAt("Tháng:", new Point(50, 80));
            cmbThangKhoa = CreateComboBox();
            cmbThangKhoa.Location = new Point(120, 77);
            cmbThangKhoa.Size = new Size(100, 25);
            for (int i = 1; i <= 12; i++)
                cmbThangKhoa.Items.Add(i);
            cmbThangKhoa.SelectedItem = DateTime.Now.Month;

            var lblNam = CreateLabelAt("Năm:", new Point(250, 80));
            cmbNamKhoa = CreateComboBox();
            cmbNamKhoa.Location = new Point(300, 77);
            cmbNamKhoa.Size = new Size(100, 25);
            for (int i = DateTime.Now.Year - 2; i <= DateTime.Now.Year + 1; i++)
                cmbNamKhoa.Items.Add(i);
            cmbNamKhoa.SelectedItem = DateTime.Now.Year;

            btnKhoaCong = CreateButton("Khóa công", Color.FromArgb(244, 67, 54));
            btnKhoaCong.Location = new Point(450, 75);
            btnKhoaCong.Size = new Size(100, 30);

            var lblWarning = new Label
            {
                Text = "⚠️ Cảnh báo: Sau khi khóa công, không thể chỉnh sửa dữ liệu chấm công trong kỳ này!",
                ForeColor = Color.Orange,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Location = new Point(50, 130),
                Size = new Size(600, 40)
            };

            tab.Controls.AddRange(new Control[] { lblTitle3, lblThang, cmbThangKhoa, lblNam, cmbNamKhoa, btnKhoaCong, lblWarning });
        }

        private void CreateInfoControls()
        {
            var lblThongTin = new Label
            {
                Text = "THÔNG TIN CHẤM CÔNG",
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                ForeColor = Color.FromArgb(124, 77, 255),
                AutoSize = true
            };

            var lblGioVao = CreateLabel("Giờ vào:");
            dtpGioVao = CreateTimePicker();

            var lblGioRa = CreateLabel("Giờ ra:");
            dtpGioRa = CreateTimePicker();

            var lblGhiChu = CreateLabel("Ghi chú:");
            txtGhiChu = CreateTextBox();
            txtGhiChu.Multiline = true;
            txtGhiChu.Height = 60;

            btnCapNhat = CreateButton("Cập nhật", Color.FromArgb(46, 125, 50));
            btnCapNhat.Size = new Size(100, 35);

            // Summary labels
            lblTongGioCong = CreateLabel("Tổng giờ công: 0");
            lblDiTre = CreateLabel("Đi trễ: 0 phút");
            lblVeSom = CreateLabel("Về sớm: 0 phút");

            lblTongGioCong.ForeColor = Color.LightGreen;
            lblDiTre.ForeColor = Color.Orange;
            lblVeSom.ForeColor = Color.Orange;

            pnlThongTin.Controls.AddRange(new Control[] {
                lblThongTin, lblGioVao, dtpGioVao, lblGioRa, dtpGioRa,
                lblGhiChu, txtGhiChu, btnCapNhat,
                lblTongGioCong, lblDiTre, lblVeSom
            });

            LayoutInfoControls();
        }

        private void LayoutInfoControls()
        {
            int y = 10;
            int spacing = 35;

            pnlThongTin.Controls[0].Location = new Point(10, y); // Title
            y += 40;

            pnlThongTin.Controls[1].Location = new Point(10, y); // lblGioVao
            dtpGioVao.Location = new Point(10, y + 20);
            dtpGioVao.Size = new Size(150, 25);
            y += 50;

            pnlThongTin.Controls[3].Location = new Point(10, y); // lblGioRa
            dtpGioRa.Location = new Point(10, y + 20);
            dtpGioRa.Size = new Size(150, 25);
            y += 50;

            pnlThongTin.Controls[5].Location = new Point(10, y); // lblGhiChu
            txtGhiChu.Location = new Point(10, y + 20);
            txtGhiChu.Size = new Size(350, 60);
            y += 90;

            btnCapNhat.Location = new Point(10, y);
            y += 50;

            lblTongGioCong.Location = new Point(10, y);
            y += 25;
            lblDiTre.Location = new Point(10, y);
            y += 25;
            lblVeSom.Location = new Point(10, y);
        }

        private Button CreateButton(string text, Color backColor)
        {
            return new Button
            {
                Text = text,
                BackColor = backColor,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Size = new Size(80, 30),
                Font = new Font("Segoe UI", 9, FontStyle.Bold)
            };
        }

        private TextBox CreateTextBox()
        {
            return new TextBox
            {
                BackColor = Color.FromArgb(70, 70, 70),
                ForeColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                Font = new Font("Segoe UI", 9)
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
                Font = new Font("Segoe UI", 9),
                Size = new Size(120, 25)
            };
        }

        private DateTimePicker CreateTimePicker()
        {
            return new DateTimePicker
            {
                Format = DateTimePickerFormat.Time,
                ShowUpDown = true,
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

        private Label CreateLabelAt(string text, Point location)
        {
            var lbl = CreateLabel(text);
            lbl.Location = location;
            return lbl;
        }

        private void LayoutControls()
        {
            PerformLayout();
        }

        public void PerformLayout()
        {
            if (this.Controls.Count == 0) return;

            int formWidth = this.ClientSize.Width - 40; // Account for padding
            int formHeight = this.ClientSize.Height - 40;

            // Title
            this.Controls[0].Location = new Point(20, 20);

            // Tab Control - responsive size with larger minimum
            tabControl.Location = new Point(20, 70);
            tabControl.Size = new Size(formWidth, Math.Max(formHeight - 90, 600)); // Increased from 500 to 600

            // Layout filter controls in Tab 1 - Smart responsive layout
            if (pnlFilter != null)
            {
                // Make filter panel responsive
                pnlFilter.Size = new Size(formWidth - 40, 80);
                
                // Adaptive layout based on available width
                if (formWidth < 600) // Very small - stack vertically
                {
                    LayoutFilterControlsVertical();
                    pnlFilter.Height = 120; // More height needed for vertical layout
                }
                else if (formWidth < 800) // Medium - compact horizontal
                {
                    LayoutFilterControlsCompact();
                }
                else // Large - full horizontal
                {
                    LayoutFilterControlsFull();
                }
            }

            // Layout DataGridViews to be responsive
            LayoutDataGridViews();
            
            // Layout Check In/Out panel if exists
            LayoutCheckInOutPanel();
        }

        private void LayoutFilterControlsVertical()
        {
            // Stack controls vertically for very small screens
            pnlFilter.Controls[0].Location = new Point(10, 10); // lblTuNgay
            dtpTuNgay.Location = new Point(80, 8);
            dtpTuNgay.Size = new Size(100, 25);

            pnlFilter.Controls[2].Location = new Point(190, 10); // lblDenNgay
            dtpDenNgay.Location = new Point(260, 8);
            dtpDenNgay.Size = new Size(100, 25);

            pnlFilter.Controls[4].Location = new Point(10, 40); // lblNhanVien
            cmbNhanVien.Location = new Point(80, 38);
            cmbNhanVien.Size = new Size(150, 25);

            btnTimKiem.Location = new Point(240, 38);
            btnTimKiem.Size = new Size(80, 25);
            btnLamMoi.Location = new Point(330, 38);
            btnLamMoi.Size = new Size(80, 25);
        }

        private void LayoutFilterControlsCompact()
        {
            // Compact horizontal layout for medium screens
            pnlFilter.Controls[0].Location = new Point(10, 15); // lblTuNgay
            dtpTuNgay.Location = new Point(10, 35);
            dtpTuNgay.Size = new Size(90, 25);

            pnlFilter.Controls[2].Location = new Point(110, 15); // lblDenNgay
            dtpDenNgay.Location = new Point(110, 35);
            dtpDenNgay.Size = new Size(90, 25);

            pnlFilter.Controls[4].Location = new Point(210, 15); // lblNhanVien
            cmbNhanVien.Location = new Point(210, 35);
            cmbNhanVien.Size = new Size(120, 25);

            btnTimKiem.Location = new Point(340, 33);
            btnTimKiem.Size = new Size(70, 25);
            btnLamMoi.Location = new Point(420, 33);
            btnLamMoi.Size = new Size(70, 25);
        }

        private void LayoutFilterControlsFull()
        {
            // Full horizontal layout for large screens
            pnlFilter.Controls[0].Location = new Point(10, 15); // lblTuNgay
            dtpTuNgay.Location = new Point(10, 35);
            dtpTuNgay.Size = new Size(120, 25);

            pnlFilter.Controls[2].Location = new Point(150, 15); // lblDenNgay
            dtpDenNgay.Location = new Point(150, 35);
            dtpDenNgay.Size = new Size(120, 25);

            pnlFilter.Controls[4].Location = new Point(290, 15); // lblNhanVien
            cmbNhanVien.Location = new Point(290, 35);
            cmbNhanVien.Size = new Size(200, 25);

            btnTimKiem.Location = new Point(510, 33);
            btnLamMoi.Location = new Point(600, 33);
        }

        private void LayoutDataGridViews()
        {
            if (tabControl?.TabPages == null) return;

            int tabWidth = tabControl.Width - 40;
            int tabHeight = tabControl.Height - (pnlFilter?.Height > 80 ? 140 : 120); // Adjust for filter height
            
            // Ensure minimum height for proper display
            tabHeight = Math.Max(tabHeight, 450); // Minimum 450px height

            // Tab 1: Attendance Records - Adaptive layout
            if (dgvChamCong != null && pnlThongTin != null)
            {
                if (tabWidth < 700) // Small screen - stack vertically
                {
                    // DataGridView on top - larger minimum size
                    dgvChamCong.Location = new Point(20, pnlFilter.Bottom + 20);
                    dgvChamCong.Size = new Size(tabWidth, Math.Max((int)(tabHeight * 0.6), 300)); // Min 300px
                    
                    // Information Panel below - ensure minimum size
                    pnlThongTin.Location = new Point(20, dgvChamCong.Bottom + 10);
                    pnlThongTin.Size = new Size(tabWidth, Math.Max((int)(tabHeight * 0.35), 150)); // Min 150px
                }
                else // Large screen - side by side
                {
                    // DataGridView on left (60% width) - ensure minimum size
                    dgvChamCong.Location = new Point(20, pnlFilter.Bottom + 20);
                    dgvChamCong.Size = new Size(Math.Max((int)(tabWidth * 0.6), 400), Math.Max(tabHeight, 400)); // Min 400x400
                    
                    // Information Panel on right (35% width) - ensure minimum size
                    pnlThongTin.Location = new Point(dgvChamCong.Right + 20, dgvChamCong.Top);
                    pnlThongTin.Size = new Size(Math.Max((int)(tabWidth * 0.35), 250), Math.Max(tabHeight, 400)); // Min 250x400
                }
            }

            // Tab 2: Schedule & Attendance View - Full width with minimum size
            if (dgvLichChamCong != null)
            {
                dgvLichChamCong.Location = new Point(20, 80);
                dgvLichChamCong.Size = new Size(Math.Max(tabWidth, 600), Math.Max(tabHeight + 40, 450)); // Min 600x450
            }
        }

        private void LayoutCheckInOutPanel()
        {
            if (pnlCheckInOut == null) return;

            int formWidth = this.ClientSize.Width - 40;
            int formHeight = this.ClientSize.Height - 40;

            // Center the panel horizontally
            int panelWidth = Math.Min(600, formWidth - 40);
            int panelHeight = 400;
            
            pnlCheckInOut.Size = new Size(panelWidth, panelHeight);
            pnlCheckInOut.Location = new Point((formWidth - panelWidth) / 2 + 20, 70);
        }

        private void SetupEventHandlers()
        {
            dgvChamCong.SelectionChanged += dgvChamCong_SelectionChanged;
            btnTimKiem.Click += btnTimKiem_Click;
            btnLamMoi.Click += btnLamMoi_Click;
            btnCapNhat.Click += btnCapNhat_Click;
            btnKhoaCong.Click += btnKhoaCong_Click;
            btnCheckIn.Click += btnCheckIn_Click;
            btnCheckOut.Click += btnCheckOut_Click;
            btnRefreshStatus.Click += btnRefreshStatus_Click;
            
            // Load status when tab is selected
            tabControl.SelectedIndexChanged += tabControl_SelectedIndexChanged;
            
            // Setup auto-refresh timer for check in/out status
            _refreshTimer = new System.Windows.Forms.Timer();
            _refreshTimer.Interval = 30000; // 30 seconds
            _refreshTimer.Tick += RefreshTimer_Tick;
        }

        private void InitializeForm()
        {
            LoadComboBoxData();
            LoadData();
            SetFormPermissions();
        }

        private void LoadComboBoxData()
        {
            try
            {
                // Load employees for filter
                var nhanViens = _nhanVienRepository.GetAll();

                var allOption = new NhanVien { MaNV = -1, HoTen = "-- Tất cả --" };
                var employeeList = new[] { allOption }.Concat(nhanViens).ToList();

                cmbNhanVien.DataSource = employeeList;
                cmbNhanVien.DisplayMember = "HoTen";
                cmbNhanVien.ValueMember = "MaNV";
                cmbNhanVien.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                ShowMessage($"Lỗi tải dữ liệu ComboBox: {ex.Message}", "Lỗi", MessageBoxIcon.Error);
            }
        }

        private void LoadData()
        {
            try
            {
                // Load attendance records
                var chamCongs = (int)cmbNhanVien.SelectedValue == -1 ?
                    _chamCongRepository.GetByPeriod(dtpTuNgay.Value, dtpDenNgay.Value) :
                    _chamCongRepository.GetByEmployee((int)cmbNhanVien.SelectedValue, dtpTuNgay.Value, dtpDenNgay.Value);

                dgvChamCong.DataSource = chamCongs;
                ConfigureAttendanceGrid();

                // Load schedule & attendance view
                var lichChamCongs = _chamCongRepository.GetLichChamCongNgay(dtpTuNgay.Value, dtpDenNgay.Value);
                dgvLichChamCong.DataSource = lichChamCongs;
                ConfigureScheduleAttendanceGrid();
            }
            catch (Exception ex)
            {
                ShowMessage($"Lỗi tải dữ liệu: {ex.Message}", "Lỗi", MessageBoxIcon.Error);
            }
        }

        private void ConfigureAttendanceGrid()
        {
            if (dgvChamCong.Columns.Count > 0)
            {
                dgvChamCong.Columns["MaChamCong"].HeaderText = "Mã";
                dgvChamCong.Columns["TenNhanVien"].HeaderText = "Nhân viên";
                dgvChamCong.Columns["NgayLam"].HeaderText = "Ngày làm";
                dgvChamCong.Columns["GioVao"].HeaderText = "Giờ vào";
                dgvChamCong.Columns["GioRa"].HeaderText = "Giờ ra";
                dgvChamCong.Columns["GioCong"].HeaderText = "Giờ công";
                dgvChamCong.Columns["DiTrePhut"].HeaderText = "Đi trễ (phút)";
                dgvChamCong.Columns["VeSomPhut"].HeaderText = "Về sớm (phút)";
                dgvChamCong.Columns["Khoa"].HeaderText = "Khóa";

                if (dgvChamCong.Columns["MaNV"] != null)
                    dgvChamCong.Columns["MaNV"].Visible = false;
                if (dgvChamCong.Columns["TenCa"] != null)
                    dgvChamCong.Columns["TenCa"].Visible = false;
            }
        }

        private void ConfigureScheduleAttendanceGrid()
        {
            if (dgvLichChamCong.Columns.Count > 0)
            {
                dgvLichChamCong.Columns["HoTen"].HeaderText = "Nhân viên";
                dgvLichChamCong.Columns["NgayLam"].HeaderText = "Ngày";
                dgvLichChamCong.Columns["TenCa"].HeaderText = "Ca làm";
                dgvLichChamCong.Columns["GioBatDau"].HeaderText = "Giờ bắt đầu";
                dgvLichChamCong.Columns["GioKetThuc"].HeaderText = "Giờ kết thúc";
                dgvLichChamCong.Columns["GioVao"].HeaderText = "Giờ vào";
                dgvLichChamCong.Columns["GioRa"].HeaderText = "Giờ ra";
                dgvLichChamCong.Columns["GioCong"].HeaderText = "Giờ công";
                dgvLichChamCong.Columns["TrangThaiLich"].HeaderText = "Trạng thái";

                if (dgvLichChamCong.Columns["MaNV"] != null)
                    dgvLichChamCong.Columns["MaNV"].Visible = false;
            }
        }

        private void SetFormPermissions()
        {
            bool canManageAttendance = VuToanThang_23110329.Data.CurrentUser.HasPermission("MANAGE_ATTENDANCE");
            bool isEmployee = VuToanThang_23110329.Data.CurrentUser.IsNhanVien;
            
            btnCapNhat.Enabled = canManageAttendance;
            if (btnKhoaCong != null)
                btnKhoaCong.Enabled = VuToanThang_23110329.Data.CurrentUser.IsHR;
            
            dtpGioVao.Enabled = canManageAttendance;
            dtpGioRa.Enabled = canManageAttendance;
            txtGhiChu.Enabled = canManageAttendance;

            // Hide/show tabs based on role
            if (tabControl?.TabPages != null && tabControl.TabPages.Count > 0)
            {
                if (isEmployee)
                {
                    // Nhân viên chỉ thấy tab Check In/Out
                    var checkInOutTab = tabControl.TabPages[2]; // Save Check In/Out tab
                    tabControl.TabPages.Clear();
                    tabControl.TabPages.Add(checkInOutTab);
                }
                else if (!VuToanThang_23110329.Data.CurrentUser.IsHR)
                {
                    // Quản lý/Kế toán không thấy tab khóa công
                    if (tabControl.TabPages.Count > 3)
                        tabControl.TabPages.RemoveAt(3); // Remove lock tab
                }
            }
        }

        private void LoadAttendanceToForm(ChamCong cc)
        {
            if (cc == null) return;

            _currentChamCong = cc;
            
            if (cc.GioVao.HasValue)
                dtpGioVao.Value = cc.GioVao.Value;
            if (cc.GioRa.HasValue)
                dtpGioRa.Value = cc.GioRa.Value;

            // Update summary labels
            lblTongGioCong.Text = $"Tổng giờ công: {cc.GioCong?.ToString("F2") ?? "0"} giờ";
            lblDiTre.Text = $"Đi trễ: {cc.DiTrePhut ?? 0} phút";
            lblVeSom.Text = $"Về sớm: {cc.VeSomPhut ?? 0} phút";

            btnCapNhat.Enabled = !cc.Khoa && VuToanThang_23110329.Data.CurrentUser.HasPermission("MANAGE_ATTENDANCE");
        }

        private void ShowMessage(string message, string title, MessageBoxIcon icon)
        {
            MessageBox.Show(message, title, MessageBoxButtons.OK, icon);
        }

        // Event Handlers
        private void dgvChamCong_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvChamCong.SelectedRows.Count > 0)
            {
                var selectedCC = (ChamCong)dgvChamCong.SelectedRows[0].DataBoundItem;
                LoadAttendanceToForm(selectedCC);
            }
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void btnCapNhat_Click(object sender, EventArgs e)
        {
            if (_currentChamCong == null)
            {
                ShowMessage("Vui lòng chọn bản ghi chấm công để cập nhật!", "Cảnh báo", MessageBoxIcon.Warning);
                return;
            }

            if (_currentChamCong.Khoa)
            {
                ShowMessage("Không thể cập nhật chấm công đã khóa!", "Cảnh báo", MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var result = _chamCongRepository.UpdateChamCong(
                    _currentChamCong.MaChamCong,
                    dtpGioVao.Value,
                    dtpGioRa.Value,
                    txtGhiChu.Text.Trim()
                );

                if (result.Success)
                {
                    ShowMessage(result.Message, "Thành công", MessageBoxIcon.Information);
                    LoadData();
                }
                else
                {
                    ShowMessage(result.Message, "Lỗi", MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                ShowMessage($"Lỗi cập nhật chấm công: {ex.Message}", "Lỗi", MessageBoxIcon.Error);
            }
        }

        private void btnKhoaCong_Click(object sender, EventArgs e)
        {
            if (cmbThangKhoa.SelectedItem == null || cmbNamKhoa.SelectedItem == null)
            {
                ShowMessage("Vui lòng chọn tháng và năm để khóa!", "Cảnh báo", MessageBoxIcon.Warning);
                return;
            }

            int thang = (int)cmbThangKhoa.SelectedItem;
            int nam = (int)cmbNamKhoa.SelectedItem;

            var result = MessageBox.Show(
                $"Bạn có chắc chắn muốn khóa công tháng {thang}/{nam}?\n\nSau khi khóa, không thể chỉnh sửa dữ liệu chấm công trong kỳ này!",
                "Xác nhận khóa công",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            if (result == DialogResult.Yes)
            {
                try
                {
                    var lockResult = _chamCongRepository.KhoaCongThang(nam, thang);

                    if (lockResult.Success)
                    {
                        ShowMessage(lockResult.Message, "Thành công", MessageBoxIcon.Information);
                        LoadData();
                    }
                    else
                    {
                        ShowMessage(lockResult.Message, "Lỗi", MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    ShowMessage($"Lỗi khóa công: {ex.Message}", "Lỗi", MessageBoxIcon.Error);
                }
            }
        }

        // Event Handlers for Check In/Out
        private void tabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Load status when Check In/Out tab is selected
            if (tabControl.SelectedTab?.Text == "Check In/Out")
            {
                LoadCurrentStatus();
                _refreshTimer.Start(); // Start auto-refresh
            }
            else
            {
                _refreshTimer.Stop(); // Stop auto-refresh when not on check in/out tab
            }
        }

        private void RefreshTimer_Tick(object sender, EventArgs e)
        {
            // Only refresh if we're on the check in/out tab
            if (tabControl.SelectedTab?.Text == "Check In/Out")
            {
                LoadCurrentStatus();
            }
        }

        private void btnCheckIn_Click(object sender, EventArgs e)
        {
            if (VuToanThang_23110329.Data.CurrentUser.CurrentEmployeeId == null)
            {
                ShowMessage("Không tìm thấy thông tin nhân viên!", "Lỗi", MessageBoxIcon.Error);
                return;
            }

            try
            {
                var result = _chamCongRepository.CheckIn(VuToanThang_23110329.Data.CurrentUser.CurrentEmployeeId.Value);

                if (result.Success)
                {
                    ShowMessage(result.Message, "Thành công", MessageBoxIcon.Information);
                    LoadCurrentStatus(); // Refresh status
                }
                else
                {
                    ShowMessage(result.Message, "Lỗi", MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                ShowMessage($"Lỗi check in: {ex.Message}", "Lỗi", MessageBoxIcon.Error);
            }
        }

        private void btnCheckOut_Click(object sender, EventArgs e)
        {
            if (VuToanThang_23110329.Data.CurrentUser.CurrentEmployeeId == null)
            {
                ShowMessage("Không tìm thấy thông tin nhân viên!", "Lỗi", MessageBoxIcon.Error);
                return;
            }

            try
            {
                var result = _chamCongRepository.CheckOut(VuToanThang_23110329.Data.CurrentUser.CurrentEmployeeId.Value);

                if (result.Success)
                {
                    ShowMessage(result.Message, "Thành công", MessageBoxIcon.Information);
                    LoadCurrentStatus(); // Refresh status
                }
                else
                {
                    ShowMessage(result.Message, "Lỗi", MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                ShowMessage($"Lỗi check out: {ex.Message}", "Lỗi", MessageBoxIcon.Error);
            }
        }

        private void btnRefreshStatus_Click(object sender, EventArgs e)
        {
            LoadCurrentStatus();
        }

        private void LoadCurrentStatus()
        {
            if (VuToanThang_23110329.Data.CurrentUser.CurrentEmployeeId == null)
            {
                lblTrangThaiHienTai.Text = "Không tìm thấy thông tin nhân viên";
                lblTrangThaiHienTai.ForeColor = Color.Red;
                return;
            }

            try
            {
                _currentStatus = _chamCongRepository.GetTrangThaiChamCong(VuToanThang_23110329.Data.CurrentUser.CurrentEmployeeId.Value);

                if (_currentStatus != null)
                {
                    UpdateStatusDisplay();
                    UpdateButtonStates();
                }
                else
                {
                    lblTrangThaiHienTai.Text = "Không thể tải trạng thái";
                    lblTrangThaiHienTai.ForeColor = Color.Red;
                }
            }
            catch (Exception ex)
            {
                lblTrangThaiHienTai.Text = $"Lỗi: {ex.Message}";
                lblTrangThaiHienTai.ForeColor = Color.Red;
            }
        }

        private void UpdateStatusDisplay()
        {
            if (_currentStatus == null) return;

            // Update status text and color
            switch (_currentStatus.TrangThaiHanhDong)
            {
                case "KhongCoLich":
                    lblTrangThaiHienTai.Text = "🏠 Hôm nay bạn được nghỉ";
                    lblTrangThaiHienTai.ForeColor = Color.LightGreen;
                    break;
                case "ChuaDenGioCheckIn":
                    var gioCheckIn = _currentStatus.GioSomNhatCheckIn?.ToString("HH:mm") ?? "N/A";
                    var gioHienTai = DateTime.Now.ToString("HH:mm");
                    lblTrangThaiHienTai.Text = $"⏳ Chưa đến giờ (hiện tại: {gioHienTai}, check in từ: {gioCheckIn})";
                    lblTrangThaiHienTai.ForeColor = Color.Orange;
                    break;
                case "CoTheCheckIn":
                    lblTrangThaiHienTai.Text = "✅ Sẵn sàng check in";
                    lblTrangThaiHienTai.ForeColor = Color.LightGreen;
                    break;
                case "CoTheCheckOut":
                    var gioVao = _currentStatus.GioVao?.ToString("HH:mm") ?? "N/A";
                    lblTrangThaiHienTai.Text = $"🟡 Đã check in lúc {gioVao} - Sẵn sàng check out";
                    lblTrangThaiHienTai.ForeColor = Color.Yellow;
                    break;
                case "DaHoanThanh":
                    var gioVaoHT = _currentStatus.GioVao?.ToString("HH:mm") ?? "N/A";
                    var gioRaHT = _currentStatus.GioRa?.ToString("HH:mm") ?? "N/A";
                    lblTrangThaiHienTai.Text = $"🎉 Hoàn thành ({gioVaoHT} - {gioRaHT})";
                    lblTrangThaiHienTai.ForeColor = Color.LightBlue;
                    break;
                case "LichDaKhoa":
                case "CongDaKhoa":
                    lblTrangThaiHienTai.Text = "Lịch/Công đã bị khóa";
                    lblTrangThaiHienTai.ForeColor = Color.Red;
                    break;
                default:
                    lblTrangThaiHienTai.Text = _currentStatus.TrangThaiHanhDong;
                    lblTrangThaiHienTai.ForeColor = Color.White;
                    break;
            }

            // Update ca info based on status
            if (_currentStatus.KhongCoLich)
            {
                lblThongTinCa.Text = "📅 Hôm nay không có ca làm việc";
                lblThongTinCa.ForeColor = Color.Orange;
                lblThongTinChamCong.Text = "Bạn không có lịch làm việc trong ngày hôm nay";
                lblThongTinChamCong.ForeColor = Color.LightGray;
            }
            else if (!string.IsNullOrEmpty(_currentStatus.TenCa))
            {
                var ngayLam = _currentStatus.NgayLam.ToString("dd/MM/yyyy");
                var thoiGianCa = _currentStatus.ThoiGianCa;
                
                lblThongTinCa.Text = $"📋 Ca hôm nay: {_currentStatus.TenCa}";
                lblThongTinCa.ForeColor = Color.LightBlue;
                
                // Hiển thị thông tin chấm công thực tế
                var thongTinChiTiet = $"🕐 Ca: {thoiGianCa} | 📅 {ngayLam}";
                
                if (_currentStatus.GioVao.HasValue && _currentStatus.GioRa.HasValue)
                {
                    // Đã hoàn thành chấm công
                    var gioVao = _currentStatus.GioVao.Value.ToString("HH:mm");
                    var gioRa = _currentStatus.GioRa.Value.ToString("HH:mm");
                    var gioCong = _currentStatus.GioCong?.ToString("0.0") ?? "0";
                    thongTinChiTiet = $"✅ Vào: {gioVao} | Ra: {gioRa} | Công: {gioCong}h";
                    
                    if (_currentStatus.DiTrePhut > 0 || _currentStatus.VeSomPhut > 0)
                    {
                        var diTre = _currentStatus.DiTrePhut > 0 ? $" | 🔴 Trễ: {_currentStatus.DiTrePhut}p" : "";
                        var veSom = _currentStatus.VeSomPhut > 0 ? $" | 🟠 Sớm: {_currentStatus.VeSomPhut}p" : "";
                        thongTinChiTiet += diTre + veSom;
                    }
                }
                else if (_currentStatus.GioVao.HasValue)
                {
                    // Đã check in, chưa check out
                    var gioVao = _currentStatus.GioVao.Value.ToString("HH:mm");
                    thongTinChiTiet = $"🟡 Đã vào lúc: {gioVao} | Chưa check out";
                }
                else
                {
                    // Chưa check in
                    if (_currentStatus.GioSomNhatCheckIn.HasValue)
                    {
                        var gioCheckIn = _currentStatus.GioSomNhatCheckIn.Value.ToString("HH:mm");
                        thongTinChiTiet += $" | ⏰ Check in từ: {gioCheckIn}";
                    }
                }
                
                lblThongTinChamCong.Text = thongTinChiTiet;
                lblThongTinChamCong.ForeColor = Color.LightGray;
            }
            else
            {
                lblThongTinCa.Text = "⚠️ Không có thông tin ca làm việc";
                lblThongTinCa.ForeColor = Color.Orange;
                lblThongTinChamCong.Text = "Vui lòng liên hệ quản lý để được hỗ trợ";
                lblThongTinChamCong.ForeColor = Color.LightGray;
            }
        }

        private void UpdateButtonStates()
        {
            if (_currentStatus == null)
            {
                btnCheckIn.Enabled = false;
                btnCheckOut.Enabled = false;
                return;
            }

            btnCheckIn.Enabled = _currentStatus.CoTheCheckIn;
            btnCheckOut.Enabled = _currentStatus.CoTheCheckOut;

            // Update button appearance and text
            if (_currentStatus.ChuaDenGioCheckIn)
            {
                btnCheckIn.BackColor = Color.Gray;
                btnCheckIn.Text = "CHƯA ĐẾN GIỜ";
            }
            else
            {
                btnCheckIn.BackColor = btnCheckIn.Enabled ? Color.FromArgb(46, 125, 50) : Color.Gray;
                btnCheckIn.Text = "CHECK IN";
            }
            
            btnCheckOut.BackColor = btnCheckOut.Enabled ? Color.FromArgb(244, 67, 54) : Color.Gray;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _refreshTimer?.Stop();
                _refreshTimer?.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
