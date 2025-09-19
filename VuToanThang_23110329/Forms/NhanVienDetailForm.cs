using System;
using System.Drawing;
using System.Windows.Forms;
using VuToanThang_23110329.Data;
using VuToanThang_23110329.Models;
using VuToanThang_23110329.Repositories;
using VuToanThang_23110329.Controls;

namespace VuToanThang_23110329.Forms
{
    public partial class NhanVienDetailForm : Form
    {
        private readonly NhanVienRepository _nhanVienRepository;
        private NhanVien _currentNhanVien;
        private bool _isEditMode;

        // UI Controls
        private TextBox txtHoTen, txtDienThoai, txtEmail, txtDiaChi, txtPhongBan, txtChucDanh, txtLuongCoBan;
        private DateTimePicker dtpNgaySinh, dtpNgayVaoLam;
        private ComboBox cmbGioiTinh, cmbTrangThai, cmbVaiTro;
        private CheckBox chkTaoTaiKhoan;
        private TextBox txtTenDangNhap, txtMatKhau;
        private ModernButton btnLuu, btnHuy;

        public OperationResult Result { get; private set; }

        public NhanVienDetailForm(NhanVien nhanVien = null)
        {
            _nhanVienRepository = new NhanVienRepository();
            _currentNhanVien = nhanVien;
            _isEditMode = nhanVien != null;
            
            InitializeComponent();
            InitializeForm();
        }

        private void InitializeForm()
        {
            SetupEventHandlers();
            LoadDataToForm();
            SetFormMode();
        }

        private void SetupEventHandlers()
        {
            chkTaoTaiKhoan.CheckedChanged += chkTaoTaiKhoan_CheckedChanged;
            btnLuu.Click += btnLuu_Click;
            btnHuy.Click += btnHuy_Click;
        }

        private void LoadDataToForm()
        {
            if (_isEditMode && _currentNhanVien != null)
            {
                txtHoTen.Text = _currentNhanVien.HoTen;
                if (_currentNhanVien.NgaySinh.HasValue) 
                    dtpNgaySinh.Value = _currentNhanVien.NgaySinh.Value;
                cmbGioiTinh.Text = _currentNhanVien.GioiTinh;
                txtDienThoai.Text = _currentNhanVien.DienThoai;
                txtEmail.Text = _currentNhanVien.Email;
                txtDiaChi.Text = _currentNhanVien.DiaChi;
                dtpNgayVaoLam.Value = _currentNhanVien.NgayVaoLam;
                cmbTrangThai.Text = _currentNhanVien.TrangThai;
                txtPhongBan.Text = _currentNhanVien.PhongBan;
                txtChucDanh.Text = _currentNhanVien.ChucDanh;
                txtLuongCoBan.Text = _currentNhanVien.LuongCoBan.ToString();
            }
            else
            {
                // Default values for new employee
                dtpNgaySinh.Value = DateTime.Now.AddYears(-25);
                dtpNgayVaoLam.Value = DateTime.Now;
                cmbTrangThai.SelectedIndex = 0; // DangLam
                cmbVaiTro.SelectedIndex = 0; // NhanVien
            }
        }

        private void SetFormMode()
        {
            // For edit mode, disable account creation section
            if (_isEditMode)
            {
                chkTaoTaiKhoan.Visible = false;
                txtTenDangNhap.Visible = false;
                txtMatKhau.Visible = false;
                cmbVaiTro.Visible = false;
                
                // Hide related labels too
                foreach (Control ctrl in this.Controls)
                {
                    if (ctrl is Label lbl)
                    {
                        if (lbl.Text.Contains("Tạo tài khoản") || 
                            lbl.Text.Contains("Tên đăng nhập") || 
                            lbl.Text.Contains("Mật khẩu") || 
                            lbl.Text.Contains("Vai trò"))
                        {
                            lbl.Visible = false;
                        }
                    }
                }
            }
            else
            {
                // For add mode, initially disable account fields
                txtTenDangNhap.Enabled = false;
                txtMatKhau.Enabled = false;
                cmbVaiTro.Enabled = false;
            }
        }

        private void chkTaoTaiKhoan_CheckedChanged(object sender, EventArgs e)
        {
            bool enabled = chkTaoTaiKhoan.Checked;
            txtTenDangNhap.Enabled = enabled;
            txtMatKhau.Enabled = enabled;
            cmbVaiTro.Enabled = enabled;
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ValidateInput())
                    return;

                if (_isEditMode)
                {
                    // Update existing employee
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
                    _currentNhanVien.LuongCoBan = decimal.Parse(txtLuongCoBan.Text);

                    Result = _nhanVienRepository.Update(_currentNhanVien);
                }
                else
                {
                    // Add new employee
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
                        LuongCoBan = decimal.Parse(txtLuongCoBan.Text),
                        TaoTaiKhoan = chkTaoTaiKhoan.Checked,
                        TenDangNhap = txtTenDangNhap.Text.Trim(),
                        MatKhauHash = txtMatKhau.Text,
                        VaiTro = cmbVaiTro.Text
                    };

                    Result = _nhanVienRepository.Insert(param);
                }

                if (Result.Success)
                {
                    MessageBox.Show(Result.Message, "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show(Result.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(txtHoTen.Text))
            {
                MessageBox.Show("Vui lòng nhập họ tên!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtHoTen.Focus();
                return false;
            }

            if (!decimal.TryParse(txtLuongCoBan.Text, out decimal luong) || luong <= 0)
            {
                MessageBox.Show("Lương cơ bản không hợp lệ!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtLuongCoBan.Focus();
                return false;
            }

            if (!_isEditMode && chkTaoTaiKhoan.Checked)
            {
                if (string.IsNullOrWhiteSpace(txtTenDangNhap.Text))
                {
                    MessageBox.Show("Vui lòng nhập tên đăng nhập!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtTenDangNhap.Focus();
                    return false;
                }

                if (string.IsNullOrWhiteSpace(txtMatKhau.Text))
                {
                    MessageBox.Show("Vui lòng nhập mật khẩu!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtMatKhau.Focus();
                    return false;
                }
            }

            return true;
        }
    }
}
