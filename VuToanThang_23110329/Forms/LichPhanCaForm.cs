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

        // UI Controls
        private DataGridView dgvLichPhanCa;
        private DateTimePicker dtpTuNgay, dtpDenNgay, dtpNgayLam;
        private ComboBox cmbNhanVien, cmbCaLam, cmbTrangThai;
        private TextBox txtGhiChu;
        private Button btnThem, btnSua, btnXoa, btnLuu, btnHuy, btnLamMoi, btnTimKiem, btnTaoLichTuan;
        private Panel pnlThongTin, pnlFilter;

        public LichPhanCaForm()
        {
            InitializeComponent();
            _lichPhanCaRepository = new LichPhanCaRepository();
            _nhanVienRepository = new NhanVienRepository();
            _caLamRepository = new CaLamRepository();
            CreateControls();
            LayoutControls();
            SetupEventHandlers();
            InitializeForm();
        }


        private void CreateControls()
        {
            // Title
            var lblTitle = new Label
            {
                Text = "LỊCH PHÂN CA",
                Font = new Font("Segoe UI", 20, FontStyle.Bold),
                ForeColor = Color.FromArgb(124, 77, 255),
                AutoSize = true
            };

            // Filter Panel
            pnlFilter = new Panel
            {
                BackColor = Color.FromArgb(60, 60, 60),
                BorderStyle = BorderStyle.FixedSingle,
                Height = 80
            };

            var lblTuNgay = CreateLabel("Từ ngày:");
            dtpTuNgay = CreateDatePicker();
            dtpTuNgay.Value = DateTime.Now.AddDays(-7);

            var lblDenNgay = CreateLabel("Đến ngày:");
            dtpDenNgay = CreateDatePicker();
            dtpDenNgay.Value = DateTime.Now.AddDays(7);

            btnTimKiem = CreateButton("Tìm kiếm", Color.FromArgb(33, 150, 243));
            btnTaoLichTuan = CreateButton("Tạo lịch tuần", Color.FromArgb(76, 175, 80));

            pnlFilter.Controls.AddRange(new Control[] { lblTuNgay, dtpTuNgay, lblDenNgay, dtpDenNgay, btnTimKiem, btnTaoLichTuan });

            // Buttons
            btnThem = CreateButton("Thêm", Color.FromArgb(46, 125, 50));
            btnSua = CreateButton("Sửa", Color.FromArgb(255, 152, 0));
            btnXoa = CreateButton("Xóa", Color.FromArgb(244, 67, 54));
            btnLuu = CreateButton("Lưu", Color.FromArgb(33, 150, 243));
            btnHuy = CreateButton("Hủy", Color.FromArgb(158, 158, 158));
            btnLamMoi = CreateButton("Làm mới", Color.FromArgb(96, 125, 139));

            btnLuu.Enabled = false;
            btnHuy.Enabled = false;

            // DataGridView
            dgvLichPhanCa = new DataGridView
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

            dgvLichPhanCa.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(124, 77, 255);
            dgvLichPhanCa.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvLichPhanCa.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10.5F, FontStyle.Bold);
            dgvLichPhanCa.DefaultCellStyle.BackColor = Color.FromArgb(60, 60, 60);
            dgvLichPhanCa.DefaultCellStyle.ForeColor = Color.White;
            dgvLichPhanCa.DefaultCellStyle.SelectionBackColor = Color.FromArgb(124, 77, 255);

            // Information Panel
            pnlThongTin = new Panel
            {
                BackColor = Color.FromArgb(60, 60, 60),
                BorderStyle = BorderStyle.FixedSingle,
                Padding = new Padding(15)
            };

            CreateInfoControls();

            this.Controls.AddRange(new Control[] { lblTitle, pnlFilter, btnThem, btnSua, btnXoa, btnLuu, btnHuy, btnLamMoi, dgvLichPhanCa, pnlThongTin });
        }

        private void CreateInfoControls()
        {
            var lblThongTin = new Label
            {
                Text = "THÔNG TIN LỊCH PHÂN CA",
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                ForeColor = Color.FromArgb(124, 77, 255),
                AutoSize = true
            };

            cmbNhanVien = CreateComboBox();
            cmbCaLam = CreateComboBox();
            dtpNgayLam = CreateDatePicker();
            cmbTrangThai = CreateComboBox();
            cmbTrangThai.Items.AddRange(new[] { "Mo", "Khoa" });
            cmbTrangThai.SelectedIndex = 0;

            txtGhiChu = CreateTextBox();
            txtGhiChu.Multiline = true;
            txtGhiChu.Height = 60;

            pnlThongTin.Controls.AddRange(new Control[] {
                lblThongTin,
                CreateLabel("Nhân viên:"), cmbNhanVien,
                CreateLabel("Ca làm:"), cmbCaLam,
                CreateLabel("Ngày làm:"), dtpNgayLam,
                CreateLabel("Trạng thái:"), cmbTrangThai,
                CreateLabel("Ghi chú:"), txtGhiChu
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

        private DateTimePicker CreateDatePicker()
        {
            return new DateTimePicker
            {
                Format = DateTimePickerFormat.Short,
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

            // Filter Panel
            pnlFilter.Location = new Point(20, 70);
            pnlFilter.Size = new Size(1340, 80);

            // Layout filter controls
            pnlFilter.Controls[0].Location = new Point(10, 15); // lblTuNgay
            dtpTuNgay.Location = new Point(10, 35);
            dtpTuNgay.Size = new Size(120, 25);

            pnlFilter.Controls[2].Location = new Point(150, 15); // lblDenNgay
            dtpDenNgay.Location = new Point(150, 35);
            dtpDenNgay.Size = new Size(120, 25);

            btnTimKiem.Location = new Point(290, 33);
            btnTaoLichTuan.Location = new Point(400, 33);

            // Buttons
            int btnY = 170;
            btnThem.Location = new Point(20, btnY);
            btnSua.Location = new Point(130, btnY);
            btnXoa.Location = new Point(240, btnY);
            btnLuu.Location = new Point(350, btnY);
            btnHuy.Location = new Point(460, btnY);
            btnLamMoi.Location = new Point(570, btnY);

            // DataGridView
            dgvLichPhanCa.Location = new Point(20, 220);
            dgvLichPhanCa.Size = new Size(900, 500);

            // Information Panel
            pnlThongTin.Location = new Point(940, 220);
            pnlThongTin.Size = new Size(420, 500);

            LayoutInfoControls();
        }

        private void LayoutInfoControls()
        {
            int y = 10;
            int labelWidth = 100;
            int controlWidth = 280;
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
                    controls[i + 1].Location = new Point(120, y - 3);
                    controls[i + 1].Size = new Size(controlWidth, controls[i + 1] == txtGhiChu ? 60 : 23);
                    y += controls[i + 1] == txtGhiChu ? 70 : spacing;
                }
            }
        }

        private void SetupEventHandlers()
        {
            dgvLichPhanCa.SelectionChanged += dgvLichPhanCa_SelectionChanged;
            
            btnTimKiem.Click += btnTimKiem_Click;
            btnTaoLichTuan.Click += btnTaoLichTuan_Click;
            btnThem.Click += btnThem_Click;
            btnSua.Click += btnSua_Click;
            btnXoa.Click += btnXoa_Click;
            btnLuu.Click += btnLuu_Click;
            btnHuy.Click += btnHuy_Click;
            btnLamMoi.Click += btnLamMoi_Click;
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
                var caLams = _caLamRepository.GetAll().Where(c => c.KichHoat).ToList();
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
                if (dgvLichPhanCa.Columns["GhiChu"] != null)
                    dgvLichPhanCa.Columns["GhiChu"].Visible = false;
            }
        }

        private void SetFormMode(bool isEditing)
        {
            _isEditing = isEditing;
            
            btnThem.Enabled = !isEditing && VuToanThang_23110329.Data.CurrentUser.HasPermission("MANAGE_SCHEDULE");
            btnSua.Enabled = !isEditing && dgvLichPhanCa.SelectedRows.Count > 0 && VuToanThang_23110329.Data.CurrentUser.HasPermission("MANAGE_SCHEDULE");
            btnXoa.Enabled = !isEditing && dgvLichPhanCa.SelectedRows.Count > 0 && VuToanThang_23110329.Data.CurrentUser.HasPermission("MANAGE_SCHEDULE");
            btnLuu.Enabled = isEditing;
            btnHuy.Enabled = isEditing;
            btnTaoLichTuan.Enabled = !isEditing && VuToanThang_23110329.Data.CurrentUser.HasPermission("MANAGE_SCHEDULE");
            
            // Enable/disable input controls
            foreach (Control control in pnlThongTin.Controls)
            {
                if (control is TextBox || control is ComboBox || control is DateTimePicker)
                {
                    control.Enabled = isEditing;
                }
            }
        }

        private void ClearForm()
        {
            cmbNhanVien.SelectedIndex = -1;
            cmbCaLam.SelectedIndex = -1;
            dtpNgayLam.Value = DateTime.Now;
            cmbTrangThai.SelectedIndex = 0;
            txtGhiChu.Clear();
            
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
            txtGhiChu.Text = lich.GhiChu;
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
            ClearForm();
            SetFormMode(true);
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
                SetFormMode(true);
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
            try
            {
                if (cmbNhanVien.SelectedValue == null)
                {
                    ShowMessage("Vui lòng chọn nhân viên!", "Cảnh báo", MessageBoxIcon.Warning);
                    return;
                }

                if (cmbCaLam.SelectedValue == null)
                {
                    ShowMessage("Vui lòng chọn ca làm!", "Cảnh báo", MessageBoxIcon.Warning);
                    return;
                }

                if (_currentLichPhanCa == null) // Add new
                {
                    var lichPhanCa = new LichPhanCa
                    {
                        MaNV = (int)cmbNhanVien.SelectedValue,
                        MaCa = (int)cmbCaLam.SelectedValue,
                        NgayLam = dtpNgayLam.Value.Date,
                        TrangThai = cmbTrangThai.Text,
                        GhiChu = txtGhiChu.Text.Trim()
                    };

                    var result = _lichPhanCaRepository.Insert(lichPhanCa);
                    
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
                    _currentLichPhanCa.MaNV = (int)cmbNhanVien.SelectedValue;
                    _currentLichPhanCa.MaCa = (int)cmbCaLam.SelectedValue;
                    _currentLichPhanCa.NgayLam = dtpNgayLam.Value.Date;
                    _currentLichPhanCa.TrangThai = cmbTrangThai.Text;
                    _currentLichPhanCa.GhiChu = txtGhiChu.Text.Trim();

                    var result = _lichPhanCaRepository.Update(_currentLichPhanCa);
                    
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
            if (dgvLichPhanCa.SelectedRows.Count > 0)
            {
                var selectedLich = (LichPhanCa)dgvLichPhanCa.SelectedRows[0].DataBoundItem;
                LoadScheduleToForm(selectedLich);
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
