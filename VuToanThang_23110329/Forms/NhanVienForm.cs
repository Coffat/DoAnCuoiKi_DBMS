using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using VuToanThang_23110329.Data;
using VuToanThang_23110329.Models;
using VuToanThang_23110329.Repositories;
using VuToanThang_23110329.Controls;

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
        private TextBox txtSearch;
        private ComboBox cmbFilterTrangThai;
        private ModernButton btnThem, btnSua, btnXoa, btnLamMoi, btnKhoiPhuc;

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
            cmbFilterTrangThai.SelectedIndexChanged += cmbFilterTrangThai_SelectedIndexChanged;
            
            btnThem.Click += btnThem_Click;
            btnSua.Click += btnSua_Click;
            btnXoa.Click += btnXoa_Click;
            btnKhoiPhuc.Click += btnKhoiPhuc_Click;
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
            
            btnThem.Enabled = hasManagePermission;
            btnSua.Enabled = hasSelectedEmployee && hasManagePermission;
            btnXoa.Enabled = hasSelectedEmployee && hasManagePermission;
            
            // Restore button: only for HR and when a resigned employee is selected
            if (hasSelectedEmployee)
            {
                var selectedNV = (NhanVien)dgvNhanVien.SelectedRows[0].DataBoundItem;
                btnKhoiPhuc.Enabled = hasManagePermission && selectedNV.TrangThai == "Nghi";
            }
            else
            {
                btnKhoiPhuc.Enabled = false;
            }
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
            SetFormMode(false);
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            var detailForm = new NhanVienDetailForm();
            if (detailForm.ShowDialog() == DialogResult.OK)
            {
                LoadData(); // Refresh the list
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (dgvNhanVien.SelectedRows.Count > 0)
            {
                var selectedNV = (NhanVien)dgvNhanVien.SelectedRows[0].DataBoundItem;
                var detailForm = new NhanVienDetailForm(selectedNV);
                if (detailForm.ShowDialog() == DialogResult.OK)
                {
                    LoadData(); // Refresh the list
                }
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

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            LoadData();
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
