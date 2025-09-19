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
    public partial class TinhLuongForm : Form
    {
        private readonly BangLuongRepository _bangLuongRepository;
        private readonly ChamCongRepository _chamCongRepository;

        // UI Controls
        private ComboBox cmbThang, cmbNam;
        private NumericUpDown nudGioChuan, nudHeSoOT;
        private DataGridView dgvCongThang, dgvBangLuong;
        private Button btnXemCong, btnChayLuong, btnDongLuong, btnCapNhatPhuCap;
        private Panel pnlThongSo, pnlCongThang, pnlBangLuong;
        private TabControl tabControl;
        private Label lblTongNhanVien, lblTongLuong, lblTrangThai;

        public TinhLuongForm()
        {
            InitializeComponent();
            _bangLuongRepository = new BangLuongRepository();
            _chamCongRepository = new ChamCongRepository();
            InitializeForm();
        }


        private void CreateControls()
        {
            // Title
            var lblTitle = new Label
            {
                Text = "TÍNH LƯƠNG NHÂN VIÊN",
                Font = new Font("Segoe UI", 20, FontStyle.Bold),
                ForeColor = Color.FromArgb(124, 77, 255),
                AutoSize = true
            };

            // Parameters Panel
            pnlThongSo = new Panel
            {
                BackColor = Color.FromArgb(60, 60, 60),
                BorderStyle = BorderStyle.FixedSingle,
                Size = new Size(1340, 100)
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

            var lblGioChuan = CreateLabel("Giờ chuẩn/tháng:");
            nudGioChuan = new NumericUpDown
            {
                Minimum = 100,
                Maximum = 300,
                Value = 208,
                DecimalPlaces = 1,
                BackColor = Color.FromArgb(70, 70, 70),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 9)
            };

            var lblHeSoOT = CreateLabel("Hệ số OT:");
            nudHeSoOT = new NumericUpDown
            {
                Minimum = 1,
                Maximum = 3,
                Value = 1.5m,
                DecimalPlaces = 2,
                Increment = 0.1m,
                BackColor = Color.FromArgb(70, 70, 70),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 9)
            };

            btnXemCong = CreateButton("Xem công tháng", Color.FromArgb(33, 150, 243));
            btnChayLuong = CreateButton("Chạy bảng lương", Color.FromArgb(46, 125, 50));
            btnDongLuong = CreateButton("Đóng bảng lương", Color.FromArgb(244, 67, 54));

            pnlThongSo.Controls.AddRange(new Control[] {
                lblThang, cmbThang, lblNam, cmbNam,
                lblGioChuan, nudGioChuan, lblHeSoOT, nudHeSoOT,
                btnXemCong, btnChayLuong, btnDongLuong
            });

            // Tab Control
            tabControl = new TabControl
            {
                BackColor = Color.FromArgb(60, 60, 60),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 10, FontStyle.Bold)
            };

            // Tab 1: Monthly Attendance Summary
            var tabCongThang = new TabPage("Tổng hợp công tháng")
            {
                BackColor = Color.FromArgb(50, 50, 50),
                ForeColor = Color.White
            };

            // Tab 2: Payroll
            var tabBangLuong = new TabPage("Bảng lương")
            {
                BackColor = Color.FromArgb(50, 50, 50),
                ForeColor = Color.White
            };

            CreateTab1Controls(tabCongThang);
            CreateTab2Controls(tabBangLuong);

            tabControl.TabPages.AddRange(new TabPage[] { tabCongThang, tabBangLuong });

            this.Controls.AddRange(new Control[] { lblTitle, pnlThongSo, tabControl });
        }

        private void CreateTab1Controls(TabPage tab)
        {
            // Attendance Summary Grid
            dgvCongThang = CreateDataGridView();
            dgvCongThang.Location = new Point(20, 20);
            dgvCongThang.Size = new Size(1300, 500);

            var lblInfo = new Label
            {
                Text = "Tổng hợp công tháng - Dữ liệu từ view vw_CongThang",
                ForeColor = Color.LightGray,
                Font = new Font("Segoe UI", 10, FontStyle.Italic),
                Location = new Point(20, 530),
                AutoSize = true
            };

            tab.Controls.AddRange(new Control[] { dgvCongThang, lblInfo });
        }

        private void CreateTab2Controls(TabPage tab)
        {
            // Summary Panel
            var pnlSummary = new Panel
            {
                BackColor = Color.FromArgb(60, 60, 60),
                BorderStyle = BorderStyle.FixedSingle,
                Size = new Size(1300, 60),
                Location = new Point(20, 20)
            };

            lblTongNhanVien = CreateStatLabel("Tổng nhân viên: 0", Color.White);
            lblTongLuong = CreateStatLabel("Tổng lương: 0 VNĐ", Color.LightGreen);
            lblTrangThai = CreateStatLabel("Trạng thái: Chưa chạy", Color.Orange);

            pnlSummary.Controls.AddRange(new Control[] { lblTongNhanVien, lblTongLuong, lblTrangThai });

            // Payroll Grid
            dgvBangLuong = CreateDataGridView();
            dgvBangLuong.Location = new Point(20, 100);
            dgvBangLuong.Size = new Size(1300, 400);

            // Update Allowance Panel
            var pnlCapNhat = new Panel
            {
                BackColor = Color.FromArgb(60, 60, 60),
                BorderStyle = BorderStyle.FixedSingle,
                Size = new Size(1300, 80),
                Location = new Point(20, 520)
            };

            var lblCapNhat = CreateLabel("Cập nhật phụ cấp/khấu trừ:");
            btnCapNhatPhuCap = CreateButton("Cập nhật hàng loạt", Color.FromArgb(156, 39, 176));

            pnlCapNhat.Controls.AddRange(new Control[] { lblCapNhat, btnCapNhatPhuCap });

            tab.Controls.AddRange(new Control[] { pnlSummary, dgvBangLuong, pnlCapNhat });

            // Layout summary controls
            lblTongNhanVien.Location = new Point(20, 20);
            lblTongLuong.Location = new Point(200, 20);
            lblTrangThai.Location = new Point(450, 20);

            // Layout update controls
            lblCapNhat.Location = new Point(20, 15);
            btnCapNhatPhuCap.Location = new Point(20, 35);
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
                Size = new Size(80, 25)
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
                MultiSelect = false,
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

            // Parameters Panel
            pnlThongSo.Location = new Point(20, 70);

            // Layout parameter controls
            var controls = pnlThongSo.Controls.Cast<Control>().ToArray();
            
            // First row
            controls[0].Location = new Point(20, 15); // lblThang
            cmbThang.Location = new Point(20, 35);
            
            controls[2].Location = new Point(120, 15); // lblNam
            cmbNam.Location = new Point(120, 35);

            controls[4].Location = new Point(220, 15); // lblGioChuan
            nudGioChuan.Location = new Point(220, 35);
            nudGioChuan.Size = new Size(80, 25);

            controls[6].Location = new Point(320, 15); // lblHeSoOT
            nudHeSoOT.Location = new Point(320, 35);
            nudHeSoOT.Size = new Size(80, 25);

            // Buttons
            btnXemCong.Location = new Point(450, 25);
            btnChayLuong.Location = new Point(590, 25);
            btnDongLuong.Location = new Point(730, 25);

            // Tab Control
            tabControl.Location = new Point(20, 190);
            tabControl.Size = new Size(1360, 650);
        }

        private void SetupEventHandlers()
        {
            btnXemCong.Click += btnXemCong_Click;
            btnChayLuong.Click += btnChayLuong_Click;
            btnDongLuong.Click += btnDongLuong_Click;
            btnCapNhatPhuCap.Click += btnCapNhatPhuCap_Click;

            cmbThang.SelectedIndexChanged += (s, e) => LoadPayrollData();
            cmbNam.SelectedIndexChanged += (s, e) => LoadPayrollData();

            dgvBangLuong.CellDoubleClick += dgvBangLuong_CellDoubleClick;
        }

        private void InitializeForm()
        {
            if (!VuToanThang_23110329.Data.CurrentUser.HasPermission("MANAGE_PAYROLL"))
            {
                ShowMessage("Bạn không có quyền truy cập chức năng này!", "Cảnh báo", MessageBoxIcon.Warning);
                this.Close();
                return;
            }

            LoadAttendanceData();
            LoadPayrollData();
        }

        private void LoadAttendanceData()
        {
            try
            {
                int thang = (int)cmbThang.SelectedItem;
                int nam = (int)cmbNam.SelectedItem;

                var congThang = _chamCongRepository.GetCongThang(nam, thang);
                dgvCongThang.DataSource = congThang;
                ConfigureAttendanceGrid();
            }
            catch (Exception ex)
            {
                ShowMessage($"Lỗi tải dữ liệu công tháng: {ex.Message}", "Lỗi", MessageBoxIcon.Error);
            }
        }

        private void LoadPayrollData()
        {
            try
            {
                int thang = (int)cmbThang.SelectedItem;
                int nam = (int)cmbNam.SelectedItem;

                var bangLuong = _bangLuongRepository.GetByPeriod(nam, thang);
                dgvBangLuong.DataSource = bangLuong;
                ConfigurePayrollGrid();
                UpdatePayrollSummary(bangLuong);
            }
            catch (Exception ex)
            {
                ShowMessage($"Lỗi tải dữ liệu bảng lương: {ex.Message}", "Lỗi", MessageBoxIcon.Error);
            }
        }

        private void ConfigureAttendanceGrid()
        {
            if (dgvCongThang.Columns.Count > 0)
            {
                dgvCongThang.Columns["MaNV"].HeaderText = "Mã NV";
                dgvCongThang.Columns["Nam"].HeaderText = "Năm";
                dgvCongThang.Columns["Thang"].HeaderText = "Tháng";
                dgvCongThang.Columns["TongGioCong"].HeaderText = "Tổng giờ công";
                dgvCongThang.Columns["TongPhutDiTre"].HeaderText = "Tổng phút đi trễ";
                dgvCongThang.Columns["TongPhutVeSom"].HeaderText = "Tổng phút về sớm";

                // Format numeric columns
                dgvCongThang.Columns["TongGioCong"].DefaultCellStyle.Format = "F2";
                dgvCongThang.Columns["TongGioCong"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }
        }

        private void ConfigurePayrollGrid()
        {
            if (dgvBangLuong.Columns.Count > 0)
            {
                dgvBangLuong.Columns["MaBangLuong"].HeaderText = "Mã BL";
                dgvBangLuong.Columns["TenNhanVien"].HeaderText = "Nhân viên";
                dgvBangLuong.Columns["ChucDanh"].HeaderText = "Chức danh";
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
                if (dgvBangLuong.Columns["PhongBan"] != null)
                    dgvBangLuong.Columns["PhongBan"].Visible = false;

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

                // Color status column
                dgvBangLuong.CellFormatting += (s, e) =>
                {
                    if (dgvBangLuong.Columns[e.ColumnIndex].Name == "TrangThai")
                    {
                        switch (e.Value?.ToString())
                        {
                            case "Mo":
                                e.CellStyle.ForeColor = Color.Orange;
                                break;
                            case "Dong":
                                e.CellStyle.ForeColor = Color.LightGreen;
                                break;
                        }
                    }
                };
            }
        }

        private void UpdatePayrollSummary(System.Collections.Generic.List<BangLuong> bangLuongs)
        {
            lblTongNhanVien.Text = $"Tổng nhân viên: {bangLuongs.Count}";
            
            decimal tongLuong = bangLuongs.Sum(bl => bl.ThucLanh);
            lblTongLuong.Text = $"Tổng lương: {tongLuong:N0} VNĐ";

            string trangThai = bangLuongs.Count > 0 ? 
                (bangLuongs.All(bl => bl.TrangThai == "Dong") ? "Đã đóng" : 
                 bangLuongs.Any(bl => bl.TrangThai == "Mo") ? "Đang mở" : "Chưa chạy") : 
                "Chưa chạy";
            
            lblTrangThai.Text = $"Trạng thái: {trangThai}";
            lblTrangThai.ForeColor = trangThai == "Đã đóng" ? Color.LightGreen : 
                                   trangThai == "Đang mở" ? Color.Orange : Color.Gray;

            // Enable/disable buttons based on status
            btnChayLuong.Enabled = trangThai != "Đã đóng";
            btnDongLuong.Enabled = trangThai == "Đang mở";
            btnCapNhatPhuCap.Enabled = trangThai == "Đang mở";
        }

        private void ShowMessage(string message, string title, MessageBoxIcon icon)
        {
            MessageBox.Show(message, title, MessageBoxButtons.OK, icon);
        }

        // Event Handlers
        private void btnXemCong_Click(object sender, EventArgs e)
        {
            LoadAttendanceData();
            tabControl.SelectedIndex = 0; // Switch to attendance tab
        }

        private void btnChayLuong_Click(object sender, EventArgs e)
        {
            int thang = (int)cmbThang.SelectedItem;
            int nam = (int)cmbNam.SelectedItem;

            var result = MessageBox.Show(
                $"Bạn có chắc chắn muốn chạy bảng lương tháng {thang}/{nam}?\n\n" +
                $"Giờ chuẩn: {nudGioChuan.Value}\n" +
                $"Hệ số OT: {nudHeSoOT.Value}",
                "Xác nhận chạy bảng lương",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result == DialogResult.Yes)
            {
                try
                {
                    var payrollResult = _bangLuongRepository.ChayBangLuong(
                        nam, thang, nudGioChuan.Value, nudHeSoOT.Value);

                    if (payrollResult.Success)
                    {
                        ShowMessage(payrollResult.Message, "Thành công", MessageBoxIcon.Information);
                        LoadPayrollData();
                        tabControl.SelectedIndex = 1; // Switch to payroll tab
                    }
                    else
                    {
                        ShowMessage(payrollResult.Message, "Lỗi", MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    ShowMessage($"Lỗi chạy bảng lương: {ex.Message}", "Lỗi", MessageBoxIcon.Error);
                }
            }
        }

        private void btnDongLuong_Click(object sender, EventArgs e)
        {
            int thang = (int)cmbThang.SelectedItem;
            int nam = (int)cmbNam.SelectedItem;

            var result = MessageBox.Show(
                $"Bạn có chắc chắn muốn đóng bảng lương tháng {thang}/{nam}?\n\n" +
                "Sau khi đóng, không thể chỉnh sửa bảng lương này!",
                "Xác nhận đóng bảng lương",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            if (result == DialogResult.Yes)
            {
                try
                {
                    var closeResult = _bangLuongRepository.DongBangLuong(nam, thang);

                    if (closeResult.Success)
                    {
                        ShowMessage(closeResult.Message, "Thành công", MessageBoxIcon.Information);
                        LoadPayrollData();
                    }
                    else
                    {
                        ShowMessage(closeResult.Message, "Lỗi", MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    ShowMessage($"Lỗi đóng bảng lương: {ex.Message}", "Lỗi", MessageBoxIcon.Error);
                }
            }
        }

        private void btnCapNhatPhuCap_Click(object sender, EventArgs e)
        {
            var form = new CapNhatPhuCapForm((int)cmbNam.SelectedItem, (int)cmbThang.SelectedItem);
            if (form.ShowDialog() == DialogResult.OK)
            {
                LoadPayrollData();
            }
        }

        private void dgvBangLuong_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dgvBangLuong.SelectedRows.Count > 0)
            {
                var selectedBL = (BangLuong)dgvBangLuong.SelectedRows[0].DataBoundItem;
                if (selectedBL.TrangThai == "Mo")
                {
                    var form = new ChinhSuaLuongForm(selectedBL);
                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        LoadPayrollData();
                    }
                }
                else
                {
                    ShowMessage("Bảng lương đã đóng, không thể chỉnh sửa!", "Thông báo", MessageBoxIcon.Information);
                }
            }
        }
    }

    // Placeholder forms for payroll management
    public partial class CapNhatPhuCapForm : Form
    {
        public CapNhatPhuCapForm(int nam, int thang)
        {
            InitializeComponent();
            this.Text = $"Cập nhật phụ cấp - {thang}/{nam}";
            CreateControls();
        }

        private void CreateControls()
        {
            var lblTitle = new Label
            {
                Text = "Cập nhật phụ cấp hàng loạt - Chức năng đang phát triển",
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                Location = new Point(50, 50),
                AutoSize = true
            };

            var btnClose = new Button
            {
                Text = "Đóng",
                BackColor = Color.FromArgb(158, 158, 158),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Size = new Size(80, 35),
                Location = new Point(250, 300),
                DialogResult = DialogResult.Cancel
            };

            this.Controls.AddRange(new Control[] { lblTitle, btnClose });
        }
    }

    public partial class ChinhSuaLuongForm : Form
    {
        public ChinhSuaLuongForm(BangLuong bangLuong)
        {
            InitializeComponent();
            this.Text = $"Chỉnh sửa lương - {bangLuong.TenNhanVien}";
            CreateControls();
        }

        private void CreateControls()
        {
            var lblTitle = new Label
            {
                Text = "Chỉnh sửa lương cá nhân - Chức năng đang phát triển",
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                Location = new Point(50, 50),
                AutoSize = true
            };

            var btnClose = new Button
            {
                Text = "Đóng",
                BackColor = Color.FromArgb(158, 158, 158),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Size = new Size(80, 35),
                Location = new Point(200, 300),
                DialogResult = DialogResult.Cancel
            };

            this.Controls.AddRange(new Control[] { lblTitle, btnClose });
        }
    }
}
