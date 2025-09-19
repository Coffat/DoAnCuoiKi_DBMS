using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using VuToanThang_23110329.Data;
using VuToanThang_23110329.Models;
using VuToanThang_23110329.Repositories;

namespace VuToanThang_23110329.Forms
{
    public partial class BangLuongForm : Form
    {
        private readonly BangLuongRepository _bangLuongRepository;

        // UI Controls
        private DataGridView dgvBangLuong;
        private ComboBox cmbThang, cmbNam, cmbTrangThai;
        private Button btnTimKiem, btnXuatExcel, btnInBangLuong, btnLamMoi;
        private Panel pnlFilter, pnlSummary;
        private Label lblTongNhanVien, lblTongLuong, lblLuongTB;

        public BangLuongForm()
        {
            InitializeComponent();
            _bangLuongRepository = new BangLuongRepository();
            InitializeForm();
        }

        private void InitializeComponent()
        {
            this.BackColor = Color.FromArgb(50, 50, 50);
            this.Size = new Size(1400, 900);
            this.Text = "Bảng lương";
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
                Text = "BẢNG LƯƠNG NHÂN VIÊN",
                Font = new Font("Segoe UI", 20, FontStyle.Bold),
                ForeColor = Color.FromArgb(124, 77, 255),
                AutoSize = true
            };

            // Filter Panel
            pnlFilter = new Panel
            {
                BackColor = Color.FromArgb(60, 60, 60),
                BorderStyle = BorderStyle.FixedSingle,
                Size = new Size(1340, 80)
            };

            var lblThang = CreateLabel("Tháng:");
            cmbThang = CreateComboBox();
            for (int i = 1; i <= 12; i++)
                cmbThang.Items.Add(i);
            cmbThang.SelectedItem = DateTime.Now.Month;

            var lblNam = CreateLabel("Năm:");
            cmbNam = CreateComboBox();
            for (int i = DateTime.Now.Year - 2; i <= DateTime.Now.Year + 1; i++)
                cmbNam.Items.Add(i);
            cmbNam.SelectedItem = DateTime.Now.Year;

            var lblTrangThai = CreateLabel("Trạng thái:");
            cmbTrangThai = CreateComboBox();
            cmbTrangThai.Items.AddRange(new[] { "Tất cả", "Mo", "Dong" });
            cmbTrangThai.SelectedIndex = 0;

            btnTimKiem = CreateButton("Tìm kiếm", Color.FromArgb(33, 150, 243));
            btnXuatExcel = CreateButton("Xuất Excel", Color.FromArgb(46, 125, 50));
            btnInBangLuong = CreateButton("In bảng lương", Color.FromArgb(156, 39, 176));
            btnLamMoi = CreateButton("Làm mới", Color.FromArgb(96, 125, 139));

            pnlFilter.Controls.AddRange(new Control[] {
                lblThang, cmbThang, lblNam, cmbNam, lblTrangThai, cmbTrangThai,
                btnTimKiem, btnXuatExcel, btnInBangLuong, btnLamMoi
            });

            // Summary Panel
            pnlSummary = new Panel
            {
                BackColor = Color.FromArgb(60, 60, 60),
                BorderStyle = BorderStyle.FixedSingle,
                Size = new Size(1340, 60)
            };

            lblTongNhanVien = CreateStatLabel("Tổng nhân viên: 0", Color.White);
            lblTongLuong = CreateStatLabel("Tổng chi lương: 0 VNĐ", Color.LightGreen);
            lblLuongTB = CreateStatLabel("Lương TB: 0 VNĐ", Color.LightBlue);

            pnlSummary.Controls.AddRange(new Control[] { lblTongNhanVien, lblTongLuong, lblLuongTB });

            // DataGridView
            dgvBangLuong = new DataGridView
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

            dgvBangLuong.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(124, 77, 255);
            dgvBangLuong.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvBangLuong.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10.5F, FontStyle.Bold);
            dgvBangLuong.DefaultCellStyle.BackColor = Color.FromArgb(60, 60, 60);
            dgvBangLuong.DefaultCellStyle.ForeColor = Color.White;
            dgvBangLuong.DefaultCellStyle.SelectionBackColor = Color.FromArgb(124, 77, 255);

            this.Controls.AddRange(new Control[] { lblTitle, pnlFilter, pnlSummary, dgvBangLuong });
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

