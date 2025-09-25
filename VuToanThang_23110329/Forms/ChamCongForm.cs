using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using VuToanThang_23110329.Models;
using VuToanThang_23110329.Repositories;
using VuToanThang_23110329.Data;

namespace VuToanThang_23110329.Forms
{
    public partial class ChamCongForm : Form
    {
        private readonly ChamCongRepository _chamCongRepository;
        private readonly NhanVienRepository _nhanVienRepository;
        private ChamCong _currentChamCong;
        private bool _isEditing = false;
        private TrangThaiChamCong _currentStatus;
        private System.Windows.Forms.Timer _refreshTimer;

        public ChamCongForm()
        {
            InitializeComponent();
            _chamCongRepository = new ChamCongRepository();
            _nhanVienRepository = new NhanVienRepository();
            InitializeForm();
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
            if (btnMoKhoaCong != null)
                btnMoKhoaCong.Enabled = VuToanThang_23110329.Data.CurrentUser.IsHR;
            
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
            try
            {
                var selectedTabText = tabControl.SelectedTab?.Text ?? "null";
                System.Diagnostics.Debug.WriteLine($"Tab changed to: {selectedTabText}");
                
                // Load status when Check In/Out tab is selected
                if (tabControl.SelectedTab?.Text == "Check In/Out")
                {
                    System.Diagnostics.Debug.WriteLine("Loading status for Check In/Out tab");
                    LoadCurrentStatus();
                    _refreshTimer?.Start(); // Start auto-refresh
                }
                else
                {
                    _refreshTimer?.Stop(); // Stop auto-refresh when not on check in/out tab
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"tabControl_SelectedIndexChanged Error: {ex}");
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
            // Test database connection first
            TestDatabaseConnection();
            LoadCurrentStatus();
        }

        private void TestDatabaseConnection()
        {
            try
            {
                lblTrangThaiHienTai.Text = "🔄 Đang kiểm tra kết nối database...";
                lblTrangThaiHienTai.ForeColor = Color.Yellow;
                Application.DoEvents();

                // Test simple query
                var testResult = SqlHelper.ExecuteScalar("SELECT COUNT(*) FROM NhanVien");
                System.Diagnostics.Debug.WriteLine($"Database test result: {testResult}");
                
                lblTrangThaiHienTai.Text = $"✅ Database OK - Có {testResult} nhân viên";
                lblTrangThaiHienTai.ForeColor = Color.LightGreen;
                Application.DoEvents();
                
                System.Threading.Thread.Sleep(1000); // Show result for 1 second
            }
            catch (Exception ex)
            {
                lblTrangThaiHienTai.Text = $"❌ Lỗi database: {ex.Message}";
                lblTrangThaiHienTai.ForeColor = Color.Red;
                System.Diagnostics.Debug.WriteLine($"Database connection error: {ex}");
                throw; // Re-throw to stop further processing
            }
        }

        private void LoadCurrentStatus()
        {
            try
            {
                // Debug info
                lblTrangThaiHienTai.Text = "🔄 Đang kiểm tra thông tin nhân viên...";
                lblTrangThaiHienTai.ForeColor = Color.Yellow;
                Application.DoEvents(); // Force UI update

                if (VuToanThang_23110329.Data.CurrentUser.CurrentEmployeeId == null)
                {
                    lblTrangThaiHienTai.Text = "❌ Không tìm thấy thông tin nhân viên";
                    lblTrangThaiHienTai.ForeColor = Color.Red;
                    lblThongTinCa.Text = "Vui lòng đăng nhập lại";
                    lblThongTinChamCong.Text = "Không có thông tin nhân viên trong session";
                    return;
                }

                var maNV = VuToanThang_23110329.Data.CurrentUser.CurrentEmployeeId.Value;
                lblTrangThaiHienTai.Text = $"🔄 Đang tải trạng thái cho nhân viên {maNV}...";
                lblTrangThaiHienTai.ForeColor = Color.Yellow;
                Application.DoEvents(); // Force UI update

                _currentStatus = _chamCongRepository.GetTrangThaiChamCong(maNV);

                if (_currentStatus != null)
                {
                    lblTrangThaiHienTai.Text = "✅ Đã tải xong, đang cập nhật giao diện...";
                    lblTrangThaiHienTai.ForeColor = Color.LightGreen;
                    Application.DoEvents(); // Force UI update

                    UpdateStatusDisplay();
                    UpdateButtonStates();
                }
                else
                {
                    lblTrangThaiHienTai.Text = "⚠️ Không có dữ liệu trạng thái";
                    lblTrangThaiHienTai.ForeColor = Color.Orange;
                    lblThongTinCa.Text = "Không tìm thấy thông tin ca làm việc";
                    lblThongTinChamCong.Text = "Vui lòng liên hệ quản lý để được hỗ trợ";
                }
            }
            catch (Exception ex)
            {
                lblTrangThaiHienTai.Text = $"❌ Lỗi: {ex.Message}";
                lblTrangThaiHienTai.ForeColor = Color.Red;
                lblThongTinCa.Text = "Có lỗi xảy ra khi tải dữ liệu";
                lblThongTinChamCong.Text = $"Chi tiết: {ex.GetType().Name}";
                
                // Log chi tiết để debug
                System.Diagnostics.Debug.WriteLine($"LoadCurrentStatus Error: {ex}");
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

        private void btnMoKhoaCong_Click(object sender, EventArgs e)
        {
            if (cmbThangKhoa.SelectedItem == null || cmbNamKhoa.SelectedItem == null)
            {
                MessageBox.Show("Vui lòng chọn tháng và năm cần mở khóa!", "Thông báo", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int thang = (int)cmbThangKhoa.SelectedItem;
            int nam = (int)cmbNamKhoa.SelectedItem;

            var confirmResult = MessageBox.Show(
                $"Bạn có chắc chắn muốn MỞ KHÓA công tháng {thang}/{nam}?\n\n" +
                "Sau khi mở khóa, dữ liệu chấm công có thể được chỉnh sửa lại.",
                "Xác nhận mở khóa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirmResult == DialogResult.Yes)
            {
                try
                {
                    var unlockResult = _chamCongRepository.MoKhoaCongThang(nam, thang);

                    if (unlockResult.Success)
                    {
                        ShowMessage(unlockResult.Message, "Thành công", MessageBoxIcon.Information);
                        
                        // Refresh data
                        LoadData();
                        LoadLichChamCong();
                    }
                    else
                    {
                        ShowMessage(unlockResult.Message, "Lỗi", MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    ShowMessage($"Lỗi mở khóa công: {ex.Message}", "Lỗi", MessageBoxIcon.Error);
                }
            }
        }

    }
}
