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
                    
                    // ✅ IMPROVED: Sử dụng TVF thay vì View để có filter thời gian và SoNgayTuTao
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
                        WHERE (@TrangThai = N'Tất cả' OR TrangThai = @TrangThaiParam)
                        AND (@Loai = N'Tất cả' OR Loai = @LoaiParam)
                        ORDER BY MaDon DESC";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaNV", currentMaNV);
                        cmd.Parameters.AddWithValue("@SoThangGanNhat", 6); // Lấy 6 tháng gần nhất

                        // Xử lý filter trạng thái
                        string selectedTrangThai = cmbTrangThai.SelectedItem?.ToString() ?? "Tất cả";
                        cmd.Parameters.AddWithValue("@TrangThai", selectedTrangThai);
                        
                        string trangThaiParam = selectedTrangThai == "Chờ duyệt" ? "ChoDuyet" :
                                               selectedTrangThai == "Đã duyệt" ? "DaDuyet" :
                                               selectedTrangThai == "Từ chối" ? "TuChoi" : "";
                        cmd.Parameters.AddWithValue("@TrangThaiParam", trangThaiParam);

                        // Xử lý filter loại đơn
                        string selectedLoai = cmbLoaiDon.SelectedItem?.ToString() ?? "Tất cả";
                        cmd.Parameters.AddWithValue("@Loai", selectedLoai);
                        
                        string loaiParam = selectedLoai == "Nghỉ phép" ? "NGHI" :
                                          selectedLoai == "Làm thêm giờ" ? "OT" : "";
                        cmd.Parameters.AddWithValue("@LoaiParam", loaiParam);

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
                            dgvDonTu.Columns["SoNgayTuTao"].HeaderText = "Số ngày từ tạo";

                            dgvDonTu.Columns["TuLuc"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";
                            dgvDonTu.Columns["DenLuc"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";
                            dgvDonTu.Columns["SoGio"].DefaultCellStyle.Format = "F2";
                            dgvDonTu.Columns["SoNgayTuTao"].DefaultCellStyle.Format = "N0";
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

        // ✅ FLEXIBLE METHOD: Cho phép thay đổi số tháng lấy dữ liệu
        public void LoadDataWithTimeRange(int soThangGanNhat)
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
                        WHERE (@TrangThai = N'Tất cả' OR TrangThai = @TrangThaiParam)
                        AND (@Loai = N'Tất cả' OR Loai = @LoaiParam)
                        ORDER BY MaDon DESC";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaNV", currentMaNV);
                        cmd.Parameters.AddWithValue("@SoThangGanNhat", soThangGanNhat);

                        // Xử lý filter trạng thái
                        string selectedTrangThai = cmbTrangThai.SelectedItem?.ToString() ?? "Tất cả";
                        cmd.Parameters.AddWithValue("@TrangThai", selectedTrangThai);
                        
                        string trangThaiParam = selectedTrangThai == "Chờ duyệt" ? "ChoDuyet" :
                                               selectedTrangThai == "Đã duyệt" ? "DaDuyet" :
                                               selectedTrangThai == "Từ chối" ? "TuChoi" : "";
                        cmd.Parameters.AddWithValue("@TrangThaiParam", trangThaiParam);

                        // Xử lý filter loại đơn
                        string selectedLoai = cmbLoaiDon.SelectedItem?.ToString() ?? "Tất cả";
                        cmd.Parameters.AddWithValue("@Loai", selectedLoai);
                        
                        string loaiParam = selectedLoai == "Nghỉ phép" ? "NGHI" :
                                          selectedLoai == "Làm thêm giờ" ? "OT" : "";
                        cmd.Parameters.AddWithValue("@LoaiParam", loaiParam);

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
                            dgvDonTu.Columns["SoNgayTuTao"].HeaderText = "Số ngày từ tạo";

                            dgvDonTu.Columns["TuLuc"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";
                            dgvDonTu.Columns["DenLuc"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";
                            dgvDonTu.Columns["SoGio"].DefaultCellStyle.Format = "F2";
                            dgvDonTu.Columns["SoNgayTuTao"].DefaultCellStyle.Format = "N0";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải dữ liệu: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}