        private ComboBox CreateComboBox()
        {
            return new ComboBox
            {
                BackColor = Color.FromArgb(70, 70, 70),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                DropDownStyle = ComboBoxStyle.DropDownList,
                Font = new Font("Segoe UI", 9),
                Size = new Size(80, 25)
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

            // Layout filter controls
            pnlFilter.Controls[0].Location = new Point(10, 15); // lblThang
            cmbThang.Location = new Point(10, 35);

            pnlFilter.Controls[2].Location = new Point(110, 15); // lblNam
            cmbNam.Location = new Point(110, 35);

            pnlFilter.Controls[4].Location = new Point(210, 15); // lblTrangThai
            cmbTrangThai.Location = new Point(210, 35);
            cmbTrangThai.Size = new Size(100, 25);

            btnTimKiem.Location = new Point(330, 33);
            btnXuatExcel.Location = new Point(440, 33);
            btnInBangLuong.Location = new Point(550, 33);
            btnLamMoi.Location = new Point(660, 33);

            // Summary Panel
            pnlSummary.Location = new Point(20, 170);

            lblTongNhanVien.Location = new Point(20, 20);
            lblTongLuong.Location = new Point(200, 20);
            lblLuongTB.Location = new Point(450, 20);

            // DataGridView
            dgvBangLuong.Location = new Point(20, 250);
            dgvBangLuong.Size = new Size(1340, 600);
        }

        private void SetupEventHandlers()
        {
            btnTimKiem.Click += btnTimKiem_Click;
            btnXuatExcel.Click += btnXuatExcel_Click;
            btnInBangLuong.Click += btnInBangLuong_Click;
            btnLamMoi.Click += btnLamMoi_Click;

            dgvBangLuong.CellDoubleClick += dgvBangLuong_CellDoubleClick;
        }

        private void InitializeForm()
        {
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                int thang = (int)cmbThang.SelectedItem;
                int nam = (int)cmbNam.SelectedItem;

                var bangLuongs = _bangLuongRepository.GetByPeriod(nam, thang);

                // Filter by status if selected
                if (cmbTrangThai.SelectedIndex > 0)
                {
                    string selectedStatus = cmbTrangThai.Text;
                    bangLuongs = bangLuongs.Where(bl => bl.TrangThai == selectedStatus).ToList();
                }

                dgvBangLuong.DataSource = bangLuongs;
                ConfigureDataGridView();
                UpdateSummary(bangLuongs);
            }
            catch (Exception ex)
            {
                ShowMessage($"Lỗi tải dữ liệu: {ex.Message}", "Lỗi", MessageBoxIcon.Error);
            }
        }

        private void ConfigureDataGridView()
        {
            if (dgvBangLuong.Columns.Count > 0)
            {
                dgvBangLuong.Columns["MaBangLuong"].HeaderText = "Mã BL";
                dgvBangLuong.Columns["TenNhanVien"].HeaderText = "Nhân viên";
                dgvBangLuong.Columns["ChucDanh"].HeaderText = "Chức danh";
                dgvBangLuong.Columns["PhongBan"].HeaderText = "Phòng ban";
                dgvBangLuong.Columns["LuongCoBan"].HeaderText = "Lương CB";
                dgvBangLuong.Columns["TongGioCong"].HeaderText = "Giờ công";
                dgvBangLuong.Columns["GioOT"].HeaderText = "Giờ OT";
                dgvBangLuong.Columns["PhuCap"].HeaderText = "Phụ cấp";
                dgvBangLuong.Columns["KhauTru"].HeaderText = "Khấu trừ";
                dgvBangLuong.Columns["ThueBH"].HeaderText = "Thuế BH";
                dgvBangLuong.Columns["ThucLanh"].HeaderText = "Thực lãnh";
                dgvBangLuong.Columns["TrangThai"].HeaderText = "Trạng thái";

                // Hide some columns
                if (dgvBangLuong.Columns["MaNV"] != null)
                    dgvBangLuong.Columns["MaNV"].Visible = false;
                if (dgvBangLuong.Columns["Nam"] != null)
                    dgvBangLuong.Columns["Nam"].Visible = false;
                if (dgvBangLuong.Columns["Thang"] != null)
                    dgvBangLuong.Columns["Thang"].Visible = false;

                // Format currency columns
                var currencyColumns = new[] { "LuongCoBan", "PhuCap", "KhauTru", "ThueBH", "ThucLanh" };
                foreach (var col in currencyColumns)
                {
                    if (dgvBangLuong.Columns[col] != null)
                    {
                        dgvBangLuong.Columns[col].DefaultCellStyle.Format = "N0";
                        dgvBangLuong.Columns[col].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    }
                }

                // Format hour columns
                var hourColumns = new[] { "TongGioCong", "GioOT" };
                foreach (var col in hourColumns)
                {
                    if (dgvBangLuong.Columns[col] != null)
                    {
                        dgvBangLuong.Columns[col].DefaultCellStyle.Format = "F2";
                        dgvBangLuong.Columns[col].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    }
                }

                // Color status and salary columns
                dgvBangLuong.CellFormatting += (s, e) =>
                {
                    if (dgvBangLuong.Columns[e.ColumnIndex].Name == "TrangThai")
                    {
                        switch (e.Value?.ToString())
                        {
                            case "Mo":
                                e.CellStyle.ForeColor = Color.Orange;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                                break;
                            case "Dong":
                                e.CellStyle.ForeColor = Color.LightGreen;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                                break;
                        }
                    }
                    else if (dgvBangLuong.Columns[e.ColumnIndex].Name == "ThucLanh")
                    {
                        e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                        e.CellStyle.ForeColor = Color.LightGreen;
                    }
                };
            }
        }

