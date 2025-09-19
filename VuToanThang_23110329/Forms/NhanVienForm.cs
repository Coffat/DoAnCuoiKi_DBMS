using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using VuToanThang_23110329.Models;
using VuToanThang_23110329.Repositories;

namespace VuToanThang_23110329.Forms
{
    public partial class NhanVienForm : Form
    {
        private readonly NhanVienRepository _nhanVienRepository;
        private readonly AuthRepository _authRepository;
        private NhanVien _currentNhanVien;
        private bool _isEditing = false;

        public NhanVienForm()
        {
            InitializeComponent();
            _nhanVienRepository = new NhanVienRepository();
            _authRepository = new AuthRepository();
            InitializeForm();
        }

        private void InitializeComponent()
        {
            this.BackColor = Color.FromArgb(50, 50, 50);
            this.Size = new Size(1200, 800);
            this.Text = "Quản lý nhân viên";
            
            // Create a simple layout with DataGridView and basic controls
            var dgv = new DataGridView
            {
                Dock = DockStyle.Fill,
                BackgroundColor = Color.FromArgb(60, 60, 60),
                ForeColor = Color.White,
                BorderStyle = BorderStyle.None,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                ReadOnly = true,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = false
            };
            
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(124, 77, 255);
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10.5F, FontStyle.Bold);
            dgv.DefaultCellStyle.BackColor = Color.FromArgb(60, 60, 60);
            dgv.DefaultCellStyle.ForeColor = Color.White;
            dgv.DefaultCellStyle.SelectionBackColor = Color.FromArgb(124, 77, 255);
            
            this.Controls.Add(dgv);
            
            var titleLabel = new Label
            {
                Text = "Quản lý nhân viên",
                Font = new Font("Segoe UI", 18, FontStyle.Bold),
                ForeColor = Color.White,
                Location = new Point(20, 20),
                AutoSize = true
            };
            
            this.Controls.Add(titleLabel);
            titleLabel.BringToFront();
        }

        private void InitializeForm()
        {
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                var nhanViens = CurrentUser.IsHR ? 
                    _nhanVienRepository.GetAll() : 
                    _nhanVienRepository.GetByRLS();
                
                // Since we don't have the DataGridView properly configured yet,
                // we'll just show a message for now
                ShowMessage($"Đã tải {nhanViens.Count} nhân viên", "Thông báo", MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                ShowMessage($"Lỗi tải dữ liệu: {ex.Message}", "Lỗi", MessageBoxIcon.Error);
            }
        }

        private void ShowMessage(string message, string title, MessageBoxIcon icon)
        {
            MessageBox.Show(message, title, MessageBoxButtons.OK, icon);
        }

        // Placeholder methods for events
        private void txtSearch_TextChanged(object sender, EventArgs e) { }
        private void dgvNhanVien_SelectionChanged(object sender, EventArgs e) { }
        private void btnCreateAccount_CheckedChanged(object sender, EventArgs e) { }
        private void Button_Click(object sender, EventArgs e) { }
    }
}
