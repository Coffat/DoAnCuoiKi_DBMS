using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using VuToanThang_23110329.Data;
using VuToanThang_23110329.Models;
using VuToanThang_23110329.Repositories;

namespace VuToanThang_23110329.Forms
{
    public partial class DonTuHRForm : Form
    {
        private readonly DonTuRepository _donTuRepository;

        // UI Controls
        private DataGridView dgvDonTu;
        private ComboBox cmbTrangThai, cmbLoai, cmbPhongBan;
        private DateTimePicker dtpTuNgay, dtpDenNgay;
        private Button btnTimKiem, btnDuyet, btnTuChoi, btnLamMoi, btnXuatBaoCao;
        private Panel pnlFilter, pnlThongKe, pnlThongTin;
        private Label lblTongSo, lblChoDuyet, lblDaDuyet, lblTuChoi;
        private TextBox txtGhiChuDuyet;

        public DonTuHRForm()
        {
            InitializeComponent();
            _donTuRepository = new DonTuRepository();
            InitializeForm();
        }

        private void InitializeComponent()
        {
            this.BackColor = Color.FromArgb(50, 50, 50);
            this.Size = new Size(1400, 900);
            this.Text = "Quản lý đơn từ - HR";
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
                Text = "QUẢN LÝ ĐƠN TỪ - NHÂN SỰ",
                Font = new Font("Segoe UI", 20, FontStyle.Bold),
                ForeColor = Color.FromArgb(124, 77, 255),
                AutoSize = true
            };

            // Statistics Panel
            pnlThongKe = new Panel
            {
                BackColor = Color.FromArgb(60, 60, 60),
                BorderStyle = BorderStyle.FixedSingle,
                Size = new Size(1340, 60)
            };

            lblTongSo = CreateStatLabel("Tổng số: 0", Color.White);
            lblChoDuyet = CreateStatLabel("Chờ duyệt: 0", Color.Orange);
            lblDaDuyet = CreateStatLabel("Đã duyệt: 0", Color.LightGreen);
            lblTuChoi = CreateStatLabel("Từ chối: 0", Color.LightCoral);

            pnlThongKe.Controls.AddRange(new Control[] { lblTongSo, lblChoDuyet, lblDaDuyet, lblTuChoi });

            // Filter Panel
            pnlFilter = new Panel
            {
                BackColor = Color.FromArgb(60, 60, 60),
                BorderStyle = BorderStyle.FixedSingle,
                Size = new Size(1340, 80)
            };

            dtpTuNgay = CreateDatePicker();
            dtpTuNgay.Value = DateTime.Now.AddDays(-30);
            dtpDenNgay = CreateDatePicker();

            cmbTrangThai = CreateComboBox();
            cmbTrangThai.Items.AddRange(new[] { "Tất cả", "ChoDuyet", "DaDuyet", "TuChoi" });
            cmbTrangThai.SelectedIndex = 0;

            cmbLoai = CreateComboBox();
            cmbLoai.Items.AddRange(new[] { "Tất cả", "NGHI", "OT" });
            cmbLoai.SelectedIndex = 0;

            cmbPhongBan = CreateComboBox();
            cmbPhongBan.Items.AddRange(new[] { "Tất cả", "Bán hàng", "Kho", "Kế toán", "Bảo vệ" });
            cmbPhongBan.SelectedIndex = 0;

            btnTimKiem = CreateButton("Tìm kiếm", Color.FromArgb(33, 150, 243));
            btnXuatBaoCao = CreateButton("Xuất báo cáo", Color.FromArgb(156, 39, 176));
            btnLamMoi = CreateButton("Làm mới", Color.FromArgb(96, 125, 139));

            pnlFilter.Controls.AddRange(new Control[] {
                CreateLabel("Từ ngày:"), dtpTuNgay, CreateLabel("Đến ngày:"), dtpDenNgay,
                CreateLabel("Trạng thái:"), cmbTrangThai, CreateLabel("Loại:"), cmbLoai,
                CreateLabel("Phòng ban:"), cmbPhongBan, btnTimKiem, btnXuatBaoCao, btnLamMoi
            });

            // Action Buttons
            btnDuyet = CreateButton("Duyệt hàng loạt", Color.FromArgb(46, 125, 50));
            btnTuChoi = CreateButton("Từ chối hàng loạt", Color.FromArgb(244, 67, 54));

            // DataGridView
            dgvDonTu = CreateDataGridView();

