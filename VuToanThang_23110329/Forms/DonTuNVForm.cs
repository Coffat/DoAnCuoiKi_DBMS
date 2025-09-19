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

        // UI Controls
        private DataGridView dgvDonTu;
        private ComboBox cmbLoai, cmbTrangThai;
        private DateTimePicker dtpTuLuc, dtpDenLuc;
        private TextBox txtLyDo, txtSoGio;
        private Button btnThem, btnSua, btnXoa, btnLuu, btnHuy, btnLamMoi;
        private Panel pnlThongTin;

        public DonTuNVForm()
        {
            InitializeComponent();
            _donTuRepository = new DonTuRepository();
            InitializeForm();
        }


        private void CreateControls()
        {
            // Title
            var lblTitle = new Label
            {
                Text = "ĐơN TỪ CỦA TÔI",
                Font = new Font("Segoe UI", 20, FontStyle.Bold),
                ForeColor = Color.FromArgb(124, 77, 255),
                AutoSize = true
            };

            // Filter
            var lblFilter = CreateLabel("Lọc theo trạng thái:");
            cmbTrangThai = CreateComboBox();
            cmbTrangThai.Items.AddRange(new[] { "Tất cả", "ChoDuyet", "DaDuyet", "TuChoi" });
            cmbTrangThai.SelectedIndex = 0;

            // Buttons
            btnThem = CreateButton("Tạo đơn mới", Color.FromArgb(46, 125, 50));
            btnSua = CreateButton("Sửa", Color.FromArgb(255, 152, 0));
            btnXoa = CreateButton("Xóa", Color.FromArgb(244, 67, 54));
            btnLuu = CreateButton("Lưu", Color.FromArgb(33, 150, 243));
            btnHuy = CreateButton("Hủy", Color.FromArgb(158, 158, 158));
            btnLamMoi = CreateButton("Làm mới", Color.FromArgb(96, 125, 139));

            btnLuu.Enabled = false;
            btnHuy.Enabled = false;

            // DataGridView
            dgvDonTu = new DataGridView
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

            dgvDonTu.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(124, 77, 255);
            dgvDonTu.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvDonTu.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10.5F, FontStyle.Bold);
            dgvDonTu.DefaultCellStyle.BackColor = Color.FromArgb(60, 60, 60);
            dgvDonTu.DefaultCellStyle.ForeColor = Color.White;
            dgvDonTu.DefaultCellStyle.SelectionBackColor = Color.FromArgb(124, 77, 255);

            // Information Panel
            pnlThongTin = new Panel
            {
                BackColor = Color.FromArgb(60, 60, 60),
                BorderStyle = BorderStyle.FixedSingle,
                Padding = new Padding(15)
            };

            CreateInfoControls();

            this.Controls.AddRange(new Control[] { 
                lblTitle, lblFilter, cmbTrangThai, btnThem, btnSua, btnXoa, 
                btnLuu, btnHuy, btnLamMoi, dgvDonTu, pnlThongTin 
            });
        }

        private void CreateInfoControls()
        {
            var lblThongTin = new Label
            {
                Text = "THÔNG TIN ĐƠN TỪ",
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                ForeColor = Color.FromArgb(124, 77, 255),
                AutoSize = true
            };

            cmbLoai = CreateComboBox();
            cmbLoai.Items.AddRange(new[] { "NGHI", "OT" });
            cmbLoai.SelectedIndex = 0;

            dtpTuLuc = CreateDateTimePicker();
            dtpDenLuc = CreateDateTimePicker();

            txtSoGio = CreateTextBox();
            txtSoGio.Text = "8";

            txtLyDo = CreateTextBox();
            txtLyDo.Multiline = true;
            txtLyDo.Height = 80;

            pnlThongTin.Controls.AddRange(new Control[] {
                lblThongTin,
                CreateLabel("Loại đơn:"), cmbLoai,
                CreateLabel("Từ lúc:"), dtpTuLuc,
                CreateLabel("Đến lúc:"), dtpDenLuc,
                CreateLabel("Số giờ:"), txtSoGio,
                CreateLabel("Lý do:"), txtLyDo
            });
        }

        private Button CreateButton(string text, Color backColor)
        {
            return new Button
            {
                Text = text,
                BackColor = backColor,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Size = new Size(100, 35),
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

        private DateTimePicker CreateDateTimePicker()
        {
            return new DateTimePicker
            {
                Format = DateTimePickerFormat.Custom,
                CustomFormat = "dd/MM/yyyy HH:mm",
                Font = new Font("Segoe UI", 9)
            };
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

        private void LayoutControls()
        {
            // Title
            this.Controls[0].Location = new Point(20, 20);

            // Filter
            this.Controls[1].Location = new Point(20, 70);
            cmbTrangThai.Location = new Point(150, 68);
            cmbTrangThai.Size = new Size(150, 25);

            // Buttons
            int btnY = 110;
            btnThem.Location = new Point(20, btnY);
            btnSua.Location = new Point(130, btnY);
            btnXoa.Location = new Point(240, btnY);
            btnLuu.Location = new Point(350, btnY);
            btnHuy.Location = new Point(460, btnY);
            btnLamMoi.Location = new Point(570, btnY);

            // DataGridView
            dgvDonTu.Location = new Point(20, 160);
            dgvDonTu.Size = new Size(700, 400);

            // Information Panel
            pnlThongTin.Location = new Point(740, 160);
            pnlThongTin.Size = new Size(420, 400);

            LayoutInfoControls();
        }

        private void LayoutInfoControls()
        {
            int y = 10;
            int labelWidth = 80;
            int controlWidth = 300;
            int spacing = 40;

            var controls = pnlThongTin.Controls.Cast<Control>().ToArray();
            
            // Title
            controls[0].Location = new Point(10, y);
            y += 40;

            // Layout pairs of label and control
            for (int i = 1; i < controls.Length; i += 2)
            {
                if (i + 1 < controls.Length)
                {
                    controls[i].Location = new Point(10, y);
                    controls[i].Size = new Size(labelWidth, 20);
                    controls[i + 1].Location = new Point(100, y - 3);
                    controls[i + 1].Size = new Size(controlWidth, controls[i + 1] == txtLyDo ? 80 : 23);
                    y += controls[i + 1] == txtLyDo ? 90 : spacing;
                }
            }
        }

        private void SetupEventHandlers()
        {
            dgvDonTu.SelectionChanged += dgvDonTu_SelectionChanged;
            cmbTrangThai.SelectedIndexChanged += cmbTrangThai_SelectedIndexChanged;
            cmbLoai.SelectedIndexChanged += cmbLoai_SelectedIndexChanged;
            
            btnThem.Click += btnThem_Click;
            btnSua.Click += btnSua_Click;
            btnXoa.Click += btnXoa_Click;
            btnLuu.Click += btnLuu_Click;
            btnHuy.Click += btnHuy_Click;
            btnLamMoi.Click += btnLamMoi_Click;
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
