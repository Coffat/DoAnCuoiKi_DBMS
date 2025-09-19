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
        private Panel pnlFilter, pnlThongTin;
        private TabControl tabControl;
        private Label lblTongGioCong, lblDiTre, lblVeSom;

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

            // Tab 3: Lock Period
            var tabKhoaCong = new TabPage("Khóa công kỳ")
            {
                BackColor = Color.FromArgb(50, 50, 50),
                ForeColor = Color.White
            };

            CreateTab1Controls(tabChamCong);
            CreateTab2Controls(tabLichChamCong);
            CreateTab3Controls(tabKhoaCong);

            tabControl.TabPages.AddRange(new TabPage[] { tabChamCong, tabLichChamCong, tabKhoaCong });

            this.Controls.AddRange(new Control[] { lblTitle, tabControl });
        }

        private void CreateTab1Controls(TabPage tab)
        {
            // Filter Panel
            pnlFilter = new Panel
            {
                BackColor = Color.FromArgb(60, 60, 60),
                BorderStyle = BorderStyle.FixedSingle,
                Size = new Size(1320, 80),
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
            dgvChamCong.Size = new Size(900, 400);

            // Information Panel
            pnlThongTin = new Panel
            {
                BackColor = Color.FromArgb(60, 60, 60),
                BorderStyle = BorderStyle.FixedSingle,
                Padding = new Padding(15),
                Location = new Point(940, 120),
                Size = new Size(400, 400)
            };

            CreateInfoControls();

            tab.Controls.AddRange(new Control[] { pnlFilter, dgvChamCong, pnlThongTin });
        }

        private void CreateTab2Controls(TabPage tab)
        {
            // Schedule & Attendance Combined View
            dgvLichChamCong = CreateDataGridView();
            dgvLichChamCong.Location = new Point(20, 80);
            dgvLichChamCong.Size = new Size(1320, 500);

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
            // Title
            this.Controls[0].Location = new Point(20, 20);

            // Tab Control
            tabControl.Location = new Point(20, 70);
            tabControl.Size = new Size(1360, 800);

            // Layout filter controls in Tab 1
            if (pnlFilter != null)
            {
                pnlFilter.Controls[0].Location = new Point(10, 15); // lblTuNgay
                dtpTuNgay.Location = new Point(10, 35);

                pnlFilter.Controls[2].Location = new Point(150, 15); // lblDenNgay
                dtpDenNgay.Location = new Point(150, 35);

                pnlFilter.Controls[4].Location = new Point(290, 15); // lblNhanVien
                cmbNhanVien.Location = new Point(290, 35);
                cmbNhanVien.Size = new Size(200, 25);

                btnTimKiem.Location = new Point(510, 33);
                btnLamMoi.Location = new Point(600, 33);
            }
        }

        private void SetupEventHandlers()
        {
            dgvChamCong.SelectionChanged += dgvChamCong_SelectionChanged;
            btnTimKiem.Click += btnTimKiem_Click;
            btnLamMoi.Click += btnLamMoi_Click;
            btnCapNhat.Click += btnCapNhat_Click;
            btnKhoaCong.Click += btnKhoaCong_Click;
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
                var nhanViens = VuToanThang_23110329.Data.CurrentUser.IsHR || VuToanThang_23110329.Data.CurrentUser.IsQuanLy ? 
                    _nhanVienRepository.GetAll() : 
                    _nhanVienRepository.GetByRLS();

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
            
            btnCapNhat.Enabled = canManageAttendance;
            btnKhoaCong.Enabled = VuToanThang_23110329.Data.CurrentUser.IsHR;
            
            dtpGioVao.Enabled = canManageAttendance;
            dtpGioRa.Enabled = canManageAttendance;
            txtGhiChu.Enabled = canManageAttendance;

            if (!canManageAttendance)
            {
                tabControl.TabPages.RemoveAt(2); // Remove lock tab for non-HR users
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
    }
}
