using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using Guna.UI2.WinForms;

namespace VuToanThang_23110329.Forms
{
    public partial class frmPhongBan_ChucVu : Form
    {
        private string connectionString;
        private DataTable dtPhongBan;
        private DataTable dtChucVu;
        private bool editingPhongBan = false;
        private bool editingChucVu = false;

        public frmPhongBan_ChucVu()
        {
            InitializeComponent();
            InitializeConnectionString();
        }

        private void frmPhongBan_ChucVu_Load(object sender, EventArgs e)
        {
            SetupDataGridViews();
            LoadData();
        }

        private void btnThemPhongBan_Click(object sender, EventArgs e)
        {
            txtMaPhongBan.Text = "";
            txtTenPhongBan.Text = "";
            txtMoTaPhongBan.Text = "";
            editingPhongBan = true;
        }

        private void btnSuaPhongBan_Click(object sender, EventArgs e)
        {
            if (dgvPhongBan.CurrentRow == null)
            {
                MessageBox.Show("Chọn một phòng ban để sửa.");
                return;
            }
            editingPhongBan = true;
            DataRowView row = dgvPhongBan.CurrentRow.DataBoundItem as DataRowView;
            if (row != null)
            {
                txtMaPhongBan.Text = row["MaPhongBan"].ToString();
                txtTenPhongBan.Text = row["TenPhongBan"].ToString();
                txtMoTaPhongBan.Text = row["MoTa"].ToString();
            }
        }

