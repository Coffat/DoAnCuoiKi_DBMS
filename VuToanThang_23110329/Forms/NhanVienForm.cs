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
    public partial class NhanVienForm : Form
    {
        private readonly NhanVienRepository _nhanVienRepository;
        private readonly AuthRepository _authRepository;
        private NhanVien _currentNhanVien;
        private bool _isEditing = false;

        // UI Controls
        private DataGridView dgvNhanVien;
        private TextBox txtSearch, txtHoTen, txtDienThoai, txtEmail, txtDiaChi, txtPhongBan, txtChucDanh, txtLuongCoBan;
        private DateTimePicker dtpNgaySinh, dtpNgayVaoLam;
        private ComboBox cmbGioiTinh, cmbTrangThai, cmbVaiTro, cmbFilterTrangThai;
        private CheckBox chkTaoTaiKhoan;
        private TextBox txtTenDangNhap, txtMatKhau;
        private Button btnThem, btnSua, btnXoa, btnLuu, btnHuy, btnLamMoi, btnKhoiPhuc;
        private Panel pnlThongTin;

        public NhanVienForm()
        {
            InitializeComponent();
            _nhanVienRepository = new NhanVienRepository();
            _authRepository = new AuthRepository();
            InitializeForm();
        }


        private void SetupEventHandlers()
        {
            txtSearch.TextChanged += txtSearch_TextChanged;
            dgvNhanVien.SelectionChanged += dgvNhanVien_SelectionChanged;
            chkTaoTaiKhoan.CheckedChanged += chkTaoTaiKhoan_CheckedChanged;
            cmbFilterTrangThai.SelectedIndexChanged += cmbFilterTrangThai_SelectedIndexChanged;
            
            btnThem.Click += btnThem_Click;
            btnSua.Click += btnSua_Click;
            btnXoa.Click += btnXoa_Click;
            btnKhoiPhuc.Click += btnKhoiPhuc_Click;
            btnLuu.Click += btnLuu_Click;
            btnHuy.Click += btnHuy_Click;
            btnLamMoi.Click += btnLamMoi_Click;
        }

        private void InitializeForm()
        {
            SetupEventHandlers();
            LoadData();
            SetFormMode(false);
        }

        private void LoadData()
        {
            try
            {
                // Test database connection first
                if (!SqlHelper.TestConnection())
                {
                    ShowMessage("Không thể kết nối đến cơ sở dữ liệu!", "Cảnh báo", MessageBoxIcon.Warning);
                    return;
                }

                var nhanViens = VuToanThang_23110329.Data.CurrentUser.IsHR ? 
                    _nhanVienRepository.GetAll() : 
                    _nhanVienRepository.GetByRLS();

                dgvNhanVien.DataSource = nhanViens;
                ConfigureDataGridView();
            }
            catch (Exception ex)
            {
                ShowMessage($"Lỗi tải dữ liệu: {ex.Message}\n\nChi tiết: {ex.StackTrace}", "Lỗi", MessageBoxIcon.Error);
            }
        }

        private void ConfigureDataGridView()
        {
            if (dgvNhanVien.Columns.Count > 0)
            {
                dgvNhanVien.Columns["MaNV"].HeaderText = "Mã NV";
                dgvNhanVien.Columns["HoTen"].HeaderText = "Họ tên";
                dgvNhanVien.Columns["NgaySinh"].HeaderText = "Ngày sinh";
                dgvNhanVien.Columns["GioiTinh"].HeaderText = "Giới tính";
                dgvNhanVien.Columns["DienThoai"].HeaderText = "Điện thoại";
                dgvNhanVien.Columns["Email"].HeaderText = "Email";
                dgvNhanVien.Columns["PhongBan"].HeaderText = "Phòng ban";
                dgvNhanVien.Columns["ChucDanh"].HeaderText = "Chức danh";
                dgvNhanVien.Columns["TrangThai"].HeaderText = "Trạng thái";
                
                // Hide some columns
                if (dgvNhanVien.Columns["MaNguoiDung"] != null)
                    dgvNhanVien.Columns["MaNguoiDung"].Visible = false;
                if (dgvNhanVien.Columns["DiaChi"] != null)
                    dgvNhanVien.Columns["DiaChi"].Visible = false;
                if (dgvNhanVien.Columns["LuongCoBan"] != null)
                    dgvNhanVien.Columns["LuongCoBan"].Visible = false;
            }
        }

        private void SetFormMode(bool isEditing)
        {
            _isEditing = isEditing;
            
            bool hasManagePermission = VuToanThang_23110329.Data.CurrentUser.HasPermission("MANAGE_EMPLOYEES");
            bool hasSelectedEmployee = dgvNhanVien.SelectedRows.Count > 0;
            
            btnThem.Enabled = !isEditing && hasManagePermission;
            btnSua.Enabled = !isEditing && hasSelectedEmployee && hasManagePermission;
            btnXoa.Enabled = !isEditing && hasSelectedEmployee && hasManagePermission;
            btnLuu.Enabled = isEditing;
            btnHuy.Enabled = isEditing;
            
            // Restore button: only for HR and when a resigned employee is selected
            if (hasSelectedEmployee && !isEditing)
            {
                var selectedNV = (NhanVien)dgvNhanVien.SelectedRows[0].DataBoundItem;
                btnKhoiPhuc.Enabled = hasManagePermission && selectedNV.TrangThai == "Nghi";
            }
            else
            {
                btnKhoiPhuc.Enabled = false;
            }
            
            // Enable/disable input controls
            foreach (Control control in pnlThongTin.Controls)
            {
                if (control is TextBox || control is ComboBox || control is DateTimePicker || control is CheckBox)
                {
                    control.Enabled = isEditing;
                }
            }
        }

        private void ClearForm()
        {
            txtHoTen.Clear();
            dtpNgaySinh.Value = DateTime.Now.AddYears(-25);
            cmbGioiTinh.SelectedIndex = -1;
            txtDienThoai.Clear();
            txtEmail.Clear();
            txtDiaChi.Clear();
            dtpNgayVaoLam.Value = DateTime.Now;
            cmbTrangThai.SelectedIndex = 0; // DangLam
            txtPhongBan.Clear();
            txtChucDanh.Clear();
            txtLuongCoBan.Clear();
            chkTaoTaiKhoan.Checked = false;
            txtTenDangNhap.Clear();
            txtMatKhau.Clear();
            cmbVaiTro.SelectedIndex = 0; // NhanVien
            
            _currentNhanVien = null;
        }

        private void LoadEmployeeToForm(NhanVien nv)
        {
            if (nv == null) return;

            _currentNhanVien = nv;
            txtHoTen.Text = nv.HoTen;
            if (nv.NgaySinh.HasValue) dtpNgaySinh.Value = nv.NgaySinh.Value;
            cmbGioiTinh.Text = nv.GioiTinh;
            txtDienThoai.Text = nv.DienThoai;
            txtEmail.Text = nv.Email;
            txtDiaChi.Text = nv.DiaChi;
            dtpNgayVaoLam.Value = nv.NgayVaoLam;
            cmbTrangThai.Text = nv.TrangThai;
            txtPhongBan.Text = nv.PhongBan;
            txtChucDanh.Text = nv.ChucDanh;
            txtLuongCoBan.Text = nv.LuongCoBan.ToString();
        }

        private void ShowMessage(string message, string title, MessageBoxIcon icon)
        {
            MessageBox.Show(message, title, MessageBoxButtons.OK, icon);
        }

        // Event Handlers
        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            FilterData();
        }

        private void FilterData()
        {
            try
            {
                var allData = VuToanThang_23110329.Data.CurrentUser.IsHR ? 
                    _nhanVienRepository.GetAll() : 
                    _nhanVienRepository.GetByRLS();

                var filteredData = allData.AsEnumerable();

                // Filter by status
                if (cmbFilterTrangThai.SelectedIndex > 0) // 0 = "Tất cả"
                {
                    var statusFilter = cmbFilterTrangThai.Text;
                    filteredData = filteredData.Where(nv => nv.TrangThai == statusFilter);
                }

                // Filter by search term
                if (!string.IsNullOrWhiteSpace(txtSearch.Text))
                {
                    var searchTerm = txtSearch.Text.ToLower().Trim();
                    filteredData = filteredData.Where(nv => 
                        nv.HoTen.ToLower().Contains(searchTerm) ||
                        nv.DienThoai?.ToLower().Contains(searchTerm) == true ||
                        nv.Email?.ToLower().Contains(searchTerm) == true ||
                        nv.PhongBan?.ToLower().Contains(searchTerm) == true ||
                        nv.ChucDanh?.ToLower().Contains(searchTerm) == true ||
                        nv.MaNV.ToString().Contains(searchTerm)
                    );
                }
                
                dgvNhanVien.DataSource = filteredData.ToList();
                ConfigureDataGridView();
            }
            catch (Exception ex)
            {
                ShowMessage($"Lỗi tìm kiếm: {ex.Message}", "Lỗi", MessageBoxIcon.Error);
            }
        }

        private void dgvNhanVien_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvNhanVien.SelectedRows.Count > 0 && !_isEditing)
            {
                var selectedNV = (NhanVien)dgvNhanVien.SelectedRows[0].DataBoundItem;
                LoadEmployeeToForm(selectedNV);
                SetFormMode(false);
            }
        }

        private void chkTaoTaiKhoan_CheckedChanged(object sender, EventArgs e)
        {
            bool enabled = chkTaoTaiKhoan.Checked && _isEditing;
            txtTenDangNhap.Enabled = enabled;
            txtMatKhau.Enabled = enabled;
            cmbVaiTro.Enabled = enabled;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            ClearForm();
            SetFormMode(true);
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (dgvNhanVien.SelectedRows.Count > 0)
            {
                SetFormMode(true);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dgvNhanVien.SelectedRows.Count > 0)
            {
                var result = MessageBox.Show("Bạn có chắc chắn muốn xóa nhân viên này?", "Xác nhận", 
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                
                if (result == DialogResult.Yes)
                {
                    try
                    {
                        var selectedNV = (NhanVien)dgvNhanVien.SelectedRows[0].DataBoundItem;
                        var deleteResult = _nhanVienRepository.Delete(selectedNV.MaNV);
                        
                        if (deleteResult.Success)
                        {
                            ShowMessage(deleteResult.Message, "Thành công", MessageBoxIcon.Information);
                            LoadData();
                            ClearForm();
                        }
                        else
                        {
                            ShowMessage(deleteResult.Message, "Lỗi", MessageBoxIcon.Error);
                        }
                    }
                    catch (Exception ex)
                    {
                        ShowMessage($"Lỗi xóa nhân viên: {ex.Message}", "Lỗi", MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtHoTen.Text))
                {
                    ShowMessage("Vui lòng nhập họ tên!", "Cảnh báo", MessageBoxIcon.Warning);
                    return;
                }

                if (!decimal.TryParse(txtLuongCoBan.Text, out decimal luongCoBan))
                {
                    ShowMessage("Lương cơ bản không hợp lệ!", "Cảnh báo", MessageBoxIcon.Warning);
                    return;
                }

                if (_currentNhanVien == null) // Add new
                {
                    var param = new ThemMoiNhanVienParams
                    {
                        HoTen = txtHoTen.Text.Trim(),
                        NgaySinh = dtpNgaySinh.Value,
                        GioiTinh = cmbGioiTinh.Text,
                        DienThoai = txtDienThoai.Text.Trim(),
                        Email = txtEmail.Text.Trim(),
                        DiaChi = txtDiaChi.Text.Trim(),
                        NgayVaoLam = dtpNgayVaoLam.Value,
                        PhongBan = txtPhongBan.Text.Trim(),
                        ChucDanh = txtChucDanh.Text.Trim(),
                        LuongCoBan = luongCoBan,
                        TaoTaiKhoan = chkTaoTaiKhoan.Checked,
                        TenDangNhap = txtTenDangNhap.Text.Trim(),
                        MatKhauHash = txtMatKhau.Text,
                        VaiTro = cmbVaiTro.Text
                    };

                    var result = _nhanVienRepository.Insert(param);
                    
                    if (result.Success)
                    {
                        ShowMessage(result.Message, "Thành công", MessageBoxIcon.Information);
                        LoadData();
                        SetFormMode(false);
                        ClearForm();
                    }
                    else
                    {
                        ShowMessage(result.Message, "Lỗi", MessageBoxIcon.Error);
                    }
                }
                else // Update existing
                {
                    _currentNhanVien.HoTen = txtHoTen.Text.Trim();
                    _currentNhanVien.NgaySinh = dtpNgaySinh.Value;
                    _currentNhanVien.GioiTinh = cmbGioiTinh.Text;
                    _currentNhanVien.DienThoai = txtDienThoai.Text.Trim();
                    _currentNhanVien.Email = txtEmail.Text.Trim();
                    _currentNhanVien.DiaChi = txtDiaChi.Text.Trim();
                    _currentNhanVien.NgayVaoLam = dtpNgayVaoLam.Value;
                    _currentNhanVien.TrangThai = cmbTrangThai.Text;
                    _currentNhanVien.PhongBan = txtPhongBan.Text.Trim();
                    _currentNhanVien.ChucDanh = txtChucDanh.Text.Trim();
                    _currentNhanVien.LuongCoBan = luongCoBan;

                    var result = _nhanVienRepository.Update(_currentNhanVien);
                    
                    if (result.Success)
                    {
                        ShowMessage(result.Message, "Thành công", MessageBoxIcon.Information);
                        LoadData();
                        SetFormMode(false);
                    }
                    else
                    {
                        ShowMessage(result.Message, "Lỗi", MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                ShowMessage($"Lỗi lưu dữ liệu: {ex.Message}", "Lỗi", MessageBoxIcon.Error);
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            SetFormMode(false);
            if (dgvNhanVien.SelectedRows.Count > 0)
            {
                var selectedNV = (NhanVien)dgvNhanVien.SelectedRows[0].DataBoundItem;
                LoadEmployeeToForm(selectedNV);
            }
            else
            {
                ClearForm();
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            LoadData();
            ClearForm();
            SetFormMode(false);
        }

        private void cmbFilterTrangThai_SelectedIndexChanged(object sender, EventArgs e)
        {
            FilterData();
        }

        private void btnKhoiPhuc_Click(object sender, EventArgs e)
        {
            if (dgvNhanVien.SelectedRows.Count > 0)
            {
                var selectedNV = (NhanVien)dgvNhanVien.SelectedRows[0].DataBoundItem;
                
                if (selectedNV.TrangThai != "Nghi")
                {
                    ShowMessage("Chỉ có thể khôi phục nhân viên đã nghỉ việc!", "Cảnh báo", MessageBoxIcon.Warning);
                    return;
                }

                var result = MessageBox.Show($"Bạn có chắc chắn muốn khôi phục nhân viên {selectedNV.HoTen}?", "Xác nhận", 
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                
                if (result == DialogResult.Yes)
                {
                    try
                    {
                        var restoreResult = _nhanVienRepository.Restore(selectedNV.MaNV);
                        
                        if (restoreResult.Success)
                        {
                            ShowMessage(restoreResult.Message, "Thành công", MessageBoxIcon.Information);
                            LoadData();
                            ClearForm();
                        }
                        else
                        {
                            ShowMessage(restoreResult.Message, "Lỗi", MessageBoxIcon.Error);
                        }
                    }
                    catch (Exception ex)
                    {
                        ShowMessage($"Lỗi khôi phục nhân viên: {ex.Message}", "Lỗi", MessageBoxIcon.Error);
                    }
                }
            }
        }
    }
}
