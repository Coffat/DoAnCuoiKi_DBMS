using System;
using System.Linq;
using System.Windows.Forms;
using VuToanThang_23110329.Models;
using VuToanThang_23110329.Repositories;

namespace VuToanThang_23110329.Forms
{
    public partial class ChinhSuaLuongForm : Form
    {
        private readonly BangLuong _bangLuong;
        private readonly BangLuongRepository _repository;

        public ChinhSuaLuongForm(BangLuong bangLuong)
        {
            InitializeComponent();
            _bangLuong = bangLuong;
            _repository = new BangLuongRepository();
            this.Text = $"Chỉnh sửa lương - {bangLuong.TenNhanVien}";
            InitializeForm();
        }

        private void InitializeForm()
        {
            // Update employee info labels
            UpdateEmployeeInfo();

            // Setup event handlers
            nudPhuCap.ValueChanged += (s, e) => UpdateThucLanh();
            nudKhauTru.ValueChanged += (s, e) => UpdateThucLanh();
            nudThueBH.ValueChanged += (s, e) => UpdateThucLanh();
            btnSave.Click += BtnSave_Click;

            LoadData();
        }

        private void UpdateEmployeeInfo()
        {
            // Update title
            this.Controls.OfType<Label>().FirstOrDefault()?.Dispose();
            var lblTitle = new Label
            {
                Text = $"CHỈNH SỬA LƯƠNG - {_bangLuong.TenNhanVien}",
                ForeColor = System.Drawing.Color.FromArgb(124, 77, 255),
                Font = new System.Drawing.Font("Segoe UI", 16, System.Drawing.FontStyle.Bold),
                Location = new System.Drawing.Point(20, 20),
                AutoSize = true
            };
            this.Controls.Add(lblTitle);
            lblTitle.BringToFront();

            // Update employee info in panel
            var pnlInfo = this.Controls.OfType<Panel>().FirstOrDefault();
            if (pnlInfo != null)
            {
                var labels = pnlInfo.Controls.OfType<Label>().ToArray();
                if (labels.Length >= 4)
                {
                    labels[0].Text = $"Nhân viên: {_bangLuong.TenNhanVien}";
                    labels[1].Text = $"Chức danh: {_bangLuong.ChucDanh}";
                    labels[2].Text = $"Lương cơ bản: {_bangLuong.LuongCoBan:N0} VNĐ";
                    labels[3].Text = $"Giờ công: {_bangLuong.TongGioCong:F2} | OT: {_bangLuong.GioOT:F2}";
                }
            }
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
