using System;
using System.Drawing;
using System.Windows.Forms;
using VuToanThang_23110329.Models;
using VuToanThang_23110329.Repositories;

namespace VuToanThang_23110329.Forms
{
    public partial class ChinhSuaLuongForm : Form
    {
        private readonly BangLuong _bangLuong;
        private readonly BangLuongRepository _repository;
        private NumericUpDown nudPhuCap, nudKhauTru, nudThueBH;
        private Label lblThucLanh;

        public ChinhSuaLuongForm(BangLuong bangLuong)
        {
            InitializeComponent();
            _bangLuong = bangLuong;
            _repository = new BangLuongRepository();
            this.Text = $"Chỉnh sửa lương - {bangLuong.TenNhanVien}";
            this.Size = new Size(500, 400);
            this.BackColor = Color.FromArgb(50, 50, 50);
            CreateControls();
            LoadData();
        }

        private void CreateControls()
        {
            var lblTitle = new Label
            {
                Text = $"CHỈNH SỬA LƯƠNG - {_bangLuong.TenNhanVien}",
                ForeColor = Color.FromArgb(124, 77, 255),
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                Location = new Point(20, 20),
                AutoSize = true
            };

            // Employee info panel
            var pnlInfo = new Panel
            {
                BackColor = Color.FromArgb(60, 60, 60),
                BorderStyle = BorderStyle.FixedSingle,
                Size = new Size(460, 100),
                Location = new Point(20, 60)
            };

            var lblNhanVien = new Label { Text = $"Nhân viên: {_bangLuong.TenNhanVien}", ForeColor = Color.White, Location = new Point(20, 15), AutoSize = true };
            var lblChucDanh = new Label { Text = $"Chức danh: {_bangLuong.ChucDanh}", ForeColor = Color.White, Location = new Point(20, 35), AutoSize = true };
            var lblLuongCB = new Label { Text = $"Lương cơ bản: {_bangLuong.LuongCoBan:N0} VNĐ", ForeColor = Color.White, Location = new Point(20, 55), AutoSize = true };
            var lblGioCong = new Label { Text = $"Giờ công: {_bangLuong.TongGioCong:F2} | OT: {_bangLuong.GioOT:F2}", ForeColor = Color.White, Location = new Point(250, 55), AutoSize = true };

            pnlInfo.Controls.AddRange(new Control[] { lblNhanVien, lblChucDanh, lblLuongCB, lblGioCong });

            // Edit panel
            var pnlEdit = new Panel
            {
                BackColor = Color.FromArgb(60, 60, 60),
                BorderStyle = BorderStyle.FixedSingle,
                Size = new Size(460, 120),
                Location = new Point(20, 180)
            };

            var lblPhuCap = new Label { Text = "Phụ cấp:", ForeColor = Color.White, Location = new Point(20, 20), AutoSize = true };
            nudPhuCap = new NumericUpDown
            {
                Maximum = 10000000,
                DecimalPlaces = 0,
                Increment = 50000,
                BackColor = Color.FromArgb(70, 70, 70),
                ForeColor = Color.White,
                Size = new Size(120, 25),
                Location = new Point(100, 17)
            };
            nudPhuCap.ValueChanged += (s, e) => UpdateThucLanh();

            var lblKhauTru = new Label { Text = "Khấu trừ:", ForeColor = Color.White, Location = new Point(240, 20), AutoSize = true };
            nudKhauTru = new NumericUpDown
            {
                Maximum = 10000000,
                DecimalPlaces = 0,
                Increment = 50000,
                BackColor = Color.FromArgb(70, 70, 70),
                ForeColor = Color.White,
                Size = new Size(120, 25),
                Location = new Point(320, 17)
            };
            nudKhauTru.ValueChanged += (s, e) => UpdateThucLanh();

            var lblThueBH = new Label { Text = "Thuế BH:", ForeColor = Color.White, Location = new Point(20, 50), AutoSize = true };
            nudThueBH = new NumericUpDown
            {
                Maximum = 10000000,
                DecimalPlaces = 0,
                Increment = 10000,
                BackColor = Color.FromArgb(70, 70, 70),
                ForeColor = Color.White,
                Size = new Size(120, 25),
                Location = new Point(100, 47)
            };
            nudThueBH.ValueChanged += (s, e) => UpdateThucLanh();

            lblThucLanh = new Label
            {
                Text = "Thực lãnh: 0 VNĐ",
                ForeColor = Color.FromArgb(124, 77, 255),
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                Location = new Point(240, 50),
                AutoSize = true
            };

            pnlEdit.Controls.AddRange(new Control[] { lblPhuCap, nudPhuCap, lblKhauTru, nudKhauTru, lblThueBH, nudThueBH, lblThucLanh });

            // Buttons
            var btnSave = new Button
            {
                Text = "Lưu",
                BackColor = Color.FromArgb(124, 77, 255),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Size = new Size(100, 35),
                Location = new Point(280, 320),
                DialogResult = DialogResult.OK
            };
            btnSave.Click += BtnSave_Click;

            var btnCancel = new Button
            {
                Text = "Hủy",
                BackColor = Color.FromArgb(80, 80, 80),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Size = new Size(100, 35),
                Location = new Point(390, 320),
                DialogResult = DialogResult.Cancel
            };

            this.Controls.AddRange(new Control[] { lblTitle, pnlInfo, pnlEdit, btnSave, btnCancel });
        }

        private void LoadData()
        {
            try
            {
                nudPhuCap.Value = (decimal)_bangLuong.PhuCap;
                nudKhauTru.Value = (decimal)_bangLuong.KhauTru;
                nudThueBH.Value = (decimal)_bangLuong.ThueBH;
                UpdateThucLanh();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi load dữ liệu: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateThucLanh()
        {
            try
            {
                var luongCB = _bangLuong.LuongCoBan;
                var gioOT = _bangLuong.GioOT;
                var luongOT = gioOT * (luongCB / 160m) * 1.5m; // 1.5x overtime rate
                
                var thucLanh = luongCB + luongOT + nudPhuCap.Value - nudKhauTru.Value - nudThueBH.Value;
                lblThucLanh.Text = $"Thực lãnh: {thucLanh:N0} VNĐ";
            }
            catch (Exception ex)
            {
                lblThucLanh.Text = "Thực lãnh: Lỗi tính toán";
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                var result = _repository.UpdatePhuCapKhauTru(
                    _bangLuong.MaBangLuong,
                    nudPhuCap.Value,
                    nudKhauTru.Value,
                    nudThueBH.Value
                );

                if (result.Success)
                {
                    MessageBox.Show("Cập nhật thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show($"Lỗi cập nhật: {result.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
