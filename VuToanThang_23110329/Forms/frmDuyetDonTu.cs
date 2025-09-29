using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using Guna.UI2.WinForms;
using VuToanThang_23110329;

namespace VuToanThang_23110329.Forms
{
    public partial class frmDuyetDonTu : Form
    {
        private string connectionString;
        private string currentUserRole;
        private int currentUserId;
        
        public frmDuyetDonTu()
        {
            InitializeComponent();
            InitializeConnectionString();
        }

        private void InitializeConnectionString()
        {
            var cs = System.Configuration.ConfigurationManager.ConnectionStrings["HrDb"];
            if (cs == null)
            {
                MessageBox.Show("Không tìm thấy chuỗi kết nối 'HrDb' trong App.config. Vui lòng kiểm tra cấu hình.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                connectionString = string.Empty;
            }
            else
            {
                connectionString = cs.ConnectionString;
            }
            
            // ✅ FIXED: Lấy thông tin từ UserSession
            if (!UserSession.IsLoggedIn)
            {
                MessageBox.Show("Vui lòng đăng nhập để sử dụng chức năng này.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.Close();
                return;
            }
            
            currentUserRole = UserSession.VaiTro;
            currentUserId = UserSession.MaNguoiDung;
        }

        private void frmDuyetDonTu_Load(object sender, EventArgs e)
        {
            SetupDataGridView();
            LoadComboBoxData();
            LoadData();
            ConfigureRoleCapabilities();
        }

        private void SetupDataGridView()
        {
            dgvDonTu.Columns.Clear();
            dgvDonTu.Columns.Add("MaDon", "Mã đơn");
            dgvDonTu.Columns.Add("TenNhanVien", "Nhân viên");
            dgvDonTu.Columns.Add("Loai", "Loại đơn");
            dgvDonTu.Columns.Add("TuLuc", "Từ lúc");
            dgvDonTu.Columns.Add("DenLuc", "Đến lúc");
            dgvDonTu.Columns.Add("SoGio", "Số giờ");
            dgvDonTu.Columns.Add("LyDo", "Lý do");
            dgvDonTu.Columns.Add("TrangThai", "Trạng thái");
            dgvDonTu.Columns.Add("NgayTao", "Ngày tạo");

            // Set column widths
            dgvDonTu.Columns["MaDon"].Width = 80;
            dgvDonTu.Columns["TenNhanVien"].Width = 150;
            dgvDonTu.Columns["Loai"].Width = 80;
            dgvDonTu.Columns["TuLuc"].Width = 130;
            dgvDonTu.Columns["DenLuc"].Width = 130;
            dgvDonTu.Columns["SoGio"].Width = 80;
            dgvDonTu.Columns["LyDo"].Width = 200;
            dgvDonTu.Columns["TrangThai"].Width = 100;
            dgvDonTu.Columns["NgayTao"].Width = 130;
        }

        private void LoadComboBoxData()
        {
            // Load status filter
            cmbTrangThai.Items.Clear();
            cmbTrangThai.Items.Add("Tất cả");
            cmbTrangThai.Items.Add("Chờ duyệt");
            cmbTrangThai.Items.Add("Đã duyệt");
            cmbTrangThai.Items.Add("Từ chối");
            cmbTrangThai.SelectedIndex = 0;

            // Load request type filter
            cmbLoaiDon.Items.Clear();
            cmbLoaiDon.Items.Add("Tất cả");
            cmbLoaiDon.Items.Add("Nghỉ phép");
            cmbLoaiDon.Items.Add("Tăng ca");
            cmbLoaiDon.SelectedIndex = 0;
        }

        private void ConfigureRoleCapabilities()
        {
            // Only HR and QuanLy can approve/reject requests
            bool canApprove = currentUserRole == "HR" || currentUserRole == "QuanLy";
            btnDuyet.Enabled = canApprove;
            btnTuChoi.Enabled = canApprove;
        }

        private void LoadData()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = @"
                        SELECT dt.MaDon, nv.HoTen as TenNhanVien, dt.Loai, dt.TuLuc, dt.DenLuc, 
                               dt.SoGio, dt.LyDo, dt.TrangThai, dt.TuLuc as NgayTao
                        FROM dbo.DonTu dt
                        INNER JOIN dbo.NhanVien nv ON dt.MaNV = nv.MaNV
                        WHERE 1=1";

                    // Apply filters
                    if (cmbTrangThai.SelectedIndex > 0)
                    {
                        string statusFilter = cmbTrangThai.SelectedItem.ToString();
                        switch (statusFilter)
                        {
                            case "Chờ duyệt":
                                query += " AND dt.TrangThai = N'ChoDuyet'";
                                break;
                            case "Đã duyệt":
                                query += " AND dt.TrangThai = N'DaDuyet'";
                                break;
                            case "Từ chối":
                                query += " AND dt.TrangThai = N'TuChoi'";
                                break;
                        }
                    }

                    if (cmbLoaiDon.SelectedIndex > 0)
                    {
                        string typeFilter = cmbLoaiDon.SelectedItem.ToString();
                        switch (typeFilter)
                        {
                            case "Nghỉ phép":
                                query += " AND dt.Loai = N'NGHI'";
                                break;
                            case "Tăng ca":
                                query += " AND dt.Loai = N'OT'";
                                break;
                        }
                    }

                    if (!string.IsNullOrEmpty(txtTimKiem.Text))
                    {
                        query += " AND nv.HoTen LIKE N'%' + @TimKiem + '%'";
                    }

                    query += " ORDER BY dt.TuLuc DESC";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        if (!string.IsNullOrEmpty(txtTimKiem.Text))
                        {
                            cmd.Parameters.AddWithValue("@TimKiem", txtTimKiem.Text);
                        }

                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);

                        dgvDonTu.Rows.Clear();
                        foreach (DataRow row in dt.Rows)
                        {
                            dgvDonTu.Rows.Add(
                                row["MaDon"],
                                row["TenNhanVien"],
                                row["Loai"].ToString() == "NGHI" ? "Nghỉ phép" : "Tăng ca",
                                Convert.ToDateTime(row["TuLuc"]).ToString("dd/MM/yyyy HH:mm"),
                                Convert.ToDateTime(row["DenLuc"]).ToString("dd/MM/yyyy HH:mm"),
                                row["SoGio"],
                                row["LyDo"],
                                GetStatusText(row["TrangThai"].ToString()),
                                Convert.ToDateTime(row["NgayTao"]).ToString("dd/MM/yyyy")
                            );
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải dữ liệu: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string GetStatusText(string status)
        {
            switch (status)
            {
                case "ChoDuyet": return "Chờ duyệt";
                case "DaDuyet": return "Đã duyệt";
                case "TuChoi": return "Từ chối";
                default: return status;
            }
        }

        private void btnDuyet_Click(object sender, EventArgs e)
        {
            ProcessRequest("DaDuyet", "Duyệt đơn từ");
        }

        private void btnTuChoi_Click(object sender, EventArgs e)
        {
            ProcessRequest("TuChoi", "Từ chối đơn từ");
        }

        private void ProcessRequest(string newStatus, string actionName)
        {
            if (dgvDonTu.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn đơn từ cần xử lý!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int maDon = Convert.ToInt32(dgvDonTu.SelectedRows[0].Cells["MaDon"].Value);
            string currentStatus = dgvDonTu.SelectedRows[0].Cells["TrangThai"].Value.ToString();

            if (currentStatus != "Chờ duyệt")
            {
                MessageBox.Show("Chỉ có thể xử lý đơn từ đang chờ duyệt!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult result = MessageBox.Show($"Bạn có chắc chắn muốn {actionName.ToLower()} này?", 
                actionName, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        conn.Open();
                        using (SqlCommand cmd = new SqlCommand("dbo.sp_DuyetDonTu", conn))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@MaDon", maDon);
                            cmd.Parameters.AddWithValue("@MaNguoiDuyet", currentUserId);
                            cmd.Parameters.AddWithValue("@ChapNhan", newStatus == "DaDuyet" ? 1 : 0);

                            cmd.ExecuteNonQuery();
                            MessageBox.Show($"{actionName} thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadData();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi khi {actionName.ToLower()}: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        private void cmbTrangThai_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        private void cmbLoaiDon_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        private void dgvDonTu_SelectionChanged(object sender, EventArgs e)
        {
            // Enable/disable buttons based on selection and status
            bool hasSelection = dgvDonTu.SelectedRows.Count > 0;
            bool canProcess = false;

            if (hasSelection)
            {
                string status = dgvDonTu.SelectedRows[0].Cells["TrangThai"].Value.ToString();
                canProcess = status == "Chờ duyệt" && (currentUserRole == "HR" || currentUserRole == "QuanLy");
            }

            btnDuyet.Enabled = canProcess;
            btnTuChoi.Enabled = canProcess;
        }

        private void dgvDonTu_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