        private void btnXoaPhongBan_Click(object sender, EventArgs e)
        {
            if (dgvPhongBan.CurrentRow == null)
            {
                MessageBox.Show("Chọn một phòng ban để xóa.");
                return;
            }
            int ma = Convert.ToInt32(dgvPhongBan.CurrentRow.Cells["MaPhongBan"].Value);
            if (MessageBox.Show("Xóa phòng ban này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("DELETE FROM dbo.PhongBan WHERE MaPhongBan = @Ma", conn))
                {
                    cmd.Parameters.AddWithValue("@Ma", ma);
                    cmd.ExecuteNonQuery();
                }
            }
            LoadData();
        }

        private void btnThemChucVu_Click(object sender, EventArgs e)
        {
            txtMaChucVu.Text = "";
            txtTenChucVu.Text = "";
            txtMoTaChucVu.Text = "";
            editingChucVu = true;
        }

        private void btnSuaChucVu_Click(object sender, EventArgs e)
        {
            if (dgvChucVu.CurrentRow == null)
            {
                MessageBox.Show("Chọn một chức vụ để sửa.");
                return;
            }
            editingChucVu = true;
            DataRowView row = dgvChucVu.CurrentRow.DataBoundItem as DataRowView;
            if (row != null)
            {
                txtMaChucVu.Text = row["MaChucVu"].ToString();
                txtTenChucVu.Text = row["TenChucVu"].ToString();
                txtMoTaChucVu.Text = row["MoTa"].ToString();
            }
        }

        private void btnXoaChucVu_Click(object sender, EventArgs e)
        {
            if (dgvChucVu.CurrentRow == null)
            {
                MessageBox.Show("Chọn một chức vụ để xóa.");
                return;
            }
            int ma = Convert.ToInt32(dgvChucVu.CurrentRow.Cells["MaChucVu"].Value);
            if (MessageBox.Show("Xóa chức vụ này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("DELETE FROM dbo.ChucVu WHERE MaChucVu = @Ma", conn))
                {
                    cmd.Parameters.AddWithValue("@Ma", ma);
                    cmd.ExecuteNonQuery();
                }
            }
            LoadData();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (editingPhongBan)
            {
                if (string.IsNullOrWhiteSpace(txtTenPhongBan.Text)) { MessageBox.Show("Nhập tên phòng ban."); return; }
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    if (string.IsNullOrWhiteSpace(txtMaPhongBan.Text))
                    {
                        using (SqlCommand cmd = new SqlCommand("INSERT INTO dbo.PhongBan(TenPhongBan, MoTa, KichHoat) VALUES(@Ten,@MoTa,1)", conn))
                        {
                            cmd.Parameters.AddWithValue("@Ten", txtTenPhongBan.Text.Trim());
                            cmd.Parameters.AddWithValue("@MoTa", (object)txtMoTaPhongBan.Text ?? DBNull.Value);
                            cmd.ExecuteNonQuery();
                        }
                    }
                    else
                    {
                        using (SqlCommand cmd = new SqlCommand("UPDATE dbo.PhongBan SET TenPhongBan=@Ten, MoTa=@MoTa WHERE MaPhongBan=@Ma", conn))
                        {
                            cmd.Parameters.AddWithValue("@Ten", txtTenPhongBan.Text.Trim());
                            cmd.Parameters.AddWithValue("@MoTa", (object)txtMoTaPhongBan.Text ?? DBNull.Value);
                            cmd.Parameters.AddWithValue("@Ma", Convert.ToInt32(txtMaPhongBan.Text));
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
                editingPhongBan = false;
                LoadData();
                return;
            }

            if (editingChucVu)
            {
                if (string.IsNullOrWhiteSpace(txtTenChucVu.Text)) { MessageBox.Show("Nhập tên chức vụ."); return; }
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    if (string.IsNullOrWhiteSpace(txtMaChucVu.Text))
                    {
                        using (SqlCommand cmd = new SqlCommand("INSERT INTO dbo.ChucVu(TenChucVu, MoTa, KichHoat) VALUES(@Ten,@MoTa,1)", conn))
                        {
                            cmd.Parameters.AddWithValue("@Ten", txtTenChucVu.Text.Trim());
                            cmd.Parameters.AddWithValue("@MoTa", (object)txtMoTaChucVu.Text ?? DBNull.Value);
                            cmd.ExecuteNonQuery();
                        }
                    }
                    else
                    {
                        using (SqlCommand cmd = new SqlCommand("UPDATE dbo.ChucVu SET TenChucVu=@Ten, MoTa=@MoTa WHERE MaChucVu=@Ma", conn))
                        {
                            cmd.Parameters.AddWithValue("@Ten", txtTenChucVu.Text.Trim());
                            cmd.Parameters.AddWithValue("@MoTa", (object)txtMoTaChucVu.Text ?? DBNull.Value);
                            cmd.Parameters.AddWithValue("@Ma", Convert.ToInt32(txtMaChucVu.Text));
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
                editingChucVu = false;
                LoadData();
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            editingPhongBan = false;
            editingChucVu = false;
            ClearForm();
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            // TODO: Search logic here
        }

        private void tabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            // TODO: Tab changed logic here
        }

        private void LoadData()
        {
            if (string.IsNullOrEmpty(connectionString)) return;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlDataAdapter da = new SqlDataAdapter("SELECT MaPhongBan, TenPhongBan, MoTa, KichHoat FROM dbo.PhongBan ORDER BY TenPhongBan", conn))
                {
                    dtPhongBan = new DataTable();
                    da.Fill(dtPhongBan);
                    dgvPhongBan.DataSource = dtPhongBan;
                }
                using (SqlDataAdapter da = new SqlDataAdapter("SELECT MaChucVu, TenChucVu, MoTa, KichHoat FROM dbo.ChucVu ORDER BY TenChucVu", conn))
                {
                    dtChucVu = new DataTable();
                    da.Fill(dtChucVu);
                    dgvChucVu.DataSource = dtChucVu;
                }
            }
        }

        private void ClearForm()
        {
            txtMaPhongBan.Text = "";
            txtTenPhongBan.Text = "";
            txtMoTaPhongBan.Text = "";
            txtMaChucVu.Text = "";
            txtTenChucVu.Text = "";
            txtMoTaChucVu.Text = "";
        }

        private void SetupDataGridViews()
        {
            // Phòng ban
            dgvPhongBan.AutoGenerateColumns = false;
            dgvPhongBan.Columns.Clear();
            dgvPhongBan.Columns.Add(new DataGridViewTextBoxColumn { Name = "MaPhongBan", HeaderText = "Mã", DataPropertyName = "MaPhongBan", Width = 80 });
            dgvPhongBan.Columns.Add(new DataGridViewTextBoxColumn { Name = "TenPhongBan", HeaderText = "Tên phòng ban", DataPropertyName = "TenPhongBan", Width = 250 });
            dgvPhongBan.Columns.Add(new DataGridViewTextBoxColumn { Name = "MoTa", HeaderText = "Mô tả", DataPropertyName = "MoTa", Width = 400 });
            dgvPhongBan.Columns.Add(new DataGridViewCheckBoxColumn { Name = "KichHoat", HeaderText = "Kích hoạt", DataPropertyName = "KichHoat", Width = 90 });

            // Chức vụ
            dgvChucVu.AutoGenerateColumns = false;
            dgvChucVu.Columns.Clear();
            dgvChucVu.Columns.Add(new DataGridViewTextBoxColumn { Name = "MaChucVu", HeaderText = "Mã", DataPropertyName = "MaChucVu", Width = 80 });
            dgvChucVu.Columns.Add(new DataGridViewTextBoxColumn { Name = "TenChucVu", HeaderText = "Tên chức vụ", DataPropertyName = "TenChucVu", Width = 250 });
            dgvChucVu.Columns.Add(new DataGridViewTextBoxColumn { Name = "MoTa", HeaderText = "Mô tả", DataPropertyName = "MoTa", Width = 400 });
            dgvChucVu.Columns.Add(new DataGridViewCheckBoxColumn { Name = "KichHoat", HeaderText = "Kích hoạt", DataPropertyName = "KichHoat", Width = 90 });
        }

        private void InitializeConnectionString()
        {
            try
            {
                var cs = System.Configuration.ConfigurationManager.ConnectionStrings["HrDb"];
                connectionString = cs?.ConnectionString ?? "";
            }
            catch
            {
                connectionString = "";
            }
        }
    }
}
