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
    public partial class LichPhanCaForm : Form
    {
        private readonly LichPhanCaRepository _lichPhanCaRepository;
        private readonly NhanVienRepository _nhanVienRepository;
        private readonly CaLamRepository _caLamRepository;
        private LichPhanCa _currentLichPhanCa;
        private bool _isEditing = false;


        public LichPhanCaForm()
        {
            InitializeComponent();
            _lichPhanCaRepository = new LichPhanCaRepository();
            _nhanVienRepository = new NhanVienRepository();
            _caLamRepository = new CaLamRepository();
            InitializeForm();
        }



        private void InitializeForm()
        {
            LoadComboBoxData();
            LoadData();
            SetFormMode(false);
        }

        private void LoadComboBoxData()
        {
            try
            {
                // Load employees
                var nhanViens = VuToanThang_23110329.Data.CurrentUser.IsHR ? 
                    _nhanVienRepository.GetAll() : 
                    _nhanVienRepository.GetByRLS();

                cmbNhanVien.DataSource = nhanViens;
                cmbNhanVien.DisplayMember = "HoTen";
                cmbNhanVien.ValueMember = "MaNV";

                // Load shifts
                var caLams = _caLamRepository.GetAll();
                cmbCaLam.DataSource = caLams;
                cmbCaLam.DisplayMember = "TenCa";
                cmbCaLam.ValueMember = "MaCa";
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
                var lichPhanCas = VuToanThang_23110329.Data.CurrentUser.IsNhanVien && VuToanThang_23110329.Data.CurrentUser.CurrentEmployeeId.HasValue ?
                    _lichPhanCaRepository.GetByEmployee(VuToanThang_23110329.Data.CurrentUser.CurrentEmployeeId.Value, dtpTuNgay.Value, dtpDenNgay.Value) :
                    _lichPhanCaRepository.GetByPeriod(dtpTuNgay.Value, dtpDenNgay.Value);

                dgvLichPhanCa.DataSource = lichPhanCas;
                ConfigureDataGridView();
            }
            catch (Exception ex)
            {
                ShowMessage($"Lỗi tải dữ liệu: {ex.Message}", "Lỗi", MessageBoxIcon.Error);
            }
        }

        private void ConfigureDataGridView()
        {
            if (dgvLichPhanCa.Columns.Count > 0)
            {
                dgvLichPhanCa.Columns["MaLich"].HeaderText = "Mã lịch";
                dgvLichPhanCa.Columns["TenNhanVien"].HeaderText = "Nhân viên";
                dgvLichPhanCa.Columns["NgayLam"].HeaderText = "Ngày làm";
                dgvLichPhanCa.Columns["TenCa"].HeaderText = "Ca làm";
                dgvLichPhanCa.Columns["GioBatDau"].HeaderText = "Giờ bắt đầu";
                dgvLichPhanCa.Columns["GioKetThuc"].HeaderText = "Giờ kết thúc";
                dgvLichPhanCa.Columns["TrangThai"].HeaderText = "Trạng thái";
                
                // Hide some columns
                if (dgvLichPhanCa.Columns["MaNV"] != null)
                    dgvLichPhanCa.Columns["MaNV"].Visible = false;
                if (dgvLichPhanCa.Columns["MaCa"] != null)
                    dgvLichPhanCa.Columns["MaCa"].Visible = false;
                if (dgvLichPhanCa.Columns["HeSoCa"] != null)
                    dgvLichPhanCa.Columns["HeSoCa"].Visible = false;
            }
        }

        private void SetFormMode(bool isEditing)
        {
            _isEditing = isEditing;
            
            btnThem.Enabled = !isEditing && VuToanThang_23110329.Data.CurrentUser.HasPermission("MANAGE_SCHEDULE");
            btnSua.Enabled = !isEditing && dgvLichPhanCa.SelectedRows.Count > 0 && VuToanThang_23110329.Data.CurrentUser.HasPermission("MANAGE_SCHEDULE");
            btnXoa.Enabled = !isEditing && dgvLichPhanCa.SelectedRows.Count > 0 && VuToanThang_23110329.Data.CurrentUser.HasPermission("MANAGE_SCHEDULE");
            btnLuu.Enabled = false; // Always disabled since we use dialog now
            btnHuy.Enabled = false; // Always disabled since we use dialog now
            btnTaoLichTuan.Enabled = !isEditing && VuToanThang_23110329.Data.CurrentUser.HasPermission("MANAGE_SCHEDULE");
            
            // Keep original panel color since we don't edit inline anymore
            pnlThongTin.BackColor = Color.FromArgb(60, 60, 60);
            
            // All input controls are always disabled since we use dialog
            foreach (Control control in pnlThongTin.Controls)
            {
                if (control is TextBox || control is ComboBox || control is DateTimePicker)
                {
                    control.Enabled = false;
                    control.BackColor = Color.FromArgb(70, 70, 70);
                }
            }
        }

        private void ClearForm()
        {
            cmbNhanVien.SelectedIndex = -1;
            cmbCaLam.SelectedIndex = -1;
            dtpNgayLam.Value = DateTime.Now;
            cmbTrangThai.SelectedIndex = 0;
            
            _currentLichPhanCa = null;
        }

        private void LoadScheduleToForm(LichPhanCa lich)
        {
            if (lich == null) return;

            _currentLichPhanCa = lich;
            cmbNhanVien.SelectedValue = lich.MaNV;
            cmbCaLam.SelectedValue = lich.MaCa;
            dtpNgayLam.Value = lich.NgayLam;
            cmbTrangThai.Text = lich.TrangThai;
        }

        private void ShowMessage(string message, string title, MessageBoxIcon icon)
        {
            MessageBox.Show(message, title, MessageBoxButtons.OK, icon);
        }

        // Event Handlers
        private void dgvLichPhanCa_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvLichPhanCa.SelectedRows.Count > 0 && !_isEditing)
            {
                var selectedLich = (LichPhanCa)dgvLichPhanCa.SelectedRows[0].DataBoundItem;
                LoadScheduleToForm(selectedLich);
                SetFormMode(false);
            }
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void btnTaoLichTuan_Click(object sender, EventArgs e)
        {
            var form = new TaoLichTuanForm(dtpTuNgay.Value);
            if (form.ShowDialog() == DialogResult.OK)
            {
                LoadData();
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            var detailForm = new LichPhanCaDetailForm();
            if (detailForm.ShowDialog() == DialogResult.OK)
            {
                LoadData();
                ShowMessage("Thêm lịch phân ca thành công!", "Thành công", MessageBoxIcon.Information);
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (dgvLichPhanCa.SelectedRows.Count > 0)
            {
                var selectedLich = (LichPhanCa)dgvLichPhanCa.SelectedRows[0].DataBoundItem;
                if (selectedLich.TrangThai == "Khoa")
                {
                    ShowMessage("Không thể sửa lịch đã khóa!", "Cảnh báo", MessageBoxIcon.Warning);
                    return;
                }
                
                var detailForm = new LichPhanCaDetailForm(selectedLich);
                if (detailForm.ShowDialog() == DialogResult.OK)
                {
                    LoadData();
                    ShowMessage("Cập nhật lịch phân ca thành công!", "Thành công", MessageBoxIcon.Information);
                }
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dgvLichPhanCa.SelectedRows.Count > 0)
            {
                var selectedLich = (LichPhanCa)dgvLichPhanCa.SelectedRows[0].DataBoundItem;
                if (selectedLich.TrangThai == "Khoa")
                {
                    ShowMessage("Không thể xóa lịch đã khóa!", "Cảnh báo", MessageBoxIcon.Warning);
                    return;
                }

                var result = MessageBox.Show("Bạn có chắc chắn muốn xóa lịch phân ca này?", "Xác nhận", 
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                
                if (result == DialogResult.Yes)
                {
                    try
                    {
                        var deleteResult = _lichPhanCaRepository.Delete(selectedLich.MaLich);
                        
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
                        ShowMessage($"Lỗi xóa lịch phân ca: {ex.Message}", "Lỗi", MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            // This button is now disabled and logic is handled in LichPhanCaDetailForm
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            // This button is now disabled and logic is handled in LichPhanCaDetailForm
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            LoadData();
            ClearForm();
            SetFormMode(false);
        }
    }
}
