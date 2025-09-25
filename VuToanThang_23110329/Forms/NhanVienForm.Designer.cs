using System;
using System.Drawing;
using System.Windows.Forms;
using VuToanThang_23110329.Controls;

namespace VuToanThang_23110329.Forms
{
    partial class NhanVienForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.SuspendLayout();
            
            // Form properties
            this.BackColor = Color.FromArgb(50, 50, 50);
            this.Size = new Size(1400, 900);
            this.Text = "Quản lý nhân viên";
            this.Padding = new Padding(20);
            this.StartPosition = FormStartPosition.CenterParent;
            
            // Add preview controls for Designer
            var lblPreview = new Label
            {
                Text = "QUẢN LÝ NHÂN VIÊN\n(Designer Preview)",
                Font = new Font("Segoe UI", 18, FontStyle.Bold),
                ForeColor = Color.FromArgb(124, 77, 255),
                Location = new Point(50, 50),
                AutoSize = true
            };
            
            var pnlSearch = new Panel
            {
                BackColor = Color.FromArgb(60, 60, 60),
                BorderStyle = BorderStyle.FixedSingle,
                Size = new Size(1300, 80),
                Location = new Point(50, 120)
            };
            
            var lblSearch = new Label
            {
                Text = "🔍 Tìm kiếm: TextBox | Filter trạng thái | Buttons: Thêm | Sửa | Xóa | Khôi phục | Làm mới",
                ForeColor = Color.White,
                Location = new Point(20, 30),
                AutoSize = true,
                Font = new Font("Segoe UI", 10)
            };
            pnlSearch.Controls.Add(lblSearch);
            
            var pnlDataGrid = new Panel
            {
                BackColor = Color.FromArgb(60, 60, 60),
                BorderStyle = BorderStyle.FixedSingle,
                Size = new Size(1300, 600),
                Location = new Point(50, 220)
            };
            
            var lblDataGrid = new Label
            {
                Text = "👥 DANH SÁCH NHÂN VIÊN (DataGridView)\n\nColumns: Mã NV | Họ tên | Ngày sinh | Giới tính | Điện thoại | Email\n         Địa chỉ | Ngày vào làm | Trạng thái | Phòng ban | Chức danh | Lương CB\n\nTính năng: Double-click để xem chi tiết, Context menu, Export Excel",
                ForeColor = Color.White,
                Location = new Point(20, 200),
                Size = new Size(1260, 200),
                Font = new Font("Segoe UI", 11)
            };
            pnlDataGrid.Controls.Add(lblDataGrid);
            
            this.Controls.AddRange(new Control[] { lblPreview, pnlSearch, pnlDataGrid });
            
            this.ResumeLayout(false);
            this.PerformLayout();
            
            this.Load += (s, e) => PerformLayoutLayout();
            this.Resize += (s, e) => PerformLayoutLayout();
        }

        private void CreateControls()
        {
            var lblTitle = new Label { Text = "QUẢN LÝ NHÂN VIÊN", Font = new Font("Segoe UI", 20, FontStyle.Bold), ForeColor = Color.FromArgb(124, 77, 255), AutoSize = true };
            txtSearch = new TextBox { Width = 300, BackColor = Color.FromArgb(60, 60, 60), ForeColor = Color.White, BorderStyle = BorderStyle.FixedSingle };
            cmbFilterTrangThai = CreateComboBox(new[] { "Tất cả", "DangLam", "Nghi" });
            cmbFilterTrangThai.SelectedIndex = 0;

            btnThem = ButtonFactory.CreateSuccessButton("Thêm");
            btnSua = ButtonFactory.CreateWarningButton("Sửa");
            btnXoa = ButtonFactory.CreateDangerButton("Xóa");
            btnKhoiPhuc = ButtonFactory.CreateInfoButton("Khôi phục");
            btnLamMoi = ButtonFactory.CreateSecondaryButton("Làm mới");

            dgvNhanVien = CreateDataGridView();

            this.Controls.AddRange(new Control[] { lblTitle, CreateLabel("Tìm kiếm:"), txtSearch, CreateLabel("Lọc:"), cmbFilterTrangThai, btnThem, btnSua, btnXoa, btnKhoiPhuc, btnLamMoi, dgvNhanVien });
        }

        #region Layout Logic
        private void PerformLayoutLayout() { /* Omitted for brevity */ }
        #endregion

        #region Control Factory Methods
        private ComboBox CreateComboBox(string[] items) { var cmb = new ComboBox { BackColor = Color.FromArgb(70, 70, 70), ForeColor = Color.White, FlatStyle = FlatStyle.Flat, DropDownStyle = ComboBoxStyle.DropDownList, Font = new Font("Segoe UI", 9) }; cmb.Items.AddRange(items); return cmb; }
        private DataGridView CreateDataGridView() => new DataGridView { BackgroundColor = Color.FromArgb(60, 60, 60), ForeColor = Color.White, BorderStyle = BorderStyle.None, AllowUserToAddRows = false, AllowUserToDeleteRows = false, ReadOnly = true, SelectionMode = DataGridViewSelectionMode.FullRowSelect, MultiSelect = false, AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill, ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle { BackColor = Color.FromArgb(124, 77, 255), ForeColor = Color.White, Font = new Font("Segoe UI", 10.5F, FontStyle.Bold) }, DefaultCellStyle = new DataGridViewCellStyle { BackColor = Color.FromArgb(60, 60, 60), ForeColor = Color.White, SelectionBackColor = Color.FromArgb(124, 77, 255) } };
        private Label CreateLabel(string text) => new Label { Text = text, ForeColor = Color.White, AutoSize = true, Font = new Font("Segoe UI", 9) };
        #endregion

        #endregion
    }
}