        private void UpdateSummary(System.Collections.Generic.List<BangLuong> bangLuongs)
        {
            lblTongNhanVien.Text = $"Tổng nhân viên: {bangLuongs.Count}";
            
            decimal tongLuong = bangLuongs.Sum(bl => bl.ThucLanh);
            lblTongLuong.Text = $"Tổng chi lương: {tongLuong:N0} VNĐ";

            decimal luongTB = bangLuongs.Count > 0 ? tongLuong / bangLuongs.Count : 0;
            lblLuongTB.Text = $"Lương TB: {luongTB:N0} VNĐ";
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

        private void btnXuatExcel_Click(object sender, EventArgs e)
        {
            try
            {
                // Placeholder for Excel export functionality
                ShowMessage("Chức năng xuất Excel đang được phát triển!", "Thông báo", MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                ShowMessage($"Lỗi xuất Excel: {ex.Message}", "Lỗi", MessageBoxIcon.Error);
            }
        }

        private void btnInBangLuong_Click(object sender, EventArgs e)
        {
            try
            {
                // Placeholder for print functionality
                ShowMessage("Chức năng in bảng lương đang được phát triển!", "Thông báo", MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                ShowMessage($"Lỗi in bảng lương: {ex.Message}", "Lỗi", MessageBoxIcon.Error);
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void dgvBangLuong_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dgvBangLuong.SelectedRows.Count > 0)
            {
                var selectedBL = (BangLuong)dgvBangLuong.SelectedRows[0].DataBoundItem;
                var detailForm = new ChiTietBangLuongForm(selectedBL);
                detailForm.ShowDialog();
            }
        }
    }

    // Detail form for viewing payroll details
    public partial class ChiTietBangLuongForm : Form
    {
        public ChiTietBangLuongForm(BangLuong bangLuong)
        {
            InitializeComponent();
            LoadPayrollDetails(bangLuong);
        }

        private void InitializeComponent()
        {
            this.Text = "Chi tiết bảng lương";
            this.Size = new Size(600, 500);
            this.BackColor = Color.FromArgb(50, 50, 50);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
        }

        private void LoadPayrollDetails(BangLuong bl)
        {
            var lblTitle = new Label
            {
                Text = $"CHI TIẾT LƯƠNG - {bl.TenNhanVien}",
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                ForeColor = Color.FromArgb(124, 77, 255),
                Location = new Point(20, 20),
                AutoSize = true
            };

            var details = $@"
Mã bảng lương: {bl.MaBangLuong}
Nhân viên: {bl.TenNhanVien}
Chức danh: {bl.ChucDanh}
Phòng ban: {bl.PhongBan}
Kỳ lương: {bl.Thang:00}/{bl.Nam}

=== THÔNG TIN LƯƠNG ===
Lương cơ bản: {bl.LuongCoBan:N0} VNĐ
Tổng giờ công: {bl.TongGioCong:F2} giờ
Giờ làm thêm: {bl.GioOT:F2} giờ

=== TÍNH TOÁN ===
Phụ cấp: {bl.PhuCap:N0} VNĐ
Khấu trừ: {bl.KhauTru:N0} VNĐ
Thuế & BH: {bl.ThueBH:N0} VNĐ

=== KẾT QUẢ ===
Thực lãnh: {bl.ThucLanh:N0} VNĐ
Trạng thái: {(bl.TrangThai == "Mo" ? "Đang mở" : "Đã đóng")}";

            var lblDetails = new Label
            {
                Text = details,
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 10),
                Location = new Point(20, 60),
                Size = new Size(540, 350),
                AutoSize = false
            };

            var btnClose = new Button
            {
                Text = "Đóng",
                BackColor = Color.FromArgb(158, 158, 158),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Size = new Size(80, 35),
                Location = new Point(250, 420)
            };

            btnClose.Click += (s, e) => this.Close();

            this.Controls.AddRange(new Control[] { lblTitle, lblDetails, btnClose });
        }
    }
}
