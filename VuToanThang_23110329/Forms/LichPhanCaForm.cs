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
                var nhanViens = _nhanVienRepository.GetAll();

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
                List<LichPhanCa> lichPhanCas;

                // Nếu có text tìm kiếm (không phải placeholder)
                if (!string.IsNullOrWhiteSpace(txtTimKiem?.Text) && txtTimKiem.Text != "Nhập ID hoặc tên nhân viên...")
                {
                    lichPhanCas = _lichPhanCaRepository.SearchByEmployeeIdOrName(txtTimKiem.Text.Trim(), dtpTuNgay.Value, dtpDenNgay.Value);
                }
                // Nếu là nhân viên thường, chỉ xem lịch của mình
                else if (VuToanThang_23110329.Data.CurrentUser.IsNhanVien && VuToanThang_23110329.Data.CurrentUser.CurrentEmployeeId.HasValue)
                {
                    lichPhanCas = _lichPhanCaRepository.GetByEmployee(VuToanThang_23110329.Data.CurrentUser.CurrentEmployeeId.Value, dtpTuNgay.Value, dtpDenNgay.Value);
                }
                // Nếu là quản lý/HR, xem tất cả
                else
                {
                    lichPhanCas = _lichPhanCaRepository.GetByPeriod(dtpTuNgay.Value, dtpDenNgay.Value);
                }

                dgvLichPhanCa.DataSource = lichPhanCas;
                ConfigureDataGridView();
                
                // Cập nhật title với số kết quả
                UpdateFormTitle(lichPhanCas.Count);
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

        public new void PerformLayout()
        {
            // Call the base class method first
            base.PerformLayout();
            
            // Then apply our custom layout
            if (this.Controls.Count > 0)
            {
                LayoutAllControlsCustom();
            }
        }

        private void LayoutAllControlsCustom()
        {
            int formWidth = this.ClientSize.Width;
            int formHeight = this.ClientSize.Height;

            // Title at top
            var lblTitle = this.Controls.OfType<Label>().FirstOrDefault(l => l.Text == "LỊCH PHÂN CA");
            if (lblTitle != null)
            {
                lblTitle.Location = new Point(20, 20);
            }

            // Filter panel below title
            if (pnlFilter != null)
            {
                pnlFilter.Location = new Point(20, 60);
                pnlFilter.Size = new Size(formWidth - 40, 80);
                LayoutFilterPanel();
            }

            // Action buttons below filter panel
            LayoutActionButtons();

            // Main content area (DataGridView and Info Panel)
            LayoutMainContent();
        }

        private void LayoutFilterPanel()
        {
            if (pnlFilter?.Controls == null || pnlFilter.Controls.Count == 0) return;

            int x = 10;
            int y = 20;

            // Layout filter controls horizontally
            foreach (Control control in pnlFilter.Controls)
            {
                if (control is Label)
                {
                    control.Location = new Point(x, y + 5);
                    x += control.Width + 5;
                }
                else if (control is DateTimePicker)
                {
                    control.Location = new Point(x, y);
                    control.Size = new Size(120, 25);
                    x += 130;
                }
                else if (control is TextBox)
                {
                    control.Location = new Point(x, y);
                    control.Size = new Size(200, 25);
                    x += 210;
                }
                else if (control is Button)
                {
                    control.Location = new Point(x, y);
                    // Nút "Xóa" nhỏ hơn
                    if (control.Text == "Xóa")
                    {
                        control.Size = new Size(60, 30);
                        x += 70;
                    }
                    else
                    {
                        control.Size = new Size(100, 30);
                        x += 110;
                    }
                }
            }
        }

        private void LayoutActionButtons()
        {
            var buttons = this.Controls.OfType<Button>()
                .Where(b => b != btnTimKiem && b != btnTaoLichTuan && b != btnXoaTimKiem)
                .ToList();

            if (buttons.Count == 0) return;

            int x = 20;
            int y = 150;

            foreach (var button in buttons)
            {
                button.Location = new Point(x, y);
                button.Size = new Size(80, 30);
                x += 90;
            }
        }

        private void LayoutMainContent()
        {
            if (dgvLichPhanCa == null || pnlThongTin == null) return;

            int formWidth = this.ClientSize.Width;
            int formHeight = this.ClientSize.Height;
            int contentY = 190; // Below buttons
            int availableWidth = formWidth - 40;
            int availableHeight = formHeight - contentY - 20;

            // Adaptive layout based on screen size
            if (formWidth < 700) // Small screen - vertical stacking
            {
                // DataGridView on top (60% height)
                dgvLichPhanCa.Location = new Point(20, contentY);
                dgvLichPhanCa.Size = new Size(availableWidth, (int)(availableHeight * 0.6));

                // Info Panel below (35% height)
                pnlThongTin.Location = new Point(20, dgvLichPhanCa.Bottom + 10);
                pnlThongTin.Size = new Size(availableWidth, (int)(availableHeight * 0.35));
            }
            else if (formWidth < 1000) // Medium screen - compact side-by-side
            {
                // DataGridView on left (65% width)
                dgvLichPhanCa.Location = new Point(20, contentY);
                dgvLichPhanCa.Size = new Size((int)(availableWidth * 0.65), availableHeight);

                // Info Panel on right (30% width)
                pnlThongTin.Location = new Point(dgvLichPhanCa.Right + 20, contentY);
                pnlThongTin.Size = new Size((int)(availableWidth * 0.30), availableHeight);
            }
            else // Large screen - full side-by-side
            {
                // DataGridView on left (70% width)
                dgvLichPhanCa.Location = new Point(20, contentY);
                dgvLichPhanCa.Size = new Size((int)(availableWidth * 0.70), availableHeight);

                // Info Panel on right (25% width)
                pnlThongTin.Location = new Point(dgvLichPhanCa.Right + 20, contentY);
                pnlThongTin.Size = new Size((int)(availableWidth * 0.25), availableHeight);
            }

            // Layout info panel controls
            LayoutInfoPanelControls();
        }

        private void LayoutInfoPanelControls()
        {
            if (pnlThongTin?.Controls == null || pnlThongTin.Controls.Count == 0) return;

            int y = 20;
            int spacing = 35;
            int labelWidth = 80;
            int controlWidth = Math.Max(150, pnlThongTin.Width - labelWidth - 30);

            // Title first
            if (pnlThongTin.Controls.Count > 0)
            {
                pnlThongTin.Controls[0].Location = new Point(10, y);
                pnlThongTin.Controls[0].Size = new Size(pnlThongTin.Width - 20, 25);
                y += 40;
            }

            // Layout label-control pairs
            for (int i = 1; i < pnlThongTin.Controls.Count; i += 2)
            {
                if (i + 1 < pnlThongTin.Controls.Count)
                {
                    // Label
                    pnlThongTin.Controls[i].Location = new Point(10, y);
                    pnlThongTin.Controls[i].Size = new Size(labelWidth, 20);
                    
                    // Control
                    pnlThongTin.Controls[i + 1].Location = new Point(labelWidth + 10, y - 3);
                    pnlThongTin.Controls[i + 1].Size = new Size(controlWidth, 25);
                    
                    y += spacing;
                }
            }
        }

        private void LayoutControlsVertical()
        {
            // Stack DataGridView and Info Panel vertically for small screens
            if (dgvLichPhanCa != null && pnlThongTin != null)
            {
                int availableWidth = this.ClientSize.Width - 40;
                int availableHeight = this.ClientSize.Height - 120; // Account for filter panel

                // DataGridView on top (60% height)
                dgvLichPhanCa.Location = new Point(20, 100);
                dgvLichPhanCa.Size = new Size(availableWidth, (int)(availableHeight * 0.6));

                // Info Panel below (35% height)
                pnlThongTin.Location = new Point(20, dgvLichPhanCa.Bottom + 10);
                pnlThongTin.Size = new Size(availableWidth, (int)(availableHeight * 0.35));

                // Reorganize info panel controls for vertical layout
                OrganizeInfoPanelVertical();
            }
        }

        private void LayoutControlsCompact()
        {
            // Compact side-by-side layout for medium screens
            if (dgvLichPhanCa != null && pnlThongTin != null)
            {
                int availableWidth = this.ClientSize.Width - 60; // Account for spacing
                int availableHeight = this.ClientSize.Height - 120;

                // DataGridView on left (65% width)
                dgvLichPhanCa.Location = new Point(20, 100);
                dgvLichPhanCa.Size = new Size((int)(availableWidth * 0.65), availableHeight);

                // Info Panel on right (30% width)
                pnlThongTin.Location = new Point(dgvLichPhanCa.Right + 20, 100);
                pnlThongTin.Size = new Size((int)(availableWidth * 0.30), availableHeight);

                // Reorganize info panel controls for compact layout
                OrganizeInfoPanelCompact();
            }
        }

        private void LayoutControlsFull()
        {
            // Full side-by-side layout for large screens
            if (dgvLichPhanCa != null && pnlThongTin != null)
            {
                int availableWidth = this.ClientSize.Width - 60;
                int availableHeight = this.ClientSize.Height - 120;

                // DataGridView on left (70% width)
                dgvLichPhanCa.Location = new Point(20, 100);
                dgvLichPhanCa.Size = new Size((int)(availableWidth * 0.70), availableHeight);

                // Info Panel on right (25% width)
                pnlThongTin.Location = new Point(dgvLichPhanCa.Right + 20, 100);
                pnlThongTin.Size = new Size((int)(availableWidth * 0.25), availableHeight);

                // Reorganize info panel controls for full layout
                OrganizeInfoPanelFull();
            }
        }

        private void OrganizeInfoPanelVertical()
        {
            // Horizontal layout of controls within info panel for vertical mode
            if (pnlThongTin?.Controls != null && pnlThongTin.Controls.Count > 0)
            {
                int y = 20;
                int spacing = 35;
                int labelWidth = 80;
                int controlWidth = pnlThongTin.Width - labelWidth - 30;

                // Title first
                pnlThongTin.Controls[0].Location = new Point(10, y);
                pnlThongTin.Controls[0].Size = new Size(pnlThongTin.Width - 20, 25);
                y += 40;

                // Layout label-control pairs
                for (int i = 1; i < pnlThongTin.Controls.Count; i += 2)
                {
                    if (i + 1 < pnlThongTin.Controls.Count)
                    {
                        // Label
                        pnlThongTin.Controls[i].Location = new Point(10, y);
                        pnlThongTin.Controls[i].Size = new Size(labelWidth, 20);
                        
                        // Control
                        pnlThongTin.Controls[i + 1].Location = new Point(labelWidth + 10, y - 3);
                        pnlThongTin.Controls[i + 1].Size = new Size(controlWidth, 25);
                        
                        y += spacing;
                    }
                }
            }
        }

        private void OrganizeInfoPanelCompact()
        {
            // Vertical stacking of controls within narrow info panel
            if (pnlThongTin?.Controls != null && pnlThongTin.Controls.Count > 0)
            {
                int y = 20;
                int spacing = 35;
                int labelWidth = 70;
                int controlWidth = pnlThongTin.Width - labelWidth - 30;

                // Title first
                pnlThongTin.Controls[0].Location = new Point(10, y);
                pnlThongTin.Controls[0].Size = new Size(pnlThongTin.Width - 20, 25);
                y += 40;

                // Layout label-control pairs
                for (int i = 1; i < pnlThongTin.Controls.Count; i += 2)
                {
                    if (i + 1 < pnlThongTin.Controls.Count)
                    {
                        // Label
                        pnlThongTin.Controls[i].Location = new Point(10, y);
                        pnlThongTin.Controls[i].Size = new Size(labelWidth, 20);
                        
                        // Control
                        pnlThongTin.Controls[i + 1].Location = new Point(labelWidth + 10, y - 3);
                        pnlThongTin.Controls[i + 1].Size = new Size(controlWidth, 25);
                        
                        y += spacing;
                    }
                }
            }
        }

        private void OrganizeInfoPanelFull()
        {
            // Standard vertical stacking with more spacing for full layout
            if (pnlThongTin?.Controls != null && pnlThongTin.Controls.Count > 0)
            {
                int y = 20;
                int spacing = 40;
                int labelWidth = 90;
                int controlWidth = pnlThongTin.Width - labelWidth - 30;

                // Title first
                pnlThongTin.Controls[0].Location = new Point(15, y);
                pnlThongTin.Controls[0].Size = new Size(pnlThongTin.Width - 30, 25);
                y += 45;

                // Layout label-control pairs
                for (int i = 1; i < pnlThongTin.Controls.Count; i += 2)
                {
                    if (i + 1 < pnlThongTin.Controls.Count)
                    {
                        // Label
                        pnlThongTin.Controls[i].Location = new Point(15, y);
                        pnlThongTin.Controls[i].Size = new Size(labelWidth, 20);
                        
                        // Control
                        pnlThongTin.Controls[i + 1].Location = new Point(labelWidth + 15, y - 3);
                        pnlThongTin.Controls[i + 1].Size = new Size(controlWidth, 25);
                        
                        y += spacing;
                    }
                }
            }
        }

        private void ShowMessage(string message, string title, MessageBoxIcon icon)
        {
            MessageBox.Show(message, title, MessageBoxButtons.OK, icon);
        }

        private void UpdateFormTitle(int resultCount)
        {
            string baseTitle = "LỊCH PHÂN CA";
            
            if (!string.IsNullOrWhiteSpace(txtTimKiem?.Text) && txtTimKiem.Text != "Nhập ID hoặc tên nhân viên...")
            {
                this.Text = $"{baseTitle} - Tìm kiếm: '{txtTimKiem.Text.Trim()}' ({resultCount} kết quả)";
            }
            else
            {
                this.Text = $"{baseTitle} ({resultCount} bản ghi)";
            }
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

        private void txtTimKiem_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                LoadData();
                e.Handled = true;
            }
        }

        private void btnXoaTimKiem_Click(object sender, EventArgs e)
        {
            ClearSearchText();
            LoadData();
        }

        private void txtTimKiem_Enter(object sender, EventArgs e)
        {
            if (txtTimKiem.Text == "Nhập ID hoặc tên nhân viên...")
            {
                txtTimKiem.Text = "";
                txtTimKiem.ForeColor = Color.White;
            }
        }

        private void txtTimKiem_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTimKiem.Text))
            {
                txtTimKiem.Text = "Nhập ID hoặc tên nhân viên...";
                txtTimKiem.ForeColor = Color.Gray;
            }
        }

        private void ClearSearchText()
        {
            txtTimKiem.Text = "Nhập ID hoặc tên nhân viên...";
            txtTimKiem.ForeColor = Color.Gray;
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
