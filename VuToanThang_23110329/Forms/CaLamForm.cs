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
    public partial class CaLamForm : Form
    {
        private readonly CaLamRepository _caLamRepository;
        private CaLam _currentCaLam;
        private bool _isEditing = false;

        // UI Controls
        private DataGridView dgvCaLam;
        private TextBox txtSearch, txtTenCa, txtHeSoCa;
        private DateTimePicker dtpGioBatDau, dtpGioKetThuc;
        private Button btnThem, btnSua, btnXoa, btnLuu, btnHuy, btnLamMoi;
        private Panel pnlThongTin;

        public CaLamForm()
        {
            InitializeComponent();
            _caLamRepository = new CaLamRepository();
            InitializeForm();
        }


        private void SetupEventHandlers()
        {
            // Only set up DataGridView and TextBox event handlers here
            // Button event handlers are now set up in Designer.cs
            if (txtSearch != null)
                txtSearch.TextChanged += txtSearch_TextChanged;
            
            if (dgvCaLam != null)
                dgvCaLam.SelectionChanged += dgvCaLam_SelectionChanged;
        }

        private void InitializeForm()
        {
            LoadData();
            SetupEventHandlers();
            SetFormMode(false);
        }

        private void LoadData()
        {
            try
            {
                var caLams = _caLamRepository.GetAll();
                dgvCaLam.DataSource = caLams;
                ConfigureDataGridView();
            }
            catch (Exception ex)
            {
                ShowMessage($"Lỗi tải dữ liệu: {ex.Message}", "Lỗi", MessageBoxIcon.Error);
            }
        }

        private void ConfigureDataGridView()
        {
            if (dgvCaLam.Columns.Count > 0)
            {
                dgvCaLam.Columns["MaCa"].HeaderText = "Mã ca";
                dgvCaLam.Columns["TenCa"].HeaderText = "Tên ca";
                dgvCaLam.Columns["GioBatDau"].HeaderText = "Giờ bắt đầu";
                dgvCaLam.Columns["GioKetThuc"].HeaderText = "Giờ kết thúc";
                dgvCaLam.Columns["HeSoCa"].HeaderText = "Hệ số";
            }
        }

        private void SetFormMode(bool isEditing)
        {
            _isEditing = isEditing;
            
            btnThem.Enabled = !isEditing && VuToanThang_23110329.Data.CurrentUser.HasPermission("MANAGE_SHIFTS");
            btnSua.Enabled = !isEditing && dgvCaLam.SelectedRows.Count > 0 && VuToanThang_23110329.Data.CurrentUser.HasPermission("MANAGE_SHIFTS");
            btnXoa.Enabled = !isEditing && dgvCaLam.SelectedRows.Count > 0 && VuToanThang_23110329.Data.CurrentUser.HasPermission("MANAGE_SHIFTS");
            btnLuu.Enabled = isEditing;
            btnHuy.Enabled = isEditing;
            
            // Enable/disable input controls
            foreach (Control control in pnlThongTin.Controls)
            {
                if (control is TextBox || control is DateTimePicker || control is CheckBox)
                {
                    control.Enabled = isEditing;
                }
            }
        }

        private void ClearForm()
        {
            txtTenCa.Clear();
            dtpGioBatDau.Value = DateTime.Today.AddHours(8); // 8:00 AM
            dtpGioKetThuc.Value = DateTime.Today.AddHours(17); // 5:00 PM
            txtHeSoCa.Text = "1.0";
            
            _currentCaLam = null;
        }

        private void LoadShiftToForm(CaLam ca)
        {
            if (ca == null) return;

            _currentCaLam = ca;
            txtTenCa.Text = ca.TenCa;
            dtpGioBatDau.Value = DateTime.Today.Add(ca.GioBatDau);
            dtpGioKetThuc.Value = DateTime.Today.Add(ca.GioKetThuc);
            txtHeSoCa.Text = ca.HeSoCa.ToString();
        }

        private void ShowMessage(string message, string title, MessageBoxIcon icon)
        {
            MessageBox.Show(message, title, MessageBoxButtons.OK, icon);
        }

        // Event Handlers
        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            try
            {
                var allShifts = _caLamRepository.GetAll();
                var searchTerm = txtSearch.Text.ToLower().Trim();
                
                if (string.IsNullOrEmpty(searchTerm))
                {
                    dgvCaLam.DataSource = allShifts;
                }
                else
                {
                    var filteredShifts = allShifts.Where(ca => 
                        ca.TenCa.ToLower().Contains(searchTerm) ||
                        ca.GioBatDau.ToString(@"hh\:mm").Contains(searchTerm) ||
                        ca.GioKetThuc.ToString(@"hh\:mm").Contains(searchTerm) ||
                        ca.HeSoCa.ToString().Contains(searchTerm)
                    ).ToList();
                    
                    dgvCaLam.DataSource = filteredShifts;
                }
                
                ConfigureDataGridView();
            }
            catch (Exception ex)
            {
                ShowMessage($"Lỗi tìm kiếm: {ex.Message}", "Lỗi", MessageBoxIcon.Error);
            }
        }

        private void dgvCaLam_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvCaLam.SelectedRows.Count > 0 && !_isEditing)
            {
                var selectedCa = (CaLam)dgvCaLam.SelectedRows[0].DataBoundItem;
                LoadShiftToForm(selectedCa);
                SetFormMode(false);
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            ClearForm();
            SetFormMode(true);
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (dgvCaLam.SelectedRows.Count > 0)
            {
                SetFormMode(true);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dgvCaLam.SelectedRows.Count > 0)
            {
                var result = MessageBox.Show("Bạn có chắc chắn muốn xóa ca làm này?", "Xác nhận", 
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                
                if (result == DialogResult.Yes)
                {
                    try
                    {
                        var selectedCa = (CaLam)dgvCaLam.SelectedRows[0].DataBoundItem;
                        var deleteResult = _caLamRepository.Delete(selectedCa.MaCa);
                        
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
                        ShowMessage($"Lỗi xóa ca làm: {ex.Message}", "Lỗi", MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtTenCa.Text))
                {
                    ShowMessage("Vui lòng nhập tên ca!", "Cảnh báo", MessageBoxIcon.Warning);
                    return;
                }

                if (!decimal.TryParse(txtHeSoCa.Text, out decimal heSoCa) || heSoCa <= 0)
                {
                    ShowMessage("Hệ số ca không hợp lệ!", "Cảnh báo", MessageBoxIcon.Warning);
                    return;
                }

                var gioBatDau = dtpGioBatDau.Value.TimeOfDay;
                var gioKetThuc = dtpGioKetThuc.Value.TimeOfDay;

                if (gioBatDau >= gioKetThuc)
                {
                    ShowMessage("Giờ bắt đầu phải nhỏ hơn giờ kết thúc!", "Cảnh báo", MessageBoxIcon.Warning);
                    return;
                }

                if (_currentCaLam == null) // Add new
                {
                    var caLam = new CaLam
                    {
                        TenCa = txtTenCa.Text.Trim(),
                        GioBatDau = gioBatDau,
                        GioKetThuc = gioKetThuc,
                        HeSoCa = heSoCa
                    };

                    var result = _caLamRepository.Insert(caLam);
                    
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
                    _currentCaLam.TenCa = txtTenCa.Text.Trim();
                    _currentCaLam.GioBatDau = gioBatDau;
                    _currentCaLam.GioKetThuc = gioKetThuc;
                    _currentCaLam.HeSoCa = heSoCa;

                    var result = _caLamRepository.Update(_currentCaLam);
                    
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
            if (dgvCaLam.SelectedRows.Count > 0)
            {
                var selectedCa = (CaLam)dgvCaLam.SelectedRows[0].DataBoundItem;
                LoadShiftToForm(selectedCa);
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
    }
}
