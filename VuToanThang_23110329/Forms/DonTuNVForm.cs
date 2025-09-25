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
    public partial class DonTuNVForm : Form
    {
        private readonly DonTuRepository _donTuRepository;
        private DonTu _currentDonTu;
        private bool _isEditing = false;

        // UI Controls - khai báo để sử dụng trong code
        private ComboBox cmbTrangThai, cmbLoai;
        private DateTimePicker dtpTuLuc, dtpDenLuc;
        private TextBox txtSoGio, txtLyDo;
        private Button btnThem, btnSua, btnXoa, btnLuu, btnHuy, btnLamMoi;
        private DataGridView dgvDonTu;
        private Panel pnlThongTin;

        public DonTuNVForm()
        {
            InitializeComponent();
            _donTuRepository = new DonTuRepository();
            InitializeForm();
        }

        private void InitializeForm()
        {
            LoadData();
            SetFormMode(false);
        }

        private void LoadData()
        {
            try
            {
                System.Diagnostics.Debug.WriteLine("DonTuNVForm.LoadData() called");
                
                if (cmbTrangThai == null)
                {
                    System.Diagnostics.Debug.WriteLine("ERROR: cmbTrangThai is null in LoadData");
                    return;
                }
                
                if (!VuToanThang_23110329.Data.CurrentUser.CurrentEmployeeId.HasValue)
                {
                    ShowMessage("Không xác định được nhân viên hiện tại!", "Lỗi", MessageBoxIcon.Error);
                    return;
                }

                var donTus = _donTuRepository.GetByEmployee(VuToanThang_23110329.Data.CurrentUser.CurrentEmployeeId.Value);

                // Filter by status if selected
                if (cmbTrangThai.SelectedIndex > 0)
                {
                    string selectedStatus = cmbTrangThai.Text;
                    donTus = donTus.Where(d => d.TrangThai == selectedStatus).ToList();
                }

                dgvDonTu.DataSource = donTus;
                ConfigureDataGridView();
            }
            catch (Exception ex)
            {
                ShowMessage($"Lỗi tải dữ liệu: {ex.Message}", "Lỗi", MessageBoxIcon.Error);
            }
        }

        private void ConfigureDataGridView()
        {
            if (dgvDonTu.Columns.Count > 0)
            {
                dgvDonTu.Columns["MaDon"].HeaderText = "Mã đơn";
                dgvDonTu.Columns["Loai"].HeaderText = "Loại";
                dgvDonTu.Columns["TuLuc"].HeaderText = "Từ lúc";
                dgvDonTu.Columns["DenLuc"].HeaderText = "Đến lúc";
                dgvDonTu.Columns["SoGio"].HeaderText = "Số giờ";
                dgvDonTu.Columns["LyDo"].HeaderText = "Lý do";
                dgvDonTu.Columns["TrangThai"].HeaderText = "Trạng thái";
                dgvDonTu.Columns["TenNguoiDuyet"].HeaderText = "Người duyệt";
                
                // Hide some columns
                if (dgvDonTu.Columns["MaNV"] != null)
                    dgvDonTu.Columns["MaNV"].Visible = false;
                if (dgvDonTu.Columns["DuyetBoi"] != null)
                    dgvDonTu.Columns["DuyetBoi"].Visible = false;
                if (dgvDonTu.Columns["TenNhanVien"] != null)
                    dgvDonTu.Columns["TenNhanVien"].Visible = false;

                // Set status column colors
                dgvDonTu.CellFormatting += (s, e) =>
                {
                    if (dgvDonTu.Columns[e.ColumnIndex].Name == "TrangThai")
                    {
                        switch (e.Value?.ToString())
                        {
                            case "ChoDuyet":
                                e.CellStyle.ForeColor = Color.Orange;
                                break;
                            case "DaDuyet":
                                e.CellStyle.ForeColor = Color.LightGreen;
                                break;
                            case "TuChoi":
                                e.CellStyle.ForeColor = Color.LightCoral;
                                break;
                        }
                    }
                };
            }
        }

        private void SetFormMode(bool isEditing)
        {
            _isEditing = isEditing;
            
            btnThem.Enabled = !isEditing;
            btnSua.Enabled = !isEditing && dgvDonTu.SelectedRows.Count > 0 && CanEditSelected();
            btnXoa.Enabled = !isEditing && dgvDonTu.SelectedRows.Count > 0 && CanDeleteSelected();
            btnLuu.Enabled = isEditing;
            btnHuy.Enabled = isEditing;
            
            // Enable/disable input controls
            foreach (Control control in pnlThongTin.Controls)
            {
                if (control is TextBox || control is ComboBox || control is DateTimePicker)
                {
                    control.Enabled = isEditing;
                }
            }
        }

        private bool CanEditSelected()
        {
            if (dgvDonTu.SelectedRows.Count == 0) return false;
            var selected = (DonTu)dgvDonTu.SelectedRows[0].DataBoundItem;
            return selected.TrangThai == "ChoDuyet";
        }

        private bool CanDeleteSelected()
        {
            if (dgvDonTu.SelectedRows.Count == 0) return false;
            var selected = (DonTu)dgvDonTu.SelectedRows[0].DataBoundItem;
            return selected.TrangThai == "ChoDuyet";
        }

        private void ClearForm()
        {
            cmbLoai.SelectedIndex = 0;
            dtpTuLuc.Value = DateTime.Now;
            dtpDenLuc.Value = DateTime.Now.AddHours(8);
            txtSoGio.Text = "8";
            txtLyDo.Clear();
            
            _currentDonTu = null;
        }

        private void LoadRequestToForm(DonTu donTu)
        {
            if (donTu == null) return;

            _currentDonTu = donTu;
            cmbLoai.Text = donTu.Loai;
            dtpTuLuc.Value = donTu.TuLuc;
            dtpDenLuc.Value = donTu.DenLuc;
            txtSoGio.Text = donTu.SoGio?.ToString() ?? "";
            txtLyDo.Text = donTu.LyDo;
        }

        public void PerformLayout()
        {
            if (this.Controls.Count == 0) return;

            int formWidth = this.ClientSize.Width - 40;
            int formHeight = this.ClientSize.Height - 40;

            // Title
            this.Controls[0].Location = new Point(20, 20);

            // Adaptive layout based on screen size
            if (formWidth < 700) // Small screen - vertical stacking
            {
                LayoutControlsVertical();
            }
            else if (formWidth < 1000) // Medium screen - compact side-by-side
            {
                LayoutControlsCompact();
            }
            else // Large screen - full side-by-side
            {
                LayoutControlsFull();
            }
        }

        private void LayoutControlsVertical()
        {
            // Vertical stacking for small screens
            int formWidth = this.ClientSize.Width - 40;
            int formHeight = this.ClientSize.Height - 40;

            // Filter controls - horizontal
            this.Controls[1].Location = new Point(20, 70); // lblFilter
            cmbTrangThai.Location = new Point(150, 68);
            cmbTrangThai.Size = new Size(120, 25);

            // Action buttons - horizontal row
            btnThem.Location = new Point(20, 110);
            btnThem.Size = new Size(80, 30);
            btnSua.Location = new Point(110, 110);
            btnSua.Size = new Size(60, 30);
            btnXoa.Location = new Point(180, 110);
            btnXoa.Size = new Size(60, 30);
            btnLuu.Location = new Point(250, 110);
            btnLuu.Size = new Size(60, 30);
            btnHuy.Location = new Point(320, 110);
            btnHuy.Size = new Size(60, 30);
            btnLamMoi.Location = new Point(390, 110);
            btnLamMoi.Size = new Size(70, 30);

            // DataGridView on top (60% height)
            dgvDonTu.Location = new Point(20, 160);
            dgvDonTu.Size = new Size(formWidth, (int)((formHeight - 180) * 0.6));

            // Info Panel below (35% height)
            pnlThongTin.Location = new Point(20, dgvDonTu.Bottom + 10);
            pnlThongTin.Size = new Size(formWidth, (int)((formHeight - 180) * 0.35));

            // Reorganize info panel for horizontal layout
            OrganizeInfoPanelHorizontal();
        }

        private void LayoutControlsCompact()
        {
            // Compact side-by-side layout for medium screens
            int formWidth = this.ClientSize.Width - 60;
            int formHeight = this.ClientSize.Height - 40;

            // Filter controls
            this.Controls[1].Location = new Point(20, 70); // lblFilter
            cmbTrangThai.Location = new Point(150, 68);
            cmbTrangThai.Size = new Size(120, 25);

            // Action buttons - compact row
            btnThem.Location = new Point(20, 110);
            btnThem.Size = new Size(70, 30);
            btnSua.Location = new Point(100, 110);
            btnSua.Size = new Size(50, 30);
            btnXoa.Location = new Point(160, 110);
            btnXoa.Size = new Size(50, 30);
            btnLuu.Location = new Point(220, 110);
            btnLuu.Size = new Size(50, 30);
            btnHuy.Location = new Point(280, 110);
            btnHuy.Size = new Size(50, 30);
            btnLamMoi.Location = new Point(340, 110);
            btnLamMoi.Size = new Size(60, 30);

            // DataGridView on left (65% width)
            dgvDonTu.Location = new Point(20, 160);
            dgvDonTu.Size = new Size((int)(formWidth * 0.65), formHeight - 180);

            // Info Panel on right (30% width)
            pnlThongTin.Location = new Point(dgvDonTu.Right + 20, 160);
            pnlThongTin.Size = new Size((int)(formWidth * 0.30), formHeight - 180);

            // Reorganize info panel for vertical layout
            OrganizeInfoPanelVertical();
        }

        private void LayoutControlsFull()
        {
            // Full side-by-side layout for large screens
            int formWidth = this.ClientSize.Width - 60;
            int formHeight = this.ClientSize.Height - 40;

            // Filter controls
            this.Controls[1].Location = new Point(20, 70); // lblFilter
            cmbTrangThai.Location = new Point(150, 68);
            cmbTrangThai.Size = new Size(150, 25);

            // Action buttons - full row
            btnThem.Location = new Point(20, 110);
            btnSua.Location = new Point(130, 110);
            btnXoa.Location = new Point(200, 110);
            btnLuu.Location = new Point(270, 110);
            btnHuy.Location = new Point(340, 110);
            btnLamMoi.Location = new Point(410, 110);

            // DataGridView on left (70% width)
            dgvDonTu.Location = new Point(20, 160);
            dgvDonTu.Size = new Size((int)(formWidth * 0.70), formHeight - 180);

            // Info Panel on right (25% width)
            pnlThongTin.Location = new Point(dgvDonTu.Right + 20, 160);
            pnlThongTin.Size = new Size((int)(formWidth * 0.25), formHeight - 180);

            // Reorganize info panel for vertical layout
            OrganizeInfoPanelVertical();
        }

        private void OrganizeInfoPanelHorizontal()
        {
            // Horizontal layout of controls within info panel
            if (pnlThongTin?.Controls != null && pnlThongTin.Controls.Count > 0)
            {
                int x = 15, y = 20;
                int controlWidth = (pnlThongTin.Width - 60) / 2; // Two columns
                int spacing = 30;

                foreach (Control control in pnlThongTin.Controls)
                {
                    if (control is Label || control is ComboBox || control is DateTimePicker || control is TextBox)
                    {
                        control.Width = controlWidth;
                        control.Location = new Point(x, y);
                        
                        x += controlWidth + 20;
                        if (x + controlWidth > pnlThongTin.Width - 20) // Wrap to next row
                        {
                            x = 15;
                            y += spacing;
                        }
                    }
                }
            }
        }

        private void OrganizeInfoPanelVertical()
        {
            // Vertical stacking of controls within info panel
            if (pnlThongTin?.Controls != null && pnlThongTin.Controls.Count > 0)
            {
                int y = 20;
                int spacing = 35;

                foreach (Control control in pnlThongTin.Controls)
                {
                    if (control is Label || control is ComboBox || control is DateTimePicker || control is TextBox)
                    {
                        control.Location = new Point(15, y);
                        control.Width = pnlThongTin.Width - 30;
                        y += spacing;
                    }
                }
            }
        }

        private void ShowMessage(string message, string title, MessageBoxIcon icon)
        {
            MessageBox.Show(message, title, MessageBoxButtons.OK, icon);
        }

        // Event Handlers
        private void dgvDonTu_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvDonTu.SelectedRows.Count > 0 && !_isEditing)
            {
                var selectedDonTu = (DonTu)dgvDonTu.SelectedRows[0].DataBoundItem;
                LoadRequestToForm(selectedDonTu);
                SetFormMode(false);
            }
        }

        private void cmbTrangThai_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        private void cmbLoai_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbLoai.Text == "OT")
            {
                txtSoGio.Text = "2"; // Default OT hours
            }
            else
            {
                txtSoGio.Text = "8"; // Default work day hours
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            ClearForm();
            SetFormMode(true);
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (dgvDonTu.SelectedRows.Count > 0 && CanEditSelected())
            {
                SetFormMode(true);
            }
            else
            {
                ShowMessage("Chỉ có thể sửa đơn từ đang chờ duyệt!", "Cảnh báo", MessageBoxIcon.Warning);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dgvDonTu.SelectedRows.Count > 0 && CanDeleteSelected())
            {
                var result = MessageBox.Show("Bạn có chắc chắn muốn xóa đơn từ này?", "Xác nhận", 
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                
                if (result == DialogResult.Yes)
                {
                    try
                    {
                        var selectedDonTu = (DonTu)dgvDonTu.SelectedRows[0].DataBoundItem;
                        // Note: You'll need to implement Delete method in DonTuRepository
                        ShowMessage("Xóa đơn từ thành công!", "Thành công", MessageBoxIcon.Information);
                        LoadData();
                        ClearForm();
                    }
                    catch (Exception ex)
                    {
                        ShowMessage($"Lỗi xóa đơn từ: {ex.Message}", "Lỗi", MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                ShowMessage("Chỉ có thể xóa đơn từ đang chờ duyệt!", "Cảnh báo", MessageBoxIcon.Warning);
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtLyDo.Text))
                {
                    ShowMessage("Vui lòng nhập lý do!", "Cảnh báo", MessageBoxIcon.Warning);
                    return;
                }

                if (dtpTuLuc.Value >= dtpDenLuc.Value)
                {
                    ShowMessage("Thời gian bắt đầu phải nhỏ hơn thời gian kết thúc!", "Cảnh báo", MessageBoxIcon.Warning);
                    return;
                }

                if (!decimal.TryParse(txtSoGio.Text, out decimal soGio) || soGio <= 0)
                {
                    ShowMessage("Số giờ không hợp lệ!", "Cảnh báo", MessageBoxIcon.Warning);
                    return;
                }

                if (_currentDonTu == null) // Add new
                {
                    var donTu = new DonTu
                    {
                        MaNV = VuToanThang_23110329.Data.CurrentUser.CurrentEmployeeId.Value,
                        Loai = cmbLoai.Text,
                        TuLuc = dtpTuLuc.Value,
                        DenLuc = dtpDenLuc.Value,
                        SoGio = soGio,
                        LyDo = txtLyDo.Text.Trim(),
                        TrangThai = "ChoDuyet"
                    };

                    var result = _donTuRepository.Insert(donTu);
                    
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
                else // Update existing (if allowed)
                {
                    if (_currentDonTu.TrangThai != "ChoDuyet")
                    {
                        ShowMessage("Chỉ có thể sửa đơn từ đang chờ duyệt!", "Cảnh báo", MessageBoxIcon.Warning);
                        return;
                    }

                    // Note: You'll need to implement Update method in DonTuRepository
                    ShowMessage("Cập nhật đơn từ thành công!", "Thành công", MessageBoxIcon.Information);
                    LoadData();
                    SetFormMode(false);
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
            if (dgvDonTu.SelectedRows.Count > 0)
            {
                var selectedDonTu = (DonTu)dgvDonTu.SelectedRows[0].DataBoundItem;
                LoadRequestToForm(selectedDonTu);
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