            // Information Panel
            pnlThongTin = new Panel
            {
                BackColor = Color.FromArgb(60, 60, 60),
                BorderStyle = BorderStyle.FixedSingle,
                Padding = new Padding(15)
            };

            var lblGhiChu = CreateLabel("Ghi chú duyệt:");
            txtGhiChuDuyet = CreateTextBox();
            txtGhiChuDuyet.Multiline = true;
            txtGhiChuDuyet.Height = 80;

            pnlThongTin.Controls.AddRange(new Control[] { lblGhiChu, txtGhiChuDuyet });

            this.Controls.AddRange(new Control[] {
                lblTitle, pnlThongKe, pnlFilter, btnDuyet, btnTuChoi, dgvDonTu, pnlThongTin
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
                Size = new Size(120, 35),
                Font = new Font("Segoe UI", 9, FontStyle.Bold)
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
                Font = new Font("Segoe UI", 9),
                Size = new Size(120, 25)
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

        private DataGridView CreateDataGridView()
        {
            var dgv = new DataGridView
            {
                BackgroundColor = Color.FromArgb(60, 60, 60),
                ForeColor = Color.White,
                BorderStyle = BorderStyle.None,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                ReadOnly = true,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = true,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            };

            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(124, 77, 255);
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10.5F, FontStyle.Bold);
            dgv.DefaultCellStyle.BackColor = Color.FromArgb(60, 60, 60);
            dgv.DefaultCellStyle.ForeColor = Color.White;
            dgv.DefaultCellStyle.SelectionBackColor = Color.FromArgb(124, 77, 255);

            return dgv;
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
            pnlThongKe.Location = new Point(20, 70);
            lblTongSo.Location = new Point(20, 20);
            lblChoDuyet.Location = new Point(150, 20);
            lblDaDuyet.Location = new Point(280, 20);
            lblTuChoi.Location = new Point(410, 20);

            // Filter Panel
            pnlFilter.Location = new Point(20, 150);

            // Layout filter controls
            int x = 20, y = 15;
            pnlFilter.Controls[0].Location = new Point(x, y); // "Từ ngày:"
            dtpTuNgay.Location = new Point(x, y + 20);
            x += 140;

            pnlFilter.Controls[2].Location = new Point(x, y); // "Đến ngày:"
            dtpDenNgay.Location = new Point(x, y + 20);
            x += 140;

            pnlFilter.Controls[4].Location = new Point(x, y); // "Trạng thái:"
            cmbTrangThai.Location = new Point(x, y + 20);
            x += 140;

            pnlFilter.Controls[6].Location = new Point(x, y); // "Loại:"
            cmbLoai.Location = new Point(x, y + 20);
            x += 140;

            pnlFilter.Controls[8].Location = new Point(x, y); // "Phòng ban:"
            cmbPhongBan.Location = new Point(x, y + 20);
            x += 140;

            btnTimKiem.Location = new Point(x, y + 18);
            x += 130;
            btnXuatBaoCao.Location = new Point(x, y + 18);
            x += 130;
            btnLamMoi.Location = new Point(x, y + 18);

            // Action Buttons
            btnDuyet.Location = new Point(20, 250);
            btnTuChoi.Location = new Point(150, 250);

            // DataGridView
            dgvDonTu.Location = new Point(20, 300);
            dgvDonTu.Size = new Size(900, 400);

            // Information Panel
            pnlThongTin.Location = new Point(940, 300);
            pnlThongTin.Size = new Size(420, 400);

            pnlThongTin.Controls[0].Location = new Point(15, 15);
            txtGhiChuDuyet.Location = new Point(15, 40);
            txtGhiChuDuyet.Size = new Size(390, 80);
        }

        private void SetupEventHandlers()
        {
            btnTimKiem.Click += btnTimKiem_Click;
            btnDuyet.Click += btnDuyet_Click;
            btnTuChoi.Click += btnTuChoi_Click;
            btnXuatBaoCao.Click += btnXuatBaoCao_Click;
            btnLamMoi.Click += btnLamMoi_Click;

            cmbTrangThai.SelectedIndexChanged += (s, e) => LoadData();
            cmbLoai.SelectedIndexChanged += (s, e) => LoadData();
            cmbPhongBan.SelectedIndexChanged += (s, e) => LoadData();
        }

        private void InitializeForm()
        {
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                var donTus = _donTuRepository.GetAll();

                // Apply filters
                donTus = donTus.Where(d => d.TuLuc.Date >= dtpTuNgay.Value.Date && 
                                          d.TuLuc.Date <= dtpDenNgay.Value.Date).ToList();

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
                dgvDonTu.Columns["LyDo"].HeaderText = "Lý do";
                dgvDonTu.Columns["TrangThai"].HeaderText = "Trạng thái";
                dgvDonTu.Columns["TenNguoiDuyet"].HeaderText = "Người duyệt";

                // Hide some columns
                if (dgvDonTu.Columns["MaNV"] != null)
                    dgvDonTu.Columns["MaNV"].Visible = false;
                if (dgvDonTu.Columns["DuyetBoi"] != null)
                    dgvDonTu.Columns["DuyetBoi"].Visible = false;

                // Color status column
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

        private void ShowMessage(string message, string title, MessageBoxIcon icon)
        {
            MessageBox.Show(message, title, MessageBoxButtons.OK, icon);
        }

        // Event Handlers
        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void btnDuyet_Click(object sender, EventArgs e)
        {
            if (dgvDonTu.SelectedRows.Count == 0)
            {
                ShowMessage("Vui lòng chọn ít nhất một đơn từ để duyệt!", "Cảnh báo", MessageBoxIcon.Warning);
                return;
            }

            var result = MessageBox.Show(
                $"Bạn có chắc chắn muốn duyệt {dgvDonTu.SelectedRows.Count} đơn từ đã chọn?",
                "Xác nhận duyệt hàng loạt",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result == DialogResult.Yes)
            {
                try
                {
                    int successCount = 0;
                    foreach (DataGridViewRow row in dgvDonTu.SelectedRows)
                    {
                        var donTu = (DonTu)row.DataBoundItem;
                        if (donTu.TrangThai == "ChoDuyet")
                        {
                            var approveResult = _donTuRepository.DuyetDonTu(
                                donTu.MaDon,
                                CurrentUser.CurrentEmployeeId.Value,
                                true
                            );
                            if (approveResult.Success) successCount++;
                        }
                    }

                    ShowMessage($"Đã duyệt thành công {successCount} đơn từ!", "Thành công", MessageBoxIcon.Information);
                    LoadData();
                    txtGhiChuDuyet.Clear();
                }
                catch (Exception ex)
                {
                    ShowMessage($"Lỗi duyệt đơn từ: {ex.Message}", "Lỗi", MessageBoxIcon.Error);
                }
            }
        }

        private void btnTuChoi_Click(object sender, EventArgs e)
        {
            if (dgvDonTu.SelectedRows.Count == 0)
            {
                ShowMessage("Vui lòng chọn ít nhất một đơn từ để từ chối!", "Cảnh báo", MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtGhiChuDuyet.Text))
            {
                ShowMessage("Vui lòng nhập lý do từ chối!", "Cảnh báo", MessageBoxIcon.Warning);
                return;
            }

            var result = MessageBox.Show(
                $"Bạn có chắc chắn muốn từ chối {dgvDonTu.SelectedRows.Count} đơn từ đã chọn?",
                "Xác nhận từ chối hàng loạt",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result == DialogResult.Yes)
            {
                try
                {
                    int successCount = 0;
                    foreach (DataGridViewRow row in dgvDonTu.SelectedRows)
                    {
                        var donTu = (DonTu)row.DataBoundItem;
                        if (donTu.TrangThai == "ChoDuyet")
                        {
                            var rejectResult = _donTuRepository.DuyetDonTu(
                                donTu.MaDon,
                                CurrentUser.CurrentEmployeeId.Value,
                                false
                            );
                            if (rejectResult.Success) successCount++;
                        }
                    }

                    ShowMessage($"Đã từ chối thành công {successCount} đơn từ!", "Thành công", MessageBoxIcon.Information);
                    LoadData();
                    txtGhiChuDuyet.Clear();
                }
                catch (Exception ex)
                {
                    ShowMessage($"Lỗi từ chối đơn từ: {ex.Message}", "Lỗi", MessageBoxIcon.Error);
                }
            }
        }

        private void btnXuatBaoCao_Click(object sender, EventArgs e)
        {
            ShowMessage("Chức năng xuất báo cáo đang được phát triển!", "Thông báo", MessageBoxIcon.Information);
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            LoadData();
            txtGhiChuDuyet.Clear();
        }
    }
}
