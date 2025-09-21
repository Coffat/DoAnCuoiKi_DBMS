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
    public partial class DonTuSMForm : Form
    {
        private readonly DonTuRepository _donTuRepository;
        private DonTu _currentDonTu;

        // UI Controls
        private DataGridView dgvDonTu;
        private ComboBox cmbTrangThai, cmbLoai;
        private DateTimePicker dtpTuNgay, dtpDenNgay;
        private TextBox txtLyDo, txtGhiChuDuyet;
        private Button btnTimKiem, btnDuyet, btnTuChoi, btnLamMoi, btnXemChiTiet;
        private Panel pnlFilter, pnlThongTin;
        private Label lblTongSo, lblChoDuyet, lblDaDuyet, lblTuChoi;

        public DonTuSMForm()
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
                Text = "DUYỆT ĐƠN TỪ - QUẢN LÝ",
                Font = new Font("Segoe UI", 20, FontStyle.Bold),
                ForeColor = Color.FromArgb(124, 77, 255),
                AutoSize = true
            };

            // Statistics Panel
            var pnlStats = new Panel
            {
                BackColor = Color.FromArgb(60, 60, 60),
                BorderStyle = BorderStyle.FixedSingle,
                Size = new Size(1340, 60)
            };

            lblTongSo = CreateStatLabel("Tổng số: 0", Color.White);
            lblChoDuyet = CreateStatLabel("Chờ duyệt: 0", Color.Orange);
            lblDaDuyet = CreateStatLabel("Đã duyệt: 0", Color.LightGreen);
            lblTuChoi = CreateStatLabel("Từ chối: 0", Color.LightCoral);

            pnlStats.Controls.AddRange(new Control[] { lblTongSo, lblChoDuyet, lblDaDuyet, lblTuChoi });

            // Filter Panel
            pnlFilter = new Panel
            {
                BackColor = Color.FromArgb(60, 60, 60),
                BorderStyle = BorderStyle.FixedSingle,
                Size = new Size(1340, 80)
            };

            var lblTuNgay = CreateLabel("Từ ngày:");
            dtpTuNgay = CreateDatePicker();
            dtpTuNgay.Value = DateTime.Now.AddDays(-30);

            var lblDenNgay = CreateLabel("Đến ngày:");
            dtpDenNgay = CreateDatePicker();

            var lblTrangThai = CreateLabel("Trạng thái:");
            cmbTrangThai = CreateComboBox();
            cmbTrangThai.Items.AddRange(new[] { "Tất cả", "ChoDuyet", "DaDuyet", "TuChoi" });
            cmbTrangThai.SelectedIndex = 1; // Default to pending

            var lblLoai = CreateLabel("Loại:");
            cmbLoai = CreateComboBox();
            cmbLoai.Items.AddRange(new[] { "Tất cả", "NGHI", "OT" });
            cmbLoai.SelectedIndex = 0;

            btnTimKiem = CreateButton("Tìm kiếm", Color.FromArgb(33, 150, 243));
            btnLamMoi = CreateButton("Làm mới", Color.FromArgb(96, 125, 139));

            pnlFilter.Controls.AddRange(new Control[] { 
                lblTuNgay, dtpTuNgay, lblDenNgay, dtpDenNgay, 
                lblTrangThai, cmbTrangThai, lblLoai, cmbLoai, 
                btnTimKiem, btnLamMoi 
            });

            // Action Buttons
            btnDuyet = CreateButton("Duyệt", Color.FromArgb(46, 125, 50));
            btnTuChoi = CreateButton("Từ chối", Color.FromArgb(244, 67, 54));
            btnXemChiTiet = CreateButton("Xem chi tiết", Color.FromArgb(156, 39, 176));

            btnDuyet.Enabled = false;
            btnTuChoi.Enabled = false;
            btnXemChiTiet.Enabled = false;

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
                lblTitle, pnlStats, pnlFilter, btnDuyet, btnTuChoi, btnXemChiTiet,
                dgvDonTu, pnlThongTin 
            });
        }

        private void CreateInfoControls()
        {
            var lblThongTin = new Label
            {
                Text = "CHI TIẾT ĐƠN TỪ",
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                ForeColor = Color.FromArgb(124, 77, 255),
                AutoSize = true
            };

            var lblNhanVien = CreateLabel("Nhân viên:");
            var txtNhanVien = CreateReadOnlyTextBox();

            var lblLoaiDon = CreateLabel("Loại đơn:");
            var txtLoaiDon = CreateReadOnlyTextBox();

            var lblThoiGian = CreateLabel("Thời gian:");
            var txtThoiGian = CreateReadOnlyTextBox();

            var lblSoGio = CreateLabel("Số giờ:");
            var txtSoGioInfo = CreateReadOnlyTextBox();

            var lblLyDoLabel = CreateLabel("Lý do:");
            txtLyDo = CreateReadOnlyTextBox();
            txtLyDo.Multiline = true;
            txtLyDo.Height = 60;

            var lblTrangThaiInfo = CreateLabel("Trạng thái:");
            var txtTrangThaiInfo = CreateReadOnlyTextBox();

            var lblGhiChuDuyet = CreateLabel("Ghi chú duyệt:");
            txtGhiChuDuyet = CreateTextBox();
            txtGhiChuDuyet.Multiline = true;
            txtGhiChuDuyet.Height = 60;
            // PlaceholderText not available in .NET Framework
            // txtGhiChuDuyet.PlaceholderText = "Nhập ghi chú khi duyệt/từ chối...";

            pnlThongTin.Controls.AddRange(new Control[] {
                lblThongTin,
                lblNhanVien, txtNhanVien,
                lblLoaiDon, txtLoaiDon,
                lblThoiGian, txtThoiGian,
                lblSoGio, txtSoGioInfo,
                lblLyDoLabel, txtLyDo,
                lblTrangThaiInfo, txtTrangThaiInfo,
                lblGhiChuDuyet, txtGhiChuDuyet
            });
        }

        private Label CreateStatLabel(string text, Color color)
        {
            return new Label
            {
                Text = text,
                ForeColor = color,
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                AutoSize = true
            };
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

        private TextBox CreateReadOnlyTextBox()
        {
            var txt = CreateTextBox();
            txt.ReadOnly = true;
            txt.BackColor = Color.FromArgb(50, 50, 50);
            return txt;
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
                Font = new Font("Segoe UI", 9),
                Size = new Size(120, 25)
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

            // Statistics Panel
            var pnlStats = (Panel)this.Controls[1];
            pnlStats.Location = new Point(20, 70);
            
            lblTongSo.Location = new Point(20, 20);
            lblChoDuyet.Location = new Point(150, 20);
            lblDaDuyet.Location = new Point(280, 20);
            lblTuChoi.Location = new Point(410, 20);

            // Filter Panel
            pnlFilter.Location = new Point(20, 150);
            
            // Layout filter controls
            pnlFilter.Controls[0].Location = new Point(10, 15); // lblTuNgay
            dtpTuNgay.Location = new Point(10, 35);

            pnlFilter.Controls[2].Location = new Point(150, 15); // lblDenNgay
            dtpDenNgay.Location = new Point(150, 35);

            pnlFilter.Controls[4].Location = new Point(290, 15); // lblTrangThai
            cmbTrangThai.Location = new Point(290, 35);
            cmbTrangThai.Size = new Size(120, 25);

            pnlFilter.Controls[6].Location = new Point(430, 15); // lblLoai
            cmbLoai.Location = new Point(430, 35);
            cmbLoai.Size = new Size(100, 25);

            btnTimKiem.Location = new Point(550, 33);
            btnLamMoi.Location = new Point(660, 33);

            // Action Buttons
            int btnY = 250;
            btnDuyet.Location = new Point(20, btnY);
            btnTuChoi.Location = new Point(130, btnY);
            btnXemChiTiet.Location = new Point(240, btnY);

            // DataGridView
            dgvDonTu.Location = new Point(20, 300);
            dgvDonTu.Size = new Size(900, 400);

            // Information Panel
            pnlThongTin.Location = new Point(940, 300);
            pnlThongTin.Size = new Size(420, 500);

            LayoutInfoControls();
        }

        private void LayoutInfoControls()
        {
            int y = 10;
            int labelWidth = 100;
            int controlWidth = 280;
            int spacing = 35;

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
                    controls[i + 1].Location = new Point(10, y + 20);
                    
                    int controlHeight = (controls[i + 1] == txtLyDo || controls[i + 1] == txtGhiChuDuyet) ? 60 : 23;
                    controls[i + 1].Size = new Size(controlWidth, controlHeight);
                    
                    y += controlHeight + spacing;
                }
            }
        }

        private void SetupEventHandlers()
        {
            dgvDonTu.SelectionChanged += dgvDonTu_SelectionChanged;
            
            btnTimKiem.Click += btnTimKiem_Click;
            btnLamMoi.Click += btnLamMoi_Click;
            btnDuyet.Click += btnDuyet_Click;
            btnTuChoi.Click += btnTuChoi_Click;
            btnXemChiTiet.Click += btnXemChiTiet_Click;

            cmbTrangThai.SelectedIndexChanged += (s, e) => LoadData();
            cmbLoai.SelectedIndexChanged += (s, e) => LoadData();
        }

        private void InitializeForm()
        {
            CreateControls();
            LayoutControls();
            SetupEventHandlers();
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                System.Diagnostics.Debug.WriteLine("DonTuSMForm.LoadData() called");
                
                if (cmbTrangThai == null)
                {
                    System.Diagnostics.Debug.WriteLine("ERROR: cmbTrangThai is null in LoadData");
                    return;
                }
                
                var donTus = _donTuRepository.GetAll();

                // Apply filters
                if (cmbTrangThai.SelectedIndex > 0)
                {
                    string selectedStatus = cmbTrangThai.Text;
                    donTus = donTus.Where(d => d.TrangThai == selectedStatus).ToList();
                }

                if (cmbLoai.SelectedIndex > 0)
                {
                    string selectedType = cmbLoai.Text;
                    donTus = donTus.Where(d => d.Loai == selectedType).ToList();
                }

                // Date filter
                donTus = donTus.Where(d => d.TuLuc.Date >= dtpTuNgay.Value.Date && 
                                          d.TuLuc.Date <= dtpDenNgay.Value.Date).ToList();

                dgvDonTu.DataSource = donTus;
                ConfigureDataGridView();
                UpdateStatistics(donTus);
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
                dgvDonTu.Columns["TenNhanVien"].HeaderText = "Nhân viên";
                dgvDonTu.Columns["Loai"].HeaderText = "Loại";
                dgvDonTu.Columns["TuLuc"].HeaderText = "Từ lúc";
                dgvDonTu.Columns["DenLuc"].HeaderText = "Đến lúc";
                dgvDonTu.Columns["SoGio"].HeaderText = "Số giờ";
                dgvDonTu.Columns["TrangThai"].HeaderText = "Trạng thái";
                dgvDonTu.Columns["TenNguoiDuyet"].HeaderText = "Người duyệt";
                
                // Hide some columns
                if (dgvDonTu.Columns["MaNV"] != null)
                    dgvDonTu.Columns["MaNV"].Visible = false;
                if (dgvDonTu.Columns["DuyetBoi"] != null)
                    dgvDonTu.Columns["DuyetBoi"].Visible = false;
                if (dgvDonTu.Columns["LyDo"] != null)
                    dgvDonTu.Columns["LyDo"].Visible = false;

                // Set status column colors
                dgvDonTu.CellFormatting += (s, e) =>
                {
                    if (dgvDonTu.Columns[e.ColumnIndex].Name == "TrangThai")
                    {
                        switch (e.Value?.ToString())
                        {
                            case "ChoDuyet":
                                e.CellStyle.ForeColor = Color.Orange;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
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

        private void UpdateStatistics(System.Collections.Generic.List<DonTu> donTus)
        {
            lblTongSo.Text = $"Tổng số: {donTus.Count}";
            lblChoDuyet.Text = $"Chờ duyệt: {donTus.Count(d => d.TrangThai == "ChoDuyet")}";
            lblDaDuyet.Text = $"Đã duyệt: {donTus.Count(d => d.TrangThai == "DaDuyet")}";
            lblTuChoi.Text = $"Từ chối: {donTus.Count(d => d.TrangThai == "TuChoi")}";
        }

        private void LoadRequestToForm(DonTu donTu)
        {
            if (donTu == null) return;

            _currentDonTu = donTu;
            
            var controls = pnlThongTin.Controls.Cast<Control>().ToArray();
            
            // Update info controls (assuming order from CreateInfoControls)
            ((TextBox)controls[2]).Text = donTu.TenNhanVien; // txtNhanVien
            ((TextBox)controls[4]).Text = donTu.Loai; // txtLoaiDon
            ((TextBox)controls[6]).Text = $"{donTu.TuLuc:dd/MM/yyyy HH:mm} - {donTu.DenLuc:dd/MM/yyyy HH:mm}"; // txtThoiGian
            ((TextBox)controls[8]).Text = donTu.SoGio?.ToString() ?? ""; // txtSoGioInfo
            txtLyDo.Text = donTu.LyDo; // txtLyDo
            ((TextBox)controls[12]).Text = donTu.TrangThai; // txtTrangThaiInfo

            // Enable/disable buttons based on status
            bool canApprove = donTu.TrangThai == "ChoDuyet";
            btnDuyet.Enabled = canApprove;
            btnTuChoi.Enabled = canApprove;
            btnXemChiTiet.Enabled = true;

            txtGhiChuDuyet.Enabled = canApprove;
        }

        public void PerformLayout()
        {
            if (this.Controls.Count == 0) return;

            int formWidth = this.ClientSize.Width - 40;
            int formHeight = this.ClientSize.Height - 40;

            // Title
            this.Controls[0].Location = new Point(20, 20);

            // Statistics Panel - responsive
            this.Controls[1].Location = new Point(20, 70); // pnlStats
            ((Panel)this.Controls[1]).Size = new Size(formWidth, 60);

            // Adaptive statistics layout
            if (formWidth < 600) // Small screen - stack vertically
            {
                LayoutManagerStatisticsVertical();
                ((Panel)this.Controls[1]).Height = 80;
            }
            else // Horizontal layout
            {
                LayoutManagerStatisticsHorizontal();
                ((Panel)this.Controls[1]).Height = 60;
            }

            // Filter Panel - responsive
            int filterY = this.Controls[1].Bottom + 20;
            pnlFilter.Location = new Point(20, filterY);
            pnlFilter.Size = new Size(formWidth, 80);

            // Adaptive filter layout
            if (formWidth < 700) // Small screen - vertical stacking
            {
                LayoutManagerFilterVertical();
                pnlFilter.Height = 120;
            }
            else if (formWidth < 1000) // Medium screen - compact
            {
                LayoutManagerFilterCompact();
                pnlFilter.Height = 80;
            }
            else // Large screen - full layout
            {
                LayoutManagerFilterFull();
                pnlFilter.Height = 80;
            }

            // Content area - adaptive layout
            int contentY = pnlFilter.Bottom + 20;
            int contentHeight = formHeight - (contentY + 20);

            if (formWidth < 800) // Small screen - stack vertically
            {
                LayoutManagerContentVertical(contentY, contentHeight, formWidth);
            }
            else // Large screen - side by side
            {
                LayoutManagerContentSideBySide(contentY, contentHeight, formWidth);
            }
        }

        private void LayoutManagerStatisticsVertical()
        {
            // Stack statistics vertically for small screens
            var pnlStats = (Panel)this.Controls[1];
            lblTongSo.Location = new Point(20, 15);
            lblChoDuyet.Location = new Point(200, 15);
            
            lblDaDuyet.Location = new Point(20, 40);
            lblTuChoi.Location = new Point(200, 40);
        }

        private void LayoutManagerStatisticsHorizontal()
        {
            // Horizontal layout for larger screens
            var pnlStats = (Panel)this.Controls[1];
            int spacing = Math.Max(120, pnlStats.Width / 5);
            
            lblTongSo.Location = new Point(20, 20);
            lblChoDuyet.Location = new Point(20 + spacing, 20);
            lblDaDuyet.Location = new Point(20 + spacing * 2, 20);
            lblTuChoi.Location = new Point(20 + spacing * 3, 20);
        }

        private void LayoutManagerFilterVertical()
        {
            // Stack filter controls vertically for small screens
            int y1 = 10, y2 = 40, y3 = 70;
            
            pnlFilter.Controls[0].Location = new Point(10, y1); // lblTuNgay
            dtpTuNgay.Location = new Point(80, y1 - 2);
            dtpTuNgay.Size = new Size(100, 25);

            pnlFilter.Controls[2].Location = new Point(190, y1); // lblDenNgay
            dtpDenNgay.Location = new Point(260, y1 - 2);
            dtpDenNgay.Size = new Size(100, 25);

            pnlFilter.Controls[4].Location = new Point(10, y2); // lblTrangThai
            cmbTrangThai.Location = new Point(80, y2 - 2);
            cmbTrangThai.Size = new Size(100, 25);

            pnlFilter.Controls[6].Location = new Point(190, y2); // lblLoai
            cmbLoai.Location = new Point(230, y2 - 2);
            cmbLoai.Size = new Size(80, 25);

            btnTimKiem.Location = new Point(10, y3 - 2);
            btnTimKiem.Size = new Size(70, 25);
            btnLamMoi.Location = new Point(90, y3 - 2);
            btnLamMoi.Size = new Size(70, 25);
        }

        private void LayoutManagerFilterCompact()
        {
            // Compact horizontal layout for medium screens
            int x = 10, y = 15;
            
            pnlFilter.Controls[0].Location = new Point(x, y); // lblTuNgay
            dtpTuNgay.Location = new Point(x, y + 20);
            dtpTuNgay.Size = new Size(90, 25);
            x += 100;

            pnlFilter.Controls[2].Location = new Point(x, y); // lblDenNgay
            dtpDenNgay.Location = new Point(x, y + 20);
            dtpDenNgay.Size = new Size(90, 25);
            x += 100;

            pnlFilter.Controls[4].Location = new Point(x, y); // lblTrangThai
            cmbTrangThai.Location = new Point(x, y + 20);
            cmbTrangThai.Size = new Size(80, 25);
            x += 90;

            pnlFilter.Controls[6].Location = new Point(x, y); // lblLoai
            cmbLoai.Location = new Point(x, y + 20);
            cmbLoai.Size = new Size(60, 25);
            x += 70;

            btnTimKiem.Location = new Point(x, y + 18);
            btnTimKiem.Size = new Size(60, 25);
            x += 70;
            btnLamMoi.Location = new Point(x, y + 18);
            btnLamMoi.Size = new Size(60, 25);
        }

        private void LayoutManagerFilterFull()
        {
            // Full horizontal layout for large screens
            int x = 10, y = 15;
            
            pnlFilter.Controls[0].Location = new Point(x, y); // lblTuNgay
            dtpTuNgay.Location = new Point(x, y + 20);
            dtpTuNgay.Size = new Size(120, 25);
            x += 130;

            pnlFilter.Controls[2].Location = new Point(x, y); // lblDenNgay
            dtpDenNgay.Location = new Point(x, y + 20);
            dtpDenNgay.Size = new Size(120, 25);
            x += 130;

            pnlFilter.Controls[4].Location = new Point(x, y); // lblTrangThai
            cmbTrangThai.Location = new Point(x, y + 20);
            cmbTrangThai.Size = new Size(100, 25);
            x += 110;

            pnlFilter.Controls[6].Location = new Point(x, y); // lblLoai
            cmbLoai.Location = new Point(x, y + 20);
            cmbLoai.Size = new Size(80, 25);
            x += 90;

            btnTimKiem.Location = new Point(x, y + 18);
            x += 100;
            btnLamMoi.Location = new Point(x, y + 18);
        }

        private void LayoutManagerContentVertical(int startY, int availableHeight, int availableWidth)
        {
            // Stack DataGridView and Info Panel vertically
            dgvDonTu.Location = new Point(20, startY);
            dgvDonTu.Size = new Size(availableWidth, (int)(availableHeight * 0.65));

            if (pnlThongTin != null)
            {
                pnlThongTin.Location = new Point(20, dgvDonTu.Bottom + 10);
                pnlThongTin.Size = new Size(availableWidth, (int)(availableHeight * 0.30));
            }

            // Position action buttons
            int buttonY = Math.Max(dgvDonTu.Bottom, pnlThongTin?.Bottom ?? 0) + 10;
            btnDuyet.Location = new Point(20, buttonY);
            btnTuChoi.Location = new Point(100, buttonY);
            btnXemChiTiet.Location = new Point(180, buttonY);
        }

        private void LayoutManagerContentSideBySide(int startY, int availableHeight, int availableWidth)
        {
            // Side-by-side layout for larger screens
            dgvDonTu.Location = new Point(20, startY);
            dgvDonTu.Size = new Size((int)(availableWidth * 0.65), availableHeight);

            if (pnlThongTin != null)
            {
                pnlThongTin.Location = new Point(dgvDonTu.Right + 20, startY);
                pnlThongTin.Size = new Size((int)(availableWidth * 0.30), availableHeight);
            }

            // Position action buttons at bottom
            int buttonY = startY + availableHeight + 10;
            btnDuyet.Location = new Point(20, buttonY);
            btnTuChoi.Location = new Point(130, buttonY);
            btnXemChiTiet.Location = new Point(240, buttonY);
        }

        private void ShowMessage(string message, string title, MessageBoxIcon icon)
        {
            MessageBox.Show(message, title, MessageBoxButtons.OK, icon);
        }

        // Event Handlers
        private void dgvDonTu_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvDonTu.SelectedRows.Count > 0)
            {
                var selectedDonTu = (DonTu)dgvDonTu.SelectedRows[0].DataBoundItem;
                LoadRequestToForm(selectedDonTu);
            }
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            LoadData();
            txtGhiChuDuyet.Clear();
        }

        private void btnDuyet_Click(object sender, EventArgs e)
        {
            if (_currentDonTu == null || _currentDonTu.TrangThai != "ChoDuyet")
            {
                ShowMessage("Vui lòng chọn đơn từ đang chờ duyệt!", "Cảnh báo", MessageBoxIcon.Warning);
                return;
            }

            var result = MessageBox.Show(
                $"Bạn có chắc chắn muốn duyệt đơn {_currentDonTu.Loai} của {_currentDonTu.TenNhanVien}?",
                "Xác nhận duyệt",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result == DialogResult.Yes)
            {
                try
                {
                    if (!VuToanThang_23110329.Data.CurrentUser.CurrentEmployeeId.HasValue)
                    {
                        ShowMessage("Không xác định được người duyệt!", "Lỗi", MessageBoxIcon.Error);
                        return;
                    }

                    var approveResult = _donTuRepository.DuyetDonTu(
                        _currentDonTu.MaDon,
                        VuToanThang_23110329.Data.CurrentUser.CurrentEmployeeId.Value,
                        true
                    );

                    if (approveResult.Success)
                    {
                        ShowMessage(approveResult.Message, "Thành công", MessageBoxIcon.Information);
                        LoadData();
                        txtGhiChuDuyet.Clear();
                    }
                    else
                    {
                        ShowMessage(approveResult.Message, "Lỗi", MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    ShowMessage($"Lỗi duyệt đơn từ: {ex.Message}", "Lỗi", MessageBoxIcon.Error);
                }
            }
        }

        private void btnTuChoi_Click(object sender, EventArgs e)
        {
            if (_currentDonTu == null || _currentDonTu.TrangThai != "ChoDuyet")
            {
                ShowMessage("Vui lòng chọn đơn từ đang chờ duyệt!", "Cảnh báo", MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtGhiChuDuyet.Text))
            {
                ShowMessage("Vui lòng nhập lý do từ chối!", "Cảnh báo", MessageBoxIcon.Warning);
                return;
            }

            var result = MessageBox.Show(
                $"Bạn có chắc chắn muốn từ chối đơn {_currentDonTu.Loai} của {_currentDonTu.TenNhanVien}?",
                "Xác nhận từ chối",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result == DialogResult.Yes)
            {
                try
                {
                    if (!VuToanThang_23110329.Data.CurrentUser.CurrentEmployeeId.HasValue)
                    {
                        ShowMessage("Không xác định được người duyệt!", "Lỗi", MessageBoxIcon.Error);
                        return;
                    }

                    var rejectResult = _donTuRepository.DuyetDonTu(
                        _currentDonTu.MaDon,
                        VuToanThang_23110329.Data.CurrentUser.CurrentEmployeeId.Value,
                        false
                    );

                    if (rejectResult.Success)
                    {
                        ShowMessage(rejectResult.Message, "Thành công", MessageBoxIcon.Information);
                        LoadData();
                        txtGhiChuDuyet.Clear();
                    }
                    else
                    {
                        ShowMessage(rejectResult.Message, "Lỗi", MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    ShowMessage($"Lỗi từ chối đơn từ: {ex.Message}", "Lỗi", MessageBoxIcon.Error);
                }
            }
        }

        private void btnXemChiTiet_Click(object sender, EventArgs e)
        {
            if (_currentDonTu == null) return;

            var detailForm = new DonTuDetailForm(_currentDonTu);
            detailForm.ShowDialog();
        }
    }

    // Simple detail form for viewing request details
    public partial class DonTuDetailForm : Form
    {
        public DonTuDetailForm(DonTu donTu)
        {
            InitializeComponent();
            LoadRequestDetails(donTu);
        }


        private void LoadRequestDetails(DonTu donTu)
        {
            var lblTitle = new Label
            {
                Text = $"CHI TIẾT ĐƠN {donTu.Loai}",
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                ForeColor = Color.FromArgb(124, 77, 255),
                Location = new Point(20, 20),
                AutoSize = true
            };

            var details = $@"
Mã đơn: {donTu.MaDon}
Nhân viên: {donTu.TenNhanVien}
Loại đơn: {donTu.Loai}
Từ lúc: {donTu.TuLuc:dd/MM/yyyy HH:mm}
Đến lúc: {donTu.DenLuc:dd/MM/yyyy HH:mm}
Số giờ: {donTu.SoGio}
Trạng thái: {donTu.TrangThai}
Người duyệt: {donTu.TenNguoiDuyet ?? "Chưa có"}

Lý do:
{donTu.LyDo}";

            var lblDetails = new Label
            {
                Text = details,
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 10),
                Location = new Point(20, 60),
                Size = new Size(440, 250),
                AutoSize = false
            };

            var btnClose = new Button
            {
                Text = "Đóng",
                BackColor = Color.FromArgb(158, 158, 158),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Size = new Size(80, 35),
                Location = new Point(200, 320)
            };

            btnClose.Click += (s, e) => this.Close();

            this.Controls.AddRange(new Control[] { lblTitle, lblDetails, btnClose });
        }
    }
}
