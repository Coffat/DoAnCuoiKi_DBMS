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
        private TextBox txtSearch, txtTenCa, txtMoTa, txtHeSoCa;
        private DateTimePicker dtpGioBatDau, dtpGioKetThuc;
        private CheckBox chkKichHoat;
        private Button btnThem, btnSua, btnXoa, btnLuu, btnHuy, btnLamMoi;
        private Panel pnlThongTin;

        public CaLamForm()
        {
            InitializeComponent();
            _caLamRepository = new CaLamRepository();
            InitializeForm();
        }

        private void InitializeComponent()
        {
            this.BackColor = Color.FromArgb(50, 50, 50);
            this.Size = new Size(1200, 800);
            this.Text = "Quản lý ca làm";
            this.Padding = new Padding(20);

            CreateControls();
            LayoutControls();
            SetupEventHandlers();
        }

        private void CreateControls()
        {
            // Title
            var lblTitle = new Label
            {
                Text = "QUẢN LÝ CA LÀM",
                Font = new Font("Segoe UI", 20, FontStyle.Bold),
                ForeColor = Color.FromArgb(124, 77, 255),
                AutoSize = true
            };

            // Search
            var lblSearch = new Label { Text = "Tìm kiếm:", ForeColor = Color.White, AutoSize = true };
            txtSearch = new TextBox { Width = 300, BackColor = Color.FromArgb(60, 60, 60), ForeColor = Color.White, BorderStyle = BorderStyle.FixedSingle };

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
            dgvCaLam = new DataGridView
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

            dgvCaLam.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(124, 77, 255);
            dgvCaLam.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvCaLam.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10.5F, FontStyle.Bold);
            dgvCaLam.DefaultCellStyle.BackColor = Color.FromArgb(60, 60, 60);
            dgvCaLam.DefaultCellStyle.ForeColor = Color.White;
            dgvCaLam.DefaultCellStyle.SelectionBackColor = Color.FromArgb(124, 77, 255);

            // Information Panel
            pnlThongTin = new Panel
            {
                BackColor = Color.FromArgb(60, 60, 60),
                BorderStyle = BorderStyle.FixedSingle,
                Padding = new Padding(15)
            };

            CreateInfoControls();

            this.Controls.AddRange(new Control[] { lblTitle, lblSearch, txtSearch, btnThem, btnSua, btnXoa, btnLuu, btnHuy, btnLamMoi, dgvCaLam, pnlThongTin });
        }

        private void CreateInfoControls()
        {
            var lblThongTin = new Label
            {
                Text = "THÔNG TIN CA LÀM",
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                ForeColor = Color.FromArgb(124, 77, 255),
                AutoSize = true
            };

            txtTenCa = CreateTextBox();
            dtpGioBatDau = CreateTimePicker();
            dtpGioKetThuc = CreateTimePicker();
            txtHeSoCa = CreateTextBox();
            txtMoTa = CreateTextBox();
            txtMoTa.Multiline = true;
            txtMoTa.Height = 60;
            chkKichHoat = new CheckBox
            {
                Text = "Kích hoạt",
                ForeColor = Color.White,
                AutoSize = true,
                Checked = true
            };

            pnlThongTin.Controls.AddRange(new Control[] {
                lblThongTin,
                CreateLabel("Tên ca:"), txtTenCa,
                CreateLabel("Giờ bắt đầu:"), dtpGioBatDau,
                CreateLabel("Giờ kết thúc:"), dtpGioKetThuc,
                CreateLabel("Hệ số ca:"), txtHeSoCa,
                CreateLabel("Mô tả:"), txtMoTa,
                chkKichHoat
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
                Size = new Size(80, 35),
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

        private DateTimePicker CreateTimePicker()
        {
            return new DateTimePicker
            {
                Format = DateTimePickerFormat.Time,
                ShowUpDown = true,
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

            // Search
            this.Controls[1].Location = new Point(20, 70);
            txtSearch.Location = new Point(100, 68);

            // Buttons
            int btnY = 110;
            btnThem.Location = new Point(20, btnY);
            btnSua.Location = new Point(110, btnY);
            btnXoa.Location = new Point(200, btnY);
            btnLuu.Location = new Point(290, btnY);
            btnHuy.Location = new Point(380, btnY);
            btnLamMoi.Location = new Point(470, btnY);

            // DataGridView
            dgvCaLam.Location = new Point(20, 160);
            dgvCaLam.Size = new Size(700, 400);

            // Information Panel
            pnlThongTin.Location = new Point(740, 160);
            pnlThongTin.Size = new Size(420, 400);

            LayoutInfoControls();
        }

        private void LayoutInfoControls()
        {
            int y = 10;
            int labelWidth = 100;
            int controlWidth = 250;
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
                    controls[i + 1].Size = new Size(controlWidth, controls[i + 1] == txtMoTa ? 60 : 23);
                    y += controls[i + 1] == txtMoTa ? 70 : spacing;
                }
            }

            // Handle checkbox separately
            if (controls.Length > 0 && controls[controls.Length - 1] is CheckBox)
            {
                controls[controls.Length - 1].Location = new Point(10, y);
            }
        }

        private void SetupEventHandlers()
        {
            txtSearch.TextChanged += txtSearch_TextChanged;
            dgvCaLam.SelectionChanged += dgvCaLam_SelectionChanged;
            
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
                dgvCaLam.Columns["KichHoat"].HeaderText = "Kích hoạt";
                
                // Hide MoTa column in grid
                if (dgvCaLam.Columns["MoTa"] != null)
                    dgvCaLam.Columns["MoTa"].Visible = false;
            }
        }

        private void SetFormMode(bool isEditing)
        {
            _isEditing = isEditing;
            
            btnThem.Enabled = !isEditing && CurrentUser.HasPermission("MANAGE_SHIFTS");
            btnSua.Enabled = !isEditing && dgvCaLam.SelectedRows.Count > 0 && CurrentUser.HasPermission("MANAGE_SHIFTS");
            btnXoa.Enabled = !isEditing && dgvCaLam.SelectedRows.Count > 0 && CurrentUser.HasPermission("MANAGE_SHIFTS");
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
            txtMoTa.Clear();
            chkKichHoat.Checked = true;
            
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
            txtMoTa.Text = ca.MoTa;
            chkKichHoat.Checked = ca.KichHoat;
        }

        private void ShowMessage(string message, string title, MessageBoxIcon icon)
        {
            MessageBox.Show(message, title, MessageBoxButtons.OK, icon);
        }

        // Event Handlers
        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            // Implement search functionality
            LoadData(); // For now, just reload data
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
                        HeSoCa = heSoCa,
                        MoTa = txtMoTa.Text.Trim(),
                        KichHoat = chkKichHoat.Checked
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
                    _currentCaLam.MoTa = txtMoTa.Text.Trim();
                    _currentCaLam.KichHoat = chkKichHoat.Checked;

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
