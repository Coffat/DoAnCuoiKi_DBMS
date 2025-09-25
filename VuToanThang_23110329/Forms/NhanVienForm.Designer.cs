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
            this.BackColor = Color.FromArgb(50, 50, 50);
            this.Size = new Size(1400, 900);
            this.Text = "Quản lý nhân viên";
            this.Padding = new Padding(20);

            CreateControls();
            
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
