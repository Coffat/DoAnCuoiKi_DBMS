using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using VuToanThang_23110329;

namespace VuToanThang_23110329.Forms
{
    public partial class frmLichTuan : Form
    {
        private string _connectionString;
        private DateTime _currentWeekStart;
        private int _selectedMaNV = 0;

        public frmLichTuan()
        {
            InitializeComponent();
            _connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["HrDb"]?.ConnectionString ?? "";
        }

        private void frmLichTuan_Load(object sender, EventArgs e)
        {
            SetupDataGridView();      // ✅ Setup columns TRƯỚC
            InitializeWeek(DateTime.Today);
            LoadNhanVien();
            LoadWeekSchedule();       // ✅ Load data SAU khi có columns
        }

        private void InitializeWeek(DateTime anyDate)
        {
            // Tìm thứ Hai của tuần
            int daysFromMonday = ((int)anyDate.DayOfWeek + 6) % 7;
            _currentWeekStart = anyDate.Date.AddDays(-daysFromMonday);
            
            lblWeekInfo.Text = $"Tuần: {_currentWeekStart:dd/MM/yyyy} - {_currentWeekStart.AddDays(6):dd/MM/yyyy}";
        }

        private void LoadNhanVien()
        {
            cboNhanVien.Items.Clear();
            cboNhanVien.DisplayMember = "Display";
            cboNhanVien.ValueMember = "MaNV";

            try
            {
                using (var conn = new SqlConnection(_connectionString))
                using (var cmd = new SqlCommand("SELECT MaNV, HoTen FROM dbo.vw_NhanVien_Full WHERE TrangThai = N'DangLam' ORDER BY HoTen", conn))
                {
                    conn.Open();
                    var dt = new DataTable();
                    dt.Columns.Add("MaNV", typeof(int));
                    dt.Columns.Add("Display", typeof(string));

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int maNV = Convert.ToInt32(reader["MaNV"]);
                            string hoTen = reader["HoTen"].ToString();
                            dt.Rows.Add(maNV, $"{maNV} - {hoTen}");
                        }
                    }

                    cboNhanVien.DataSource = dt;
                    if (cboNhanVien.Items.Count > 0)
                        cboNhanVien.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải danh sách nhân viên: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SetupDataGridView()
        {
            dgvLichTuan.Rows.Clear();
            dgvLichTuan.Columns.Clear();
            dgvLichTuan.AutoGenerateColumns = false;
            dgvLichTuan.AllowUserToAddRows = false;

            // Cột ngày
            var colNgay = new DataGridViewTextBoxColumn
            {
                Name = "colNgay",
                HeaderText = "Ngày",
                Width = 100,
                ReadOnly = true
            };
            dgvLichTuan.Columns.Add(colNgay);

            // Cột thứ
            var colThu = new DataGridViewTextBoxColumn
            {
                Name = "colThu",
                HeaderText = "Thứ",
                Width = 80,
                ReadOnly = true
            };
            dgvLichTuan.Columns.Add(colThu);

            // Cột ca làm (ComboBox)
            var colCa = new DataGridViewComboBoxColumn
            {
                Name = "colCa",
                HeaderText = "Ca làm",
                Width = 200,
                DisplayMember = "Display",
                ValueMember = "MaCa"
            };
            dgvLichTuan.Columns.Add(colCa);

            // Cột trạng thái
            var colTrangThai = new DataGridViewTextBoxColumn
            {
                Name = "colTrangThai",
                HeaderText = "Trạng thái",
                Width = 100,
                ReadOnly = true
            };
            dgvLichTuan.Columns.Add(colTrangThai);

            // Cột ghi chú
            var colGhiChu = new DataGridViewTextBoxColumn
            {
                Name = "colGhiChu",
                HeaderText = "Ghi chú",
                Width = 200,
                ReadOnly = false
            };
            dgvLichTuan.Columns.Add(colGhiChu);

            // Cột ẩn: MaLich
            var colMaLich = new DataGridViewTextBoxColumn
            {
                Name = "colMaLich",
                HeaderText = "MaLich",
                Visible = false
            };
            dgvLichTuan.Columns.Add(colMaLich);

            dgvLichTuan.DefaultCellStyle.BackColor = Color.White;
            dgvLichTuan.DefaultCellStyle.ForeColor = Color.Black;
            dgvLichTuan.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(240, 240, 240);
        }

        private void LoadWeekSchedule()
        {
            if (cboNhanVien.SelectedValue == null) return;

            _selectedMaNV = Convert.ToInt32(cboNhanVien.SelectedValue);
            dgvLichTuan.Rows.Clear();

            // Load danh sách ca cho ComboBox
            var dtCaLam = LoadCaLam();

            try
            {
                using (var conn = new SqlConnection(_connectionString))
                using (var cmd = new SqlCommand("SELECT * FROM dbo.tvf_LichTheoTuan(@MaNV, @NgayBatDau) ORDER BY Ngay", conn))
                {
                    cmd.Parameters.AddWithValue("@MaNV", _selectedMaNV);
                    cmd.Parameters.AddWithValue("@NgayBatDau", _currentWeekStart);

                    conn.Open();
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            DateTime ngay = reader.GetDateTime(0);
                            int? maLich = reader.IsDBNull(2) ? (int?)null : reader.GetInt32(2);
                            int? maCa = reader.IsDBNull(3) ? (int?)null : reader.GetInt32(3);
                            string trangThai = reader.IsDBNull(4) ? "" : reader[4].ToString();
                            string ghiChu = reader.IsDBNull(5) ? "" : reader[5].ToString();

                            int rowIndex = dgvLichTuan.Rows.Add();
                            var row = dgvLichTuan.Rows[rowIndex];

                            row.Cells["colNgay"].Value = ngay.ToString("dd/MM/yyyy");
                            row.Cells["colThu"].Value = GetDayOfWeekName(ngay.DayOfWeek);
                            
                            // Set ComboBox data source
                            var comboCell = (DataGridViewComboBoxCell)row.Cells["colCa"];
                            comboCell.DataSource = dtCaLam.Copy();
                            comboCell.Value = maCa;

                            row.Cells["colTrangThai"].Value = trangThai;
                            row.Cells["colGhiChu"].Value = ghiChu;
                            row.Cells["colMaLich"].Value = maLich;

                            // Màu sắc theo trạng thái
                            if (trangThai == "Khoa")
                            {
                                row.DefaultCellStyle.BackColor = Color.FromArgb(255, 230, 230);
                                row.ReadOnly = true;
                            }
                            else if (trangThai == "Huy")
                            {
                                row.DefaultCellStyle.BackColor = Color.FromArgb(220, 220, 220);
                            }

                            // Màu cho cuối tuần
                            if (ngay.DayOfWeek == DayOfWeek.Saturday || ngay.DayOfWeek == DayOfWeek.Sunday)
                            {
                                if (trangThai != "Khoa")
                                    row.DefaultCellStyle.BackColor = Color.FromArgb(230, 240, 255);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải lịch tuần: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private DataTable LoadCaLam()
        {
            var dt = new DataTable();
            dt.Columns.Add("MaCa", typeof(int));
            dt.Columns.Add("Display", typeof(string));

            // Thêm dòng trống
            dt.Rows.Add(0, "-- Không có ca --");

            try
            {
                using (var conn = new SqlConnection(_connectionString))
                using (var cmd = new SqlCommand("sp_CaLam_GetAll", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    conn.Open();
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int maCa = Convert.ToInt32(reader["MaCa"]);
                            string tenCa = reader["TenCa"].ToString();
                            TimeSpan gioBatDau = (TimeSpan)reader["GioBatDau"];
                            TimeSpan gioKetThuc = (TimeSpan)reader["GioKetThuc"];
                            dt.Rows.Add(maCa, $"{tenCa} ({gioBatDau:hh\\:mm}-{gioKetThuc:hh\\:mm})");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải danh sách ca: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return dt;
        }

        private string GetDayOfWeekName(DayOfWeek day)
        {
            switch (day)
            {
                case DayOfWeek.Monday: return "Thứ 2";
                case DayOfWeek.Tuesday: return "Thứ 3";
                case DayOfWeek.Wednesday: return "Thứ 4";
                case DayOfWeek.Thursday: return "Thứ 5";
                case DayOfWeek.Friday: return "Thứ 6";
                case DayOfWeek.Saturday: return "Thứ 7";
                case DayOfWeek.Sunday: return "CN";
                default: return "";
            }
        }

        private void btnPrevWeek_Click(object sender, EventArgs e)
        {
            InitializeWeek(_currentWeekStart.AddDays(-7));
            LoadWeekSchedule();
        }

        private void btnNextWeek_Click(object sender, EventArgs e)
        {
            InitializeWeek(_currentWeekStart.AddDays(7));
            LoadWeekSchedule();
        }

        private void cboNhanVien_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadWeekSchedule();
        }

        private void btnSaoChepTuan_Click(object sender, EventArgs e)
        {
            if (_selectedMaNV == 0)
            {
                MessageBox.Show("Vui lòng chọn nhân viên.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var result = MessageBox.Show(
                "Sao chép lịch từ tuần trước sang tuần này?\n\n" +
                "- Nếu chọn 'Yes': Sẽ ghi đè lịch hiện tại\n" +
                "- Nếu chọn 'No': Chỉ thêm vào ngày chưa có lịch",
                "Xác nhận",
                MessageBoxButtons.YesNoCancel,
                MessageBoxIcon.Question);

            if (result == DialogResult.Cancel) return;

            bool overwrite = (result == DialogResult.Yes);
            DateTime prevWeekStart = _currentWeekStart.AddDays(-7);

            try
            {
                using (var conn = new SqlConnection(_connectionString))
                using (var cmd = new SqlCommand("dbo.sp_LichPhanCa_CloneWeek", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@NgayBatDauFrom", prevWeekStart);
                    cmd.Parameters.AddWithValue("@NgayBatDauTo", _currentWeekStart);
                    cmd.Parameters.AddWithValue("@Overwrite", overwrite);

                    conn.Open();
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            int soLich = reader.GetInt32(0);
                            MessageBox.Show($"Đã sao chép {soLich} lịch từ tuần trước.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }

                LoadWeekSchedule();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi sao chép tuần: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnKhoaTuan_Click(object sender, EventArgs e)
        {
            if (_selectedMaNV == 0)
            {
                MessageBox.Show("Vui lòng chọn nhân viên.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var result = MessageBox.Show(
                $"Khóa lịch tuần này cho nhân viên {cboNhanVien.Text}?\n\n" +
                "Sau khi khóa, lịch sẽ không thể chỉnh sửa.",
                "Xác nhận khóa tuần",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (result != DialogResult.Yes) return;

            try
            {
                using (var conn = new SqlConnection(_connectionString))
                using (var cmd = new SqlCommand("dbo.sp_LichPhanCa_KhoaTuan", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@MaNV", _selectedMaNV);
                    cmd.Parameters.AddWithValue("@NgayBatDau", _currentWeekStart);

                    conn.Open();
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            int soLich = reader.GetInt32(0);
                            MessageBox.Show($"Đã khóa {soLich} lịch trong tuần.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }

                LoadWeekSchedule();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khóa tuần: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnMoKhoaTuan_Click(object sender, EventArgs e)
        {
            if (_selectedMaNV == 0)
            {
                MessageBox.Show("Vui lòng chọn nhân viên.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Kiểm tra quyền
            if (UserSession.VaiTro != "HR" && UserSession.VaiTro != "QuanLy")
            {
                MessageBox.Show("Chỉ HR hoặc Quản lý mới có quyền mở khóa lịch.", "Không có quyền", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var result = MessageBox.Show(
                $"Mở khóa lịch tuần này cho nhân viên {cboNhanVien.Text}?\n\n" +
                "Thao tác này sẽ được ghi log.",
                "Xác nhận mở khóa tuần",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (result != DialogResult.Yes) return;

            try
            {
                using (var conn = new SqlConnection(_connectionString))
                using (var cmd = new SqlCommand("dbo.sp_LichPhanCa_MoKhoaTuan", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@MaNV", _selectedMaNV);
                    cmd.Parameters.AddWithValue("@NgayBatDau", _currentWeekStart);
                    cmd.Parameters.AddWithValue("@MaNguoiDung", UserSession.MaNguoiDung);

                    conn.Open();
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            int soLich = reader.GetInt32(0);
                            MessageBox.Show($"Đã mở khóa {soLich} lịch trong tuần.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }

                LoadWeekSchedule();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi mở khóa tuần: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (_selectedMaNV == 0)
            {
                MessageBox.Show("Vui lòng chọn nhân viên.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int saved = 0;
            int errors = 0;

            try
            {
                using (var conn = new SqlConnection(_connectionString))
                {
                    conn.Open();

                    for (int i = 0; i < dgvLichTuan.Rows.Count; i++)
                    {
                        var row = dgvLichTuan.Rows[i];
                        if (row.ReadOnly) continue; // Bỏ qua dòng đã khóa

                        DateTime ngay = _currentWeekStart.AddDays(i);
                        object maLichObj = row.Cells["colMaLich"].Value;
                        object maCaObj = row.Cells["colCa"].Value;
                        string ghiChu = row.Cells["colGhiChu"].Value?.ToString() ?? "";

                        // Nếu không chọn ca, xóa lịch (nếu có)
                        if (maCaObj == null || maCaObj == DBNull.Value)
                        {
                            if (maLichObj != null && maLichObj != DBNull.Value)
                            {
                                // Xóa lịch
                                using (var cmd = new SqlCommand("dbo.sp_LichPhanCa_Delete", conn))
                                {
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.Parameters.AddWithValue("@Id", Convert.ToInt32(maLichObj));
                                    try
                                    {
                                        cmd.ExecuteNonQuery();
                                        saved++;
                                    }
                                    catch (SqlException ex)
                                    {
                                        errors++;
                                        MessageBox.Show($"Lỗi xóa lịch ngày {ngay:dd/MM}: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    }
                                }
                            }
                            continue;
                        }

                        int maCa = Convert.ToInt32(maCaObj);

                        // Insert hoặc Update
                        if (maLichObj == null || maLichObj == DBNull.Value)
                        {
                            // Insert
                            using (var cmd = new SqlCommand("dbo.sp_LichPhanCa_Insert", conn))
                            {
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.Parameters.AddWithValue("@MaNV", _selectedMaNV);
                                cmd.Parameters.AddWithValue("@Ngay", ngay);
                                cmd.Parameters.AddWithValue("@MaCa", maCa);
                                cmd.Parameters.AddWithValue("@TrangThai", "DuKien");
                                cmd.Parameters.AddWithValue("@GhiChu", ghiChu);
                                var outParam = cmd.Parameters.Add("@MaLich_OUT", SqlDbType.Int);
                                outParam.Direction = ParameterDirection.Output;

                                try
                                {
                                    cmd.ExecuteNonQuery();
                                    row.Cells["colMaLich"].Value = outParam.Value;
                                    saved++;
                                }
                                catch (SqlException ex)
                                {
                                    errors++;
                                    MessageBox.Show($"Lỗi thêm lịch ngày {ngay:dd/MM}: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                }
                            }
                        }
                        else
                        {
                            // Update
                            using (var cmd = new SqlCommand("dbo.sp_LichPhanCa_Update", conn))
                            {
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.Parameters.AddWithValue("@Id", Convert.ToInt32(maLichObj));
                                cmd.Parameters.AddWithValue("@MaCa", maCa);
                                cmd.Parameters.AddWithValue("@TrangThai", DBNull.Value);
                                cmd.Parameters.AddWithValue("@GhiChu", ghiChu);

                                try
                                {
                                    cmd.ExecuteNonQuery();
                                    saved++;
                                }
                                catch (SqlException ex)
                                {
                                    errors++;
                                    MessageBox.Show($"Lỗi cập nhật lịch ngày {ngay:dd/MM}: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                }
                            }
                        }
                    }
                }

                if (errors == 0)
                {
                    MessageBox.Show($"Đã lưu {saved} thay đổi thành công.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadWeekSchedule();
                }
                else
                {
                    MessageBox.Show($"Đã lưu {saved} thay đổi, {errors} lỗi.", "Hoàn thành", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    LoadWeekSchedule();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi lưu lịch: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            LoadWeekSchedule();
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
