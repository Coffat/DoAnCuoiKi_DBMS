using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using VuToanThang_23110329.Repositories;

namespace VuToanThang_23110329.Forms
{
    public partial class CapNhatPhuCapForm : Form
    {
        private readonly int _nam, _thang;
        private readonly BangLuongRepository _repository;

        public CapNhatPhuCapForm(int nam, int thang)
        {
            InitializeComponent();
            _nam = nam;
            _thang = thang;
            _repository = new BangLuongRepository();
            this.Text = $"Cập nhật phụ cấp - {thang}/{nam}";
            InitializeForm();
        }

        private void InitializeForm()
        {
            // Update title with period
            this.Controls.OfType<Label>().FirstOrDefault()?.Dispose();
            var lblTitle = new Label
            {
                Text = $"CẬP NHẬT PHỤ CẤP/KHẤU TRỪ - {_thang}/{_nam}",
                ForeColor = System.Drawing.Color.FromArgb(124, 77, 255),
                Font = new System.Drawing.Font("Segoe UI", 16, System.Drawing.FontStyle.Bold),
                Location = new System.Drawing.Point(20, 20),
                AutoSize = true
            };
            this.Controls.Add(lblTitle);
            lblTitle.BringToFront();

            // Setup event handlers
            btnApplyAll.Click += BtnApplyAll_Click;
            btnApplySelected.Click += BtnApplySelected_Click;

            LoadData();
        }

        private void LoadData()
        {
            try
            {
                var result = _repository.GetBangLuongByThangNam(_thang, _nam);
                if (result.Success && result.Data != null)
                {
                    var dataTable = result.Data as DataTable;
                    dgvNhanVien.DataSource = dataTable;

                    // Configure columns
                    if (dgvNhanVien.Columns.Count > 0)
                    {
                        dgvNhanVien.Columns["MaBangLuong"].Visible = false;
                        dgvNhanVien.Columns["MaNV"].HeaderText = "Mã NV";
                        dgvNhanVien.Columns["TenNhanVien"].HeaderText = "Tên nhân viên";
                        dgvNhanVien.Columns["ChucDanh"].HeaderText = "Chức danh";
                        dgvNhanVien.Columns["LuongCoBan"].HeaderText = "Lương CB";
                        dgvNhanVien.Columns["PhuCap"].HeaderText = "Phụ cấp";
                        dgvNhanVien.Columns["KhauTru"].HeaderText = "Khấu trừ";
                        dgvNhanVien.Columns["ThueBH"].HeaderText = "Thuế BH";
                        dgvNhanVien.Columns["ThucLanh"].HeaderText = "Thực lãnh";

                        // Make editable columns
                        dgvNhanVien.Columns["PhuCap"].ReadOnly = false;
                        dgvNhanVien.Columns["KhauTru"].ReadOnly = false;
                        dgvNhanVien.Columns["ThueBH"].ReadOnly = false;
                    }
                }
                else
                {
                    MessageBox.Show($"Không thể tải dữ liệu: {result.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi load dữ liệu: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnApplyAll_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Áp dụng cho tất cả nhân viên?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                ApplyToRows(dgvNhanVien.Rows.Cast<DataGridViewRow>().ToArray());
            }
        }

        private void BtnApplySelected_Click(object sender, EventArgs e)
        {
            var selectedRows = dgvNhanVien.SelectedRows.Cast<DataGridViewRow>().ToArray();
            if (selectedRows.Length == 0)
            {
                MessageBox.Show("Vui lòng chọn ít nhất một nhân viên!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show($"Áp dụng cho {selectedRows.Length} nhân viên được chọn?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                ApplyToRows(selectedRows);
            }
        }

        private void ApplyToRows(DataGridViewRow[] rows)
        {
            try
            {
                int successCount = 0;
                int errorCount = 0;

                foreach (var row in rows)
                {
                    if (row.Cells["MaBangLuong"].Value != null)
                    {
                        int maBangLuong = Convert.ToInt32(row.Cells["MaBangLuong"].Value);
                        var result = _repository.UpdatePhuCapKhauTru(
                            maBangLuong,
                            nudPhuCap.Value,
                            nudKhauTru.Value,
                            nudThueBH.Value
                        );

                        if (result.Success)
                        {
                            successCount++;
                            // Update display values
                            row.Cells["PhuCap"].Value = nudPhuCap.Value;
                            row.Cells["KhauTru"].Value = nudKhauTru.Value;
                            row.Cells["ThueBH"].Value = nudThueBH.Value;
                        }
                        else
                        {
                            errorCount++;
                        }
                    }
                }

                MessageBox.Show($"Cập nhật hoàn tất!\nThành công: {successCount}\nLỗi: {errorCount}", 
                    "Kết quả", MessageBoxButtons.OK, MessageBoxIcon.Information);

                if (successCount > 0)
                {
                    LoadData(); // Refresh data
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi cập nhật: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
