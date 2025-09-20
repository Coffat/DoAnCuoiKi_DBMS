using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using VuToanThang_23110329.Data;
using VuToanThang_23110329.Models;
using VuToanThang_23110329.Repositories;

namespace VuToanThang_23110329.Forms
{
    public partial class LichPhanCaDetailForm : Form
    {
        private readonly LichPhanCaRepository _lichPhanCaRepository;
        private readonly NhanVienRepository _nhanVienRepository;
        private readonly CaLamRepository _caLamRepository;
        private LichPhanCa _currentLichPhanCa;
        private bool _isEditMode;

        public LichPhanCa LichPhanCa { get; private set; }

        public LichPhanCaDetailForm(LichPhanCa lichPhanCa = null)
        {
            InitializeComponent();
            _lichPhanCaRepository = new LichPhanCaRepository();
            _nhanVienRepository = new NhanVienRepository();
            _caLamRepository = new CaLamRepository();
            
            _currentLichPhanCa = lichPhanCa;
            _isEditMode = lichPhanCa != null;
            
            InitializeForm();
        }

        private void InitializeForm()
        {
            this.Text = _isEditMode ? "Sửa lịch phân ca" : "Thêm lịch phân ca";
            LoadComboBoxData();
            
            if (_isEditMode)
            {
                LoadDataToForm();
            }
            else
            {
                // Set default values for new entry
                dtpNgayLam.Value = DateTime.Now;
                cmbTrangThai.SelectedIndex = 0; // "Mo"
            }
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

        private void LoadDataToForm()
        {
            if (_currentLichPhanCa == null) return;

            cmbNhanVien.SelectedValue = _currentLichPhanCa.MaNV;
            cmbCaLam.SelectedValue = _currentLichPhanCa.MaCa;
            dtpNgayLam.Value = _currentLichPhanCa.NgayLam;
            cmbTrangThai.Text = _currentLichPhanCa.TrangThai;
            // GhiChu is removed, no need to load it.
        }

        private void ShowMessage(string message, string title, MessageBoxIcon icon)
        {
            MessageBox.Show(message, title, MessageBoxButtons.OK, icon);
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbNhanVien.SelectedValue == null)
                {
                    ShowMessage("Vui lòng chọn nhân viên!", "Cảnh báo", MessageBoxIcon.Warning);
                    cmbNhanVien.Focus();
                    return;
                }

                if (cmbCaLam.SelectedValue == null)
                {
                    ShowMessage("Vui lòng chọn ca làm!", "Cảnh báo", MessageBoxIcon.Warning);
                    cmbCaLam.Focus();
                    return;
                }

                if (_isEditMode)
                {
                    // Update existing
                    _currentLichPhanCa.MaNV = Convert.ToInt32(cmbNhanVien.SelectedValue);
                    _currentLichPhanCa.MaCa = Convert.ToInt32(cmbCaLam.SelectedValue);
                    _currentLichPhanCa.NgayLam = dtpNgayLam.Value.Date;
                    _currentLichPhanCa.TrangThai = cmbTrangThai.Text;

                    var result = _lichPhanCaRepository.Update(_currentLichPhanCa);
                    
                    if (result.Success)
                    {
                        LichPhanCa = _currentLichPhanCa;
                        ShowMessage(result.Message, "Thành công", MessageBoxIcon.Information);
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                    else
                    {
                        ShowMessage(result.Message, "Lỗi", MessageBoxIcon.Error);
                    }
                }
                else
                {
                    // Add new
                    var lichPhanCa = new LichPhanCa
                    {
                        MaNV = Convert.ToInt32(cmbNhanVien.SelectedValue),
                        MaCa = Convert.ToInt32(cmbCaLam.SelectedValue),
                        NgayLam = dtpNgayLam.Value.Date,
                        TrangThai = cmbTrangThai.Text
                    };

                    var result = _lichPhanCaRepository.Insert(lichPhanCa);
                    
                    if (result.Success)
                    {
                        LichPhanCa = lichPhanCa;
                        ShowMessage(result.Message, "Thành công", MessageBoxIcon.Information);
                        this.DialogResult = DialogResult.OK;
                        this.Close();
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
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void cmbCaLam_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Update shift info display when shift is selected
            if (cmbCaLam.SelectedItem is CaLam selectedShift)
            {
                lblShiftInfo.Text = $"Giờ làm: {selectedShift.ThoiGianDisplay} | Hệ số: {selectedShift.HeSoCa}";
                lblShiftInfo.Visible = true;
            }
            else
            {
                lblShiftInfo.Visible = false;
            }
        }
    }
}
