using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using Guna.UI2.WinForms;

namespace VuToanThang_23110329.Forms
{
    public partial class frmXemDonCuaToi : Form
    {
        private string connectionString;
        private int currentMaNV;

        public frmXemDonCuaToi()
        {
            InitializeComponent();
            InitializeConnectionString();
        }

        private void InitializeConnectionString()
        {
            var cs = System.Configuration.ConfigurationManager.ConnectionStrings["HrDb"];
            if (cs == null)
            {
                MessageBox.Show("Không tìm thấy chuỗi kết nối 'HrDb' trong App.config.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                connectionString = string.Empty;
            }
            else
            {
                connectionString = cs.ConnectionString;
            }
            currentMaNV = UserSession.MaNV > 0 ? UserSession.MaNV : 1;
        }

        private void frmXemDonCuaToi_Load(object sender, EventArgs e)
        {
            SetupComboBoxes();
            LoadData();
        }

        private void SetupComboBoxes()
        {
            cmbTrangThai.Items.Clear();
            cmbTrangThai.Items.Add("Tất cả");
            cmbTrangThai.Items.Add("Chờ duyệt");
            cmbTrangThai.Items.Add("Đã duyệt");
            cmbTrangThai.Items.Add("Từ chối");
            cmbTrangThai.SelectedIndex = 0;

            cmbLoaiDon.Items.Clear();
            cmbLoaiDon.Items.Add("Tất cả");
            cmbLoaiDon.Items.Add("Nghỉ phép");
            cmbLoaiDon.Items.Add("Làm thêm giờ");
            cmbLoaiDon.SelectedIndex = 0;
        }

        private void LoadData()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    
                    // ✅ OPTIMIZED: Sử dụng vw_DonTu_ChiTiet thay vì raw SQL
                    string query = @"
                        SELECT 
                            MaDon,
                            CASE Loai 
                                WHEN 'NGHI' THEN N'Nghỉ phép'
                                WHEN 'OT' THEN N'Làm thêm giờ'
                            END as LoaiDon,
                            TuLuc,
                            DenLuc,
                            SoGio,
                            LyDo,
                            CASE TrangThai
                                WHEN 'ChoDuyet' THEN N'Chờ duyệt'
                                WHEN 'DaDuyet' THEN N'Đã duyệt'
                                WHEN 'TuChoi' THEN N'Từ chối'
                            END as TrangThai,
                            TenNguoiDuyet as NguoiDuyet
                        FROM dbo.vw_DonTu_ChiTiet
                        WHERE MaNV = @MaNV";

                    // Thêm filter theo trạng thái
                    if (cmbTrangThai.SelectedIndex > 0)
                    {
                        string trangThai = cmbTrangThai.SelectedIndex == 1 ? "ChoDuyet" :
                                          cmbTrangThai.SelectedIndex == 2 ? "DaDuyet" : "TuChoi";
                        query += " AND TrangThai = @TrangThai";
                    }

                    // Thêm filter theo loại đơn
                    if (cmbLoaiDon.SelectedIndex > 0)
                    {
                        string loai = cmbLoaiDon.SelectedIndex == 1 ? "NGHI" : "OT";
                        query += " AND Loai = @Loai";
                    }

                    query += " ORDER BY MaDon DESC";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaNV", currentMaNV);

                        if (cmbTrangThai.SelectedIndex > 0)
                        {
                            string trangThai = cmbTrangThai.SelectedIndex == 1 ? "ChoDuyet" :
                                              cmbTrangThai.SelectedIndex == 2 ? "DaDuyet" : "TuChoi";
                            cmd.Parameters.AddWithValue("@TrangThai", trangThai);
                        }

                        if (cmbLoaiDon.SelectedIndex > 0)
                        {
                            string loai = cmbLoaiDon.SelectedIndex == 1 ? "NGHI" : "OT";
                            cmd.Parameters.AddWithValue("@Loai", loai);
                        }

                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);

                        dgvDonTu.DataSource = dt;

                        if (dgvDonTu.Columns.Count > 0)
                        {
                            dgvDonTu.Columns["MaDon"].HeaderText = "Mã đơn";
                            dgvDonTu.Columns["LoaiDon"].HeaderText = "Loại đơn";
                            dgvDonTu.Columns["TuLuc"].HeaderText = "Từ lúc";
                            dgvDonTu.Columns["DenLuc"].HeaderText = "Đến lúc";
                            dgvDonTu.Columns["SoGio"].HeaderText = "Số giờ";
                            dgvDonTu.Columns["LyDo"].HeaderText = "Lý do";
                            dgvDonTu.Columns["TrangThai"].HeaderText = "Trạng thái";
                            dgvDonTu.Columns["NguoiDuyet"].HeaderText = "Người duyệt";

                            dgvDonTu.Columns["TuLuc"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";
                            dgvDonTu.Columns["DenLuc"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";
                            dgvDonTu.Columns["SoGio"].DefaultCellStyle.Format = "F2";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải dữ liệu: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
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

        // ✅ NEW METHOD: Sử dụng tvf_LichSuDonTuNhanVien để lấy lịch sử đơn từ
        public void LoadLichSuDonTuOptimized(int soThangGanNhat = 6)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = @"
                        SELECT 
                            MaDon,
                            CASE Loai 
                                WHEN 'NGHI' THEN N'Nghỉ phép'
                                WHEN 'OT' THEN N'Làm thêm giờ'
                            END as LoaiDon,
                            TuLuc,
                            DenLuc,
                            SoGio,
                            LyDo,
                            CASE TrangThai
                                WHEN 'ChoDuyet' THEN N'Chờ duyệt'
                                WHEN 'DaDuyet' THEN N'Đã duyệt'
                                WHEN 'TuChoi' THEN N'Từ chối'
                            END as TrangThai,
                            NguoiDuyet,
                            SoNgayTuTao
                        FROM dbo.tvf_LichSuDonTuNhanVien(@MaNV, @SoThangGanNhat)
                        ORDER BY NgayTao DESC";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaNV", currentMaNV);
                        cmd.Parameters.AddWithValue("@SoThangGanNhat", soThangGanNhat);

                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);

                        dgvDonTu.DataSource = dt;

                        // Setup column headers với cột mới
                        if (dgvDonTu.Columns.Count > 0)
                        {
                            dgvDonTu.Columns["MaDon"].HeaderText = "Mã đơn";
                            dgvDonTu.Columns["LoaiDon"].HeaderText = "Loại đơn";
                            dgvDonTu.Columns["TuLuc"].HeaderText = "Từ lúc";
                            dgvDonTu.Columns["DenLuc"].HeaderText = "Đến lúc";
                            dgvDonTu.Columns["SoGio"].HeaderText = "Số giờ";
                            dgvDonTu.Columns["LyDo"].HeaderText = "Lý do";
                            dgvDonTu.Columns["TrangThai"].HeaderText = "Trạng thái";
                            dgvDonTu.Columns["NguoiDuyet"].HeaderText = "Người duyệt";
                            dgvDonTu.Columns["SoNgayTuTao"].HeaderText = "Số ngày từ tạo";

                            dgvDonTu.Columns["SoGio"].DefaultCellStyle.Format = "F2";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải lịch sử đơn từ: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
