using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using VuToanThang_23110329.Repositories;

namespace VuToanThang_23110329.Forms
{
    public partial class CapNhatPhuCapForm : Form
    {
        private readonly int _nam, _thang;
        private readonly BangLuongRepository _repository;
        private DataGridView dgvNhanVien;
        private NumericUpDown nudPhuCap, nudKhauTru, nudThueBH;
        private Button btnApplyAll, btnApplySelected;

        public CapNhatPhuCapForm(int nam, int thang)
        {
            InitializeComponent();
            _nam = nam;
            _thang = thang;
            _repository = new BangLuongRepository();
            this.Text = $"Cập nhật phụ cấp - {thang}/{nam}";
            this.Size = new Size(800, 600);
            this.BackColor = Color.FromArgb(50, 50, 50);
            CreateControls();
            LoadData();
        }

        private void CreateControls()
        {
            var lblTitle = new Label
            {
                Text = $"CẬP NHẬT PHỤ CẤP/KHẤU TRỪ - {_thang}/{_nam}",
                ForeColor = Color.FromArgb(124, 77, 255),
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                Location = new Point(20, 20),
                AutoSize = true
            };

            // Input panel
            var pnlInput = new Panel
            {
                BackColor = Color.FromArgb(60, 60, 60),
                BorderStyle = BorderStyle.FixedSingle,
                Size = new Size(760, 80),
                Location = new Point(20, 60)
            };

            var lblPhuCap = new Label { Text = "Phụ cấp:", ForeColor = Color.White, Location = new Point(20, 20), AutoSize = true };
            nudPhuCap = new NumericUpDown
            {
                Maximum = 10000000,
                DecimalPlaces = 0,
                Increment = 50000,
                BackColor = Color.FromArgb(70, 70, 70),
                ForeColor = Color.White,
                Size = new Size(100, 25),
                Location = new Point(20, 40)
            };

            var lblKhauTru = new Label { Text = "Khấu trừ:", ForeColor = Color.White, Location = new Point(150, 20), AutoSize = true };
            nudKhauTru = new NumericUpDown
            {
                Maximum = 10000000,
                DecimalPlaces = 0,
                Increment = 50000,
                BackColor = Color.FromArgb(70, 70, 70),
                ForeColor = Color.White,
                Size = new Size(100, 25),
                Location = new Point(150, 40)
            };

            var lblThueBH = new Label { Text = "Thuế BH:", ForeColor = Color.White, Location = new Point(280, 20), AutoSize = true };
            nudThueBH = new NumericUpDown
            {
                Maximum = 10000000,
                DecimalPlaces = 0,
                Increment = 10000,
                BackColor = Color.FromArgb(70, 70, 70),
                ForeColor = Color.White,
                Size = new Size(100, 25),
                Location = new Point(280, 40)
            };

            btnApplyAll = new Button
            {
                Text = "Áp dụng tất cả",
                BackColor = Color.FromArgb(124, 77, 255),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Size = new Size(120, 30),
                Location = new Point(420, 25)
            };
            btnApplyAll.Click += BtnApplyAll_Click;

            btnApplySelected = new Button
            {
                Text = "Áp dụng đã chọn",
                BackColor = Color.FromArgb(80, 160, 80),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Size = new Size(120, 30),
                Location = new Point(550, 25)
            };
            btnApplySelected.Click += BtnApplySelected_Click;

            pnlInput.Controls.AddRange(new Control[] { lblPhuCap, nudPhuCap, lblKhauTru, nudKhauTru, lblThueBH, nudThueBH, btnApplyAll, btnApplySelected });

            // DataGridView
            dgvNhanVien = new DataGridView
            {
                BackgroundColor = Color.FromArgb(60, 60, 60),
                ForeColor = Color.White,
                GridColor = Color.FromArgb(80, 80, 80),
                BorderStyle = BorderStyle.None,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = true,
                Size = new Size(760, 400),
                Location = new Point(20, 160),
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            };

            // Close button
            var btnClose = new Button
            {
                Text = "Đóng",
                BackColor = Color.FromArgb(80, 80, 80),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Size = new Size(100, 35),
                Location = new Point(680, 570),
                DialogResult = DialogResult.OK
            };

            this.Controls.AddRange(new Control[] { lblTitle, pnlInput, dgvNhanVien, btnClose });
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
                            (double)nudPhuCap.Value,
                            (double)nudKhauTru.Value,
                            (double)nudThueBH.Value
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
