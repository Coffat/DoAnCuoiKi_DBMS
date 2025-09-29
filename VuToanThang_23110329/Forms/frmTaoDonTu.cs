using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using Guna.UI2.WinForms;

namespace VuToanThang_23110329.Forms
{
    public partial class frmTaoDonTu : Form
    {
        private string connectionString;
        private int currentMaNV;

        public frmTaoDonTu()
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
            // Lấy MaNV từ session
            currentMaNV = UserSession.MaNV > 0 ? UserSession.MaNV : 1;
        }

        private void frmTaoDonTu_Load(object sender, EventArgs e)
        {
            SetupComboBox();
            SetupDateTimePickers();
            ClearForm();
        }

        private void SetupComboBox()
        {
            cmbLoaiDon.Items.Clear();
            cmbLoaiDon.Items.Add("NGHI - Nghỉ phép");
            cmbLoaiDon.Items.Add("OT - Làm thêm giờ");
            cmbLoaiDon.SelectedIndex = 0;
        }

        private void SetupDateTimePickers()
        {
            dtpTuLuc.Format = DateTimePickerFormat.Custom;
            dtpTuLuc.CustomFormat = "dd/MM/yyyy HH:mm";
            dtpTuLuc.ShowUpDown = false;
            dtpTuLuc.Value = DateTime.Now;

            dtpDenLuc.Format = DateTimePickerFormat.Custom;
            dtpDenLuc.CustomFormat = "dd/MM/yyyy HH:mm";
            dtpDenLuc.ShowUpDown = false;
            dtpDenLuc.Value = DateTime.Now.AddHours(8);
        }

        private void cmbLoaiDon_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Nếu chọn NGHI thì disable txtSoGio, nếu OT thì enable
            bool isOT = cmbLoaiDon.SelectedIndex == 1;
            txtSoGio.Enabled = isOT;
            txtSoGio.ReadOnly = !isOT;
            
            if (!isOT)
            {
                txtSoGio.Text = "";
                txtSoGio.PlaceholderText = "Tự động tính";
            }
            else
            {
                txtSoGio.PlaceholderText = "Nhập số giờ OT";
                CalculateHours();
            }
        }

        private void dtpTuLuc_ValueChanged(object sender, EventArgs e)
        {
            // Đảm bảo DenLuc >= TuLuc
            if (dtpDenLuc.Value < dtpTuLuc.Value)
            {
                dtpDenLuc.Value = dtpTuLuc.Value.AddHours(1);
            }
            CalculateHours();
        }

        private void dtpDenLuc_ValueChanged(object sender, EventArgs e)
        {
            // Đảm bảo DenLuc >= TuLuc
            if (dtpDenLuc.Value < dtpTuLuc.Value)
            {
                MessageBox.Show("Thời gian kết thúc phải sau thòi gian bắt đầu!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dtpDenLuc.Value = dtpTuLuc.Value.AddHours(1);
            }
            CalculateHours();
        }

        private void CalculateHours()
        {
            TimeSpan duration = dtpDenLuc.Value - dtpTuLuc.Value;
            double hours = duration.TotalHours;
            
            if (cmbLoaiDon.SelectedIndex == 1) // OT
            {
                txtSoGio.Text = hours.ToString("F2");
            }
        }

        private void btnTaoDon_Click(object sender, EventArgs e)
        {
            if (!ValidateInput())
                return;
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    
                    // Sử dụng stored procedure sp_DonTu_Insert
                    string loai = cmbLoaiDon.SelectedIndex == 0 ? "NGHI" : "OT";
                    decimal soGio = 0;
                    if (!string.IsNullOrWhiteSpace(txtSoGio.Text))
                    {
                        decimal.TryParse(txtSoGio.Text, out soGio);
                    }
                    
                    using (SqlCommand cmd = new SqlCommand("dbo.sp_DonTu_Insert", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@MaNV", currentMaNV);
                        cmd.Parameters.AddWithValue("@Loai", loai);
                        cmd.Parameters.AddWithValue("@TuLuc", dtpTuLuc.Value);
                        cmd.Parameters.AddWithValue("@DenLuc", dtpDenLuc.Value);
                        cmd.Parameters.AddWithValue("@SoGio", soGio > 0 ? (object)soGio : DBNull.Value);
                        cmd.Parameters.AddWithValue("@LyDo", txtLyDo.Text.Trim());

                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Tạo đơn thành công! Đơn đang chờ duyệt.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ClearForm();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tạo đơn: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ValidateInput()
        {
            // Kiểm tra loại đơn
            if (cmbLoaiDon.SelectedIndex < 0)
            {
                MessageBox.Show("Vui lòng chọn loại đơn!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbLoaiDon.Focus();
                return false;
            }

            // Kiểm tra thòi gian
            if (dtpDenLuc.Value <= dtpTuLuc.Value)
            {
                MessageBox.Show("Thòi gian kết thúc phải sau thòi gian bắt đầu!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dtpDenLuc.Focus();
                return false;
            }

            // Kiểm tra số giờ cho OT
            if (cmbLoaiDon.SelectedIndex == 1) // OT
            {
                if (string.IsNullOrEmpty(txtSoGio.Text))
                {
                    MessageBox.Show("Vui lòng nhập số giờ OT!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtSoGio.Focus();
                    return false;
                }

                if (!decimal.TryParse(txtSoGio.Text, out decimal soGio) || soGio <= 0)
                {
                    MessageBox.Show("Số giờ OT không hợp lệ!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtSoGio.Focus();
                    return false;
                }
            }

            // Kiểm tra lý do
            if (string.IsNullOrWhiteSpace(txtLyDo.Text))
            {
                MessageBox.Show("Vui lòng nhập lý do!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtLyDo.Focus();
                return false;
            }

            return true;
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có chắc muốn hủy tạo đơn?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                ClearForm();
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            ClearForm();
        }

        private void ClearForm()
        {
            cmbLoaiDon.SelectedIndex = 0;
            dtpTuLuc.Value = DateTime.Now;
            dtpDenLuc.Value = DateTime.Now.AddHours(8);
            txtSoGio.Text = "";
            txtLyDo.Text = "";
            txtLyDo.Focus();
        }
    }
}
