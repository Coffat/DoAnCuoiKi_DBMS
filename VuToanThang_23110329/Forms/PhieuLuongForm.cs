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
    public partial class PhieuLuongForm : Form
    {
        private readonly BangLuongRepository _bangLuongRepository;
        private readonly ChamCongRepository _chamCongRepository;

        // UI Controls
        private ComboBox cmbThang, cmbNam;
        private Button btnXemPhieu, btnInPhieu, btnLamMoi;
        private Panel pnlPhieuLuong, pnlFilter;
        private Label lblThongTinNV, lblThongTinLuong, lblTinhToan, lblKetQua;

        public PhieuLuongForm()
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
                Text = "PHIẾU LƯƠNG CỦA TÔI",
                Font = new Font("Segoe UI", 20, FontStyle.Bold),
                ForeColor = Color.FromArgb(124, 77, 255),
                AutoSize = true
            };

            // Filter Panel
            pnlFilter = new Panel
            {
                BackColor = Color.FromArgb(60, 60, 60),
                BorderStyle = BorderStyle.FixedSingle,
                Size = new Size(840, 80)
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

            btnXemPhieu = CreateButton("Xem phiếu lương", Color.FromArgb(33, 150, 243));
            btnInPhieu = CreateButton("In phiếu lương", Color.FromArgb(46, 125, 50));
            btnLamMoi = CreateButton("Làm mới", Color.FromArgb(96, 125, 139));

            pnlFilter.Controls.AddRange(new Control[] {
                lblThang, cmbThang, lblNam, cmbNam, btnXemPhieu, btnInPhieu, btnLamMoi
            });

            // Payslip Panel
            pnlPhieuLuong = new Panel
            {
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                Size = new Size(840, 600),
                AutoScroll = true
            };

            CreatePayslipContent();

            this.Controls.AddRange(new Control[] { lblTitle, pnlFilter, pnlPhieuLuong });
        }

        private void CreatePayslipContent()
        {
            // Header
            var lblHeader = new Label
            {
                Text = "PHIẾU LƯƠNG NHÂN VIÊN",
                Font = new Font("Arial", 18, FontStyle.Bold),
                ForeColor = Color.Black,
                Location = new Point(250, 20),
                AutoSize = true
            };

            var lblCompany = new Label
            {
                Text = "SIÊU THỊ MINI - HỆ THỐNG QUẢN LÝ NHÂN SỰ",
                Font = new Font("Arial", 12, FontStyle.Bold),
                ForeColor = Color.Black,
                Location = new Point(200, 50),
                AutoSize = true
            };

            // Employee Info Section
            lblThongTinNV = new Label
            {
                Text = "=== THÔNG TIN NHÂN VIÊN ===\n\n" +
                       "Họ tên: \n" +
                       "Mã nhân viên: \n" +
                       "Chức danh: \n" +
                       "Phòng ban: \n" +
                       "Kỳ lương: ",
                Font = new Font("Arial", 11),
                ForeColor = Color.Black,
                Location = new Point(30, 100),
                Size = new Size(380, 120),
                AutoSize = false
            };

            // Salary Info Section
            lblThongTinLuong = new Label
            {
                Text = "=== THÔNG TIN CÔNG & LƯƠNG ===\n\n" +
                       "Lương cơ bản: \n" +
                       "Tổng giờ công: \n" +
                       "Giờ làm thêm: \n" +
                       "Hệ số OT: ",
                Font = new Font("Arial", 11),
                ForeColor = Color.Black,
                Location = new Point(430, 100),
                Size = new Size(380, 120),
                AutoSize = false
            };

            // Calculation Section
            lblTinhToan = new Label
            {
                Text = "=== CHI TIẾT TÍNH TOÁN ===\n\n" +
                       "Lương cơ bản: \n" +
                       "Tiền làm thêm: \n" +
                       "Phụ cấp khác: \n" +
                       "Tổng thu nhập: \n\n" +
                       "Khấu trừ: \n" +
                       "Thuế & Bảo hiểm: \n" +
                       "Tổng khấu trừ: ",
                Font = new Font("Arial", 11),
                ForeColor = Color.Black,
                Location = new Point(30, 250),
                Size = new Size(380, 200),
                AutoSize = false
            };

            // Result Section
            lblKetQua = new Label
            {
                Text = "=== THỰC LÃNH ===\n\n",
                Font = new Font("Arial", 14, FontStyle.Bold),
                ForeColor = Color.Red,
                Location = new Point(430, 250),
                Size = new Size(380, 200),
                AutoSize = false
            };

            // Footer
            var lblFooter = new Label
            {
                Text = "Ghi chú: Phiếu lương này được tạo tự động từ hệ thống.\n" +
                       "Mọi thắc mắc xin liên hệ phòng Nhân sự.",
                Font = new Font("Arial", 9, FontStyle.Italic),
                ForeColor = Color.Gray,
                Location = new Point(30, 480),
                Size = new Size(780, 40),
                AutoSize = false
            };

            var lblSignature = new Label
            {
                Text = "Ngày in: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm") + "\n\n" +
                       "Người lập                    Kế toán                    Giám đốc\n\n\n" +
                       "___________              ___________              ___________",
                Font = new Font("Arial", 10),
                ForeColor = Color.Black,
                Location = new Point(200, 530),
                Size = new Size(400, 100),
                AutoSize = false,
                TextAlign = ContentAlignment.TopCenter
            };

            pnlPhieuLuong.Controls.AddRange(new Control[] {
                lblHeader, lblCompany, lblThongTinNV, lblThongTinLuong, 
                lblTinhToan, lblKetQua, lblFooter, lblSignature
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
            pnlFilter.Controls[0].Location = new Point(20, 15); // lblThang
            cmbThang.Location = new Point(20, 35);

            pnlFilter.Controls[2].Location = new Point(120, 15); // lblNam
            cmbNam.Location = new Point(120, 35);

            btnXemPhieu.Location = new Point(220, 25);
            btnInPhieu.Location = new Point(350, 25);
            btnLamMoi.Location = new Point(480, 25);

            // Payslip Panel
            pnlPhieuLuong.Location = new Point(20, 170);
        }

        private void SetupEventHandlers()
        {
            btnXemPhieu.Click += btnXemPhieu_Click;
            btnInPhieu.Click += btnInPhieu_Click;
            btnLamMoi.Click += btnLamMoi_Click;
        }

        private void InitializeForm()
        {
            if (!VuToanThang_23110329.Data.CurrentUser.CurrentEmployeeId.HasValue)
            {
                ShowMessage("Không xác định được nhân viên hiện tại!", "Lỗi", MessageBoxIcon.Error);
                this.Close();
                return;
            }

            LoadPayslip();
        }

        private void LoadPayslip()
        {
            try
            {
                if (!VuToanThang_23110329.Data.CurrentUser.CurrentEmployeeId.HasValue)
                {
                    ShowMessage("Không xác định được nhân viên hiện tại!", "Lỗi", MessageBoxIcon.Error);
                    return;
                }

                int thang = (int)cmbThang.SelectedItem;
                int nam = (int)cmbNam.SelectedItem;

                var bangLuong = _bangLuongRepository.GetByEmployeeAndPeriod(
                    VuToanThang_23110329.Data.CurrentUser.CurrentEmployeeId.Value, nam, thang);

                if (bangLuong == null)
                {
                    ShowEmptyPayslip();
                    return;
                }

                // Load attendance summary
                var congThang = _chamCongRepository.GetCongThang(nam, thang)
                    .FirstOrDefault(c => c.MaNV == VuToanThang_23110329.Data.CurrentUser.CurrentEmployeeId.Value);

                DisplayPayslip(bangLuong, congThang);
            }
            catch (Exception ex)
            {
                ShowMessage($"Lỗi tải phiếu lương: {ex.Message}", "Lỗi", MessageBoxIcon.Error);
            }
        }

        private void ShowEmptyPayslip()
        {
            lblThongTinNV.Text = "=== THÔNG TIN NHÂN VIÊN ===\n\n" +
                                "Không có dữ liệu lương cho kỳ này.\n" +
                                "Vui lòng liên hệ phòng Kế toán để biết thêm chi tiết.";

            lblThongTinLuong.Text = "";
            lblTinhToan.Text = "";
            lblKetQua.Text = "";

            btnInPhieu.Enabled = false;
        }

        private void DisplayPayslip(BangLuong bl, vw_CongThang congThang)
        {
            // Employee Info
            lblThongTinNV.Text = $"=== THÔNG TIN NHÂN VIÊN ===\n\n" +
                                $"Họ tên: {bl.TenNhanVien}\n" +
                                $"Mã nhân viên: {bl.MaNV}\n" +
                                $"Chức danh: {bl.ChucDanh}\n" +
                                $"Phòng ban: {bl.PhongBan}\n" +
                                $"Kỳ lương: {bl.Thang:00}/{bl.Nam}";

            // Work & Salary Info
            lblThongTinLuong.Text = $"=== THÔNG TIN CÔNG & LƯƠNG ===\n\n" +
                                   $"Lương cơ bản: {bl.LuongCoBan:N0} VNĐ\n" +
                                   $"Tổng giờ công: {bl.TongGioCong:F2} giờ\n" +
                                   $"Giờ làm thêm: {bl.GioOT:F2} giờ\n" +
                                   $"Hệ số OT: 1.5";

            // Calculation Details
            decimal luongOT = bl.GioOT * (bl.LuongCoBan / 208) * 1.5m;
            decimal tongThuNhap = bl.LuongCoBan + luongOT + bl.PhuCap;
            decimal tongKhauTru = bl.KhauTru + bl.ThueBH;

            lblTinhToan.Text = $"=== CHI TIẾT TÍNH TOÁN ===\n\n" +
                              $"Lương cơ bản: {bl.LuongCoBan:N0} VNĐ\n" +
                              $"Tiền làm thêm: {luongOT:N0} VNĐ\n" +
                              $"Phụ cấp khác: {bl.PhuCap:N0} VNĐ\n" +
                              $"Tổng thu nhập: {tongThuNhap:N0} VNĐ\n\n" +
                              $"Khấu trừ: {bl.KhauTru:N0} VNĐ\n" +
                              $"Thuế & Bảo hiểm: {bl.ThueBH:N0} VNĐ\n" +
                              $"Tổng khấu trừ: {tongKhauTru:N0} VNĐ";

            // Final Result
            lblKetQua.Text = $"=== THỰC LÃNH ===\n\n" +
                            $"{bl.ThucLanh:N0} VNĐ\n\n" +
                            $"Bằng chữ: {NumberToWords(bl.ThucLanh)} đồng\n\n" +
                            $"Trạng thái: {(bl.TrangThai == "Dong" ? "Đã chốt" : "Tạm tính")}";

            // Add attendance details if available
            if (congThang != null)
            {
                lblThongTinLuong.Text += $"\n\nCông tháng: {congThang.TongGioCong:F2} giờ\n" +
                                        $"Đi trễ: {congThang.TongPhutDiTre} phút\n" +
                                        $"Về sớm: {congThang.TongPhutVeSom} phút";
            }

            btnInPhieu.Enabled = true;
        }

        private string NumberToWords(decimal number)
        {
            // Simple number to words conversion (Vietnamese)
            // This is a simplified version - in production, use a proper library
            if (number == 0) return "Không";
            
            long intPart = (long)number;
            if (intPart < 1000) return intPart.ToString();
            if (intPart < 1000000) return $"{intPart / 1000} nghìn {intPart % 1000}";
            if (intPart < 1000000000) return $"{intPart / 1000000} triệu {(intPart % 1000000) / 1000} nghìn";
            
            return $"{intPart / 1000000000} tỷ {(intPart % 1000000000) / 1000000} triệu";
        }

        private void ShowMessage(string message, string title, MessageBoxIcon icon)
        {
            MessageBox.Show(message, title, MessageBoxButtons.OK, icon);
        }

        // Event Handlers
        private void btnXemPhieu_Click(object sender, EventArgs e)
        {
            LoadPayslip();
        }

        private void btnInPhieu_Click(object sender, EventArgs e)
        {
            try
            {
                // Create a print-friendly version
                var printForm = new PrintPayslipForm(pnlPhieuLuong);
                printForm.ShowDialog();
            }
            catch (Exception ex)
            {
                ShowMessage($"Lỗi in phiếu lương: {ex.Message}", "Lỗi", MessageBoxIcon.Error);
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            LoadPayslip();
        }
    }

    // Print form for payslip
    public partial class PrintPayslipForm : Form
    {
        private Panel payslipPanel;

        public PrintPayslipForm(Panel originalPanel)
        {
            InitializeComponent();
            ClonePayslipPanel(originalPanel);
        }

            var btnPrint = new Button
            {
                Text = "In",
                BackColor = Color.FromArgb(46, 125, 50),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Size = new Size(80, 35),
                Location = new Point(20, 20)
            };

            var btnClose = new Button
            {
                Text = "Đóng",
                BackColor = Color.FromArgb(158, 158, 158),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Size = new Size(80, 35),
                Location = new Point(110, 20)
            };

            btnPrint.Click += (s, e) => {
                // Placeholder for actual printing
                MessageBox.Show("Chức năng in đang được phát triển!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            };

            btnClose.Click += (s, e) => this.Close();

            this.Controls.AddRange(new Control[] { btnPrint, btnClose });
        }

        private void ClonePayslipPanel(Panel original)
        {
            payslipPanel = new Panel
            {
                Size = new Size(800, 600),
                Location = new Point(50, 70),
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle
            };

            // Clone all controls from original panel
            foreach (Control ctrl in original.Controls)
            {
                if (ctrl is Label lbl)
                {
                    var newLabel = new Label
                    {
                        Text = lbl.Text,
                        Font = lbl.Font,
                        ForeColor = lbl.ForeColor,
                        Location = lbl.Location,
                        Size = lbl.Size,
                        AutoSize = lbl.AutoSize,
                        TextAlign = lbl.TextAlign
                    };
                    payslipPanel.Controls.Add(newLabel);
                }
            }

            this.Controls.Add(payslipPanel);
        }
    }
}
