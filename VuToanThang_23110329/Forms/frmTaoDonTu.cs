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
        private int currentUserId;
        
        public frmTaoDonTu()
        {
            InitializeComponent();
            InitializeConnectionString();
        }

        private void InitializeConnectionString()
        {
            connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            // Get current user info (assuming stored in static class)
            currentUserId = 1; // TODO: Get from session
        }

        private void frmTaoDonTu_Load(object sender, EventArgs e)
        {
            LoadComboBoxData();
            SetDefaultValues();
            ConfigureForm();
        }

        private void LoadComboBoxData()
        {
            cmbLoaiDon.Items.Clear();
            cmbLoaiDon.Items.Add("Nghỉ phép");
            cmbLoaiDon.Items.Add("Tăng ca");
            cmbLoaiDon.SelectedIndex = 0;
        }

        private void SetDefaultValues()
        {
            dtpTuLuc.Value = DateTime.Now;
            dtpDenLuc.Value = DateTime.Now.AddDays(1);
            txtSoGio.Text = "";
            txtLyDo.Text = "";
        }

        private void ConfigureForm()
        {
            // Initially hide hours field for leave requests
            UpdateFormBasedOnRequestType();
        }

        private void UpdateFormBasedOnRequestType()
        {
            bool isOvertime = cmbLoaiDon.SelectedIndex == 1; // Tăng ca
            lblSoGio.Visible = isOvertime;
            txtSoGio.Visible = isOvertime;
            
            if (isOvertime)
            {
                txtSoGio.PlaceholderText = "Nhập số giờ tăng ca";
            }
            else
            {
                txtLyDo.PlaceholderText = "Nhập lý do nghỉ phép";
            }
        }

        private void btnTaoDon_Click(object sender, EventArgs e)
        {
            if (ValidateForm())
            {
                CreateRequest();
            }
        }

        private bool ValidateForm()
        {
            // Validate request type
            if (cmbLoaiDon.SelectedIndex < 0)
            {
                MessageBox.Show("Vui lòng chọn loại đơn!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // Validate dates
            if (dtpTuLuc.Value >= dtpDenLuc.Value)
            {
                MessageBox.Show("Thời gian bắt đầu phải trước thời gian kết thúc!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // Validate hours for overtime
            if (cmbLoaiDon.SelectedIndex == 1) // Tăng ca
            {
                if (string.IsNullOrEmpty(txtSoGio.Text))
                {
                    MessageBox.Show("Vui lòng nhập số giờ tăng ca!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }

                if (!decimal.TryParse(txtSoGio.Text, out decimal hours) || hours <= 0)
                {
                    MessageBox.Show("Số giờ phải là số dương!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }

            // Validate reason
            if (string.IsNullOrEmpty(txtLyDo.Text.Trim()))
            {
                MessageBox.Show("Vui lòng nhập lý do!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        private void CreateRequest()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = @"
                        INSERT INTO dbo.DonTu (MaNV, Loai, TuLuc, DenLuc, SoGio, LyDo, TrangThai)
                        VALUES (@MaNV, @Loai, @TuLuc, @DenLuc, @SoGio, @LyDo, N'ChoDuyet')";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaNV", currentUserId);
                        cmd.Parameters.AddWithValue("@Loai", cmbLoaiDon.SelectedIndex == 0 ? "NGHI" : "OT");
                        cmd.Parameters.AddWithValue("@TuLuc", dtpTuLuc.Value);
                        cmd.Parameters.AddWithValue("@DenLuc", dtpDenLuc.Value);
                        
                        if (cmbLoaiDon.SelectedIndex == 1 && !string.IsNullOrEmpty(txtSoGio.Text))
                        {
                            cmd.Parameters.AddWithValue("@SoGio", decimal.Parse(txtSoGio.Text));
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@SoGio", DBNull.Value);
                        }
                        
                        cmd.Parameters.AddWithValue("@LyDo", txtLyDo.Text.Trim());

                        cmd.ExecuteNonQuery();
                        
                        MessageBox.Show("Tạo đơn từ thành công! Đơn từ đã được gửi để chờ duyệt.", 
                            "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        
                        ClearForm();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tạo đơn từ: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn hủy tạo đơn từ?", 
                "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            
            if (result == DialogResult.Yes)
            {
                this.Close();
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
            dtpDenLuc.Value = DateTime.Now.AddDays(1);
            txtSoGio.Text = "";
            txtLyDo.Text = "";
            UpdateFormBasedOnRequestType();
        }

        private void cmbLoaiDon_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateFormBasedOnRequestType();
        }

        private void dtpTuLuc_ValueChanged(object sender, EventArgs e)
        {
            // Auto-adjust end date if it's before start date
            if (dtpDenLuc.Value <= dtpTuLuc.Value)
            {
                dtpDenLuc.Value = dtpTuLuc.Value.AddDays(1);
            }
        }

        private void dtpDenLuc_ValueChanged(object sender, EventArgs e)
        {
            // Validate that end date is after start date
            if (dtpDenLuc.Value <= dtpTuLuc.Value)
            {
                MessageBox.Show("Thời gian kết thúc phải sau thời gian bắt đầu!", 
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dtpDenLuc.Value = dtpTuLuc.Value.AddDays(1);
            }
        }

        private void txtSoGio_TextChanged(object sender, EventArgs e)
        {
            // Validate numeric input
            if (!string.IsNullOrEmpty(txtSoGio.Text))
            {
                if (!decimal.TryParse(txtSoGio.Text, out decimal hours) || hours < 0)
                {
                    // Remove invalid characters
                    txtSoGio.Text = txtSoGio.Text.Substring(0, txtSoGio.Text.Length - 1);
                    txtSoGio.SelectionStart = txtSoGio.Text.Length;
                }
            }
        }

        private void txtLyDo_TextChanged(object sender, EventArgs e)
        {
            // Limit text length
            if (txtLyDo.Text.Length > 255)
            {
                txtLyDo.Text = txtLyDo.Text.Substring(0, 255);
                txtLyDo.SelectionStart = txtLyDo.Text.Length;
            }
        }
    }
}
